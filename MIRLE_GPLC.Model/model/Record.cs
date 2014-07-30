using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model.model
{
    public class Record
    {
        private DateTime _time = new DateTime();
        private float? _val;

        public DateTime time
        {
            get { return _time; }
        }

        public float? val
        {
            get { return _val; }
        }

        public Record()
        {
        }

        public Record(DateTime time, float? val)
        {
            this._time = time;
            this._val = val;
        }

    }
}
