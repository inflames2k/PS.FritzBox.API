using System;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class WANCommonInterfaceConfigClientHandler : ClientHandler
    {
        WANDevice.WANCommonInterfaceConfigClient _client;

        public WANCommonInterfaceConfigClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<WANDevice.WANCommonInterfaceConfigClient>();
        }

        public override async Task Handle()
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
                            await this.GetOnlineMonitor();
                            break;
                        case "2":
                            await this.GetCommonLinkProperties();
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

        private async Task GetOnlineMonitor()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Groupindex: ");
            if (!UInt16.TryParse(this.GetInputFunc(), out UInt16 groupIndex))
                this.PrintOutputAction("Invalid group index");
            else
            {
                var monitor = await _client.GetOnlineMonitorAsync(groupIndex);
                this.PrintObject(monitor);
            }
        }

        private async Task GetCommonLinkProperties()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            var props = await this._client.GetCommonLinkPropertiesAsync();
            this.PrintObject(props);
        }
    }
}