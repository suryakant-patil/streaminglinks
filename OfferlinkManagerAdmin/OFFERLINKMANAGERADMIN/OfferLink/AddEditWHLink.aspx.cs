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
    public partial class AddEditWHLink : System.Web.UI.Page
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
                    ltheader.Text = "Edit WH Link";
                    lttop.Text = "&nbsp;&nbsp;&nbsp; Edit WH Link";
                }
                else
                {
                    ltheader.Text = "Add WH Link";
                    lttop.Text = "&nbsp;&nbsp;&nbsp; Add WH Link";
                }

                validPage.Visible = false;
                dupli.Visible = false;
                Tr2.Visible = false;
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/AddEditWHLink.aspx.cs Page_Load", ex);

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
                            string whurl=string.Format("{0}&sel={1}&stake={2}&url={3}",txtWhUrl.Text.Trim(),txtSel.Text.Trim(),txtStake.Text.Trim(),txtUrl.Text.Trim());
                            objlink.LinkName = txtLinkName.Text.Trim();
                            objlink.LinkReference =whurl;
                            objlink.Region = rdoregion.SelectedValue;
                            objlink.IsBetSlip = "Y";
                            objlink.IsExpire = ddlexpire.SelectedValue;
                            objlink.ExpireDate = GetDate(txtexpiredate.Text);
                            objlink.FastBetName = txtfastbetname.Text.Trim();
                            objlink.Shortenurl = BLL.Constants.Fastbeturl + CommonLib.StringHandler.ToTitle(txtfastbetname.Text.Trim());
                            objlink.FastBetTotitle = CommonLib.StringHandler.ToTitle(txtfastbetname.Text.Trim());
                            objlink.Sel = txtSel.Text.Trim();
                            objlink.Stake = txtStake.Text.Trim();
                            objlink.Whurl = txtUrl.Text.Trim();
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
                            int fastbetcount = objlink.CheckDuplicateFastbetForWHUrl(objlink.Shortenurl, objlink.Linkid, objlink.Region);
                            int promolinkcount = objlink.CheckDuplicateWHLinkName(objlink.LinkName, objlink.Linkid, objlink.Region);
                            if (fastbetcount == 0 && promolinkcount == 0)
                            {
                                _linkid = objlink.WHLink_Save();
                                Session["region"] = rdoregion.SelectedValue;

                                if (ViewState["oldvalue"] != null)
                                {
                                    objlink.OldValues = ViewState["oldvalue"].ToString();
                                    objlink.NewValues = whurl;
                                    objlink.SaveOfferLinkHistory();
                                }
                                Response.Redirect("ListOfferLinks.aspx", false);
                            }
                            else
                            {
                                dupli.Visible = true;
                                if (fastbetcount > 0)
                                {
                                    ltdupsub.Text = "This FastBet Name already exists. Please choose another.";
                                }
                                if (promolinkcount > 0)
                                {
                                    ltdupsub.Text = "This link name already exists. Please choose another.";
                                }
                                objlink.SaveDuplicatePromoLink();

                            }
                        }
                    }
                    else
                    {
                        validPage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/AddEditWHLink.aspx.cs IsPostBack", ex);
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
                            dt = obj.GetWHLinkDetails(Request.QueryString["linkid"].ToString());
                            if (dt.Rows.Count > 0)
                            {
                                txtLinkName.Text = dt.Rows[0]["LinkName"].ToString();
                                txtSel.Text = dt.Rows[0]["sel"].ToString();
                                txtStake.Text = dt.Rows[0]["stake"].ToString();
                                txtUrl.Text = dt.Rows[0]["whurl"].ToString();
                                string whurl = string.Format("{0}&sel={1}&stake={2}&url={3}", txtWhUrl.Text.Trim(), txtSel.Text.Trim(), txtStake.Text.Trim(), txtUrl.Text.Trim());
                                ViewState["oldvalue"] =whurl;
                                rdoregion.SelectedValue = dt.Rows[0]["region"].ToString();
                                ddlexpire.SelectedValue = dt.Rows[0]["IsExpire"].ToString();
                                txtexpiredate.Text = Convert.ToDateTime(dt.Rows[0]["ExpireDate"]).ToString("dd/MM/yyyy HH:mm");
                                txtfastbetname.Text = dt.Rows[0]["FastBetName"].ToString();
                                if (dt.Rows[0]["IsBetSlip"].ToString() == "Y")
                                {
                                    txtfastbetname.ReadOnly = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/AddEditWHLink.aspx.cs IsNotPostBack", ex);
                }
            }
        }

        private string GenerateUniqueID()
        {
            string uniqueId = "";
            Guid randomId = Guid.NewGuid();
            return uniqueId = randomId.ToString().ToUpper();
        }



        /// <summary>
        ///  get formated date
        /// </summary>
        /// <param name="strdate"></param>
        /// <returns></returns>
        public string GetDate(string strdate)
        {
            string date = "";
            try
            {
                if (strdate.Trim().Length > 0)
                {
                    string[] strarray;
                    string[] arr;

                    arr = strdate.Split(' ');
                    string time = arr[1];
                    strarray = arr[0].Split('/');

                    if (strarray.Length > 2)
                    {
                        date = string.Format("{0}-{1}-{2} {3}", strarray[2], strarray[1], strarray[0], time);
                    }
                }
                else
                {
                    date = string.Format("{0} ", DateTime.Now.AddYears(5).ToString("yyyy-MM-dd"));
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/AddEditWHLink.aspx.cs GetDate", ex);
            }
            return date;

        }

    }
}