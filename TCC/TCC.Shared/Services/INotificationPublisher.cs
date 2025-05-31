namespace TCC.Shared.Services;

public interface INotificationPublisher
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
}