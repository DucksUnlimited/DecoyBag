
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Create session variable if not found in memory yet
            If Session("PageTitle") Is Nothing Then
                Session.Add("PageTitle", String.Empty)
            End If
            If Session("SystemTitle") Is Nothing Then
                Session.Add("SystemTitle", String.Empty)
            End If
            If Session("EventTitle") Is Nothing Then
                Session.Add("EventTitle", String.Empty)
            End If

            'TODO - Get page title from query string
            UpdateOrd.Text = Session("UpdateOrd")
            SystemTitle.Text = Session("SystemTitle")
            PageTitle.Text = Session("PageTitle")
            EventTitle.text = Session("EventTitle")

            'Format date time info for page.
            DatePH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
            TimePH.Text = DateTime.Now.ToString("h:mm:ss - tt")
            DateSH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
            TimeSH.Text = DateTime.Now.ToString("h:mm:ss - tt")

            'Display error from session variable
            If Not IsNothing(Session("LastError")) Then
                lblError.Text = Session("LastError")
            Else 'If not declared yet, create blank session variable
                Session("LastError") = ""
            End If

        Catch ex As Exception

        End Try

    End Sub
End Class

