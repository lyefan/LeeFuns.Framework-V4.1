using System;
using System.IO;

namespace LeeFuns.Utilities
{
    public class SizeHelper
    {
        public static string CountSize(long Size)
        {
            double num2;
            string str = "";
            long num = 0L;
            num = Size;
            if (num < 1024.0)
            {
                return (num.ToString("F2") + " 字节");
            }
            if ((num >= 1024.0) && (num < 0x100000L))
            {
                num2 = ((double) num) / 1024.0;
                return (num2.ToString("F2") + " KB");
            }
            if ((num >= 0x100000L) && (num < 0x40000000L))
            {
                num2 = (((double) num) / 1024.0) / 1024.0;
                return (num2.ToString("F2") + " MB");
            }
            if (num >= 0x40000000L)
            {
                str = ((((((double) num) / 1024.0) / 1024.0) / 1024.0)).ToString("F2") + " GB";
            }
            return str;
        }

        public static string GetFileSize(FileInfo File)
        {
            long length = File.Length;
            if (length >= 0x40000000L)
            {
                if (((((length / 0x400L) * 0x400L) * 0x400L) * 0x400L) >= 0x400L)
                {
                    return string.Format("{0:############0.00} TB", (((((double) length) / 1024.0) * 1024.0) * 1024.0) * 1024.0);
                }
                return string.Format("{0:####0.00} GB", ((((double) length) / 1024.0) * 1024.0) * 1024.0);
            }
            if (length >= 0x100000L)
            {
                return string.Format("{0:####0.00} MB", (((double) length) / 1024.0) * 1024.0);
            }
            if (length >= 0x400L)
            {
                return string.Format("{0:####0.00} KB", ((double) length) / 1024.0);
            }
            return string.Format("{0:####0.00} Bytes", length);
        }

        public static string GetFileSize(string FilePath)
        {
            FileInfo info = new FileInfo(FilePath);
            long length = info.Length;
            if (length >= 0x40000000L)
            {
                if (((((length / 0x400L) * 0x400L) * 0x400L) * 0x400L) >= 0x400L)
                {
                    return string.Format("{0:########0.00} TB", (((((double) length) / 1024.0) * 1024.0) * 1024.0) * 1024.0);
                }
                return string.Format("{0:####0.00} GB", ((((double) length) / 1024.0) * 1024.0) * 1024.0);
            }
            if (length >= 0x100000L)
            {
                return string.Format("{0:####0.00} MB", (((double) length) / 1024.0) * 1024.0);
            }
            if (length >= 0x400L)
            {
                return string.Format("{0:####0.00} KB", ((double) length) / 1024.0);
            }
            return string.Format("{0:####0.00} Bytes", length);
        }
    }
}

