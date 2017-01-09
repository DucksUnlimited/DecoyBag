<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DB002.aspx.vb" Inherits="DB002" Strict="false" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit"%>
<%@ REGISTER tagprefix="telerik" assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <META http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
    <META name="GENERATOR" content="IBM Software Development Platform">
    <META http-equiv="Content-Style-Type" content="text/css">

    <link rel="stylesheet" href="http://www.ducksystem.com/NATURAL/99723934.CSS" type="text/css"/>
    <link rel="stylesheet" href="http://www.ducksystem.com/NATURAL/99723933.CSS" type="text/css" media="print"/>
    
    <style type="text/css">
      .validatorCalloutHighlight
      {
         background-color: lemonchiffon;
      }
      .group
      {
         background-color: DarkSeaGreen;
         font-size: 26pt;
      } 
      #TIMESTAMPP                    
      {
         float: right;
         font-size: 6pt;
      }
      #TIMESTAMPS                    
      {
         float: right;
         font-size: 6pt;
      }
    </style>
    
    <title>***** Duck Bag *****</title>
</head>
<body leftmargin="0" topmargin="0">
    <div id="CONTAINER">
        <div id="HEADER">
          <div id="DUCKHEAD"><a><img src="http://www.ducksystem.com/NATURAL/99725393.JPG" border=0></a></div>
          <div class='SCREEN-HIDE' style="position:relative; float: left;"><img src="http://www.ducksystem.com/NATURAL/99875006.GIF" border=0 width=45 height=30></div>
          <div id="SYSTEM_INFO" align="right">
            <div id="TITLE" align="left">
                <asp:Label ID="Title" runat="server"></asp:Label></div>
        <div id="TIMESTAMPP" class=SCREEN-HIDE>
            <asp:Label ID="DatePH" runat="server"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="TimePH" runat="server"></asp:Label>
        </div>  <!--- ending timestamp div -->
        <div id="TIMESTAMPS" class=PRINTER-HIDE>
            <asp:Label ID="DateSH" runat="server"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="TimeSH" runat="server"></asp:Label>
        </div>  <!--- ending timestamp div -->
            <div class="SPACER"></div>
          </div>
        </div>
        <div class="SPACER"></div>
        <center>
            <div id="APPLICATION">
                <form id="form1" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <telerik:RadToolTipManager runat="server" ID="RadToolTipManager1" Skin="Web20"
                        Position="MiddleRight" AutoCloseDelay="4000"
                        OffsetY="20"
                        OffsetX="20"></telerik:RadToolTipManager>
                      <div>
                        <ASP:Label id="lblStatus" runat="server" forecolor="Red" />
                    <asp:Panel ID="panelCatalog" runat="server" Height="450px" Visible="False" Width="98%" Wrap="False">
                                         
                    <table style="width: 98%; height: 280px" cellpadding="5" >
                    <tr>
                        <td colspan=2 bgcolor=DarkSeaGreen>
                          <asp:Label ID="catGroup" runat="server" CssClass="group"></asp:Label></td></tr>
                    <tr>
                        <td align="left" valign="top">
		                    <ASP:REPEATER id="rptGrids" runat="server" >
			                    <ITEMTEMPLATE>
				                    <ASP:BUTTON id="lnk1" runat="server" Width="150px" onclick="lnk1_Click" 
				                         text='<%# Container.DataItem( "catdesc" ) %>' 
				                         commandargument='<%# Container.DataItem("catgrp" ) %>' /> 
			                    </ITEMTEMPLATE>
			                    <SEPARATORTEMPLATE><br /></SEPARATORTEMPLATE>
		                    </ASP:REPEATER>
                        </td>
                        
                        
                        <td valign="top" >
		
			 <table style="width:750px;" cellpadding="0" cellspacing="0" border="1">
			 <tr>
			 <td style="width:75px;Font-Size:14pt;text-align:center" BgColor=LightGreen>Quantity</td>
			 <td style="width:75px;Font-Size:14pt;text-align:center" BgColor=LightGreen>Available</td>
			 <td style="width:76px;Font-Size:14pt" BgColor=LightGreen>Cost</td>
			 <td style="width:91px;Font-Size:14pt" BgColor=LightGreen>Image <br /> Preview</td>
			 <td style="width:55px;Font-Size:14pt" BgColor=LightGreen>Item</td>
			 <td style="Font-Size:14pt" BgColor=LightGreen>Description</td>
			 </tr>
			 </table>
                          <div style="width:750px;height:352px;overflow:scroll;">
                            <ASP:DATAGRID id="dgCatalog"  runat="server" autogeneratecolumns="False" 
                                    onitemdatabound="dgCatalog_ItemDataBound" showheader="false" >
                                <COLUMNS>
              						<ASP:BOUNDCOLUMN datafield="ccnumb" headertext="ID" visible="False" itemstyle-width="100px" >
                                         <HeaderStyle CssClass="FLOAT" BackColor=LightGreen Font-Size=14pt />
                                      </ASP:BOUNDCOLUMN>
                                    <ASP:TEMPLATECOLUMN headertext="Quanity" itemstyle-width="80px" >
                                        <ITEMTEMPLATE>
                                            <ASP:TEXTBOX TabIndex=1 id="txtQ" Width="25px" runat="server" MaxLength=4 style="text-align:center"  />
                                        </ITEMTEMPLATE>
                                        <HeaderStyle CssClass="FLOAT" BackColor=LightGreen Font-Size=14pt />
                                        <ItemStyle HorizontalAlign="center" />
                                    </ASP:TEMPLATECOLUMN>
                                    <ASP:BOUNDCOLUMN datafield="avalqty" headertext="Available" itemstyle-width="80px" DataFormatString="{0:#,##0}"  >
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                          Font-Strikeout="False" Font-Underline="False" Wrap="False"  />
                                        <HeaderStyle CssClass="FLOAT" BackColor=LightGreen Font-Size=14pt />
                                    </ASP:BOUNDCOLUMN>
                                    <ASP:BOUNDCOLUMN datafield="ipocst" headertext="Cost" itemstyle-width="80px" DataFormatString="{0:#,##0.00}"  >
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                             Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <HeaderStyle CssClass="FLOAT" BackColor=LightGreen Font-Size=14pt />
                                    </ASP:BOUNDCOLUMN>
                                    <ASP:TEMPLATECOLUMN headertext="Image Preview" itemstyle-width="93px">
                                        <ITEMTEMPLATE>
                                            <ASP:LITERAL id="litLink" runat="server" />
                                            <ASP:IMAGE TabIndex=0 id="img1" runat="server" Width="75px" Height="75px" />
                                        </ITEMTEMPLATE>
                                        <HeaderStyle CssClass="FLOAT" BackColor=LightGreen Font-Size=14pt />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" HorizontalAlign="center" />
                                    </ASP:TEMPLATECOLUMN>
                                    <ASP:BOUNDCOLUMN datafield="ccnumb" headertext="Item" itemstyle-width="57px"  >
                                        <HeaderStyle CssClass="FLOAT" BackColor=LightGreen Font-Size=14pt />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" HorizontalAlign="center" />
                                    </ASP:BOUNDCOLUMN>
                                    <ASP:BOUNDCOLUMN datafield="itmdsc" headertext="Item Description" itemstyle-width="357px" >
                                        <HeaderStyle CssClass="FLOAT" BackColor=LightGreen Font-Size=14pt />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" HorizontalAlign="center" />
                                    </ASP:BOUNDCOLUMN>
                                    <ASP:BOUNDCOLUMN datafield="it11dv" headertext="Category" visible="False" itemstyle-width="100px" >
                                         <HeaderStyle CssClass="FLOAT" BackColor=LightGreen Font-Size=14pt />
                                      </ASP:BOUNDCOLUMN>
                                    
                                </COLUMNS>
                                <PagerStyle Mode="NumericPages" Visible="False" />
                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <AlternatingItemStyle CssClass="ALT-BROWSE-2" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <ItemStyle  CssClass="ALT-BROWSE-1" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle  Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </ASP:DATAGRID>
                          </div>
                        </td></tr></table>
                        <br />
                        <br />
                        
                        <ASP:BUTTON id="btnNext" runat="server" text="Preview Decoy Bag" />
                                <div class="SPACER"></div>

                    </asp:Panel>
                    </div>
                    <div>
                    <asp:Panel ID="panelDuckBagCart" runat="server" Height="475px" Visible="False" Width="98%" Wrap="False">
                     <table style="width: 98%; height: 200px" cellpadding="5" >
                       <tr><td>
                        <div id=FLOAT>
                        <ASP:LITERAL id="litPreview" runat="server" />
                        </div></td></tr></table>
                        <br /><br />
                        <ASP:BUTTON id="btnEdit" runat="server" text=" Edit Decoy Bag " />
                        &nbsp;&nbsp;
                        <ASP:BUTTON id="btnCancelPreview" runat="server" text=" Cancel Decoy Bag " />
                        &nbsp;&nbsp; &nbsp;&nbsp;
                        <ASP:BUTTON id="btnSubmit" runat="server" text=" Close Decoy Bag " /> 

                    </asp:Panel>
                          &nbsp;
                    <div>
                        &nbsp;&nbsp;</div>
                 </div>
                </form>
                    <ASP:PANEL id="panelMessage" runat="server">
                        <ASP:LITERAL id="litMessage" runat="server" />
                    </ASP:PANEL>
            </div>
        </center>
        <div>
        <table width="750" cellSpacing="0" cellPadding="0" border="0">
          <tr>
            <td height="2px"></td>
          </tr>
          <tr>
            <td width="10">
              <table style="width: 750px" cellSpacing=0 cellPadding=0 border=0>
                <tr>
                  <td width="10" class="footer" style="height: 20px"></td>
                  <td width="200" class="footer" vAlign="middle" align="left" style="height: 20px"><span style="COLOR: #666666">&#169;Ducks Unlimited, Inc.</span></td>
                  <td width="540" class="footer" style="padding-right: 10px; height: 20px;" vAlign="middle" align="right"><a href="http://www.ducks.org/contactdu.aspx">Contact Us</a>&nbsp;| <a href="http://www.ducks.org/About_DU/AboutDucksUnlimited/2103/PrivacyStatement.html">Privacy</a>&nbsp;|&nbsp;<a href="http://www.ducks.org/jobs.aspx">Jobs</a> | <a href="http://www.ducks.org/faq.aspx">FAQ's</a>&nbsp;| <a href="http://www.ducks.org/About_DU/AboutDucksUnlimitedHome/2359/FinancialInformation.html">Financials</a></td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
    </div>
    <div class="SPACER"></div>
    </div>
</body>
</html>
