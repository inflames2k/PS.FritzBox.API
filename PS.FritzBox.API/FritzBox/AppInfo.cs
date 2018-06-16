namespace PS.FritzBox.API
{
    /// <summary>
    /// app informations
    /// </summary>
    public class AppInfo
    {
        /// <summary>
        /// gets or sets the Unique identifier of the app instance (unique within the single box).
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the User friendly display name of the app instance.
        /// </summary>
        public string AppDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the MAC address of the device (or device interface). Empty string means: unknown
        /// </summary>
        public string AppDeviceMAC { get; set; }

        /// <summary>
        /// Gets or sets the Username for the app instance.
        /// </summary>
        public string AppUsername { get; set; }

        /// <summary>
        /// Gets or sets the Password for the app instance.
        /// </summary>
        public string AppPassword { get; set; }

        /// <summary>
        /// Gets or sets the FRITZ!OS app specific configuration right.
        /// </summary>
        public RightEnum AppRight { get; set; }

        /// <summary>
        /// Gets or sets the FRITZ!OS NAS specific right.
        /// </summary>
        public RightEnum NASRight { get; set; }

        /// <summary>
        /// Gets or sets the FRITZ!OS phone specific right. 
        /// </summary>
        public RightEnum PhoneRight { get; set; }

        /// <summary>
        /// Gets or sets the FRITZ!OS home automation specific right.
        /// </summary>
        public RightEnum HomeautoRight { get; set; }

        /// <summary>
        /// Gets or sets if the app wants access from the internet
        /// </summary>
        public bool AppInternetRights { get; set; }
    }
}