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


        #endregion
    }
}
