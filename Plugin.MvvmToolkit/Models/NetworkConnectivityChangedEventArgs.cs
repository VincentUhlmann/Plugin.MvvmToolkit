namespace Plugin.MvvmToolkit.Models;

/// <summary>
/// Event arguments for the <see cref="IConnectivityService.NetworkConnectivityChanged"/> event.
/// </summary>
public sealed class NetworkConnectivityChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the current network state.
    /// </summary>
    public NetworkState NetworkState { get; }

    /// <summary>
    /// Gets the active network types.
    /// </summary>
    public IEnumerable<NetworkType> NetworkTypes { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkConnectivityChangedEventArgs"/> class.
    /// </summary>
    /// <param name="networkState"></param>
    /// <param name="networkTypes"></param>
    public NetworkConnectivityChangedEventArgs(NetworkState networkState, IEnumerable<NetworkType> networkTypes)
    {
        NetworkState = networkState;
        NetworkTypes = networkTypes;
    }
}