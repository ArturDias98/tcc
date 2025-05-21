namespace TCC.Shared.Services;

public interface IOpcStatusService
{
    bool IsConnected { get; }
}