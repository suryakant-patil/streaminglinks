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
    public partial class List_PromotionalLinkReport : System.Web.UI.Page
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
                    string day1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    string day2 = DateTime.Now.ToString("yyyy-MM-dd");
                    if (null != Request.QueryString["p"])
                    {
                        pagenumber = Convert.ToInt32(Request.QueryString["p"].ToString());
                    }
                    else
                    {
                        pagenumber = 1;
                    }
                    PromotinalLinkReportList(day1,day2);
                }
                else
                {
                    string day1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    string day2 = DateTime.Now.ToString("yyyy-MM-dd");

                    //txtstartdate.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                    //txtenddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    disp_labels();
                    //if (Request.QueryString["clear"] != null)
                    //{
                    //    txtstartdate.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                    //    txtenddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //}
                    if (Request.QueryString["excel"] != null)
                    {
                        if (Request.QueryString["excel"] == "excel")
                        {
                            using (PromotionalLinkReportMgmt obj = new PromotionalLinkReportMgmt(strconn))
                            {
                               
                                DataTable dtexcel = obj.GetPromotionalExitClickList(day1,day2);
                                ExcelMgmt objexcel = new ExcelMgmt();
                                string excelfilename = "promotionlink_" + DateTime.Now.ToString("ddMMyyyyHHmmsss") + ".csv";
                                objexcel.WriteToCSV(dtexcel,excelfilename);
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
                    PromotinalLinkReportList(day1,day2);
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/List_PromotionalLinkReport.aspx.cs Page_Load", ex);
            }
        }

        #endregion


        #region:Page Methods:

        private void disp_labels()
        {
            ltheader.Text = "Promotional Link Report";
        }

        private void PromotinalLinkReportList(string day1,string day2)
        {
            string txt = "";
            int i=0;            
            try
            {
                DataTable dt = new DataTable();

                using (PromotionalLinkReportMgmt obj=new PromotionalLinkReportMgmt(strconn))
                {
                    dt = obj.GetPromotionalExitClickList(day1,day2);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;
                           
                            txt += "<tr height='30' valign='top'>";
                            txt += "<td class='text' align= 'center' bgcolor='#FFFFFF' valign='middle' style='font-family:verdana;font-size:11px;'>"+i.ToString()+"</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' ><a class='link' href='PromotionalLinkReportDetails.aspx?referrerid=" + dr["Referrerid"].ToString() + "'>" + dr["ReferrerName"].ToString() + "</a> </td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;text-align: center;' bgcolor='#FFFFFF' valign='middle' >" + obj.GetPromotionalLinkCountDayWise(0, Convert.ToInt32(dr["Referrerid"])) + "</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;text-align: center;' bgcolor='#FFFFFF' valign='middle' >" + obj.GetPromotionalLinkCountDayWise(1, Convert.ToInt32(dr["Referrerid"])) + "</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;text-align: center;' bgcolor='#FFFFFF' valign='middle' >" + dr["Exitclicktotal"].ToString() + "</td>";                           
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
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/List_PromotionalLinkReport.aspx.cs PromotinalLinkReportList", ex);
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