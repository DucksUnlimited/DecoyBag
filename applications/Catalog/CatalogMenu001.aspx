<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CatalogMenu001.aspx.vb" Inherits="CatalogMenu001" title="Catalog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align=center>    
        <table width=50% height=50px>
            <tr>
                <td width=30%></td>
                <td align=center>
                    <telerik:RadMenu ID="RadMenu1" runat="server" Skin="Outlook" Font-Size="14pt">
                        <Items>
                            <telerik:RadMenuItem text="Promo Maintenance" BorderWidth=1 BorderStyle=Groove
                                 Font-Size=14px Enabled=true>
                                <Items>
                                    <telerik:RadMenuItem Text="Group Maintenance"
                                        NavigateUrl=CatGroup001.aspx >
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem Text="Catalog Maintenance"
                                        NavigateUrl=CatMaint001.aspx >
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>
                            <telerik:RadMenuItem text="Catalog" BorderWidth=1 BorderStyle=Groove
                                 Font-Size=14px>
                                <Items>
                                    <telerik:RadMenuItem Text="New Order Entry Point"
                                        NavigateUrl=CatEntryLoad3.aspx >
                                    </telerik:RadMenuItem>
                                    <telerik:RadMenuItem Text="RD Entry Point"
                                        NavigateUrl=CatEntryLoad2.aspx Enabled=true >
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenu>
                </td>
                <td width=10%></td>
            </tr>
        </table>
    
    
    </div>
</asp:Content>

