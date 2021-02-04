using System;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    /// <summary>
    /// handler for layer3 forwarding client
    /// </summary>
    internal class Layer3ForwardingClientHandler : ClientHandler
    {

        Layer3ForwardingClient _client;
        public Layer3ForwardingClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            _client = device.GetServiceClient<Layer3ForwardingClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;
            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"Layer3Forwading{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetDefaultConnectionService");
                this.PrintOutputAction("2 - SetDefaultConnectionService");
                this.PrintOutputAction("3 - GetForwardNumberOfEntries");
                this.PrintOutputAction("r - Return");

                input = this.GetInputFunc();

                try
                {
                    switch (input)
                    {
                        case "1":
                            await this.GetDefaultConnectionService();
                            break;
                        case "2":
                            await this.SetDefaultConnectionService();
                            break;
                        case "3":
                            await this.GetForwardNumberOfEntries();
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

        private async Task GetDefaultConnectionService()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            var service = await this._client.GetDefaultConnectionServiceAsync();
            this.PrintOutputAction($"DefaultConnectionService: {service}");
        }

        private async Task SetDefaultConnectionService()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Connection Service:");
            await this._client.SetDefaultConnectionServiceAsync(this.GetInputFunc());
            this.PrintOutputAction("Default connection service set");
        }

        private async Task GetForwardNumberOfEntries()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var num = await this._client.GetForwardNumberOfEntriesAsync();
            this.PrintOutputAction($"Forward entries: {num}");
        }
    }
}