namespace PS.FritzBox.API
{
    /// <summary>
    /// app vpm config informations
    /// </summary>
    public class AppVPNInfo
    {
        /// <summary>
        /// gets or sets the Identifier of the app instance the VPN configuration belongs to.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the ip sec identifier
        /// </summary>
        public string IPSecIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the IPSecPreSharedKey
        /// </summary>
        public string IPSecPreSharedKey { get; set; }

        /// <summary>
        /// Gets or sets the IPSecXauthUsername
        /// </summary>
        public string IPSecXauthUsername { get; set; }

        /// <summary>
        /// Gets or sets the IPSecXauthPassword
        /// </summary>
        public string IPSecXauthPassword { get; set; }
    }
}