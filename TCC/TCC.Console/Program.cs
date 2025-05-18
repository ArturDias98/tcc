using FuzzyClient.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpcUaClient;
using TCC.Console;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, configureBuilder) =>
    {
        configureBuilder.SetBasePath(context.HostingEnvironment.ContentRootPath);
        configureBuilder.AddJsonFile("appsettings.json", false);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddOpcUaClient();
        services.AddApiServices(context.Configuration);
        services.AddHostedService<CalculateHostedService>();
    }).ConfigureLogging(cfg => cfg.AddConsole())
    .Build();

await host.RunAsync();