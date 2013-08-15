namespace MIRLE_GPLC.form
{
    partial class ToolTipContent
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.Label();
            this.labelCounter = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.listView_plc = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_data = new System.Windows.Forms.ListView();
            this.item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTitle.Location = new System.Drawing.Point(19, 16);
            this.labelTitle.MaximumSize = new System.Drawing.Size(400, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(43, 19);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "title";
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(19, 40);
            this.labelText.MaximumSize = new System.Drawing.Size(400, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(30, 14);
            this.labelText.TabIndex = 1;
            this.labelText.Text = "text";
            // 
            // labelCounter
            // 
            this.labelCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCounter.AutoSize = true;
            this.labelCounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCounter.Location = new System.Drawing.Point(447, 16);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(34, 14);
            this.labelCounter.TabIndex = 2;
            this.labelCounter.Text = "page";
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBack.Location = new System.Drawing.Point(438, 36);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(26, 23);
            this.buttonBack.TabIndex = 3;
            this.buttonBack.Text = "<";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Location = new System.Drawing.Point(470, 36);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(26, 23);
            this.buttonNext.TabIndex = 4;
            this.buttonNext.Text = ">";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // listView_plc
            // 
            this.listView_plc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_plc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.id,
            this.ip,
            this.port});
            this.listView_plc.FullRowSelect = true;
            this.listView_plc.Location = new System.Drawing.Point(22, 84);
            this.listView_plc.Name = "listView_plc";
            this.listView_plc.Size = new System.Drawing.Size(265, 255);
            this.listView_plc.TabIndex = 5;
            this.listView_plc.UseCompatibleStateImageBehavior = false;
            this.listView_plc.View = System.Windows.Forms.View.Details;
            this.listView_plc.SelectedIndexChanged += new System.EventHandler(this.listView_plc_SelectedIndexChanged);
            this.listView_plc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_plc_MouseDoubleClick);
            // 
            // name
            // 
            this.name.Text = "名稱";
            this.name.Width = 70;
            // 
            // id
            // 
            this.id.Text = "ID";
            this.id.Width = 30;
            // 
            // ip
            // 
            this.ip.Text = "IP";
            this.ip.Width = 110;
            // 
            // port
            // 
            this.port.Text = "Port";
            this.port.Width = 50;
            // 
            // listView_data
            // 
            this.listView_data.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_data.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.item,
            this.value});
            this.listView_data.FullRowSelect = true;
            this.listView_data.Location = new System.Drawing.Point(307, 84);
            this.listView_data.Name = "listView_data";
            this.listView_data.Size = new System.Drawing.Size(174, 255);
            this.listView_data.TabIndex = 6;
            this.listView_data.UseCompatibleStateImageBehavior = false;
            this.listView_data.View = System.Windows.Forms.View.Details;
            this.listView_data.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_data_MouseDoubleClick);
            // 
            // item
            // 
            this.item.Text = "項目";
            this.item.Width = 90;
            // 
            // value
            // 
            this.value.Text = "值";
            this.value.Width = 80;
            // 
            // ToolTipContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView_data);
            this.Controls.Add(this.listView_plc);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.labelCounter);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.labelTitle);
            this.Name = "ToolTipContent";
            this.Size = new System.Drawing.Size(508, 384);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label labelCounter;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.ListView listView_plc;
        private System.Windows.Forms.ListView listView_data;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader ip;
        private System.Windows.Forms.ColumnHeader port;
        private System.Windows.Forms.ColumnHeader item;
        private System.Windows.Forms.ColumnHeader value;
    }
}
