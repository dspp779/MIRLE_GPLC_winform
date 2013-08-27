using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Security
{
    public class UnauthorizedException : Exception
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
                return authority + " authority required to do such operation.";
            }
        }
    }
}
