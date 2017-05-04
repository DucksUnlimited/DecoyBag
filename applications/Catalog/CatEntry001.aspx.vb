Imports System.Data
Imports iSeriesDB.iSeriesCatalog
Imports System.IO
Imports Telerik.Web.UI

Partial Class CatEntry001
    Inherits System.Web.UI.Page

    'Public oiSeries As New ClassiSeriesDataAccess
    Public parms As New ClassSessionManager
    Public objDucks As New ClassDucksSystemCalls
    Public objGen As New GeneralRoutines

    Dim curdate As String = DateTime.Now.ToString("yyyyMMdd")
    Dim dayofweek As String = DateTime.Now.DayOfWeek.ToString()
    Dim timeofday As String = DateTime.Now.ToString("HHmm")
    Dim eventInfo As String
    Dim rdonly As Boolean
    Dim firstTime As Boolean = True
    Dim btnText As String = String.Empty
    Dim btnCode As String = String.Empty
    Dim mvCoorID As String = String.Empty
    Dim Quote As String = """"
    Dim NoQuote As String = String.Empty

    Dim volunteer As String = "NO"
    Dim dtDuckSystem As New DataTable

#Region "Properties"

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

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            ' get session manager object

            Session.Timeout = 600

            parms = Session("oSessionManager")
            Dim wUpdateOrd As Boolean = Session("UpdateOrder")
            'Parse query into parameters
            If Session("UpdateOrder") Then
                btnExit.Text = "Exit & Cancel Order Changes"
            Else
                btnExit.Text = "Exit & Cancel Order"
            End If

            If Not Page.IsPostBack And BPostBack = False And CancelOrder = False And Not Session("UpdateOrder") Then
                parms.parseQueryString(Request)
                'Set for RD Order entry
                If parms.RDID = parms.UserID Then
                    parms.RdOnly = True
                    Session("Approved") = True
                End If
                rdonly = parms.RdOnly
                Dim wrkevid As String = parms.EventID.Substring(0, 2) & parms.EventID.Substring(3, 5)
                parms.EventID = wrkevid
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

                If Session("UrlStart") Is Nothing Then
                    Session.Add("UrlStart", String.Empty)
                    Dim urlStart As String = Request.UrlReferrer.AbsoluteUri
                    Dim i As Integer
                    i = urlStart.IndexOf("&tp")
                    If i > 0 Then
                        Dim newUrlReturn As String = String.Empty
                        newUrlReturn = urlStart.Substring(0, i)
                        newUrlReturn = newUrlReturn + "&tp=IS&pg=CBW202&ab=1&kp=" + parms.UserID
                        Session("UrlStart") = newUrlReturn
                    Else
                        Session("UrlStart") = urlStart
                    End If
                End If
                'Check to see if the Friday system backup is running
                'If dayofweek = "5" And (timeofday > "1659" And timeofday < "2030") Then
                'redirect to a page that states not active page
                'Response.Redirect("CatEntry005A.aspx")
                'End If

                'oiSeries = New ClassiSeriesDataAccess

                'Initialize all system variables
                objGen.InitializeSessionVairables()
                Session("DTState") = LoadStateDropDown()

                Dim sLineType As String = ""

                'get Coordinator cnaid from rd number
                mvCoorID = GetCoordinatorInfo(parms.RDNUM)

                'get Event information for duck system and fill the parameters with values
                dtDuckSystem = objDucks.GetEventData(parms.EventID, parms.EventDate, parms.RDID, mvCoorID, parms.UserID)
                For Each dr As DataRow In dtDuckSystem.Rows
                    sLineType = dr("ParmType").ToString
                    'Split line types in data fields
                    Select Case sLineType
                        Case "evnam"
                            parms.EventName = dr("ParmData").ToString.Trim()
                        Case "usfnm", "vlfnm"
                            parms.UsrFName = dr("ParmData").ToString.Trim()
                        Case "uslnm", "vllnm"
                            parms.UsrLName = dr("ParmData").ToString.Trim()
                        Case "useml", "vleml"
                            Session("eMailAddr") = dr("ParmData").ToString.Trim()
                        Case "rdnam"
                            parms.RDName = dr("ParmData").ToString.Trim()
                        Case "rdeml"
                            parms.RDEMail = dr("ParmData").ToString.Trim()
                        Case "crnam"
                            parms.CorName = dr("ParmData").ToString.Trim()
                        Case "creml"
                            parms.CorEMail = dr("ParmData").ToString.Trim()
                    End Select

                Next

                Session("UpdateOrd") = "New Merchandise Order"
                Session("SystemTitle") = "Event Merchandise Order Entry"
                Session("EventTitle") = (parms.EventName + "  -  " + parms.EventID)
                Session("PageTitle") = "Item Selection"
            End If


            'Dim slibrary As String = System.Configuration.ConfigurationManager.AppSettings("LIBRARY")

            'If Session("LIBRARY") Is Nothing Then
            '    Session("LIBRARY") = slibrary
            'End If

            'todo - Activate Security checks
            parms.IDKey = "IASJ"

            If Not Page.IsPostBack And BPostBack = False And Not Session("UpdateOrder") Then

                'Load catagory buttons
                GetCatsDataTable()

                rptGrids.DataSource = DTCats
                rptGrids.DataBind()

                'Load all catalog items
                GetMainDataTable()

                'Select first button as default button
                firstTime = True
                For Each rpi As RepeaterItem In rptGrids.Items
                    Dim btn As Button = CType(rpi.FindControl("Lnk1"), Button)

                    'Find oput if the group has items to select from.
                    'turn it off if not.
                    Dim items As DataRow() = DT.Select("ccpzon='" + btn.CommandArgument.ToString() + "'")
                    If items.Length > 0 Then
                        If firstTime Then
                            firstTime = False
                            'set selected button color and save code
                            btn.BackColor = Drawing.Color.Wheat
                            btnText = btn.Text.ToString().Trim()
                            btnCode = btn.CommandArgument.ToString()
                        Else
                            btn.BackColor = Drawing.Color.Empty
                        End If
                        btn.Visible = True
                    Else
                        'hide button if no items in group
                        'btn.Visible = False
                        btn.Enabled = False
                    End If
                Next

                catGroup.Text = btnText.Trim()
                'set a view of the table for the first button code
                Dim dv As DataView = New DataView(DT, "ccpzon=" + btnCode, "ccpzon asc", DataViewRowState.CurrentRows)

                'Load catalog items for selected button
                dgCatalog.DataSource = dv.ToTable()
                dgCatalog.DataBind()

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

                End If

            Else
                If BPostBack Then

                    rptGrids.DataSource = DTCats
                    rptGrids.DataBind()

                    firstTime = True
                    For Each rpi As RepeaterItem In rptGrids.Items
                        Dim btn As Button = CType(rpi.FindControl("Lnk1"), Button)
                        Dim drs As DataRow() = DTOrder.Select("CategoryID='" + btn.CommandArgument + "'")
                        'Find oput if the group has items to select from.
                        'turn it off if not.
                        Dim items As DataRow() = DT.Select("CCPZON='" + btn.CommandArgument.ToString() + "'")
                        If items.Length > 0 Then
                            If firstTime Then
                                firstTime = False
                                'set selected button color and save code
                                btn.BackColor = Drawing.Color.Wheat
                                btnText = btn.Text.ToString().Trim()
                                btnCode = btn.CommandArgument.ToString()
                            Else
                                If drs.Length > 0 Then
                                    btn.BackColor = Drawing.Color.Tan
                                Else
                                    btn.BackColor = Drawing.Color.Empty
                                End If
                            End If
                            btn.Visible = True
                        Else
                            'hide button if no items in group
                            btn.Visible = False
                        End If
                    Next

                    Dim dv As DataView = New DataView(DT, "CCPZON=" + btnCode, "ccpzon asc", DataViewRowState.CurrentRows)
                    catGroup.Text = btnText
                    dgCatalog.DataSource = dv.ToTable()
                    dgCatalog.DataBind()

                    '---------------LOAD THE QUANTITIES RWM 6/13/2008-------------------
                    'For Each dgi As DataGridItem In dgCatalog.Items
                    '    Dim txt As TextBox = CType(dgi.FindControl("txtQ"), TextBox)
                    '    If Not txt Is Nothing Then
                    '        Dim drs As DataRow() = DTOrder.Select("ProductID='" + dgi.Cells(0).Text + "'")
                    '        If (drs.Length > 0) Then
                    '            Dim currentRow As DataRow = drs(0)
                    '            txt.Text = IIf((Not currentRow("Qty").ToString().Trim() = String.Empty And Not currentRow("Qty").ToString().Trim() = "0"), currentRow("Qty").ToString().Trim(), String.Empty)
                    '        End If
                    '    End If
                    'Next
                    For Each dgi As GridDataItem In dgCatalog.Items
                        Dim ProductID As String = dgi("ccnumb").Text.Trim
                        Dim txt As TextBox = CType(dgi.FindControl("txtQ"), TextBox)
                        If Not txt Is Nothing Then
                            Dim drs As DataRow() = DTOrder.Select("ProductID='" + ProductID + "'")
                            If (drs.Length > 0) Then
                                Dim currentRow As DataRow = drs(0)
                                txt.Text = IIf((Not currentRow("Qty").ToString().Trim() = String.Empty And Not currentRow("Qty").ToString().Trim() = "0"), currentRow("Qty").ToString().Trim(), String.Empty)
                            End If
                        End If
                    Next


                    BPostBack = False
                End If
            End If

        Catch ex As Exception
            lblStatus.Text = ex.Message
        End Try

    End Sub
#End Region

#Region "Routines"

    Protected Sub RemoveFromOrderTable(ByVal ProductID As String)

        'Remove product row from order table
        Dim drs As DataRow() = DTOrder.Select("ProductID = '" + ProductID + "'")
        If drs.Length > 0 Then
            Dim dr As DataRow = drs(0)
            DTOrder.Rows.Remove(dr)
        End If

    End Sub

    Protected Sub FillOrderTable()

        Try

            'Loop throuh all catalog items
            'For Each dgi As GridDataItem In dgCatalog.Items
            For Each dgi As GridDataItem In dgCatalog.Items

                'Grab product id
                'Dim ProductID As String = dgi("ccnumb").Text.Trim
                Dim ProductID As String = dgi.Cells(0).Text

                'Select product id from catalog list table to a datarow
                Dim products As DataRow() = DT.Select("CCNUMB='" + ProductID + "'")
                Dim product As DataRow = DT.NewRow()
                'Make sure product record was selected
                If products.Length > 0 Then
                    product = products(0)
                End If

                'Make sure qty not larger than available
                Dim instock As Int32 = 0
                If Not product Is Nothing Then
                    instock = ConvertToInt(product("avalqty").ToString())
                End If

                'If a qty was entered, move the line into the DTOrder table

                'Dim txt2 As String = TryCast(dgi.FindControl("ccnumb"), BoundField).Text
                Dim txt As String = TryCast(dgi.FindControl("txtQ"), TextBox).Text
                'Dim txt As RadTextBox = CType(dgi.FindControl("txtQ"), RadTextBox)
                If Not txt Is Nothing Then
                    If Not txt.ToString = String.Empty And Not txt.ToString.Trim() = "0" Then
                        Dim qty As Int32 = ConvertToInt(txt)

                        'If qty is available and within range
                        'If qty <= instock Then

                        Dim dr As DataRow = DTOrder.NewRow()

                        'Set fields within new data row
                        'dr(0) = dgi("ccnumb").Text.Trim
                        dr(0) = dgi.Cells(0).Text 'Set qty in table
                        dr(1) = ConvertToInt(txt)
                        'dr(2) = dgi("itmdsc").Text.Trim
                        'dr(3) = dgi("unitcost").Text
                        dr(2) = dgi.Cells(6).Text
                        dr(3) = dgi.Cells(3).Text
                        dr(4) = 0
                        'dr(5) = dgi("ccpzon").Text.Trim
                        dr(5) = dgi.Cells(7).Text

                        'Remove previous item from order table
                        RemoveFromOrderTable(ProductID)

                        'Add new item for product to order table
                        DTOrder.Rows.Add(dr)

                        'Else
                        'Attempt to get rid of order line for product if it exists.
                        'lblStatus.Text = " Quantity Greater than Available<br><br>"
                        'RemoveFromOrderTable(ProductID)
                        'End If

                    Else 'Qty was blank or 0
                        'Attempt to get rid of order line for product if it exists.
                        RemoveFromOrderTable(ProductID)
                    End If

                End If
            Next

            'Set the colors of the catalog group buttons for selected items.
            For Each rpi As RepeaterItem In rptGrids.Items
                Dim btn As Button = CType(rpi.FindControl("Lnk1"), Button)
                Dim drs As DataRow() = DTOrder.Select("CategoryID='" + btn.CommandArgument + "'")
                'If category has any selected items, set color
                If drs.Length > 0 Then
                    btn.BackColor = Drawing.Color.Tan
                Else 'Category has no selected items.
                    btn.BackColor = Drawing.Color.Empty
                End If
            Next

            'Check to see if a gun was selected and turn on FFL required flag.
            'Reset FFL flag and catagory
            Session("FFLRequired") = False
            Dim drGuns As DataRow() = DTOrder.Select("ProductID like 'B%'")
            If drGuns.Length > 0 Then
                Session("FFLRequired") = True
            End If
        Catch ex As Exception
            lblStatus.Text = ex.Message
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
            'Dim dr As iDB2DataReader
            'Load table by calling function out of ClassiSeriesDataAccess
            'dr = oiSeries.LoadCatsGroupsDataTable(rdonly, eventType)
            DTCats = LoadCatsGroupsDataTable(rdonly, eventType)
            'DTCats.Load(dr)

        Catch ex As Exception

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

        End Try
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

    'Protected Function GenerateToolTip(ByVal target As String, ByVal text As String) As RadToolTip
    '    Dim ttip As RadToolTip = New RadToolTip()
    '    ttip.ID = target & "_ttip"
    '    ttip.TargetControlID = target
    '    ttip.IsClientID = True
    '    ttip.Text = text
    '    'ttip.Width = 400
    '    ttip.ShowEvent = ToolTipShowEvent.OnMouseOver
    '    ttip.RelativeTo = ToolTipRelativeDisplay.Element
    '    ttip.Position = ToolTipPosition.BottomRight
    '    ttip.RenderInPageRoot = True
    '    ttip.AutoCloseDelay = 9500
    '    'ttip.Skin = "Sunset"
    '    Return ttip
    'End Function

    Protected Sub OnAjaxUpdate(sender As Object, args As ToolTipUpdateEventArgs)
        Me.UpdateToolTip(args.Value, args.UpdatePanel)
    End Sub

    Private Sub UpdateToolTip(elementID As String, panel As UpdatePanel)
        Dim ctrl As Control = Page.LoadControl("ProductDetails.ascx")
        ctrl.ID = "UcProductDetails1"

        panel.ContentTemplateContainer.Controls.Add(ctrl)
        Dim details As ProductDetails = DirectCast(ctrl, ProductDetails)
        details.CCNumb = elementID
    End Sub

#End Region

#Region "Event Handlers"

    Sub lnk1_Click(ByVal Sender As Object, ByVal e As System.EventArgs)

        FillOrderTable()

        Dim lb As Button = CType(Sender, Button)

        catGroup.Text = lb.Text

        Dim dv As DataView = New DataView(DT, "ccpzon='" + lb.CommandArgument + "'", "ccpzon asc", DataViewRowState.CurrentRows)

        dgCatalog.DataSource = dv.ToTable()
        dgCatalog.DataBind()

        lb.BackColor = Drawing.Color.Wheat

        For Each dgi As GridDataItem In dgCatalog.Items
            Dim txt As TextBox = TryCast(dgi.FindControl("txtQ"), TextBox)
            Dim itemnum As String = dgi("ccnumb").Text.Trim
            If Not txt.Text Is Nothing Then
                Dim drs As DataRow() = DTOrder.Select("ProductID='" + itemnum + "'")
                If (drs.Length > 0) Then
                    Dim currentRow As DataRow = drs(0)
                    txt.Text = IIf((Not currentRow("Qty").ToString().Trim() = String.Empty And Not currentRow("Qty").ToString().Trim() = "0"), currentRow("Qty").ToString().Trim(), String.Empty)
                    'lb.BackColor = Drawing.Color.Tan
                End If
            End If
        Next
    End Sub

    Protected Sub dgCatalog_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgCatalog.ItemDataBound

        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then

            Dim img As New System.Web.UI.WebControls.Image()
            'Dim lit As New Literal()
            'Dim lnk As String
            Dim itemNum As String = DataBinder.Eval(e.Item.DataItem, "ccnumb").ToString().Trim()
            'Dim maxAllowed As Decimal = 0
            Dim imgPath As String = "Images/" + DataBinder.Eval(e.Item.DataItem, "ccnumb").ToString().Trim() + ".jpg"
            Dim txtPath As String = "/Doc/Descriptions/" + DataBinder.Eval(e.Item.DataItem, "ccnumb").ToString().Trim() + ".txt"

            img = CType(e.Item.FindControl("img1"), System.Web.UI.WebControls.Image)
            'lit = CType(e.Item.FindControl("litLink"), Literal)

            Dim ctrl As Control = e.Item.FindControl("img1")
            If Not [Object].Equals(ctrl, Nothing) Then
                If Not [Object].Equals(Me.RadToolTipManager1, Nothing) Then
                    'RadToolTipManager1.TargetControls.Add(ctrl.ClientID.ToString())
                    RadToolTipManager1.TargetControls.Add(ctrl.ClientID, (TryCast(e.Item, GridDataItem)).GetDataKeyValue("ccnumb").ToString(), True)
                End If
            End If '

            Dim toolHeader As String = DataBinder.Eval(e.Item.DataItem, "itmdsc")

            If Not img Is Nothing Then
                img.ImageUrl = imgPath
                toolHeader = Replace(toolHeader, Chr(34), "")
            Else
                img.ImageUrl = "Images/ImageNotAvailable.jpg"
            End If
            If File.Exists(imgPath) Then
            Else
            End If
            'End If
            'If (TypeOf e.Item Is GridEditableItem) Then
            'set validation for quantity in the catalog list
            'e.Item.Edit = True
            Dim lbl As New Label()
            Dim wrkQtty As Decimal = 0
            Dim maxOrderQtty As Decimal = DataBinder.Eval(e.Item.DataItem, "cceop")
            Dim multipleQtty As Decimal = DataBinder.Eval(e.Item.DataItem, "ccmqoh")
            Dim regexp As New RegularExpressionValidator()
            regexp = CType(e.Item.FindControl("RegExpQty"), RegularExpressionValidator)
            Dim rngpmax As New RangeValidator()
            rngpmax = CType(e.Item.FindControl("RVMaxQty"), RangeValidator)
            lbl = CType(e.Item.FindControl("multiAllow"), Label)

            'Set max quantity based on who is entering/updating the order
            wrkQtty = 0
            'maxAllowed = multipleQtty * maxOrderQtty
            rngpmax.Text = "Quantity over the max of " & maxOrderQtty.ToString()
            rngpmax.MaximumValue = maxOrderQtty
            rngpmax.SetFocusOnError = True

            'Set up validator for multiple items per package
            If multipleQtty > 1 Then
                regexp.ValidationExpression = "^(0)"
                Dim d As Integer = 0
                For d = 0 To maxOrderQtty + 1
                    If wrkQtty <> 0 Then
                        regexp.ValidationExpression += "|(" & wrkQtty.ToString() & ")"
                    End If
                    wrkQtty = wrkQtty + multipleQtty
                Next d
                regexp.ValidationExpression += "$"
                regexp.ErrorMessage = "Quantity must be multiple of " & multipleQtty.ToString()
                regexp.SetFocusOnError = True
            Else
                regexp.Enabled = False
                regexp.ValidationExpression = "^[0-" & maxOrderQtty.ToString() & "]$"
                regexp.ErrorMessage = "Quantity over the max of " & maxOrderQtty.ToString()
            End If
            If maxOrderQtty > 1 Then
                lbl.Visible = True
            Else
                lbl.Visible = False
            End If

        End If

    End Sub


    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click

        If Page.IsValid Then
            FillOrderTable()
            'Redirect to location that started the catalog program.
            Response.Redirect("CatEntry002.aspx")
        End If


    End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        DT().Dispose()
        DTOrder().Dispose()
        DTCats().Dispose()

        Response.Redirect(Session("UrlStart"))

    End Sub

#End Region
End Class
