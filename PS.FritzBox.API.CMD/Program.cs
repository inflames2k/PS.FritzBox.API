using PS.FritzBox.API;
using PS.FritzBox.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    class Program
    {
        static Dictionary<string, ClientHandler> _clientHandlers = new Dictionary<string, ClientHandler>();

        static void Main(string[] args)
        {
            Configure();

            string input = string.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine(" 1 - DeviceInfo");
                Console.WriteLine(" 2 - DeviceConfig");
                Console.WriteLine(" 3 - LanConfigSecurity");
                Console.WriteLine(" 4 - LANEthernetInterface");
                Console.WriteLine(" 5 - LANHostConfigManagement");
                Console.WriteLine(" 6 - WANCommonInterfaceConfig");
                Console.WriteLine(" 7 - WANPPPConnection");
                Console.WriteLine(" 8 - AppSetup");
                Console.WriteLine(" 9 - Layer3Forwarding");
                Console.WriteLine("10 - UserInterface");

                Console.WriteLine("r - Reinitialize");
                Console.WriteLine("q - Exit");

                input = Console.ReadLine();
                if (_clientHandlers.ContainsKey(input))
                    _clientHandlers[input].Handle();
                else if (input.ToLower() == "r")
                    Configure();
                else if (input.ToLower() != "q")
                    Console.WriteLine("invalid choice");

            } while (input.ToLower() != "q");
        }

        static void Configure()
        {
            ConnectionSettings settings = GetConnectionSettings();
            InitClientHandler(settings);
        }

        /// <summary>
        /// Method to get the connections ettings
        /// </summary>
        /// <returns>the connection settings</returns>
        static ConnectionSettings GetConnectionSettings()
        {
            ConnectionSettings settings = new ConnectionSettings();
            Console.Write("Url: ");
            settings.BaseUrl = Console.ReadLine();
            Console.Write("User: ");
            settings.UserName = Console.ReadLine();
            Console.Write("Password: ");
            settings.Password = Console.ReadLine();

            return settings;
        }

        /// <summary>
        /// Method to initialize the client handlers
        /// </summary>
        /// <param name="settings"></param>
        static void InitClientHandler(ConnectionSettings settings)
        {
            _clientHandlers.Clear();
            Action clearOutput = () => Console.Clear();
            Action wait = () => Console.ReadKey();
            Action<string> printOutput = (output) => Console.WriteLine(output);
            Func<string> getInput = () => Console.ReadLine();

            _clientHandlers.Add("1", new DeviceInfoClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("2", new DeviceConfigClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("3", new LanConfigSecurityHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("4", new LANEthernetInterfaceClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("5", new LANHostConfigManagementClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("6", new WANCommonInterfaceConfigClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("7", new WANPPPConnectionClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("8", new AppSetupClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("9", new Layer3ForwardingClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("10", new UserInterfaceClientHandler(settings, printOutput, getInput, wait, clearOutput));
        }

        
    }
}
