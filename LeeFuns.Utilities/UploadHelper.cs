using System;
using System.IO;
using System.Web;

namespace LeeFuns.Utilities
{
    public class UploadHelper
    {
        public static string FileUpload(HttpPostedFileBase file, string path, string FileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string str = Path.GetExtension(file.FileName).ToLower();
            string str2 = SizeHelper.CountSize((long) file.ContentLength);
            try
            {
                int num = (file.ContentLength / 0x400) / 0x400;
                if (num > 10)
                {
                    return "-1";
                }
                file.SaveAs(path + FileName);
                return "succeed";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}

