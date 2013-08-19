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

        private static int executeUpdate(string sql)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(sql))
            {
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        #region -- table creation --

        private static void createProjectTable()
        {
            string schema = "CREATE TABLE Project ( id INTEGER PRIMARY KEY AUTOINCREMENT, "
                + "name TEXT, addr TEXT, lat REAL, lng REAL)";
            executeUpdate(schema);
        }

        private static void createPLCTable()
        {
            string schema = "CREATE TABLE PLC ( id INTEGER PRIMARY KEY AUTOINCREMENT, "
                + "net_id INT, net_ip VARCHAR(15), net_port INT, alias VARCHAR(20), project_id INTEGER)";
            executeUpdate(schema);
        }

        private static void createItemTable()
        {
            string schema = "CREATE TABLE Item ( id INTEGER PRIMARY KEY AUTOINCREMENT, "
                + "start_addr INT, length INT, format VARCHAR(10), alias VARCHAR(20), plc_id INTEGER)";
            executeUpdate(schema);
        }

        #endregion

        #region -- insert model --

        public static int insertProject(ProjectData p)
        {
            return insertProject(p.name, p.addr, p.lat, p.lng);
        }
        public static int insertProject(string name, string addr, double lat, double lng)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO Project (name, addr, lat, lng) values (@name, @addr, @lat, @lng)"))
            {
                cmd.Parameters.Add("@name", DbType.String).Value = name;
                cmd.Parameters.Add("@addr", DbType.String).Value = addr;
                cmd.Parameters.Add("@lat", DbType.Double).Value = lat;
                cmd.Parameters.Add("@lng", DbType.Double).Value = lng;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int insertPLC(PLC plc, long project_id)
        {
            return insertPLC(plc.netid, plc.ip, plc.port, plc.alias, project_id);
        }
        public static int insertPLC(int netid, string ip, int port, string alias, long project_id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO PLC (net_id, net_ip, net_port, alias, project_id) "
                + "values (@net_id, @net_ip, @net_port, @alias, @project_id)"))
            {
                cmd.Parameters.Add("@net_id", DbType.Int32).Value = netid;
                cmd.Parameters.Add("@net_ip", DbType.String).Value = ip;
                cmd.Parameters.Add("@net_port", DbType.Int32).Value = port;
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                cmd.Parameters.Add("@project_id", DbType.Int64).Value = project_id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int insertItem(Record r, long plc_id)
        {
            return insertItem(r.addr, r.length, r.format, r.alias,  plc_id);
        }
        public static int insertItem(int start_addr, int length, string format, string alias, long plc_id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO Item (start_addr, length, format, alias, plc_id) "
                + "values (@start_addr, @length, @format, @alias, @plc_id)"))
            {
                cmd.Parameters.Add("@start_addr", DbType.Int32).Value = start_addr;
                cmd.Parameters.Add("@length", DbType.Int32).Value = length;
                cmd.Parameters.Add("@format", DbType.String).Value = format;
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                cmd.Parameters.Add("@plc_id", DbType.Int64).Value = plc_id;
                return SQLiteDBMS.execUpdate(cmd);
            }

        }
        public static void insertItem(List<Record> list, long plc_id)
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
                    string sql = "INSERT INTO Item (id, start_addr, length, format, alias, plc_id) "
                    + "values (@id, @start_addr, @length, @format, @alias, @plc_id)";
                    using (cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@id", DbType.Int64).Value = r.id;
                        cmd.Parameters.Add("@start_addr", DbType.Int32).Value = r.addr;
                        cmd.Parameters.Add("@length", DbType.Int32).Value = r.length;
                        cmd.Parameters.Add("@format", DbType.String).Value = r.format;
                        cmd.Parameters.Add("@alias", DbType.String).Value = r.alias;
                        cmd.Parameters.Add("@plc_id", DbType.Int64).Value = plc_id;
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
                createProjectTable();
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
                        "SELECT * FROM PLC WHERE project_id=" + project_id, conn);
                    List<PLC> pList = new List<PLC>();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = reader.GetInt64(0);
                            PLC p = new PLC(id, reader.GetInt32(1),
                                reader.GetString(2), reader.GetInt32(3),
                                reader.GetString(4), null);
                            pList.Add(p);
                        }
                    }
                    return pList;
                }
            }
            catch (SQLiteException)
            {
                createPLCTable();
                return new List<PLC>();
            }
            catch (Exception)
            {
                return new List<PLC>();
            }
        }

        public static List<Record> getItemList(long plc_id)
        {
            try
            {
                using (SQLiteConnection conn = SQLiteDBMS.getConnection())
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(
                        "SELECT * FROM Item WHERE plc_id =" + plc_id, conn);
                    List<Record> list = new List<Record>();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Record(reader.GetInt64(0),
                                reader.GetInt32(1), reader.GetInt32(2),
                                reader.GetString(3), reader.GetString(4),
                                reader.GetInt64(5)));
                        }
                    }
                    return list;
                }
            }
            catch (SQLiteException)
            {
                createItemTable();
                return new List<Record>();
            }
            catch (Exception)
            {
                return new List<Record>();
            }
        }

        #endregion

        #region -- update model --

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

        public static int updatePLC(PLC plc)
        {
            return updatePLC(plc.id, plc.netid, plc.ip, plc.port, plc.alias);
        }
        public static int updatePLC(long id, int netid, string ip, int port, string alias)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE PLC SET net_id=@net_id, net_ip=@net_ip, net_port=@net_port, alias=@alias WHERE id=@id"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                cmd.Parameters.Add("@net_id", DbType.Int32).Value = netid;
                cmd.Parameters.Add("@net_ip", DbType.String).Value = ip;
                cmd.Parameters.Add("@net_port", DbType.Int32).Value = port;
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int updateItem(Record r)
        {
            return updateItem(r.id, r.addr, r.length, r.format, r.alias);
        }
        public static int updateItem(long id, int start_addr, int length, string format, string alias)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE Item SET start_addr=@start_addr, length=@length, format=@format, alias=@alias"
                + " WHERE id=@id"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                cmd.Parameters.Add("@start_addr", DbType.Int32).Value = start_addr;
                cmd.Parameters.Add("@length", DbType.Int32).Value = length;
                cmd.Parameters.Add("@format", DbType.String).Value = format;
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        #endregion

        #region -- delete model --


        public static int deleteProject(long id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM Project WHERE id=@id"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int deletePLC(long id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM PLC WHERE id=@id"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int deleteItem(long id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM Item WHERE id=@id"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        #endregion

        #region -- input method --

        public static void inputPLC(PLC p, long project_id)
        {
            try
            {
                updatePLC(p);
            }
            catch (SQLiteException)
            {
                insertPLC(p, project_id);
            }
        }

        public static void inputItem(Record record)
        {
            try
            {
                updateItem(record);
            }
            catch (SQLiteException)
            {
                insertItem(record, record.plc_id);
            }
        }

        #endregion
    }
}
