namespace PS.FritzBox.API.WANDevice
{
    /// <summary>
    /// class representing wandsl interface statistics
    /// </summary>
    public class WANDSLInterfaceStatistics
    {
        /// <summary>
        /// Gets or sets the receive blocks
        /// </summary>
        public uint ReceiveBlocks { get; set; }

        /// <summary>
        /// Gets or sets the transmit blocks
        /// </summary>
        public uint TransmitBlocks { get; set; }

        /// <summary>
        /// Gets or sets the cell delin
        /// </summary>
        public uint CellDelin { get; set; }

        /// <summary>
        /// Gets or sets the link retain
        /// </summary>
        public uint LinkRetrain { get; set; }

        /// <summary>
        /// Gets or sets the init errors
        /// </summary>
        public uint InitErrors { get; set; }

        /// <summary>
        /// Gets or sets the InitTimeouts
        /// </summary>
        public uint InitTimeouts { get; set; }

        /// <summary>
        /// Gets or sets LossOfFraming
        /// </summary>
        public uint LossOfFraming { get; set; }

        /// <summary>
        /// Gets or sets ErroredSecs
        /// </summary>
        public uint ErroredSecs { get; set; }

        /// <summary>
        /// Gets or sets SeverelyErroredSecs
        /// </summary>
        public uint SeverelyErroredSecs { get; set; }

        /// <summary>
        /// Gets or sets FECErrors
        /// </summary>
        public uint FECErrors { get; set; }

        /// <summary>
        /// Gets or sets ATUCFECErrors
        /// </summary>
        public uint ATUCFECErrors { get; set; }

        /// <summary>
        /// Gets or sets HECErrors
        /// </summary>
        public uint HECErrors { get; set; }

        /// <summary>
        /// Gets or sets ATUCHECErrors
        /// </summary>
        public uint ATUCHECErrors { get; set; }

        /// <summary>
        /// Gets or sets CRCErrors
        /// </summary>
        public uint CRCErrors { get; set; }

        /// <summary>
        /// Gets or sets ATUCCRCErrors
        /// </summary>
        public uint ATUCCRCErrors { get; set; }
    }
}