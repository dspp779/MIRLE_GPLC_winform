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

        public static void copyTo(string path)
        {
            using (SQLiteConnection db1 = getConnection(dataSource), db2 = getConnection(path))
            {
                db1.Open();
                db2.Open();
                //db1.BackupDatabase(db2, path, dataSource, -1, null, 0);
                //db2.BackupDatabase(db1, path, dataSource, -1, null, 0);
            }
        }

        public static SQLiteConnection getConnection()
        {
            return getConnection(dataSource);
        }


        private static SQLiteConnection getConnection(string path)
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
