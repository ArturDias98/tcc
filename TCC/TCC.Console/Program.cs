using FuzzyClient.Service;
using FuzzyClient.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpcUaClient;
using TCC.Console.Settings;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, configureBuilder) =>
    {
        configureBuilder.SetBasePath(context.HostingEnvironment.ContentRootPath);
        configureBuilder.AddJsonFile("appsettings.json", false);
    })
    .ConfigureServices(services =>
    {
        services.AddOpcUaClient();
        services.AddApiServices(config =>
        {
            config.Port = 50123;
            config.DebugMode = false;
        });
    })
    .Build();

var apiService = host.Services.GetRequiredService<IApiService>();

var config = host.Services.GetRequiredService<IConfiguration>();

var opcSettings = new OpcSettings();
config.Bind("OpcSettings", opcSettings);

var opcClient = host.Services.GetRequiredService<IOpcClient>();
await opcClient.ConfigureAsync(opcSettings.Server);
await opcClient.StartAsync();

opcClient.AddMonitoredItems([
    opcSettings.InputTag,
    opcSettings.OutputTag,
    opcSettings.SetpointTag
]);

var opcDictionary = new Dictionary<string, double>
{
    { opcSettings.InputTag, 0 },
    { opcSettings.OutputTag, 0 },
    { opcSettings.SetpointTag, 0 }
};

opcClient.OnValueChanged += (_, args) =>
{
    Console.WriteLine($"Tag: {args.NodeId}, Value: {args.Value}");
    if (double.TryParse(args.Value.ToString(), out var value))
    {
        opcDictionary[args.NodeId] = value;
    }

    var error = opcDictionary[opcSettings.SetpointTag] - opcDictionary[opcSettings.OutputTag];
    
    if (error < 0)
        error = 0;
    if (error > 100)
        error = 100;

    var output = apiService.CalculateAsync(error).Result;
    Console.WriteLine($"Write output {output}");
    opcClient.WriteAsync(opcSettings.InputTag, output).Wait();
};

await host.RunAsync();