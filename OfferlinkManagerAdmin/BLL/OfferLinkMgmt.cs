using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLib;
using System.Data;
using System.Data.SqlClient;


namespace BLL
{
    public class OfferLinkMgmt:IDisposable
    {
        
        DAL dal;
        #region :: constructor ::

        public OfferLinkMgmt()
        {
            //
            // TODO: Add constructor logic here
            //
            dal = new DAL();
        }
        #endregion

        #region :: prameterized constructor ::
        public OfferLinkMgmt(string dbconn)
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
        ~OfferLinkMgmt()
        {
            Dispose();
        }
        #endregion

        #region :: private properties ::

        private int _totalCount = 0;
        private string _linkid = "0";
        private string _linkName = string.Empty;
        private string _linkReference = string.Empty;
        private string _cookieURl = string.Empty;
        private string _action = string.Empty;
        private string _actiondate = string.Empty;
        private string _oldvalues = string.Empty;
        private string _newvalues = string.Empty;
        private string _actionby = string.Empty;
        private string _shortenurl = string.Empty;

        #endregion

        #region :: public properties ::

        public string Shortenurl
        {
            get { return _shortenurl; }
            set { _shortenurl = value; }
        }

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        public string ActionDate
        {
            get { return _actiondate; }
            set { _actiondate = value; }
        }
        public string OldValues
        {
            get { return _oldvalues; }
            set { _oldvalues = value; }
        }
        public string NewValues
        {
            get { return _newvalues; }
            set { _newvalues = value; }
        }
        public string ActionBy
        {
            get { return _actionby; }
            set { _actionby = value; }
        }

        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }
        public string Linkid
        {
            get { return _linkid; }
            set { _linkid = value; }
        }
        public string LinkName
        {
            get { return _linkName; }
            set { _linkName = value; }
        }
        public string LinkReference
        {
            get { return _linkReference; }
            set { _linkReference = value; }
        }
        public string CookieURl
        {
            get { return _cookieURl; }
            set { _cookieURl = value; }
        }



        #endregion

        #region :: Method ::

