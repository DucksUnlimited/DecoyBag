<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatGroup002.aspx.vb" Inherits="CatGroup002" title="Group Maintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align=center>
    <table style="width: 40%; " align=center>
    <tr>
    <td align=center colspan=2 style="font-size:8pt;"></td>
    </tr>
    <tr>
    <td align=center>
    <asp:Button ID="ButtonSave" runat="server" Text="Save Group" />
    </td>
    <td align=center>
    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CausesValidation=false />
    </td>
    </tr>
    <tr>
    <td align=left colspan=2 style="font-size:8pt;">
    <asp:Label ID="LblError" runat="server" ForeColor="Red" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td align=right>
    <asp:Label ID="Label2" runat="server" Text="Group"></asp:Label>
    </td>
    <td align=left>
    <asp:TextBox ID="TxtGroup" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align=right>
    <asp:Label ID="Label3" runat="server" Text="Description"></asp:Label>
    </td>
    <td align=left>
    <asp:TextBox ID="TxtDesc" runat="server"></asp:TextBox>
    <br/>
    <asp:RequiredFieldValidator ID="RFVTxtDesc" runat="server" 
         ErrorMessage="Please enter description."
         ControlToValidate="TxtDesc"
         display=Dynamic
         SetFocusOnError=true>
    </asp:RequiredFieldValidator>
    </td>
    <tr>
    <td align=right>
    <asp:Label ID="Label4" runat="server" Text="Type"></asp:Label>
    </td>
    <td align=left>
    <asp:DropDownList ID="DropDownType" runat="server">
        <asp:ListItem Value="  "></asp:ListItem>
        <asp:ListItem Value="SP"></asp:ListItem>
        <asp:ListItem Value="RD"></asp:ListItem>
        <asp:ListItem Value="XX"></asp:ListItem>
    </asp:DropDownList>
    </td>
    </tr>
    </table>
   </div>
</asp:Content>

