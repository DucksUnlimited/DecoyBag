<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CompMagMaint001.aspx.vb" Inherits="CompMagMaint001" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript">

function ButtonChecked()
{
       if (document.getElementById('<%=RB1.ClientID %>').checked)
   {
       document.getElementById('<%=tbFName.ClientID %>').disabled=false;
       document.getElementById('<%=tbTName.ClientID %>').disabled=false;
       document.getElementById('<%=tbCity.ClientID %>').disabled=true;
       document.getElementById('<%=ddlState.ClientID %>').disabled=true;
       document.getElementById('<%=tbZip.ClientID %>').disabled=true;
       document.getElementById('<%=tbSequence.ClientID %>').disabled=true;
       document.getElementById('<%=tbFDept.ClientID %>').disabled=true;
       document.getElementById('<%=tbTDept.ClientID %>').disabled=true;
   }
   
   if (document.getElementById('<%=RB2.ClientID %>').checked)
   {
       document.getElementById('<%=tbFName.ClientID %>').disabled=true;
       document.getElementById('<%=tbTName.ClientID %>').disabled=true;
       document.getElementById('<%=tbCity.ClientID %>').disabled=false;
       document.getElementById('<%=ddlState.ClientID %>').disabled=true;
       document.getElementById('<%=tbZip.ClientID %>').disabled=true;
       document.getElementById('<%=tbSequence.ClientID %>').disabled=true;
       document.getElementById('<%=tbFDept.ClientID %>').disabled=true;
       document.getElementById('<%=tbTDept.ClientID %>').disabled=true;
   }
   
   if (document.getElementById('<%=RB3.ClientID %>').checked)
   {
       document.getElementById('<%=tbFName.ClientID %>').disabled=true;
       document.getElementById('<%=tbTName.ClientID %>').disabled=true;
       document.getElementById('<%=tbCity.ClientID %>').disabled=true;
       document.getElementById('<%=ddlState.ClientID %>').disabled=false;
       document.getElementById('<%=tbZip.ClientID %>').disabled=true;
       document.getElementById('<%=tbSequence.ClientID %>').disabled=true;
       document.getElementById('<%=tbFDept.ClientID %>').disabled=true;
       document.getElementById('<%=tbTDept.ClientID %>').disabled=true;
   }
   
   if (document.getElementById('<%=RB4.ClientID %>').checked)
   {
       document.getElementById('<%=tbFName.ClientID %>').disabled=true;
       document.getElementById('<%=tbTName.ClientID %>').disabled=true;
       document.getElementById('<%=tbCity.ClientID %>').disabled=true;
       document.getElementById('<%=ddlState.ClientID %>').disabled=true;
       document.getElementById('<%=tbZip.ClientID %>').disabled=false;
       document.getElementById('<%=tbSequence.ClientID %>').disabled=true;
       document.getElementById('<%=tbFDept.ClientID %>').disabled=true;
       document.getElementById('<%=tbTDept.ClientID %>').disabled=true;
   }
   
   if (document.getElementById('<%=RB5.ClientID %>').checked)
   {
       document.getElementById('<%=tbFName.ClientID %>').disabled=true;
       document.getElementById('<%=tbTName.ClientID %>').disabled=true;
       document.getElementById('<%=tbCity.ClientID %>').disabled=true;
       document.getElementById('<%=ddlState.ClientID %>').disabled=true;
       document.getElementById('<%=tbZip.ClientID %>').disabled=true;
       document.getElementById('<%=tbSequence.ClientID %>').disabled=false;
       document.getElementById('<%=tbFDept.ClientID %>').disabled=true;
       document.getElementById('<%=tbTDept.ClientID %>').disabled=true;
   }
      if (document.getElementById('<%=RB6.ClientID %>').checked)
   {
       document.getElementById('<%=tbFName.ClientID %>').disabled=true;
       document.getElementById('<%=tbTName.ClientID %>').disabled=true;
       document.getElementById('<%=tbCity.ClientID %>').disabled=true;
       document.getElementById('<%=ddlState.ClientID %>').disabled=true;
       document.getElementById('<%=tbZip.ClientID %>').disabled=true;
       document.getElementById('<%=tbSequence.ClientID %>').disabled=true;
       document.getElementById('<%=tbFDept.ClientID %>').disabled=false;
       document.getElementById('<%=tbTDept.ClientID %>').disabled=false;
   }

   
}
   
