namespace MIRLE_GPLC.form
{
    partial class InputForm
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
            this.groupBox_location = new System.Windows.Forms.GroupBox();
            this.label_latlng_lng = new System.Windows.Forms.Label();
            this.textBox_latlng_lng = new System.Windows.Forms.TextBox();
            this.label_latlng_lat = new System.Windows.Forms.Label();
            this.textBox_latlng_lat = new System.Windows.Forms.TextBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.InputButton = new System.Windows.Forms.Button();
            this.groupBox_case = new System.Windows.Forms.GroupBox();
            this.textBox_case_addr = new System.Windows.Forms.TextBox();
            this.label_case_addr = new System.Windows.Forms.Label();
            this.textBox_case_Name = new System.Windows.Forms.TextBox();
            this.label_case_Name = new System.Windows.Forms.Label();
            this.textBox_case_ID = new System.Windows.Forms.TextBox();
            this.label_case_ID = new System.Windows.Forms.Label();
            this.groupBox_addr = new System.Windows.Forms.GroupBox();
            this.textBox_net_IP = new System.Windows.Forms.TextBox();
            this.label_net_port = new System.Windows.Forms.Label();
            this.label_net_IP = new System.Windows.Forms.Label();
            this.textBox_net_port = new System.Windows.Forms.TextBox();
            this.textBox_net_ID = new System.Windows.Forms.TextBox();
            this.label_net_ID = new System.Windows.Forms.Label();
            this.groupBox_location.SuspendLayout();
            this.groupBox_case.SuspendLayout();
            this.groupBox_addr.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_location
            // 
            this.groupBox_location.Controls.Add(this.label_latlng_lng);
            this.groupBox_location.Controls.Add(this.textBox_latlng_lng);
            this.groupBox_location.Controls.Add(this.label_latlng_lat);
            this.groupBox_location.Controls.Add(this.textBox_latlng_lat);
            this.groupBox_location.Location = new System.Drawing.Point(214, 12);
            this.groupBox_location.Name = "groupBox_location";
            this.groupBox_location.Size = new System.Drawing.Size(209, 80);
            this.groupBox_location.TabIndex = 15;
            this.groupBox_location.TabStop = false;
            this.groupBox_location.Text = "Location";
            // 
            // label_latlng_lng
            // 
            this.label_latlng_lng.AutoSize = true;
            this.label_latlng_lng.Location = new System.Drawing.Point(110, 36);
            this.label_latlng_lng.Name = "label_latlng_lng";
            this.label_latlng_lng.Size = new System.Drawing.Size(24, 12);
            this.label_latlng_lng.TabIndex = 9;
            this.label_latlng_lng.Text = "Lng";
            // 
            // textBox_latlng_lng
            // 
            this.textBox_latlng_lng.Location = new System.Drawing.Point(149, 33);
            this.textBox_latlng_lng.MaxLength = 16;
            this.textBox_latlng_lng.Name = "textBox_latlng_lng";
            this.textBox_latlng_lng.Size = new System.Drawing.Size(49, 22);
            this.textBox_latlng_lng.TabIndex = 8;
            // 
            // label_latlng_lat
            // 
            this.label_latlng_lat.AutoSize = true;
            this.label_latlng_lat.Location = new System.Drawing.Point(11, 36);
            this.label_latlng_lat.Name = "label_latlng_lat";
            this.label_latlng_lat.Size = new System.Drawing.Size(20, 12);
            this.label_latlng_lat.TabIndex = 7;
            this.label_latlng_lat.Text = "Lat";
            // 
            // textBox_latlng_lat
            // 
            this.textBox_latlng_lat.Location = new System.Drawing.Point(46, 33);
            this.textBox_latlng_lat.MaxLength = 16;
            this.textBox_latlng_lat.Name = "textBox_latlng_lat";
            this.textBox_latlng_lat.Size = new System.Drawing.Size(49, 22);
            this.textBox_latlng_lat.TabIndex = 6;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(306, 190);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 14;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // InputButton
            // 
            this.InputButton.Location = new System.Drawing.Point(306, 142);
            this.InputButton.Name = "InputButton";
            this.InputButton.Size = new System.Drawing.Size(75, 23);
            this.InputButton.TabIndex = 13;
            this.InputButton.Text = "Add";
            this.InputButton.UseVisualStyleBackColor = true;
            // 
            // groupBox_case
            // 
            this.groupBox_case.Controls.Add(this.textBox_case_addr);
            this.groupBox_case.Controls.Add(this.label_case_addr);
            this.groupBox_case.Controls.Add(this.textBox_case_Name);
            this.groupBox_case.Controls.Add(this.label_case_Name);
            this.groupBox_case.Controls.Add(this.textBox_case_ID);
            this.groupBox_case.Controls.Add(this.label_case_ID);
            this.groupBox_case.Location = new System.Drawing.Point(12, 107);
            this.groupBox_case.Name = "groupBox_case";
            this.groupBox_case.Size = new System.Drawing.Size(243, 177);
            this.groupBox_case.TabIndex = 12;
            this.groupBox_case.TabStop = false;
            this.groupBox_case.Text = "Case";
            // 
            // textBox_case_addr
            // 
            this.textBox_case_addr.Location = new System.Drawing.Point(6, 133);
            this.textBox_case_addr.MaxLength = 15;
            this.textBox_case_addr.Name = "textBox_case_addr";
            this.textBox_case_addr.Size = new System.Drawing.Size(228, 22);
            this.textBox_case_addr.TabIndex = 10;
            // 
            // label_case_addr
            // 
            this.label_case_addr.AutoSize = true;
            this.label_case_addr.Location = new System.Drawing.Point(6, 115);
            this.label_case_addr.Name = "label_case_addr";
            this.label_case_addr.Size = new System.Drawing.Size(29, 12);
            this.label_case_addr.TabIndex = 11;
            this.label_case_addr.Text = "地址";
            // 
            // textBox_case_Name
            // 
            this.textBox_case_Name.Location = new System.Drawing.Point(8, 84);
            this.textBox_case_Name.MaxLength = 15;
            this.textBox_case_Name.Name = "textBox_case_Name";
            this.textBox_case_Name.Size = new System.Drawing.Size(223, 22);
            this.textBox_case_Name.TabIndex = 8;
            // 
            // label_case_Name
            // 
            this.label_case_Name.AutoSize = true;
            this.label_case_Name.Location = new System.Drawing.Point(8, 66);
            this.label_case_Name.Name = "label_case_Name";
            this.label_case_Name.Size = new System.Drawing.Size(29, 12);
            this.label_case_Name.TabIndex = 9;
            this.label_case_Name.Text = "案名";
            // 
            // textBox_case_ID
            // 
            this.textBox_case_ID.Location = new System.Drawing.Point(6, 37);
            this.textBox_case_ID.MaxLength = 15;
            this.textBox_case_ID.Name = "textBox_case_ID";
            this.textBox_case_ID.Size = new System.Drawing.Size(100, 22);
            this.textBox_case_ID.TabIndex = 6;
            // 
            // label_case_ID
            // 
            this.label_case_ID.AutoSize = true;
            this.label_case_ID.Location = new System.Drawing.Point(6, 19);
            this.label_case_ID.Name = "label_case_ID";
            this.label_case_ID.Size = new System.Drawing.Size(17, 12);
            this.label_case_ID.TabIndex = 7;
            this.label_case_ID.Text = "ID";
            // 
            // groupBox_addr
            // 
            this.groupBox_addr.Controls.Add(this.textBox_net_IP);
            this.groupBox_addr.Controls.Add(this.label_net_port);
            this.groupBox_addr.Controls.Add(this.label_net_IP);
            this.groupBox_addr.Controls.Add(this.textBox_net_port);
            this.groupBox_addr.Controls.Add(this.textBox_net_ID);
            this.groupBox_addr.Controls.Add(this.label_net_ID);
            this.groupBox_addr.Location = new System.Drawing.Point(12, 12);
            this.groupBox_addr.Name = "groupBox_addr";
            this.groupBox_addr.Size = new System.Drawing.Size(196, 80);
            this.groupBox_addr.TabIndex = 11;
            this.groupBox_addr.TabStop = false;
            this.groupBox_addr.Text = "Address";
            // 
            // textBox_net_IP
            // 
            this.textBox_net_IP.Location = new System.Drawing.Point(30, 36);
            this.textBox_net_IP.MaxLength = 15;
            this.textBox_net_IP.Name = "textBox_net_IP";
            this.textBox_net_IP.Size = new System.Drawing.Size(100, 22);
            this.textBox_net_IP.TabIndex = 0;
            // 
            // label_net_port
            // 
            this.label_net_port.AutoSize = true;
            this.label_net_port.Location = new System.Drawing.Point(136, 18);
            this.label_net_port.Name = "label_net_port";
            this.label_net_port.Size = new System.Drawing.Size(24, 12);
            this.label_net_port.TabIndex = 5;
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
            this.textBox_net_port.Location = new System.Drawing.Point(136, 36);
            this.textBox_net_port.MaxLength = 5;
            this.textBox_net_port.Name = "textBox_net_port";
            this.textBox_net_port.Size = new System.Drawing.Size(49, 22);
            this.textBox_net_port.TabIndex = 4;
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
            this.label_net_ID.TabIndex = 3;
            this.label_net_ID.Text = "ID";
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 297);
            this.Controls.Add(this.groupBox_location);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.InputButton);
            this.Controls.Add(this.groupBox_case);
            this.Controls.Add(this.groupBox_addr);
            this.Name = "InputForm";
            this.Text = "InputForm";
            this.groupBox_location.ResumeLayout(false);
            this.groupBox_location.PerformLayout();
            this.groupBox_case.ResumeLayout(false);
            this.groupBox_case.PerformLayout();
            this.groupBox_addr.ResumeLayout(false);
            this.groupBox_addr.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_location;
        private System.Windows.Forms.Label label_latlng_lng;
        private System.Windows.Forms.TextBox textBox_latlng_lng;
        private System.Windows.Forms.Label label_latlng_lat;
        private System.Windows.Forms.TextBox textBox_latlng_lat;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button InputButton;
        private System.Windows.Forms.GroupBox groupBox_case;
        private System.Windows.Forms.TextBox textBox_case_addr;
        private System.Windows.Forms.Label label_case_addr;
        private System.Windows.Forms.TextBox textBox_case_Name;
        private System.Windows.Forms.Label label_case_Name;
        private System.Windows.Forms.TextBox textBox_case_ID;
        private System.Windows.Forms.Label label_case_ID;
        private System.Windows.Forms.GroupBox groupBox_addr;
        private System.Windows.Forms.TextBox textBox_net_IP;
        private System.Windows.Forms.Label label_net_port;
        private System.Windows.Forms.Label label_net_IP;
        private System.Windows.Forms.TextBox textBox_net_port;
        private System.Windows.Forms.TextBox textBox_net_ID;
        private System.Windows.Forms.Label label_net_ID;
    }
}