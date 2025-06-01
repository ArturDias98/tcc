using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using TCC.Shared.Models;

namespace TCC.UI.RazorLib.Pages;

public partial class Home : IHandle<DataModel>, IDisposable
{
    private PlotModel _plotModel = new();
    private double _points;
    
    private void LoadPlotModel()
    {
        _plotModel = new PlotModel
        {
            Title = "Fuzzy Plot",
            PlotAreaBorderColor = OxyColors.Black,
            TextColor = OxyColors.Black
        };
        
        var axisX = new LinearAxis()
        {
            Position = AxisPosition.Bottom,
            Minimum = 0,
            Maximum = 1000,
            TicklineColor = OxyColors.Black,
            IsPanEnabled = false,
            IsZoomEnabled = false,
            StringFormat = "N2"
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
        };

        _plotModel.Axes.Add(axisX);
        _plotModel.Axes.Add(axisY);
        _plotModel.Series.Add(new LineSeries());
    }
    
    [Inject] private IEventAggregator EventAggregator { get; set; } = null!;
    
    public Task HandleAsync(DataModel message)
    {
        var series = _plotModel.Series[0] as LineSeries ?? throw new Exception("Series is not a LineSeries");
        var xAxis = _plotModel.Axes[0] as LinearAxis ?? throw new Exception("X Axis is not a LinearAxis");
        
        series.Points.Add(new DataPoint(_points++, message.Level));
        
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