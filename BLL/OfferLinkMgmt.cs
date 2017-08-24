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
        private string _region = string.Empty;
        private string _isBetSlip = string.Empty;
        private string _isExpire = string.Empty;
        private string _expireDate = string.Empty;
        private string _fastbetname = string.Empty;
        private string _fastbettotile = string.Empty;
        private string _sel = string.Empty;
        private string _stake = string.Empty;
        private string _whurl = string.Empty;

        #endregion

        #region :: public properties ::

        public string Sel
        {
            get { return _sel; }
            set { _sel = value; }
        }

        public string Stake
        {
            get { return _stake; }
            set { _stake = value; }
        }

        public string Whurl
        {
            get { return _whurl; }
            set { _whurl = value; }
        }

        public string FastBetTotitle
        {
            get { return _fastbettotile; }
            set { _fastbettotile = value; }
        }

        public string FastBetName
        {
            get { return _fastbetname; }
            set { _fastbetname = value; }
        }

        public string IsBetSlip
        {
            get { return _isBetSlip; }
            set { _isBetSlip = value; }
        }

        public string IsExpire
        {
            get { return _isExpire; }
            set { _isExpire = value; }
        }

        public string ExpireDate
        {
            get { return _expireDate; }
            set { _expireDate = value; }
        }

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

        private string _randomid = "";
      
        public string RandomId
        {
            get { return _randomid; ; }
            set { _randomid = value; }
        }

        public string Region
        {
            get { return _region; ; }
            set { _region = value; }
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
        public DataTable GetOfferLinkList(int pagesize, int currentpage, string search, string region, string datesort)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param ={new SqlParameter("@PageSize",SqlDbType.Int),
										 new SqlParameter("@CurrentPage",SqlDbType.Int),
										 new SqlParameter("@ItemCount",SqlDbType.Int),										 
										 new SqlParameter("@Search",SqlDbType.VarChar),
									     new SqlParameter("@region",SqlDbType.Char),
                                         new SqlParameter("@datesort",SqlDbType.VarChar)
										 };
                param[0].Value = pagesize;
                param[1].Value = currentpage;
                param[2].Value = 0;
                param[2].Direction = ParameterDirection.InputOutput;
                param[3].Value = search;
                param[4].Value = region;
                param[5].Value = datesort;
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
        public void EditOfferLink(string linkid, string linkname, string linkref, string cookieuri, string shortenurl,string bitlyrel)
        {
            try
            {
                SqlParameter[] param ={new SqlParameter("@LinkID",SqlDbType.Int),
				new SqlParameter("@LinkName",SqlDbType.VarChar),
				new SqlParameter("@LinkReference",SqlDbType.VarChar),
				new SqlParameter("@CookieURl",SqlDbType.VarChar),
				new SqlParameter("@shortenurl",SqlDbType.NVarChar),
                new SqlParameter("@addedby",SqlDbType.Int),
                new SqlParameter("@bitlyrelation",SqlDbType.VarChar)};
                param[0].Value = linkid;
                param[1].Value = linkname;
                param[2].Value = linkref;
                param[3].Value = cookieuri;
                param[4].Value = shortenurl;
                param[5].Value = LoginInfo.Userid;
                param[6].Value = bitlyrel;
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
                new SqlParameter("@RandomUniqueId",SqlDbType.VarChar),
                new SqlParameter("@region",SqlDbType.Char),
                new SqlParameter("@IsBetSlip",SqlDbType.Char),
                new SqlParameter("@IsExpire",SqlDbType.Char),
                new SqlParameter("@ExpireDate",SqlDbType.DateTime),
                new SqlParameter("@shortenurl",SqlDbType.NVarChar),
                new SqlParameter("@FastBetName",SqlDbType.VarChar),
                new SqlParameter("@fastbettotitle",SqlDbType.VarChar)
                };
                
                param[0].Direction = ParameterDirection.InputOutput;
                param[0].Value = Linkid;
                param[1].Value = LinkName;
                param[2].Value = LinkReference;
                param[3].Value = CookieURl;
                param[4].Value = LoginInfo.Userid;
                param[5].Value = RandomId;
                param[6].Value = Region;
                param[7].Value = IsBetSlip;
                param[8].Value = IsExpire;
                param[9].Value = ExpireDate;
                param[10].Value = Shortenurl;
                param[11].Value = FastBetName;
                param[12].Value = FastBetTotitle;
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

        public int CheckDuplicateFastbet(string shortenurl,string linkid,string region)
        {
            int count = 0;
            object objcount = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@shortenurl",SqlDbType.VarChar),
                                       new SqlParameter("@linkid",SqlDbType.Int),
                                       new SqlParameter("@region",SqlDbType.Char)};
                                       
                param[0].Value = shortenurl;
                param[1].Value = linkid;
                param[2].Value = region;
                objcount = dal.ExecuteScalar(CommandType.StoredProcedure, "AN_SP_CheckDuplicateFastBetUrl", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs CheckDuplicateFastbet", ex);
            }
            if (!string.IsNullOrEmpty(objcount.ToString()))
            {
                count = Convert.ToInt32(objcount);
            }
            return count;
        }

        public int CheckDuplicatePromotionalLink(string linkname, string linkid,string region)
        {
            int count = 0;
            object objcount = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@linkname",SqlDbType.VarChar),
                                       new SqlParameter("@linkid",SqlDbType.Int),
                                       new SqlParameter("@region",SqlDbType.Char)};
                param[0].Value = linkname;
                param[1].Value = linkid;
                param[2].Value = region;
                objcount = dal.ExecuteScalar(CommandType.StoredProcedure, "AN_SP_CheckDuplicatePromotionalLink", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs CheckDuplicatePromotionalLink", ex);
            }
            if (!string.IsNullOrEmpty(objcount.ToString()))
            {
                count = Convert.ToInt32(objcount);
            }
            return count;
        }

        public void SaveDuplicatePromoLink()
        {
            try
            {
                SqlParameter[] param = { new SqlParameter("@LinkName",SqlDbType.VarChar),
                                       new SqlParameter("@region",SqlDbType.Char),
                                       new SqlParameter("@FastBetName",SqlDbType.VarChar),
                                       new SqlParameter("@IsBetSlip",SqlDbType.Char),
                                       new SqlParameter("@IsExpire",SqlDbType.Char),
                                       new SqlParameter("@ExpireDate",SqlDbType.SmallDateTime),
                                       new SqlParameter("@Addedby",SqlDbType.Int),
                                       new SqlParameter("@fastbeturl",SqlDbType.VarChar)};

                param[0].Value = LinkName;
                param[1].Value = Region;
                param[2].Value = FastBetName;
                param[3].Value = IsBetSlip;
                param[4].Value = IsExpire;
                param[5].Value = ExpireDate;
                param[6].Value = LoginInfo.Userid;
                param[7].Value = Shortenurl;

                dal.ExecuteNonQuery(CommandType.StoredProcedure, "AN_SP_DuplicatePromoLink_Save", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs SaveDuplicatePromoLink", ex);
            }
        }

        #endregion


        #region :: WH URL ::

        /// <summary>
        /// Save link  for william hill
        /// </summary>
        /// <returns></returns>
        public int WHLink_Save()
        {
            int linkid = 0;
            try
            {
                SqlParameter[] param ={new SqlParameter("@LinkID",SqlDbType.Int),
				new SqlParameter("@LinkName",SqlDbType.VarChar),
				new SqlParameter("@LinkReference",SqlDbType.VarChar),				
                new SqlParameter("@addedby",SqlDbType.Int),
                new SqlParameter("@RandomUniqueId",SqlDbType.VarChar),
                new SqlParameter("@region",SqlDbType.Char),
                new SqlParameter("@IsBetSlip",SqlDbType.Char),
                new SqlParameter("@IsExpire",SqlDbType.Char),
                new SqlParameter("@ExpireDate",SqlDbType.DateTime),
                new SqlParameter("@shortenurl",SqlDbType.NVarChar),
                new SqlParameter("@FastBetName",SqlDbType.VarChar),
                new SqlParameter("@fastbettotitle",SqlDbType.VarChar),
                new SqlParameter("@sel",SqlDbType.NVarChar),
                new SqlParameter("@stake",SqlDbType.NVarChar),
                new SqlParameter("@whurl",SqlDbType.NVarChar)
                };

                param[0].Direction = ParameterDirection.InputOutput;
                param[0].Value = Linkid;
                param[1].Value = LinkName;
                param[2].Value = LinkReference;               
                param[3].Value = LoginInfo.Userid;
                param[4].Value = RandomId;
                param[5].Value = Region;
                param[6].Value = IsBetSlip;
                param[7].Value = IsExpire;
                param[8].Value = ExpireDate;
                param[9].Value = Shortenurl;
                param[10].Value = FastBetName;
                param[11].Value = FastBetTotitle;
                param[12].Value = Sel;
                param[13].Value =Stake ;
                param[14].Value = Whurl;
                dal.ExecuteNonQuery(CommandType.StoredProcedure, "AN_SP_WHLink_Save", param);
                linkid = Convert.ToInt32(param[0].Value);

            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs WHLink_Save", ex);
            }
            return linkid;
        }


        /// <summary>
        /// check duplicate fastbeturl name for william hill
        /// </summary>
        /// <param name="shortenurl"></param>
        /// <param name="linkid"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public int CheckDuplicateFastbetForWHUrl(string shortenurl, string linkid, string region)
        {
            int count = 0;
            object objcount = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@shortenurl",SqlDbType.VarChar),
                                       new SqlParameter("@linkid",SqlDbType.Int),
                                       new SqlParameter("@region",SqlDbType.Char)};

                param[0].Value = shortenurl;
                param[1].Value = linkid;
                param[2].Value = region;
                objcount = dal.ExecuteScalar(CommandType.StoredProcedure, "AN_SP_CheckDuplicateFastBetUrlForWHUrl", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs CheckDuplicateFastbetForWHUrl", ex);
            }
            if (!string.IsNullOrEmpty(objcount.ToString()))
            {
                count = Convert.ToInt32(objcount);
            }
            return count;
        }


        /// <summary>
        /// check duplicate william hill name
        /// </summary>
        /// <param name="linkname"></param>
        /// <param name="linkid"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public int CheckDuplicateWHLinkName(string linkname, string linkid, string region)
        {
            int count = 0;
            object objcount = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@linkname",SqlDbType.VarChar),
                                       new SqlParameter("@linkid",SqlDbType.Int),
                                       new SqlParameter("@region",SqlDbType.Char)};
                param[0].Value = linkname;
                param[1].Value = linkid;
                param[2].Value = region;
                objcount = dal.ExecuteScalar(CommandType.StoredProcedure, "AN_SP_CheckDuplicateWHLinkName", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs CheckDuplicateWHLinkName", ex);
            }
            if (!string.IsNullOrEmpty(objcount.ToString()))
            {
                count = Convert.ToInt32(objcount);
            }
            return count;
        }


        /// <summary>
        /// get william hill link details
        /// </summary>
        /// <param name="linkid"></param>
        /// <returns></returns>
        public DataTable GetWHLinkDetails(string linkid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = { new SqlParameter("@LinkID", SqlDbType.Int) };
                param[0].Value = linkid;
                dt = dal.GetDataTable(CommandType.StoredProcedure, "AN_SP_WHLink_Details", param);
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "GamingNetBLL.OfferLinkMgmt.cs GetWHLinkDetails", ex);
            }
            return dt;
        }


        #endregion
    }
}
