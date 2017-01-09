<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatEntry006.aspx.vb" Inherits="CatEntry006" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align=center>
        <br />       
        <table style="width: 90%;" cellpadding="5" align=center >
        <tr>
            <td  align=center colspan=2>
                <table style="width: 90%;" cellpadding=5 align=center>
                    <tr>
                        <td align=center>
                            <asp:Button ID="btnExit" OnClick="Exit_Pgm" runat="server" Text="Exit Decoy Bag" CausesValidation=false />                
                        </td>
<%--                        <td align=center>
                            <asp:Button ID="NewOrder" runat="server" Text="Create New Order"
                                Enabled=false />
                        </td>--%>
                        <td align=center>
                            <asp:Button ID="OrderSelect" OnClick="Get_Selection" runat="server" Text="Continue"
                                Enabled=false />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height=10px>
            <td colspan=4>&nbsp;</td>
        </tr>
<%--        <tr>
            <td align=center colspan=2>
                <asp:Label ID="NoOrder" runat="server" Text="No Pending Orders Found"
                    Visible=false
                    Font-Size=14pt
                    ForeColor=DarkGreen>
                </asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td align=right>
                <asp:DropDownList ID="PendingOrders" runat="server" >
                </asp:DropDownList>
            </td>
            <td align=left width=50%> 
            </td>
        </tr>
<%--        <tr>
            <td align=center colspan=2>
                <asp:Label ID="NoApproved" runat="server" Text="No Approved Orders Found"
                    Visible=false
                    Font-Size=14pt
                    ForeColor=DarkGreen>
                </asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td align=right>    
                <asp:DropDownList ID="ApprovedOrders" runat="server">
                </asp:DropDownList>
            </td>
            <td align=left width=50%>
            </td>
        </tr>
        <tr>
            <td align=center colspan=2>
                <asp:CustomValidator ID="CVOrderSelect" runat="server" ErrorMessage="Must Select A Pending Order."
                    Display=Dynamic
                    OnServerValidate="MustSelectPendingOrder">
                </asp:CustomValidator>
            </td>
        </tr>
    </table>
</div>
<div align=center>
    <table style="width: 90%;" align=center>
        <tr>
            <td align=center>
                <img src="Images/spacer.gif" />
            </td>
        </tr>
    </table>
</div>
<div>
</div>
<div id=Div1 style="height:45px;"></div>
</asp:Content>

