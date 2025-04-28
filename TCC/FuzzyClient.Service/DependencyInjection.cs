using Microsoft.Extensions.DependencyInjection;

namespace FuzzyClient.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        return services
            .Configure<ApiSettings>(opt =>
            {
                opt.Port = 50123;
                opt.DebugMode = false;
            })
            .AddHostedService<ApiHostedService>();
    }
}