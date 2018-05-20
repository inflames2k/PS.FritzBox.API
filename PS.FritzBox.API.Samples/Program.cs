using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.FritzBox.API.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            string selection = string.Empty;
            do
            {
                PrintMainMenu();
            } while (selection != "Q");
        }

        static void PrintMainMenu()
        {
            Console.WriteLine("1: DeviceInfo");
            Console.WriteLine("2: LANEthernetConfig");
            Console.WriteLine("3: WANIPConnection");
            Console.WriteLine("Q: Quit");
        }
    }
}
