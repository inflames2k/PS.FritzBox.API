using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    public class ConnectionStatusInfo
    {
        /// <summary>
        /// Gets or sets the connection status
        /// </summary>
        public ConnectionStatus ConnectionStatus { get; internal set; }

        /// <summary>
        /// Gets the last connection error
        /// </summary>
        public ConnectionError LastConnectionError { get; internal set; }

        /// <summary>
        /// Gets the uptime
        /// </summary>
        public UInt32 Uptime { get; internal set; }
    }
}
