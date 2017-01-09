<%@ Page Language="VB" AutoEventWireup="true" CodeFile="EditCust.aspx.vb" Inherits="EditCust" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 118px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
  
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Customer</td>
                <td>
                    <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Last Name</td>
                <td>
                    <asp:TextBox ID="txtLastname" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Init</td>
                <td>
                    <asp:TextBox ID="txtInit" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Street</td>
                <td>
                    <asp:TextBox ID="txtStreet" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    City
                </td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    State
                </td>
                <td>
                    <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Zipcode</td>
                <td>
                    <asp:TextBox ID="txtZipcode" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="ButtonUpdate" runat="server" Text="Save" Width="61px" />
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                    <asp:Button ID="ButtonDelete" runat="server" Height="26px" Text="Delete" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
  
    </div>
    </form>
</body>
</html>
