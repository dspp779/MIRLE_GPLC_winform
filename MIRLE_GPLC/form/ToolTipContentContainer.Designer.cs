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
            this.toolTipContent1 = new MIRLE_GPLC.form.ProjectDataView();
            this.SuspendLayout();
            // 
            // toolTipContent1
            // 
            this.toolTipContent1.AutoSize = true;
            this.toolTipContent1.Location = new System.Drawing.Point(13, 13);
            this.toolTipContent1.Name = "toolTipContent1";
            this.toolTipContent1.Size = new System.Drawing.Size(521, 356);
            this.toolTipContent1.TabIndex = 0;
            // 
            // ProjectToolTipContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolTipContent1);
            this.Name = "ProjectToolTipContent";
            this.Size = new System.Drawing.Size(544, 384);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProjectDataView toolTipContent1;









    }
}
