namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing settings for client connection settings
    /// </summary>
    public class ConnectionSettings
    {
        /// <summary>
        /// base url
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the request timeout
        /// </summary>
        public int Timeout { get; set; } = 10;

        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }
    }
}
