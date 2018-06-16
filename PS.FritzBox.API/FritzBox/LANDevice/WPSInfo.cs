namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// class for wps informations
    /// </summary>
    public class WPSInfo
    {
        /// <summary>
        /// gets the wps status
        /// </summary>
        public WPSStatus Status { get; internal set; }

        /// <summary>
        /// Gets the wps mode
        /// </summary>
        public WPSMode Mode { get; internal set; }
    }
}