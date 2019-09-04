using System;
using System.Net;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    internal class LANHostConfigManagementClientHandler : ClientHandler
    {
        LANDevice.LANHostConfigManagementClient _client;

        public LANHostConfigManagementClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<LANDevice.LANHostConfigManagementClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"LANHostConfigManagement{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetInfo");
                this.PrintOutputAction("2 - GetSubnetMask");
                this.PrintOutputAction("3 - GetIPInterfaceNumberOfEntries");
                this.PrintOutputAction("4 - GetAddressRange");
                this.PrintOutputAction("5 - GetDNSServers");
                this.PrintOutputAction("6 - GetIPRouters");
                this.PrintOutputAction("7 - SetSubnetMask");
                this.PrintOutputAction("8 - SetAddressRange");
                this.PrintOutputAction("9 - SetDHCPServerEnable");

                this.PrintOutputAction("r - Return");
                input = this.GetInputFunc();

                try
                {
                    switch (input)
                    {
                        case "1":
                            await this.GetInfo();
                            break;
                        case "2":
                            await this.GetSubnetMask();
                            break;
                        case "3":
                            await this.GetIPInterfaceNumberOfEntries();
                            break;
                        case "4":
                            await this.GetAddressRange();
                            break;
                        case "5":
                            await this.GetDNSServers();
                            break;
                        case "6":
                            await this.GetIPRouters();
                            break;
                        case "7":
                            await this.SetSubnetMask();
                            break;
                        case "8":
                            await this.SetAddressRange();
                            break;
                        case "9":
                            await this.SetDHCPServerEnable();
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

        private async Task GetInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = await this._client.GetInfoAsync();
            this.PrintObject(info);
        }

        private async Task GetSubnetMask()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var subnetMask = await this._client.GetSubnetMaskAsync();
            this.PrintOutputAction($"Subnetmask: {subnetMask}");
        }

        private async Task GetIPInterfaceNumberOfEntries()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var count = await this._client.GetIPInterfaceNumberOfEntriesAsync();
            this.PrintOutputAction($"Interface count: {count}");
        }

        private async Task GetAddressRange()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var range = await this._client.GetAddressRangeAsync();
            this.PrintObject(range);
        }

        private async Task GetDNSServers()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var servers = await this._client.GetDNSServerAsync();
            foreach (var server in servers)
                this.PrintOutputAction(server.ToString());
        }

        private async Task GetIPRouters()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var servers = await this._client.GetIPRoutersListAsync();
            foreach (var server in servers)
                this.PrintOutputAction(server.ToString());
        }

        private async Task SetSubnetMask()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("New subnetmask:");
            var subnet = this.GetInputFunc();

            if (!IPAddress.TryParse(subnet, out IPAddress address))
            {
                this.PrintOutputAction("invalid subnet mask");
            }
            else
            {
                await this._client.SetSubnetMaskAsync(address);
                this.PrintOutputAction("subnet mask set");
            }
        }

        private async Task SetAddressRange()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Min address:");
            var minAddress = this.GetInputFunc();
            this.PrintOutputAction("Max address:");
            var maxAddress = this.GetInputFunc();

            if (!IPAddress.TryParse(minAddress, out IPAddress min) || !IPAddress.TryParse(maxAddress, out IPAddress max))
            {
                this.PrintOutputAction("One or more addresses are invalid");
            }
            else
            {
                await this._client.SetAddressRangeAsync(min, max);
                this.PrintOutputAction("IP range set");
            }
        }

        private async Task SetDHCPServerEnable()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("enable state? (1/0)");
            var enable = this.GetInputFunc() == "1";

            await this._client.SetDHCPServerEnableAsync(enable);
            this.PrintOutputAction("enabled state changed");
        }
    }
}