<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatEntry001.aspx.vb" Inherits="CatEntry001" title="Catalog Item Selection" Strict = "False" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit"%>--%>
<%@ REGISTER tagprefix="telerik" assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" %>
<%@ Register Src="ProductDetails.ascx" TagName="ProductDetails" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgCatalog">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgCatalog" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadToolTipManager1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolTipManager runat="server" ID="RadToolTipManager1" Skin="Web20"
        Position="MiddleRight" AutoCloseDelay="9000" RelativeTo="Element" 
        onajaxupdate="OnAjaxUpdate" OffsetY="20" OffsetX="20">
    </telerik:RadToolTipManager>

  <asp:Panel ID="Panel1" runat="server" DefaultButton="btnNext" >
      <div>
        <ASP:Label id="lblStatus" runat="server" forecolor="Red" />
        <br />       
        <table style="width: 100%; height: 280px" cellpadding="5" >
            <tr>
                <td colspan="2" align="center">
                    <table style="width: 100%;">
                        <tr>
                            <td align="center">
                                <ASP:BUTTON id="btnExit" runat="server" text="Exit Decoy Bag" />
                            </td>
                            <td align="center">
                                <ASP:BUTTON id="btnNext" runat="server" text="Preview Decoy Bag" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color:DarkSeaGreen;" align="center">
                  <asp:Label ID="catGroup" runat="server" CssClass="group" ></asp:Label></td></tr>
            <tr>
                <td align="center" valign="top">
                    <ASP:REPEATER id="rptGrids" runat="server"  >
                        <ITEMTEMPLATE>
                            <ASP:BUTTON id="lnk1" runat="server" Width="150px" onclick="lnk1_Click" 
                                 text='<%# Container.DataItem( "catdesc" ).trim() %>' 
                                 commandargument='<%# Container.DataItem("catgrp" ) %>' /> 
                        </ITEMTEMPLATE>
                        <SEPARATORTEMPLATE><br /></SEPARATORTEMPLATE>
                    </ASP:REPEATER>
                </td>
                <td valign="top" >
                    <asp:Panel ID="PanelGrid" runat="server">
                    <div style="width:100%;">
                        <telerik:RadGrid ID="dgCatalog" runat="server" AutoGenerateColumns="false" Width="100%" > 
                        <MasterTableView DataKeyNames="ccnumb" CommandItemDisplay="None" EditMode="Batch">
                            <HeaderStyle BackColor="LightGreen" BorderWidth="1" ForeColor="Black" Font-Size="14pt" HorizontalAlign="center" />
                            <ItemStyle BorderColor="black" BorderWidth="1" />
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="ccnumb" Display="false" DataField="ccnumb" HeaderText="ID" Visible="true">
                                <HeaderStyle CssClass="FLOAT" BackColor="LightGreen" Font-Size="14pt" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Quantity" >
                                <ItemTemplate>
                                    <asp:TextBox TabIndex="1" id="txtQ" Width="25px"  runat="server" MaxLength="3" style="text-align:center" />
                                    <br />
                                    <asp:Label ID="multiAllow" runat="server" Visible="false" Text="Multiples Allowed" ></asp:Label>
                                    <br />
                                    <asp:RangeValidator id="RVMaxQty" runat="server" 
                                        ControlToValidate="txtQ"
                                        ForeColor="Red"
                                        display="dynamic"
                                        MinimumValue="0"
                                        MaximumValue="1"
                                        Type="Integer"
                                        SetFocusOnError="true"
                                        Text="Quantity greater than max allowed!">
                                    </asp:RangeValidator>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegExpQty" runat="server" 
                                        display="dynamic"
                                        ForeColor="Red"
                                        ErrorMessage="Invalid quantity"
                                        SetFocusOnError="true"
                                        Controltovalidate="txtQ"
                                        validationExpression="^[0-1]$">
                                    </asp:RegularExpressionValidator>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" BorderColor="Black" BorderWidth="1px"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="center" Font-Size="12pt" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="avalqty" Visible="false" HeaderText="Available" DataFormatString="{0:#,##0}">
                                <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" BorderColor="Black" BorderWidth="1px"
                                    Font-Strikeout="False" Font-Underline="False" Wrap="False" Font-Size="12pt" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="unitcost" HeaderText="Cost" DataFormatString="{0:#,##0.00}" ReadOnly="true"  >
                                <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" BorderColor="Black" BorderWidth="1px"
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" Font-Size="12pt" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="Image Preview">
                                <ItemTemplate>
                                    <ASP:image id="img1" Width="75px" Height="75px" runat="server"/>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" BorderColor="Black" BorderWidth="1px"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="center" Font-Size="12pt" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ccnumb" HeaderText="Item" ReadOnly="true" >
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" BorderColor="Black" BorderWidth="1px"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="center" Font-Size="12pt" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="itmdsc" HeaderText="Item Description" ReadOnly="true" >
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" BorderColor="Black" BorderWidth="1px"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="left" Font-Size="12pt" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn datafield="ccpzon" headertext="Category" Display="false" visible="true" >
                                </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" Visible="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />                
                        <AlternatingItemStyle CssClass="ALT-BROWSE-2" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle  CssClass="ALT-BROWSE-1" BorderWidth="1" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle  Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        </MasterTableView>
                        </telerik:RadGrid>                    
                    </div>
                    </asp:Panel>
                    <asp:Panel ID="ttipsHolder" runat="server">
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
  </asp:Panel>
</asp:Content>
