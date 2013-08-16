using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class PLC
    {
        private long _id;
        private int _netid;
        private string _ip;
        private int _port;
        private string _alias;
        private List<Record> _dataFields;

        public long id
        {
            get { return _id; }
        }

        public int netid
        {
            get { return _netid; }
        }

        public string ip
        {
            get { return _ip; }
        }

        public int port
        {
            get { return _port; }
        }

        public string alias
        {
            get { return _alias; }
        }

        public List<Record> dataFields
        {
            get {
                if (_dataFields == null)
                {
                    reload();
                }
                return _dataFields;
            }
        }

        public PLC(long id, int netid, string ip, int port, string alias, List<Record> dataFields)
        {
            this._id = id;
            this._netid = netid;
            this._ip = ip;
            this._port = port;
            this._alias = alias;
            this._dataFields = dataFields;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}.{2}", ip, port, id);
        }

        public void reload()
        {
            _dataFields = ModelUtil.getItemList(_id);
        }

    }
}
