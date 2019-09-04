using PS.FritzBox.API.WANDevice.WANConnectionDevice;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class WANPPPConnectionClientHandler : ClientHandler
    {
        WANPPPConnectionClient _client;

        public WANPPPConnectionClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            _client = device.GetServiceClient<WANPPPConnectionClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"WANPPPConnectionClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - ForceTermination");
                this.PrintOutputAction("2 - RequestConnetion");
                this.PrintOutputAction("3 - GetInfo");
                this.PrintOutputAction("4 - GetDNSServers");
                this.PrintOutputAction("5 - GetExternalIPAddress");
                this.PrintOutputAction("6 - GetAutoDisconnectTimeSpan");
                this.PrintOutputAction("7 - GetConnectionTypeInfo");
                this.PrintOutputAction("8 - GetLinkLayerMaxBitrates");
                this.PrintOutputAction("9 - GetNATRSIPStatus");
                this.PrintOutputAction("10 - GetStatusInfo");
                this.PrintOutputAction("11 - GetPortMappingNumberOfEntries");
                this.PrintOutputAction("12 - GetGenericPortMappingEntry");
                this.PrintOutputAction("13 - GetSpecificPortMappingEntry");
                this.PrintOutputAction("14 - GetPortMappings");
                this.PrintOutputAction("15 - AddPortMapping");
                this.PrintOutputAction("16 - DeletePortmapping");
                this.PrintOutputAction("17 - GetUserName");
                this.PrintOutputAction("18 - SetAutoDisconnectTimeSpan");

                this.PrintOutputAction("r - Return");

                input = this.GetInputFunc();

                try
                {
                    switch (input)
                    {
                        case "1":
                            await this.ForceTermination();
                            break;
                        case "2":
                            await this.RequestConnection();
                            break;
                        case "3":
                            await this.GetInfo();
                            break;
                        case "4":
                            await this.GetDNSServers();
                            break;
                        case "5":
                            await this.GetExternalIPAddress();
                            break;
                        case "6":
                            await this.GetAutoDisconnectTimeSpan();
                            break;
                        case "7":
                            await this.GetConnectionTypeInfo();
                            break;
                        case "8":
                            await this.GetLinkLayerMaxBitrates();
                            break;
                        case "9":
                            await this.GetNATRSIPStatus();
                            break;
                        case "10":
                            await this.GetStatusInfo();                            
                            break;
                        case "11":
                            await this.GetPortMappingNumberOfEntries();
                            break;
                        case "12":
                            await this.GetGenericPortMappingEntry();
                            break;
                        case "14":
                            await this.GetPortMappings();
                            break;
                        case "13":
                            await this.GetSpecificPortMappingEntry();
                            break;
                        case "17":
                            await this.GetUserName();
                            break;
                        case "18":
                            await this.SetAutoDisconnectTimeSpan();
                            break;
                        case "15":
                            await this.AddPortMapping();
                            break;
                        case "16":                            
                            await this.DeletePortMapping();
                            break;
                        case "r":
                            break;
                        default:
                            this.PrintOutputAction("invalid choice");
                            break;
                    }

                    if (input != "r")
                        this.WaitAction();
                }
                catch (Exception ex)
                {
                    this.PrintOutputAction(ex.ToString());
                    this.WaitAction();
                }

            } while (input != "r");
        }

        private async Task ForceTermination()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            await this._client.ForceTerminationAsync();
            this.PrintOutputAction("Termination forced");
        }

        private async Task RequestConnection()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            await this._client.RequestConnectionAsync();
            this.PrintOutputAction("connection requested");
        }

        private async Task GetInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = await this._client.GetInfoAsync();
            this.PrintObject(info);
        }

        private async Task GetDNSServers()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var dnsServers = await this._client.GetDNSServersAsync();
            foreach (var dnsServer in dnsServers)
                this.PrintOutputAction(dnsServer.ToString());
        }

        private async Task GetExternalIPAddress()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var ip = await this._client.GetExternalIPAddressAsync();
            this.PrintOutputAction($"external IPAddress: {ip}");
        }

        private async Task GetAutoDisconnectTimeSpan()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var autoDisconnectSettings = await this._client.GetAutoDisconnectTimeSpanAsync();
            this.PrintObject(autoDisconnectSettings);
        }

        private async Task GetConnectionTypeInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = await this._client.GetConnectionTypeInfoAsync();
            this.PrintObject(info);
        }

        private async Task GetLinkLayerMaxBitrates()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = await this._client.GetLinkLayerMaxBitRatesAsync();
            this.PrintObject(info);
        }

        private async Task GetNATRSIPStatus()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var status = await this._client.GetNATRSIPStatusAsync();
            this.PrintObject(status);
        }

        private async Task GetStatusInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var statusInfo = await this._client.GetStatusInfoAsync();
            this.PrintObject(statusInfo);
        }

        private async Task GetPortMappingNumberOfEntries()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var portMappings = await this._client.GetPortMappingNumberOfEntriesAsync();
            this.PrintOutputAction($"Portmappings: {portMappings}");
        }

        private async Task GetGenericPortMappingEntry()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Index: ");
            if (Int32.TryParse(this.GetInputFunc(), out Int32 index))
            {
                var entry = await this._client.GetGenericPortMappingEntryAsync(index);
                this.PrintObject(entry);
            }
            else
                this.PrintOutputAction("Invalid input");
        }

        /// <summary>
        /// Method to get the port mappings
        /// </summary>
        private async Task GetPortMappings()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var entries = await this._client.GetPortMappingEntriesAsync();
            foreach(var entry in entries)
            {
                this.PrintOutputAction($"Entry: {entries.IndexOf(entry)}");
                this.PrintObject(entry);
            }
        }

        private async Task AddPortMapping()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            PortMappingEntry entry = new PortMappingEntry();
            this.PrintOutputAction("Remote host:");
            if (IPAddress.TryParse(this.GetInputFunc(), out IPAddress remote))
                entry.RemoteHost = remote;
            this.PrintOutputAction("External port:");
            entry.ExternalPort = UInt16.TryParse(this.GetInputFunc(), out UInt16 res) ? res : (ushort)1;
            this.PrintOutputAction("Internal host:");
            entry.InternalHost = IPAddress.TryParse(this.GetInputFunc(), out IPAddress internalHost) ? internalHost : IPAddress.None;
            this.PrintOutputAction("internal port:");
            entry.InternalPort = UInt16.TryParse(this.GetInputFunc(), out UInt16 res2) ? res2 : (ushort)1;
            this.PrintOutputAction("Protocol (UDP, TCP, ESP, GRE)");
            entry.PortMappingProtocol = (PortMappingProtocol)Enum.Parse(typeof(PortMappingProtocol), this.GetInputFunc());          
            entry.Enabled = true;
            this.PrintOutputAction("Description:");
            entry.Description = this.GetInputFunc();
            await this._client.AddPortMappingAsync(entry);
            this.PrintOutputAction("Port mapping added");
        }

        private async Task DeletePortMapping()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("RemoteHost host:");
            IPAddress remoteHost = IPAddress.TryParse(this.GetInputFunc(), out IPAddress internalHost) ? internalHost :null;
            this.PrintOutputAction("external port:");
            ushort externalPort = UInt16.TryParse(this.GetInputFunc(), out UInt16 res2) ? res2 : (ushort)1;
            this.PrintOutputAction("Protocol (UDP, TCP, ESP, GRE)");
            var pPortMappingProtocol = (PortMappingProtocol)Enum.Parse(typeof(PortMappingProtocol), this.GetInputFunc());

            await this._client.DeletePortMappingAsync(remoteHost, externalPort, pPortMappingProtocol);
            this.PrintOutputAction("Port mapping deleted");
        }

        private async Task GetSpecificPortMappingEntry()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintOutputAction("RemoteHost host:");
            IPAddress remoteHost = IPAddress.TryParse(this.GetInputFunc(), out IPAddress internalHost) ? internalHost : null;
            this.PrintOutputAction("external port:");
            ushort externalPort = UInt16.TryParse(this.GetInputFunc(), out UInt16 res2) ? res2 : (ushort)1;
            this.PrintOutputAction("Protocol (UDP, TCP, ESP, GRE)");
            var pPortMappingProtocol = (PortMappingProtocol)Enum.Parse(typeof(PortMappingProtocol), this.GetInputFunc());

            var entry = await _client.GetSpecificPortMappingEntryAsync(remoteHost, externalPort, pPortMappingProtocol);
            this.PrintObject(entry);
        }

        private async Task GetUserName()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            var userName = await this._client.GetUserNameAsync();
            this.PrintOutputAction($"UserName: {userName}");
        }

        private async Task SetAutoDisconnectTimeSpan()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            AutoDisconnectTimeSpan span = new AutoDisconnectTimeSpan();
            this.PrintOutputAction("Enable? (y / n)");
            span.PreventionEnable = this.GetInputFunc() == "y";
            this.PrintOutputAction("Hour:");
            span.PreventionHour = ushort.TryParse(this.GetInputFunc(), out ushort hour) ? hour : (ushort)0;

            await this._client.SetAutoDisconnectTimeSpanAsync(span);
            this.PrintOutputAction("AutoDisconnectTime set");
        }
    }
}