namespace Plugin.MvvmToolkit.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an error occurs during navigation in a navigation service.
/// </summary>
public sealed class NavigationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the NavigationException class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public NavigationException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the NavigationException class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public NavigationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}