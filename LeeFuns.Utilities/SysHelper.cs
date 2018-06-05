using System;
using System.Diagnostics;
using System.Threading;
using System.Web;

namespace LeeFuns.Utilities
{
    public static class SysHelper
    {
        public static string GetMethodName(int level)
        {
            StackTrace trace = new StackTrace();
            return trace.GetFrame(level).GetMethod().Name;
        }

        public static string GetPath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath(virtualPath);
        }

        public static AppDomain CurrentAppDomain
        {
            get
            {
                return Thread.GetDomain();
            }
        }

        public static string NewLine
        {
            get
            {
                return Environment.NewLine;
            }
        }
    }
}

