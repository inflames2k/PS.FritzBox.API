namespace PS.FritzBox.API
{
    /// <summary>
    /// enum for dyn dns status
    /// </summary>
    public enum DDNSStatus
    {
        /// <summary>
        /// offline
        /// </summary>
        offline,
        /// <summary>
        /// cgcking
        /// </summary>
        checking,
        /// <summary>
        /// updating
        /// </summary>
        updating,
        /// <summary>
        /// updated
        /// </summary>
        updated,
        /// <summary>
        /// verifying
        /// </summary>
        verifying,
        /// <summary>
        /// complete
        /// </summary>
        complete,
        /// <summary>
        /// new address
        /// </summary>
        new_address,
        /// <summary>
        /// account disabled
        /// </summary>
        account_disabled,
        /// <summary>
        /// internet not connected
        /// </summary>
        internet_not_connected,
        /// <summary>
        /// undefined
        /// </summary>
        undefined
    }
}