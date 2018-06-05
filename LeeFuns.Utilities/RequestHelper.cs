using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace LeeFuns.Utilities
{
    public class RequestHelper
    {
        public static string buildurl(string url, string param)
        {
            string str = url;
            if (url.IndexOf(param) <= 0)
            {
                return str;
            }
            if (url.IndexOf("&", (int) (url.IndexOf(param) + param.Length)) > 0)
            {
                return (url.Substring(0, url.IndexOf(param) - 1) + url.Substring(url.IndexOf("&", (int) (url.IndexOf(param) + param.Length)) + 1));
            }
            return url.Substring(0, url.IndexOf(param) - 1);
        }

        public static string GetCurrentFullHost()
        {
            HttpRequest request = HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }

        public static string GetDnsSafeHost()
        {
            return HttpContext.Current.Request.Url.DnsSafeHost;
        }

        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        public static string GetPageName()
        {
            string[] strArray = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].ToLower();
        }

        public static int GetParamCount()
        {
            return (HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count);
        }

        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        public static string GetServerString(string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public static string GetUrlReferrer()
        {
            string str = null;
            try
            {
                str = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch
            {
            }
            if (str == null)
            {
                return "";
            }
            return str;
        }

        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "*/*";
            request.Timeout = 0x3a98;
            request.AllowAutoRedirect = false;
            WebResponse response = null;
            string str = null;
            try
            {
                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    str = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                response = null;
            }
            return str;
        }

        public static string HttpPost(string url, string param)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 0x3a98;
            request.AllowAutoRedirect = false;
            StreamWriter writer = null;
            WebResponse response = null;
            string str = null;
            try
            {
                writer = new StreamWriter(request.GetRequestStream());
                writer.Write(param);
                writer.Close();
                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    str = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                writer = null;
                response = null;
            }
            return str;
        }

        public static bool IsBrowserGet()
        {
            string[] strArray = new string[] { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string str = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str.IndexOf(strArray[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        public static bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                string[] strArray = new string[] { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
                string str = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (str.IndexOf(strArray[i]) >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }

        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        public static string UrlExecute(string urlPath)
        {
            string str;
            if (string.IsNullOrEmpty(urlPath))
            {
                return "error";
            }
            StringWriter writer = new StringWriter();
            try
            {
                HttpContext.Current.Server.Execute(urlPath, writer);
                str = writer.ToString();
            }
            catch (Exception)
            {
                str = "error";
            }
            finally
            {
                writer.Close();
                writer.Dispose();
            }
            return str;
        }

        public static string GetScriptName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
        }

        public static string GetScriptNameQuery
        {
            get
            {
                return HttpContext.Current.Request.Url.Query;
            }
        }

        public static string GetScriptNameQueryString
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
            }
        }

        public static string GetScriptUrl
        {
            get
            {
                return ((GetScriptNameQueryString == "") ? GetScriptName : string.Format("{0}?{1}", GetScriptName, GetScriptNameQueryString));
            }
        }
    }
}

