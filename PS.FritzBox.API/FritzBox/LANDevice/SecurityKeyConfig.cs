namespace PS.FritzBox.API.LANDevice
{
    public class SecurityKeyConfig
    {
        public string WEPKey0 { get; internal set; }
        public string WEPKey1 { get; internal set; }
        public string WEPKey2 { get; internal set; }
        public string WEPKey3 { get; internal set; }
        public string PreSharedKey { get; internal set; }
        public string KeyPassphrase { get; internal set; }
    }
}