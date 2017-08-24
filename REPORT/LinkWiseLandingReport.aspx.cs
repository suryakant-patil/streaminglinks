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
    public partial class LinkWiseLandingReport : System.Web.UI.Page
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

                //upload pages on main site 
                if (IsPostBack)
                {
                    //string day1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    //string day2 = DateTime.Now.ToString("yyyy-MM-dd");
                    Session["day1"] = txtstartdate.Text;
                    Session["day2"] = txtenddate.Text;
                    
                    if (null != Request.QueryString["p"])
                    {
                        pagenumber = Convert.ToInt32(Request.QueryString["p"].ToString());
                    }
                    else
                    {
                        pagenumber = 1;
                    }
                    PromotinalLinkReportList(GetDate(txtstartdate.Text), GetDate(txtenddate.Text), ddllink.SelectedValue);

                   // PromotinalLinkReportList(GetDate(txtstartdate.Text), GetDate(txtenddate.Text), Request.QueryString["referrerid"]);
                }
                else
                {
                    //string day1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    //string day2 = DateTime.Now.ToString("yyyy-MM-dd");

                    txtstartdate.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                    txtenddate.Text = DateTime.Now.ToString("dd/MM/yyyy");                


                    FillOfferLink();
                    

                    if (Session["offerlink"] == null)
                    {
                        Session["offerlink"] = "0";
                    }
                    else
                    {
                        Session["offerlink"] = ddllink.SelectedValue;
                    }
                    PromotinalLinkReportList(GetDate(txtstartdate.Text), GetDate(txtenddate.Text), ddllink.SelectedValue);
                  
                    disp_labels();
                    if (Request.QueryString["clear"] != null)
                    {
                        txtstartdate.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                        txtenddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        Session["day1"] = txtstartdate.Text;
                        Session["day2"] = txtenddate.Text;
                        ddllink.SelectedValue = "0";
                        Session["offerlink"] = "0";

                    }
                    if (Request.QueryString["excel"] != null)
                    {
                        if (Request.QueryString["excel"] == "excel")
                        {
                            if (Session["day1"] == null && Session["day2"] == null)
                            {
                                Session["day1"] = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                                Session["day2"] = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                            using (ReportMgmt obj = new ReportMgmt(strconn))
                            {
                                DataTable dtexcel = obj.GetUserJourneyList(GetDate(Session["day1"].ToString()), GetDate(Session["day2"].ToString()),ddllink.SelectedValue);
                                ExcelMgmt objexcel = new ExcelMgmt();
                                string excelfilename = "userjourney_" + DateTime.Now.ToString("ddMMyyyyHHmmsss") + ".csv";
                                objexcel.WriteToCSV(dtexcel, excelfilename);
                            }
                        }
                    }
                    if (null != Request.QueryString["p"])
                    {
                        pagenumber = Convert.ToInt32(Request.QueryString["p"].ToString());
                    }
                    else
                    {
                        pagenumber = 1;
                    }
                    //if (Request.QueryString["referrerid"] != null)
                    //{
                    //    txtstartdate.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                    //    txtenddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //    PromotinalLinkReportList(GetDate(txtstartdate.Text), GetDate(txtenddate.Text), Request.QueryString["referrerid"]);
                    //}
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "Report/LinkWiseLandingReport.aspx.cs Page_Load", ex);
            }
        }

        #endregion


        #region:Page Methods:

        private void disp_labels()
        {           
            ltheader.Text = "User Journey By Exit Clicks";                    
        }

        private void LoadList(string siteid,string linkid)
        {           
            using (ReportMgmt obj = new ReportMgmt(strconn))
            {
                obj.StartDate = GetDate(txtstartdate.Text);
                obj.EndDate = GetDate(txtenddate.Text);
                ltlist.Text = obj.SP_GetReviewListLandingPage(siteid, linkid);
            }
        }

        private void PromotinalLinkReportList(string fromdate, string todate, string linkid)
        {
            string txt = "";
            int i = 0;
            try
            {
                DataTable dt = new DataTable();

                using (ReportMgmt obj = new ReportMgmt(strconn))
                {
                    dt = obj.GetUserJourneyList(fromdate, todate, linkid);
                    if (dt.Rows.Count > 0)
                    {                       
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;                            
                            txt += "<tr height='30' valign='top'>";
                            txt += "<td class='text' align= 'center' bgcolor='#FFFFFF' valign='middle' style='font-family:verdana;font-size:11px;text-align: center;'>" + i.ToString() + "</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + Convert.ToDateTime(dr["page_visited_time"]).ToString("dd/MM/yyyy HH:mm") + " </td>";                            
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' ><a class='link' target='_blank' href='" + dr["searchengine_ref_url"].ToString() + "'>" + dr["searchrobot_name"].ToString() + "</a></td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" +dr["searchkeyword"].ToString() +'-' +dr["visitor_country"].ToString() + "</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' ><a class='link' href='" + dr["page_name"].ToString() + "'>" + dr["page_name"].ToString() + " </a></td>";                            
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + Convert.ToDateTime(dr["hitdate"]).ToString("dd/MM/yyyy HH:mm") + " </td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' ><a class='link' target='_blank' href='" + dr["ReferrerUrl"].ToString() + "'>" + dr["ReferrerUrl"].ToString() + "</a></td>";
                           
                            txt += "</tr>";
                        }
                    }
                    else
                    {
                        txt = "<tr height='30' valign='top'><td class='error' align='center' bgcolor='#FFFFFF' valign='middle' style='padding-right:3px;' colspan='12'> No Records Found! </td></tr>";
                    }
                    ltlist.Text = txt;

                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "Report/LinkWiseLandingReport.aspx.cs PromotinalLinkReportList", ex);
            }
        }

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

        public int GetPages(int Totalcount)
        {
            int TotalPages = 0;
            TotalPages = Totalcount / pagesize;
            if (Totalcount % pagesize != 0)
            {
                TotalPages = TotalPages + 1;
            }
            return TotalPages;
        }


        public string getNavigationHTML(int pageNumber, int TotalCount)
        {
            int TotalPages = 0;
            int range = 9;
            int mid = 5;
            int start = 1;

            TotalPages = TotalCount / pagesize;

            if (TotalCount % pagesize != 0)
                TotalPages = TotalPages + 1;
            int end = (TotalPages > 9) ? 9 : TotalPages;
            string retVal = "";
            string url = BaseUrl + "OfferLink/List_PromotionalLinkReport.aspx?linkid=" + Request.QueryString["linkid"] + "&p=";
            if (pageNumber > (mid + 1) && TotalPages > range)
            {
                int remaining = TotalPages - pageNumber;

                if (remaining >= 3)
                {
                    // eqally distribute on both sides
                    start = pageNumber - mid;
                    end = pageNumber + mid;
                }
                else
                {
                    // find distribution
                    end = TotalPages;
                    start = TotalPages - (range - 1);
                }
            }
            string imgP = "";
            string imgN = "";
            if (pageNumber == 1)
            {
                imgP = "&nbsp;";
            }
            else
            {
                imgP = "&nbsp; <a href='" + url + (pageNumber - 1).ToString() + "' class='link'><<</a>&nbsp;";
            }
            if (pageNumber < TotalPages)
            {
                imgN = "&nbsp;<a href='" + url + (pageNumber + 1).ToString() + "' class='link'>>></a>";
            }
            else
            {
                imgN = "&nbsp;";
            }

            for (int i = start; i <= end; i++)
            {
                if (i != pageNumber)
                {
                    retVal += "&nbsp;<a href='" + url + i.ToString() + "' class='link'>" + i + "</a>&nbsp;";
                }
                else
                {
                    retVal += "&nbsp;<span class=headings>" + i + "</span>";
                }
                if (i < end)
                {
                    retVal += "&nbsp;&nbsp;";
                }

            }
            retVal = imgP + " " + retVal + " " + imgN;
            return retVal;
        }

        public void FillOfferLink()
        {
            try
            {
                using (ReportMgmt obj = new ReportMgmt(strconn))
                {
                    DataTable dt = new DataTable();
                    dt = obj.GetOfferlink();
                    if (dt.Rows.Count > 0)
                    {
                        ddllink.DataSource = dt;
                        ddllink.DataTextField = "LinkName";
                        ddllink.DataValueField = "LinkID";
                        ddllink.DataBind();
                    }
                    ddllink.Items.Insert(0, new ListItem("-select-", "0"));
                }
            }
            catch(Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "Report/LinkWiseLandingReport.aspx.cs FillOfferLink", ex);
            }
        }

        #endregion

    }
}