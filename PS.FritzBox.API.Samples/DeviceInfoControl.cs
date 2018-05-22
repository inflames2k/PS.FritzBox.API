using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.FritzBox.API.Samples
{
    public partial class DeviceInfoControl : UserControl
    {
        public FritzSettings Settings { get; set; }

        public DeviceInfoControl()
        {
            InitializeComponent();
        }

        private async void RefreshDeviceInfo()
        {
            try
            {
                var client = new DeviceInfoClient(
                                            new ConnectionSettings()
                                            {
                                                BaseUrl = this.Settings?.BaseUrl,
                                                Timeout = this.Settings != null ? this.Settings.Timeout : 10000,
                                                UserName = this.Settings?.UserName,
                                                Password = this.Settings.Password
                                            });

                var info = await client.GetDeviceInfoAsync();
                this.pgDeviceInfo.SelectedObject = info;

                this.lbDeviceLog.DataSource = info.DeviceLog;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.RefreshDeviceInfo();
        }

        private async void RefreshLog()
        {
            try
            {
                var client = new DeviceInfoClient(
                                            new ConnectionSettings()
                                            {
                                                BaseUrl = this.Settings?.BaseUrl,
                                                Timeout = this.Settings != null ? this.Settings.Timeout : 10000,
                                                UserName = this.Settings?.UserName,
                                                Password = this.Settings.Password
                                            });

                var log = await client.GetDeviceLogAsync();

                this.lbDeviceLog.DataSource = log;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.RefreshLog();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new DeviceInfoClient(
                                          new ConnectionSettings()
                                          {
                                              BaseUrl = this.Settings?.BaseUrl,
                                              Timeout = this.Settings != null ? this.Settings.Timeout : 10000,
                                              UserName = this.Settings?.UserName,
                                              Password = this.Settings.Password
                                          });

                ushort secPort = await client.GetSecurityPortAsync();
                this.textBox1.Text = secPort.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
