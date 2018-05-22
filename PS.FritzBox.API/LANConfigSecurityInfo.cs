using System;

namespace PS.FritzBox.API
{
    /// <summary>
    /// lan config security info
    /// </summary>
    public class PasswordInfo
    {
        /// <summary>
        /// Gets or sets the maximum number of password chars
        /// </summary>
        public UInt16 MaxChars { get; set; }

        /// <summary>
        /// Gets or sets the minimum number of password chars
        /// </summary>
        public UInt16 MinChars { get; set; }

        /// <summary>
        /// Gets or sets the allowed chars for the password
        /// </summary>
        public string AllowedChars { get; set; }
    }
}