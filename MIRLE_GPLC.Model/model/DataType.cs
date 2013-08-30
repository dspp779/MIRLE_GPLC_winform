using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public enum DataType
    {
        NONE = 0,
        WORD = 1,
        LONG = 2,
        FLOAT = 3,
        LONG_SWAP = 4,
        FLOAT_SWAP = 5
    }

    public static class DataTypeUtil
    {
        public static ushort size(DataType type)
        {
            switch(type)
            {
                case DataType.NONE:
                    return 0;
                case DataType.WORD:
                    return 1;
                case DataType.LONG:
                case DataType.LONG_SWAP:
                    return 4;
                case DataType.FLOAT:
                case DataType.FLOAT_SWAP:
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
