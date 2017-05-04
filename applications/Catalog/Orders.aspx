<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Orders.aspx.vb" Inherits="Orders" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
<meta name="GENERATOR" content="IBM Software Development Platform" />
<meta http-equiv="Content-Style-Type" content="text/css" />

<link rel="stylesheet" href="99723934.CSS" type="text/css"/>
<link rel="stylesheet" href="99723933.CSS" type="text/css" media="print"/>
<style type="text/css">
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
<title>Browes Orders</title>
</head>
<body >
  <div id="CONTAINER">

    <div id="HEADER">
      <div id="DUCKHEAD"><a><img src="http://ww1.ducksystem.com/NATURAL/99725393.JPG" border="0" alt="" /></a></div>
      <div class="SCREEN-HIDE" style="position:relative; float: left;"><img src="http://ww1.ducksystem.com/NATURAL/99875006.GIF" border="0" alt="" width="45" height="30" /></div>
      <div id="SYSTEM_INFO" align="right">
        <div id="TITLE" align="left">Order Inquiry</div>
        <div id="TIMESTAMPP" class="SCREEN-HIDE">
            <asp:Label ID="DatePH" runat="server"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="TimePH" runat="server"></asp:Label>
        </div>  <!--- ending timestamp div -->
        <div id="TIMESTAMPS" class="PRINTER-HIDE">
            <asp:Label ID="DateSH" runat="server"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="TimeSH" runat="server"></asp:Label>
        </div>  <!--- ending timestamp div -->
        <div class="SPACER"></div>
        <div align="center" style="width:100%; float:left; font-size:12pt;">
            <asp:Label ID="OrderStatus" runat="server" ></asp:Label>
        </div>
      </div>
    </div>
    <div class="SPACER"></div>
    <center>
      <div id="APPLICATION">
        <form id="form2" runat="server">
          <table style="width: 900px">
            <tr>
              <td style="width: 135px;" align="right">
                    <asp:Label ID="Label1" runat="server" Text="Order Number" Width="104px"></asp:Label></td>
              <td style="width: 154px;" align="left">
                    <asp:Label ID="lblhtnum" runat="server" Width="104px" ForeColor="DimGray"></asp:Label></td>
              <td style="width: 157px;"  align="left">
                    Ship To</td>
              <td style="width: 158px;" align="left">
                    <asp:Label ID="lblhtsno" runat="server" ForeColor="DimGray" Width="128px"></asp:Label></td>
              <td style="width: 118px;" align="left">
                    Bill To</td>
              <td style="width: 157px;" align="left">
                    <asp:Label ID="lblhtcno" runat="server" ForeColor="DimGray" Width="112px"></asp:Label></td>
            </tr>
            <tr>
              <td style="width: 135px;" align="right">
                    Original Order</td>
              <td style="width: 154px;" align="left">
                    <asp:Label ID="lblhtmdm4" runat="server" ForeColor="DimGray"></asp:Label></td>
              <td colspan="2" style="width: 157px;" align="left">
                    <asp:Label ID="lblhtsnam" runat="server" ForeColor="DimGray" Width="304px"></asp:Label></td>
              <td colspan="2" style="width: 152px;" align="left">
                    <asp:Label ID="lblhtbnam" runat="server" ForeColor="DimGray" Width="248px"></asp:Label></td>
            </tr>
            <tr>
              <td style="width: 135px" align="right">
                    Sale Branch</td>
              <td style="width: 154px" align="left">
                    <asp:Label ID="lblcs21sb" runat="server" ForeColor="DimGray"></asp:Label></td>
              <td colspan="2" style="width: 157px" align="left">
                    <asp:Label ID="lblhtsad1" runat="server" ForeColor="DimGray" Width="304px"></asp:Label></td>
              <td colspan="2" style="width: 152px" align="left">
                    <asp:Label ID="lblhtbad1" runat="server" ForeColor="DimGray" Width="248px"></asp:Label></td>
            </tr>
            <tr>
              <td style="width: 135px" align="right">
                    Warehouse</td>
              <td style="width: 154px" align="left">
                    <asp:Label ID="lblhtorsc" runat="server" ForeColor="DimGray"></asp:Label></td>
              <td colspan="2" style="width: 157px" align="left">
                    <asp:Label ID="lblhtsad2" runat="server" ForeColor="DimGray" Width="304px"></asp:Label></td>
              <td colspan="2" style="width: 152px" align="left">
                    <asp:Label ID="lblhtbad2" runat="server" ForeColor="DimGray" Width="248px"></asp:Label></td>
            </tr>
            <tr>
              <td style="width: 135px" align="right">
                    Entry Date</td>
              <td style="width: 154px" align="left">
                    <asp:Label ID="lblhtdte" runat="server" ForeColor="DimGray" ></asp:Label></td>
              <td colspan="" style="width: 157px" align="left">
                    <asp:Label ID="lblhtscty" runat="server" Width="144px" ForeColor="DimGray"></asp:Label></td>
              <td colspan="" style="width: 157px" align="left">
                    <asp:Label ID="lblhtszip" runat="server" Width="48px" ForeColor="DimGray"></asp:Label>
                    -
                    <asp:Label ID="lblhtszp2" runat="server" Width="40px" ForeColor="DimGray"></asp:Label></td>
              <td colspan="" style="width: 118px" align="left">
                    <asp:Label ID="lblhtbcty" runat="server" Width="112px" ForeColor="DimGray"></asp:Label></td>
              <td colspan="" style="width: 152px" align="left">
                    <asp:Label ID="lblhtbzip" runat="server" Width="48px" ForeColor="DimGray"></asp:Label>
                    -
                    <asp:Label ID="lblhtbzp2" runat="server" Width="40px" ForeColor="DimGray"></asp:Label></td>
            </tr>
            <tr>
              <td style="width: 135px" align="right">
                    Ship Date</td>
              <td style="width: 154px" align="left">
                    <asp:Label ID="lblhtdlby" runat="server" ForeColor="DimGray"></asp:Label></td>
              <td colspan="4" style="width: 157px" align="left"></td>
            </tr>
          </table>
          <table style="width: 900px">
            <tr>
              <td style="width: 152px; height: 21px">
                    Customer P.O. Number</td>
              <td style="width: 580px; height: 21px" align="left">
                    <asp:Label ID="lblhtcpo" runat="server" Width="552px" ForeColor="DimGray"></asp:Label></td>
              <td style="height: 21px" align="left">
                    <asp:Label ID="lblhtcodt" runat="server" ForeColor="DimGray"></asp:Label></td>
            </tr>
          </table>
          <br />
          
            <asp:Table ID="UPSTable" runat="server" Enabled="true">
                <asp:TableHeaderRow ID="thr" runat="server">
                    <asp:TableHeaderCell ID="thc" runat="server" Text="UPS Tracking Number"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="thc2" runat="server" Text="Gun Serial Number"></asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
          
          <br />
          <div id="FLOAT">
            <asp:Gridview ID="OrdLineView" runat="server" AutoGenerateColumns="False" CellSpacing="1" GridLines="None">
              <Columns>
                <asp:BoundField DataField="htllin" HeaderText="Line" Visible="False" >
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle CssClass="FLOAT" />
                </asp:BoundField>
                <asp:BoundField DataField="htqor" HeaderText="Ordered" >
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="FLOAT" />
                </asp:BoundField>
                <asp:BoundField DataField="htlqsh" HeaderText="Shipped" >
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="FLOAT" />
                </asp:BoundField>
                <asp:BoundField DataField="htlqbo" HeaderText="Bk Ord" >
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="FLOAT" />
                </asp:BoundField>
                <asp:BoundField DataField="htlun" HeaderText="UoM" >
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="FLOAT" />
                </asp:BoundField>
                <asp:BoundField DataField="htlpnm" HeaderText="Item #">
                    <HeaderStyle CssClass="FLOAT" />
                </asp:BoundField>
                <asp:BoundField DataField="htldsc" HeaderText="Description" >
                    <HeaderStyle CssClass="FLOAT" />
                </asp:BoundField>
                <asp:BoundField DataField="htlser" HeaderText="Gun Serial Number" Visible="true" >
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="FLOAT" />
                </asp:BoundField>
              </Columns>
            <HeaderStyle BackColor="LightSkyBlue" HorizontalAlign="Center" VerticalAlign="Bottom" CssClass="display: table-header-group" />
            <AlternatingRowStyle CssClass="ALT-BROWSE-2" />
            <RowStyle CssClass="ALT-BROWSE-1" />
            </asp:Gridview>

          </div>
        </form>    
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
</body>
</html>
