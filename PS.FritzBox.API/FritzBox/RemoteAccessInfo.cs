namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing remote access info
    /// </summary>
    public class RemoteAccessInfo
    {
        /// <summary>
        /// Gets or sets if remote access is enabled
        /// </summary>
        public string Enabled { get; set; }

        /// <summary>
        /// Gets or sets the remote access port
        /// </summary>
        public short Port { get; set; }

        /// <summary>
        /// Gets or sets the remote access user name
        /// </summary>
        public string Username { get; set; }
    }
}