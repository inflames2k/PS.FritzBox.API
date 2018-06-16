using System;
using System.Collections.Generic;

namespace PS.FritzBox.API.SOAP
{
    /// <summary>
    /// Gets or sets the soap request parameters
    /// </summary>
    internal class SoapRequestParameters
    {
        /// <summary>
        /// Gets or sets the name of the soap action
        /// </summary>
        public string SoapAction { get; set; }

        /// <summary>
        /// Gets or sets the action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the name of the request namespace
        /// </summary>
        public string RequestNameSpace { get; set; }

        /// <summary>
        /// Gets or sets the request parameters
        /// </summary>
        public List<SoapRequestParameter> Parameters { get; set; } = new List<SoapRequestParameter>();

        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the timeout in milliseconds
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Gets or sets the network credentials
        /// </summary>
        public System.Net.ICredentials Credentials { get { return String.IsNullOrEmpty(this.UserName) ? null : new System.Net.NetworkCredential(this.UserName, this.Password); } }

        /// <summary>
        /// Method to add a request parameter
        /// </summary>
        /// <param name="parameter">the request parameter</param>
        public void AddParameter(SoapRequestParameter parameter)
        {
            this.Parameters.Add(parameter);
        }

        /// <summary>
        /// Method to remove a parameter
        /// </summary>
        /// <param name="parameter">the request parameter</param>
        /// <returns>true if the parameter has been removed successfully</returns>
        public bool RemoveParameter(SoapRequestParameter parameter)
        {
            return this.Parameters.Remove(parameter);
        }
    }
}
