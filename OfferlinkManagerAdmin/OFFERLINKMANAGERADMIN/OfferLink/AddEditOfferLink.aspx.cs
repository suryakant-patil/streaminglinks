using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
#region :: Namesspace ::
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.Configuration;
#endregion

namespace offerlinkmanageradmin.OfferLink
{
    public partial class AddEditOfferLink : System.Web.UI.Page
    {
        /// <summary>
        /// Summary description for AddEditOfferLink.
        /// </summary>

        #region :: public variables ::
        public string BaseUrl = ""; 
        #endregion

        #region :: variables ::
        string adsenseconn = "";
        #endregion

       

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            try
            {              
                
                BaseUrl = ConfigurationManager.AppSettings["BaseURL"];
                adsenseconn = ConfigurationManager.AppSettings["Iframaddsense"];               

                try
                {
                    if (BLL.LoginInfo.Userid == null)
                    {
                        Response.Redirect(BLL.Constants.OldAdminUrl + "login.aspx", false);
                    }
                }
                catch
                {
                    Response.Redirect(BLL.Constants.OldAdminUrl + "login.aspx", false);
                }

                if (Request.QueryString["id"] != null)
                {
                    ltheader.Text = "Edit Promotional Link";
                    lttop.Text = "&nbsp;&nbsp;&nbsp; Edit Promotional Link";                  
                }
                else
                {
                    ltheader.Text = "Add Promotional Link";
                    lttop.Text = "&nbsp;&nbsp;&nbsp; Add Promotional Link";
                }
               
                validPage.Visible = false;
                dupli.Visible = false;
                Tr2.Visible = false;
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/AddEditOfferLink.aspx.cs Page_Load", ex);

            }
            if (IsPostBack)
            {
                try
                {
                    Page.Validate();
                    if (IsValid)
                    {
                       
                        using (OfferLinkMgmt objlink = new OfferLinkMgmt(adsenseconn))
                        {
                            objlink.LinkName = txtLinkName.Text.Trim();
                            objlink.LinkReference = txtlink.Text.Trim();
                            objlink.CookieURl = txtcookieurl.Text.Trim();
                            objlink.Region = rdoregion.SelectedValue;
                            if (Request.QueryString["linkid"] != null)
                            {
                                objlink.Linkid = Request.QueryString["linkid"].ToString();
                                objlink.Action = "Update";
                            }
                            else
                            {
                                objlink.Linkid = "0";
                            }
                            int _linkid = 0;
                            objlink.RandomId = GenerateUniqueID();
                            _linkid=objlink.LinkOffer_Save();
                          
                            if (objlink.Linkid == "0")
                            {
                                BitlyShortenUrl objbitly = new BitlyShortenUrl();
                                objlink.Shortenurl = objbitly.ShortenUrl(Constants.Bitlyurl + objlink.RandomId);
                                objlink.AddShortenUrl(_linkid);
                            }

                            if (ViewState["oldvalue"] != null)
                            {
                                objlink.OldValues = ViewState["oldvalue"].ToString();
                                objlink.NewValues = txtlink.Text;
                                objlink.SaveOfferLinkHistory();
                            }                          
                            Response.Redirect("ListOfferLinks.aspx", false);
                        }
                    }
                    else
                    {
                        validPage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/AddEditOfferLink.aspx.cs IsPostBack", ex);
                }
            }
            else
            {
                try
                {
                    if (Request.QueryString["linkid"] != null)
                    {
                        using (OfferLinkMgmt obj = new OfferLinkMgmt(adsenseconn))
                        {
                            DataTable dt = new DataTable();
                            dt = obj.GetLinkOfferDetails(Request.QueryString["linkid"].ToString());
                            if (dt.Rows.Count > 0)
                            {
                                txtLinkName.Text = dt.Rows[0]["LinkName"].ToString();
                                txtlink.Text = dt.Rows[0]["LinkReference"].ToString();
                                txtcookieurl.Text = dt.Rows[0]["CookieURl"].ToString();
                                ViewState["oldvalue"] = txtlink.Text;
                                rdoregion.SelectedValue= dt.Rows[0]["region"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/AddEditOfferLink.aspx.cs IsNotPostBack", ex);
                }
            }
        }

        private string GenerateUniqueID()
        {
            string uniqueId = "";
            Guid randomId = Guid.NewGuid();
            return uniqueId = randomId.ToString().ToUpper();
        }

    }
}
