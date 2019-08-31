namespace PS.FritzBox.API.WANDevice
{
    /// <summary>
    /// class representing dsl diagnose info
    /// </summary>
    public class WANDSLDiagnoseInfo
    {
        /// <summary>
        /// Gets or sets the dsl diagnose state
        /// </summary>
        public DSLDiagnoseState DiagnoseState { get; set; }

        /// <summary>
        /// Gets or sets the CableNokDistance
        /// </summary>
        public int CableNokDistance { get; set; }

        /// <summary>
        /// Gets or sets the time of the last diagnosis
        /// </summary>
        public uint LastDiagnoseTime { get; set; }

        /// <summary>
        /// Gets or sets SignalLossTime
        /// </summary>
        public uint SignalLossTime { get; set; }

        /// <summary>
        /// Gets or sets if dsl is active
        /// </summary>
        public bool DSLActive { get; set; }

        /// <summary>
        /// Gets or sets if dsl is synced
        /// </summary>
        public bool DSLSync { get; set; }
    }
}