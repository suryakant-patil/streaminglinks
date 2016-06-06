<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master/AdminMasterPage.Master" CodeBehind="AddEditOfferLink.aspx.cs" Inherits="offerlinkmanageradmin.OfferLink.AddEditOfferLink" ValidateRequest="false" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphRightPanel" runat="server">
<HTML>
	<HEAD>
		<title>Add Edit Offer Link</title>
		<LINK href="../css/admincp.css" type="text/css" rel="stylesheet">
		
	</HEAD>
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
													
													<td width="874" class="header" align="left">&gt;&gt;
														<asp:Literal ID="ltheader" Runat="server"></asp:Literal><asp:Literal id="ltmaincat" Runat="server"></asp:Literal>
                                                      <%--  <a href="http://anil/calodianmedia/promotionalstat.aspx?siteurl=75047EBB-0F29-4CBE-8A98-2000C9F628B4&siteid=1&usersessionid=4195">calendonianmedia</a>
                                                        <a href="http://bit.ly/26ZruIr">calendonianmedia</a>--%>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										
										<td width="874" valign="top" bgcolor="#ffffff">
											<form id="ed"  method="post" encType="multipart/form-data" 
												runat="server" onsubmit="validate();">
												<table border="0" width="100%" height="100%" cellpadding="0" cellspacing="0" align="left">
													<tr valign="top">
														<td>
															<table border="0" width="100%" cellpadding="0" cellspacing="0" bgcolor="#999999" align="center">
																<tr>
																	<td align="center">
																		<table borderColor="#d5e2eb" cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<TBODY>
																				<tr>
																					<td>
																						<table cellSpacing="1" cellPadding="0" width="100%" bgColor="#999999">
																							<TBODY>
																								<tr align="left" bgColor="#eeeeee" runat="server" valign="middle" height="30" ID="Tr1"
																									NAME="Tr1">
																									<td class="headings" colSpan="2" valign="middle"><asp:literal id="lttop" runat="server"></asp:literal></td>
																								</tr>
																								<tr id="validPage" align="left" bgColor="#ffffff" runat="server" valign="middle" height="30">
																									<td class="error" colSpan="2" valign="middle"><asp:validationsummary id="ValidationSummary1" runat="server" CssClass="error" DisplayMode="List"></asp:validationsummary><asp:literal id="ltError" runat="server"></asp:literal></td>
																								</tr>
																								<tr id="dupli" align="left" bgColor="#ffffff" runat="server" valign="middle">
																									<td class="error" colSpan="2" height="30"><asp:literal id="ltdupsub" runat="server"></asp:literal></td>
																								</tr>
																								<tr id="Tr2" align="left" bgColor="#ffffff" runat="server" valign="middle">
																									<td class="error" colSpan="2" height="30"><asp:literal id="ltDup" runat="server"></asp:literal></td>
																								</tr>
																								<tr class="text" bgColor="#ffffff">
																									<td class="headings" height="30" width="10%" noWrap align="left">&nbsp;<span class="bold">
																												 Link Name</span>
																									</td>
																									<td class="text" width="90%" align="left"><asp:TextBox ID="txtLinkName" Runat="server" Columns="75" class="text"></asp:TextBox><asp:RequiredFieldValidator ID="Requiredfieldvalidator2" Runat="server" ControlToValidate="txtLinkName"
																											ErrorMessage="Please Enter Link Name !"><span class="error">*</span>
																										</asp:RequiredFieldValidator>
																									</td>
																								</tr>																	
																								<tr class="text" bgColor="#ffffff">
																									<td class="headings" height="30" width="10%" noWrap align="left">&nbsp;<span class="bold">
																												 Link </span>
																									</td>
																									<td class="text" width="90%" align="left"><asp:TextBox ID="txtlink" Runat="server" Columns="75" Rows="10" TextMode="MultiLine" class="text"></asp:TextBox>
																									
																									</td>
																								</tr>
																								<tr class="text" bgColor="#ffffff">
																									<td class="headings" height="30" width="10%" noWrap align="left">&nbsp;<span class="bold">
																												 Cookie Url </span>
																									</td>
																									<td class="text" width="90%" align="left"><asp:TextBox ID="txtcookieurl" Runat="server" Columns="75" class="text" TextMode="MultiLine" Rows="10"></asp:TextBox>
																									
																									</td>
																								</tr>
																								
																								<tr class="text" bgColor="#ffffff">
																									<td height="30">&nbsp;</td>
																									<td align="left" height="30" valign="middle">&nbsp;<input type="submit" id="btnsubmit" value="submit" class='buttontext'>
																										&nbsp;<input type="reset" class='buttontext' value="Cancel" name="reset" onclick="document.location.href='ListOfferLinks.aspx'">
																									</td>
																								</tr>
																							</TBODY>
																						</table>
																					</td>
																				</tr>
																			</TBODY>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td>&nbsp;</td>
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
			  
	</body>
</HTML>
</asp:content>

