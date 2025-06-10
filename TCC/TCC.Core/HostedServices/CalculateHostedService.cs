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

        var errorTag = settings.OpcModel.ErrorTag;
        var outputTag = settings.OpcModel.OutputTag;
        var rateTag = settings.OpcModel.RateTag;
        var levelTag = settings.OpcModel.LevelTag;

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
                    [errorTag, rateTag, levelTag],
                    stoppingToken);

                var parse = read
                    .Select(i => double.TryParse(i.ToString(), out var result) ? result : 0)
                    .ToList();

                var error = parse[0];
                var rate = parse[1];
                var level = parse[2];

                var calculate = await apiService.CalculateAsync(
                    error,
                    rate,
                    stoppingToken);

                await Task.WhenAll(opcClient.WriteAsync(
                        outputTag,
                        calculate,
                        stoppingToken),
                    publisher.PublishAsync(
                        new DataModel(
                            error,
                            level,
                            rate,
                            calculate,
                            DateTime.Now),
                        stoppingToken));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing OPC tags");
            }
        }
    }
}