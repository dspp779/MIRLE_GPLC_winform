using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public class Scaling
    {
        private DataType _scale_type;
        private double _raw_hi;
        private double _raw_lo;
        private double _scale_hi;
        private double _scale_lo;

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
    }
}
