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
    /// client for wlan configuration service for the Guest network
    /// </summary>
    public class WLANConfigurationClient3 : WLANConfigurationClient
    {
        #region Construction / Destruction

        
        public WLANConfigurationClient3(string url, int timeout) : base(url, timeout)
        {
        }

        
        public WLANConfigurationClient3(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        
        public WLANConfigurationClient3(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        
        public WLANConfigurationClient3(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/wlanconfig3";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:WLANConfiguration:3";
    }
}
