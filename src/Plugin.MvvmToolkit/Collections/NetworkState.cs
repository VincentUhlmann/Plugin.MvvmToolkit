namespace Plugin.MvvmToolkit.Collections;

/// <summary>
/// Describes the state of the network connection.
/// </summary>
public enum NetworkState
{
    /// <summary>
    /// The state of the network is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The device is not connected to any network.
    /// </summary>
    None = 1,

    /// <summary>
    /// The device is connected to a local network.
    /// </summary>
    Local = 2,

    /// <summary>
    /// The device is connected to a network with limited internet access.
    /// </summary>
    ConstrainedInternet = 3,

    /// <summary>
    /// The device is connected to the internet.
    /// </summary>
    Internet = 4
}
