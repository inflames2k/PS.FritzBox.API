namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// class holding security key configuration
    /// </summary>
    public class SecurityKeyConfig
    {
        /// <summary>
        /// Gets or sets the wep key 0
        /// </summary>
        public string WEPKey0 { get; set; }

        /// <summary>
        /// Gets or sets the wep key 1
        /// </summary>
        public string WEPKey1 { get; set; }

        /// <summary>
        /// Gets or sets the wep key 2
        /// </summary>
        public string WEPKey2 { get; set; }

        /// <summary>
        /// Gets or sets the wep key 3
        /// </summary>
        public string WEPKey3 { get; set; }

        /// <summary>
        /// Gets or sets the preshared key
        /// </summary>
        public string PreSharedKey { get; set; }

        /// <summary>
        /// Gets or sets the key pass phrase
        /// </summary>
        public string KeyPassphrase { get; set; }
    }
}