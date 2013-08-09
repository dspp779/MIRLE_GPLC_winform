using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class Record
    {
        public int _plcid;
        public int _addr;
        public int _length;
        public string _format;
        public string _alias;

        public int PLC_ID
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
        
        public Record(int plcid, int addr, int length, string format, string alias)
        {
            this._plcid = plcid;
            this._addr = addr;
            this._length = length;
            this._format = format;
            this._alias = alias;
        }
    }
}
