using System.Text;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;
using TCC.Shared.Services;
using TCC.UI.RazorLib.Events;

namespace TCC.UI.RazorLib.Shared;

public partial class Monitoring : ComponentBase
{
    private CancellationTokenSource _cancellationTokenSource = new();
    private readonly MonitoringModel _model = new();
    private bool _errorModal;
    private string _errorMessage = string.Empty;
    private bool _isMonitoring;

    private async Task StartMonitoring()
    {
        var builder = new StringBuilder();

        if (!ApiStatusService.IsConnected)
            builder.Append("<p>Api desconectada</p>");

        if (!OpcStatusService.IsConnected)
            builder.Append("<p>Opc desconectado</p>");

        _errorMessage = builder.ToString();
        var hasError = !string.IsNullOrWhiteSpace(_errorMessage);

        if (hasError)
        {
            _errorModal = true;
            StateHasChanged();
            return;
        }

        _cancellationTokenSource = new CancellationTokenSource();
        
        await EventAggregator.PublishAsync(new ClearPlotEvent(_model.IntervalSeconds));
        
        await MonitoringService.StartMonitoringAsync(
            _model.Setpoint,
            _model.IntervalSeconds,
            _cancellationTokenSource.Token);

        _isMonitoring = true;

        StateHasChanged();
    }

    private void StopMonitoring()
    {
        _isMonitoring = false;
        _cancellationTokenSource.Cancel();
        StateHasChanged();
    }

    [Inject] private IMonitoringService MonitoringService { get; set; } = null!;
    [Inject] private IApiStatusService ApiStatusService { get; set; } = null!;
    [Inject] private IOpcStatusService OpcStatusService { get; set; } = null!;
    [Inject] private IEventAggregator EventAggregator { get; set; } = null!;

    protected override void OnInitialized()
    {
        MonitoringService.OnMonitoringChanged += (_, value) =>
        {
            if (_isMonitoring == value)
                return;
            
            _isMonitoring = value;
            InvokeAsync(StateHasChanged);
        };
    }
}

internal class MonitoringModel
{
    public double Setpoint { get; set; } = 1.5;
    public int IntervalSeconds { get; set; } = 10;
}