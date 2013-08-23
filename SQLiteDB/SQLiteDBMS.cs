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
        private static string dataSource = "default.sql";

        public static void setDBPath(string path)
        {
            dataSource = path;
        }
        
        public static SQLiteConnection getConnection()
        {
            if (!File.Exists(dataSource))
            {
                SQLiteConnection.CreateFile(dataSource);
            }
            return new SQLiteConnection("Data Source=" + dataSource);
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
