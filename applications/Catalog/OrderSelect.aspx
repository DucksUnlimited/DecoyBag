<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="OrderSelect.aspx.vb" Inherits="OrderSelect" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Get Data" />
        <asp:TextBox ID="lblHtnum" runat="server"></asp:TextBox><br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="HTNUM" DataNavigateUrlFormatString="Orders.aspx?htnum={0}"
                    DataTextField="HTNUM" HeaderText="Order Num" />
                <asp:BoundField DataField="HTDTE" HeaderText="Enerted" />
                <asp:BoundField DataField="HTORSC" HeaderText="WH" />
                <asp:BoundField DataField="HTDLBY" HeaderText="Shipped" />
            </Columns>
        </asp:GridView>
        &nbsp;<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        </div>
</asp:Content>
