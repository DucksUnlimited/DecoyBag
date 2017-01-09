Imports IBM.Data.DB2.iSeries
Partial Class OrderSelectDU
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            'lblHtnum.Text = "00883400"

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim connectionString As ConnectionStringSettingsCollection = ConfigurationManager.ConnectionStrings
        Dim connString As String = connectionString("S1032895").ToString()
        Dim connS As New iDB2Connection(connString)
        connS.Open()

        Dim cmd As New iDB2Command("select HTNUM, HTMDM4 from chtmst where htnum=@htnum", connS)
        cmd.Parameters.Add("@htnum", iDB2DbType.iDB2Char, 8)
        cmd.Parameters("@htnum").Value = lblHtnum.Text.Trim

        Dim dr As iDB2DataReader
        dr = cmd.ExecuteReader()

        If dr.Read Then
            Dim orgOrd As String = dr("htmdm4")

            Dim cmd2 As New iDB2Command("select HTNUM, HTMDM4, HTDTE, HTORSC, HTDLBY from chtmst where htmdm4=@htmdm4 order by htnum", connS)
            cmd2.Parameters.Add("@htmdm4", iDB2DbType.iDB2Char, 8)
            cmd2.Parameters("@htmdm4").Value = orgOrd

            Dim dr2 As iDB2DataReader
            dr2 = cmd2.ExecuteReader()

            GridView1.DataSource = dr2
            GridView1.DataBind()

            dr2.Close()
            cmd2.Dispose()
        End If

        dr.Close()
        cmd.Dispose()
        connS.Close()

    End Sub
End Class
