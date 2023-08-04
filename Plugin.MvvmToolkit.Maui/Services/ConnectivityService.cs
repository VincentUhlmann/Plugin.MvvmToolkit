namespace Plugin.MvvmToolkit.Maui.Services;

/// <inheritdoc />
public sealed class ConnectivityService : IConnectivityService
{
    /// <inheritdoc />
    public NetworkState NetworkState => Connectivity.Current.NetworkAccess switch {
        NetworkAccess.Unknown => NetworkState.Unknown,
        NetworkAccess.None => NetworkState.None,
        NetworkAccess.Local => NetworkState.Local,
        NetworkAccess.ConstrainedInternet => NetworkState.ConstrainedInternet,
        NetworkAccess.Internet => NetworkState.Internet,
        _ => throw new NotImplementedException()
    };

    /// <inheritdoc />
    public IEnumerable<NetworkType> NetworkTypes => Connectivity.Current.ConnectionProfiles.Select(x => x switch {
        ConnectionProfile.Unknown => NetworkType.Unknown,
        ConnectionProfile.Bluetooth => NetworkType.Bluetooth,
        ConnectionProfile.Cellular => NetworkType.Cellular,
        ConnectionProfile.Ethernet => NetworkType.Ethernet,
        ConnectionProfile.WiFi => NetworkType.WiFi,
        _ => throw new NotImplementedException()
    });

    /// <inheritdoc />
    public event EventHandler<NetworkConnectivityChangedEventArgs>? NetworkConnectivityChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectivityService"/> class.
    /// </summary>
    public ConnectivityService()
    {
        Connectivity.ConnectivityChanged += (sender, args) => {
            NetworkConnectivityChanged?.Invoke(sender, new NetworkConnectivityChangedEventArgs(NetworkState, NetworkTypes));
        };
    }
}