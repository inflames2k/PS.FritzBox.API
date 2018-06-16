using System;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// Lan statistic informations
    /// </summary>
    public class LANStatistics
    {
        /// <summary>
        /// Gets the bytes sent
        /// </summary>
        public UInt32 BytesSent { get; set; }
        /// <summary>
        /// Gets the bytes received
        /// </summary>
        public UInt32 BytesReceived { get; set; }
        /// <summary>
        /// Gets the packets sent
        /// </summary>
        public UInt32 PacketsSent { get; set; }
        /// <summary>
        /// Gets the packets received
        /// </summary>
        public UInt32 PacketsReceived { get; set; }
    }
}