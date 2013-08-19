using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Data.Common;
using MIRLE_GPLC.Model;
using MIRLE_GPLC.form;
using MIRLE_GPLC.form.marker;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using SuperContextMenu;
using Modbus;
using Modbus.Client;

namespace MIRLE_GPLC
{
    public partial class MainForm : Form
    {
        private GMapOverlay markersOverlay;
        private GMapMarker currMarker;
        private bool isDragging = false;

        private List<GMapMarker> mouseOveredMarkers = new List<GMapMarker>();

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
            loadProjects();

            /* start up resident refresh 
             * modbus tcp worker
             */
            //ThreadPool.QueueUserWorkItem(new WaitCallback(modbusTCPWorker));

        }

        public void loadProjects()
        {
            markersOverlay.Clear();
            ThreadPool.QueueUserWorkItem(new WaitCallback(loadProjects));
        }

        private void loadProjects(object o)
        {
            List<ProjectData> list = ModelUtil.getProjectList();
            // reverse order for reverse render order
            list.Reverse();
            // get project list from database and add markers to map
            foreach (ProjectData p in list)
            {
                addPMarker(p);
            }
        }

        #endregion

        #region -- Mouse Events --

        private void gMap_MouseClick(object sender, MouseEventArgs e)
        {
            PointLatLng latlng = gMap.FromLocalToLatLng(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                textBox_latlng_lat.Text = string.Format("{0:0.00000}", latlng.Lat);
                textBox_latlng_lng.Text = string.Format("{0:0.00000}", latlng.Lng);
            }
        }

