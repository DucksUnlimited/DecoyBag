<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DefaultVB.aspx.vb" Inherits="DefaultVB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ExpertPDF VB.NET Demo</title>
    <link href="styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div>
        ExpertPDF is simple to use. This page was created with VB.NET. <a href="Default.aspx">Click here to see a C# sample.</a>
        <br />
        Enter any url below and click "Export". Your page will be exported to PDF.
        <br />
        <br />
        
	Url: <asp:TextBox ID="TxtURL" runat="server" Text="http://www.html-to-pdf.net" Width="277px"></asp:TextBox>
	<asp:LinkButton ID="BtnExport" Text="Export" runat="server" OnClick="BtnExport_Click">Export</asp:LinkButton>
        <br />
        <br />
        The code used on this page looks like this:
        <br />
<pre class="csharpcode">
<span class="kwrd">Dim</span> pdfConverter <span class="kwrd">As</span> PdfConverter = <span class="kwrd">New</span> PdfConverter

pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
pdfConverter.PdfDocumentOptions.ShowHeader = <span class="kwrd">true</span>
pdfConverter.PdfDocumentOptions.ShowFooter = <span class="kwrd">true</span>
pdfConverter.PdfDocumentOptions.LeftMargin = 5
pdfConverter.PdfDocumentOptions.RightMargin = 5
pdfConverter.PdfDocumentOptions.TopMargin = 5
pdfConverter.PdfDocumentOptions.BottomMargin = 5
pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = <span class="kwrd">true</span>

pdfConverter.PdfDocumentOptions.ShowHeader = <span class="kwrd">false</span>
<span class="rem">'pdfConverter.PdfHeaderOptions.HeaderText = "Sample header: " + TxtURL.Text</span>
<span class="rem">'pdfConverter.PdfHeaderOptions.HeaderTextColor = Color.Blue</span>
<span class="rem">'pdfConverter.PdfHeaderOptions.HeaderDescriptionText = string.Empty</span>
<span class="rem">'pdfConverter.PdfHeaderOptions.DrawHeaderLine = false</span>

pdfConverter.PdfFooterOptions.FooterText = (<span class="str">"Sample footer: "</span>  _
            + (TxtURL.Text + <span class="str">". You can change color, font and other options"</span>))
pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
pdfConverter.PdfFooterOptions.DrawFooterLine = <span class="kwrd">false</span>
pdfConverter.PdfFooterOptions.PageNumberText = <span class="str">"Page"</span>
pdfConverter.PdfFooterOptions.ShowPageNumber = <span class="kwrd">true</span>

<span class="rem">'pdfConverter.LicenseKey = "put your license key here"</span>
<span class="kwrd">Dim</span> downloadBytes() <span class="kwrd">As</span> <span class="kwrd">Byte</span> = pdfConverter.GetPdfFromUrlBytes(url)
<span class="kwrd">Dim</span> response <span class="kwrd">As</span> System.Web.HttpResponse = System.Web.HttpContext.Current.Response

response.Clear
response.AddHeader(<span class="str">"Content-Type"</span>, <span class="str">"binary/octet-stream"</span>)
response.AddHeader(<span class="str">"Content-Disposition"</span>, (<span class="str">"attachment; filename="</span>  _
                + (downloadName + (<span class="str">"; size="</span> + downloadBytes.Length.ToString))))
response.Flush
response.BinaryWrite(downloadBytes)
response.Flush
response.<span class="kwrd">End</span>
</pre>    
    
    </div>
    </form>
</body>
</html>
