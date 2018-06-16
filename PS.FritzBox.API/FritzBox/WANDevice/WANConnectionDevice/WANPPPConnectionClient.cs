using System;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using PS.FritzBox.API.Base;
using PS.FritzBox.API.SOAP;
using System.Net;
using System.Collections.Generic;

namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    /// <summary>
    /// client for wan ppp connection service
    /// </summary>
    public class WANPPPConnectionClient : FritzTR64Client
    {
        #region Construction / Destruction
        
        public WANPPPConnectionClient(string url, int timeout) : base(url, timeout)
        {
        }
        
        public WANPPPConnectionClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }
        
        public WANPPPConnectionClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }
        
        public WANPPPConnectionClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/wanpppconn1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>                                 
        protected override string RequestNameSpace => "urn:dslforum-org:service:WANPPPConnection:1";

        /// <summary>
        /// Method to get the wan ppp connection info
        /// </summary>
        /// <returns></returns>
        public async Task<WANPPPConnectionInfo> GetInfoAsync()
        {
            WANPPPConnectionInfo info = new WANPPPConnectionInfo();
            XDocument document = await this.InvokeAsync("GetInfo", null);

            // connection status values
            info.ConnectionStatus.ConnectionStatus = (ConnectionStatus)Enum.Parse(typeof(ConnectionStatus), document.Descendants("NewConnectionStatus").First().Value);
            info.ConnectionStatus.LastConnectionError = (ConnectionError)Enum.Parse(typeof(ConnectionError), document.Descendants("NewLastConnectionError").First().Value);
            info.ConnectionStatus.Uptime = Convert.ToUInt32(document.Descendants("NewUptime").First().Value);
            // connecton type values
            info.ConnectionType.ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), document.Descendants("NewConnectionType").First().Value);
            info.ConnectionType.PossibleConnectionTypes = (PossibleConnectionTypes)Enum.Parse(typeof(PossibleConnectionTypes), document.Descendants("NewPossibleConnectionTypes").First().Value);

            // link layer max bitrate values
            info.LinkLayerMaxBitRates.DownstreamMaxBitRate = Convert.ToUInt32(document.Descendants("NewDownstreamMaxBitRate").First().Value);
            info.LinkLayerMaxBitRates.UpstreamMaxBitRate = Convert.ToUInt32(document.Descendants("NewUpstreamMaxBitRate").First().Value);

            // nat rsip 
            info.NATRSIPStatus.RSIPAvailable = document.Descendants("NewRSIPAvailable").First().Value == "1";
            info.NATRSIPStatus.NATEnabled = document.Descendants("NewNATEnabled").First().Value == "1";

            info.ExternalIPAddress = IPAddress.TryParse(document.Descendants("NewExternalIPAddress").First().Value, out IPAddress external) ? external : IPAddress.None;
            info.IdleDisconnectTime = Convert.ToUInt32(document.Descendants("NewIdleDisconnectTime").First().Value);
            info.Name = document.Descendants("NewName").First().Value;
            info.TransportType = document.Descendants("NewTransportType").First().Value;
            info.UserName = document.Descendants("NewUserName").First().Value;

            info.PPPoEACName = document.Descendants("NewPPPoEACName").First().Value;
            info.PPPoEServiceName = document.Descendants("NewPPPoEServiceName").First().Value;
            info.RemoteIPAddress = IPAddress.TryParse(document.Descendants("NewRemoteIPAddress").First().Value, out IPAddress remote) ? remote : IPAddress.None;
            info.RouteProtocolRx = document.Descendants("NewRouteProtocolRx").First().Value;

            return info;
        }

        /// <summary>
        /// Method to get the connection type info
        /// </summary>
        /// <returns>the connection type info</returns>
        public async Task<ConnectionTypeInfo> GetConnectionTypeInfoAsync()
        {
            ConnectionTypeInfo info = new ConnectionTypeInfo();

            XDocument document = await this.InvokeAsync("GetConnectionTypeInfo", null);

            info.ConnectionType = (ConnectionType)Enum.Parse(typeof(ConnectionType), document.Descendants("NewConnectionType").First().Value);
            info.PossibleConnectionTypes = (PossibleConnectionTypes)Enum.Parse(typeof(PossibleConnectionTypes), document.Descendants("NewPossibleConnectionTypes").First().Value);

            return info;
        }

        /// <summary>
        /// Method to set the connection type
        /// </summary>
        /// <param name="connectionType">the new connection type</param>
        public async Task SetConnectionTypeAsync(string connectionType)
        {
            var parameter = new SoapRequestParameter("NewConnectionType", connectionType);
            XDocument document = await this.InvokeAsync("SetConnectionType", parameter);
        }

        /// <summary>
        /// Method to get the status info
        /// </summary>
        /// <returns></returns>
        public async Task<ConnectionStatusInfo> GetStatusInfoAsync()
        {
            ConnectionStatusInfo info = new ConnectionStatusInfo();

            XDocument document = await this.InvokeAsync("GetStatusInfo", null);

            info.ConnectionStatus = (ConnectionStatus)Enum.Parse(typeof(ConnectionStatus), document.Descendants("NewConnectionStatus").First().Value);
            info.LastConnectionError = (ConnectionError)Enum.Parse(typeof(ConnectionError), document.Descendants("NewLastConnectionError").First().Value);
            info.Uptime = UInt32.TryParse(document.Descendants("NewUptime").First().Value, out UInt32 value) ? value : 0;
            return info;
        }

        /// <summary>
        /// Method to get the username
        /// </summary>
        /// <returns>the user name</returns>
        public async Task<string> GetUserNameAsync()
        {
            XDocument document = await this.InvokeAsync("GetUserName", null);
            return document.Descendants("NewUserName").First().Value;
        }

        /// <summary>
        /// Method to set the user name
        /// </summary>
        /// <param name="userName">the new username</param>
        public async Task SetUserNameAsync(string userName)
        {
            var parameter = new SoapRequestParameter("NewUserName", userName);
            XDocument document = await this.InvokeAsync("SetUserName", parameter);
        }

        /// <summary>
        /// Method to set the password
        /// </summary>
        /// <param name="password">the new password</param>
        public async Task SetPasswordAsync(string password)
        {
            var parameter = new SoapRequestParameter("NewPassword", password);
            XDocument document = await this.InvokeAsync("SetPassword", parameter);
        }

        /// <summary>
        /// Method to get the nat rsip status
        /// </summary>
        /// <returns>the nat rsipstatus</returns>
        public async Task<NATRSIPStatus> GetNATRSIPStatusAsync()
        {
            NATRSIPStatus status = new NATRSIPStatus();
            XDocument document = await this.InvokeAsync("GetNATRSIPStatus", null);

            status.NATEnabled = document.Descendants("NewNATEnabled").First().Value == "1";
            status.RSIPAvailable = document.Descendants("NewRSIPAvailable").First().Value == "1";

            return status;
        }

        /// <summary>
        /// Method to force the termination of the ppp connection
        /// </summary>
        public async Task ForceTerminationAsync()
        {
            XDocument document = await this.InvokeAsync("ForceTermination", null);
        }
                               
        /// <summary>
        /// Method to request a ppp connection
        /// </summary>
        public async Task RequestConnectionAsync()
        {
            XDocument document = await this.InvokeAsync("RequestConnection", null);
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
        /// Method to get the dns servers
        /// </summary>
        /// <returns>the dns servers</returns>
        public async Task<List<IPAddress>> GetDNSServersAsync()
        {
            List<IPAddress> addresses = new List<IPAddress>();
            XDocument document = await this.InvokeAsync("X_GetDNSServers", null);
            var dnsservers = document.Descendants("NewDNSServers").First().Value.Split(new char[] { '\r', '\n', ',' }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();
            foreach (var dnsServer in dnsservers)
                addresses.Add(IPAddress.TryParse(dnsServer, out IPAddress server) ? server : IPAddress.None);

            return addresses;
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
        /// Method to get the link layer max bitrates
        /// </summary>
        /// <returns>the link layer max bitrates</returns>
        public async Task<LinkLayerMaxBitRates> GetLinkLayerMaxBitRatesAsync()
        {
            LinkLayerMaxBitRates bitRates = new LinkLayerMaxBitRates();
            XDocument document = await this.InvokeAsync("GetLinkLayerMaxBitRates", null);

            bitRates.DownstreamMaxBitRate = Convert.ToUInt32(document.Descendants("NewDownstreamMaxBitRate").First().Value);
            bitRates.UpstreamMaxBitRate = Convert.ToUInt32(document.Descendants("NewUpstreamMaxBitRate").First().Value);
            return bitRates;
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
        /// Method to set the connection trigger
        /// </summary>
        /// <param name="trigger">the new connection trigger</param>
        public async Task SetConnectionTriggerAsync(string trigger)
        {
            var parameter = new SoapRequestParameter("NewConnectionTrigger", trigger);
            await this.InvokeAsync("SetConnectionTrigger", parameter);
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
            entry.Enabled = document.Descendants("NewEnabled").First().Value == "1";
            entry.InternalHost = IPAddress.TryParse(document.Descendants("NewInternalClient").First().Value, out IPAddress internalHost) ? internalHost : IPAddress.None;
            entry.InternalPort = Convert.ToUInt16(document.Descendants("NewInternalPort").First().Value);
            entry.LeaseDuration = Convert.ToUInt32(document.Descendants("NewLeaseDuration").First().Value);
            entry.PortMappingProtocol = (PortMappingProtocol)Enum.Parse(typeof(PortMappingProtocol), document.Descendants("NewProtocol").First().Value);
            entry.RemoteHost = IPAddress.TryParse(document.Descendants("NewRemoteHost").First().Value, out IPAddress remoteHost) ? remoteHost : IPAddress.None;
            entry.ExternalPort = Convert.ToUInt16(document.Descendants("NewExternalPort").First().Value);

            return entry;
        }

        /// <summary>
        /// Method to get all port mappings
        /// </summary>
        /// <returns>the port mappings</returns>
        public async Task<List<PortMappingEntry>> GetPortMappingEntriesAsync()
        {
            List<PortMappingEntry> portMappings = new List<PortMappingEntry>();
            int count = await this.GetPortMappingNumberOfEntriesAsync();

            for (int i = 0; i < count; i++)
                portMappings.Add(await this.GetGenericPortMappingEntryAsync(i));

            return portMappings;
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
                new SoapRequestParameter("NewRemoteHost", remoteHost?.ToString()),
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

            entry.Enabled = document.Descendants("NewEnabled").First().Value == "1";
            entry.InternalHost = IPAddress.TryParse(document.Descendants("NewInternalClient").First().Value, out IPAddress internalHost) ? internalHost : IPAddress.None;
            entry.InternalPort = Convert.ToUInt16(document.Descendants("NewInternalPort").First().Value);
            entry.LeaseDuration = Convert.ToUInt32(document.Descendants("NewLeaseDuration").First().Value);
            entry.Description = document.Descendants("NewPortMappingDescription").First().Value;

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
                new SoapRequestParameter("NewRemoteHost", entry.RemoteHost?.ToString()),
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
                new SoapRequestParameter("NewRemoteHost", remoteHost?.ToString()),
                new SoapRequestParameter("NewExternalPort", externalPort),
                new SoapRequestParameter("NewProtocol", protocol.ToString())
            };

            await this.InvokeAsync("DeletePortMapping", parameters.ToArray());
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
        /// Method to get the auto disconnect prevention settings
        /// </summary>
        /// <returns>the auto disconnect prevention settings</returns>
        public async Task<UInt16> GetAutoDisconnectTimeSpanAsync()
        {
            XDocument document = await this.InvokeAsync("GetAutoDisconnectTime", null);
            return Convert.ToUInt16(document.Descendants("NewAutoDisconnectTime").First().Value);
        }

        /// <summary>
        /// Method to set the auto disconnect prevention settings
        /// </summary>
        /// <param name="settings">the prevention settings</param>
        /// <returns></returns>
        public async Task SetAutoDisconnectTimeSpanAsync(AutoDisconnectTimeSpan settings)
        {
            List<SoapRequestParameter> parameters = new List<SoapRequestParameter>()
            {
                new SoapRequestParameter("NewX_AVM-DE_DisconnectPreventionEnable", settings.PreventionEnable ? 1 : 0),
                new SoapRequestParameter("NewX_AVM-DE_DisconnectPreventionHour", settings.PreventionHour)
            };

            await this.InvokeAsync("X_AVM_DE_SetAutoDisconnectTimeSpan", parameters.ToArray());
        }
    }
}
