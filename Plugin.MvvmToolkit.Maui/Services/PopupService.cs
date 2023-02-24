namespace Plugin.MvvmToolkit.Maui.Services;

internal sealed class PopupService : IPopupService
{
    public async Task DisplayPopupAsync(string title, string message, string cancel)
    {
        await Application.Current!.MainPage!.DisplayAlert(title, message, cancel);
    }

    public async Task<bool> DisplayPopupAsync(string title, string message, string accept, string cancel)
    {
        return await Application.Current!.MainPage!.DisplayAlert(title, message, accept, cancel);
    }

    public async Task<string> DisplayPopupAsync(string title, string[] buttons, string? cancel = null, string? destruction = null)
    {
        return await Application.Current!.MainPage!.DisplayActionSheet(title, cancel, destruction, buttons);
    }
}
