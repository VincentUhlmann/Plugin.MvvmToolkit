namespace Plugin.MvvmToolkit.Collections;

/// <summary>
/// Describes the type of connection the device is using.
/// </summary>
public enum NetworkType
{
    /// <summary>
    /// The type of network is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The device is connected via Bluetooth.
    /// </summary>
    Bluetooth = 1,

    /// <summary>
    /// The device is connected via Cellular.
    /// </summary>
    Cellular = 2,

    /// <summary>
    /// The device is connected via Ethernet.
    /// </summary>
    Ethernet = 3,

    /// <summary>
    /// The device is connected via WiFi.
    /// </summary>
    WiFi = 4
}