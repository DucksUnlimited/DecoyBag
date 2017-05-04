<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatEntry003.aspx.vb" Inherits="CatEntry003" title="Ship To" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" src="JScript/Translate.js"> </script>

    <asp:HiddenField ID="hrdonly" runat="server" Value="N" />
    
    <telerik:RadCodeBlock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
                function RequestStart()
                {
                    var ordupdate = document.getElementById('<%= hrdonly.ClientID %>').value;

                    if (ordupdate == "N")
                    {
                        return true;
                    }
                    var cbvalue = document.getElementById('<%= cbApprove.ClientID %>').checked;

                    if(!cbvalue)
                    {
                        var c=confirm("Approval Box has not been checked. Click CANCEL to go back and check Approve Order box. Or click OK to exit the order without approving it.");

                            if(c)
                                return true;
                            else
                                return false;
                    }
                    else{return true;
                    }
                }
        </script>
    </telerik:RadCodeBlock>

<asp:Panel ID="Panel1" runat="server" >
<div align="center">
    <table style="align-content:center; align-self:center; width: 80%; ">
        <tr>
            <td align="center" colspan="4" style="font-size:8pt;">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" align="center" valign="middle">
                <table style="align-self:center; width: 80%; ">
                    <tr>
                        <td align="center" colspan="2" valign="middle">
                            <asp:Button ID="btnExit" runat="server" Text="Exit Decoy Bag" CausesValidation="false" />
                        </td>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnReturn" runat="server" Text="Return to Order Form" CausesValidation="false" />
                        </td>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSubmit" runat="server"
                                Text="Complete Order"
                                 />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="font-size:14pt;">
                <asp:CheckBox ID="cbApprove" runat="server"
                    Checked="false"
                    BackColor="#ccffcc"
                    Visible="true"
                    text="Check Here to Approve Order"
                    TextAlign="Right" >
                 </asp:CheckBox>
                &nbsp;
            </td>
        </tr>
        <tr> 
            <td colspan="4">                        
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
               <asp:Label ID="Label2" runat="server" Text="Ship To:" Font-Size="14pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
            </td>
            <td align="left" style="width: 558px">
                <asp:Label ID="LBLshpchapter" runat="server" Font-Size="14pt" ForeColor="darkGreen"></asp:Label></td>
            <td align="right" style="text-wrap:none;" >
               <asp:Label ID="Label3" runat="server" Text="Bill To:" Font-Size="14pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="LBLbillchapter" runat="server" Font-Size="14pt" ForeColor="darkGreen"></asp:Label></td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="font-size:6pt;">&nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label1" runat="server" Text="Name:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left" style="width: 558px">
                <asp:TextBox ID="LBLcshp1" runat="server" Font-Size="10pt" MaxLength="30" Width="210px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFVName" runat="server" 
                    ErrorMessage="Must enter Name"
                    Display="dynamic"  
                    ControlToValidate="LBLcshp1"
                    SetFocusOnError="true">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVName" runat="server"
                    ErrorMessage="Invalid Name."
                    Display="dynamic"  
                    ControlToValidate="LBLcshp1"
                    ValidationExpression="^[a-zA-Z-'\.\s]{2,128}$"
                    SetFocusOnError="true">
                </asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
            <td align="left">
                <asp:Label ID="LBLcsname" runat="server" Font-Size="10pt"></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label4" runat="server" Text="Address Line 1:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left" style="width: 558px">
                <asp:TextBox ID="LBLcshp2" runat="server" Font-Size="10pt" MaxLength="30" Width="210px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFVAddr1" runat="server" 
                    ErrorMessage="Must Enter Address line."
                    Display="dynamic"  
                    ControlToValidate="LBLcshp2"
                    SetFocusOnError="true">
                </asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
            <td align="left">
                <asp:Label ID="LBLcsadd1" runat="server" Font-Size="10pt"></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label5" runat="server" Text="Address Line 2:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left" style="width: 558px;">
                <asp:TextBox ID="LBLcshp3" runat="server" Font-Size="10pt" MaxLength="30" Width="210px"></asp:TextBox>
            </td> 
             <td>&nbsp;</td>
           <td align="left">
                <asp:Label ID="LBLcsadd2" runat="server" Font-Size="10pt"></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label6" runat="server" Text="City:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
           <td align="left" style="width: 558px">
                <asp:TextBox ID="LBLcshp6" runat="server" Font-Size="10pt" MaxLength="25" Width="180px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFVCity" runat="server" 
                    ErrorMessage="Must enter City."
                    ControlToValidate="LBLcshp6"
                    SetFocusOnError="true">
                </asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
            <td align="left" style="text-wrap:none;">
                <asp:Label ID="LBLcsctst" runat="server" Font-Size="10pt"></asp:Label>
                <asp:Label ID="LBLcszipa" runat="server" Font-Size="10pt" Width="36px"></asp:Label>
                <asp:Label ID="LBLcszip2" runat="server" Font-Size="10pt" Width="32px"></asp:Label></td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label7" runat="server" Text="State:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
           <td align="left" style="width: 558px">
                <asp:DropDownList ID="ddlState" runat="server" Font-Size="10pt"></asp:DropDownList>
            </td> 
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label8" runat="server" Text="Zip Code:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
           <td align="left" style="width: 558px">
                <asp:TextBox ID="LBLcshpza" runat="server" Font-Size="10pt" MaxLength="5" Width="70px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="LBLcshpz2" runat="server" Font-Size="10pt" MaxLength="4" Width="32px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFVZipCode" runat="server" 
                    ErrorMessage="Must enter Zip Code."
                    display="Dynamic"
                    ControlToValidate="LBLcshpza"
                    SetFocusOnError="true">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVZipCode" runat="server"
                    ErrorMessage="ZIP Code must be 5 numeric digits"
                    ControlToValidate="LBLcshpza"
                    display="Dynamic"
                    ValidationExpression="^\d{5}$"
                    SetFocusOnError="true">
                </asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr style="height:10px;">
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td align="right" valign="middle" style="text-wrap:none;">
                <asp:Label ID="label17" runat="server" Text="Your Email Address:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="emailaddr" runat="server" MaxLength="150" Width="575px"></asp:TextBox>
                <br />                
                <asp:RequiredFieldValidator ID="RFVeMail" runat="server"
                    ErrorMessage="Must have an EMail address to enter order!"
                    display="Dynamic"
                    ControlToValidate="emailaddr"
                    SetFocusOnError="true">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVeMail" runat="server"
                    ErrorMessage="Not a valid eMail"
                    ControlToValidate="emailaddr"
                    display="Dynamic"
                    ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                    SetFocusOnError="true">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr style="height:10px;">
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td align="right" valign="middle" style="text-wrap:none;">
                <asp:Label ID="label18" runat="server" Text="Comments:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <telerik:radspell id="spell1" runat="server" controltocheck="comments" SupportedLanguages="en-US,English" Skin="Office2010Silver" Width="80%">
                </telerik:radspell>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="comments" runat="server" MaxLength="500"  
                    rows="9" TextMode="MultiLine" Columns="70">
                    </asp:TextBox>                
            </td>
        </tr>
        <tr style="height:10px;">
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
	            <asp:Panel ID="FFLPanel" runat="server" Visible="false" Width="100%">

                    <script type="text/javascript">
                        
                        function PopupAbove(e, pickerID)
                        { 
                         var datePicker;
                         if (pickerID == null)
                         {
                             datePicker = $find("<%= fflexp.ClientID %>");
                         }
                         else
                         {
                         datePicker = $find(pickerID);
                         }
                         
                            var textBox = datePicker.get_textBox();
                            var popupElement = datePicker.get_popupContainer();
                            
                            var dimensions = datePicker.getElementDimensions(popupElement);
                            var position = datePicker.getElementPosition(textBox);
                            
                            datePicker.showPopup(position.x, position.y - dimensions.height - 35);
                        }
                        
                    </script>
                    
                    <script type="text/javascript">
                            function MustSelectState_ClientValidate(src, args)
                            {
                                var city = document.getElementById('<%=fflcity.ClientID %>');
                                var state = document.getElementById('<%=ddlfflstate.ClientID %>');
                                var error = document.getElementById('<%=CVStateSelect.ClientID %>');

                                args.IsValid=true;
                                if(city)
                                {
                                    if(city.value==""||city.value==null||city.value==" ")
                                    {
                                        if(state.selectedIndex>0)
                                        {
                                            error.ErrorMessage = 'Must enter a City if Selecting a State.';
                                            args.IsValid=false;
                                        }
                                    }
                                    else
                                    {
                                        if(state.selectedIndex<1)
                                        {
                                            args.IsValid=false;
                                        }                                       
                                    }
                                }
                            }
                    </script>
	                <table style="width: 100%;align-self:center ">
	                    <tr>
		                    <td align="center" colspan="2" style="background-color:darkseagreen; font-size:26pt;">FFL Information is Required</td>
	                    </tr>
	                    <tr>
		                    <td align="center" style="font-size:10pt;">&nbsp;</td>
	                    </tr>
	                    <tr>
		                    <td align="right">
			                    <asp:Label ID="Label9" runat="server" Text="Company Name:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
			                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		                    </td>
		                    <td align="left">
			                    <asp:TextBox ID="fflorg" runat="server" MaxLength="60" Width="420px"></asp:TextBox>
			                    &nbsp;&nbsp;&nbsp;
			                    <asp:RequiredFieldValidator ID="RFVCompany" runat="server" Enabled="false"
				                    ErrorMessage="Must enter Company!"
				                    Visible="false"
				                    display="Dynamic"
				                    ControlToValidate="fflorg"
				                    SetFocusOnError="true">
			                    </asp:RequiredFieldValidator>
		                    </td>
	                    </tr>
	                    <tr>
		                    <td align="right">
			                    <asp:Label ID="Label10" runat="server" Text="Attention To:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
			                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		                    </td>
		                    <td align="left">
			                    <asp:TextBox ID="fflattn" runat="server" MaxLength="60" Width="420px"></asp:TextBox>
			                    &nbsp;&nbsp;&nbsp;
			                    <asp:RequiredFieldValidator ID="RFVAttenTo" runat="server" Enabled="false"
				                    ErrorMessage="Must enter Attention To!"
				                    Visible="false"
				                    display="Dynamic"
				                    ControlToValidate="fflattn"
				                    SetFocusOnError="true">
			                    </asp:RequiredFieldValidator>
		                    </td>
	                    </tr>
	                    <tr>
		                    <td align="right">
			                    <asp:Label ID="Label11" runat="server" Text="Address:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
			                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		                    </td>
		                    <td align="left">
			                    <asp:TextBox ID="ffladdr" runat="server" MaxLength="60" Width="420px"></asp:TextBox>
			                    &nbsp;&nbsp;&nbsp;
		                    </td>
	                    </tr>
	                    <tr>
		                    <td align="right">
			                    <asp:Label ID="Label12" runat="server" Text="City:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
			                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		                    </td>
		                    <td align="left">
			                    <asp:TextBox ID="fflcity" runat="server" MaxLength="40" Width="280px" ></asp:TextBox>
			                    &nbsp;&nbsp;&nbsp;
			                    <asp:RequiredFieldValidator ID="RFVFFLCity" runat="server" Enabled="false"
				                    ErrorMessage="Must enter City!"
				                    display="Dynamic"
				                    Visible="false"
				                    ControlToValidate="fflcity"
				                    SetFocusOnError="true">
			                    </asp:RequiredFieldValidator>
		                    </td>
	                    </tr>
	                    <tr>
		                    <td align="right">
			                    <asp:Label ID="Label13" runat="server" Text="State:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
			                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		                    </td>
	                       <td align="left">
			                    <asp:DropDownList ID="ddlFFLState" runat="server"></asp:DropDownList> 
			                    &nbsp;&nbsp;&nbsp;
                                <asp:CustomValidator ID="CVStateSelect" runat="server" ErrorMessage="Must Select A State if City is entered."
                                    Display="Dynamic"
                                    ClientValidationFunction="MustSelectState_ClientValidate">
                                </asp:CustomValidator>
                            </td>
                        </tr>
	                    <tr>
	                        <td align="right">
                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="DarkGreen" Text="Zip Code:"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td align="left">
                                <asp:TextBox ID="fflzip5" runat="server" MaxLength="5" Width="36px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="fflzip4" runat="server" MaxLength="4" Width="32px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="REVFFLZip5" runat="server" ControlToValidate="fflzip5" display="Dynamic" Enabled="false" ErrorMessage="ZIP Code must be 5 numeric digits" SetFocusOnError="true" ValidationExpression="^\d{5}$" Visible="false">
			                    </asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="REVFFLZip4" runat="server" ControlToValidate="fflzip4" display="Dynamic" Enabled="false" ErrorMessage="ZIP + 4 must be 4 numeric digits" SetFocusOnError="true" ValidationExpression="^\d{4}$" Visible="false">
			                    </asp:RegularExpressionValidator>
                            </td>
                        </tr>
	                    <tr>
		                    <td align="right">
			                    <asp:Label ID="Label15" runat="server" Text="FFL Number:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
			                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		                    </td>
	                       <td align="left">
	                        <table>
	                            <tr>
	                                <td align="left">
        			                    <asp:TextBox ID="fflnum" runat="server" MaxLength="20"></asp:TextBox>
	                                </td>
	                                <td align="right">
	                                    &nbsp;&nbsp;&nbsp;&nbsp;
        			                    <asp:Label ID="Label16" runat="server" Text="Expiration Date:" Font-Size="12pt" Font-Bold="True" ForeColor="DarkGreen"></asp:Label>
		        	                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                                </td>
	                                <td align="left" style="width:25px;">
                                        <telerik:RadDatePicker ID="fflexp" runat="server"
                                          skin="Office2010Silver"
                                          >
                                        </telerik:RadDatePicker>
                                        &nbsp;
                                    </td>
	                            </tr>
	                         </table>
                            </td>
	                    </tr>
	                    <tr>
		                    <td align="center" colspan="2" style="font-size:14pt;background-color:darkseagreen;">Please note: FFL date must not expire prior to order date.<br />Please Send Signed Federal Firearms License. </td>
	                    </tr>
	                </table>
	            </asp:Panel>              
		    </td>
        </tr>
    </table>
  </div>
</asp:Panel>  
<telerik:RadAjaxManager ID="RAJM1" runat="server" DefaultLoadingPanelID="LoadingPanel1" >
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="btnSubmit">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Panel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    <ClientEvents OnRequestStart="RequestStart" />
</telerik:RadAjaxManager>
  <telerik:RadAjaxLoadingPanel ID="LoadingPanel1" runat="server" Transparency="30"  HorizontalAlign="Center" BackColor="#E0E0E0">
  <img alt="Building Order..." src="/Images/2loading.gif"  align="middle" />
  </telerik:RadAjaxLoadingPanel>
</asp:Content>
