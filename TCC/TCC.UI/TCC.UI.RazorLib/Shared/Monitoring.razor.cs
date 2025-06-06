using Microsoft.AspNetCore.Components;

namespace TCC.UI.RazorLib.Shared;

public partial class Monitoring : ComponentBase
{
    private MonitoringModel _model = new();
    private bool _isMonitoring = false;
    
    private Task StartMonitoring()
    {
        _isMonitoring = true;
        StateHasChanged();
        return Task.CompletedTask;
    }
    
    private Task StopMonitoring()
    {
        _isMonitoring = false;
        StateHasChanged();
        return Task.CompletedTask;
    }
}

internal class MonitoringModel
{
    public double Setpoint { get; set; } = 1.5;
    public int IntervalSeconds { get; set; } = 10;
}