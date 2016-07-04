<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="LinkWiseLandingReport.aspx.cs" Inherits="offerlinkmanageradmin.Report.LinkWiseLandingReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRightPanel" runat="server">
<HTML>
	<HEAD>
		<title></title>
		<LINK href="../css/admincp.css" type="text/css" rel="stylesheet">
        <script src="../datepicker/jquery.datetimepicker.js"></script>
        <link href="../datepicker/jquery.datetimepicker.css" rel="Stylesheet" />
	</HEAD>
	<body bottomMargin="0" bgColor="#eeeeee" leftMargin="0" topMargin="0" rightMargin="0">
		<table height="100%" cellSpacing="1" cellPadding="0" width="100%" border="0">
			<tr>
				<td align="center">
					<table height="100%" cellSpacing="1" cellPadding="0" width="100%" border="0">
						<tr>
							<td align="center">
								<table height="100%" cellSpacing="1" cellPadding="0" width="100%" align="center" bgColor="#ffffff"
									border="0">
									
									<tr height="20">
										<td class="header" colSpan="2">
											<table width="100%">
												<tr>
													
													<td class="header" align="left" width="82%">&gt;&gt;
														<asp:literal id="ltheader" Runat="server"></asp:literal></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td vAlign="top" width="180" bgColor="#ffffff"></td>
										<td vAlign="top" align="center" width="100%" bgColor="#ffffff">
											<form id="mainfoot" method="post" runat="server">
												<table height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr height="10">
														<td>&nbsp;</td>
													</tr>
													<tr vAlign="top" bgColor="#ffffff">
														<td align="center" width="100%">
															<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																<tr>
																	<td align="center">
																		<table cellSpacing="0" cellPadding="0" width="98%" align="center" border="0">
																			<TBODY>
																			<tr>
																			 <td class="text" align="left" bgColor="#ffffff" colSpan="6"><asp:literal id="ltpaging" Runat="server"></asp:literal></td>
																				</tr>
                                                                                <tr>
                                                                                <td class="text" align="right">
                                                                                Start date <asp:TextBox ID="txtstartdate" runat="server" Class="text"></asp:TextBox>&nbsp;
                                                                                End date <asp:TextBox ID="txtenddate" runat="server" Class="text"></asp:TextBox>&nbsp;                                                                              
                                                                                <asp:Button ID="btnsearch" class="buttontext" Runat="server" Text="Search"></asp:Button>
                                                                                <input type="button" class="buttontext" value="clear" onclick="Showlist();" />
                                                                                &nbsp;<input id="btnexcel" name="btnexcel" value="Export To Excel" onclick="showExcellist()" type="button" class="buttontext"/>
                                                                                </br>                                                                                 
                                                                                </td>
                                                                                </tr>
                                                                                <tr height='30'>
                                                                                <td class="text"> Link Name <asp:DropDownList ID="ddllink" runat="server" AutoPostBack="true" CssClass="text" style='width:200px;'></asp:DropDownList>
                                                                                &nbsp; 
                                                                                </td>
                                                                                 
                                                                                </tr>
                                                                                <tr class="text" align="left">
                                                                                <td><asp:Literal ID="ltreferrername" runat="server"></asp:Literal></td>
                                                                                </tr>
																				<tr>
																					<td>
																						<table cellSpacing="1" cellPadding="3" width="100%" align="center" bgColor="#999999" border="0">
																							
																							<tr bgColor="#eeeeee" height="20">
																								<td class="headings" align="center" width ="20px"></td>
																								<td class="headings" align="left" width ="100px">Visit Date</td>
																								<td class="headings" align="left" width ="200px">Engine</td>																									
                                                                                                <td class="headings" align="left" width ="100px">Keyword</td>																								
                                                                                                <td class="headings" align="left" width ="200px">Landing Page</td>	
                                                                                                <td class="headings" align="left" width ="70px">Exit Date</td>
                                                                                                <td class="headings" align="left" width ="200px">Exit Page</td>																											
																							</tr>
																							<asp:literal id="ltlist" Runat="server"></asp:literal>
																							
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
		<script>

		    function showExcellist() {
		        window.location.href = 'LinkWiseLandingReport.aspx?excel=excel';
		    }

		    function Showlist() {	      

		        window.location.href = 'LinkWiseLandingReport.aspx?clear=clear';
		    }

		    $('#<%=txtstartdate.ClientID%>').datetimepicker({
		        format: 'd/m/Y',
		        onShow: function (ct) {
		            maxDate: jQuery('#<%=txtstartdate.ClientID%>').val()

		        },
		        timepicker: false
		    });

		    $('#<%=txtenddate.ClientID%>').datetimepicker({
		        format: 'd/m/Y',
		        onShow: function (ct) {
		            maxDate: jQuery('#<%=txtstartdate.ClientID%>').val()

		        },
		        timepicker: false
		    });
        </script>
	</body>
</HTML>
</asp:Content>
