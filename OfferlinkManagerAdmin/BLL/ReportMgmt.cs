using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CommonLib;

namespace BLL
{
    public class ReportMgmt:IDisposable
    {
         DAL dal;
        #region :: constructor ::

        public ReportMgmt()
        {
            //
            // TODO: Add constructor logic here
            //
            dal = new DAL();
        }
        #endregion

        #region :: prameterized constructor ::
        public ReportMgmt(string dbconn)
        {
            dal = new DAL(dbconn);
        }

        #endregion

        #region ::Dispose ::
        public void Dispose()
        {
            dal.Dispose();
        }

        #endregion

        private int _totalaggrate = 0;
        private string _aggratevalue = "";
        public string dirpath = "";

        private string _startdate = string.Empty;
        private string _enddate = string.Empty;

        private string _optionvalue = string.Empty;

        #region :Public Property:
        public int TotalAggrate
        {
            get { return this._totalaggrate; }
            set { this._totalaggrate = value; }
        }
        public string AggrateValue
        {
            get { return this._aggratevalue; }
            set { this._aggratevalue = value; }
        }
        public string StartDate
        {
            get { return this._startdate; }
            set { this._startdate = value; }
        }
        public string EndDate
        {
            get { return this._enddate; }
            set { this._enddate = value; }
        }

        public string OptionValue
        {
            get { return this._optionvalue; }
            set { this._optionvalue = value; }
        }


        #endregion 

        #region :: Destructor ::
        ~ReportMgmt()
        {
            Dispose();
        }
        #endregion

        #region :: ::

        public string SP_GetSerachKewyordWithoutExit(string SiteId)
        {
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();

            string reviewid = string.Empty;

            string reviewhour = string.Empty;
            string reviewstring = string.Empty;

            string searchkeyword = string.Empty;
            DateTime searchdate = new DateTime();
            string enginename = string.Empty;
            string landingpage = string.Empty;
            string engineURL = string.Empty;

            string pageid = string.Empty;

            int cnt = 0;

            //_startdate = "2010-09-02 00:00:00";
            //_enddate = "2010-09-02 23:59:59";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            try
            {
                SqlParameter[] msgpara = { new SqlParameter("siteID", SqlDbType.Int), new SqlParameter("startdate", SqlDbType.VarChar), new SqlParameter("enddate", SqlDbType.VarChar) };
                msgpara[0].Value = SiteId;
                msgpara[1].Value = _startdate;
                msgpara[2].Value = _enddate;


              //  ds = ObjDal.GetDataSet(CommandType.StoredProcedure, "VS_SP_GetSearchkeyword", msgpara);
                ds = dal.GetDataSet(CommandType.StoredProcedure, "VS_SP_GetSearchkeyword", msgpara);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    sb.Append(@"<tr>
                                <td>
                                    <table width='100%' border='0' cellpadding='3' bgcolor='#EEEEEE'>
                                      <tr>
                                        <td bgcolor='#eeeeee'>Date</td>
                                        <td bgcolor='#eeeeee'>Engine</td>
                                        <td bgcolor='#eeeeee'>Keyword</td>
                                        <td bgcolor='#eeeeee'>Landing page</td>
                                      </tr>");

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        pageid = dr["page_id"].ToString();

                        cnt = 0;

                        string exitLink = "select count(*) as newcnt from PromotionalLinkStat where UserSessionId = '" + pageid + "'";
                       // ds2 = ObjDal.GetDataSet(exitLink);
                        ds2 = dal.GetDataSet(exitLink);
                        if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                            {
                                cnt = Convert.ToInt32(dr2["newcnt"].ToString());
                            }
                        }

                        if (cnt == 0)
                        {
                            engineURL = dr["searchengine_ref_url"].ToString();
                            searchkeyword = dr["searchkeyword"].ToString();
                            enginename = dr["searchrobot_name"].ToString();
                            searchdate = Convert.ToDateTime(dr["page_visited_time"].ToString());

                            sb.Append(@"<tr>
                                <td bgcolor='#ffffff'>" + searchdate.ToString("dd-MM-yyyy-HH:mm:ss") + @"</td>
                                <td bgcolor='#ffffff'><a class='linkhref' target='_blank' href='" + engineURL + "'>" + enginename + @"</a></td>
                                <td bgcolor='#ffffff'>" + searchkeyword + @"</td>
                                <td bgcolor='#ffffff'><a class='linkhref' target='_blank' href='" + landingpage + "'>" + landingpage + @"</a></td>                                
                              </tr>");
                        }
                    }

                    sb.Append(@"</table>
                        </td>
                    </tr>");
                }
            }
            catch (System.Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Client, "SP_GetSerachKewyordWithoutExit: ", ex);
            }
            finally
            {
                ds.Dispose();
            }
            return sb.ToString();
        }


