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
        protected override string ControlUrl => "/upnp/control/layer3forwarding";

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
        /// <param name="sourceIPAddress">the source ip address</param>
        /// <param name="sourceSubnetMask">the source subnet mask</param>
        /// <param name="destinationIPAddress">the destination ip address</param>
        /// <param name="destinationSubnetMask">the destination subnet mask</param>
        /// <returns></returns>
        public async Task DeleteForwardingEntryAsync(IPAddress sourceIPAddress, IPAddress sourceSubnetMask, IPAddress destinationIPAddress, IPAddress destinationSubnetMask)
        {
            await this.DeleteForwardingEntryAsync(
                new Layer3ForwardingEntry()
                {
                    SourceIPAddress = sourceIPAddress,
                    SourceSubnetMask = sourceIPAddress,
                    DestinationIPAddress = destinationIPAddress,
                    DestinationSubnetMask = destinationIPAddress
                });
        }

        /// <summary>
        /// Method to delete a forwarding entry
        /// </summary>
        /// <param name="entry">the entry to delete</param>
        /// <returns></returns>
        public async Task DeleteForwardingEntryAsync(Layer3ForwardingEntry entry)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewDestIPAddress", entry.DestinationIPAddress.ToString()),
                new SOAP.SoapRequestParameter("NewDestSubnetMask", entry.DestinationSubnetMask.ToString()),
                new SOAP.SoapRequestParameter("NewSourceIPAddress", entry.SourceIPAddress.ToString()),
                new SOAP.SoapRequestParameter("NewSourceSubnetMask", entry.SourceSubnetMask.ToString())                
            };

            await this.InvokeAsync("AddForwardingEntry", parameters.ToArray());
        }

        /// <summary>
        /// Method to get a specific forwarding entry
        /// </summary>
        /// <param name="sourceIPAddress">the source ip address</param>
        /// <param name="sourceSubnetMask">the source subnet mask</param>
        /// <param name="destinationIPAddress">the destination ip address</param>
        /// <param name="destinationSubnetMask">the destination subnet mask</param>
        /// <returns>the specific forwarding entry</returns>
        public async Task<Layer3ForwardingEntry> GetSpecificForwardingEntryAsync(IPAddress sourceIPAddress, IPAddress sourceSubnetMask, IPAddress destinationIPAddress, IPAddress destinationSubnetMask)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewDestIPAddress", destinationIPAddress.ToString()),
                new SOAP.SoapRequestParameter("NewDestSubnetMask", destinationSubnetMask.ToString()),
                new SOAP.SoapRequestParameter("NewSourceIPAddress", sourceIPAddress.ToString()),
                new SOAP.SoapRequestParameter("NewSourceSubnetMask", sourceSubnetMask.ToString())
            };

            XDocument document = await this.InvokeAsync("GetSpecificForwardingEntry", parameters.ToArray());

            return new Layer3ForwardingEntry()
            {
                DestinationIPAddress = destinationIPAddress,
                DestinationSubnetMask = destinationSubnetMask,
                SourceIPAddress = sourceIPAddress,
                SourceSubnetMask = sourceSubnetMask,
                ForwardingMetric = Convert.ToInt32(document.Descendants("NewForwardingMetric").First().Value),
                GatewayIPAddress = IPAddress.TryParse(document.Descendants("NewGatewayIPAddress").First().Value, out IPAddress gateway) ? gateway : IPAddress.None,
                Interface = document.Descendants("NewInterface").First().Value,
                Type = document.Descendants("NewType").First().Value,
                Enabled = document.Descendants("NewEnable").First().Value == "1",
                Status = document.Descendants("NewStatus").First().Value
            };
        }

        /// <summary>
        /// Method to get a generic layer3 forwarding entry
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>the generic entry</returns>
        public async Task<Layer3ForwardingEntry> GetGenericForwardingEntryAsync(int index)
        {
            XDocument document = await this.InvokeAsync("GetGenericForwardingEntry", new SOAP.SoapRequestParameter("NewForwardingIndex", index));

            return new Layer3ForwardingEntry()
            {
                DestinationIPAddress = IPAddress.TryParse(document.Descendants("NewDestIPAddress").First().Value, out IPAddress destIP) ? destIP : IPAddress.None,
                DestinationSubnetMask = IPAddress.TryParse(document.Descendants("NewDestSubnetMask").First().Value, out IPAddress destSub) ? destSub : IPAddress.None,
                SourceIPAddress = IPAddress.TryParse(document.Descendants("NewSourceIPAddress").First().Value, out IPAddress sourceIP) ? sourceIP : IPAddress.None,
                SourceSubnetMask = IPAddress.TryParse(document.Descendants("NewSourceSubnetMask").First().Value, out IPAddress sourceSub) ? sourceSub : IPAddress.None,
                ForwardingMetric = Convert.ToInt32(document.Descendants("NewForwardingMetric").First().Value),
                GatewayIPAddress = IPAddress.TryParse(document.Descendants("NewGatewayIPAddress").First().Value, out IPAddress gateway) ? gateway : IPAddress.None,
                Interface = document.Descendants("NewInterface").First().Value,
                Type = document.Descendants("NewType").First().Value,
                Enabled = document.Descendants("NewEnable").First().Value == "1",
                Status = document.Descendants("NewStatus").First().Value
            };
        }

        /// <summary>
        /// Method to set enabled state for a forwarding entry
        /// </summary>
        /// <param name="sourceIPAddress">source ip address</param>
        /// <param name="sourceSubnetMask">source subnet mask</param>
        /// <param name="destinationIPAddress">destination ip address</param>
        /// <param name="destinationSubnetMask">destination subnet mask</param>
        /// <param name="enabled">flag if enabled or disabled</param>
        /// <returns></returns>
        public async Task SetForwardingEntryEnableAsync(IPAddress sourceIPAddress, IPAddress sourceSubnetMask, IPAddress destinationIPAddress, IPAddress destinationSubnetMask, bool enabled)
        {
            await this.SetForwardingEntryEnableAsync(new Layer3ForwardingEntry()
                                                    {
                                                        SourceIPAddress = sourceIPAddress,
                                                        SourceSubnetMask = sourceIPAddress,
                                                        DestinationIPAddress = destinationIPAddress,
                                                        DestinationSubnetMask = destinationSubnetMask
                                                    },
                                                    enabled);
        }

        /// <summary>
        /// Method to set enabled state for a forwarding entry
        /// </summary>
        /// <param name="entry">the entry</param>
        /// <param name="enabled">flag if enabled or disabled</param>
        /// <returns></returns>
        public async Task SetForwardingEntryEnableAsync(Layer3ForwardingEntry entry, bool enabled)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewDestIPAddress", entry.DestinationIPAddress.ToString()),
                new SOAP.SoapRequestParameter("NewDestSubnetMask", entry.DestinationSubnetMask.ToString()),
                new SOAP.SoapRequestParameter("NewSourceIPAddress", entry.SourceIPAddress.ToString()),
                new SOAP.SoapRequestParameter("NewSourceSubnetMask", entry.SourceSubnetMask.ToString()),
                new SOAP.SoapRequestParameter("NewEnable", enabled ? "1" : "0")
            };

            await this.InvokeAsync("SetForwardingEntryEnable", parameters.ToArray());
        }

        /// <summary>
        /// Method to get all forwarding entries
        /// </summary>
        /// <returns>all forwarding entries</returns>
        public async Task<List<Layer3ForwardingEntry>> GetForwardingEntriesAsync()
        {
            List<Layer3ForwardingEntry> entries = new List<Layer3ForwardingEntry>();
            int count = await this.GetForwardNumberOfEntriesAsync();

            for (int i = 0; i < count; i++)
                entries.Add(await this.GetGenericForwardingEntryAsync(i));

            return entries;
        }
    }
}
