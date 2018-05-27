using PS.FritzBox.API.SOAP;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API.Base
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
            this.ConnectionSettings.BaseUrl = url;
            this.ConnectionSettings.Timeout = timeout;
        }

        /// <summary>
        /// constructor for the tr064 service
        /// </summary>
        /// <param name="url">the base url</param>
        /// <param name="timeout">the timeout in milliseconds</param>
        /// <param name="username">the connection user name</param>
        public FritzTR64Client(string url, int timeout, string username) : this(url, timeout)
        {
            this.ConnectionSettings.UserName = username;
        }

        /// <summary>
        /// constructor for the tr064 service
        /// </summary>
        /// <param name="url">the base url</param>
        /// <param name="timeout">the timeout in milliseconds</param>
        /// <param name="username">the connection user name</param>
        /// <param name="password">the connection password</param>
        public FritzTR64Client(string url, int timeout, string username, string password) : this(url, timeout, username)
        {
            this.ConnectionSettings.Password = password;
        }

        /// <summary>
        /// Constructor for tr064 service
        /// </summary>
        /// <param name="connectionSettings">the connection settings</param>
        public FritzTR64Client(ConnectionSettings connectionSettings)
        {
            this.ConnectionSettings = connectionSettings;
        }

        #endregion

        /// <summary>
        /// Gets or sets the connection settings
        /// </summary>
        public ConnectionSettings ConnectionSettings { get; set; } = new ConnectionSettings();

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
        /// <param name="action">soap action to execute</param>
        /// <param name="parameter">soap request parameters</param>
        /// <returns>the resulting xml document</returns>
        internal async Task<XDocument> InvokeAsync(string action, params SoapRequestParameter[] parameter)
        {
            SoapClient client = new SoapClient();

            SoapRequestParameters parameters = new SoapRequestParameters();

            parameters.UserName = this.ConnectionSettings.UserName;
            parameters.Password = this.ConnectionSettings.Password;
            parameters.Timeout = this.ConnectionSettings.Timeout;

            parameters.RequestNameSpace = this.RequestNameSpace;
            parameters.SoapAction = $"{this.RequestNameSpace}#{action}";
            parameters.Action = $"{action}";
            if (parameter != null)
                parameters.Parameters.AddRange(parameter);

            XDocument soapResult = await client.InvokeAsync($"{this.ConnectionSettings.BaseUrl}{this.ControlUrl}", parameters);

            this.ParseSoapFault(soapResult);

            return soapResult;
        }

        /// <summary>
        /// Method to parse the soap fault
        /// </summary>
        /// <param name="document">the result of the soap request</param>
        internal void ParseSoapFault(XDocument document)
        {            
            if(document.Descendants("faultcode").Count() > 0)
            {
                string code = document.Descendants("faultcode").First().Value;
                string text = document.Descendants("faultstring").First().Value;

                string upnpErrorText = string.Empty;
                if (document.Descendants("detail").Count() > 0)
                {
                    XElement detailElement = document.Descendants("detail").First();
                    XElement upnpError = (XElement)detailElement.FirstNode;
                   // XElement upnpError = detailElement.Element(new XName("UPnPError").FirstOrDefault();
                    foreach (XElement element in upnpError != null ? upnpError.Elements() : detailElement.Elements())
                    {
                        upnpErrorText += $"{element.Name}: {element.Value}{Environment.NewLine}";
                    }
                }
                throw new SoapFaultException(code, text, upnpErrorText);
            }
        }
    }
}
