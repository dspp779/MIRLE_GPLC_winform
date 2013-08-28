using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MIRLE.GPLC.DB.SQLite;
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
            string schema =
                "CREATE TABLE Project ( id INTEGER, name TEXT, addr TEXT, lat REAL, lng REAL )";
            executeUpdate(schema);
        }

        private static void createPLCTable()
        {
            string schema = "CREATE TABLE PLC ( id INTEGER PRIMARY KEY AUTOINCREMENT,"
                + " alias VARCHAR(20),net_id INT, net_ip VARCHAR(15), net_port INT, "
                + " polling_rate INT, project_id INTEGER )";
            executeUpdate(schema);
        }

        private static void createTagTable()
        {
            string schema = "CREATE TABLE Tag ( id INTEGER PRIMARY KEY AUTOINCREMENT,"
                + " alias VARCHAR(20), addr INT, data_type VARCHAR(10), format VARCHAR(5), unit VARCHAR(10),"
                + " raw_hi REAL, raq_lo REAL, scale_type VARCHAR(10), scale_hi REAL, scale_lo REAL,"
                + " plc_id INTEGER )";
            executeUpdate(schema);
        }

        #endregion

        #region -- insert model --

        public static int insertProject(ProjectData p)
        {
            return insertProject(p.id, p.name, p.addr, p.lat, p.lng);
        }
        public static int insertProject(long id, string name, string addr, double lat, double lng)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO Project values (@id, @name, @addr, @lat, @lng)"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                cmd.Parameters.Add("@name", DbType.String).Value = name;
                cmd.Parameters.Add("@addr", DbType.String).Value = addr;
                cmd.Parameters.Add("@lat", DbType.Double).Value = lat;
                cmd.Parameters.Add("@lng", DbType.Double).Value = lng;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int insertPLC(PLC plc, long project_id)
        {
            return insertPLC(plc.netid, plc.ip, plc.port, plc.alias, plc.polling_rate, project_id);
        }
        public static int insertPLC(int netid, string ip, int port, string alias, int polling_rate, long project_id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO PLC (net_id, net_ip, net_port, alias, polling_rate, project_id) "
                + "values (@net_id, @net_ip, @net_port, @alias, @polling_rate, @project_id)"))
            {
                cmd.Parameters.Add("@net_id", DbType.Int32).Value = netid;
                cmd.Parameters.Add("@net_ip", DbType.String).Value = ip;
                cmd.Parameters.Add("@net_port", DbType.Int32).Value = port;
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                cmd.Parameters.Add("@polling_rate", DbType.Int32).Value = polling_rate;
                cmd.Parameters.Add("@project_id", DbType.Int64).Value = project_id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int insertTag(Tag r, long plc_id)
        {
            return insertTag(r.addr, r.length, r.format, r.alias,  plc_id);
        }
        public static int insertTag(int start_addr, int length, string format, string alias, long plc_id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO Tag (start_addr, length, format, alias, plc_id) "
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
        public static void insertTag(List<Tag> list, long plc_id)
        {
            using (SQLiteConnection conn = SQLiteDBMS.getConnection())
            {
                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO Tag (id, start_addr, length, format, alias, plc_id)"
                            + " values (@id, @start_addr, @length, @format, @alias, @plc_id)";
                        cmd.Parameters.Add("@id", DbType.Int64);
                        cmd.Parameters.Add("@start_addr", DbType.Int32);
                        cmd.Parameters.Add("@length", DbType.Int32);
                        cmd.Parameters.Add("@format", DbType.String);
                        cmd.Parameters.Add("@alias", DbType.String);
                        cmd.Parameters.Add("@plc_id", DbType.Int64);
                        foreach (Tag tag in list)
                        {
                            insertTag(cmd, tag, plc_id);
                        }
                    }
                    transaction.Commit();
                }
            }
        }
        private static int insertTag(SQLiteCommand cmd, Tag tag, long id)
        {
            cmd.Parameters["@id"].Value = tag.id;
            cmd.Parameters["@start_addr"].Value = tag.addr;
            cmd.Parameters["@length"].Value = tag.length;
            cmd.Parameters["@format"].Value = tag.format;
            cmd.Parameters["@alias"].Value = tag.alias;
            cmd.Parameters["@plc_id"].Value = id;
            return cmd.ExecuteNonQuery();
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
                                reader.GetString(4), reader.GetInt32(5), null);
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

        public static List<Tag> getTagList(long plc_id)
        {
            try
            {
                using (SQLiteConnection conn = SQLiteDBMS.getConnection())
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(
                        "SELECT * FROM Tag WHERE plc_id =" + plc_id, conn);
                    List<Tag> list = new List<Tag>();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Tag(reader.GetInt64(0),
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
                createTagTable();
                return new List<Tag>();
            }
            catch (Exception)
            {
                return new List<Tag>();
            }
        }

        #endregion

        #region -- update model --

        public static int updateProject(long id, string name, string addr, double lat, double lng, long oid)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE Project SET id=@id, name=@name, addr=@addr, lat=@lat, lng=@lng WHERE id=@oid"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                cmd.Parameters.Add("@name", DbType.String).Value = name;
                cmd.Parameters.Add("@addr", DbType.String).Value = addr;
                cmd.Parameters.Add("@lat", DbType.Double).Value = lat;
                cmd.Parameters.Add("@lng", DbType.Double).Value = lng;
                cmd.Parameters.Add("@oid", DbType.Int64).Value = oid;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int updatePLC(PLC plc)
        {
            return updatePLC(plc.id, plc.netid, plc.ip, plc.port, plc.alias, plc.polling_rate);
        }
        public static int updatePLC(long id, int netid, string ip, int port, string alias, int polling_rate)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE PLC SET net_id=@net_id, net_ip=@net_ip, net_port=@net_port, alias=@alias,"
                + " polling_rate=@polling_rate WHERE id=@id"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                cmd.Parameters.Add("@net_id", DbType.Int32).Value = netid;
                cmd.Parameters.Add("@net_ip", DbType.String).Value = ip;
                cmd.Parameters.Add("@net_port", DbType.Int32).Value = port;
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                cmd.Parameters.Add("@polling_rate", DbType.Int32).Value = polling_rate;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int updateTag(Tag r)
        {
            return updateTag(r.id, r.addr, r.length, r.format, r.alias);
        }
        public static int updateTag(long id, int start_addr, int length, string format, string alias)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE Tag SET start_addr=@start_addr, length=@length, format=@format, alias=@alias"
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
            deletePLCs(id);
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM Project WHERE id=@id"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int deletePLC(long id)
        {
            deleteTags(id);
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM PLC WHERE id=@id"))    
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }
        private static int deletePLCs(long project_id)
        {
            foreach (PLC plc in getPLCList(project_id))
            {
                deleteTags(plc.id);
            }
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM PLC WHERE project_id=@project_id"))
            {
                cmd.Parameters.Add("@project_id", DbType.Int64).Value = project_id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int deleteTag(long id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM Tag WHERE id=@id"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }
        private static int deleteTags(long plc_id)
        {
            // delete items belonging to the plc
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM Tag WHERE plc_id=@plcid"))
            {
                cmd.Parameters.Add("@plcid", DbType.Int64).Value = plc_id;
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

        public static void inputTag(Tag tag)
        {
            try
            {
                updateTag(tag);
            }
            catch (SQLiteException)
            {
                insertTag(tag, tag.plc_id);
            }
        }

        #endregion

        #region -- db file operation --

        public static void setPath(string path)
        {
            SQLiteDBMS.setDBPath(path);
        }
        public static void copyTo(string path)
        {
            SQLiteDBMS.copyTo(path);
        }
        
        #endregion
    }
}
