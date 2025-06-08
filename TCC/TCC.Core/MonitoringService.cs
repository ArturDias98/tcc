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
        bool isMonitoring;

        try
        {
            if (_cancellationTokenSource is null)
            {
                isMonitoring = false;
            }
            else
            {
                _cancellationTokenSource?.Token.ThrowIfCancellationRequested();
                isMonitoring = true;
            }
        }
        catch (OperationCanceledException)
        {
            isMonitoring = false;
        }
        catch (Exception)
        {
            isMonitoring = false;
        }

        OnMonitoringChanged?.Invoke(this, isMonitoring);
        return isMonitoring;
    }

    public bool IsMonitoring => ValidateMonitoring();

    public int IntervalSeconds { get; private set; }

    public double Setpoint { get; private set; }
    public event EventHandler<bool>? OnMonitoringChanged;

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

        _cancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(cancellationToken);

        _cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(intervalSeconds));
    }
}