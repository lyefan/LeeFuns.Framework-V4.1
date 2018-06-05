using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;

namespace LeeFuns.Utilities
{
    public class FileDownHelper
    {
        public static void DownLoad(string FileName)
        {
            string path = MapPathFile(FileName);
            long num = 0x32000L;
            byte[] buffer = new byte[num];
            long length = 0L;
            FileStream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                length = stream.Length;
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(Path.GetFileName(path)));
                HttpContext.Current.Response.AddHeader("Content-Length", length.ToString());
                while (length > 0L)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int count = stream.Read(buffer, 0, Convert.ToInt32(num));
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, count);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.Clear();
                        length -= count;
                    }
                    else
                    {
                        length = -1L;
                    }
                }
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.Write("Error:" + exception.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                HttpContext.Current.Response.Close();
            }
        }

        public static void DownLoadold(string FileName, string name)
        {
            string path = MapPathFile(FileName);
            if (File.Exists(path))
            {
                FileInfo info = new FileInfo(path);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(name, Encoding.UTF8));
                HttpContext.Current.Response.AppendHeader("Content-Length", info.Length.ToString());
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteFile(path);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        public static bool FileExists(string FileName)
        {
            return File.Exists(MapPathFile(FileName));
        }

        public static string FileNameExtension(string FileName)
        {
            return Path.GetExtension(MapPathFile(FileName));
        }

        public static string MapPathFile(string FileName)
        {
            return HttpContext.Current.Server.MapPath(FileName);
        }

        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                FileStream input = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader reader = new BinaryReader(input);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long length = input.Length;
                    long num2 = 0L;
                    int count = 0x2800;
                    int millisecondsTimeout = ((int) Math.Floor((double) (((long) (0x3e8 * count)) / _speed))) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 0xce;
                        num2 = Convert.ToInt64(_Request.Headers["Range"].Split(new char[] { '=', '-' })[1]);
                    }
                    _Response.AddHeader("Content-Length", (length - num2).ToString());
                    if (num2 != 0L)
                    {
                        _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", num2, length - 1L, length));
                    }
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, Encoding.UTF8));
                    reader.BaseStream.Seek(num2, SeekOrigin.Begin);
                    int num5 = ((int) Math.Floor((double) ((length - num2) / ((long) count)))) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(reader.ReadBytes(count));
                            Thread.Sleep(millisecondsTimeout);
                        }
                        else
                        {
                            i = num5;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    reader.Close();
                    input.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

