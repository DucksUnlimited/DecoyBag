Imports System.Configuration
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Partial Class DB002
    Inherits System.Web.UI.Page

    Dim curdate As String = DateTime.Now.ToString("yyyyMMdd")
    'Dim DTCats As DataTable...


    Enum Panels
        Catalog
        Preview
        Message
    End Enum

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

    Private Property EventID() As String
        Get
            If Session("EventID") Is Nothing Then
                Session.Add("EventID", String.Empty)
            End If

            Return DirectCast(Session("EventID"), String)
        End Get
        Set(ByVal value As String)
            Session("EventID") = value
        End Set
    End Property

    Private Property EventD() As String
        Get
            If Session("EventD") Is Nothing Then
                Session.Add("EventD", String.Empty)
            End If

            Return DirectCast(Session("EventD"), String)
        End Get
        Set(ByVal value As String)
            Session("EventD") = value
        End Set
    End Property

    Private Property EventDt() As String
        Get
            If Session("EventDt") Is Nothing Then
                Session.Add("EventDt", String.Empty)
            End If

            Return DirectCast(Session("EventDt"), String)
        End Get
        Set(ByVal value As String)
            Session("EventDt") = value
        End Set
    End Property

    Private Property VolID() As String
        Get
            If Session("VolID") Is Nothing Then
                Session.Add("VolID", String.Empty)
            End If

            Return DirectCast(Session("VolID"), String)
        End Get
        Set(ByVal value As String)
            Session("VolID") = value
        End Set
    End Property

    Dim connectionString As ConnectionStringSettingsCollection = ConfigurationManager.ConnectionStrings
    Dim cS As String = connectionString("S1032895.OleDb").ToString
    Dim duconn As OleDbConnection '= New OleDbConnection(cS)
    Dim volunteer As String = "NO"

#End Region

#Region "Page Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'TEST
        Dim slibrary As String = ConfigurationManager.AppSettings("LIBRARY")

        If Session("LIBRARY") Is Nothing Then
            Session("LIBRARY") = slibrary
        End If


        'If Not Request.QueryString("IDKEY") Is Nothing Then
        '    Dim idkey As String = Request.QueryString("IDKEY").ToString
        'Else
        '    EventID = "TN001820095"
        'End If

        If Not Request.QueryString("EventID") Is Nothing Then
            EventID = Request.QueryString("EventID").ToString
        Else
            EventID = "TN001820095"
        End If

        If Not Request.QueryString("EventD") Is Nothing Then
            EventD = Request.QueryString("EventD").ToString
        Else
            EventD = "Wolf River Nhq Sponsor Dinner"
        End If

        If Not Request.QueryString("EventDt") Is Nothing Then
            EventDt = Request.QueryString("EventDt").ToString
        Else
            EventDt = "20090213"
        End If

        If Not Request.QueryString("VolID") Is Nothing Then
            volunteer = "YES"
            VolID = Request.QueryString("VolID").ToString
        Else
            volunteer = "NO"
        End If

        DatePH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
        TimePH.Text = DateTime.Now.ToString("h:mm:ss - tt")
        DateSH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
        TimeSH.Text = DateTime.Now.ToString("h:mm:ss - tt")
        Title.Text = EventD


        If (Not Page.IsPostBack) Then

            GetCatsDataTable()
            GetMainDataTable()

            rptGrids.DataSource = DTCats
            rptGrids.DataBind()

            Dim dv As DataView = New DataView(DT, "it11dv='020'", "it11dv asc", DataViewRowState.CurrentRows)

            catGroup.Text = "Support Materials"

            dgCatalog.DataSource = dv.ToTable()
            dgCatalog.DataBind()

            For Each rpi As RepeaterItem In rptGrids.Items
                Dim btn As Button = CType(rpi.FindControl("Lnk1"), Button)
                If (btn.CommandArgument = "010" Or btn.Text = "Firearms") And volunteer = "YES" Then
                    btn.Enabled = False
                End If
                If btn.CommandArgument = "020" Then
                    btn.BackColor = Drawing.Color.Wheat
                Else
                    btn.BackColor = Drawing.Color.Empty
                End If
            Next

            DisplayPanel(Panels.Catalog)

            If Session("DTOrder") Is Nothing Then

                Dim stringType As System.Type
                stringType = System.Type.GetType("System.String")

                Dim dc As DataColumn = New DataColumn("ProductID")
                DTOrder.Columns.Add(dc)
                dc = New DataColumn("Qty")
                DTOrder.Columns.Add(dc)
                dc = New DataColumn("CategoryID", stringType)
                DTOrder.Columns.Add(dc)


            End If


        End If

    End Sub
