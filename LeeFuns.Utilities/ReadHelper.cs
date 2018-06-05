using System;
using System.IO;
using System.Text;

namespace LeeFuns.Utilities
{
    public class ReadHelper
    {
        public static string ReadFile(string FilePath)
        {
            string str = string.Empty;
            Encoding encoding = Encoding.GetEncoding("gb2312");
            StreamReader reader = new StreamReader(FilePath, encoding);
            try
            {
                str = reader.ReadToEnd();
                reader.Close();
            }
            catch
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
            return str;
        }

        public static string ReadTxtFile(string FilePath)
        {
            string str = "";
            using (FileStream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string str2 = string.Empty;
                    while (!reader.EndOfStream)
                    {
                        str2 = str2 + reader.ReadLine() + "\r\n";
                        str = str2;
                    }
                }
            }
            return str;
        }
    }
}

