Imports System.Data
Imports iSeriesDB.iSeriesCatalog

Partial Class ReturnCodes001
    Inherits System.Web.UI.Page

    'Public oiSeries As New ClassiSeriesDataAccess
    Public parms As New ClassSessionManager
    Dim mvLastError As String

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Reset last error
        mvLastError = ""

        Try
            ' get session manager object
            parms = Session("oSessionManager")

            If Not Page.IsPostBack Then


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

                Session("SystemTitle") = " "
                Session("UpdateOrd") = "Return Records"
                Session("EventTitle") = " "
                Session("PageTitle") = " "

            End If

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try

    End Sub

    Protected Sub ButtonFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonFind.Click

        Try

            'Reset last error
            mvLastError = ""
            If Format(StartDate.SelectedDate, "yyyyMMdd") > Format(EndDate.SelectedDate, "yyyyMMdd") Then
                Label5.Visible = True
                Label4.Visible = False
                ButtonExcel.Enabled = False
                ButtonExcel.Visible = False
                GridView1.Visible = False
            Else

                DT = GetReturnRecords(Format(StartDate.SelectedDate, "yyyyMMdd"), Format(EndDate.SelectedDate, "yyyyMMdd"))
                If DT.Rows.Count > 0 Then
                    GridView1.DataSource = DT
                    GridView1.DataBind()
                    Label4.Visible = False
                    Label5.Visible = False
                    ButtonExcel.Enabled = True
                    ButtonExcel.Visible = True
                    GridView1.Visible = True
                Else

                    Label4.Visible = True
                    Label5.Visible = False
                    ButtonExcel.Enabled = False
                    ButtonExcel.Visible = False
                    GridView1.Visible = False
                End If
            End If

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing
        End Try

    End Sub

    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        Try

            Response.Redirect("PromoMenu001.aspx")

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExcel.Click

        Dim defaultExcludedColumns As New ArrayList()
        Dim strStyle As String = "<style>.text { mso-number-format:mm\/dd\/yyyy; } </style>"

        'The value that is used is the header text assign to the column
        'defaultExcludedColumns.Add("Image")
        defaultExcludedColumns.Add(" ")

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        EnableViewState = False
        Dim oStringWriter As New System.IO.StringWriter()
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)
        ClearControls(GridView1)

        'Exclude any columns that the header text matches any value in the drop array
        For Each field As DataControlField In GridView1.Columns
            If defaultExcludedColumns.Contains(field.HeaderText) Then
                field.Visible = False
            End If
        Next

        'Dump grid to an excel spread sheet
        GridView1.RenderControl(oHtmlTextWriter)
        Response.Write(strStyle)
        Response.Write(oStringWriter.ToString())
        Response.End()
    End Sub
    Private Sub ClearControls(ByVal control As Control)
        Dim i As Integer = control.Controls.Count - 1
        While i >= 0
            ClearControls(control.Controls(i))
            System.Math.Max(System.Threading.Interlocked.Decrement(i), i + 1)
        End While
        If Not (TypeOf control Is TableCell) Then
            If Not control.[GetType]().GetProperty("SelectedItem") Is Nothing Then
                Dim literal As New LiteralControl()
                control.Parent.Controls.Add(literal)
                Try
                    literal.Text = DirectCast(control.[GetType]().GetProperty("SelectedItem").GetValue(control, Nothing), String)
                Catch
                End Try
                control.Parent.Controls.Remove(control)
            ElseIf Not control.[GetType]().GetProperty("Text") Is Nothing Then
                Dim literal As New LiteralControl()
                control.Parent.Controls.Add(literal)
                literal.Text = DirectCast(control.[GetType]().GetProperty("Text").GetValue(control, Nothing), String)
                control.Parent.Controls.Remove(control)
            End If
        End If
        Return
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        'this is necessary for the export to excel button

    End Sub



End Class
