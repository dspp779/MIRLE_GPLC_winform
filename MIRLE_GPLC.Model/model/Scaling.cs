using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class Scaling
    {
        internal DataType _scale_type;
        internal double _raw_hi;
        internal double _raw_lo;
        internal double _scale_hi;
        internal double _scale_lo;

        public DataType scale_type
        {
            get { return _scale_type; }
        }

        public double raw_hi
        {
            get { return _raw_hi; }
        }

        public double raw_lo
        {
            get { return _raw_lo; }
        }

        public double scale_hi
        {
            get { return _scale_hi; }
        }

        public double scale_lo
        {
            get { return _scale_lo; }
        }


        public Scaling(DataType scale_type, double raw_hi, double raw_lo, double scale_hi, double scale_lo)
        {
            this._raw_hi = raw_hi;
            this._raw_lo = raw_lo;
            this._scale_type = scale_type;
            this._scale_hi = scale_hi;
            this._scale_lo = scale_lo;
        }

        public Scaling(string scale_type, double raw_hi, double raw_lo, double scale_hi, double scale_lo)
            : this((DataType)System.Enum.Parse(typeof(DataType), scale_type), raw_hi, raw_lo, scale_hi, scale_lo)
        {
        }

        public Scaling(string scale_type, string raw_hi, string raw_lo, string scale_hi, string scale_lo)
            : this((DataType)System.Enum.Parse(typeof(DataType), scale_type),
            double.Parse(raw_hi), double.Parse(raw_lo), double.Parse(scale_hi), double.Parse(scale_lo))
        {
        }

        public ValueType Scale(double val)
        {
            double r = val * (scale_hi - scale_lo) / (raw_hi - raw_lo) + scale_lo;
            switch(scale_type)
            {
                case DataType.WORD:
                    return Convert.ToInt16(r);
                case DataType.LONG:
                    return Convert.ToInt64(r);
                case DataType.FLOAT:
                    return Convert.ToSingle(r);
                default:
                    throw new Exception();
            }
        }

    }
}
