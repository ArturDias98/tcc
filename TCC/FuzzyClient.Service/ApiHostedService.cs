using System.Diagnostics;
using FuzzyClient.Service.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FuzzyClient.Service;

internal sealed class ApiHostedService(
    IApiService apiService,
    ILogger<ApiHostedService> logger,
    IOptions<ApiSettings> options) : IHostedService
{
    private ApiSettings Settings => options.Value;
    private Process? _apiProcess;

    private Process CreateProcess()
    {
        var fileName = Path.Combine(Directory.GetCurrentDirectory(),
            "fuzzy-api",
            "main.exe");
        var startInfo = new ProcessStartInfo
        {
            FileName = fileName,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            EnvironmentVariables =
            {
                ["FLASK_DEBUG"] = Settings.DebugMode.ToString(),
                ["FLASK_PORT"] = Settings.Port.ToString()
            }
        };

        return new Process { StartInfo = startInfo };
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (await apiService.IsHealthAsync(cancellationToken))
            {
                return;
            }

            _apiProcess = CreateProcess();
            
            if (_apiProcess.Start())
            {
                logger.LogInformation("fuzzy-api started.");
            }
            else
            {
                logger.LogError("fuzzy-api failed to start.");
            }
        }
        catch (Exception e)
        {
            logger.LogError("Error on starting fuzzy-api: {Message}", e.Message);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping fuzzy-api...");

        try
        {
            if (_apiProcess is { HasExited: false })
            {
                _apiProcess.Kill();
                await _apiProcess.WaitForExitAsync(cancellationToken);
                _apiProcess.Dispose();
                logger.LogInformation("fuzzy-api stopped.");
            }
        }
        catch (Exception e)
        {
            logger.LogError("Error on stopping fuzzy-api: {Message}", e.Message);
            return;
        }
        
        logger.LogInformation("fuzzy-api stoped.");
    }
}