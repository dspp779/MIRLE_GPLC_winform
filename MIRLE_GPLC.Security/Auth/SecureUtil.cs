using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MIRLE.GPLC.DB.MySql;

namespace MIRLE_GPLC.Security
{
    internal static class SecureUtil
    {
        // check authentication from database
        internal static GPLCAuthority Authenticate(string id, string pass)
        {
            try
            {
                using (MySqlConnection conn = MySqlDbInterface.getConnection())
                {
                    conn.Open();
                    MySqlCommand cmd =
                        new MySqlCommand("SELECT auth FROM User WHERE id=@id AND pass=@pass", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@pass", CryptoUtil.encryptSHA1(pass));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            String str = reader.GetString(0);
                            return (str.Equals("Administrator")) ? GPLCAuthority.Administrator : GPLCAuthority.Operator;
                        }
                        else
                        {
                            throw new WrongIdPassException();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.ErrorCode == 1)
                {
                    createSchema();
                }

            }
            return GPLCAuthority.Anonymous;
        }
        // create a new user
        internal static void newUser(string id, string pass, GPLCAuthority auth)
        {
            try
            {
                using (MySqlCommand sql = new MySqlCommand("INSERT INTO User values(@id, @pass, @auth)"))
                {
                    sql.Parameters.AddWithValue("@id", id);
                    sql.Parameters.AddWithValue("@pass", CryptoUtil.encryptSHA1(pass));
                    sql.Parameters.AddWithValue("@auth", auth.ToString());
                    MySqlDbInterface.execUpdate(sql);
                }
            }
            catch (MySqlException ex)
            {
                switch(ex.Number)
                {
                    case 1146:
                        createSchema();
                        newUser(id, pass, auth);
                        break;
                    default:
                        break;
                }
            }
        }
        // create user schema
        private static void createSchema()
        {
            try
            {
                using (MySqlCommand sql = new MySqlCommand("CREATE TABLE User (id varchar(20) PRIMARY KEY, pass text, auth text)"))
                {
                    MySqlDbInterface.execUpdate(sql);
                }
            }
            catch (MySqlException)
            {
            }
        }
    }
}
