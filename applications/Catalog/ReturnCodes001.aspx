<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="ReturnCodes001.aspx.vb" Inherits="ReturnCodes001" title="Untitled Page" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div>
  <table style="width: 90%; " align=center>
   <tr>
    <td align=center colspan=1 style="font-size:8pt; width: 870px;">
    </td>
   </tr>
   <tr>
    <td style="height: 79px; width: 870px;">
     <table style="width: 80%; " align=center>
      <tr>
      <td align=center style="height: 59px">
          <asp:Button ID="ButtonCancel" runat="server" Text="  Cancel    " CausesValidation="False" />
      </td>
      
      <td align=center style="height: 59px">
          <asp:Button ID="ButtonFind" runat="server" Text="  Find Records   " />
      </td>
      <td align=center style="height: 59px">
          <asp:Button ID="ButtonExcel" runat="server" Text="Export to Excel" Enabled="False" Visible="False" />
      </td>
      </tr>
      </table>
      </td>
     </tr>
     <tr>
      <td align=center colspan=1 style="font-size:8pt; height: 25px; width: 870px;"></td>
     </tr>
     <tr>
     <td style="height: 79px; width: 870px;">
         <br />
     <table style="width: 40%; " align=center>
   <tr>
   <td align=right>
   <asp:Label ID="Label2" runat="server" Text="Start Date"></asp:Label>
   </td>
   <td>
   <telerik:RadDatePicker ID="StartDate" runat="server" MaxDate="9999-12-31">
   </telerik:RadDatePicker>
   <br/>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
         ErrorMessage="Must enter start date"
         Display="dynamic"  
         ControlToValidate="StartDate"
         SetFocusOnError=true>
    </asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td align=right>
    <asp:Label ID="Label3" runat="server" Text="Ending Date"></asp:Label>
    </td>
    <td>
    <telerik:RadDatePicker ID="EndDate" runat="server" MaxDate="9999-12-31">
    </telerik:RadDatePicker>
    <br/>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
         ErrorMessage="Must enter ending date"
         Display="dynamic"  
         ControlToValidate="EndDate"
         SetFocusOnError=true>
    </asp:RequiredFieldValidator>
    </td>
    </tr>
    </table>
    </td>
    </tr>
    <tr>
    <td align=center colspan=1 style="font-size:8pt; width: 870px;">
    </td>
    </tr>
   <tr>
   <td align=center>
   <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="No Records Found" Visible="False"></asp:Label>
   </td>
   </tr>
      <tr>
   <td align=center>
   <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="Start Date must be less than End Date" Visible="False"></asp:Label>
   </td>
   </tr>

       <tr>
    <td align=center colspan=1 style="font-size:8pt; width: 870px;">
    </td>
    </tr>

    <tr>
      <td align=center colspan=1 style="font-size:8pt; height: 107px;">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="htlpnm" HeaderText="Item Number" >
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="htldsc" HeaderText="Item Desc." >
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="cs33fo" HeaderText="Return Code" />
            <asp:BoundField DataField="htnum" HeaderText="Order Number" />
            <asp:BoundField DataField="htdte" HeaderText="Entry Date" />
            <asp:BoundField DataField="htcno" HeaderText="Chapter" />
            <asp:BoundField DataField="htorby" HeaderText="Ordered By" >
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="slsnm" HeaderText="Regional Director" >
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
                  <asp:BoundField DataField="mssage" HeaderText="Comment" >
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
      
        </Columns>
    </asp:GridView>
   </td>
   </tr>


  </table>
 </div>
</asp:Content>

