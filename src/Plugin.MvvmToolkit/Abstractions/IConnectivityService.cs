namespace Plugin.MvvmToolkit.Abstractions;

/// <summary>
/// Lets you listen to the state of network connectivity and gather information about the connection.
/// </summary>
public interface IConnectivityService
{
    /// <summary>
    /// Gets the current state of network access.
    /// </summary>
    NetworkState NetworkState { get; }

    /// <summary>
    /// Gets the current active network types.
    /// </summary>
    IEnumerable<NetworkType> NetworkTypes { get; }

    /// <summary>
    /// Occurs when the network connectivity changes.
    /// </summary>
    event EventHandler<NetworkConnectivityChangedEventArgs>? NetworkConnectivityChanged;
}
