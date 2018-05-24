namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    /// <summary>
    /// enum for connection error
    /// </summary>
    public enum ConnectionError
    {
        /// <summary>
        /// No error
        /// </summary>
        ERROR_NONE,
        /// <summary>
        /// isp timed out
        /// </summary>
        ERROR_ISP_TIME_OUT,
        /// <summary>
        /// command aborted
        /// </summary>
        ERROR_COMMAND_ABORTED,
        /// <summary>
        /// not enabled for internet
        /// </summary>
        ERROR_NOT_ENABLED_FOR_INTERNET,
        /// <summary>
        /// bad phone number
        /// </summary>
        ERROR_BAD_PHONE_NUMBER,
        /// <summary>
        /// user disconnect
        /// </summary>
        ERROR_USER_DISCONNECT,
        /// <summary>
        /// isp disconnect
        /// </summary>
        ERROR_ISP_DISCONNECT,
        /// <summary>
        /// idle disconnect
        /// </summary>
        ERROR_IDLE_DISCONNECT,
        /// <summary>
        /// forced disconnect
        /// </summary>
        ERROR_FORCED_DISCONNECT,
        /// <summary>
        /// server out of resources
        /// </summary>
        ERROR_SERVER_OUT_OF_RESOURCES,
        /// <summary>
        /// restricted logon hours
        /// </summary>
        ERROR_RESTRICTED_LOGON_HOURS,
        /// <summary>
        /// account disabled
        /// </summary>
        ERROR_ACCOUNT_DISABLED,
        /// <summary>
        /// account expired
        /// </summary>
        ERROR_ACCOUNT_EXPIRED,
        /// <summary>
        /// password expired
        /// </summary>
        ERROR_PASSWORD_EXPIRED,
        /// <summary>
        /// authentication failure
        /// </summary>
        ERROR_AUTHENTICATION_FAILURE,
        /// <summary>
        /// error no dial tone
        /// </summary>
        ERROR_NO_DIALTONE,
        /// <summary>
        /// no carrier
        /// </summary>
        ERROR_NO_CARRIER,
        /// <summary>
        /// no answer
        /// </summary>
        ERROR_NO_ANSWER,
        /// <summary>
        /// line busy
        /// </summary>
        ERROR_LINE_BUSY,
        /// <summary>
        /// unsuported bits per seconds
        /// </summary>
        ERROR_UNSUPPORTED_BITSPERSECOND,
        /// <summary>
        /// too many line errors
        /// </summary>
        ERROR_TOO_MANY_LINE_ERRORS,
        /// <summary>
        /// ip configuration
        /// </summary>
        ERROR_IP_CONFIGURATION,
        /// <summary>
        /// unknown
        /// </summary>
        ERROR_UNKNOWN
    }
}
