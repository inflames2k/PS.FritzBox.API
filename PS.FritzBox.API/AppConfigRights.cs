namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing app config rights
    /// </summary>
    public class AppConfigRights
    {
        /// <summary>
        /// Gets or set if acces rights from the internet are configured
        /// </summary>
        public bool InternetRights { get; set; }

        /// <summary>
        /// gets or sets if the current access is comming from internet
        /// </summary>
        public bool AccessFromInternet { get; set; }

        /// <summary>
        /// Gets or sets the FRITZ!OS configuration right.
        /// </summary>
        public RightEnum ConfigRight { get; set; }

        /// <summary>
        /// Gets or sets the FRITZ!OS app specific configuration right. 
        /// </summary>
        public RightEnum AppRight { get; set; }

        /// <summary>
        /// FRITZ!OS phone specific right. 
        /// </summary>
        public RightEnum PhoneRight { get; set; }

        /// <summary>
        /// Gets or sets the FRITZ!OS NAS specific right. 
        /// </summary>
        public RightEnum NasRight { get; set; }

        /// <summary>
        /// Gets or sets the FRITZ!OS dial specific right. 
        /// </summary>
        public RightEnum DialRight { get; set; }

        /// <summary>
        /// Gets or sets the FRITZ!OS home automation specific right. 
        /// </summary>
        public RightEnum HomeautoRight { get; set; }
    }
}