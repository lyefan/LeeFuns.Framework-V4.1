using System;
using System.Text;

namespace LeeFuns.Utilities
{
    public sealed class ConvertHelper
    {
        public static int BytesToInt32(byte[] data)
        {
            if (data.Length < 4)
            {
                return 0;
            }
            int num = 0;
            if (data.Length >= 4)
            {
                byte[] dst = new byte[4];
                Buffer.BlockCopy(data, 0, dst, 0, 4);
                num = BitConverter.ToInt32(dst, 0);
            }
            return num;
        }

        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                string str = Convert.ToString(Convert.ToInt32(value, from), to);
                if (to == 2)
                {
                    switch (str.Length)
                    {
                        case 3:
                            str = "00000" + str;
                            break;

                        case 4:
                            str = "0000" + str;
                            break;

                        case 5:
                            str = "000" + str;
                            break;

                        case 6:
                            str = "00" + str;
                            break;

                        case 7:
                            str = "0" + str;
                            break;
                    }
                }
                return str;
            }
            catch
            {
                return "0";
            }
        }

        public static string RepairZero(string text, int limitedLength)
        {
            string str = "";
            for (int i = 0; i < (limitedLength - text.Length); i++)
            {
                str = str + "0";
            }
            return (str + text);
        }

        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
    }
}

