<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatMaint004.aspx.vb" Inherits="CatMaint004" title="Catalog Maintenance" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table style="width: 98%; " align="center">
   <tr>
   <td align="center" colspan="3" style="font-size:8pt;"></td>
   </tr>
   <tr>
   <td></td>
   <td align="center">
   <asp:Button ID="ButtonSave" runat="server" Text="Save Item" />
   </td>
   <td align="center">
   <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CausesValidation="false" />
   </td>
   </tr>
   <tr>
   <td align="left" colspan="3" style="font-size:8pt;">
   <asp:Label ID="LblError" runat="server" ForeColor="Red" Text=""></asp:Label></td>
   </tr>
   <tr>
    <td align="left" rowspan="10" valign="middle">
        <asp:Image ID="ItemImage" runat="server" />
        <asp:Literal ID="ItemDesc" runat="server" Visible="true"></asp:Literal>
   </td>
   <td align="right">
   <asp:Label ID="Label1" runat="server" Text="Item Number"></asp:Label>
   </td>
   <td>
   <asp:TextBox ID="TxtItem" runat="server"></asp:TextBox>
   </td>
   </tr>
   <tr>
   <td align="right">
   <asp:Label ID="Label2" runat="server" Text="Effective Date"></asp:Label>
   </td>
   <td>
   <telerik:RadDatePicker ID="ItmEffDate" runat="server" MaxDate="9999-12-31">
   </telerik:RadDatePicker>
   <br/>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
         ErrorMessage="Must enter effective date"
         Display="dynamic"  
         ControlToValidate="ItmEffDate"
         SetFocusOnError="true">
    </asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td align="right">
    <asp:Label ID="Label3" runat="server" Text="Expired Date"></asp:Label>
    </td>
    <td>
    <telerik:RadDatePicker ID="ItmExpDate" runat="server" MaxDate="9999-12-31">
    </telerik:RadDatePicker>
    <br/>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
         ErrorMessage="Must enter expire date"
         Display="dynamic"  
         ControlToValidate="ItmExpDate"
         SetFocusOnError="true">
    </asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td align="right">
    <asp:Label ID="Label4" runat="server" Text="Incremental Qty"></asp:Label>
    </td>
    <td>    
    <asp:TextBox ID="TxtIncQty" runat="server"></asp:TextBox>
    <br/>
         <asp:RequiredFieldValidator ID="RFVIncQty" runat="server" 
         ErrorMessage="Must enter quantity"
         Display="dynamic"  
         ControlToValidate="TxtIncQty"
         SetFocusOnError="true">
         </asp:RequiredFieldValidator>
         <asp:RangeValidator id="RVMaxQty" runat="server"
         ControlToValidate="TxtIncQty"
         display="Dynamic"
         MinimumValue="1"
         MaximumValue="999"
         Type="Integer"
         SetFocusOnError="true"
         Text="Invalid quantity">
         </asp:RangeValidator>
    </td>
    </tr>
    <tr>
    <td align="right">
    <asp:Label ID="Label5" runat="server" Text="Max. Qty"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="TxtMaxQty" runat="server"></asp:TextBox>
    <br/>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
         ErrorMessage="Must enter Maximum Quantity"
         Display="dynamic"  
         ControlToValidate="TxtMaxQty"
         SetFocusOnError="true">
         </asp:RequiredFieldValidator>
         <asp:RangeValidator id="RangeValidator1" runat="server"
         ControlToValidate="TxtMaxQty"
         display="Dynamic"
         MinimumValue="1"
         MaximumValue="999"
         Type="Integer"
         SetFocusOnError="true"
         Text="Invalid quantity">
         </asp:RangeValidator>
    </td>
    </tr>
    <tr>
    <td align="right">
    <asp:Label ID="Label6" runat="server" Text="Art Package Loc."></asp:Label>
    </td>
    <td>
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem Value="AF">Atlantic</asp:ListItem>
        <asp:ListItem Value="PF">Pacific</asp:ListItem>
        <asp:ListItem Value="MF">Mississippi</asp:ListItem>
     </asp:DropDownList> 
    </td>
    </tr>
    <tr>
   <td align="right">
        <asp:Label ID="Label7" runat="server" Text="Catalog Group"></asp:Label>
    </td>
    <td>
    <asp:DropDownList ID="DropDownGroup" runat="server">
    </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td align="right">
    <asp:Label ID="Label8" runat="server" Text="Sequence Number"></asp:Label>
    </td>
    <td>    
    <asp:TextBox ID="TxtSeqNum" runat="server"></asp:TextBox>
    <br/>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
         ErrorMessage="Must enter Sequence number"
         Display="dynamic"  
         ControlToValidate="TxtSeqNum"
         SetFocusOnError="true">
         </asp:RequiredFieldValidator>
         <asp:RangeValidator id="RangeValidator3" runat="server"
         ControlToValidate="TxtSeqNum"
         display="Dynamic"
         MinimumValue="1"
         MaximumValue="9999"
         Type="Integer"
         SetFocusOnError="true"
         Text="Invalid sequence">
         </asp:RangeValidator>
    </td>
    </tr>
    <tr>

    <td align="right" valign="middle">
        <asp:Label ID="Label9" runat="server" Text="Upload Item Image"></asp:Label>
    </td>
    <td valign="bottom">
        <asp:FileUpload ID="fileup" runat="server"
           />
           <br/>
        <asp:RegularExpressionValidator ID="REVUpLoad" runat="server"
            ErrorMessage="Invalid File type"
            ControlToValidate="fileup"
            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.JPG|.jpg)$"
            >
        </asp:RegularExpressionValidator>
        </td>

    </tr>
    <tr>

    <td align="right" valign="middle">
        <asp:Label ID="Label10" runat="server" Text="Upload Item Description"></asp:Label>
    </td>
    <td valign="bottom">
        <asp:FileUpload ID="DescUpload" runat="server"
           />
           <br/>
        <asp:RegularExpressionValidator ID="REVDesc" runat="server"
            ErrorMessage="Invalid File type"
            ControlToValidate="DescUpload"
            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.TXT|.txt)$"
            >
        </asp:RegularExpressionValidator>
    </td>

    </tr>
    </table>
    </asp:Content>

