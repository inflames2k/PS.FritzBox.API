using System;

namespace PS.FritzBox.API
{
    /// <summary>
    /// extended update info
    /// </summary>
    public class XUpdateInfo
    {
        /// <summary>
        /// auto update mode
        /// </summary>
        public AutoUpdateMode AutoUpdateMode { get; internal set; }
        /// <summary>
        /// update time
        /// </summary>
        public DateTime UpdateTime { get; internal set; }
        /// <summary>
        /// update state
        /// </summary>
        public UpdateSuccessState UpdateSuccess { get; internal set; }
        /// <summary>
        /// last firmware version
        /// </summary>
        internal FirmwareInfo LastFirmware { get; set; }
        /// <summary>
        /// current firmware version
        /// </summary>
        internal FirmwareInfo CurrentFirmware { get; set; }
    }
}