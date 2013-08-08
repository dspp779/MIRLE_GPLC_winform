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

namespace MIRLE_GPLC
{
    public partial class MainForm : Form
    {
        private GMapOverlay markersOverlay;
        private bool isDragging = false;

        private List<GMapMarker> hoverMarkerList = new List<GMapMarker>();

        public MainForm()
        {
            InitializeComponent();
            this.gMap.DragButton = System.Windows.Forms.MouseButtons.Left;
            this.gMap.ShowCenter = false;
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
        }

        #region -- Mouse Events --

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
        }

        private void gMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                gMap.Zoom++;
            }
        }

        private void gMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDragging && e.Button == MouseButtons.Right)
            {
                GMarkerGoogle m = new GMarkerGoogle(gMap.FromLocalToLatLng(e.X, e.Y),
                    GMarkerGoogleType.red_dot);
                // add to overlay
                markersOverlay.Markers.Add(m);
                // allow map zooming
                //marker.IsHitTestVisible = false;
            }
            if (e.Button == MouseButtons.Left)
            {
                hoverMarkerList.Clear();
            }
            isDragging = false;
        }

        private void gMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (GMapMarker marker in markersOverlay.Markers)
                {
                    if (marker.IsMouseOver)
                    {
                        hoverMarkerList.Add(marker);
                    }
                }
            }
        }

        #endregion

    }
}
