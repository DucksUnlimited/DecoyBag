Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports iSeriesDB.iSeriesCatalog
Imports System.Data
Imports System.IO
Imports System.Drawing

Partial Class GetSPCost
    Inherits System.Web.UI.Page 

    Dim evSource() As String = {"AT", "CR", "D ", "DL", "F ", "GB", "H ", "IV", "NO", "O ", "S ", "SB", "T ", "VC", "W ", "WP", "OA", "V "}
    Dim evType() As String = {"P", "C", "D", "L", "F", "T", "M", "I", "N", "O", "S", "E", "B", "V", "G", "W", "Z", "U"}
    Dim evNumber() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35,
                    36,37,38,39,40,41,42,43,44,45,46,47,48,49,50}
    Dim evCode() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                    "!","@","#","$","%","[","*","(",")","-","_","}","+","{",":"}
    Dim DT As DataTable
    Dim emkey As String
    Dim orderPO As String
    Public totalItemCost As Decimal = 0
    Public totalCommCost As Decimal = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim eventKey As String = Request("kp")
        Dim evState As String = eventKey.Substring(0, 2)
        Dim evChapter As String = eventKey.Substring(3, 3)
        Dim evnbr As Integer = Integer.Parse(eventKey.Substring(6, 3))
        Dim evnumb As String = evnbr.ToString
        Dim evsrc As String = eventKey.Substring(9, 2)
        Dim orderDtae As String = eventKey.Substring(11, 8)
        Dim stateChapt As String = evState + evChapter
        Dim emDate As String = eventKey.Substring(11, 4) + "/" + eventKey.Substring(15, 2) + "/" + eventKey.Substring(17, 2)
        Dim index As Integer = Array.IndexOf(evSource, evsrc)
        Dim index2 As Integer = Array.IndexOf(evNumber, evnbr)
        orderPO = evState + evChapter + evCode(index2) + evType(index)
        emkey = evState + "." + evChapter + "." + evCode(index2) + "." + evType(index) + "." + emDate

        PageTitle.Text = "Event Merchandise for - " + emkey
        DatePH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
        TimePH.Text = DateTime.Now.ToString("h:mm:ss - tt")
        DateSH.Text = DateTime.Now.ToString("dddd,\ \ MMM d, yyyy")
        TimeSH.Text = DateTime.Now.ToString("h:mm:ss - tt")

        DT = GetEvItems(orderPO, orderDtae)

        For Each row In DT.Rows
            If row("linetype") = "K" Then
                row("unitcost") = 0.00
            Else
                row("unitcost") = row("qtyshipped") * row("unitcost")
            End If
            If row("billto") = stateChapt Then
                row("extsalescost") = row("unitcost")
            Else
                row("extsalescost") = 0.00
            End If
        Next

        OrdLineView.DataSource = DT
        OrdLineView.DataBind()

    End Sub

    Protected Sub OrdLineView_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As DataRow = DirectCast(e.Row.DataItem, DataRowView).Row
            Dim ordlink = DirectCast(e.Row.FindControl("ordlink"), HyperLink)
            ordlink.NavigateUrl = "http://catalog.ducks.org/Orders.aspx?htnum=" + ordlink.Text
            Dim unitcost As Decimal = Convert.ToDecimal(e.Row.Cells(4).Text)
            e.Row.Cells(4).Text = unitcost.ToString("0.00")
            Dim extcost As Decimal = Convert.ToDecimal(e.Row.Cells(5).Text)
            e.Row.Cells(5).Text = extcost.ToString("0.00")
            totalItemCost += Convert.ToDecimal(e.Row.Cells(4).Text)
            totalCommCost += Convert.ToDecimal(e.Row.Cells(5).Text)
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = "Total:"
            e.Row.Cells(4).Text = totalItemCost.ToString("c")
            e.Row.Cells(5).Text = totalCommCost.ToString("c")
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click

        Dim excel As New ExcelPackage()
        Dim workSheet = excel.Workbook.Worksheets.Add("Sheet1")
        Dim sheetRows As Integer = 2
        Dim totalCols = 6
        Dim totalRows = DT.Rows.Count

        workSheet.Cells(1, 1).Value = "Ordered Items for " & emkey
        Using range = workSheet.Cells("A1:F1")
            range.Merge = True
            range.Style.Font.SetFromFont(New Font("Britannic Bold", 18, FontStyle.Italic))
            range.Style.Font.Color.SetColor(Color.Black)
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous
            range.Style.Fill.PatternType = ExcelFillStyle.Solid
            range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228))
        End Using
        workSheet.Cells(2, 1).Value = "Item #"
        workSheet.Cells(2, 2).Value = "Ordered"
        workSheet.Cells(2, 3).Value = "Description"
        workSheet.Cells(2, 4).Value = "Order #"
        workSheet.Cells(2, 5).Value = "Item Cost"
        workSheet.Cells(2, 6).Value = "Committe Cost"

        Using range = workSheet.Cells("A2:F2")
            range.Style.Font.SetFromFont(New Font("Britannic Bold", 12))
            range.Style.Font.Color.SetColor(Color.Black)
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            range.Style.Fill.PatternType = ExcelFillStyle.Solid
            range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228))
        End Using

        Using range = workSheet.Cells(3, 4, 4 + totalRows, 4)
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        End Using

        Using range = workSheet.Cells(3, 2, 4 + totalRows, 2)
            range.Style.Numberformat.Format = "###,##0"
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
        End Using

        Using range = workSheet.Cells(2, 5, 4 + totalRows + 1, 6)
            range.Style.Numberformat.Format = "###,##0.00"
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
        End Using

        For Each row In DT.Rows
            sheetRows = sheetRows + 1
            workSheet.Cells(sheetRows, 1).Value = row("ITEMNUM").ToString.Trim
            workSheet.Cells(sheetRows, 2).Value = Convert.ToInt32(row("QTYSHIPPED"))
            workSheet.Cells(sheetRows, 3).Value = row("DESCRIPTION").ToString.Trim
            workSheet.Cells(sheetRows, 4).Value = Convert.ToInt32(row("HTNUM"))
            workSheet.Cells(sheetRows, 5).Value = Convert.ToDecimal(row("unitcost"))
            workSheet.Cells(sheetRows, 6).Value = Convert.ToDecimal(row("extsalescost"))
        Next

        sheetRows = sheetRows + 2
        workSheet.Cells(sheetRows, 4).Value = "Total:"
        workSheet.Cells(sheetRows, 4).Style.Font.Bold = True
        workSheet.Cells(sheetRows, 4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
        workSheet.Cells(sheetRows, 5).Formula = String.Format("Sum({0})", workSheet.Cells(3, 5, totalRows + 2, 5).Address)
        workSheet.Cells(sheetRows, 5).Style.Font.Bold = True
        workSheet.Cells(sheetRows, 5).Style.Numberformat.Format = "$###,##0.00"
        workSheet.Cells(sheetRows, 5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
        workSheet.Cells(sheetRows, 6).Formula = String.Format("Sum({0})", workSheet.Cells(3, 6, totalRows + 2, 6).Address)
        workSheet.Cells(sheetRows, 6).Style.Font.Bold = True
        workSheet.Cells(sheetRows, 6).Style.Numberformat.Format = "$###,##0.00"
        workSheet.Cells(sheetRows, 6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right

        Using range = workSheet.Cells(sheetRows, 1, sheetRows, 6)
            range.Style.Font.SetFromFont(New Font("Britannic Bold", 14))
            range.Style.Font.Color.SetColor(Color.Black)
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right
            range.Style.Fill.PatternType = ExcelFillStyle.Solid
            range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228))
        End Using

        workSheet.Cells.AutoFitColumns(1)
        workSheet.Cells.AutoFitColumns(2)
        workSheet.Column(3).Width = 40
        workSheet.Column(4).Width = 12
        workSheet.Column(5).Width = 18
        workSheet.Column(6).Width = 18

        excel.Workbook.Calculate()

        Dim filename As String = "Event_Orders_for_" & emkey & ".xlsx"
        Using memoryStream = New MemoryStream()
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;  filename=" & filename)
            excel.SaveAs(memoryStream)
            memoryStream.WriteTo(Response.OutputStream)
            Response.Flush()
            Response.[End]()
        End Using


    End Sub

End Class
