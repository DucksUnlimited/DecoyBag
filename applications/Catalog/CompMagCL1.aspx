<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CompMagCL1.aspx.vb" Inherits="CompMagCL1" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div>
  <table style="width: 90%; " align="center">
    <tr>
      <td align="center" colspan="1" style="font-size:8pt;"></td>
    </tr>
    <tr>
      <td align="center" colspan="1" style="font-size:8pt;"></td>
    </tr>
    <tr>
    <td style="height: 53px"> 
    <table style="width: 75%; " align="center">
    <tr>
    <td align="center" style="height: 79px">
        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit File Transfer" />
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
            <td align="center" colspan="2">
                <asp:Label ID="Label1" runat="server" Text="The Job has been Submitted"
                    Visible="false"
                    Font-Size="14pt"
                    ForeColor="DarkGreen">
                </asp:Label>
            </td>
        </tr>
                    <tr>
            <td align="center" colspan="2">
                <asp:Label ID="Label7" runat="server" Text="The Job was not Submitted"
                    Visible="false"
                    Font-Size="14pt"
                    ForeColor="DarkGreen">
                </asp:Label>
            </td>
        </tr>

            <tr>
      <td align="center" colspan="1" style="font-size:8pt;"></td>
    </tr>
            <tr>
            <td align="center" colspan="2">
                <asp:Label ID="Label2" runat="server" Text="Wait at least fifteen minutes."
                    Visible="false"
                    Font-Size="14pt"
                    ForeColor="DarkGreen">
                </asp:Label>
            </td>
        </tr>
            <tr>
            <td align="center" colspan="2">
                <asp:Label ID="Label3" runat="server" Text="Then check the last send info option."
                    Visible="false"
                    Font-Size="14pt"
                    ForeColor="DarkGreen">
                </asp:Label>
            </td>
        </tr>
            <tr>
            <td align="center" colspan="2">
                <asp:Label ID="Label4" runat="server" Text="If it has not changed to today's date,"
                    Visible="false"
                    Font-Size="14pt"
                    ForeColor="DarkGreen">
                </asp:Label>
            </td>
        </tr>
            <tr>
            <td align="center" colspan="2">
                <asp:Label ID="Label5" runat="server" Text="please call a programmer."
                    Visible="false"
                    Font-Size="14pt"
                    ForeColor="DarkGreen">
                </asp:Label>
            </td>
        </tr>
            <tr>
            <td align="center" colspan="2">
                <asp:Label ID="Label6" runat="server" Text="Do not submit this job again."
                    Visible="false"
                    Font-Size="14pt"
                    ForeColor="DarkGreen">
                </asp:Label>
            </td>
        </tr>

    </table>
    </div>

</asp:Content>

