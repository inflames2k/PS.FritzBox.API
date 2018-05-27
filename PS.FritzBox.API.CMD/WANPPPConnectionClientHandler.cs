using PS.FritzBox.API.WANDevice.WANConnectionDevice;
using System;

namespace PS.FritzBox.API.CMD
{
    public class WANPPPConnectionClientHandler : ClientHandler
    {
        WANPPPConnectionClient _client;
        public WANPPPConnectionClientHandler(ConnectionSettings settings, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput) : base(settings, printOutput, getInput, wait, clearOutput)
        {
            _client = new WANPPPConnectionClient(settings);
        }

        public override void Handle()
        {
            string input = string.Empty;

            do
            {
                this.ClearOutputAction();
                this.PrintOutputAction($"DeviceInfoClient{Environment.NewLine}########################");
                this.PrintOutputAction("1 - ForceTermination");
                this.PrintOutputAction("2 - RequestConnetion");
                this.PrintOutputAction("3 - GetInfo");
                this.PrintOutputAction("4 - GetDNSServers");
                this.PrintOutputAction("5 - GetExternalIPAddress");
                this.PrintOutputAction("6 - GetAutoDisconnectTimeSpan");
                this.PrintOutputAction("7 - GetConnectionTypeInfo");
                this.PrintOutputAction("8 - GetLinkLayerMaxBitrates");
                this.PrintOutputAction("9 - GetNATRSIPStatus");
                this.PrintOutputAction("10 - GetStatusInfo");
                this.PrintOutputAction("11 - GetPortMappingNumberOfEntries");
                this.PrintOutputAction("12 - GetGenericPortMappingEntry");
                this.PrintOutputAction("13 - GetSpecificPortMappingEntry");
                this.PrintOutputAction("14 - GetUserName");
                this.PrintOutputAction("15 - SetAutoDisconnectTimeSpan");
                this.PrintOutputAction("16 - SetConnectionTrigger");
                this.PrintOutputAction("17 - SetConnectionType");
                this.PrintOutputAction("18 - SetIdleDisconnectTime");
                this.PrintOutputAction("19 - SetUserName");
                this.PrintOutputAction("20 - SetPassword");
                this.PrintOutputAction("r - Return");

                input = this.GetInputFunc();

                try
                {
                    switch (input)
                    {

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
    }
}