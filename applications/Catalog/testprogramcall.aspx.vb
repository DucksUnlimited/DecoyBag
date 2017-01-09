Imports System.Data.OleDb
Partial Class testprogramcall
    Inherits System.Web.UI.Page

    Public oiSeries As New ClassiSeriesDataAccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' Dim lclDSSWR006 As New ClassiSeriesDataAccess.rtnDSSWR006

        Try

            'Reset error variable in session on page load
            Session("LastError") = ""

            'Call SWR006 to get next order
            'lclDSSWR006 = oiSeries.GetNextOrderMultParmSWR006("HT", "", "", "", 0, "")

            'Display the order number
            'LblOrder.Text = lclDSSWR006.sOrderNumber
            'LblError.Text = oiSeries.GetLastError

            'Dim dr As OleDbDataReader
            'dr = oiSeries.LoadCustomer("TN019")
            'If dr.Read Then
            '    LblOrder.Text = dr("CSTNUM")
            'End If




            'Call CATR001 to update header
            'LblOrder.Text = oiSeries.InsertOrderHeaderCATR001("TN0321L", "20080502", "SJONES", "Sharon Jones", "475 S Perkins Rd.", "", "Memphis, TN", "39117", "", 1148.69, "")
            '    LblOrder.Text = oiSeries.InsertOrderHeader("TN0321L", "20080502", "SJONES", "Sharon Jones", "475 S Perkins Rd.", "", "Memphis, TN", "39117", "", 1148.69, "")
            '   If LblOrder.Text.Trim <> "" Then
            'Dim rtnbool As Boolean
            'rtnbool = oiSeries.InsertOrderDetail(LblOrder.Text, 1, "P101", "EA", 1, 42.69, "WALL BANNER LARGE")
            'End If
            'Display the order number

            'LblError.Text = oiSeries.GetLastError

            'Save error for display
            Session("LastError") = oiSeries.GetLastError

        Catch ex As Exception

        End Try

    End Sub
End Class
