Imports System.IO
Imports system.data
Partial Class EventInvSum
    Inherits System.Web.UI.Page

    Public oiSeries As New ClassiSeriesDataAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim dtl As DataTable = oiSeries.GetEventInvSum()

        'Bind the Returned Data to the Data Grid for Display

        GridView1.DataSource = dtl
        GridView1.DataBind()


        Dim defaultsExcludeColumns As New ArrayList()
        Dim strStyle As String = "<style>.text { mso-number-format:mm\dd\yyyy; )</style>"

        'The value that is used is the header text assign to the column
        defaultsExcludeColumns.Add("Image")
        defaultsExcludeColumns.Add(" ")

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
            If defaultsExcludeColumns.Contains(field.HeaderText) Then
                field.Visible = False
            End If
        Next

        'Dump grid to an excel spread sheet
        GridView1.RenderControl(oHtmlTextWriter)
        Response.Write(strStyle)
        Response.Write(oStringWriter.ToString())
        Response.End()

        Response.Redirect("PromoMenu001.aspx")


    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        'this subroutine is needed so that RenderControl will work correctly

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
End Class
