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
        /// Method to get the password info
        /// </summary>
        /// <returns>the password info</returns>
        public async Task<DataValidationInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);
            DataValidationInfo info = new DataValidationInfo();

            info.AllowedChars = document.Descendants("NewAllowedCharsPassword").First().Value;
            info.MinChars = Convert.ToUInt16(document.Descendants("NewMinCharsPassword").First().Value);
            info.MaxChars = Convert.ToUInt16(document.Descendants("NewMaxCharsPassword").First().Value);

            return info;
        }

        /// <summary>
        /// Method to get if anonymous login is enabled
        /// </summary>
        /// <returns>true if anonymous login is enabled</returns>
        public async Task<bool> GetAnonymousLoginAsync()
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_GetAnonymousLogin", null);
            return document.Descendants("NewX_AVM-DE_AnonymousLoginEnabled").First().Value == "1";
        }

        /// <summary>
        /// Method to get the current user
        /// </summary>
        /// <returns>the current user</returns>
        public async Task<LANConfigSecurityUser> GetCurrentUserAsync()
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_GetCurrentUser", null);
            LANConfigSecurityUser user = new LANConfigSecurityUser();
            user.Username = document.Descendants("NewX_AVM-DE_CurrentUsername").First().Value;

            string rightsString = document.Descendants("NewX_AVM-DE_CurrentUserRights").First().Value;
            XDocument rightsDocument = XDocument.Parse(rightsString);

            IEnumerable<XElement> paths = rightsDocument.Descendants("path");
            IEnumerable<XElement> rights = rightsDocument.Descendants("access");
            
            for(int i = 0; i < paths.Count(); i++)
            {
                LANConfigSecurityRight right = new LANConfigSecurityRight();
                right.Path = paths.Skip(i).Take(1).First().Value;
                right.Access = rights.Skip(i).Take(1).First().Value;
                user.Rights.Add(right);
            }

            return user;
        }

        

        /// <summary>
        /// Method to set the password for the current user
        /// </summary>
        /// <param name="password">the password</param>
        public async Task SetConfigPasswordAsync(string password)
        {
            XDocument document = await this.InvokeAsync("SetConfigPassword", new SOAP.SoapRequestParameter("NewPassword", password));
        }
    }
}
