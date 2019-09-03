namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    /// <summary>
    /// class representing dsl link info
    /// </summary>
    public class DSLLinkInfo
    {
        /// <summary>
        /// Gets or sets the link status
        /// </summary>
        public WANDSLLinkStatus LinkStatus { get; set; }

        /// <summary>
        /// Gets or sets the link type
        /// </summary>
        public WANDSLLinkType LinkType { get; set; }
    }
}