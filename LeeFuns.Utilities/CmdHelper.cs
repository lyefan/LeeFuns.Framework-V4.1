using System;
using System.Diagnostics;
using System.Web;

namespace LeeFuns.Utilities
{
    public class CmdHelper
    {
        public static string ExeCommand(string commandText)
        {
            return ExeCommand(new string[] { commandText });
        }

        public static string ExeCommand(string[] commandTexts)
        {
            Process process = new Process {
                StartInfo = { FileName = "cmd.exe", UseShellExecute = false, RedirectStandardInput = true, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true }
            };
            string message = null;
            try
            {
                process.Start();
                foreach (string str2 in commandTexts)
                {
                    process.StandardInput.WriteLine(str2);
                }
                process.StandardInput.WriteLine("exit");
                message = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return message;
        }

        public static void Rar(string s, string d)
        {
            ExeCommand(HttpContext.Current.Server.MapPath("~/rar.exe") + " a \"" + d + "\" \"" + s + "\" -ep1");
        }

        public static bool StartApp(string appName)
        {
            return StartApp(appName, ProcessWindowStyle.Hidden);
        }

        public static bool StartApp(string appName, ProcessWindowStyle style)
        {
            return StartApp(appName, null, style);
        }

        public static bool StartApp(string appName, string arguments)
        {
            return StartApp(appName, arguments, ProcessWindowStyle.Hidden);
        }

        public static bool StartApp(string appName, string arguments, ProcessWindowStyle style)
        {
            bool flag = false;
            Process process = new Process {
                StartInfo = { FileName = appName, WindowStyle = style, Arguments = arguments }
            };
            try
            {
                process.Start();
                process.WaitForExit();
                process.Close();
                flag = true;
            }
            catch
            {
            }
            return flag;
        }

        public static void UnRar(string s, string d)
        {
            ExeCommand(HttpContext.Current.Server.MapPath("~/rar.exe") + " x \"" + s + "\" \"" + d + "\" -o+");
        }
    }
}

