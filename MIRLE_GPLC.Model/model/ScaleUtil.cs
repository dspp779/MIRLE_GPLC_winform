using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    internal static class ScaleUtil
    {
        internal static ValueType getVal(byte[] val, DataType type)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(val);
            }

            switch (type)
            {
                case DataType.WORD:
                    return BitConverter.ToInt16(val , 0);
                case DataType.LONG:
                    return BitConverter.ToInt64(val, 0);
                case DataType.LONG_SWAP:
                    return BitConverter.ToInt64(swap(val), 0);
                case DataType.FLOAT:
                    return BitConverter.ToSingle(val, 0);
                case DataType.FLOAT_SWAP:
                    return BitConverter.ToSingle(swap(val), 0);
                default:
                    throw new Exception();
            }
        }
        private static byte[] swap(byte[] val)
        {
            swap(val, 0, 3);
            swap(val, 1, 2);
            return val;
        }
        private static void swap(byte[] val, int i, int j)
        {
            val[i] += val[j];
            val[j] = (byte)(val[i] - val[j]);
            val[i] = (byte)(val[i] - val[j]);
        }

    }
}
