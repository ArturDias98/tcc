using FuzzyClient.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OpcUaClient;
using TCC.Console.Settings;

namespace TCC.Console;

public class CalculateHostedService(
    IOpcClient opcClient,
    IApiService apiService,
    IConfiguration configuration) : BackgroundService
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
                System.Console.WriteLine($"Read opc tags");
                var read = await opcClient.ReadAsync(
                    [opcSettings.LevelTag, opcSettings.RateTag],
                    stoppingToken);

                var parse = read
                    .Select(i => double.TryParse(i.ToString(), out var result) ? result : 0)
                    .ToList();

                System.Console.WriteLine($"Calculate output value");
                var calculate = await apiService.CalculateAsync(
                    parse[0],
                    parse[1],
                    stoppingToken);

                System.Console.WriteLine("Write output");
                await opcClient.WriteAsync(
                    opcSettings.OutputTag,
                    calculate,
                    stoppingToken);

                System.Console.WriteLine($"Write output value: {calculate}");
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Error on calculate: {e.Message}");
            }
        }
    }
}