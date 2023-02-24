namespace Plugin.MvvmToolkit.Exceptions;

public sealed class DuplicateViewRouteException : Exception
{
    public DuplicateViewRouteException(string message) : base(message)
    {
    }
}
