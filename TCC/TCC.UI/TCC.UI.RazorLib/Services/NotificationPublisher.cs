using EventAggregator.Blazor;
using TCC.Shared.Services;

namespace TCC.UI.RazorLib.Services;

internal sealed class NotificationPublisher(
    IEventAggregator eventAggregator) : INotificationPublisher
{
    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        return eventAggregator.PublishAsync(message);
    }

    public void Subscribe(object subscriber)
    {
        eventAggregator.Subscribe(subscriber);
    }

    public void Unsubscribe(object subscriber)
    {
        eventAggregator.Unsubscribe(subscriber);
    }
}