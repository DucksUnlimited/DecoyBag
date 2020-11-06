<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatMaint002.aspx.vb" Inherits="CatMaint002" title="Catalog Maintenance" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <table style="width: 40%; " align="center">
   <tr>
   <td align="center" colspan="2" style="font-size:8pt;"></td>
   </tr>
   <tr>
   <td align="center">
   <asp:Button ID="ButtonSave" runat="server" Text="Save/View Items" />
   </td>
   <td align="center">
   <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CausesValidation="false"  />
   </td>
   </tr>
   <tr>
   <td align="left" colspan="2" style="font-size:8pt;">
   <asp:Label ID="LblError" runat="server" ForeColor="Red" Text=""></asp:Label>&nbsp;</td>
   </tr>
   <tr>
   <td align="right">
   <asp:Label ID="Label1" runat="server" Text="Catalog Name"></asp:Label>
   </td>
   <td>
   <asp:TextBox ID="TxtCatName" runat="server" ></asp:TextBox>
   <br/>
   <asp:RequiredFieldValidator ID="RFVCatName" runat="server" 
         ErrorMessage="Must enter Catalog Name"
         Display="dynamic"  
         ControlToValidate="TxtCatName"
         SetFocusOnError="true">
   </asp:RequiredFieldValidator>
   </td>
   </tr>
   <tr>
   <td align="right">
   <asp:Label ID="Label2" runat="server" Text="Effective Date"></asp:Label>
   </td>
   <td>
   <telerik:RadDatePicker ID="CatEffDate" runat="server" MaxDate="9999-12-31">
   </telerik:RadDatePicker>
   <br/>
   <asp:RequiredFieldValidator ID="RFVeffdate" runat="server" 
         ErrorMessage="Must enter effective date"
         Display="dynamic"  
         ControlToValidate="CatEffDate"
         SetFocusOnError="true">
   </asp:RequiredFieldValidator>
   </td>
   </tr>
   <tr>
   <td align="right">
   <asp:Label ID="Label3" runat="server" Text="Expiration Date"></asp:Label>
   </td>
   <td>
   <telerik:RadDatePicker ID="CatExpDate" runat="server" MaxDate="9999-12-31">
   </telerik:RadDatePicker>
   <br/>
   <asp:RequiredFieldValidator ID="RFVexpdate" runat="server" 
         ErrorMessage="Must enter expire date"
         Display="dynamic"  
         ControlToValidate="CatExpDate"
         SetFocusOnError="true">
   </asp:RequiredFieldValidator>
   </td>
   </tr>
   <tr>
   <td align="right">
   <asp:Label ID="Label4" runat="server" Text="Catalog Type"></asp:Label>
   </td>
   <td>
   <asp:TextBox ID="TxtCatType" runat="server"></asp:TextBox>
   <br/>
   <asp:RequiredFieldValidator ID="RFVcattype" runat="server"
         ErrorMessage="Must enter catalog type"
         Display="dynamic"  
         ControlToValidate="TxtCatType"
         SetFocusOnError="true">
   </asp:RequiredFieldValidator>
   </td>
   </tr>
   </table>
</asp:Content>

