using System.Text.Json;
using TCC.Shared.Models;
using TCC.Shared.Services;

namespace TCC.Core;

internal sealed class SettingsService : ISettingsService
{
    private readonly string _settingsFilePath;
    private readonly JsonSerializerOptions _serializerOptions = new() { WriteIndented = true };

    public SettingsService()
    {
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var tccDirectory = Path.Combine(documentsPath, "TCC");
        Directory.CreateDirectory(tccDirectory);
        _settingsFilePath = Path.Combine(tccDirectory, "settings.json");
    }

    public async Task<SettingsModel> GetSettingsAsync(CancellationToken cancellation = default)
    {
        if (!File.Exists(_settingsFilePath))
        {
            return new SettingsModel();
        }

        var json = await File.ReadAllTextAsync(_settingsFilePath, cancellation);
        return JsonSerializer.Deserialize<SettingsModel>(json) ?? new SettingsModel();
    }

    public async Task<bool> SaveSettingsAsync(SettingsModel settings, CancellationToken cancellation = default)
    {
        try
        {
            var json = JsonSerializer.Serialize(settings, _serializerOptions);
            await File.WriteAllTextAsync(_settingsFilePath, json, cancellation);
            return true;
        }
        catch
        {
            return false;
        }
    }
}