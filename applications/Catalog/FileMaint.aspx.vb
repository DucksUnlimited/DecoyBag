Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim objiSeries As ClassiSeriesDatabase

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click


        Dim sSQLWhere As String = ""

        '------------------------------------------------------------
        'If customer number passed in, add the SQL WHERE clause
        '------------------------------------------------------------
        If Trim(txtCusnum.Text) <> "" Then
            sSQLWhere = sSQLWhere & " WHERE CUSNUM =" & txtCusnum.Text
        ElseIf Trim(txtName.Text) <> "" Then
            sSQLWhere = sSQLWhere & " WHERE TRIM(LSTNAM) LIKE '" & txtName.Text & "'"
        End If

        '------------------------------------------------------------
        'Run the SQL ODBC Query to Get Us Some Data
        '------------------------------------------------------------
        Dim dt1 As DataTable = objiSeries.GetCustomerList(sSQLWhere)

        '------------------------------------------------------------
        'Bind the Returned Data to the Data Grid for Display
        '------------------------------------------------------------
        GridView1.DataSource = dt1
        GridView1.DataBind()

    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


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

        '------------------------------------------------------------
        'Reset error
        '------------------------------------------------------------
        If objiSeries.IsConnected = False Then
            lblError.Text = "Could not connect to iSeries."
        Else
            lblError.Text = ""
        End If

    End Sub

    Protected Sub ButtonReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonReset.Click
        '------------------------------------------------------------
        'Clear/reset entry fields
        '------------------------------------------------------------
        txtCusnum.Text = ""
        txtName.Text = ""
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        'Select customer and save in session
        Session("sCUSNUM") = GridView1.SelectedRow.Cells(1).Text
        Session("sMODE") = "UPDATE"
        Session("sLOADREC") = "1"

        'Go to details page
        Response.Redirect("EditCust.aspx")

    End Sub

    Protected Sub ButtonNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonNew.Click

        Session("sMODE") = "INSERT"
        Session("sLOADREC") = "0"

        'Go to details page
        Response.Redirect("EditCust.aspx")

    End Sub
End Class
