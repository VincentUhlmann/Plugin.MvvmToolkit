namespace Plugin.MvvmToolkit.Messages;

/// <summary>
/// A message that is sent when a navigation occurs.
/// </summary>
/// <typeparam name="TResult"></typeparam>
public sealed class NavigationResultMessage<TResult> : AsyncRequestMessage<TResult>
{
}