using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Web;

namespace BLL
{
    public class ExcelMgmt
    {
        public void WriteToCSV(DataTable dt, string fileName)
        {
            string attachment = "attachment; filename=" + fileName + "";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            WriteColumnName(dt);
            foreach (DataRow objRow in dt.Rows)
            {
                WriteFileInfo(objRow);
            }
            HttpContext.Current.Response.End();
        }

        private static void WriteColumnName(DataTable dt)
        {
            string columnNames = "";
            foreach (DataColumn objCol in dt.Columns)
            {
                columnNames += objCol.ColumnName + ",";
            }
            //columnNames = columnNames.Remove(columnNames.Length - 1,1);
            HttpContext.Current.Response.Write(columnNames);
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        private void WriteFileInfo(DataRow drFile)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (object objVal in drFile.ItemArray)
            {
                AddComma(Convert.ToString(objVal), stringBuilder);
            }
            HttpContext.Current.Response.Write(stringBuilder.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        private void AddComma(string val, StringBuilder stringBuilder)
        {
            //val = val.Replace(",", " ");
            val = val.Replace("\n", " ");
            val = val.Replace("\r", " ");
            val = val.Replace("\t", " ");
            val = val.Trim();
            val = val.Replace("\"", "\"\"");

            if (val.IndexOf(",") > 0)
                val = "\"" + val + "\"";

            stringBuilder.Append(val);
            //stringBuilder.Append(CsvQuote(val));
            stringBuilder.Append(",");
        }


    }
}
