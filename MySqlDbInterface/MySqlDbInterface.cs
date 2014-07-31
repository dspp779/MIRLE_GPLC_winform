using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace MIRLE.GPLC.DB.MySql
{
    public class MySqlDbInterface
    {
        // default db file path
        private static string dataSource = @"server=localhost;user id=root;Password= ;persist security info=True;database=mirle;charset=utf8";

        // get SQL connection
        public static MySqlConnection getConnection()
        {
            return getConnection(dataSource);
        }

        // get SQL connection with specified db file path
        private static MySqlConnection getConnection(string dataSource)
        {
            return new MySqlConnection(dataSource);
        }

        // execute an query SQL command such as SELECT
        public static MySqlDataReader execQuery(MySqlCommand cmd)
        {
            using (MySqlConnection conn = getConnection())
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteReader();
            }
        }

        /* execute an update SQL command such as CREATE TABLE, INSERT, UPDATE...etc.
         * return number of modified record
         * */
        public static int execUpdate(MySqlCommand cmd)
        {
            using (MySqlConnection conn = getConnection())
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
        }

        // get row ID of the last inserted record
        private static int getMaxId(MySqlConnection conn, string table)
        {
            MySqlCommand cmd = new MySqlCommand("select MAX(id) from @table", conn);
            cmd.Parameters.Add("@table", MySqlDbType.String).Value = table;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                return (reader.Read()) ? reader.GetInt32(0) : -1;
            }
        }

        /* execute an insert command
         * return row id of the inserted record
         * */
        public static int execInsert(MySqlCommand cmd)
        {
            using (MySqlConnection conn = getConnection())
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
