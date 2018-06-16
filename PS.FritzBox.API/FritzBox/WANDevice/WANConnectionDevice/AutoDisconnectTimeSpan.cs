using System;

namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    /// <summary>
    /// class representing auto disconnect timespan
    /// </summary>
    public class AutoDisconnectTimeSpan
    {
        /// <summary>
        /// Gets or sets if the disconnect prevention is enabled
        /// </summary>
        public bool PreventionEnable { get; set; }

        /// <summary>
        /// Gets or sets the disconnect time
        /// </summary>
        public UInt16 PreventionHour { get; set; }
    }
}