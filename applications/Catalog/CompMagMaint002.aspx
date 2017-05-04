<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CompMagMaint002.aspx.vb" Inherits="CompMagMaint002" title="Untitled Page" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript">
function TranslateWord(field)
{
    var newfield = " ";
    newfield = field.toUpperCase();
    return newfield;
}
function ForeignEdit()
{
    var ddlfor = document.getElementById('<%=ddlForeign.ClientID %>').selectedIndex;

    if (ddlfor==0)
    {
       document.getElementById('<%=txtDCity.ClientID %>').disabled=true;
       document.getElementById('<%=ddlState.ClientID %>').disabled=true;
       document.getElementById('<%=txtDZip.ClientID %>').disabled=true;
       document.getElementById('<%=txtFCity.ClientID %>').disabled=true;

    }

    if (ddlfor==1)
    {
       document.getElementById('<%=txtDCity.ClientID %>').disabled=false;
       document.getElementById('<%=ddlState.ClientID %>').disabled=false;
       document.getElementById('<%=txtDZip.ClientID %>').disabled=false;
       document.getElementById('<%=txtFCity.ClientID %>').disabled=true;

    }
    
    if (ddlfor==2)
    {
       document.getElementById('<%=txtDCity.ClientID %>').disabled=true;
       document.getElementById('<%=ddlState.ClientID %>').disabled=true;
       document.getElementById('<%=txtDZip.ClientID %>').disabled=true;
       document.getElementById('<%=txtFCity.ClientID %>').disabled=false;

    }
}
function ForeignEdit_ClientValidate(src, args)
{
    var ddlfor = document.getElementById('<%=ddlForeign.ClientID %>').selectedIndex;
    var ddldst = document.getElementById('<%=ddlstate.ClientID %>').selectedIndex;
    var ddldcity = document.getElementById('<%=TxtDCity.ClientID %>').value;
    var ddldzip = document.getElementById('<%=TxtDZip.ClientID %>').value;
    var ddlfcity = document.getElementById('<%=TxtFCity.ClientID %>').value;
    
    if (ddlfor==0)
    {
        document.getElementById('<%=ForeignError.ClientID %>').style.display='inline';
        args.IsValid=false;
    }
    
    if (ddlfor==1)
    {
            if(ddldst == 0||ddldcity == null||ddldcity == ""||ddldcity == " "||ddldzip == null||ddldzip == ""||ddldzip == " ")
            {
            document.getElementById('<%=ForeignError.ClientID %>').style.display='inline';
            args.IsValid=false;

            }
            else {
            document.getElementById('<%=ForeignError.ClientID %>').style.display='none';
            args.IsValid=true;
          
            }
   }
       if (ddlfor==2)
    {
            if(ddlfcity == null||ddlfcity == ""||ddlfcity == " ")
            {
            document.getElementById('<%=ForeignError.ClientID %>').style.display='inline';
            args.IsValid=false;

            }
            else {
            document.getElementById('<%=ForeignError.ClientID %>').style.display='none';
            args.IsValid=true;
          
            }
   }

}


