using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace MIRLE.GPLC.DB.SQLite
{
    public static class SQLiteDBMS
    {
        // default db file path
        private static string dataSource = @"default.sql";

        // set db file path
        public static void setDBPath(string path)
        {
            dataSource = path;
        }

        // copy current db file to specified path
        public static void copyTo(string path)
        {
            System.IO.File.Copy(dataSource, path, true);
        }

        // get SQL connection
        public static SQLiteConnection getConnection()
        {
            return getConnection(dataSource);
        }

        // get SQL connection with specified db file path
        private static SQLiteConnection getConnection(string path)
        {
            if (!File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
            }
            return new SQLiteConnection(@"Data Source=" + path);
        }

        // execute an query SQL command such as SELECT
        public static DataTable execQuery(SQLiteCommand cmd)
        {
            using (SQLiteConnection conn = getConnection())
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }

        /* execute an update SQL command such as CREATE TABLE, INSERT, UPDATE...etc.
         * return number of modified record
         * */
        public static int execUpdate(SQLiteCommand cmd)
        {
            using (SQLiteConnection conn = getConnection())
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
        }

        // get row ID of the last inserted record
        private static int getLastInsertRowId(SQLiteConnection conn)
        {
            SQLiteCommand cmd = new SQLiteCommand("select last_insert_rowid()", conn);
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                return (reader.Read()) ? reader.GetInt32(0) : -1;
            }
        }

        /* execute an insert command
         * return row id of the inserted record
         * */
        public static int execInsert(SQLiteCommand cmd)
        {
            using (SQLiteConnection conn = getConnection())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                return getLastInsertRowId(conn);
            }
        }
    }
}
