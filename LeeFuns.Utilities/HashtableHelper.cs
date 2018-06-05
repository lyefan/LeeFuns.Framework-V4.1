using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LeeFuns.Utilities
{
    public class HashtableHelper
    {
        public static Hashtable GetModelToHashtable<T>(T model)
        {
            Hashtable hashtable = new Hashtable();
            PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo info in properties)
            {
                string name = info.Name;
                hashtable[name] = info.GetValue(model, null);
            }
            return hashtable;
        }

        public static object HackType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                NullableConverter converter = new NullableConverter(conversionType);
                conversionType = converter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        public static T HashtableToModel<T>(Hashtable ht)
        {
            T local = Activator.CreateInstance<T>();
            Type type = local.GetType();
            foreach (PropertyInfo info in type.GetProperties())
            {
                object obj2 = ht[info.Name];
                if (info.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                {
                    obj2 = CommonHelper.ToDateTime(obj2);
                }
                info.SetValue(local, HackType(obj2, info.PropertyType), null);
            }
            return local;
        }

        public static bool IsNullOrDBNull(object obj)
        {
            return ((obj is DBNull) || string.IsNullOrEmpty(obj.ToString()));
        }

        public static bool IsNullOrEmpty(object item)
        {
            if (!(((item == null) || !(item.ToString() != "null")) || string.IsNullOrEmpty(item.ToString())))
            {
                return false;
            }
            return true;
        }

        public static Hashtable JsonToHashtable(string strJson)
        {
            Hashtable hashtable = new Hashtable();
            if (strJson == null)
            {
                return hashtable;
            }
            Regex regex = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string str = regex.Match(strJson).Value;
            DataTable dt = new DataTable();
            MatchCollection matchs = new Regex("(?<={)[^}]+(?=})").Matches(strJson);
            for (int i = 0; i < matchs.Count; i++)
            {
                string[] strArray = matchs[i].Value.Split(new char[] { ',' });
                if (dt.Columns.Count == 0)
                {
                    dt = new DataTable {
                        TableName = str
                    };
                    foreach (string str3 in strArray)
                    {
                        DataColumn column = new DataColumn();
                        string[] strArray2 = str3.Split(new char[] { ':' });
                        column.ColumnName = strArray2[0].Replace("\"", "");
                        dt.Columns.Add(column);
                    }
                    dt.AcceptChanges();
                }
                DataRow row = dt.NewRow();
                for (int j = 0; j < strArray.Length; j++)
                {
                    row[j] = strArray[j].Split(new char[] { ':' })[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                dt.Rows.Add(row);
                dt.AcceptChanges();
            }
            return DataHelper.DataTableToHashtable(dt);
        }

        public static Hashtable List_Key_ValueToHashtable(string item)
        {
            Hashtable hashtable = new Hashtable();
            foreach (string str in item.Split(new char[] { '☺' }))
            {
                if (str.Length > 0)
                {
                    string[] strArray = str.Split(new char[] { '☻' });
                    hashtable[strArray[0]] = strArray[1];
                }
            }
            return hashtable;
        }

        public static Hashtable ParameterToHashtable(string str)
        {
            Hashtable hashtable = new Hashtable();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArray = str.Split(new char[] { '≌' });
                foreach (string str2 in strArray)
                {
                    if (str2.Length > 0)
                    {
                        string[] strArray2 = str2.Split(new char[] { '☻' });
                        hashtable[strArray2[0]] = strArray2[1];
                    }
                }
            }
            return hashtable;
        }
    }
}

