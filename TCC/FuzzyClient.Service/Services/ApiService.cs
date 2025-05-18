using System.Net.Http.Json;
using FuzzyClient.Service.Services.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FuzzyClient.Service.Services;

internal sealed class ApiService(
    ILogger<ApiService> logger,
    HttpClient client) : IApiService
{
    public async Task<bool> IsHealthAsync(CancellationToken cancellation = default)
    {
        try
        {
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

    public async Task<double> CalculateAsync(double level, double rate, CancellationToken cancellation = default)
    {
        try
        {
            var response = await client.PostAsJsonAsync(
                "valve-opening",
                new FuzzyRequestModel()
                {
                    Level = level,
                    Rate = rate
                }, 
                cancellation);

            var content = await response
                .Content
                .ReadFromJsonAsync<FuzzyResponseModel>(cancellation) ?? new FuzzyResponseModel();
            
            return content.ValveOpening;
        }
        catch (Exception e)
        {
            logger.LogError("Error on calculate fuzzy logic: {Message}", e.Message);
            return 0;
        }
    }
}