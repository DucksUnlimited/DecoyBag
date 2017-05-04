Imports System.Data
Imports iSeriesDB.iSeriesCompMag

Partial Class CompMagRpt001
    Inherits System.Web.UI.Page
    Public parms As New ClassSessionManager

    Dim mvLastError As String
    Dim tQtyOrd As Integer = 0


#Region "Properties"

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

#End Region

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
                Session("UpdateOrd") = "Comp. Mag. State Summary Report"
                Session("EventTitle") = " "
                Session("PageTitle") = " "


                DT = GetMagStateRprt()
                If DT.Rows.Count > 0 Then
                    GridView1.DataSource = DT
                    GridView1.DataBind()
                    GridView1.Visible = True
                    ButtonExcel.Visible = True
                    'Label4.Visible = False

                Else
                    GridView1.Visible = False
                    ButtonExcel.Visible = False
                    'Label4.Visible = True
                End If


            End If

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try


    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            tQtyOrd += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "names"))
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Total"
            e.Row.Cells(1).Text = tQtyOrd.ToString()
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            'e.Row.Font.Bold = True
        End If
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        'this is necessary for the export to excel button

    End Sub


    Protected Sub ButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExcel.Click

        Dim strStyle As String = "<style>.text { mso-number-format:mm\/dd\/yyyy; } </style>"


        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        EnableViewState = False
        Dim oStringWriter As New System.IO.StringWriter()
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)
        ClearControls(GridView1)


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


    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        'Reset last error
        mvLastError = ""


        Try
            Response.Redirect("CompMagMenu001.aspx")

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try


    End Sub
End Class
