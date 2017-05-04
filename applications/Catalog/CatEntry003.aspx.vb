Imports System.IO
Imports System.Data
Imports System.Net.Mail
Imports ExpertPdf.HtmlToPdf
Imports iSeriesDB.iSeriesCatalog
Imports IBM.Data.DB2.iSeries
'Imports CommonRoutines

Partial Class CatEntry003
    Inherits System.Web.UI.Page

    Public parms As New ClassSessionManager
    'Public oiSeries As New ClassiSeriesDataAccess

    Dim pageError As Boolean = False

#Region "Properties"

    Private Property DTCats() As DataTable
        Get
            If Session("DTCats") Is Nothing Then
                Session.Add("DTCats", New DataTable())
            End If

            Return DirectCast(Session("DTCats"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("DTCats") = value
        End Set
    End Property

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

    Private Property DTOrder() As DataTable
        Get
            If Session("DTOrder") Is Nothing Then
                Session.Add("DTOrder", New DataTable())
            End If

            Return DirectCast(Session("DTOrder"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("DTOrder") = value
        End Set
    End Property

    Private Property BPostBack() As Boolean
        Get
            If Session("BPostBack") Is Nothing Then
                Session.Add("BPostBack", New Boolean)
            End If

            Return DirectCast(Session("BPostBack"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            Session("BPostBack") = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'retrieve default Ship To and Bill To information

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Try

            parms = Session("oSessionManager")
            Session("PageTitle") = "Ship To Information"

            Dim cityState As String
            Dim shipToState As String
            Dim r As Integer

            Dim LENGTH_TEXT As String = 500


            Dim lengthFunction As String = "function isMaxLength(txtBox) {"
            lengthFunction += " if(txtBox) { "
            lengthFunction += "     return ( txtBox.value.length <=" + LENGTH_TEXT + ");"
            lengthFunction += " }"
            lengthFunction += "}"

            Me.comments.Attributes.Add("onkeypress", "return isMaxLength(this);")
            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "txtLength", lengthFunction, True)


            If Session("UpdateOrder") Then
                btnExit.Text = "Exit & Cancel Order Changes"
            Else
                btnExit.Text = "Exit & Cancel Order"
            End If


            'If initial page load and not a postback or page refresh, 
            'reload our data. 
            If Not Page.IsPostBack Then

                Dim dr As iDB2DataReader
                'Dim dr As oledbDataReader
                Dim evID As String = parms.EventID.Substring(0, 5)

                'Turn off approval check box.
                'It will be turned on during update section
                cbApprove.Visible = False

                'Build State tables
                ddlState.DataSource = Session("DTState")
                ddlState.DataTextField = "stname"
                ddlState.DataValueField = "stdust"
                ddlState.DataBind()
                ddlFFLState.DataSource = Session("DTState")
                ddlFFLState.DataTextField = "stname"
                ddlFFLState.DataValueField = "stdust"
                ddlFFLState.DataBind()
                ddlFFLState.Items.Insert(0, "  Select a State")
                ddlFFLState.Items.Item(0).Value = "  "
                ddlFFLState.SelectedIndex = 0

                LBLcshp1.Attributes.Add("onchange", "this.value=TranslateName(this.value);")
                LBLcshp2.Attributes.Add("onchange", "this.value=TranslateField(this.value);")
                LBLcshp3.Attributes.Add("onchange", "this.value=TranslateField(this.value);")
                LBLcshp6.Attributes.Add("onchange", "this.value=TranslateField(this.value);")
                comments.Attributes.Add("onchange", "this.value=TranslateSentence(this.value);")
                fflorg.Attributes.Add("onchange", "this.value=TranslateField(this.value);")
                fflattn.Attributes.Add("onchange", "this.value=TranslateName(this.value);")
                ffladdr.Attributes.Add("onchange", "this.value=TranslateField(this.value);")
                fflcity.Attributes.Add("onchange", "this.value=TranslateField(this.value);")

                LBLcshp1.Focus()

                cbApprove.Visible = False
                cbApprove.Enabled = True
                hrdonly.Value = "N"
                If Session("UpdateOrder") Then

                    cbApprove.Visible = True
                    cbApprove.Checked = True
                    hrdonly.Value = "Y"
                    LBLbillchapter.Text = Session("LBLbillchapter")
                    LBLcsname.Text = Session("LBLcsname").ToString.Trim()
                    LBLcsadd1.Text = Session("LBLcsadd1").ToString.Trim()
                    LBLcsadd2.Text = Session("LBLcsadd2").ToString.Trim()
                    LBLcsctst.Text = Session("LBLcsctst").ToString.Trim()
                    LBLcszipa.Text = Session("LBLcszipa").ToString.Trim()
                    LBLcszip2.Text = Session("LBLcszip2").ToString.Trim()
                    'ship to
                    LBLshpchapter.Text = Session("LBLshpchapter")
                    LBLcshp1.Text = Session("LBLcshp1").ToString.Trim()
                    LBLcshp2.Text = Session("LBLcshp2").ToString.Trim()
                    LBLcshp3.Text = Session("LBLcshp3").ToString.Trim()
                    LBLcshp6.Text = Session("LBLcshp6").ToString.Trim()
                    LBLcshpza.Text = Session("LBLcshpza").ToString.Trim()
                    LBLcshpz2.Text = Session("LBLcshpz2").ToString.Trim()
                    emailaddr.Text = Session("eMailAddr").ToString.Trim()
                    emailaddr.Enabled = False
                    RFVeMail.Enabled = False
                    REVeMail.Enabled = False
                    comments.Text = Session("Comments").ToString.Trim()
                    If Session("FFLRequired") Then
                        FFLPanel.Visible = True
                        REVFFLZip5.Enabled = True
                        REVFFLZip5.Visible = True
                        REVFFLZip4.Enabled = True
                        REVFFLZip4.Visible = True
                        fflexp.DatePopupButton.Attributes.Add("onclick", "PopupAbove(event, '" & fflexp.ClientID & "')")
                        'FFL Information
                        If Not Session("FFLOrganization") Is Nothing Then
                            fflorg.Text = Session("FFLOrganization").ToString.Trim()
                            fflattn.Text = Session("FFLName").ToString.Trim()
                            ffladdr.Text = Session("FFLAddr").ToString.Trim()
                            fflcity.Text = Session("FFLCity").ToString.Trim()
                            fflzip5.Text = Session("FFLZip5").ToString.Trim()
                            fflzip4.Text = Session("FFLZip4").ToString.Trim()
                            ddlFFLState.SelectedIndex = ddlFFLState.Items.IndexOf(ddlFFLState.Items.FindByValue(Session("FFLState").ToString().Trim()))
                            fflnum.Text = Session("FFLNumber").ToString.Trim()
                            If Not Session("FFLExpirDate") Is Nothing And Not Session("FFLExpirDate") = "" And Not Session("FFLExpirDate") = "        " Then
                                Dim wkdate As String = Session("FFLExpirDate")
                                fflexp.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                            End If
                        End If
                    End If
                Else
                    If parms.RdOnly Then
                        cbApprove.Visible = True
                        cbApprove.Checked = True
                        hrdonly.Value = "Y"
                    End If
                    dr = LoadCustomer(evID)
                    If dr.Read Then
                        'bill to
                        LBLbillchapter.Text = dr("CSTNUM")
                        LBLcsname.Text = dr("CSNAME").ToString.Trim()
                        LBLcsadd1.Text = dr("CSADD1").ToString.Trim()
                        LBLcsadd2.Text = dr("CSADD2").ToString.Trim()
                        LBLcsctst.Text = dr("CSCTST").ToString.Trim()
                        LBLcszipa.Text = dr("CSZIPA").ToString.Trim()
                        LBLcszip2.Text = dr("CSZIP2").ToString.Trim()
                        'ship to
                        LBLshpchapter.Text = dr("CSTNUM").ToString.Trim()
                        LBLcshp1.Text = dr("CSNAME").ToString.Trim()
                        LBLcshp2.Text = dr("CSADD1").ToString.Trim()
                        LBLcshp3.Text = dr("CSADD2").ToString.Trim()
                        LBLcshp6.Text = dr("CSCTST").ToString.Trim()
                        LBLcshpza.Text = dr("CSZIPA").ToString.Trim()
                        LBLcshpz2.Text = dr("CSZIP2").ToString.Trim()
                        emailaddr.Text = Session("eMailAddr").ToString.Trim()
                        comments.Text = ""
                        If Session("FFLRequired") Then
                            FFLPanel.Visible = True
                            REVFFLZip5.Enabled = True
                            REVFFLZip5.Visible = True
                            REVFFLZip4.Enabled = True
                            REVFFLZip4.Visible = True
                            fflexp.DatePopupButton.Attributes.Add("onclick", "PopupAbove(event, '" & fflexp.ClientID & "')")
                            'FFL Information
                            fflorg.Text = ""
                            fflattn.Text = ""
                            ffladdr.Text = ""
                            fflcity.Text = ""
                            fflzip5.Text = ""
                            fflzip4.Text = ""
                            'ddlFFLState.SelectedIndex = ddlFFLState.Items.IndexOf(ddlFFLState.Items.FindByValue(dr("fflst")))
                            fflnum.Text = ""
                        End If
                    End If
                End If
                cityState = LBLcshp6.Text.Trim()
                r = cityState.Length - 1
                shipToState = cityState.Substring(r - 1, 2)
                LBLcshp6.Text = cityState.Substring(0, r - 2)

                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(shipToState))

            End If

        Catch ex As Exception
            'Response.Clear()
            'Response.Write("error in load <br />" + ex.Message + "<br />")
            'Response.Flush()
            'Response.End()
        End Try

    End Sub
    'if Retrun to Order Form selected
    Protected Sub btnRETURN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturn.Click

        BPostBack = True
        Response.Redirect("CatEntry001.aspx")
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim lctrace As Long = 0
		Dim lcttext As String = " "

        'Response.Clear()
        'Response.Flush()

        Try

            'Response.Write("Start of submit <br />")
            'Response.Flush()

            If Not pageError Then

                'rebuild LBLcsshp6 from screen
                LBLcshp6.Text += " " & ddlState.SelectedItem.Value.Trim()

                'save screen varibles to session so these fields can be used in next screen


                'Response.Write("Start of Bill to & ship to layout <br />")
                'Response.Flush()

                'bill to
                Session("LBLbillchapter") = LBLbillchapter.Text.Trim()
                Session("LBLcsname") = LBLcsname.Text.Trim()
                Session("LBLcsadd1") = LBLcsadd1.Text.Trim()
                Session("LBLcsadd2") = LBLcsadd2.Text.Trim()
                Session("LBLcsctst") = LBLcsctst.Text.Trim()
                Session("LBLcszipa") = LBLcszipa.Text.Trim()
                Session("LBLcszip2") = LBLcszip2.Text.Trim()
                Session("LBLshpchapter") = LBLshpchapter.Text.Trim()
                'ship to
                Session("LBLcshp1") = LBLcshp1.Text.ToUpper.Trim()
                Session("LBLcshp2") = LBLcshp2.Text.ToUpper.Trim()
                Session("LBLcshp3") = LBLcshp3.Text.ToUpper.Trim()
                Session("LBLcshp6") = LBLcshp6.Text.ToUpper.Trim()
                Session("LBLcshpza") = LBLcshpza.Text.Trim()
                Session("LBLcshpz2") = LBLcshpz2.Text.Trim()
                'eMail & Comments
                Session("eMailAddr") = emailaddr.Text
                Session("Comments") = comments.Text
                'ffl information
                Session("FFLOrganization") = fflorg.Text.ToUpper
                Session("FFLName") = fflattn.Text.Trim()
                Session("FFLAddr") = ffladdr.Text.Trim()
                Session("FFLCity") = fflcity.Text.Trim()
                Session("FFLState") = ddlFFLState.SelectedItem.Value.Trim()
                Session("FFLZip5") = fflzip5.Text.Trim()
                Session("FFLZip4") = fflzip4.Text.Trim()
                Session("FFLNumber") = fflnum.Text.Trim()
                Session("FFLExpirDate") = Format(fflexp.SelectedDate, "yyyyMMdd")

                Dim sOrder As String = ""
                Dim approved As String = "N"
                Dim updtorder As String = "N"
                If Session("UpdateOrder") Then
                    updtorder = "Y"
                End If
                If cbApprove.Checked Then
                    Session("Approved") = True
                    approved = "Y"
                End If

                'update header and detail on as/400 and return order number

                'Response.Write("Save order <br />")
                'Response.Flush()

                sOrder = SaveOrder(parms.EventID, parms.EventDate, parms.UserID, parms.RDNUM, DTOrder,
                    LBLcshp1.Text.ToUpper, LBLcshp2.Text.ToUpper, LBLcshp3.Text.ToUpper, LBLcshp6.Text.ToUpper, LBLcshpza.Text, LBLcshpz2.Text,
                    fflorg.Text, fflattn.Text, ffladdr.Text, fflcity.Text, ddlFFLState.SelectedItem.Value.Trim(), fflzip5.Text, fflzip4.Text, fflnum.Text, Format(fflexp.SelectedDate, "yyyyMMdd"),
                    emailaddr.Text, comments.Text.ToString.Trim, approved, parms.TotPrice, parms.CatName, Session("UpdateOrderNumber"))
                If sOrder.Trim = "" Then
                    Throw New Exception("Error in SaveOrder in CatEntry003")
                End If
                'sOrder = Session("UpdateOrderNumber")
                parms.CurOrder = sOrder

                'date = Mid$(date,7,2) & "/" & Mid$(date,5,2) & "/" & Mid$(date,1,4)


                'code for ExpertPDF converter
                'set up the URL for the program that will create the html page that is converted.
                Dim url As String = Request.Url.OriginalString
                Dim urlBase As String = Request.Url.OriginalString
                Dim dirLevel As String = Page.ResolveUrl("~/")
                Dim fflpdf As String = "N"
                If Session("FFLRequired") Then
                    fflpdf = "Y"
                End If
                url = url.Remove(url.LastIndexOf(dirLevel), ((url.Length) - url.LastIndexOf(dirLevel))) + dirLevel
                url += "CatEntryPDF.aspx?evid=" + parms.EventID + "&evnm=" + parms.EventName.Trim()
                url += "&evdt=" + parms.EventDate + "&ord=" + sOrder.Trim + "&updtord=" + updtorder
                url += "&fflpdf=" + fflpdf + "&approved=" + approved

                'Build path & file name for PDF file
                Dim downloadName As String = "Ord" + sOrder + ".pdf"
                Dim outFilePath As String = "c:/Doc/Ord" + sOrder + ".pdf"

                Dim sw As New StringWriter()
                Server.Execute("CatEntryPDF.aspx", sw)
                Dim htmlCodeToConvert As String = sw.GetStringBuilder().ToString()

                'Response.Redirect("CatEntryPDF2.aspx?evid=" + parms.EventID + "&evnm=" + parms.EventName.Trim() + "&evdt=" + parms.EventDate + "&ord=" + sOrder.Trim + "&updtord=" + updtorder)
                'Response.Redirect("CatEntryPDF.aspx")

                'converts html to a PDF file. 
                Dim pdfConverter As PdfConverter = New PdfConverter

                'Set up parms for PDF maker

                pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.Letter
                pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.BestSpeed
                pdfConverter.PdfDocumentOptions.ShowHeader = False
                pdfConverter.PdfDocumentOptions.ShowFooter = False
                pdfConverter.PdfDocumentOptions.LeftMargin = 2
                pdfConverter.PdfDocumentOptions.RightMargin = 2
                pdfConverter.PdfDocumentOptions.TopMargin = 2
                pdfConverter.PdfDocumentOptions.BottomMargin = 2
                pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = False

                pdfConverter.LicenseKey = "Ob2VZl0lWNftrUJf0Na+MbUMoJ4bCSvcVrEU8UORNGYmN429pYDPYfzPTqYK/WHh"

                'Build PDF file
                'pdfConverter.SavePdfFromUrlToFile(url, outFilePath)
                urlBase = urlBase.Remove(urlBase.LastIndexOf(dirLevel), ((urlBase.Length) - urlBase.LastIndexOf(dirLevel))) + dirLevel
                pdfConverter.SavePdfFromHtmlStringToFile(htmlCodeToConvert, outFilePath, urlBase)


                'Response.Write("Start of email build <br />")
                'Response.Flush()

                'Code for eMail function
                'send email regarding the order to Vol, RD, Corr and attach a PDF of the order
                Dim smtpMail As New MailMessage()
                Dim smtp As New SmtpClient
                Dim bodyText As String = String.Empty
                Dim subjectLine As String = String.Empty

                'Build eMail message for Updated and New orders
                If Session("UpdateOrder") Or Session("Approved") Then
                    If cbApprove.Checked Then
                        subjectLine = "Order -  " & sOrder & " has been APPROVED  for  " & parms.EventName.ToUpper.Trim() & " - " & parms.EventID
                        'Build body for RD and Cor. when order has been updated and apporved.
                        bodyText = parms.RDName.Trim() & ",<br /><br />" & "Order   <b>" & parms.CurOrder.Trim() & "</b> for <b>" & parms.EventName.ToUpper.Trim()
                        bodyText += "</b> to be held on    <b>" & parms.EventDate.Substring(4, 2) & "/" & parms.EventDate.Substring(6, 2) & "/"
                        bodyText += parms.EventDate.Substring(0, 4) & "</b> has been updated. <br />"
                        bodyText += "<b><font style=""font-size:14pt;"">This order has also been approved for shipment </font></b>"
                        bodyText += "<br /><br /><br />Attached you will find a revised PDF receipt of the order."
                        lctrace = 100
                        lcttext = "Cat EMail -" & parms.CatEMail & " -- RD EMail -" & parms.RDEMail & " -- Cor EMail -" & parms.CorEMail & " -- "
                        If Session("eMailAddr") = parms.RDEMail Then
                            smtpMail = New MailMessage(parms.CatEMail, parms.RDEMail, subjectLine, bodyText)
                        Else
                            smtpMail = New MailMessage(parms.CatEMail, Session("eMailAddr"), subjectLine, bodyText)
                            smtpMail.CC.Add(parms.RDEMail)
                        End If
                        smtpMail.CC.Add(parms.CorEMail)
                        lctrace = 200
                        lcttext = "No Errors"
                    Else
                        subjectLine = "Order -  " & sOrder & " has been UPDATED  for  " & parms.EventName.ToUpper.Trim() & " - " & parms.EventID
                        'Build body for RD and Cor. when order has been updated and not apporved.
                        bodyText = parms.RDName.Trim() & ",<br /><br />" & "Order   " & parms.CurOrder.Trim() & " for <b>" & parms.EventName.ToUpper.Trim()
                        bodyText += "</b> to be held on    <b>" & parms.EventDate.Substring(4, 2) & "/" & parms.EventDate.Substring(6, 2) & "/"
                        bodyText += parms.EventDate.Substring(0, 4) & "</b> has been updated. <br /><br />Attached you will find a revised PDF receipt of the order."
                        lctrace = 150
                        lcttext = "Cat EMail -" & parms.CatEMail & " -- RD EMail -" & parms.RDEMail & " -- "
                        smtpMail = New MailMessage(parms.CatEMail, parms.RDEMail, subjectLine, bodyText)
                        lctrace = 250
                        lcttext = "No Errors"
                    End If
                    Else
                        subjectLine = "Pending Catalog Order -  " & sOrder & "  for  " & parms.EventName.ToUpper.Trim() & " - " & parms.EventID

                        'Check to see if RD is entered the order
                        If parms.RDID = parms.UserID Then

                            bodyText = parms.RDName.Trim() & ",<br /><br />" & "Thank you for your order for <b>" & parms.EventName.ToUpper.Trim()
                            bodyText += "</b> to be held on    <b>" & parms.EventDate.Substring(4, 2) & "/" & parms.EventDate.Substring(6, 2) & "/"
                            bodyText += parms.EventDate.Substring(0, 4) & "</b>.<br /><br />"
                            bodyText += "<b><font style=""font-size:14pt;"">This order is waiting on your approval. You may not get all items due to inventory availability</font></b>"
                            bodyText += "<br /><br />Attached you will find a PDF receipt of the order."

                            lctrace = 170
                            lcttext = "Cat EMail -" & parms.CatEMail & " -- RD EMail -" & Session("eMailAddr") & " -- "
                            smtpMail = New MailMessage(parms.CatEMail, Session("eMailAddr"), subjectLine, bodyText)
                            lctrace = 270
                            lcttext = "No Errors"

                        Else

                            'Build body for User when it's not a RD entering the order.
                            bodyText = parms.UsrFName.Trim() & ",<br /><br />" & "Thank you for your order for <b>" & parms.EventName.ToUpper.Trim()
                            bodyText += "</b> to be held on    <b>" & parms.EventDate.Substring(4, 2) & "/" & parms.EventDate.Substring(6, 2) & "/"
                            bodyText += parms.EventDate.Substring(0, 4) & "</b>.<br /><br />"
                            bodyText += "<b><font style=""font-size:14pt;"">This order is pending approval by your RD. You may not get all items due to inventory availability</font></b>"
                            bodyText += "<br /><br />Attached you will find a PDF receipt of the order."

                            lctrace = 180
                            lcttext = "Cat EMail -" & parms.CatEMail & " -- Session EMail -" & Session("eMailAddr") & " -- RD EMail -" & parms.RDEMail & " -- "
                            smtpMail = New MailMessage(parms.CatEMail, Session("eMailAddr"), subjectLine, bodyText)
                            smtpMail.CC.Add(parms.RDEMail)
                            lctrace = 280
                            lcttext = "No Errors"

                        End If

                    End If

                'send email to RD and Cor.
                lctrace = 300
                smtpMail.Attachments.Add(New Attachment(outFilePath))
                smtpMail.IsBodyHtml = True
                smtpMail.Priority = MailPriority.High
                smtp.Send(smtpMail)
                lctrace = 400
				lcttext = "No Errors"

                Session("LBLsOrder") = sOrder

                'Redirect to final page if all is good in ShipTo information. 
                Response.Redirect("CatEntry004.aspx")

            End If

        Catch ex As Exception

            'Save error message for display on master page
            'lblError.Text = ex.Message & "Trace point:" & lctrace & " - " & lcttext


            'Response.Write("error received <br />" + ex.Message + "<br />")
            'Response.Flush()
            'Response.End()

            lblError.Text = ex.Message

        End Try
    End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        DT().Dispose()
        DTOrder().Dispose()
        DTCats().Dispose()

        Response.Redirect(Session("UrlStart"))

    End Sub

    'Protected Sub MustSelectState(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CVStateSelect.ServerValidate

    '    Try

    '        args.IsValid = False
    '        pageError = True

    '        If Not fflcity.Text Is Nothing And Not fflcity.Text = "" Then
    '            If Not fflcity.Text.Substring(0, 2) = "  " Then
    '                If ddlFFLState.SelectedIndex < 1 Then
    '                    'CVStateSelect.ErrorMessage = "Must select one or the other."
    '                    ddlFFLState.Focus()
    '                Else
    '                    args.IsValid = True
    '                    pageError = False
    '                End If
    '            Else
    '                If ddlFFLState.SelectedIndex > 0 Then
    '                    CVStateSelect.ErrorMessage = "Must enter a City if Selecting a State."
    '                    fflcity.Focus()
    '                Else
    '                    args.IsValid = True
    '                    pageError = False
    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub

End Class
