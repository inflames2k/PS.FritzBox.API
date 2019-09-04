using System;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class LANEthernetInterfaceClientHandler : ClientHandler
    {
        LANDevice.LANEthernetInterfaceClient _client;

        public LANEthernetInterfaceClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<LANDevice.LANEthernetInterfaceClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"LanConfigSecurityHandler{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetInfo");
                this.PrintOutputAction("2 - GetStatistics");
                this.PrintOutputAction("3 - SetEnable");
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
                            await this.GetStatistics();
                            break;
                        case "3":
                            await this.SetEnable();
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
            var info =await  this._client.GetInfoAsync();
            this.PrintObject(info);
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
    }
}