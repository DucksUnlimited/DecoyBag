Imports System.Data

Partial Class ML002
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ArrayFList As New ArrayList
        Dim myFSO As New Scripting.FileSystemObject
        Dim f = myFSO.GetFolder("//S1032895/qifs/labels")
        Dim i As Integer, fl

        Session("PageTitle") = "Membership Mailing Menu"

        If Not Page.IsPostBack Then

            i = 0
            For Each fl In f.Files
                If LCase(Right(fl.Name, 3)) = "txt" Then
                    i = i + 1
                    ArrayFList.Add(UCase(fl.Name))
                End If
            Next
            ArrayFList.Sort()
            FileList.DataSource = ArrayFList
            FileList.DataBind()
            FileList.SelectedIndex = 0
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim direction As String
        Dim newsyn As String

        If News.Checked Then
            newsyn = "Y"
        Else
            newsyn = "N"
        End If

        direction = ("ML001.aspx?mailreq=" + FileList.SelectedItem.Value + "&news=" + newsyn)

        Response.Redirect(direction)

    End Sub
End Class