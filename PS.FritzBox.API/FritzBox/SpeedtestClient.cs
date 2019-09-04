using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    public class SpeedtestClient : FritzTR64Client
    {
        #region Construction / Destruction

        public SpeedtestClient(string url, int timeout) : base(url, timeout)
        {
        }

        public SpeedtestClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        public SpeedtestClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        public SpeedtestClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion  

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/x_speedtest";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:X_AVM-DE_Speedtest:1";

        /// <summary>
        /// Method to get speedtest info
        /// </summary>
        /// <returns>the speed test info</returns>
        public async Task<SpeedtestInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);

            return new SpeedtestInfo
            {
                EnableTcp = document.Descendants("NewEnableTcp").First().Value == "1",
                EnableUdp = document.Descendants("NewEnableUdp").First().Value == "1",
                EnableUdpBidirect = document.Descendants("NewEnableUdpBidirect").First().Value == "1",
                WANEnableTcp = document.Descendants("NewWANEnableTcp").First().Value == "1",
                WANEnableUdp = document.Descendants("NewWANEnableUdp").First().Value == "1",
                PortTcp =  Convert.ToUInt32(document.Descendants("NewPortTcp").First().Value),
                PortUdp = Convert.ToUInt32(document.Descendants("NewPortUdp").First().Value),
                PortUdpBidirect = Convert.ToUInt32(document.Descendants("NewPortUdpBidirect").First().Value)
            };
        }
    }
}
