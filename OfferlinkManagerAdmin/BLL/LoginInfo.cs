using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BLL
{
   public class LoginInfo
    {      

        public static void SetSession(string userid)
        {
            System.Web.HttpContext.Current.Session[Settings.SessionUserID] = userid;           
        }

        public static string Userid
        {
            get { return System.Web.HttpContext.Current.Session[Settings.SessionUserID].ToString(); }
        }      

        public static bool IsLogin()
        {
            
            bool flag = true;
            try
            {
                if (System.Web.HttpContext.Current.Session[Settings.SessionUserID] != null)
                     flag = true;
                else
                     flag = false;
            }
            catch
            {
                flag = false;
            }

            return flag;

        }
    }
}
