<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetSPCost.aspx.vb" Inherits="GetSPCost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
        <meta name="GENERATOR" content="IBM Software Development Platform" />
        <meta http-equiv="Content-Style-Type" content="text/css" />

        <link rel="stylesheet" href="99723934.CSS" type="text/css"/>
        <link rel="stylesheet" href="99723933.CSS" type="text/css" media="print"/>
<%--        <link rel="stylesheet" href="http://ww1.ducksystem.com/NATURAL/99723933.CSS" type="text/css" media="print"/>--%>
        <style type="text/css">
            .tfooter {
                background-image: none;
	            /*background-color: #768666;*/
        	    border-top: dotted 1px #000;
	            border-bottom: solid 1px #000;
	            border-right: solid 1px #768666;
	            color: #000000;
	            /*display: block;*/
	            float: none;
                font-size: 14pt;
                text-align: right;
            }
            #APPLICATION .ALT-BROWSE-1 {
	            text-align: left;
	            line-height: 1;
            }
	        #APPLICATION .ALT-BROWSE-2 {
	            text-align: left;
	            line-height: 1;
            }
            #APPLICATION .ALT-BROWSE-3 {
	            text-align: left;
	            line-height: 1;
            }
            #TIMESTAMPP {
                float: right;
                font-size: 6pt;
            }
            #TIMESTAMPS {
                float: right;
                font-size: 6pt;
            }
            TD
            {
                margin: 4px;
                padding: 5px;
            }

        </style>
        <title>Event Merchandise</title>
    </head>
    <body>
      <form runat="server">
        <div id="CONTAINER">
            <div id="HEADER">
                <div id="DUCKHEAD"><a><img src="http://ww1.ducksystem.com/NATURAL/99725393.JPG" border="0" alt=" " /></a></div>
                <div class="SCREEN-HIDE" style="position:relative; float: left;"><img src="http://ww1.ducksystem.com/NATURAL/99875006.GIF" border="0" width="45" height="30" alt=" " /></div>
                <div id="SYSTEM_INFO">
                    <div id="TITLE"> 
                        <asp:Label ID="PageTitle" runat="server"></asp:Label>
                    </div>
                    <div id="TIMESTAMPP" class="SCREEN-HIDE" align="right">
                        <asp:Label ID="DatePH" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="TimePH" runat="server"></asp:Label>
                    </div>  <!--- ending timestampp div -->
                    <div id="TIMESTAMPS" class="PRINTER-HIDE" align="right">
                        <asp:Label ID="DateSH" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="TimeSH" runat="server"></asp:Label>
                    </div>  <!--- ending timestamps div -->
                    <div class="SPACER"></div>
                </div>
            </div> 
            <div class="SPACER"></div>
            <center>
            <div id="APPLICATION">
                <div id="FLOAT">
                    <asp:Gridview ID="OrdLineView" runat="server" AutoGenerateColumns="False" CellSpacing="1" GridLines="None"
                        ShowFooter="true" OnRowDataBound="OrdLineView_RowDataBound">
                      <Columns>
                        <asp:BoundField DataField="ItemNum" HeaderText="Item #">
                            <ItemStyle CssClass="FONT-4" Font-Size="12pt" />
                            <HeaderStyle CssClass="FLOAT" Font-Size="14pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="qtyordered" HeaderText="Qty Ordered" >
                            <ItemStyle HorizontalAlign="right" CssClass="FONT-4" Font-Size="12pt" />
                            <HeaderStyle CssClass="FLOAT" Font-Size="14pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="description" HeaderText="Description" >
                            <ItemStyle HorizontalAlign="left" CssClass="FONT-4" Font-Size="12pt" />
                            <HeaderStyle CssClass="FLOAT" Font-Size="14pt" />
                        </asp:BoundField>
                          <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:HyperLink ID="ordlink" runat="server" CssClass="FONT-4" Font-Size="12pt" Text='<%# Eval("htnum") %>'>
                                  </asp:HyperLink>
                              </ItemTemplate>
                          </asp:TemplateField>
<%--                        <asp:BoundField DataField="htnum" HeaderText="Order #" >
                            <ItemStyle HorizontalAlign="Center" CssClass="FONT-4" Font-Size="12pt" />
                            <HeaderStyle CssClass="FLOAT" Font-Size="14pt" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="unitcost" HeaderText="Item Cost" >
                            <ItemStyle HorizontalAlign="right" CssClass="FONT-4" Font-Size="12pt" />
                            <HeaderStyle CssClass="FLOAT" Font-Size="14pt" />
                            <FooterStyle CssClass="tfooter" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EXTSALEsCOST" HeaderText="Committee Cost" >
                            <ItemStyle HorizontalAlign="right" CssClass="FONT-4" Font-Size="12pt" />
                            <HeaderStyle CssClass="FLOAT" Font-Size="14pt" />
                            <FooterStyle CssClass="tfooter" />
                        </asp:BoundField>
                      </Columns>
                      <HeaderStyle BackColor="LightSkyBlue" HorizontalAlign="Center" VerticalAlign="Bottom" CssClass="display: table-header-group" />
                      <FooterStyle BackColor="LightSkyBlue" HorizontalAlign="Center" VerticalAlign="Bottom" CssClass="tfooter"  />
                      <AlternatingRowStyle CssClass="ALT-BROWSE-2" />
                      <RowStyle CssClass="ALT-BROWSE-1" />
                    </asp:Gridview>
                </div>
            </div>
        </center>
    <table width="900" cellspacing="0" cellpadding="0" border="0">
      <tr>
        <td height="2px"></td>
      </tr>
      <tr>
        <td width="10">
          <table style="width: 750px" cellspacing="0" cellpadding="0" border="0">
            <tr>
              <td width="10" class="footer" style="height: 20px"></td>
              <td width="200" class="footer" valign="middle" align="left" style="height: 20px"><span style="COLOR: #666666">&#169;Ducks Unlimited, Inc.</span></td>
              <td width="540" class="footer" style="padding-right: 10px; height: 20px;" valign="middle" align="right"><a href="http://www.ducks.org/contactdu.aspx">Contact Us</a>&nbsp;| <a href="http://www.ducks.org/About_DU/AboutDucksUnlimited/2103/PrivacyStatement.html">Privacy</a>&nbsp;|&nbsp;<a href="http://www.ducks.org/jobs.aspx">Jobs</a> | <a href="http://www.ducks.org/faq.aspx">FAQ's</a>&nbsp;| <a href="http://www.ducks.org/About_DU/AboutDucksUnlimitedHome/2359/FinancialInformation.html">Financials</a></td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </div>
 </form>
</body>
</html>