        private void gMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // zoom in when double clicked
            if (e.Button == MouseButtons.Left)
            {
                gMap.Position = gMap.FromLocalToLatLng(e.Location.X, e.Location.Y);
                gMap.Zoom++;
            }
        }

        private void gMap_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng latlng = gMap.FromLocalToLatLng(e.X, e.Y);
            // display latlng where mouse is on
            latlngLabel.Text = string.Format("({0:0.00}, {1:0.00})", latlng.Lat, latlng.Lng);

            // clicked when mouse moving : dragging
            if (e.Button == MouseButtons.Right)
            {
                isDragging = true;
            }
            // move hover markers while dragging
            if (isDragging && currMarker != null && !(currMarker is ProjectMarker) )
            {
                setCurrMarker(latlng);
                gMap.Refresh();

            }
        }

        private void gMap_MouseUp(object sender, MouseEventArgs e)
        {
            InputButton.Text = (currMarker is ProjectMarker) ? "Modify" : "Add";
            // set dragging status to false while mouse up
            isDragging = false;
        }

        private void gMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (currMarker is GMarkerGoogle && !(currMarker is ProjectMarker))
            {
                foreach (GMapMarker marker in mouseOveredMarkers)
                {
                    if (marker is ProjectMarker)
                    {
                        mouseOveredMarkers.Remove(currMarker);
                        markersOverlay.Markers.Remove(currMarker);
                        currMarker.Dispose();
                        currMarker = null;
                        gMap.Refresh();
                        break;
                    }
                }
            }
            // mouse down on marker
            if (mouseOveredMarkers.Count > 0)
            {
                currMarker = mouseOveredMarkers[0];
            }
            // add new marker while right mouse button downed
            else if (e.Button == MouseButtons.Right)
            {
                setCurrMarker(gMap.FromLocalToLatLng(e.X, e.Y));
            }
            gMap.Refresh();
        }

        #endregion

        #region -- Marker Event --

        private void gMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            textBox_latlng_lat.Text = string.Format("{0:0.00000}", item.Position.Lat);
            textBox_latlng_lng.Text = string.Format("{0:0.00000}", item.Position.Lng);
            if (mouseOveredMarkers.Contains(item))
            {
                if (item is ProjectMarker)
                {
                    ProjectData pd = (item as ProjectMarker).ProjectData;
                    // set text box
                    label_case_ID.Text = "ID:" + pd.id.ToString();
                    textBox_case_Name.Text = pd.name;
                    richTextBox_case_addr.Text = pd.addr;

                    // get marker local position
                    //GPoint pos = gMap.FromLatLngToLocal(item.Position);
                    // set map center
                    //this.gMap.Position = gMap.FromLocalToLatLng((int)pos.X, (int)pos.Y + gMap.Height / 4);
                    if (e.Button == MouseButtons.Left)
                    {
                        viewProject(item);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        inputProject(mouseOveredMarkers.Last());
                    }
                }
                else
                {
                    inputProject(mouseOveredMarkers.Last());
                }
            }
        }

        private void gMap_OnMarkerEnter(GMapMarker item)
        {
            mouseOveredMarkers.Add(item);
        }

        private void gMap_OnMarkerLeave(GMapMarker item)
        {
            mouseOveredMarkers.Remove(item);
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
                string name = textBox_case_Name.Text;
                string addr = richTextBox_case_addr.Text;
                double lat = double.Parse(textBox_latlng_lat.Text);
                double lng = double.Parse(textBox_latlng_lng.Text);
                if (lat > 90 || lat < -90 || lng > 180 || lat < -180)
                {
                    throw new FormatException("Latlng value out of bound");
                }

                // update database
                if (InputButton.Text.Equals("Modify"))
                {
                    long id = (currMarker as ProjectMarker).ProjectData.id;
                    ModelUtil.updateProject(id, name, addr, lat, lng);
                }
                else
                {
                    addProject(name, addr, lat, lng);
                }
                // update marker overlay and current marker
                loadProjects();
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
            marker.ToolTipMode = (gMap.Zoom > 13) ? MarkerTooltipMode.Always :
                MarkerTooltipMode.OnMouseOver;
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
            if (currMarker == null || currMarker is ProjectMarker)
            {
                currMarker = new GMarkerGoogle(p, GMarkerGoogleType.green_pushpin);
                markersOverlay.Markers.Add(currMarker);
            }
            else
            {
                currMarker.Position = p;
            }
            // set textbox as the latlng of current marker
            textBox_latlng_lat.Text = string.Format("{0:0.00000}", currMarker.Position.Lat);
            textBox_latlng_lng.Text = string.Format("{0:0.00000}", currMarker.Position.Lng);
            // refresh map
            this.gMap.Refresh();
            return currMarker;
        }

        #endregion
        
        #region -- DB transaction --

        private void addProject(string name, string addr, double lat, double lng)
        {
            try
            {
                ModelUtil.insertProject(name, addr, lat, lng);
                markersOverlay.Markers.Remove(currMarker);
                currMarker.Dispose();
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void gMap_OnMapZoomChanged()
        {
            MarkerTooltipMode mode = (gMap.Zoom > 13) ? MarkerTooltipMode.Always :
                MarkerTooltipMode.OnMouseOver;
            foreach (GMapMarker marker in markersOverlay.Markers)
            {
                marker.ToolTipMode = mode;
            }
        }

        private void viewProject(GMapMarker item)
        {
            // set context menu
            List<ProjectMarker> list = new List<ProjectMarker>();
            foreach (GMapMarker marker in mouseOveredMarkers)
            {
                list.Add(marker as ProjectMarker);
            }
            // set context menu
            ToolTipContentContainer ttc = new ToolTipContentContainer(list);

            GPoint p = gMap.FromLatLngToLocal(item.Position);
            p.Offset(item.Size.Width / 2, -1 * (item.Size.Height));
            contextMenu(ttc, new Point((int)p.X, (int)p.Y));
        }
        private void inputProject(GMapMarker item)
        {
            // set context menu
            ToolTipContentContainer ttc = new ToolTipContentContainer(item);

            GPoint p = gMap.FromLatLngToLocal(item.Position);
            p.Offset(item.Size.Width*2/3, -1 * (item.Size.Height));
            contextMenu(ttc, new Point((int)p.X, (int)p.Y));
        }

        private void contextMenu(ToolTipContentContainer ttc, Point p)
        {
            // set context menu
            PoperContainer ttcContainer = new PoperContainer(ttc);
            ttcContainer.Show(this, p);
        }
    }
}
