using FuzzyClient.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpcUaClient;
using TCC.Core.HostedServices;
using TCC.Shared.Services;

namespace TCC.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        var apiStatusService = new ApiStatusService();
        services.Configure<HostOptions>(opt =>
        {
            opt.ServicesStartConcurrently = true;
            opt.ServicesStopConcurrently = true;
        });
        
        return services
            .AddTransient<ISettingsService, SettingsService>()
            .AddSingleton(apiStatusService)
            .AddSingleton<IApiStatusService>(_ => apiStatusService)
            .AddOpcUaClient()
            .AddApiServices()
            .AddSingleton<IMonitoringService, MonitoringService>()
            .AddHostedService<OpcHostedService>()
            .AddHostedService<ApiStatusHostedService>()
            .AddHostedService<CalculateHostedService>();
    }
}