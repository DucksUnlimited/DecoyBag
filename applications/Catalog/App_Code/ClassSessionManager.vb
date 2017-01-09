Imports Microsoft.VisualBasic
Imports System.Web

Public Class ClassSessionManager

    Dim mvIDKey As String
    Dim mvEventID As String
    Dim mvEventDt As String
    Dim mvEventName As String
    Dim mvUsrFName As String
    Dim mvUsrLName As String
    Dim mvRDID As String
    Dim mvRDNum As String
    Dim mvRdOnly As Boolean
    Dim mvRDName As String
    Dim mvRDEMail As String
    Dim mvCorName As String
    Dim mvCorEMail As String
    Dim mvUsrID As String
    Dim mvCatEMail As String = "catalog@ducks.org"
    Dim mvCatName As String
    Dim mvCurOrder As String
    Dim mvTotPrice As String
    Dim mvUpDtOrder As String
    Dim mvFFLPDF As String
    Dim mvApproved As String

#Region "Properties"

    Public Property CatEMail() As String
        Get
            Return mvCatEMail
        End Get
        Set(ByVal value As String)
            mvCatEMail = value
        End Set
    End Property

    Public Property CatName() As String
        Get
            Return mvCatName
        End Get
        Set(ByVal value As String)
            mvCatName = value
        End Set
    End Property

    Public Property IDKey() As String
        Get
            If mvIDKey Is Nothing Then
                mvIDKey = ""
            End If

            Return mvIDKey
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvIDKey = ""
            Else
                mvIDKey = value
            End If
        End Set
    End Property

    Public Property EventID() As String
        Get
            If mvEventID Is Nothing Then
                mvEventID = ""
            End If

            Return mvEventID
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvEventID = ""
            Else
                mvEventID = value
            End If
        End Set
    End Property

    Public Property EventDate() As String
        Get
            If mvEventDt Is Nothing Then
                mvEventDt = ""
            End If

            Return mvEventDt
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvEventDt = ""
            Else
                mvEventDt = value
            End If
        End Set
    End Property

    Public Property EventName() As String
        Get
            If mvEventName Is Nothing Then
                mvEventName = ""
            End If

            Return mvEventName
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvEventName = ""
            Else
                mvEventName = value
            End If
        End Set
    End Property

    Public Property RdOnly() As Boolean
        Get
            Return mvRdOnly
        End Get
        Set(ByVal value As Boolean)
            mvRdOnly = value
        End Set
    End Property

    Public Property UsrFName() As String
        Get
            If mvUsrFName Is Nothing Then
                mvUsrFName = ""
            End If

            Return mvUsrFName
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvUsrFName = ""
            Else
                mvUsrFName = value
            End If
        End Set
    End Property

    Public Property UsrLName() As String
        Get
            If mvUsrLName Is Nothing Then
                mvUsrLName = ""
            End If

            Return mvUsrLName
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvUsrLName = ""
            Else
                mvUsrLName = value
            End If
        End Set
    End Property

    Public Property RDID() As String
        Get
            If mvRDID Is Nothing Then
                mvRDID = ""
            End If

            Return mvRDID
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvRDID = ""
            Else
                mvRDID = value
            End If
        End Set
    End Property

    Public Property RDNUM() As String
        Get
            If mvRDNum Is Nothing Then
                mvRDID = ""
            End If

            Return mvRDNum
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvRDNum = ""
            Else
                mvRDNum = value
            End If
        End Set
    End Property

    Public Property RDName() As String
        Get
            If mvRDName Is Nothing Then
                mvRDName = ""
            End If

            Return mvRDName
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvRDName = ""
            Else
                mvRDName = value
            End If
        End Set
    End Property

    Public Property RDEMail() As String
        Get
            If mvRDEMail Is Nothing Then
                mvRDEMail = ""
            End If

            Return mvRDEMail
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvRDEMail = ""
            Else
                mvRDEMail = value
            End If
        End Set
    End Property

    Public Property CorName() As String
        Get
            If mvCorName Is Nothing Then
                mvCorName = ""
            End If

            Return mvCorName
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvCorName = ""
            Else
                mvCorName = value
            End If
        End Set
    End Property

    Public Property CorEMail() As String
        Get
            If mvCorEMail Is Nothing Then
                mvCorEMail = ""
            End If

            Return mvCorEMail
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvCorEMail = ""
            Else
                mvCorEMail = value
            End If
        End Set
    End Property

    Public Property UserID() As String
        Get
            If mvUsrID Is Nothing Then
                mvUsrID = ""
            End If

            Return mvUsrID
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                mvUsrID = ""
            Else
                mvUsrID = value
            End If
        End Set
    End Property

    Public Property CurOrder() As String
        Get
            Return mvCurOrder
        End Get
        Set(ByVal value As String)
            mvCurOrder = value
        End Set
    End Property

    Public Property TotPrice() As String
        Get
            Return mvTotPrice
        End Get
        Set(ByVal value As String)
            mvTotPrice = value
        End Set
    End Property

    Public Property UpDtOrder() As String
        Get
            Return mvUpDtOrder
        End Get
        Set(ByVal value As String)
            mvUpDtOrder = value
        End Set
    End Property

    Public Property FFLPDF() As String
        Get
            Return mvFFLPDF
        End Get
        Set(ByVal value As String)
            mvFFLPDF = value
        End Set
    End Property

    Public Property Approved() As String
        Get
            Return mvApproved
        End Get
        Set(ByVal value As String)
            mvApproved = value
        End Set
    End Property

#End Region

    Public Sub parseQueryString(ByVal request As HttpRequest)
        Try
            CatEMail = mvCatEMail
            CatName = mvCatName
            IDKey = request.QueryString("idkey")
            EventID = request.QueryString("evid")
            EventDate = request.QueryString("evdt")
            EventName = request.QueryString("evnm")
            UsrFName = request.QueryString("usrfnm")
            UsrLName = request.QueryString("usrlnm")
            RdOnly = mvRdOnly
            RDID = request.QueryString("rdid")
            RDNUM = request.QueryString("rdnum")
            RDName = request.QueryString("rdnm")
            RDEMail = request.QueryString("rdemail")
            CorName = request.QueryString("cornm")
            CorEMail = request.QueryString("coremail")
            UserID = request.QueryString("usrid")

            CurOrder = request.QueryString("ord")
            TotPrice = request.QueryString("totprice")

            UpDtOrder = request.QueryString("updtord")
            FFLPDF = request.QueryString("fflpdf")
            Approved = request.QueryString("approved")

        Catch ex As Exception

        End Try
    End Sub

    Public Function IsLoggedIn() As Boolean
        Try
            If IDKey = "IASJ" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
