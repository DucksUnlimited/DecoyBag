<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatEntry005.aspx.vb" Inherits="CatEntry005" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <br />       
            <table style="width: 90%;" cellpadding="5" align=center >
            <tr>
                <td align=center>
                    <img src="Images/spacer.gif" />
                </td>
            </tr>
            <tr>
                <td align=center>
                    <asp:Button ID="btnMessage" runat="server"
                        Visible=true
                        BackColor="#DDDFC3"
                        BorderStyle=Outset
                        BorderColor=Tomato
                        BorderWidth=7px
                        Font-Size=12pt/>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table style="width: 90%;" align=center>
            <tr>
                <td align=center>
                    <img src="Images/spacer.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table style="width: 90%;" cellpadding=5 align=center>
            <tr>
                <td align=center>
                    <asp:Button ID="btnExit" runat="server" Text="Exit Decoy Bag" />
                </td>
            </tr>
            <tr>
                <td align=center>
                    <img src="Images/spacer.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id=Div1 style="height:45px;"></div>

</asp:Content> 

