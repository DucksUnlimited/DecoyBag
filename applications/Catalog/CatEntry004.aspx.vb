Imports System.Data

Partial Class CatEntry004
    Inherits System.Web.UI.Page

    Public parms As New ClassSessionManager

    Dim tQtyOrd As Integer = 0
    Dim ExtCost As Decimal = CDec(0.0)
    Dim tExtCost As Decimal = CDec(0.0)


#Region "Properties"

    Private Property DTCats() As DataTable
        Get
            If Session("DTCats") Is Nothing Then
                Session.Add("DTCats", New DataTable())
            End If

            Return DirectCast(Session("DTCats"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("DTCats") = value
        End Set
    End Property

    Private Property DT() As DataTable
        Get
            If Session("DT") Is Nothing Then
                Session.Add("DT", New DataTable())
            End If

            Return DirectCast(Session("DT"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("DT") = value
        End Set
    End Property

    Private Property DTOrder() As DataTable
        Get
            If Session("DTOrder") Is Nothing Then
                Session.Add("DTOrder", New DataTable())
            End If

            Return DirectCast(Session("DTOrder"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("DTOrder") = value
        End Set
    End Property

#End Region

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'retrieve default Ship To and Bill To information 
        Try

            parms = Session("oSessionManager")
            If Session("UpdateOrder") Then
                Session("PageTitle") = "Order -  " + parms.CurOrder + "  -  Updated"
                If Session("Approved") Then
                    Session("PageTitle") = "Order -  " + parms.CurOrder + "  -  Approved"
                End If
            Else
                Session("PageTitle") = " Order -  " + parms.CurOrder + "  - Added"
            End If
            Dim sOrder = parms.CurOrder
            'If initial page load and not a postback or page refresh, 
            'reload our data. 
            If Not Page.IsPostBack And Session("BPostBack") <> True Then

                LBLbillchapter.Text = Session("LBLbillchapter")
                LBLcsname.Text = Session("LBLcsname")
                LBLcsadd1.Text = Session("LBLcsadd1")
                LBLcsadd2.Text = Session("LBLcsadd2")
                LBLcsctst.Text = Session("LBLcsctst")
                LBLcszipa.Text = Session("LBLcszipa")
                LBLcszip2.Text = Session("LBLcszip2")
                'ship to
                LBLshpchapter.Text = Session("LBLshpchapter")
                LBLcshp1.Text = Session("LBLcshp1")
                LBLcshp2.Text = Session("LBLcshp2")
                LBLcshp3.Text = Session("LBLcshp3")
                LBLcshp6.Text = Session("LBLcshp6")
                LBLcshpza.Text = Session("LBLcshpza")
                LBLcshpz2.Text = Session("LBLcshpz2")
                comments.Text = Session("Comments")
                If Session("FFLRequired") Then
                    FFLPanel.Visible = True
                    'FFL Information
                    If Not Session("FFLOrganization") Is Nothing Then
                        fflorg.Text = Session("FFLOrganization").ToString
                        fflattn.Text = Session("FFLName").ToString
                        ffladdr.Text = Session("FFLAddr").ToString
                        fflcsz.Text = Session("FFLCity").ToString.Trim() + "    " + Session("FFLState").ToString().Trim()
                        fflcsz.Text += "       " + Session("FFLZip5").ToString.Trim()
                        If Not Session("FFLZip4").ToString = "    " And Not Session("FFLZip4").ToString = "" Then
                            fflcsz.Text += ("  -  " + Session("FFLZip4").ToString.Trim())
                        End If
                        fflnum.Text = Session("FFLNumber").ToString
                        If Not Session("FFLExpirDate") Is Nothing And Not Session("FFLExpirDate") = "" And Not Session("FFLExpirDate") = "        " Then
                            Dim wkdate As String = Session("FFLExpirDate")
                            fflexp.Text = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                        End If
                    End If
                End If

                'get order detail lines
                'Dim dtDetail = oiSeries.LoadOrderDetail(parms.CurOrder)
                'bind the order lines to the datagrid so we may see them
                GridView1.DataSource = DTOrder
                GridView1.DataBind()

            End If

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try

            'Remove tables
            DT.Clear()
            DTOrder.Clear()
            DTCats.Clear()
            DT().Dispose()
            DTOrder().Dispose()
            DTCats().Dispose()
            Dim urlStart As String = Session("UrlStart")
            Session.Abandon()
            Response.Redirect(urlStart)


            'Redirect to location that started the catalog program CatEntry001.aspx  
            'Response.Redirect(Session("UrlStart"))


        Catch ex As Exception

            'Save error message for display on master page
            lblError.Text = ex.Message

        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            tQtyOrd += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Qty"))
            ExtCost = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Qty")) * Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Cost"))
            e.Row.Cells(4).Text = ExtCost.ToString()
            tExtCost += ExtCost
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Totals:"
            e.Row.Cells(2).Text = tQtyOrd.ToString()
            e.Row.Cells(4).Text = tExtCost.ToString("c")
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            'e.Row.Font.Bold = True
        End If
    End Sub
End Class
