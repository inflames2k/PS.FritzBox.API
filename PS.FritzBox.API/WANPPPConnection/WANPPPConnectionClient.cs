using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class for getting informations about the wan ppp connection
    /// </summary>
    public class WANPPPConnectionClient : FritzTR64Client
    {
        public WANPPPConnectionClient(string url, int timeout) : base(url, timeout)
        {
        }

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/tr064/upnp/control/wanpppconn1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>                                 
        protected override string RequestNameSpace => "urn:dslforum-org:service:WANPPPConnection:1";

        /// <summary>
        /// Method to get the wan ppp connection info
        /// </summary>
        /// <returns></returns>
        public async Task<WANPPPConnectionInfo> GetInfo()
        {
            WANPPPConnectionInfo info = new WANPPPConnectionInfo();
            XDocument document = await this.Invoke("GetInfo", null);

            // connection status values
            info.ConnectionStatus.ConnectionStatus = document.Descendants("NewConnectionStatus").First().Value;
            info.ConnectionStatus.LastConnectionError = document.Descendants("NewLastConnectionError").First().Value;
            info.ConnectionStatus.Uptime = Convert.ToUInt32(document.Descendants("NewUpTime").First().Value);
            // connecton type values
            info.ConnectionType.ConnectionType = document.Descendants("NewConnectionType").First().Value;
            info.ConnectionType.PossibleConnectionTypes = document.Descendants("NewPossibleConnectionTypes").First().Value;

            // link layer max bitrate values
            info.LinkLayerMaxBitRates.DownstreamMaxBitRate = Convert.ToUInt32(document.Descendants("NewDownstreamMaxBitRate").First().Value);
            info.LinkLayerMaxBitRates.UpstreamMaxBitRate = Convert.ToUInt32(document.Descendants("UpstreamMaxBitRate").First().Value);

            // nat rsip 
            info.NATRSIPStatus.RSIPAvailable = Convert.ToBoolean(document.Descendants("NewRSIPAvailable").First().Value);
            info.NATRSIPStatus.NATEnabled = Convert.ToBoolean(document.Descendants("NewNATEnabled").First().Value);

            info.ExternalIPAddress = document.Descendants("NewExternalIPAddress").First().Value;
            info.IdleDisconnectTime = Convert.ToUInt32(document.Descendants("NewIdleDisconnectTime").First().Value);
            info.Name = document.Descendants("NewName").First().Value;
            info.TransportType = document.Descendants("NewTransportType").First().Value;
            info.UserName = document.Descendants("NewUserName").First().Value;

            info.PPPoEACName = document.Descendants("NewPPPoEACName").First().Value;
            info.PPPoEServiceName = document.Descendants("NewPPPoEServiceName").First().Value;
            info.RemoteIPAddress = document.Descendants("NewRemoteIPAddress").First().Value;
            info.RouteProtocolRx = document.Descendants("NewRouteProtocolRx").First().Value;

            return info;
        }

        /// <summary>
        /// Method to get the connection type info
        /// </summary>
        /// <returns>the connection type info</returns>
        public async Task<ConnectionTypeInfo> GetConnectionTypeInfo()
        {
            ConnectionTypeInfo info = new ConnectionTypeInfo();

            XDocument document = await this.Invoke("GetConnectionTypeInfo", null);

            info.ConnectionType = document.Descendants("NewConnectionType").First().Value;
            info.PossibleConnectionTypes = document.Descendants("NewPossibleConnectionTypes").First().Value;

            return info;
        }

        /// <summary>
        /// Method to set the connection type
        /// </summary>
        /// <param name="connectionType">the new connection type</param>
        public async void SetConnectionType(string connectionType)
        {
            var parameter = new SoapRequestParameter("NewConnectionType", connectionType);
            XDocument document = await this.Invoke("SetConnectionType", parameter);
        }

        public async Task<ConnectionStatusInfo> GetStatusInfo()
        {
            ConnectionStatusInfo info = new ConnectionStatusInfo();

            XDocument document = await this.Invoke("GetStatusInfo", null);

            info.ConnectionStatus = document.Descendants("NewConnectionStatus").First().Value;
            info.LastConnectionError = document.Descendants("NewLastConnectionError").First().Value;
            info.Uptime = Convert.ToUInt32(document.Descendants("NewUpTime").First().Value);
            return info;
        }

        /// <summary>
        /// Method to get the username
        /// </summary>
        /// <returns>the user name</returns>
        public async Task<string> GetUserName()
        {
            XDocument document = await this.Invoke("GetUserName", null);
            return document.Descendants("NewUserName").First().Value;
        }

        /// <summary>
        /// Method to set the user name
        /// </summary>
        /// <param name="userName">the new username</param>
        public async void SetUserName(string userName)
        {
            var parameter = new SoapRequestParameter("NewUserName", userName);
            XDocument document = await this.Invoke("SetUserName", parameter);
        }

        /// <summary>
        /// Method to set the password
        /// </summary>
        /// <param name="password">the new password</param>
        public async void SetPassword(string password)
        {
            var parameter = new SoapRequestParameter("NewPassword", password);
            XDocument document = await this.Invoke("SetPassword", parameter);
        }

        /// <summary>
        /// Method to get the nat rsip status
        /// </summary>
        /// <returns>the nat rsipstatus</returns>
        public async Task<NATRSIPStatus> GetNATRSIPStatus()
        {
            NATRSIPStatus status = new NATRSIPStatus();
            XDocument document = await this.Invoke("GetNATRSIPStatus", null);

            status.NATEnabled = Convert.ToBoolean(document.Descendants("NewNATEnabled").First().Value);
            status.RSIPAvailable = Convert.ToBoolean(document.Descendants("NewRSIPAvailable").First().Value);

            return status;
        }

        /// <summary>
        /// Method to force the termination of the ppp connection
        /// </summary>
        public async void ForceTermination()
        {
            XDocument document = await this.Invoke("ForceTermination", null);
        }

        /// <summary>
        /// Method to request a ppp connection
        /// </summary>
        public async void RequestConnection()
        {
            XDocument document = await this.Invoke("RequestConnection", null);
        }

        /// <summary>
        /// Method to get the external ip address
        /// </summary>
        /// <returns>the external ip address</returns>
        public async Task<string> GetExternalIPAddress()
        {
            XDocument document = await this.Invoke("GetExternalIPAddress", null);
            return document.Descendants("NewExternalIPAddress").First().Value;
        }

        /// <summary>
        /// Method to get the dns servers
        /// </summary>
        /// <returns>the dns servers</returns>
        public async Task<string> GetDNSServers()
        {
            XDocument document = await this.Invoke("X_GetDNSServers", null);
            return document.Descendants("NewDNSServers").First().Value;
        }

        /// <summary>
        /// Method to set the dns servers
        /// </summary>
        /// <param name="dnsServers">the dns servers</param>
        public async void SetDNSServers(string dnsServers)
        {
            var parameter = new SoapRequestParameter("NewDNSServers", dnsServers);
            XDocument document = await this.Invoke("X_SetDNSServers", parameter);
        }

        /// <summary>
        /// Method to get the link layer max bitrates
        /// </summary>
        /// <returns>the link layer max bitrates</returns>
        public async Task<LinkLayerMaxBitRates> GetLinkLayerMaxBitRates()
        {
            LinkLayerMaxBitRates bitRates = new LinkLayerMaxBitRates();
            XDocument document = await this.Invoke("GetLinkLayerMaxBitRates", null);

            bitRates.DownstreamMaxBitRate = Convert.ToUInt32(document.Descendants("NewDownstreamMaxBitRate").First().Value);
            bitRates.UpstreamMaxBitRate = Convert.ToUInt32(document.Descendants("UpstreamMaxBitRate").First().Value);
            return bitRates;
        }
    }
}
