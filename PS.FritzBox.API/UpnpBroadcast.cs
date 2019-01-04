using System;
using System.Net;
using System.Text;

namespace PS.FritzBox.API
{
    /// <summary>
    /// Contains all information that is necessary to perform a UPNP broadcast.
    /// </summary>
    internal class UpnpBroadcast
    {
        /// <summary>
        /// Gets the content of the broadcast, the data that shall be sent.
        /// </summary>
        public byte[] Content { get; }

        /// <summary>
        /// Gets the number of bytes of <see cref="Content"/>.
        /// </summary>
        public int ContentLenght
        {
            get { return Content.Length; }
        }

        /// <summary>
        /// Gets the ip that shall be used for the broadcast. This can either be an IPv4 or IPv6.
        /// </summary>
        public IPAddress IpAdress
        {
            get
            {
                return IpEndPoint.Address;
            }
        }

        /// <summary>
        /// Gets the whole <see cref="IPEndPoint"/> that shall be used for the broadcast.
        /// </summary>
        public IPEndPoint IpEndPoint { get; }

        /// <summary>
        /// Gets the port which shall be used for the broadcast.
        /// </summary>
        public int Port { get { return 1900; } }

        private UpnpBroadcast(string host)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new ArgumentException(nameof(host));
            }

            var ipAdress = IPAddress.Parse(host);
            IpEndPoint = new IPEndPoint(ipAdress, Port);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("M-SEARCH * HTTP/1.1");
            sb.AppendLine($"Host:239.255.255.250:1900");
            sb.AppendLine("Man:\"ssdp:discover\"");
            sb.AppendLine("ST:urn:schemas-upnp-org:device:InternetGatewayDevice:1");
            sb.AppendLine("MX:3");

            Content = Encoding.ASCII.GetBytes(sb.ToString());
        }

        /// <summary>
        /// Creates a UPNP broadcast for an IPv4 network interface.
        /// </summary>
        public static UpnpBroadcast CreateIpV4Broadcast()
        {
            return new UpnpBroadcast("239.255.255.250");
        }

        /// <summary>
        /// Creates a UPNP broadcast for an IPv6 network interface.
        /// </summary>
        public static UpnpBroadcast CreateIpV6Broadcast()
        {
            return new UpnpBroadcast("FF05::C");
        }
    }
}