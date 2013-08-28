using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class Tag
    {
        private long _id;
        private int _addr;
        private int _length;
        private string _format;
        private string _alias;
        private long _plcid;

        public long id
        {
            get { return _id; }
        }

        public int addr
        {
            get { return _addr; }
        }

        public int length
        {
            get { return _length; }
        }

        public string format
        {
            get { return _format; }
        }

        public string alias
        {
            get { return _alias; }
        }

        public long plc_id
        {
            get { return _plcid; }
        }

        public Tag(long id, int addr, int length, string format, string alias, long plcid)
        {
            this._id = id;
            this._addr = addr;
            this._length = length;
            this._format = format;
            this._alias = alias;
            this._plcid = plcid;
        }

        public string getVal()
        {
            return _format;
        }
    }
}
