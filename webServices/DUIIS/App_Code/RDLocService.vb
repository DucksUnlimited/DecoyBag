Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports DuckSystemDB.DuckSystem

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class RDLocService
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function GetRDLocation(ByVal sRDId As String) As String

        'Set up Ducksystem Call

        Dim location As String
        'dim srdcnaid as string
        'srdcnaid = "0" + sRDId

        Try
            'Get all records from GLDATA using SQL statement
            location = GetRDLocation(sRDId)

            Return location

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
