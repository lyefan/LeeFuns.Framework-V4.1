using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml;

namespace LeeFuns.Utilities
{
    public class XMLHelper
    {
        public static bool AppendChild(string filePath, string xPath, XmlNode xmlNode)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);
                XmlNode node = document.SelectSingleNode(xPath);
                XmlNode newChild = document.ImportNode(xmlNode, true);
                node.AppendChild(newChild);
                document.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AppendChild(string filePath, string xPath, string toFilePath, string toXPath)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(toFilePath);
                XmlNode node = document.SelectSingleNode(toXPath);
                XmlNodeList list = ReadNodes(filePath, xPath);
                if (list != null)
                {
                    foreach (XmlElement element in list)
                    {
                        XmlNode newChild = document.ImportNode(element, true);
                        node.AppendChild(newChild);
                    }
                    document.Save(toFilePath);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static XmlDocument LoadXmlDoc(string filePath)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);
                return document;
            }
            catch
            {
                return null;
            }
        }

        public static XmlNodeList ReadNodes(string filePath, string xPath)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);
                return document.SelectSingleNode(xPath).ChildNodes;
            }
            catch
            {
                return null;
            }
        }

        public static bool UpdateNodeInnerText(string filePath, string xPath, string value)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);
                XmlElement element = (XmlElement) document.SelectSingleNode(xPath);
                element.InnerText = value;
                document.Save(filePath);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static DataSet XMLToDataSet(string xmlData)
        {
            if (!string.IsNullOrEmpty(xmlData))
            {
                DataSet set = new DataSet();
                set.ReadXml(new StringReader(xmlData));
                return set;
            }
            return null;
        }

        public static DataTable XMLToDataTable(string xmlData)
        {
            if (!string.IsNullOrEmpty(xmlData))
            {
                DataSet set = new DataSet();
                set.ReadXml(new StringReader(xmlData));
                if (set.Tables.Count > 0)
                {
                    return set.Tables[0];
                }
            }
            return null;
        }

        public static Hashtable XMLToHashtable(string xmlData)
        {
            return DataHelper.DataTableToHashtable(XMLToDataTable(xmlData));
        }
    }
}

