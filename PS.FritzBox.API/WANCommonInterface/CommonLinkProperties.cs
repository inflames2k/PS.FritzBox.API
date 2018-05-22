using System;

namespace PS.FritzBox.API
{
    public class CommonLinkProperties
    {
        /// <summary>
        /// Gets or sets the wan access type
        /// </summary>
        public string WANAccessType { get; internal set; }

        /// <summary>
        /// Gets the max upstream bitrate on layer1
        /// </summary>
        public UInt32 Layer1UpstreamMaxBitRate { get; internal set; }

        /// <summary>
        /// Gets the max downstream bitrate on layer1
        /// </summary>
        public UInt32 Layer1DownstreamMaxBitRate { get; internal set; }

        /// <summary>
        /// Gets the physical link status
        /// </summary>
        public string PhysicalLinkStatus { get; internal set; }
    }
}
