Imports IBM.Data.DB2.iSeries
Imports iSeriesDB.iSeriesCatalog
Imports System.Data
Partial Class Orders
    Inherits System.Web.UI.Page

    'Dim oiSeries = New ClassiSeriesDataAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'Format dates
        DatePH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
        TimePH.Text = DateTime.Now.ToString("h:mm:ss - tt")
        DateSH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
        TimeSH.Text = DateTime.Now.ToString("h:mm:ss - tt")

        'Load Order Header and Detail
        Dim dr As iDB2DataReader = LoadOrderHeaderP(Request("HTNUM"))
        Dim drOrdDtl As iDB2DataReader = LoadOrderDetailP(Request("HTNUM"))
        Dim dt As DataTable

        'Move order header fields to screen
        UPSTable.Visible = False
        If dr.Read Then
            If dr("HTRCFL").Equals("Q") Then
                OrderStatus.Text = "Approved Decoy Bag Order"
            Else
                If dr("htrcfl").Equals("R") Then
                    OrderStatus.Text = "Released to Warehouse"
                Else
                    If dr("htrcfl").Equals("P") Then
                        OrderStatus.Text = "Order Being Picked"
                    Else
                        If dr("htrcfl").Equals("H") Then
                            OrderStatus.Text = "Order on BackOrder"
                        Else
                            OrderStatus.Text = "Order Shipped"
                            dt = LoadUPSTracking(Request("HTNUM"))
                            Dim alt As Boolean
                            alt = False
                            If dt.Rows.Count > 0 Then
                                Dim i As Integer
                                Dim drw() As DataRow
                                drw = dt.Select()
                                For i = 0 To drw.GetUpperBound(0)
                                    Dim r As New TableRow()
                                    Dim c As New TableCell()
                                    Dim c2 As New TableCell()
                                    c.HorizontalAlign = HorizontalAlign.Center
                                    If (drw(i)("utrack").ToString().Substring(0, 2) = "No") Then
                                        c.Controls.Add(New LiteralControl(drw(i)("utrack").ToString()))
                                        c.ColumnSpan = 2
                                        r.Cells.Add(c)
                                    Else
                                        Dim UPSTrack As String = (drw(i)("utrack").ToString())
                                        'Dim upshref As String
                                        'upshref = "<a href=" + """http://wwwapps.ups.com/WebTracking/OnlineTool?InquiryNumber1=" + UPSTrack.Trim + "&UPS_HTML_License=CC3B576D07929CD8&UPS_HTML_Version=3.0&TypeOfInquiryNumber=T""" + "target=" + """_blank""" + ">" + UPSTrack.Trim + "</a>"
                                        'c.Controls.Add(New LiteralControl(upshref.ToString()))
                                        Dim upshref As New System.Web.UI.WebControls.HyperLink
                                        upshref.Text = (drw(i)("utrack").ToString())
                                        If UPSTrack.Substring(0, 2) = "1Z" Then
                                            upshref.NavigateUrl = "http://wwwapps.ups.com/WebTracking/OnlineTool?InquiryNumber1=" + UPSTrack.Trim
										Else
                                            upshref.NavigateUrl = "https://www.fedex.com/apps/fedextrack/?tracknumbers=" + UPSTrack.Trim
										End If
                                        upshref.Target = "_blank"
                                        c.Controls.Add(upshref)
                                        r.Cells.Add(c)
                                        c2.Controls.Add(New LiteralControl(drw(i)("utser").ToString()))
                                        c2.HorizontalAlign = HorizontalAlign.Center
                                        r.Cells.Add(c2)
                                    End If
                                    If alt Then
                                        r.CssClass = "ALT-BROWSE-2"
                                        alt = False
                                    Else
                                        r.CssClass = "ALT-BROWSE-1"
                                        alt = True
                                    End If
                                    UPSTable.Rows.Add(r)
                                Next i
                            End If
                            UPSTable.Visible = True
                        End If
                    End If
                End If
            End If
            lblhtnum.Text = dr("HTNUM")
            lblhtmdm4.Text = dr("HTMDM4")
            lblcs21sb.Text = dr("CS21SB")
            lblhtorsc.Text = dr("HTORSC")
            lblhtdte.Text = dr("HTDTE").ToString.Substring(0, 4) + "/" + dr("HTDTE").ToString.Substring(4, 2) + "/" + dr("HTDTE").ToString.Substring(6, 2)
            lblhtsno.Text = dr("HTSNO")
            lblhtsnam.Text = dr("HTSNAM")
            lblhtsad1.Text = dr("HTSAD1")
            lblhtsad2.Text = dr("HTSAD2")
            lblhtscty.Text = dr("HTSCTY")
            lblhtszip.Text = dr("HTSZIP")
            lblhtszp2.Text = dr("HTSZP2")
            lblhtcno.Text = dr("HTCNO")
            lblhtbnam.Text = dr("HTBNAM")
            lblhtbad1.Text = dr("HTBAD1")
            lblhtbad2.Text = dr("HTBAD2")
            lblhtbcty.Text = dr("HTBCTY")
            lblhtbzip.Text = dr("HTBZIP")
            lblhtbzp2.Text = dr("HTBZP2")
            Dim shpdate As String = dr("HTDLBY").ToString()
            If (shpdate Is Nothing Or shpdate = "0") Then
                lblhtdlby.Text = "          "
            Else
                lblhtdlby.Text = dr("HTDLBY").ToString.Substring(0, 4) + "/" + dr("HTDLBY").ToString.Substring(4, 2) + "/" + dr("HTDLBY").ToString.Substring(6, 2)
            End If
            lblhtcpo.Text = dr("HTCPO")
            lblhtcodt.Text = dr("HTCODT").ToString.Substring(0, 4) + "/" + dr("HTCODT").ToString.Substring(4, 2) + "/" + dr("HTCODT").ToString.Substring(6, 2)
        End If

        'Bind order details to grid
        OrdLineView.DataSource = drOrdDtl
        OrdLineView.DataBind()

    End Sub

End Class
