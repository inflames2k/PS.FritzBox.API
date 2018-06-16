using System;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// Informations about lan ethernet interface
    /// </summary>
    public class LANEthernetInterfaceInfo
    {
        /// <summary>
        /// gets the enable state
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// gets the interface status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Gets the mac address
        /// </summary>
        public string MACAddress { get; set; }
        /// <summary>
        /// gets the max bit rate
        /// </summary>
        public UInt32 MaxBitRate { get; set; }
        /// <summary>
        /// Gets the duplex mode
        /// </summary>
        public string DuplexMode { get; set; }

    }
}