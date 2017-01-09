<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="OrdersTest.aspx.vb" Inherits="OrdersTest" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table style="width: 900px">
    <tr>
      <td style="width: 135px;" align="right">
            <asp:Label ID="Label1" runat="server" Text="Order Number" Width="104px"></asp:Label></td>
      <td style="width: 154px;" align="left">
            <asp:Label ID="lblhtnum" runat="server" Width="104px" ForeColor="DimGray"></asp:Label></td>
      <td style="width: 157px;"  align="left">
            Ship To</td>
      <td style="width: 158px;" align="left">
            <asp:Label ID="lblhtsno" runat="server" ForeColor="DimGray" Width="128px"></asp:Label></td>
      <td style="width: 118px;" align="left">
            Bill To</td>
      <td style="width: 157px;" align="left">
            <asp:Label ID="lblhtcno" runat="server" ForeColor="DimGray" Width="112px"></asp:Label></td>
    </tr>
    <tr>
      <td style="width: 135px;" align="right">
            Original Order</td>
      <td style="width: 154px;" align="left">
            <asp:Label ID="lblhtmdm4" runat="server" ForeColor="DimGray"></asp:Label></td>
      <td colspan="2" style="width: 157px;" align="left">
            <asp:Label ID="lblhtsnam" runat="server" ForeColor="DimGray" Width="304px"></asp:Label></td>
      <td colspan="2" style="width: 152px;" align="left">
            <asp:Label ID="lblhtbnam" runat="server" ForeColor="DimGray" Width="248px"></asp:Label></td>
    </tr>
    <tr>
      <td style="width: 135px" align="right">
            Sale Branch</td>
      <td style="width: 154px" align="left">
            <asp:Label ID="lblcs21sb" runat="server" ForeColor="DimGray"></asp:Label></td>
      <td colspan="2" style="width: 157px" align="left">
            <asp:Label ID="lblhtsad1" runat="server" ForeColor="DimGray" Width="304px"></asp:Label></td>
      <td colspan="2" style="width: 152px" align="left">
            <asp:Label ID="lblhtbad1" runat="server" ForeColor="DimGray" Width="248px"></asp:Label></td>
    </tr>
    <tr>
      <td style="width: 135px" align="right">
            Warehouse</td>
      <td style="width: 154px" align="left">
            <asp:Label ID="lblhtorsc" runat="server" ForeColor="DimGray"></asp:Label></td>
      <td colspan="2" style="width: 157px" align="left">
            <asp:Label ID="lblhtsad2" runat="server" ForeColor="DimGray" Width="304px"></asp:Label></td>
      <td colspan="2" style="width: 152px" align="left">
            <asp:Label ID="lblhtbad2" runat="server" ForeColor="DimGray" Width="248px"></asp:Label></td>
    </tr>
    <tr>
      <td style="width: 135px; height: 26px;" align="right">
            Entry</td>
      <td style="width: 154px; height: 26px;" align="left">
            <asp:Label ID="lblhtdte" runat="server" ForeColor="DimGray"></asp:Label></td>
      <td colspan="" style="width: 157px; height: 26px;" align="left">
            <asp:Label ID="lblhtscty" runat="server" Width="144px" ForeColor="DimGray"></asp:Label></td>
      <td colspan="" style="width: 157px; height: 26px;" align="left">
            <asp:Label ID="lblhtszip" runat="server" Width="48px" ForeColor="DimGray"></asp:Label>
            -
            <asp:Label ID="lblhtszp2" runat="server" Width="40px" ForeColor="DimGray"></asp:Label></td>
      <td colspan="" style="width: 118px; height: 26px;" align="left">
            <asp:Label ID="lblhtbcty" runat="server" Width="112px" ForeColor="DimGray"></asp:Label></td>
      <td colspan="" style="width: 152px; height: 26px;" align="left">
            <asp:Label ID="lblhtbzip" runat="server" Width="48px" ForeColor="DimGray"></asp:Label>
            -
            <asp:Label ID="lblhtbzp2" runat="server" Width="40px" ForeColor="DimGray"></asp:Label></td>
    </tr>
  </table>
  <table style="width: 900px">
    <tr>
      <td style="width: 152px; height: 21px">
            Customer P.O. Number</td>
      <td style="width: 580px; height: 21px" align="left">
            <asp:Label ID="lblhtcpo" runat="server" Width="552px" ForeColor="DimGray"></asp:Label></td>
      <td style="height: 21px" align="left">
            <asp:Label ID="lblhtcodt" runat="server" ForeColor="DimGray"></asp:Label></td>
    </tr>
  </table>
  <br />
  <br />
  <div id="FLOAT">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="98%" CellSpacing="1" GridLines="None">
      <Columns>
        <asp:BoundField DataField="htllin" HeaderText="Line" >
            <ItemStyle HorizontalAlign="Right" />
            <HeaderStyle CssClass="FLOAT" />
        </asp:BoundField>
        <asp:BoundField DataField="htqor" HeaderText="Order" >
            <ItemStyle HorizontalAlign="Right" />
            <HeaderStyle CssClass="FLOAT" />
        </asp:BoundField>
        <asp:BoundField DataField="htlqsh" HeaderText="Ship" >
            <ItemStyle HorizontalAlign="Right" />
            <HeaderStyle CssClass="FLOAT" />
        </asp:BoundField>
        <asp:BoundField DataField="htlqbo" HeaderText="Bk Ord" >
            <ItemStyle HorizontalAlign="Right" />
            <HeaderStyle CssClass="FLOAT" />
        </asp:BoundField>
        <asp:BoundField DataField="htlun" HeaderText="UoM" >
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle CssClass="FLOAT" />
        </asp:BoundField>
        <asp:BoundField DataField="htlpnm" HeaderText="Item #" >
            <HeaderStyle CssClass="FLOAT" />
        </asp:BoundField>
        <asp:BoundField DataField="htldsc" HeaderText="Description" >
            <HeaderStyle CssClass="FLOAT" />
        </asp:BoundField>
        <asp:BoundField DataField="htlupr" HeaderText="Unit Price" >
            <ItemStyle HorizontalAlign="Right" />
            <HeaderStyle CssClass="FLOAT" />
        </asp:BoundField>
      </Columns>
    <HeaderStyle BackColor="LightSkyBlue" HorizontalAlign="Center" VerticalAlign="Bottom" CssClass="'display: table-header-group'" />
    <AlternatingRowStyle CssClass="ALT-BROWSE-2" />
    <RowStyle CssClass="ALT-BROWSE-1" />
    </asp:GridView>

  </div>
</asp:Content>

