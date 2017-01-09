Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
Imports System.data
Imports System.Web.HttpContext
Imports System.Configuration

Public Class iSeriesFIDO

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("FidoiSeries"))
    Shared mvLastError As String = ""
    Shared count As Integer

    Shared Function CheckName(ByVal lastName As String, ByVal firstName As String, ByVal state As String) As Boolean
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from cvregp where LNAME = '" & lastName & "' and FNAME = '" & firstName & "' and stateprov = '" & state & "'"

            Dim da As New iDB2DataAdapter(query, conn)
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

    Shared Function BuildWorkBanqTck() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkFile
        'Desc. . : Build work file for printing Blank Banquet Tickets 
        '           
        'Parms . : 0
        'Returns : Returns Boolean.
        '---------------------------------------------------------

        Dim oCmd As New iDB2Command
        Dim rc As Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            oCmd.Connection = conn

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVC005"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkTableCardsBlank() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkTableCardsBlank
        'Desc. . : Build work file for printing Blank Table Cards 
        '           
        'Parms . : 0
        'Returns : Returns Boolean.
        '---------------------------------------------------------

        Dim oCmd As New iDB2Command
        Dim rc As Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            oCmd.Connection = conn

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVC007"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkTableCards() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkTableCards
        'Desc. . : Build work file for printing Table Cards 
        '           
        'Parms . : 0
        'Returns : Returns Boolean.
        '---------------------------------------------------------

        Dim oCmd As New iDB2Command
        Dim rc As Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            oCmd.Connection = conn

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVC006"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkFile() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkFile
        'Desc. . : Build work file for printing Confirmation Letters 
        '           
        'Parms . : 0
        'Returns : Returns a last used BidID.
        '---------------------------------------------------------

        Dim oCmd As New iDB2Command
        Dim rc As Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            oCmd.Connection = conn

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVC002"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkEnvelope() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkEnvelope
        'Desc. . : Build work file for printing Envelopes 
        '           
        'Parms . : 0
        'Returns : Returns a last used BidID.
        '---------------------------------------------------------

        Dim oCmd As New iDB2Command
        Dim rc As Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            oCmd.Connection = conn

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVC008"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkNameTag() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkNameTag
        'Desc. . : Build work file for printing Name Tags 
        '           
        'Parms . : 0
        'Returns : Returns a last used BidID.
        '---------------------------------------------------------

        Dim oCmd As New iDB2Command
        Dim rc As Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            oCmd.Connection = conn

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVC003"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkExpo() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkExpo
        'Desc. . : Build work file for printing Confirmation Letters 
        '           
        'Parms . : 0
        'Returns : Returns a last used BidID.
        '---------------------------------------------------------

        Dim oCmd As New iDB2Command
        Dim rc As Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            oCmd.Connection = conn

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVC004"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkFinal() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkFinal
        'Desc. . : Build work file for showing financials
        '           
        'Parms . : 0
        'Returns : Return Code.
        '---------------------------------------------------------

        Dim oCmd As New iDB2Command
        Dim rc As Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            oCmd.Connection = conn

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVC009"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function FindRegistrants(ByVal lastName As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegistrants
        'Desc. . : Find all registrants with the last name passed into it.
        'Parms . : 1.) Last Name
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            lastName = lastName.Replace("'", "''")

            Dim r As Integer

            r = lastName.IndexOf("%")

            If r > 0 Then
                query = "select * from cvregp where LNAME LIKE '" & lastName & "' ORDER BY lname,fname"
            Else
                query = "select * from cvregp where LNAME = '" & lastName & "' ORDER BY lname,fname"
            End If

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetConvAttend() As DataTable
        '---------------------------------------------------------
        'Function: GetConvAttend
        'Desc. . : Loades a data table of all convention Attendies.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim rConvCnt As Decimal = 0
        Dim rConvAmt As Decimal = 0

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from table(convattend()) as t "
            query += "order by itemtyp, itemcnt desc"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Dim ndr As DataRow = dt.NewRow()
            Dim svType As String = "0"
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                rConvCnt = rConvCnt + Convert.ToDouble(dr("itemcnt"))
                rConvAmt = rConvAmt + Convert.ToDouble(dr("itemamt"))
            Next
            ndr = dt.NewRow()
            ndr.Item(0) = " "
            ndr.Item(1) = 0
            ndr.Item(2) = 0
            ndr.Item(3) = 1
            dt.Rows.Add(ndr)
            ndr = dt.NewRow()
            ndr.Item(0) = "Total Registered"
            ndr.Item(1) = rConvCnt
            ndr.Item(2) = rConvAmt
            ndr.Item(3) = 1
            dt.Rows.Add(ndr)
            ndr = dt.NewRow()
            ndr.Item(0) = " "
            ndr.Item(1) = 0
            ndr.Item(2) = 0
            ndr.Item(3) = 1
            dt.Rows.Add(ndr)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegistrant(ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrants
        'Desc. . : Get registrant for the bid ID passed into it or all.
        'Parms . : 1.) Bid ID
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            If bidid = "ALL" Then
                query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(country) country, TRIM(addr1) addr1, TRIM(city) city, TRIM(stateprov) stateprov, CONCAT(TRIM(fname),CONCAT(' ',TRIM(lname))) fullname "
                query += "from cvregp "
                query += "order by lname"
            Else
                query = "select * from cvregp where (bidid = " & bidid & ")"
            End If

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegistrant() As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrants
        'Desc. . : Get all registrants.
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select bidid, fname, lname, stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, position1, position2, volcode "
            query += "from cvregp "
            'query += "where lname like 'A%' "
            query += "order by fullname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegistrantVerified() As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrantVerified
        'Desc. . : Get registrants with Verified = False.
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select p.bidid, p.fname, p.lname, p.stateprov, TRIM(p.position1) pos1, TRIM(p.position2) pos2, p.volcode, p.verified, p.newpledge, p.pledgepd,  CONCAT(TRIM(p.lname),CONCAT(', ',TRIM(p.fname))) fullname,"
            query += " CONCAT(p.volcode,CONCAT(' -',TRIM(c.voldesc))) fullvol, "
            query += " c.voldesc"
            query += " from cvregp p"
            query += " left outer join cvvolp c on (c.volcode = p.volcode) "
            query += " where p.verified = '0' "
            query += " order by fullname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetAttendDetails(ByVal atnType As String) As DataTable
        '---------------------------------------------------------
        'Function: GetFinalDetails
        'Desc. . : Loades a data table of all registrants for a specific registration type.
        'Parms . : registration type
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim grpStatus As String = String.Empty

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            atnType = atnType.Replace("'", "''")

            query = "select TRIM(lname) lname, CONCAT(TRIM(fname),CONCAT(' ', TRIM(lname))) fullname, TRIM(addr1) addr1, "
            query += "TRIM(city) city, stateprov, TRIM(country) country, p.bidid, paytype "
            query += "from cvregitmp p left outer join cvregp c on c.bidid = p.bidid "
            query += "left outer join cvgrppayp d on d.groupid = c.groupid "
            query += "where TRIM(itemdesc) = '" + atnType + "' and (TRIM(paystatus) <> 'Complimentary' "
            query += "and TRIM(paystatus) <> 'DU Staff') "
            query += "order by lname, TRIM(fullname)"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetAttending(ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetAttending
        'Desc. . : Find all registrants attending items from Bid ID
        'Parms . : 1.) Bid ID
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from cvregitmp where (bidid = " & bidid & ")"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetLeaderShip(ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetLeaderShip
        'Desc. . : Find all registrants leadership workshop items from Bid ID
        'Parms . : 1.) Bid ID
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from cvregldp where (bidid = " & bidid & ")"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetStateName(ByVal stateid As String) As String
        '---------------------------------------------------------
        'Function: LoadStateDropDown
        'Desc. . : Loades a data table of state name and codes.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim stateName As String = String.Empty

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select stcode, stname from cvstatep where stateid='" & stateid & "' order by stname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                stateName = dr("stcode").ToString.Trim
            Next

            Return stateName

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetPosition(ByVal positionid As String) As String
        '---------------------------------------------------------
        'Function: LoadStateDropDown
        'Desc. . : Loades a data table of state name and codes.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim positionName As String = String.Empty

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select posid, posdesc from cvpositp where posid='" & positionid & "' order by posdesc"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                positionName = dr("posdesc").ToString.Trim
            Next

            Return positionName

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetBanquetSeating(ByRef listorder As String) As DataTable
        '---------------------------------------------------------
        'Function: GetBanquetSeating
        'Desc. . : Get all registrants seating request
        '
        'Parms . : List Order
        'Returns : Returns a data table of registrants.
        '---------------------------------------------------------
        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select p.fname, p.lname, p.stateprov, p.banqtbl, p.groupid, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname,"
            query += " c.seatwith"
            query += " from cvregp p"
            query += " left outer join  cvseatp c on (c.groupid = p.groupid)"
            query += " where p.nobanqu = '0'"
            If listorder = "A" Then
                query += " order by fullname"
            End If
            If listorder = "S" Then
                query += " order by p.stateprov, fullname"
            End If
            If listorder = "T" Then
                query += " order by p.banqtbl, fullname"
            End If

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetBanquetTable(ByRef listorder As String) As DataTable
        '---------------------------------------------------------
        'Function: GetBanquetTable
        'Desc. . : Get all registrants Table assignment
        '
        'Parms . : List Order
        'Returns : Returns a data table of registrants.
        '---------------------------------------------------------
        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select p.fname, p.lname, p.stateprov, p.banqtbl, p.groupid, p.bidid, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname,"
            query += " c.seatwith"
            query += " from cvregp p"
            query += " left outer join  cvseatp c on (c.groupid = p.groupid)"
            query += " where p.nobanqu = '0'"
            If listorder = "A" Then
                query += " order by fullname"
            End If
            If listorder = "S" Then
                query += " order by p.stateprov, fullname"
            End If
            If listorder = "T" Then
                query += " order by p.banqtbl, fullname"
            End If

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetLastBidID(ByVal regtype As String) As String
        '---------------------------------------------------------
        'Function: GetLastBidID
        'Desc. . : Get the last Bid ID number from records loaded 
        '           from the Web.
        'Parms . : 0
        'Returns : Returns a last used BidID.
        '---------------------------------------------------------

        Dim oCmd As New iDB2Command
        Dim dt As New DataTable
        Dim stateName As String = String.Empty

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            oCmd.Connection = conn

            oCmd.Parameters.Add("regtype", iDB2DbType.iDB2Char, 1)
            oCmd.Parameters("regtype").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("regtype").Value = regtype

            oCmd.Parameters.Add("lastbidid", iDB2DbType.iDB2Char, 4)
            oCmd.Parameters("lastbidid").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("lastbidid").Value = " "

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVR001"

            oCmd.ExecuteNonQuery()

            Return oCmd.Parameters("lastbidid").Value

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetWorkFile() As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from cvregallp order by lname, fname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetTableDetails(ByVal banqtable As String) As DataTable
        '---------------------------------------------------------
        'Function: GetConvTable
        'Desc. . : Loades a data table of all totals by tables.
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

            query = "select bidid, lname, CONCAT(TRIM(fname), CONCAT(' ', TRIM(lNAME))) fullname, city, stateprov, country "
            query += "from cvregp "
            query += "where banqtbl =" + banqtable + " and nobanqu = '0' "
            query += "order by lname, fullname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetWorkBanquetReg() As DataTable
        '---------------------------------------------------------
        'Function: GetWorkBanquetReg
        'Desc. . : Loades a data table of all Banquet Reg Cards that
        '           have not been printed.
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

            query = "select * from cvregp "
            query += "where banqreg = '0' and banqtbl = 0 and nobanqu = '0' "
            query += "order by lname, fname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetWorkBanquetTicket() As DataTable
        '---------------------------------------------------------
        'Function: GetWorkBanquetTicket
        'Desc. . : Loades a data table of all Banquet Tickets that
        '           have not been printed.
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

            query = "select * from cvregp where banqtck = '0' and banqtbl > 0 order by lname, fname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetWorkEnvelopes() As DataTable
        '---------------------------------------------------------
        'Function: GetWorkEnvelopes
        'Desc. . : Loades a data table of all name and address that
        '           have not been printed on envelopes.
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

            query = "select * from cvregp where envelope = '0' order by lname desc, fname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetWorkLabels() As DataTable
        '---------------------------------------------------------
        'Function: GetWorkLabels
        'Desc. . : Loades a data table of all name and address that
        '           have not been printed on labels.
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

            query = "select * from cvregp where convlbl = '0' order by lname, fname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetWorkNameTags() As DataTable
        '---------------------------------------------------------
        'Function: GetWorkNameTags
        'Desc. . : Loades a data table of all name that have not
        '           had their Name Tags Printed.
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

            query = "select * from cvregp where nametag = '0' order by lname, fname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetExpoTypes() As DataTable
        '---------------------------------------------------------
        'Function: GetExpoTypes
        'Desc. . : Loades a data table of all expo types.
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

            query = "select * from cvexpotypp order by expotpname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetExpoExhibitor() As DataTable
        '---------------------------------------------------------
        'Function: GetExpoExhibitor
        'Desc. . : Loades a data table of all exhibitor names that
        '           have not been printed on ID Cards.
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

            query = "select * from cvexpop where (exptype = 'E' and expprint = '0') order by expname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetExpoMedia() As DataTable
        '---------------------------------------------------------
        'Function: GetExpoMedia
        'Desc. . : Loades a data table of all media names that
        '           have not been printed on ID Cards.
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

            query = "select * from cvexpop where (exptype = 'M' and expprint = '0') order by expname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetExpoSponsor() As DataTable
        '---------------------------------------------------------
        'Function: GetExpoSponsor
        'Desc. . : Loades a data table of all sponsor names that
        '           have not been printed on ID Cards.
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

            query = "select * from cvexpop where (exptype = 'S' and expprint = '0') order by expname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetExpoNames() As DataTable
        '---------------------------------------------------------
        'Function: GetExpoNames
        'Desc. . : Loades a data table of all expo names that
        '           are in the name list.
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

            query = "select exprecno,exptype,TRIM(expname) expname,expprint,expcount,expotype,TRIM(expotpname) expotpname "
            query += "from cvexpop P inner join cvexpotypp C on C.expotype = P.exptype "
            query += "order by exptype, expname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetFinancials() As DataTable
        '---------------------------------------------------------
        'Function: GetFinancials
        'Desc. . : Loades a data table of all convention financials.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            Dim rc As Boolean = BuildWorkFinal()

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select TRIM(desc) desc, itemcnt, itemamt from cvfinalp where itemtype = 0 order by itemcnt desc, desc"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetFinalDetails(ByVal finType As String) As DataTable
        '---------------------------------------------------------
        'Function: GetFinalDetails
        'Desc. . : Loades a data table of all registrants for a specific registration type.
        'Parms . : registration type
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim grpStatus As String = String.Empty

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            finType = finType.Replace("'", "''")

            If finType = "Staff" Or finType = "Complimentary" Then
                If finType = "Staff" Then
                    grpStatus = "DU Staff"
                Else
                    grpStatus = finType
                End If
                query = "select TRIM(lname) lname, CONCAT(TRIM(fname),CONCAT(' ', TRIM(lname))) fullname, TRIM(addr1) addr1, "
                query += "TRIM(city) city, stateprov, TRIM(country) country, bidid, paytype "
                query += "from cvgrppayp p left outer join cvregp c on c.groupid = p.groupid "
                query += "where paystatus = '" + grpStatus + "' "
                query += "order by lname, TRIM(fname)"
            Else
                query = "select TRIM(lname) lname, CONCAT(TRIM(fname),CONCAT(' ', TRIM(lname))) fullname, TRIM(addr1) addr1, "
                query += "TRIM(city) city, stateprov, TRIM(country) country, p.bidid, paytype "
                query += "from cvregitmp p left outer join cvregp c on c.bidid = p.bidid "
                query += "left outer join cvgrppayp d on d.groupid = c.groupid "
                query += "where TRIM(itemdesc) = '" + finType + "' "
                query += "order by lname, TRIM(fullname)"
            End If

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetConvFinal() As DataTable
        '---------------------------------------------------------
        'Function: GetConvFinal
        'Desc. . : Loades a data table of all convention financials.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim rConvCnt As Decimal = 0
        Dim rConvAmt As Decimal = 0
        Dim rTourCnt As Decimal = 0
        Dim rTourAmt As Decimal = 0

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from table(convfinal()) as t "
            query += "order by itemtyp, itemcnt desc"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Dim ndr As DataRow = dt.NewRow()
            Dim svType As String = "0"
            Dim dtr As Integer = dt.Rows.Count + 4
            For i As Integer = 0 To dtr
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim ckType As String = dr("itemtyp").ToString()
                If ckType <> svType Then
                    If svType = "0" Then
                        ndr = dt.NewRow()
                        ndr.Item(0) = " "
                        ndr.Item(1) = 0
                        ndr.Item(2) = 0
                        ndr.Item(3) = 2
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow
                        ndr.Item(0) = "Tours"
                        ndr.Item(1) = 0
                        ndr.Item(2) = 0
                        ndr.Item(3) = 2
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow()
                        ndr.Item(0) = " "
                        ndr.Item(1) = 0
                        ndr.Item(2) = 0
                        ndr.Item(3) = 2
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow
                        ndr.Item(0) = "Total Registered"
                        ndr.Item(1) = rConvCnt
                        ndr.Item(2) = rConvAmt
                        ndr.Item(3) = 2
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow()
                        ndr.Item(0) = " "
                        ndr.Item(1) = 0
                        ndr.Item(2) = 0
                        ndr.Item(3) = 2
                        dt.Rows.InsertAt(ndr, i)
                        i += 5
                    End If
                    If svType = "4" Then
                        ndr = dt.NewRow
                        ndr.Item(0) = "Total Tours"
                        ndr.Item(1) = rTourCnt
                        ndr.Item(2) = rTourAmt
                        ndr.Item(3) = 6
                        dt.Rows.InsertAt(ndr, i)
                        i += 1
                        Exit For
                    End If
                    svType = ckType
                End If
                If ckType = "0" Then
                    rConvCnt = rConvCnt + Convert.ToDouble(dr("itemcnt"))
                    rConvAmt = rConvAmt + Convert.ToDouble(dr("itemamt"))
                End If
                If ckType = "4" Then
                    rTourCnt = rTourCnt + Convert.ToDouble(dr("itemcnt"))
                    rTourAmt = rTourAmt + Convert.ToDouble(dr("itemamt"))
                End If
            Next
            ndr = dt.NewRow()
            ndr.Item(0) = " "
            ndr.Item(1) = 0
            ndr.Item(2) = 0
            ndr.Item(3) = 6
            dt.Rows.Add(ndr)
            ndr = dt.NewRow()
            ndr.Item(0) = "Total Tours"
            ndr.Item(1) = rTourCnt
            ndr.Item(2) = rTourAmt
            ndr.Item(3) = 6
            dt.Rows.Add(ndr)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetConvStaff() As DataTable
        '---------------------------------------------------------
        'Function: GetStaff
        'Desc. . : Loades a data table of all convention staff.
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

            query = "select CONCAT(TRIM(fname),CONCAT(' ',TRIM(lname))) fullname, lname"
            query += " from cvregp p"
            query += " left outer join cvgrppayp c on (c.groupid = p.groupid)"
            query += " where TRIM(paystatus) = 'DU Staff'"
            query += " order by lname, fname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetConvTables() As DataTable
        '---------------------------------------------------------
        'Function: GetConvTable
        'Desc. . : Loades a data table of all totals by tables.
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

            query = "select * from table(convtable()) as t "
            query += "order by tabledesc"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetConvTours() As DataTable
        '---------------------------------------------------------
        'Function: GetConvTours
        'Desc. . : Loades a data table of totals by tours.
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

            query = "select p.ttitle, c.itemdate, COUNT(c.itemid) as tourct, "
            query += "SUBSTRING(c.itemdate,5,4) as tourdt, CONCAT(TRIM(ttitle),CONCAT(' ',SUBSTRING(c.itemdate,5,4))) as tourdesc "
            query += "from CVTOURSP p "
            query += "LEFT OUTER JOIN CVREGITMP c on (c.itemid = p.tourid) "
            query += "GROUP BY p.ttitle,c.itemdate,p.tourid "
            query += "ORDER BY p.ttitle,c.itemdate"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function IsConnectionOpen() As Boolean
        '---------------------------------------------------------
        'Function: ReopenConnection
        'Desc. . : Reopen iSeries connection if failed. 
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------
        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

        Catch ex As Exception

        End Try


    End Function
    Shared Function LoadConvStateDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadStateDropDown
        'Desc. . : Loades a data table of state name and codes.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim sJob As String = ""

        'Reset last error
        mvLastError = ""

        Try

            sJob = conn.JobName

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select stcode, stname, storder from cvstatep order by storder, stcode"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Dim stType As String = " "
            For i As Integer = 0 To dt.Rows.Count + 2
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim ckType As String = dr("storder").ToString()
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
                    End If
                    If ckType = "4" Then
                        ndr.Item(0) = "  "
                        ndr.Item(1) = "---- South America ----"
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
            count = count + 1
            If count > 6 Then
                Return Nothing
            Else
                conn.Close()
                conn.Open()
                Dim dt2 As DataTable = LoadConvStateDropDown()
                count = 0
                Return dt2
            End If

        End Try

    End Function

    Shared Function LoadConvStateDropDown(ByVal country As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadStateDropDown
        'Desc. . : Loades a data table of state name and codes.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim count As Integer

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select stcode, stname, storder from cvstatep where stcountry='" & country & "' order by stname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            count = count + 1
            If count > 6 Then
                Return Nothing
            Else
                conn.Close()
                conn.Open()
                Dim dt2 As DataTable = LoadConvStateDropDown(country)
                count = 0
                Return dt2
            End If
            Return Nothing
        End Try

    End Function

    Shared Function LoadConvRegDropDown() As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select adesc, acost, CONCAT(TRIM(adesc),CONCAT(' ------- $',acost)) ConvRegDesc from cvattendp"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                dr("adesc") = dr("adesc").ToString.Trim
            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadPayGroup(ByVal groupnum As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadPayGroup
        'Desc. . : Loades a data table of convention registration type and cost.
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

            query = "select groupid, paytype, cclastfour, ccexp, ccname, ccresponse, "
            query += "payamount, paystatus, source "
            query += "from cvgrppayp where groupid =" & groupnum & " order by groupid"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            'For i As Integer = 0 To dt.Rows.Count - 1
            '    Dim dr As DataRow = dt.Rows.Item(i)
            '    dr("adesc") = dr("adesc").ToString.Trim
            'Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadVolCodesDropDown() As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select VolCode, CONCAT(VolCOde,CONCAT(' - ',VolDesc)) as VolCdDesc from cvvolp order by VolCode"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadPositionsDropDown() As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select posDesc from cvpositp where posType=1 order by posDesc"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadMemberStatusDropDown() As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select posDesc from cvpositp where posType=2 order by posDesc"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadToursDropDown(ByVal tourdate As String) As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If
            query = "select p.tourdate, p.tourid, p.tourtime,"
            query += " c.ttitle, c.tlength, c.tcosta"
            query += " from cvdatep p"
            query += " left outer join  cvtoursp c on (c.tourid = p.tourid)"
            query += " where p.tourdate='" & tourdate & "' order by p.tourdate, p.tourid"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("TourDateTime")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim tblstring As String = dr("ttitle").ToString.Trim + "  (" + dr("tourtime").ToString.Trim + ")"
                dr("TourDateTime") = tblstring

            Next

            Return dt

        Catch ex As Exception

            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadPayTypeDropDown() As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from cvpaytpp order by paycode"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadPayStatusDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadPayStatusDropDown
        'Desc. . : Loades a data table of payment status.
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

            query = "select * from cvpaystp order by pstatid"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadRegSourceDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadRegSourceDropDown
        'Desc. . : Loades a data table of registration source.
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

            query = "select * from cvsourcep order by sourid"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadLeaderShipDropDown(ByVal grp As String) As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If
            query = "select ldrid,ldrdesc,ldrgroup,ldrorder from cvldrshpp"
            query += " where ldrgroup='" & grp & "' order by ldrgroup, ldrorder"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception

            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadConvRegPDF(ByVal sGroupCode As String) As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from cvregp where groupid=" & sGroupCode & " order by bidid"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadConvLdrShpPDF(ByVal sbidid As String, ByVal stime As String) As DataTable
        '---------------------------------------------------------
        'Function: Load Convention Registrants leadership items for PDF
        'Desc. . : Loades a data table of convention registrants selected leadership.
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

            query = "select p.bidid, p.groupid, p.ldrshipid,"
            query += " c.ldrdesc, c.ldrgroup"
            query += " from cvregldp p"
            query += " left outer join  cvldrshpp c on (c.ldrid = p.ldrshipid)"
            query += " where (p.bidid=" & sbidid & " and c.ldrgroup=" & stime & ") order by p.bidid, p.ldrshipid"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadConvAttendPDF(ByVal sBidNum As String) As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from cvregitmp where bidid=" & sBidNum

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadConvTourPDF(ByVal sBidNum As String) As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from ToursScheduled where Registrant=" & sBidNum

            Dim da As New iDB2DataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadConvPayPDF(ByVal sGroupCode As String) As DataTable
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from cvgrppayp where groupid=" & sGroupCode

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function InsertExpoRecord(ByVal itype As String, ByVal iname As String, ByVal icount As Integer) As Boolean
        '---------------------------------------------------------
        'Function: InsertExpoRecord
        'Desc. . : Insert a record into CVEXPOP using SQL
        'Parms . : 1.) Type
        '          2.) Name
        '          3.) Number od IDs needed 
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim irecno As Integer = 0

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select max(exprecno) from cvexpop"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                irecno = dr(0)
            End If

            iname = iname.Replace("'", "''")
            irecno += 1

            Dim cmd As New iDB2Command("insert into  cvexpop (exprecno,exptype,expname,expprint,expcount) values('@@exprecno','@@exptype','@@expname','@@expprint','@@expcount')", conn)
            cmd.CommandText = cmd.CommandText.Replace("@@exprecno", irecno)
            cmd.CommandText = cmd.CommandText.Replace("@@exptype", itype)
            cmd.CommandText = cmd.CommandText.Replace("@@expname", iname.Trim())
            cmd.CommandText = cmd.CommandText.Replace("@@expprint", "0")
            cmd.CommandText = cmd.CommandText.Replace("@@expcount", icount)

            'Execute the record insert
            cmd.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertPositions(ByVal dtPositions As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertGroup
        'Desc. . : Insert record in CATGROUP file using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For ct = 0 To dtPositions.Rows.Count - 1

                wtext = dtPositions.Rows(ct).Item("positionDesc").ToString
                wtext = wtext.Replace("'", "''")
                dtPositions.Rows(ct).Item("positionDesc") = wtext

                Dim cmd As New iDB2Command("insert into  cvpositp (posid,postype,posdesc) values('@@posid','@@postype','@@posdesc')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@posid", dtPositions.Rows(ct).Item("positionID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@postype", dtPositions.Rows(ct).Item("positionType").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@posdesc", dtPositions.Rows(ct).Item("positionDesc").ToString())

                'Execute the record insert
                cmd.ExecuteNonQuery()

            Next

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try


    End Function

    Shared Function InsertAttendingOptions(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertAttendingOptions
        'Desc. . : Insert records into CVATTENDP using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For ct = 0 To dt.Rows.Count - 1

                wtext = dt.Rows(ct).Item("attendingOptionDesc").ToString
                wtext = wtext.Replace("'", " ")
                dt.Rows(ct).Item("attendingOptionDesc") = wtext

                Dim cmd As New iDB2Command("insert into  cvattendp (attenid,adesc,adate,acost) values('@@attenid','@@adesc','@@adate','@@acost')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@attenid", dt.Rows(ct).Item("attendingOptionID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@adesc", dt.Rows(ct).Item("attendingOptionDesc").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@adate", dt.Rows(ct).Item("AttendDateTime").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@acost", dt.Rows(ct).Item("attendingOptionPrice").ToString())

                'Execute the record insert
                cmd.ExecuteNonQuery()

            Next

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertTourDates(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertTourDates
        'Desc. . : Insert records into CVDATEP using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For ct = 0 To dt.Rows.Count - 1

                Dim cmd As New iDB2Command("insert into  cvdatep (tourdtid,tourid,tourdate,tourtime) values('@@tourdtid','@@tourid','@@tourdate','@@tourtime')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@tourdtid", dt.Rows(ct).Item("tourDateID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@tourid", dt.Rows(ct).Item("tourID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@tourdate", dt.Rows(ct).Item("tourDateTime").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@tourtime", dt.Rows(ct).Item("tourTime").ToString())

                'Execute the record insert
                cmd.ExecuteNonQuery()

            Next

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertTours(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertTours
        'Desc. . : Insert records into CVTOURSP using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For ct = 0 To dt.Rows.Count - 1

                Dim cmd As New iDB2Command("insert into  cvtoursp (tourid,ttitle,tdesc,tlength,tcosta,tcostc,tchildage) values('@@tourid','@@ttitle','@@tdesc','@@tlength','@@tcosta','@@tcostc','@@tchildage')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@tourid", dt.Rows(ct).Item("tourID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@ttitle", dt.Rows(ct).Item("tourTitle").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@tdesc", dt.Rows(ct).Item("tourDesc").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@tlength", dt.Rows(ct).Item("tourLength").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@tcosta", dt.Rows(ct).Item("tourCostAdult").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@tcostc", dt.Rows(ct).Item("tourCostChild").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@tchildage", dt.Rows(ct).Item("tourChildAge").ToString())

                'Execute the record insert
                cmd.ExecuteNonQuery()

            Next

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertLeadershipItems(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertTours
        'Desc. . : Insert records into CVTOURSP using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For ct = 0 To dt.Rows.Count - 1

                Dim cmd As New iDB2Command("insert into  cvldrshpp (ldrid,ldrdesc,ldrgroup,ldrorder,ldrdttm) values('@@ldrid','@@ldrdesc','@@ldrgroup','@@ldrorder','@@ldrdttm')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@ldrid", dt.Rows(ct).Item("leadershipItemID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@ldrdesc", dt.Rows(ct).Item("leadershipItemDesc").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@ldrgroup", dt.Rows(ct).Item("leadershipItemGroup").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@ldrorder", dt.Rows(ct).Item("leadershipItemSortOrder").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@ldrdttm", dt.Rows(ct).Item("leadershipItemTime").ToString())

                'Execute the record insert
                cmd.ExecuteNonQuery()

            Next

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertRegistrantItems(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertTours
        'Desc. . : Insert records into CVTOURSP using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For ct = 0 To dt.Rows.Count - 1
                wtext = dt.Rows(ct).Item("registrationItemDesc").ToString
                wtext = wtext.Replace("'", "''")
                dt.Rows(ct).Item("registrationItemDesc") = wtext

                Dim cmd As New iDB2Command("insert into  cvregitmp (bidid,groupid,itemid,itemdesc,itemdate,itemamount) values('@@bidid','@@groupid','@@itemid','@@itemdesc','@@itemdate','@@itemamount')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@bidid", dt.Rows(ct).Item("registrationID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@groupid", dt.Rows(ct).Item("registrationGroupCode").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@itemid", dt.Rows(ct).Item("registrationItemTourID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@itemdesc", dt.Rows(ct).Item("registrationItemDesc").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@itemdate", dt.Rows(ct).Item("regItemDate").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@itemamount", dt.Rows(ct).Item("registrationItemPrice").ToString())

                'Execute the record insert
                cmd.ExecuteNonQuery()

            Next

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertGroupPay(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertGroupPay
        'Desc. . : Insert records into CVGRPPAYP using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For ct = 0 To dt.Rows.Count - 1

                Dim cmd As New iDB2Command("insert into  cvgrppayp (groupid,paytype,cclastfour,ccexp,ccname,ccresponse,payamount,paydatetim, paystatus, source) values('@@groupid','@@paytype','@@cclastfour','@@ccexp','@@ccname','@@ccresponse','@@payamount','@@paydatetime','@@paystatus','@@source')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@groupid", dt.Rows(ct).Item("groupCode").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@paytype", dt.Rows(ct).Item("paymentType").ToString().Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@cclastfour", dt.Rows(ct).Item("ccLastFour").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@ccexp", dt.Rows(ct).Item("ccExp").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@ccname", dt.Rows(ct).Item("ccName").ToString().Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@ccresponse", dt.Rows(ct).Item("ccResponse").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@payamount", dt.Rows(ct).Item("totalCharged").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@paydatetime", dt.Rows(ct).Item("ccDateTime").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@paystatus", dt.Rows(ct).Item("paystatus").ToString().Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@source", dt.Rows(ct).Item("source").ToString().Trim())

                'Execute the record insert
                cmd.ExecuteNonQuery()

            Next

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertRegLeadership(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertRegLeadership
        'Desc. . : Insert records into CVGRPPAYP using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For ct = 0 To dt.Rows.Count - 1

                Dim cmd As New iDB2Command("insert into  cvregldp (bidid,groupid,ldrshipid) values('@@bidid','@@groupid','@@ldrshipid')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@bidid", dt.Rows(ct).Item("registrationID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@groupid", dt.Rows(ct).Item("groupCode").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@ldrshipid", dt.Rows(ct).Item("leadershipItemID").ToString())

                'Execute the record insert
                cmd.ExecuteNonQuery()

            Next

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertGroupSeating(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertGroupSeating
        'Desc. . : Insert records into CVSEATP using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        Dim wtext As String
        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For ct = 0 To dt.Rows.Count - 1

                wtext = dt.Rows(ct).Item("seatWithNames").ToString
                wtext = wtext.Replace("'", "''")
                dt.Rows(ct).Item("seatWithNames") = wtext

                Dim cmd As New iDB2Command("insert into  cvseatp (groupid,seatwith) values('@@groupid','@@seatwith')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@groupid", dt.Rows(ct).Item("groupCode").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@seatwith", dt.Rows(ct).Item("seatWithNames").ToString())

                'Execute the record insert
                cmd.ExecuteNonQuery()

            Next

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertGroupSeating(ByVal sGroupCode As String, ByVal seatWithNames As String) As Boolean
        '---------------------------------------------------------
        'Function: InsertGroupSeating
        'Desc. . : Insert records into CVSEATP using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        Dim wtext As String
        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            wtext = seatWithNames.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            seatWithNames = wtext

            Dim cmd As New iDB2Command("insert into  cvseatp (groupid,seatwith) values('@@groupid','@@seatwith')", conn)
            cmd.CommandText = cmd.CommandText.Replace("@@groupid", sGroupCode.ToString())
            cmd.CommandText = cmd.CommandText.Replace("@@seatwith", seatWithNames.ToString().Trim())

            'Execute the record insert
            cmd.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertRegistrant(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertRegistrant
        'Desc. . : Loades all the information abour a group of attendies.
        'Parms . : Table of registrants, table of activities, table of group info
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim BidNum As Integer = 0
        Dim firstconv As String = String.Empty
        Dim banquetonly As String = String.Empty
        Dim nobanquet As String = String.Empty
        Dim country As String = String.Empty
        Dim zip As String = String.Empty
        Dim fzip As String = String.Empty
        Dim GroupNum As Int64 = 0
        Dim prefmaddr As String = String.Empty
        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows
                Dim today As Date = DateTime.Today
                GroupNum = row("groupCode")
                BidNum = row("registrationID")
                If row("firstConvention") Then
                    firstconv = "1"
                Else
                    firstconv = "0"
                End If
                banquetonly = "0"
                nobanquet = "0"
                If row("country") Then
                    country = "Canada"
                Else
                    country = "USA"
                End If
                If row("mailingAddress") Then
                    prefmaddr = "Business"
                Else
                    prefmaddr = "Home"
                End If
                If row("attending") = "2" Or row("attending") = "4" Or row("Attending") = "5" Then
                    banquetonly = "1"
                Else
                    If row("attending") = "3" Then
                        nobanquet = "1"
                    End If
                End If

                Dim statecode As String = GetStateName(row("stateProvinceID"))
                Dim pos1text As String = GetPosition(row("position0"))
                Dim pos2text As String = GetPosition(row("position1"))

                wtext = pos1text.ToString().Trim()
                wtext = wtext.Replace("'", "''")
                pos1text = wtext
                wtext = pos2text.ToString().Trim()
                wtext = wtext.Replace("'", "''")
                pos2text = wtext
                wtext = row("firstName").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("firstName") = wtext
                wtext = row("lastName").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("lastName") = wtext
                wtext = row("nickname").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("nickname") = wtext
                wtext = row("address1").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("address1") = wtext
                wtext = row("address2").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("address2") = wtext
                wtext = row("city").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("city") = wtext

                query = "insert into cvregp(bidid, Groupid, fname, lname, NICKNAME, email, age,"
                query += " FirstConv, position1, position2, preferAddr, country, ADDR1, ADDR2, CITY, stateprov, postcode,"
                query += " hphone, bphone, nametag, banquet, nobanqu, convlbl, convltr, banqreg, banqtck,"
                query += " verified, newpledge, pledgepd, envelope, noshow)"
                query += " values(" & BidNum & ", " & GroupNum & ", '" & row("firstName").ToString & "', '"
                query += row("lastName").ToString & "', '" & row("nickname").ToString & " ', '" & row("email").ToString.Trim & " ', "
                query += row("age") & ", '" & firstconv & "', '" & pos1text & "', '" & pos2text & " ', '"
                query += prefmaddr & "', '" & country & "', '" & row("address1").ToString & "', '" & row("address2").ToString & " ', '"
                query += row("city").ToString & "', '" & statecode.Trim & "', '" & row("zip").Trim & "', '"
                query += row("homePhone").ToString.Trim & "', '" & row("businessPhone").ToString.Trim & "', '0', '"
                query += banquetonly & "', '" & nobanquet & "','0','0','0','0','0','0','0','0','0')"

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function InsertRegistrant(ByVal regtype As String, ByVal dt As DataTable, ByVal dta As DataTable, ByVal dtg As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertRegistrant
        'Desc. . : Loades all the information abour a group of attendies.
        'Parms . : Table of registrants, table of activities, table of group info
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim BidNum As Integer = 0
        Dim firstconv As String = String.Empty
        Dim banquetonly As String = String.Empty
        Dim nobanquet As String = String.Empty
        Dim GroupNum As Int64 = 0
        Dim wtext As String = String.Empty

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            BidNum = GetLastBidID(regtype)

            For Each rowg As DataRow In dtg.Rows
                Dim today As String
                today = DateTime.Now.ToString("yyyyMMddhhmmss")

                GroupNum = rowg("groupnum")

                query = "insert into cvgrppayp(groupid, paytype, cclastfour, ccexp, ccname, ccresponse, payamount, paydatetim, paystatus, source)"
                query += " values(" & GroupNum & ", '" & rowg("cardtype").ToString & "', '"
                query += rowg("cardnum").ToString & "', '" & rowg("cardexp").ToString & " ', '" & rowg("cardname").ToString.Trim & " ', '"
                query += rowg("cardauth").ToString().Trim() & "', " & rowg("amount") & ", '"
                query += today & "', '" & rowg("paystatus").ToString & "', '" & rowg("source") & "')"

                Command = New iDB2Command(query, conn)
                If Not conn.State = ConnectionState.Open Then
                    conn.Open()
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
                    If row("nobanquet") Then
                        nobanquet = "1"
                    Else
                        nobanquet = "0"
                    End If

                    wtext = row("title1").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    row("title1") = wtext.Trim()

                    wtext = row("title2").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    row("title2") = wtext.Trim()
                    wtext = row("firstName").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    row("firstName") = wtext
                    wtext = row("lastName").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    row("lastName") = wtext
                    wtext = row("nickname").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    row("nickname") = wtext
                    wtext = row("street1").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    row("street1") = wtext
                    wtext = row("street2").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    row("street2") = wtext
                    wtext = row("city").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    row("city") = wtext
                    wtext = row("dietrest").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    row("dietrest") = wtext

                    query = "insert into cvregp(bidid, Groupid, fname, lname, NICKNAME, email, age,"
                    query += " FirstConv, position1, position2, preferAddr, country, ADDR1, ADDR2, CITY, stateprov, postcode,"
                    query += " hphone, bphone, dietrest, banquet, volcode, nobanqu, nametag, convlbl, convltr, banqreg, banqtck, "
                    query += "verified, newpledge, pledgepd, envelope, noshow)"
                    query += " values(" & BidNum & ", " & GroupNum & ", '" & row("firstName").ToString & "', '"
                    query += row("lastName").ToString & "', '" & row("nickname").ToString & " ', '" & row("email").ToString().Trim() & " ', "
                    query += row("age") & ", '" & firstconv & "', '" & row("title1") & "', '" & row("title2") & " ', '"
                    query += row("perfmaddr") & "', '" & row("country") & "', '" & row("street1").ToString & "', '" & row("street2").ToString & " ', '"
                    query += row("city").ToString & "', '" & row("state").ToString & "', '" & row("zipcode").Trim & "', '"
                    query += row("phone").ToString.Trim & "', '" & row("busphone").ToString.Trim & "', '"
                    query += row("dietrest").ToString.Trim & "', '" & banquetonly & "', '"
                    query += row("volcode").ToString.Trim & "', '" & nobanquet & "','0','0','0','0','0','0','0','0','0','0')"

                    Command = New iDB2Command(query, conn)
                    rc = Command.ExecuteNonQuery

                    If row("leadshp1").ToString() <> "  " And row("leadshp1").ToString() <> "" Then
                        query = "insert into cvregldp(bidid, groupid, ldrshipid) "
                        query += "values(" & BidNum & ", " & GroupNum & ", "
                        query += row("leadshp1") & ")"

                        Command = New iDB2Command(query, conn)
                        rc = Command.ExecuteNonQuery
                    End If

                    If row("leadshp2").ToString() <> "  " And row("leadshp2").ToString() <> "" Then
                        query = "insert into cvregldp(bidid, groupid, ldrshipid) "
                        query += "values(" & BidNum & ", " & GroupNum & ", "
                        query += row("leadshp2") & ")"

                        Command = New iDB2Command(query, conn)
                        rc = Command.ExecuteNonQuery
                    End If

                    If row("leadshp3").ToString() <> "  " And row("leadshp3").ToString() <> "" Then
                        query = "insert into cvregldp(bidid, groupid, ldrshipid) "
                        query += "values(" & BidNum & ", " & GroupNum & ", "
                        query += row("leadshp3") & ")"

                        Command = New iDB2Command(query, conn)
                        rc = Command.ExecuteNonQuery
                    End If

                    'Dim rowa As DataRow() = dta.[Select]("recnumber =" & row("recnumber"))
                    For Each rowa As DataRow In dta.Rows
                        If rowa("recnumber") = row("recnumber") Then

                            wtext = rowa("activity").ToString().Trim()
                            wtext = wtext.Replace("'", "''")
                            rowa("activity") = wtext

                            query = "insert into cvregitmp(bidid, groupid, itemid, itemdesc, itemdate, itemamount) "
                            query += "values(" & BidNum & ", " & GroupNum & ", "
                            query += rowa("actnumber") & ", '" & rowa("activity").ToString.Trim & "', '"
                            query += rowa("actdate") & "', " & rowa("amount") & ")"

                            Command = New iDB2Command(query, conn)
                            rc = Command.ExecuteNonQuery
                        End If
                    Next

                Next

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateGroupPay(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateGroupPay
        'Desc. . : Update Group Pay record
        'Parms . : 1.) Group Pay Data Table
        '
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                query = "update cvgrppayp set "
                query += "paytype='" & row("cardtype").ToString.Trim & "', "
                query += "cclastfour='" & row("cardnum").ToString.Trim & "', "
                query += "ccexp='" & row("cardexp").ToString & "', "
                query += "ccname='" & row("cardname").ToString & "', "
                query += "ccresponse='" & row("cardauth").ToString & "', "
                query += "payamount=" & row("amount").ToString & ", "
                query += "paystatus='" & row("paystatus").ToString & "', "
                query += "source='" & row("source").ToString & "' "
                query += "where groupid=" & row("groupnum")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function UpdateRegistrant(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateRegistrant
        'Desc. . : Update Registrant record with Position1, 2, VolCode
        'Parms . : 1.) Registrant Data Table
        '
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                wtext = row("position1").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("position1") = wtext
                wtext = row("position2").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("position2") = wtext

                query = "update cvregp set "
                query += "position1='" & row("position1").ToString.Trim & "', "
                query += "position2='" & row("position2").ToString.Trim & "', "
                query += "volcode='" & row("volcode").ToString & "' "
                query += "where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function UpdateRegistrantVerified(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateRegistrantVerified
        'Desc. . : Update Registrant record with Verified, NewPledge, PledgePd
        'Parms . : 1.) Registrant Data Table
        '
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows


                query = "update cvregp set "
                query += "verified='" & row("verified").ToString.Trim & "', "
                query += "newpledge='" & row("newpledge").ToString.Trim & "', "
                query += "pledgepd='" & row("pledgepd").ToString & "' "
                query += "where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function UpdateRegistrant(ByVal dt As DataTable, ByVal bolConvAttend As Boolean, ByVal DTA As DataTable, ByVal bolLeaderShip As Boolean, ByVal DTL As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateRegistrant
        'Desc. . : Updates the information about an attendie.
        'Parms . : Table of registrants, table of activities, table of group info
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim firstconv As String = String.Empty
        Dim banquetonly As String = String.Empty
        Dim nobanquet As String = String.Empty
        Dim nametag As String = String.Empty
        Dim noshow As String = String.Empty
        Dim nameaddr As String = String.Empty
        Dim confletter As String = String.Empty
        Dim envelope As String = String.Empty
        Dim verified As String = String.Empty
        Dim newpledge As String = String.Empty
        Dim pledgepd As String = String.Empty
        Dim country As String = String.Empty
        Dim zip As String = String.Empty
        Dim fzip As String = String.Empty
        Dim prefmaddr As String = String.Empty
        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows
                Dim today As Date = DateTime.Today
                If row("firstConv") Then
                    firstconv = "1"
                Else
                    firstconv = "0"
                End If
                If row("banquet") Then
                    banquetonly = "1"
                Else
                    banquetonly = "0"
                End If
                If row("nobanqu") Then
                    nobanquet = "1"
                Else
                    nobanquet = "0"
                End If
                If row("nametag") Then
                    nametag = "1"
                Else
                    nametag = "0"
                End If
                If row("noshow") Then
                    noshow = "1"
                Else
                    noshow = "0"
                End If
                If row("convlbl") Then
                    nameaddr = "1"
                Else
                    nameaddr = "0"
                End If
                If row("convltr") Then
                    confletter = "1"
                Else
                    confletter = "0"
                End If
                If row("envelope") Then
                    envelope = "1"
                Else
                    envelope = "0"
                End If
                If row("verified") Then
                    verified = "1"
                Else
                    verified = "0"
                End If
                If row("newpledge") Then
                    newpledge = "1"
                Else
                    newpledge = "0"
                End If
                If row("pledgepd") Then
                    pledgepd = "1"
                Else
                    pledgepd = "0"
                End If

                wtext = row("position1").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("position1") = wtext
                wtext = row("position2").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("position2") = wtext
                wtext = row("fName").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("fName") = wtext
                wtext = row("lName").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("lName") = wtext
                wtext = row("nickname").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("nickname") = wtext
                wtext = row("addr1").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("addr1") = wtext
                wtext = row("addr2").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("addr2") = wtext
                wtext = row("city").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("city") = wtext
                wtext = row("dietrest").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("dietrest") = wtext

                query = "update cvregp set "
                query += "fname='" & row("fname").ToString.Trim & "', "
                query += "lname='" & row("lname").ToString.Trim & "', "
                query += "NICKNAME='" & row("nickname").ToString.Trim & "', "
                query += "email='" & row("email").ToString.Trim & "', "
                query += "age=" & row("age") & ", "
                query += "FirstConv='" & firstconv & "', "
                query += "position1='" & row("position1") & "', "
                query += "position2='" & row("position2") & "', "
                query += "preferAddr='" & row("preferaddr") & "', "
                query += "country='" & row("country") & "', "
                query += "ADDR1= '" & row("addr1") & "', "
                query += "ADDR2= '" & row("addr2") & "', "
                query += "CITY='" & row("city") & "', "
                query += "stateprov='" & row("stateprov") & "', "
                query += "postcode='" & row("postcode") & "', "
                query += "hphone='" & row("hphone") & "', "
                query += "bphone='" & row("bphone") & "', "
                query += "dietrest='" & row("dietrest") & "', "
                query += "volcode='" & row("volcode") & "', "
                query += "banquet='" & banquetonly & "', "
                query += "nobanqu='" & nobanquet & "', "
                query += "nametag='" & nametag & "', "
                query += "noshow='" & noshow & "', "
                query += "convlbl='" & nameaddr & "', "
                query += "convltr='" & confletter & "', "
                query += "envelope='" & envelope & "', "
                query += "verified='" & verified & "', "
                query += "newpledge='" & newpledge & "', "
                query += "pledgepd='" & pledgepd & "' "
                query += "where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

                If bolConvAttend Then

                    query = "delete from cvregitmp where bidid =" & row("bidid")
                    Command = New iDB2Command(query, conn)
                    rc = Command.ExecuteNonQuery

                    For Each rowa As DataRow In DTA.Rows

                        wtext = rowa("itemdesc").ToString().Trim()
                        wtext = wtext.Replace("'", "''")
                        rowa("itemdesc") = wtext

                        query = "insert into cvregitmp(bidid, groupid, itemid, itemdesc, itemdate, itemamount) "
                        query += "values(" & rowa("bidid") & ", " & rowa("groupid") & ", "
                        query += rowa("itemid") & ", '" & rowa("itemdesc").ToString.Trim & "', '"
                        query += rowa("itemdate") & "', " & rowa("itemamount") & ")"

                        Command = New iDB2Command(query, conn)
                        rc = Command.ExecuteNonQuery

                    Next

                    query = "SELECT SUM(itemamount) as RegAmount FROM cvregitmp WHERE groupid=" & row("groupid")

                    Dim da As New iDB2DataAdapter(query, conn)
                    Dim dtAmount = New DataTable
                    dtAmount.Clear()
                    da.Fill(dtAmount)

                    query = "update cvgrppayp set "
                    query += "payamount=" & dtAmount(0).Item("RegAmount") & " where groupid =" & row("groupid")

                    Command = New iDB2Command(query, conn)
                    rc = Command.ExecuteNonQuery

                End If

                If bolLeaderShip Then

                    query = "delete from cvregldp where bidid =" & row("bidid")
                    Command = New iDB2Command(query, conn)
                    rc = Command.ExecuteNonQuery

                    For Each rowl As DataRow In DTL.Rows

                        query = "insert into cvregldp(bidid, groupid, ldrshipid) "
                        query += "values(" & rowl("bidid") & ", " & rowl("groupid") & ", "
                        query += rowl("ldrshipid") & ")"

                        Command = New iDB2Command(query, conn)
                        rc = Command.ExecuteNonQuery

                    Next
                End If

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateSeating(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateSeating
        'Desc. . : Update record in CVSEATP file using SQL
        'Parms . : 1.) Group Seating Data Table
        '
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                wtext = row("seatwith").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("seatwith") = wtext

                If row("deleterec") = "Yes" Then
                    query = "delete from cvseatp "
                    query += "where groupid=" & row("groupid")
                Else
                    If row("newseat") = "No" Then
                        query = "update cvseatp set "
                        query += "seatwith='" & row("seatwith").ToString.Trim & "' "
                        query += "where groupid=" & row("groupid")
                    Else
                        query = "insert into cvseatp(groupid, seatwith) "
                        query += "values(" & row("groupid").ToString.Trim & ", '"
                        query += row("seatwith").ToString.Trim & "')"
                    End If
                End If
                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function UpdateTable(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateSeating
        'Desc. . : Update record in CVSEATP file using SQL
        'Parms . : 1.) Group Seating Data Table
        '
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                query = "update cvregp set "
                query += "banqtbl=" & row("banqtbl")
                query += " where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function UpdateLetterFlag(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateLetterFlag
        'Desc. . : Updates the Letter printed flag on CVREGP
        'Parms . : Table of registrants
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                query = "update cvregp set "
                query += "convltr = '1'"
                query += "where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateNameAddrFlag(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateNameAddrFlag
        'Desc. . : Updates the Name & Address Labels printed flag on CVREGP
        'Parms . : Table of registrants
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                query = "update cvregp set "
                query += "convlbl = '1'"
                query += "where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateBanqRegFlag(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateBanqRegFlag
        'Desc. . : Updates the Banquet Registration Card printed flag on CVREGP
        'Parms . : Table of registrants
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                query = "update cvregp set "
                query += "banqreg = '1' "
                query += "where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateBanqTckFlag(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateBanqTckFlag
        'Desc. . : Updates the Banguet Ticket printed flag on CVREGP
        'Parms . : Table of registrants
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                query = "update cvregp set "
                query += "banqtck = '1' "
                query += "where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateEnvelopesFlag(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateNameTagFlag
        'Desc. . : Updates the Envelope printed flag on CVREGP
        'Parms . : Table of registrants
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                query = "update cvregp set "
                query += "envelope = '1' "
                query += "where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateNameTagFlag(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateNameTagFlag
        'Desc. . : Updates the Name Tag printed flag on CVREGP
        'Parms . : Table of registrants
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                query = "update cvregp set "
                query += "nametag = '1' "
                query += "where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateExpoRecord(ByVal RecNo As Integer, ByVal RecType As String, ByVal RecName As String, ByVal RecPrint As Boolean, ByVal RecCount As Double) As Boolean
        '---------------------------------------------------------
        'Function: UpdateExpoRecord
        'Desc. . : Updates the Expo Exhibitor Names printed flag on CVEXPOP
        'Parms . : Table of names
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim wtext As String
        Dim printed As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            wtext = RecName.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            RecName = wtext

            If RecPrint Then
                printed = "1"
            Else
                printed = "0"
            End If

            query = "update cvexpop set "
            query += "exptype = '" & RecType.Trim() & "', "
            query += "expname = '" & RecName.Trim() & "', "
            query += "expprint = '" & printed & "', "
            query += "expcount = " & RecCount & " "
            query += "where exprecno=" & RecNo

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateExpoExhibFlag(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateExpoExhibFlag
        'Desc. . : Updates the Expo Exhibitor Names printed flag on CVEXPOP
        'Parms . : Table of names
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim savname As String = String.Empty
        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                wtext = row("expname").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("expname") = wtext

                If row("expname") <> savname Then

                    query = "update cvexpop set "
                    query += "expprint = '1' "
                    query += "where expname='" & row("expname") & "' and exptype='E'"

                    Command = New iDB2Command(query, conn)
                    rc = Command.ExecuteNonQuery

                    savname = row("expname")
                End If

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateExpoMediaFlag(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateExpoMediaFlag
        'Desc. . : Updates the Expo Media Names printed flag on CVEXPOP
        'Parms . : Table of names
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim savname As String = String.Empty
        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                wtext = row("expname").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("expname") = wtext

                If row("expname") <> savname Then

                    query = "update cvexpop set "
                    query += "expprint = '1' "
                    query += "where expname='" & row("expname") & "' and exptype='M'"

                    Command = New iDB2Command(query, conn)
                    rc = Command.ExecuteNonQuery

                    savname = row("expname")
                End If

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateExpoSponFlag(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateExpoSponFlag
        'Desc. . : Updates the Expo Sponsor Names printed flag on CVEXPOP
        'Parms . : Table of names
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim savname As String = String.Empty
        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For Each row As DataRow In dt.Rows

                wtext = row("expname").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("expname") = wtext

                If row("expname") <> savname Then

                    query = "update cvexpop set "
                    query += "expprint = '1' "
                    query += "where expname='" & row("expname") & "' and exptype='S'"

                    Command = New iDB2Command(query, conn)
                    rc = Command.ExecuteNonQuery

                    savname = row("expname")
                End If

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function DeleteExpoRecord(ByVal recno As Integer) As Boolean
        '---------------------------------------------------------
        'Function: DeleteExpoRecord
        'Desc. . : Delete a record from CVEXPOP
        'Parms . : Record number
        'Returns : Returns a boolean object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "delete from cvexpop where exprecno =" & recno
            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

End Class
