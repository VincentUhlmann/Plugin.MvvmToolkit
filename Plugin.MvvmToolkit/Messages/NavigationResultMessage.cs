[assembly: InternalsVisibleTo("Plugin.MvvmToolkit.Maui")]

namespace Plugin.MvvmToolkit.Messages;

/// <summary>
/// A message that is sent when a navigation occurs.
/// </summary>
/// <typeparam name="TResult"></typeparam>
internal sealed class NavigationResultMessage<TResult> : AsyncRequestMessage<TResult>
{
}