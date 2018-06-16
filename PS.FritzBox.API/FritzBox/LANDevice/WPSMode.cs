using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// enumeration for wps modes
    /// </summary>
    public enum WPSMode
    {
        /// <summary>
        /// pin client
        /// </summary>
        pin_client,
        /// <summary>
        /// pin ap
        /// </summary>
        pin_ap,
        /// <summary>
        /// pbc
        /// </summary>
        pbc,
        /// <summary>
        /// stop
        /// </summary>
        stop
    }
}
