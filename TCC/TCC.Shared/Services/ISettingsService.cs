using TCC.Shared.Models;

namespace TCC.Shared.Services;

public interface ISettingsService
{
    Task<SettingsModel> GetSettingsAsync(CancellationToken cancellation = default);
    Task<bool> SaveSettingsAsync(SettingsModel settings, CancellationToken cancellation = default);
}