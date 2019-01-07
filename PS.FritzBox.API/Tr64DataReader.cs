using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PS.FritzBox.API
{
    internal class Tr64DataReader
    {
        public async Task<Tr64Description> ReadDeviceInfoAsync(FritzDevice device)
        {
            var uri = CreateRequestUri(device);
            var httpRequest = WebRequest.CreateHttp(uri);
            httpRequest.Timeout = 10000;

            try
            {
                using (var response = (HttpWebResponse)(await httpRequest.GetResponseAsync()))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var responseStream = response.GetResponseStream())
                        using (var responseReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            var data = await responseReader.ReadToEndAsync();
                            return new Tr64Description(device, data);
                        }
                    }
                    else
                    {
                        throw new FritzDeviceException($"Failed to get device info for device {device.Location.Host}. Response {response.StatusCode} - {response.StatusDescription}.");
                    }
                }
            }
            catch (WebException ex)
            {
                throw new FritzDeviceException($"Failed to get device info for device {device.Location.Host}.", ex);
            }
        }

        private Uri CreateRequestUri(FritzDevice device)
        {
            var uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "http";
            uriBuilder.Host = device.Location.Host;
            uriBuilder.Port = device.Location.Port;
            uriBuilder.Path = "tr64desc.xml";

            return uriBuilder.Uri;
        }
    }
}