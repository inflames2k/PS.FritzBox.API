using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// client for wan ip connection interface
    /// </summary>
    public class WANIPConnectionClient : FritzTR64Client
    {
        public WANIPConnectionClient(string url, int timeout) : base(url, timeout)
        {
        }

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/tr064/upnp/control/WANIPConn1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:WANIPConnection:1";

        /// <summary>
        /// Method to get the connection info
        /// </summary>
        /// <returns></returns>
        public async Task<WANIPConnectionInfo> GetInfo()
        {
            XDocument document = await this.Invoke("GetInfo", null);
            WANIPConnectionInfo info = new WANIPConnectionInfo();

            // connection status values
            info.ConnectionStatus.ConnectionStatus = document.Descendants("NewConnectionStatus").First().Value;
            info.ConnectionStatus.LastConnectionError = document.Descendants("NewLastConnectionError").First().Value;
            info.ConnectionStatus.Uptime = Convert.ToUInt32(document.Descendants("NewUptime").First().Value);
            // connecton type values
            info.ConnectionType.ConnectionType = document.Descendants("NewConnectionType").First().Value;
            info.ConnectionType.PossibleConnectionTypes = document.Descendants("NewPossibleConnectionTypes").First().Value;
            
            // nat rsip 
            info.NATRSIPStatus.RSIPAvailable = Convert.ToBoolean(document.Descendants("NewRSIPAvailable").First().Value);
            info.NATRSIPStatus.NATEnabled = Convert.ToBoolean(document.Descendants("NewNATEnabled").First().Value);
            info.ExternalIPAddress = document.Descendants("NewExternalIPAddress").First().Value;
            info.Name = document.Descendants("NewName").First().Value;
            info.DNSEnabled = Convert.ToBoolean(document.Descendants("NewDNSEnabled").First().Value);
            info.DNSServers = document.Descendants("NewDNSServers").First().Value;
            info.MACAddress = document.Descendants("NewMACAddress").First().Value;
            info.ConnectionTrigger = document.Descendants("NewConnectionTrigger").First().Value;
            info.Enabled = Convert.ToBoolean(document.Descendants("NewEnable").First().Value);
            info.DNSOverrideAllowed = Convert.ToBoolean(document.Descendants("NewDNSOverrideAllowed").First().Value);
            info.RouteProtocolRx = document.Descendants("NewRouteProtocolRx").First().Value;

            return info;
        }

        /// <summary>
        /// Method to get the connection type info
        /// </summary>
        /// <returns>the connection type info</returns>
        public async Task<ConnectionTypeInfo> GetConnectionTypeInfo()
        {
            XDocument document = await this.Invoke("GetConnectionTypeInfo", null);
            ConnectionTypeInfo info = new ConnectionTypeInfo();
            info.ConnectionType = document.Descendants("NewConnectionType").First().Value;
            info.PossibleConnectionTypes = document.Descendants("NewPossibleConnectionTypes").First().Value;
            return info;
        }

        /// <summary>
        /// Method to set the connection type
        /// </summary>
        /// <param name="connectionType"></param>
        public async void SetConnectionType(string connectionType)
        {
            var parameter = new SoapRequestParameter("NewConnectionType", connectionType);
            await this.Invoke("SetConnectionType", parameter);
        }

        /// <summary>
        /// Method to get the connection state info
        /// </summary>
        /// <returns>the state info</returns>
        public async Task<ConnectionStatusInfo> GetStatusInfo()
        {
            XDocument document = await this.Invoke("GetStatusInfo", null);
            ConnectionStatusInfo info = new ConnectionStatusInfo();
            info.ConnectionStatus = document.Descendants("NewConnectionStatus").First().Value;
            info.LastConnectionError = document.Descendants("NewLastConnectionError").First().Value;
            info.Uptime = Convert.ToUInt32(document.Descendants("NewUptime").First().Value);

            return info;
        }

        /// <summary>
        /// Method to get the nat rsip status
        /// </summary>
        /// <returns>the nat rsip status</returns>
        public async Task<NATRSIPStatus> GetNATRSIPStatus()
        {
            XDocument document = await this.Invoke("GetNATRSIPStatus", null);
            NATRSIPStatus info = new NATRSIPStatus();

            info.RSIPAvailable = Convert.ToBoolean(document.Descendants("NewRSIPAvailable").First().Value);
            info.NATEnabled = Convert.ToBoolean(document.Descendants("NewNATEnabled").First().Value);
            
            return info;
        }

        /// <summary>
        /// Method to set the connection trigger
        /// </summary>
        /// <param name="trigger">the new connection trigger</param>
        public async void SetConnectionTrigger(string trigger)
        {
            var parameter = new SoapRequestParameter("NewConnectionTrigger", trigger);
            await this.Invoke("SetConnectionTrigger", parameter);
        }

        /// <summary>
        /// Method to force termination
        /// </summary>
        public async void ForceTermination()
        {
            await this.Invoke("ForceTermination", null);
        }

        /// <summary>
        /// Method to request a connection
        /// </summary>
        public async void RequestConnection()
        {
            await this.Invoke("RequestConnection", null);
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
        /// Method to get the number of port mappings
        /// </summary>
        /// <returns></returns>
        public async Task<UInt16> GetPortMappingNumberOfEntries()
        {
            XDocument document = await this.Invoke("GetPortMappingNumberOfEntries", null);
            return Convert.ToUInt16(document.Descendants("NewPortMappingNumberOfEntries").First().Value);
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
        /// Method to set the route protocol
        /// </summary>
        /// <param name="routeProtocol">the new route protocol</param>
        public async void SetRouteProtocolRx(string routeProtocol)
        {
            SoapRequestParameter parameter = new SoapRequestParameter("NewRouteProtocolRX", routeProtocol);
            await this.Invoke("SetRouteProtocolRx", parameter);
        }

        /// <summary>
        /// Method to set the idle disconnect time
        /// </summary>
        /// <param name="idleDisconnectTime">the disconnect time</param>
        public async void SetIdleDisconnectTime(UInt32 idleDisconnectTime)
        {
            SoapRequestParameter parameter = new SoapRequestParameter("NewIdleDisconnectTime", idleDisconnectTime);
            await this.Invoke("SetIdleDisconnectTime", parameter);
        }

        // GetGenericPortMappingEntry
        // GetSpecificPortMappingEntry
        // AddPortMapping
        // DeletePortMapping

    }
}
