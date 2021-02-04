using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API.WANDevice.WANConnectionDevice
{
    /// <summary>
    /// client for WAN DSL Link Config service
    /// </summary>
    public class WANDSLLinkConfigClient : FritzTR64Client
    {
        #region Construction / Destruction

        public WANDSLLinkConfigClient(string url, int timeout) : base(url, timeout)
        {
        }

        public WANDSLLinkConfigClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        public WANDSLLinkConfigClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        public WANDSLLinkConfigClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/wandsllinkconfig1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>                                 
        protected override string RequestNameSpace => "urn:dslforum-org:service:WANDSLLinkConfig:1";

        /// <summary>
        /// Method to get WANDSLLinkInfo
        /// </summary>
        /// <returns>the ethernet link status</returns>
        public async Task<WANDSLLinkInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);

            return new WANDSLLinkInfo
            {
                ATMEncapsulation = document.Descendants("NewATMEncapsulation").First().Value,
                ATMPeakCellRate = Convert.ToUInt32(document.Descendants("NewATMPeakCellRate").First().Value),
                ATMQoS = document.Descendants("NewATMQoS").First().Value,
                ATMSustainableCellRate = Convert.ToUInt32(document.Descendants("NewATMSustainableCellRate").First().Value),
                AutoConfig = document.Descendants("NewAutoConfig").First().Value == "1",
                DestinationAddress = document.Descendants("NewDestinationAddress").First().Value,
                Enabled = document.Descendants("NewEnable").First().Value == "1",
                LinkInfo = new DSLLinkInfo
                {
                    LinkStatus = (WANDSLLinkStatus)Enum.Parse(typeof(WANDSLLinkStatus), document.Descendants("NewLinkStatus").First().Value),
                    LinkType = (WANDSLLinkType)Enum.Parse(typeof(WANDSLLinkType), document.Descendants("NewLinkType").First().Value)
                }
            };
        }

        /// <summary>
        /// Method to change enabled state
        /// </summary>
        /// <param name="enable">new enabled state</param>
        /// <returns></returns>
        public async Task SetEnableAsync(bool enable)
        {
            XDocument document = await this.InvokeAsync("SetEnable", new SOAP.SoapRequestParameter("NewEnable", enable ? 1 : 0));
        }

        /// <summary>
        /// Method to set the DSL Link Type
        /// </summary>
        /// <param name="linkType">the dsl link type</param>
        /// <returns></returns>
        public async Task SetDSLLinkTypeAsync(WANDSLLinkType linkType)
        {
            XDocument document = await this.InvokeAsync("SetDSLLinkType", new SOAP.SoapRequestParameter("LinkType", linkType.ToString()));
        }

        /// <summary>
        /// Method to get the dsl link info
        /// </summary>
        /// <returns>the dsl link info</returns>
        public async Task<DSLLinkInfo> GetDSLLinkInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetDSLLinkInfo", null);

            return new DSLLinkInfo
            {
                LinkStatus = (WANDSLLinkStatus)Enum.Parse(typeof(WANDSLLinkStatus), document.Descendants("NewLinkStatus").First().Value),
                LinkType = (WANDSLLinkType)Enum.Parse(typeof(WANDSLLinkType), document.Descendants("NewLinkType").First().Value)
            };
        }

        /// <summary>
        /// Method to set new destination address
        /// </summary>
        /// <param name="destinationAddress">the destination address</param>
        /// <returns>the destination address</returns>
        public async Task SetDestinationAddressAsync(string destinationAddress)
        {
            XDocument document = await this.InvokeAsync("SetDestinationAddress", new SOAP.SoapRequestParameter("NewDestinationAddress´", destinationAddress));
        }

        /// <summary>
        /// Method to get the destination address
        /// </summary>
        /// <returns>the destination address</returns>
        public async Task<String> GetDestinationAddressAsync()
        {
            XDocument document = await this.InvokeAsync("GetDestinationAddress", null);
            return document.Descendants("NewDestinationAddress").First().Value;
        }

        /// <summary>
        /// Method to set ATM Encapsulation
        /// </summary>
        /// <param name="ATMEncapsulation">the atm encapsulation</param>
        /// <returns></returns>
        public async Task SetATMEncapsulationAsync(string ATMEncapsulation)
        {
            XDocument document = await this.InvokeAsync("SetATMEncapsulation", new SOAP.SoapRequestParameter("NewATMEncapsulation", ATMEncapsulation));
        }

        /// <summary>
        /// Method to get the atm encapsulation
        /// </summary>
        /// <returns>the atm encapsulation</returns>
        public async Task<String> GetATMEncapsulationAsync()
        {
            XDocument document = await this.InvokeAsync("GetATMEncapsulation", null);
            return document.Descendants("NewATMEncapsulation").First().Value;
        }

        /// <summary>
        /// Method to get auto config value
        /// </summary>
        /// <returns>the auto config value</returns>
        public async Task<bool> GetAutoConfigAsync()
        {
            XDocument document = await this.InvokeAsync("GetAutoConfig", null);
            return document.Descendants("NewAutoConfig").First().Value == "1";
        }

        /// <summary>
        /// Method to get the wan dsl link statistic
        /// </summary>
        /// <returns>the wan dsllink statistic</returns>
        public async Task<WANDSLLinkStatistic> GetStatisticsAsync()
        {
            XDocument document = await this.InvokeAsync("GetStatistics", null);

            return new WANDSLLinkStatistic
            {
                ATMTransmittedBlocks = Convert.ToUInt32(document.Descendants("NewATMTransmittedBlocks").First().Value),
                ATMReceivedBlocks = Convert.ToUInt32(document.Descendants("NewATMReceivedBlocks").First().Value),
                AAL5CRCErrors = Convert.ToUInt32(document.Descendants("NewAAL5CRCErrors").First().Value),
                ATMCRCErrors = Convert.ToUInt32(document.Descendants("NewATMCRCErrors").First().Value)
            };
        }
    }
}
