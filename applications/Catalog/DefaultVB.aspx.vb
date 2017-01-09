Imports System.Drawing
Imports ExpertPdf.HtmlToPdf

Partial Class DefaultVB
    Inherits System.Web.UI.Page

    Protected Sub BtnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        Dim url As String = TxtURL.Text.Trim()
        Dim downloadName As String = "ExportedReport.pdf"

        If (Not Page.IsValid) Then
            Return
        End If

        Dim pdfConverter As PdfConverter = New PdfConverter

        pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
        pdfConverter.PdfDocumentOptions.ShowHeader = True
        pdfConverter.PdfDocumentOptions.ShowFooter = True
        pdfConverter.PdfDocumentOptions.LeftMargin = 5
        pdfConverter.PdfDocumentOptions.RightMargin = 5
        pdfConverter.PdfDocumentOptions.TopMargin = 5
        pdfConverter.PdfDocumentOptions.BottomMargin = 5
        pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

        pdfConverter.PdfDocumentOptions.ShowHeader = False
        'pdfConverter.PdfHeaderOptions.HeaderText = "Sample header: " + TxtURL.Text
        'pdfConverter.PdfHeaderOptions.HeaderTextColor = Color.Blue
        'pdfConverter.PdfHeaderOptions.HeaderDescriptionText = string.Empty
        'pdfConverter.PdfHeaderOptions.DrawHeaderLine = false

        pdfConverter.PdfDocumentOptions.ShowFooter = False
        'pdfConverter.PdfFooterOptions.FooterText = ("Sample footer: " _
        '            + (TxtURL.Text + ". You can change color, font and other options"))
        'pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
        'pdfConverter.PdfFooterOptions.DrawFooterLine = False
        'pdfConverter.PdfFooterOptions.PageNumberText = "Page"
        'pdfConverter.PdfFooterOptions.ShowPageNumber = True

        pdfConverter.SavePdfFromUrlToFile(url, "/CatalogPDFs/Test.PDF")

        'pdfConverter.LicenseKey = "put your license key here"
        'Dim downloadBytes() As Byte = pdfConverter.GetPdfFromUrlBytes(url)
        'Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response


        'response.Clear()
        'response.AddHeader("Content-Type", "binary/octet-stream")
        'response.AddHeader("Content-Disposition", ("attachment; filename=" _
        '                + (downloadName + ("; size=" + downloadBytes.Length.ToString))))
        'response.Flush()
        'response.BinaryWrite(downloadBytes)
        'response.Flush()
        'response.End()


    End Sub
End Class
