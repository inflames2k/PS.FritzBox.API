using System;
using System.Net;

namespace PS.FritzBox.API
{
    /// <summary>
    /// layer 3 forwarding entry
    /// </summary>
    public class Layer3ForwardingEntry
    {
        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the destination ip address
        /// </summary>
        public IPAddress DestinationIPAddress { get; set; }

        /// <summary>
        /// Gets or sets the destination subnet mask
        /// </summary>
        public IPAddress DestinationSubnetMask { get; set; }

        /// <summary>
        /// Gets or sets the source ip address
        /// </summary>
        public IPAddress SourceIPAddress { get; set; }

        /// <summary>
        /// Gets or sets the source subnet mask
        /// </summary>
        public IPAddress SourceSubnetMask { get; set; }

        /// <summary>
        /// Gets or sets the ip address of the gateway
        /// </summary>
        public IPAddress GatewayIPAddress { get; set; }

        /// <summary>
        /// Gets or sets the interface (must be default connection service)
        /// </summary>
        public string Interface { get; set; }

        /// <summary>
        /// gets or sets the forwarding metric
        /// </summary>
        public Int32 ForwardingMetric { get; set; }

        /// <summary>
        /// Gets the status
        /// </summary>
        public string Status { get; internal set; }

        /// <summary>
        /// Gets if the forwarding is enabled
        /// </summary>
        public bool Enabled { get; internal set; }
    }
}