using PS.FritzBox.API.WANDevice.WANConnectionDevice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.FritzBox.API.Samples
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.BindData(this.txtBaseUrl, "Text", this.ConnectionSettings, "BaseUrl");
            this.BindData(this.txtPassword, "Text", this.ConnectionSettings, "Password");
            this.BindData(this.txtTimeout, "Text", this.ConnectionSettings, "Timeout");
            this.BindData(this.txtUserName, "Text", this.ConnectionSettings, "UserName");

            this.deviceInfoControl1.Settings = this.ConnectionSettings;

        }

        public FritzSettings ConnectionSettings { get; set; } = new FritzSettings();
        
        private void BindData(Control target, string property, Object source, string dataMember)
        {
            target.DataBindings.Clear();
            target.DataBindings.Add(property, source, dataMember);
        }

        protected override async void OnShown(EventArgs e)
        {
            try
            {
                LANConfigSecurityClient client = new LANConfigSecurityClient("https://fritz.box", 10000, "inflames2k", "ps1988@rie");
                var info = await client.GetCurrentUserAsync();
                string rights = string.Empty;
                foreach(var right in info.Rights)
                {
                    rights += $"{right.Path}: {right.Access}{Environment.NewLine}";
                }
                MessageBox.Show($"Rights: {info.Rights.Count}{Environment.NewLine}{rights}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            base.OnShown(e);
        }
    }
}
