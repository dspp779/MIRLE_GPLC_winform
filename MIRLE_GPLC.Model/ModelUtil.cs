using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using MIRLE.GPLC.DB.MySql;

namespace MIRLE_GPLC.Model
{
    public class ModelUtil
    {
        // execute update command, return number of modified records
        private static int executeUpdate(string sql)
        {
            using (MySqlCommand cmd = new MySqlCommand(sql))
            {
                return MySqlDbInterface.execUpdate(cmd);
            }
        }
        // get row ID of the last inserted record
        private static int getLastInsertId()
        {
            using (MySqlConnection conn = MySqlDbInterface.getConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    return (reader.Read()) ? reader.GetInt32(0) : -1;
                }
            }
        }

        #region -- table creation --

        /* Here's methods that create the schema for this application
         * You can find table definition in TABLE.txt under MIRLE.GPLC.Model.
         * */

        private static void createProjectTable()
        {
            string schema =
                "CREATE TABLE Project (id BIGINT PRIMARY KEY, name TEXT, addr TEXT, lat DOUBLE, lng DOUBLE);";
            executeUpdate(schema);
        }
        private static void createProjectDeviceTable()
        {
            string schema =
                "CREATE TABLE ProjectDevice ( project_id INTEGER, plc_name TEXT )";
            executeUpdate(schema);
        }
        /*
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
        */
        #endregion

        #region -- insert model --

        /* insert methods for model
         * using parameters instead of directly string utility
         * can avoid malicious operation such as SQL injection.
         * */

