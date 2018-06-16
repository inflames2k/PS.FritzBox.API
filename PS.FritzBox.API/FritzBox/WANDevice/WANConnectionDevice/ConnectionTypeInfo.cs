using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    public class ConnectionTypeInfo
    {
        /// <summary>
        /// Gets the connection type
        /// </summary>
        public ConnectionType ConnectionType { get; internal set; }

        /// <summary>
        /// Gets the possible connection types
        /// </summary>
        public PossibleConnectionTypes PossibleConnectionTypes { get; internal set; }
    }
}
