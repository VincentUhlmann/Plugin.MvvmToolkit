namespace Plugin.MvvmToolkit.Abstractions;

/// <summary>
/// Represents a service that displays popups.
/// </summary>
public interface IPopupService
{
    /// <summary>
    /// Displays a message popup.
    /// </summary>
    /// <param name="popup">The message popup to display.</param>
    Task DisplayMessagePopupAsync(MessagePopup popup);

    /// <summary>
    /// Displays a confirmation popup.
    /// </summary>
    /// <param name="popup">The confirmation popup to display.</param>
    /// <returns>True if the user accepted the confirmation, false otherwise.</returns>
    Task<bool> DisplayConfirmationPopupAsync(ConfirmationPopup popup);
}
