using FuzzyClient.Service;
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

var config = host.Services.GetRequiredService<IConfiguration>();

var opcSettings = new OpcSettings();
config.Bind("OpcSettings", opcSettings);

var service = host.Services.GetRequiredService<IOpcClient>();
await service.ConfigureAsync(opcSettings.Server);
await service.StartAsync();

service.AddMonitoredItems([
    opcSettings.InputTag, 
    opcSettings.OutputTag,
    opcSettings.SetpointTag
]);

service.OnValueChanged += (_, args) =>
{
    Console.WriteLine($"Tag: {args.NodeId}, Value: {args.Value}");
};

await host.RunAsync();