namespace MIRLE_GPLC.form
{
    partial class PLCInputControl
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_name = new System.Windows.Forms.Label();
            this.label_plc = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox_addr = new System.Windows.Forms.GroupBox();
            this.textBox_net_ip = new System.Windows.Forms.TextBox();
            this.label_net_port = new System.Windows.Forms.Label();
            this.label_net_IP = new System.Windows.Forms.Label();
            this.textBox_net_port = new System.Windows.Forms.TextBox();
            this.textBox_net_ID = new System.Windows.Forms.TextBox();
            this.label_net_ID = new System.Windows.Forms.Label();
            this.textBox_poll_rate = new System.Windows.Forms.TextBox();
            this.label_poll_rate = new System.Windows.Forms.Label();
            this.groupBox_addr.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(3, 35);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(91, 22);
            this.textBox_name.TabIndex = 14;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(5, 20);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(32, 12);
            this.label_name.TabIndex = 13;
            this.label_name.Text = "Name";
            // 
            // label_plc
            // 
            this.label_plc.AutoSize = true;
            this.label_plc.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_plc.Location = new System.Drawing.Point(3, 0);
            this.label_plc.Name = "label_plc";
            this.label_plc.Size = new System.Drawing.Size(38, 16);
            this.label_plc.TabIndex = 12;
            this.label_plc.Text = "PLC";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(107, 136);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(9, 136);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox_addr
            // 
            this.groupBox_addr.Controls.Add(this.textBox_net_ip);
            this.groupBox_addr.Controls.Add(this.label_net_port);
            this.groupBox_addr.Controls.Add(this.label_net_IP);
            this.groupBox_addr.Controls.Add(this.textBox_net_port);
            this.groupBox_addr.Controls.Add(this.textBox_net_ID);
            this.groupBox_addr.Controls.Add(this.label_net_ID);
            this.groupBox_addr.Location = new System.Drawing.Point(1, 63);
            this.groupBox_addr.Name = "groupBox_addr";
            this.groupBox_addr.Size = new System.Drawing.Size(192, 67);
            this.groupBox_addr.TabIndex = 15;
            this.groupBox_addr.TabStop = false;
            this.groupBox_addr.Text = "Address";
            // 
            // textBox_net_ip
            // 
            this.textBox_net_ip.Location = new System.Drawing.Point(29, 36);
            this.textBox_net_ip.Name = "textBox_net_ip";
            this.textBox_net_ip.Size = new System.Drawing.Size(100, 22);
            this.textBox_net_ip.TabIndex = 3;
            // 
            // label_net_port
            // 
            this.label_net_port.AutoSize = true;
            this.label_net_port.Location = new System.Drawing.Point(136, 18);
            this.label_net_port.Name = "label_net_port";
            this.label_net_port.Size = new System.Drawing.Size(24, 12);
            this.label_net_port.TabIndex = 4;
            this.label_net_port.Text = "port";
            // 
            // label_net_IP
            // 
            this.label_net_IP.AutoSize = true;
            this.label_net_IP.Location = new System.Drawing.Point(30, 18);
            this.label_net_IP.Name = "label_net_IP";
            this.label_net_IP.Size = new System.Drawing.Size(15, 12);
            this.label_net_IP.TabIndex = 1;
            this.label_net_IP.Text = "IP";
            // 
            // textBox_net_port
            // 
            this.textBox_net_port.Enabled = false;
            this.textBox_net_port.Location = new System.Drawing.Point(135, 36);
            this.textBox_net_port.MaxLength = 5;
            this.textBox_net_port.Name = "textBox_net_port";
            this.textBox_net_port.Size = new System.Drawing.Size(49, 22);
            this.textBox_net_port.TabIndex = 5;
            this.textBox_net_port.Text = "502";
            // 
            // textBox_net_ID
            // 
            this.textBox_net_ID.Location = new System.Drawing.Point(6, 36);
            this.textBox_net_ID.MaxLength = 2;
            this.textBox_net_ID.Name = "textBox_net_ID";
            this.textBox_net_ID.Size = new System.Drawing.Size(17, 22);
            this.textBox_net_ID.TabIndex = 2;
            // 
            // label_net_ID
            // 
            this.label_net_ID.AutoSize = true;
            this.label_net_ID.Location = new System.Drawing.Point(6, 18);
            this.label_net_ID.Name = "label_net_ID";
            this.label_net_ID.Size = new System.Drawing.Size(17, 12);
            this.label_net_ID.TabIndex = 0;
            this.label_net_ID.Text = "ID";
            // 
            // textBox_poll_rate
            // 
            this.textBox_poll_rate.Location = new System.Drawing.Point(110, 35);
            this.textBox_poll_rate.Name = "textBox_poll_rate";
            this.textBox_poll_rate.Size = new System.Drawing.Size(75, 22);
            this.textBox_poll_rate.TabIndex = 18;
            // 
            // label_poll_rate
            // 
            this.label_poll_rate.AutoSize = true;
            this.label_poll_rate.Location = new System.Drawing.Point(108, 20);
            this.label_poll_rate.Name = "label_poll_rate";
            this.label_poll_rate.Size = new System.Drawing.Size(58, 12);
            this.label_poll_rate.TabIndex = 19;
            this.label_poll_rate.Text = "Polling rate";
            // 
            // PLCInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_poll_rate);
            this.Controls.Add(this.textBox_poll_rate);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.label_plc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox_addr);
            this.Name = "PLCInputControl";
            this.Size = new System.Drawing.Size(196, 163);
            this.groupBox_addr.ResumeLayout(false);
            this.groupBox_addr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_plc;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox_addr;
        private System.Windows.Forms.TextBox textBox_net_ip;
        private System.Windows.Forms.Label label_net_port;
        private System.Windows.Forms.Label label_net_IP;
        private System.Windows.Forms.TextBox textBox_net_port;
        private System.Windows.Forms.TextBox textBox_net_ID;
        private System.Windows.Forms.Label label_net_ID;
        private System.Windows.Forms.TextBox textBox_poll_rate;
        private System.Windows.Forms.Label label_poll_rate;

    }
}
