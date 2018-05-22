namespace PS.FritzBox.API.Samples
{
    partial class DeviceInfoControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRefreshDeviceInfo = new System.Windows.Forms.Button();
            this.pgDeviceInfo = new System.Windows.Forms.PropertyGrid();
            this.lbDeviceLog = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRefreshDeviceInfo
            // 
            this.btnRefreshDeviceInfo.Location = new System.Drawing.Point(4, 4);
            this.btnRefreshDeviceInfo.Name = "btnRefreshDeviceInfo";
            this.btnRefreshDeviceInfo.Size = new System.Drawing.Size(121, 23);
            this.btnRefreshDeviceInfo.TabIndex = 0;
            this.btnRefreshDeviceInfo.Text = "RefreshDeviceInfo";
            this.btnRefreshDeviceInfo.UseVisualStyleBackColor = true;
            this.btnRefreshDeviceInfo.Click += new System.EventHandler(this.button1_Click);
            // 
            // pgDeviceInfo
            // 
            this.pgDeviceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pgDeviceInfo.HelpVisible = false;
            this.pgDeviceInfo.Location = new System.Drawing.Point(4, 33);
            this.pgDeviceInfo.Name = "pgDeviceInfo";
            this.pgDeviceInfo.Size = new System.Drawing.Size(213, 354);
            this.pgDeviceInfo.TabIndex = 1;
            // 
            // lbDeviceLog
            // 
            this.lbDeviceLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDeviceLog.FormattingEnabled = true;
            this.lbDeviceLog.Location = new System.Drawing.Point(235, 59);
            this.lbDeviceLog.Name = "lbDeviceLog";
            this.lbDeviceLog.Size = new System.Drawing.Size(554, 329);
            this.lbDeviceLog.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(235, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "RefreshLog";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(395, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(501, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 21);
            this.button2.TabIndex = 5;
            this.button2.Text = "GetSecurityPort";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DeviceInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbDeviceLog);
            this.Controls.Add(this.pgDeviceInfo);
            this.Controls.Add(this.btnRefreshDeviceInfo);
            this.Name = "DeviceInfoControl";
            this.Size = new System.Drawing.Size(792, 390);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefreshDeviceInfo;
        private System.Windows.Forms.PropertyGrid pgDeviceInfo;
        private System.Windows.Forms.ListBox lbDeviceLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
    }
}
