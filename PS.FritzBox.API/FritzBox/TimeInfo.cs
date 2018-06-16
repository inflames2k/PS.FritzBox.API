using System;
using System.Net;

namespace PS.FritzBox.API
{
    /// <summary>
    /// time info
    /// </summary>
    public class TimeInfo
    {
        /// <summary>
        /// Gets the ntp server 1
        /// </summary>
        public IPAddress NTPServer1 { get; internal set; }

        /// <summary>
        /// Gets the ntp server 2
        /// </summary>
        public IPAddress NTPServer2 { get; internal set; }

        /// <summary>
        /// gets the current local time
        /// </summary>
        public DateTime CurrentLocalTime { get; internal set; }

        /// <summary>
        /// gets the local timezone
        /// </summary>
        public string LocalTimeZone { get; internal set; }

        /// <summary>
        /// Gets the local time zone name
        /// </summary>
        public string LocalTimeZoneName { get; internal set; }

        /// <summary>
        /// Gets if daylight saving time is used
        /// </summary>
        public bool DaylightSavingsUsed { get; internal set; }

        /// <summary>
        /// Gets start time of daylight saving
        /// </summary>
        public DateTime DaylightSavingsStart { get; internal set; }

        /// <summary>
        /// Gets end time of daylight saving
        /// </summary>
        public DateTime DaylightSavingsEnd { get; internal set; }
    }
}