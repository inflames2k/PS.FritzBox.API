using PS.FritzBox.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class DeviceInfoClientHandler : ClientHandler
    {
        DeviceInfoClient _client;

        public DeviceInfoClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<DeviceInfoClient>(); // new DeviceInfoClient(settings);
        }

        /// <summary>
        /// Method to handle
        /// </summary>
        public override async Task Handle()
        {
            string input = string.Empty;
            
            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"DeviceInfoClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - GetInfo");
                this.PrintOutputAction("2 - GetDeviceLog");
                this.PrintOutputAction("3 - GetSecurityPort");
                this.PrintOutputAction("r - Return");

                input = this.GetInputFunc();

                try
                {
                    switch (input)
                    {
                        case "1":
                            await this.GetDeviceInfo();
                            break;
                        case "2":
                            await this.GetLog();
                            break;
                        case "3":
                            await this.GetSecurityPort();
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
        /// Method to get and print device info
        /// </summary>
        private async Task GetDeviceInfo()
        {
            
            this.ClearOutputAction();
            base.PrintEntry();
            DeviceInfo info = await this._client.GetDeviceInfoAsync();
            base.PrintObject(info);
        }

        /// <summary>
        /// Method to get the device log
        /// </summary>
        private async Task GetLog()
        {
            this.ClearOutputAction();
            base.PrintEntry();

            var log = await this._client.GetDeviceLogAsync();
            foreach (var entry in log)
                this.PrintOutputAction(entry);
        }

        /// <summary>
        /// Method to get the security port
        /// </summary>
        private async Task GetSecurityPort()
        {
            this.ClearOutputAction();            
            base.PrintEntry();
            UInt16 secPort = await this._client.GetSecurityPortAsync();
            this.PrintOutputAction($"SecurityPort: {secPort}");
        }
    }
}
