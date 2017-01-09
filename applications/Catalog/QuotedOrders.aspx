<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="QuotedOrders.aspx.vb" Inherits="QuotedOrders" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div>
  <table style="width: 90%; " align=center>
    <tr>
      <td align=center colspan=1 style="font-size:8pt;"></td>
    </tr>
    <tr>
    <td align=center>
    <asp:Label ID="LblCatName" runat="server"></asp:Label>
    </td>
    </tr>
    <tr>
      <td align=center colspan=1 style="font-size:8pt;"></td>
    </tr>
    <tr>
    <td style="height: 79px"> 
    <table style="width: 75%; " align=center>
    <tr>
    <td align=center style="height: 79px">
        <asp:Button ID="ButtonExcel" runat="server" Text="Export to Excel" />
    </td>
    <td align=center style="height: 79px">
    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
    </td>
    </tr>
    </table>
    </td>    
    </tr>
     <tr>
      <td align=center colspan=1 style="font-size:8pt;"></td>
    </tr>
    <tr>
    <td align=center style="height: 148px">
       <asp:Label ID="Label4" runat="server" BackColor="#D6D2E1" Font-Size=18px ForeColor="Red" Text="No Orders Found" Visible="False"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <RowStyle  CssClass="ALT-BROWSE-1" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
        <AlternatingRowStyle  CssClass="ALT-BROWSE-2" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
        <Columns>
            <asp:CommandField HeaderText=" " ShowSelectButton="True"  />
            <asp:BoundField DataField="htnum" HeaderText="Order" />
            <asp:BoundField DataField="htcpo" HeaderText="Event" >
            </asp:BoundField>
            <asp:BoundField DataField="htcodt" HeaderText="Event Date" DataFormatString="{0:####/##/##}" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="htdte" HeaderText="Entered Date" DataFormatString="{0:####/##/##}" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="htcno" HeaderText="Bill-To" />
            <asp:BoundField DataField="htslm1" HeaderText="RD" />
            <asp:BoundField DataField="htorby" HeaderText="Orderd By" />
            <asp:BoundField DataField="htmdm4" HeaderText="Original #" />
           <asp:BoundField DataField="approv" HeaderText="Approved"  >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>

         </Columns>
    </asp:GridView>
    </td>
    </tr>
    </table>
    </div>


</asp:Content>

