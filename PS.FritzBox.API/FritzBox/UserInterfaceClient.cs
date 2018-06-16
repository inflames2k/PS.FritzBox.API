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
    /// client for user interface service
    /// </summary>
    public class UserInterfaceClient : FritzTR64Client
    {
        #region Construction / Destruction
        
        public UserInterfaceClient(string url, int timeout) : base(url, timeout)
        {
        }
        
        public UserInterfaceClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }
        
        public UserInterfaceClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }
        
        public UserInterfaceClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion  

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/userif";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:UserInterface:1";

        /// <summary>
        /// Method to get the info
        /// </summary>
        /// <returns>the info</returns>
        public async Task<UserInterfaceInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);
            return new UserInterfaceInfo()
            {                
                PasswordRequired = document.Descendants("NewPasswordRequired").First().Value == "1",
                PasswordUserSelectable = document.Descendants("NewPasswordUserSelectable").First().Value == "1",
                WarrantyDate = document.Descendants("NewWarrantyDate").First().Value,
                Version = document.Descendants("NewX_AVM-DE_Version").First().Value,
                DownloadUrl = document.Descendants("NewX_AVM-DE_DownloadURL").First().Value,
                InfoUrl = document.Descendants("NewX_AVM-DE_InfoURL").First().Value,                
                LaborVersion = document.Descendants("NewX_AVM-DE_LaborVersion").First().Value,
                UpdateState = new UpdateInfo()
                {
                    UpgradeAvailable = document.Descendants("NewUpgradeAvailable").First().Value == "1",
                    UpdateState = (UpdateState)Enum.Parse(typeof(UpdateState), document.Descendants("NewX_AVM-DE_UpdateState").First().Value),
                },
            };
        }

        /// <summary>
        /// Method to check for update
        /// </summary>
        /// <param name="laborVersion">the labor version</param>
        /// <returns></returns>
        public async Task CheckUpdateAsync(string laborVersion)
        {
            await this.InvokeAsync("X_AVM-DE_CheckUpdate", new SOAP.SoapRequestParameter("NewX_AVM-DE_LaborVersion", laborVersion));
        }

        /// <summary>
        /// Method to prepare the update
        /// </summary>
        /// <returns>the CGI Info with session id valid up to 60 seconds</returns>
        public async Task<CGIInfo> DoPrepareCGIAsync()
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_DoPrepareCGI", null);
            return new CGIInfo()
            {
                CGIPath = document.Descendants("NewX_AVM-DE_CGI").First().Value,
                SessionID = document.Descendants("NewX_AVM-DE_SesssionID").First().Value
            };
        }

        /// <summary>
        /// Method to do update
        /// </summary>
        /// <returns>the update state</returns>
        public async Task<UpdateInfo> DoUpdateAsync()
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_DoUpdate", null);
            return new UpdateInfo()
            {
                UpgradeAvailable = document.Descendants("NewUpgradeAvailable").First().Value == "1",
                UpdateState = (UpdateState)Enum.Parse(typeof(UpdateState), document.Descendants("NewX_AVM-DE_UpdateState").First().Value)
            };
        }

        /// <summary>
        /// Method to do manual update
        /// </summary>
        /// <param name="downloadUrl">the download url</param>
        /// <param name="allowDowngrade">flag if downgrade is valid</param>
        /// <returns></returns>
        public async Task DoManualUpdateAsync(string downloadUrl, bool allowDowngrade)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewX_AVM-DE_DownloadURL", downloadUrl),
                new SOAP.SoapRequestParameter("NewX_AVM-DE_AllowDowngrade", allowDowngrade ? "1" : "0")
            };

            await this.InvokeAsync("X_AVM-DE_DoManualUpdate", parameters.ToArray());
        }

        /// <summary>
        /// Method to get the extended update info
        /// </summary>
        /// <returns></returns>
        public async Task<XUpdateInfo> GetUpdateInfoAsync()
        {
            XDocument document =    await this.InvokeAsync("X_AVM-DE_GetInfo", null);

            return new XUpdateInfo()
            {
                AutoUpdateMode = (AutoUpdateMode)Enum.Parse(typeof(AutoUpdateMode), document.Descendants("NewX_AVM-DE_AutoUpdateMode").First().Value),
                UpdateTime = DateTime.TryParse(document.Descendants("NewX_AVM-DE_UpdateTime").First().Value, out DateTime updateTime) ? updateTime : default(DateTime),
                // info of the last firmware
                LastFirmware = new FirmwareInfo()
                {
                    LastFirmwareVersion = document.Descendants("NewX_AVM-DE_LastFwVersion").First().Value,
                    LastInfoUrl = document.Descendants("NewX_AVM-DE_LastInfoUrl").First().Value
                },
                CurrentFirmware = new FirmwareInfo()
                {
                    LastFirmwareVersion = document.Descendants("NewX_AVM-DE_CurrentFwVersion").First().Value
                },
                UpdateSuccess = (UpdateSuccessState)Enum.Parse(typeof(UpdateSuccessState), document.Descendants("NewX_AVM-DE_UpdateSuccessful").First().Value)
            };
        }

        /// <summary>
        /// Method to set the update config
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task SetConfigAsync(AutoUpdateMode mode)
        {
            await this.InvokeAsync("X_AVM-DE_SetConfig", new SOAP.SoapRequestParameter("NewX_AVM-DE_AutoUpdateMode", mode.ToString()));
        }
    }
}
