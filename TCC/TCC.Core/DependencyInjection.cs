using FuzzyClient.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpcUaClient;

namespace TCC.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddOpcUaClient()
            .AddApiServices(configuration);
    }
}