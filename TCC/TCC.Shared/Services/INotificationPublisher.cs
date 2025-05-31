namespace TCC.Shared.Services;

public interface INotificationPublisher
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    
    void Subscribe(object subscriber);

    void Unsubscribe(object subscriber);
}