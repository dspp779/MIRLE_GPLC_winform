namespace MIRLE_GPLC.form
{
    partial class AuthForm
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
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.textBox_pass = new System.Windows.Forms.TextBox();
            this.label_id = new System.Windows.Forms.Label();
            this.label_pass = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.label_help = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_id
            // 
            this.textBox_id.Location = new System.Drawing.Point(61, 10);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(163, 22);
            this.textBox_id.TabIndex = 0;
            // 
            // textBox_pass
            // 
            this.textBox_pass.Location = new System.Drawing.Point(61, 38);
            this.textBox_pass.Name = "textBox_pass";
            this.textBox_pass.PasswordChar = '*';
            this.textBox_pass.Size = new System.Drawing.Size(163, 22);
            this.textBox_pass.TabIndex = 1;
            // 
            // label_id
            // 
            this.label_id.AutoSize = true;
            this.label_id.Location = new System.Drawing.Point(13, 13);
            this.label_id.Name = "label_id";
            this.label_id.Size = new System.Drawing.Size(17, 12);
            this.label_id.TabIndex = 2;
            this.label_id.Text = "ID";
            // 
            // label_pass
            // 
            this.label_pass.AutoSize = true;
            this.label_pass.Location = new System.Drawing.Point(12, 41);
            this.label_pass.Name = "label_pass";
            this.label_pass.Size = new System.Drawing.Size(48, 12);
            this.label_pass.TabIndex = 3;
            this.label_pass.Text = "Password";
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ok.Location = new System.Drawing.Point(24, 89);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 4;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_cancel.Location = new System.Drawing.Point(139, 89);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // label_help
            // 
            this.label_help.AutoSize = true;
            this.label_help.ForeColor = System.Drawing.Color.Red;
            this.label_help.Location = new System.Drawing.Point(13, 65);
            this.label_help.Name = "label_help";
            this.label_help.Size = new System.Drawing.Size(0, 12);
            this.label_help.TabIndex = 6;
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 118);
            this.Controls.Add(this.label_help);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.label_pass);
            this.Controls.Add(this.label_id);
            this.Controls.Add(this.textBox_pass);
            this.Controls.Add(this.textBox_id);
            this.Name = "AuthForm";
            this.Text = "Auth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.TextBox textBox_pass;
        private System.Windows.Forms.Label label_id;
        private System.Windows.Forms.Label label_pass;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label_help;
    }
}