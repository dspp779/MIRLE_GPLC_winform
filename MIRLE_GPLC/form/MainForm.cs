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

namespace MIRLE_GPLC
{
    public partial class MainForm : Form
    {
        private GMapOverlay markersOverlay;
        private bool isDragging = false;

        private List<GMapMarker> hoverMarkerList = new List<GMapMarker>();

        AbsModbusClient client;

        #region -- Form Properties & Initialization --

        public MainForm()
        {
            InitializeComponent();
            this.gMap.DragButton = System.Windows.Forms.MouseButtons.Left;
            this.gMap.ShowCenter = false;
            dataGridView1.Rows.Add(10);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // initialize map properties
            gMap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.MapProviders.GMapProvider.Language = GMap.NET.LanguageType.ChineseTraditional;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            this.gMap.Position = new PointLatLng(23.8, 121);


            // intialize overlay
            markersOverlay = new GMapOverlay("markers");
            gMap.Overlays.Add(markersOverlay);

            /* start up resident refresh 
             * modbus tcp worker
             */
            //ThreadPool.QueueUserWorkItem(new WaitCallback(modbusTCPWorker));

            foreach (ProjectData p in ModelUtil.getProjectList())
            {
                addMarker(p.lat, p.lng);
            }
        }

        #endregion

        #region -- Mouse Events --

        private void gMap_MouseClick(object sender, MouseEventArgs e)
        {
            PointLatLng latlng = gMap.FromLocalToLatLng(e.X, e.Y);
            textBox_latlng_lat.Text = string.Format("{0:0.000}", latlng.Lat);
            textBox_latlng_lng.Text = string.Format("{0:0.000}", latlng.Lng);
        }

        private void gMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                gMap.Zoom++;
            }
        }

        private void gMap_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng latlng = gMap.FromLocalToLatLng(e.X, e.Y);
            latlngLabel.Text = string.Format("({0:0.0}, {1:0.0})", latlng.Lat, latlng.Lng);

            if (e.Button != MouseButtons.None)
            {
                isDragging = true;
            }
            if (isDragging)
            {
                foreach (GMapMarker marker in hoverMarkerList)
                {
                    marker.Position = latlng;
                }
            }
            gMap.Refresh();
        }

        private void gMap_MouseUp(object sender, MouseEventArgs e)
        {
            PointLatLng p = gMap.FromLocalToLatLng(e.X, e.Y);
            hoverMarkerList.Clear();
            isDragging = false;
        }

        private void gMap_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (GMapMarker marker in markersOverlay.Markers)
            {
                if (marker.IsMouseOver)
                {
                    hoverMarkerList.Add(marker);
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                if (hoverMarkerList.Count == 0)
                {
                    hoverMarkerList.Add(addMarker(gMap.FromLocalToLatLng(e.X, e.Y)));
                }
            }
        }

        #endregion

        #region -- Marker Event --

        private void gMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                item.ToolTipMode = MarkerTooltipMode.Always;
            }
        }

        #endregion

        #region -- Modbus TCP Worker --

        private void modbusTCPWorker(object o)
        {
            // initialize modbus TCP/IP
            ModbusClientAdpater adpater = new ModbusClientAdpater();
            TcpModbusConnectConfig config = new TcpModbusConnectConfig() { IpAddress = "192.168.30.236", Port = 502 };
            client = adpater.CreateModbusClient(EnumModbusFraming.TCP);
            string str = string.Format("Connecting to {0}:{1}", config.IpAddress, config.Port);

            // modbus TCP connect
            try
            {
                do
                {
                    Invoke(new ModbusStatusHandler(RefreshModbusLabel), new Object[] { str });
                    SpinWait.SpinUntil(() => false, 1000);
                } while (!client.Connect(config));
            }
            catch (SocketException)
            {
                Invoke(new ModbusStatusHandler(RefreshModbusLabel), new Object[] { "Modbus TCP/IP connect fail" });
            }

            // refresh dataGrid periodically
            while (client.IsConnected)
            {
                try
                {
                    var result = client.ReadHoldingRegistersToDecimal(1, 0, 10);
                    int i = 0;
                    foreach (long d in result)
                    {
                        int slot = -1;
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
                double lat = double.Parse(textBox_latlng_lat.Text);
                double lng = double.Parse(textBox_latlng_lng.Text);
                if (lat > 90 || lat < -90 || lng > 180 || lat < -180)
                {
                    throw new FormatException("Latlng value out of bound");
                }

                addMarker(lat, lng);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region -- UI delegate --

        delegate void dataGridHandler(int i, long d);
        private void RefreshGridValue(int i, long d)
        {
            int slot = i % 10;
            dataGridView1.Rows[slot].HeaderCell.Value = i.ToString();
            dataGridView1.Rows[slot].Cells[0].Value = d;
            //dataGridView2.DataSource = SQLiteDBMS.execQuery("SELECT * FROM test");
        }

        delegate void ModbusStatusHandler(string status);
        private void RefreshModbusLabel(string status)
        {
            modbusStatusLabel.Text = status;
        }

        private GMapMarker addMarker(double lat, double lng)
        {
            return addMarker(new PointLatLng(lat, lng));
        }

        private GMapMarker addMarker(PointLatLng p)
        {
            GMarkerGoogle m = new GMarkerGoogle(p, GMarkerGoogleType.green_pushpin);
            this.gMap.Position = p;
            // add to overlay
            markersOverlay.Markers.Add(m);
            // allow map zooming
            //marker.IsHitTestVisible = false;
            return m;
        }

        #endregion

    }
}
