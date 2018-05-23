using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// client for LANHostConfigManagement service
    /// </summary>
    public class LANHostConfigManagementClient : FritzTR64Client
    {
        #region Construction / Destruction

        public LANHostConfigManagementClient(string url, int timeout) : base(url, timeout)
        {
        }

        public LANHostConfigManagementClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        public LANHostConfigManagementClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        public LANHostConfigManagementClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => throw new NotImplementedException();

        /// <summary>
        /// gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => throw new NotImplementedException();

        /// <summary>
        /// Method to get the lan host config info
        /// </summary>
        /// <returns>the lan host config info</returns>
        public async Task<LANHostConfigInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);
            LANHostConfigInfo info = new LANHostConfigInfo();

            throw new Exception(document.ToString());

            return info;
        }
    }
}
