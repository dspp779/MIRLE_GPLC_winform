namespace MIRLE_GPLC.form
{
    partial class TagInputControl
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.comboBox_format = new System.Windows.Forms.ComboBox();
            this.textBox_addr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_type = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_name = new System.Windows.Forms.Label();
            this.label_field = new System.Windows.Forms.Label();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.groupBox_scaling = new System.Windows.Forms.GroupBox();
            this.checkBox_scale_linear = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_scale_type = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_scale_lo = new System.Windows.Forms.TextBox();
            this.textBox_scale_hi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_raw_lo = new System.Windows.Forms.TextBox();
            this.textBox_raw_hi = new System.Windows.Forms.TextBox();
            this.textBox_unit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label_info = new System.Windows.Forms.Label();
            this.groupBox_scaling.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(135, 231);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(29, 231);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // comboBox_format
            // 
            this.comboBox_format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_format.FormattingEnabled = true;
            this.comboBox_format.Items.AddRange(new object[] {
            "####",
            "###.#",
            "##.##",
            "#.###"});
            this.comboBox_format.Location = new System.Drawing.Point(128, 104);
            this.comboBox_format.Name = "comboBox_format";
            this.comboBox_format.Size = new System.Drawing.Size(82, 20);
            this.comboBox_format.TabIndex = 11;
            // 
            // textBox_addr
            // 
            this.textBox_addr.Location = new System.Drawing.Point(24, 62);
            this.textBox_addr.Name = "textBox_addr";
            this.textBox_addr.Size = new System.Drawing.Size(64, 22);
            this.textBox_addr.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "Format";
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Location = new System.Drawing.Point(126, 46);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(53, 12);
            this.label_type.TabIndex = 6;
            this.label_type.Text = "Data Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Address";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(74, 21);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(143, 22);
            this.textBox_name.TabIndex = 3;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(8, 24);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(53, 12);
            this.label_name.TabIndex = 2;
            this.label_name.Text = "Tag Name";
            // 
            // label_field
            // 
            this.label_field.AutoSize = true;
            this.label_field.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_field.Location = new System.Drawing.Point(7, 1);
            this.label_field.Name = "label_field";
            this.label_field.Size = new System.Drawing.Size(41, 16);
            this.label_field.TabIndex = 1;
            this.label_field.Text = "field";
            // 
            // comboBox_type
            // 
            this.comboBox_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "WORD",
            "LONG",
            "FLOAT",
            "LONG SWAP",
            "FLOAT SWAP"});
            this.comboBox_type.Location = new System.Drawing.Point(128, 62);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(82, 20);
            this.comboBox_type.TabIndex = 7;
            // 
            // groupBox_scaling
            // 
            this.groupBox_scaling.Controls.Add(this.checkBox_scale_linear);
            this.groupBox_scaling.Controls.Add(this.label7);
            this.groupBox_scaling.Controls.Add(this.comboBox_scale_type);
            this.groupBox_scaling.Controls.Add(this.label5);
            this.groupBox_scaling.Controls.Add(this.label6);
            this.groupBox_scaling.Controls.Add(this.textBox_scale_lo);
            this.groupBox_scaling.Controls.Add(this.textBox_scale_hi);
            this.groupBox_scaling.Controls.Add(this.label4);
            this.groupBox_scaling.Controls.Add(this.label2);
            this.groupBox_scaling.Controls.Add(this.textBox_raw_lo);
            this.groupBox_scaling.Controls.Add(this.textBox_raw_hi);
            this.groupBox_scaling.Location = new System.Drawing.Point(3, 127);
            this.groupBox_scaling.Name = "groupBox_scaling";
            this.groupBox_scaling.Size = new System.Drawing.Size(227, 103);
            this.groupBox_scaling.TabIndex = 12;
            this.groupBox_scaling.TabStop = false;
            this.groupBox_scaling.Text = "Scaling";
            // 
            // checkBox_scale_linear
            // 
            this.checkBox_scale_linear.AutoSize = true;
            this.checkBox_scale_linear.Location = new System.Drawing.Point(11, 21);
            this.checkBox_scale_linear.Name = "checkBox_scale_linear";
            this.checkBox_scale_linear.Size = new System.Drawing.Size(54, 16);
            this.checkBox_scale_linear.TabIndex = 13;
            this.checkBox_scale_linear.Text = "Linear";
            this.checkBox_scale_linear.UseVisualStyleBackColor = true;
            this.checkBox_scale_linear.CheckedChanged += new System.EventHandler(this.checkBox_scale_linear_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "Data Type";
            // 
            // comboBox_scale_type
            // 
            this.comboBox_scale_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_scale_type.Enabled = false;
            this.comboBox_scale_type.FormattingEnabled = true;
            this.comboBox_scale_type.Items.AddRange(new object[] {
            "WORD",
            "LONG",
            "FLOAT"});
            this.comboBox_scale_type.Location = new System.Drawing.Point(6, 67);
            this.comboBox_scale_type.Name = "comboBox_scale_type";
            this.comboBox_scale_type.Size = new System.Drawing.Size(82, 20);
            this.comboBox_scale_type.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "Scale Low";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(163, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "Scale High";
            // 
            // textBox_scale_lo
            // 
            this.textBox_scale_lo.Enabled = false;
            this.textBox_scale_lo.Location = new System.Drawing.Point(164, 72);
            this.textBox_scale_lo.Name = "textBox_scale_lo";
            this.textBox_scale_lo.Size = new System.Drawing.Size(58, 22);
            this.textBox_scale_lo.TabIndex = 23;
            // 
            // textBox_scale_hi
            // 
            this.textBox_scale_hi.Enabled = false;
            this.textBox_scale_hi.Location = new System.Drawing.Point(164, 32);
            this.textBox_scale_hi.Name = "textBox_scale_hi";
            this.textBox_scale_hi.Size = new System.Drawing.Size(58, 22);
            this.textBox_scale_hi.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(100, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "Raw Low";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "Raw High";
            // 
            // textBox_raw_lo
            // 
            this.textBox_raw_lo.Enabled = false;
            this.textBox_raw_lo.Location = new System.Drawing.Point(100, 72);
            this.textBox_raw_lo.Name = "textBox_raw_lo";
            this.textBox_raw_lo.Size = new System.Drawing.Size(58, 22);
            this.textBox_raw_lo.TabIndex = 19;
            // 
            // textBox_raw_hi
            // 
            this.textBox_raw_hi.Enabled = false;
            this.textBox_raw_hi.Location = new System.Drawing.Point(100, 32);
            this.textBox_raw_hi.Name = "textBox_raw_hi";
            this.textBox_raw_hi.Size = new System.Drawing.Size(57, 22);
            this.textBox_raw_hi.TabIndex = 17;
            // 
            // textBox_unit
            // 
            this.textBox_unit.Location = new System.Drawing.Point(24, 102);
            this.textBox_unit.Name = "textBox_unit";
            this.textBox_unit.Size = new System.Drawing.Size(64, 22);
            this.textBox_unit.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "Unit";
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.BackColor = System.Drawing.SystemColors.Control;
            this.label_info.ForeColor = System.Drawing.Color.Red;
            this.label_info.Location = new System.Drawing.Point(91, 1);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(0, 12);
            this.label_info.TabIndex = 0;
            // 
            // TagInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_info);
            this.Controls.Add(this.textBox_unit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox_scaling);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.label_field);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.comboBox_format);
            this.Controls.Add(this.textBox_addr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_type);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_name);
            this.Name = "TagInputControl";
            this.Size = new System.Drawing.Size(234, 255);
            this.groupBox_scaling.ResumeLayout(false);
            this.groupBox_scaling.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox comboBox_format;
        private System.Windows.Forms.TextBox textBox_addr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_field;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.GroupBox groupBox_scaling;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_scale_lo;
        private System.Windows.Forms.TextBox textBox_scale_hi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_raw_lo;
        private System.Windows.Forms.TextBox textBox_raw_hi;
        private System.Windows.Forms.ComboBox comboBox_scale_type;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_unit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBox_scale_linear;
        private System.Windows.Forms.Label label_info;
    }
}
