using System.Net;

namespace PS.FritzBox.API
{
    internal class DDNSInfo
    {
        public string Domain { get; set; }
        public string Enabled { get; set; }
        public DDNSMode Mode { get; set; }
        public string ProviderName { get; set; }
        public IPAddress ServerIPv4 { get; set; }
        public IPAddress ServerIPv6 { get; set; }
        public DDNSStatus StatusIPv4 { get; set; }
        public DDNSStatus StatusIPv6 { get; set; }
        public string UpdateUrl { get; set; }
        public string Username { get; set; }
    }
}