<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CatEntryPDF2.aspx.vb" Inherits="CatEntryPDF2" title="PDF Receipt" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <META http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
    <META name="GENERATOR" content="IBM Software Development Platform">
    <META http-equiv="Content-Style-Type" content="text/css">

    <link rel="stylesheet" href="99723933.CSS" type="text/css"/>
    
    <style type="text/css">
      .validatorCalloutHighlight
      {
         background-color: lemonchiffon;
      }
      .group
      {
         background-color: DarkSeaGreen;
         font-size: 26pt;
      } 
      #TIMESTAMPP                    
      {
         float: right;
         font-size: 8pt;
      }
    </style>
    
    <title>Ducks Unlimited</title>
</head>
<body leftmargin="0" topmargin="0">
    <div id="CONTAINER">
        <div id="HEADER">
          <div style="position:relative; float: left;"><img src="Images/99725393.JPG"  align=middle border=0></div>
          <div id="SYSTEM_INFO" align="right">
            <div align="center" style="color:DarkGreen; width:auto; font-size:18pt">
                <table width=65%>
                    <tr>
                        <td align=left>
                           <asp:Label ID="UpdateOrd" runat="server"></asp:Label>
                        </td>
                        <td align=right>
                            <asp:Label ID="SystemTitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div align=center style="width:65%; float:left; font-size:16pt; font-weight:bold;">
                <asp:Label ID="EventTitle" runat="server" ></asp:Label>
            </div>
            <div  align="center" style="width:65%; float:left; font-size:14pt; font-weight:bold;">
                <asp:Label ID="PageTitle" runat="server" ></asp:Label>
            </div>
          </div>
        </div>
        <div id="APPLICATION">
            <form id="form1" runat="server">
                <br />
                <br />
                <table width=98%>
                    <tr>
                        <td align=center colspan=4>
                            <asp:textbox ID="tbPending" runat="server"
                                BackColor="#ccffcc"
                                Visible=false
                                text="Order Pending RD Approval."
                                Font-Size=18pt
                                Width=31.5%
                                 
                             />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align=center colspan=4 style="font-size:18pt;">
                            <asp:CheckBox ID="cbApprove" runat="server"
                                Checked=false
                                BackColor="#ccffcc"
                                Visible=false
                                text="Order has been Approved for Shipment!"
                                TextAlign=Right
                             />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align=center colspan=4 style="font-size:10pt;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align=center>
                            <asp:Label ID="Label1" runat="server" Text="Ship To:" Font-Size="14pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LBLshpchapter" runat="server" Font-Size="12pt"></asp:Label></td>
                        <td align=center>
                            <asp:Label ID="LBLBillto" runat="server" Text="Bill To:" Font-Size="14pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LBLbillchapter" runat="server" Font-Size="12pt"></asp:Label></td>
                    </tr> 
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="LBLosname" runat="server"  Font-Size="12pt"></asp:Label></td>
                        <td></td>
                        <td>
                            <asp:Label ID="LBLobname" runat="server" Font-Size="12pt"></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="LBLosadd1" runat="server" Font-Size="12pt"></asp:Label></td>
                        <td></td>
                        <td>
                            <asp:Label ID="LBLobadd1" runat="server" Font-Size="12pt"></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="LBLosadd2" runat="server" Font-Size="12pt"></asp:Label></td>
                        <td></td>
                        <td>
                            <asp:Label ID="LBLobadd2" runat="server" Font-Size="12pt"></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="LBLoscsz" runat="server" Font-Size="12pt"></asp:Label>&nbsp;
                        <td></td>
                        <td>
                            <asp:Label ID="LBLobcsz" runat="server" Font-Size="12pt"></asp:Label>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td align=center colspan=4 style="width: 100%; height: 18px">
                            <div>
	                        <asp:Panel HorizontalAlign=Center ID="FFLPanel" runat="server" Visible=false Width="968px">
	                            <table style="width:100%;" align=center>
	                                <tr>
		                                <td align=center colspan=2 style="font-size:16pt;">&nbsp;</td>
	                                </tr>
	                                <tr>
		                                <td align=center bgcolor=DarkSeaGreen colspan=2 style="font-size:26pt;">FFL Information</td>
	                                </tr>
	                                <tr style="border-style:solid;border-color:DarkSeaGreen;border-width:thick;">
		                                <td width=40%>
		                                </td>
		                                <td align=left>
			                                <asp:Label ID="fflorg" runat="server" Font-Size="14pt"></asp:Label>
		                                </td>
	                                </tr>
	                                <tr>
		                                <td width=40%>
		                                </td>
		                                <td align=left>
			                                <asp:Label ID="fflattn" runat="server" Font-Size="14pt"></asp:Label>
		                                </td>
	                                </tr>
	                                <tr>
		                                <td width=40%>
		                                </td>
		                                <td align=left>
			                                <asp:Label ID="ffladdr" runat="server" Font-Size="14pt"></asp:Label>
		                                </td>
	                                </tr>
	                                <tr>
		                                <td width=40%>
		                                </td>
		                                <td align=left>
			                                <asp:Label ID="fflcsz" runat="server" Font-Size="14pt"></asp:Label>
		                                </td>
	                                </tr>
	                                <tr>
		                                <td width=40%>
		                                </td>
	                                   <td align=left>
	                                    <table>
	                                        <tr>
	                                            <td>
        			                                <asp:Label ID="fflnum" runat="server" Font-Size="14pt"></asp:Label>
	                                            </td>
	                                            <td>
		        	                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                                            </td>
	                                            <td>
                                                    <asp:Label ID="fflexp" runat="server" Font-Size="14pt"></asp:Label>
                                                </td>
	                                        </tr>
	                                     </table>
                                        </td>
	                                </tr>
	                                <tr>
            		                    <td align=center bgcolor=DarkSeaGreen colspan=2 style="font-size:14pt;">Please note: FFL date must not expire prior to order date.<br />Please Send Signed Federal Firearms License. </td>
	                                </tr>
	                                <tr>
		                                <td align=center colspan=2 style="font-size:16pt;">&nbsp;</td>
	                                </tr>
	                            </table>
	                        </asp:Panel>
	                        </div>
	                    </td>
	                </tr>
                    <tr>
                        <td colspan="4" style="width: 100%; height: 18px">
                            <div>
                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False" Width="968px" ShowFooter="True">
                                <RowStyle  CssClass="ALT-BROWSE-1" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle  BackColor=LightGreen Font-Bold="False" />
                                <FooterStyle BackColor=LightGreen Font-Bold=False ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="3px" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingrowStyle CssClass="ALT-BROWSE-2" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <Columns>
                                    <asp:BoundField DataField="htlpnm" HeaderText="Item Number" >
                                        <HeaderStyle CssClass="FLOAT" Font-Size="14pt" HorizontalAlign="Left" Wrap="False" />
                                        <ItemStyle Wrap="False" Font-Size="12pt" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="htldsc" HeaderText="Description" >
                                        <HeaderStyle CssClass="FLOAT" Font-Size="14pt" HorizontalAlign="Left" Wrap="False" />
                                        <ItemStyle Wrap="False" Font-Size="12pt" />
                                        <FooterStyle CssClass="FOOTER" font-size="14pt" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="htqor" HeaderText="Quantity Ordered" FooterText="Total" >
                                        <HeaderStyle CssClass="FLOAT" Font-Size="14pt" HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle HorizontalAlign=Center Wrap="False" Font-Size="12pt" />
                                        <FooterStyle CssClass="FOOTER" font-size="14pt" />
                                   </asp:BoundField>
                                    
                                    <asp:BoundField DataField="htluct" HeaderText="Cost" DataFormatString={0:#,##0.00} >
                                        <HeaderStyle CssClass="FLOAT" Font-Size="14pt" HorizontalAlign="Right" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" Wrap="False" Font-Size="12pt" />
                                        <FooterStyle CssClass="FOOTER" font-size="14pt" ForeColor="Black" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="cs33ec" HeaderText="Extended Cost" DataFormatString={0:#,##0.00}>
                                        <HeaderStyle CssClass="FLOAT" Font-Size="14pt" HorizontalAlign="Right" Wrap="False" />
                                        <ItemStyle HorizontalAlign="right" Wrap="False" Font-Size="12pt" />
                                        <FooterStyle CssClass="FOOTER" font-size="14pt" />
                                   </asp:BoundField>
                                    
                                </Columns>
                            </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
   </div>
            
</body>
</html>
