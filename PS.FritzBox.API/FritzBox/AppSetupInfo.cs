namespace PS.FritzBox.API
{
    /// <summary>
    /// app setup info
    /// </summary>
    public class AppSetupInfo
    {
        /// <summary>
        /// Gets the min chars for app id
        /// </summary>
        public ushort MinCharsAppId { get; internal set; }

        /// <summary>
        /// Gets the max chars for the app id
        /// </summary>
        public ushort MaxCharsAppId { get; internal set; }

        /// <summary>
        /// gets the allowed chars for the app id
        /// </summary>
        public string AllowedCharsAppId { get; internal set; }

        /// <summary>
        /// gets the min chars for app display name
        /// </summary>
        public ushort MinCharsAppDisplayName { get; internal set; }

        /// <summary>
        /// gets the min chars for display name
        /// </summary>
        public ushort MaxCharsAppDisplayName { get; internal set; }

        /// <summary>
        /// gets the max chars for display name
        /// </summary>
        public ushort MinCharsAppUsername { get; internal set; }

        /// <summary>
        /// gets the max chars for app user name
        /// </summary>
        public ushort MaxCharsAppUsername { get; internal set; }

        /// <summary>
        /// gets the min chars for app password
        /// </summary>
        public ushort MinCharsAppPassword { get; internal set; }

        /// <summary>
        /// gets the allowed chars for user name
        /// </summary>
        public string AllowedCharsAppUsername { get; internal set; }

        /// <summary>
        /// gets the max chars for password
        /// </summary>
        public ushort MaxCharsAppPassword { get; internal set; }

        /// <summary>
        /// gets the allowed chars for app password
        /// </summary>
        public string AllowedCharsAppPassword { get; internal set; }

        /// <summary>
        /// gets the min chars for ip sec identifier
        /// </summary>
        public short MinCharsIPSecIdentifier { get; internal set; }

        /// <summary>
        /// gets the max chars for ip sec identifier
        /// </summary>
        public ushort MaxCharsIPSecIdentifier { get; internal set; }

        /// <summary>
        /// gets the allowed chars for ip sec identifier
        /// </summary>
        public string AllowedCharsIPSecIdentifier { get; internal set; }

        /// <summary>
        /// gets the min chars for ip sec pre shared key
        /// </summary>
        public ushort MinCharsIPSecPreSharedKey { get; internal set; }

        /// <summary>
        /// gets the allowe chars for ip sec pre shared key
        /// </summary>
        public string AllowedCharsIPSecPreSharedKey { get; internal set; }

        /// <summary>
        /// gets the max chars for ip sec pre shared key
        /// </summary>
        public ushort MaxCharsIPSecPreSharedKey { get; internal set; }

        /// <summary>
        /// gets the min chars for ip sec auth username
        /// </summary>
        public ushort MinCharsIPSecXauthUsername { get; internal set; }

        /// <summary>
        /// gets the max chars for ip sec auth username
        /// </summary>
        public ushort MaxCharsIPSecXauthUsername { get; internal set; }

        /// <summary>
        /// gets the allowed chars for ip sec auth username
        /// </summary>
        public string AllowedCharsIPSecXauthUsername { get; internal set; }

        /// <summary>
        /// gets the min chars for ip sec auth password
        /// </summary>
        public ushort MinCharsIPSecXauthPassword { get; internal set; }

        /// <summary>
        /// gets the max chars for ip sec auth password
        /// </summary>
        public ushort MaxCharsIPSecXauthPassword { get; internal set; }

        /// <summary>
        /// gets the allowed chars for ip sec auth password
        /// </summary>
        public string AllowedCharsIPSecXauthPassword { get; set; }

        /// <summary>
        /// gets the Allowed characters for CryptAlgos
        /// </summary>
        public string AllowedCharsCryptAlgos { get; internal set; }

        /// <summary>
        /// gets the Allowed characters for AppAVMAddress
        /// </summary>
        public string AllowedCharsAppAVMAddress { get; internal set; }
    }
}