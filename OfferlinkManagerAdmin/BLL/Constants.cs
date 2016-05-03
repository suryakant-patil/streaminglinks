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

    }
}
