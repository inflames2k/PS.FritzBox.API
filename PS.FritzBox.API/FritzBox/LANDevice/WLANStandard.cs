using System;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// enumeration of wlan standards
    /// </summary>
    [Flags]
    public enum WLANStandard
    {
        /// <summary>
        /// WLAN standard a
        /// </summary>
        a,
        /// <summary>
        /// WLAN standard b
        /// </summary>
        b,
        /// <summary>
        /// WLAN standard g
        /// </summary>
        g,
        /// <summary>
        /// WLAN standard n
        /// </summary>
        n,
        /// <summary>
        /// WLAN standard ac
        /// </summary>
        ac
    }
}