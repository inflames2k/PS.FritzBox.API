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
    /// client for Remote Access service
    /// </summary>
    public class RemoteAccessClient : FritzTR64Client
    {
        #region Construction / Destruction

        public RemoteAccessClient(string url, int timeout) : base(url, timeout)
        {
        }

        public RemoteAccessClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        public RemoteAccessClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        public RemoteAccessClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion  

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/x_remote";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:X_AVM-DE_RemoteAccess:1";

        /// <summary>
        /// Method to get the remote access info
        /// </summary>
        /// <returns>the remote access info</returns>
        public async Task<RemoteAccessInfo> GetInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetInfo", null);

            return new RemoteAccessInfo
            {
                Enabled = document.Descendants("NewEnabled").First().Value,
                Port = Convert.ToInt16(document.Descendants("NewPort").First().Value),
                Username = document.Descendants("NewUsername").First().Value
            };
        }

        /// <summary>
        /// Method to set remote access config
        /// </summary>
        /// <param name="enable">flag if remote access should be enabled</param>
        /// <param name="port">port for remote access</param>
        /// <param name="userName">username for remote access</param>
        /// <param name="password">password for remote access</param>
        /// <returns></returns>
        public async Task SetConfigAsync(bool enable, UInt16 port, string userName, string password)
        {
            List<SOAP.SoapRequestParameter> parameters = new List<SOAP.SoapRequestParameter>()
            {
                new SOAP.SoapRequestParameter("NewEnabled", enable ? 1 : 0),
                new SOAP.SoapRequestParameter("NewPort", port),
                new SOAP.SoapRequestParameter("NewUsername", userName),
                new SOAP.SoapRequestParameter("NewPassword", password)
            };

            await this.InvokeAsync("SetConfig", parameters.ToArray());
        }

        /// <summary>
        /// Method to get DDNS info
        /// </summary>
        /// <returns>the ddns info</returns>
        public async Task<DDNSInfo> GetDDNSInfoAsync()
        {
            XDocument document = await this.InvokeAsync("GetDDNSInfo", null);

            return new DDNSInfo
            {
                Domain = document.Descendants("NewDomain").First().Value,
                Enabled = document.Descendants("NewEnabled").First().Value,
                Mode = (DDNSMode)Enum.Parse(typeof(DDNSMode), document.Descendants("NewMode").First().Value),
                ProviderName = document.Descendants("NewProviderName").First().Value,
                ServerIPv4 = IPAddress.TryParse(document.Descendants("NewServerIPv4").First().Value, out IPAddress v4) ? v4 : IPAddress.None,
                ServerIPv6 = IPAddress.TryParse(document.Descendants("NewServerIPv6").First().Value, out IPAddress v6) ? v6 : IPAddress.None,
                StatusIPv4 = (DDNSStatus)Enum.Parse(typeof(DDNSStatus), document.Descendants("NewStatusIPv4").First().Value.Replace("-", "_")),
                StatusIPv6 = (DDNSStatus)Enum.Parse(typeof(DDNSStatus), document.Descendants("NewStatusIPv6").First().Value.Replace("-", "_")),
                UpdateUrl = document.Descendants("NewUpdateUrl").First().Value,
                Username = document.Descendants("NewUsername").First().Value
            };
        }

        /// <summary>
        /// Method to get the dyn dns providers
        /// </summary>
        /// <returns>the dyn dns providers</returns>
        public async Task<ICollection<DDNSProvider>> GetDDNSProvidersAsync()
        {
            XDocument document = await this.InvokeAsync("GetDDNSProviders", null);

            // parse the provider list
            XDocument providerList = XDocument.Parse(document.Descendants("NewProviderList").First().Value);

            List<DDNSProvider> providers = new List<DDNSProvider>();

            foreach(XElement element in this.GetElements(providerList.Root, "Item"))
            {
                providers.Add(new DDNSProvider()
                {
                    ProviderName = this.GetElementValue(element, "ProviderName"),
                    InfoUrl = this.GetElementValue(element, "InfoURL")
                });
            }           

            return providers;
        }

        /// <summary>
        /// Mehtod to get an element
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private XElement GetElement(XElement parent, string key)
        {
            return parent.Element(parent.Document.Root.Name.Namespace + key);
        }

        /// <summary>
        /// Method to get an element value
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetElementValue(XElement parent, string key)
        {
            return this.GetElement(parent, key).Value;
        }

        /// <summary>
        /// Method to get element collection
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private IEnumerable<XElement> GetElements(XElement parent, string key)
        {
            return parent.Elements(parent.Document.Root.Name.Namespace + key);
        }
    }
}
