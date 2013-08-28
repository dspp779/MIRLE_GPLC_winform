using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MIRLE_GPLC.Security
{
    internal static class CryptoUtil
    {

        internal static string encryptSHA1(string str)
        {
            byte[] data = Encoding.Default.GetBytes(str);
            SHA1 sha = new SHA1CryptoServiceProvider();
            return Convert.ToBase64String(sha.ComputeHash(data));
        }
    }
}
