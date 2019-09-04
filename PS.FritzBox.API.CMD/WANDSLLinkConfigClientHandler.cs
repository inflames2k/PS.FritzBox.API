using System;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class WANDSLLinkConfigClientHandler : ClientHandler
    {
        WANDevice.WANConnectionDevice.WANDSLLinkConfigClient _client;
        private string _configSID;

        public WANDSLLinkConfigClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<WANDevice.WANConnectionDevice.WANDSLLinkConfigClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"WANDSLLinkConfigClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetInfo");
                this.PrintOutputAction("2 - SetEnable");
                this.PrintOutputAction("3 - GetDSLLinkInfo");
                this.PrintOutputAction("4 - SetDestinationAddress");
                this.PrintOutputAction("5 - GetDestinationAddress");
                this.PrintOutputAction("6 - GetATMEncapsulation");
                this.PrintOutputAction("7 - GetAutoConfig");
                this.PrintOutputAction("8 - GetStatistics");

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
                            await this.SetEnable();
                            break;
                        case "3":
                            await this.GetDSLLinkInfo();
                            break;
                        case "4":
                            await this.SetDestinationAddress();
                            break;
                        case "5":
                            await this.GetDestinationAddress();
                            break;
                        case "6":
                            await this.GetATMEncapsulation();
                            break;
                        case "7":
                            await this.GetAutoConfig();
                            break;
                        case "8":
                            await this.GetStatistics();
                            break;
                        case "r":
                            break;
                        default:
                            this.PrintOutputAction("invalid choice");
                            break;
                    }

                    if(input != "r")
                        this.WaitAction();
                }
                catch (Exception ex)
                {
                    this.PrintOutputAction(ex.ToString());
                    this.WaitAction();
                }

            } while (input != "r");
        }

        private async Task GetAutoConfig()
        {
            this.PrintEntry();
            this.ClearOutputAction();

            this.PrintOutputAction($"Auto Config: {await this._client.GetAutoConfigAsync()}");
        }

        private async Task GetDestinationAddress()
        {
            this.PrintEntry();
            this.ClearOutputAction();

            this.PrintOutputAction($"Destination Address: {await this._client.GetDestinationAddressAsync()}");
        }

        private async Task GetATMEncapsulation()
        {
            this.PrintEntry();
            this.ClearOutputAction();

            this.PrintOutputAction($"ATM Encapsulation: {await this._client.GetATMEncapsulationAsync()}");
        }

        private async Task SetDestinationAddress()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("New Destination address?");
            var address = this.GetInputFunc();
            await this._client.SetDestinationAddressAsync(address);
            this.PrintOutputAction("Changed setting for destination address");
        }

        private async Task GetDSLLinkInfo()
        {
            this.PrintEntry();
            this.ClearOutputAction();

            this.PrintObject(await this._client.GetDSLLinkInfoAsync());
        }

        private async Task GetStatistics()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var statistics = await this._client.GetStatisticsAsync();
            this.PrintObject(statistics);
        }

        private async Task SetEnable()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Enable? (1/0)");
            var enable = this.GetInputFunc() == "1";
            await this._client.SetEnableAsync(enable);
            this.PrintOutputAction("Changed setting for enabled state");
        }

        private async Task GetInfo()
        {
            this.PrintEntry();
            this.ClearOutputAction();

            this.PrintObject(await this._client.GetInfoAsync());
        }
    }

}