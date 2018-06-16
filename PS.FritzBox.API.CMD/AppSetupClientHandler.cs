using System;

namespace PS.FritzBox.API.CMD
{
    public class AppSetupClientHandler : ClientHandler
    {
        AppSetupClient _client;

        public AppSetupClientHandler(ConnectionSettings settings, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(settings, printOutput, getInput, wait, clearOutput)
        {
            _client = new AppSetupClient(settings);
        }

        public override void Handle()
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
                            this.GetInfo();
                            break;
                        case "2":
                            this.GetConfig();
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

        private void GetInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = this._client.GetInfoAsync().GetAwaiter().GetResult();
            this.PrintObject(info);
        }

        private void GetConfig()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var config = this._client.GetConfigAsync().GetAwaiter().GetResult();
            this.PrintObject(config);
        }
    }
}