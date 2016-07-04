#region :: Namesspace ::
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using BLL;
#endregion

namespace offerlinkmanageradmin.Report
{
    public partial class List_ExitClickOfferLinkReport : System.Web.UI.Page
    {
        #region: Variables:
        public string BaseUrl = "";

        int pagenumber = 1;
        public string strconn = "";
        public int pagesize = 50;

        #endregion

        #region:Page Events:

        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                BaseUrl = ConfigurationManager.AppSettings["BaseURL"];
                strconn = ConfigurationManager.AppSettings["Iframaddsense"];
                try
                {
                    if (string.IsNullOrEmpty(BLL.LoginInfo.Userid))
                    {
                        Response.Redirect(BLL.Constants.OldAdminUrl + "login.aspx", false);
                    }
                }
                catch
                {
                    Response.Redirect(BLL.Constants.OldAdminUrl + "login.aspx", false);
                }
               
                if (IsPostBack)
                {
                    PromotionalLinkHourswise(GetDate(txtstartdate.Text));
                   
                }
                else
                {
                    txtstartdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    PromotionalLinkHourswise(GetDate(txtstartdate.Text));
                   
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/PromotionalLinkReportDetails.aspx.cs Page_Load", ex);
            }
        }

        #endregion


        private void PromotionalLinkHourswise(string startdate)
        {           

            try
            {
                using (PromotionalLinkReportMgmt obj=new PromotionalLinkReportMgmt(strconn))
                {
                    ltlist.Text = obj.GetExitClikHourswise(startdate);
                }
            }

            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/PromotionalLinkHourswise.aspx.cs Page_Load", ex);

            }

        }


        #region:Page Methods:     

      

        public string GetDate(string strdate)
        {
            string date = "";
            if (strdate.Trim().Length > 0)
            {
                string[] strarray;
                string[] arr;

                arr = strdate.Split(' ');
                //string time=arr[1];
                strarray = arr[0].Split('/');

                if (strarray.Length > 2)
                {
                    date = string.Format("{0}-{1}-{2} ", strarray[2], strarray[1], strarray[0]);
                }
            }
            else
            {
                date = string.Format("{0} ", DateTime.Now.ToString("yyyy-MM-dd"));
            }
            return date;

        }     


      

        #endregion

    }
}