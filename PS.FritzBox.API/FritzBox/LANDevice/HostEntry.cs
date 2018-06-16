using System.Net;

namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// class representing a host
    /// </summary>
    public class HostEntry
    {
        /// <summary>
        /// Gets the mac address
        /// </summary>
        public string MACAddress { get; internal set; }
        /// <summary>
        /// Gets the ip address
        /// </summary>
        public IPAddress IPAddress { get; internal set; }
        /// <summary>
        /// gets the address source
        /// </summary>
        public string AddressSource { get; internal set; }
        /// <summary>
        /// Gets the lease time remaining
        /// </summary>
        public uint LeaseTimeRemaining { get; internal set; }
        /// <summary>
        /// gets the interface type
        /// </summary>
        public string InterfaceType { get; internal set; }
        /// <summary>
        /// Gets if the host is active
        /// </summary>
        public bool Active { get; internal set; }
        /// <summary>
        /// Gets the host name
        /// </summary>
        public string HostName { get; internal set; }
    }
}