using System.Diagnostics;
using System.Net.Http.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FuzzyClient.Service;

internal sealed class ApiHostedService(
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

    private async Task<bool> IsApiRunning(CancellationToken token)
    {
        logger.LogInformation("Checking if fuzzy-api is running...");

        try
        {
            using var client = new HttpClient();

            var response = await client.GetFromJsonAsync<Dictionary<string, string>>(
                $"http://localhost:{Settings.Port}/api/v1/health",
                token);

            return response?["status"] == "healthy";
        }
        catch (Exception e)
        {
            logger.LogError("Error on checking fuzzy-api: {Message}", e.Message);

            return false;
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (await IsApiRunning(cancellationToken))
            {
                return;
            }

            _apiProcess = CreateProcess();
            var success = _apiProcess.Start();
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
        }
    }
}