namespace PS.FritzBox.API
{
    /// <summary>
    /// cgi info 
    /// </summary>
    public class CGIInfo
    {
        /// <summary>
        /// cgi update path
        /// </summary>
        public string CGIPath { get; internal set; }
        /// <summary>
        /// session id valid up to 60 seconds
        /// </summary>
        public string SessionID { get; internal set; }
    }
}