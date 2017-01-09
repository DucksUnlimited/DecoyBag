Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.HttpContext
Imports System.Configuration

Public Class SQLConvention
    Shared conn As New SqlConnection(ConfigurationManager.AppSettings("ConvSQL"))
    Shared mvLastError As String = ""

    Shared Function GetStateTable() As DataTable
        '---------------------------------------------------------
        'Function: GetAttendingOptions
        'Desc. . : Gets all the Attending Options Table.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If


            query = "select * from web_tb_Content_States order by stateID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetAttendingOptions() As DataTable
        '---------------------------------------------------------
        'Function: GetAttendingOptions
        'Desc. . : Gets all the Attending Options Table.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If


            query = "select * from tb_DUNC_Attending_Options order by orderByID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("AttendDateTime")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim tblstring As String = dr("attendingOptionDate").ToString
                If Not tblstring Is Nothing Or tblstring = "" Then
                    Dim wDateTime As DateTime = CType(tblstring, DateTime)
                    dr("AttendDateTime") = Format(wDateTime, "yyyyMMdd").ToString()
                Else
                    dr("AttendDateTime") = " "
                End If
            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetTourDates() As DataTable
        '---------------------------------------------------------
        'Function: GetTourDates
        'Desc. . : Gets all the Tour Dates Table.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Tour_Dates order by tourDateID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("tourDateTime")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim tblstring As String = dr("tourDate").ToString
                If Not tblstring Is Nothing Or tblstring = "" Then
                    Dim wDateTime As DateTime = CType(tblstring, DateTime)
                    dr("tourDateTime") = Format(wDateTime, "yyyyMMdd").ToString()
                Else
                    dr("tourDateTime") = " "
                End If
            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetTours() As DataTable
        '---------------------------------------------------------
        'Function: GetTours
        'Desc. . : Gets all the Tours Table.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Tours order by tourID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetLeadershipItems() As DataTable
        '---------------------------------------------------------
        'Function: GetTours
        'Desc. . : Gets all the Tours Table.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Registration_Leadership_Items order by leadershipItemID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetPositions() As DataTable
        '---------------------------------------------------------
        'Function: GetAttendingOptions
        'Desc. . : Gets all the Attending Options Table.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Positions order by positionID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegistrations() As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrations
        'Desc. . : Gets all the registrants from the on-line entry.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Registrations where (isFinalized = 1) order by registrationID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegistrations(ByVal sBidID As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrations
        'Desc. . : Gets registrants from the on-line entry using a starting BidID.
        'Parms . : Starting Bid ID
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Registrations"
            query += " where (registrationID > " & sBidID & " and isFinalized = 1)"
            query += " order by registrationID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegPayments() As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrations
        'Desc. . : Gets all the registrants from the on-line entry.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Registration_Payments order by paymentID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("ccDateTime")
            dt.Columns.Add("paystatus")
            dt.Columns.Add("source")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                dr("paystatus") = "Paid in Full"
                dr("source") = "Web"
                Dim tblstring As String = dr("dateTime").ToString
                If Not tblstring Is Nothing Or tblstring = "" Then
                    Dim wDateTime As DateTime = CType(tblstring, DateTime)
                    Dim whour As String = Hour(wDateTime).ToString("D2")
                    Dim wminute As String = Minute(wDateTime).ToString("D2")
                    Dim wsecond As String = Second(wDateTime).ToString("D2")
                    dr("ccDateTime") = Format(wDateTime, "yyyyMMdd").ToString()
                    dr("ccDateTime") = dr("ccDateTime") & whour & wminute & wsecond
                Else
                    dr("ccDateTime") = " "
                End If
            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegPayments(ByVal sgroupcode As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrations
        'Desc. . : Gets all the registrants from the on-line entry.
        'Parms . : Group Code to select
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Registration_Payments"
            query += " where (groupCode = '" & sgroupcode & "')"
            query += " order by paymentID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("ccDateTime")
            dt.Columns.Add("paystatus")
            dt.Columns.Add("source")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                dr("paystatus") = "Paid in Full"
                dr("source") = "Web"
                Dim tblstring As String = dr("dateTime").ToString
                If Not tblstring Is Nothing Or tblstring = "" Then
                    Dim wDateTime As DateTime = CType(tblstring, DateTime)
                    Dim whour As String = Hour(wDateTime).ToString("D2")
                    Dim wminute As String = Minute(wDateTime).ToString("D2")
                    Dim wsecond As String = Second(wDateTime).ToString("D2")
                    dr("ccDateTime") = Format(wDateTime, "yyyyMMdd").ToString()
                    dr("ccDateTime") = dr("ccDateTime") & whour & wminute & wsecond
                Else
                    dr("ccDateTime") = " "
                End If
            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegItems() As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrations
        'Desc. . : Gets all the registrants from the on-line entry.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Registration_Items order by registrationItemID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("regItemDate")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim tblstring As String = dr("registrationItemDate").ToString
                If Not tblstring Is Nothing Or tblstring = "" Then
                    Dim wDateTime As DateTime = CType(tblstring, DateTime)
                    dr("regItemDate") = Format(wDateTime, "yyyyMMdd").ToString()
                Else
                    dr("regItemDate") = " "
                End If
            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegItems(ByVal sgroupcode As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegItems
        'Desc. . : Gets all the registrants items from the on-line entry.
        'Parms . : Group Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Registration_Items"
            query += " where (registrationGroupCode = '" & sgroupcode & "')"
            'query += "and registrationItemTourID < 7 )"
            query += " order by registrationItemID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("regItemDate")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim tblstring As String = dr("registrationItemDate").ToString
                If Not tblstring Is Nothing Or tblstring = "" Then
                    Dim wDateTime As DateTime = CType(tblstring, DateTime)
                    dr("regItemDate") = Format(wDateTime, "yyyyMMdd").ToString()
                Else
                    dr("regItemDate") = " "
                End If
            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegLeadership() As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrations
        'Desc. . : Gets all the registrants from the on-line entry.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Registration_Items"
            query += " where (registrationItemTourID > 6 )"
            query += " order by registrationItemID"

            'query = "select * from tb_DUNC_Registration_Leadership order by registrationID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegLeadership(ByVal sgroupcode As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegLeadership
        'Desc. . : Gets registration leadership items for a specific group.
        'Parms . : Group Code
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If


            query = "select * from tb_DUNC_Registration_Items"
            query += " where (registrationGroupCode = '" & sgroupcode & "' and"
            query += " registrationItemTourID > 6 )"
            query += " order by registrationItemID"

            'query = "select * from tb_DUNC_Registration_Leadership"
            'query += " where (groupCode = '" & sgroupcode & "')"
            'query += " order by registrationID"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetGroupSeating() As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrations
        'Desc. . : Gets all the registrants from the on-line entry.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Registration_Seating order by groupCode"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetGroupSeating(ByVal sgroupcode As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrations
        'Desc. . : Gets all the registrants from the on-line entry.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from tb_DUNC_Registration_Seating"
            query += " where (groupCode = '" & sgroupcode & "')"
            query += " order by groupCode"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

End Class
