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

        public DataTable GetPromotionalExitClickListDetails(string fromdate, string todate,string referrerid)
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
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_GetPromotionalLinkExitClickDetails", param);
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



        #endregion
    }
}
