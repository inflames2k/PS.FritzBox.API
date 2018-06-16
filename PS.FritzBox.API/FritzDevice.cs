using PS.FritzBox.API.Base;
using PS.FritzBox.API.LANDevice;
using PS.FritzBox.API.WANDevice;
using PS.FritzBox.API.WANDevice.WANConnectionDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing a fritz device
    /// </summary>
    public class FritzDevice
    {
        internal FritzDevice()
        {
        }

        /// <summary>
        /// Method to parse the udp response
        /// </summary>
        /// <param name="address"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static FritzDevice ParseResponse(IPAddress address, int port, string response)
        {
            FritzDevice device = new FritzDevice();
            device.IPAddress = address;
            device.Port = port;
            device.Location = device.ParseResponse(response);
            if (device.Location == null)
                return null;
            else
                return device;
        }

        /// <summary>
        /// Method to parse the response
        /// </summary>
        /// <param name="response">the response</param>
        private Uri ParseResponse(string response)
        {
            Dictionary<string, string> values = response.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Skip(1)
                                                 .Select(line => line.Split(new[] { ":" }, 2, StringSplitOptions.None))
                                                 .Where(parts => parts.Length == 2)
                .                                 ToDictionary(parts => parts[0].ToLowerInvariant().Trim(), parts => parts[1].Trim());

            if (values.ContainsKey("location"))
            {
                string location = values["location"];
                return Uri.TryCreate(location, UriKind.Absolute, out Uri locationUri) ? locationUri : new UriBuilder() { Scheme = "unknown", Host = location }.Uri;
            }
            else
                return null;
        }

        /// <summary>
        /// Gets the device type
        /// </summary>
        public string DeviceType { get; internal set; }

        /// <summary>
        /// Gets the friendly name
        /// </summary>
        public string FriendlyName { get; internal set; }

        /// <summary>
        /// Gets the manufacturer
        /// </summary>
        public string Manufacturer { get; internal set; }

        /// <summary>
        /// Gets the model name
        /// </summary>
        public string ModelName { get; internal set; }

        /// <summary>
        /// Gets the model description
        /// </summary>
        public string ModelDescription { get; internal set; }

        /// <summary>
        /// Gets the manufacturer url
        /// </summary>
        public string ManufacturerUrl { get; internal set; }

        /// <summary>
        /// Gets the ip address
        /// </summary>
        public IPAddress IPAddress { get; internal set; }

        /// <summary>
        /// Gets the port
        /// </summary>
        public int Port { get; internal set; }

        /// <summary>
        /// Gets or sets the location
        /// </summary>
        public Uri Location { get; set; }

        /// <summary>
        /// Gets the model number
        /// </summary>
        public string ModelNumber { get; internal set; }

        /// <summary>
        /// Gets the udn
        /// </summary>
        public string UDN { get; internal set; }

        /// <summary>
        /// the list of valid services
        /// </summary>
        private List<Type> _validServices = new List<Type>();

        /// <summary>
        /// Method to check if the device contains a given service
        /// </summary>
        /// <typeparam name="T">the service type parameter</typeparam>
        /// <returns>true if the device containes the given service</returns>
        public bool ContainsService<T>()
        {
            return this._validServices.Contains(typeof(T));
        }

        /// <summary>
        /// Method to get service client
        /// </summary>
        /// <typeparam name="T">type param</typeparam>
        /// <param name="settings">connection settings</param>
        /// <returns>the service client</returns>
        public T GetServiceClient<T>(ConnectionSettings settings)
        {
            if (!this.ContainsService<T>())
                throw new ApplicationException("Given service not is not available on the device.");

            if (String.IsNullOrEmpty(settings.BaseUrl))
            {
                settings.BaseUrl = $"http://{this.IPAddress}:{this.Port}";
                // get the security port
                int port = new DeviceInfoClient(settings.BaseUrl, settings.Timeout).GetSecurityPortAsync().GetAwaiter().GetResult();
                settings.BaseUrl = $"https://{this.IPAddress}:{port}";
            }

            return (T)Activator.CreateInstance(typeof(T), settings);
        }

        /// <summary>
        /// Method to parse the fritz tr64 description
        /// </summary>
        /// <param name="data">the description data</param>
        public void ParseTR64Desc(string data)
        {
            XDocument document = XDocument.Parse(data);
            XElement deviceRoot = this.GetElement(document.Root, "device");
            
            if (deviceRoot != null)
            {
                // read device info
                this.DeviceType = this.GetElementValue(deviceRoot, "deviceType");
                this.FriendlyName = this.GetElementValue(deviceRoot, "friendlyName");
                this.Manufacturer = this.GetElementValue(deviceRoot, "manufacturer");
                this.ManufacturerUrl = this.GetElementValue(deviceRoot, "manufacturerURL");
                this.ModelName = this.GetElementValue(deviceRoot, "modelName");
                this.ModelDescription = this.GetElementValue(deviceRoot, "modelDescription");
                this.ModelNumber = this.GetElementValue(deviceRoot, "modelNumber");
                this.UDN = this.GetElementValue(deviceRoot, "UDN");

                // iterate through the services and check for available services
                IEnumerable<XElement> serviceElements = this.GetServices(deviceRoot);
                this.AppendServices(serviceElements);
            }
        }

        /// <summary>
        /// Method to get the service elements
        /// </summary>
        /// <param name="deviceRoot"></param>
        /// <returns></returns>
        private IEnumerable<XElement> GetServices(XElement deviceRoot)
        {
            List<XElement> services = new List<XElement>();
            string deviceName = this.GetElementValue(deviceRoot, "friendlyName");
            XElement deviceList = this.GetElement(deviceRoot, "deviceList");
            if (deviceList != null)
            {
                IEnumerable<XElement> deviceElements = this.GetElements(deviceList, "device");
                foreach (XElement deviceElement in deviceElements)
                    services.AddRange(this.GetServices(deviceElement));
            }

            
            XElement serviceList = this.GetElement(deviceRoot, "serviceList");
            IEnumerable<XElement> serviceElements = this.GetElements(serviceList, "service");
            foreach (XElement serviceElement in serviceElements)
            {
                services.Add(serviceElement);
            }

            return services;
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

        /// <summary>
        /// Method to append found services
        /// </summary>
        /// <param name="serviceElements">the service elements</param>
        private void AppendServices(IEnumerable<XElement> serviceElements)
        {
            foreach(XElement serviceElement in serviceElements)
            {
                switch(this.GetElementValue(serviceElement, "serviceType"))
                {
                    case "urn:dslforum-org:service:DeviceInfo:1":
                        this._validServices.Add(typeof(DeviceInfoClient));
                        break;
                    case "urn:dslforum-org:service:DeviceConfig:1":
                        this._validServices.Add(typeof(DeviceConfigClient));
                        break;
                    case "urn:dslforum-org:service:Layer3Forwarding:1":
                        this._validServices.Add(typeof(Layer3ForwardingClient));
                        break;
                    case "urn:dslforum-org:service:LANConfigSecurity:1":
                        this._validServices.Add(typeof(LANConfigSecurityClient));
                        break;
                    case "urn:dslforum-org:service:Time:1":
                        this._validServices.Add(typeof(TimeServiceClient));
                        break;
                    case "urn:dslforum-org:service:UserInterface:1":
                        this._validServices.Add(typeof(UserInterfaceClient));
                        break;
                    case "urn:dslforum-org:service:X_AVM-DE_AppSetup:1":
                        this._validServices.Add(typeof(AppSetupClient));
                        break;
                    case "urn:dslforum-org:service:WLANConfiguration:1":
                        this._validServices.Add(typeof(WLANConfigurationClient));
                        break;
                    case "urn:dslforum-org:service:Hosts:1":
                        this._validServices.Add(typeof(HostsClient));
                        break;
                    case "urn:dslforum-org:service:LANEthernetInterfaceConfig:1":
                        this._validServices.Add(typeof(LANEthernetInterfaceClient));
                        break;
                    case "urn:dslforum-org:service:LANHostConfigManagement:1":
                        this._validServices.Add(typeof(LANHostConfigManagementClient));
                        break;
                    case "urn:dslforum-org:service:WANCommonInterfaceConfig:1":
                        this._validServices.Add(typeof(WANCommonInterfaceConfigClient));
                        break;
                    case "urn:dslforum-org:service:WANPPPConnection:1":
                        this._validServices.Add(typeof(WANPPPConnectionClient));
                        break;
                    case "urn:dslforum-org:service:WANIPConnection:1":
                        this._validServices.Add(typeof(WANIPConnectionClient));
                        break;
                }
            }
        }
    }
}
