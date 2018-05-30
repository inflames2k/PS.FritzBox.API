using System;

namespace PS.FritzBox.API.CMD
{
    /// <summary>
    /// handler for layer3 forwarding client
    /// </summary>
    internal class Layer3ForwardingClientHandler : ClientHandler
    {

        Layer3ForwardingClient _client;
        public Layer3ForwardingClientHandler(ConnectionSettings settings, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(settings, printOutput, getInput, wait, clearOutput)
        {
            _client = new Layer3ForwardingClient(settings);
        }

        public override void Handle()
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
                            this.GetDefaultConnectionService();
                            break;
                        case "2":
                            this.SetDefaultConnectionService();
                            break;
                        case "3":
                            this.GetForwardNumberOfEntries();
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

        private void GetDefaultConnectionService()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            var service = this._client.GetDefaultConnectionServiceAsync().GetAwaiter().GetResult();
            this.PrintOutputAction($"DefaultConnectionService: {service}");
        }

        private void SetDefaultConnectionService()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Connection Service:");
            this._client.SetDefaultConnectionServiceAsync(this.GetInputFunc()).GetAwaiter().GetResult();
            this.PrintOutputAction("Default connection service set");
        }

        private void GetForwardNumberOfEntries()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var num = this._client.GetForwardNumberOfEntriesAsync().GetAwaiter().GetResult();
            this.PrintOutputAction($"Forward entries: {num}");
        }
    }
}