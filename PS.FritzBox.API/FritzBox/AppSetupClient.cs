using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// client for app setup service
    /// </summary>
    public class AppSetupClient : FritzTR64Client
    {
        #region COnstruction / Destruction
        [Obsolete("Use DeviceLocator for finding devices and get the service from the FritzDevice.")]
        public AppSetupClient(string url, int timeout) : base(url, timeout)
        {
        }
        [Obsolete("Use DeviceLocator for finding devices and get the service from the FritzDevice.")]
        public AppSetupClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }
        [Obsolete("Use DeviceLocator for finding devices and get the service from the FritzDevice.")]
        public AppSetupClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }
        [Obsolete("Use DeviceLocator for finding devices and get the service from the FritzDevice.")]
        public AppSetupClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }
        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/x_appsetup";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:X_AVM-DE_AppSetup:1";

        /// <summary>
        /// Method to get the app setup info
        /// </summary>
        /// <returns>the app setup info</returns>
        public async Task<AppSetupInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);

            AppSetupInfo info = new AppSetupInfo();
            info.MinCharsAppId = Convert.ToUInt16(document.Descendants("NewMinCharsAppId").First().Value);
            info.MaxCharsAppId = Convert.ToUInt16(document.Descendants("NewMaxCharsAppId").First().Value);
            info.AllowedCharsAppId = document.Descendants("NewAllowedCharsAppId").First().Value;
            info.MinCharsAppDisplayName = Convert.ToUInt16(document.Descendants("NewMinCharsAppDisplayName").First().Value);
            info.MaxCharsAppDisplayName = Convert.ToUInt16(document.Descendants("NewMaxCharsAppDisplayName").First().Value);
            info.MinCharsAppUsername = Convert.ToUInt16(document.Descendants("NewMinCharsAppUsername").First().Value);
            info.MaxCharsAppUsername = Convert.ToUInt16(document.Descendants("NewMaxCharsAppUsername").First().Value);
            info.AllowedCharsAppUsername = document.Descendants("NewAllowedCharsAppUsername").First().Value;
            info.MinCharsAppPassword = Convert.ToUInt16(document.Descendants("NewMinCharsAppPassword").First().Value);
            info.MaxCharsAppPassword = Convert.ToUInt16(document.Descendants("NewMaxCharsAppPassword").First().Value);
            info.AllowedCharsAppPassword = document.Descendants("NewAllowedCharsAppPassword").First().Value;
            info.MinCharsIPSecIdentifier = Convert.ToInt16(document.Descendants("NewMinCharsIPSecIdentifier").First().Value);
            info.MaxCharsIPSecIdentifier = Convert.ToUInt16(document.Descendants("NewMaxCharsIPSecIdentifier").First().Value);
            info.AllowedCharsIPSecIdentifier = document.Descendants("NewAllowedCharsIPSecIdentifier").First().Value;
            info.MinCharsIPSecPreSharedKey = Convert.ToUInt16(document.Descendants("NewMinCharsIPSecPreSharedKey").First().Value);
            info.MaxCharsIPSecPreSharedKey = Convert.ToUInt16(document.Descendants("NewMaxCharsIPSecPreSharedKey").First().Value);
            info.AllowedCharsIPSecPreSharedKey = document.Descendants("NewAllowedCharsIPSecPreSharedKey").First().Value;
            info.MinCharsIPSecXauthUsername = Convert.ToUInt16(document.Descendants("NewMinCharsIPSecXauthUsername").First().Value);
            info.MaxCharsIPSecXauthUsername = Convert.ToUInt16(document.Descendants("NewMaxCharsIPSecXauthUsername").First().Value);
            info.AllowedCharsIPSecXauthUsername = document.Descendants("NewAllowedCharsIPSecXauthUsername").First().Value;
            info.MinCharsIPSecXauthPassword = Convert.ToUInt16(document.Descendants("NewMinCharsIPSecXauthPassword").First().Value);
            info.MaxCharsIPSecXauthPassword = Convert.ToUInt16(document.Descendants("NewMaxCharsIPSecXauthPassword").First().Value);
            info.AllowedCharsIPSecXauthPassword = document.Descendants("NewAllowedCharsIPSecXauthPassword").First().Value;
            info.AllowedCharsCryptAlgos = document.Descendants("NewAllowedCharsCryptAlgos").First().Value;
            info.AllowedCharsAppAVMAddress = document.Descendants("NewAllowedCharsAppAVMAddress").First().Value;

            return info;
        }

        /// <summary>
        /// Method to get the config values
        /// </summary>
        /// <returns>the config</returns>
        public async Task<AppConfigRights> GetConfigAsync()
        {
            XDocument document = await this.InvokeAsync("GetConfig", null);

            AppConfigRights rights = new AppConfigRights();
            rights.InternetRights = document.Descendants("NewInternetRights").First().Value == "1";
            rights.AccessFromInternet = document.Descendants("NewAccessFromInternet").First().Value == "1";
            rights.ConfigRight = (RightEnum)Enum.Parse(typeof(RightEnum), document.Descendants("NewConfigRight").First().Value);
            rights.AppRight = (RightEnum)Enum.Parse(typeof(RightEnum), document.Descendants("NewAppRight").First().Value);
            rights.PhoneRight = (RightEnum)Enum.Parse(typeof(RightEnum), document.Descendants("NewPhoneRight").First().Value);
            rights.NasRight = (RightEnum)Enum.Parse(typeof(RightEnum), document.Descendants("NewNasRight").First().Value);
            rights.DialRight = (RightEnum)Enum.Parse(typeof(RightEnum), document.Descendants("NewDialRight").First().Value);
            rights.HomeautoRight = (RightEnum)Enum.Parse(typeof(RightEnum), document.Descendants("NewHomeautoRight").First().Value);

            return rights;
        }

        /// <summary>
        /// Method to register an app
        /// </summary>
        /// <param name="info">the app info</param>
        /// <returns></returns>
        public async Task RegisterAppAsync(AppInfo info)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewAppDeviceMAC", info.AppDeviceMAC),
                new SOAP.SoapRequestParameter("NewAppDisplayName", info.AppDisplayName),
                new SOAP.SoapRequestParameter("NewAppId", info.AppId),
                new SOAP.SoapRequestParameter("NewAppInternetRights", info.AppInternetRights ? "1" : "0"),
                new SOAP.SoapRequestParameter("NewAppPassword", info.AppPassword),
                new SOAP.SoapRequestParameter("NewAppRight", info.AppRight.ToString()),
                new SOAP.SoapRequestParameter("NewAppUsername", info.AppUsername),
                new SOAP.SoapRequestParameter("NewHomeautoRight", info.HomeautoRight.ToString()),
                new SOAP.SoapRequestParameter("NewNASRight", info.NASRight.ToString()),
                new SOAP.SoapRequestParameter("NewPhoneRight", info.PhoneRight.ToString())
            };

            await this.InvokeAsync("RegisterApp", parameters.ToArray());
        }

        /// <summary>
        /// Configuration of a VPN (IPsec) access for the app instance. Every app instance can have 
        /// at most only one VPN access configuration.
        /// </summary>
        /// <param name="info">the vpn config info</param>
        /// <returns></returns>
        public async Task SetAppVPNASync(AppVPNInfo info)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewAppId", info.AppId),
                new SOAP.SoapRequestParameter("NewIPSecIdentifier", info.IPSecIdentifier),
                new SOAP.SoapRequestParameter("NewIPSecPreSharedKey", info.IPSecPreSharedKey),
                new SOAP.SoapRequestParameter("NewIPSecXauthPassword", info.IPSecXauthPassword),
                new SOAP.SoapRequestParameter("NewIPSecXauthUsername", info.IPSecXauthUsername)

            };
            await this.InvokeAsync("SetAppVPN", parameters.ToArray());
        }

        /// <summary>
        /// Configuration of a message receiver for the app instance. Every app instance can have at 
        /// most only one message receiver configuration.
        /// </summary>
        /// <param name="config">the config</param>
        /// <returns>the box sender id</returns>
        public async Task<string> SetAppMessageReceiverAsync(AppMessageReceiverConfig config)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewAppAVMAddress", config.AppAVMAddress),
                new SOAP.SoapRequestParameter("NewAppAVMPasswordHash", config.AppAVMPasswordHash),
                new SOAP.SoapRequestParameter("NewAppId", config.AppId),
                new SOAP.SoapRequestParameter("NewCryptAlgos", config.CryptAlgos),
                new SOAP.SoapRequestParameter("NewEncryptionSecret", config.EncryptionSecret)
            };

            XDocument document = await this.InvokeAsync("SetAppMessageReceiver", parameters.ToArray());
            return document.Descendants("NewBoxSenderId").First().Value;
        }

        /// <summary>
        /// Reset an event specified by a event ID. If more than one event with the same ID exist, only one event is reset. 
        /// </summary>
        /// <param name="eventID">the id of the event to reset</param>
        public async Task ResetEventAsync(UInt32 eventID)
        {
            await this.InvokeAsync("ResetEvent", new SOAP.SoapRequestParameter("NewEventId", eventID));
        }
    }
}
