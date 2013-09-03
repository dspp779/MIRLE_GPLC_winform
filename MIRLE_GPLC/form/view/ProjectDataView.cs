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
using Modbus.TCP;
using System.Threading;
using System.Net.Sockets;
using System.Diagnostics;

namespace MIRLE_GPLC.form
{
    internal partial class ProjectDataView : UserControl
    {
        private List<ProjectMarker> markers = new List<ProjectMarker>();
        private int _shownMarker = -1;
        private int _lastSelectedPLC;

        private int ShownMarker
        {
            get { return _shownMarker; }
            set
            {
                if (value == _shownMarker)
                {
                    return;
                }
                else if (value < 0)
                {
                    value = markers.Count - 1;
                }
                else if (value >= markers.Count)
                {
                    value = 0;
                }

                _shownMarker = value;
                lastSelectedPLC = -1;

                labelTitle.Text = markers[_shownMarker].ProjectData.name;
                labelText.Text = markers[_shownMarker].ProjectData.addr;

                labelCounter.Text = (_shownMarker + 1).ToString() + " of " + markers.Count;

                Refresh();
            }
        }
        // index of last selected plc 
        private int lastSelectedPLC
        {
            get
            {
                return (_lastSelectedPLC < markers[_shownMarker].ProjectData.plcs.Count)
                    ? _lastSelectedPLC : markers[_shownMarker].ProjectData.plcs.Count - 1;
            }
            set
            {
                /*if (value == _lastSelectedPLC)
                {
                    return;
                }*/
                _lastSelectedPLC = value;

                // index of the last selected plc should not be over the list range
                Debug.Assert(_lastSelectedPLC < markers[_shownMarker].ProjectData.plcs.Count);
                // refresh tag list corresponding to the selected plc
                refreshTagList();
            }
        }

        public ProjectDataView()
        {
            InitializeComponent();
        }

        public void init(List<ProjectMarker> markers)
        {
            if (markers.Count == 0)
            {
                return;
            }

            // Assign the list of markers
            this.markers = markers;

            // Sort the list of markers
            this.markers.Sort(
                delegate(ProjectMarker m1, ProjectMarker m2)
                {
                    return m1.ProjectData.id.CompareTo(m2.ProjectData.id);
                }
            );

            // Show the first marker
            ShownMarker = 0;

            buttonBack.Enabled = buttonNext.Enabled = (markers.Count != 1);
            this.Show();
        }

        #region -- Refresh List Method --

        public override void Refresh()
        {
            // hide input controls
            plcInputControl.Hide();
            tagInputControl1.Hide();
            // show listViews
            this.listView_tag.Show();
            this.listView_plc.Show();
            // refresh
            refreshPLCList();
            base.Refresh();
        }
        private void refreshPLCList()
        {
            // current project
            ProjectData project = markers[_shownMarker].ProjectData;
            // loading plc list of the project
            project.reload();

            // clear the listView
            listView_plc.Items.Clear();
            listView_tag.Items.Clear();
            // put plc list into listView
            foreach (PLC plc in project.plcs)
            {
                // set listView item 
                ListViewItem item = new ListViewItem(plc.alias);
                item.SubItems.Add(plc.netid.ToString());
                item.SubItems.Add(plc.ip);
                item.SubItems.Add(plc.port.ToString());
                // add to listView
                listView_plc.Items.Add(item);
            }
            // if last selection, refresh tag list as well
            if (lastSelectedPLC >= 0)
            {
                listView_plc.Items[lastSelectedPLC].Selected = true;
            }
        }
        private void refreshTagList()
        {
            // clear tag listView
            listView_tag.Items.Clear();
            // if no plc seleted, no need to refresh tag list
            if (lastSelectedPLC < 0)
            {
                return;
            }

            // check if index is in correct range
            Debug.Assert(lastSelectedPLC < markers[_shownMarker].ProjectData.plcs.Count);

            // last selected plc
            PLC plc = markers[_shownMarker].ProjectData.plcs[lastSelectedPLC];
            // load tag list of the plc
            plc.reload();
            // refresh the listView
            foreach (Tag r in plc.tags)
            {
                ListViewItem item = new ListViewItem(r.alias);
                item.SubItems.Add("????");
                listView_tag.Items.Add(item);
            }

            // lauch modbus TCP worker responsible for refresh value of the tag
            Utility.modbusWorkerPool.lauchViewWorker(plc);
        }

        #endregion

        #region -- Event Handler --

        // button clicks for changing shown marker
        private void buttonBack_Click(object sender, EventArgs e)
        {
            ShownMarker--;
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            ShownMarker++;
        }

