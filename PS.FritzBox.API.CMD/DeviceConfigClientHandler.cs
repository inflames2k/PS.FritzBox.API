using System;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public class DeviceConfigClientHandler : ClientHandler
    {
        DeviceConfigClient _client;
        private string _configSID;

        public DeviceConfigClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<DeviceConfigClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"DeviceConfigClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - FactoryReset");
                this.PrintOutputAction("2 - Reboot");
                this.PrintOutputAction("3 - StartConfiguration");
                this.PrintOutputAction("4 - FinishConfiguration");
                this.PrintOutputAction("5 - GetConfigFile");
                this.PrintOutputAction("6 - DownloadConfigFile");
                this.PrintOutputAction("7 - GenerateURLSID");
                this.PrintOutputAction("8 - GetPersistentData");
                this.PrintOutputAction("9 - SetPersistentData");
               
                this.PrintOutputAction("r - Return");

                input = this.GetInputFunc();

                try
                {
                    switch (input)
                    {
                        case "1":
                            await this.FactoryReset();
                            break;
                        case "2":
                            await this.Reboot();
                            break;
                        case "3":
                            await this.StartConfiguration();
                            break;
                        case "4":
                            await this.FinishConfiguration();
                            break;
                        case "5":
                            await this.GetConfigFIle();
                            break;
                        case "6":
                            await this.DownloadConfigFile();
                            break;
                        case "7":
                            await this.GenerateUrlSID();
                            break;
                        case "8":
                            await this.GetPersistentData();
                            break;
                        case "9":
                            await this.SetPersistentData();
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
        private async Task FactoryReset()
        {
            this.PrintEntry();
            this.ClearOutputAction();
            this.PrintOutputAction("Are you sure to reset? (y/n)");
            string result = this.GetInputFunc();

            if (result == "y")
                await _client.FactoryResetAsync();
            else
                this.PrintOutputAction("Reset aborted");
        }

        /// <summary>
        /// Method to reboot the device
        /// </summary>
        private async Task Reboot()
        {

            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Are you sure to reboot? (y/n)");
            string result = this.GetInputFunc();

            if (result == "y")
               await _client.RebootAsync();
            else
                this.PrintOutputAction("Reboot aborted");
        }

        /// <summary>
        /// Method to generate uuid
        /// </summary>
        private async Task GenerateUUID()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this._configSID = await _client.GenerateUUIDAsync();
            this.PrintOutputAction($"UUID: {this._configSID}");
        }

        private async Task StartConfiguration()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            await this.GenerateUUID();
            await this._client.StartConfigurationAsync(this._configSID);
        }

        private async Task FinishConfiguration()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            string result = await this._client.FinishConfigurationAsync();
            this.PrintOutputAction($"Configuration result: {result}");
        }

        private async Task GetConfigFIle()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Password: ");          
            string result = await this._client.GetConfigFileAsync(this.GetInputFunc());
            this.PrintOutputAction($"Configfile: {result}");
        }

        private async Task DownloadConfigFile()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("Targetpath: ");
            string target = this.GetInputFunc();
            this.PrintOutputAction("Password: ");
            await this._client.DownloadConfigFileAsync(this.GetInputFunc(), target);
            this.PrintOutputAction("Config file downloaded");
        }

        private async Task GenerateUrlSID()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            string urlSID = await this._client.GenerateUrlSIDAsync();
            this.PrintOutputAction($"UrlSID: {urlSID}");
        }

        private async Task GetPersistentData()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            string persistentData = await this._client.GetPersistentDataAsync();
            this.PrintOutputAction($"Persistent Data: {persistentData}");
        }

        private async Task SetPersistentData()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("New persistent data: ");
            await this._client.SetPersistentDataAsync(this.GetInputFunc());
            this.PrintOutputAction("Persistent data written.");
        }
    }

}