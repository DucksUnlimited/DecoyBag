<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatEntry004.aspx.vb" Inherits="CatEntry004" title="Order Receipt" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div align=center>
    <table align=center style="width: 80%"> 
        <tr>
            <td align="center" Height=20px colspan="4">
                <asp:Button ID="btnExit" runat="server"  Font-Size="12pt" Text="Order Completed - Exit Catalog Order Entry Here - Thank You." /></td>
        </tr>
    <tr>
         <td colspan=4>                        
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td align=right>
               <asp:Label ID="Label2" runat="server" Text="Ship To:" Font-Size="14pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td align=left style="width: 250px; ">
                <asp:Label ID="LBLshpchapter" runat="server" Font-Size="10pt"></asp:Label></td>
            <td align=right>
               <asp:Label ID="Label3" runat="server" Text="Bill To:" Font-Size="14pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td align=left style="width: 250px; ">
                <asp:Label ID="LBLbillchapter" runat="server" Font-Size="10pt"></asp:Label></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td align=left style="width: 250px">
                <asp:Label ID="LBLcshp1" runat="server" Font-Size="10pt"></asp:Label></td>
            <td>&nbsp;</td>
            <td align=left style="width: 250">
                <asp:Label ID="LBLcsname" runat="server" Font-Size="10pt"></asp:Label></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td align=left style="width: 250px;">
                <asp:Label ID="LBLcshp2" runat="server" Font-Size="10pt"></asp:Label></td>
            <td>&nbsp;</td>
            <td align=left style="width: 250px;">
                <asp:Label ID="LBLcsadd1" runat="server" Font-Size="10pt"></asp:Label></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td align=left style="width: 250px; ">
                <asp:Label ID="LBLcshp3" runat="server" Font-Size="10pt"></asp:Label></td>
             <td>&nbsp;</td>
           <td align=left style="width: 250px; ">
                <asp:Label ID="LBLcsadd2" runat="server" Font-Size="10pt"></asp:Label></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
           <td align=left style="width: 250px;">
                <asp:Label ID="LBLcshp6" runat="server" Font-Size="10pt"></asp:Label>
                <asp:Label ID="LBLcshpza" runat="server" Width="34px" Font-Size="10pt"></asp:Label>
                <asp:Label ID="LBLcshpz2" runat="server" Width="32px" Font-Size="10pt"></asp:Label></td>
            <td>&nbsp;</td>
            <td align=left style="width: 250px;">
                <asp:Label ID="LBLcsctst" runat="server" Font-Size="10pt"></asp:Label>
                <asp:Label ID="LBLcszipa" runat="server" Width="34px" Font-Size="10pt"></asp:Label>
                <asp:Label ID="LBLcszip2" runat="server" Width="32px" Font-Size="10pt"></asp:Label></td>
        </tr>
        <tr height=10px>
            <td colspan=4>&nbsp;</td>
        </tr>
        <tr>
            <td align="right" valign="middle" nowrap="noWrap">
                <asp:Label ID="label18" runat="server" Text="Comments:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td align=left colspan=3>
                <asp:TextBox ID="comments" runat="server" MaxLength="500" ReadOnly=true  
                    rows=9 TextMode=MultiLine Columns=70>
                </asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td align=center colspan=4 style="width:100%; height:18px">
                <div>
                <asp:Panel HorizontalAlign=Center ID="FFLPanel" runat="server" Visible=false Width="968px">
                    <table style="width:100%;" align=center >
                        <tr>
                            <td align=center colspan=2 style="font-size:16pt;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align=center bgcolor=DarkSeaGreen colspan=2 style="font-size:26pt;">FFL Information</td>
                        </tr>
                        <tr style="border-left-color:DarkSeaGreen; border-left-width:thick; border-style:solid; border-right-color:DarkSeaGreen;border-right-width:thick;" >
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
            <td colspan="4" style="width: 100%; height: 20px">
                <div>
                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False" Width="968px" ShowFooter="True">
                        <RowStyle  CssClass="ALT-BROWSE-1" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                        <HeaderStyle  BackColor=LightGreen Font-Bold="False" />
                        <FooterStyle BackColor=LightGreen Font-Bold=False ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="3px" />
                        <AlternatingrowStyle CssClass="ALT-BROWSE-2" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <Columns>
                            <asp:BoundField DataField="ProductID" HeaderText="Item Number" >
                                <HeaderStyle BackColor=LightGreen ForeColor="Black" Font-Size="14pt" HorizontalAlign="Left" Wrap="False" />
                                <ItemStyle Wrap="False" Font-Size="12pt" />
                            </asp:BoundField>
                                    
                            <asp:BoundField DataField="Description" HeaderText="Description" >
                                <HeaderStyle BackColor=LightGreen ForeColor="Black" Font-Size="14pt" HorizontalAlign="Left" Wrap="False" />
                                <ItemStyle Wrap="False" Font-Size="12pt" />
                                <FooterStyle CssClass="FOOTER" font-size="14pt" ForeColor="Black" />
                            </asp:BoundField>
                                    
                            <asp:BoundField DataField="Qty" HeaderText="Quantity Ordered" FooterText="Total" >
                                <HeaderStyle BackColor=LightGreen ForeColor="Black" Font-Size="14pt" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" Font-Size="12pt" />
                                <FooterStyle CssClass="FOOTER" font-size="14pt" ForeColor="Black" />
                            </asp:BoundField>
                                    
                            <asp:BoundField DataField="Cost" HeaderText="Cost" DataFormatString={0:#,##0.00} >
                                <HeaderStyle BackColor=LightGreen ForeColor="Black" Font-Size="14pt" HorizontalAlign="Right" Wrap="False" />
                                <ItemStyle HorizontalAlign="Right" Wrap="False" Font-Size="12pt" />
                                <FooterStyle CssClass="FOOTER" font-size="14pt" ForeColor="Black" />
                            </asp:BoundField>
                                    
                            <asp:BoundField DataField="ExtendedCost" HeaderText="Extended Cost" DataFormatString={0:#,##0.00}>
                                <HeaderStyle BackColor=LightGreen ForeColor="Black" Font-Size="14pt" HorizontalAlign="Right" Wrap="False" />
                                <ItemStyle HorizontalAlign="right" Wrap="False" Font-Size="12pt" />
                                <FooterStyle CssClass="FOOTER" font-size="14pt" />
                            </asp:BoundField>
                                    
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
       
       
    </table>
    <br />
  </div>
</asp:Content>



