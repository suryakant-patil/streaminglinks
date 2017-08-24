<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListOfferLinkHistory.aspx.cs" Inherits="offerlinkmanageradmin.OfferLink.ListOfferLinkHistory" %>
<HTML>
	<HEAD>
		<title>Promotional Link History List</title>
		<LINK href="../css/admincp.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" bgColor="#eeeeee" leftMargin="0" topMargin="0" rightMargin="0" style="background: none;">
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
																					<td>
																						<table cellSpacing="1" cellPadding="3" width="100%" align="center" bgColor="#999999" border="0">
																							
																							<tr bgColor="#eeeeee" height="20">
																								<td class="headings" align="center" width ="20px"></td>
																								<td class="headings" align="left" width ="100px">Link Name</td>
																								<td class="headings" align="left" width ="200px">Old Link</td>	
																								<td class="headings" align="left" width ="200px">New Link</td>			
																								<td class="headings" align="left" width ="100px">Action</td>	
																								<td class="headings" align="left" width ="200px">Action By</td>	
																								<td class="headings" align="left" width ="200px">Action Date</td>	
																								
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
		
	</body>
</HTML>
