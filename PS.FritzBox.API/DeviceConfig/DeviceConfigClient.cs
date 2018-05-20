using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class for getting device configuration and configuring the fritz.box
    /// (backup and restore settings, factory reset, reboot)
    /// </summary>
    public class DeviceConfigClient : FritzTR64Client
    {
        public DeviceConfigClient(string url, int timeout) : base(url, timeout)
        {

        }

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/tr064/upnp/control/deviceconfig";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:DeviceConfig:1";
               
        /// <summary>
        /// Method to invoke a factory reset
        /// </summary>
        public async void FactoryReset()
        {
            XDocument document = await this.Invoke("FactoryReset", null);
        }

        /// <summary>
        /// Method to invoke a reboot
        /// </summary>
        public async void Reboot()
        {
            XDocument document = await this.Invoke("Reboot", null);
        }

        /// <summary>
        /// Method to get the config file
        /// </summary>
        /// <param name="password">the password to encrypt the config file</param>
        /// <returns>the url to the config file</returns>
        public async Task<string> GetConfigFile(string password)
        {
            // get the config file data from device
            XDocument document = await this.Invoke("X_AVM-DE_GetConfigFile", new SoapRequestParameter("NewX_AVM-DE_Password", password));
            // parse the url out of the result
            string configFile = document.Descendants("NewX_AVM-DE_ConfigFileUrl").First().Value;

            // create uri and replace the host in config file url
            Uri uri = default(Uri);
            Uri.TryCreate(this.Url, UriKind.Absolute, out uri);
            return configFile.Replace("127.0.0.1", uri.Host);
        }

        /// <summary>
        /// Method to download the config file and save it to given path
        /// </summary>
        /// <param name="password">the password for the config file</param>
        /// <param name="path">the path to save the file to</param>
        public async void DownloadConfigFile(string password, string path)
        {
            // get the config file url from device
            string configFile = await this.GetConfigFile(password);
            // get the config file and write it to file system
            byte[] fileContent = await this.DownloadFile(configFile);
            File.WriteAllBytes(path, fileContent);
        }

        /// <summary>
        /// Method to set the config file
        /// </summary>
        /// <param name="password">the password to decrypt the config file</param>
        /// <param name="url">the url to the config file</param>
        public async void SetConfigFile(string password, string url)
        {
            XDocument document = await this.Invoke("X_AVM-DE_SetConfigFile", new SoapRequestParameter("NewX_AVM-DE_Password", password), new SoapRequestParameter("NewX_AVM-DE_ConfigFileUrl", url));
            // parse the result
        }

        /// <summary>
        /// Method to download a file
        /// </summary>
        /// <param name="url">the url</param>
        /// <returns></returns>
        private async Task<byte[]> DownloadFile(string url)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            using (var client = new HttpClient(handler))
            {
                using (var result = await client.GetAsync(url))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsByteArrayAsync();
                    }

                }
            }
            return null;
        }
    }
}
