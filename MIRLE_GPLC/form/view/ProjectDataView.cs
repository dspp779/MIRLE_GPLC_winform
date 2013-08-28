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
        private static Master MBmaster;
        private static Thread modbusThread;

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
                if (MBmaster != null && MBmaster.connected)
                {
                    MBmaster.disconnect();
                    MBmaster = null;
                }
                if (modbusThread != null && modbusThread.IsAlive)
                {
                    modbusThread.Abort();
                    modbusThread = null;
                }

                _lastSelectedPLC = value;

                Debug.Assert(_lastSelectedPLC < markers[_shownMarker].ProjectData.plcs.Count);

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

            // Sort the list of markers if you want to
            // Exclude this if you dont care about order
            this.markers.Sort(
                delegate(ProjectMarker m1, ProjectMarker m2)
                {
                    return m1.ProjectData.id.CompareTo(m2.ProjectData.id);
                }
            );

            // Show the first marker
            buttonBack.Enabled = buttonNext.Enabled = (markers.Count != 1);
            ShownMarker = 0;

            this.Show();
        }

        #region -- Refresh List Method --

        public override void Refresh()
        {
            // hide input control
            plcInputControl.Hide();
            tagInputControl1.Hide();
            this.listView_tag.Show();
            this.listView_plc.Show();
            refreshPLCList();
            base.Refresh();
        }
        private void refreshPLCList()
        {
            ProjectData project = markers[_shownMarker].ProjectData;
            project.reload();

            listView_plc.Items.Clear();
            listView_tag.Items.Clear();
            foreach (PLC plc in project.plcs)
            {
                ListViewItem item = new ListViewItem(plc.alias);
                item.SubItems.Add(plc.netid.ToString());
                item.SubItems.Add(plc.ip);
                item.SubItems.Add(plc.port.ToString());
                listView_plc.Items.Add(item);
            }
            if (lastSelectedPLC >= 0)
            {
                listView_plc.Items[lastSelectedPLC].Selected = true;
            }
        }
        private void refreshTagList()
        {
            listView_tag.Items.Clear();
            if (lastSelectedPLC < 0)
            {
                return;
            }

            Debug.Assert(lastSelectedPLC < markers[_shownMarker].ProjectData.plcs.Count);

            PLC plc = markers[_shownMarker].ProjectData.plcs[lastSelectedPLC];
            plc.reload();
            foreach (Tag r in plc.tags)
            {
                ListViewItem item = new ListViewItem(r.alias);
                item.SubItems.Add("????");
                listView_tag.Items.Add(item);
            }

            // modbus worker
            if (modbusThread != null && modbusThread.IsAlive)
            {
                modbusThread.Abort();
                modbusThread = null;
            }
            modbusThread = new Thread(new ParameterizedThreadStart(modbusTCPWorker));
            modbusThread.Start(plc);
        }

        #endregion

        #region -- Event Handler --

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
                lastSelectedPLC = listView_plc.SelectedIndices[0];
                listView_plc.HideSelection = false;
            }
        }
        private void listView_plc_DoubleClick(object sender, EventArgs e)
        {
            if (GPLC.AuthVerify(Security.GPLCAuthority.Administrator))
            {
                int index = listView_plc.SelectedIndices.Count > 0 ? listView_plc.SelectedIndices[0] : -1;
                PLCEditControl(index);
            }
        }
        private void listView_tag_DoubleClick(object sender, EventArgs e)
        {
            if (GPLC.AuthVerify(Security.GPLCAuthority.Administrator))
            {
                int index = listView_tag.SelectedIndices.Count > 0 ? listView_tag.SelectedIndices[0] : -1;
                TagControl(index);
            }
        }

        private void listView_plc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lastSelectedPLC >= 0)
            {
                Debug.Assert(lastSelectedPLC < markers[_shownMarker].ProjectData.plcs.Count);
                PLC plc = markers[_shownMarker].ProjectData.plcs[lastSelectedPLC];
                ModelUtil.deletePLC(plc.id);
                refreshPLCList();
            }
        }
        private void listView_tag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lastSelectedPLC >= 0)
            {
                Debug.Assert(lastSelectedPLC < markers[_shownMarker].ProjectData.plcs.Count);
                if (listView_tag.SelectedIndices.Count > 0)
                {
                    int index = listView_tag.SelectedIndices[0];
                    PLC plc = markers[_shownMarker].ProjectData.plcs[lastSelectedPLC];
                    ModelUtil.deleteTag(plc.tags[index].id);
                    refreshTagList();
                }
            }
        }

        #endregion

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
                listView_tag_DoubleClick(sender, e);
            }
        }
        private void listView_plc_MouseUp(object sender, MouseEventArgs e)
        {
            if(listView_plc.Visible && listView_plc.SelectedIndices.Count <= 0)
            {
                lastSelectedPLC = -1;
                listView_plc.HideSelection = true;
            }
        }

        #endregion

        #region -- input control --
        
        private void PLCEditControl(int index)
        {
            this.listView_tag.Hide();
            this.listView_plc.Hide();

            ProjectData project = markers[ShownMarker].ProjectData;

            if (index < 0)
            {
                plcInputControl.init(project);
            }
            else
            {
                plcInputControl.init(project, project.plcs[index]);
            }
        }
        private void TagControl(int index)
        {
            if (lastSelectedPLC < 0)
            {
                return;
            }

            this.listView_tag.Hide();
            this.listView_plc.Hide();

            PLC plc = markers[ShownMarker].ProjectData.plcs[lastSelectedPLC];

            if (index < 0)
            {
                tagInputControl1.init(plc.id);
            }
            else
            {
                Debug.Assert(index < plc.tags.Count);
                tagInputControl1.init(plc.id, plc.tags[index]);
            }
        }

        #endregion

        #region -- Modbus TCP Worker --

        private void modbusTCPWorker(object o)
        {
            try
            {
                modbusTCPWorker(o as PLC);
            }
            catch (ThreadAbortException)
            {
            }
        }
        private void modbusTCPWorker(PLC plc)
        {
            int pollingRate = int.Parse(Setting.GPLCSetting.settingRead(@"Polling/Rate"));
            if (plc == null)
            {
                return;
            }

            // initialize modbus TCP/IP
            if (MBmaster != null && MBmaster.connected)
            {
                MBmaster.disconnect();
            }
            MBmaster = new Master(plc.ip, Convert.ToUInt16(plc.port));
            // connecting message "Connecting to [IP]:[PORT]"
            //string str = string.Format("Connecting to {0}:{1}", plc.ip, plc.port);

            // refresh dataGrid periodically
            while (MBmaster != null && MBmaster.connected)
            {
                try
                {
                    int i = 0;
                    foreach (Tag r in plc.tags)
                    {
                        readData(Convert.ToByte(r.id), Convert.ToUInt16(r.addr), Convert.ToUInt16(r.length), i++);
                    }
                    // spin wait
                    SpinWait.SpinUntil(() => false, pollingRate);
                }
                catch (Exception)
                {
                }
            }
        }

        private void readData(byte id, ushort addr, ushort length, int index)
        {
            try
            {
                // modbus read
                long val = modbusRead(id, addr, length);
                Invoke(new TagHandler(RefreshTag), new Object[] { index, val.ToString() });
            }
            catch (Exception)
            {
                //Invoke(new DataFieldHandler(RefreshDataField), new Object[] { index, "????" });
            }
        }
        private long modbusRead(byte id, ushort addr, ushort length)
        {
            // get the most significant digit
            int pow = (int) Math.Pow(10, Math.Floor(Math.Log10(addr)));
            int ldigit = (int)Math.Floor((double)addr / pow);
            addr = (pow > 1) ? (ushort)(addr % pow) : addr;
            byte[] data = new byte[length*2];
            // four operation type
            switch (ldigit)
            {
                case 1:
                    MBmaster.ReadCoils(1, id, addr, length, ref data);
                    break;
                case 2:
                    MBmaster.ReadDiscreteInputs(2, id, addr, length, ref data);
                    break;
                case 3:
                    MBmaster.ReadInputRegister(3, id, addr, length, ref data);
                    return (data[0] << 8) + data[1];
                case 4:
                    MBmaster.ReadHoldingRegister(4, id, addr, length, ref data);
                    return (data[0] << 8) + data[1];
                default:
                    MBmaster.ReadHoldingRegister(4, id, addr, length, ref data);
                    break;
            }
            return data[0];
        }

        #endregion

        // status delegate
        delegate void TagHandler(int i, string str);
        private void RefreshTag(int i, string str)
        {
            if (listView_tag.Items.Count > i)
            {
                listView_tag.Items[i].SubItems[1].Text = str;
            }
        }

    }
}
