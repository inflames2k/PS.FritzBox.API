using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API.FritzBox.LANDevice
{
    public class WLanStatistics
    {
        /// <summary>
        /// Gets or sets the number of total packets sent 
        /// </summary>
        public UInt64 TotalPacketsSent { get; set; }

        /// <summary>
        /// Gets or sets the total packets received
        /// </summary>
        public UInt64 TotalPacketsReceived { get; set; }
    }
}
