<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="ML001.aspx.vb" Inherits="ML001" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

          &nbsp; &nbsp;
          <div align=center>
            <asp:Panel ID="SubmitSelect" runat="server">
<!--            <table  style="width: 232px;"> -->
            <table align=center>
              <tr>
                <td style="padding: 0px; margin: 0px; width: 129px; height: 26px;">
                  <asp:Label ID="Label1" runat="server" Text="AS/400 User ID" Width="128px"></asp:Label>
                </td>
                <td align="left" style="padding: 0px; margin: 0px; width: 154px; height: 26px;">
                  <asp:TextBox ID="UserId" runat="server" MaxLength="10"
                    BorderStyle="solid" BorderWidth="1px" BorderColor="#a9a9a9" TabIndex="1"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td style="padding: 0px; margin: 0px; width: 129px; height: 26px;">
                  <asp:Label ID="Label3" runat="server" Text="AS/400 Password" Width="128px"></asp:Label>
                </td>
                <td align="left" style="padding: 0px; margin: 0px; width: 154px; height: 26px;">
                  <asp:TextBox ID="Password" runat="server" MaxLength="10" TextMode=Password
                    BorderStyle="solid" BorderWidth="1px" BorderColor="#a9a9a9" CausesValidation="True"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td style="padding: 0px; margin: 0px; width: 129px; height: 26px;">
                  <asp:Label ID="Label2" runat="server" Text="Job Name" Width="128px"></asp:Label>
                </td>
                <td style="padding: 0px; margin: 0px; width: 154px; height: 26px;" align="left">
                  <asp:TextBox ID="JobName" runat="server" MaxLength="10"
                    BorderStyle="solid" BorderWidth="1px" BorderColor="#a9a9a9"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td style="padding: 0px; margin: 0px; width: 129px; height: 24px;">
                  <asp:Label ID="FileIn" runat="server" Text="Text File Name" Width="128px"></asp:Label>
                </td>
                <td style="padding: 0px; margin: 0px; width: 154px; height: 24px;" align="left">
                  <asp:DropDownList ID="FileList" runat="server" Width="100%">
                  </asp:DropDownList>
                </td>
              </tr>
              <tr>
                <td style="padding: 0px; margin: 0px; height: 26px;">
                  <asp:Label ID="Label4" runat="server" Text="Origination Zip Code" Visible="False"></asp:Label>
                </td>
                <td style="padding: 0px; margin: 0px; width: 154px; height: 26px;" align="left">
                    <asp:DropDownList ID="OrgZip" runat="server" Visible="False">
                    </asp:DropDownList></td>
              </tr>
            </table>
            &nbsp;&nbsp;<br />
                  <asp:RequiredFieldValidator Runat="server" id="UReq"
                    ControlToValidate="UserId"
                    ErrorMessage="<b>Required Field Missing</b><br />A User ID is required."
                    Display="None" SetFocusOnError="True" />
                  <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="UReqE"
                    TargetControlID="UReq"
                    Width="250px"              
                    HighlightCssClass="validatorCalloutHighlight" />
                  <asp:RequiredFieldValidator Runat="server" id="PWReq"
                    ControlToValidate="Password"
                    ErrorMessage="<b>Required Field Missing</b><br />A Password is required."
                    Display="None" SetFocusOnError="True" />
                  <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="PWReqE"
                    TargetControlID="PWReq"
                    Width="250px"                           
                    HighlightCssClass="validatorCalloutHighlight" />
                  <asp:RequiredFieldValidator Runat="server" id="JNReq"
                    ControlToValidate="JobName"
                    ErrorMessage="<b>Required Field Missing</b><br />A Job Name is required."
                    Display="None" SetFocusOnError="True" />
                  <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="JNReqE"
                    TargetControlID="JNReq"
                    Width="250px"
                    HighlightCssClass="validatorCalloutHighlight" />
                  <asp:RegularExpressionValidator Runat="server" id="JNReg"
                    ControlToValidate="JobName"
                    ErrorMessage="<b>Invalid Job Name</b><br />Job Name <b>MUST</b> start with an alpha character & <b>NO</b> spaces or special characters allowed."
                    Display="None"
                    ValidationExpression="^[a-zA-Z][0-9a-zA-Z]*$"
                    SetFocusOnError="True" />
                  <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="JNRegE"
                    TargetControlID="JNReg"
                    Width="250px"
                    HighlightCssClass="validatorCalloutHighlight" />
            <table>
              <tr>
                <td>
                  <fieldset><legend style=color:darkgreen;>Labels</legend>
                    <table>
                      <tr>
                        <td align=left valign=middle>
                          <asp:CheckBox ID="Labels" runat="server" Checked="True" Text="Labels" Enabled="False" />
                          <br />
                        </td>
                        <td align=left valign=bottom>
                          <fieldset><legend style=color:darkblue;>Copies</legend>
                            <a>&nbsp;</a>
                            <asp:TextBox ID="LCopies" runat="server" Text="1" Width="50" style="text-align:center" />
                            <ajaxToolkit:NumericUpDownExtender ID="NUDE1" runat="server"
                              TargetControlID="LCopies" 
                              Width="50"
                              RefValues="" 
                              ServiceDownMethod=""
                              ServiceUpMethod=""
                              TargetButtonDownID=""
                              TargetButtonUpID="" 
                              Minimum = "1"
                              Maximum = "4" />
                          </fieldset>
                        </td>
                        <td align=left valign=bottom>
                          <fieldset><legend style=color:darkblue;>Sort</legend>
                            <asp:RadioButtonList ID="SortTypeL" runat="server" RepeatDirection="Horizontal">
                              <asp:ListItem Selected="True" Value="LN ">Last Name</asp:ListItem>
                              <asp:ListItem Value="ZL ">Zip/Last Name</asp:ListItem>
                            </asp:RadioButtonList>
                          </fieldset>
                        </td>
                        <td align=left valign=bottom>
                          <fieldset><legend style=color:darkblue;>Type</legend>
                            <asp:RadioButtonList ID="LabelType" runat="server" RepeatDirection="Horizontal">
                              <asp:ListItem Value="1">1 Up</asp:ListItem>
                              <asp:ListItem Selected="True" Value="4">4 Up</asp:ListItem>
                            </asp:RadioButtonList>
                          </fieldset>
                        </td>
                        <td align=left valign=bottom>
					&nbsp;
                        </td>
                      </tr>
                    </table>
                    <br />
                  </fieldset>
                </td>
              </tr>                   
              <tr>
                <td align=left>
                  <fieldset><legend style=color:darkgreen;>Reports</legend>
                    <table>
                      <tr>
                        <td align=left valign=middle>
                          <asp:CheckBox ID="Reports" runat="server" Text="Report" />
                          <br />
                        </td>
                        <td align=left valign=bottom>
                          <fieldset><legend style=color:darkblue;>Copies</legend>
                            <a>&nbsp;</a>                         
                            <asp:TextBox ID="RCopies" runat="server" Text="1" Width="50" style="text-align:center" />
                            <ajaxToolkit:NumericUpDownExtender ID="NUDE2" runat="server"
                              TargetControlID="RCopies" 
                              Width="50"
                              RefValues="" 
                              ServiceDownMethod=""
                              ServiceUpMethod=""
                              TargetButtonDownID=""
                              TargetButtonUpID="" 
                              Minimum = "1"
                              Maximum = "4" />
                          </fieldset>
                        </td>
                        <td align=left valign=bottom colspan=3>
                          <fieldset><legend style=color:darkblue;>Sort</legend>                         
                            <asp:RadioButtonList ID="SortTypeR" runat="server" RepeatDirection="Horizontal">
                              <asp:ListItem Selected="True" Value="LN ">Last Name</asp:ListItem>
                              <asp:ListItem Value="ZL ">Zip/Last Name</asp:ListItem>
                            </asp:RadioButtonList>
                          </fieldset>
                        </td>
                      </tr>
                    </table>  
                      <br />
                  </fieldset>
                </td>
              </tr>
            </table>
            <br />
            <div align=center>
            <table border="0" cellpadding="0" cellspacing="0" width=50%>
              <tr>
                <td align=left bgcolor="#dddfc3" width=50%>
                   <asp:Button ID="SbmJob" runat="server" Text="Submit Job" />
                </td>
                <td align=right bgcolor="#dddfc3" width=50%>
                   <asp:Button ID="DuckSystem" runat="server" Text="Return to Mail Request" CausesValidation=false />
                </td>
              </tr>
            </table>
            </div>
            </asp:Panel>
            <br />
        <div>
            <table border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                    <asp:Label id="SubmitJob" runat="server" CssClass="jobsubmit" BorderWidth="0px" />
                </td>
              </tr>
            </table>
        </div>
    </div>

</asp:Content>
