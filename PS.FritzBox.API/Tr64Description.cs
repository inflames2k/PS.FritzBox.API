using System;

namespace PS.FritzBox.API
{
    internal class Tr64Description
    {
        /// <summary>
        /// Gets the device the description is valid for.
        /// </summary>
        public FritzDevice Device { get; }

        /// <summary>
        /// The raw data retrieved from the device.
        /// </summary>
        public string Data { get; }

        public Tr64Description(FritzDevice device, string data)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException("message", nameof(data));
            }

            Device = device;
            Data = data;
        }
    }
}