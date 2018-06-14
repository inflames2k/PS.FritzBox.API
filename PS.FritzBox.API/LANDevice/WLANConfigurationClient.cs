using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    }
}
