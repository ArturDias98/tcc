using FuzzyClient.Service.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpcUaClient;
using TCC.Shared.Models;
using TCC.Shared.Models.Metrics;
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
    private ECalculateStatus _status = ECalculateStatus.Idle;
    private List<double> _levels = [];
    private List<double> _time = [];

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var settings = await settingsService
            .GetSettingsAsync(stoppingToken)
            .ConfigureAwait(false);

        var errorTag = settings.OpcModel.ErrorTag;
        var outputTag = settings.OpcModel.OutputTag;
        var rateTag = settings.OpcModel.RateTag;
        var levelTag = settings.OpcModel.LevelTag;
        var time = DateTime.Now;

        while (!stoppingToken.IsCancellationRequested)
        {
            if (!monitoringService.IsMonitoring)
            {
                if (_status == ECalculateStatus.Calculating)
                {
                    var metrics = await apiService.GetMetricsAsync(
                            monitoringService.Setpoint,
                            0.02,
                            _levels,
                            _time,
                            stoppingToken)
                        .ConfigureAwait(false);

                    await publisher.PublishAsync(
                        metrics,
                        stoppingToken)
                        .ConfigureAwait(false);
                    
                    _status = ECalculateStatus.Idle;
                    _levels = [];
                    _time = [];
                }

                await Task.Delay(500, stoppingToken).ConfigureAwait(false);
                time = DateTime.Now;
                continue;
            }

            try
            {
                _status = ECalculateStatus.Calculating;
                var read = await opcClient.ReadAsync(
                    [errorTag, rateTag, levelTag],
                    stoppingToken)
                    .ConfigureAwait(false);

                var parse = read
                    .Select(i => double.TryParse(i.ToString(), out var result) ? result : 0)
                    .ToList();

                var error = parse[0];
                var rate = parse[1];
                var level = parse[2];
                var interval = DateTime.Now - time;
                var seconds = interval.TotalSeconds;
                
                _levels.Add(level);
                _time.Add(seconds);
                
                var calculate = await apiService.CalculateAsync(
                    error,
                    rate,
                    stoppingToken)
                    .ConfigureAwait(false);

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
                        stoppingToken))
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing OPC tags");
            }
        }
    }
}

internal enum ECalculateStatus
{
    Idle,
    Calculating
}