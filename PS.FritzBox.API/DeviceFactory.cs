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
        /// <param name="defaultPort">The Default pot for accessing the device.</param>
        /// <returns>The ready-to-use device.</returns>
        /// <exception cref="FritzDeviceException">Thrown if a device can not be created or if the data is incomplete.</exception>
        public async Task<FritzDevice> CreateDeviceAsync(IPAddress address, int defaultPort)
        {
            return await FritzDevice.CreateDeviceAsync(address, defaultPort);
        }
    }
}