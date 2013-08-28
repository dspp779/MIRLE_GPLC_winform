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

        public PLC(long id, int netid, string ip, int port, string alias, List<Tag> tags)
        {
            this._id = id;
            this._netid = netid;
            this._ip = ip;
            this._port = port;
            this._alias = alias;
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
