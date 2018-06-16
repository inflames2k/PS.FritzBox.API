namespace PS.FritzBox.API
{
    /// <summary>
    /// update informations
    /// </summary>
    public class UpdateInfo
    {
        /// <summary>
        /// Gets if an upgrade is available
        /// </summary>
        public bool UpgradeAvailable { get; internal set; }
        /// <summary>
        /// Gets the update state
        /// </summary>
        public UpdateState UpdateState { get; internal set; }
    }
}