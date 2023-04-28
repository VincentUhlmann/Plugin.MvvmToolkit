namespace Plugin.MvvmToolkit.Exceptions;

public sealed class RouteNotFoundException : Exception
{
    public RouteNotFoundException(string message) : base(message)
    {
    }
}
