using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus;
using Modbus.Client;
using System.Threading;
using System.Net.Sockets;
using MIRLE_GPLC.Model;
using System.Data.Common;
using MIRLE_GPLC.form.marker;
using MIRLE_GPLC.form;
using SuperContextMenu;
using GMap.NET.WindowsForms.ToolTips;

namespace MIRLE_GPLC
{
    public partial class MainForm : Form
    {
        private GMapOverlay markersOverlay;
        private GMapMarker currMarker = new GMarkerGoogle(
            new PointLatLng(), GMarkerGoogleType.green_pushpin);
        private bool isDragging = false;

        private List<GMapMarker> focusMarkerList = new List<GMapMarker>();
        private List<ProjectMarker> mouseOveredMarkers = new List<ProjectMarker>();

        AbsModbusClient client;

        #region -- Form Properties & Initialization --

        public MainForm()
        {
            InitializeComponent();
            // set drag map mouse button to left
            this.gMap.DragButton = System.Windows.Forms.MouseButtons.Left;
            // no center red cross
            this.gMap.ShowCenter = false;
            // init modbus view
            dataGridView1.Rows.Add(10);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // initial position on screen
            this.CenterToScreen();

            // initialize map properties
            gMap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            // set language
            GMap.NET.MapProviders.GMapProvider.Language = GMap.NET.LanguageType.ChineseTraditional;
            // tile retrieve policy: ServerOnly, ServerAndCache, CacheOnly.
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            // initial latlng
            this.gMap.Position = new PointLatLng(23.8, 121);


            // intialize marker overlay
            markersOverlay = new GMapOverlay("markers");
            gMap.Overlays.Add(markersOverlay);

            /* load projects from database
             * and add markers onto map
             */
            ThreadPool.QueueUserWorkItem(new WaitCallback(loadProjects));

            /* start up resident refresh 
             * modbus tcp worker
             */
            //ThreadPool.QueueUserWorkItem(new WaitCallback(modbusTCPWorker));

        }

        private void loadProjects(object o)
        {
            // get project list from database and add markers to map
            foreach (ProjectData p in ModelUtil.getProjectList())
            {
                addPMarker(p);
            }
        }

        #endregion

        #region -- Mouse Events --

        private void gMap_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void gMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // zoom in when double clicked
            if (e.Button == MouseButtons.Left)
            {
                gMap.Zoom++;
            }
        }

        private void gMap_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng latlng = gMap.FromLocalToLatLng(e.X, e.Y);
            // display latlng where mouse is on
            latlngLabel.Text = string.Format("({0:0.00}, {1:0.00})", latlng.Lat, latlng.Lng);

