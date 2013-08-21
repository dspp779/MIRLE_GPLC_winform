namespace MIRLE_GPLC.form
{
    partial class ToolTipContentContainer
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
            this.projectDataInputControl1 = new MIRLE_GPLC.form.ProjectDataInputControl();
            this.projectDataView = new MIRLE_GPLC.form.ProjectDataView();
            this.button_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // projectDataInputControl1
            // 
            this.projectDataInputControl1.AutoSize = true;
            this.projectDataInputControl1.Location = new System.Drawing.Point(8, 8);
            this.projectDataInputControl1.Name = "projectDataInputControl1";
            this.projectDataInputControl1.Size = new System.Drawing.Size(289, 360);
            this.projectDataInputControl1.TabIndex = 1;
            this.projectDataInputControl1.Visible = false;
            // 
            // projectDataView
            // 
            this.projectDataView.Location = new System.Drawing.Point(8, 8);
            this.projectDataView.Name = "projectDataView";
            this.projectDataView.Size = new System.Drawing.Size(522, 356);
            this.projectDataView.TabIndex = 0;
            this.projectDataView.Visible = false;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(455, 368);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 2;
            this.button_close.Text = "close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // ToolTipContentContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.projectDataInputControl1);
            this.Controls.Add(this.projectDataView);
            this.Name = "ToolTipContentContainer";
            this.Size = new System.Drawing.Size(544, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProjectDataView projectDataView;
        private ProjectDataInputControl projectDataInputControl1;
        private System.Windows.Forms.Button button_close;
    }
}
