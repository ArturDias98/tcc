using Microsoft.Extensions.DependencyInjection;
using TCC.Shared.Services;
using TCC.UI.RazorLib.Services;

namespace TCC.UI.RazorLib;

public static class DependencyInjection
{
    public static IServiceCollection AddUiServices(this IServiceCollection services)
    {
        return services
            .AddAntDesign()
            .AddOxyPlotBlazor()
            .AddEventAggregator()
            .AddSingleton<INotificationPublisher, NotificationPublisher>();
    }
}