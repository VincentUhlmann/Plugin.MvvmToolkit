namespace Plugin.MvvmToolkit.Models;

/// <summary>
/// Represents a base popup.
/// </summary>
public abstract class BasePopup
{
    /// <summary>
    /// Gets the title of the popup.
    /// </summary>
    public string Title { get; } = string.Empty;

    /// <summary>
    /// Gets the message of the popup.
    /// </summary>
    public string Message { get; } = string.Empty;

    /// <summary>
    /// Gets the cancel button text.
    /// </summary>
    public string Cancel { get; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasePopup"/> class.
    /// </summary>
    /// <param name="title">The title of the popup.</param>
    /// <param name="message">The message of the popup.</param>
    /// <param name="cancel">The cancel button text.</param>
    public BasePopup(string title, string message, string cancel)
    {
        Title = title;
        Message = message;
        Cancel = cancel;
    }
}
