namespace Plugin.MvvmToolkit.Maui.Services;

/// <inheritdoc />
public sealed class PopupService : IPopupService
{
    /// <inheritdoc />
    public async Task DisplayMessagePopupAsync(MessagePopup popup)
    {
        if (Application.Current is null || Application.Current.MainPage is null)
            throw new InvalidOperationException("Application.Current.MainPage is null.");

        await Application.Current.MainPage.DisplayAlert(popup.Title, popup.Message, popup.Cancel);
    }

    /// <inheritdoc />
    public async Task<bool> DisplayConfirmationPopupAsync(ConfirmationPopup popup)
    {
        if (Application.Current is null || Application.Current.MainPage is null)
            throw new InvalidOperationException("Application.Current.MainPage is null.");

        return await Application.Current.MainPage.DisplayAlert(popup.Title, popup.Message, popup.Accept, popup.Cancel);
    }
}
