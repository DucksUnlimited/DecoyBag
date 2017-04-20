Imports System.IO
Imports System.Data
Imports iSeriesDB.iSeriesCatalog

Partial Class ProductDetails
    Inherits System.Web.UI.UserControl

    'Public oiSeries As New ClassiSeriesDataAccess

    Public Property CCNumb() As String
        Get
            If ViewState("CCNumb") Is Nothing Then
                Return ""
            End If
            Return DirectCast(ViewState("CCNumb"), String)
        End Get
        Set(ByVal value As String)

            ViewState("CCNumb") = value
        End Set
    End Property

    Public itmdsc As String

    Protected Overrides Sub OnPreRender(e As EventArgs)

        Try

            MyBase.OnPreRender(e)

            Dim itmimg As New Image
            itmimg.ImageUrl = "Images/" + CCNumb.ToString().Trim() + ".jpg"
            Dim desc As New Label
            desc.Text = " "
            Dim details As New Literal
            details.Text = "/Doc/Descriptions/" + CCNumb.ToString().Trim() + ".txt"
            Dim dttooltip As New DataTable
            dttooltip.Columns.Add("itmdesc", Type.GetType("System.String"))
            dttooltip.Columns.Add("itmimage", Type.GetType("System.String"))
            dttooltip.Columns.Add("ttdetail", Type.GetType("System.String"))

            Dim dr As DataRow = dttooltip.NewRow()
            dr(0) = desc.Text
            dr(1) = itmimg.ImageUrl.ToString.Trim
            dr(2) = details.Text
            dttooltip.Rows.Add(dr)
            ProductsView.DataSource = dttooltip

            DataBind()

        Catch ex As Exception
            Dim msg As String = ex.Message
        End Try

    End Sub

    Protected Sub ProductsView_DataBound(ByVal sender As Object, ByVal e As EventArgs)

        Dim imgPath As String = "/Images/" + CCNumb.ToString().Trim() + ".jpg"
        Dim txtPath As String = "/Doc/Descriptions/" + CCNumb.ToString().Trim() + ".txt"
        Dim itmimg As Image = CType(ProductsView.FindControl("itmimage"), Image)
        Dim itmimage As System.Web.UI.WebControls.Image = CType(ProductsView.FindControl("itmimage"), System.Web.UI.WebControls.Image)
        Dim lit As System.Web.UI.WebControls.Literal = CType(ProductsView.FindControl("ttdetail"), Literal)
        Dim desc As Label = CType(ProductsView.FindControl("itmdesc"), Label)
        Dim ttText As String
        Dim toolHeader As String = GetItemDesc(CCNumb.ToString().Trim())

        desc.Text = toolHeader

        'If image Is Nothing Then
        '    Return
        'End If

        If Not File.Exists(imgPath) Then
            itmimage.ImageUrl = "Images/ImageNotAvailable.jpg"
        Else
            itmimage.ImageUrl = "Images/" + CCNumb.ToString().Trim() + ".jpg"
        End If

        If File.Exists(txtPath) Then
            ttText = "<ul>"
            Dim objStreamReader As StreamReader
            objStreamReader = File.OpenText(txtPath)

            While objStreamReader.Peek <> -1
                ttText += "<li style=""text-align:left;text-wrap:normal;"">" + objStreamReader.ReadLine().Trim() + "</li>"
            End While
            objStreamReader.Close()
            ttText += "</ul></div>"
        Else
            ttText = " "
        End If

        lit.Text = ttText.ToString.Trim

    End Sub
End Class
