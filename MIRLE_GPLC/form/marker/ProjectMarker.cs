using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using MIRLE_GPLC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.form.marker
{
    /* ProjectMaker is just an type of Google type marker
     * with specified style, and corresponding ProjectData
     * */
    internal class ProjectMarker : GMarkerGoogle
    {
        private ProjectData project;

        public ProjectData ProjectData
        {
            get { return project; }
        }

        public ProjectMarker(PointLatLng latlng, ProjectData project)
            : base(latlng, GMarkerGoogleType.red_dot)
        {
            this.project = project;
        }
    }
}