</script>
<center>
 <div>
   <table style="width: 60%; " align="center">
        <tr>
           <td align="center" colspan="3" style="font-size:8pt;"></td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="ButtonSave" runat="server" Text="Save Item" />
            </td>
            <td align="center" style="width: 259px">
                <asp:Button ID="ButtonDelete" runat="server" Text="Delete Item" />
            </td>
            <td align="center">
                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CausesValidation="false"  />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" style="font-size:8pt;">
                <asp:Label ID="LblError" runat="server" ForeColor="Red" Text=""></asp:Label>&nbsp;
            </td>
        </tr>
        <tr> 
            <td align="right">
                <asp:Label ID="Label1" runat="server" Text="Sequence Number"></asp:Label>
            </td>
            <td style="width: 59px">
                <asp:TextBox ID="TxtSeqNum" runat="server" Enabled="False" Visible="False" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label2" runat="server" Text="First Mag Date"></asp:Label>
            </td>
            <td style="width: 259px">
                <telerik:RadDatePicker ID="MagEffDate" runat="server" MaxDate="9999-12-31">
                </telerik:RadDatePicker>
                <asp:Label ID="Label5" runat="server" Text="*" ForeColor="red" Font-Size="14pt"></asp:Label>
                <br/>
                <asp:RequiredFieldValidator ID="RFVeffdate" runat="server" 
                    ErrorMessage="Must enter effective date"
                    Display="dynamic"  
                    ControlToValidate="MagEffDate"
                    SetFocusOnError="true">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label3" runat="server" Text="Department"></asp:Label>
            </td>
            <td style="width: 259px">
                <asp:TextBox ID="TxtDept" runat="server"></asp:TextBox>
                <asp:Label ID="Label17" runat="server" Text="*" ForeColor="red" Font-Size="14pt"></asp:Label>
                <br/>
                <asp:RequiredFieldValidator ID="RFVDept" runat="server"
                    ErrorMessage="Must enter catalog type"
                    Display="dynamic"  
                    ControlToValidate="TxtDept"
                    SetFocusOnError="true">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label4" runat="server" Text="Name Sort"></asp:Label>
            </td>
            <td style="width: 259px">
                <asp:TextBox ID="TxtSort" runat="server"></asp:TextBox>
                <asp:Label ID="Label16" runat="server" Text="*" ForeColor="red" Font-Size="14pt"></asp:Label>
                <br/>
                <asp:RequiredFieldValidator ID="RFVSort" runat="server"
                    ErrorMessage="Must enter sort name"
                    Display="dynamic"  
                    ControlToValidate="TxtSort"
                    SetFocusOnError="true">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label6" runat="server" Text="Print Line 1"></asp:Label>
            </td>
            <td style="width: 359px">
                <asp:TextBox ID="TxtLine1" runat="server" Width="200px"></asp:TextBox>
                <asp:Label ID="Label18" runat="server" Text="*" ForeColor="red" Font-Size="14pt"></asp:Label>
                <br/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="Must enter print line 1"
                    Display="dynamic"  
                    ControlToValidate="TxtLine1"
                    SetFocusOnError="true">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label7" runat="server" Text="Print Line 2"></asp:Label>
            </td>
            <td style="width: 359px">
                <asp:TextBox ID="TxtLine2" runat="server" Width="200px"></asp:TextBox>
                <asp:Label ID="Label19" runat="server" Text="*" ForeColor="red" Font-Size="14pt"></asp:Label>
                <br/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ErrorMessage="Must enter print line 2"
                    Display="dynamic"  
                    ControlToValidate="TxtLine2"
                    SetFocusOnError="true">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label8" runat="server" Text="Print Line 3"></asp:Label>
            </td>
            <td style="width: 359px">
                <asp:TextBox ID="TxtLine3" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
             <td align="right">
                <asp:Label ID="Label9" runat="server" Text="Print Line 4"></asp:Label>
            </td>
            <td style="width: 359px">
                <asp:TextBox ID="TxtLine4" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label10" runat="server" Text="Print Line 5"></asp:Label>
            </td>
            <td style="width: 359px;">
                <asp:TextBox ID="TxtLine5" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label15" runat="server" Text="For/Domestic Code"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlForeign" runat="server">
                    <asp:ListItem Value="  " Selected="True" Text="Select a Code"></asp:ListItem>
                    <asp:ListItem Value="D" Text="D - Domestic"></asp:ListItem>
                    <asp:ListItem Value="F" Text="F - Foreign"></asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label20" runat="server" Text="*" ForeColor="red" Font-Size="14pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="ForeignError" runat="server" style="display:none" 
                    Text="Please fill in correct city/st/zip field." ForeColor="red"></asp:Label>
                <asp:CustomValidator ID="CVForeign" runat="server"
                    ErrorMessage=" "
                    Display="Dynamic"
                    ClientValidationFunction="ForeignEdit_ClientValidate">
                </asp:CustomValidator>
            </td>                                
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label11" runat="server" Text="Domestic City"></asp:Label>
            </td>
            <td style="width: 259px">
                <asp:TextBox ID="TxtDCity" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label12" runat="server" Text="Domestic State"></asp:Label>
            </td>
            <td style="width: 259px">
                <asp:DropDownList ID="ddlState" runat="server" Enabled="false">
                </asp:DropDownList>
                <br/>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label13" runat="server" Text="Domestic Zip"></asp:Label>
            </td>
            <td style="width: 259px">
                <asp:TextBox ID="TxtDZip" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label14" runat="server" Text="Foreign City/St/Zip"></asp:Label>
            </td>
            <td style="width: 359px">
                <asp:TextBox ID="TxtFCity" runat="server" Enabled="false" Width="250px"></asp:TextBox>
            </td>
        </tr>
   </table>   
 </div>
</center>
</asp:Content>
