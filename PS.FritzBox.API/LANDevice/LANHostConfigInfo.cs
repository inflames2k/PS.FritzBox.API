using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// class representing lan host config info
    /// </summary>
    public class LANHostConfigInfo
    {
        /// <summary>
        /// Gets if the dhcp server is configurable
        /// </summary>
        public bool DHCPServerConfigurable { get; internal set; }

        /// <summary>
        /// Gets dhcp relay
        /// </summary>
        public bool DHCPRelay { get; internal set; }

        /// <summary>
        /// Gets the address range
        /// </summary>
        public LANHostConfigAddressRange AddressRange { get; internal set; } = new LANHostConfigAddressRange();

        /// <summary>
        /// Gets the reserved addresses
        /// </summary>
        public List<IPAddress> ReservedAddresses { get; internal set; } = new List<IPAddress>();

        /// <summary>
        /// Gets if the dhcp server is enabled
        /// </summary>
        public bool DHCPServerEnable { get; internal set; }

        /// <summary>
        /// Gets or sets the subnet mask
        /// </summary>
        public IPAddress SubnetMask { get; internal set; }

        /// <summary>
        /// Gets the dns servers
        /// </summary>
        public List<IPAddress> DNSServers { get; internal set; } = new List<IPAddress>();

        /// <summary>
        /// Gets the domain name
        /// </summary>
        public string DomainName { get; internal set; }

        /// <summary>
        /// Gets the ip routers
        /// </summary>
        public List<IPAddress> IPRouters { get; internal set; } = new List<IPAddress>();
    }
}
