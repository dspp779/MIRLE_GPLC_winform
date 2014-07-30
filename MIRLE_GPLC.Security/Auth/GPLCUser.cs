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

        // get auth of anonymous
        public void Authenticate()
        {
            _authority = GPLCAuthority.Anonymous;
        }
        // get auth by id and password
        public void Authenticate(string id, string pass)
        {
            this.id = id;
            this.pass = pass;
            try
            {
                /* create user and set its authority
                 */
                //SecureUtil.newUser(id, pass, GPLCAuthority.Administrator);
                
                // verify id and passwod
                _authority = SecureUtil.Authenticate(id, pass);
            }
            catch (WrongIdPassException ex)
            {
                _authority = GPLCAuthority.Anonymous;
                throw ex;
            }
        }

        // check if the user has the authority equal to or higher than a specific auth
        public void Authenticate(GPLCAuthority auth)
        {
            if (authority > auth)
            {
                throw new UnauthorizedException(auth);
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
