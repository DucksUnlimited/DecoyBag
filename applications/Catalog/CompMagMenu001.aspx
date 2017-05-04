<%@ Page Language="VB" MasterPageFile="~/DUIMasterPage.master" AutoEventWireup="false" CodeFile="CompMagMenu001.aspx.vb" Inherits="CompMagMenu001" title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="center">    
        <table width="50%" align="center" style="height:50px;">
            <tr>
                <td width="45%"></td>
                <td align="center">
                    <telerik:RadMenu ID="RadMenu1" runat="server" Skin="Vista"  Font-Size="14px">
                        <Items>
                            <telerik:RadMenuItem text="Comp/Mag" BorderWidth="1px" BorderStyle="Groove"
                                 Font-Size="14px" runat="server">
                                <Items>
                                    <telerik:RadMenuItem Text="File Maintenance" runat="server"
                                        NavigateUrl="CompMagMaint001.aspx" ImageUrl="Images/Excel-16.gif" >
                                    </telerik:RadMenuItem> 
                                    <telerik:RadMenuItem Text="State Counts" runat="server"
                                        NavigateUrl="CompMagRpt001.aspx" ImageUrl="Images/Excel-16.gif" >
                                    </telerik:RadMenuItem> 
                                    <telerik:RadMenuItem Text="Last Send Info" runat="server"
                                        NavigateUrl="CompMagRpt002.aspx" ImageUrl="Images/Excel-16.gif" >
                                    </telerik:RadMenuItem> 
                                    <telerik:RadMenuItem Text="Submit File Transfer" runat="server"
                                        NavigateUrl="CompMagCL1.aspx" ImageUrl="Images/Excel-16.gif" >
                                    </telerik:RadMenuItem> 

                                </Items>
                            </telerik:RadMenuItem>
                        </Items>
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </telerik:RadMenu>
                </td>
                <td width="10%"></td>
            </tr>
        </table>
    
    
    </div>


</asp:Content>

