namespace TCC.Shared.Services;

public interface IApiStatusService
{
    bool IsConnected { get; }
    event EventHandler<bool> OnConnectionChanged;
}