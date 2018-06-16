using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// class for holding ip address range
    /// </summary>
    public class LANHostConfigAddressRange
    {
        /// <summary>
        /// Gets the min address
        /// </summary>
        public IPAddress MinAddress { get; internal set; }

        /// <summary>
        /// Gets the max address
        /// </summary>
        public IPAddress MaxAddress { get; internal set; }
    }
}
