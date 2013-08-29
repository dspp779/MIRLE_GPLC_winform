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
        private static int getLastInsertRowId()
        {
            using (SQLiteConnection conn = SQLiteDBMS.getConnection())
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand("select last_insert_rowid()", conn);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    return (reader.Read()) ? reader.GetInt32(0) : -1;
                }
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
                + " alias VARCHAR(20), net_id INT, net_ip VARCHAR(15), net_port INT, "
                + " polling_rate INT, project_id INTEGER )";
            executeUpdate(schema);
        }

        private static void createTagTable()
        {
            string schema = "CREATE TABLE Tag ( id INTEGER PRIMARY KEY AUTOINCREMENT,"
                + " alias VARCHAR(20), addr INT, data_type VARCHAR(10), format VARCHAR(5), unit VARCHAR(10),"
                + " plc_id INTEGER )";
            executeUpdate(schema);
        }

        private static void createScalingTable()
        {
            string schema = "CREATE TABLE Scaling ( scale_type VARCHAR(10), raw_hi REAL, raw_lo REAL,"
                + " scale_hi REAL, scale_lo REAL, tag_id INTEGER )";
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
                SQLiteDBMS.execUpdate(cmd);
            }
            return getLastInsertRowId();
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
                SQLiteDBMS.execUpdate(cmd);
            }
            return getLastInsertRowId();
        }

        public static int insertTag(Tag tag)
        {
            return insertTag(tag.alias, tag.addr, tag.type, tag.format, tag.unit, tag.plc_id);
        }
        public static int insertTag(string alias, int addr, string type, string format, string unit, long plc_id)
        {
            DataType t = DataType.NONE;
            System.Enum.TryParse<DataType>(type, out t);
            return insertTag(alias, addr, t, format, unit, plc_id);
        }
        public static int insertTag(string alias, int addr, DataType type, string format, string unit, long plc_id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO Tag (alias, addr, data_type, format, unit, plc_id) "
                + "values (@alias, @addr, @data_type, @format, @unit, @plc_id)"))
            {
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                cmd.Parameters.Add("@addr", DbType.Int32).Value = addr;
                cmd.Parameters.Add("@data_type", DbType.String).Value = type.ToString();
                cmd.Parameters.Add("@format", DbType.String).Value = format;
                cmd.Parameters.Add("@unit", DbType.String).Value = unit;
                // foreigh
                cmd.Parameters.Add("@plc_id", DbType.Int64).Value = plc_id;
                SQLiteDBMS.execUpdate(cmd);
            }
            return getLastInsertRowId();

        }

        public static void insertTag(List<Tag> list, long plc_id)
        {
            using (SQLiteConnection conn = SQLiteDBMS.getConnection())
            {
                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            "INSERT INTO Tag (alias, addr, data_type, format, unit, raw_hi, raw_lo, scale_type, scale_hi, scale_lo, plc_id)"
                            + " values (@alias, @addr, @data_type, @format, @unit, @raw_hi, @raw_lo, @scale_type, @scale_hi, @scale_lo, @plc_id)";
                        cmd.Parameters.Add("@alias", DbType.String);
                        cmd.Parameters.Add("@addr", DbType.Int32);
                        cmd.Parameters.Add("@data_type", DbType.String);
                        cmd.Parameters.Add("@format", DbType.String);
                        cmd.Parameters.Add("@unit", DbType.String);
                        // scale
                        cmd.Parameters.Add("@scale_type", DbType.String);
                        cmd.Parameters.Add("@raw_hi", DbType.Double);
                        cmd.Parameters.Add("@raw_lo", DbType.Double);
                        cmd.Parameters.Add("@scale_hi", DbType.Double);
                        cmd.Parameters.Add("@scale_lo", DbType.Double);
                        // foreigh
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
            cmd.Parameters["@alias"].Value = tag.alias;
            cmd.Parameters["@addr"].Value = tag.addr;
            cmd.Parameters["@data_type"].Value = tag.type.ToString();
            cmd.Parameters["@format"].Value = tag.format;
            cmd.Parameters["@unit"].Value = tag.unit;
            // foreigh
            cmd.Parameters["@plc_id"].Value = id;
            return cmd.ExecuteNonQuery();
        }

        public static int insertScaling(Scaling s, long tag_id)
        {
            return insertScaling(s._scale_type.ToString(), s._raw_hi, s._raw_lo, s._scale_hi, s._scale_lo, tag_id);
        }
        public static int insertScaling(string scale_type, double raw_hi, double raw_lo, double scale_hi, double scale_lo, long tag_id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO Scaling (scale_type, raw_hi, raw_lo, scale_hi, scale_lo, tag_id) "
                + "values (@scale_type, @raw_hi, @raw_lo, @scale_hi, @scale_lo, @tag_id)"))
            {
                // scale
                cmd.Parameters.Add("@scale_type", DbType.String).Value = scale_type.ToString();
                cmd.Parameters.Add("@raw_hi", DbType.Double).Value = raw_hi;
                cmd.Parameters.Add("@raw_lo", DbType.Double).Value = raw_lo;
                cmd.Parameters.Add("@scale_hi", DbType.Double).Value = scale_hi;
                cmd.Parameters.Add("@scale_lo", DbType.Double).Value = scale_lo;
                cmd.Parameters.Add("@tag_id", DbType.Int64).Value = tag_id;
                SQLiteDBMS.execUpdate(cmd);
            }
            return getLastInsertRowId();
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
                            PLC p = new PLC(id, reader.GetString(1),
                                reader.GetInt32(2), reader.GetString(3),
                                reader.GetInt32(4), reader.GetInt32(5), null);
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
                            list.Add(new Tag(reader.GetInt64(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetString(3), reader.GetString(4),
                                reader.GetString(5), reader.GetInt64(6)
                            ));
                        }
                    }
                    return list;
                }
            }
            catch (SQLiteException)
            {
                createTagTable();
            }
            return new List<Tag>();
        }

        public static Scaling getScaling(long tag_id)
        {
            try
            {
                using (SQLiteConnection conn = SQLiteDBMS.getConnection())
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(
                        "SELECT * FROM Scaling WHERE tag_id =" + tag_id, conn);
                    List<Tag> list = new List<Tag>();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Scaling(reader.GetString(0), reader.GetDouble(1),
                                reader.GetDouble(2), reader.GetDouble(3), reader.GetDouble(4));
                        }
                    }
                }
            }
            catch (SQLiteException)
            {
                createScalingTable();
            }
            return null;
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
            return updateTag(r.id, r.alias, r.addr, r.type, r.format, r.unit);
        }
        public static int updateTag(long id, string alias, int addr, string type, string format, string unit)
        {
            return updateTag(id, alias, addr, (DataType)System.Enum.Parse(typeof(DataType), type), format, unit);
        }
        public static int updateTag(long id, string alias, int addr, DataType data_type, string format, string unit)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE Tag SET alias=@alias, addr=@addr, data_type=@data_type, format=@format, unit=@unit"
                + " WHERE id=@id"))
            {
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                cmd.Parameters.Add("@addr", DbType.Int32).Value = addr;
                cmd.Parameters.Add("@data_type", DbType.String).Value = data_type.ToString();
                cmd.Parameters.Add("@format", DbType.String).Value = format;
                cmd.Parameters.Add("@unit", DbType.String).Value = unit;
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }

        public static int updateScaling(Scaling s, long tag_id)
        {
            return updateScaling(s._scale_type.ToString(), s._raw_hi, s._raw_lo, s._scale_hi, s._scale_lo, tag_id);
        }
        public static int updateScaling(string scale_type, double raw_hi, double raw_lo, double scale_hi, double scale_lo, long tag_id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE Scaling SET scale_type=@scale_type, raw_hi=@raw_hi, raw_lo=@raw_lo,"
                + " scale_hi=@scale_hi, scale_lo=@scale_lo WHERE tag_id=@tag_id"))
            {
                cmd.Parameters.Add("@scale_type", DbType.String).Value = scale_type.ToString();
                cmd.Parameters.Add("@raw_hi", DbType.Double).Value = raw_hi;
                cmd.Parameters.Add("@raw_lo", DbType.Double).Value = raw_lo;
                cmd.Parameters.Add("@scale_hi", DbType.Double).Value = scale_hi;
                cmd.Parameters.Add("@scale_lo", DbType.Double).Value = scale_lo;
                cmd.Parameters.Add("@tag_id", DbType.Int64).Value = tag_id;
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
            deleteScaling(id);
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM Tag WHERE id=@id"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }
        private static int deleteTags(long plc_id)
        {
            foreach (Tag tag in getTagList(plc_id))
            {
                deleteScaling(tag.id);
            }
            // delete items belonging to the plc
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM Tag WHERE plc_id=@plcid"))
            {
                cmd.Parameters.Add("@plcid", DbType.Int64).Value = plc_id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }
        public static int deleteScaling(long tag_id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(
                "delete FROM Scaling WHERE tag_id=@tag_id"))
            {
                cmd.Parameters.Add("@tag_id", DbType.Int64).Value = tag_id;
                return SQLiteDBMS.execUpdate(cmd);
            }
        }
        #endregion

        #region -- input method --

        public static int inputPLC(PLC p, long project_id)
        {
            if (updatePLC(p) < 1)
            {
                return insertPLC(p, project_id);
            }
            return -1;
        }

        public static int inputTag(Tag tag)
        {
            if (tag.id < 0 || updateTag(tag) < 1)
            {
                return insertTag(tag);
            }
            return -1;
        }

        public static int inputScaling(Scaling s, long tag_id)
        {
            if (updateScaling(s, tag_id) < 1)
            {
                return insertScaling(s, tag_id);
            }
            return -1;
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
