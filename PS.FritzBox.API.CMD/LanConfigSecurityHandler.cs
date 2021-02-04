using System;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class LanConfigSecurityHandler : ClientHandler
    {
        LANConfigSecurityClient _client;

        public LanConfigSecurityHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<LANConfigSecurityClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"LanConfigSecurityHandler{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetAnonymousLogin");
                this.PrintOutputAction("2 - GetCurrentUser");
                this.PrintOutputAction("3 - GetInfo");
                this.PrintOutputAction("4 - SetConfigPassword");
                this.PrintOutputAction("r - Return");

                input = this.GetInputFunc();

                try
                {
                    switch (input)
                    {
                        case "1":
                            await this.GetAnonymousLogin();
                            break;
                        case "2":
                            await this.GetCurrentUser();
                            break;
                        case "3":
                            await this.GetInfo();
                            break;
                        case "4":
                            await this.SetConfigPassword();
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

        private async Task GetAnonymousLogin()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var anonymousLogin = await this._client.GetAnonymousLoginAsync();
            this.PrintOutputAction($"AnonymousLogin: {anonymousLogin}");
        }

        private async Task GetCurrentUser()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var currentUser = await this._client.GetCurrentUserAsync();
            this.PrintObject(currentUser);
        }

        private async Task GetInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var info = await this._client.GetInfoAsync();
            this.PrintObject(info);
        }

        private async Task SetConfigPassword()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("New password:");
            await this._client.SetConfigPasswordAsync(this.GetInputFunc());
            this.PrintOutputAction("Password changed.");
        }
    }
}