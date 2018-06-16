namespace PS.FritzBox.API
{
    /// <summary>
    /// update info
    /// </summary>
    public class UserInterfaceInfo
    {
        /// <summary>
        /// Gets if a password is required
        /// </summary>
        public bool PasswordRequired { get; internal set; }
        /// <summary>
        /// Gets if the password is user selectable
        /// </summary>
        public bool PasswordUserSelectable { get; internal set; }
        /// <summary>
        /// Gets the warranty date
        /// </summary>
        public string WarrantyDate { get; internal set; }
        /// <summary>
        /// Gets the version
        /// </summary>
        public string Version { get; internal set; }
        /// <summary>
        /// Gets the download url
        /// </summary>
        public string DownloadUrl { get; internal set; }
        /// <summary>
        /// Gets the info url
        /// </summary>
        public string InfoUrl { get; internal set; }

        /// <summary>
        /// Gets the update state
        /// </summary>
        public UpdateInfo UpdateState { get; internal set; }
        /// <summary>
        /// Gets the labor version
        /// </summary>
        public string LaborVersion { get; internal set; }
    }
}