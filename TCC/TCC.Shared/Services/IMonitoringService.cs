namespace TCC.Shared.Services;

public interface IMonitoringService
{
    bool IsMonitoring { get; }
    int IntervalSeconds { get; }
    double Setpoint { get; }
    
    Task StartMonitoringAsync(double setpoint, int intervalSeconds, CancellationToken cancellationToken = default);
}