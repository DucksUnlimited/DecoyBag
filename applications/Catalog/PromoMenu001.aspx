<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="PromoMenu001.aspx.vb" Inherits="PromoMenu001" title="Promotions" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <div style="align-self:center;">    
        <table style="width:50%;height:50%;">
            <tr>
                <td align="center" style="width:40%;"></td>
                <td align="center" style="width:20%;">
                    <telerik:RadMenu ID="RadMenu1" runat="server" Skin="Vista"  Font-Size="14px">
                        <Items>
                            <telerik:RadMenuItem text="Inventory" BorderWidth="1px" BorderStyle="Groove"
                                 Font-Size="14px" runat="server">
                                <Items>
                                    <telerik:RadMenuItem text="Promo Maintenance" runat="server">
                                        <Items>
                                            <telerik:RadMenuItem Text="Group Maintenance"
                                                NavigateUrl="CatGroup001.aspx" runat="server" >
                                            </telerik:RadMenuItem>
                                            <telerik:RadMenuItem Text="Catalog Maintenance"
                                                NavigateUrl="CatMaint001.aspx" runat="server" >
                                            </telerik:RadMenuItem>
                                        </Items>    
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem Text="Event Merchandise Sum" runat="server"
                                        NavigateUrl="EventInvSum.aspx" ImageUrl="Images/Excel-16.gif" >
                                    </telerik:RadMenuItem> 
                                    <telerik:RadMenuItem Text="Return Codes" runat="server"
                                        NavigateUrl="ReturnCodes001.aspx" ImageUrl="Images/Excel-16.gif" >
                                    </telerik:RadMenuItem> 
                                </Items>
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem text="Catalog" BorderWidth="1px" BorderStyle="Groove"
                                 Font-Size="14px" runat="server">
                                <Items>
                                    <telerik:RadMenuItem Text="New Order Entry Point"
                                        NavigateUrl="CatEntryLoad.aspx" runat="server" >
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem Text="RD Entry Point"
                                        NavigateUrl="CatEntryLoad2.aspx" runat="server" >
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>
                        </Items>
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </telerik:RadMenu>
                </td>
                <td style="width:40%"></td>
            </tr>
        </table>
    
    
    </div>
</center>
</asp:Content>

