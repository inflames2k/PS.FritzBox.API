using PS.FritzBox.API.Base;
using PS.FritzBox.API.SOAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// client for speedtest service
    /// </summary>
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
                Config = new SpeedtestConfig
                {
                    EnableTcp = document.Descendants("NewEnableTcp").First().Value == "1",
                    EnableUdp = document.Descendants("NewEnableUdp").First().Value == "1",
                    EnableUdpBidirect = document.Descendants("NewEnableUdpBidirect").First().Value == "1",
                    WANEnableTcp = document.Descendants("NewWANEnableTcp").First().Value == "1",
                    WANEnableUdp = document.Descendants("NewWANEnableUdp").First().Value == "1"
                },
                PortTcp =  Convert.ToUInt32(document.Descendants("NewPortTcp").First().Value),
                PortUdp = Convert.ToUInt32(document.Descendants("NewPortUdp").First().Value),
                PortUdpBidirect = Convert.ToUInt32(document.Descendants("NewPortUdpBidirect").First().Value)
            };
        }

        /// <summary>
        /// Method to set the speed test config
        /// </summary>
        /// <param name="config">the speed test config</param>
        /// <returns></returns>
        public async Task SetConfigAsync(SpeedtestConfig config)
        {
            List<SoapRequestParameter> parameters = new List<SoapRequestParameter>()
            {
                new SoapRequestParameter("NewEnableTcp", config.EnableTcp),
                new SoapRequestParameter("NewEnableUdp", config.EnableUdp),
                new SoapRequestParameter("NewEnableUdpBidirect", config.EnableUdpBidirect),
                new SoapRequestParameter("NewWANEnableTcp", config.WANEnableTcp),
                new SoapRequestParameter("NewWANEnableUdp", config.WANEnableUdp)
            };            

            XDocument document = await this.InvokeAsync("SetConfig", parameters.ToArray());
        }
    }
}
