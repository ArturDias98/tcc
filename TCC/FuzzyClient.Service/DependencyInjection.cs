using FuzzyClient.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FuzzyClient.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        var apiSettings = new ApiSettings
        {
            Port = 50123,
            DebugMode = false
        };
        
        return services
            .Configure<ApiSettings>(opt =>
            {
                opt.Port = apiSettings.Port;
                opt.DebugMode = apiSettings.DebugMode;
            })
            .Configure<ApiOptions>(opt => opt.EndPoint = $"http://localhost:{apiSettings.Port}/api/")
            .AddTransient<IApiService, ApiService>()
            .AddHostedService<ApiHostedService>();
    }
}