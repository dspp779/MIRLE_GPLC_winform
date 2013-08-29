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
        private int _polling_rate;
        private List<Tag> _tags;

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

        public int polling_rate
        {
            get { return _polling_rate; }
        }

        public List<Tag> tags
        {
            get {
                if (_tags == null)
                {
                    reload();
                }
                return _tags;
            }
        }

        public PLC(long id, string alias, int netid, string ip, int port, int polling_rate, List<Tag> tags)
        {
            this._id = id;
            this._netid = netid;
            this._ip = ip;
            this._port = port;
            this._alias = alias;
            this._polling_rate = polling_rate;
            this._tags = tags;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}.{2}", ip, port, id);
        }

        public void reload()
        {
            _tags = ModelUtil.getTagList(_id);
        }

    }
}
