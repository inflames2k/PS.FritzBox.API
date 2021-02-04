namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing speed test config
    /// </summary>
    public class SpeedtestConfig
    {
        /// <summary>
        /// Gets or sets if tcp is enabled
        /// </summary>
        public bool EnableTcp { get; set; }

        /// <summary>
        /// Gets or sets if udp is enabled
        /// </summary>
        public bool EnableUdp { get; set; }

        /// <summary>
        /// Gets or sets if udp bidirectional is enabled
        /// </summary>
        public bool EnableUdpBidirect { get; set; }

        /// <summary>
        /// Gets or sets if tcp is enabled for wan
        /// </summary>
        public bool WANEnableTcp { get; set; }

        /// <summary>
        /// Gets or sets if udp is enabled for wan
        /// </summary>
        public bool WANEnableUdp { get; set; }
    }
}