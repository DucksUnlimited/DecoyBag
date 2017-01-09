
Partial Class CatEntryLoad3
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

        If Session("UrlStart") Is Nothing Then
            Session.Add("UrlStart", String.Empty)
        End If

        If Not Page.IsPostBack Then

            If Not Session("BPostBack") Then
                Dim urlStart As String = Request.UrlReferrer.AbsoluteUri
                Session("UrlStart") = urlStart
            End If
            Session("UpdateOrd") = "Enter New Order by RD/Vol"
            Session("SystemTitle") = "Event Merchandise Order Entry"
            Session("PageTitle") = "Manually Start Catalog Entry"
            Session("EventTitle") = ""

        End If

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim direction As String

        direction = ("CatEntry001.aspx?evid=" + evid.Text + "&evdt=" + evdate.Text)
        direction += ("&usrid=" + usrid.Text)
        direction += ("&rdid=" + rdid.Text + "&rdnum=" + rdnum.Text)

        Response.Redirect(direction)

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Response.Redirect(Session("UrlStart"))

    End Sub
End Class
