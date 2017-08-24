<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="PromotionalLinkReport.aspx.cs" Inherits="offerlinkmanageradmin.OfferLink.PromotionalLinkReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphRightPanel" runat="server">
    <body bottommargin="0" bgcolor="#eeeeee" leftmargin="0" topmargin="0" rightmargin="0">
        <table height="100%" cellspacing="1" cellpadding="0" width="100%" border="0">
            <tr>
                <td align="center">
                    <table height="100%" cellspacing="1" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td align="center">
                                <table height="100%" cellspacing="1" cellpadding="0" width="100%" align="center"
                                    bgcolor="#ffffff" border="0">
                                    <tr height="20">
                                        <td class="header" colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td class="header" align="left" width="82%">
                                                        &gt;&gt;
                                                        <asp:Literal ID="ltheader" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" width="874" bgcolor="#ffffff">
                                            <form id="form1" method="post" runat="server">
                                            <table height="100%" cellspacing="0" cellpadding="0" width="100%" align="center"
                                                border="0">
                                                <tr height="10">
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr valign="top" bgcolor="#ffffff">
                                                    <td align="center" width="100%">
                                                        <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                            <tr>
                                                                <td align="center">
                                                                    <table cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellspacing="1" cellpadding="3" width="100%" align="center" bgcolor="#999999"
                                                                                        border="0">
                                                                                       
                                                                                        <asp:TreeView ID="TreeView1" runat="server" NodeIndent="15">
                                                                                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                                                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                                                                                NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                                                                            <ParentNodeStyle Font-Bold="False" />
                                                                                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                                                                                VerticalPadding="0px" />
                                                                                        </asp:TreeView>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            </form>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </body>
</asp:Content>
