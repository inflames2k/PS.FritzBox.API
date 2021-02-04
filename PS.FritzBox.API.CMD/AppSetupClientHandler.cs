using System;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class AppSetupClientHandler : ClientHandler
    {
        AppSetupClient _client;

        public AppSetupClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            _client = device.GetServiceClient<AppSetupClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;
            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"AppSetupClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetInfo");
                this.PrintOutputAction("2 - GetConfig");
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
                            await this.GetConfig();
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

        private async Task GetConfig()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var config = await this._client.GetConfigAsync();
            this.PrintObject(config);
        }
    }
}