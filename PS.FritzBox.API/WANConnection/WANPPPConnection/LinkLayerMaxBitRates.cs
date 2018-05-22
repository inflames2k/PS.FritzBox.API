using System;

namespace PS.FritzBox.API
{
    public class LinkLayerMaxBitRates
    {
        /// <summary>
        /// Gets the upstrea max bitrate
        /// </summary>
        public UInt32 UpstreamMaxBitRate { get; internal set; }

        /// <summary>
        /// Gets the downstream max bitrate
        /// </summary>
        public UInt32 DownstreamMaxBitRate { get; internal set; }
    }
}
