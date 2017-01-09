Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.data

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class CatEntryServices
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function GetNextOrderSWR006(ByVal sType As String) As String

        'Get iSeries connection
        Dim oiSeries As New ClassHoldoledb

        Try

            'Call Get next order number routine
            Dim sOrder As String
            sOrder = oiSeries.GetNextOrderSWR006(sType, "", "", "", 0, "")

            Return sOrder

        Catch ex As Exception
            Return ""
        End Try
    End Function
    <WebMethod()> _
        Public Function SendEmail(ByVal sTo As String, ByVal SSubject As String, ByVal sMessage As String) As Boolean

        'Get email object
        Dim oEmail As New ClassEmail

        Try

            'Send the messages
            oEmail.SendEmailMessage("ikatz@ducks.org", sTo, "", SSubject, sMessage, "")
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    <WebMethod()> _
        Public Function GetCustomerDT(ByVal sChapter As String) As DataSet

        'Get iseries object
        Dim oiSeries As New ClassHoldoledb

        Try

            'Read customer record
            Dim ds1 As DataSet
            ds1 = oiSeries.LoadCustomerDS(sChapter)

            'Return dataset to web service caller
            Return ds1

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
