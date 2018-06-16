using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
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
        public async Task<IEnumerable<FritzDevice>> DiscoverAsync()
        {
            return await this.FindDevicesAsync();            
        }

        /// <summary>
        /// Method to find fritz devices
        /// </summary>
        /// <returns>the fritz devices</returns>
        private async Task<IEnumerable<FritzDevice>> FindDevicesAsync()
        {
            List<FritzDevice> devices = new List<FritzDevice>();
            Action<FritzDevice> callback = (device) =>
            {
                lock (devices)
                {
                    devices.Add(device);
                }
            };

            await this.DiscoverBroadcast(callback);
            return devices;
        }

        /// <summary>
        /// Method to discover by broadcast
        /// </summary>
        /// <returns></returns>
        private async Task DiscoverBroadcast(Action<FritzDevice> callback)
        {
            List<Task> broadcastTasks = new List<Task>();
            // iterate through all adapters and send multicast on 
            // valid adapters
            foreach(NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (adapter.SupportsMulticast && adapter.OperationalStatus == OperationalStatus.Up
                   && adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    int index = this.GetIPPropertiesIndex(properties);
                    IPAddress broadcastAddress = this.GetUnicastAddress(properties);

                    // skip if invalid address
                    if (broadcastAddress == null || broadcastAddress == IPAddress.None || broadcastAddress.IsIPv6LinkLocal)
                        continue;

                    broadcastTasks.Add(BeginSendReceiveAsync(broadcastAddress, index, callback));     
                }

                await Task.WhenAll(broadcastTasks.ToArray());
            }
        }

        private async Task BeginSendReceiveAsync(IPAddress broadcastAddress, int index, Action<FritzDevice> callback)
        {
            using (UdpClient client = new UdpClient(broadcastAddress.AddressFamily))
            {
                List<Task> receivebroadcast = new List<Task>();
                client.MulticastLoopback = false;
                Socket socket = client.Client;
                
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, IPAddress.HostToNetworkOrder(index));
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                socket.ReceiveBufferSize = Int32.MaxValue;
                client.ExclusiveAddressUse = false;
                socket.Bind(new IPEndPoint(broadcastAddress, 1901));

                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse("239.255.255.250"), index));

                await Task.WhenAll(
                    this.ReceiveAsync(client, callback),
                    this.BroadcastAsync(client)
                    );
            }
        }

        /// <summary>
        /// Method to receive async
        /// </summary>
        /// <param name="client">the udp client</param>
        /// <returns>the result task</returns>
        private async Task ReceiveAsync(UdpClient client, Action<FritzDevice> deviceCallback)
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 1900);

            // delay for 20 seconds
            Task waitTask = Task.Delay(TimeSpan.FromSeconds(10));

            List<Task> getDeviceInfoTasks = new List<Task>();

            Dictionary<IPAddress, FritzDevice> discovered = new Dictionary<IPAddress, FritzDevice>();

            while (!waitTask.IsCompleted)
            {
                var receiveTask = SafeReceiveAsync(client);

                // check wat for tasks
                Task finishedTask = await Task.WhenAny(receiveTask, waitTask);
                if (finishedTask == receiveTask)
                {
                    UdpReceiveResult result = await receiveTask;

                    // if there is data in buffer read and pars it
                    if (result.Buffer != null && result.Buffer.Length > 0)
                    {
                        string response = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length);

                        // create device by endpoint data
                        FritzDevice device = FritzDevice.ParseResponse(result.RemoteEndPoint.Address, result.RemoteEndPoint.Port, response);
                        if (!discovered.ContainsKey(result.RemoteEndPoint.Address))
                        {
                            if (device != null && device.Location != null && device.Location.Scheme != "unknown")
                            {
                                // fetch the device info
                                deviceCallback?.Invoke(device);
                                getDeviceInfoTasks.Add(this.GetDeviceInfo(device));
                                discovered.Add(result.RemoteEndPoint.Address, device);
                            }
                        }
                    }
                }
            }

            if (getDeviceInfoTasks.Count > 0)
            {
                await Task.WhenAll(getDeviceInfoTasks);
            }
        }

        /// <summary>
        /// Method to safe receive
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private async Task<UdpReceiveResult> SafeReceiveAsync(UdpClient client)
        {
            try
            {
                return await client.ReceiveAsync();
            }
            catch
            {
                return new UdpReceiveResult();
            }
        }

        /// <summary>
        /// Method to execute the broadcast
        /// </summary>
        /// <param name="client">the udp client</param>
        /// <returns></returns>
        private async Task BroadcastAsync(UdpClient client)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("M-SEARCH * HTTP/1.1");
            sb.AppendLine("Host:239.255.255.250:1900");
            sb.AppendLine("Man:\"ssdp:discover\"");
            sb.AppendLine("ST:urn:schemas-upnp-org:device:InternetGatewayDevice:1");
            sb.AppendLine("MX:3");

        
            byte[] searchBytes = Encoding.ASCII.GetBytes(sb.ToString());

            int broadcasts = 0;
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("239.255.255.250"), 1900);
            do
            {
                await client.SendAsync(searchBytes, searchBytes.Length, endpoint);
                broadcasts++;                    

                await Task.Delay(TimeSpan.FromSeconds(2));

            } while (broadcasts < 5);
        }

        /// <summary>
        /// task for fetching device infos
        /// </summary>
        /// <param name="device">the device to find the infos for</param>
        private async Task GetDeviceInfo(FritzDevice device)
        {
            try
            {
                Uri.TryCreate($"http://{device.IPAddress}:{device.Port}/tr64desc.xml", UriKind.Absolute, out Uri uri);
                var httpRequest = WebRequest.CreateHttp(device.Location);
                httpRequest.Timeout = 10000;
                              
                using (var response = (HttpWebResponse)(await httpRequest.GetResponseAsync()))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var responseStream = response.GetResponseStream())
                        using (var responseReader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            var data = await responseReader.ReadToEndAsync();
                            device.ParseTR64Desc(data);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Method to get the ip properties index
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private Int32 GetIPPropertiesIndex(IPInterfaceProperties properties)
        {
            int index = -1;
            
            try
            {
                index = properties.GetIPv4Properties().Index;
            }
            catch
            {
                try
                {
                    index = properties.GetIPv6Properties().Index;
                }
                catch
                {
                    // failed to get ipv4 of ipv6 properties..                    
                }
            }

            return index;
        }    

        /// <summary>
        /// Method to get the unicast address
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private IPAddress GetUnicastAddress(IPInterfaceProperties properties)
        {
            IPAddress ipAddress = IPAddress.None; 

            foreach(UnicastIPAddressInformation addressInfo in properties.UnicastAddresses)
            {
                if(addressInfo.Address.AddressFamily == AddressFamily.InterNetwork
                   || (addressInfo.Address.AddressFamily == AddressFamily.InterNetworkV6 && !addressInfo.Address.IsIPv6LinkLocal))
                {
                    ipAddress = addressInfo.Address;
                    break;
                }
            }

            return ipAddress;
        }

        #endregion
    }
}
