<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="List_ExitClickOfferLinkReport.aspx.cs" Inherits="offerlinkmanageradmin.Report.List_ExitClickOfferLinkReport" %>
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
                                                                                <asp:Button ID="btnsearch" class="buttontext" Runat="server" Text="Search"></asp:Button>
                                                                                <input type="button" class="buttontext" value="clear" onclick="Showlist();" />
                                                                           
                                                                                </td>
                                                                                </tr>
                                                                                
																				<tr>
																					<td>
																						<table cellSpacing="1" cellPadding="3" width="100%" align="center" bgColor="#999999" border="0">
																							
																							<tr bgColor="#eeeeee" height="20">
																								
																								<td class="headings" align="left" width ="300px">Hours</td>	
																								<td class="headings" align="left" >Total</td>																																																																																																																  																																														
																								<td class="headings" align="center" >1</td>
																								<td class="headings" align="center" >2</td> 
																								<td class="headings" align="center" >3</td>
																								<td class="headings" align="center" >4</td> 
																								<td class="headings" align="center" >5</td>
																								<td class="headings" align="center" >6</td> 
																								<td class="headings" align="center" >7</td>
																								<td class="headings" align="center" >8</td> 
																								<td class="headings" align="center" >9</td>
																								<td class="headings" align="center" >10</td>
																								<td class="headings" align="center" >11</td> 
																								<td class="headings" align="center" >12</td> 
																								<td class="headings" align="center" >13</td> 
																								<td class="headings" align="center" >14</td> 
																								<td class="headings" align="center" >15</td> 
																								<td class="headings" align="center" >16</td> 
																								<td class="headings" align="center" >17</td> 
																								<td class="headings" align="center" >18</td> 
																								<td class="headings" align="center" >19</td> 
																								<td class="headings" align="center" >20</td> 
																								<td class="headings" align="center" >21</td> 
																								<td class="headings" align="center" >22</td> 
																								<td class="headings" align="center" >23</td> 
																								<td class="headings" align="center" >24</td> 
																								
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

		    

		    function Showlist() {
		        window.location.href = 'List_ExitClickOfferLinkReport.aspx';
		    }

		    $('#<%=txtstartdate.ClientID%>').datetimepicker({
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
