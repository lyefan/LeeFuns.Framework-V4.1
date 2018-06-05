using System;
using System.Data;
using System.Data.OleDb;

namespace LeeFuns.Utilities
{
    public class ImportExcel
    {
        private static string ConnectionString(string fileName)
        {
            return string.Format(fileName.EndsWith(".xls") ? "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;" : "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\"", fileName);
        }

        public static DataTable ExcelToDataTable(string sheet, string filename)
        {
            DataTable table;
            OleDbConnection selectConnection = new OleDbConnection(ConnectionString(filename));
            try
            {
                string selectCommandText = " SELECT * FROM [Sheet1$]";
                selectConnection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommandText, selectConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                selectConnection.Close();
                table = dataSet.Tables[0];
            }
            catch (Exception)
            {
                selectConnection.Close();
                selectConnection.Dispose();
                throw;
            }
            return table;
        }
    }
}

