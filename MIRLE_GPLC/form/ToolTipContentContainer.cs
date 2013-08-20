﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperContextMenu;
using GMap.NET.WindowsForms;
using MIRLE_GPLC.form.marker;
using MIRLE_GPLC.Model;

namespace MIRLE_GPLC.form
{
    internal partial class ToolTipContentContainer : SuperContextMenu.PopedContainer
    {
        public ToolTipContentContainer(GMapMarker marker)
        {
            InitializeComponent();
            this.Size = new Size(300, 370);
            projectDataInputControl1.init(marker);
        }

        public ToolTipContentContainer(List<ProjectMarker> markers)
        {
            InitializeComponent();
            this.Size = new Size(544, 384);
            projectDataView.init(markers);
        }
    }
}