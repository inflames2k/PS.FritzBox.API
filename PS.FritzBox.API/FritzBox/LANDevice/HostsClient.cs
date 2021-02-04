using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// client for hosts service
    /// </summary>
    public class HostsClient : FritzTR64Client
    {
        #region Construction / Destruction
        
        public HostsClient(string url, int timeout) : base(url, timeout)
        {
        }
        
        public HostsClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }
        
        public HostsClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }
        
        public HostsClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/hosts";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:Hosts:1";

        /// <summary>
        /// Method to get the number of hosts
        /// </summary>
        /// <returns>the number of hosts</returns>
        public async Task<UInt16> GetHostNumberOfEntriesAsync()
        {
            XDocument document = await this.InvokeAsync("GetHostNumberOfEntries", null);
            return UInt16.TryParse(document.Descendants("NewHostNumberOfEntries").First().Value, out UInt16 number) ? number : (UInt16)0;
        }

        /// <summary>
        /// Method to get a specific host entry
        /// </summary>
        /// <param name="macAddress">the mac address of the entry</param>
        /// <returns>the host entry</returns>
        public async Task<HostEntry> GetSpecificHostEntryAsync(string macAddress)
        {
            XDocument document = await this.InvokeAsync("GetSpecificHostEntry", new SOAP.SoapRequestParameter("NewMACAddress", macAddress));
            return new HostEntry()
            {
                MACAddress = macAddress, 
                IPAddress = IPAddress.TryParse(document.Descendants("NewIPAddress").First().Value, out IPAddress ip) ? ip : IPAddress.None,
                AddressSource = document.Descendants("NewAddressSource").First().Value,
                LeaseTimeRemaining = UInt32.TryParse(document.Descendants("NewLeaseTimeRemaining").First().Value, out UInt32 leaseTime) ? leaseTime : (UInt32)0,
                InterfaceType = document.Descendants("NewInterfaceType").First().Value,
                Active = document.Descendants("NewActive").First().Value == "1",
                HostName = document.Descendants("NewHostName").First().Value
            };
        }

        /// <summary>
        /// Method to get a specific host entry by index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>the host entry</returns>
        public async Task<HostEntry> GetGenericHostEntryAsync(UInt16 index)
        {
            XDocument document = await this.InvokeAsync("GetGenericHostEntry", new SOAP.SoapRequestParameter("NewIndex", index));
            return new HostEntry()
            {
                MACAddress = document.Descendants("NewMACAddress").First().Value,
                IPAddress = IPAddress.TryParse(document.Descendants("NewIPAddress").First().Value, out IPAddress ip) ? ip : IPAddress.None,
                AddressSource = document.Descendants("NewAddressSource").First().Value,
                LeaseTimeRemaining = UInt32.TryParse(document.Descendants("NewLeaseTimeRemaining").First().Value, out UInt32 leaseTime) ? leaseTime : (UInt32)0,
                InterfaceType = document.Descendants("NewInterfaceType").First().Value,
                Active = document.Descendants("NewActive").First().Value == "1",
                HostName = document.Descendants("NewHostName").First().Value
            };
        }
    }
}
