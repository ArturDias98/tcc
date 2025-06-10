using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using TCC.Shared.Models;
using TCC.Shared.Services;
using TCC.UI.RazorLib.Events;

namespace TCC.UI.RazorLib.Pages;

public partial class Home : IHandle<DataModel>, IHandle<ClearPlotEvent>, IDisposable
{
    private PlotModel _plotModel = new();
    private double _rate = 0;
    private double _error = 0;
    private double _output = 0;
    
    private void LoadPlotModel()
    {
        _plotModel = new PlotModel
        {
            Title = "",
            PlotAreaBorderColor = OxyColors.Black,
            TextColor = OxyColors.Black
        };

        var axisX = new DateTimeAxis 
        {
            Position = AxisPosition.Bottom,
            Minimum = DateTimeAxis.ToDouble(DateTime.Now),
            Maximum =  DateTimeAxis.ToDouble(DateTime.Now.AddSeconds(10)),
            TicklineColor = OxyColors.Black,
            IsPanEnabled = false,
            IsZoomEnabled = false,
        };

        var axisY = new LinearAxis()
        {
            Position = AxisPosition.Left,
            Minimum = 0,
            Maximum = 2,
            TicklineColor = OxyColors.Black,
            IsPanEnabled = false,
            IsZoomEnabled = false,
            StringFormat = "N2",
            Title = "Level",
        };

        _plotModel.Axes.Add(axisX);
        _plotModel.Axes.Add(axisY);
        _plotModel.Series.Add(new LineSeries(){Title = "Level", Color = OxyColors.Blue});
        _plotModel.Series.Add(new LineSeries(){Title = "Setpoint", Color = OxyColors.Red});
    }

    [Inject] private IEventAggregator EventAggregator { get; set; } = null!;
    [Inject] private IMonitoringService MonitoringService { get; set; } = null!;

    public Task HandleAsync(DataModel message)
    {
        _rate = message.Rate;
        _error = message.Error;
        _output = message.Output;
        
        var levelSeries = _plotModel.Series[0] as LineSeries ?? throw new Exception("Series is not a LineSeries");
        var setpointSeries = _plotModel.Series[1] as LineSeries ?? throw new Exception("Series is not a LineSeries");

        levelSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(message.Timestamp), message.Level));
        setpointSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(message.Timestamp), MonitoringService.Setpoint));

        _plotModel.InvalidatePlot(true);

        return InvokeAsync(StateHasChanged);
    }
    
    public Task HandleAsync(ClearPlotEvent message)
    {
        _rate = 0;
        _error = 0;
        _output = 0;
        
        var levelSeries = _plotModel.Series[0] as LineSeries ?? throw new Exception("Series is not a LineSeries");
        levelSeries.Points.Clear();
        
        var setpointSeries = _plotModel.Series[1] as LineSeries ?? throw new Exception("Series is not a LineSeries");
        setpointSeries.Points.Clear();
        
        var xAxis = _plotModel.Axes[0] as DateTimeAxis ?? throw new Exception("X Axis is not a DateTimeAxis");
        xAxis.Minimum = DateTimeAxis.ToDouble(DateTime.Now);
        xAxis.Maximum = DateTimeAxis.ToDouble(DateTime.Now.AddSeconds(message.IntervalSeconds));
        
        _plotModel.InvalidatePlot(true);
        
        return InvokeAsync(StateHasChanged);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;

        LoadPlotModel();
        StateHasChanged();
    }

    protected override void OnInitialized()
    {
        EventAggregator.Subscribe(this);
    }

    public void Dispose()
    {
        EventAggregator.Unsubscribe(this);
        GC.SuppressFinalize(this);
    }
}