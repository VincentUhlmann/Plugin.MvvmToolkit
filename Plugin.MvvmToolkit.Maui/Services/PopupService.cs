namespace Plugin.MvvmToolkit.Maui.Services;

/// <inheritdoc />
public sealed class PopupService : IPopupService
{
    /// <inheritdoc />
    public async Task DisplayMessagePopupAsync(string title, string message, string cancel)
    {
        if (Application.Current is null || Application.Current.MainPage is null)
            throw new InvalidOperationException("Application.Current.MainPage is null.");

        await Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }

    /// <inheritdoc />
    public async Task<bool> DisplayConfirmationPopupAsync(string title, string message, string accept, string cancel)
    {
        if (Application.Current is null || Application.Current.MainPage is null)
            throw new InvalidOperationException("Application.Current.MainPage is null.");

        return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
    }
}
