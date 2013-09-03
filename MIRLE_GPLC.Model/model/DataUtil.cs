using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Model
{
    public static class DataUtil
    {
        internal static ValueType getVal(byte[] val, DataType type)
        {
            // reverse the array if system is little endian
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(val);
            }

            // return value
            switch (type)
            {
                case DataType.WORD:
                    return BitConverter.ToInt16(val , 0);
                case DataType.LONG:
                    return BitConverter.ToInt64(val, 0);
                case DataType.LONG_SWAP:
                    // swap
                    swap(val, 0, 7);
                    swap(val, 1, 6);
                    swap(val, 2, 5);
                    swap(val, 3, 4);
                    return BitConverter.ToInt64(val, 0);
                case DataType.FLOAT:
                    return BitConverter.ToSingle(val, 0);
                case DataType.FLOAT_SWAP:
                    // swap
                    swap(val, 0, 3);
                    swap(val, 1, 2);
                    return BitConverter.ToSingle(val, 0);
                default:
                    throw new Exception();
            }
        }
        // swap to item in a byte array
        private static void swap(byte[] val, int i, int j)
        {
            val[i] += val[j];
            val[j] = (byte)(val[i] - val[j]);
            val[i] = (byte)(val[i] - val[j]);
        }
        // get word size of an data type
        public static ushort size(DataType type)
        {
            switch (type)
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
