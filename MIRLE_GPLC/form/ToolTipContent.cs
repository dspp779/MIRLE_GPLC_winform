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
    internal partial class ToolTipContent : SuperContextMenu.PopedContainer
    {
        private List<ProjectMarker> markers = new List<ProjectMarker>();
        private int _shownMarker = 0;
        private PLC lastSelectedPLC;

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

                refreshPLCList();
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

        private void refreshPLCList()
        {
            ProjectData p = markers[_shownMarker].ProjectData;
            p.loadPLC();

            listView_plc.Items.Clear();
            listView_data.Items.Clear();
            foreach (PLC plc in p.plcs)
            {
                ListViewItem item = new ListViewItem("PLC");
                item.SubItems.Add(plc.id.ToString());
                item.SubItems.Add(plc.ip);
                item.SubItems.Add(plc.port.ToString());
                listView_plc.Items.Add(item);
            }
        }

        private void refreshDataField(PLC plc)
        {
            listView_data.Items.Clear();
            foreach (Record r in plc.dataFields)
            {
                ListViewItem item = new ListViewItem(r.alias);
                item.SubItems.Add(r.getVal());
                listView_data.Items.Add(item);
            }
            if (plc.dataFields.Count == 0)
            {
                ListViewItem item = new ListViewItem("none");
                listView_data.Items.Add(item);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ShownMarker--;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            ShownMarker++;
        }

        private void listView_plc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_plc.SelectedIndices.Count > 0)
            {
                lastSelectedPLC = markers[_shownMarker].ProjectData.plcs[listView_plc.SelectedIndices[0]];
                refreshDataField(lastSelectedPLC);
            }
        }

        private void listView_plc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listView_plc.SelectedIndices.Count > 0 ? listView_plc.SelectedIndices[0] : -1;
            PLCEditDialog(index);
            //refreshPLCList();
        }

        private void listView_data_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listView_data.SelectedIndices.Count > 0 ? listView_data.SelectedIndices[0] : -1;
            DataFieldDialog(index);
            //refreshPLCList();
        }

        private void PLCEditDialog(int index)
        {
            ProjectData project = markers[_shownMarker].ProjectData;
            PLC plc = project.plcs[index];

            PLCForm f = (index >= 0) ?
                new PLCForm(project, plc) : new PLCForm(project);
            
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                ModelUtil.inputPLC(f.plc, project.id);
            }
        }

        private void DataFieldDialog(int index)
        {
            DataFieldForm f = (index >= 0) ?
                new DataFieldForm(lastSelectedPLC.dataFields[index]) : new DataFieldForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                ModelUtil.inputDataField(f.record);
            }
        }

    }
}
