namespace TCC.Shared.Services;

public interface IMonitoringService
{
    bool IsMonitoring { get; }
    int IntervalSeconds { get; }
    double Setpoint { get; }
    
    event EventHandler<bool> OnMonitoringChanged;
    
    Task StartMonitoringAsync(double setpoint, int intervalSeconds, CancellationToken cancellationToken = default);
}