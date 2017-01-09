
Partial Class FieldOpsMenu001
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
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
            If Session("BPostBack") Is Nothing Then
                Session.Add("BPostBack", New Boolean)
            End If
            Session("UpdateOrd") = " "
            Session("SystemTitle") = "Field Ops Menu"
            Session("EventTitle") = " "
            Session("PageTitle") = " "
            Session("BPostBack") = False
        Catch ex As Exception

        End Try


    End Sub

End Class
