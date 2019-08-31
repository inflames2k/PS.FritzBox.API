namespace PS.FritzBox.API.WANDevice
{
    /// <summary>
    /// class representing WANDSLInterface info
    /// </summary>
    public class WANDSLInterfaceInfo
    {
        /// <summary>
        /// Gets if the interface is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the data path
        /// </summary>
        public string DataPath { get; set; }

        /// <summary>
        /// Gets or sets the upstream current rate
        /// </summary>
        public uint UpstreamCurrentRate { get; set; }

        /// <summary>
        /// Gets or sets the downstream current rate
        /// </summary>
        public uint DownstreamCurrentRate { get; set; }

        /// <summary>
        /// Gets or sets the upstream max rate
        /// </summary>
        public uint UpstreamMaxRate { get; set; }

        /// <summary>
        /// Gets or sets the upstream max rate
        /// </summary>
        public uint DownstreamMaxRate { get; set; }

        /// <summary>
        /// gets or sets the upstream noise margin
        /// </summary>
        public uint UpstreamNoiseMargin { get; set; }

        /// <summary>
        /// Gets or sets the downstream noise margin
        /// </summary>
        public uint DownstreamNoiseMargin { get; set; }

        /// <summary>
        /// Gets or sets the upstream attenuation
        /// </summary>
        public uint UpstreamAttenuation { get; set; }

        /// <summary>
        /// Gets or sets the downstream attenuation
        /// </summary>
        public uint DownstreamAttenuation { get; set; }

        /// <summary>
        /// Gets or sets the ATURVendor
        /// </summary>
        public string ATURVendor { get; set; }

        /// <summary>
        /// Gets or sets the ATURCountry
        /// </summary>
        public string ATURCountry { get; set; }

        /// <summary>
        /// Gets or sets the UpstreamPower
        /// </summary>
        public uint UpstreamPower { get; set; }

        /// <summary>
        /// Gets or sets the downstream power
        /// </summary>
        public uint DownstreamPower { get; set; }
    }
}