function MustEnterName_ClientValidate(src, args)
{
    if (document.getElementById('<%=RB1.ClientID %>').checked)
    {
        var fname = document.getElementById('<%=tbFName.ClientID %>').value;
        var tname = document.getElementById('<%=tbTName.ClientID %>').value;
        if((fname == null||fname == ""||fname == " ")||(tname == null||tname == ""||tname == " "))
        {
            args.IsValid=false;
        }
        else {
            args.IsValid=true;    
        }
    }
} 

function MustEnterCity_ClientValidate(src, args)
{
    if (document.getElementById('<%=RB2.ClientID %>').checked)
    {
        var field = document.getElementById('<%=tbCity.ClientID %>').value;
        if(field == null||field == ""||field == " ")
        {
            args.IsValid=false;
        }
        else {
            args.IsValid=true;    
        }
    }
}

function MustSelectState_ClientValidate(src, args)
{
    if (document.getElementById('<%=RB3.ClientID %>').checked)
    {
        var state = document.getElementById('<%=ddlState.ClientID %>').selectedIndex;
        if(state<1)
        {
            args.IsValid=false;
        }
        else {
            args.IsValid=true;
        }
    }
}

function MustEnterZip_ClientValidate(src, args)
{
    if (document.getElementById('<%=RB4.ClientID %>').checked)
    {
        var field = document.getElementById('<%=tbZip.ClientID %>').value;
        if(field == null||field == ""||field == " ")
        {
            args.IsValid=false;
        }
        else {
            args.IsValid=true;
        }
    }
}

function MustEnterSequence_ClientValidate(src, args)
{
    if (document.getElementById('<%=RB5.ClientID %>').checked)
    {
        var field = document.getElementById('<%=tbSequence.ClientID %>').value;
        if(field == null||field == ""||field == " ")
        {
            args.IsValid=false;
        }
        else {
            args.IsValid=true;
        }
    }
}

function MustEnterDept_ClientValidate(src, args)
{
    if (document.getElementById('<%=RB6.ClientID %>').checked)
    {
        var fdept = document.getElementById('<%=tbFDept.ClientID %>').value;
        var tdept = document.getElementById('<%=tbTDept.ClientID %>').value;
        if((fdept == null||fdept == ""||fdept == " ")||(tdept == null||tdept == ""||tdept == " "))
        {
            args.IsValid=false;
        }
        else {
            args.IsValid=true;    
        }
    }
} 

