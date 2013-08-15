using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class Record
    {
        private long _plcid;
        private int _addr;
        private int _length;
        private string _format;
        private string _alias;

        public Int64 PLC_ID
        {
            get { return _plcid; }
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

        public Record(long plcid, int addr, int length, string format, string alias)
        {
            this._plcid = plcid;
            this._addr = addr;
            this._length = length;
            this._format = format;
            this._alias = alias;
        }

        public string getVal()
        {
            return _format;
        }
    }
}
