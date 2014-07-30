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
using MIRLE_GPLC.Security;
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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // initial position on screen
            this.CenterToScreen();

            // initialize map properties
            gMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            // set language
            GMap.NET.MapProviders.GMapProvider.Language = GMap.NET.LanguageType.ChineseTraditional;
            // tile retrieve policy: ServerOnly, ServerAndCache, CacheOnly.
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            // initial latlng
            this.gMap.Position = new PointLatLng(23.8, 121);

            // intialize marker overlay
            markersOverlay = new GMapOverlay("markers");
            gMap.Overlays.Add(markersOverlay);

        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            // initial Auth
            authenticate();
            //authenticate("root", "1234567");
        }

        private void loadProjects()
        {
            /* project data can be extent
             * create another thread to work on
             */
            ThreadPool.QueueUserWorkItem(new WaitCallback(loadProjects));
        }
        private void loadProjects(object o)
        {
            List<ProjectData> list = ModelUtil.getProjectList();
            // reverse order for reverse render order
            
            // get project list from database
            list.Reverse();
            // add markers to map
            foreach (ProjectData p in list)
            {
                addPMarker(p);
            }
        }

        public override void Refresh()
        {
            // clear markers
            markersOverlay.Clear();
            mouseOveredMarkers.Clear();
            // clear current marker
            if (currMarker != null)
            {
                currMarker.Dispose();
                currMarker = null;
            }
            /* load projects from database
             * and add markers onto map
             */
            loadProjects();
            base.Refresh();
        }

        #endregion

        #region -- Authentication --

        private void authenticate()
        {
            // get authentication of current user
            GPLC.user.Authenticate();
            // refresh authentication status label
            RefreshAuthStatus();
            // refresh gMap
            Refresh();
        }
        private int authenticate(string id, string pass)
        {
            try
            {
                // user login with given id and password and get corresponding authentication
                GPLC.user.Authenticate(id, pass);
                string str = string.Format("以{0}身分登入成功。({1}權限)", id, GPLC.user.authority.ToString());
                MessageBox.Show(str, "認證成功");
                return 1;
            }
            catch (WrongIdPassException)
            {
                MessageBox.Show("使用者名稱不存在或密碼錯誤", "認證失敗");
                return 0;
            }
            finally
            {
                // refresh authentication status label
                RefreshAuthStatus();
                // refresh gMap
                Refresh();
            }
        }
        private void authForm()
        {
            do
            {
                // login form
                AuthForm authform = new AuthForm();
                if (authform.ShowDialog() == System.Windows.Forms.DialogResult.OK && authenticate(authform.id, authform.pass) == 0)
                {
                    continue;
                }
                break;
            } while (true);
        }

        #endregion

        #region -- Mouse Events --

        private void gMap_MouseMove(object sender, MouseEventArgs e)
        {
            // compute corresponding latlng on gMap with given local position
            PointLatLng latlng = gMap.FromLocalToLatLng(e.X, e.Y);
            // display latlng where mouse is on
            latlngLabel.Text = string.Format("({0:0.00}, {1:0.00})", latlng.Lat, latlng.Lng);

            // clicked when mouse moving : dragging
            if (e.Button == MouseButtons.Right)
            {
                isDragging = true;
            }
            // move hover markers while dragging
            if (isDragging && currMarker != null && !(currMarker is ProjectMarker))
            {
                setCurrMarker(latlng);
                // refresh marker movement on gMap
                gMap.Refresh();
            }
        }

        private void gMap_MouseUp(object sender, MouseEventArgs e)
        {
            /* determine whether user is adding or modifying a project by whether
             * a project marker or a green push-pin marker last clicked
             * */
            InputButton.Text = (currMarker is ProjectMarker) ? "Modify" : "Add";
            // set dragging status to false while mouse up
            isDragging = false;
        }
        private void gMap_MouseDown(object sender, MouseEventArgs e)
        {
            /* if there's any project marker clicked while a green push-pin marker is clicked
             * green push-pin marker should be disappeared
             * */
            if (currMarker is GMarkerGoogle && !(currMarker is ProjectMarker))
            {
                foreach (GMapMarker marker in mouseOveredMarkers)
                {
                    if (marker is ProjectMarker)
                    {
                        // remove current marker
                        mouseOveredMarkers.Remove(currMarker);
                        markersOverlay.Markers.Remove(currMarker);
                        currMarker.Dispose();
                        currMarker = null;
                        // refresh gMap
                        gMap.Refresh();
                        break;
                    }
                }
            }
            // determine current marker 
            if (mouseOveredMarkers.Count > 0)
            {
                currMarker = mouseOveredMarkers.Last();
            }
            // add new marker while right mouse button downed
            else if (e.Button == MouseButtons.Right && GPLC.AuthVerify(GPLCAuthority.Administrator))
            {
                setCurrMarker(gMap.FromLocalToLatLng(e.X, e.Y));
            }
            gMap.Refresh();
        }

        private void gMap_MouseClick(object sender, MouseEventArgs e)
        {
            // compute corresponding latlng on gMap with given local position
            PointLatLng latlng = gMap.FromLocalToLatLng(e.X, e.Y);
            // display clicked latlng on textbox 
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
                // set gMap center : double clicked position
                gMap.Position = gMap.FromLocalToLatLng(e.Location.X, e.Location.Y);
                gMap.Zoom++;
            }
        }

        #endregion

        #region -- Marker Events --

        private void gMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            // 
            GMapMarker marker = (mouseOveredMarkers.Count > 0) ? mouseOveredMarkers.Last() : item;
            // display clicked latlng on textbox 
            textBox_latlng_lat.Text = string.Format("{0:0.00000}", marker.Position.Lat);
            textBox_latlng_lng.Text = string.Format("{0:0.00000}", marker.Position.Lng);
            // double check to avoid inconsistency : check if clicked item really in mouseOvered marker list
            if (mouseOveredMarkers.Contains(item))
            {
                // left mouse button clicked : add a project or view a project
                if (e.Button == MouseButtons.Left)
                {
                    // view a project
                    if (item is ProjectMarker)
                    {
                        ProjectData pd = (marker as ProjectMarker).ProjectData;
                        // set text box
                        textBox_project_id.Text = pd.id.ToString();
                        textBox_project_name.Text = pd.name;
                        richTextBox_project_addr.Text = pd.addr;
                        // get marker local position
                        GPoint pos = gMap.FromLatLngToLocal(item.Position);
                        // set map center
                        //this.gMap.Position = gMap.FromLocalToLatLng((int)pos.X, (int)pos.Y + gMap.Height / 4);

                        viewProject(item);
                    }
                    // add a project
                    else if (GPLC.AuthVerify(GPLCAuthority.Administrator))
                    {
                        inputProject(item);
                    }
                }
                // modify a project
                else if (e.Button == MouseButtons.Right && GPLC.AuthVerify(GPLCAuthority.Administrator))
                {
                    if (item is ProjectMarker)
                    {
                        inputProject(marker);
                    }
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

        #region -- UI delegate --

        /* There is a tradition for windows form application
         * UI can only be modified by the thread that created it.
         * If one thread that is not the creator wants to modify content of a control,
         * it can invoke control modified delegate,
         * and the UI thread would acauire the task.
         * */

        // status delegate
        private void RefreshAuthStatus()
        {
            // check if invoke is required
            if (this.InvokeRequired)
            {
                Invoke(new AuthStatusHandler(RefreshAuthStatus), new object[] { GPLC.user.authInfo() });
            }
            else
            {
                RefreshAuthStatus(GPLC.user.authInfo());
            }
        }
        // authentication status label delegator
        delegate void AuthStatusHandler(string status);
        private void RefreshAuthStatus(string status)
        {
            AuthStatusLabel.Text = status;
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
                return (GMapMarker)Invoke(new addMarkerHandler(addMarker), new Object[] { m });
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
            // check authentication
            GPLC.user.Authenticate(GPLCAuthority.Administrator);

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

        #region -- Zooming Event --

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
            // set tooltip mode for all markers on gMap
            foreach (GMapMarker marker in markersOverlay.Markers)
            {
                marker.ToolTipMode = mode;
            }
        }

        #endregion

        #region -- context menu --

        private void viewProject(GMapMarker item)
        {
            // set context menu
            List<ProjectMarker> list = new List<ProjectMarker>();
            // add all clicked markers
            foreach (GMapMarker marker in mouseOveredMarkers)
            {
                if (marker is ProjectMarker)
                {
                    list.Add(marker as ProjectMarker);
                }
            }
            // set context menu
            if (ttc != null)
            {
                ttc.Dispose();
                ttc = null;
            }
            /* create a view project tooltip container for context menu
             *  : pass list of project markers
             * */
            ttc = new ToolTipContentContainer(list);

            // compute show position
            GPoint p = gMap.FromLatLngToLocal(item.Position);
            p.Offset(item.Size.Width / 2, -1 * (item.Size.Height));
            // show contextMenu
            contextMenu(ttc, new Point((int)p.X, (int)p.Y));
        }
        private void inputProject(GMapMarker item)
        {
            // check authentication
            GPLC.user.Authenticate(GPLCAuthority.Administrator);

            // set context menu
            if (ttc != null)
            {
                ttc.Dispose();
                ttc = null;
            }
            /* create a input project tooltip container for context menu
             *  : pass a marker
             * */
            ttc = new ToolTipContentContainer(item);

            // compute show position
            GPoint p = gMap.FromLatLngToLocal(item.Position);
            p.Offset(item.Size.Width * 2 / 3, -1 * (item.Size.Height));
            // show contextMenu
            contextMenu(ttc, new Point(Convert.ToInt32(p.X), Convert.ToInt32(p.Y)));
        }

        private void contextMenu(ToolTipContentContainer ttc, Point p)
        {
            // set tooltip container context menu
            PoperContainer ttcContainer = new PoperContainer(ttc);
            // show context menu at specified position
            ttcContainer.Show(this, p);
        }

        #endregion

        #region -- File Operation --

        // open db file
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // set db file path
            ModelUtil.setPath(openFileDialog1.FileName);
            // refresh gMap
            Refresh();
        }

        // save db file
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog(this);
        }
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            // copy present db file to speciefied path
            ModelUtil.copyTo(saveFileDialog1.FileName);
            // set db file path
            ModelUtil.setPath(saveFileDialog1.FileName);
        }

        #endregion


        private void InputButton_Click(object sender, EventArgs e)
        {
            try
            {
                // check authentication
                GPLC.Auth(GPLCAuthority.Administrator);

                // parse textBox contents
                long id = long.Parse(textBox_project_id.Text);
                string name = textBox_project_name.Text;
                string addr = richTextBox_project_addr.Text;
                double lat = double.Parse(textBox_latlng_lat.Text);
                double lng = double.Parse(textBox_latlng_lng.Text);
                // check data range
                if (lat > 90 || lat < -90 || lng > 180 || lat < -180)
                {
                    // out of range
                    throw new FormatException("Latlng value out of bound");
                }

                // update database
                try
                {
                    // modify a project or add a project
                    if (InputButton.Text.Equals("Modify"))
                    {
                        // original id
                        long oid = (currMarker as ProjectMarker).ProjectData.id;
                        ModelUtil.updateProject(id, name, addr, lat, lng, oid);
                    }
                    else
                    {
                        // insert a new project
                        ModelUtil.insertProject(id, name, addr, lat, lng);
                        // remove green push-pin marker
                        markersOverlay.Markers.Remove(currMarker);
                        if (currMarker != null)
                        {
                            currMarker.Dispose();
                        }
                    }
                }
                catch (DbException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // update marker overlay and current marker
                Refresh();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                MessageBox.Show(ex.Message, "Fatal Error");
                //Application.Exit();
            }
        }

        // authentication related events
        private void ToolStripMenuItem_anonymous_Click(object sender, EventArgs e)
        {
            authenticate();
        }
        private void ToolStripMenuItem_auth_Click(object sender, EventArgs e)
        {
            authForm();
        }

    }
}
