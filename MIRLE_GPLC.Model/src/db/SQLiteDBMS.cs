using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace MIRLE.GPLC.Model.SQLite
{
    public class SQLiteDBMS
    {
        public static SQLiteConnection getConnection()
        {
            if (!File.Exists("default.sql"))
            {
                SQLiteConnection.CreateFile("default.sql");
            }
            return new SQLiteConnection("Data Source=default.sql");
        }

        public static SQLiteConnection getConnection(string path)
        {
            if (!File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
            }
            return new SQLiteConnection("Data Source=" + path);
        }

        public static DataTable execQuery(SQLiteCommand cmd)
        {
            using (SQLiteConnection conn = getConnection())
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }

        public static int execUpdate(SQLiteCommand cmd)
        {
            using (SQLiteConnection conn = getConnection())
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
