Imports System.Data
Imports iSeriesDB.iSeriesCompMag

Partial Class CompMagMaint002
    Inherits System.Web.UI.Page
    Public parms As New ClassSessionManager

    Dim mvLastError As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Reset last error
        mvLastError = ""
        Try
            ' get session manager object
            parms = Session("oSessionManager")
            If Not Page.IsPostBack Then

                Dim wkdate As String

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
                Session("UpdateOrd") = "Complimentary Magazine Maintenance"
                Session("EventTitle") = " "
                Session("PageTitle") = "Fields with an * are required!"

                'Build State table
                ddlState.DataSource = Session("DTState")
                ddlState.DataTextField = "stname"
                ddlState.DataValueField = "stdust"
                ddlState.DataBind()
                ddlState.Items.Insert(0, "  Select a State")
                ddlState.Items.Item(0).Value = "  "
                ddlState.SelectedIndex = 0

                TxtDept.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                TxtSort.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                TxtLine1.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                TxtLine2.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                TxtLine3.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                TxtLine4.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                TxtLine5.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                TxtDCity.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                TxtDZip.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                TxtFCity.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")

                ddlForeign.Attributes.Add("onchange", "ForeignEdit()")

                If Session("sLOADREC") = "1" Then
                    Dim dtitml As DataTable = GetMagSequence(Session("sSEQUENCE"))
                    Dim dr1 As DataRow
                    If Not IsNothing(dtitml) Then
                        dr1 = dtitml.Rows(0)


                        TxtSeqNum.Text = dr1("CPSEQ").ToString
                        TxtSeqNum.Visible = True

                        wkdate = dr1("CPDATE").ToString
                        MagEffDate.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                        TxtDept.Text = dr1("CPDEPT").ToString.Trim
                        TxtSort.Text = dr1("CPSORT").ToString.Trim
                        TxtLine1.Text = dr1("CPLIN1").ToString.Trim
                        TxtLine2.Text = dr1("CPLIN2").ToString.Trim
                        TxtLine3.Text = dr1("CPLIN3").ToString.Trim
                        TxtLine4.Text = dr1("CPLIN4").ToString.Trim
                        TxtLine5.Text = dr1("CPLIN5").ToString.Trim

                        ddlForeign.SelectedIndex = ddlForeign.Items.IndexOf(ddlForeign.Items.FindByValue(dr1("CPCODE")))
                        ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dr1("CPSTAT")))
                        TxtDCity.Text = dr1("CPCITY").ToString.Trim
                        TxtDZip.Text = dr1("CPZIP").ToString.Trim
                        TxtFCity.Text = dr1("CPCNTY").ToString.Trim
                        If dr1("CPCODE") = "D" Then
                            TxtDCity.Enabled = True
                            ddlState.Enabled = True
                            TxtDZip.Enabled = True
                            TxtFCity.Enabled = False
                        Else
                            TxtDCity.Enabled = False
                            ddlState.Enabled = False
                            TxtDZip.Enabled = False
                            TxtFCity.Enabled = True
                        End If


                    End If
                    Session("sLOADREC") = "0"

                  
                Else
                    wkdate = DateTime.Now.ToString("yyyyMMdd")
                    MagEffDate.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                    ButtonDelete.Visible = False



                End If
            End If

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try
    End Sub

    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        Try
            'Reset last error
            mvLastError = ""

            Response.Redirect("CompMagMaint001.aspx")

        Catch ex As Exception

            mvLastError = ex.Message
            'Return Nothing


        End Try


    End Sub

    Protected Sub ButtonSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSave.Click

        Dim rtnbool As Boolean
        Dim MagSeq As String = String.Empty
        Dim NewMagSeq As Integer

        Try
            'Reset last error
            mvLastError = ""


            If ddlForeign.SelectedItem.Value = "D" Then
                TxtFCity.Text = " "
            Else
                ddlState.SelectedItem.Value = " "
                TxtDCity.Text = " "
                TxtDZip.Text = " "
            End If

            Select Case Session("sMode")
                Case "INSERT"
                    MagSeq = GetMaxMagSeq()
                    NewMagSeq = MagSeq
                    NewMagSeq += 1
                    rtnbool = InsertMagComp(TxtLine1.Text.ToUpper.Trim, TxtLine2.Text.ToUpper.Trim, TxtLine3.Text.ToUpper.Trim, TxtLine4.Text.ToUpper.Trim, TxtLine5.Text.ToUpper.Trim, TxtDCity.Text.ToUpper.Trim, ddlState.SelectedItem.Value, TxtDZip.Text.ToUpper.Trim, TxtFCity.Text.ToUpper.Trim, ddlForeign.SelectedItem.Value, NewMagSeq, Format(MagEffDate.SelectedDate, "yyyyMMdd"), TxtDept.Text.ToUpper.Trim, TxtSort.Text.ToUpper.Trim)
                Case "UPDATE"
                    rtnbool = UpdatetMagComp(TxtLine1.Text.ToUpper.Trim, TxtLine2.Text.ToUpper.Trim, TxtLine3.Text.ToUpper.Trim, TxtLine4.Text.ToUpper.Trim, TxtLine5.Text.ToUpper.Trim, TxtDCity.Text.ToUpper.Trim, ddlState.SelectedItem.Value, TxtDZip.Text.ToUpper.Trim, TxtFCity.Text.ToUpper.Trim, ddlForeign.SelectedItem.Value, TxtSeqNum.Text, Format(MagEffDate.SelectedDate, "yyyyMMdd"), TxtDept.Text.ToUpper.Trim, TxtSort.Text.ToUpper.Trim)
            End Select

            If rtnbool = True Then
                Response.Redirect("CompMagMaint001.aspx")
            Else
                LblError.Text = "Save Error"
            End If
        Catch ex As Exception

            mvLastError = ex.Message
            'Return Nothing


        End Try


    End Sub

    Protected Sub ButtonDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click

        Dim rtnbool As Boolean

        Try

            'Reset last error
            mvLastError = ""

            rtnbool = DeleteMagComp(TxtSeqNum.Text)

            If rtnbool = True Then
                Response.Redirect("CompMagMaint001.aspx")
            Else
                LblError.Text = "Delete Error"
            End If

        Catch ex As Exception

            mvLastError = ex.Message
            'Return Nothing


        End Try




    End Sub
End Class
