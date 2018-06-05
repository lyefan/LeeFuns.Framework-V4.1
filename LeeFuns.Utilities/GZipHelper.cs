using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;

namespace LeeFuns.Utilities
{
    public class GZipHelper
    {
        public static string Compress(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            MemoryStream stream = new MemoryStream();
            using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress, true))
            {
                stream2.Write(bytes, 0, bytes.Length);
            }
            stream.Position = 0L;
            byte[] buffer = stream.ToArray();
            stream.Read(buffer, 0, buffer.Length);
            byte[] dst = new byte[buffer.Length + 4];
            Buffer.BlockCopy(buffer, 0, dst, 4, buffer.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(bytes.Length), 0, dst, 0, 4);
            return Convert.ToBase64String(dst);
        }

        public static string Uncompress(string compressedText)
        {
            byte[] buffer = Convert.FromBase64String(compressedText);
            MemoryStream stream = new MemoryStream();
            int num = BitConverter.ToInt32(buffer, 0);
            stream.Write(buffer, 4, buffer.Length - 4);
            byte[] buffer2 = new byte[num];
            stream.Position = 0L;
            new GZipStream(stream, CompressionMode.Decompress).Read(buffer2, 0, buffer2.Length);
            return Encoding.UTF8.GetString(buffer2);
        }

        private static void Zip(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
            {
                strFile = strFile + Path.DirectorySeparatorChar;
            }
            Crc32 crc = new Crc32();
            string[] fileSystemEntries = Directory.GetFileSystemEntries(strFile);
            foreach (string str in fileSystemEntries)
            {
                if (Directory.Exists(str))
                {
                    Zip(str, s, staticFile);
                }
                else
                {
                    FileStream stream = File.OpenRead(str);
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(str.Substring(staticFile.LastIndexOf(@"\") + 1)) {
                        DateTime = DateTime.Now,
                        Size = stream.Length
                    };
                    stream.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public static void ZipFile(string strFile, string strZip)
        {
            strFile = HttpContext.Current.Server.MapPath(strFile);
            strZip = HttpContext.Current.Server.MapPath(strZip);
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
            {
                strFile = strFile + Path.DirectorySeparatorChar;
            }
            ZipOutputStream s = new ZipOutputStream(File.Create(strZip));
            s.SetLevel(6);
            Zip(strFile, s, strFile);
            s.Finish();
            s.Close();
        }
    }
}

