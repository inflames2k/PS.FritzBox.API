using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// base class for fritzbox tr64 services
    /// </summary>
    public abstract class FritzTR64Client
    {
        #region Construction / Destruction

        /// <summary>
        /// constructor for the tr64 service
        /// </summary>
        /// <param name="url">the service url</param>
        /// <param name="timeout">the timeout in milliseconds</param>
        public FritzTR64Client(string url, int timeout)
        {
            this.Url = String.Concat(url, this.ControlUrl);
            this.Timeout = timeout;
        }

        #endregion

        /// <summary>
        /// Gets or sets the service url
        /// </summary>
        public string Url { get; internal set; }

        /// <summary>
        /// gets or sets the request timeout
        /// </summary>
        public int Timeout { get; internal set; }

        /// <summary>
        /// Gets or sets the fritz!box user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// gets or sets the fritz!box password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// gets or sets the control url
        /// </summary>
        protected abstract string ControlUrl { get; }

        /// <summary>
        /// Gets or sets the request namespace
        /// </summary>
        protected abstract string RequestNameSpace { get; }

        /// <summary>
        /// Method to invoke the given action
        /// </summary>
        /// <param name="Action"></param>
        /// <returns></returns>
        internal async Task<XDocument> InvokeAsync(string action, params SoapRequestParameter[] parameter)
        {
            SoapClient client = new SoapClient();
            SoapRequestParameters parameters = new SoapRequestParameters();

            parameters.UserName = this.UserName;
            parameters.Password = this.Password;

            parameters.RequestNameSpace = this.RequestNameSpace;
            parameters.SoapAction = $"{this.RequestNameSpace}#{action}";
            parameters.Action = $"{action}";
            if (parameter != null)
                parameters.Parameters.AddRange(parameter);

            XDocument soapResult = await client.InvokeAsync(this.Url, parameters);

            this.ParseSoapFault(soapResult);

            return soapResult;
        }

        /// <summary>
        /// Method to parse the soap fault
        /// </summary>
        /// <param name="document">the result of the soap request</param>
        internal void ParseSoapFault(XDocument document)
        {
            if(document.Descendants("Fault").Count() > 0)
            {
                string code = document.Descendants("faultcode").First().Value;
                string text = document.Descendants("faultstring").First().Value;

                throw new SoapFaultException(code, text);
            }
        }
    }
}
