Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports iSeriesDB.iSeriesCatalog

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class RDEvOrders
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function ChkEventOrds(ByVal rdnum As String) As Boolean

        Dim rc As Boolean

        rc = ChkQuotOrderRD(rdnum)

        Return rc

    End Function

End Class
