Imports iSeriesDB.iSeriesCatalog
Imports System.Data
Partial Class CatMaint002
    Inherits System.Web.UI.Page
    Public parms As New ClassSessionManager
    ' Public oiSeries As New ClassiSeriesDataAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Page.IsPostBack Then
                Dim wkdate As String

                If Session("sLOADREC") = "1" Then
                    Dim dtcat1 As DataTable = GetCat(Session("sCCCON#"))
                    Dim dr1 As DataRow
                    If Not IsNothing(dtcat1) Then
                        dr1 = dtcat1.Rows(0)
                        TxtCatName.Text = dr1("CCCON#").ToString
                        wkdate = dr1("CCEFFD").ToString
                        CatEffDate.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                        wkdate = dr1("CCEXPD").ToString
                        CatExpDate.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                        TxtCatType.Text = dr1("CCUDV2").ToString
                    End If
                    Session("sLOADREC") = "0"
                   
                    TxtCatName.Enabled = False

                Else
                    wkdate = DateTime.Now.ToString("yyyyMMdd")
                    CatEffDate.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                    wkdate = "99991231"
                    CatExpDate.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)

                    TxtCatName.Enabled = True

                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ButtonSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        Dim rtnbool As Boolean

        Try

            Select Case Session("sMode")
                Case "INSERT"
                    rtnbool = InsertCat(TxtCatName.Text, Format(CatEffDate.SelectedDate, "yyyyMMdd"), Format(CatExpDate.SelectedDate, "yyyyMMdd"), TxtCatType.Text)
                Case "UPDATE"
                    rtnbool = UpdateCat(TxtCatName.Text, Format(CatEffDate.SelectedDate, "yyyyMMdd"), Format(CatExpDate.SelectedDate, "yyyyMMdd"), TxtCatType.Text)
            End Select
            If rtnbool = False Then
                'LblError.Text = oiSeries.GetLastError
            Else
                Session("sCCCON#") = TxtCatName.Text
                Session("sEXPIRED") = "0"
                Response.Redirect("CatMaint003.aspx")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        Try
            Session("BPostBack") = True
            Response.Redirect("CatMaint001.aspx")

        Catch ex As Exception

        End Try
    End Sub
End Class
