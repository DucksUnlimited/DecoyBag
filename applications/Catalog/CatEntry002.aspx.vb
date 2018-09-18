Imports System.Data
Imports iSeriesDB.iSeriesCatalog

Partial Class CatEntry002
    Inherits System.Web.UI.Page

    Public parms As New ClassSessionManager
    Dim objGen As New GeneralRoutines
    'Dim oiSeries As New ClassiSeriesDataAccess

    Dim tQtyOrd As Integer = 0
    Dim ExtCost As Decimal = CDec(0.0)
    Dim tExtCost As Decimal = CDec(0.0)

#Region "Properties"

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

    Private Property BPostBack() As Boolean
        Get
            If Session("BPostBack") Is Nothing Then
                Session.Add("BPostBack", New Boolean)
            End If

            Return DirectCast(Session("BPostBack"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            Session("BPostBack") = value
        End Set
    End Property
    Private Property CancelOrder() As Boolean
        Get
            If Session("CancelOrder") Is Nothing Then
                Session.Add("CancelOrder", New Boolean)
            End If

            Return DirectCast(Session("CancelOrder"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            Session("CancelOrder") = value
        End Set
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        parms = Session("oSessionManager")
        Session("PageTitle") = "Preview Decoy Bag"

        If Session("UpdateOrder") Then
            btnExit.Text = "Exit & Cancel Order Changes"
            btnCancelPreview.Text = "Cancel Changes & Re-Start Order"
        Else
            btnExit.Text = "Exit & Cancel Order"
            btnCancelPreview.Text = "Cancel & Re-Start Order"
        End If

        'Sort order table into item number order
        DTOrder = objGen.sortTable(DTOrder, "ProductID")

        GridView1.DataSource = DTOrder
        GridView1.DataBind()


    End Sub

    Public Shared Function ConvertToInt(ByVal sInt As String) As Integer
        'this step will remove commas and spaces if there are any
        sInt = StripSpecialCharactersAndSpaces(sInt)

        Dim r As New Regex("^\d+$")
        If r.IsMatch(sInt) Then
            Return Integer.Parse(sInt)
        End If

        Return 0

    End Function

    Public Shared Function StripSpecialCharactersAndSpaces(ByVal s As String) As String
        Dim r As New Regex("\s+")
        'remove all whitespace
        s = r.Replace(s, "")
        ' to a single space
        Dim mc As MatchCollection = Regex.Matches(s, "[A-Za-z0-9]", RegexOptions.IgnoreCase)
        s = String.Empty
        For Each m As Match In mc
            s += m.ToString()
        Next

        Return s

    End Function

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
            parms.TotPrice = tExtCost.ToString()
        End If
    End Sub

    Public Sub GetQuoteOrderItems()

        Try
            'Build Order table of items from the existing order
            'initialize order table
            If Session("DTOrder") Is Nothing Then

                Dim stringType As System.Type
                stringType = System.Type.GetType("System.String")

                Dim dc As DataColumn = New DataColumn("ProductID")
                DTOrder.Columns.Add(dc)
                dc = New DataColumn("Qty")
                DTOrder.Columns.Add(dc)
                dc = New DataColumn("Description")
                DTOrder.Columns.Add(dc)
                dc = New DataColumn("Cost")
                DTOrder.Columns.Add(dc)
                dc = New DataColumn("ExtendedCost")
                DTOrder.Columns.Add(dc)
                dc = New DataColumn("CategoryID", stringType)
                DTOrder.Columns.Add(dc)
            Else
                DTOrder.Clear()
            End If

            'Dim dr2 As iDB2DataReader = LoadQuotOrderDetail(Session("UpdateOrderNumber"))
            Dim dtQuote As DataTable = LoadQuotOrderDetail(Session("UpdateOrderNumber"))
            'dtQuote.Load(dr2)

            Dim tQtyOrd As Integer = 0
            Dim ExtCost As Decimal = CDec(0.0)
            Dim Cost As Decimal = CDec(0.0)

            For Each drow As DataRow In dtQuote.Rows

                Dim drw As DataRow = DTOrder.NewRow()
                tQtyOrd = drow("HTQor")
                Cost = drow("htluct")
                ExtCost = drow("CS33EC")

                'Set fields within new data row
                drw(0) = drow("HTLPNM").ToString.Trim() 'Set qty in table
                drw(1) = drow("HTQor").ToString
                drw(2) = drow("HTLDSC")
                drw(3) = Cost.ToString("0.00")
                drw(4) = ExtCost.ToString("0.00")
                Dim itemNumber As String = drow("HTLPNM").ToString.Trim()
                'get catagory for item from in catalog
                Dim items As DataRow() = DT.Select("ccnumb='" + drow("HTLPNM").ToString.Trim() + "'")
                If (items.Length > 0) Then
                    Dim currentrow As DataRow = items(0)
                    drw(5) = currentrow("ccpzon")
                End If

                'Add item from selected order to order table
                DTOrder.Rows.Add(drw)

            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancelPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelPreview.Click
        DT.Clear()
        DTOrder.Clear()
        BPostBack = False
        CancelOrder = True
        If Session("UpdateOrder") Then
            GetMainDataTable()
            GetQuoteOrderItems()
            BPostBack = True
        End If
        Response.Redirect("CatEntry001.aspx")

    End Sub

    Protected Sub GetMainDataTable()
        '-------------------------------------------------------
        'Subroutine: GetMainDataTable
        'Desc. . . : Load all catalog items to data table. 
        '            We filter this list on the fly later
        '            by using a DataView that is filtered by category.
        '-------------------------------------------------------
        Try

            DT.Clear()
            Dim dr As DataRow
            'Load table by calling function out of ClassiSeriesDataAccess
            DT = LoadCatalogDataTable(parms.EventDate, parms.EventID.Substring(6, 1), parms.EventID.Substring(0, 2))

            'Get catalog name from table
            If DT.Rows.Count > 0 Then
                'Load 1st (only) row of data to get catalog name
                dr = DT.Rows(0)
                parms.CatName = dr("cccon#").ToString

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        BPostBack = True
        Response.Redirect("CatEntry001.aspx")
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSubmit.Click

        Response.Redirect("CatEntry003.aspx")

    End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        DT().Dispose()
        DTOrder().Dispose()
        DTCats().Dispose()
        Dim urlStart As String = Session("UrlStart")
        Session.Abandon()
        Response.Redirect(urlStart)

    End Sub
End Class
