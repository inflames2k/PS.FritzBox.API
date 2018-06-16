using PS.FritzBox.API.WANDevice.WANConnectionDevice;
using System;
using System.Net;

namespace PS.FritzBox.API.CMD
{
    public class WANIPConnectonClientHandler : ClientHandler
    {
        WANIPConnectionClient _client;

        public WANIPConnectonClientHandler(ConnectionSettings settings, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(settings, printOutput, getInput, wait, clearOutput)
        {
            _client = new WANIPConnectionClient(settings);
        }

        public override void Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"WANIPConnectionClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - ForceTermination");
                this.PrintOutputAction("2 - RequestTermination");
                this.PrintOutputAction("3 - RequestConnetion");
                this.PrintOutputAction("4 - GetStatusInfo");
                this.PrintOutputAction("5 - GetDNSServers");
                this.PrintOutputAction("6 - GetExternalIPAddress");
                this.PrintOutputAction("7 - GetConnectionTypeInfo");
                this.PrintOutputAction("8 - GetNATRSIPStatus");

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
                            this.RequestTermination();
                            break;
                        case "3":
                            this.RequestConnection();
                            break;
                        case "4":
                            this.GetStatusInfo();
                            break;
                        case "5":
                            this.GetDNSServers();
                            break;
                        case "6":
                            this.GetExternalIPAddress();
                            break;
                        case "7":
                            this.GetConnectionTypeInfo();
                            break;
                        case "8":
                            this.GetNATRSIPStatus();
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

        private void RequestTermination()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this._client.RequestTerminationAsync().GetAwaiter().GetResult();
            this.PrintOutputAction("Termination forced");
        }

        private void RequestConnection()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this._client.RequestConnectionAsync().GetAwaiter().GetResult();
            this.PrintOutputAction("connection requested");
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
              
        private void GetConnectionTypeInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = this._client.GetConnectionTypeInfoAsync().GetAwaiter().GetResult();
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
    }
}