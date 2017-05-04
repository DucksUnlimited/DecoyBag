Imports System.Data
Imports iSeriesDB.iSeriesCompMag
Imports iSeriesDB.iSeriesCatalog



Partial Class CompMagMaint001
    Inherits System.Web.UI.Page
    Public parms As New ClassSessionManager

    Dim mvLastError As String

#Region "Properties"

    Private Property DT() As DataTable
        Get
            If Session("DT") Is Nothing Then
                Session.Add("DT", New DataTable())
            End If

            Return DirectCast(Session("DT"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("DT") = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Reset last error
        mvLastError = ""

        Try
            ' get session manager object
            parms = Session("oSessionManager")

            If RB1.Checked Then
                tbFName.Enabled = True
                tbTName.Enabled = True
                tbCity.Enabled = False
                ddlState.Enabled = False
                tbZip.Enabled = False
                tbSequence.Enabled = False
                tbFDept.Enabled = False
                tbTDept.Enabled = False
            End If
            If RB2.Checked Then
                tbFName.Enabled = False
                tbTName.Enabled = False
                tbCity.Enabled = True
                ddlState.Enabled = False
                tbZip.Enabled = False
                tbSequence.Enabled = False
                tbFDept.Enabled = False
                tbTDept.Enabled = False
            End If
            If RB3.Checked Then
                tbFName.Enabled = False
                tbTName.Enabled = False
                tbCity.Enabled = False
                ddlState.Enabled = True
                tbZip.Enabled = False
                tbSequence.Enabled = False
                tbFDept.Enabled = False
                tbTDept.Enabled = False
            End If
            If RB4.Checked Then
                tbFName.Enabled = False
                tbTName.Enabled = False
                tbCity.Enabled = False
                ddlState.Enabled = False
                tbZip.Enabled = True
                tbSequence.Enabled = False
                tbFDept.Enabled = False
                tbTDept.Enabled = False
            End If
            If RB5.Checked Then
                tbFName.Enabled = False
                tbTName.Enabled = False
                tbCity.Enabled = False
                ddlState.Enabled = False
                tbZip.Enabled = False
                tbSequence.Enabled = True
                tbFDept.Enabled = False
                tbTDept.Enabled = False
            End If
            If RB6.Checked Then
                tbFName.Enabled = False
                tbTName.Enabled = False
                tbCity.Enabled = False
                ddlState.Enabled = False
                tbZip.Enabled = False
                tbSequence.Enabled = False
                tbFDept.Enabled = True
                tbTDept.Enabled = True
            End If


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
                Session("UpdateOrd") = "Complimentary Magazine Maintenance"
                Session("EventTitle") = " "
                Session("PageTitle") = " "


                Session("DTState") = LoadStateDropDown()
                'Build State table
                ddlState.DataSource = Session("DTState")
                ddlState.DataTextField = "stname"
                ddlState.DataValueField = "stdust"
                ddlState.DataBind()
                ddlState.Items.Insert(0, "  Select a State")
                ddlState.Items.Item(0).Value = "  "
                ddlState.SelectedIndex = 0


                RB1.Attributes.Add("onclick", "ButtonChecked()")
                RB2.Attributes.Add("onclick", "ButtonChecked()")
                RB3.Attributes.Add("onclick", "ButtonChecked()")
                RB4.Attributes.Add("onclick", "ButtonChecked()")
                RB5.Attributes.Add("onclick", "ButtonChecked()")
                RB6.Attributes.Add("onclick", "ButtonChecked()")

                tbFName.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                tbTName.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                tbCity.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                tbFDept.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")
                tbTDept.Attributes.Add("onchange", "this.value=TranslateWord(this.value);")


            End If

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try

    End Sub


    Protected Sub ButtonFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonFind.Click

        'Reset last error
        mvLastError = ""

        Try

            If RB1.Checked Then

                DT = GetMagName(tbFName.Text, tbTName.Text)
                If DT.Rows.Count > 0 Then
                    GridView1.DataSource = DT
                    GridView1.DataBind()
                    GridView1.Visible = True
                    ButtonExcel.Visible = True
                    Label4.Visible = False
                Else
                    GridView1.Visible = False
                    ButtonExcel.Visible = False
                    Label4.Visible = True
                End If

            End If
            If RB2.Checked Then

                DT = GetMagCity(tbCity.Text)
                If DT.Rows.Count > 0 Then
                    GridView1.DataSource = DT
                    GridView1.DataBind()
                    GridView1.Visible = True
                    ButtonExcel.Visible = True
                    Label4.Visible = False
                Else
                    GridView1.Visible = False
                    ButtonExcel.Visible = False
                    Label4.Visible = True
                End If

            End If
            If RB3.Checked Then

                DT = GetMagState(ddlState.SelectedItem.Value)

                If DT.Rows.Count > 0 Then
                    GridView1.DataSource = DT
                    GridView1.DataBind()
                    GridView1.Visible = True
                    ButtonExcel.Visible = True
                    Label4.Visible = False
                Else
                    GridView1.Visible = False
                    ButtonExcel.Visible = False
                    Label4.Visible = True
                End If

            End If

            If RB4.Checked Then

                DT = GetMagZip(tbZip.Text)
                If DT.Rows.Count > 0 Then
                    GridView1.DataSource = DT
                    GridView1.DataBind()
                    GridView1.Visible = True
                    ButtonExcel.Visible = True
                    Label4.Visible = False
                Else
                    GridView1.Visible = False
                    ButtonExcel.Visible = False
                    Label4.Visible = True
                End If

            End If
            If RB5.Checked Then

                DT = GetMagSequence(tbSequence.Text)
                If DT.Rows.Count > 0 Then
                    GridView1.DataSource = DT
                    GridView1.DataBind()
                    GridView1.Visible = True
                    ButtonExcel.Visible = True
                    Label4.Visible = False
                Else
                    GridView1.Visible = False
                    ButtonExcel.Visible = False
                    Label4.Visible = True
                End If

            End If


            If RB6.Checked Then

                DT = GetMagDept(tbFDept.Text, tbTDept.Text)

                If DT.Rows.Count > 0 Then
                    GridView1.DataSource = DT
                    GridView1.DataBind()
                    GridView1.Visible = True
                    ButtonExcel.Visible = True
                    Label4.Visible = False
                Else
                    GridView1.Visible = False
                    ButtonExcel.Visible = False
                    Label4.Visible = True
                End If

            End If

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try

    End Sub

    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        'Reset last error
        mvLastError = ""


        Try
            Response.Redirect("CompMagMenu001.aspx")

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try


    End Sub

    Protected Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click

        'Reset last error
        mvLastError = ""


        Try

            Session("sMODE") = "INSERT"
            Session("sLOADREC") = "0"
            Response.Redirect("CompMagMaint002.aspx")

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try


    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        'Reset last error
        mvLastError = ""


        Try
            Session("sSEQUENCE") = GridView1.SelectedRow.Cells(1).Text
            Session("sMODE") = "UPDATE"
            Session("sLOADREC") = "1"


            Response.Redirect("CompMagMaint002.aspx")

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try


    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        'this is necessary for the export to excel button

    End Sub


    Protected Sub ButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExcel.Click

        Dim strStyle As String = "<style>.text { mso-number-format:mm\/dd\/yyyy; } </style>"
        Dim ExcelGrid As New DataGrid()

        ExcelGrid.DataSource = DT
        ExcelGrid.DataBind()

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        EnableViewState = False
        Dim oStringWriter As New System.IO.StringWriter()
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

        'Dump grid to an excel spread sheet
        ExcelGrid.RenderControl(oHtmlTextWriter)
        Response.Write(strStyle)
        Response.Write(oStringWriter.ToString())
        Response.End()


    End Sub


End Class
