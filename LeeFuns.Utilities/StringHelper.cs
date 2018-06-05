using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace LeeFuns.Utilities
{
    public class StringHelper
    {
        public static string ClipString(string inputString, int len)
        {
            bool flag = false;
            if ((len % 2) == 1)
            {
                flag = true;
                len--;
            }
            ASCIIEncoding encoding = new ASCIIEncoding();
            int num = 0;
            string str = "";
            byte[] bytes = encoding.GetBytes(inputString);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                try
                {
                    str = str + inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }
                if (num > len)
                {
                    break;
                }
            }
            byte[] buffer2 = Encoding.Default.GetBytes(inputString);
            if (flag && (buffer2.Length > len))
            {
                str = str + "…";
            }
            return str;
        }

        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        public static string DelLastLength(string str, int Length)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Substring(0, str.Length - Length);
            return str;
        }

        public static string GetArrayStr(List<int> list)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == (list.Count - 1))
                {
                    builder.Append(list[i].ToString());
                }
                else
                {
                    builder.Append(list[i]);
                    builder.Append(",");
                }
            }
            return builder.ToString();
        }

        public static string GetArrayStr(List<string> list, string speater)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == (list.Count - 1))
                {
                    builder.Append(list[i]);
                }
                else
                {
                    builder.Append(list[i]);
                    builder.Append(speater);
                }
            }
            return builder.ToString();
        }

        public static string GetArrayValueStr(Dictionary<int, int> list)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<int, int> pair in list)
            {
                builder.Append(pair.Value + ",");
            }
            if (list.Count > 0)
            {
                return DelLastComma(builder.ToString());
            }
            return "";
        }

        public static string GetCleanStyle(string StrList, string SplitString)
        {
            if (StrList == null)
            {
                return "";
            }
            return StrList.Replace(SplitString, "");
        }

        public static string GetMD5(string s)
        {
            s = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            return s.ToLower().Substring(8, 0x10);
        }

        public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
        {
            string str = "";
            if (StrList == null)
            {
                str = "";
                Error = "请输入需要划分格式的字符串";
                return str;
            }
            int length = StrList.Length;
            int num2 = GetCleanStyle(NewStyle, SplitString).Length;
            if (length != num2)
            {
                str = "";
                Error = "样式格式的长度与输入的字符长度不符，请重新输入";
                return str;
            }
            string str2 = "";
            for (int i = 0; i < NewStyle.Length; i++)
            {
                if (NewStyle.Substring(i, 1) == SplitString)
                {
                    str2 = str2 + "," + i;
                }
            }
            if (str2 != "")
            {
                str2 = str2.Substring(1);
            }
            string[] strArray = str2.Split(new char[] { ',' });
            foreach (string str3 in strArray)
            {
                StrList = StrList.Insert(int.Parse(str3), SplitString);
            }
            str = StrList;
            Error = "";
            return str;
        }

        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[] { ',' });
        }

        public static List<string> GetStrArray(string str, char speater, bool toLower)
        {
            List<string> list = new List<string>();
            string[] strArray = str.Split(new char[] { speater });
            foreach (string str2 in strArray)
            {
                if (!string.IsNullOrEmpty(str2) && (str2 != speater.ToString()))
                {
                    string item = str2;
                    if (toLower)
                    {
                        item = str2.ToLower();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<string> GetSubStringList(string o_str, char sepeater)
        {
            List<string> list = new List<string>();
            string[] strArray = o_str.Split(new char[] { sepeater });
            foreach (string str in strArray)
            {
                if (!(string.IsNullOrEmpty(str) || !(str != sepeater.ToString())))
                {
                    list.Add(str);
                }
            }
            return list;
        }

        public static string HtmlToTxt(string strHtml)
        {
            string[] strArray = new string[] { "<script[^>]*?>.*?</script>", "<(\\/\\s*)?!?((\\w+:)?\\w+)(\\w+(\\s*=?\\s*(([\"'])(\\\\[\"'tbnr]|[^\\7])*?\\7|\\w+)|.{0})|\\s)*?(\\/\\s*)?>", @"([\r\n])[\s]+", "&(quot|#34);", "&(amp|#38);", "&(lt|#60);", "&(gt|#62);", "&(nbsp|#160);", "&(iexcl|#161);", "&(cent|#162);", "&(pound|#163);", "&(copy|#169);", @"&#(\d+);", "-->", @"<!--.*\n" };
            string str = strArray[0];
            string input = strHtml;
            for (int i = 0; i < strArray.Length; i++)
            {
                input = new Regex(strArray[i], RegexOptions.IgnoreCase).Replace(input, string.Empty);
            }
            input.Replace("<", "");
            input.Replace(">", "");
            input.Replace("\r\n", "");
            return input;
        }

        public static bool IsNullOrEmpty(object data)
        {
            return ((data == null) || (((data.GetType() == typeof(string)) && string.IsNullOrEmpty(data.ToString().Trim())) || (data.GetType() == typeof(DBNull))));
        }

        public static bool IsNullOrEmpty<T>(T data)
        {
            return ((data == null) || (((data.GetType() == typeof(string)) && string.IsNullOrEmpty(data.ToString().Trim())) || (data.GetType() == typeof(DBNull))));
        }

        public static bool IsNumberId(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null)
            {
                return false;
            }
            Regex regex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return regex.IsMatch(_value);
        }

        public static string[] SplitMulti(string str, string splitstr)
        {
            string[] strArray = null;
            if ((str != null) && (str != ""))
            {
                strArray = new Regex(splitstr).Split(str);
            }
            return strArray;
        }

        public static string SqlSafeString(string String, bool IsDel)
        {
            if (IsDel)
            {
                String = String.Replace("'", "");
                String = String.Replace("\"", "");
                return String;
            }
            String = String.Replace("'", "&#39;");
            String = String.Replace("\"", "&#34;");
            return String;
        }

        public static int StrLength(string inputString)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            int num = 0;
            byte[] bytes = encoding.GetBytes(inputString);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
            }
            return num;
        }

        public static int StrToId(string _value)
        {
            if (IsNumberId(_value))
            {
                return int.Parse(_value);
            }
            return 0;
        }

        public static string ToDBC(string input)
        {
            char[] chArray = input.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == '　')
                {
                    chArray[i] = ' ';
                }
                else if ((chArray[i] > 0xff00) && (chArray[i] < 0xff5f))
                {
                    chArray[i] = (char) (chArray[i] - 0xfee0);
                }
            }
            return new string(chArray);
        }

        public static string ToSBC(string input)
        {
            char[] chArray = input.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == ' ')
                {
                    chArray[i] = '　';
                }
                else if (chArray[i] < '\x007f')
                {
                    chArray[i] = (char) (chArray[i] + 0xfee0);
                }
            }
            return new string(chArray);
        }
    }
}

