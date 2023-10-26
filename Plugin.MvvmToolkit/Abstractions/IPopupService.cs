namespace Plugin.MvvmToolkit.Abstractions;

/// <summary>
/// Represents a service that displays popups.
/// </summary>
public interface IPopupService
{
    /// <summary>
    /// Displays a message popup.
    /// </summary>
    /// <param name="title">The title of the popup.</param>
    /// <param name="message">The message of the popup.</param>
    /// <param name="cancel">The text of the cancel button.</param>
    Task DisplayMessagePopupAsync(string title, string message, string cancel);

    /// <summary>
    /// Displays a confirmation popup.
    /// </summary>
    /// <param name="title">The title of the popup.</param>
    /// <param name="message">The message of the popup.</param>
    /// <param name="accept">The text of the accept button.</param>
    /// <param name="cancel">The text of the cancel button.</param>
    /// <returns>If the user accepted the popup.</returns>
    Task<bool> DisplayConfirmationPopupAsync(string title, string message, string accept, string cancel);
}
