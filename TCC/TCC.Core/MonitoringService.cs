using OpcUaClient;
using TCC.Shared.Services;

namespace TCC.Core;

internal sealed class MonitoringService(
    IOpcClient opcClient,
    ISettingsService settingsService) : IMonitoringService
{
    private CancellationTokenSource? _cancellationTokenSource;
    
    private bool ValidateMonitoring()
    {
        try
        {
            if (_cancellationTokenSource is null)
            {
                return false;
            }
            
            _cancellationTokenSource.Token.ThrowIfCancellationRequested();
            return true;
        }
        catch (OperationCanceledException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool IsMonitoring => ValidateMonitoring();
    
    public int IntervalSeconds { get; private set; }
    
    public double Setpoint { get; private set; }

    public async Task StartMonitoringAsync(
        double setpoint,
        int intervalSeconds,
        CancellationToken cancellationToken = default)
    {
        IntervalSeconds = intervalSeconds;
        Setpoint = setpoint;
        
        var settings = await settingsService
            .GetSettingsAsync(cancellationToken)
            .ConfigureAwait(false);

        var tag = settings.OpcModel.SetpointTag;
        
        await opcClient
            .WriteAsync(tag, setpoint, cancellationToken)
            .ConfigureAwait(false);
        
        _cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(intervalSeconds));
    }
}