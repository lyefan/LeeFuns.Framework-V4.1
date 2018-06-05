using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace LeeFuns.Utilities
{
    public class DataHelper
    {
        public static string DataSetToXML(DataSet ds)
        {
            if (ds != null)
            {
                StringWriter writer = new StringWriter();
                ds.WriteXml(writer);
                return writer.ToString();
            }
            return string.Empty;
        }

        public static Hashtable DataTableToHashtable(DataTable dt)
        {
            Hashtable hashtable = new Hashtable();
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string columnName = dt.Columns[i].ColumnName;
                    hashtable[columnName] = row[columnName];
                }
            }
            return hashtable;
        }

        public static string DataTableToXML(DataTable dt)
        {
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                StringWriter writer = new StringWriter();
                dt.WriteXml(writer);
                return writer.ToString();
            }
            return string.Empty;
        }

        public static DataTable GetNewDataTable(DataTable dt, string condition)
        {
            if (!IsExistRows(dt))
            {
                if (condition.Trim() == "")
                {
                    return dt;
                }
                DataTable table = new DataTable();
                table = dt.Clone();
                DataRow[] rowArray = dt.Select(condition);
                for (int i = 0; i < rowArray.Length; i++)
                {
                    table.ImportRow(rowArray[i]);
                }
                return table;
            }
            return null;
        }

        public static List<T> IListToList<T>(IList list)
        {
            T[] array = new T[list.Count];
            list.CopyTo(array, 0);
            return new List<T>(array);
        }

        public static bool IsExistRows(DataTable dt)
        {
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                return false;
            }
            return true;
        }

        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            if ((entitys == null) || (entitys.Count < 1))
            {
                throw new Exception("需转换的集合为空");
            }
            Type type = entitys[0].GetType();
            PropertyInfo[] properties = type.GetProperties();
            DataTable table = new DataTable();
            int index = 0;
            while (index < properties.Length)
            {
                table.Columns.Add(properties[index].Name);
                index++;
            }
            foreach (object obj2 in entitys)
            {
                if (obj2.GetType() != type)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] values = new object[properties.Length];
                for (index = 0; index < properties.Length; index++)
                {
                    values[index] = properties[index].GetValue(obj2, null);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}

