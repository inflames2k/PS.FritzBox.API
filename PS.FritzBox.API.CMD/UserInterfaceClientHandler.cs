using System;

namespace PS.FritzBox.API.CMD
{
    internal class UserInterfaceClientHandler : ClientHandler
    {
        UserInterfaceClient _client;
        public UserInterfaceClientHandler(ConnectionSettings settings, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(settings, printOutput, getInput, wait, clearOutput)
        {
            this._client = new UserInterfaceClient(settings);
        }

        public override void Handle()
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
                            this.GetInfo();
                            break;
                        case "2":
                            this.GetUpdateInfo();
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

        private void GetUpdateInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = this._client.GetUpdateInfoAsync().GetAwaiter().GetResult();

            this.PrintObject(info);

        }
    }
}