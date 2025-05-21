namespace TCC.Shared.Models;

public class SettingsModel
{
    public OpcModel OpcModel { get; set; } = new();
    public ApiModel ApiModel { get; set; } = new();
}