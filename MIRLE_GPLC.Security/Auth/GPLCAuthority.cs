using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRLE_GPLC.Security
{
    // enum for authority
    public enum GPLCAuthority : byte
    {
        Administrator = 0,
        Operator = 1,
        Anonymous = 2,
    }
}
