Imports IBM.Data.DB2.iSeries
Imports iSeriesDB.iSeriesCatalog
Imports System.Data

Partial Class CatEntryPDF2
    Inherits System.Web.UI.Page

    Public parms As New ClassSessionManager

    Dim tQtyOrd As Integer = 0
    Dim tExtCost As Decimal = CDec(0.0)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            parms = Session("oSessionManager")
            parms.parseQueryString(Request)
            cbApprove.Visible = False
            tbPending.Visible = False
            If parms.UpDtOrder = "Y" Then
                UpdateOrd.Text = "Order Updated"
                If parms.Approved = "Y" Then
                    UpdateOrd.Text = "Approved Updated"
                    cbApprove.Checked = True
                    cbApprove.Visible = True
                End If
            Else
                tbPending.Visible = True
                UpdateOrd.Text = "Pending Order"
            End If
            SystemTitle.Text = "Order - " + parms.CurOrder
            EventTitle.Text = (parms.EventName + "  -  " + parms.EventID)
            EventTitle.Text += ("           " + parms.EventDate.Substring(4, 2) + "/" + parms.EventDate.Substring(6, 2))
            EventTitle.Text += ("/" + parms.EventDate.Substring(0, 4))
            PageTitle.Text = "Order Receipt"


            'Format date time info for page.
            'DatePH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
            'TimePH.Text = DateTime.Now.ToString("h:mm:ss - tt")

            'Get bill to & ship to information for order header
            Dim drOrdHdr As iDB2DataReader

            'Build key for Order Header information 
            'Key is order number
            drOrdHdr = LoadOrderHeader(parms.CurOrder)

            'Check to see if header was read
            If drOrdHdr.Read Then

                'bill to
                LBLbillchapter.Text = drOrdHdr("htcno").ToString
                LBLobname.Text = drOrdHdr("htbnam").ToString
                LBLobadd1.Text = drOrdHdr("htbad1").ToString
                LBLobadd2.Text = drOrdHdr("htbad2").ToString
                LBLobcsz.Text = (drOrdHdr("htbcty").ToString.Trim() + "    " + drOrdHdr("htbzip").ToString)
                If Not drOrdHdr("htbzp2").ToString = "    " Then
                    LBLobcsz.Text += (" - " + drOrdHdr("htbzp2").ToString)
                End If

                'ship to
                LBLshpchapter.Text = drOrdHdr("htsno").ToString
                LBLosname.Text = drOrdHdr("htsnam").ToString
                LBLosadd1.Text = drOrdHdr("htsad1").ToString
                LBLosadd2.Text = drOrdHdr("htsad2").ToString
                LBLoscsz.Text = (drOrdHdr("htscty").ToString.Trim() + "    " + drOrdHdr("htszip").ToString)
                If Not drOrdHdr("htszp2").ToString = "    " Then
                    LBLoscsz.Text += ("  -  " + drOrdHdr("htszp2").ToString)
                End If
                If parms.FFLPDF = "Y" Then
                    FFLPanel.Visible = True
                    'FFL Information
                    fflorg.Text = drOrdHdr("fflorg").ToString
                    fflattn.Text = drOrdHdr("fflattn").ToString
                    ffladdr.Text = drOrdHdr("ffladdr").ToString
                    fflcsz.Text = (drOrdHdr("fflcity").ToString.Trim() + "   " + drOrdHdr("fflst").ToString.Trim())
                    fflcsz.Text += ("    " + drOrdHdr("fflzip5").ToString)
                    If Not drOrdHdr("fflzip4").ToString = "    " And Not drOrdHdr("fflzip4").ToString = "" Then
                        fflcsz.Text += ("  -  " + drOrdHdr("fflzip4").ToString)
                    End If
                    fflnum.Text = drOrdHdr("fflnum").ToString
                    If Not drOrdHdr("fflexp") Is Nothing And Not drOrdHdr("fflexp") = "" And Not drOrdHdr("fflexp") = "        " Then
                        Dim wkdate As String = drOrdHdr("fflexp")
                        fflexp.Text = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                    End If
                End If
            End If

            'get order detail lines
            Dim dtDetail As iDB2DataReader = LoadOrderDetail(parms.CurOrder)
            'bind the order lines to the datagrid so we may see them
            GridView1.DataSource = dtDetail
            GridView1.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            tQtyOrd += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "htqor"))
            tExtCost += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cs33ec"))
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Totals:"
            e.Row.Cells(2).Text = tQtyOrd.ToString()
            e.Row.Cells(4).Text = tExtCost.ToString("c")
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub
End Class
