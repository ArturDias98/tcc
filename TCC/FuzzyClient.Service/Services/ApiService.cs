using System.Net.Http.Json;
using FuzzyClient.Service.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FuzzyClient.Service.Services;

internal sealed class ApiService(
    ILogger<ApiService> logger,
    IOptions<ApiOptions> options) : IApiService
{
    private ApiOptions Options => options.Value;

    public async Task<bool> IsHealthAsync(CancellationToken cancellation = default)
    {
        try
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(Options.EndPoint);

            var response = await client.GetFromJsonAsync<Dictionary<string, string>>(
                "health",
                cancellation);

            return response?["status"] == "healthy";
        }
        catch (Exception e)
        {
            logger.LogError("Error on check if api is healthy: {Message}", e.Message);
            return false;
        }
    }

    public async Task<double> CalculateAsync(double percentage, CancellationToken cancellation = default)
    {
        try
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(Options.EndPoint);

            var response = await client.PostAsJsonAsync(
                "valve-opening",
                new FuzzyRequestModel()
                {
                    Error = percentage
                }, 
                cancellation);

            var content = await response
                .Content
                .ReadFromJsonAsync<FuzzyResponseModel>(cancellation) ?? new FuzzyResponseModel();
            
            return content.Percentage;
        }
        catch (Exception e)
        {
            logger.LogError("Error on calculate fuzzy logic: {Message}", e.Message);
            return 0;
        }
    }
}