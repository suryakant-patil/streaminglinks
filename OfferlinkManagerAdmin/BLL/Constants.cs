using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace BLL
{
    public class Constants
    {
        public static readonly string siteurl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
        public static readonly string adminpath = System.Configuration.ConfigurationSettings.AppSettings["AdminPath"];    
        public static readonly string linkurl = System.Configuration.ConfigurationSettings.AppSettings["linkUrl"];
        public static readonly string Csspath = System.Configuration.ConfigurationSettings.AppSettings["AdminURLCSS"];
        public static readonly string AdminURL = System.Configuration.ConfigurationSettings.AppSettings["AdminURL"];        
        public static readonly string recachexml = "recachexml.aspx";       
        public static readonly string Bitlyurl = System.Configuration.ConfigurationSettings.AppSettings["Bitlyurl"];
        public static readonly string OldAdminUrl = System.Configuration.ConfigurationSettings.AppSettings["OldAdminUrl"];
        public static readonly string Fastbeturl = System.Configuration.ConfigurationSettings.AppSettings["Fastbeturl"];


        public static String toTitle(object input)
        {
           
            string output = input.ToString().Trim().Replace("\"", @"");
            output = output.ToString().Trim().Replace("é", "e");
            output = output.ToString().Trim().Replace("â", "a");
            output = output.ToString().Trim().Replace("´", " ");
            output = output.ToString().Trim().Replace("®", " ");
            output = output.ToString().Trim().Replace("*", " ");
            output = output.ToString().Trim().Replace("–", "");
            output = output.ToString().Trim().Replace("’", "'");
            output = output.ToString().Trim().Replace("!", "-");
            output = output.ToString().Trim().Replace("‘", "'");
            output = output.ToString().Trim().Replace("“", "");
            output = output.ToString().Trim().Replace("”", "");
            output = output.ToString().Trim().Replace("‘", "'");
            output = output.ToString().Trim().Replace("¼", "1/4");
            output = output.ToString().Trim().Replace("€", "");
            output = output.ToString().Trim().Replace("¥", "");
            output = output.ToString().Trim().Replace("…", "...");
            output = output.ToString().Trim().Replace("'", @"");
            output = output.ToString().Trim().Replace(".", " ");
            output = output.ToString().Trim().Replace(":", " ");
            output = output.ToString().Trim().Replace(";", " ");
            output = output.ToString().Trim().Replace("?", " ");
            output = output.ToString().Trim().Replace("? ", " ");
            output = output.ToString().Trim().Replace(",", " ");
            output = output.ToString().Trim().Replace("|", " ");
            output = output.ToString().Trim().Replace("£", " ");
            output = output.ToString().Trim().Replace("$", " ");
            output = output.ToString().Trim().Replace("&", " ");
            output = output.ToString().Trim().Replace("<", " ");
            output = output.ToString().Trim().Replace(">", " ");
            output = output.ToString().Trim().Replace(", ", " ");
            output = output.ToString().Trim().Replace("/", " ");
            output = output.ToString().Trim().Replace("%", " ");
            output = output.ToString().Trim().Replace("#", " ");
            output = output.ToString().Trim().Replace(" ", "-");
            output = output.ToString().Trim().Replace("--", "-");
            output = output.ToString().Trim().Replace("+", "-");
            output = output.ToString().Trim().Replace("[", "-");
            output = output.ToString().Trim().Replace("]", "-");
            output = output.ToString().Trim().Replace("»", "-");
            output = output.ToString().Trim().Replace("@", "");
            output = output.ToString().Trim().Replace("--", "-");          
               
            return output;

        }


    }
}
