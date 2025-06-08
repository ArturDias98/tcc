using FuzzyClient.Service.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TCC.Shared.Services;

namespace TCC.Core.HostedServices;

internal class ApiStatusHostedService(
    IApiService apiService,
    ApiStatusService apiStatusService,
    ILogger<ApiStatusHostedService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var isHealth = await apiService.IsHealthAsync(stoppingToken);
                apiStatusService.ChangeConnectionStatus(isHealth);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking API health status");
                apiStatusService.ChangeConnectionStatus(false);
            }

            await Task.Delay(5000, stoppingToken); // Check every 5 seconds
        }
    }
}

internal sealed class ApiStatusService : IApiStatusService
{
    public void ChangeConnectionStatus(bool isConnected)
    {
        IsConnected = isConnected;
        OnConnectionChanged?.Invoke(this, isConnected);
    }
    
    public bool IsConnected { get; private set; }
    public event EventHandler<bool>? OnConnectionChanged;
}