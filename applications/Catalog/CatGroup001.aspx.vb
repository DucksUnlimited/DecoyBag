Imports System.Data
Imports SQLAccessDB.SQLAccess


Partial Class CatGroup001
    Inherits System.Web.UI.Page

    Public parms As New ClassSessionManager

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
                Session("UpdateOrd") = "Catalog Group Maintenance"
                Session("EventTitle") = " "
                Session("PageTitle") = " "

                If Session("BPostBack") Is Nothing Then
                    Session.Add("BPostBack", New Boolean)
                    Session("BPostBack") = False
                End If
                If Not Session("BPostBack") Then
                    Dim urlStart As String = Request.UrlReferrer.AbsoluteUri
                    If Session("UrlStart") Is Nothing Then
                        Session.Add("UrlStart", String.Empty)
                    End If
                    Session("UrlStart") = urlStart
                End If

                'Run the SQL Query to get data
                Dim dtl As DataTable = GetGroupList()
                'Bind the Returned Data to the Data Grid for Display
                GridView1.DataSource = dtl
                GridView1.DataBind()

            End If

        Catch ex As Exception

        End Try


    End Sub


    Protected Sub ButtonNew_Click(sender As Object, e As EventArgs) Handles ButtonNew.Click
        Try

            Session("sMODE") = "INSERT"
            Session("sLOADREC") = ""

            Dim sGroup As String = ""

            sGroup = GetNextGroupNumber()
            If sGroup.Trim = "" Then
                Throw New Exception("Invalid next group CatGroup001")
            End If

            Session("sGROUPNUM") = sGroup


            'Go to details page
            Response.Redirect("CatGroup002.aspx")


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        Try

            'Select group and save in sessionj

            Session("sCATGRP") = GridView1.SelectedRow.Cells(1).Text
            Session("sMODE") = "UPDATE"
            Session("sLOADREC") = "1"

            'Go to details page
            Response.Redirect("CatGroup002.aspx")

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        Try

            Dim urlStart As String = Session("UrlStart")
            Session.Abandon()
            Response.Redirect(urlStart)

        Catch ex As Exception

        End Try

    End Sub

End Class
