using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using MIRLE.GPLC.DB.SQLite;
using System.Data.SQLite;
using System.Data;

namespace MIRLE_GPLC.Security
{
    public class GPLCUser
    {
        private string id;
        private string pass;
        private GPLCAuthority _authority = GPLCAuthority.Anonymous;

        public GPLCAuthority authority
        {
            get
            {
                //Authenticate();
                return _authority;
            }
        }

        public GPLCUser()
        {
        }

        public GPLCUser(string id, string pass)
        {
            this.id = id;
            this.pass = pass;
        }

        public void Authenticate()
        {
            _authority = GPLCAuthority.Anonymous;
        }

        // check if the user has the authority equal to or higher than a specific auth
        public void Authenticate(GPLCAuthority auth)
        {
            if (authority > auth)
            {
                throw new UnauthorizedException(auth);
            }
        }

        // get auth by id and password
        public void Authenticate(string id, string pass)
        {
            this.id = id;
            this.pass = pass;
            try
            {
                SecureUtil.newUser(id, pass, GPLCAuthority.Administrator);
                _authority = SecureUtil.Authenticate(id, pass);
            }
            catch (WrongIdPassException ex)
            {
                _authority = GPLCAuthority.Anonymous;
                throw ex;
            }
        }

        // auth information string
        public string authInfo()
        {
            switch (authority)
            {
                case GPLCAuthority.Administrator:
                case GPLCAuthority.Operator:
                    return id + "/" + authority.ToString();
                case GPLCAuthority.Anonymous:
                    return authority.ToString();
                default:
                    return "";
            }
        }
    }
}
