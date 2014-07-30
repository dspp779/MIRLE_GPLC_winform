using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class PLC
    {
        private long _id;
        private string _name;
        private List<Tag> _tags;

        public long id
        {
            get { return _id; }
        }

        public string name
        {
            get { return _name; }
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

        public PLC(long id, string name, List<Tag> tags)
        {
            this._id = id;
            this._name= name;
            this._tags = tags;
        }

        public override string ToString()
        {
            List<ProjectData> plist = ModelUtil.getProjectList();
            return string.Format("{0}@{1}.{2}", name, plist[0].name, id);
        }

        public void reload()
        {
            _tags = new List<Tag>();
        }

    }
}
