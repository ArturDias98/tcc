using Microsoft.AspNetCore.Components;
using TCC.Shared.Models;
using TCC.Shared.Services;

namespace TCC.UI.RazorLib.Shared;

public partial class Settings : ComponentBase
{
    private SettingsModel _settings = new();

    private async Task SaveAsync()
    {
        await SettingsService.SaveSettingsAsync(_settings);
    }
    
    [Inject] private ISettingsService SettingsService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _settings = await SettingsService.GetSettingsAsync();
    }
}