<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatEntryLoad.aspx.vb" Inherits="CatEntryLoad" title="Start New Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align=center>
        <br />       
        <table style="width: 90%;" cellpadding="5" align=center >
            <tr>
                <td align=right>
                    <asp:Label ID="label8" runat="server" Text="Event ID"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td align=left width=280px>
                    <asp:TextBox ID="evid" runat="server" MaxLength=8 Width="65px" Text="TN00321L"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="label9" runat="server" Text="Event Date"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td align=left>
                    <asp:TextBox ID="evdate" runat="server" MaxLength=8 Width="65px" Text="20170915"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                     ErrorMessage="Dah..  We need an event date!"
                     ControlToValidate="evdate"
                     SetFocusOnError=true
                     ValidateEmptyText=true>
                    </asp:CustomValidator>                   
                </td>
            </tr>
            <tr>
                <td align=right>
                    <asp:Label ID="label1" runat="server" Text="User ID"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td align=left>
                    <asp:TextBox ID="usrid" runat="server" MaxLength=9 Width="70px" Text="002590116"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align=right nowrap="noWrap">
                    <asp:Label ID="label4" runat="server" Text="Regional Director ID"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td align=left>
                    <asp:TextBox ID="rdid" runat="server" MaxLength=9 Width="70px" Text="001307464"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="middle" style="height: 26px" nowrap="noWrap">
                    <asp:Label ID="label11" runat="server" Text="Coordinator Name"></asp:Label>
                </td>
                <td style="height: 26px">&nbsp;</td>
                <td align=left style="height: 26px">
                    <asp:TextBox ID="corname" runat="server" MaxLength=40 Width="280px" Text="Karen Preston"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="middle" nowrap="noWrap">
                    <asp:Label ID="label12" runat="server" Text="Coordinator eMail"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td align=left>
                    <asp:TextBox ID="coremail" runat="server" MaxLength=100 Width="750px" Text="jjackson@ducks.org"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="middle" nowrap="noWrap">
                    <asp:Label ID="label2" runat="server" Text="RD Number"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td align=left>
                    <asp:TextBox ID="rdnum" runat="server" MaxLength=3 Width="50px" Text="39"></asp:TextBox>
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

