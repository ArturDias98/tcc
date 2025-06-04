using Microsoft.AspNetCore.Components;

namespace TCC.UI.RazorLib.Shared;

public partial class Monitoring : ComponentBase
{
    private MonitoringModel _model = new();
    private bool _isMonitoring = false;
    
    private async Task StartMonitoring()
    {
        _isMonitoring = true;
        StateHasChanged();
    }
    
    private async Task StopMonitoring()
    {
        _isMonitoring = false;
        StateHasChanged();
    }
}

internal class MonitoringModel
{
    public double Setpoint { get; set; } = 1.5;
    public int IntervalSeconds { get; set; } = 10;
}