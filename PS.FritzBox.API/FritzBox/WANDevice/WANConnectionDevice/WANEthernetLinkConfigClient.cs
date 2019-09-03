using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    /// <summary>
    /// client for WAN Ethernet Link Config service
    /// </summary>
    public class WANEthernetLinkConfigClient : FritzTR64Client
    {
        #region Construction / Destruction

        public WANEthernetLinkConfigClient(string url, int timeout) : base(url, timeout)
        {
        }

        public WANEthernetLinkConfigClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        public WANEthernetLinkConfigClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        public WANEthernetLinkConfigClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/wanethlinkconfig1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>                                 
        protected override string RequestNameSpace => "urn:dslforum-org:service:WANEthernetLinkConfig:1";

        /// <summary>
        /// Method to get the ethernet link status
        /// </summary>
        /// <returns>the ethernet link status</returns>
        public async Task<EthernetLinkStatus> GetEthernetLinkStatusAsync()
        {
            XDocument document = await this.InvokeAsync("GetEthernetLinkStatus", null);
            return (EthernetLinkStatus)Enum.Parse(typeof(EthernetLinkStatus), document.Descendants("NewEthernetLinkStatus").First().Value);
        }
    }
}
