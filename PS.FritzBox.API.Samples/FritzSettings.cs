using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.FritzBox.API.Samples
{
    public class FritzSettings : BindableObject
    {
        private string _baseUrl = "https://fritz.box";
        /// <summary>
        /// Gets or sets the base url
        /// </summary>
        public string BaseUrl
        {
            get { return _baseUrl; }
            set {
                if (_baseUrl == value)
                    return;
                _baseUrl = value;
                this.OnPropertyChanged();
            }
        }

        private int _timeout = 10000;
        /// <summary>
        /// Gets or sets the timeout
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set {
                if (_timeout == value)
                    return;
                _timeout = value;
                this.OnPropertyChanged();
            }
        }

        private string _userName;
        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set {
                if (_userName == value)
                    return;
                _userName = value;
                this.OnPropertyChanged();
            }
        }

        private string _password;
        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password
        {
            get { return _password; }
            set {
                if (_password == value)
                    return;
                _password = value;
                this.OnPropertyChanged();
            }
        }

    }
}
