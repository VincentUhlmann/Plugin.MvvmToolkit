namespace Plugin.MvvmToolkit.Models;

/// <summary>
/// Represents a confirmation popup.
/// </summary>
public sealed class ConfirmationPopup : BasePopup
{
    /// <summary>
    /// Gets the accept button text.
    /// </summary>
    public string Accept { get; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmationPopup"/> class.
    /// </summary>
    /// <param name="title">The title of the popup.</param>
    /// <param name="message">The message of the popup.</param>
    /// <param name="accept">The accept button text.</param>
    /// <param name="cancel">The cancel button text.</param>
    public ConfirmationPopup(string title, string message, string accept, string cancel) : base(title, message, cancel)
    {
        Accept = accept;
    }
}
