using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing password info
    /// </summary>
    public class DataValidationInfo
    {
        /// <summary>
        /// Gets or sets the minimum number of chars
        /// </summary>
        public UInt16 MinChars { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of chars
        /// </summary>
        public UInt16 MaxChars { get; set; }

        /// <summary>
        /// Gets or sets the allowed chars
        /// </summary>
        public string AllowedChars { get; set; }
    }
}
