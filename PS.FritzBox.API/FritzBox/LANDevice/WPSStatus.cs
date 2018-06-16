using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// enumeration of wps status
    /// </summary>
    public enum WPSStatus
    {
        /// <summary>
        /// off
        /// </summary>
        off,
        /// <summary>
        /// inactive
        /// </summary>
        inactive,
        /// <summary>
        /// active
        /// </summary>
        active,
        /// <summary>
        /// success
        /// </summary>
        success,
        /// <summary>
        /// err_common
        /// </summary>
        err_common,
        /// <summary>
        /// err_timeout
        /// </summary>
        err_timeout,
        /// <summary>
        /// err reconfig
        /// </summary>
        err_reconfig,
        /// <summary>
        /// err internal
        /// </summary>
        err_internal,
        /// <summary>
        /// err abort
        /// </summary>
        err_abort
    }
}
