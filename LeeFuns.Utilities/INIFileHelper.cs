using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace LeeFuns.Utilities
{
    public class INIFileHelper
    {
        public static string path;

        public static void ClearAllSection()
        {
            IniWriteValue(null, null, null);
        }

        public static void ClearSection(string Section)
        {
            IniWriteValue(Section, null, null);
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, byte[] retVal, int size, string filePath);
        public static string IniReadValue(string Section, string Key)
        {
            HttpSessionState session = HttpContext.Current.Session;
            if (path == null)
            {
                path = session["path"].ToString();
            }
            StringBuilder retVal = new StringBuilder(0xff);
            int num = GetPrivateProfileString(Section, Key, "", retVal, 0xff, path);
            return retVal.ToString();
        }

        public static byte[] IniReadValues(string section, string key)
        {
            byte[] retVal = new byte[0xff];
            int num = GetPrivateProfileString(section, key, "", retVal, 0xff, path);
            return retVal;
        }

        public static void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}

