#region :: Namespace ::
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BLL;
using System.Data;
#endregion

namespace offerlinkmanageradmin.OfferLink
{
    public partial class ListOfferLinks : System.Web.UI.Page
    {

        #region: Variables:

        public string BaseUrl = "";            
        int pagenumber = 1;     
        string strconn = "";        
        int pagesize = 100;

        #endregion

        #region:Page Events:

        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
               
                BaseUrl = ConfigurationManager.AppSettings["BaseURL"];
                ltbaseurl.Text = BaseUrl.ToString();
                ltbaseurl1.Text = BaseUrl.ToString();
                ltbaseurl2.Text = BaseUrl.ToString();
                strconn = ConfigurationManager.AppSettings["Iframaddsense"];
                try
                {
                    if (string.IsNullOrEmpty(BLL.LoginInfo.Userid))
                    {
                        Response.Redirect(BLL.Constants.OldAdminUrl + "login.aspx",false);
                    }
                }
                catch
                {
                    Response.Redirect(BLL.Constants.OldAdminUrl + "login.aspx",false);
                }
                
                if (IsPostBack)
                {
                    Session["txtsearch"] = txtsearch.Text;
                    Session["datesort"] = ddlsort.SelectedValue;
                    if (null != Request.QueryString["p"])
                    {
                        pagenumber = Convert.ToInt32(Request.QueryString["p"].ToString());
                    }
                    else
                    {
                        pagenumber = 1;
                    }
                    Session["region"] = ddlregion.SelectedValue;
                    OfferLinkList(pagesize, pagenumber, Session["txtsearch"].ToString(), Session["region"].ToString(),Session["datesort"].ToString());
                }
                else
                {
                    if (Session["datesort"] == null)
                    {
                        ddlsort.SelectedValue = "0";
                        Session["datesort"] = "0";
                    }
                    
                    if (Session["txtsearch"] != null)
                    {
                        txtsearch.Text = Session["txtsearch"].ToString();
                    }
                    else
                    {
                        Session["txtsearch"] = "";
                    }
                    if (Session["region"] != null)
                    {
                        ddlregion.SelectedValue = Session["region"].ToString();
                    }
                    else
                    {
                        Session["region"] = "0";  
                    }
                    if (Request.QueryString["clear"] != null)
                    {
                        txtsearch.Text = "";
                        Session["txtsearch"] = "";
                        Session["region"] = "0";
                        ddlregion.SelectedValue = "0";
                    }                    
                    if (null != Request.QueryString["aprids"] && null != Request.QueryString["re"])
                    {
                        using (OfferLinkMgmt obj = new OfferLinkMgmt(strconn))
                        {
                            obj.ChangeOfferLinkStatus(Request.QueryString["aprids"].ToString(), Request.QueryString["re"].ToString());
                            if (Request.QueryString["re"] == "D")
                            {
                                obj.AddDeletedBy(Request.QueryString["aprids"]); 
                            }
                            
                            switch (Request.QueryString["re"])
                            {
                                case "Y":
                                    obj.Action = "Activate";
                                    break;
                                case "N":
                                    obj.Action = "Deactivate";
                                    break;
                                case "D":
                                    obj.Action = "Delete";
                                    break;
                                     
                            }
                            string strids = Request.QueryString["aprids"].ToString();
                            string[] strarray = strids.Split(',');
                            if (strarray.Length > 0)
                            {                                
                                for (int i = 0; i < strarray.Length; i++)
                                {
                                    obj.Linkid = strarray[i];
                                    obj.SaveOfferLinkHistory();
                                }                                
                              
                            }                            
                            Response.Redirect("ListOfferLinks.aspx");
                        }
                    }
                    disp_labels();
                    Session["txtsearch"] = txtsearch.Text;
                    if (null != Request.QueryString["p"])
                    {
                        pagenumber = Convert.ToInt32(Request.QueryString["p"].ToString());
                    }
                    else
                    {
                        pagenumber = 1;
                    }
                    OfferLinkList(pagesize, pagenumber, Session["txtsearch"].ToString(), Session["region"].ToString(), Session["datesort"].ToString());
                }
            }
            catch (Exception ex)
            {

                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/ListOfferLinks.aspx.cs Page_Load", ex);
            }
        }
        #endregion

        #region:Page Methods:

        private void disp_labels()
        {
            ltheader.Text = "Promotional Link List";

        }

        private void OfferLinkList(int pagesize, int currentpageno, string search,string region,string datesort)
        {
            string txt = "";
            string tmp = "";
            string str = "";
            string lid = "0";
            string urlhttp = "";
            string color = "";
            try
            {
                DataTable dt = new DataTable();
                using (OfferLinkMgmt obj = new OfferLinkMgmt(strconn))
                {
                    dt = obj.GetOfferLinkList(pagesize, currentpageno, search,region,datesort);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (Convert.ToDateTime(dr["ExpireDate"].ToString()) <= DateTime.Now)
                            {
                                color = "#e1e1e1";
                            }
                            else
                            {
                                color = "#FFFFFF";
                            }

                            txt += "<tr height='30' valign='top'>";
                            txt += "<td   bgcolor='" + color + "' style='text-align:center;font-family:verdana;font-size:11px;' align=center valign=top style='padding-top:5px;'><input type ='checkbox' onclick='disselect()' name = 'fcheck[]' value='" + dr["linkID"].ToString() + "'></td>";
                           // txt += "<td class='text' align='right'   bgcolor='#FFFFFF' valign='middle' style='padding-right:5px;'>" + dr["rownumber"].ToString() + ".</td>";
                            str = dr["linkname"].ToString();
                            lid = dr["LinkID"].ToString();
                            txt += "<td  width='250px'  align='left' style='padding-left:5px;color:#000000;font-family:verdana;font-size:11px;' bgcolor='" + color + "' valign='middle' nowrap title='" + dr["LinkName"].ToString() + "'><a  href='AddEditOfferLink.aspx?linkid=" + dr["LinkID"].ToString() + "' class='link'>" + str + "</a></td>";

                            txt += "<td  align='center' style='padding-left:5px;color:#000000;font-family:verdana;font-size:11px;' bgcolor='" + color + "' valign='middle' nowrap>" + dr["LinkID"].ToString() + "</td>";
                            //if (dr["linkreference"].ToString().Length > 30)
                            //{
                            //   // str = Util.StringHandlers.splitNewsString(dr["linkreference"].ToString(), 30);
                            //}
                            //else
                            //{
                            //    str = dr["linkreference"].ToString();
                            //}
                            str = dr["linkreference"].ToString();
                            
                            //txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle'><div id='" + lid + "_div'><a href='" + dr["linkreference"].ToString() + "' target='_blank' id='" + lid + "_a' class='text'> " + str + " </a></div></br>CM-Link : <a target='_blank' href='" + BLL.Constants.Bitlyurl + dr["RandomUniqueId"].ToString() + "'>" + BLL.Constants.Bitlyurl + dr["RandomUniqueId"].ToString() + "</a></td>";
                            txt += "<td  align='left' style='padding-left:5px;color:#000000;font-family:verdana;font-size:11px;' bgcolor='" + color + "' valign='middle'><a class='publink' href='" + dr["linkreference"].ToString() + "' target='_blank' id='" + lid + "_a' > " + str + " </a></td>";
                            if (dr["IsBetSlip"].ToString() == "Y")
                            {
                                urlhttp = "http://";
                            }
                            else
                            {
                                urlhttp = "";
                            }
                            txt += "<td  align='left' style='padding-left:5px;color:#000000;font-family:verdana;font-size:11px;' bgcolor='" + color + "' valign='middle' nowrap><a class='publink' href='" + urlhttp + dr["shortenurl"].ToString().Trim() + "' target='_blank' id='" + lid + "_divbita'> " + dr["shortenurl"].ToString() + " </a></td>";
                            txt += "<td  align='left' style='padding-left:5px;color:#000000;font-family:verdana;font-size:11px;' bgcolor='" + color + "' valign='middle'> "+ dr["bitlyrelation"].ToString() + "</td>";
                            txt += "<td  class='linktd' bgcolor='" + color + "' align= 'center'  valign='middle' style='font-family:verdana;font-size:11px;color:#000000;'><span class='added' id='user_" + dr["LinkID"].ToString() + "'  data-id='" + dr["addedby"].ToString() + "," + dr["modifiedby"].ToString() + "'></span></br><span id='deluser_" + dr["LinkID"].ToString() + "' class='thread' data-id='" + dr["LinkID"].ToString() + "_" + dr["deletedby"].ToString() + "'></span></td>";
                            txt += "<td  align='left' style='padding-left:5px;color:#000000;font-family:verdana;font-size:11px;' bgcolor='" + color + "' valign='middle' nowrap>" + Convert.ToDateTime(dr["addedon"].ToString()).ToString("dd/MM/yyyy") + "</br>" + Convert.ToDateTime(dr["modifiedon"].ToString()).ToString("dd/MM/yyyy") + "</td>";
                            if (dr["IsExpire"].ToString() == "Y")
                            {
                                txt += "<td  align='left' style='padding-left:5px;color:#000000;font-family:verdana;font-size:11px;' bgcolor='" + color + "' valign='middle' nowrap>" + Convert.ToDateTime(dr["ExpireDate"].ToString()).ToString("dd/MM/yyyy HH:mm") + "</td>";
                            }
                            else
                            {
                                txt += "<td  align='left' style='padding-left:5px;color:#000000;font-family:verdana;font-size:11px;' bgcolor='" + color + "' valign='middle' nowrap></td>";
                            }
                            if (dr["IsActive"].ToString() == "N")
                            {
                                tmp = "<img src='../images/icon_status_red.gif' border='0'>";
                            }
                            else
                            {
                                if (dr["IsActive"].ToString() == "D")
                                {
                                    tmp = "<img src='../images/icon_status_blue.gif' border='0'>";
                                }
                                else
                                {
                                    tmp = "<img src='../images/icon_status_green.gif' border='0'>";
                                }
                            }
                            txt += "<td  style='text-align:center;color:#000000;font-family:verdana;font-size:11px;' bgcolor='" + color + "' valign='middle'>" + tmp + "</td>";
                            txt += "<td  align='center' style='color:#000000;font-family:verdana;font-size:11px;' bgcolor='" + color + "' valign='middle'><a title='click to view history' class='editlink' href='#' data-linkid='" + dr["LinkID"].ToString() + "'>View</a></td>";
                            
                            txt += "</tr>";
                        }
                    }
                    else
                    {
                        txt = "<tr height='30' valign='top'><td class='error' align='center' bgcolor='#FFFFFF' valign='middle' style='padding-right:3px;' colspan='13'> No Records Found! </td></tr>";
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
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/ListOfferLinks.aspx.cs OfferLinkList", ex);
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
            string url = BaseUrl + "OfferLink/ListOfferLinks.aspx?p=";
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
