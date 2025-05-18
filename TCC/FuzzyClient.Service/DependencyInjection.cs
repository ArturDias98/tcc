using FuzzyClient.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FuzzyClient.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddHttpClient<IApiService, ApiService>(client =>
            {
                var address = configuration.GetConnectionString("Api")
                    ?? throw new Exception("Could not resolve api endpoint");
                client.BaseAddress = new Uri(address);
            });
        return services;
    }
}