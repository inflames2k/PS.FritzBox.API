using System;
using System.Collections.Generic;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// class representing channel info
    /// </summary>
    public class ChannelInfo
    {
        /// <summary>
        /// Gets the channel
        /// </summary>
        public UInt16 Channel { get; internal set; }

        /// <summary>
        /// Gets the possible channels
        /// </summary>
        public IEnumerable<UInt16> PossibleChannels { get; internal set; }
    }
}