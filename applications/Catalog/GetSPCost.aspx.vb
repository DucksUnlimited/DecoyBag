Imports iSeriesDB.iSeriesCatalog
Imports System.Data

Partial Class GetSPCost
    Inherits System.Web.UI.Page

    Dim evSource() As String = {"AT", "CR", "D ", "DL", "F ", "GB", "H ", "IV", "NO", "O ", "S ", "SB", "T ", "VC", "W ", "WP"}
    Dim evType() As String = {"P", "C", "D", "L", "F", "T", "M", "I", "N", "O", "S", "E", "B", "V", "G", "W"}
    Dim evNumber() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35}
    Dim evCode() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
    Dim DT As DataTable
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
        Dim orderPO As String = evState + evChapter + evCode(index2) + evType(index)
        Dim emKey As String = evState + "." + evChapter + "." + evCode(index2) + "." + evType(index) + "." + emDate

        PageTitle.Text = "Event Merchandise for - " + emKey
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
End Class
