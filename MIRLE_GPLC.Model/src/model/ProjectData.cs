using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class ProjectData
    {
        private long _id;
        private string _name;
        private string _addr;
        private double _lat;
        private double _lng;
        private List<PLC> _plcs;

        public Int64 id
        {
            get { return _id; }
        }

        public string name
        {
            get { return _name; }
        }

        public string addr
        {
            get { return _addr; }
        }

        public double lat
        {
            get { return _lat; }
        }

        public double lng
        {
            get { return _lng; }
        }

        public List<PLC> plcs
        {
            get { return _plcs; }
        }

        public ProjectData(Int64 id, string name, string addr,
            double lat, double lng, List<PLC> plcs)
        {
            this._id = id;
            this._name = name;
            this._addr = addr;
            this._lat = lat;
            this._lng = lng;
            this._plcs = plcs;
        }

        public void loadPLC()
        {
            _plcs = ModelUtil.getPLCList(id);
        }
    }
}
