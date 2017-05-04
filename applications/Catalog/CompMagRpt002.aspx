<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CompMagRpt002.aspx.vb" Inherits="CompMagRpt002" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div align="center">
   <table style="width: 80%; " align="center">
     <tr>
      <td align="center" colspan="4" style="height: 79px;">
      <table style="width: 80%; " align="center">
      <tr>
      <td align="center" style="height: 59px">
        <asp:Button ID="ButtonCancel" runat="server" Text="      Cancel     " CausesValidation="False" />
      </td>
      </tr>
      </table>
      </td>
     </tr>
     <tr>
     <td colspan="4" style="height: 79px;">
       <table style="width: 40%; " align="center" bgcolor="#d6d2e1">
       <tr>
       <td align="right">
       <asp:Label ID="Label1" runat="server" Text="Last Send YYMMDD"></asp:Label>
       </td>
       <td>
       <asp:TextBox ID="TxtSendDate" runat="server" Width="116px" MaxLength="15" ></asp:TextBox>
          <br/>

       </td>
       </tr>
       <tr>
       <td align="right">
       <asp:Label ID="Label2" runat="server" Text="Last Send Time"></asp:Label>
       </td>
       <td>
       <asp:TextBox ID="TxtSendTime" runat="server" MaxLength="20" Width="116px"></asp:TextBox>
          <br/>

       </td>
       </tr>
       </table>
   </td>
   </tr>
    
<tr>
<td align="center" colspan="4" style="height: 148px;">

    <asp:GridView ID="GridView1" ForeColor="Black" BackColor="#D6D2E1" runat="server" AutoGenerateColumns="False" Width="70%" >
        <Columns>
            <asp:BoundField DataField="srclin" HeaderText="Comp Mag Log" >
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    </td>
    </tr>
      </table>
    </div>


</asp:Content>

