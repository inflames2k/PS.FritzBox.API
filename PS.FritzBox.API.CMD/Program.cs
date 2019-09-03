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
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Searching for devices...");
            IEnumerable<FritzDevice> devices = await GetDevices();

            if (devices.Count() > 0)
            {
                Console.WriteLine($"Found {devices.Count()} devices.");
                string input = string.Empty;
                int deviceIndex = -1;
                do
                {
                    int counter = 0;
                    foreach (FritzDevice device in devices)
                    {
                        Console.WriteLine($"{counter} - {device.ModelName}");
                    }
                    counter++;

                    input = Console.ReadLine();

                } while (!Int32.TryParse(input, out deviceIndex) && (deviceIndex < 0 || deviceIndex >= devices.Count()));

                FritzDevice selected = devices.Skip(deviceIndex).First();


                Configure(selected);

                do
                {
                    Console.Clear();
                    Console.WriteLine(" 1 - DeviceInfo");
                    Console.WriteLine(" 2 - DeviceConfig");
                    Console.WriteLine(" 3 - LanConfigSecurity");
                    Console.WriteLine(" 4 - LANEthernetInterface");
                    Console.WriteLine(" 5 - LANHostConfigManagement");
                    Console.WriteLine(" 6 . WANCommonInterfaceConfig");
                    Console.WriteLine(" 7 - WANIPPConnection");
                    Console.WriteLine(" 8 - WANPPPConnection");
                    Console.WriteLine(" 9 - AppSetup");
                    Console.WriteLine("10 - Layer3Forwarding");
                    Console.WriteLine("11 - UserInterface");
                    Console.WriteLine("12 - WLANConfiguration");
                    Console.WriteLine("13 - WLANConfiguration2");
                    Console.WriteLine("14 - WLANConfiguration3");
                    Console.WriteLine("15 - WANDSLInterfaceConfig");
                    Console.WriteLine("16 - WANEthernetLinkConfig");
                    Console.WriteLine("17 - WANDSLLinkConfig");

                    Console.WriteLine("r - Reinitialize");
                    Console.WriteLine("q - Exit");

                    input = Console.ReadLine();
                    if (_clientHandlers.ContainsKey(input))
                        await _clientHandlers[input].Handle();
                    else if (input.ToLower() == "r")
                        Configure(selected);
                    else if (input.ToLower() != "q")
                        Console.WriteLine("invalid choice");

                } while (input.ToLower() != "q");
            }
            else
            {
                Console.WriteLine("No devices found");
                Console.ReadLine();
            }
        }

        static async Task<IEnumerable<FritzDevice>> GetDevices()
        {
            return await new DeviceLocator().DiscoverAsync();
        }

        static void Configure(FritzDevice device)
        {
            ConnectionSettings settings = GetConnectionSettings();
            device.GetServiceClient<DeviceInfoClient>(settings);
            InitClientHandler(settings);
        }

        /// <summary>
        /// Method to get the connections ettings
        /// </summary>
        /// <returns>the connection settings</returns>
        static ConnectionSettings GetConnectionSettings()
        {
            ConnectionSettings settings = new ConnectionSettings();
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
            _clientHandlers.Add("7", new WANIPConnectonClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("8", new WANPPPConnectionClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("9", new AppSetupClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("10", new Layer3ForwardingClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("11", new UserInterfaceClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("12", new WLANConfigurationClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("13", new WLANConfigurationClientHandler2(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("14", new WLANConfigurationClientHandler3(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("15", new WANDSLInterfaceConfigClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("16", new WANEthernetLinkConfigClientHandler(settings, printOutput, getInput, wait, clearOutput));
            _clientHandlers.Add("17", new WANDSLLinkConfigClientHandler(settings, printOutput, getInput, wait, clearOutput));
        }

        
    }
}
