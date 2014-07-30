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
            this.DeleteButton = new System.Windows.Forms.Button();
            this.InputButton = new System.Windows.Forms.Button();
            this.groupBox_case = new System.Windows.Forms.GroupBox();
            this.textBox_project_id = new System.Windows.Forms.TextBox();
            this.label_project_id = new System.Windows.Forms.Label();
            this.richTextBox_project_addr = new System.Windows.Forms.RichTextBox();
            this.label_project_addr = new System.Windows.Forms.Label();
            this.textBox_project_Name = new System.Windows.Forms.TextBox();
            this.label_project_Name = new System.Windows.Forms.Label();
            this.label_project = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label_info = new System.Windows.Forms.Label();
            this.groupBox_location.SuspendLayout();
            this.groupBox_case.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_location
            // 
            this.groupBox_location.Controls.Add(this.label_latlng_lng);
            this.groupBox_location.Controls.Add(this.textBox_latlng_lng);
            this.groupBox_location.Controls.Add(this.label_latlng_lat);
            this.groupBox_location.Controls.Add(this.textBox_latlng_lat);
            this.groupBox_location.Location = new System.Drawing.Point(3, 216);
            this.groupBox_location.Name = "groupBox_location";
            this.groupBox_location.Size = new System.Drawing.Size(243, 80);
            this.groupBox_location.TabIndex = 3;
            this.groupBox_location.TabStop = false;
            this.groupBox_location.Text = "Location";
            // 
            // label_latlng_lng
            // 
            this.label_latlng_lng.AutoSize = true;
            this.label_latlng_lng.Location = new System.Drawing.Point(109, 36);
            this.label_latlng_lng.Name = "label_latlng_lng";
            this.label_latlng_lng.Size = new System.Drawing.Size(24, 12);
            this.label_latlng_lng.TabIndex = 2;
            this.label_latlng_lng.Text = "Lng";
            // 
            // textBox_latlng_lng
            // 
            this.textBox_latlng_lng.Location = new System.Drawing.Point(143, 33);
            this.textBox_latlng_lng.MaxLength = 16;
            this.textBox_latlng_lng.Name = "textBox_latlng_lng";
            this.textBox_latlng_lng.Size = new System.Drawing.Size(58, 22);
            this.textBox_latlng_lng.TabIndex = 3;
            // 
            // label_latlng_lat
            // 
            this.label_latlng_lat.AutoSize = true;
            this.label_latlng_lat.Location = new System.Drawing.Point(11, 36);
            this.label_latlng_lat.Name = "label_latlng_lat";
            this.label_latlng_lat.Size = new System.Drawing.Size(20, 12);
            this.label_latlng_lat.TabIndex = 0;
            this.label_latlng_lat.Text = "Lat";
            // 
            // textBox_latlng_lat
            // 
            this.textBox_latlng_lat.Location = new System.Drawing.Point(41, 33);
            this.textBox_latlng_lat.MaxLength = 16;
            this.textBox_latlng_lat.Name = "textBox_latlng_lat";
            this.textBox_latlng_lat.Size = new System.Drawing.Size(58, 22);
            this.textBox_latlng_lat.TabIndex = 1;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(131, 0);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 1;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Visible = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // InputButton
            // 
            this.InputButton.Location = new System.Drawing.Point(12, 1);
            this.InputButton.Name = "InputButton";
            this.InputButton.Size = new System.Drawing.Size(75, 23);
            this.InputButton.TabIndex = 0;
            this.InputButton.Text = "Add";
            this.InputButton.UseVisualStyleBackColor = true;
            this.InputButton.Click += new System.EventHandler(this.InputButton_Click);
            // 
            // groupBox_case
            // 
            this.groupBox_case.Controls.Add(this.textBox_project_id);
            this.groupBox_case.Controls.Add(this.label_project_id);
            this.groupBox_case.Controls.Add(this.richTextBox_project_addr);
            this.groupBox_case.Controls.Add(this.label_project_addr);
            this.groupBox_case.Controls.Add(this.textBox_project_Name);
            this.groupBox_case.Controls.Add(this.label_project_Name);
            this.groupBox_case.Location = new System.Drawing.Point(3, 40);
            this.groupBox_case.Name = "groupBox_case";
            this.groupBox_case.Size = new System.Drawing.Size(243, 170);
            this.groupBox_case.TabIndex = 2;
            this.groupBox_case.TabStop = false;
            this.groupBox_case.Text = "Case";
            // 
            // textBox_project_id
            // 
            this.textBox_project_id.Location = new System.Drawing.Point(6, 36);
            this.textBox_project_id.MaxLength = 100;
            this.textBox_project_id.Name = "textBox_project_id";
            this.textBox_project_id.Size = new System.Drawing.Size(223, 22);
            this.textBox_project_id.TabIndex = 1;
            // 
            // label_project_id
            // 
            this.label_project_id.AutoSize = true;
            this.label_project_id.Location = new System.Drawing.Point(6, 18);
            this.label_project_id.Name = "label_project_id";
            this.label_project_id.Size = new System.Drawing.Size(29, 12);
            this.label_project_id.TabIndex = 0;
            this.label_project_id.Text = "案號";
            // 
            // richTextBox_project_addr
            // 
            this.richTextBox_project_addr.Location = new System.Drawing.Point(6, 117);
            this.richTextBox_project_addr.Name = "richTextBox_project_addr";
            this.richTextBox_project_addr.Size = new System.Drawing.Size(223, 48);
            this.richTextBox_project_addr.TabIndex = 5;
            this.richTextBox_project_addr.Text = "";
            // 
            // label_project_addr
            // 
            this.label_project_addr.AutoSize = true;
            this.label_project_addr.Location = new System.Drawing.Point(6, 102);
            this.label_project_addr.Name = "label_project_addr";
            this.label_project_addr.Size = new System.Drawing.Size(29, 12);
            this.label_project_addr.TabIndex = 4;
            this.label_project_addr.Text = "地址";
            // 
            // textBox_project_Name
            // 
            this.textBox_project_Name.Location = new System.Drawing.Point(6, 77);
            this.textBox_project_Name.MaxLength = 100;
            this.textBox_project_Name.Name = "textBox_project_Name";
            this.textBox_project_Name.Size = new System.Drawing.Size(223, 22);
            this.textBox_project_Name.TabIndex = 3;
            // 
            // label_project_Name
            // 
            this.label_project_Name.AutoSize = true;
            this.label_project_Name.Location = new System.Drawing.Point(6, 59);
            this.label_project_Name.Name = "label_project_Name";
            this.label_project_Name.Size = new System.Drawing.Size(29, 12);
            this.label_project_Name.TabIndex = 2;
            this.label_project_Name.Text = "案名";
            // 
            // label_project
            // 
            this.label_project.AutoSize = true;
            this.label_project.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_project.Location = new System.Drawing.Point(3, 10);
            this.label_project.Name = "label_project";
            this.label_project.Size = new System.Drawing.Size(58, 16);
            this.label_project.TabIndex = 0;
            this.label_project.Text = "Project";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.InputButton);
            this.panel1.Controls.Add(this.DeleteButton);
            this.panel1.Location = new System.Drawing.Point(16, 302);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 24);
            this.panel1.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.label_info);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(67, 20);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(127, 24);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.BackColor = System.Drawing.SystemColors.Control;
            this.label_info.ForeColor = System.Drawing.Color.Red;
            this.label_info.Location = new System.Drawing.Point(91, 0);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(33, 12);
            this.label_info.TabIndex = 0;
            this.label_info.Text = "label1";
            // 
            // ProjectDataInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_project);
            this.Controls.Add(this.groupBox_location);
            this.Controls.Add(this.groupBox_case);
            this.Name = "ProjectDataInputControl";
            this.Size = new System.Drawing.Size(249, 332);
            this.groupBox_location.ResumeLayout(false);
            this.groupBox_location.PerformLayout();
            this.groupBox_case.ResumeLayout(false);
            this.groupBox_case.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_location;
        private System.Windows.Forms.Label label_latlng_lng;
        private System.Windows.Forms.TextBox textBox_latlng_lng;
        private System.Windows.Forms.Label label_latlng_lat;
        private System.Windows.Forms.TextBox textBox_latlng_lat;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button InputButton;
        private System.Windows.Forms.GroupBox groupBox_case;
        private System.Windows.Forms.RichTextBox richTextBox_project_addr;
        private System.Windows.Forms.Label label_project_addr;
        private System.Windows.Forms.TextBox textBox_project_Name;
        private System.Windows.Forms.Label label_project_Name;
        private System.Windows.Forms.Label label_project;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_project_id;
        private System.Windows.Forms.Label label_project_id;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label_info;
    }
}
