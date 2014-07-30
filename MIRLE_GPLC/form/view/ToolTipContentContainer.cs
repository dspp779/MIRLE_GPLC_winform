using System;
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
            // Input(modify or add) project container size
            this.Size = new Size(305, 400);
            // init project data input control
            projectDataInputControl.init(marker);
        }

        public ToolTipContentContainer(List<ProjectMarker> markers)
        {
            InitializeComponent();
            // view project container size
            this.Size = new Size(540, 400);
            /* set project view control to modbus worker
             * so that it can modify project view contents
             *  */
            // init project view control
            projectDataView.init(markers);
        }

        private void ToolTipContentContainer_Load(object sender, EventArgs e)
        {
            //this.VisibleChanged += new EventHandler(ToolTipVisibleChanged);
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ToolTipVisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                //this.projectDataView.Dispose();
            }
        }

    }
}
