namespace Plugin.MvvmToolkit.Models;

/// <summary>
/// Represents a message popup.
/// </summary>
public sealed class MessagePopup : BasePopup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessagePopup"/> class.
    /// </summary>
    /// <param name="title">The title of the popup.</param>
    /// <param name="message">The message of the popup.</param>
    /// <param name="cancel">The cancel button text.</param>
    public MessagePopup(string title, string message, string cancel) : base(title, message, cancel)
    {
    }
}
