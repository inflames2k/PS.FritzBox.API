using PS.FritzBox.API.Base;
using PS.FritzBox.API.SOAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    /// <summary>
    /// client for wan ip connection service
    /// </summary>
    public class WANIPConnectionClient : FritzTR64Client
    {
        #region Construction / Destruction
        [Obsolete("Use DeviceLocator for finding devices and get the service from the FritzDevice.")]
        public WANIPConnectionClient(string url, int timeout) : base(url, timeout)
        {
        }
        [Obsolete("Use DeviceLocator for finding devices and get the service from the FritzDevice.")]
        public WANIPConnectionClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }
        [Obsolete("Use DeviceLocator for finding devices and get the service from the FritzDevice.")]
        public WANIPConnectionClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }
        [Obsolete("Use DeviceLocator for finding devices and get the service from the FritzDevice.")]
        public WANIPConnectionClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/igdupnp/control/WANIPConn1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:schemas-upnp-org:service:WANIPConnection:1";


        /// <summary>
        /// Method to get the connection type info
        /// </summary>
        /// <returns>the connection type info</returns>
        public async Task<ConnectionTypeInfo> GetConnectionTypeInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetConnectionTypeInfo", null);
            ConnectionTypeInfo info = new ConnectionTypeInfo();
            info.ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), document.Descendants("NewConnectionType").First().Value);
            info.PossibleConnectionTypes = (PossibleConnectionTypes)Enum.Parse(typeof(PossibleConnectionTypes), document.Descendants("NewPossibleConnectionTypes").First().Value);
            return info;
        }

        /// <summary>
        /// Method to set the connection type
        /// </summary>
        /// <param name="connectionType"></param>
        public async Task SetConnectionTypeAsync(string connectionType)
        {
            var parameter = new SoapRequestParameter("NewConnectionType", connectionType);
            await this.InvokeAsync("SetConnectionType", parameter);
        }

        /// <summary>
        /// Method to get the connection state info
        /// </summary>
        /// <returns>the state info</returns>
        public async Task<ConnectionStatusInfo> GetStatusInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetStatusInfo", null);
            ConnectionStatusInfo info = new ConnectionStatusInfo();
            info.ConnectionStatus = (ConnectionStatus)Enum.Parse(typeof(ConnectionStatus), document.Descendants("NewConnectionStatus").First().Value);
            info.LastConnectionError = (ConnectionError)Enum.Parse(typeof(ConnectionError), document.Descendants("NewLastConnectionError").First().Value);
            info.Uptime = Convert.ToUInt32(document.Descendants("NewUptime").First().Value);

            return info;
        }

        /// <summary>
        /// Method to get the nat rsip status
        /// </summary>
        /// <returns>the nat rsip status</returns>
        public async Task<NATRSIPStatus> GetNATRSIPStatusAsync()
        {
            XDocument document = await this.InvokeAsync("GetNATRSIPStatus", null);
            NATRSIPStatus info = new NATRSIPStatus();

            info.NATEnabled = document.Descendants("NewNATEnabled").First().Value == "1";
            info.RSIPAvailable = document.Descendants("NewRSIPAvailable").First().Value == "1";

            return info;
        }

        /// <summary>
        /// Method to set the connection trigger
        /// </summary>
        /// <param name="trigger">the new connection trigger</param>
        public async Task SetConnectionTriggerAsync(string trigger)
        {
            var parameter = new SoapRequestParameter("NewConnectionTrigger", trigger);
            await this.InvokeAsync("SetConnectionTrigger", parameter);
        }

        /// <summary>
        /// Method to force termination
        /// </summary>
        public async Task ForceTerminationAsync()
        {
            await this.InvokeAsync("ForceTermination", null);
        }

        /// <summary>
        /// Method to request termination
        /// </summary>
        /// <returns></returns>
        public async Task RequestTerminationAsync()
        {
            XDocument document = await this.InvokeAsync("RequestTermination", null);
        }

        /// <summary>
        /// Method to request a connection
        /// </summary>
        public async Task RequestConnectionAsync()
        {
            await this.InvokeAsync("RequestConnection", null);
        }

        /// <summary>
        /// Method to get the dns servers
        /// </summary>
        /// <returns>the dns servers</returns>
        public async Task<IEnumerable<IPAddress>> GetDNSServersAsync()
        {
            List<IPAddress> addresses = new List<IPAddress>();
            XDocument document = await this.InvokeAsync("X_AVM_DE_GetDNSServer", null);
            addresses.Add(IPAddress.TryParse(document.Descendants("NewIPv4DNSServer1").First().Value, out IPAddress first) ? first : IPAddress.None);
            addresses.Add(IPAddress.TryParse(document.Descendants("NewIPv4DNSServer2").First().Value, out IPAddress second) ? second : IPAddress.None);
            
            return addresses;
        }

        /// <summary>
        /// Method to get the external ip address
        /// </summary>
        /// <returns>the external ip address</returns>
        public async Task<string> GetExternalIPAddressAsync()
        {
            XDocument document = await this.InvokeAsync("GetExternalIPAddress", null);
            return document.Descendants("NewExternalIPAddress").First().Value;
        }

        /// <summary>
        /// Method to set the route protocol
        /// </summary>
        /// <param name="routeProtocol">the new route protocol</param>
        public async Task SetRouteProtocolRxAsync(string routeProtocol)
        {
            SoapRequestParameter parameter = new SoapRequestParameter("NewRouteProtocolRX", routeProtocol);
            await this.InvokeAsync("SetRouteProtocolRx", parameter);
        }
    }
}
