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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.latlngLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox_addr = new System.Windows.Forms.GroupBox();
            this.textBox_net_IP = new System.Windows.Forms.TextBox();
            this.label_net_port = new System.Windows.Forms.Label();
            this.label_net_IP = new System.Windows.Forms.Label();
            this.textBox_net_port = new System.Windows.Forms.TextBox();
            this.textBox_net_ID = new System.Windows.Forms.TextBox();
            this.label_net_ID = new System.Windows.Forms.Label();
            this.groupBox_case = new System.Windows.Forms.GroupBox();
            this.textBox_case_addr = new System.Windows.Forms.TextBox();
            this.label_case_addr = new System.Windows.Forms.Label();
            this.textBox_case_Name = new System.Windows.Forms.TextBox();
            this.label_case_Name = new System.Windows.Forms.Label();
            this.textBox_case_ID = new System.Windows.Forms.TextBox();
            this.label_case_ID = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox_location = new System.Windows.Forms.GroupBox();
            this.label_latlng_lng = new System.Windows.Forms.Label();
            this.textBox_latlng_lng = new System.Windows.Forms.TextBox();
            this.label_latlng_lat = new System.Windows.Forms.Label();
            this.textBox_latlng_lat = new System.Windows.Forms.TextBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.InputButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gMap = new GMap.NET.WindowsForms.GMapControl();
            this.statusStrip1.SuspendLayout();
            this.groupBox_addr.SuspendLayout();
            this.groupBox_case.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox_location.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.latlngLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 660);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1035, 22);
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
            this.latlngLabel.Size = new System.Drawing.Size(1020, 17);
            this.latlngLabel.Spring = true;
            this.latlngLabel.Text = "latlngLabel";
            // 
            // groupBox_addr
            // 
            this.groupBox_addr.Controls.Add(this.textBox_net_IP);
            this.groupBox_addr.Controls.Add(this.label_net_port);
            this.groupBox_addr.Controls.Add(this.label_net_IP);
            this.groupBox_addr.Controls.Add(this.textBox_net_port);
            this.groupBox_addr.Controls.Add(this.textBox_net_ID);
            this.groupBox_addr.Controls.Add(this.label_net_ID);
            this.groupBox_addr.Location = new System.Drawing.Point(3, 10);
            this.groupBox_addr.Name = "groupBox_addr";
            this.groupBox_addr.Size = new System.Drawing.Size(196, 80);
            this.groupBox_addr.TabIndex = 6;
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
            // groupBox_case
            // 
            this.groupBox_case.Controls.Add(this.textBox_case_addr);
            this.groupBox_case.Controls.Add(this.label_case_addr);
            this.groupBox_case.Controls.Add(this.textBox_case_Name);
            this.groupBox_case.Controls.Add(this.label_case_Name);
            this.groupBox_case.Controls.Add(this.textBox_case_ID);
            this.groupBox_case.Controls.Add(this.label_case_ID);
            this.groupBox_case.Location = new System.Drawing.Point(3, 105);
            this.groupBox_case.Name = "groupBox_case";
            this.groupBox_case.Size = new System.Drawing.Size(243, 177);
            this.groupBox_case.TabIndex = 7;
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox_location);
            this.panel1.Controls.Add(this.resetButton);
            this.panel1.Controls.Add(this.InputButton);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.groupBox_case);
            this.panel1.Controls.Add(this.groupBox_addr);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(616, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(419, 657);
            this.panel1.TabIndex = 3;
            // 
            // groupBox_location
            // 
            this.groupBox_location.Controls.Add(this.label_latlng_lng);
            this.groupBox_location.Controls.Add(this.textBox_latlng_lng);
            this.groupBox_location.Controls.Add(this.label_latlng_lat);
            this.groupBox_location.Controls.Add(this.textBox_latlng_lat);
            this.groupBox_location.Location = new System.Drawing.Point(203, 10);
            this.groupBox_location.Name = "groupBox_location";
            this.groupBox_location.Size = new System.Drawing.Size(209, 80);
            this.groupBox_location.TabIndex = 10;
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
            this.resetButton.Location = new System.Drawing.Point(297, 188);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 9;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // InputButton
            // 
            this.InputButton.Location = new System.Drawing.Point(297, 140);
            this.InputButton.Name = "InputButton";
            this.InputButton.Size = new System.Drawing.Size(75, 23);
            this.InputButton.TabIndex = 8;
            this.InputButton.Text = "Add";
            this.InputButton.UseVisualStyleBackColor = true;
            this.InputButton.Click += new System.EventHandler(this.InputButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value});
            this.dataGridView1.Location = new System.Drawing.Point(150, 381);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 165;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(267, 274);
            this.dataGridView1.TabIndex = 4;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
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
            this.gMap.Location = new System.Drawing.Point(0, 0);
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
            this.gMap.Size = new System.Drawing.Size(615, 657);
            this.gMap.TabIndex = 0;
            this.gMap.Zoom = 8D;
            this.gMap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gMap_OnMarkerClick);
            this.gMap.Load += new System.EventHandler(this.MainForm_Load);
            this.gMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseClick);
            this.gMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseDoubleClick);
            this.gMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseDown);
            this.gMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseMove);
            this.gMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gMap_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 682);
            this.Controls.Add(this.gMap);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox_addr.ResumeLayout(false);
            this.groupBox_addr.PerformLayout();
            this.groupBox_case.ResumeLayout(false);
            this.groupBox_case.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox_location.ResumeLayout(false);
            this.groupBox_location.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel latlngLabel;
        private System.Windows.Forms.GroupBox groupBox_addr;
        private System.Windows.Forms.TextBox textBox_net_IP;
        private System.Windows.Forms.Label label_net_port;
        private System.Windows.Forms.Label label_net_IP;
        private System.Windows.Forms.TextBox textBox_net_port;
        private System.Windows.Forms.TextBox textBox_net_ID;
        private System.Windows.Forms.Label label_net_ID;
        private System.Windows.Forms.GroupBox groupBox_case;
        private System.Windows.Forms.TextBox textBox_case_addr;
        private System.Windows.Forms.Label label_case_addr;
        private System.Windows.Forms.TextBox textBox_case_Name;
        private System.Windows.Forms.Label label_case_Name;
        private System.Windows.Forms.TextBox textBox_case_ID;
        private System.Windows.Forms.Label label_case_ID;
        private System.Windows.Forms.Panel panel1;
        private GMap.NET.WindowsForms.GMapControl gMap;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button InputButton;
        private System.Windows.Forms.GroupBox groupBox_location;
        private System.Windows.Forms.Label label_latlng_lng;
        private System.Windows.Forms.TextBox textBox_latlng_lng;
        private System.Windows.Forms.Label label_latlng_lat;
        private System.Windows.Forms.TextBox textBox_latlng_lat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}

