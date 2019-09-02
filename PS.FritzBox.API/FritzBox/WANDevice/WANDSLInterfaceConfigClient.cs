using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API.WANDevice
{
    /// <summary>
    /// client for wan dsl interface config service
    /// </summary>
    public class WANDSLInterfaceConfigClient : FritzTR64Client
    {
        #region Construction / Destruction

        public WANDSLInterfaceConfigClient(string url, int timeout) : base(url, timeout)
        {
        }

        public WANDSLInterfaceConfigClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        public WANDSLInterfaceConfigClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        public WANDSLInterfaceConfigClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion
        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/wandslifconfig1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:WANDSLInterfaceConfig:1";

        /// <summary>
        /// Method to get WANDSL Interface info 
        /// </summary>
        /// <returns>the wan dsl interface info</returns>
        public async Task<WANDSLInterfaceInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);

            return new WANDSLInterfaceInfo()
            {
                Enabled = document.Descendants("NewEnable").First().Value == "1",
                ATURVendor = document.Descendants("NewATURVendor").First().Value,
                DataPath = document.Descendants("NewDataPath").First().Value,
                DownstreamAttenuation = Convert.ToUInt32(document.Descendants("NewDownstreamAttenuation").First().Value),
                UpstreamAttenuation = Convert.ToUInt32(document.Descendants("NewUpstreamAttenuation").First().Value),
                DownstreamCurrentRate = Convert.ToUInt32(document.Descendants("NewDownstreamCurrRate").First().Value),
                DownstreamMaxRate = Convert.ToUInt32(document.Descendants("NewDownstreamMaxRate").First().Value),
                DownstreamNoiseMargin = Convert.ToUInt32(document.Descendants("NewDownstreamNoiseMargin").First().Value),
                DownstreamPower = Convert.ToUInt32(document.Descendants("NewDownstreamPower").First().Value),
                ATURCountry = document.Descendants("NewATURCountry").First().Value,
                Status = document.Descendants("NewStatus").First().Value,
                UpstreamCurrentRate = Convert.ToUInt32(document.Descendants("NewUpstreamCurrRate").First().Value),
                UpstreamMaxRate = Convert.ToUInt32(document.Descendants("NewUpstreamMaxRate").First().Value),
                UpstreamNoiseMargin = Convert.ToUInt32(document.Descendants("NewUpstreamNoiseMargin").First().Value),
                UpstreamPower = Convert.ToUInt32(document.Descendants("NewUpstreamPower").First().Value),
            };
        }

        /// <summary>
        /// Method to get the interface statistics
        /// </summary>
        /// <returns>the interface statistics</returns>
        public async Task<WANDSLInterfaceStatistics> GetStatisticsTotalAsync()
        {
            XDocument document = await this.InvokeAsync("GetStatisticsTotal", null);

            return new WANDSLInterfaceStatistics()
            {
                ATUCCRCErrors = Convert.ToUInt32(document.Descendants("NewATUCCRCErrors").First().Value),
                ATUCFECErrors = Convert.ToUInt32(document.Descendants("NewATUCFECErrors").First().Value),
                ATUCHECErrors = Convert.ToUInt32(document.Descendants("NewATUCHECErrors").First().Value),
                CellDelin = Convert.ToUInt32(document.Descendants("NewCellDelin").First().Value),
                CRCErrors = Convert.ToUInt32(document.Descendants("NewCRCErrors").First().Value),
                ErroredSecs = Convert.ToUInt32(document.Descendants("NewErroredSecs").First().Value),
                FECErrors = Convert.ToUInt32(document.Descendants("NewFECErrors").First().Value),
                HECErrors = Convert.ToUInt32(document.Descendants("NewHECErrors").First().Value),
                InitErrors = Convert.ToUInt32(document.Descendants("NewInitErrors").First().Value),
                InitTimeouts = Convert.ToUInt32(document.Descendants("NewInitTimeouts").First().Value),
                LinkRetrain = Convert.ToUInt32(document.Descendants("NewLinkRetrain").First().Value),
                LossOfFraming = Convert.ToUInt32(document.Descendants("NewLossOfFraming").First().Value),
                ReceiveBlocks = Convert.ToUInt32(document.Descendants("NewReceiveBlocks").First().Value),
                SeverelyErroredSecs = Convert.ToUInt32(document.Descendants("NewSeverelyErroredSecs").First().Value),
                TransmitBlocks = Convert.ToUInt32(document.Descendants("NewTransmitBlocks").First().Value)
            };
        }

        /// <summary>
        /// Method to get dsl diagnose info
        /// </summary>
        /// <returns>the dsl diagnose info</returns>
        public async Task<WANDSLDiagnoseInfo> GetDSLDiagnoseInfoAsync()
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_GetDSLDiagnoseInfo", null);

            return new WANDSLDiagnoseInfo()
            {
                DiagnoseState = (DSLDiagnoseState)Enum.Parse(typeof(DSLDiagnoseState), document.Descendants("NewX_AVM-DE_DSLDiagnoseState").First().Value),
                CableNokDistance = Convert.ToInt32(document.Descendants("NewX_AVM-DE_CableNokDistance").First().Value),
                DSLActive = document.Descendants("NewX_AVM-DE_DSLActive").First().Value == "1",
                DSLSync = document.Descendants("NewX_AVM-DE_DSLSync").First().Value == "1",
                LastDiagnoseTime = Convert.ToUInt32(document.Descendants("NewX_AVM-DE_DSLLastDiagnoseTime").First().Value),
                SignalLossTime = Convert.ToUInt32(document.Descendants("NewX_AVM-DE_DSLSignalLossTime").First().Value)
            };
        }
    }
}