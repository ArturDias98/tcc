using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TCC.Console;
using TCC.Core;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, configureBuilder) =>
    {
        configureBuilder.SetBasePath(context.HostingEnvironment.ContentRootPath);
        configureBuilder.AddJsonFile("appsettings.json", false);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddCoreServices();
        services.AddHostedService<CalculateHostedService>();
    }).ConfigureLogging(cfg => cfg.AddConsole())
    .Build();

await host.RunAsync();