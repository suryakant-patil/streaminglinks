<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master/AdminMasterPage.Master"
    CodeBehind="AddEditOfferLink.aspx.cs" Inherits="offerlinkmanageradmin.OfferLink.AddEditOfferLink"
    ValidateRequest="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphRightPanel" runat="server">
    <html>
    <head>
        <title>Add Edit Offer Link</title>
        <link href="../css/admincp.css" type="text/css" rel="stylesheet">
        <link rel="stylesheet" type="text/css" href="../datepicker/jquery.datetimepicker.css" />
    </head>
    <body topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" bgcolor="#eeeeee">
        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="1">
            <tr>
                <td align="center">
                    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#ffffff">
                        <tr>
                            <td align="center">
                                <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="1">
                                    <tr height="20">
                                        <td colspan="2" class="header">
                                            <table width='100%'>
                                                <tr>
                                                    <td width="874" class="header" align="left">
                                                        &gt;&gt;
                                                        <asp:Literal ID="ltheader" runat="server"></asp:Literal><asp:Literal ID="ltmaincat"
                                                            runat="server"></asp:Literal>
                                                        <%--  <a href="http://anil/calodianmedia/promotionalstat.aspx?siteurl=75047EBB-0F29-4CBE-8A98-2000C9F628B4&siteid=1&usersessionid=4195">calendonianmedia</a>
                                                        <a href="http://bit.ly/26ZruIr">calendonianmedia</a>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="874" valign="top" bgcolor="#ffffff">
                                            <form id="ed" method="post" enctype="multipart/form-data" runat="server" onsubmit="return validate();">
                                            <table border="0" width="100%" height="100%" cellpadding="0" cellspacing="0" align="left">
                                                <tr valign="top">
                                                    <td>
                                                        <table border="0" width="100%" cellpadding="0" cellspacing="0" bgcolor="#999999"
                                                            align="center">
                                                            <tr>
                                                                <td align="center">
                                                                    <table bordercolor="#d5e2eb" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellspacing="1" cellpadding="0" width="100%" bgcolor="#999999">
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
                                                                                                <td class="text" width="90%" align="left">
                                                                                                    <asp:RadioButtonList ID="rdoregion" runat="server" CssClass="text" RepeatDirection="Horizontal">
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
                                                                                                    &nbsp;<span class="bold"> Link </span>
                                                                                                </td>
                                                                                                <td class="text" width="90%" align="left">
                                                                                                    <asp:TextBox ID="txtlink" runat="server" Columns="85" Rows="2" TextMode="MultiLine"
                                                                                                        class="text"></asp:TextBox>
                                                                                                    <asp:RegularExpressionValidator ID="regUrl" runat="server" ControlToValidate="txtlink"
                                                                                                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" ErrorMessage="Please Enter Valid Link !"><span class="error">*</span></asp:RegularExpressionValidator>
                                                                                                   
                                                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator12" runat="server" ControlToValidate="txtlink"
                                                                                                        ErrorMessage="Please Enter Link !"><span class="error">*</span>
                                                                                                    </asp:RequiredFieldValidator>
                                                                                                     <span class='thread'>(example: http://www.bettingpro.com)</span>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr class="text" bgcolor="#ffffff">
                                                                                                <td class="headings" height="30" width="10%" nowrap align="left">
                                                                                                    &nbsp;<span class="bold"> Cookie Url </span>
                                                                                                </td>
                                                                                                <td class="text" width="90%" align="left">
                                                                                                    <asp:TextBox ID="txtcookieurl" runat="server" Columns="85" class="text" TextMode="MultiLine"
                                                                                                        Rows="1"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr class="text" bgcolor="#ffffff">
                                                                                                <td  class="headings" height="30" width="10%" nowrap align="left">
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
                                                                                                <td  class="headings" height="30" width="10%" nowrap align="left">
                                                                                                    &nbsp;Expire Date
                                                                                                </td>
                                                                                                <td class='text'>
                                                                                                    <asp:TextBox ID="txtexpiredate" CssClass="text" Columns="15" runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr class="text" bgcolor="#ffffff">
                                                                                                <td  class="headings" height="30" width="10%" nowrap align="left">
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
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            </form>
                                        </td>
                                    </tr>
                                </table>
                                </FORM>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <script src="../datepicker/jquery.datetimepicker.js"></script>
        <script>
        $('#<%=txtexpiredate.ClientID %>').datetimepicker({timepicker:true,format:'d/m/Y H:i',step:15})

       
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
