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
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(19, 16);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(28, 14);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "title";
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(19, 40);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(30, 14);
            this.labelText.TabIndex = 1;
            this.labelText.Text = "text";
            // 
            // labelCounter
            // 
            this.labelCounter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCounter.AutoSize = true;
            this.labelCounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCounter.Location = new System.Drawing.Point(240, 183);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(31, 14);
            this.labelCounter.TabIndex = 2;
            this.labelCounter.Text = "label";
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(158, 357);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 3;
            this.buttonBack.Text = "Previous";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(285, 357);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 4;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // ToolTipContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
    }
}
