using System;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// class representing wlan informations
    /// </summary>
    public class WLANInfo
    {
        public WLANConfig Config { get; internal set; } = new WLANConfig();

        /// <summary>
        /// Gets if wlan is enabled
        /// </summary>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// Gets the status
        /// </summary>
        public string Status { get; internal set; }

        /// <summary>
        /// Gets the wlan standard
        /// </summary>
        public WLANStandard Standard { get; internal set; }

        /// <summary>
        /// Gets the bssid
        /// </summary>
        public string BSSID { get; internal set; }

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