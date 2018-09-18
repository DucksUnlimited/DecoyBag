
Partial Class CatEntry005
    Inherits System.Web.UI.Page

    Public parms As New ClassSessionManager

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            parms = Session("oSessionManager")

            btnMessage.Text = parms.EventID.Substring(0, 5) & "  has not been activated for the Decoy Bag at Present Time."

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnMessage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Dim urlStart As String = Session("UrlStart")
        Session.Abandon()
        Response.Redirect(urlStart)
        'Response.Redirect(Session("UrlStart"))

    End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Dim urlStart As String = Session("UrlStart")
        Session.Abandon()
        Response.Redirect(urlStart)
        'Response.Redirect(Session("UrlStart"))

    End Sub
End Class
