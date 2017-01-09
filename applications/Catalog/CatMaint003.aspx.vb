Imports IBM.Data.DB2.iSeries
Imports System.Data
Imports System.IO

Partial Class CatMaint003
    Inherits System.Web.UI.Page
    Public parms As New ClassSessionManager
    Public oiSeries As New ClassiSeriesDataAccess
    Dim curdate As String = Format(Date.Today(), "yyyyMMdd")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Run the SQL Query to get data

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
                Session("UpdateOrd") = "Catalog Maintenance"
                Session("EventTitle") = " "
                Session("PageTitle") = " "


                LblCatName.Text = Session("sCCCON#")
                Dim dtl As DataTable = oiSeries.GetCatItemList(LblCatName.Text, Session("sEXPIRED"))

                'Bind the Returned Data to the Data Grid for Display

                GridView1.DataSource = dtl
                GridView1.DataBind()

                If Session("sEXPIRED") = "1" Then
                    ButtonExpired.Text = "Show Current Items"
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Protected Sub ButtonNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonNew.Click

        Try
            Session("sMODE") = "INSERT"
            Session("sLOADREC") = ""
            Response.Redirect("CatMaint004.aspx")

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GridView1_ImageBind(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try

            If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

                Dim img As New Image()
                Dim itemNum As String = DataBinder.Eval(e.Row.DataItem, "ccnumb").ToString().Trim()
                Dim imgPath As String = "/Images/" + DataBinder.Eval(e.Row.DataItem, "ccnumb").ToString().Trim() + ".jpg"

                img = CType(e.Row.FindControl("Image1"), Image)
                If Not img Is Nothing Then
                    'img.ImageUrl = "Images/" + DataBinder.Eval(e.Row.DataItem, "ccnumb").ToString().Trim() + ".jpg"
                    img.ImageUrl = "~/Images/" + DataBinder.Eval(e.Row.DataItem, "ccnumb").ToString().Trim() + ".jpg"
                End If
                If File.Exists(imgPath) Then
                Else
                    img.ImageUrl = "~/Images/ImageNotAvailable.jpg"
                End If

                Dim avlQttyChk As String = DataBinder.Eval(e.Row.DataItem, "avalqty").ToString().Trim()
                If avlQttyChk Is Nothing Or avlQttyChk = " " Or avlQttyChk = "" Then
                    e.Row.Cells(2).Text() = 0
                End If
                Dim expdate As String = DataBinder.Eval(e.Row.DataItem, "ccexpl").ToString().Trim()
                If expdate < curdate Then
                    e.Row.BackColor() = Drawing.Color.LightSkyBlue
                End If

                'These are the date cells & we are setting them to a class of text for the export to excel will format 
                'the date correctly.
                e.Row.Cells(5).Attributes.Add("class", "text")
                e.Row.Cells(6).Attributes.Add("class", "text")

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        Try

            Session("sCCNUMB") = GridView1.SelectedRow.Cells(3).Text
            Dim text1 As Date = GridView1.SelectedRow.Cells(5).Text
            Session("sCCEFFL") = Format(text1, "yyyyMMdd")
            Session("sMODE") = "UPDATE"
            Session("sLOADREC") = "1"

            Response.Redirect("CatMaint004.aspx")

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        Try

            Session("BPostBack") = True
            Response.Redirect("CatMaint001.aspx")

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Excel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Excel.Click

        Dim defaultExcludedColumns As New ArrayList()
        Dim strStyle As String = "<style>.text { mso-number-format:mm\/dd\/yyyy; } </style>"

        'The value that is used is the header text assign to the column
        defaultExcludedColumns.Add("Image")
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

    Protected Sub ButtonExpired_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExpired.Click
        If Session("sEXPIRED") = "0" Then
            Session("sEXPIRED") = "1"
            ButtonExpired.Text = "Show Current Items"
        Else
            Session("sEXPIRED") = "0"
            ButtonExpired.Text = "Show Expired Items"
        End If

        Dim dtl As DataTable = oiSeries.GetCatItemList(LblCatName.Text, Session("sEXPIRED"))

        'Bind the Returned Data to the Data Grid for Display

        GridView1.DataSource = dtl
        GridView1.DataBind()


    End Sub
End Class
