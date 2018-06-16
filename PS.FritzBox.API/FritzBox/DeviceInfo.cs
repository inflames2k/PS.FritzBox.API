using System;
using System.Collections.Generic;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class representing device info
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// Gets the manufacturer name
        /// </summary>
        public string ManufacturerName { get; internal set; }
        /// <summary>
        /// Gets the manufacturer oui
        /// </summary>
        public string ManufacturerOUI { get; internal set; }
        /// <summary>
        /// Gets the model name
        /// </summary>
        public string ModelName { get; internal set; }
        /// <summary>
        /// Gets the description
        /// </summary>
        public string Description { get; internal set; }
        /// <summary>
        /// Gets the product class
        /// </summary>
        public string ProductClass { get; internal set; }
        /// <summary>
        /// Gets the serial number
        /// </summary>
        public string SerialNumber { get; internal set; }
        /// <summary>
        /// Gets the software version
        /// </summary>
        public string SoftwareVersion { get; internal set; }
        /// <summary>
        /// Gets the hardware version
        /// </summary>
        public string HardwareVersion { get; internal set; }
        /// <summary>
        /// Gets the spec version
        /// </summary>
        public string SpecVersion { get; internal set; }
        /// <summary>
        /// Gets the uptime
        /// </summary>
        public UInt32 UpTime { get; internal set; }
        /// <summary>
        /// Gets the start time
        /// </summary>
        public DateTime StartTime { get { return DateTime.Now.AddSeconds(UpTime * -1); } }
        /// <summary>
        /// Gets the device log
        /// </summary>
        public List<string> DeviceLog { get; internal set; }
    }
}
