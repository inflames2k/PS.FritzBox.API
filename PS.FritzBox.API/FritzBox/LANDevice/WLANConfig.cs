using System;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// wlan configuration
    /// </summary>
    public class WLANConfig
    {
        /// <summary>
        /// Gets or sets the channel
        /// </summary>
        public UInt16 Channel { get; set; }

        /// <summary>
        /// Gets or sets the ssid
        /// </summary>
        public string SSID { get; set; }
        
        /// <summary>
        /// Gets or sets the beacon type
        /// </summary>
        public BeaconType BeaconType { get; set; }

        /// <summary>
        /// Gets or sets if mac adress control is enabled
        /// </summary>
        public bool MACAddressControlEnabled { get; set; }

        /// <summary>
        /// Gets or sets the basic encryption modes
        /// </summary>
        public BasicEncryptionModes BasicEncryptionModes { get; set; }
    }
}