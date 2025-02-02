namespace Plugin.MvvmToolkit.Maui.Services;

/// <inheritdoc />
public sealed class PopupService : IPopupService
{
    /// <inheritdoc />
    public async Task DisplayMessagePopupAsync(string title, string message, string cancel)
    {
        if (Application.Current is null || Application.Current.Windows[0]?.Page is null)
            throw new InvalidOperationException("Application.Current.WIndows[0].Page is null.");

        await Application.Current!.Windows[0]!.Page!.DisplayAlert(title, message, cancel);
    }

    /// <inheritdoc />
    public async Task<bool> DisplayConfirmationPopupAsync(string title, string message, string accept, string cancel)
    {
        if (Application.Current is null || Application.Current.Windows[0]?.Page is null)
            throw new InvalidOperationException("Application.Current.WIndows[0].Page is null.");

        return await Application.Current!.Windows[0]!.Page!.DisplayAlert(title, message, accept, cancel);
    }
}