Imports iSeriesDB.iSeriesCatalog
Imports System.Data

Partial Class CatGroup002
    Inherits System.Web.UI.Page
    Public parms As New ClassSessionManager
    'Public oiSeries As New ClassiSeriesDataAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try


            'Get group record if LoadREc parm passed
            If Session("sLOADREC") = "1" Then

                Dim dtgroup1 As DataTable = GetGroup(Session("sCATGRP"))
                Dim dr1 As DataRow

                'Move to screen if
                If Not IsNothing(dtgroup1) Then
                    dr1 = dtgroup1.Rows(0)
                    TxtGroup.Text = dr1("CATGRP").ToString
                    TxtDesc.Text = dr1("CATDESC").ToString
                    DropDownType.SelectedIndex = DropDownType.Items.IndexOf(DropDownType.Items.FindByValue(dr1("CATTYPE").ToString().Trim()))
                    'TxtType.Text = dr1("CATTYPE").ToString
                End If

                'Reset record load flag
                Session("sLOADREC") = "0"

                'Enable disable screen fields on record load
                Select Case Session("sMode")

                    Case "INSERT"
                        TxtGroup.Enabled = False
                    Case "UPDATE"
                        TxtGroup.Enabled = False
                End Select
            Else 'Load record = 0

                TxtGroup.Enabled = False

                If Session("sMode") = "INSERT" Then
                    TxtGroup.Text = Session("sGROUPNUM")
                End If



            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        Try

            'Go to main
            Session("BPostBack") = True
            Response.Redirect("CatGroup001.aspx")

        Catch ex As Exception

        End Try

    End Sub


    Protected Sub ButtonSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSave.Click

        'Perform insert/update/delete and redirect if done
        Dim rtnbool As Boolean

        Try

            Select Case Session("sMode")
                Case "INSERT"
                    rtnbool = InsertGroup(TxtGroup.Text, TxtDesc.Text, DropDownType.SelectedItem.Value)
                Case "UPDATE"
                    rtnbool = UpdateGroup(TxtGroup.Text, TxtDesc.Text, DropDownType.SelectedItem.Value)
            End Select

            'Display error or return to main if done
            If rtnbool = False Then
                LblError.Text = "Update error"
            Else
                Response.Redirect("CatGroup001.aspx")
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
