using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;


namespace offerlinkmanageradmin.OfferLink
{
    public partial class PromotionalLinkReport : System.Web.UI.Page
    {
        string strconn = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                strconn = ConfigurationManager.AppSettings["Iframaddsense"];
                if (!Page.IsPostBack)
                {
                    ltheader.Text = "Promotional Link Report";
                    using (BLL.PromotionalLinkReportMgmt obj = new BLL.PromotionalLinkReportMgmt(strconn))
                    {
                        DataTable dt = new DataTable();
                        dt = obj.GetMainReferrelName();
                        if (dt.Rows.Count > 0)
                        {
                            PopulateTreeView(dt, 0, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/PromotionalLinkReport.aspx.cs Page_Load", ex);
            }
            
        }

        /// <summary>
        ///  Tree view for site
        /// </summary>
        /// <param name="dtParent"></param>
        /// <param name="parentId"></param>
        /// <param name="treeNode"></param>
        private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            try
            {
                foreach (DataRow row in dtParent.Rows)
                {
                    TreeNode child = new TreeNode
                    {
                        Text = row["Name"].ToString(),
                        Value = row["Id"].ToString()
                    };
                    if (parentId == 0)
                    {
                        TreeView1.Nodes.Add(child);
                        using (BLL.PromotionalLinkReportMgmt objreport = new BLL.PromotionalLinkReportMgmt(strconn))
                        {
                            DataTable dtChild = objreport.GetParentReferrel(child.Value);
                            PopulateTreeView(dtChild, int.Parse(child.Value), child);
                        }
                    }
                    else
                    {
                        treeNode.ChildNodes.Add(child);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.Admin, "OfferLink/PromotionalLinkReport.aspx.cs PopulateTreeView", ex);
            }
        }



    }
}