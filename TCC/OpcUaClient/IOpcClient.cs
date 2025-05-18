namespace OpcUaClient;

public interface IOpcClient
{
    bool IsConnected { get; }

    event EventHandler<bool> OnConnectionChanged;
    event EventHandler<(string NodeId, object Value)> OnValueChanged;

    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
    Task WriteAsync(string nodeId, object value, CancellationToken cancellationToken = default);
    Task<List<object>> ReadAsync(List<string> ids, CancellationToken cancellationToken = default);
    Task ConfigureAsync(string endPoint, CancellationToken cancellationToken = default);

    void AddMonitoredItems(List<string> nodIds);
}