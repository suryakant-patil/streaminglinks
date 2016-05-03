using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace offerlinkmanageradmin
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                string url = Request.UrlReferrer.ToString();               
                if (!string.IsNullOrEmpty(url))
                {
                    if (url.Contains(Constants.OldAdminUrl))
                    {
                        if (Request.QueryString["userid"] != null)
                        {
                            LoginInfo.SetSession(Request.QueryString["userid"]);
                            Response.Redirect(BLL.Constants.AdminURL + "OfferLink/ListOfferLinks.aspx",false);
                        }
                    }
                    else
                    {
                        Response.Redirect(Constants.OldAdminUrl+"login.aspx");
                    }
                }
            }
            catch(Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "index.aspx.cs", ex);
                Response.Redirect(Constants.OldAdminUrl + "login.aspx");

            }
            
           
        }
    }
}