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

namespace MIRLE_GPLC.form
{
    internal partial class ToolTipContent : SuperContextMenu.PopedContainer
    {
        private List<ProjectMarker> markers = new List<ProjectMarker>();
        private int _shownMarker = 0;

        private int ShownMarker
        {
            get { return _shownMarker; }
            set
            {
                if (value < 0)
                    value = markers.Count - 1;
                else if (value >= markers.Count)
                    value = 0;

                _shownMarker = value;

                labelTitle.Text = markers[_shownMarker].ProjectData.name;
                labelText.Text = markers[_shownMarker].ProjectData.addr;

                labelCounter.Text = (_shownMarker + 1).ToString() + " of " + markers.Count;
            }
        }

        public ToolTipContent(List<ProjectMarker> Markers)
        {
            InitializeComponent();

            if (Markers.Count == 0)
                return;

            // Assign the list of markers
            this.markers = Markers;

            // Sort the list of markers if you want to
            // Exclude this if you dont care about order
            markers.Sort(
                delegate(ProjectMarker m1, ProjectMarker m2)
                {
                    return m1.ProjectData.id.CompareTo(m2.ProjectData.id);
                }
            );

            // Show the first marker
            buttonBack.Enabled = buttonNext.Enabled = (markers.Count != 1);
            ShownMarker = 0;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ShownMarker--;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            ShownMarker++;
        }
    }
}
