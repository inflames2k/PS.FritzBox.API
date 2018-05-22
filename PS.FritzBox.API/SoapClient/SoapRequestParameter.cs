namespace PS.FritzBox.API.SOAP
{
    /// <summary>
    /// class representing soap request parameters
    /// </summary>
    internal class SoapRequestParameter
    {
        public SoapRequestParameter(string name, object value)
        {
            this.ParameterName = name;
            this.ParameterValue = value;
        }

        /// <summary>
        /// Gets or sets the parameter name
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Gets or sets the parameter value
        /// </summary>
        public object ParameterValue { get; set; }
    }
}
