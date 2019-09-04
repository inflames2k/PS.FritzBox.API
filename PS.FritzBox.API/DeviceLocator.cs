using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    [Obsolete("Use FritzDevice.LocateDevicesAsync() - Will be made internal in Version 2.0")]
    public class DeviceLocator 
    {
        #region Methods

        /// <summary>
        /// Method to start discovery
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use FritzDevice.LocateDevicesAsync()")]
        public async Task<ICollection<FritzDevice>> DiscoverAsync()
        {
            return await this.FindDevicesAsync();            
        }

        /// <summary>
        /// Method to find fritz devices
        /// </summary>
        /// <returns>the fritz devices</returns>
        private async Task<ICollection<FritzDevice>> FindDevicesAsync()
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
                    IPAddress broadcastAddress = this.GetUnicastAddress(properties);

                    // skip if invalid address
                    if (broadcastAddress == null || broadcastAddress == IPAddress.None || broadcastAddress.IsIPv6LinkLocal)
                        continue;

                    broadcastTasks.Add(BeginSendReceiveAsync(broadcastAddress, callback));     
                }

                await Task.WhenAll(broadcastTasks.ToArray());
            }
        }

        private async Task BeginSendReceiveAsync(IPAddress broadcastAddress, Action<FritzDevice> callback)
        {
            using (UdpClient client = new UdpClient(broadcastAddress.AddressFamily))
            {
                List<Task> receivebroadcast = new List<Task>();
                client.MulticastLoopback = false;
                Socket socket = client.Client;


                var broadcastViaIpV4 = broadcastAddress.AddressFamily == AddressFamily.InterNetwork;

                if (broadcastViaIpV4)
                {
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, broadcastAddress.GetAddressBytes());
                }
                else
                {
                    byte[] interfaceArray = BitConverter.GetBytes((int)broadcastAddress.ScopeId);

                    var mcastOption = new IPv6MulticastOption(broadcastAddress, broadcastAddress.ScopeId);
                    socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastInterface, interfaceArray);
                }

                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                socket.ReceiveBufferSize = Int32.MaxValue;
                client.ExclusiveAddressUse = false;
                socket.Bind(new IPEndPoint(broadcastAddress, 1901));

                var broadCast = broadcastViaIpV4 ? UpnpBroadcast.CreateIpV4Broadcast() : UpnpBroadcast.CreateIpV6Broadcast();
                if (broadcastViaIpV4)
                {
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(broadCast.IpAdress));
                }
                else
                {
                    socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, new IPv6MulticastOption(broadCast.IpAdress, broadcastAddress.ScopeId));
                }

                await Task.WhenAll(
                    this.ReceiveAsync(client, callback),
                    this.BroadcastAsync(client, broadCast)
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

            var getDeviceInfoTasks = new List<Task<Tr64Description>>();

            Dictionary<IPAddress, FritzDevice> discovered = new Dictionary<IPAddress, FritzDevice>();

            var tr64DataReader = new Tr64DataReader();
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
                        FritzDevice device = await FritzDevice.ParseResponseAsync(result.RemoteEndPoint.Address, response);
                        if (!discovered.ContainsKey(result.RemoteEndPoint.Address))
                        {
                            if (device != null && device.Location != null && device.Location.Scheme != "unknown")
                            {
                                // fetch the device info
                                deviceCallback?.Invoke(device);
                                getDeviceInfoTasks.Add(tr64DataReader.ReadDeviceInfoAsync(device));
                                discovered.Add(result.RemoteEndPoint.Address, device);
                            }
                        }
                    }
                }
            }

            if (getDeviceInfoTasks.Count > 0)
            {
               
                foreach(var task in getDeviceInfoTasks)
                {
                    try
                    {
                        var description = await task;
                        description.Device.ParseTR64Desc(description.Data);
                    } catch(FritzDeviceException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
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
        /// <param name="broadcast">The broadcast to send to the client.</param>
        private async Task BroadcastAsync(UdpClient client, UpnpBroadcast broadcast)
        {
            int broadcasts = 0;
          
            do
            {
                await client.SendAsync(broadcast.Content, broadcast.ContentLenght, broadcast.IpEndPoint);
                broadcasts++;                    

                await Task.Delay(TimeSpan.FromSeconds(2));

            } while (broadcasts < 5);
        }

      

        /// <summary>
        /// Method to get the unicast address
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private IPAddress GetUnicastAddress(IPInterfaceProperties properties)
        {
            IPAddress ipAddress = IPAddress.None;

            foreach (UnicastIPAddressInformation addressInfo in properties.UnicastAddresses)
            {
                if (addressInfo.Address.AddressFamily == AddressFamily.InterNetwork
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