#End Region

#Region "Routines"

    Protected Sub RemoveFromOrderTable(ByVal ProductID As String)

        Dim drs As DataRow() = DTOrder.Select("ProductID = '" + ProductID + "'")
        If drs.Length > 0 Then
            Dim dr As DataRow = drs(0)
            DTOrder.Rows.Remove(dr)
        End If


    End Sub

    Protected Sub FillOrderTable()
        For Each dgi As DataGridItem In dgCatalog.Items

            Dim ProductID As String = dgi.Cells(0).Text

            Dim products As DataRow() = DT.Select("ccnumb='" + ProductID + "'")
            Dim product As DataRow = DT.NewRow()
            If products.Length > 0 Then
                product = products(0)
            End If

            Dim instock As Int32 = 0
            If Not product Is Nothing Then
                instock = ConvertToInt(product("avalqty").ToString())
            End If

            Dim txt As TextBox = CType(dgi.FindControl("txtQ"), TextBox)
            If Not txt Is Nothing Then
                If Not txt.Text.Trim() = String.Empty And Not txt.Text.Trim() = "0" Then
                    Dim qty As Int32 = ConvertToInt(txt.Text)
                    If qty <= instock Then

                        Dim dr As DataRow = DTOrder.NewRow()

                        dr(0) = dgi.Cells(0).Text
                        dr(1) = ConvertToInt(txt.Text)
                        'dr(2) = ConvertToInt(dgi.Cells(7).Text)	'it11dv
                        dr(2) = dgi.Cells(7).Text

                        RemoveFromOrderTable(ProductID)

                        DTOrder.Rows.Add(dr)
                    Else
                        lblStatus.Text = " Quantity Greater than Available<br><br>"
                        RemoveFromOrderTable(ProductID)
                    End If

                Else
                    RemoveFromOrderTable(dgi.Cells(0).Text)
                End If

            End If
        Next

        For Each rpi As RepeaterItem In rptGrids.Items
            Dim btn As Button = CType(rpi.FindControl("Lnk1"), Button)
            Dim drs As DataRow() = DTOrder.Select("CategoryID='" + btn.CommandArgument + "'")
            If drs.Length > 0 Then
                btn.BackColor = Drawing.Color.Tan
            Else
                btn.BackColor = Drawing.Color.Empty
            End If
        Next
    End Sub

    Protected Sub GetCatsDataTable()
        DTCats.Clear()
        Dim query As String = String.Empty

        query = "Select catgrp, catdesc From jackson.catgroup order by catgrp"

        Dim sc As OleDbCommand = New OleDbCommand(query, duconn)
        Dim sa As OleDbDataAdapter = New OleDbDataAdapter(sc)

        duconn.Open()
        sa.Fill(DTCats)
        duconn.Close()
    End Sub
    Protected Sub GetMainDataTable()
        DT.Clear()
        Dim query As String = String.Empty
        Dim contract As String = String.Empty

        contract = "2008 EM SPRING PKG"
        'contract = "2008 EM FALL PACKAGE"
        query = "select p.cccon#, p.cceffd, p.ccexpd, c.ccnumb, c.cciseq, d.itmdsc, d.it11dv, e.ipocst, sum(f.ibqoh-f.ibqal) as avalqty"
        query += " from jackson.cstcon p"
        query += " left outer join jackson.cstcol c on (c.cccon# = p.cccon#)"
        query += " left outer join jackson.itmmst d on (d.itmnum = c.ccnumb)"
        query += " left outer join jackson.itmcmp e on (e.ipitm# = d.itmnum and e.ibbrch = '  ')"
        query += " left outer join jackson.ibrbin f on (f.ibitm# = d.itmnum)"
        query += " where (p.cccon# = '" + contract + "') and (p.cceffd < " + curdate + " and p.ccexpd > " + curdate + ")"
        query += " group by p.cccon#, p.cceffd, p.ccexpd, c.ccnumb, c.cciseq, d.itmdsc, d.it11dv, e.ipocst"
        query += " order by d.it11dv, c.cciseq"

        Dim sc As OleDbCommand = New OleDbCommand(query, duconn)
        Dim sa As OleDbDataAdapter = New OleDbDataAdapter(sc)

        duconn.Open()
        sa.Fill(DT)
        duconn.Close()
    End Sub

    Protected Sub DisplayPanel(ByVal panel As Panels)

        panelCatalog.Visible = False
        panelMessage.Visible = False
        panelDuckBagCart.Visible = False

        If (panel = Panels.Catalog) Then
            panelCatalog.Visible = True
        ElseIf panel = Panels.Message Then
            panelMessage.Visible = True
        ElseIf panel = Panels.Preview Then
            panelDuckBagCart.Visible = True
        End If

    End Sub


    Protected Sub DisplayPanel2(ByVal spanel As String)

        panelCatalog.Visible = False
        panelMessage.Visible = False
        panelDuckBagCart.Visible = False

        If (spanel = "Catalog") Then
            panelCatalog.Visible = True
        ElseIf spanel = "Message" Then
            panelMessage.Visible = True
        ElseIf spanel = "Preview" Then
            panelDuckBagCart.Visible = True
        End If

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
#End Region

