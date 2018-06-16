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

        public DeviceInfoClientHandler(ConnectionSettings settings, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(settings, printOutput, getInput, wait, clearOutput)
        {
            this._client = new DeviceInfoClient(settings);
        }

        /// <summary>
        /// Method to handle
        /// </summary>
        public override void Handle()
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
                            this.GetDeviceInfo();
                            break;
                        case "2":
                            this.GetLog();
                            break;
                        case "3":
                            this.GetSecurityPort();
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
        private void GetDeviceInfo()
        {
            
            this.ClearOutputAction();
            base.PrintEntry();
            DeviceInfo info = this._client.GetDeviceInfoAsync().GetAwaiter().GetResult();
            base.PrintObject(info);
        }

        /// <summary>
        /// Method to get the device log
        /// </summary>
        private void GetLog()
        {
            this.ClearOutputAction();
            base.PrintEntry();

            var log = this._client.GetDeviceLogAsync().GetAwaiter().GetResult();
            foreach (var entry in log)
                this.PrintOutputAction(entry);
        }

        /// <summary>
        /// Method to get the security port
        /// </summary>
        private void GetSecurityPort()
        {
            this.ClearOutputAction();            
            base.PrintEntry();
            UInt16 secPort = this._client.GetSecurityPortAsync().GetAwaiter().GetResult();
            this.PrintOutputAction($"SecurityPort: {secPort}");
        }
    }
}