        // selection change in plc list view, selected plc would be saved
        private void listView_plc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_plc.SelectedIndices.Count > 0)
            {
                // set last selected plc
                lastSelectedPLC = listView_plc.SelectedIndices[0];
                // avoid no selection
                listView_plc.HideSelection = false;
            }
        }
        // double click event for plc listView : modify or add a plc
        private void listView_plc_DoubleClick(object sender, EventArgs e)
        {
            // check if current user has the authority to do this operation
            if (GPLC.AuthVerify(Security.GPLCAuthority.Administrator))
            {
                // get the index of selected plc, -1 if no selection
                int index = listView_plc.SelectedIndices.Count > 0 ? listView_plc.SelectedIndices[0] : -1;
                // show plc edit control
                PLCEditControl(index);
            }
        }
        private void listView_tag_DoubleClick(object sender, EventArgs e)
        {
            // check if current user has the authority to do this operation
            if (GPLC.AuthVerify(Security.GPLCAuthority.Administrator))
            {
                // get the index of selected plc, -1 if no selection
                int index = listView_tag.SelectedIndices.Count > 0 ? listView_tag.SelectedIndices[0] : -1;
                // show tag edit control
                TagEditControl(index);
            }
        }

        private void listView_plc_KeyDown(object sender, KeyEventArgs e)
        {
            // delete operation for plc
            if (e.KeyCode == Keys.Delete && lastSelectedPLC >= 0)
            {
                // check if current user has the authority to do this operation
                if (GPLC.AuthVerify(Security.GPLCAuthority.Administrator))
                {
                    // index of the last selected plc should not be over the list range
                    Debug.Assert(lastSelectedPLC < markers[_shownMarker].ProjectData.plcs.Count);

                    // last selected plc
                    PLC plc = markers[_shownMarker].ProjectData.plcs[lastSelectedPLC];
                    // db operation
                    ModelUtil.deletePLC(plc.id);
                    // refresh list
                    refreshPLCList();
                }
            }
        }
        private void listView_tag_KeyDown(object sender, KeyEventArgs e)
        {
            // delete operation for tag
            if (e.KeyCode == Keys.Delete && lastSelectedPLC >= 0)
            {
                // check if current user has the authority to do this operation
                if (GPLC.AuthVerify(Security.GPLCAuthority.Administrator))
                {
                    // index of the last selected plc should not be over the list range
                    Debug.Assert(lastSelectedPLC < markers[_shownMarker].ProjectData.plcs.Count);

                    if (listView_tag.SelectedIndices.Count > 0)
                    {
                        // last selected plc
                        PLC plc = markers[_shownMarker].ProjectData.plcs[lastSelectedPLC];
                        // last selected tag
                        int index = listView_tag.SelectedIndices[0];
                        // db operation
                        ModelUtil.deleteTag(plc.tags[index].id);
                        // refresh tag list
                        refreshTagList();
                    }
                }
            }
        }
        private void listView_plc_MouseUp(object sender, MouseEventArgs e)
        {
            if (listView_plc.Visible && listView_plc.SelectedIndices.Count <= 0)
            {
                // set no selection
                lastSelectedPLC = -1;
                // hide selected when no selection
                listView_plc.HideSelection = true;
            }
        }

        #endregion

        #region -- mouse double click event for non-item area --

        /* Since there's no double click event triggered in non-item area
         * for an listView control, extra effort needs to be made to achieve
         * triggering double clicked event in non-item area for lisView control
         * */

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
                listView_tag_DoubleClick(sender, e);
            }
        }

        #endregion

        #region -- input control --
        
        private void PLCEditControl(int index)
        {
            this.listView_tag.Hide();
            //this.listView_plc.Hide();

            // current project
            ProjectData project = markers[ShownMarker].ProjectData;

            // add a plc
            if (index < 0)
            {
                plcInputControl.init(project);
            }
            // modify a plc
            else
            {
                plcInputControl.init(project, project.plcs[index]);
            }
        }
        private void TagEditControl(int index)
        {
            if (lastSelectedPLC < 0)
            {
                return;
            }

            //this.listView_tag.Hide();
            this.listView_plc.Hide();

            // last selected plc
            PLC plc = markers[ShownMarker].ProjectData.plcs[lastSelectedPLC];

            // add a tag
            if (index < 0)
            {
                tagInputControl1.init(plc.id);
            }
            // modify a tag
            else
            {
                Debug.Assert(index < plc.tags.Count);
                tagInputControl1.init(plc.id, plc.tags[index]);
            }
        }

        #endregion

        // tag list refresh delegate, for modbus TCP worker's modification
        public void RefreshTagList(string[] list)
        {
            Invoke(new TagHandler(RefreshTag), new Object[] { list });
        }
        delegate void TagHandler(string[] list);
        private void RefreshTag(string[] list)
        {
            int i = 0;
            foreach (string str in list)
            {
                if (listView_tag.Items.Count > i)
                {
                    listView_tag.Items[i++].SubItems[1].Text = str;
                }
            }
        }

    }
}
