<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="EventInvSum.aspx.vb" Inherits="EventInvSum" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div>
  <table style="width: 90%; " align=center>
    <tr>
      <td align=center colspan=1 style="font-size:8pt; height: 124px;">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="witem" HeaderText="Item #" />
            <asp:BoundField DataField="wdesc" HeaderText="Description" />
            <asp:BoundField DataField="wcqty" HeaderText="Contract Qty" />
            <asp:BoundField DataField="wcost" HeaderText="Cost" />
            <asp:BoundField DataField="wavail" HeaderText="Avail at  DU">
                <HeaderStyle VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="duamt" HeaderText="Dollars at DU" />
            <asp:BoundField DataField="wosqty" HeaderText="Lan Inv." />
            <asp:BoundField DataField="laamt" HeaderText="Dollars at Lanigans" />
            <asp:BoundField DataField="wrdqty" HeaderText="RD Inv." />
            <asp:BoundField DataField="rdamt" HeaderText="Dollars In RD Inv" />
            <asp:BoundField DataField="totqty" HeaderText="Tot. Inv." />
            <asp:BoundField DataField="totamt" HeaderText="Tot.Cost Ext. " />
            <asp:BoundField DataField="wavg" HeaderText="Avg Amt Recvd" />
            <asp:BoundField DataField="wcbr" HeaderText="CBR" />
            <asp:BoundField DataField="wrpt" HeaderText="#Reporting" />
                     
        </Columns>
    </asp:GridView>
   </td>
   </tr>
   </table>
   </div>
</asp:Content>

