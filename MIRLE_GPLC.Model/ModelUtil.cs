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

        public static int addProject(ProjectData p)
        {
            return addProject(p.id, p.name, p.addr, p.lat, p.lng);
        }

        public static int addProject(long id, string name, string addr, double lat, double lng)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO Project (id, name, addr, lat, lng) values (@id, @name, @addr, @lat, @lng)"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                cmd.Parameters.Add("@name", DbType.String).Value = name;
                cmd.Parameters.Add("@addr", DbType.String).Value = addr;
                cmd.Parameters.Add("@lat", DbType.Double).Value = lat;
                cmd.Parameters.Add("@lng", DbType.Double).Value = lng;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int addPLC(long plc_id, int id, string ip, int port, long project_id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO PLC (plc_id, id, ip, port, project_id) values (@plc_id, @id, @ip, @port, @project_id)"))
            {
                cmd.Parameters.Add("@plc_id", DbType.Int64).Value = plc_id;
                cmd.Parameters.Add("@id", DbType.Int32).Value = id;
                cmd.Parameters.Add("@ip", DbType.String).Value = ip;
                cmd.Parameters.Add("@port", DbType.Int32).Value = port;
                cmd.Parameters.Add("@project_id", DbType.Int64).Value = project_id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }


        public static int addDataField(long plc_id, string start_addr, int length, string format, string alias)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO DataField (plc_id, start_addr, length, format, alias) "
                + "values (@plc_id, @start_addr, @length, @format, @alias)"))
            {
                cmd.Parameters.Add("@plc_id", DbType.Int64).Value = plc_id;
                cmd.Parameters.Add("@start_addr", DbType.Int32).Value = start_addr;
                cmd.Parameters.Add("@length", DbType.Int32).Value = length;
                cmd.Parameters.Add("@format", DbType.String).Value = format;
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                return SQLiteDBMS.execUpdate(cmd);
            }

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
                    + "values (@plc_id, @addr, @length , @format, @alias)";
                    using (cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@plc_id", DbType.Int64).Value = plc_id;
                        cmd.Parameters.Add("@start_addr", DbType.Int32).Value = r.addr;
                        cmd.Parameters.Add("@length", DbType.Int32).Value = r.length;
                        cmd.Parameters.Add("@format", DbType.String).Value = r.format;
                        cmd.Parameters.Add("@alias", DbType.String).Value = r.alias;
                        cmd.ExecuteNonQuery();
                    }
                }

                // end of optimization for bulk insert
                using (cmd = new SQLiteCommand("end", conn))
                {
                    cmd.ExecuteNonQuery();
                }
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
                using (SQLiteCommand cmd = new SQLiteCommand("CREATE TABLE Project ("
                    + "id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, addr TEXT, lat REAL, lng REAL)"))
                {
                    SQLiteDBMS.execUpdate(cmd);
                }

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
                using (SQLiteCommand cmd = new SQLiteCommand("CREATE TABLE PLC ("
                    + "plc_id INTEGER PRIMARY KEY AUTOINCREMENT, id INT, ip VARCHAR(15), "
                    + "port INT, project_id INTEGER)"))
                {
                    SQLiteDBMS.execUpdate(cmd);
                }

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
                using (SQLiteCommand cmd = new SQLiteCommand("CREATE TABLE DataField ("
                    + "plc_id INTEGER, start_addr INT, length INT, format VARCHAR(10), "
                    + "alias VARCHAR(20))"))
                {
                    SQLiteDBMS.execUpdate(cmd);
                }

                return new List<Record>();
            }
            catch (Exception)
            {
                return new List<Record>();
            }
        }

        #endregion

        #region -- modify model --

        public static int updateProject(long id, string name, string addr, double lat, double lng)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE Project SET name=@name, addr=@addr, lat=@lat, lng=@lng WHERE id=@id"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                cmd.Parameters.Add("@name", DbType.String).Value = name;
                cmd.Parameters.Add("@addr", DbType.String).Value = addr;
                cmd.Parameters.Add("@lat", DbType.Double).Value = lat;
                cmd.Parameters.Add("@lng", DbType.Double).Value = lng;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int updatePLC(long plc_id, int id, string ip, int port, long project_id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE PLC SET id=@id, ip=@ip, port=@port, project_id=@project_id WHERE plc_id=@plc_id"))
            {
                cmd.Parameters.Add("@plc_id", DbType.Int64).Value = plc_id;
                cmd.Parameters.Add("@id", DbType.Int32).Value = id;
                cmd.Parameters.Add("@ip", DbType.String).Value = ip;
                cmd.Parameters.Add("@port", DbType.Int32).Value = port;
                cmd.Parameters.Add("@project_id", DbType.Int64).Value = project_id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int updateDataField(long plc_id, string start_addr, int length, string format, string alias)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE DataField SET start_addr=@start_addr, length=@length, format=@format, alias=@alias"
                + "' WHERE plc_id=@plc_id"))
            {
                cmd.Parameters.Add("@plc_id", DbType.Int64).Value = plc_id;
                cmd.Parameters.Add("@start_addr", DbType.Int32).Value = start_addr;
                cmd.Parameters.Add("@length", DbType.Int32).Value = length;
                cmd.Parameters.Add("@format", DbType.String).Value = format;
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        #endregion
    }
}
