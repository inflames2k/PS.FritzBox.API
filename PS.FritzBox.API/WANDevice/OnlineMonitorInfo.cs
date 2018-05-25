using System;
using System.Collections.Generic;

namespace PS.FritzBox.API.WANDevice
{
    public class OnlineMonitorInfo
    {
        /// <summary>
        /// Gets the sync group mode
        /// </summary>
        public string SyncGroupMode { get; internal set; }
        /// <summary>
        /// Gets the sync group name
        /// </summary>
        public string SyncGroupName { get; internal set; }
        /// <summary>
        /// Gets the total number of sync groups
        /// </summary>
        public UInt32 TotalNumberSyncGroups { get; internal set; }
        /// <summary>
        /// Gets or sets the current downstream in bits per second
        /// </summary>
        public List<UInt32> DownStream { get; internal set; }
        /// <summary>
        /// Gets the current media downstream in bits per seconds
        /// </summary>
        public List<UInt32> DownStream_Media { get; internal set; }
        /// <summary>
        /// Gets the current upstream in bits per second
        /// </summary>
        public List<UInt32> UpStream { get; internal set; }
        /// <summary>
        /// Gets the max downstream in bits per second
        /// </summary>
        public UInt32 MaxDownStream { get; internal set; }
        /// <summary>
        /// Gets the max upstream in bits per second
        /// </summary>
        public UInt32 MaxUpStream { get; internal set; }        
        /// <summary>
        /// gets the last measures of upstream on default prio
        /// </summary>
        public List<UInt32> UpstreamDefaultPrio { get; internal set; }
        /// <summary>
        /// gets the last measures of upstream on high prio
        /// </summary>
        public List<UInt32> UpstreamHighPrio { get; internal set; }
        /// <summary>
        /// Gets the last measures of upstream on low prio
        /// </summary>
        public List<UInt32> UpstreamLowPrio { get; internal set; }
        /// <summary>
        /// Gets the last measures of downstream prio
        /// </summary>
        public List<UInt32> UpstreamRealtimePrio { get; internal set; }
    }
}
