using FuzzyClient.Service.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpcUaClient;
using TCC.Shared.Services;

namespace TCC.Core.HostedServices;

public class CalculateHostedService(
    IOpcClient opcClient,
    IApiService apiService,
    ISettingsService settingsService,
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
            try
            {
                var read = await opcClient.ReadAsync(
                    [levelTag, rateTag],
                    stoppingToken);

                var parse = read
                    .Select(i => double.TryParse(i.ToString(), out var result) ? result : 0)
                    .ToList();

                var calculate = await apiService.CalculateAsync(
                    parse[0],
                    parse[1],
                    stoppingToken);

                await opcClient.WriteAsync(
                    outputTag,
                    calculate,
                    stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing OPC tags");
            }
        }
    }
}