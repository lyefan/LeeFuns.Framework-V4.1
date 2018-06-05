using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace LeeFuns.Utilities
{
    public static class HttpHelper
    {
        static HttpHelper()
        {
            ServicePointManager.DefaultConnectionLimit = 0x200;
            UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
            Timeout = 0x186a0;
        }

        public static byte[] DownloadData(string address)
        {
            using (MyWebClient client = new MyWebClient())
            {
                return client.DownloadData(address);
            }
        }

        public static long GetContentLength(string url)
        {
            long contentLength;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.UserAgent = UserAgent;
            request.Method = "HEAD";
            request.Timeout = 0x1388;
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                contentLength = response.ContentLength;
            }
            else
            {
                contentLength = -1L;
            }
            response.Close();
            return contentLength;
        }

        public static bool GetContentString(string url, out string message, Encoding encoding = null)
        {
            bool flag;
            try
            {
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }
                using (MyWebClient client = new MyWebClient())
                {
                    message = encoding.GetString(client.DownloadData(url));
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
                flag = false;
            }
            return flag;
        }

        public static string Post(string url, byte[] data, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            using (MyWebClient client = new MyWebClient())
            {
                client.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                byte[] bytes = client.UploadData(url, "POST", data);
                return encoding.GetString(bytes);
            }
        }

        public static int Timeout { get; set; }
        public static string UserAgent { get; set; }


        private class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                HttpWebRequest webRequest = base.GetWebRequest(address) as HttpWebRequest;
                if (webRequest == null)
                {
                    return null;
                }
                webRequest.Timeout = HttpHelper.Timeout;
                webRequest.UserAgent = HttpHelper.UserAgent;
                return webRequest;
            }
        }
    }
}

