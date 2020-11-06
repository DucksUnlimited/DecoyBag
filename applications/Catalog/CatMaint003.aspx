<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatMaint003.aspx.vb" Inherits="CatMaint003" title="Catalog Maintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div>
  <table style="width: 90%; " align="center">
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
    <td style="height: 79px"> 
    <table style="width: 90%; " align="center">
    <tr>
    <td align="center" style="height: 79px">
        <asp:Button ID="ButtonNew" runat="server" Text="Add New Item" />
    </td>
    <td align="center" style="height: 79px">
        <asp:Button ID="Excel" runat="server" Text="Export to Excel" />
    </td>
    <td align="center" style="height: 79px">
        <asp:Button ID="ButtonExpired" runat="server" Text="Show Expired Items" />
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
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <RowStyle  CssClass="ALT-BROWSE-1" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
        <AlternatingRowStyle  CssClass="ALT-BROWSE-2" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
        <Columns>
            <asp:CommandField HeaderText=" " ShowSelectButton="True"  />
            <asp:TemplateField HeaderText="Image" ShowHeader="false">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" width="30" Height="30" ImageAlign="Middle" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="avalqty" HeaderText="Avail. Qty" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ccnumb" HeaderText="Item" />
            <asp:BoundField DataField="cciseq" HeaderText="Sequence" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="cceffl" HeaderText="Effective Date" DataFormatString="{0:####/##/##}" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ccexpl" HeaderText="Expired Date" DataFormatString="{0:####/##/##}" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="catdesc" HeaderText="Group" />
            <asp:BoundField DataField="itmdsc" HeaderText="Item Desc." />
                   </Columns>
    </asp:GridView>
    </td>
    </tr>
    </table>
    </div>
</asp:Content>

