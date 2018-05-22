using PS.FritzBox.API.Base;
using PS.FritzBox.API.SOAP;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// client for device config service
    /// </summary>
    public class DeviceConfigClient : FritzTR64Client
    {
        #region COnstruction / Destruction
        public DeviceConfigClient(string url, int timeout) : base(url, timeout)
        {
        }

        public DeviceConfigClient(string url, int timeout, string username) : base(url, timeout, username)
        {
        }

        public DeviceConfigClient(string url, int timeout, string username, string password) : base(url, timeout, username, password)
        {
        }

        public DeviceConfigClient(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }
        #endregion

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/tr064/upnp/control/deviceconfig";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:DeviceConfig:1";
               
        /// <summary>
        /// Method to infoke a factory reset
        /// </summary>
        public void FactoryReset() => this.FactoryResetAsync().Wait();
        
        /// <summary>
        /// async method to invoke a factory reset
        /// </summary>
        public async Task FactoryResetAsync()
        {
            XDocument document = await this.InvokeAsync("FactoryReset", null);
        }

        /// <summary>
        /// Method to infoke a reboot
        /// </summary>
        public void Reboot() => this.RebootAsync().Wait();
        
        /// <summary>
        /// async method to invoke a reboot
        /// </summary>
        public async Task RebootAsync()
        {
            XDocument document = await this.InvokeAsync("Reboot", null);
        }

        /// <summary>
        /// Method to get the config file
        /// </summary>
        /// <param name="password">the password to encrypt the config file</param>
        /// <returns>the url to the config file</returns>
        public string GetConfigFile(string password) => this.GetConfigFileAsync(password).Result;

        /// <summary>
        /// async method to get the config file
        /// </summary>
        /// <param name="password">the password to encrypt the config file</param>
        /// <returns>the url to the config file</returns>
        public async Task<string> GetConfigFileAsync(string password)
        {
            // get the config file data from device
            XDocument document = await this.InvokeAsync("X_AVM-DE_GetConfigFile", new SoapRequestParameter("NewX_AVM-DE_Password", password));
            // parse the url out of the result
            string configFile = document.Descendants("NewX_AVM-DE_ConfigFileUrl").First().Value;

            // create uri and replace the host in config file url
            Uri uri = default(Uri);
            Uri.TryCreate(this.ConnectionSettings.BaseUrl, UriKind.Absolute, out uri);
            return configFile.Replace("127.0.0.1", uri.Host);
        }

        /// <summary>
        /// Method to download the config file and save it to given path
        /// </summary>
        /// <param name="password">the password for the config file</param>
        /// <param name="path">the path to save the file to</param>
        public void DownloadConfigFile(string password, string path) => this.DownloadConfigFileAsync(password, path).Wait();

        /// <summary>
        /// async method to download the config file and save it to given path
        /// </summary>
        /// <param name="password">the password for the config file</param>
        /// <param name="path">the path to save the file to</param>
        public async Task DownloadConfigFileAsync(string password, string path)
        {
            // get the config file url from device
            string configFile = await this.GetConfigFileAsync(password);
            // get the config file and write it to file system
            byte[] fileContent = await this.DownloadFileAsync(configFile);
            File.WriteAllBytes(path, fileContent);
        }

        /// <summary>
        /// method to set the config file
        /// </summary>
        /// <param name="password">the password to decrypt the config file</param>
        /// <param name="url">the url to the config file</param>
        public void SetConfigFile(string password, string url) => this.SetConfigFileAsync(password, url).Wait();
        
        /// <summary>
        /// async method to set the config file
        /// </summary>
        /// <param name="password">the password to decrypt the config file</param>
        /// <param name="url">the url to the config file</param>
        public async Task SetConfigFileAsync(string password, string url)
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_SetConfigFile", new SoapRequestParameter("NewX_AVM-DE_Password", password), new SoapRequestParameter("NewX_AVM-DE_ConfigFileUrl", url));
        }

        /// <summary>
        /// async method to download a file
        /// </summary>
        /// <param name="url">the url</param>
        /// <returns></returns>
        private async Task<byte[]> DownloadFileAsync(string url)
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
