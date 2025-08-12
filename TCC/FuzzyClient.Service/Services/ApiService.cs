using System.Net.Http.Json;
using FuzzyClient.Service.Services.Models;
using Microsoft.Extensions.Logging;
using TCC.Shared.Models.Metrics;

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

    public async Task<MetricsModel> GetMetricsAsync(double reference, double tolerance, List<double> y, List<double> t, CancellationToken cancellation = default)
    {
        try
        {
            var response = await client.PostAsJsonAsync(
                "performance-metrics",
                new MetricsRequestModel()
                {
                    Reference = reference,
                    Tolerance = tolerance,
                    Values = y,
                    Time = t
                }, 
                cancellation);
            
            var content = await response
                .Content
                .ReadFromJsonAsync<MetricsResponseModel>(cancellation) ?? new MetricsResponseModel();
            
            return new MetricsModel()
            {
                Mse = content.MSE ?? 0,
                Overshoot = content.Overshoot ?? 0,
                SettlingTime = content.SettlingTime ?? 0
            };
        }
        catch (Exception e)
        {
            logger.LogError("Error on get metrics for reference: {Message}", e.Message);
            return new MetricsModel();
        }
    }
}