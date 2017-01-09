Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data
Imports System.Web.HttpContext

Public Class ClassAccessDataAccess

    Dim mvLastError As String = ""

    Private Property Connection() As String
        Get
            If System.Web.HttpContext.Current.Session("Connection") Is Nothing Then
                Current.Session.Add("Connection", New ConnectionState)
            End If

            Return DirectCast(Current.Session("Connection"), String)
        End Get
        Set(ByVal value As String)
            Current.Session("Connection") = value
        End Set
    End Property

    Private Property CnAccess() As OleDbConnection
        Get
            If Current.Session("CnAccess") Is Nothing Then
                Current.Session.Add("CnAccess", New OleDbConnection)
            End If

            Return DirectCast(Current.Session("CnAccess"), OleDbConnection)
        End Get
        Set(ByVal value As OleDbConnection)
            Current.Session("CnAccess") = value
        End Set
    End Property

    Private Property CnState() As ConnectionState
        Get
            If Current.Session("CnState") Is Nothing Then
                Current.Session.Add("CnState", New ConnectionState)
            End If

            Return DirectCast(Current.Session("CnState"), ConnectionState)
        End Get
        Set(ByVal value As ConnectionState)
            Current.Session("CnState") = value
        End Set
    End Property

    Public Function CheckName(ByVal lastName As String, ByVal firstName As String, ByVal state As String) As Boolean
        '---------------------------------------------------------
        'Function: CheckName
        'Desc. . : Check to see if individual is already registered.
        'Parms . : 1.) Last Name
        '          2.) First Name
        '          3.) State
        'Returns : Returns a found indicator
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim found As Boolean = False

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select * from Convention where LASTNAME = '" & lastName & "' and FIRSTNAME = '" & firstName & "' and STATE = '" & state & "'"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                found = True
            End If

            Return found

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadConvStateDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadStateDropDown
        'Desc. . : Loades a data table of state name and codes.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select STATE, LONGSTATE, stateType from STATELST order by stateType, LONGSTATE"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Dim stType As String = " "
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim ckType As String = dr("stateType").ToString()
                If ckType <> stType Then
                    Dim ndr As DataRow = dt.NewRow()
                    If ckType = "1" Then
                        ndr.Item(0) = "  "
                        ndr.Item(1) = "---- United States ----"
                        ndr.Item(2) = "9"
                        dt.Rows.InsertAt(ndr, i)
                    End If
                    If ckType = "2" Then
                        ndr.Item(0) = "  "
                        ndr.Item(1) = "---- Canada ----"
                        ndr.Item(2) = "9"
                        dt.Rows.InsertAt(ndr, i)
                    End If
                    If ckType = "3" Then
                        ndr.Item(0) = "  "
                        ndr.Item(1) = "---- Mexico ----"
                        ndr.Item(2) = "9"
                        dt.Rows.InsertAt(ndr, i)
                        Exit For
                    End If
                    stType = ckType
                    i += 1
                End If
            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadConvStateDropDown(ByVal country As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadStateDropDown
        'Desc. . : Loades a data table of state name and codes.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select STATE, LONGSTATE, stateType from STATELST where COUNTRY='" & country & "' order by LONGSTATE"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadConvRegDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadConvRegDropDown
        'Desc. . : Loades a data table of convention registration type and cost.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select ConvRegistration, ConvFee, ConvRegistration & '  ($' & ConvFee & ')' As ConvRegDesc from ConventionChoices"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadVolCodesDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadVolCodesDropDown
        'Desc. . : Loades a data table of Volunteer codes and descriptions.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select VolCode, [Vol_Code Text] as VolDesc from [Volunteer Codes] order by VolCode"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadPositionsDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadPositionsDropDown
        'Desc. . : Loades a data table of DU position of leadership and descriptions.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select positionDisplay, positionDesc from Positions where positionType=1 order by positionDisplay"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadMemberStatusDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadMemberStatusDropDown
        'Desc. . : Loades a data table of DU member status and descriptions.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select positionDisplay, positionDesc from Positions where positionType=2 order by positionDisplay"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadToursDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadToursDropDown
        'Desc. . : Loades a data table of Tours and descriptions.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select TourDesc, DateTime as TourDate, Option, TourFee, [Order], [Confirmation Text] from TourChoices order by [Order]"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("TourDateTime")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                If dr("Order") > 1 Then
                    Dim tblstring As String = dr("TourDate").ToString
                    If Not tblstring Is Nothing Or tblstring = "" Then
                        Dim wDateTime As DateTime = CType(tblstring, DateTime)
                        dr("TourDateTime") = Format(wDateTime, "dddd MMM dd yyyy h:mm tt").ToString()
                    Else
                        dr("TourDateTime") = " "
                    End If
                Else
                    dr("TourDateTime") = " "
                End If
            Next

            Return dt

        Catch ex As Exception

            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadPayTypeDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadPayTypeDropDown
        'Desc. . : Loades a data table of payment types.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select Pay_type, [Order] from [Payment Types]"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function InsertRegistrant(ByVal dt As DataTable, ByVal dta As DataTable, ByVal dtg As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: LoadVolCodesDropDown
        'Desc. . : Loades a data table of Volunteer codes and descriptions.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim Command As OleDbCommand
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim BidNum As Integer = 0
        Dim firstconv As String = String.Empty
        Dim banquetonly As String = String.Empty
        Dim country As String = String.Empty
        Dim zip As String = String.Empty
        Dim fzip As String = String.Empty
        Dim GroupNum As Int64 = 0

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select max(Registrant) from Convention"

            Dim da1 As New OleDbDataAdapter(query, CnAccess)
            Dim dtn = New DataTable
            dtn.Clear()
            da1.Fill(dtn)

            If dtn.Rows.Count > 0 Then
                Dim dr1 As DataRow = dtn.Rows(0)
                BidNum = dr1("Expr1000")
                If BidNum < 2000 Then
                    BidNum = 2000
                End If
            End If

            For Each rowg As DataRow In dtg.Rows
                Dim today As Date = DateTime.Today
                GroupNum = rowg("groupnum")

                query = "insert into ConventionPayments(groupCode, ccType, ccAmount, Pmt_Status,"
                query += " DateReceived, [Source]) "
                query += "values(" & GroupNum & ", '" & rowg("cardtype").ToString.Trim & "', "
                query += rowg("amount") & ", '" & rowg("paystatus").ToString.Trim & "', #"
                query += today & "#, '" & rowg("source").ToString.Trim & "')"

                Command = New OleDbCommand(query, CnAccess)
                CnState = CnAccess.State
                If CnState = ConnectionState.Closed Then
                    CnAccess.Open()
                End If

                rc = Command.ExecuteNonQuery

                For Each row As DataRow In dt.Rows

                    BidNum += 1
                    If row("firstconv") Then
                        firstconv = "1"
                    Else
                        firstconv = "0"
                    End If
                    If row("banquetonly") Then
                        banquetonly = "1"
                    Else
                        banquetonly = "0"
                    End If
                    If row("country") = "USA" Then
                        zip = row("zipcode")
                        fzip = " "
                        country = " "
                    Else
                        fzip = row("zipcode")
                        country = row("country").ToString.Trim
                        zip = " "
                    End If
                    query = "insert into convention(Registrant, GroupCode, CNA_ID, LASTNAME, FIRSTNAME, NICKNAME,"
                    query += " First_Conv, KIDS_AGE, ADDR1, ADDR2, CITY, STATE, ZIPCODE, [FOREIGN ADDRESS], "
                    query += "[FOREIGN POSTAL CODE], H_PHONE, W_PHONE, CAPACITY1, CAPACITY2, VOLCODE, email, preferredAddress)"
                    query += " values(" & BidNum & ", " & GroupNum & ", " & row("cnaid") & ", '" & row("lastname").ToString & "', '"
                    query += row("firstname").ToString & "', '" & row("nickname").ToString & " ', '"
                    query += firstconv & "', " & row("age") & ", '" & row("street1").ToString & "', '" & row("street2").ToString & " ', '"
                    query += row("city").ToString & "', '" & row("state").ToString & "', '" & zip.Trim & " ', '" & country.Trim & " ', '"
                    query += fzip.Trim & " ', '" & row("phone").ToString.Trim & "', '" & row("busphone").ToString.Trim & " ', '"
                    query += row("title1").ToString.Trim & "', '" & row("title2").ToString.Trim & " ', '"
                    query += row("volcode").ToString.Trim & " ', '" & row("email").ToString.Trim & " ', '" & row("perfmaddr").ToString.Trim & "')"

                    Command = New OleDbCommand(query, CnAccess)
                    rc = Command.ExecuteNonQuery

                    Dim reccount As Integer = 0
                    Dim i As Integer = 0
                    'Dim rowa As DataRow() = dta.[Select]("recnumber =" & row("recnumber"))
                    For Each rowa As DataRow In dta.Rows
                        If rowa("recnumber") = row("recnumber") Then
                            If i = 0 Then
                                query = "insert into convRegistrants(Registrant, ConvFee, ConvRegistration) "
                                query += "values(" & BidNum & ", " & rowa("amount") & ", '"
                                query += rowa("activity").ToString.Trim & "')"

                                Command = New OleDbCommand(query, CnAccess)
                                rc = Command.ExecuteNonQuery
                                i += 1
                            Else
                                query = "insert into ToursScheduled(Registrant, TourFee, Tour) "
                                query += "values(" & BidNum & ", " & rowa("amount") & ", '" & rowa("activity").ToString.Trim & "')"

                                Command = New OleDbCommand(query, CnAccess)
                                rc = Command.ExecuteNonQuery
                                reccount = reccount + 1
                            End If
                        End If
                    Next
                    If reccount = 0 Then
                        query = "insert into ToursScheduled(Registrant, TourFee, Tour) "
                        query += "values(" & BidNum & ", 0, 'No Tour Scheduled')"

                        Command = New OleDbCommand(query, CnAccess)
                        rc = Command.ExecuteNonQuery
                    End If
                    Next

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadConvRegPDF(ByVal sGroupCode As String) As DataTable
        '---------------------------------------------------------
        'Function: Load Convention Registrants for PDF
        'Desc. . : Loades a data table of convention registrants.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select * from convention where GroupCode=" & sGroupCode & " order by Registrant"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadConvAttendPDF(ByVal sBidNum As String) As DataTable
        '---------------------------------------------------------
        'Function: Load Convention Registrants Events PDF
        'Desc. . : Loades a data table of convention registrants events.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select * from convRegistrants where Registrant=" & sBidNum

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadConvTourPDF(ByVal sBidNum As String) As DataTable
        '---------------------------------------------------------
        'Function: Load Convention Registrants tours for PDF
        'Desc. . : Loades a data table of convention registrants tours.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select * from ToursScheduled where Registrant=" & sBidNum

            Dim da As New OleDbDataAdapter(query, CnAccess)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function
    Public Function LoadConvPayPDF(ByVal sGroupCode As String) As DataTable
        '---------------------------------------------------------
        'Function: Load Convention Registrants payments for PDF
        'Desc. . : Loades a data table of convention registrants payments.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select * from conventionpayments where GroupCode=" & sGroupCode

            Dim da As New OleDbDataAdapter(query, CnAccess)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadGroupName(ByVal sGroupCode As String) As DataTable
        '---------------------------------------------------------
        'Function: Load Group Name for re-print list
        'Desc. . : Loades a data table of Group numbers and names .
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            CnState = CnAccess.State
            If CnState = ConnectionState.Closed Then
                CnAccess.Open()
            End If

            query = "select GroupCode, Registrant, LASTNAME, FIRSTNAME from Convention where GroupCode=" & sGroupCode
            query += " order by GroupCode, Registrant"

            Dim da As New OleDbDataAdapter(query, CnAccess)
            dt.Clear()
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                Dim dr As DataRow
                dr = dt.NewRow()
                dr.Item("GroupCode") = sGroupCode
                dr.Item("Registrant") = 0
                dr.Item("LASTNAME") = "Name Found."
                dr.Item("FIRSTNAME") = "No Members"
                dt.Rows.Add(dr)
            End If

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

End Class
