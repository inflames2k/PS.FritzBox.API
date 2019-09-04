using System;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class SpeedtestClientHandler : ClientHandler
    {
        SpeedtestClient _client;
        private string _configSID;

        public SpeedtestClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<SpeedtestClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"SpeedtestClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetInfo");
                this.PrintOutputAction("2 - SetConfig");

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
                            this.PrintOutputAction("Not supported yet");
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

        private async Task GetInfo()
        {
            this.PrintEntry();
            this.ClearOutputAction();

            this.PrintObject(await this._client.GetInfoAsync());
        }
    }

}