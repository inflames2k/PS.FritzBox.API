using PS.FritzBox.API.Base;
using PS.FritzBox.API.FritzBox.LANDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// client for wlan configuration service 
    /// </summary>
    public class WLANConfigurationClient : FritzTR64Client
    {
        #region Construction / Destruction

        
        public WLANConfigurationClient(string url, int timeout) : base(url, timeout)
        {
        }

        
        public WLANConfigurationClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        
        public WLANConfigurationClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        
        public WLANConfigurationClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/wlanconfig1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:WLANConfiguration:1";

        /// <summary>
        /// Method to enable or disable the wlan
        /// </summary>
        /// <param name="enabled">true or false (enabled or disabled)</param>
        /// <returns></returns>
        public async Task SetEnableAsync(bool enabled)
        {
            await this.InvokeAsync("SetEnable", new SOAP.SoapRequestParameter("NewEnable", enabled ? "1" : "0"));
        }

        /// <summary>
        /// Method to get the wlan info
        /// </summary>
        /// <returns></returns>
        public async Task<WLANInfo> GetInfoAsync()
        {            
            XDocument document = await this.InvokeAsync("GetInfo", null);
            return new WLANInfo()
            {
                Config = new WLANConfig()
                {
                    BasicEncryptionModes = (BasicEncryptionModes)Enum.Parse(typeof(BasicEncryptionModes), document.Descendants("NewBasicEncryptionModes").First().Value),
                    BeaconType = Enum.TryParse<BeaconType>(document.Descendants("NewBeaconType").First().Value, out BeaconType result) ? result : BeaconType._11i,
                    Channel = Convert.ToUInt16(document.Descendants("NewChannel").First().Value),
                    MACAddressControlEnabled = document.Descendants("NewMACAddressControlEnabled").First().Value == "1",
                    SSID = document.Descendants("NewSSID").First().Value,
                },
                BSSID = document.Descendants("NewBSSID").First().Value,
                Enabled = document.Descendants("NewEnable").First().Value == "1",
                Standard = (WLANStandard)Enum.Parse(typeof(WLANStandard), document.Descendants("NewStandard").First().Value),
                Status = document.Descendants("NewStatus").First().Value,
                PSKValidationInfo = new DataValidationInfo()
                {
                    MinChars = Convert.ToUInt16(document.Descendants("NewMinCharsPSK").First().Value),
                    MaxChars = Convert.ToUInt16(document.Descendants("NewMaxCharsPSK").First().Value),
                    AllowedChars = document.Descendants("NewAllowedCharsPSK").First().Value
                },
                SSIDValidationInfo = new DataValidationInfo()
                {
                    MinChars = Convert.ToUInt16(document.Descendants("NewMinCharsSSID").First().Value),
                    MaxChars = Convert.ToUInt16(document.Descendants("NewMaxCharsSSID").First().Value),
                    AllowedChars = document.Descendants("NewAllowedCharsSSID").First().Value
                }
            };
        }

        /// <summary>
        /// Method to set the wlan configuration
        /// </summary>
        /// <param name="config">the configuration</param>
        /// <returns></returns>
        public async Task SetConfigAsync(WLANConfig config)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewChannel", config.Channel),
                new SOAP.SoapRequestParameter("NewSSID", config.SSID),
                new SOAP.SoapRequestParameter("NewBeaconType", config.BeaconType != BeaconType._11i ? config.BeaconType.ToString() : "11i"),
                new SOAP.SoapRequestParameter("NewMacAddressControlEnabled", config.MACAddressControlEnabled ? "1" : "0"),
                new SOAP.SoapRequestParameter("NewBasicEncryptionModes", config.BasicEncryptionModes.ToString()),
            };

            await this.InvokeAsync("SetConfig", parameters.ToArray());
        }

        /// <summary>
        /// Method to get the security key information
        /// </summary>
        /// <returns></returns>
        public async Task<SecurityKeyConfig> GetSecurityKeysAsync()
        {
            XDocument document = await this.InvokeAsync("GetSecurityKeys", null);
            return new SecurityKeyConfig()
            {
                WEPKey0 = document.Descendants("NewWEPKey0").First().Value,
                WEPKey1 = document.Descendants("NewWEPKey1").First().Value,
                WEPKey2 = document.Descendants("NewWEPKey2").First().Value,
                WEPKey3 = document.Descendants("NewWEPKey3").First().Value,
                PreSharedKey = document.Descendants("NewPreSharedKey").First().Value,
                KeyPassphrase = document.Descendants("NewKeyPassphrase").First().Value
            };
        }

        /// <summary>
        /// Method to set the security keys
        /// </summary>
        /// <param name="config">the security key config</param>
        /// <returns></returns>
        public async Task SetSecurityKeysAsync(SecurityKeyConfig config)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewWEPKey0", config.WEPKey0),
                new SOAP.SoapRequestParameter("NewWEPKey1", config.WEPKey1),
                new SOAP.SoapRequestParameter("NewWEPKey2", config.WEPKey2),
                new SOAP.SoapRequestParameter("NewWEPKey3", config.WEPKey3),
                new SOAP.SoapRequestParameter("NewPreSharedKey", config.PreSharedKey),
                new SOAP.SoapRequestParameter("NewKeyPassphrase", config.KeyPassphrase)
            };

            await this.InvokeAsync("SetSecurityKeys", parameters.ToArray());
        }

        /// <summary>
        /// Method to get the default wep key index
        /// </summary>
        /// <returns>the default wep key index</returns>
        public async Task<UInt16> GetDefaultWEPKeyIndexAsync()
        {
            XDocument document = await this.InvokeAsync("GetDefaultWEPKeyIndex", null);
            return Convert.ToUInt16(document.Descendants("NewDefaultWEPKeyIndex").First().Value);
        }

        /// <summary>
        /// Method to set the default wep key index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns></returns>
        public async Task SetDefaultWEPKeyIndexAsync(UInt16 index)
        {
            await this.InvokeAsync("SetDefaultWEPKeyIndex", new SOAP.SoapRequestParameter("NewDefaultWEPKeyIndex", index));
        }

        /// <summary>
        /// Method to get the basic beacon security properties
        /// </summary>
        /// <returns>encryption mode</returns>
        public async Task<BasicEncryptionModes> GetBasBeaconSecurityPropertiesAsync()
        {
            XDocument document = await this.InvokeAsync("GetBasBeaconSecurityProperties", null);
            return (BasicEncryptionModes)Enum.Parse(typeof(BasicEncryptionModes), document.Descendants("NewBasicEncryptionModes").First().Value);
        }

        /// <summary>
        /// Method to set the basic beacon security properties
        /// </summary>
        /// <returns></returns>
        public async Task SetBasBeaconSecurityPropertiesAsync(BasicEncryptionModes encryptionMode)
        {
            await this.InvokeAsync("SetBasBeaconSecurityProperties", new SOAP.SoapRequestParameter("NewBasicEncryptionModes", encryptionMode.ToString()));
        }

        /// <summary>
        /// Method to get the bssid
        /// </summary>
        /// <returns>the bssid</returns>
        public async Task<string> GetBSSIDAsync()
        {
            XDocument document = await this.InvokeAsync("GetBSSID", null);
            return document.Descendants("NewBSSID").First().Value;
        }

        /// <summary>
        /// Method to get the bssid
        /// </summary>
        /// <returns>the bssid</returns>
        public async Task<string> GetSSIDAsync()
        {
            XDocument document = await this.InvokeAsync("GetSSID", null);
            return document.Descendants("NewSSID").First().Value;
        }

        /// <summary>
        /// Method to set the bssid
        /// </summary>
        /// <param name="ssid">the new ssid</param>
        public async Task SetSSIDAsync(string ssid)
        {
            await this.InvokeAsync("SetSSID", new SOAP.SoapRequestParameter("NewSSID", ssid));
        }

        /// <summary>
        /// Method to get the beacon type
        /// </summary>
        /// <returns>the beacon type</returns>
        public async Task<BeaconType> GetBeaconTypeAsync()
        {
            XDocument document = await this.InvokeAsync("GetBeaconType", null);
            return Enum.TryParse<BeaconType>(document.Descendants("NewBeaconType").First().Value, out BeaconType result) ? result : BeaconType._11i;
        }

        /// <summary>
        /// Method to set the beacon type
        /// </summary>
        /// <param name="type">the new beacon type</param>
        /// <returns></returns>
        public async Task SetBeaconTypeAsync(BeaconType type)
        {
            await this.InvokeAsync("SetBeaconType", new SOAP.SoapRequestParameter("NewBeaconType", type == BeaconType._11i ? "11i" : type.ToString()));
        }

        /// <summary>
        /// Method to get the channel info
        /// </summary>
        /// <returns>the channel info</returns>
        public async Task<ChannelInfo> GetChannelInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetChannelInfo", null);
            return new ChannelInfo()
            {
                Channel = Convert.ToUInt16(document.Descendants("NewChannel").First().Value),
                PossibleChannels = document.Descendants("NewPossibleChannels").First().Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select((entry) => UInt16.Parse(entry.Trim())).ToList()
            };
        }

        /// <summary>
        /// Method to set the channel
        /// </summary>
        /// <param name="channel">the new channel</param>
        /// <returns></returns>
        public async Task SetChannelAsync(UInt16 channel)
        {
            await this.InvokeAsync("SetChannel", new SOAP.SoapRequestParameter("NewChannel", channel));
        }

        /// <summary>
        /// Method to get the total assiciations
        /// </summary>
        /// <returns>the total associations</returns>
        public async Task<UInt16> GetTotalAssociationsAsync()
        {
            XDocument document = await this.InvokeAsync("GetTotalAssociations", null);
            return Convert.ToUInt16(document.Descendants("NewTotalAssociations").First().Value);
        }

        /// <summary>
        /// Method to get a generic associated device info
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>the device info</returns>
        public async Task<WLANDeviceInfo> GetGenericAssociatedDeviceInfoAsync(int index)
        {
            XDocument document = await this.InvokeAsync("GetGenericAssociatedDeviceInfo", new SOAP.SoapRequestParameter("NewAssociatedDeviceIndex", index));
            return new WLANDeviceInfo()
            {
                MACAddress = document.Descendants("NewAssociatedDeviceMACAddress").First().Value,
                IPAddress = IPAddress.TryParse(document.Descendants("NewAssociatedDeviceIPAddress").First().Value, out IPAddress ip) ? ip : IPAddress.None,
                DeviceAuthState = document.Descendants("NewAssociatedDeviceAuthState").First().Value == "0",
                Speed = Convert.ToUInt16(document.Descendants("NewX_AVM-DE_Speed").First().Value),
                SignalStrength = Convert.ToUInt16(document.Descendants("NewX_AVM-DE_SignalStrength").First().Value)
            };
        }

        /// <summary>
        /// Method to get the associated devices
        /// </summary>
        /// <returns>the associated devices</returns>
        public async Task<IEnumerable<WLANDeviceInfo>> GetAssociatedDevicesAsync()
        {
            List<WLANDeviceInfo> devices = new List<WLANDeviceInfo>();

            for (int i = 0; i < await this.GetTotalAssociationsAsync(); i++)
                devices.Add(await this.GetGenericAssociatedDeviceInfoAsync(i));

            return devices;
        }

        /// <summary>
        /// Method to get a specific associated device by the mac address
        /// </summary>
        /// <param name="macAddress">the mac address</param>
        /// <returns>the device info</returns>
        public async Task<WLANDeviceInfo> GetSpecificAssociatedDeviceInfoAsync(string macAddress)
        {
            XDocument document = await this.InvokeAsync("GetSpecificAssociatedDeviceInfo", new SOAP.SoapRequestParameter("NewAssociatedDeviceMACAddress", macAddress));
            return new WLANDeviceInfo()
            {
                MACAddress = macAddress,
                IPAddress = IPAddress.TryParse(document.Descendants("NewAssociatedDeviceIPAddress").First().Value, out IPAddress ip) ? ip : IPAddress.None,
                DeviceAuthState = document.Descendants("NewAssociatedDeviceAuthState").First().Value == "0",
                Speed = Convert.ToUInt16(document.Descendants("NewX_AVM-DE_Speed").First().Value),
                SignalStrength = Convert.ToUInt16(document.Descendants("NewX_AVM-DE_SignalStrength").First().Value)
            };
        }

        /// <summary>
        /// Method to get a specific associated device by the ip address
        /// </summary>
        /// <param name="ipAddress">the ip address</param>
        /// <returns>the device info</returns>
        public async Task<WLANDeviceInfo> GetSpecificAssociatedDeviceInfoByIpAsync(IPAddress ipAddress)
        {
            XDocument document = await this.InvokeAsync("GetSpecificAssociatedDeviceInfoByIp", new SOAP.SoapRequestParameter("NewAssociatedDeviceIPAddress", ipAddress.ToString()));
            return new WLANDeviceInfo()
            {
                MACAddress = document.Descendants("NewAssociatedDeviceMACAddress").First().Value,
                IPAddress = ipAddress,
                DeviceAuthState = document.Descendants("NewAssociatedDeviceAuthState").First().Value == "0",
                Speed = Convert.ToUInt16(document.Descendants("NewX_AVM-DE_Speed").First().Value),
                SignalStrength = Convert.ToUInt16(document.Descendants("NewX_AVM-DE_SignalStrength").First().Value)
            };
        }

        /// <summary>
        /// Method to enable or disable the surfstick
        /// </summary>
        /// <param name="enabled">new enabled value</param>
        /// <returns></returns>
        public async Task SetStickSurfEnableAsync(bool enabled)
        {
            await this.InvokeAsync("X_AVM-DE_SetStickSurfEnable", new SOAP.SoapRequestParameter("NewStickSurfEnable", enabled ? "1" : "0"));
        }

        /// <summary>
        /// Method to get if the connection is ip tv optimized
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetIPTVOptimizedAsync()
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_GetIPTVOptimized", null);
            return document.Descendants("NewX_AVM-DE_IPTVoptimize").First().Value == "1";            
        }

        /// <summary>
        /// Method to set optimize state for ip tv
        /// </summary>
        /// <param name="optimize">the new optimize state</param>
        /// <returns></returns>
        public async Task SetIPTVOptimizedAsync(bool optimize)
        {
            await this.InvokeAsync("X_AVM-DE_SetIPTVOptimized", new SOAP.SoapRequestParameter("NewX_AVM-DE_IPTVoptimize", optimize ? "1" : "0"));
        }

        /// <summary>
        /// Method to get night control informations
        /// </summary>
        /// <returns>the night control informations</returns>
        public async Task<NightControlInfo> GetNightControlAsync()
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_GetNightControl", null);
            return new NightControlInfo()
            {
                NightControl = document.Descendants("NewNightControl").First().Value,
                NightTimeControlNoForcedOff = document.Descendants("NewNightTimeControlNoForcedOff").First().Value == "1"
            };
        }

        /// <summary>
        /// Method to get wps informations
        /// </summary>
        /// <returns></returns>
        public async Task<WPSInfo> GetWPSInfoAsync()
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_GetWPSInfo", null);
            return new WPSInfo()
            {
                Mode = (WPSMode)Enum.Parse(typeof(WPSMode), document.Descendants("NewX_AVM-DE_WPSMode").First().Value),
                Status = (WPSStatus)Enum.Parse(typeof(WPSStatus), document.Descendants("NewX_AVM-DE_WPSStatus").First().Value)
            };
        }

        /// <summary>
        /// Method to get paket statistics
        /// </summary>
        /// <returns>the packet statistics</returns>
        public async Task<WLanStatistics> GetStatisticsAsync()
        {
            XDocument document = await this.InvokeAsync("GetStatistics", null);
            return this.FillWLanStatistics(document);
        }

        /// <summary>
        /// Method to get paket statistics
        /// </summary>
        /// <returns>the packet statistics</returns>
        public async Task<WLanStatistics> GetPacketStatisticsAsync()
        {
            XDocument document = await this.InvokeAsync("GetPacketStatistics", null);
            return this.FillWLanStatistics(document);
        }

        /// <summary>
        /// Method to fill packet statistics
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private WLanStatistics FillWLanStatistics(XDocument document)
        {
            return new WLanStatistics()
            {
                TotalPacketsSent = Convert.ToUInt64(document.Descendants("NewTotalPacketsSent").First().Value),
                TotalPacketsReceived = Convert.ToUInt64(document.Descendants("NewTotalPacketsReceived").First().Value)
            };
        }

        /// <summary>
        /// Method to enable or disable the 5GHz WLAN
        /// </summary>
        /// <returns></returns>
        public async Task SetHighFrequencyBandAsync(bool enableHighFrequency)
        {
            XDocument document = await this.InvokeAsync("X_SetHighFrequencyBand", new SOAP.SoapRequestParameter("NewEnableHighFrequency", enableHighFrequency ? 1 : 0));
        }
    }
}
