using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            WANCommonInterfaceConfigClient client = new WANCommonInterfaceConfigClient("https://fritz.box", 10000);
            client.UserName = "inflames2k";
            client.Password = "********";
            
            OnlineMonitorInfo monitor = client.GetOnlineMonitor(0).Result;

            Console.WriteLine($"Max Downstream: {monitor.MaxDownStream}");
            Console.WriteLine($"Max Upstream: {monitor.MaxUpStream}");

            Console.ReadLine();
        }
    }
}
