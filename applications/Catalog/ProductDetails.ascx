<%@ Control Language="VB" AutoEventWireup="true" CodeFile="ProductDetails.ascx.vb" Inherits="ProductDetails" %>
 
<div style="border: 2px solid #999999; margin: 3px;">
<table runat="server" style="width: 100%" id="ProductWrapper" border="0" cellpadding="2"
    cellspacing="0">
    <tr>
        <td style="text-align: center;">
            <asp:FormView ID="ProductsView" runat="server" 
                               OnDataBound="ProductsView_DataBound">
                <ItemTemplate>
                    <h3>
                        <asp:Label ID="itmdesc" runat="server"></asp:Label>
                    </h3>
                    <br />
                    <asp:Image ID="itmimage" runat="server" />
                    <br />
                    <asp:Literal ID="ttdetail" runat="server"  />
                </ItemTemplate>
            </asp:FormView>
        </td>
    </tr>
</table>
</div>