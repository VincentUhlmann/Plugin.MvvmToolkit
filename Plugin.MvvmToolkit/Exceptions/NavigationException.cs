namespace Plugin.MvvmToolkit.Exceptions;

public sealed class NavigationException : Exception
{
    public NavigationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
