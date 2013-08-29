﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class Tag
    {
        private long _id;
        private int _addr;
        private DataType _type;
        private string _format;
        private string _unit;
        private string _alias;
        private long _plcid;

        public Scaling scale;

        public long id
        {
            get { return _id; }
        }
        public int addr
        {
            get { return _addr; }
        }
        public DataType type
        {
            get { return _type; }
        }
        public string format
        {
            get { return _format; }
        }
        public string unit
        {
            get { return _unit; }
        }
        public string alias
        {
            get { return _alias; }
        }
        public long plc_id
        {
            get { return _plcid; }
        }

        public Tag(long id, string alias, int addr, DataType type, string format, string unit, long plcid)
        {
            this._id = id;
            this._addr = addr;
            this._type = type;
            this._format = format;
            this._unit = unit;
            this._alias = alias;
            this._plcid = plcid;
        }

        public Tag(long id, string alias, int addr, string type, string format, string unit, long plcid)
            : this(id, alias, addr, (DataType)System.Enum.Parse(typeof(DataType), type), format, unit, plcid)
        {
        }
    }
}