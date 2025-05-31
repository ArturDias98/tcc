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
            Maximum = 10,
            TicklineColor = OxyColors.Black,
            IsPanEnabled = false,
            IsZoomEnabled = false,
            StringFormat = "N2"
        };

        var axisY = new LinearAxis()
        {
            Position = AxisPosition.Left,
            Minimum = -1,
            Maximum = 1,
            TicklineColor = OxyColors.Black,
            IsPanEnabled = false,
            IsZoomEnabled = false,
            StringFormat = "N2",
        };

        _plotModel.Axes.Add(axisX);
        _plotModel.Axes.Add(axisY);
        _plotModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)")
        {
            TrackerFormatString = "{0}\n{1}: {2:0.000}\n{3}: {4:0.000}",
        });
    }
    
    [Inject] private IEventAggregator EventAggregator { get; set; } = null!;
    
    public Task HandleAsync(DataModel message)
    {
        return Task.CompletedTask;
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