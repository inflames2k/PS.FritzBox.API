using System;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    internal class UserInterfaceClientHandler : ClientHandler
    {
        UserInterfaceClient _client;
        public UserInterfaceClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<UserInterfaceClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"UserInterfaceClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetInfo");
                this.PrintOutputAction("2 - GetUpdateInfo");
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
                            await this.GetUpdateInfo();
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

        private async Task GetUpdateInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = await this._client.GetUpdateInfoAsync();

            this.PrintObject(info);

        }
    }
}