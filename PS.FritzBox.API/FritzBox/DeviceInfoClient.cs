using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// client for device info service
    /// </summary>
    public class DeviceInfoClient : FritzTR64Client
    {
        #region Construction / Destruction
        /// <summary>
        /// constructor for the device info service
        /// </summary>
        /// <param name="url">the service base url</param>
        /// <param name="timeout">the service timeout</param>
        
        public DeviceInfoClient(string url, int timeout) : base(url, timeout)
        {
        }
        
        public DeviceInfoClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }
        
        public DeviceInfoClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }
        
        public DeviceInfoClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }

        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/upnp/control/deviceinfo"; 

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:DeviceInfo:1";  

        /// <summary>
        /// async Method to get the device info
        /// </summary>
        /// <returns>the device info</returns>
        public async Task<DeviceInfo> GetDeviceInfoAsync()
        {
            // call the device info here and fill it with data
            XDocument document = await this.InvokeAsync("GetInfo", null);

            DeviceInfo info = new DeviceInfo();
            info.ManufacturerName = document.Descendants("NewManufacturerName").First().Value;
            info.HardwareVersion = document.Descendants("NewHardwareVersion").First().Value;
            info.Description = document.Descendants("NewDescription").First().Value;
            info.ManufacturerOUI = document.Descendants("NewManufacturerOUI").First().Value;
            info.ModelName = document.Descendants("NewModelName").First().Value;
            info.ProductClass = document.Descendants("NewProductClass").First().Value;
            info.SerialNumber = document.Descendants("NewSerialNumber").First().Value;
            info.SoftwareVersion = document.Descendants("NewSoftwareVersion").First().Value;
            info.SpecVersion = document.Descendants("NewSpecVersion").First().Value;
            info.UpTime = Convert.ToUInt32(document.Descendants("NewUpTime").First().Value);
            info.DeviceLog = document.Descendants("NewDeviceLog").First().Value.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            return info;
        }
        
        /// <summary>
        /// async Method to get the device log 
        /// </summary>
        /// <returns>the device log</returns>
        public async Task<List<string>> GetDeviceLogAsync()
        {
            XDocument document = await this.InvokeAsync("GetDeviceLog", null);            
            return document.Descendants("NewDeviceLog").First().Value.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        /// async Method to get the security port of the device
        /// </summary>
        /// <returns>the security port</returns>
        public async Task<UInt16> GetSecurityPortAsync()
        {
            XDocument document = await this.InvokeAsync("GetSecurityPort", null);
            return Convert.ToUInt16(document.Descendants("NewSecurityPort").First().Value);
        }
    }
}
