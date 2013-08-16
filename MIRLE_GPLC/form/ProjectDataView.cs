using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MIRLE_GPLC.form.marker;
using MIRLE_GPLC.Model;

namespace MIRLE_GPLC.form
{
    internal partial class ProjectDataView : UserControl
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

        public ProjectDataView()
        {
            InitializeComponent();
        }

        public void init(List<ProjectMarker> Markers)
        {
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

        #region -- Refresh List Method --

        private void refreshPLCList()
        {
            ProjectData p = markers[_shownMarker].ProjectData;
            p.reload();

            listView_plc.Items.Clear();
            listView_data.Items.Clear();
            foreach (PLC plc in p.plcs)
            {
                ListViewItem item = new ListViewItem(plc.alias);
                item.SubItems.Add(plc.netid.ToString());
                item.SubItems.Add(plc.ip);
                item.SubItems.Add(plc.port.ToString());
                listView_plc.Items.Add(item);
            }
            lastSelectedPLC = null;
        }

        private void refreshItemList(PLC plc)
        {
            listView_data.Items.Clear();
            foreach (Record r in plc.dataFields)
            {
                ListViewItem item = new ListViewItem(r.alias);
                item.SubItems.Add(r.getVal());
                listView_data.Items.Add(item);
            }
        }

        #endregion

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
                refreshItemList(lastSelectedPLC);
            }
        }

        private void listView_plc_DoubleClick(object sender, EventArgs e)
        {
            int index = listView_plc.SelectedIndices.Count > 0 ? listView_plc.SelectedIndices[0] : -1;
            PLCEditDialog(index);
        }

        private void listView_data_DoubleClick(object sender, EventArgs e)
        {
            int index = listView_data.SelectedIndices.Count > 0 ? listView_data.SelectedIndices[0] : -1;
            DataFieldDialog(index);
        }


        #region -- mouse double click event for non-item area --

        private void listView_plc_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                listView_plc_DoubleClick(sender, e);
            }
        }

        private void listView_data_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                listView_data_DoubleClick(sender, e);
            }
        }

        #endregion

        #region -- input dialog --
        
        private void PLCEditDialog(int index)
        {
            ProjectData project = markers[_shownMarker].ProjectData;

            PLCForm f = (index >= 0) ?
                new PLCForm(project, project.plcs[index]) : new PLCForm(project);
            
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.plc.id > 0)
                {
                    ModelUtil.inputPLC(f.plc, project.id);
                }
                else
                {
                    ModelUtil.insertPLC(f.plc, project.id);
                }
            }
        }
        private void DataFieldDialog(int index)
        {
            if (lastSelectedPLC == null)
            {
                return;
            }

            DataFieldForm f = (index >= 0) ?
                new DataFieldForm(lastSelectedPLC.dataFields[index]) : new DataFieldForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.record.id > 0)
                {
                    ModelUtil.inputItem(f.record);
                }
                else
                {
                    ModelUtil.insertItem(f.record, lastSelectedPLC.id);
                }
            }
        }

        #endregion

    }
}
