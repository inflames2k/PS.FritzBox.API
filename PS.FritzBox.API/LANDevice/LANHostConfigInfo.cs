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
        /// Gets the min address
        /// </summary>
        public IPAddress MinAddress { get; internal set; }

        /// <summary>
        /// Gets the max address
        /// </summary>
        public IPAddress MaxAddress { get; internal set; }

        /// <summary>
        /// Gets the reserved addresses
        /// </summary>
        public IEnumerable<IPAddress> ReservedAddresses { get; internal set; }

        /// <summary>
        /// Gets if the dhcp server is enabled
        /// </summary>
        public bool DHCPServerEnable { get; internal set; }

        /// <summary>
        /// Gets if the interface is enabled
        /// </summary>
        public bool Enable { get; internal set; }

        /// <summary>
        /// Gets the interfaceip address
        /// </summary>
        public IPAddress IPAddress { get; internal set; }

        /// <summary>
        /// Gets the ip addressing type
        /// </summary>
        public string IPAddressingType { get; internal set; }

        /// <summary>
        /// Gets or sets the subnet mask
        /// </summary>
        public IPAddress SubnetMask { get; internal set; }

        /// <summary>
        /// Gets the dns servers
        /// </summary>
        public string DNSServers { get; internal set; }

        /// <summary>
        /// Gets the domain name
        /// </summary>
        public string DomainName { get; internal set; }

        /// <summary>
        /// Gets the ip routers
        /// </summary>
        public string IPRouters { get; internal set; }

        /// <summary>
        /// Gets the number of ip interfaces
        /// </summary>
        public UInt16 IPInterfaceNumberOfEntries { get; internal set; }
    }
}
