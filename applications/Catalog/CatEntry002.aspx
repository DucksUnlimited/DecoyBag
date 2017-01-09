<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatEntry002.aspx.vb" Inherits="CatEntry002" title="Selected Items" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <asp:Panel ID="Panel" runat="server" DefaultButton="btnEdit">

  <div>
    <table align="center" style="width: 100%; height: 200px" cellpadding=5>
      <tr>
        <td align=center>
          <ASP:BUTTON id="btnExit" runat="server" text=" Exit Decoy Bag " />
        </td>
        <td align=center>
          <ASP:BUTTON id="btnCancelPreview" runat="server" text=" Cancel Order " /> 
        </td>
        <td align=center>
          <ASP:BUTTON id="btnEdit" runat="server" text=" Return to Order Form " />
        </td>
        <td align=center>
            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Images/decoybag.jpg" AlternateText="Complete Order" /></td>
      </tr>
      <tr>
        <td align="center" colspan=4 style="height: 155px">
          <div>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False" Width="968px" ShowFooter="True">
                <RowStyle  CssClass="ALT-BROWSE-1" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                <HeaderStyle BackColor=LightGreen Font-Bold="False" ForeColor="Black" />
                <FooterStyle BackColor=LightGreen Font-Bold=False ForeColor="Black" />
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
                        <FooterStyle CssClass="FOOTER" font-size="14pt" />
                    </asp:BoundField>
                                    
                    <asp:BoundField DataField="Qty" HeaderText="Quantity Ordered" FooterText="Total" >
                        <HeaderStyle BackColor=LightGreen ForeColor="Black" Font-Size="14pt" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign=Center Wrap="False" Font-Size="12pt" />
                        <FooterStyle CssClass="FOOTER" font-size="14pt" />
                    </asp:BoundField>
                                    
                    <asp:BoundField DataField="Cost" HeaderText="Cost" DataFormatString={0:#,##0.00} >
                        <HeaderStyle BackColor=LightGreen ForeColor="Black" Font-Size="14pt" HorizontalAlign="Right" Wrap="False" />
                        <ItemStyle HorizontalAlign="Right" Wrap="False" Font-Size="12pt" />
                        <FooterStyle CssClass="FOOTER" font-size="14pt" ForeColor="Black" />
                    </asp:BoundField>
                                    
                    <asp:BoundField DataField="ExtendedCost" HeaderText="Extended Cost" DataFormatString={0:#,##0.00}>
                        <HeaderStyle BackColor=LightGreen ForeColor="Black" Font-Size="14pt" HorizontalAlign="Right" Wrap="False" />
                        <ItemStyle HorizontalAlign=Right Wrap="False" Font-Size="12pt" />
                        <FooterStyle CssClass="FOOTER" font-size="14pt" />
                    </asp:BoundField>
                                    
                </Columns>
            </asp:GridView>
          </div>
        </td>
      </tr>
    </table>
  </div>
  </asp:Panel>
</asp:Content>

