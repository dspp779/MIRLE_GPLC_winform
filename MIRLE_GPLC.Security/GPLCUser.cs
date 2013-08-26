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
        private GPLCAuthority _authority = GPLCAuthority.Anonymous;

        public GPLCAuthority access
        {
            get { return _authority; }
        }

        public GPLCUser()
        {

        }

        public GPLCUser(string username, string pass)
        {
        }

        private static GPLCAuthority Authenticate(string id, string pass)
        {
            try
            {
                using (SQLiteConnection conn = SQLiteDBMS.getConnection())
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT Authentication FROM User WHERE id=@id AND pass=@pass", conn);
                    cmd.Parameters.Add("@id", DbType.String).Value = id;
                    cmd.Parameters.Add("@pass", DbType.String).Value = pass;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            String str = reader.GetString(0);
                        }
                        else
                        {
                            return GPLCAuthority.Anonymous;
                        }
                    }
                }
            }
            catch (SQLiteException)
            {
                createSchema();
            }
            return GPLCAuthority.Anonymous;
        }

        private static void createSchema()
        {
            try
            {
                using (SQLiteCommand sql = new SQLiteCommand("CREATE TABLE User (id varchar(20), pass varchar(20))"))
                {
                    SQLiteDBMS.execUpdate(sql);
                }
            }
            catch (SQLiteException)
            {
            }
        }
    }
}