        public string SP_GetReviewListLandingPage(string SiteId, string linkid)
        {
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds5 = new DataSet();

            string reviewid = string.Empty;

            string reviewhour = string.Empty;
            string reviewstring = string.Empty;

            string searchkeyword = string.Empty;
            DateTime searchdate = new DateTime();
            string enginename = string.Empty;
            string landingpage = string.Empty;
            string engineURL = string.Empty;
            string countryABB = string.Empty;

            string pageid = string.Empty;
            string linkurl = string.Empty;
            DateTime linkdate = new DateTime();

            int cnt = 0;

            //_startdate = "2010-09-02 00:00:00";
            //_enddate = "2010-09-02 23:59:59";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            try
            {
                SqlParameter[] msgpara = { new SqlParameter("siteID", SqlDbType.Int), new SqlParameter("startdate", SqlDbType.VarChar), new SqlParameter("enddate", SqlDbType.VarChar), new SqlParameter("linkid", SqlDbType.VarChar) };
                msgpara[0].Value = SiteId;
                msgpara[1].Value = _startdate;
                msgpara[2].Value = _enddate;
                msgpara[3].Value = linkid;

                ds = dal.GetDataSet(CommandType.StoredProcedure, "VS_SP_ReviewWiseLanding", msgpara);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        reviewid = dr["linkid"].ToString();

                        if (cnt == 0)
                        {
                            sb.Append(@"<tr>
                                <td align='left'><b>Link name</b>: " + dr["linkname"].ToString() + @"   Total count:(" + dr["counts"].ToString() + @")</td>
                            </tr>
                            <tr>");

                            SqlParameter[] msgpara1 = { new SqlParameter("siteID", SqlDbType.Int), new SqlParameter("startdate", SqlDbType.VarChar), new SqlParameter("enddate", SqlDbType.VarChar), new SqlParameter("linkid", SqlDbType.VarChar) };
                            msgpara1[0].Value = SiteId;
                            msgpara1[1].Value = _startdate;
                            msgpara1[2].Value = _enddate;
                            msgpara1[3].Value = reviewid;

                            int cntvalue = 0;
                            int day = 0;
                            ds2 =dal.GetDataSet(CommandType.StoredProcedure, "VS_SP_GetPageIDForHyperlink", msgpara1);
                            if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                            {
                                sb.Append(@"<td>
                                        <table width='100%' border='0' cellpadding='3' bgcolor='#EEEEEE'>
                                          <tr>
                                            <td bgcolor='#eeeeee'>Date</td>
                                            <td bgcolor='#eeeeee'>Engine</td>
                                            <td bgcolor='#eeeeee'>Keyword</td>
                                            <td bgcolor='#eeeeee'>Landing page</td>
                                            <td bgcolor='#eeeeee'>Exit Date</td>
                                            <td bgcolor='#eeeeee'>Exit page</td>
                                          </tr>");

                                linkurl = string.Empty;
                                pageid = string.Empty;

                                foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                {
                                    pageid = dr2["usersessionid"].ToString();
                                    linkurl = dr2["URLreferer"].ToString();
                                    linkdate = Convert.ToDateTime(dr2["hitdate"].ToString());

                                    SqlParameter[] msgpara5 = { new SqlParameter("pageid", SqlDbType.VarChar), new SqlParameter("startdate", SqlDbType.VarChar), new SqlParameter("enddate", SqlDbType.VarChar) };
                                    msgpara5[0].Value = pageid;
                                    msgpara5[1].Value = _startdate;
                                    msgpara5[2].Value = _enddate;

                                    ds5 = dal.GetDataSet(CommandType.StoredProcedure, "VS_SP_GetSearchKeywordForHyperlink", msgpara5);

                                    if (ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
                                    {
                                        engineURL = "";
                                        searchkeyword = "";
                                        enginename = "";
                                        landingpage = "";

                                        foreach (DataRow _dr5 in ds5.Tables[0].Rows)
                                        {
                                            try
                                            {
                                                engineURL = _dr5["searchengine_ref_url"].ToString();
                                                searchkeyword = _dr5["searchkeyword"].ToString();
                                                enginename = _dr5["searchrobot_name"].ToString();
                                                searchdate = Convert.ToDateTime(_dr5["page_visited_time"].ToString());
                                                landingpage = _dr5["landing_page"].ToString();
                                                countryABB = _dr5["visitor_country"].ToString();
                                                if (landingpage == "" || landingpage == "0")
                                                {
                                                    landingpage = _dr5["page_name"].ToString();
                                                }
                                            }
                                            catch { }

                                            sb.Append(@"<tr>
                                            <td bgcolor='#ffffff'>" + searchdate.ToString("dd-MM-yyyy HH:mm:ss") + @"</td>
                                            <td bgcolor='#ffffff'><a class='linkhref' target='_blank' href='" + engineURL + "'>" + enginename + @"</a></td>
                                            <td bgcolor='#ffffff'>" + searchkeyword + " - " + countryABB + @"</td>
                                            <td bgcolor='#ffffff'><a class='linkhref' target='_blank' href='" + landingpage + "'>" + landingpage + @"</a></td>
                                            <td bgcolor='#ffffff'>" + linkdate.ToString("dd-MM-yyyy HH:mm:ss") + @"</td>
                                            <td bgcolor='#ffffff'><a class='linkhref' target='_blank' href='" + linkurl + "'>" + linkurl + @"</a></td>
                                          </tr>");
                                        }
                                    }
                                }
                                sb.Append(@"</table>
                                </td>");
                            }
                            sb.Append("</tr>");
                        }

                        if (linkid == reviewid)
                        {
                            _optionvalue += "<option selceted value='" + reviewid + "'>" + dr["linkname"].ToString() + "(" + dr["counts"].ToString() + ")</option>";
                        }
                        else
                        {
                            _optionvalue += "<option value='" + reviewid + "'>" + dr["linkname"].ToString() + "(" + dr["counts"].ToString() + ")</option>";
                        }
                        cnt++;

                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Client, "SP_GetReviewListLandingPage: ", ex);
            }
            finally
            {
                ds.Dispose();
            }
            return sb.ToString();
        }



        public DataTable GetOfferlink()
        {
            DataTable dt = new DataTable();
            string strsql = "";
            try
            {                
                strsql = "select LinkID,LinkName from OfferLinkManager where IsActive='Y' order by LinkName ";
                dt=dal.GetDataTable(strsql);
            }
            catch (System.Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Client, "GetOfferlink: ", ex);
            }
            return dt;
        }


        public DataTable GetUserJourneyList(string fromdate, string todate, string linkid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = { new SqlParameter("@fromdate",SqlDbType.Date),
                                       new SqlParameter("@todate",SqlDbType.Date),
                                       new SqlParameter("@linkid",SqlDbType.Int)};
                param[0].Value = fromdate;
                param[1].Value = todate;
                param[2].Value = linkid;
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_UserJourneyReportList", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetUserJourneyList", ex);
            }
            return dt;
        }



        #endregion
    }
}
