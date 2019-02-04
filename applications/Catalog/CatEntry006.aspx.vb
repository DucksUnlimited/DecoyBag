Imports IBM.Data.DB2.iSeries
Imports System.Data
Imports iSeriesDB.iSeriesCatalog

Partial Class CatEntry006
    Inherits System.Web.UI.Page

    'Public oiSeries As New ClassiSeriesDataAccess
    Public parms As New ClassSessionManager
    Dim objDucks As New ClassDucksSystemCalls
    Dim objGen As New GeneralRoutines

    Dim curdate As String = DateTime.Now.ToString("yyyyMMdd")
    Dim eventInfo As String
    Dim rdonly As Boolean = True
    Dim pageError As Boolean = False
    Dim btnText As String = String.Empty
    Dim btnCode As String = String.Empty
    Dim mvDropDown As String = String.Empty
    Dim mvQuoteOrder As String = String.Empty
    Dim mvCoorID As String = String.Empty
    Dim dtDuckSystem As New DataTable

    Dim volunteer As String = "NO"
    Dim dtQuoteOrder As DataTable

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

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            ' get session manager object
            parms = Session("oSessionManager")

            If Session("LastError") Is Nothing Then
                Session.Add("LastError", String.Empty)
            End If
            If Session("UpdateOrd") Is Nothing Then
                Session.Add("UpdateOrd", String.Empty)
            End If

            If Session("PageTitle") Is Nothing Then
                Session.Add("PageTitle", String.Empty)
            End If

            If Session("SystemTitle") Is Nothing Then
                Session.Add("SystemTitle", String.Empty)
            End If

            If Session("EventTitle") Is Nothing Then
                Session.Add("EventTitle", String.Empty)
            End If

            Session("SystemTitle") = "Event Merchandise Order Entry"
            'Session("EventTitle") = (parms.EventName + "  -  " + parms.EventID)
            Session("PageTitle") = "Order Selection/New Order"

            If Not Page.IsPostBack Then

                objGen.InitializeSessionVairables()

                'Parse query into parameters
                parms.parseQueryString(Request)

                If Not BPostBack Then
                    Dim urlStart As String
                    If parms.RDLoc = Nothing Or parms.RDLoc = "" Then
                        urlStart = "http://ww1.ducksystem.com/phpbin/www.php"
                    Else
                        urlStart = "http://ww1.ducksystem.com/phpbin/www.php?&se=9935228&tp=SL&pg=SYW232VN&ab=1&p1=1         9969544&p2=9983351   9981371&p3=9983174   5&p5=          " & parms.RDLoc & "&p9=F&p10=          Y&ex=NO-SELECT"
                    End If
                    Session("UrlStart") = urlStart
                End If

                ' oiSeries = New ClassiSeriesDataAccess

                Session("DTState") = LoadStateDropDown()

                If parms.EventID Is Nothing Or parms.EventID = "" Then

                    'PendingOrders.Visible = False
                    OrderSelect.Visible = False
                    OrderSelect.Enabled = False
                    'NoOrder.Visible = False
                    'ApprovedOrders.Visible = False
                    'NoApproved.Visible = False

                    dtQuoteOrder = LoadQuotOrderListRD(parms.RDNUM)

                    'Check to see if there are any orders to pick from
                    If dtQuoteOrder.Rows.Count > 0 Then

                        'Build order drop down from found orders

                        Dim dvPend As DataView = New DataView(dtQuoteOrder, "approv<>'Y'", "approv asc", DataViewRowState.CurrentRows)
                        Dim dtPend As New DataTable
                        dtPend = dvPend.ToTable
                        Dim dvAprv As DataView = New DataView(dtQuoteOrder, "approv='Y'", "approv asc", DataViewRowState.CurrentRows)
                        Dim dtAprv As New DataTable
                        dtAprv = dvAprv.ToTable

                        OrderSelect.Visible = True
                        OrderSelect.Enabled = True

                        PendingOrders.Visible = True
                        PendingOrders.DataSource = dtPend
                        PendingOrders.DataTextField = "dropname"
                        PendingOrders.DataValueField = "htnum"
                        PendingOrders.DataBind()
                        ApprovedOrders.Visible = True
                        ApprovedOrders.DataSource = dtAprv
                        ApprovedOrders.DataTextField = "dropname"
                        ApprovedOrders.DataValueField = "htnum"
                        ApprovedOrders.DataBind()

                        If dtPend.Rows.Count > 0 Then
                            PendingOrders.Items.Insert(0, "  Select a Pending Order")
                            PendingOrders.SelectedIndex = 0
                            objGen.SortDropDown(PendingOrders)
                        Else
                            PendingOrders.Items.Insert(0, "  No Pending Orders")
                            PendingOrders.SelectedIndex = 0
                            'NoOrder.Visible = True
                        End If

                        If dtAprv.Rows.Count > 0 Then
                            ApprovedOrders.Items.Insert(0, "  Select an Approved Order")
                            ApprovedOrders.SelectedIndex = 0
                            objGen.SortDropDown(ApprovedOrders)
                        Else
                            ApprovedOrders.Items.Insert(0, "  No Approved Orders")
                            ApprovedOrders.SelectedIndex = 0
                            'NoApproved.Visible = True
                        End If

                    Else
                        'NoOrder.Visible = True
                    End If
                    'oiSeries.Release()
                End If

            End If

        Catch ex As Exception
            Session("LastError") = ex.Message
        End Try
    End Sub

    Protected Sub GetCatsDataTable()
        '-------------------------------------------------------
        'Subroutine: GetCatsDataTable
        'Desc. . . : Load the catalog group buttons to web page.
        '-------------------------------------------------------

        Try

            'Clear Catagory buttons table
            DTCats.Clear()
            'Build event type parameter
            Dim eventType As String = parms.EventID.Substring(6, 1)
            'Load table by calling function out of ClassiSeriesDataAccess
            DTCats = LoadCatsGroupsDataTable(parms.RdOnly, eventType)

        Catch ex As Exception
            Session("LastError") = ex.Message
        End Try
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
            Session("LastError") = ex.Message
        End Try
    End Sub

    Protected Sub Load_Order()

        Try
            Dim dr As iDB2DataReader

            parms.RdOnly = True

            'oiSeries = New ClassiSeriesDataAccess

            'Get order header information from the selected order.
            dr = LoadQuotOrderHeader(mvQuoteOrder)

            Session("Approved") = True

            If dr.Read Then
                Session("UpdateOrderNumber") = dr("HTNUM")
                Session("UpdateOrder") = True
                parms.EventID = dr("HTCPO")
                parms.EventDate = dr("htcodt").ToString
                parms.RDNUM = dr("HTSLM1").Substring(1, 2)

                'Bill to Information
                Session("LBLbillchapter") = dr("HTCNO")
                Session("LBLcsname") = dr("HTBNAM")
                Session("LBLcsadd1") = dr("HTBAD1")
                Session("LBLcsadd2") = dr("HTBAD2")
                Session("LBLcsctst") = dr("HTBCTY")
                Session("LBLcszipa") = dr("HTBZIP")
                Session("LBLcszip2") = dr("HTBZP2")

                'Ship To Information
                Session("LBLshpchapter") = dr("HTSNO")
                Session("LBLcshp1") = dr("HTSNAM")
                Session("LBLcshp2") = dr("HTSAD1")
                Session("LBLcshp3") = dr("HTSAD2")
                Session("LBLcshp6") = dr("HTSCTY")
                Session("LBLcshpza") = dr("HTSZIP")
                Session("LBLcshpz2") = dr("HTSZP2")

                'Email & Comments
                Session("eMailAddr") = dr("catema")
                Session("Comments") = dr("catcom")

                'FFL Information
                Session("FFLOrganization") = dr("fflorg")
                Session("FFLName") = dr("fflattn")
                Session("FFLAddr") = dr("ffladdr")
                Session("FFLCity") = dr("fflcity")
                Session("FFLState") = dr("fflst")
                Session("FFLZip5") = dr("fflzip5")
                Session("FFLZip4") = dr("fflzip4")
                Session("FFLNumber") = dr("fflnum")
                Session("FFLExpirDate") = dr("fflexp").ToString
                If Not dr("approv") Is Nothing And Not IsDBNull(dr("approv")) Then
                    If dr("approv") = "Y" Then
                        Session("Approved") = True
                    End If
                End If

            End If

            'Load catagory buttons
            GetCatsDataTable()

            'Load all catalog items
            GetMainDataTable()


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

            Dim dtQuote As New DataTable
            dtQuote = LoadQuotOrderDetail(mvQuoteOrder)
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

            Dim sLineType As String = ""

            'get Coordinator cnaid from rd number
            mvCoorID = GetCoordinatorInfo(parms.RDNUM)

            'get Event information for duck system and fill the parameters with values
            dtDuckSystem = objDucks.GetEventData(parms.EventID, parms.EventDate, parms.RDID, mvCoorID, parms.UserID)
            For Each drow As DataRow In dtDuckSystem.Rows
                sLineType = drow("ParmType").ToString
                'Split line types in data fields
                Select Case sLineType
                    Case "evnam"
                        parms.EventName = drow("ParmData").ToString.Trim()
                    Case "useml", "vleml"
                        Session("eMailAddr") = drow("ParmData").ToString.Trim()
                    Case "usfnm", "vlfnm"
                        parms.UsrFName = drow("ParmData").ToString.Trim()
                    Case "uslnm", "vllnm"
                        parms.UsrLName = drow("ParmData").ToString.Trim()
                    Case "rdnam"
                        parms.RDName = drow("ParmData").ToString.Trim()
                    Case "rdeml"
                        parms.RDEMail = drow("ParmData").ToString.Trim()
                    Case "crnam"
                        parms.CorName = drow("ParmData").ToString.Trim()
                    Case "creml"
                        parms.CorEMail = drow("ParmData").ToString.Trim()
                End Select

            Next

            BPostBack = True

            'oiSeries.Release()

            Session("UpdateOrd") = "Updating Order -  " + Session("UpdateOrderNumber")
            Session("SystemTitle") = "Event Merchandise Order Entry"
            Session("EventTitle") = (parms.EventName + "  -  " + parms.EventID)
            Session("PageTitle") = "Item Selection"

            'Redirect to location that started the catalog program CatEntry001.aspx  
            Response.Redirect("CatEntry001.aspx", False)

        Catch ex As Exception
            Session("LastError") = ex.Message
        End Try

    End Sub

    Protected Sub Get_Selection(ByVal Src As Object, ByVal Args As EventArgs) Handles OrderSelect.Click

        Try

            If Not pageError Then

                If PendingOrders.SelectedIndex > 0 Then
                    mvQuoteOrder = PendingOrders.SelectedItem.Value
                Else
                    mvQuoteOrder = ApprovedOrders.SelectedItem.Value
                End If

                Load_Order()
            End If

        Catch ex As Exception
            Session("LastError") = ex.Message
        End Try

    End Sub

    Protected Sub Exit_Pgm(ByVal Src As Object, ByVal Args As EventArgs)

        DT().Clear()
        DTOrder().Clear()
        DTCats().Clear()
        DT().Dispose()
        DTOrder().Dispose()
        DTCats().Dispose()
        Dim urlStart As String = Session("UrlStart")
        Session.Abandon()
        Response.Redirect(urlStart)
        'Response.Redirect(Session("UrlStart"))

    End Sub

    Protected Sub MustSelectPendingOrder(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CVOrderSelect.ServerValidate

        Try

            args.IsValid = False
            pageError = True

            If ApprovedOrders.SelectedIndex > 0 And PendingOrders.SelectedIndex > 0 Then
                CVOrderSelect.ErrorMessage = "Must select one or the other not both."
                PendingOrders.Focus()
            Else
                If ApprovedOrders.SelectedIndex <= 0 And PendingOrders.SelectedIndex <= 0 Then
                    CVOrderSelect.ErrorMessage = "Must select one or the other."
                    PendingOrders.Focus()
                Else
                    args.IsValid = True
                    pageError = False
                End If
            End If

        Catch ex As Exception
            Session("LastError") = ex.Message
        End Try

    End Sub

End Class
