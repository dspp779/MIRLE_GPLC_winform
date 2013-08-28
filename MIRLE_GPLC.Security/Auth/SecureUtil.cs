using MIRLE.GPLC.DB.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace MIRLE_GPLC.Security
{
    internal static class SecureUtil
    {
        internal static GPLCAuthority Authenticate(string id, string pass)
        {
            try
            {
                using (SQLiteConnection conn = SQLiteDBMS.getConnection())
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT auth FROM User WHERE id=@id AND pass=@pass", conn);
                    cmd.Parameters.Add("@id", DbType.String).Value = id;
                    cmd.Parameters.Add("@pass", DbType.String).Value = CryptoUtil.encryptSHA1(pass);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            String str = reader.GetString(0);
                            //str = CryptoUtil.Decrypt(str, id);
                            return (str.Equals("Administrator")) ? GPLCAuthority.Administrator : GPLCAuthority.Operator;
                        }
                        else
                        {
                            throw new WrongIdPassException();
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode == 1)
                {
                    createSchema();
                }

            }
            return GPLCAuthority.Anonymous;
        }

        internal static void newUser(string id, string pass, GPLCAuthority auth)
        {
            try
            {
                using (SQLiteCommand sql = new SQLiteCommand("INSERT INTO User values(@id, @pass, @auth)"))
                {
                    sql.Parameters.Add("@id", DbType.String).Value = id;
                    sql.Parameters.Add("@pass", DbType.String).Value = CryptoUtil.encryptSHA1(pass);
                    sql.Parameters.Add("@auth", DbType.String).Value = auth.ToString();
                    SQLiteDBMS.execUpdate(sql);
                }
            }
            catch (SQLiteException ex)
            {
                switch(ex.ErrorCode)
                {
                    case 1:
                        createSchema();
                        newUser(id, pass, auth);
                        break;
                    case 19:
                    default:
                        break;
                }
            }
        }
        private static void createSchema()
        {
            try
            {
                using (SQLiteCommand sql = new SQLiteCommand("CREATE TABLE User (id varchar(20) PRIMARY KEY, pass text, auth text)"))
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
