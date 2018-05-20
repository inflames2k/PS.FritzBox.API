using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing wan ip connection info
    /// </summary>
    public class WANIPConnectionInfo
    {
        /// <summary>
        /// Gets or sets if the connection is enabled
        /// </summary>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// Gets or sets the connection type
        /// </summary>
        public ConnectionTypeInfo ConnectionType { get; internal set; } = new ConnectionTypeInfo();

        /// <summary>
        /// Gets or sets the connection status
        /// </summary>
        public ConnectionStatusInfo ConnectionStatus { get; set; } = new ConnectionStatusInfo();

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the nat rsip status
        /// </summary>
        public NATRSIPStatus NATRSIPStatus { get; internal set; } = new NATRSIPStatus();

        /// <summary>
        /// Gets the external ip address
        /// </summary>
        public string ExternalIPAddress { get; internal set; }

        /// <summary>
        /// Gets or sets the dns servers
        /// </summary>
        public string DNSServers { get; internal set; }

        /// <summary>
        /// Gets or sets the mac address
        /// </summary>
        public string MACAddress { get; internal set; }

        /// <summary>
        /// Gets or sets the connection trigger
        /// </summary>
        public string ConnectionTrigger { get; internal set; }

        /// <summary>
        /// Gets or sets the route protocol
        /// </summary>
        public string RouteProtocolRx { get; internal set; }

        /// <summary>
        /// Gets or sets if dns is enabled
        /// </summary>
        public bool DNSEnabled { get; internal set; }

        /// <summary>
        /// Gets or sets if dns override is allowed
        /// </summary>
        public bool DNSOverrideAllowed { get; internal set; }
    }
}
