using System;
using System.Net;

namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    public class WANPPPConnectionInfo
    {
        /// <summary>
        /// Gets the connection type informations
        /// </summary>
        public ConnectionTypeInfo ConnectionType { get; internal set; } = new ConnectionTypeInfo();
        /// <summary>
        /// Gets the connection status
        /// </summary>
        public ConnectionStatusInfo ConnectionStatus { get; internal set; } = new ConnectionStatusInfo();
        /// <summary>
        /// Gets the nat rsip status
        /// </summary>
        public NATRSIPStatus NATRSIPStatus { get; internal set; } = new NATRSIPStatus();
        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets the link layer max bit rates
        /// </summary>
        public LinkLayerMaxBitRates LinkLayerMaxBitRates { get; internal set; } = new LinkLayerMaxBitRates();
        /// <summary>
        /// Gets the idle disconnect time
        /// </summary>
        public UInt32 IdleDisconnectTime { get; internal set; }        
        /// <summary>
        /// Gets the user name
        /// </summary>
        public string UserName { get; internal set; }
        /// <summary>
        /// Gets the transport type
        /// </summary>
        public string TransportType { get; internal set; }
        /// <summary>
        /// Gets the route protocol
        /// </summary>
        public string RouteProtocolRx { get; internal set; }
        /// <summary>
        /// Gets the PPPoE service name
        /// </summary>
        public string PPPoEServiceName { get; internal set; }
        /// <summary>
        /// Gets the remote ip address
        /// </summary>
        public IPAddress RemoteIPAddress { get; internal set; }
        /// <summary>
        /// Gets the external ip address
        /// </summary>
        public IPAddress ExternalIPAddress { get; internal set; }
        /// <summary>
        /// Gets the PPPoEACName
        /// </summary>
        public string PPPoEACName { get; internal set; }
    }
}
