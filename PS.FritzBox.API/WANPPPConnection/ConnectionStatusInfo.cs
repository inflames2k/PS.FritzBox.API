using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API
{
    public class ConnectionStatusInfo
    {
        /// <summary>
        /// Gets the connection status
        /// </summary>
        public string ConnectionStatus { get; internal set; }

        /// <summary>
        /// Gets the last connection error
        /// </summary>
        public string LastConnectionError { get; internal set; }

        /// <summary>
        /// Gets the uptime
        /// </summary>
        public UInt32 Uptime { get; internal set; }
    }
}
