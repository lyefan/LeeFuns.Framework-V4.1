using System;
using System.Web;

namespace LeeFuns.Utilities
{
    public class SessionHelper
    {
        public static void Add(string strSessionName, object objValue)
        {
            HttpContext.Current.Session[strSessionName] = objValue;
        }

        public static object Get(string strSessionName)
        {
            return HttpContext.Current.Session[strSessionName];
        }

        public static void Remove(string strSessionName)
        {
            HttpContext.Current.Session.Remove(strSessionName);
        }

        public static void Set(string strSessionName, object objValue, int iExpires, int iYear)
        {
            HttpContext.Current.Session[strSessionName] = objValue;
            if (iExpires > 0)
            {
                HttpContext.Current.Session.Timeout = iExpires;
            }
            else if (iYear > 0)
            {
                HttpContext.Current.Session.Timeout = 0x80520 * iYear;
            }
        }
    }
}