        public static int insertProject(ProjectData p)
        {
            return insertProject(p.id, p.name, p.addr, p.lat, p.lng);
        }
        public static int insertProject(long id, string name, string addr, double lat, double lng)
        {
            using (MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO Project values (@id, @name, @addr, @lat, @lng)"))
            {
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                cmd.Parameters.Add("@name", DbType.String).Value = name;
                cmd.Parameters.Add("@addr", DbType.String).Value = addr;
                cmd.Parameters.Add("@lat", DbType.Double).Value = lat;
                cmd.Parameters.Add("@lng", DbType.Double).Value = lng;
                return MySqlDbInterface.execInsert(cmd);
            }
        }
        /*
        public static int insertPLC(PLC plc, long project_id)
        {
            return insertPLC(plc.alias, plc.netid, plc.ip, plc.port, plc.polling_rate, project_id);
        }
        public static int insertPLC(string alias, int netid, string ip, int port, int polling_rate, long project_id)
        {
            using (MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO PLC (net_id, net_ip, net_port, alias, polling_rate, project_id) "
                + "values (@net_id, @net_ip, @net_port, @alias, @polling_rate, @project_id)"))
            {
                cmd.Parameters.Add("@net_id", DbType.Int32).Value = netid;
                cmd.Parameters.Add("@net_ip", DbType.String).Value = ip;
                cmd.Parameters.Add("@net_port", DbType.Int32).Value = port;
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                cmd.Parameters.Add("@polling_rate", DbType.Int32).Value = polling_rate;
                cmd.Parameters.Add("@project_id", DbType.Int64).Value = project_id;
                return MySqlDbInterface.execInsert(cmd);
            }
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
            using (MySqlCommand cmd = new MySqlCommand(
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
                return MySqlDbInterface.execInsert(cmd);
            }

        }

        public static int insertScaling(Scaling s, long tag_id)
        {
            return insertScaling(s._scale_type.ToString(), s._raw_hi, s._raw_lo, s._scale_hi, s._scale_lo, tag_id);
        }
        public static int insertScaling(string scale_type, double raw_hi, double raw_lo, double scale_hi, double scale_lo, long tag_id)
        {
            using (MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO Scaling (scale_type, raw_hi, raw_lo, scale_hi, scale_lo, tag_id) "
                + "values (@scale_type, @raw_hi, @raw_lo, @scale_hi, @scale_lo, @tag_id)"))
            {
                // scale
                cmd.Parameters.AddWithValue("@scale_type", scale_type.ToString());
                cmd.Parameters.AddWithValue("@raw_hi", raw_hi);
                cmd.Parameters.AddWithValue("@raw_lo", raw_lo);
                cmd.Parameters.AddWithValue("@scale_hi", scale_hi);
                cmd.Parameters.AddWithValue("@scale_lo", scale_lo);
                cmd.Parameters.AddWithValue("@tag_id", tag_id);
                return MySqlDbInterface.execInsert(cmd);
            }
        }
        */
        /* method for bulk insert (not tested yet)
         * using transaction to achieve this demand
         * a transaction is an atomic sql operation
         * operations in a transaction are zero-or-none.
         * */
        public static void insertTag(List<Tag> list, long plc_id)
        {
            using (MySqlConnection conn = MySqlDbInterface.getConnection())
            {
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            "INSERT INTO Tag (alias, addr, data_type, format, unit, plc_id) "
                            + "values (@alias, @addr, @data_type, @format, @unit, @plc_id)";
                        cmd.Parameters.Add("@alias", DbType.String);
                        cmd.Parameters.Add("@addr", DbType.Int32);
                        cmd.Parameters.Add("@data_type", DbType.String);
                        cmd.Parameters.Add("@format", DbType.String);
                        cmd.Parameters.Add("@unit", DbType.String);
                        foreach (Tag tag in list)
                        {
                            insertTag(cmd, tag, plc_id);
                        }
                    }
                    transaction.Commit();
                }
            }
        }
        private static int insertTag(MySqlCommand cmd, Tag tag, long id)
        {
            cmd.Parameters["@alias"].Value = tag.alias;
            cmd.Parameters["@addr"].Value = tag.addr;
            cmd.Parameters["@data_type"].Value = tag.type.ToString();
            cmd.Parameters["@format"].Value = tag.format;
            cmd.Parameters["@unit"].Value = tag.unit;
            return cmd.ExecuteNonQuery();
        }

        #endregion

        #region -- get model list --

        /* methods that get list of model
         * */

        public static List<ProjectData> getProjectList()
        {
            try
            {
                List<ProjectData> pList = new List<ProjectData>();
                using (MySqlConnection conn = MySqlDbInterface.getConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Project", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // get column values
                            pList.Add(
                                new ProjectData(reader.GetInt64(0), reader.GetString(1),
                                reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), null)
                            );
                        }
                    }
                }
                return pList;
            }
            catch (MySqlException ex)
            {
                // table not exist
                if (ex.Number == 1146)
                {
                    createProjectTable();
                    return new List<ProjectData>();
                }
                else throw ex;
            }
        }
        public static List<PLC> getPLCList(long project_id)
        {
            try
            {
                using (MySqlConnection conn = MySqlDbInterface.getConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "SELECT plc_name FROM ProjectDevice WHERE project_id=@project_id", conn);
                    cmd.Parameters.AddWithValue("@project_id", project_id);
                    List<PLC> pList = new List<PLC>();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PLC p = new PLC(project_id, reader.GetString(0), null);
                            pList.Add(p);
                        }
                    }
                    return pList;
                }
            }
            catch (MySqlException ex)
            {
                // table not exist
                if (ex.Number == 1146)
                {
                    createProjectDeviceTable();
                    return new List<PLC>();
                }
                else throw ex;
            }
        }
        public static List<PLC> getTagList(string name)
        {
            try
            {
                using (MySqlConnection conn = MySqlDbInterface.getConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "SELECT * FROM @devicename", conn);
                    cmd.Parameters.AddWithValue("@devicename", name);
                    List<Tag> pList = new List<Tag>();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PLC p = new PLC(project_id, reader.GetString(0), null);
                            pList.Add(p);
                        }
                    }
                    return pList;
                }
            }
            catch (MySqlException ex)
            {
                // table not exist
                if (ex.Number == 1146)
                {
                    createProjectDeviceTable();
                    return new List<PLC>();
                }
                else throw ex;
            }
        }
        #endregion

        #region -- update model --

        /* update methods for model
         * using parameters instead of directly string utility
         * can avoid malicious operation such as SQL injection.
         * */

        public static int updateProject(long id, string name, string addr, double lat, double lng, long oid)
        {
            using (MySqlCommand cmd = new MySqlCommand(
                "UPDATE Project SET id=@id, name=@name, addr=@addr, lat=@lat, lng=@lng WHERE id=@oid"))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@addr", addr);
                cmd.Parameters.AddWithValue("@lat", lat);
                cmd.Parameters.AddWithValue("@lng", lng);
                cmd.Parameters.AddWithValue("@oid", oid);
                return MySqlDbInterface.execUpdate(cmd);
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
            using (MySqlCommand cmd = new MySqlCommand(
                "UPDATE Tag SET alias=@alias, addr=@addr, data_type=@data_type, format=@format, unit=@unit"
                + " WHERE id=@id"))
            {
                cmd.Parameters.Add("@alias", DbType.String).Value = alias;
                cmd.Parameters.Add("@addr", DbType.Int32).Value = addr;
                cmd.Parameters.Add("@data_type", DbType.String).Value = data_type.ToString();
                cmd.Parameters.Add("@format", DbType.String).Value = format;
                cmd.Parameters.Add("@unit", DbType.String).Value = unit;
                cmd.Parameters.Add("@id", DbType.Int64).Value = id;
                return MySqlDbInterface.execUpdate(cmd);
            }
        }

        public static int updateScaling(Scaling s, long tag_id)
        {
            return updateScaling(s._scale_type.ToString(), s._raw_hi, s._raw_lo, s._scale_hi, s._scale_lo, tag_id);
        }
        public static int updateScaling(string scale_type, double raw_hi, double raw_lo, double scale_hi, double scale_lo, long tag_id)
        {
            using (MySqlCommand cmd = new MySqlCommand(
                "UPDATE Scaling SET scale_type=@scale_type, raw_hi=@raw_hi, raw_lo=@raw_lo,"
                + " scale_hi=@scale_hi, scale_lo=@scale_lo WHERE tag_id=@tag_id"))
            {
                cmd.Parameters.Add("@scale_type", DbType.String).Value = scale_type.ToString();
                cmd.Parameters.Add("@raw_hi", DbType.Double).Value = raw_hi;
                cmd.Parameters.Add("@raw_lo", DbType.Double).Value = raw_lo;
                cmd.Parameters.Add("@scale_hi", DbType.Double).Value = scale_hi;
                cmd.Parameters.Add("@scale_lo", DbType.Double).Value = scale_lo;
                cmd.Parameters.Add("@tag_id", DbType.Int64).Value = tag_id;
                return MySqlDbInterface.execUpdate(cmd);
            }
        }

        #endregion

        #region -- delete model --

        /* delete methods for model
         * using parameters instead of directly string utility
         * can avoid malicious operation such as SQL injection.
         * */

        public static int deleteProject(long id)
        {
            using (MySqlCommand cmd = new MySqlCommand(
                "delete FROM Project WHERE id=@id"))
            {
                cmd.Parameters.AddWithValue("@id", id);
                return MySqlDbInterface.execUpdate(cmd);
            }
        }

        #endregion

        #region -- input method --

        #endregion

        #region -- db file operation --

        // set db file path (only for SQLite
        public static void setPath(string path)
        {
            //MySqlDbInterface.setDBPath(path);
        }
        // copy current db file to specified path
        public static void copyTo(string path)
        {
            //MySqlDbInterface.copyTo(path);
        }
        
        #endregion
    }
}
