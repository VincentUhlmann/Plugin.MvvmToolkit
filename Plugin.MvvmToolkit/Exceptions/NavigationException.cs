namespace Plugin.MvvmToolkit.Exceptions;

public class NavigationException : Exception
{
    public NavigationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
