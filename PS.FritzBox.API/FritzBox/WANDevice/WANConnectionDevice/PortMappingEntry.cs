using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    /// <summary>
    /// class holding data for generic port mapping entry
    /// </summary>
    public class PortMappingEntry
    {
        /// <summary>
        /// Gets or sets the remote host
        /// </summary>
        public IPAddress RemoteHost { get; set; }

        /// <summary>
        /// Gets or sets the external port
        /// </summary>
        public UInt16 ExternalPort { get; set; }

        /// <summary>
        /// Gets or sets the port mapping protocol
        /// </summary>
        public PortMappingProtocol PortMappingProtocol { get; set; }

        /// <summary>
        /// Gets or sets the internal port
        /// </summary>
        public UInt16 InternalPort { get; set; }

        /// <summary>
        /// Gets or sets the internal host
        /// </summary>
        public IPAddress InternalHost { get; set; }

        /// <summary>
        /// Gets or sets if the port mapping is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the description for the port mapping
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the lease duration
        /// </summary>
        public UInt32 LeaseDuration { get; set; }
    }
}
