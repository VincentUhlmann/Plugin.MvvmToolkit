namespace Plugin.MvvmToolkit.Exceptions;

/// <summary>
/// Represents an exception that is thrown when attempting to add a duplicate route to a navigation service.
/// </summary>
public sealed class DuplicateRouteException : Exception
{
    /// <summary>
    /// Initializes a new instance of the DuplicateRouteException class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public DuplicateRouteException(string message) : base(message)
    {
    }
}