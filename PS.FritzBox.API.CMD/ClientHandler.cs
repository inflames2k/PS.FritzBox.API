using PS.FritzBox.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PS.FritzBox.API.CMD
{
    public abstract class ClientHandler
    {
        /// <summary>
        /// action for print output
        /// </summary>
        protected Action<string> PrintOutputAction;

        /// <summary>
        /// function for get input
        /// </summary>
        protected Func<string> GetInputFunc;

        /// <summary>
        /// action for waiting
        /// </summary>
        protected Action WaitAction;

        /// <summary>
        /// action for clearing the output
        /// </summary>
        protected Action ClearOutputAction;

        public ClientHandler(FritzDevice device, Action<string> printOutput, Func<string> getInput, Action wait, Action clearOutput)
        {
            this.PrintOutputAction = printOutput;
            this.GetInputFunc = getInput;
            this.WaitAction = wait;
            this.ClearOutputAction = clearOutput;
        }

        public abstract Task Handle();

        /// <summary>
        /// Method to print an object
        /// </summary>
        /// <param name="data"></param>
        protected void PrintObject(Object data)
        {
            var properties = data.GetType().GetProperties();

            foreach (var property in properties)
            {
                string value = string.Empty;
                Object oValue = property.GetValue(data);

                if (oValue != null && oValue is IList && oValue.GetType().IsGenericType)
                {
                    IList oData = (IList)property.GetValue(data) as IList;
                    this.PrintOutputAction($"{property.Name}:");
                    foreach (var entry in oData)
                    {
                        if (entry != null && entry.GetType().IsClass && !(entry is IPAddress) && !(entry is string))
                        {
                            this.PrintObject(entry);
                        }
                        else
                            this.PrintOutputAction($"{entry.ToString()}");
                    }
                }
                else if(oValue != null && oValue.GetType().IsClass && !(oValue is IPAddress) && !(oValue is string))
                {
                    this.PrintOutputAction($"{property.Name}:");
                    this.PrintObject(oValue);
                }
                else
                    this.PrintOutputAction($"{property?.Name}: {oValue?.ToString()}");
            }
        }

        protected void PrintEntry([CallerMemberName] string entry="")
        {
            this.PrintOutputAction($"{entry}{Environment.NewLine}#############################");
        }
    }
}
