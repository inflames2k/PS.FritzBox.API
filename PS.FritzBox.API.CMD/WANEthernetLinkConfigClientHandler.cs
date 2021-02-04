using System;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class WANEthernetLinkConfigClientHandler : ClientHandler
    {
        WANDevice.WANConnectionDevice.WANEthernetLinkConfigClient _client;
        private string _configSID;

        public WANEthernetLinkConfigClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<WANDevice.WANConnectionDevice.WANEthernetLinkConfigClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"WANEthernetLinkConfigClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetEthernetLinkStatus");
               
                this.PrintOutputAction("r - Return");

                input = this.GetInputFunc();

                try
                {
                    switch (input)
                    {
                        case "1":
                            await this.GetEthernetLinkStatus();
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

        /// <summary>
        /// Method to do a factory reset
        /// </summary>
        private async Task GetEthernetLinkStatus()
        {
            this.PrintEntry();
            this.ClearOutputAction();

            this.PrintOutputAction($"Status: {await _client.GetEthernetLinkStatusAsync()}");
        }
    }

}