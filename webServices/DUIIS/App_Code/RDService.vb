Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports DuckSystemDB.DuckSystem

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class RDService
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function GetRD(ByVal sState As String) As DataSet

        'Set up Ducksystem Call
        'Dim objDucks As New DuckSystemDB.DuckSystem

        Dim ds1 As New DataSet
        Dim dt1 As New DataTable

        Try
            'Get all records from GLDATA using SQL statement
            dt1 = GetRDListState(sState)

            'Return the data set
            ds1.Tables.Add(dt1)

            Return ds1

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
