using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MIRLE.GPLC.Model.SQLite;
using System.Data.SQLite;
using System.Data.Common;

namespace MIRLE_GPLC.Model
{
    public class ModelUtil
    {
        #region -- add model --

        public static void addProject(long id, string name, string addr, double lat, double lng)
        {
            SQLiteDBMS.execUpdate("INSERT INTO Project (id, name, addr, lat, lng) "
                + "values (" + id + ", '" + name + "', '" + addr + "', " + lat + ", " + lng
                + ")");
        }

        public static void addProject(ProjectData p)
        {
            addProject(p.id, p.name, p.addr, p.lat, p.lng);
        }

        public static void addPLC(long plc_id, int id, string ip, int port, long project_id)
        {
            SQLiteDBMS.execUpdate("INSERT INTO PLC (plc_id, id, ip, port, project_id) "
                + "values (" + plc_id + ", " + id + ", '" + ip + "', " + port + ", "
                + project_id + ")");
        }


        public static void addDataField(long plc_id, string start_addr, int length, string format, string alias)
        {
            SQLiteDBMS.execUpdate("INSERT INTO DataField (plc_id, start_addr, length, format, alias) "
                + "values (" + plc_id + ", " + start_addr + ", " + length + ", '" + format + "', '" + alias
                + "')");
        }

        public static void addDataFields(List<Record> list, long plc_id)
        {
            using (SQLiteConnection conn = SQLiteDBMS.getConnection())
            {
                conn.Open();
                SQLiteCommand cmd;

                // optimize for bulk insert
                using (cmd = new SQLiteCommand("begin", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                foreach (Record r in list)
                {
                    string sql = "INSERT INTO DataField (plc_id, start_addr, length, format, alias) "
                    + "values (" + plc_id + ", " + r.addr + ", " + r.length + ", " + r.format + ", "
                    + r.alias + ")";
                    using (cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                // end of optimization for bulk insert
                using (cmd = new SQLiteCommand("end", conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        #endregion

        #region -- get model list --

        public static List<ProjectData> getProjectList()
        {
            try
            {
                List<ProjectData> pList = new List<ProjectData>();
                using (SQLiteConnection conn = SQLiteDBMS.getConnection())
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Project", conn);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pList.Add(
                                new ProjectData(reader.GetInt64(0), reader.GetString(1),
                                reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), null)
                            );
                        }
                    }
                }
                return pList;
            }
            catch (SQLiteException)
            {
                SQLiteDBMS.execUpdate("CREATE TABLE Project"
                    + "(id INTEGER PRIMARY KEY AUTOINCREMENT,"
                    + "name TEXT, addr TEXT, lat REAL, lng REAL)"
                );

                return new List<ProjectData>();
            }
            catch (Exception)
            {
                return new List<ProjectData>();
            }
        }

        public static List<PLC> getPLCList(long project_id)
        {
            try
            {
                using (SQLiteConnection conn = SQLiteDBMS.getConnection())
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(
                        "SELECT * FROM PLC WHERE project_id =" + project_id, conn);
                    List<PLC> pList = new List<PLC>();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long plc_id = reader.GetInt64(0);
                            PLC p = new PLC(plc_id, reader.GetInt32(1),
                                reader.GetString(2), reader.GetInt32(3),
                                getDataFields(plc_id));
                            pList.Add(p);
                        }
                    }
                    return pList;
                }
            }
            catch (SQLiteException)
            {
                SQLiteDBMS.execUpdate("CREATE TABLE PLC ("
                    + "plc_id INTEGER PRIMARY KEY AUTOINCREMENT,"
                    + "id INT, ip VARCHAR(15), port INT,"
                    + "project_id INTEGER)"
                );

                return new List<PLC>();
            }
            catch (Exception)
            {
                return new List<PLC>();
            }
        }

        private static List<Record> getDataFields(long plc_id)
        {
            try
            {
                using (SQLiteConnection conn = SQLiteDBMS.getConnection())
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(
                        "SELECT * FROM DataField WHERE plc_id =" + plc_id, conn);
                    List<Record> list = new List<Record>();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Record(reader.GetInt64(0),
                                reader.GetInt32(1), reader.GetInt32(2),
                                reader.GetString(3), reader.GetString(4)));
                        }
                    }
                    return list;
                }
            }
            catch (SQLiteException)
            {
                SQLiteDBMS.execUpdate("CREATE TABLE PLC ("
                    + "plc_id INTEGER PRIMARY KEY AUTOINCREMENT,"
                    + "id INT, ip VARCHAR(15), port INT,"
                    + "project_id INTEGER)"
                );

                return new List<Record>();
            }
            catch (Exception)
            {
                return new List<Record>();
            }
        }

        #endregion

        #region -- modify model --

        public static void updateProject(long id, string name, string addr, double lat, double lng)
        {
            SQLiteDBMS.execUpdate("UPDATE Project SET   "
                + "name='" + name + "', addr='" + addr + "', lat=" + lat + ", lng=" + lat
                + "WHERE id=" + id);
        }

        #endregion
    }
}
