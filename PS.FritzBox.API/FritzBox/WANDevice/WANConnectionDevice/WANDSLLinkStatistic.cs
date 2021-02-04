namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    /// <summary>
    /// class representing wan dsl link statistic
    /// </summary>
    public class WANDSLLinkStatistic
    {
        /// <summary>
        /// the atm transmitted blocks
        /// </summary>
        public uint ATMTransmittedBlocks { get; set; }

        /// <summary>
        /// the atm received blocks
        /// </summary>
        public uint ATMReceivedBlocks { get; set; }

        /// <summary>
        /// the AAL5CRCErrors
        /// </summary>
        public uint AAL5CRCErrors { get; set; }

        /// <summary>
        /// the ATMCRCErrors
        /// </summary>
        public uint ATMCRCErrors { get; set; }
    }
}