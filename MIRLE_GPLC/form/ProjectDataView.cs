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
using Modbus.Client;
using Modbus;
using System.Threading;
using System.Net.Sockets;

namespace MIRLE_GPLC.form
{
    internal partial class ProjectDataView : UserControl
    {
        private List<ProjectMarker> markers = new List<ProjectMarker>();
        private int _shownMarker = 0;
        private PLC lastSelectedPLC;
        AbsModbusClient client;

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
                lastSelectedPLC = null;

                labelTitle.Text = markers[_shownMarker].ProjectData.name;
                labelText.Text = markers[_shownMarker].ProjectData.addr;

                labelCounter.Text = (_shownMarker + 1).ToString() + " of " + markers.Count;

                Refresh();
            }
        }

        public ProjectDataView()
        {
            InitializeComponent();
        }

        public void init(List<ProjectMarker> markers)
        {
            if (markers.Count == 0)
                return;

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
            base.Refresh();
            projectCreateControl1.Hide();
            dataFieldInputControl1.Hide();
            this.listView_data.Show();
            this.listView_plc.Show();
            refreshPLCList();
        }

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
            refreshItemList(lastSelectedPLC);
        }

        private void refreshItemList(PLC plc)
        {
            listView_data.Items.Clear();
            if (plc != null)
            {
                plc.reload();
                foreach (Record r in plc.dataFields)
                {
                    ListViewItem item = new ListViewItem(r.alias);
                    item.SubItems.Add(r.getVal());
                    listView_data.Items.Add(item);
                }
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
            }
        }

        private void listView_plc_DoubleClick(object sender, EventArgs e)
        {
            int index = listView_plc.SelectedIndices.Count > 0 ? listView_plc.SelectedIndices[0] : -1;
            PLCEditControl(index);
        }

        private void listView_data_DoubleClick(object sender, EventArgs e)
        {
            int index = listView_data.SelectedIndices.Count > 0 ? listView_data.SelectedIndices[0] : -1;
            DataFieldControl(index);
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

       
        private void listView_plc_MouseUp(object sender, MouseEventArgs e)
        {
            if(listView_plc.Visible && listView_plc.SelectedIndices.Count < 0)
            {
                lastSelectedPLC = null;
            }
            refreshItemList(lastSelectedPLC);
        }

        #endregion

        #region -- input control --
        
        private void PLCEditControl(int index)
        {
            ProjectData project = markers[_shownMarker].ProjectData;

            this.listView_data.Hide();
            this.listView_plc.Hide();
            if (index >= 0)
            {
                projectCreateControl1.init(project, project.plcs[index]);
            }
            else
            {
                projectCreateControl1.init(project);
            }
        }
        private void DataFieldControl(int index)
        {
            if (lastSelectedPLC == null)
            {
                return;
            }

            this.listView_data.Hide();
            this.listView_plc.Hide();
            if (index >= 0)
            {
                dataFieldInputControl1.init(lastSelectedPLC.id, lastSelectedPLC.dataFields[index]);
            }
            else
            {
                dataFieldInputControl1.init(lastSelectedPLC.id);
            }
        }

        #endregion

        private void listView_plc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lastSelectedPLC != null)
            {
                ModelUtil.deletePLC(lastSelectedPLC.id);
                refreshPLCList();
            }
        }

        private void listView_data_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lastSelectedPLC != null)
            {
                if (listView_data.SelectedIndices.Count > 0)
                {
                    ModelUtil.deleteItem(lastSelectedPLC.dataFields[listView_data.SelectedIndices[0]].id);
                    refreshItemList(lastSelectedPLC);
                }
            }
        }


        #region -- Modbus TCP Worker --

        private void modbusTCPWorker(object o)
        {
            PLC plc = o as PLC;
            if (plc == null)
                return;
            // initialize modbus TCP/IP
            ModbusClientAdpater adpater = new ModbusClientAdpater();
            // config ip and port
            TcpModbusConnectConfig config = new TcpModbusConnectConfig() { IpAddress = plc.ip, Port = plc.port };
            // config modbus connection type
            if (client.IsConnected)
            {
                client.Disconnect();
            }
            client = adpater.CreateModbusClient(EnumModbusFraming.TCP);
            // connecting message "Connecting to [IP]:[PORT]"
            string str = string.Format("Connecting to {0}:{1}", config.IpAddress, config.Port);

            // modbus TCP connect
            try
            {
                do
                {
                    // invoke ui thread to change tooltip
                    //Invoke(new DataFieldHandler(RefreshDataField), new Object[] { str });
                    SpinWait.SpinUntil(() => false, 1000);
                } while (!client.Connect(config));
            }
            catch (SocketException)
            {
                // invoke ui thread to change tooltip
                //Invoke(new DataFieldHandler(RefreshDataField), new Object[] { "Modbus TCP/IP connect fail" });
            }

            // refresh dataGrid periodically
            while (client.IsConnected)
            {
                try
                {
                    int i = 0;
                    foreach (Record r in plc.dataFields)
                    {
                        // modbus read
                        byte[] data = client.ReadHoldingRegisters(1, (ushort)r.addr, (ushort)r.length);
                        // convert to wanted value
                        long val = Convert.ToInt64(data);
                        Invoke(new DataFieldHandler(RefreshDataField), new Object[] { i++, val.ToString() });
                    }
                    // spin wait
                    SpinWait.SpinUntil(() => false, 1000);
                }
                catch (ModbusException)
                {
                }
            }
        }

        #endregion

        // status delegate
        delegate void DataFieldHandler(int i, string str);
        private void RefreshDataField(int i, string str)
        {
            listView_data.Items[i].SubItems[0].Text = str;
        }
    }
}
