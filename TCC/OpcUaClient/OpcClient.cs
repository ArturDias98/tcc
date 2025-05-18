using Microsoft.Extensions.Logging;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;

namespace OpcUaClient;

internal sealed class OpcClient(ILogger<OpcClient> logger) : IOpcClient
{
    private const int ReconnectPeriod = 10;

    private Session? _session;
    private SessionReconnectHandler? _reconnectHandler;
    private ApplicationConfiguration? _applicationConfiguration;
    private bool _isCertificated;
    private string _endPoint = string.Empty;

    private static void CertificateValidator_CertificateValidation(
        CertificateValidator sender,
        CertificateValidationEventArgs e)
    {
        e.Accept = true;
    }

    private void Session_KeepAlive(ISession sender, KeepAliveEventArgs e)
    {
        if (e.Status == null || !ServiceResult.IsNotGood(e.Status)) return;

        if (_reconnectHandler is not null) return;

        _reconnectHandler = new SessionReconnectHandler();
        IsConnected = false;

        OnConnectionChanged?.Invoke(this, IsConnected);
        _reconnectHandler.BeginReconnect(sender, ReconnectPeriod * 1000, Client_ReconnectComplete);
    }

    private void Client_ReconnectComplete(object? sender, EventArgs e)
    {
        if (!ReferenceEquals(sender, _reconnectHandler))
        {
            return;
        }

        _session = _reconnectHandler?.Session as Session ?? throw new Exception("Invalid reconnect handler session");
        IsConnected = true;
        _reconnectHandler?.Dispose();
        _reconnectHandler = null;

        OnConnectionChanged?.Invoke(this, IsConnected);
    }

    private void OnMonitoredItemNotification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
    {
        if (e.NotificationValue is not MonitoredItemNotification parse) return;

        OnValueChanged?.Invoke(this,
            new ValueTuple<string, object>(monitoredItem.StartNodeId.ToString(), parse.Value));
    }

    public bool IsConnected { get; private set; }

    public event EventHandler<bool>? OnConnectionChanged;
    public event EventHandler<(string NodeId, object Value)>? OnValueChanged;

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        var parse = "opc.tcp://" + _endPoint;

        logger.LogInformation("Connecting to {parse}", parse);

        var selectedEndpoint = CoreClientUtils.SelectEndpoint(
            _applicationConfiguration,
            parse,
            true,
            15000);

        var endpointConfiguration = EndpointConfiguration.Create(_applicationConfiguration);

        var endpoint = new ConfiguredEndpoint(selectedEndpoint.Server, endpointConfiguration);
        endpoint.Update(selectedEndpoint);

        _session = await Session.Create(
            _applicationConfiguration,
            endpoint,
            false,
            "Opc UA Client",
            60000,
            new UserIdentity(new AnonymousIdentityToken()), null, cancellationToken);

        IsConnected = true;

        _session.KeepAliveInterval = 1000;
        _session.KeepAlive += Session_KeepAlive;

        OnConnectionChanged?.Invoke(this, IsConnected);
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        if (_session is null)
            return;

        try
        {
            await _session.CloseAsync(cancellationToken);

            IsConnected = false;

            OnConnectionChanged?.Invoke(this, IsConnected);
        }
        catch (Exception ex)
        {
            logger.LogError("Error closing session: {Message}", ex.ToString());
        }
    }

    public async Task WriteAsync(string nodeId, object value, CancellationToken cancellationToken = default)
    {
        if (_session is null) return;

        try
        {
            WriteValue writeValue = new()
            {
                NodeId = NodeId.Parse(nodeId),
                AttributeId = Attributes.Value,
                Value =
                {
                    Value = value
                }
            };

            WriteValueCollection valuesToWrite = [writeValue];

            await _session.WriteAsync(null, valuesToWrite, CancellationToken.None);
        }
        catch (Exception ex)
        {
            logger.LogError("Error writing on node id {nodeId} value {value}. Exception {ex}",
                nodeId,
                value,
                ex.ToString());
        }
    }

    public async Task<List<object>> ReadAsync(List<string> ids, CancellationToken cancellationToken = default)
    {
        var readValues = ids.Select(i => new ReadValueId()
        {
            NodeId = i,
            AttributeId = Attributes.Value
        });
        
        var readCollection = new ReadValueIdCollection(readValues);

        if(_session is null)
            return [];
        
        var read = await _session.ReadAsync(
            null,
            0,
            TimestampsToReturn.Both,
            readCollection,
            cancellationToken);
        
        return read
            .Results
            .Select(i => i.Value)
            .ToList();
    }

    public async Task ConfigureAsync(string endPoint, CancellationToken cancellationToken = default)
    {
        if (_isCertificated) return;

        _endPoint = endPoint;

        ApplicationInstance application = new()
        {
            ApplicationType = ApplicationType.Client,
            ConfigSectionName = "Opc.Ua.SampleClient",
        };

        var root = AppDomain.CurrentDomain.BaseDirectory;
        var name = application.ConfigSectionName + ".Config.xml";
        var file = Path.Combine(root, name);

        logger.LogInformation("Configuring application with file {file}", file);

        _applicationConfiguration = await application.LoadApplicationConfiguration(file, false);

        if (await application.CheckApplicationInstanceCertificates(false))
        {
            _isCertificated = true;
            _applicationConfiguration.ApplicationUri =
                X509Utils.GetApplicationUriFromCertificate(_applicationConfiguration.SecurityConfiguration
                    .ApplicationCertificate.Certificate);
            _applicationConfiguration.CertificateValidator.CertificateValidation +=
                CertificateValidator_CertificateValidation;
        }
    }

    public void AddMonitoredItems(List<string> nodIds)
    {
        if (_session is null || !_session.Connected)
        {
            logger.LogWarning("Attempting to add monitored items to a disconnected session");
            return;
        }

        const int subscriptionPublishingInterval = 1000;
        const int itemSamplingInterval = 1000;
        const uint queueSize = 10;
        const uint lifetime = 20;

        var subscription = new Subscription(_session.DefaultSubscription)
        {
            DisplayName = "ReferenceClient Subscription",
            PublishingEnabled = true,
            PublishingInterval = subscriptionPublishingInterval,
            LifetimeCount = 0,
            MinLifetimeInterval = lifetime,
            KeepAliveCount = 5,
        };
        _session.AddSubscription(subscription);
        subscription.Create();

        var monitoredItems = nodIds.Select(i => new MonitoredItem(subscription.DefaultItem)
        {
            StartNodeId = new NodeId(i),
            AttributeId = Attributes.Value,
            SamplingInterval = itemSamplingInterval,
            QueueSize = queueSize,
            DiscardOldest = true,
        }).ToList();

        foreach (var monitoredItem in monitoredItems)
        {
            monitoredItem.Notification += OnMonitoredItemNotification;
        }

        subscription.AddItems(monitoredItems);

        subscription.ApplyChanges();
    }
}