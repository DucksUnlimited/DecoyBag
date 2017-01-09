<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="FieldOpsMenu001.aspx.vb" Inherits="FieldOpsMenu001" title="Untitled Page" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align=center>    
        <table width=50% height=50>
            <tr>
                <td width=30%></td>
                <td align=center>
                    <telerik:RadMenu ID="RadMenu1" runat="server" Skin="Vista"  Font-Size=14px>
                        <Items>
                            <telerik:RadMenuItem text="FieldOps" BorderWidth=1px BorderStyle=Groove
                                 Font-Size=14px runat="server">
                                <Items>
                                    <telerik:RadMenuItem Text="Quoted Orders" runat="server"
                                        NavigateUrl=QuotedOrders.aspx ImageUrl="Images/Excel-16.gif" >
                                    </telerik:RadMenuItem> 
                                    <telerik:RadMenuItem Text="Held Orders" runat="server"
                                        NavigateUrl=HeldOrders.aspx ImageUrl="Images/Excel-16.gif" >
                                    </telerik:RadMenuItem> 
                                </Items>
                            </telerik:RadMenuItem>
                        </Items>
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </telerik:RadMenu>
                </td>
                <td width=10%></td>
            </tr>
        </table>
    
    
    </div>



</asp:Content>

