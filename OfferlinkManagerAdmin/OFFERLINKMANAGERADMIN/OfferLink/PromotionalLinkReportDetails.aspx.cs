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

namespace offerlinkmanageradmin.OfferLink
{
    public partial class PromotionalLinkReportDetails : System.Web.UI.Page
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
                    PromotinalLinkReportList(GetDate(txtstartdate.Text),GetDate(txtenddate.Text), Request.QueryString["referrerid"]);
                }
                else
                {
                    //string day1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    //string day2 = DateTime.Now.ToString("yyyy-MM-dd");

                    
                    disp_labels();
                    if (Request.QueryString["clear"] != null)
                    {
                        txtstartdate.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                        txtenddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        Session["day1"] = txtstartdate.Text;
                        Session["day2"] = txtenddate.Text;
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
                            using (PromotionalLinkReportMgmt obj = new PromotionalLinkReportMgmt(strconn))
                            {
                                DataTable dtexcel = obj.GetPromotionalExitClickListDetails(GetDate(Session["day1"].ToString()), GetDate(Session["day2"].ToString()), Request.QueryString["referrerid"]);
                                ExcelMgmt objexcel = new ExcelMgmt();
                                string excelfilename = "promotionlinkdetails_" + DateTime.Now.ToString("ddMMyyyyHHmmsss") + ".csv";
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
                    if (Request.QueryString["referrerid"] != null)
                    {
                        txtstartdate.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                        txtenddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        PromotinalLinkReportList(GetDate(txtstartdate.Text), GetDate(txtenddate.Text), Request.QueryString["referrerid"]);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/PromotionalLinkReportDetails.aspx.cs Page_Load", ex);
            }
        }

        #endregion


        #region:Page Methods:

        private void disp_labels()
        {
            switch(Request.QueryString["referrerid"])
            {
                case "1":
                    ltheader.Text = "Twitter Promotional Link Report Details";
                break;
                case "2":
                ltheader.Text = "Facebook Promotional Link Report Details";
                break;
                case "3":
                ltheader.Text = "Sites Promotional Link Report Details";
                break;
             }
        }

        private void PromotinalLinkReportList(string fromdate, string todate,string referrerid)
        {
            string txt = "";
            int i = 0;
            try
            {
                DataTable dt = new DataTable();

                using (PromotionalLinkReportMgmt obj = new PromotionalLinkReportMgmt(strconn))
                {
                    dt = obj.GetPromotionalExitClickListDetails(fromdate, todate,referrerid);
                    if (dt.Rows.Count > 0)
                    {
                        //ltheader.Text = dt.Rows[0]["ReferrerName"].ToString() +" Details";
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;
                            //int todaysclick = obj.GetPromotionalReferrelurlCountDayWise(0, dr["ReferrerUrl"].ToString());
                            //int yesterdayclick = obj.GetPromotionalReferrelurlCountDayWise(1, dr["ReferrerUrl"].ToString());
                            //int totalclick = 0;
                            //totalclick = todaysclick + yesterdayclick;
                            txt += "<tr height='30' valign='top'>";
                            txt += "<td class='text' align= 'center' bgcolor='#FFFFFF' valign='middle' style='font-family:verdana;font-size:11px;text-align: center;'>" + i.ToString() + "</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + dr["LinkName"].ToString() + " </td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + dr["ReferrerUrl"].ToString() + "</td>";
                          //  txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + dr["UserIP"].ToString() + "</td>";
                            //txt += "<td class='text' align='left' style='padding-left:5px;text-align:center;' bgcolor='#FFFFFF' valign='middle' >" + todaysclick + "</td>";
                            //txt += "<td class='text' align='left' style='padding-left:5px;text-align:center;' bgcolor='#FFFFFF' valign='middle' >" + yesterdayclick + "</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + Convert.ToDateTime(dr["HitDate"]).ToString("dd/MM/yyyy") + "</td>";
                            txt += "<td class='text' align='center' style='padding-left:5px;text-align:center;' bgcolor='#FFFFFF' valign='middle' >" + dr["Exitclick"].ToString() + "</td>";
                            
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
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/PromotionalLinkReportDetails.aspx.cs PromotinalLinkReportList", ex);
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

        #endregion

    }
}