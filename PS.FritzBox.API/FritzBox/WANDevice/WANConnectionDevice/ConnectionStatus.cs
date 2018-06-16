namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    /// <summary>
    /// enumeration for connection states
    /// </summary>
    public enum ConnectionStatus
    {
        /// <summary>
        /// the connection is not configured
        /// </summary>
        Unconfigured,
        /// <summary>
        /// the device is connecting
        /// </summary>
        Connecting,
        /// <summary>
        /// the device is connected
        /// </summary>
        Connected,
        /// <summary>
        /// disconnect is pending
        /// </summary>
        PendingDisconnect,
        /// <summary>
        /// the device is disconnecting
        /// </summary>
        Disconnecting,
        /// <summary>
        /// the device is disconnected
        /// </summary>
        Disconnected
    }
}
