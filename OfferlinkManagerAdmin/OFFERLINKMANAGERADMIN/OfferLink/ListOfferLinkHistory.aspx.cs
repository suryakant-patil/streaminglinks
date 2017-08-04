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
    public partial class ListOfferLinkHistory : System.Web.UI.Page
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
                if (BLL.LoginInfo.Userid == null)
                {
                    Response.Redirect(BLL.Constants.OldAdminUrl + "login.aspx", false);
                }
                if (IsPostBack)
                {                   
                    if (null != Request.QueryString["p"])
                    {
                        pagenumber = Convert.ToInt32(Request.QueryString["p"].ToString());
                    }
                    else
                    {
                        pagenumber = 1;
                    }
                    OfferLinkHistoryList(pagesize, pagenumber,Request.QueryString["linkid"]);
                }
                else
                {        
                  
                    disp_labels();                   
                    if (null != Request.QueryString["p"])
                    {
                        pagenumber = Convert.ToInt32(Request.QueryString["p"].ToString());
                    }
                    else
                    {
                        pagenumber = 1;
                    }
                    OfferLinkHistoryList(pagesize, pagenumber, Request.QueryString["linkid"]);
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/ListOfferLinkHistory.aspx.cs Page_Load", ex);
            }
        }

        #endregion


        #region:Page Methods:

        private void disp_labels()
        {
            ltheader.Text = "Promotional Link History List";
        }

        /// <summary>
        ///  offer link history list
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="currentpageno"></param>
        /// <param name="linkid"></param>
        private void OfferLinkHistoryList(int pagesize, int currentpageno,string linkid)
        {
            string txt = "";            
            try
            {
                DataTable dt = new DataTable();

                using (OfferLinkMgmt obj=new OfferLinkMgmt(strconn))
                {
                    dt = obj.GetOfferLinkHistoryList(pagesize, currentpageno,linkid);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {                           
                            txt += "<tr height='30' valign='top'>";
                            txt += "<td align= 'center' bgcolor='#FFFFFF' valign='middle' style='font-family:verdana;font-size:11px;'>" + dr["rownumber"].ToString() + "</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + dr["LinkName"].ToString() + " </td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + dr["oldvalues"].ToString() + "</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + dr["newvalues"].ToString() + "</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + dr["action"].ToString() + "</td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' >" + dr["FullName"].ToString() + "</td>";
                            txt += "<td align= 'center' bgcolor='#FFFFFF' valign='middle' style='font-family:verdana;font-size:11px;' >" + DateTime.Parse(dr["actiondate"].ToString()).ToString("dd/MM/yyyy HH:mm") + "</td>";                                     
                            txt += "</tr>";
                        }
                    }
                    else
                    {
                        txt = "<tr height='30' valign='top'><td class='error' align='center' bgcolor='#FFFFFF' valign='middle' style='padding-right:3px;' colspan='12'> No Records Found! </td></tr>";
                    }
                    ltlist.Text = txt;
                    if (Convert.ToInt32(obj.TotalCount) > 0)
                    {
                        ltpaging.Text = "<span class='error'>Page " + pagenumber + " of " + GetPages(Convert.ToInt32(obj.TotalCount)) + "  </span> : " + getNavigationHTML(Convert.ToInt32(pagenumber), obj.TotalCount);                     
                    }
                    else
                    {
                        ltpaging.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/ListOfferLinkHistory.aspx.cs OfferLinkHistoryList", ex);
            }
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


        /// <summary>
        /// navigation on paging
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public string getNavigationHTML(int pageNumber, int TotalCount)
        {
            string retVal = "";
            try
            {
                int TotalPages = 0;
                int range = 9;
                int mid = 5;
                int start = 1;

                TotalPages = TotalCount / pagesize;

                if (TotalCount % pagesize != 0)
                    TotalPages = TotalPages + 1;
                int end = (TotalPages > 9) ? 9 : TotalPages;
               
                string url = BaseUrl + "OfferLink/ListOfferLinkHistory.aspx?linkid=" + Request.QueryString["linkid"] + "&p=";
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
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/ListOfferLinkHistory.aspx.cs getNavigationHTML", ex);
            }
            return retVal;
        }

        #endregion

    }
}
