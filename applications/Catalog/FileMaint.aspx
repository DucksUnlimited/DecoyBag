<%@ Page Language="VB" AutoEventWireup="true" CodeFile="FileMaint.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Enter customer, name or blank for all" style="z-index: 100; left: 8px; position: absolute; top: 24px"></asp:Label>
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Style="z-index: 107; left: 8px;
            position: absolute; top: 80px" Width="576px"></asp:Label>
        <br />
        <asp:Button ID="ButtonNew" runat="server" Text="New Cust" 
            style="z-index: 104; left: 464px; position: absolute; top: 48px; width: 83px;" />
        <br />
        <asp:TextBox ID="txtCusnum" runat="server" style="z-index: 102; left: 8px; position: absolute; top: 48px" Width="100px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Find" style="z-index: 103; left: 344px; position: absolute; top: 48px" Width="50px" />
        <asp:Button ID="ButtonReset" runat="server" Text="Reset" 
            style="z-index: 104; left: 404px; position: absolute; top: 48px" Width="50px" /><br />
    
    </div>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None" 
        style="z-index: 105; left: 8px; position: absolute; top: 104px" 
        AutoGenerateSelectButton="True">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:TextBox ID="txtName" runat="server" Style="z-index: 106; left: 8px; position: absolute;
            top: 48px" Width="200px"></asp:TextBox>
    </form>
</body>
</html>
