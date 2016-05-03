using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BLL;
using System.Text;

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
                        UpdateOfferLink(Request.Form["linkid"], Request.Form["linkname"], Request.Form["linkurl"], Request.Form["cookieuri"], Request.Form["shortenurl"], Request.Form["oldvalue"]);
                        break;
                    case "FNGETUSER":
                        GetAddedByuser(Request.Form["userid"]);
                        break;

                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "NewGamingnetAdmin/AjaxOfferLink.aspx.cs Page_Load", ex);

            }

        }

        public void UpdateOfferLink(string linkid, string linkname, string linkref, string cookieuri, string shortenurl,string oldvalue)
        {
            try
            {
                string strconn = ConfigurationManager.AppSettings["Iframaddsense"];
                using (OfferLinkMgmt objlink = new OfferLinkMgmt(strconn))
                {
                    objlink.EditOfferLink(linkid, linkname, linkref, cookieuri, shortenurl);
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
      
    }
}
