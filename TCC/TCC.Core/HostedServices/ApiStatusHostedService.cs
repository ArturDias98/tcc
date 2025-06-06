using FuzzyClient.Service.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TCC.Shared.Services;

namespace TCC.Core.HostedServices;

public class ApiStatusHostedService(
    IApiService apiService,
    ILogger<ApiStatusHostedService> logger) : BackgroundService
{
    private readonly ApiStatusService _apiStatusService = new();
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var isHealth = await apiService.IsHealthAsync(stoppingToken);
                _apiStatusService.ChangeConnectionStatus(isHealth);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking API health status");
                _apiStatusService.ChangeConnectionStatus(false);
            }

            await Task.Delay(5000, stoppingToken); // Check every 5 seconds
        }
    }
}

internal sealed class ApiStatusService : IApiStatusService
{
    public void ChangeConnectionStatus(bool isConnected)
    {
        if (IsConnected == isConnected) return;

        IsConnected = isConnected;
        OnConnectionChanged?.Invoke(this, isConnected);
    }
    
    public bool IsConnected { get; private set; }
    public event EventHandler<bool>? OnConnectionChanged;
}