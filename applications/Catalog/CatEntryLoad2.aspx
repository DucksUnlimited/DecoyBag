<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatEntryLoad2.aspx.vb" Inherits="CatEntryLoad2" title="RD Update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align=center>
        <br />       
        <table style="width: 90%;" cellpadding="5" align=center >
            <tr>
                <td align=right>
                    <asp:Label ID="label1" runat="server" Text="User ID"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td align=left>
                    <asp:TextBox ID="usrid" runat="server" MaxLength=9 Width="70px" Text="000038192"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align=right nowrap="noWrap">
                    <asp:Label ID="label4" runat="server" Text="Regional Director ID" Font-Size=10pt></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td align=left width=680px>
                    <asp:TextBox ID="rdid" runat="server" MaxLength=9 Width="70px" Text="000038192"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="middle" nowrap="noWrap">
                    <asp:Label ID="label2" runat="server" Text="RD Number" Font-Size=10pt></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td align=left>
                    <asp:TextBox ID="rdnum" runat="server" MaxLength=3 Width="50px" Text="27"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="SPACER"></div>
    <br /><br />
    <div align=center>
        <ASP:BUTTON id="btnCancel" runat="server" text="Cancel"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <ASP:BUTTON id="btnSubmit" runat="server" text="Start Decoy Bag"  />
    </div>
    <div class="SPACER"></div>

</asp:Content>

