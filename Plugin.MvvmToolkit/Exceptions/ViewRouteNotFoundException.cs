namespace Plugin.MvvmToolkit.Exceptions;

public sealed class ViewRouteNotFoundException : Exception
{
    public ViewRouteNotFoundException(string message) : base(message)
    {
    }
}
