using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class Settings
    {      
        
        public static string SessionUserID = "offerlinkadminuserid";         
        public static string GetNetworkName(string networkid)
        {
            string strnetwork = "";
            switch (networkid)
            {
                case "1":strnetwork = "Network1";
                    break;
                case "2": strnetwork = "Network2";
                    break;
                case "3": strnetwork = "Network3";
                    break;
                case "4": strnetwork = "Network4";
                    break;
                case "5": strnetwork = "Network5";
                    break;
                case "6": strnetwork = "Network6";
                    break;
                case "7": strnetwork = "Network7";
                    break;
                case "8": strnetwork = "Network8";
                    break;
                case "9": strnetwork = "Network9";
                    break;
            }
            return strnetwork;
        }


        public static string GetNetworkID(string NetworkName)
        {
            string strnetworkid = "";

            switch (NetworkName.ToLower())
            {
                case "network1": strnetworkid = "1";
                    break;
                case "network2": strnetworkid = "2";
                    break;
                case "network3": strnetworkid = "3";
                    break;
                case "network4": strnetworkid = "4";
                    break;
                case "network5": strnetworkid = "5";
                    break;
                case "network6": strnetworkid = "6";
                    break;
                case "network7": strnetworkid = "7";
                    break;
                case "network8": strnetworkid = "8";
                    break;
                case "network9": strnetworkid = "9";
                    break;
                case "network10": strnetworkid = "10";
                    break;
            }
            return strnetworkid;
        }


        public static string GetUserLevel(string levelid)
        {
            string userlevel = "";

            switch (levelid)
            {
                case "1": userlevel = "Mainadmin"; break;
                case "2": userlevel = "Super Editor"; break;
                case "3": userlevel = "Site Editor"; break;
                case "4": userlevel = "Blogger"; break;
                default: userlevel = ""; break;
            }
            return userlevel;
        }




    }
}
