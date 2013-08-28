using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MIRLE_GPLC.Setting
{
    public static class GPLCSetting
    {
        private static string settingFilePath = "settings.xml";
        private static XmlDocument doc = new XmlDocument();
        private static XmlWriterSettings WriterSetting = new XmlWriterSettings();

        static GPLCSetting()
        {
            WriterSetting.Indent = true;
            WriterSetting.IndentChars = "\t";

            // check existence of setting file
            if (File.Exists(settingFilePath))
            {
                load();
            }
            else
            {
                RestoreDefaultSetting();
            }
        }

        internal static void load()
        {
            doc.Load(settingFilePath);
        }

        internal static void save()
        {
            using (XmlWriter writer = XmlWriter.Create(settingFilePath, WriterSetting))
            {
                doc.Save(writer);
            }
        }

        internal static string settingRead(string nodePath)
        {
            return doc.SelectSingleNode(nodePath).InnerText;
        }

        internal static void settingWrite(string nodePath, string value)
        {
            doc.SelectSingleNode(nodePath).InnerText = value;
            save();
        }

        internal static void RestoreDefaultSetting()
        {
            doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            // polling rate setting
            XmlNode pollNode = doc.CreateElement("Polling");
            doc.AppendChild(pollNode);
            XmlNode pollRateNode = doc.CreateElement("Rate");
            pollRateNode.InnerText = "1000";
            pollNode.AppendChild(pollRateNode);

            // save setting
            save();
        }
    }
}
