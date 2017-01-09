Imports IBM.Data.DB2.iSeries
Imports System.Data

Partial Class OrdersTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim Balloon As SkySof.CustomTip
        Dim connectionString As ConnectionStringSettingsCollection = ConfigurationManager.ConnectionStrings
        Dim connString As String = connectionString("S1032895").ToString()
        Dim conn As New iDB2Connection(connString)
        conn.Open()

        Dim cmd As New iDB2Command("select * from fsdata50.chtmst where htnum=@htnum", conn)
        cmd.Parameters.Add("@htnum", iDB2DbType.iDB2Char, 8)
        cmd.Parameters("@htnum").Value = Request("HTNUM")

        'DatePH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
        'TimePH.Text = DateTime.Now.ToString("h:mm:ss - tt")
        'DateSH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
        'TimeSH.Text = DateTime.Now.ToString("h:mm:ss - tt")

        Dim dr As iDB2DataReader
        dr = cmd.ExecuteReader()

        If dr.Read Then
            lblhtnum.Text = dr("HTNUM")
            lblhtmdm4.Text = dr("HTMDM4")
            lblcs21sb.Text = dr("CS21SB")
            lblhtorsc.Text = dr("HTORSC")
            lblhtdte.Text = dr("HTDTE")
            lblhtsno.Text = dr("HTSNO")
            lblhtsnam.Text = dr("HTSNAM")
            lblhtsad1.Text = dr("HTSAD1")
            lblhtsad2.Text = dr("HTSAD2")
            lblhtscty.Text = dr("HTSCTY")
            lblhtszip.Text = dr("HTSZIP")
            lblhtszp2.Text = dr("HTSZP2")
            lblhtcno.Text = dr("HTCNO")
            lblhtbnam.Text = dr("HTBNAM")
            lblhtbad1.Text = dr("HTBAD1")
            lblhtbad2.Text = dr("HTBAD2")
            lblhtbcty.Text = dr("HTBCTY")
            lblhtbzip.Text = dr("HTBZIP")
            lblhtbzp2.Text = dr("HTBZP2")
            lblhtcpo.Text = dr("HTCPO")
            lblhtcodt.Text = dr("HTCODT")
        End If

        Dim cmdD As New iDB2Command("select htnum, HTLLIN, htqor, htlqsh, htlqbo, htlun, htlpnm, htldsc, htlupr from fsdata50.chtlin where htnum=@htnum", conn)
        cmdD.Parameters.Add("@htnum", iDB2DbType.iDB2Char, 8)
        cmdD.Parameters("@htnum").Value = Request("HTNUM")

        Dim drD As iDB2DataReader
        drD = cmdD.ExecuteReader()

        GridView1.DataSource = drD
        GridView1.DataBind()

        drD.Close()
        cmdD.Dispose()

        dr.Close()
        cmd.Dispose()
        conn.Close()

    End Sub

End Class
