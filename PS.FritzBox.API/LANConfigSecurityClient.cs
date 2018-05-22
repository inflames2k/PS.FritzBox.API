using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// client for lan config security service
    /// </summary>
    public class LANConfigSecurityClient : FritzTR64Client
    {
        #region Construction / Destruction

        public LANConfigSecurityClient(string url, int timeout) : base(url, timeout)
        {
        }

        public LANConfigSecurityClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        public LANConfigSecurityClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        public LANConfigSecurityClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/lanconfigsecurity";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:LANConfigSecurity:1";

        /// <summary>
        /// Method to get the lan config security info
        /// </summary>
        /// <returns>the lan config security info</returns>
        public PasswordInfo GetInfo() => this.GetInfoAsync().Result;

        /// <summary>
        /// Method to get the lan config security info
        /// </summary>
        /// <returns>the lan config security info</returns>
        public async Task<PasswordInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);
            PasswordInfo info = new PasswordInfo();

            info.AllowedChars = document.Descendants("NewAllowedCharsPassword").First().Value;
            info.MaxChars = Convert.ToUInt16(document.Descendants("NewMaxCharsPassword").First().Value);
            info.MinChars = Convert.ToUInt16(document.Descendants("NewMinCharsPassword").First().Value);

            return info;
        }
    }
}