#Region "Event Handlers"

    Sub lnk1_Click(ByVal Sender As Object, ByVal e As System.EventArgs)

        FillOrderTable()

        Dim lb As Button = CType(Sender, Button)

        catGroup.Text = lb.Text

        Dim dv As DataView = New DataView(DT, "it11dv='" + lb.CommandArgument + "'", "it11dv asc", DataViewRowState.CurrentRows)

        dgCatalog.DataSource = dv.ToTable()
        dgCatalog.DataBind()

        lb.BackColor = Drawing.Color.Wheat

        For Each dgi As DataGridItem In dgCatalog.Items
            Dim txt As TextBox = CType(dgi.FindControl("txtQ"), TextBox)
            If Not txt Is Nothing Then
                Dim drs As DataRow() = DTOrder.Select("ProductID='" + dgi.Cells(0).Text + "'")
                If (drs.Length > 0) Then
                    Dim currentRow As DataRow = drs(0)
                    txt.Text = IIf((Not currentRow("Qty").ToString().Trim() = String.Empty And Not currentRow("Qty").ToString().Trim() = "0"), currentRow("Qty").ToString().Trim(), String.Empty)
                    'lb.BackColor = Drawing.Color.Tan
                End If
            End If
        Next
    End Sub

    Protected Sub dgCatalog_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCatalog.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim img As New Image()
            Dim lit As New Literal()
            Dim imgPath As String = "/Images/" + DataBinder.Eval(e.Item.DataItem, "ccnumb").ToString().Trim() + ".jpg"

            img = CType(e.Item.FindControl("img1"), Image)
            lit = CType(e.Item.FindControl("litLink"), Literal)
            If Not img Is Nothing Then
                img.ImageUrl = "Images/" + DataBinder.Eval(e.Item.DataItem, "ccnumb").ToString().Trim() + ".jpg"
                Dim lnk As String = "<a href=""javascript:;"" title=""<img src='Images/" + DataBinder.Eval(e.Item.DataItem, "ccnumb").ToString().Trim() + ".jpg' />"">"
                lit.Text = lnk
            End If
            If File.Exists(imgPath) Then
            Else
                img.ImageUrl = "Images/ImageNotAvailable.jpg"
                Dim lnk As String = "<a href=""javascript:;"" title=""<img src='Images/ImageNotAvailable.jpg' />"">"
                lit.Text = lnk
            End If

        End If

    End Sub


    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click

        Dim preview As String = String.Empty
        Dim previewT As String = String.Empty
        preview += "<table width=98% height=200px><thead style='display: table-header-group'>"
        preview += "<tr><th align=Center BGColor=skyblue class=FLOAT> Item Number </th>"
        preview += "<th align=Center BGColor=skyblue class=FLOAT> Description </th>"
        preview += "<th align=Center BGColor=skyblue class=FLOAT> Qtty </th>"
        preview += "<th align=Center BGColor=skyblue class=FLOAT> Cost </th>"
        preview += "<th align=Center BGColor=skyblue class=FLOAT> Extended Cost </th></tr></thead><tfoot>"
        previewT += "<tbody>"

        FillOrderTable()

        Dim orderRow As DataRow
        Dim totprice As Decimal
        For Each orderRow In DTOrder.Rows
            Dim drs As DataRow() = DT.Select("ccnumb='" + orderRow("ProductID").ToString() + "'")
            If drs.Length > 0 Then
                Dim productrow As DataRow = drs(0)
                Dim qty As Int32 = ConvertToInt(orderRow("qty").ToString())
                Dim price As Decimal = Convert.ToDecimal(productrow("ipocst").ToString())
                Dim extprice As Decimal = Convert.ToDecimal(qty) * price
                totprice = totprice + extprice

                previewT += "<tr><td align='left'>" + orderRow("ProductID").ToString() + "</td><td align='left'>" + productrow("itmdsc").ToString() + "</td><td>" + orderRow("Qty").ToString() + "</td><td align=right >" + price.ToString("#,##0.00") + "</td><td align=right >" + extprice.ToString("#,##0.00") + "</td></tr>"

            End If
        Next
        previewT += "</tbody>"
        preview += "<tr><td colspan=4 style='Font-Size:14pt' BGColor=DarkKhaki>Total</td><td align='right' style='Font-Size:14pt' BGColor=DarkKhaki>" + totprice.ToString("#,##0.00") + "</td></tr></tfoot>"
        preview += previewT

        preview += "</table>"
        If (DTOrder.Rows.Count > 0) Then
            litPreview.Text = preview
            DisplayPanel(Panels.Preview)
            lblStatus.Text = String.Empty
        Else
            lblStatus.Text = " - No quantities were entered.<br><br>"
        End If

    End Sub

    Protected Sub btnCancelPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelPreview.Click
        DT.Clear()
        DTOrder.Clear()
        GetMainDataTable()

        Dim dv As DataView = New DataView(DT, "it11dv='020'", "it11dv asc", DataViewRowState.CurrentRows)

        catGroup.Text = "Support Materials"

        dgCatalog.DataSource = dv.ToTable()

        'dgCatalog.DataSource = DT
        dgCatalog.DataBind()

        For Each rpi As RepeaterItem In rptGrids.Items
            Dim btn As Button = CType(rpi.FindControl("Lnk1"), Button)
            If (btn.CommandArgument = "010" Or btn.Text = "Firearms") And volunteer = "YES" Then
                btn.Enabled = False
            End If
            If btn.CommandArgument = "020" Then
                btn.BackColor = Drawing.Color.Wheat
            Else
                btn.BackColor = Drawing.Color.Empty
            End If
        Next

        DisplayPanel(Panels.Catalog)
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        DisplayPanel(Panels.Catalog)
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        DisplayPanel(Panels.Message)
        Dim itmqty As String
        Dim itmnumber As String
        For Each dr As DataRow In DTOrder.Rows
            itmqty = dr("qty").ToString()
            itmnumber = dr("ProductID").ToString()

        Next
        'submit logic here

        litMessage.Text = "Your order has been submitted"

        'Response.Redirect("DB003.aspx?event=tn0018&evdate=20080501")
    End Sub

#End Region
End Class