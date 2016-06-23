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
                    if (null != Request.QueryString["p"])
                    {
                        pagenumber = Convert.ToInt32(Request.QueryString["p"].ToString());
                    }
                    else
                    {
                        pagenumber = 1;
                    }
                    Session["region"] = ddlregion.SelectedValue;
                    OfferLinkList(pagesize, pagenumber, Session["txtsearch"].ToString(),Session["region"].ToString());
                }
                else
                {
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
                    OfferLinkList(pagesize, pagenumber, Session["txtsearch"].ToString(), Session["region"].ToString());
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

        private void OfferLinkList(int pagesize, int currentpageno, string search,string region)
        {
            string txt = "";
            string tmp = "";
            string str = "";
            string lid = "0";
            string urlhttp = "";
            try
            {
                DataTable dt = new DataTable();
                using (OfferLinkMgmt obj = new OfferLinkMgmt(strconn))
                {
                    dt = obj.GetOfferLinkList(pagesize, currentpageno, search,region);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            txt += "<tr height='30' valign='top'>";
                            txt += "<td class='text' align='right'   bgcolor='#FFFFFF' valign='middle' style='padding-right:5px;'>" + dr["rownumber"].ToString() + ".</td>";
                            str = dr["linkname"].ToString();
                            lid = dr["LinkID"].ToString();
                            txt += "<td class='text' width='250px'  align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' nowrap title='" + dr["LinkName"].ToString() + "'><div id='" + lid + "_divname'><a href='AddEditOfferLink.aspx?linkid=" + dr["LinkID"].ToString() + "' class='link'>" + str + "</a></div></br><span class='thread'>RandomId -" + dr["RandomUniqueId"].ToString() + "</span></td>";
                            
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' nowrap>" + dr["LinkID"].ToString() + "</td>";
                            //if (dr["linkreference"].ToString().Length > 30)
                            //{
                            //   // str = Util.StringHandlers.splitNewsString(dr["linkreference"].ToString(), 30);
                            //}
                            //else
                            //{
                            //    str = dr["linkreference"].ToString();
                            //}
                            str = dr["linkreference"].ToString();
                            
                            //txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' nowrap><div id='"+lid+"_div'><a href='" + dr["linkreference"].ToString()  + "' target='_blank' id='"+lid+"_a' class='text'> " +  str + " </a></div><br><input type='button' id='"+dr["LinkID"].ToString()+"' value='Edit' style='font-size :10px;' onclick='javascript:ModifyLink(this);'></td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle'><div id='" + lid + "_div'><a href='" + dr["linkreference"].ToString() + "' target='_blank' id='" + lid + "_a' class='text'> " + str + " </a></div></br>CM-Link : <a target='_blank' href='" + BLL.Constants.Bitlyurl + dr["RandomUniqueId"].ToString() + "'>" + BLL.Constants.Bitlyurl + dr["RandomUniqueId"].ToString() + "</a></td>";
                            if (dr["IsBetSlip"].ToString() == "Y")
                            {
                                urlhttp = "http://";
                            }
                            else
                            {
                                urlhttp = "";
                            }
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' nowrap><div id='" + lid + "_divbit'><a href='"+urlhttp+ dr["shortenurl"].ToString() + "' target='_blank' id='" + lid + "_divbita' class='text'> " + dr["shortenurl"].ToString() + " </a></div></td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle'><div id='" + lid + "_divbitylrel'>" + dr["bitlyrelation"].ToString() + "</div></td>";
                            txt += "<td class='linktd text' bgcolor='#FFFFFF' align= 'center'  valign='middle' style='font-family:verdana;font-size:11px;' ><span class='added' id='user_" + dr["LinkID"].ToString() + "'  data-id='" + dr["addedby"].ToString() + "," + dr["modifiedby"].ToString() + "'></span></br><span id='deluser_" + dr["LinkID"].ToString() + "' class='thread' data-id='" + dr["LinkID"].ToString() + "_" + dr["deletedby"].ToString() + "'></span></td>";
                            txt += "<td class='text' align='left' style='padding-left:5px;' bgcolor='#FFFFFF' valign='middle' nowrap>" + Convert.ToDateTime(dr["addedon"].ToString()).ToString("dd/MM/yyyy") + "</br>" + Convert.ToDateTime(dr["modifiedon"].ToString()).ToString("dd/MM/yyyy") + "</td>";
                           
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
                            txt += "<td class='text' style='text-align:center;' bgcolor='#FFFFFF' valign='middle'>" + tmp + "</td>";
                            txt += "<td class='text' align='center' bgcolor='#FFFFFF' valign='middle'><a title='click to view history' class='editlink' href='#' data-linkid='" + dr["LinkID"].ToString() + "'>View</a></td>";
                            txt += "<td class='text'  bgcolor='#FFFFFF' style='text-align:center;' align=center valign=top style='padding-top:5px;'><input type ='checkbox' onclick='disselect()' name = 'fcheck[]' value='" + dr["linkID"].ToString() + "'></td>";
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