function TranslateWord(field)
{
    var newfield = " ";
    newfield = field.toUpperCase();
    return newfield;
}
</script>
 <div align="center">
   <table style="width: 60%; " align="center">
        <tr>
            <td align="center" colspan="4" style="font-size:8pt;"></td>
        </tr>
        <tr>
            <td colspan="4" align="center" style="height: 79px;">
                <table style="width: 80%; " align="center">
                    <tr>
                        <td align="center" style="height: 59px">
                            <asp:Button ID="ButtonFind" runat="server" Text="   Find Name   " />
                        </td>
                        <td align="center" style="height: 59px" visible="false">
                            <asp:Button ID="ButtonAdd" runat="server" Text="   Add New Name   " CausesValidation="False"/>
                        </td>
                        <td align="center" style="height: 59px">
                            <asp:Button ID="ButtonExcel" runat="server" Text="Export to Excel" CausesValidation="False" Visible="False"/>
                        </td>
                        <td align="center" style="height: 59px">
                            <asp:Button ID="ButtonCancel" runat="server" Text="      Cancel     " CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="font-size:8pt; height: 25px;"></td>
        </tr>
        <tr>
            <td colspan="4" align="center" style="height: 79px;">
                <table style="width: 60%; " align="center">
                    <tr>
                        <td align="left">
                            <asp:RadioButton ID="RB1" runat="server" GroupName="rbGroup" Text="Name Range" ForeColor="black" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbFName" runat="server"></asp:TextBox>
                            <asp:TextBox ID="tbTName" runat="server"></asp:TextBox>
                            <br/>
                            <asp:CustomValidator ID="CustomValidator1" runat="server"
                                ErrorMessage="Must enter a from & to name if using this option."
                                Display="Dynamic"
                                ClientValidationFunction="MustEnterName_ClientValidate"
                                SetFocusOnError="true">
                            </asp:CustomValidator> 
                        </td>
                    </tr> 
                    <tr>
                        <td align="left">
                            <asp:RadioButton ID="RB2" runat="server" GroupName="rbGroup" Text="City" ForeColor="Black" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbCity" runat="server"></asp:TextBox>
                        <br/>
                            <asp:CustomValidator ID="CustomValidator2" runat="server"
                                ErrorMessage="Must enter a city if using this option."
                                Display="Dynamic"
                                ClientValidationFunction="MustEnterCity_ClientValidate"
                                SetFocusOnError="true">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:RadioButton ID="RB3" runat="server" GroupName="rbGroup" Text="State" ForeColor="Black" Checked="true" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server" Font-Size="10pt"></asp:DropDownList> 
                            <br/>
                            <asp:CustomValidator ID="CVConvReg" runat="server"
                                ErrorMessage="Must select a state if using this option."
                                Display="Dynamic"
                                ClientValidationFunction="MustSelectState_ClientValidate"
                                SetFocusOnError="true">
                            </asp:CustomValidator>
                        </td>
                    </tr>    
                    <tr>
                        <td align="left">
                            <asp:RadioButton ID="RB4" runat="server" GroupName="rbGroup" Text="Zip" ForeColor="Black" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbZip" runat="server"></asp:TextBox>
                            <br/>
                            <asp:CustomValidator ID="CustomValidator3" runat="server"
                                ErrorMessage="Must enter a zip if using this option."
                                Display="Dynamic"
                                ClientValidationFunction="MustEnterZip_ClientValidate"
                                SetFocusOnError="true">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:RadioButton ID="RB5" runat="server" GroupName="rbGroup" Text="Sequence" ForeColor="Black" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbSequence" runat="server"></asp:TextBox>
                            <br/>
                            <asp:CustomValidator ID="CustomValidator4" runat="server"
                                ErrorMessage="Must enter a sequence if using this option."
                                Display="Dynamic"
                                ClientValidationFunction="MustEnterSequence_ClientValidate"
                                SetFocusOnError="true">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:RadioButton ID="RB6" runat="server" GroupName="rbGroup" Text="Dept Range" ForeColor="Black" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbFDept" runat="server"></asp:TextBox>
                            <asp:TextBox ID="tbTDept" runat="server"></asp:TextBox>
                            <br/>
                            <asp:CustomValidator ID="CustomValidator5" runat="server"
                                ErrorMessage="Must enter a from & to dept. if using this option."
                                Display="Dynamic"
                                ClientValidationFunction="MustEnterDept_ClientValidate"
                                SetFocusOnError="true">
                            </asp:CustomValidator> 
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="font-size:8pt;"></td>
        </tr>      
        <tr>
            <td align="center" colspan="4" style="height: 148px;">
                <asp:Label ID="Label4" runat="server" BackColor="#D6D2E1" Font-Size="18px" ForeColor="Red" 
                    Text="No Records Found" Visible="False"></asp:Label>
                <asp:GridView ID="GridView1" ForeColor="Black" BackColor="#D6D2E1" runat="server" AllowSorting="true" 
                    OnSorting="gvCompMag_Sorting" AutoGenerateSelectButton="True" AutoGenerateColumns="False" Width="80%" >
                    <Columns>
                        <asp:BoundField DataField="cpseq" HeaderText="Sequence" SortExpression="Seq" >
                        </asp:BoundField>
                        <asp:BoundField DataField="cpsort" HeaderText="Name Sort" SortExpression="Name" >
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cplin1" HeaderText="Line One" >
                            <ItemStyle Wrap="False" />
                        </asp:BoundField> 
                        <asp:BoundField DataField="cpdept" HeaderText="Department" >
                        </asp:BoundField>
                        <asp:BoundField DataField="cpcity" HeaderText="City">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>             
                        <asp:BoundField DataField="cpstat" HeaderText="State" >
                        </asp:BoundField>
                        <asp:BoundField DataField="cpzip" HeaderText="Zip Code" >
                        </asp:BoundField>
                        <asp:BoundField DataField="cpcnty" HeaderText="Foreign" >
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>             
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
 </div>
</asp:Content>
