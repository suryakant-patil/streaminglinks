using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace offerlinkmanageradmin
{
    public partial class logout : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session.Abandon();
                Session.Clear();
                Response.Cookies.Clear();
                Response.Redirect(BLL.Constants.OldAdminUrl + "login.aspx");
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Client, "offerlinkmanageradmin logout.aspx.cs Page_Load", ex);
            }
        }
    }
}