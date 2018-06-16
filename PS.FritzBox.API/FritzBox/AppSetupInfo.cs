namespace PS.FritzBox.API
{
    /// <summary>
    /// app setup info
    /// </summary>
    public class AppSetupInfo
    {
        /// <summary>
        /// Gets the app id validation info
        /// </summary>
        public DataValidationInfo AppIDValidationInfo { get; internal set; } = new DataValidationInfo();

        /// <summary>
        /// Gets the app display name validation info
        /// </summary>
        public DataValidationInfo AppDisplayNameValidationInfo { get; internal set; } = new DataValidationInfo();

        /// <summary>
        /// Gets the app user name validation info
        /// </summary>
        public DataValidationInfo AppUsernameValidationInfo { get; internal set; } = new DataValidationInfo();

        /// <summary>
        /// Gets the app password validation info
        /// </summary>
        public DataValidationInfo AppPasswordValidationInfo { get; internal set; } = new DataValidationInfo();

        /// <summary>
        /// Gets the ip sec identifier validation info
        /// </summary>
        public DataValidationInfo IPSecIdentifierValidationInfo { get; set; } = new DataValidationInfo();

        /// <summary>
        /// Gets the validation info for the ip sec pre shared key
        /// </summary>
        public DataValidationInfo IPSecPresharedKeyValidationInfo { get; internal set; } = new DataValidationInfo();

        /// <summary>
        /// Gets the validation info for the ip sec auth username
        /// </summary>
        public DataValidationInfo IPSecXauthUsernameValidationInfo { get; internal set; } = new DataValidationInfo();

        /// <summary>
        /// Gets the validation info for the ip sec auth password
        /// </summary>
        public DataValidationInfo IPSecXauthPasswordValidationInfo { get; internal set; } = new DataValidationInfo();
        
        /// <summary>
        /// gets the Allowed characters for CryptAlgos
        /// </summary>
        public string AllowedCharsCryptAlgos { get; internal set; }

        /// <summary>
        /// gets the Allowed characters for AppAVMAddress
        /// </summary>
        public string AllowedCharsAppAVMAddress { get; internal set; }
    }
}