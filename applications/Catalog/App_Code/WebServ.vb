Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data.OleDb
Imports System.Data

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WebServ
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function GetFile() As DataSet
        'Get iSeries Connection
        'Dim oiSeries As New ClassiSeriesDataAccess
        Dim connectionString As ConnectionStringSettingsCollection = ConfigurationManager.ConnectionStrings
        Dim connString As String = connectionString("S1032895.OleDb").ToString

        Dim connoledb As OleDbConnection = New System.Data.OleDb.OleDbConnection(connString)
        Dim ds1 As New DataSet
        Dim dt1 As New DataTable

        Try
            'Get all records from GLDATA using SQL statement
            Dim adapter As New OleDbDataAdapter("select * from fscust50.gldata", connoledb)

            'Fill the table
            adapter.Fill(dt1)

            'Return the data set
            ds1.Tables.Add(dt1)

            Return ds1

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