            // clicked when mouse moving : dragging
            if (e.Button != MouseButtons.None)
            {
                isDragging = true;
            }
            // move hover markers while dragging
            if (isDragging && currMarker != null && focusMarkerList.Contains(currMarker))
            {
                currMarker.Position = latlng;
                gMap.Refresh();
            }
            foreach (GMapMarker marker in markersOverlay.Markers)
            {
                if (marker.IsMouseOver)
                {
                    marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    break;
                }
            }
        }

        private void gMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDragging)
            {
                if (focusMarkerList.Count > 0)
                {
                    InputButton.Text = (focusMarkerList[0] is ProjectMarker)
                        ? "Modify" : "Add";
                }
            }
            // clear hover markers while mouse up
            focusMarkerList.Clear();
            // set dragging status to false while mouse up
            isDragging = false;
        }

        private void gMap_MouseDown(object sender, MouseEventArgs e)
        {
            // add hover markers to list while mouse down
            foreach (GMapMarker marker in markersOverlay.Markers)
            {
                if (marker.IsMouseOver)
                {
                    focusMarkerList.Add(marker);
                }
            }
            // mouse down on a marker
            if (focusMarkerList.Count > 0)
            {
                if (!currMarker.IsMouseOver || focusMarkerList.Count > 1)
                {
                    markersOverlay.Markers.Remove(currMarker);
                }
            }
            // add new marker while right mouse button downed
            else if (e.Button == MouseButtons.Right)
            {
                focusMarkerList.Add(setCurrMarker(gMap.FromLocalToLatLng(e.X, e.Y)));
            }
            gMap.Refresh();
        }

        #endregion

        #region -- Marker Event --

        private void gMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            // set tooltip always show when marker clicked
            /*if (e.Button == MouseButtons.Left)
            {
                item.ToolTipMode = MarkerTooltipMode.Always;
            }*/
            textBox_latlng_lat.Text = string.Format("{0:0.00000}", item.Position.Lat);
            textBox_latlng_lng.Text = string.Format("{0:0.00000}", item.Position.Lng);
            if (item is ProjectMarker)
            {
                ProjectData pd = ((ProjectMarker) item).ProjectData;
                textBox_case_ID.Text = pd.id.ToString();
                textBox_case_Name.Text = pd.name;
                textBox_case_addr.Text = pd.addr;
            }

            if (mouseOveredMarkers.Contains(item))
            {
                // get marker local position
                GPoint pos = gMap.FromLatLngToLocal(item.Position);
                // set map center
                this.gMap.Position = gMap.FromLocalToLatLng((int)pos.X, (int)pos.Y + gMap.Height / 4);
                // set context menu
                if (mouseOveredMarkers.Count >= 1)
                {
                    ToolTipContent ttc = new ToolTipContent(mouseOveredMarkers);
                    PoperContainer ttcContainer = new PoperContainer(ttc);

                    GPoint p = gMap.FromLatLngToLocal(item.Position);
                    p.Offset(item.Size.Width / 2, -1 * (item.Size.Height));
                    ttcContainer.Show(this, new System.Drawing.Point((int)p.X, (int)p.Y));
                }
            }

        }

        private void gMap_OnMarkerEnter(GMapMarker item)
        {
            if (item is ProjectMarker)
            {
                mouseOveredMarkers.Add(item as ProjectMarker);
            }
        }

        private void gMap_OnMarkerLeave(GMapMarker item)
        {
            if (item is ProjectMarker)
            {
                mouseOveredMarkers.Remove(item as ProjectMarker);
            }
        }

        #endregion

        #region -- Modbus TCP Worker --

        private void modbusTCPWorker(object o)
        {
            // initialize modbus TCP/IP
            ModbusClientAdpater adpater = new ModbusClientAdpater();
            // config ip and port
            TcpModbusConnectConfig config = new TcpModbusConnectConfig() { IpAddress = "192.168.30.236", Port = 502 };
            // config modbus connection type
            client = adpater.CreateModbusClient(EnumModbusFraming.TCP);
            // connecting message "Connecting to [IP]:[PORT]"
            string str = string.Format("Connecting to {0}:{1}", config.IpAddress, config.Port);

            // modbus TCP connect
            try
            {
                do
                {
                    // invoke ui thread to change tooltip
                    Invoke(new ModbusStatusHandler(RefreshModbusLabel), new Object[] { str });
                    SpinWait.SpinUntil(() => false, 1000);
                } while (!client.Connect(config));
            }
            catch (SocketException)
            {
                // invoke ui thread to change tooltip
                Invoke(new ModbusStatusHandler(RefreshModbusLabel), new Object[] { "Modbus TCP/IP connect fail" });
            }

            // refresh dataGrid periodically
            while (client.IsConnected)
            {
                try
                {
                    // modbus read
                    var result = client.ReadHoldingRegistersToDecimal(1, 0, 10);
                    int i = 0;
                    int slot;
                    foreach (long d in result)
                    {
                        string s = (string)dataGridView1.Rows[i].HeaderCell.Value;
                        Object val = dataGridView1.Rows[i].Cells[0].Value;
                        if (val == null || (long)val != d || int.TryParse(s, out slot) || slot != i)
                        {
                            try
                            {
                                Invoke(new dataGridHandler(RefreshGridValue), new Object[] { i, d });
                            }
                            catch (Exception)
                            {
                            }
                        }
                        i++;
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
         
        private void InputButton_Click(object sender, EventArgs e)
        {
            try
            {
                int id;
                if (!int.TryParse(textBox_case_ID.Text, out id))
                {
                    throw new FormatException("ID is not a valid");
                }
                string name = textBox_case_Name.Text;
                string addr = textBox_case_addr.Text;
                double lat = double.Parse(textBox_latlng_lat.Text);
                double lng = double.Parse(textBox_latlng_lng.Text);
                if (lat > 90 || lat < -90 || lng > 180 || lat < -180)
                {
                    throw new FormatException("Latlng value out of bound");
                }

                // update database
                if (InputButton.Text.Equals("Modify"))
                {
                    ModelUtil.updateProject(id, name, addr, lat, lng);
                }
                else
                {
                    addProject(id, name, addr, lat, lng);
                }
                // update marker overlay and current marker
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region -- UI delegate --

        // dataGridView delegate
        delegate void dataGridHandler(int i, long d);
        private void RefreshGridValue(int i, long d)
        {
            int slot = i % 10;
            dataGridView1.Rows[slot].HeaderCell.Value = i.ToString();
            dataGridView1.Rows[slot].Cells[0].Value = d;
            //dataGridView2.DataSource = SQLiteDBMS.execQuery("SELECT * FROM test");
        }

        // status delegate
        delegate void ModbusStatusHandler(string status);
        private void RefreshModbusLabel(string status)
        {
            modbusStatusLabel.Text = status;
        }

        // add marker to overlay delegate
        delegate GMapMarker addMarkerHandler(GMapMarker marker);
        private GMapMarker addMarker(GMapMarker marker)
        {
            // this.gMap.Position = p;
            // add to overlay
            markersOverlay.Markers.Add(marker);
            // allow map zooming
            //m.IsHitTestVisible = false;
            return marker;
        }

        private GMapMarker addGMarker(double lat, double lng, GMarkerGoogleType type)
        {
            return addGMarker(new PointLatLng(lat, lng), type);
        }

        private GMapMarker addGMarker(PointLatLng p, GMarkerGoogleType type)
        {
            GMarkerGoogle m = new GMarkerGoogle(p, type);
            if (gMap.InvokeRequired)
            {
                return (GMapMarker) Invoke(new addMarkerHandler(addMarker), new Object[] { m });
            }
            else
            {
                return addMarker(m);
            }
        }

        private GMapMarker addPMarker(ProjectData p)
        {
            return addPMarker(new PointLatLng(p.lat, p.lng), p);
        }

        private GMapMarker addPMarker(PointLatLng latlng, ProjectData p)
        {
            ProjectMarker m = new ProjectMarker(latlng, p);
            m.ToolTipText = p.name + "\n" + p.addr;
            m.ToolTipMode = MarkerTooltipMode.Never;
            m.ToolTip.Format.Alignment = StringAlignment.Near;
            if (gMap.InvokeRequired)
            {
                return (GMapMarker)Invoke(new addMarkerHandler(addMarker), new Object[] { m });
            }
            else
            {
                return addMarker(m);
            }
        }

        private GMapMarker setCurrMarker(PointLatLng p)
        {
            currMarker.Position = p;
            // set textbox as the latlng of current marker
            textBox_latlng_lat.Text = string.Format("{0:0.00000}", currMarker.Position.Lat);
            textBox_latlng_lng.Text = string.Format("{0:0.00000}", currMarker.Position.Lng);
            if (!markersOverlay.Markers.Contains(currMarker))
            {
                markersOverlay.Markers.Add(currMarker);
            }
            // refresh map
            this.gMap.Refresh();
            return currMarker;
        }

        #endregion
        
        #region -- DB transaction --

        private void addProject(ProjectData p)
        {
            addProject(p.id, p.name, p.addr, p.lat, p.lng);
        }

        private void addProject(long id, string name, string addr, double lat, double lng)
        {
            try
            {
                ModelUtil.addProject(id, name, addr, lat, lng);
                markersOverlay.Markers.Remove(currMarker);
                currMarker.Dispose();
                // ui
                addPMarker(new ProjectData(id, name, addr, lat, lng, null));
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

    }
}
