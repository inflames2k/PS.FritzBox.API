using PS.FritzBox.API.WANDevice.WANConnectionDevice;
using System;
using System.Net;

namespace PS.FritzBox.API.CMD
{
    public class WANPPPConnectionClientHandler : ClientHandler
    {
        WANPPPConnectionClient _client;

        public WANPPPConnectionClientHandler(ConnectionSettings settings, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(settings, printOutput, getInput, wait, clearOutput)
        {
            _client = new WANPPPConnectionClient(settings);
        }

        public override void Handle()
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
                            this.ForceTermination();
                            break;
                        case "2":
                            this.RequestConnection();
                            break;
                        case "3":
                            this.GetInfo();
                            break;
                        case "4":
                            this.GetDNSServers();
                            break;
                        case "5":
                            this.GetExternalIPAddress();
                            break;
                        case "6":
                            this.GetAutoDisconnectTimeSpan();
                            break;
                        case "7":
                            this.GetConnectionTypeInfo();
                            break;
                        case "8":
                            this.GetLinkLayerMaxBitrates();
                            break;
                        case "9":
                            this.GetNATRSIPStatus();
                            break;
                        case "10":
                            this.GetStatusInfo();                            
                            break;
                        case "11":
                            this.GetPortMappingNumberOfEntries();
                            break;
                        case "12":
                            this.GetGenericPortMappingEntry();
                            break;
                        case "14":
                            this.GetPortMappings();
                            break;
                        case "13":
                            this.GetSpecificPortMappingEntry();
                            break;
                        case "17":
                            this.GetUserName();
                            break;
                        case "18":
                            this.SetAutoDisconnectTimeSpan();
                            break;
                        case "15":
                            this.AddPortMapping();
                            break;
                        case "16":                            
                            this.DeletePortMapping();
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

        private void ForceTermination()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this._client.ForceTerminationAsync().GetAwaiter().GetResult();
            this.PrintOutputAction("Termination forced");
        }

        private void RequestConnection()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this._client.RequestConnectionAsync().GetAwaiter().GetResult();
            this.PrintOutputAction("connection requested");
        }

        private void GetInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = this._client.GetInfoAsync().GetAwaiter().GetResult();
            this.PrintObject(info);
        }

        private void GetDNSServers()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var dnsServers = this._client.GetDNSServersAsync().GetAwaiter().GetResult();
            foreach (var dnsServer in dnsServers)
                this.PrintOutputAction(dnsServer.ToString());
        }

        private void GetExternalIPAddress()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var ip = this._client.GetExternalIPAddressAsync().GetAwaiter().GetResult();
            this.PrintOutputAction($"external IPAddress: {ip}");
        }

        private void GetAutoDisconnectTimeSpan()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var autoDisconnectSettings = this._client.GetAutoDisconnectTimeSpanAsync().GetAwaiter().GetResult();
            this.PrintObject(autoDisconnectSettings);
        }

        private void GetConnectionTypeInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = this._client.GetConnectionTypeInfoAsync().GetAwaiter().GetResult();
            this.PrintObject(info);
        }

        private void GetLinkLayerMaxBitrates()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = this._client.GetLinkLayerMaxBitRatesAsync().GetAwaiter().GetResult();
            this.PrintObject(info);
        }

        private void GetNATRSIPStatus()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var status = this._client.GetNATRSIPStatusAsync().GetAwaiter().GetResult();
            this.PrintObject(status);
        }

        private void GetStatusInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var statusInfo = this._client.GetStatusInfoAsync().GetAwaiter().GetResult();
            this.PrintObject(statusInfo);
        }

        private void GetPortMappingNumberOfEntries()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var portMappings = this._client.GetPortMappingNumberOfEntriesAsync().GetAwaiter().GetResult();
            this.PrintOutputAction($"Portmappings: {portMappings}");
        }

        private void GetGenericPortMappingEntry()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Index: ");
            if (Int32.TryParse(this.GetInputFunc(), out Int32 index))
            {
                var entry = this._client.GetGenericPortMappingEntryAsync(index).GetAwaiter().GetResult();
                this.PrintObject(entry);
            }
            else
                this.PrintOutputAction("Invalid input");
        }

        /// <summary>
        /// Method to get the port mappings
        /// </summary>
        private void GetPortMappings()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var entries = this._client.GetPortMappingEntriesAsync().GetAwaiter().GetResult();
            foreach(var entry in entries)
            {
                this.PrintOutputAction($"Entry: {entries.IndexOf(entry)}");
                this.PrintObject(entry);
            }
        }

        private void AddPortMapping()
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
            this._client.AddPortMappingAsync(entry).GetAwaiter().GetResult();
            this.PrintOutputAction("Port mapping added");
        }

        private void DeletePortMapping()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("RemoteHost host:");
            IPAddress remoteHost = IPAddress.TryParse(this.GetInputFunc(), out IPAddress internalHost) ? internalHost :null;
            this.PrintOutputAction("external port:");
            ushort externalPort = UInt16.TryParse(this.GetInputFunc(), out UInt16 res2) ? res2 : (ushort)1;
            this.PrintOutputAction("Protocol (UDP, TCP, ESP, GRE)");
            var pPortMappingProtocol = (PortMappingProtocol)Enum.Parse(typeof(PortMappingProtocol), this.GetInputFunc());

            this._client.DeletePortMappingAsync(remoteHost, externalPort, pPortMappingProtocol).GetAwaiter().GetResult();
            this.PrintOutputAction("Port mapping deleted");
        }

        private void GetSpecificPortMappingEntry()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintOutputAction("RemoteHost host:");
            IPAddress remoteHost = IPAddress.TryParse(this.GetInputFunc(), out IPAddress internalHost) ? internalHost : null;
            this.PrintOutputAction("external port:");
            ushort externalPort = UInt16.TryParse(this.GetInputFunc(), out UInt16 res2) ? res2 : (ushort)1;
            this.PrintOutputAction("Protocol (UDP, TCP, ESP, GRE)");
            var pPortMappingProtocol = (PortMappingProtocol)Enum.Parse(typeof(PortMappingProtocol), this.GetInputFunc());

            var entry = _client.GetSpecificPortMappingEntryAsync(remoteHost, externalPort, pPortMappingProtocol).GetAwaiter().GetResult();
            this.PrintObject(entry);
        }

        private void GetUserName()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            var userName = this._client.GetUserNameAsync().GetAwaiter().GetResult();
            this.PrintOutputAction($"UserName: {userName}");
        }

        private void SetAutoDisconnectTimeSpan()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            AutoDisconnectTimeSpan span = new AutoDisconnectTimeSpan();
            this.PrintOutputAction("Enable? (y / n)");
            span.PreventionEnable = this.GetInputFunc() == "y";
            this.PrintOutputAction("Hour:");
            span.PreventionHour = ushort.TryParse(this.GetInputFunc(), out ushort hour) ? hour : (ushort)0;

            this._client.SetAutoDisconnectTimeSpanAsync(span).GetAwaiter().GetResult();
            this.PrintOutputAction("AutoDisconnectTime set");
        }
    }
}