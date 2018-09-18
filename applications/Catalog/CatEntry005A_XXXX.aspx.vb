
Partial Class CatEntry005A
    Inherits System.Web.UI.Page

    Public objGen As New GeneralRoutines
    Public parms As New ClassSessionManager

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' get session manager object
            parms = Session("oSessionManager")

            If Not Page.IsPostBack Then
                parms.parseQueryString(Request)
                objGen.counter()
                btnMessage.Text = " Decoy Bag is Closed at the Present Time.<br /><br /> Please try back in <b>3</b> hours."
                'btnMessage.Text = " Decoy Bag is Closed at the Present Time.<br /><br /> System will be back up on <b>Sunday</b>."

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

                Session("UpdateOrd") = " "
                Session("SystemTitle") = "Decoy Bag Closed"
                Session("EventTitle") = "  "
                Session("PageTitle") = " "

                If Session("UrlStart") Is Nothing Then
                    Session.Add("UrlStart", String.Empty)
                    Dim urlStart As String = Request.UrlReferrer.AbsoluteUri
                    Dim i As Integer
                    i = urlStart.IndexOf("&tp")
                    If i > 0 Then
                        Dim newUrlReturn As String = String.Empty
                        newUrlReturn = urlStart.Substring(0, i)
                        newUrlReturn = newUrlReturn + "&tp=IS&pg=CBW202&ab=1&kp=" + parms.UserID
                        Session("UrlStart") = newUrlReturn
                    Else
                        Session("UrlStart") = urlStart
                    End If
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Dim urlStart As String = Session("UrlStart")
        Session.Abandon()
        Response.Redirect(urlStart)
        'Response.Redirect(Session("UrlStart"))

    End Sub
End Class
