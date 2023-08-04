namespace Plugin.MvvmToolkit.Exceptions;

/// <summary>
/// Represents an exception that is thrown when attempting to navigate to a route that cannot be found in a navigation service.
/// </summary>
public sealed class RouteNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the RouteNotFoundException class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public RouteNotFoundException(string message) : base(message)
    {
    }
}