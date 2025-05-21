using FuzzyClient.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using TCC.Shared.Services;

namespace FuzzyClient.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services
            .AddHttpClient<IApiService, ApiService>((provider, client) =>
            {
                var service = provider.GetRequiredService<ISettingsService>();
                var settings = service.GetSettingsAsync().Result;
                client.BaseAddress = new Uri(settings.ApiModel.Endpoint);
            });
        return services;
    }
}