Imports IBM.Data.DB2.iSeries
Partial Class OrderSelect
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim connectionString As ConnectionStringSettingsCollection = ConfigurationManager.ConnectionStrings
        Dim connString As String = connectionString("S1032895").ToString()
        Dim connS As New iDB2Connection(connString)
        connS.Open()

        Dim cmd As New iDB2Command("select HTNUM, HTMDM4 from fsdata50.chtmst where htnum=@htnum", connS)
        cmd.Parameters.Add("@htnum", iDB2DbType.iDB2Char, 8)
        cmd.Parameters("@htnum").Value = lblHtnum.Text.Trim

        Dim dr As iDB2DataReader
        dr = cmd.ExecuteReader()

        If dr.Read Then
            Dim orgOrd As String = dr("htmdm4")

            Dim cmd2 As New iDB2Command("select HTNUM, HTMDM4, HTDTE, HTORSC, HTDLBY from fsdata50.chtmst where htmdm4=@htmdm4 order by htnum", connS)
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
