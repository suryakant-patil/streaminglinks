<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AddEditWHLink.aspx.cs" Inherits="offerlinkmanageradmin.OfferLink.AddEditWHLink" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphRightPanel" runat="server">
    <html>
    <head>
        <title>Add Edit Link</title>
        <link href="../css/admincp.css" type="text/css" rel="stylesheet">
        <link rel="stylesheet" type="text/css" href="../datepicker/jquery.datetimepicker.css" />
        <style>
            .maintable{ border-collapse: collapse;}
            .maintable tr td{border: 1px solid #999999;} 
            #rdo td{border:none;}
        </style>
    </head>
    <body topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" bgcolor="#eeeeee">
       
        <table width='100%'>
            <tr>
                <td width="874" class="header" align="left">
                    &gt;&gt;
                    <asp:Literal ID="ltheader" runat="server"></asp:Literal><asp:Literal ID="ltmaincat"
                        runat="server"></asp:Literal>
                  
                </td>
            </tr>
            <tr>
            <td width="874" valign="top" bgcolor="#ffffff">
                <form id="ed" method="post" enctype="multipart/form-data" runat="server" onsubmit="return validate();">
                <table cellspacing="1" cellpadding="0" width="100%" bgcolor="#999999" class="maintable">
                    <tbody>
                        <tr align="left" bgcolor="#eeeeee" runat="server" valign="middle" height="30" id="Tr1"
                            name="Tr1">
                            <td class="headings" colspan="2" valign="middle">
                                <asp:Literal ID="lttop" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr id="validPage" align="left" bgcolor="#ffffff" runat="server" valign="middle"
                            height="30">
                            <td class="error" colspan="2" valign="middle">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error" DisplayMode="List">
                                </asp:ValidationSummary>
                                <asp:Literal ID="ltError" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr id="dupli" align="left" bgcolor="#ffffff" runat="server" valign="middle">
                            <td class="error" colspan="2" height="30">
                                <asp:Literal ID="ltdupsub" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr id="Tr2" align="left" bgcolor="#ffffff" runat="server" valign="middle">
                            <td class="error" colspan="2" height="30">
                                <asp:Literal ID="ltDup" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr class="text" bgcolor="#ffffff">
                            <td class="headings" height="30" width="10%" nowrap align="left">
                                &nbsp;<span class="bold"> Region</span>
                            </td>
                            <td class="text" width="90%" align="left" id="rdo">
                               <asp:RadioButtonList ID="rdoregion" runat="server" CssClass="text"  RepeatDirection="Horizontal">
                                    <asp:ListItem Text="AU" Value="AU"></asp:ListItem>
                                    <asp:ListItem Text="UK" Value="GB" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="text" bgcolor="#ffffff">
                            <td class="headings" height="30" width="10%" nowrap align="left">
                                &nbsp;<span class="bold"> Link Name</span>
                            </td>
                            <td class="text" width="90%" align="left">
                                <asp:TextBox ID="txtLinkName" runat="server" Columns="75" class="text"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="Requiredfieldvalidator2" runat="server" ControlToValidate="txtLinkName" ErrorMessage="Please Enter Link Name !"><span class="error">*</span>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                         <tr class="text" bgcolor="#ffffff">
                            <td class="headings" height="30" width="10%" nowrap align="left">
                                &nbsp;<span class="bold"> WH Url </span>
                            </td>
                            <td class="text" width="90%" align="left">
                                <asp:TextBox ID="txtWhUrl" runat="server" Columns="85" Rows="4" TextMode="MultiLine"
                                    class="text" Text="http://ads2.williamhill.com/redirect.aspx?pid=191319428&lpid=1092000163&bid=908371202&var3=bet/EN/addtoslip?action=BuildSlip%26price=y%26ew=n" ReadOnly="true"></asp:TextBox>
                                
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="txtWhUrl"
                                    ErrorMessage="Please Enter WH Url !"><span class="error">*</span>
                                </asp:RequiredFieldValidator>
                               
                            </td>
                        </tr>
                        <tr class="text" bgcolor="#ffffff">
                            <td class="headings" height="30" width="10%" nowrap align="left">
                                &nbsp;<span class="bold"> Sel </span>
                            </td>
                            <td class="text" width="90%" align="left">
                                <asp:TextBox ID="txtSel" runat="server" Columns="85" Rows="1" TextMode="MultiLine"
                                    class="text"></asp:TextBox>
                                
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator12" runat="server" ControlToValidate="txtSel"
                                    ErrorMessage="Please Enter Sel !"><span class="error">*</span>
                                </asp:RequiredFieldValidator>
                               
                            </td>
                        </tr>
                        <tr class="text" bgcolor="#ffffff">
                            <td class="headings" height="30" width="10%" nowrap align="left">
                                &nbsp;<span class="bold"> Stake </span>
                            </td>
                            <td class="text" width="90%" align="left">
                                <asp:TextBox ID="txtStake" runat="server" Columns="85" Rows="1" TextMode="MultiLine"
                                    class="text"></asp:TextBox>
                                
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="txtStake"
                                    ErrorMessage="Please Enter Stake !"><span class="error">*</span>
                                </asp:RequiredFieldValidator>
                               
                            </td>
                        </tr>
                        <tr class="text" bgcolor="#ffffff">
                            <td class="headings" height="30" width="10%" nowrap align="left">
                                &nbsp;<span class="bold"> Url </span>
                            </td>
                            <td class="text" width="90%" align="left">
                                <asp:TextBox ID="txtUrl" runat="server" Columns="85" Rows="2" TextMode="MultiLine"
                                    class="text"></asp:TextBox>
                                
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ControlToValidate="txtUrl"
                                    ErrorMessage="Please Enter Url !"><span class="error">*</span>
                                </asp:RequiredFieldValidator>
                               
                            </td>
                        </tr>
                        
                        <tr class="text" bgcolor="#ffffff">
                            <td class="headings" height="30" width="10%" nowrap align="left">
                                &nbsp;Is Expire
                            </td>
                            <td class='text'>
                                <asp:DropDownList ID="ddlexpire" runat="server" CssClass="text">
                                    <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="text" bgcolor="#ffffff">
                            <td class="headings" height="30" width="10%" nowrap align="left">
                                &nbsp;Expire Date
                            </td>
                            <td class='text'>
                                <asp:TextBox ID="txtexpiredate" CssClass="text" Columns="15" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="text" bgcolor="#ffffff">
                            <td class="headings" height="30" width="10%" nowrap align="left">
                                &nbsp;FastBet Name
                            </td>
                            <td class='text'>
                                <asp:TextBox ID="txtfastbetname" CssClass="text" Columns="30" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="txtfastbetname"
                                    ErrorMessage="Please Enter FastBet Name !"><span class="error">*</span>
                                </asp:RequiredFieldValidator>
                                <span class='thread'>Only 20 character's allowed</span>
                            </td>
                        </tr>
                        <tr class="text" bgcolor="#ffffff">
                            <td height="30">
                                &nbsp;
                            </td>
                            <td align="left" height="30" valign="middle">
                                &nbsp;<input type="submit" id="btnsubmit" value="submit" class='buttontext'>
                                &nbsp;<input type="reset" class='buttontext' value="Cancel" name="reset" onclick="document.location.href='ListOfferLinks.aspx'">
                            </td>
                        </tr>
                    </tbody>
                </table>
                </form>
            </td>
            </tr>
        </table>
       
        
        <script src="../datepicker/jquery.datetimepicker.js"></script>
        <script>
            $('#<%=txtexpiredate.ClientID %>').datetimepicker({ timepicker: true, format: 'd/m/Y H:i', step: 15 })

       
        </script>
        <script>

            function validate() {

                var length = $('#<%=txtfastbetname.ClientID %>').val().length;
                console.log(length);
                if (length > 21) {
                    alert('Please add FastBet Name !');
                    return false;
                }
                else { return true; }



            }
        </script>
    </body>
    </html>
</asp:Content>