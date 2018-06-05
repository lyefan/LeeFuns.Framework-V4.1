using System;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace LeeFuns.Utilities
{
    public class NetHelper
    {
        public static void BindEndPoint(Socket socket, IPEndPoint endPoint)
        {
            if (!socket.IsBound)
            {
                socket.Bind(endPoint);
            }
        }

        public static void BindEndPoint(Socket socket, string ip, int port)
        {
            IPEndPoint localEP = CreateIPEndPoint(ip, port);
            if (!socket.IsBound)
            {
                socket.Bind(localEP);
            }
        }

        public static void Close(Socket socket)
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
            }
            catch (SocketException exception)
            {
                throw exception;
            }
            finally
            {
                socket.Close();
            }
        }

        public static bool Connect(Socket socket, string ip, int port)
        {
            bool flag;
            try
            {
                socket.Connect(ip, port);
                flag = socket.Poll(-1, SelectMode.SelectWrite);
            }
            catch (SocketException exception)
            {
                throw new Exception(exception.Message);
            }
            return flag;
        }

        public static IPEndPoint CreateIPEndPoint(string ip, int port)
        {
            return new IPEndPoint(StringToIPAddress(ip), port);
        }

        public static TcpListener CreateTcpListener()
        {
            return new TcpListener(new IPEndPoint(IPAddress.Any, 0));
        }

        public static TcpListener CreateTcpListener(string ip, int port)
        {
            return new TcpListener(new IPEndPoint(StringToIPAddress(ip), port));
        }

        public static Socket CreateTcpSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public static Socket CreateUdpSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public static string GetClientIP(Socket clientSocket)
        {
            IPEndPoint remoteEndPoint = (IPEndPoint) clientSocket.RemoteEndPoint;
            return remoteEndPoint.Address.ToString();
        }

        public static string GetIPAddress()
        {
            string userHostAddress = string.Empty;
            userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(userHostAddress) || (userHostAddress == "::1"))
            {
                return "127.0.0.1";
            }
            return userHostAddress;
        }

        public static IPEndPoint GetLocalPoint(Socket socket)
        {
            return (IPEndPoint) socket.LocalEndPoint;
        }

        public static IPEndPoint GetLocalPoint(TcpListener tcpListener)
        {
            return (IPEndPoint) tcpListener.LocalEndpoint;
        }

        public static string GetLocalPoint_IP(Socket socket)
        {
            IPEndPoint localEndPoint = (IPEndPoint) socket.LocalEndPoint;
            return localEndPoint.Address.ToString();
        }

        public static string GetLocalPoint_IP(TcpListener tcpListener)
        {
            IPEndPoint localEndpoint = (IPEndPoint) tcpListener.LocalEndpoint;
            return localEndpoint.Address.ToString();
        }

        public static int GetLocalPoint_Port(Socket socket)
        {
            IPEndPoint localEndPoint = (IPEndPoint) socket.LocalEndPoint;
            return localEndPoint.Port;
        }

        public static int GetLocalPoint_Port(TcpListener tcpListener)
        {
            IPEndPoint localEndpoint = (IPEndPoint) tcpListener.LocalEndpoint;
            return localEndpoint.Port;
        }

        public static string GetValidIP(string ip)
        {
            if (IsIP(ip))
            {
                return ip;
            }
            return "-1";
        }

        public static int GetValidPort(string port)
        {
            int num = -1;
            try
            {
                if (port == "")
                {
                    throw new Exception("端口号不能为空！");
                }
                if ((Convert.ToInt32(port) < 0) || (Convert.ToInt32(port) > 0xffff))
                {
                    throw new Exception("端口号范围无效！");
                }
                num = Convert.ToInt32(port);
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
            return num;
        }

        [DllImport("wininet")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        public static bool IsConnectedInternet()
        {
            int connectionDescription = 0;
            return InternetGetConnectedState(out connectionDescription, 0);
        }

        public static bool IsIP(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return true;
            }
            ip = ip.Trim();
            string pattern = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
            return Regex.IsMatch(ip, pattern);
        }

        public static string ReceiveMsg(Socket socket)
        {
            byte[] buffer = new byte[0x1388];
            int count = socket.Receive(buffer);
            byte[] dst = new byte[count];
            Buffer.BlockCopy(buffer, 0, dst, 0, count);
            return ConvertHelper.BytesToString(dst, Encoding.Default);
        }

        public static void ReceiveMsg(Socket socket, byte[] buffer)
        {
            socket.Receive(buffer);
        }

        public static bool SendEmail(string receiveEmail, string msgSubject, string msgBody, bool IsEnableSSL)
        {
            bool flag;
            MailMessage message = new MailMessage();
            message.To.Add(receiveEmail);
            message.Subject = msgSubject;
            message.Body = msgBody;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient {
                EnableSsl = IsEnableSSL
            };
            try
            {
                client.Send(message);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static void SendMsg(Socket socket, byte[] msg)
        {
            socket.Send(msg, msg.Length, SocketFlags.None);
        }

        public static void SendMsg(Socket socket, string msg)
        {
            byte[] buffer = ConvertHelper.StringToBytes(msg, Encoding.Default);
            socket.Send(buffer, buffer.Length, SocketFlags.None);
        }

        public static void StartListen(Socket socket, int port)
        {
            IPEndPoint endPoint = CreateIPEndPoint(LocalHostName, port);
            BindEndPoint(socket, endPoint);
            socket.Listen(100);
        }

        public static void StartListen(Socket socket, int port, int maxConnection)
        {
            IPEndPoint endPoint = CreateIPEndPoint(LocalHostName, port);
            BindEndPoint(socket, endPoint);
            socket.Listen(maxConnection);
        }

        public static void StartListen(Socket socket, string ip, int port, int maxConnection)
        {
            BindEndPoint(socket, ip, port);
            socket.Listen(maxConnection);
        }

        public static IPAddress StringToIPAddress(string ip)
        {
            return IPAddress.Parse(ip);
        }

        public static string LocalHostName
        {
            get
            {
                return Dns.GetHostName();
            }
        }
    }
}

