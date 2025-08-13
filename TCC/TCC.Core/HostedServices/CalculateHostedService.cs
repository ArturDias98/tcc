using System.Diagnostics;
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
    private SettingsModel _settings = new();
    private List<double> _levels = [];
    private List<double> _time = [];
    private double _calculate;

    private async Task UpdateSettingsAsync(CancellationToken cancellationToken)
    {
        _settings = await settingsService
            .GetSettingsAsync(cancellationToken)
            .ConfigureAwait(false);
    }
    
    private async Task CalculateAsync(
        double error,
        double rate,
        CancellationToken cancellationToken)
    {
        _calculate = await apiService.CalculateAsync(
                error,
                rate,
                cancellationToken)
            .ConfigureAwait(false);
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var time = DateTime.Now;
        var stopwatch = new Stopwatch();
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!monitoringService.IsMonitoring)
            {
                if (_status == ECalculateStatus.Calculating)
                {
                    var metrics = await apiService.GetMetricsAsync(
                            monitoringService.Setpoint,
                            _settings.ApiModel.Tolerance,
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

                await Task
                    .Delay(500, stoppingToken)
                    .ConfigureAwait(false);
                
                time = DateTime.Now;
                
                continue;
            }

            try
            {
                if (_status != ECalculateStatus.Calculating)
                {
                    await UpdateSettingsAsync(stoppingToken)
                        .ConfigureAwait(false);
                }
                
                var errorTag = _settings.OpcModel.ErrorTag;
                var outputTag = _settings.OpcModel.OutputTag;
                var rateTag = _settings.OpcModel.RateTag;
                var levelTag = _settings.OpcModel.LevelTag;
                
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
                
                stopwatch.Reset();
                
                stopwatch.Start();

                await Task.WhenAll(
                    CalculateAsync(error, rate, stoppingToken),
                    Task.Delay(_settings.ApiModel.ApiDelay, stoppingToken))
                    .ConfigureAwait(false);
                
                stopwatch.Stop();
                
                var apiLatency = stopwatch.ElapsedMilliseconds;
                
                logger.LogInformation(
                    "Api execution time {ElapsedMilliseconds} ms",
                    apiLatency);
                
                await Task.WhenAll(opcClient.WriteAsync(
                        outputTag,
                        _calculate,
                        stoppingToken),
                    publisher.PublishAsync(
                        new DataModel(
                            error,
                            level,
                            rate,
                            _calculate,
                            DateTime.Now,
                            apiLatency),
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