Imports System.Data
Imports iSeriesDB.iSeriesCatalog

Partial Class OrderSelect
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim dt As DataTable = GetOrder(lblHtnum.Text.Trim)
        If dt.Rows.Count > 0 Then
            Dim orgOrd As String = dt.Rows(0).Item("htmdm4")

            Dim dt2 As DataTable = getOrderList(orgOrd)

            GridView1.DataSource = dt2
            GridView1.DataBind()

        End If

    End Sub
End Class
