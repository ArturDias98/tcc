using FuzzyClient.Service.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpcUaClient;
using TCC.Shared.Models;
using TCC.Shared.Services;

namespace TCC.Core.HostedServices;

public class CalculateHostedService(
    IOpcClient opcClient,
    IApiService apiService,
    ISettingsService settingsService,
    INotificationPublisher publisher,
    IMonitoringService monitoringService,
    ILogger<CalculateHostedService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var settings = await settingsService
            .GetSettingsAsync(stoppingToken)
            .ConfigureAwait(false);

        await opcClient.ConfigureAsync(
            settings.OpcModel.Server,
            stoppingToken);
        await opcClient.StartAsync(stoppingToken);

        var levelTag = settings.OpcModel.LevelTag;
        var outputTag = settings.OpcModel.OutputTag;
        var rateTag = settings.OpcModel.RateTag;

        opcClient.AddMonitoredItems([
            levelTag,
            outputTag,
            rateTag
        ]);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (!monitoringService.IsMonitoring)
            {
                await Task.Delay(500, stoppingToken).ConfigureAwait(false);
                continue;
            }
            
            try
            {
                var read = await opcClient.ReadAsync(
                    [levelTag, rateTag],
                    stoppingToken);

                var parse = read
                    .Select(i => double.TryParse(i.ToString(), out var result) ? result : 0)
                    .ToList();

                var level = parse[0];
                var rate = parse[1];

                var calculate = await apiService.CalculateAsync(
                    level,
                    rate,
                    stoppingToken);

                await Task.WhenAll(opcClient.WriteAsync(
                        outputTag,
                        calculate,
                        stoppingToken),
                    publisher.PublishAsync(
                        new DataModel(
                            level,
                            rate,
                            calculate),
                        stoppingToken));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing OPC tags");
            }
        }
    }
}