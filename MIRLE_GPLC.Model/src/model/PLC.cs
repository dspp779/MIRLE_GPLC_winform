using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class PLC
    {
        private long _plcid;
        private int _id;
        private string _ip;
        private int _port;
        private List<Record> _dataFields;

        public long PLC_ID
        {
            get { return _plcid; }
        }

        public int id
        {
            get { return _id; }
        }

        public string ip
        {
            get { return _ip; }
        }

        public int port
        {
            get { return _port; }
        }

        public List<Record> dataFields
        {
            get { return _dataFields; }
        }

        public PLC(long PLC_ID, int id, string ip, int port, List<Record> dataFields)
        {
            this._plcid = PLC_ID;
            this._id = id;
            this._ip = ip;
            this._port = port;
            this._dataFields = dataFields;
        }

    }
}
