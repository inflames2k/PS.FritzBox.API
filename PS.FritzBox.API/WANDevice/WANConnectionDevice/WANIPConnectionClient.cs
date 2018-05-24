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

        public WANIPConnectionClient(string url, int timeout) : base(url, timeout)
        {
        }

        public WANIPConnectionClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        public WANIPConnectionClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        public WANIPConnectionClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/tr064/upnp/control/wanipconnection1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:WANIPConnection:1";

        /// <summary>
        /// async Method to get the connection info
        /// </summary>
        /// <returns></returns>
        public async Task<WANIPConnectionInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);
            WANIPConnectionInfo info = new WANIPConnectionInfo();

            // connection status values
            info.ConnectionStatus.ConnectionStatus = (ConnectionStatus)Enum.Parse(typeof(ConnectionStatus), document.Descendants("NewConnectionStatus").First().Value);
            info.ConnectionStatus.LastConnectionError = (ConnectionError)Enum.Parse(typeof(ConnectionError), document.Descendants("NewLastConnectionError").First().Value);
            info.ConnectionStatus.Uptime = Convert.ToUInt32(document.Descendants("NewUptime").First().Value);
            // connecton type values
            info.ConnectionType.ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), document.Descendants("NewConnectionType").First().Value);
            info.ConnectionType.PossibleConnectionTypes = (PossibleConnectionTypes)Enum.Parse(typeof(PossibleConnectionTypes), document.Descendants("NewPossibleConnectionTypes").First().Value);
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
        public async Task<string> GetDNSServersAsync()
        {
            XDocument document = await this.InvokeAsync("X_GetDNSServers", null);
            return document.Descendants("NewDNSServers").First().Value;
        }

        /// <summary>
        /// Method to set the dns servers
        /// </summary>
        /// <param name="dnsServers">the dns servers</param>
        public async Task SetDNSServersAsync(string dnsServers)
        {
            var parameter = new SoapRequestParameter("NewDNSServers", dnsServers);
            XDocument document = await this.InvokeAsync("X_SetDNSServers", parameter);
        }

        /// <summary>
        /// Method to get the number of port mappings
        /// </summary>
        /// <returns></returns>
        public async Task<UInt16> GetPortMappingNumberOfEntriesAsync()
        {
            XDocument document = await this.InvokeAsync("GetPortMappingNumberOfEntries", null);
            return Convert.ToUInt16(document.Descendants("NewPortMappingNumberOfEntries").First().Value);
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

        /// <summary>
        /// Method to set the idle disconnect time
        /// </summary>
        /// <param name="idleDisconnectTime">the disconnect time</param>
        public async Task SetIdleDisconnectTimeAsync(UInt32 idleDisconnectTime)
        {
            SoapRequestParameter parameter = new SoapRequestParameter("NewIdleDisconnectTime", idleDisconnectTime);
            await this.InvokeAsync("SetIdleDisconnectTime", parameter);
        }

        /// <summary>
        /// Method to get a generic port mapping entry
        /// </summary>
        /// <param name="mappingIndex">the mapping index</param>
        /// <returns>the generic port mapping entry</returns>
        public async Task<PortMappingEntry> GetGenericPortMappingEntryAsync(int mappingIndex)
        {
            XDocument document = await this.InvokeAsync("GetGenericPortMappingEntry", new SoapRequestParameter("NewPortMappingIndex", mappingIndex));

            PortMappingEntry entry = new PortMappingEntry();
            entry.Description = document.Descendants("NewPortMappingDescription").First().Value;
            entry.Enabled = document.Descendants("NewENabled").First().Value == "1";
            entry.InternalHost = IPAddress.TryParse(document.Descendants("NewInternalClient").First().Value, out IPAddress internalHost) ? internalHost : IPAddress.None;
            entry.InternalPort = Convert.ToUInt16(document.Descendants("NewInternalPort").First().Value);
            entry.LeaseDuration = Convert.ToUInt32(document.Descendants("NewLeaseDuration").First().Value);
            entry.PortMappingProtocol = (PortMappingProtocol)Enum.Parse(typeof(PortMappingProtocol), document.Descendants("NewProtocol").First().Value);
            entry.RemoteHost = IPAddress.TryParse(document.Descendants("NewRemoteHost").First().Value, out IPAddress remoteHost) ? remoteHost : IPAddress.None;
            entry.ExternalPort = Convert.ToUInt16(document.Descendants("NewExternalPort").First().Value);

            return entry;
        }

        /// <summary>
        /// Method to get a specific port mapping entry
        /// </summary>
        /// <param name="remoteHost">the remote host</param>
        /// <param name="externalPort">the remote port</param>
        /// <param name="protocol">the protocol</param>
        /// <returns>the specific port mapping entry</returns>
        public async Task<PortMappingEntry> GetSpecificPortMappingEntryAsync(IPAddress remoteHost, UInt16 externalPort, PortMappingProtocol protocol)
        {
            List<SoapRequestParameter> parameters = new List<SoapRequestParameter>()
            {
                new SoapRequestParameter("NewRemoteHost", remoteHost.ToString()),
                new SoapRequestParameter("NewExternalPort", externalPort),
                new SoapRequestParameter("NewProtocol", protocol.ToString())
            };

            XDocument document = await this.InvokeAsync("GetSpecificPortMappingEntry", parameters.ToArray());

            PortMappingEntry entry = new PortMappingEntry()
            {
                RemoteHost = remoteHost,
                ExternalPort = externalPort,
                PortMappingProtocol = protocol
            };

            entry.Description = document.Descendants("NewPortMappingDescription").First().Value;
            entry.Enabled = document.Descendants("NewENabled").First().Value == "1";
            entry.InternalHost = IPAddress.TryParse(document.Descendants("NewInternalClient").First().Value, out IPAddress internalHost) ? internalHost : IPAddress.None;
            entry.InternalPort = Convert.ToUInt16(document.Descendants("NewInternalPort").First().Value);
            entry.LeaseDuration = Convert.ToUInt32(document.Descendants("NewLeaseDuration").First().Value);

            return entry;
        }

        /// <summary>
        /// Method to add a port mapping
        /// </summary>
        /// <param name="entry">the port mapping entry</param>
        /// <returns></returns>
        public async Task AddPortMappingAsync(PortMappingEntry entry)
        {
            List<SoapRequestParameter> parameters = new List<SoapRequestParameter>()
            {
                new SoapRequestParameter("NewRemoteHost", entry.RemoteHost.ToString()),
                new SoapRequestParameter("NewExternalPort", entry.ExternalPort),
                new SoapRequestParameter("NewProtocol", entry.PortMappingProtocol.ToString()),
                new SoapRequestParameter("NewInternalPort", entry.InternalPort),
                new SoapRequestParameter("NewInternalClient", entry.InternalHost),
                new SoapRequestParameter("NewEnabled", entry.Enabled ? 1 : 0),
                new SoapRequestParameter("NewPortMappingDescription", entry.Description),
                new SoapRequestParameter("NewLeaseDuration", entry.LeaseDuration)
            };

            await this.InvokeAsync("AddPortMapping", parameters.ToArray());
        }

        /// <summary>
        /// Method to delete a port mapping
        /// </summary>
        /// <param name="remoteHost">the remote host</param>
        /// <param name="externalPort">the external port</param>
        /// <param name="protocol">the protocol</param>
        /// <returns></returns>
        public async Task DeletePortMappingAsync(IPAddress remoteHost, UInt16 externalPort, PortMappingProtocol protocol)
        {
            List<SoapRequestParameter> parameters = new List<SoapRequestParameter>()
            {
                new SoapRequestParameter("NewRemoteHost", remoteHost.ToString()),
                new SoapRequestParameter("NewExternalPort", externalPort),
                new SoapRequestParameter("NewProtocol", protocol.ToString())
            };

            await this.InvokeAsync("DeletePortMapping", parameters.ToArray());
        }
    }
}
