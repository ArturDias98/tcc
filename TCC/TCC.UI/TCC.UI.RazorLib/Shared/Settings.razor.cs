using Microsoft.AspNetCore.Components;
using TCC.Shared.Models;
using TCC.Shared.Services;

namespace TCC.UI.RazorLib.Shared;

public partial class Settings : ComponentBase
{
    private SettingsModel _settings = new();
    private bool _saving;

    private async Task SaveAsync()
    {
        _saving = true;
        StateHasChanged();

        await Task.WhenAll(
            SettingsService.SaveSettingsAsync(_settings),
            Task.Delay(1000));
        
        _saving = false;
        StateHasChanged();
    }

    [Inject] private ISettingsService SettingsService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _settings = await SettingsService.GetSettingsAsync();
    }
}