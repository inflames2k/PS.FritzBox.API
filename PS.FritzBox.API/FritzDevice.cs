using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing a fritz device
    /// </summary>
    public class FritzDevice
    {
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
        /// Gets the ip address
        /// </summary>
        public IPAddress IPAddress { get; internal set; }

        /// <summary>
        /// Gets the port
        /// </summary>
        public int Port { get; internal set; }

        /// <summary>
        /// Method to get service client
        /// </summary>
        /// <typeparam name="T">type param</typeparam>
        /// <param name="settings">connection settings</param>
        /// <returns>the service client</returns>
        public T GetServiceClient<T>(ConnectionSettings settings)
        {
            if (String.IsNullOrEmpty(settings.BaseUrl))
            {
                settings.BaseUrl = $"http://{this.IPAddress}:{this.Port}";
                // get the security port
                int port = new DeviceInfoClient(settings.BaseUrl, settings.Timeout).GetSecurityPortAsync().GetAwaiter().GetResult();
                settings.BaseUrl = $"https://{this.IPAddress}:{port}";
            }

            return (T)Activator.CreateInstance(typeof(T), settings);
        }
    }
}
