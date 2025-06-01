namespace TCC.Shared.Models;

public class OpcModel
{
    public string Server { get; set; } = "localhost:50000";
    public string LevelTag { get; set; } = string.Empty;
    public string OutputTag { get; set; } = string.Empty;
    public string RateTag { get; set; } = string.Empty;
    public string SetpointTag { get; set; } = string.Empty;
}