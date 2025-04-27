using Microsoft.Extensions.DependencyInjection;

namespace FuzzyClient.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        return services
            .ConfigureOptions(new ApiSettings()
            {
                Port = 50123,
                DebugMode = false
            })
            .AddHostedService<ApiHostedService>();
    }
}