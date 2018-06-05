using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;

namespace LeeFuns.Utilities
{
    public static class DirFileHelper
    {
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }

        public static void ClearDirectory(string directoryPath)
        {
            directoryPath = HttpContext.Current.Server.MapPath(directoryPath);
            if (IsExistDirectory(directoryPath))
            {
                int num;
                string[] fileNames = GetFileNames(directoryPath);
                for (num = 0; num < fileNames.Length; num++)
                {
                    DeleteFile(fileNames[num]);
                }
                string[] directories = GetDirectories(directoryPath);
                for (num = 0; num < directories.Length; num++)
                {
                    DeleteDirectory(directories[num]);
                }
            }
        }

        public static void ClearFile(string filePath)
        {
            File.Delete(filePath);
            CreateFile(filePath);
        }

        public static bool Contains(string directoryPath, string searchPattern)
        {
            bool flag;
            try
            {
                if (GetFileNames(directoryPath, searchPattern, false).Length == 0)
                {
                    return false;
                }
                flag = true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return flag;
        }

        public static bool Contains(string directoryPath, string searchPattern, bool isSearchChild)
        {
            bool flag;
            try
            {
                if (GetFileNames(directoryPath, searchPattern, true).Length == 0)
                {
                    return false;
                }
                flag = true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return flag;
        }

        public static void Copy(string sourceFilePath, string destFilePath)
        {
            File.Copy(sourceFilePath, destFilePath, true);
        }

        public static void CopyFile(string dir1, string dir2)
        {
            dir1 = dir1.Replace("/", @"\");
            dir2 = dir2.Replace("/", @"\");
            if (File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir1))
            {
                File.Copy(HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir1, HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir2, true);
            }
        }

        public static void CopyFiles(string cDir, string TempId)
        {
        }

        public static void CopyFolder(string varFromDirectory, string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);
            if (Directory.Exists(varFromDirectory))
            {
                string[] directories = Directory.GetDirectories(varFromDirectory);
                if (directories.Length > 0)
                {
                    foreach (string str in directories)
                    {
                        CopyFolder(str, varToDirectory + str.Substring(str.LastIndexOf(@"\")));
                    }
                }
                string[] files = Directory.GetFiles(varFromDirectory);
                if (files.Length > 0)
                {
                    foreach (string str2 in files)
                    {
                        File.Copy(str2, varToDirectory + str2.Substring(str2.LastIndexOf(@"\")), true);
                    }
                }
            }
        }

        public static void CreateDir(string dir)
        {
            if ((dir.Length != 0) && !Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir))
            {
                Directory.CreateDirectory(HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir);
            }
        }

        public static void CreateDirectory(string directoryPath)
        {
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public static void CreateFile(string filePath)
        {
            try
            {
                if (!IsExistFile(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    info.Create().Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void CreateFile(string dir, string pagestr)
        {
            dir = dir.Replace("/", @"\");
            if (dir.IndexOf(@"\") > -1)
            {
                CreateDir(dir.Substring(0, dir.LastIndexOf(@"\")));
            }
            StreamWriter writer = new StreamWriter(HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir, false, Encoding.GetEncoding("GB2312"));
            writer.Write(pagestr);
            writer.Close();
        }

        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                if (!IsExistFile(filePath))
                {
                    FileStream stream = new FileInfo(filePath).Create();
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Close();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteDir(string dir)
        {
            if ((dir.Length != 0) && Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir))
            {
                Directory.Delete(HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir);
            }
        }

        public static void DeleteDirectory(string directoryPath)
        {
            directoryPath = HttpContext.Current.Server.MapPath(directoryPath);
            if (IsExistDirectory(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        public static void DeleteFile(string file)
        {
            if (File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + file))
            {
                File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + file);
            }
        }

        public static void DeleteFolderFiles(string varFromDirectory, string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);
            if (Directory.Exists(varFromDirectory))
            {
                string[] directories = Directory.GetDirectories(varFromDirectory);
                if (directories.Length > 0)
                {
                    foreach (string str in directories)
                    {
                        DeleteFolderFiles(str, varToDirectory + str.Substring(str.LastIndexOf(@"\")));
                    }
                }
                string[] files = Directory.GetFiles(varFromDirectory);
                if (files.Length > 0)
                {
                    foreach (string str2 in files)
                    {
                        File.Delete(varToDirectory + str2.Substring(str2.LastIndexOf(@"\")));
                    }
                }
            }
        }

        public static void ExistsFile(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
            }
        }

        public static string GetDateDir()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static string GetDateFile()
        {
            return DateTime.Now.ToString("HHmmssff");
        }

        public static string[] GetDirectories(string directoryPath)
        {
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(directoryPath);
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return directories;
        }

        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            string[] strArray;
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                strArray = Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return strArray;
        }

        public static string GetExtension(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return info.Extension;
        }

        public static string GetFileName(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return info.Name;
        }

        public static string GetFileNameNoExtension(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return info.Name.Split(new char[] { '.' })[0];
        }

        public static string[] GetFileNames(string directoryPath)
        {
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            return Directory.GetFiles(directoryPath);
        }

        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            string[] strArray;
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                strArray = Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
            }
            catch (IOException exception)
            {
                throw exception;
            }
            return strArray;
        }

        public static DataRow[] GetFilesByTime(string path, string Extension)
        {
            if (Directory.Exists(path))
            {
                string searchPattern = string.Format("*{0}", Extension);
                string[] files = Directory.GetFiles(path, searchPattern);
                if (files.Length > 0)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add(new DataColumn("filename", Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn("createtime", Type.GetType("System.DateTime")));
                    for (int i = 0; i < files.Length; i++)
                    {
                        DataRow row = table.NewRow();
                        DateTime creationTime = File.GetCreationTime(files[i]);
                        string fileName = Path.GetFileName(files[i]);
                        row["filename"] = fileName;
                        row["createtime"] = creationTime;
                        table.Rows.Add(row);
                    }
                    return table.Select(string.Empty, "createtime desc");
                }
            }
            return new DataRow[0];
        }

        public static int GetFileSize(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return (int) info.Length;
        }

        public static int GetLineCount(string filePath)
        {
            return File.ReadAllLines(filePath).Length;
        }

        public static bool IsEmptyDirectory(string directoryPath)
        {
            try
            {
                if (GetFileNames(directoryPath).Length > 0)
                {
                    return false;
                }
                if (GetDirectories(directoryPath).Length > 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        public static void Move(string sourceFilePath, string descDirectoryPath)
        {
            string fileName = GetFileName(sourceFilePath);
            if (IsExistDirectory(descDirectoryPath))
            {
                if (IsExistFile(descDirectoryPath + @"\" + fileName))
                {
                    DeleteFile(descDirectoryPath + @"\" + fileName);
                }
                File.Move(sourceFilePath, descDirectoryPath + @"\" + fileName);
            }
        }

        public static void MoveFile(string dir1, string dir2)
        {
            dir1 = dir1.Replace("/", @"\");
            dir2 = dir2.Replace("/", @"\");
            if (File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir1))
            {
                File.Move(HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir1, HttpContext.Current.Request.PhysicalApplicationPath + @"\" + dir2);
            }
        }

        public static void WriteText(string filePath, string text, Encoding encoding)
        {
            File.WriteAllText(filePath, text, encoding);
        }
    }
}

