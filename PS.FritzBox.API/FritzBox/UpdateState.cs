namespace PS.FritzBox.API
{
    /// <summary>
    /// enum for update state
    /// </summary>
    public enum UpdateState
    {
        /// <summary>
        /// Update started
        /// </summary>
        Started,
        /// <summary>
        /// Update stopped
        /// </summary>
        Stopped,
        /// <summary>
        /// Update error
        /// </summary>
        Error,
        /// <summary>
        /// no update 
        /// </summary>
        NoUpdate,
        /// <summary>
        /// update available
        /// </summary>
        UpdateAvailable,
        /// <summary>
        /// unknown
        /// </summary>
        Unknown,
    }

    public enum UpdateSuccessState
    {
        /// <summary>
        /// unknown
        /// </summary>
        unknown,
        /// <summary>
        /// failed
        /// </summary>
        failed,
        /// <summary>
        /// succeeeded
        /// </summary>
        succeeded
    }
}