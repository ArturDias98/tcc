using FuzzyClient.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpcUaClient;
using TCC.Console.Settings;

namespace TCC.Console;

public class CalculateHostedService(
    IOpcClient opcClient,
    IApiService apiService,
    IConfiguration configuration,
    ILogger<CalculateHostedService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var opcSettings = new OpcSettings();
        configuration.Bind("OpcSettings", opcSettings);

        await opcClient.ConfigureAsync(
            opcSettings.Server,
            stoppingToken);
        await opcClient.StartAsync(stoppingToken);

        opcClient.AddMonitoredItems([
            opcSettings.LevelTag,
            opcSettings.OutputTag,
            opcSettings.RateTag
        ]);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("Read opc tags");
                
                var read = await opcClient.ReadAsync(
                    [opcSettings.LevelTag, opcSettings.RateTag],
                    stoppingToken);

                var parse = read
                    .Select(i => double.TryParse(i.ToString(), out var result) ? result : 0)
                    .ToList();

                logger.LogInformation($"Calculate output value");
                
                var calculate = await apiService.CalculateAsync(
                    parse[0],
                    parse[1],
                    stoppingToken);

                logger.LogInformation("Write output");
                
                await opcClient.WriteAsync(
                    opcSettings.OutputTag,
                    calculate,
                    stoppingToken);

                logger.LogInformation("Write output value: {calculate}", calculate);
            }
            catch (Exception e)
            {
                logger.LogError("Error on calculate: {Message}", e.ToString());
            }
        }
    }
}