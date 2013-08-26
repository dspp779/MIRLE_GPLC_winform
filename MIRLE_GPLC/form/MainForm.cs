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
using GMap.NET.ObjectModel;

namespace MIRLE_GPLC
{
    public partial class MainForm : Form
    {
        private GMapOverlay markersOverlay;
        private GMapMarker currMarker;
        private bool isDragging = false;

        private ObservableCollectionThreadSafe<GMapMarker> mouseOveredMarkers
            = new ObservableCollectionThreadSafe<GMapMarker>();

        private ToolTipContentContainer ttc;

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
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            // initial latlng
            this.gMap.Position = new PointLatLng(23.8, 121);


            // intialize marker overlay
            markersOverlay = new GMapOverlay("markers");
            gMap.Overlays.Add(markersOverlay);

            /* load projects from database
             * and add markers onto map
             */
            loadProjects();
        }

        public void loadProjects()
        {
            markersOverlay.Clear();
            currMarker = null;
            /* project data can be extent
             * create another thread to work on
             */
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
                currMarker = mouseOveredMarkers.Last();
            }
            // add new marker while right mouse button downed
            else if (e.Button == MouseButtons.Right)
            {
                setCurrMarker(gMap.FromLocalToLatLng(e.X, e.Y));
            }
            gMap.Refresh();
        }

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

        #endregion

        #region -- Marker Event --

        private void gMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            GMapMarker marker = (mouseOveredMarkers.Count > 0) ? mouseOveredMarkers.Last() : item;
            textBox_latlng_lat.Text = string.Format("{0:0.00000}", marker.Position.Lat);
            textBox_latlng_lng.Text = string.Format("{0:0.00000}", marker.Position.Lng);
            if (mouseOveredMarkers.Contains(item))
            {
                if (item is ProjectMarker)
                {
                    ProjectData pd = (marker as ProjectMarker).ProjectData;
                    // set text box
                    label_case_ID.Text = "ID:" + pd.id.ToString();
                    textBox_case_Name.Text = pd.name;
                    richTextBox_case_addr.Text = pd.addr;
                    // get marker local position
                    GPoint pos = gMap.FromLatLngToLocal(item.Position);
                    // set map center
                    this.gMap.Position = gMap.FromLocalToLatLng((int)pos.X, (int)pos.Y + gMap.Height / 4);

                    if (e.Button == MouseButtons.Left)
                    {
                        viewProject(item);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        inputProject(marker);
                    }
                }
                else if(e.Button == MouseButtons.Left)
                {
                    inputProject(item);
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
                try
                {
                    if (InputButton.Text.Equals("Modify"))
                    {
                        long id = (currMarker as ProjectMarker).ProjectData.id;
                        ModelUtil.updateProject(id, name, addr, lat, lng);
                    }
                    else
                    {
                        ModelUtil.insertProject(name, addr, lat, lng);
                        markersOverlay.Markers.Remove(currMarker);
                        currMarker.Dispose();
                    }
                }
                catch (DbException ex)
                {
                    MessageBox.Show(ex.Message);
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

        // spot detail is determined by map zoom
        private void gMap_OnMapZoomChanged()
        {
            /* set tooltip always shows while zoom is bigger than 13
             * otherwise, tooltip shows when mouse over.
             */
            MarkerTooltipMode mode = (gMap.Zoom > 13) ? MarkerTooltipMode.Always :
                MarkerTooltipMode.OnMouseOver;
            setMapDetailMode(mode);
        }
        private void setMapDetailMode(MarkerTooltipMode mode)
        {
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
            if (ttc != null)
            {
                ttc.Dispose();
                ttc = null;
            }
            ttc = new ToolTipContentContainer(list);

            GPoint p = gMap.FromLatLngToLocal(item.Position);
            p.Offset(item.Size.Width / 2, -1 * (item.Size.Height));
            contextMenu(ttc, new Point((int)p.X, (int)p.Y));
        }
        private void inputProject(GMapMarker item)
        {
            // set context menu
            if (ttc != null)
            {
                ttc.Dispose();
                ttc = null;
            }
            ttc = new ToolTipContentContainer(item);

            GPoint p = gMap.FromLatLngToLocal(item.Position);
            p.Offset(item.Size.Width*2/3, -1 * (item.Size.Height));
            contextMenu(ttc, new Point(Convert.ToInt32(p.X), Convert.ToInt32(p.Y)));
        }

        private void contextMenu(ToolTipContentContainer ttc, Point p)
        {
            // set context menu
            PoperContainer ttcContainer = new PoperContainer(ttc);
            ttcContainer.Show(this, p);
        }

        #region -- File Operation --

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ModelUtil.setPath(openFileDialog1.FileName);
            loadProjects();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog(this);
        }
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ModelUtil.copyTo(saveFileDialog1.FileName);
            ModelUtil.setPath(saveFileDialog1.FileName);
        }

        #endregion
    }
}
