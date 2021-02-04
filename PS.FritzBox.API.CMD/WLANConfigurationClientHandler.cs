using PS.FritzBox.API.LANDevice;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    internal class WLANConfigurationClientHandler : ClientHandler
    {
        WLANConfigurationClient _client;
        public WLANConfigurationClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(device, printOutput, getInput, wait, clearOutput)
        {
            this._client = device.GetServiceClient<WLANConfigurationClient>();
        }

        public override async Task Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"WLANConfigurationClient{Environment.NewLine}########################");
                this.PrintOutputAction(" 1 - GetInfo");
                this.PrintOutputAction(" 2 - GetSecurityKeys");
                this.PrintOutputAction(" 3 - GetDefaultWEPKeyIndex");
                this.PrintOutputAction(" 4 - SetDefaultWEPKeyIndexAsync");
                this.PrintOutputAction(" 5 - GetBSSID");
                this.PrintOutputAction(" 6 - GetSSID");
                this.PrintOutputAction(" 7 - SetSSID");
                this.PrintOutputAction(" 8 - GetBeaconType");
                this.PrintOutputAction(" 9 - GetChannelInfo");
                this.PrintOutputAction("10 - SetChannel");
                this.PrintOutputAction("11 - GetTotalAssociations");
                this.PrintOutputAction("12 - GetAssociatedDevices");
                this.PrintOutputAction("13 - GetIPTVOptimized");
                this.PrintOutputAction("14 - GetNightControl");
                this.PrintOutputAction("15 - GetWPSInfo");
                this.PrintOutputAction("16 - GetStatistics");
                this.PrintOutputAction("17 - GetPacketStatistics");

                this.PrintOutputAction("r - Return");

                input = this.GetInputFunc();

                try
                {
                    switch (input)
                    {
                        case "1":
                            await this.GetInfo();
                            break;
                        case "2":
                            await this.GetSecurityKeys();
                            break;
                        case "3":
                            await this.GetDefaultWEPKeyIndex();
                            break;
                        case "4":
                            await this.SetDefaultWEPKeyIndex();
                            break;
                        case "5":
                            await this.GetBSSID();
                            break;
                        case "6":
                            await this.GetSSID();
                            break;
                        case "7":
                            await this.SetSSID();
                            break;
                        case "8":
                            await this.GetBeaconType();
                            break;
                        case "9":
                            await this.GetChannelInfo();
                            break;
                        case "10":
                            await this.SetChannel();
                            break;
                        case "11":
                            await this.GetTotalAssociations();
                            break;
                        case "12":
                            await this.GetAssociatedDevices();
                            break;
                        case "13":
                            await this.GetIPTVOptimized();
                            break;
                        case "14":
                            await this.GetNightControl();
                            break;
                        case "15":
                            await this.GetWPSInfo();
                            break;
                        case "16":
                            await this.GetStatistics();
                            break;
                        case "17":
                            await this.GetPacketStatistics();
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

        private async Task GetWPSInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            var info = await this._client.GetWPSInfoAsync();
            this.PrintObject(info);
        }

        private async Task GetNightControl()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            var info = await this._client.GetNightControlAsync();
            this.PrintObject(info);
        }

        private async Task GetAssociatedDevices()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            foreach(WLANDeviceInfo info in await this._client.GetAssociatedDevicesAsync())
            {
                Console.WriteLine("###########################");
                this.PrintObject(info);
            }
        }

        private async Task GetTotalAssociations()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintOutputAction($"Total associations: {await this._client.GetTotalAssociationsAsync()}");
        }

        private async Task GetBeaconType()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintOutputAction($"Beacon Type: {await this._client.GetBeaconTypeAsync()}");
        }

        private async Task GetChannelInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintObject(await this._client.GetChannelInfoAsync());
        }

        private async Task GetInfo()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            var info = await this._client.GetInfoAsync();
            this.PrintObject(info);
        }

        private async Task GetSecurityKeys()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            var info = await this._client.GetSecurityKeysAsync();
            this.PrintObject(info);
        }

        private async Task GetDefaultWEPKeyIndex()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintOutputAction($"Default WEP Key index: {await this._client.GetDefaultWEPKeyIndexAsync()}");
        }

        private async Task SetDefaultWEPKeyIndex()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("New index:");
            if (UInt16.TryParse(this.GetInputFunc(), out UInt16 result))
            {
                await this._client.SetDefaultWEPKeyIndexAsync(result);
                this.PrintOutputAction("New index set");
            }
            else
                this.PrintOutputAction("Invalid input");
        }

        private async Task GetBSSID()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintOutputAction($"BSSID: {await this._client.GetBSSIDAsync()}");
        }

        private async Task GetSSID()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintOutputAction($"BSSID: {await this._client.GetSSIDAsync()}");
        }

        private async Task SetSSID()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            this.PrintOutputAction("New SSID:");
            await this._client.SetSSIDAsync(this.GetInputFunc());
            this.PrintOutputAction("New SSID set");
        }

        private async Task SetChannel()
        {
            this.ClearOutputAction();
            this.PrintEntry();
            var channelInfo = await this._client.GetChannelInfoAsync();
            this.PrintOutputAction("New Channel:");
            if (UInt16.TryParse(this.GetInputFunc(), out UInt16 channel) && channelInfo.PossibleChannels.Max() >= channel)
            {
                await this._client.SetChannelAsync(channel);
                this.PrintOutputAction("channel set");
            }
            else
                this.PrintOutputAction("invalid input");
        }

        private async Task GetIPTVOptimized()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintOutputAction($"IP TV Optimized: {await this._client.GetIPTVOptimizedAsync()}");
        }

        private async Task GetStatistics()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintOutputAction($"Statistics:");
            this.PrintObject(await this._client.GetStatisticsAsync());
        }

        private async Task GetPacketStatistics()
        {
            this.ClearOutputAction();
            this.PrintEntry();

            this.PrintOutputAction($"Statistics:");
            this.PrintObject(await this._client.GetPacketStatisticsAsync());
        }
    }
}