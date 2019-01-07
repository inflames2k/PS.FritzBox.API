using System;
using System.Net;
using System.Threading.Tasks;

namespace PS.FritzBox.API
{
    /// <summary>
    /// Factory that is able to create a single <see cref="FritzDevice"/> based on an ip.
    /// </summary>
    public class DeviceFactory
    {
        /// <summary>
        /// Creates a single <see cref="FritzDevice"/>.
        /// </summary>
        /// <param name="address">The address the device can be found under.</param>
        /// <returns>The ready-to-use device.</returns>
        /// <exception cref="FritzDeviceException">Thrown if a device can not be created or if the data is incomplete.</exception>
        public async Task<FritzDevice> CreateDeviceAsync(IPAddress address)
        {
            var locationBuilder = new UriBuilder("http", address.ToString(), _dataQueryPort);
            var device = new FritzDevice(address, locationBuilder.Uri);
            var tr64Reader = new Tr64DataReader();

            var tr64Data = await tr64Reader.ReadDeviceInfoAsync(device);

            device.ParseTR64Desc(tr64Data.Data);
            return device;
        }

        private const int _dataQueryPort = 49000;
    }
}