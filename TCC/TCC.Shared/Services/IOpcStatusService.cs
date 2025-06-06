namespace TCC.Shared.Services;

public interface IOpcStatusService
{
    bool IsConnected { get; }
    event EventHandler<bool> OnConnectionChanged;
}