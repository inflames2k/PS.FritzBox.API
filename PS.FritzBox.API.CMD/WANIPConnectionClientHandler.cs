using PS.FritzBox.API.WANDevice.WANConnectionDevice;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class WANIPConnectonClientHandler : ClientHandler
    {
        WANIPConnectionClient _client;

        public WANIPConnectonClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            _client = device.GetServiceClient<WANIPConnectionClient>();
        }

        public override async Task Handle()
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
                            await this.ForceTermination();
                            break;
                        case "2":
                            await this.RequestTermination();
                            break;
                        case "3":
                            await this.RequestConnection();
                            break;
                        case "4":
                            await this.GetStatusInfo();
                            break;
                        case "5":
                            await this.GetDNSServers();
                            break;
                        case "6":
                            await this.GetExternalIPAddress();
                            break;
                        case "7":
                            await this.GetConnectionTypeInfo();
                            break;
                        case "8":
                            await this.GetNATRSIPStatus();
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

        private async Task RequestTermination()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            await this._client.RequestTerminationAsync();
            this.PrintOutputAction("Termination forced");
        }

        private async Task RequestConnection()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            await this._client.RequestConnectionAsync();
            this.PrintOutputAction("connection requested");
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
              
        private async Task GetConnectionTypeInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = await this._client.GetConnectionTypeInfoAsync();
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
    }
}