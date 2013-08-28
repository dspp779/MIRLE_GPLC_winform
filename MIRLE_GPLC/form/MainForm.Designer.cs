namespace MIRLE_GPLC
{
    partial class MainForm
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (markersOverlay != null)
                {
                    markersOverlay.Dispose();
                }
                if (gMap != null)
                {
                    gMap.Dispose();
                }
                if (currMarker != null)
                {
                    currMarker.Dispose();
                }
                if (ttc != null && !ttc.IsDisposed)
                {
                    ttc.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.latlngLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.AuthStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox_location = new System.Windows.Forms.GroupBox();
            this.label_latlng_lng = new System.Windows.Forms.Label();
            this.textBox_latlng_lng = new System.Windows.Forms.TextBox();
            this.label_latlng_lat = new System.Windows.Forms.Label();
            this.textBox_latlng_lat = new System.Windows.Forms.TextBox();
            this.InputButton = new System.Windows.Forms.Button();
            this.groupBox_case = new System.Windows.Forms.GroupBox();
            this.textBox_project_id = new System.Windows.Forms.TextBox();
            this.label_project_id = new System.Windows.Forms.Label();
            this.richTextBox_project_addr = new System.Windows.Forms.RichTextBox();
            this.label_project_addr = new System.Windows.Forms.Label();
            this.textBox_project_name = new System.Windows.Forms.TextBox();
            this.label_project_name = new System.Windows.Forms.Label();
            this.gMap = new GMap.NET.WindowsForms.GMapControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.權限ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_anonymous = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_auth = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox_location.SuspendLayout();
            this.groupBox_case.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.latlngLabel,
            this.AuthStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 660);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(870, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // latlngLabel
            // 
            this.latlngLabel.Name = "latlngLabel";
            this.latlngLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.latlngLabel.RightToLeftAutoMirrorImage = true;
            this.latlngLabel.Size = new System.Drawing.Size(821, 17);
            this.latlngLabel.Spring = true;
            this.latlngLabel.Text = "latlngLabel";
            // 
            // AuthStatusLabel
            // 
            this.AuthStatusLabel.Name = "AuthStatusLabel";
            this.AuthStatusLabel.Size = new System.Drawing.Size(34, 17);
            this.AuthStatusLabel.Text = "Auth";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox_location);
            this.panel1.Controls.Add(this.InputButton);
            this.panel1.Controls.Add(this.groupBox_case);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(616, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 635);
            this.panel1.TabIndex = 3;
            // 
            // groupBox_location
            // 
            this.groupBox_location.Controls.Add(this.label_latlng_lng);
            this.groupBox_location.Controls.Add(this.textBox_latlng_lng);
            this.groupBox_location.Controls.Add(this.label_latlng_lat);
            this.groupBox_location.Controls.Add(this.textBox_latlng_lat);
            this.groupBox_location.Location = new System.Drawing.Point(4, 193);
            this.groupBox_location.Name = "groupBox_location";
            this.groupBox_location.Size = new System.Drawing.Size(243, 80);
            this.groupBox_location.TabIndex = 10;
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
            // InputButton
            // 
            this.InputButton.Location = new System.Drawing.Point(87, 279);
            this.InputButton.Name = "InputButton";
            this.InputButton.Size = new System.Drawing.Size(75, 23);
            this.InputButton.TabIndex = 8;
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
            this.groupBox_case.Controls.Add(this.textBox_project_name);
            this.groupBox_case.Controls.Add(this.label_project_name);
            this.groupBox_case.Location = new System.Drawing.Point(3, 10);
            this.groupBox_case.Name = "groupBox_case";
            this.groupBox_case.Size = new System.Drawing.Size(243, 177);
            this.groupBox_case.TabIndex = 7;
            this.groupBox_case.TabStop = false;
            this.groupBox_case.Text = "Case";
            // 
            // textBox_project_id
            // 
            this.textBox_project_id.Location = new System.Drawing.Point(6, 36);
            this.textBox_project_id.MaxLength = 100;
            this.textBox_project_id.Name = "textBox_project_id";
            this.textBox_project_id.Size = new System.Drawing.Size(223, 22);
            this.textBox_project_id.TabIndex = 12;
            // 
            // label_project_id
            // 
            this.label_project_id.AutoSize = true;
            this.label_project_id.Location = new System.Drawing.Point(6, 18);
            this.label_project_id.Name = "label_project_id";
            this.label_project_id.Size = new System.Drawing.Size(29, 12);
            this.label_project_id.TabIndex = 13;
            this.label_project_id.Text = "案號";
            // 
            // richTextBox_project_addr
            // 
            this.richTextBox_project_addr.Location = new System.Drawing.Point(6, 119);
            this.richTextBox_project_addr.Name = "richTextBox_project_addr";
            this.richTextBox_project_addr.Size = new System.Drawing.Size(223, 48);
            this.richTextBox_project_addr.TabIndex = 11;
            this.richTextBox_project_addr.Text = "";
            // 
            // label_project_addr
            // 
            this.label_project_addr.AutoSize = true;
            this.label_project_addr.Location = new System.Drawing.Point(6, 104);
            this.label_project_addr.Name = "label_project_addr";
            this.label_project_addr.Size = new System.Drawing.Size(29, 12);
            this.label_project_addr.TabIndex = 11;
            this.label_project_addr.Text = "地址";
            // 
            // textBox_project_name
            // 
            this.textBox_project_name.Location = new System.Drawing.Point(6, 79);
            this.textBox_project_name.MaxLength = 100;
            this.textBox_project_name.Name = "textBox_project_name";
            this.textBox_project_name.Size = new System.Drawing.Size(223, 22);
            this.textBox_project_name.TabIndex = 8;
            // 
            // label_project_name
            // 
            this.label_project_name.AutoSize = true;
            this.label_project_name.Location = new System.Drawing.Point(6, 61);
            this.label_project_name.Name = "label_project_name";
            this.label_project_name.Size = new System.Drawing.Size(29, 12);
            this.label_project_name.TabIndex = 9;
            this.label_project_name.Text = "案名";
            // 
            // gMap
            // 
            this.gMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMap.BackColor = System.Drawing.SystemColors.Control;
            this.gMap.Bearing = 0F;
            this.gMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gMap.CanDragMap = true;
            this.gMap.EmptyTileColor = System.Drawing.Color.RoyalBlue;
            this.gMap.GrayScaleMode = false;
            this.gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap.LevelsKeepInMemmory = 5;
            this.gMap.Location = new System.Drawing.Point(0, 22);
            this.gMap.MarkersEnabled = true;
            this.gMap.MaxZoom = 17;
            this.gMap.MinZoom = 5;
            this.gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.gMap.Name = "gMap";
            this.gMap.NegativeMode = false;
            this.gMap.PolygonsEnabled = true;
            this.gMap.RetryLoadTile = 0;
            this.gMap.RoutesEnabled = true;
            this.gMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMap.ShowTileGridLines = false;
            this.gMap.Size = new System.Drawing.Size(615, 635);
            this.gMap.TabIndex = 0;
            this.gMap.Zoom = 8D;
            this.gMap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gMap_OnMarkerClick);
            this.gMap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gMap_OnMarkerEnter);
            this.gMap.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.gMap_OnMarkerLeave);
            this.gMap.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.gMap_OnMapZoomChanged);
            this.gMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseClick);
            this.gMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseDoubleClick);
            this.gMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseDown);
            this.gMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseMove);
            this.gMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.檔案ToolStripMenuItem,
            this.權限ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(870, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 檔案ToolStripMenuItem
            // 
            this.檔案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem});
            this.檔案ToolStripMenuItem.Name = "檔案ToolStripMenuItem";
            this.檔案ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.檔案ToolStripMenuItem.Text = "檔案";
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.SaveToolStripMenuItem.Text = "Save as";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // 權限ToolStripMenuItem
            // 
            this.權限ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_anonymous,
            this.ToolStripMenuItem_auth});
            this.權限ToolStripMenuItem.Name = "權限ToolStripMenuItem";
            this.權限ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.權限ToolStripMenuItem.Text = "權限";
            // 
            // ToolStripMenuItem_anonymous
            // 
            this.ToolStripMenuItem_anonymous.Name = "ToolStripMenuItem_anonymous";
            this.ToolStripMenuItem_anonymous.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_anonymous.Text = "唯讀模式";
            this.ToolStripMenuItem_anonymous.Click += new System.EventHandler(this.ToolStripMenuItem_anonymous_Click);
            // 
            // ToolStripMenuItem_auth
            // 
            this.ToolStripMenuItem_auth.Name = "ToolStripMenuItem_auth";
            this.ToolStripMenuItem_auth.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_auth.Text = "認證";
            this.ToolStripMenuItem_auth.Click += new System.EventHandler(this.ToolStripMenuItem_auth_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 682);
            this.Controls.Add(this.gMap);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MIRLE GPLC";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox_location.ResumeLayout(false);
            this.groupBox_location.PerformLayout();
            this.groupBox_case.ResumeLayout(false);
            this.groupBox_case.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel latlngLabel;
        private System.Windows.Forms.Panel panel1;
        private GMap.NET.WindowsForms.GMapControl gMap;
        private System.Windows.Forms.GroupBox groupBox_location;
        private System.Windows.Forms.Label label_latlng_lng;
        private System.Windows.Forms.TextBox textBox_latlng_lng;
        private System.Windows.Forms.Label label_latlng_lat;
        private System.Windows.Forms.TextBox textBox_latlng_lat;
        private System.Windows.Forms.Button InputButton;
        private System.Windows.Forms.GroupBox groupBox_case;
        private System.Windows.Forms.Label label_project_addr;
        private System.Windows.Forms.TextBox textBox_project_name;
        private System.Windows.Forms.Label label_project_name;
        private System.Windows.Forms.ToolStripStatusLabel AuthStatusLabel;
        private System.Windows.Forms.RichTextBox richTextBox_project_addr;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 檔案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 權限ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_anonymous;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_auth;
        private System.Windows.Forms.TextBox textBox_project_id;
        private System.Windows.Forms.Label label_project_id;
    }
}

