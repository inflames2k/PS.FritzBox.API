namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing speed test info
    /// </summary>
    public class SpeedtestInfo
    {
        /// <summary>
        /// Gets or sets the speed test config
        /// </summary>
        public SpeedtestConfig Config { get; set; } = new SpeedtestConfig();

        /// <summary>
        /// Gets or sets the tcp port
        /// </summary>
        public uint PortTcp { get; set; }

        /// <summary>
        /// Gets or sets the udp port
        /// </summary>
        public uint PortUdp { get; set; }

        /// <summary>
        /// Gets or sets the odp bidirectional port
        /// </summary>
        public uint PortUdpBidirect { get; set; }
    }
}