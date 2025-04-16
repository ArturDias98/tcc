using Microsoft.Extensions.DependencyInjection;

namespace OpcUaClient;

public static class DependencyInjection
{
    public static IServiceCollection AddOpcUaClient(this IServiceCollection services)
    {
        return services.AddSingleton<IOpcClient, OpcClient>();
    }
}