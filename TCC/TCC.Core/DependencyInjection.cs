using FuzzyClient.Service;
using Microsoft.Extensions.DependencyInjection;
using OpcUaClient;
using TCC.Core.HostedServices;
using TCC.Shared.Services;

namespace TCC.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services
            .AddTransient<ISettingsService, SettingsService>()
            .AddSingleton<IApiStatusService, ApiStatusService>()
            .AddOpcUaClient()
            .AddApiServices()
            .AddSingleton<IMonitoringService, MonitoringService>()
            .AddHostedService<OpcHostedService>()
            .AddHostedService<ApiStatusHostedService>()
            .AddHostedService<CalculateHostedService>();
    }
}