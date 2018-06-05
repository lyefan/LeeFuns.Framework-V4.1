using System;
using System.Data;
using System.Text;
using System.Web;

namespace LeeFuns.Utilities
{
    public class DeriveWord
    {
        public static string DataTableToHtmlTable(DataTable dataTable)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            foreach (DataRow row in dataTable.Rows)
            {
                builder.Append("<tr>");
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName.Equals(""))
                    {
                        builder.Append(string.Format("<td>{0}</td>", row[column].ToString()));
                    }
                    else
                    {
                        builder.Append(string.Format("<td>{0}</td>", row[column].ToString()));
                    }
                }
                builder.Append("</tr>");
            }
            builder.Append("</table");
            return builder.ToString();
        }

        public static bool DataTableToWord(HttpResponse response, string fileName, DataTable dtbl)
        {
            response.Clear();
            response.Buffer = true;
            response.Charset = "UTF-8";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".doc");
            response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            response.ContentType = "application/ms-word";
            response.Write(DataTableToHtmlTable(dtbl));
            response.End();
            return true;
        }
    }
}

