using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS.FritzBox.API;

namespace PS.FritzBox.API.CMD
{
    public class WANDSLInterfaceConfigClientHandler : ClientHandler
    {
        WANDevice.WANDSLInterfaceConfigClient _client;

        public WANDSLInterfaceConfigClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<WANDevice.WANDSLInterfaceConfigClient>();
            
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"WANDSLInterfaceConfigClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetInfo");
                this.PrintOutputAction("2 - GetStatisticsTotal");
                this.PrintOutputAction("3 - GetDSLDiagnoseInfo");
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
                           await this.GetStatisticsTotal();
                            break;
                        case "3":
                            await this.GetDSLDiagnoseInfo();
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

        private async Task GetStatisticsTotal()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = await this._client.GetStatisticsTotalAsync();
            this.PrintObject(info);
        }

        private async Task GetDSLDiagnoseInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = await this._client.GetDSLDiagnoseInfoAsync();
            this.PrintObject(info);
        }
    }
}
