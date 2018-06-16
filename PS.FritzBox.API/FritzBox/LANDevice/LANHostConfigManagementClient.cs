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
        protected override string ControlUrl => "/upnp/control/lanhostconfigmgm";

        /// <summary>
        /// gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:LANHostConfigManagement:1";

        /// <summary>
        /// Method to get the lan host config info
        /// </summary>
        /// <returns>the lan host config info</returns>
        public async Task<LANHostConfigInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);
            LANHostConfigInfo info = new LANHostConfigInfo();

            info.DHCPRelay = document.Descendants("NewDHCPRelay").First().Value == "1";
            info.DHCPServerConfigurable = document.Descendants("NewDHCPServerConfigurable").First().Value == "1";
            info.DHCPServerEnable = document.Descendants("NewDHCPServerEnable").First().Value == "1";
            info.DomainName = document.Descendants("NewDomainName").First().Value;
           
            var ipRouters = document.Descendants("NewIPRouters").First().Value.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();
            foreach (string ipRouter in ipRouters)
                info.IPRouters.Add(IPAddress.TryParse(ipRouter, out IPAddress router) ? router : IPAddress.None);

            var dnsServers = document.Descendants("NewDNSServers").First().Value.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();
            foreach (string dnsServer in dnsServers)
                info.DNSServers.Add(IPAddress.TryParse(dnsServer, out IPAddress server) ? server : IPAddress.None);

            info.AddressRange.MaxAddress = IPAddress.TryParse(document.Descendants("NewMaxAddress").First().Value, out IPAddress maxAddress) ? maxAddress : IPAddress.None;
            info.AddressRange.MinAddress = IPAddress.TryParse(document.Descendants("NewMinAddress").First().Value, out IPAddress minAddress) ? minAddress : IPAddress.None;

            var reservedAddresses = document.Descendants("NewReservedAddresses").First().Value.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();
            foreach (string reservedAddress in reservedAddresses)
                info.ReservedAddresses.Add(IPAddress.TryParse(reservedAddress, out IPAddress reserved) ? reserved : IPAddress.None);

            info.SubnetMask = IPAddress.TryParse(document.Descendants("NewSubnetMask").First().Value, out IPAddress subnetMask) ? subnetMask : IPAddress.None;

            return info;
        }

        /// <summary>
        /// Method to set enabled state of the dhp server
        /// </summary>
        /// <param name="enabled">flag if enable or disable</param>
        /// <returns></returns>
        public async Task SetDHCPServerEnableAsync(bool enabled)
        {
            await this.InvokeAsync("SetDHCPServerEnable", new SOAP.SoapRequestParameter("NewDHCPServerEnable", enabled ? 1 : 0));
        }

        /// <summary>
        /// Method to set the subnet mask
        /// </summary>
        /// <param name="subnetMask">the new subnet mask</param>
        /// <returns></returns>
        public async Task SetSubnetMaskAsync(IPAddress subnetMask)
        {
            await this.InvokeAsync("SetSubnetMask", new SOAP.SoapRequestParameter("NewSubnetMask", subnetMask.ToString()));
        }

        /// <summary>
        /// Method to get the subnet mask
        /// </summary>
        /// <returns>the subnet mask</returns>
        public async Task<IPAddress> GetSubnetMaskAsync()
        {
            XDocument document = await this.InvokeAsync("GetSubnetMask", null);
            return IPAddress.TryParse(document.Descendants("NewSubnetMask").First().Value, out IPAddress subnetMask) ? subnetMask : IPAddress.None;
        }

        /// <summary>
        /// Method to set ip routers
        /// </summary>
        /// <returns></returns>
        public async Task SetIPRouterAsync(string routers)
        {
            await this.InvokeAsync("SetIPRouter", new SOAP.SoapRequestParameter("NewIPRouters", routers));
        }

        /// <summary>
        /// Method to get the list of ip routers
        /// </summary>
        /// <returns>the list of ip routers</returns>
        public async Task<List<IPAddress>> GetIPRoutersListAsync()
        {
            List<IPAddress> addresses = new List<IPAddress>();
            XDocument document = await this.InvokeAsync("GetIPRoutersList", null);
            var routers = document.Descendants("NewIPRouters").First().Value.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();

            foreach (var router in routers)
                addresses.Add(IPAddress.TryParse(router, out IPAddress address) ? address : IPAddress.None);

            return addresses;
        }

        /// <summary>
        /// Method to set ip interface settings
        /// </summary>
        /// <param name="enabled">flag if interface should be enabled</param>
        /// <param name="ipAddress">ip address for the interface</param>
        /// <param name="subnetMask">subnetmask for the interface</param>
        /// <param name="addressingType">the addressing type (only supports 'Static'</param>
        public async Task SetIPInterfaceAsync(bool enabled, IPAddress ipAddress, IPAddress subnetMask, string addressingType = "Static")
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewEnabled", enabled ? "1" : "0"),
                new SOAP.SoapRequestParameter("NewIPAddress", ipAddress.ToString()),
                new SOAP.SoapRequestParameter("NewSubnetMask", subnetMask.ToString()),
                new SOAP.SoapRequestParameter("NewAddressingType", addressingType)
            };

            await this.InvokeAsync("SetIPInterface", parameters.ToArray());
        }

        /// <summary>
        /// Method to get the ip address range
        /// </summary>
        /// <returns>the ip address range</returns>
        public async Task<LANHostConfigAddressRange> GetAddressRangeAsync()
        {
            XDocument document = await this.InvokeAsync("GetAddressRange", null);
            LANHostConfigAddressRange range = new LANHostConfigAddressRange();
            range.MaxAddress = IPAddress.TryParse(document.Descendants("NewMaxAddress").First().Value, out IPAddress maxAddress) ? maxAddress : IPAddress.None;
            range.MinAddress = IPAddress.TryParse(document.Descendants("NewMinAddress").First().Value, out IPAddress minAddress) ? minAddress : IPAddress.None;

            return range;
        }

        /// <summary>
        /// Method to set the ip address range
        /// </summary>
        /// <param name="minAddress">the min address</param>
        /// <param name="maxAddress">the max address</param>
        /// <returns></returns>
        public async Task SetAddressRangeAsync(IPAddress minAddress, IPAddress maxAddress)
        {
            await this.InvokeAsync("SetAddressRange", new SOAP.SoapRequestParameter("NewMinAddress", minAddress.ToString()), new SOAP.SoapRequestParameter("NewMaxAddress", maxAddress.ToString())); 
        }

        /// <summary>
        /// Method to get the number of ip interfaces
        /// </summary>
        /// <returns>the number of ip interfaces</returns>
        public async Task<UInt16> GetIPInterfaceNumberOfEntriesAsync()
        {
            XDocument document = await this.InvokeAsync("GetIPInterfaceNumberOfEntries", null);
            return Convert.ToUInt16(document.Descendants("NewIPInterfaceNumberOfEntries").First().Value);
        }

        /// <summary>
        /// Method to get the dns servers
        /// </summary>
        /// <returns>the dns servers</returns>
        public async Task<List<IPAddress>> GetDNSServerAsync()
        {
            List<IPAddress> servers = new List<IPAddress>();
            XDocument document = await this.InvokeAsync("GetDNSServers", null);          

            var dnsServers = document.Descendants("NewDNSServers").First().Value.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();
            foreach (var dnsServer in dnsServers)
                servers.Add(IPAddress.TryParse(dnsServer, out IPAddress server) ? server : IPAddress.None);

            return servers;
        }
    }
}
