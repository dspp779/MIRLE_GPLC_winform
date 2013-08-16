namespace MIRLE_GPLC.form
{
    partial class PLCForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox_addr = new System.Windows.Forms.GroupBox();
            this.textBox_net_ip = new System.Windows.Forms.TextBox();
            this.label_net_port = new System.Windows.Forms.Label();
            this.label_net_IP = new System.Windows.Forms.Label();
            this.textBox_net_port = new System.Windows.Forms.TextBox();
            this.textBox_net_ID = new System.Windows.Forms.TextBox();
            this.label_net_ID = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label_project = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.groupBox_addr.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_addr
            // 
            this.groupBox_addr.Controls.Add(this.textBox_net_ip);
            this.groupBox_addr.Controls.Add(this.label_net_port);
            this.groupBox_addr.Controls.Add(this.label_net_IP);
            this.groupBox_addr.Controls.Add(this.textBox_net_port);
            this.groupBox_addr.Controls.Add(this.textBox_net_ID);
            this.groupBox_addr.Controls.Add(this.label_net_ID);
            this.groupBox_addr.Location = new System.Drawing.Point(12, 70);
            this.groupBox_addr.Name = "groupBox_addr";
            this.groupBox_addr.Size = new System.Drawing.Size(194, 80);
            this.groupBox_addr.TabIndex = 3;
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
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(20, 156);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(122, 156);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label_project
            // 
            this.label_project.AutoSize = true;
            this.label_project.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_project.Location = new System.Drawing.Point(16, 9);
            this.label_project.Name = "label_project";
            this.label_project.Size = new System.Drawing.Size(39, 16);
            this.label_project.TabIndex = 0;
            this.label_project.Text = "case";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(16, 40);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(32, 12);
            this.label_name.TabIndex = 1;
            this.label_name.Text = "Name";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(54, 37);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(143, 22);
            this.textBox_name.TabIndex = 2;
            // 
            // PLCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 191);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.label_project);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox_addr);
            this.Name = "PLCForm";
            this.Text = "PLC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PLCForm_FormClosing);
            this.Load += new System.EventHandler(this.PLCForm_Load);
            this.groupBox_addr.ResumeLayout(false);
            this.groupBox_addr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_addr;
        private System.Windows.Forms.Label label_net_port;
        private System.Windows.Forms.Label label_net_IP;
        private System.Windows.Forms.TextBox textBox_net_port;
        private System.Windows.Forms.TextBox textBox_net_ID;
        private System.Windows.Forms.Label label_net_ID;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label_project;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_net_ip;
    }
}