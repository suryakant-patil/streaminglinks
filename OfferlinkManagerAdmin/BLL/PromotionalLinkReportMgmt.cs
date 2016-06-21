using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CommonLib;

namespace BLL
{
    public class PromotionalLinkReportMgmt:IDisposable
    {
         DAL dal;
        #region :: constructor ::

        public PromotionalLinkReportMgmt()
        {
            //
            // TODO: Add constructor logic here
            //
            dal = new DAL();
        }
        #endregion

        #region :: prameterized constructor ::
        public PromotionalLinkReportMgmt(string dbconn)
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

        #region :: Destructor ::
        ~PromotionalLinkReportMgmt()
        {
            Dispose();
        }
        #endregion

        private int _totalCount = 0;

        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }

        #region :: Method ::
        public DataTable GetMainReferrelName()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_GetMainReferrel");
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetMainReferrelName", ex);
            }
            return dt;
        }

        public DataTable GetParentReferrel(string parentreferrelid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = { new SqlParameter("@ParentReferrerId",SqlDbType.Int) };
                param[0].Value = parentreferrelid;
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_GetParentReferrel", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetParentReferrel", ex);
            }
            return dt;
        }

        //
        public DataTable GetPromotionalExitClickList(string fromdate ,string todate)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = { new SqlParameter("@fromdate",SqlDbType.Date),
                                       new SqlParameter("@todate",SqlDbType.Date)};
                param[0].Value = fromdate;
                param[1].Value = todate;
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_GetPromotionalLinkExitClickTotal", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetPromotionalExitClickList", ex);
            }
            return dt;
        }

        public DataTable GetPromotionalExitClickListDetails(string fromdate, string todate, string referrerid, int pagesize, int currentpage)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = { new SqlParameter("@fromdate",SqlDbType.Date),
                                       new SqlParameter("@todate",SqlDbType.Date),
                                       new SqlParameter("@referrerid",SqlDbType.Int),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
									    new SqlParameter("@CurrentPage",SqlDbType.Int),
										new SqlParameter("@ItemCount",SqlDbType.Int)	};
                param[0].Value = fromdate;
                param[1].Value = todate;
                param[2].Value = referrerid;
                param[3].Value = pagesize;
                param[4].Value = currentpage;
                param[5].Value = 0;
                param[5].Direction = ParameterDirection.InputOutput;
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_GetPromotionalLinkExitClickDetails", param);
                TotalCount = Convert.ToInt32(param[5].Value);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetPromotionalExitClickListDetails", ex);
            }
            return dt;
        }


        public DataTable GetPromotionalLinkDetailsForExcel(string fromdate, string todate, string referrerid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = { new SqlParameter("@fromdate",SqlDbType.Date),
                                       new SqlParameter("@todate",SqlDbType.Date),
                                       new SqlParameter("@referrerid",SqlDbType.Int)};
                param[0].Value = fromdate;
                param[1].Value = todate;
                param[2].Value = referrerid;
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_GetPromotionalLinkDetailsForExcel", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetPromotionalExitClickListDetails", ex);
            }
            return dt;
        }
        //

        public int GetPromotionalLinkCountDayWise(int day,int referrerid)
        {
            int count = 0;
            object objday = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@day", SqlDbType.Int),
                                       new SqlParameter("@ReferrerId",SqlDbType.Int)};
                param[0].Value = day;
                param[1].Value = referrerid;
                objday = dal.ExecuteScalar(CommandType.StoredProcedure, "AN_SP_GetPromotionalLinCountDaywise", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetPromotionalLinkCountDayWise", ex);
            }
            if (!string.IsNullOrEmpty(objday.ToString()))
            {
                count = Convert.ToInt32(objday);
            }
            return count;

        }

        public int GetPromotionalReferrelurlCountDayWise(int day, string referrerurl)
        {
            int count = 0;
            object objcount=null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@day", SqlDbType.Int),
                                       new SqlParameter("@ReferrerUrl",SqlDbType.VarChar)};
                param[0].Value = day;
                param[1].Value = referrerurl;
                objcount = dal.ExecuteScalar(CommandType.StoredProcedure, "AN_SP_GetPromotionalReferrelCountDaywise", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetPromotionalReferrelurlCountDayWise", ex);
            }
            if (!string.IsNullOrEmpty(objcount.ToString()))
            {
                count = Convert.ToInt32(objcount);
            }
            return count;

        }

        public DataTable GetSiteWiseTrackingDetails(string day1,string day2)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = { new SqlParameter("@fromdate",SqlDbType.DateTime),
                                       new SqlParameter("@todate",SqlDbType.DateTime)};
                param[0].Value = day1;
                param[1].Value = day2;
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_GetSiteWiseTrackingDetails",param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetSiteWiseTrackingDetails", ex);
            }
            return dt;
        }

        public int GetPromotionalLinkCountDayWise_Site(int day, int referrerid,string siteid)
        {
            int count = 0;
            object objday = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@day", SqlDbType.Int),
                                       new SqlParameter("@ReferrerId",SqlDbType.Int),
                                       new SqlParameter("@siteid",SqlDbType.Int)};
                param[0].Value = day;
                param[1].Value = referrerid;
                param[2].Value = siteid;
                objday = dal.ExecuteScalar(CommandType.StoredProcedure, "AN_SP_GetPromotionalLinCountDaywise_Site", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetPromotionalLinkCountDayWise_Site", ex);
            }
            if (!string.IsNullOrEmpty(objday.ToString()))
            {
                count = Convert.ToInt32(objday);
            }
            return count;

        }


        public string GetExitClikHourswise(string startdate)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            string reviewid = string.Empty;

            string reviewhour = string.Empty;
            string reviewstring = string.Empty;
                      

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            try
            {
                SqlParameter[] param = { new SqlParameter("@startdate", SqlDbType.DateTime) };
                param[0].Value = startdate;               
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_OfferLinkWise", param);               
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        reviewid = dr["linkid"].ToString();
                        sb.Append("<tr style='color: #333333;'><td bgcolor='#eeeeee' align='left'  valign='middle' style='font-family:verdana;font-size:11px;'>" + dr["linkname"].ToString() + "</td>");

                        reviewhour = "";
                        reviewstring = "";

                        SqlParameter[] param1 ={new SqlParameter("@linkid",SqlDbType.Int),
						new SqlParameter("@startdate",SqlDbType.DateTime)};
                        param1[0].Value = dr["linkid"].ToString();
                        param1[1].Value = startdate;
                        int cntvalue = 0;
                        int day = 0;                      
                        dt1 = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_ExitClickTotalPromotionalLinkWise", param1);
                         
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                reviewhour = dr1["hour1"].ToString();

                                for (day = cntvalue; day < 24; day++)
                                {
                                    if (reviewhour == day.ToString())
                                    {
                                        reviewstring += "<td bgcolor='#FFFFFF' valign='middle' style='font-family:verdana;font-size:11px;text-align:center;'>" + dr1["counts"].ToString() + "</td>";
                                        cntvalue++;
                                        break;
                                    }
                                    else
                                    {
                                        reviewstring += "<td bgcolor='#FFFFFF' valign='middle' style='font-family:verdana;font-size:11px;text-align:center;'>&nbsp;</td>";
                                    }
                                    cntvalue++;
                                }
                            }
                            for (int day1 = day; day1 < 23; day1++)
                            {
                                reviewstring += "<td bgcolor='#FFFFFF'>&nbsp;</td>";
                            }
                        }

                        sb.Append("<td bgcolor='#FFFFFF' align='right' valign='middle' style='font-family:verdana;font-size:11px;text-align:center;'>" + dr["counts"].ToString() + "</td>");
                        sb.Append(reviewstring);
                        sb.Append("</tr>");
                    }
                }
            }
            catch (System.Exception ex)
            {               
                CommonLib.ExceptionHandler.WriteLog(Sections.BLL, "PromotionalLinkReportMgmt.cs GetExitClikHourswise", ex);
            }
            finally
            {

            }
            return sb.ToString();
        }




        #endregion
    }
}
