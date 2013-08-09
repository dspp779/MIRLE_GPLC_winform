using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteDB;
using System.Data;

namespace MIRLE_GPLC.Model
{
    public class ModelUtil
    {
        public static List<ProjectData> getProjectList()
        {
            DataTable dt = SQLiteDBMS.execQuery("SELECT * FROM Project");
            List<ProjectData> pList = new List<ProjectData>();
            foreach (DataRow row in dt.Rows)
            {
                Int64 id = (Int64)row["ID"];
                string addr = (string)row["ADDR"];
                string name = (string)row["NAME"];
                double lat = (double)row["LAT"];
                double lng = (double)row["LNG"];
                ProjectData p = new ProjectData((int)id, addr, name, lat, lng, null);
                pList.Add(p);
            }
            return pList;
        }

        /*public static List<PLC> getPLCList(int project_id)
        {
            SQLiteDataReader reader = SQLiteDBMS.execQuery("SELECT * FROM PLC WHERE PROJECT_ID =" + project_id);
            List<PLC> pList = new List<PLC>();
            while (reader.Read())
            {
                int plc_id = reader.GetInt32(0);
                PLC p = new PLC(plc_id, reader.GetInt32(1),
                    reader.GetString(2), reader.GetInt32(3), getDataFields(plc_id));
                pList.Add(p);
            }
            return pList;
        }

        private static List<Record> getDataFields(int plc_id)
        {
            SQLiteDataReader reader = SQLiteDBMS.execQuery("SELECT * FROM DataField WHERE PLC_ID =" + plc_id);
            List<Record> fieldList = new List<Record>();
            while (reader.Read())
            {
                Record record = new Record(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),
                    reader.GetString(3), reader.GetString(4));
            }
            return fieldList;
        }*/
    }
}
