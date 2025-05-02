using FuzzyClient.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FuzzyClient.Service;

public static class DependencyInjection
{
    private static IServiceCollection ConfigureServicesInternal(
        this IServiceCollection services, 
        ApiSettings apiSettings)
    {
        return services
            .Configure<ApiSettings>(opt =>
            {
                opt.Port = apiSettings.Port;
                opt.DebugMode = apiSettings.DebugMode;
            })
            .Configure<ApiOptions>(opt => opt.EndPoint = $"http://localhost:{apiSettings.Port}/api/");
    }

    private static IServiceCollection AddApiServicesInternal(this IServiceCollection services)
    {
        return services            
            .AddTransient<IApiService, ApiService>()
            .AddHostedService<ApiHostedService>();
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        var apiSettings = new ApiSettings
        {
            Port = 50123,
            DebugMode = false
        };
        
        return services
            .ConfigureServicesInternal(apiSettings)
            .AddApiServicesInternal();
    }

    public static IServiceCollection AddApiServices(
        this IServiceCollection services, 
        Action<ApiSettings> configure)
    {
        ApiSettings apiSettings = new();
        configure(apiSettings);

        return services
            .ConfigureServicesInternal(apiSettings)
            .AddApiServicesInternal();
    }
}