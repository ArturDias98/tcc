using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpcUaClient;
using TCC.Shared.Services;

namespace TCC.Core.HostedServices;

public class OpcHostedService(
    IOpcClient opcClient,
    ISettingsService settingsService,
    ILogger<OpcHostedService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (opcClient.IsConnected)
                {
                    await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
                    continue;
                }
                
                var settings = await settingsService
                    .GetSettingsAsync(stoppingToken)
                    .ConfigureAwait(false);

                logger.LogInformation("Starting OPC client with server: {Server}", settings.OpcModel.Server);

                await opcClient.ConfigureAsync(
                    settings.OpcModel.Server,
                    stoppingToken)
                    .ConfigureAwait(false);

                await opcClient
                    .StartAsync(stoppingToken)
                    .ConfigureAwait(false);

                logger.LogInformation("OPC client started");
            }
            catch (Exception e)
            {
                logger.LogError("Error starting OPC client: {Message}", e.ToString());
                await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
            }
        }
    }
}