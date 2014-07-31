using MIRLE_GPLC.Model.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class Tag
    {
        private string _deviceName;
        private string _alias;
        private List<Record> _records;

        public string alias
        {
            get { return _alias; }
        }
        public string deviceName
        {
            get { return _deviceName; }
        }

        public List<Record> records
        {
            get
            {
                if (_records == null)
                {
                    reload();
                }
                return _records;
            }
        }

        public Tag(string alias, string deviceName)
        {
            this._alias = alias;
            this._deviceName = deviceName;
        }

        public Tag(string alias, string deviceName, List<Record> records)
        {
            this._alias = alias;
            this._deviceName = deviceName;
            this._records = records;
        }

        private void reload()
        {
            _records = ModelUtil.getTagVal(_alias, _deviceName);
        }

        public float? getVal()
        {
            int i;
            Random r = new Random();
            do
            {
                i = r.Next(0, records.Count - 1);
            } while (records[i].val == null);
            return records[i].val;
        }
    }
}
