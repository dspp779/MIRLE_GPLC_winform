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
        public ToolTipContentContainer()
        {
            InitializeComponent();
        }

        public ToolTipContentContainer(List<ProjectMarker> Markers)
        {
            InitializeComponent();
            toolTipContent1.init(Markers);
        }
    }
}
