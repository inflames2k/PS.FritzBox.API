using SsdpRadar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class for locating upnp devices
    /// </summary>
    public class DeviceLocator 
    {
        #region Methods

        /// <summary>
        /// Method to start discovery
        /// </summary>
        /// <returns></returns>
        public async Task<List<FritzDevice>> DiscoverAsync()
        {
            List<FritzDevice> devices = new List<FritzDevice>();
            FinderService service = new SsdpRadar.FinderService(5, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
            
            var response = await service.FindDevicesAsync();
            foreach(var item in response)
            {
                if(item.Info != null && item.Info.Manufacturer.StartsWith("AVM")
                   && item.Info.DeviceType.Equals("urn:schemas-upnp-org:device:InternetGatewayDevice:1"))
                {
                    FritzDevice device = new FritzDevice();
                    device.DeviceType = item.Info.DeviceType;
                    device.FriendlyName = item.Info.FriendlyName;
                    device.Manufacturer = item.Info.Manufacturer;
                    device.ModelName = item.Info.ModelName;
                    device.IPAddress = item.RemoteEndPoint;
                    device.Port = item.Location.Port;

                    devices.Add(device);
                }
            }

            return devices;
        }

        #endregion
    }
}