        /// <summary>
        ///   Get OfferlinkList
        /// </summary>
        /// <param name="pagesize">pagesize</param>
        /// <param name="currentpage">currentpage</param>
        /// <param name="search">search text</param>
        /// <returns>datattable</returns>
        public DataTable GetOfferLinkList(int pagesize, int currentpage, string search)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param ={new SqlParameter("@PageSize",SqlDbType.Int),
										 new SqlParameter("@CurrentPage",SqlDbType.Int),
										 new SqlParameter("@ItemCount",SqlDbType.Int),										 
										 new SqlParameter("@Search",SqlDbType.VarChar)									 
										 };
                param[0].Value = pagesize;
                param[1].Value = currentpage;
                param[2].Value = 0;
                param[2].Direction = ParameterDirection.InputOutput;
                param[3].Value = search;                              
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_OfferLinkList", param);
                TotalCount = Convert.ToInt32(param[2].Value);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs GetOfferLinkList", ex);                  
            }
            return dt;
        }

        /// <summary>
        ///  Edit Offer Link
        /// </summary>
        /// <param name="linkid">linkid</param>
        /// <param name="linkname">linkname</param>
        /// <param name="linkref">linkreference</param>
        /// <param name="cookieuri">cookieuri</param>
        /// <param name="shortenurl">bitlyur</param>
        public void EditOfferLink(string linkid, string linkname, string linkref, string cookieuri, string shortenurl)
        {
            try
            {
                SqlParameter[] param ={new SqlParameter("@LinkID",SqlDbType.Int),
				new SqlParameter("@LinkName",SqlDbType.VarChar),
				new SqlParameter("@LinkReference",SqlDbType.VarChar),
				new SqlParameter("@CookieURl",SqlDbType.VarChar),
				new SqlParameter("@shortenurl",SqlDbType.NVarChar),
                new SqlParameter("@addedby",SqlDbType.Int)};
                param[0].Value = linkid;
                param[1].Value = linkname;
                param[2].Value = linkref;
                param[3].Value = cookieuri;
                param[4].Value = shortenurl;
                param[5].Value = LoginInfo.Userid;             
                dal.ExecuteNonQuery(CommandType.StoredProcedure, "AN_SP_OfferLink_Edit", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs EditOfferLink", ex);
            }

        }

        /// <summary>
        ///  Change Link Status
        /// </summary>
        /// <param name="linkids">linkids</param>
        /// <param name="status">status</param>
        public void ChangeOfferLinkStatus(string linkids, string status)
        {
            try
            {
                SqlParameter[] param ={new SqlParameter("@LinkIds",SqlDbType.VarChar),
				new SqlParameter("@IsActive",SqlDbType.Char)};
                param[0].Value = linkids;
                param[1].Value = status;              
                dal.ExecuteNonQuery(CommandType.StoredProcedure, "AN_SP_ChangeOfferLinkStatus", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs ChangeOfferLinkStatus", ex);
            }
        }

        /// <summary>
        /// Save Offer Link
        /// </summary>
        public int LinkOffer_Save()
        {
            int linkid = 0;
            try
            {
                SqlParameter[] param ={new SqlParameter("@LinkID",SqlDbType.Int),
				new SqlParameter("@LinkName",SqlDbType.VarChar),
				new SqlParameter("@LinkReference",SqlDbType.VarChar),
				new SqlParameter("@CookieURl",SqlDbType.VarChar),
                new SqlParameter("@addedby",SqlDbType.Int),
                new SqlParameter("@RandomUniqueId",SqlDbType.VarChar)};
                
                param[0].Direction = ParameterDirection.InputOutput;
                param[0].Value = Linkid;
                param[1].Value = LinkName;
                param[2].Value = LinkReference;
                param[3].Value = CookieURl;
                param[4].Value = LoginInfo.Userid;
                param[5].Value = GenerateUniqueID();
                dal.ExecuteNonQuery(CommandType.StoredProcedure, "AN_SP_OfferLink_Save", param);
                linkid = Convert.ToInt32(param[0].Value);

            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs LinkOffer_Save", ex);
            }
            return linkid;
        }


        private string GenerateUniqueID()
        {
            string uniqueId="";
            Guid randomId = Guid.NewGuid();
            return  uniqueId = randomId.ToString().ToUpper();
        }

        /// <summary>
        ///  Get Offer Link Details
        /// </summary>
        /// <param name="linkid">linkid</param>
        /// <returns>datatable</returns>
        public DataTable GetLinkOfferDetails(string linkid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = { new SqlParameter("@LinkID", SqlDbType.Int) };
                param[0].Value = linkid;             
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_OfferLink_Details", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs GetLinkOfferDetails", ex);  
            }
            return dt;
        }

        /// <summary>
        ///  Get USer Name
        /// </summary>
        /// <param name="userid">userids</param>
        /// <returns>string</returns>
        public string Getusersname(string userid)
        {
            string username = "";
            string ids = "";
            try
            {
                string[] strarry = userid.Split(',');
                if (strarry.Length > 0)
                {
                    try
                    {
                        for (int i = 0; i < strarry.Length; i++)
                        {
                            if (strarry[i].Trim().Length > 0)
                            {
                                if (ids.Length == 0)
                                {
                                    ids = strarry[i];
                                }
                                else
                                {
                                    ids = ids + "," + strarry[i];
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                    if (ids.Trim().Length > 0)
                    {
                        DataTable dt = new DataTable();
                        SqlParameter[] param = { new SqlParameter("@userid", SqlDbType.VarChar) };
                        param[0].Value = ids;                      
                        dt = dal.GetDataTable(CommandType.StoredProcedure, "LSTRM_AN_SP_GetFreeBetOfferAddedByUsers", param);
                        if (dt.Rows.Count == 2)
                        {
                            username = dt.Rows[0]["FullName"].ToString() + "<br/>" + dt.Rows[1]["FullName"].ToString();
                        }
                        else
                        {
                            username = dt.Rows[0]["FullName"].ToString();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs Getusersname", ex);  
            }
            return username;

        }

        /// <summary>
        ///  Save Offer Link Histotry
        /// </summary>
        public void SaveOfferLinkHistory()
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@action",SqlDbType.VarChar),                                       
                                       new SqlParameter("@oldvalues",SqlDbType.VarChar),
                                       new SqlParameter("@newvalues",SqlDbType.VarChar),
                                       new SqlParameter("@actionby",SqlDbType.Int),
                                       new SqlParameter("@linkid",SqlDbType.Int)};

                param[0].Value = Action;               
                param[1].Value = OldValues;
                param[2].Value = NewValues;
                param[3].Value = LoginInfo.Userid;
                param[4].Value = Linkid;
                dal.ExecuteNonQuery(CommandType.StoredProcedure, "AN_SP_OfferLinkHistory_Save", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs SaveOfferLinkHistory", ex);    
            }
        }

        /// <summary>
        ///  Get Offerlink History List
        /// </summary>
        /// <param name="pagesize">page size</param>
        /// <param name="currentpage">current page</param>
        /// <param name="linkid">linkid</param>
        /// <returns>datattable</returns>
        public DataTable GetOfferLinkHistoryList(int pagesize, int currentpage,string linkid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param ={new SqlParameter("@PageSize",SqlDbType.Int),
										 new SqlParameter("@CurrentPage",SqlDbType.Int),
										 new SqlParameter("@ItemCount",SqlDbType.Int),										 
										 new SqlParameter("@linkid",SqlDbType.Int)							 
									 };
                param[0].Value = pagesize;
                param[1].Value = currentpage;
                param[2].Value = 0;
                param[2].Direction = ParameterDirection.InputOutput;
                param[3].Value = linkid;
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_OfferLinkHistory_Paging", param);
                TotalCount = Convert.ToInt32(param[2].Value);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs GetOfferLinkHistoryList", ex);   
            }
            return dt;
        }

        public void AddDeletedBy(string linkids)
        {
            string strsql = "";
            try
            {
                SqlParameter[] param = { new SqlParameter("@linkid",SqlDbType.VarChar) };
                param[0].Value=linkids;
                strsql = "update OfferLinkManager set deletedby=" + LoginInfo.Userid + " where LinkID in(@linkid)";

                dal.ExecuteNonQuery(CommandType.Text,strsql,param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs AddDeletedBy", ex);
            }
        }

        public void AddShortenUrl(int linkid)
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@linkid",SqlDbType.Int),
                                       new SqlParameter("@shortenurl",SqlDbType.NVarChar)};
                param[0].Value = linkid;
                param[1].Value = Shortenurl;

                dal.ExecuteNonQuery(CommandType.StoredProcedure, "AN_SP_AddShortenUrl", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs AddShortenUrl", ex);
            }
        }

        public DataTable GetLoginUserDetails(string uid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = { new SqlParameter("@UID",SqlDbType.Int) };
                param[0].Value = uid;
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_GetLoginUserDetails", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs GetLoginUserDetails", ex);
            }
            return dt;
        }

        #endregion
    }
}
