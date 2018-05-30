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
    /// client for layer3 forwarding service
    /// </summary>
    public class Layer3ForwardingClient : FritzTR64Client
    {
        #region Construction / Destruction

        public Layer3ForwardingClient(string url, int timeout) : base(url, timeout)
        {
        }

        public Layer3ForwardingClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        public Layer3ForwardingClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        public Layer3ForwardingClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion  

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/tr064/upnp/control/layer3forwarding";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:Layer3Forwarding:1";

        /// <summary>
        /// Method to set the default connection service
        /// </summary>
        /// <param name="defaultConnectionService">the default connection service</param>
        /// <returns></returns>
        public async Task SetDefaultConnectionServiceAsync(string defaultConnectionService)
        {
            await this.InvokeAsync("SetDefaultConnectionService", new SOAP.SoapRequestParameter("NewDefaultConnectionService", defaultConnectionService));
        }

        /// <summary>
        /// Method to get the default connection service
        /// </summary>
        /// <returns>the default connecton service</returns>
        public async Task<string> GetDefaultConnectionServiceAsync()
        {
            XDocument document = await this.InvokeAsync("GetDefaultConnectionService", null);
            return document.Descendants("NewDefaultConnectionService").First().Value;
        }

        /// <summary>
        /// Method to get the number of forward entries
        /// </summary>
        /// <returns>the number of forward entries</returns>
        public async Task<UInt16> GetForwardNumberOfEntriesAsync()
        {
            XDocument document = await this.InvokeAsync("GetForwardNumberOfEntries", null);
            return Convert.ToUInt16(document.Descendants("NewForwardNumberOfEntries").First().Value);
        }

        /// <summary>
        /// Method to add a forwarding entry
        /// </summary>
        /// <param name="entry">the entry to add</param>
        /// <returns></returns>
        public async Task AddForwardingEntryAsync(Layer3ForwardingEntry entry)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewType", entry.Type),
                new SOAP.SoapRequestParameter("NewDestIPAddress", entry.DestinationIPAddress),
                new SOAP.SoapRequestParameter("NewDestSubnetMask", entry.DestinationSubnetMask),
                new SOAP.SoapRequestParameter("NewSourceIPAddress", entry.SourceIPAddress),
                new SOAP.SoapRequestParameter("NewSourceSubnetMask", entry.SourceSubnetMask),
                new SOAP.SoapRequestParameter("NewGatewayIPAddress", entry.GatewayIPAddress),
                new SOAP.SoapRequestParameter("NewInterface", entry.Interface),
                new SOAP.SoapRequestParameter("NewForwardingMetric", entry.ForwardingMetric)
            };

            await this.InvokeAsync("AddForwardingEntry", parameters.ToArray());
        }

        /// <summary>
        /// Method to delete a forwarding entry
        /// </summary>
        /// <param name="entry">the entry to delete</param>
        /// <returns></returns>
        public async Task DeleteForwardingEntry(Layer3ForwardingEntry entry)
        {

        }
    }
}
