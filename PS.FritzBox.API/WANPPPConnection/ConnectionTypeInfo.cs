using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API
{
    public class ConnectionTypeInfo
    {
        /// <summary>
        /// Gets the connection type
        /// </summary>
        public string ConnectionType { get; internal set; }

        /// <summary>
        /// Gets the possible connection types
        /// </summary>
        public string PossibleConnectionTypes { get; internal set; }
    }
}
