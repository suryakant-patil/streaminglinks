using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace offerlinkmanageradmin.Master
{
    public partial class AdminMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string conn = System.Configuration.ConfigurationSettings.AppSettings["Network1"];
                    using (BLL.OfferLinkMgmt objlink = new BLL.OfferLinkMgmt(conn))
                    {
                        DataTable dt = new DataTable();
                        dt=objlink.GetLoginUserDetails(BLL.LoginInfo.Userid);
                        if (dt.Rows.Count > 0)
                        {
                            string userl = "";
                            ltname.Text = dt.Rows[0]["FullName"].ToString();
                            int level = Convert.ToInt32(dt.Rows[0]["UserLevel"]);
                            switch (level)
                            {
                                case 1: userl = "Mainadmin"; break;
                                case 2: userl = "Super Editor"; break;
                                case 3: userl = "Site Editor"; break;
                                case 4: userl = "Blogger"; break;
                            }
                            ltname.Text += "(" + userl.ToString() + ")";
                        }
                        ltlogout.Text = "<a href='" + BLL.Constants.AdminURL + "logout.aspx' class='logout'>Logout</a>";
                        ltDateTime.Text = System.DateTime.Now.DayOfWeek + "  " + System.DateTime.Now.ToShortDateString() + "   " + System.DateTime.Now.ToShortTimeString();
                        ltdate.Text = "Server Time : " + System.DateTime.Now.ToString("HH:mm , dd MMM yyyy");

                    }
                }
                catch (Exception ex)
                {
                    CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "AdminMasterPage !Page.IsPostBack", ex);
                }
            }
        }
    }
}