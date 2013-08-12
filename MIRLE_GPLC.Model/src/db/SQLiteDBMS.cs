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

        public static DataTable execQuery(string sql)
        {
            using (SQLiteConnection conn = getConnection())
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }

        public static int execUpdate(string sql)
        {
            using (SQLiteConnection conn = getConnection())
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
