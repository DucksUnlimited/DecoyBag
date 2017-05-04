<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CompMagRpt001.aspx.vb" Inherits="CompMagRpt001" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div align="center">
    <table style="width: 30%; " align="center">
        <tr>
            <td align="center" colspan="1" style="font-size:8pt;"></td>
        </tr>
        <tr>
        <td align="center">
        <asp:Label ID="LblCatName" runat="server"></asp:Label>
        </td>
        </tr>
        <tr>
            <td align="center" colspan="1" style="font-size:8pt;"></td>
        </tr>
        <tr>
            <td align="center" style="height: 54px"> 
                <table style="width: 75%; " align="center">
                    <tr>
                        <td align="center" style="height: 79px">
                            <asp:Button ID="ButtonExcel" runat="server" Text="Export to Excel" />
                        </td>
                        <td align="center" style="height: 79px">
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
            <td align="center" style="height: 148px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True">
                    <Columns>
                        <asp:BoundField DataField="cpstatm" HeaderText="State" >
                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                            <FooterStyle CssClass="FOOTER" />
                        </asp:BoundField>
                        <asp:BoundField DataField="names" HeaderText="Total"  >
                            <ItemStyle HorizontalAlign="Right" Width="20%" />
                            <FooterStyle CssClass="FOOTER" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
  </div>
</asp:Content>
