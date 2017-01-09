Imports System
Imports System.Data

Partial Class EditCust
    Inherits System.Web.UI.Page

    Dim objiSeries As ClassiSeriesDatabase


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '------------------------------------------------------------
        'If not connected, connect to iSeries and save to session. 
        'Otherwise grab connection from session
        '------------------------------------------------------------
        If Not IsNothing(Session("objiSeries")) Then
            objiSeries = Session("objiSeries")
        Else
            objiSeries = New ClassiSeriesDatabase
            Session("objiSeries") = objiSeries
        End If


        'Get customer record if LoadRec parm passed
        If Session("sLOADREC") = "1" Then

            Dim dtcust1 As DataTable = objiSeries.GetCustomer(Session("sCUSNUM"))
            Dim dr1 As DataRow

            'Move to screen if 
            If Not IsNothing(dtcust1) Then
                dr1 = dtcust1.Rows(0)
                txtCustomer.Text = dr1("CUSNUM").ToString
                txtLastname.Text = dr1("LSTNAM").ToString
                txtStreet.Text = dr1("STREET").ToString
                txtInit.Text = dr1("INIT").ToString
                txtCity.Text = dr1("CITY").ToString
                txtState.Text = dr1("STATE").ToString
                txtZipcode.Text = dr1("ZIPCOD").ToString
            End If

            'Reset record load flag
            Session("sLOADREC") = "0"

            'Enable disable screen fields on record load
            Select Case Session("sMode")

                Case "INSERT"
                    txtCustomer.Enabled = True
                Case "UPDATE"
                    txtCustomer.Enabled = False
                Case "DELETE"
                    txtCustomer.Enabled = False

            End Select

        Else 'Load record = 0


        End If

    End Sub


    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        'Go to main
        Response.Redirect("Default.aspx")

    End Sub

    Protected Sub ButtonDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click

        'Set delete mode
        Session("sMode") = "DELETE"

        'Perform insert/update/delete and redirect if done
        Dim rtnbool As Boolean
        Select Case Session("sMode")
            Case "DELETE"
                rtnbool = objiSeries.DeleteCustomer(txtCustomer.Text)
        End Select

        'Display error or return to main if done
        If rtnbool = False Then
            lblError.Text = objiSeries.GetLastError
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub ButtonUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUpdate.Click


        'Perform insert/update/delete and redirect if done
        Dim rtnbool As Boolean
        Select Case Session("sMode")
            Case "INSERT"
                rtnbool = objiSeries.InsertCustomer(txtCustomer.Text, txtLastname.Text, txtInit.Text, txtStreet.Text, txtCity.Text, txtState.Text, txtZipcode.Text)
            Case "UPDATE"
                rtnbool = objiSeries.UpdateCustomer(txtCustomer.Text, txtLastname.Text, txtInit.Text, txtStreet.Text, txtCity.Text, txtState.Text, txtZipcode.Text)
            Case "DELETE"
                'rtnbool = objiSeries.DeleteCustomer(txtCustomer.Text)
        End Select

        'Display error or return to main if done
        If rtnbool = False Then
            lblError.Text = objiSeries.GetLastError
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub
End Class
