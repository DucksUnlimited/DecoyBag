Imports iSeriesDB.iSeriesCompMag

Partial Class CompMagCL1
    Inherits System.Web.UI.Page
    Public parms As New ClassSessionManager

    Dim mvLastError As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Reset last error
        mvLastError = ""

        Try
            ' get session manager object
            parms = Session("oSessionManager")
            If Not Page.IsPostBack Then


                If Session("UpdateOrd") Is Nothing Then
                    Session.Add("UpdateOrd", String.Empty)
                End If

                If Session("PageTitle") Is Nothing Then
                    Session.Add("PageTitle", String.Empty)
                End If

                If Session("SystemTitle") Is Nothing Then
                    Session.Add("SystemTitle", String.Empty)
                End If

                If Session("EventTitle") Is Nothing Then
                    Session.Add("EventTitle", String.Empty)
                End If

                Session("SystemTitle") = " "
                Session("UpdateOrd") = "Comp. Mag. Submit File Transfer"
                Session("EventTitle") = " "
                Session("PageTitle") = " "

            End If

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try


    End Sub

    Protected Sub ButtonSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSubmit.Click
        Dim rtnbool As Boolean

        Try
            'Reset last error
            mvLastError = ""

            rtnbool = CallCompMagCL1()

            If rtnbool = True Then
                Label1.Visible = True
                Label2.Visible = True
                Label3.Visible = True
                Label4.Visible = True
                Label5.Visible = True
                Label6.Visible = True
                Label7.Visible = False

            Else
                Label1.Visible = False
                Label2.Visible = False
                Label3.Visible = False
                Label4.Visible = False
                Label5.Visible = True
                Label6.Visible = True
                Label7.Visible = True

            End If

        Catch ex As Exception

            mvLastError = ex.Message
            'Return Nothing


        End Try


    End Sub

    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        'Reset last error
        mvLastError = ""


        Try
            Response.Redirect("CompMagMenu001.aspx")

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try

    End Sub
End Class
