using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// client for time service
    /// </summary>
    public class TimeServiceClient : FritzTR64Client
    {
        #region Construction / Destruction
        
        public TimeServiceClient(string url, int timeout) : base(url, timeout)
        {
        }
        
        public TimeServiceClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }
        
        public TimeServiceClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }
        
        public TimeServiceClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion  

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/time";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:Time:1";

        /// <summary>
        /// Method to get the time info
        /// </summary>
        /// <returns>time info</returns>
        public async Task<TimeInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);
            TimeInfo info = new TimeInfo();
            info.NTPServer1 = IPAddress.TryParse(document.Descendants("NewNTPServer1").First().Value, out IPAddress ntp1) ? ntp1 : IPAddress.None;
            info.NTPServer2 = IPAddress.TryParse(document.Descendants("NewNTPServer2").First().Value, out IPAddress ntp2) ? ntp2 : IPAddress.None;
            info.CurrentLocalTime = Convert.ToDateTime(document.Descendants("NewCurrentLocalTime").First().Value);
            info.LocalTimeZone = document.Descendants("NewLocalTimeZone").First().Value;
            info.LocalTimeZoneName = document.Descendants("NewLocalTimeZoneName").First().Value;
            info.DaylightSavingsUsed = document.Descendants("NewDaylightSavingsUsed").First().Value == "1";
            info.DaylightSavingsStart = Convert.ToDateTime(document.Descendants("NewDaylightSavingsStart").First().Value);
            info.DaylightSavingsEnd = Convert.ToDateTime(document.Descendants("NewDaylightSavingsEnd").First().Value);
            
            return info;
        }

        /// <summary>
        /// Method to set the ntp servers
        /// </summary>
        /// <param name="ntpServer1">ntp server 1</param>
        /// <param name="ntpServer2">ntp server 2</param>
        /// <returns></returns>
        public async Task SetNTPServersAsync(IPAddress ntpServer1, IPAddress ntpServer2)
        {
            await this.InvokeAsync("SetNTPServers", new SOAP.SoapRequestParameter("NewNTPServer1", ntpServer1.ToString()), new SOAP.SoapRequestParameter("NewNTPServer2", ntpServer2.ToString()));
        }
    }
}
