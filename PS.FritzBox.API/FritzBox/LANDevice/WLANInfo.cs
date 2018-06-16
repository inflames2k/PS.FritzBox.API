using System;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// class representing wlan informations
    /// </summary>
    public class WLANInfo
    {
        /// <summary>
        /// Gets if wlan is enabled
        /// </summary>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// Gets the status
        /// </summary>
        public string Status { get; internal set; }

        /// <summary>
        /// Gets the channel
        /// </summary>
        public UInt16 Channel { get; internal set; }

        /// <summary>
        /// Gets the ssid
        /// </summary>
        public string SSID { get; internal set; }

        /// <summary>
        /// Gets the beacon type
        /// </summary>
        public BeaconType BeaconType { get; internal set; }

        /// <summary>
        /// Gets if mac adress control is enabled
        /// </summary>
        public bool MACAddressControlEnabled { get; internal set; }

        /// <summary>
        /// Gets the wlan standard
        /// </summary>
        public WLANStandard Standard { get; internal set; }

        /// <summary>
        /// Gets the bssid
        /// </summary>
        public string BSSID { get; internal set; }

        /// <summary>
        /// Gets the basic encryption modes
        /// </summary>
        public BasicEncryptionModes BasicEncryptionModes { get; internal set; }

        /// <summary>
        /// Gets the ssid validation info
        /// </summary>
        public DataValidationInfo SSIDValidationInfo { get; internal set; } = new DataValidationInfo();

        /// <summary>
        /// Gets the psk validation info
        /// </summary>
        public DataValidationInfo PSKValidationInfo { get; internal set; } = new DataValidationInfo();
    }
}