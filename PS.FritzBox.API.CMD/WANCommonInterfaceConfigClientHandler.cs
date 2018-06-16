using System;

namespace PS.FritzBox.API.CMD
{
    public class WANCommonInterfaceConfigClientHandler : ClientHandler
    {
        WANDevice.WANCommonInterfaceConfigClient _client;

        public WANCommonInterfaceConfigClientHandler(ConnectionSettings settings, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(settings, printOutput, getInput, wait, clearOutput)
        {
            this._client = new WANDevice.WANCommonInterfaceConfigClient(settings);
        }

        public override void Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"WANCommonInterfaceConfig{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetOnlineMonitor");
                this.PrintOutputAction("2 - GetCommonLinkProperties");
                this.PrintOutputAction("r - Return");

                input = this.GetInputFunc();

                try
                {
                    switch (input)
                    {
                        case "1":
                            this.GetOnlineMonitor();
                            break;
                        case "2":
                            this.GetCommonLinkProperties();
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

        private void GetOnlineMonitor()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Groupindex: ");
            if (!UInt16.TryParse(this.GetInputFunc(), out UInt16 groupIndex))
                this.PrintOutputAction("Invalid group index");
            else
            {
                var monitor = _client.GetOnlineMonitorAsync(groupIndex).GetAwaiter().GetResult();
                this.PrintObject(monitor);
            }
        }

        private void GetCommonLinkProperties()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            var props = this._client.GetCommonLinkPropertiesAsync().GetAwaiter().GetResult();
            this.PrintObject(props);
        }
    }
}