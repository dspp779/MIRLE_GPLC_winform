namespace MIRLE_GPLC.form
{
    partial class ProjectDataInputControl
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
            this.groupBox_location = new System.Windows.Forms.GroupBox();
            this.label_latlng_lng = new System.Windows.Forms.Label();
            this.textBox_latlng_lng = new System.Windows.Forms.TextBox();
            this.label_latlng_lat = new System.Windows.Forms.Label();
            this.textBox_latlng_lat = new System.Windows.Forms.TextBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.InputButton = new System.Windows.Forms.Button();
            this.groupBox_case = new System.Windows.Forms.GroupBox();
            this.richTextBox_case_addr = new System.Windows.Forms.RichTextBox();
            this.label_case_addr = new System.Windows.Forms.Label();
            this.textBox_case_Name = new System.Windows.Forms.TextBox();
            this.label_case_Name = new System.Windows.Forms.Label();
            this.label_project = new System.Windows.Forms.Label();
            this.groupBox_location.SuspendLayout();
            this.groupBox_case.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_location
            // 
            this.groupBox_location.Controls.Add(this.label_latlng_lng);
            this.groupBox_location.Controls.Add(this.textBox_latlng_lng);
            this.groupBox_location.Controls.Add(this.label_latlng_lat);
            this.groupBox_location.Controls.Add(this.textBox_latlng_lat);
            this.groupBox_location.Location = new System.Drawing.Point(3, 197);
            this.groupBox_location.Name = "groupBox_location";
            this.groupBox_location.Size = new System.Drawing.Size(243, 80);
            this.groupBox_location.TabIndex = 14;
            this.groupBox_location.TabStop = false;
            this.groupBox_location.Text = "Location";
            // 
            // label_latlng_lng
            // 
            this.label_latlng_lng.AutoSize = true;
            this.label_latlng_lng.Location = new System.Drawing.Point(109, 36);
            this.label_latlng_lng.Name = "label_latlng_lng";
            this.label_latlng_lng.Size = new System.Drawing.Size(24, 12);
            this.label_latlng_lng.TabIndex = 9;
            this.label_latlng_lng.Text = "Lng";
            // 
            // textBox_latlng_lng
            // 
            this.textBox_latlng_lng.Location = new System.Drawing.Point(143, 33);
            this.textBox_latlng_lng.MaxLength = 16;
            this.textBox_latlng_lng.Name = "textBox_latlng_lng";
            this.textBox_latlng_lng.Size = new System.Drawing.Size(58, 22);
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
            this.textBox_latlng_lat.Location = new System.Drawing.Point(41, 33);
            this.textBox_latlng_lat.MaxLength = 16;
            this.textBox_latlng_lat.Name = "textBox_latlng_lat";
            this.textBox_latlng_lat.Size = new System.Drawing.Size(58, 22);
            this.textBox_latlng_lat.TabIndex = 6;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(146, 283);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 13;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // InputButton
            // 
            this.InputButton.Location = new System.Drawing.Point(33, 283);
            this.InputButton.Name = "InputButton";
            this.InputButton.Size = new System.Drawing.Size(75, 23);
            this.InputButton.TabIndex = 12;
            this.InputButton.Text = "Add";
            this.InputButton.UseVisualStyleBackColor = true;
            this.InputButton.Click += new System.EventHandler(this.InputButton_Click);
            // 
            // groupBox_case
            // 
            this.groupBox_case.Controls.Add(this.richTextBox_case_addr);
            this.groupBox_case.Controls.Add(this.label_case_addr);
            this.groupBox_case.Controls.Add(this.textBox_case_Name);
            this.groupBox_case.Controls.Add(this.label_case_Name);
            this.groupBox_case.Location = new System.Drawing.Point(3, 40);
            this.groupBox_case.Name = "groupBox_case";
            this.groupBox_case.Size = new System.Drawing.Size(243, 151);
            this.groupBox_case.TabIndex = 11;
            this.groupBox_case.TabStop = false;
            this.groupBox_case.Text = "Case";
            // 
            // richTextBox_case_addr
            // 
            this.richTextBox_case_addr.Location = new System.Drawing.Point(6, 82);
            this.richTextBox_case_addr.Name = "richTextBox_case_addr";
            this.richTextBox_case_addr.Size = new System.Drawing.Size(223, 48);
            this.richTextBox_case_addr.TabIndex = 11;
            this.richTextBox_case_addr.Text = "";
            // 
            // label_case_addr
            // 
            this.label_case_addr.AutoSize = true;
            this.label_case_addr.Location = new System.Drawing.Point(4, 67);
            this.label_case_addr.Name = "label_case_addr";
            this.label_case_addr.Size = new System.Drawing.Size(29, 12);
            this.label_case_addr.TabIndex = 11;
            this.label_case_addr.Text = "地址";
            // 
            // textBox_case_Name
            // 
            this.textBox_case_Name.Location = new System.Drawing.Point(6, 36);
            this.textBox_case_Name.MaxLength = 100;
            this.textBox_case_Name.Name = "textBox_case_Name";
            this.textBox_case_Name.Size = new System.Drawing.Size(223, 22);
            this.textBox_case_Name.TabIndex = 8;
            // 
            // label_case_Name
            // 
            this.label_case_Name.AutoSize = true;
            this.label_case_Name.Location = new System.Drawing.Point(6, 18);
            this.label_case_Name.Name = "label_case_Name";
            this.label_case_Name.Size = new System.Drawing.Size(29, 12);
            this.label_case_Name.TabIndex = 9;
            this.label_case_Name.Text = "案名";
            // 
            // label_project
            // 
            this.label_project.AutoSize = true;
            this.label_project.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_project.Location = new System.Drawing.Point(3, 10);
            this.label_project.Name = "label_project";
            this.label_project.Size = new System.Drawing.Size(58, 16);
            this.label_project.TabIndex = 15;
            this.label_project.Text = "Project";
            // 
            // ProjectDataInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.label_project);
            this.Controls.Add(this.groupBox_location);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.InputButton);
            this.Controls.Add(this.groupBox_case);
            this.Name = "ProjectDataInputControl";
            this.Size = new System.Drawing.Size(249, 309);
            this.groupBox_location.ResumeLayout(false);
            this.groupBox_location.PerformLayout();
            this.groupBox_case.ResumeLayout(false);
            this.groupBox_case.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.RichTextBox richTextBox_case_addr;
        private System.Windows.Forms.Label label_case_addr;
        private System.Windows.Forms.TextBox textBox_case_Name;
        private System.Windows.Forms.Label label_case_Name;
        private System.Windows.Forms.Label label_project;
    }
}
