<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="ML002.aspx.vb" Inherits="ML002" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align=center>
        <table width=40%>
            <tr>
                <td align=right>
                    <asp:Label ID="Label2" runat="server" Text="Mailing File"></asp:Label>
                </td>
                <td align=left>
                    <asp:DropDownList ID="FileList" runat="server"></asp:DropDownList>                                
                </td>
            </tr>
            <tr>
                <td align=right>
                    <asp:Label ID="Label1" runat="server" Text="News"></asp:Label>                                
                </td>
                <td align=left>
                    <asp:CheckBox ID="News" runat="server" />                                
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div align=center>
    <asp:Button ID="Button1" runat="server" Text="Call ML001" />                               
    </div>

</asp:Content>
