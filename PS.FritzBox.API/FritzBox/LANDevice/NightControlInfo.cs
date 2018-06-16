namespace PS.FritzBox.API.LANDevice
{
    /// <summary>
    /// class representing night control info
    /// </summary>
    public class NightControlInfo
    {
        /// <summary>
        /// the night control data
        /// </summary>
        public string NightControl { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool NightTimeControlNoForcedOff { get; set; }
    }
}