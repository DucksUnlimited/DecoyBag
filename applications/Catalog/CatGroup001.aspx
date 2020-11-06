<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatGroup001.aspx.vb" Inherits="CatGroup001" title="Group Maintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div align="center">
    <table style="width: 80%; " align="center">
    <tr>
    <td align="center" colspan="1" style="font-size:8pt;"></td>
    </tr>
    <tr>
    <td style="height: 79px">
    <table style="width: 80%; " align="center">
    <tr>
    <td align="center">
    <asp:Button ID="ButtonNew" runat="server" Text="Add New Group" />
    </td>
    <td align="center">
    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
    </td>
    </tr>
    </table>
    </td>
    </tr>
    <tr>
    <td align="center" colspan="1" style="font-size:8pt;"></td>
    </tr>
    <tr>
    <td align="center">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True" AutoGenerateColumns="False">
        <RowStyle  CssClass="ALT-BROWSE-1" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
        <AlternatingRowStyle  CssClass="ALT-BROWSE-2" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
        <Columns>
            <asp:BoundField DataField="catgrp" HeaderText="Group Number" />
            <asp:BoundField DataField="catdesc" HeaderText="Description" />
            <asp:BoundField DataField="cattype" HeaderText="Type" />
        </Columns>
    </asp:GridView>
    </td>
    </tr>
    </table>
    </div>
</asp:Content>

