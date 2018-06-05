using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;

namespace LeeFuns.Utilities
{
    public class DeriveExcel
    {
        public static void DataTableToExcel(DataTable data, string[] DataColumn, string fileName)
        {
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Charset = "Utf-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
            builder.AppendLine("<tr style=\"background-color: #FFE88C;font-weight: bold; white-space: nowrap;\">");
            foreach (string str in DataColumn)
            {
                builder.AppendLine("<td>" + str + "</td>");
            }
            builder.AppendLine("</tr>");
            foreach (DataRow row in data.Rows)
            {
                builder.Append("<tr>");
                foreach (string str in DataColumn)
                {
                    builder.Append("<td>").Append(row[str]).Append("</td>");
                }
                builder.AppendLine("</tr>");
            }
            builder.AppendLine("</table>");
            HttpContext.Current.Response.Write(builder.ToString());
            HttpContext.Current.Response.End();
        }

        public static void HtmlToExcel(StringBuilder sbHtml, string fileName)
        {
            if (sbHtml.Length > 0)
            {
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                HttpContext.Current.Response.Charset = "Utf-8";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName + ".xls", Encoding.UTF8));
                HttpContext.Current.Response.Write(sbHtml.ToString());
                HttpContext.Current.Response.End();
            }
        }

        public static void JsonToExcel(string JsonColumn, string JsonData, string fileName)
        {
        }

        public static void ListToExcel<T>(IList list, string[] DataColumn, string fileName)
        {
            string[] strArray;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Charset = "Utf-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName + ".xls", Encoding.UTF8));
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            builder.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
            builder.AppendLine("<tr style=\"background-color: #FFE88C;font-weight: bold; white-space: nowrap;\">");
            foreach (string str in DataColumn)
            {
                strArray = str.Split(new char[] { ':' });
                builder.AppendLine("<td>" + strArray[0] + "</td>");
            }
            builder.AppendLine("</tr>");
            foreach (T local in list)
            {
                Hashtable modelToHashtable = HashtableHelper.GetModelToHashtable<T>(local);
                builder.Append("<tr>");
                foreach (string str in DataColumn)
                {
                    strArray = str.Split(new char[] { ':' });
                    builder.Append("<td>").Append(modelToHashtable[strArray[1]]).Append("</td>");
                }
                builder.AppendLine("</tr>");
            }
            builder.AppendLine("</table>");
            HttpContext.Current.Response.Write(builder.ToString());
            HttpContext.Current.Response.End();
        }
    }
}

