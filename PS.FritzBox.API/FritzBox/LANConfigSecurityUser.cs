using System;
using System.Collections.Generic;
using System.Text;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing a user
    /// </summary>
    public class LANConfigSecurityUser
    {
        /// <summary>
        /// gets or sets the user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the security rights
        /// </summary>
        public List<LANConfigSecurityRight> Rights { get; set; } = new List<LANConfigSecurityRight>();
    }
}
