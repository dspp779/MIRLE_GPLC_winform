using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Security.Exception
{
    public class UnauthorizedException : UnauthorizedAccessException
    {
        private GPLCAuthority authority;

        public UnauthorizedException(GPLCAuthority authority)
        {
            this.authority = authority;
        }

        public override string Message
        {
            get
            {
                string str = (authority == GPLCAuthority.Administrator) ?
                    "Operator" : "Administrator";
                return str + " authority needed to do such operation.";
            }
        }
    }
}
