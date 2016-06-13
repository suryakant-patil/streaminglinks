using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BLL;
using System.Text;
using System.Data;

namespace offerlinkmanageradmin
{
    public partial class AjaxOfferLink : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                switch (Request.Form["type"].ToString().ToUpper())
                {
                    case "OFFERLINK":
                        UpdateOfferLink(Request.Form["linkid"], Request.Form["linkname"], Request.Form["linkurl"], Request.Form["cookieuri"], Request.Form["shortenurl"], Request.Form["oldvalue"], Request.Form["bitlyrel"]);
                        break;
                    case "FNGETUSER":
                        GetAddedByuser(Request.Form["userid"]);
                        break;
                    case "TRACK":
                        GetSiteWiseTrackingDetails(Request.Form["referrerid"]);
                        break;

                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "NewGamingnetAdmin/AjaxOfferLink.aspx.cs Page_Load", ex);

            }

        }

        public void UpdateOfferLink(string linkid, string linkname, string linkref, string cookieuri, string shortenurl,string oldvalue,string bitlyrel)
        {
            try
            {
                string strconn = ConfigurationManager.AppSettings["Iframaddsense"];
                using (OfferLinkMgmt objlink = new OfferLinkMgmt(strconn))
                {
                    objlink.EditOfferLink(linkid, linkname, linkref, cookieuri, shortenurl,bitlyrel);
                    objlink.NewValues = linkref;
                    objlink.OldValues = oldvalue;
                    objlink.Action = "Update";
                    objlink.Linkid = linkid;
                    objlink.SaveOfferLinkHistory();
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "NewGamingnetAdmin/AjaxOfferLink.aspx.cs UpdateOfferLink", ex);

            }

        }

        public void GetAddedByuser(string userid)
        {
            StringBuilder sb = new StringBuilder();
            string strconn = ConfigurationManager.AppSettings["Network1"];
            string data = "";
            try
            {
                using (OfferLinkMgmt obj=new OfferLinkMgmt (strconn))
                {
                    data = obj.Getusersname(userid);
                    ltresult.Text = data;
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "NewGamingnetAdmin/AjaxOfferLink.aspx.cs GetAssignedFreebetAddedByuser", ex);

            }
        }

        public void GetSiteWiseTrackingDetails(string referrerid)
        {
            try
            {
                string day1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                string day2 = DateTime.Now.ToString("yyyy-MM-dd");
                string strconn = ConfigurationManager.AppSettings["Iframaddsense"];
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                string data = "";
                using (PromotionalLinkReportMgmt obj = new PromotionalLinkReportMgmt(strconn))
                {
                    dt = obj.GetSiteWiseTrackingDetails(day1,day2);
                    if (dt.Rows.Count > 0)
                    {
                         data= "<table bgcolor='#eeeeee' style='width:100%;'>{0}</table>";
                        foreach (DataRow dr in dt.Rows)
                        {
                            int today = obj.GetPromotionalLinkCountDayWise_Site(0, 3, dr["SiteID"].ToString());
                            int yesterday = obj.GetPromotionalLinkCountDayWise_Site(1, 3, dr["SiteID"].ToString());
                            sb.Append(string.Format("<tr bgcolor='#eeeeee' height='20'><td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle'>{0}</td><td class='text' style='padding-left:5px;text-align:center;width: 200px;' bgcolor='#FFFFFF' valign='middle'>{1}</td><td class='text' style='padding-left:5px;text-align:center;width:203px;' bgcolor='#FFFFFF' valign='middle'>{2}</td><td class='text' style='padding-left:5px;text-align:center;width: 135px;' bgcolor='#FFFFFF' valign='middle'>{3}</td></tr>", dr["SiteAlias"].ToString(), today, yesterday, dr["Exitclick"].ToString()));  
                        }
                    }
                    ltresult.Text = string.Format(data, sb.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "NewGamingnetAdmin/AjaxOfferLink.aspx.cs GetTrackingDetails", ex);

            }
        }
      
    }
}
