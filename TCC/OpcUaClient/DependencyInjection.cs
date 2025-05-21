using Microsoft.Extensions.DependencyInjection;
using TCC.Shared.Services;

namespace OpcUaClient;

public static class DependencyInjection
{
    public static IServiceCollection AddOpcUaClient(this IServiceCollection services)
    {
        return services
            .AddSingleton<OpcClient>()
            .AddTransient<IOpcClient>(provider => provider.GetRequiredService<OpcClient>())
            .AddTransient<IOpcStatusService>(provider => provider.GetRequiredService<OpcClient>());
    }
}