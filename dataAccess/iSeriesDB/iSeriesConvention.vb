Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
Imports System.data
Imports System.Web.HttpContext
Imports System.Configuration
Imports System.Net.Mail

Public Class iSeriesConvention

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("ConviSeries"))
    Shared mvLastError As String = ""
    Shared count As Integer

    Shared Function BuildWorkActivities() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkActivities
        'Desc. . : Build work file for Activities count 
        '           
        'Parms . : 0
        'Returns : 
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
            oCmd.CommandText = "CONVC016"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
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
            oCmd.CommandText = "CONVC002"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkEnvelope() As String
        '---------------------------------------------------------
        'Function: BuildWorkEnvelope
        'Desc. . : Build ifs file for printing Envelopes in print shop 
        '           
        'Parms . : 0
        'Returns : Number records selected.
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

            oCmd.Parameters.Add("numsel", iDB2DbType.iDB2Char, 4)
            oCmd.Parameters("numsel").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("numsel").Value = " "

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVR014"

            rc = oCmd.ExecuteNonQuery()

            Return oCmd.Parameters("numsel").Value

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkNameAddr() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkNameAddr
        'Desc. . : Build work file of Name & Address for pre-convention maiiling 
        '           
        'Parms . : 0
        'Returns : 
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
            oCmd.CommandText = "CONVC017"

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
            oCmd.CommandText = "CONVC003"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkNameTag(ByVal tagtype As String) As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkNameTag
        'Desc. . : Build work file for printing Name Tags 
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

            oCmd.Parameters.Add("tagtype", iDB2DbType.iDB2Char, 120)
            oCmd.Parameters("tagtype").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("tagtype").Value = tagtype

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVC003S"

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

    Shared Function BuildWorkGroupLabels() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkGroupLabels
        'Desc. . : Build work file for printing Group Labels 
        '           
        'Parms . : 0
        'Returns : 
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
            oCmd.CommandText = "CONVC010"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkThankYou() As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkThankYou
        'Desc. . : Build work file for printing Thank You Letters 
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
            oCmd.CommandText = "CONVC011"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function BuildWorkVolCode(ByVal hyear As String) As Boolean
        '---------------------------------------------------------
        'Function: BuildWorkNameTag
        'Desc. . : Build work file for printing Name Tags 
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

            oCmd.Parameters.Add("hyear", iDB2DbType.iDB2Char, 4)
            oCmd.Parameters("hyear").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("hyear").Value = hyear

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVC018"

            rc = oCmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

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

    Shared Function DeleteGroupDetail(ByVal groupid, ByVal pseq) As Boolean
        '---------------------------------------------------------
        'Function: DeleteGroupDetail
        'Desc. . : Delete a record from Group Payments file
        'Parms . : Group number & Sequence Number
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

            query = "delete from cvgrpdtlp where groupid =" & groupid & " and pseq =" & pseq
            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
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

    Shared Function FindRegGName(ByVal lastName As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegGName
        'Desc. . : Find all registrants with the last name passed into it.
        'Parms . : 1.) Last Name
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As String = String.Empty
        Dim firstTime As Boolean = True

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            lastName = lastName.Replace("'", "''")

            Dim r As Integer

            r = lastName.IndexOf("%")

            'If r > 0 Then
            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu, groupid "
            query += "from cvregp where (ucase(lname) LIKE ucase('" & lastName.Trim & "%')) ORDER BY lname,fname"

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

    Shared Function FindRegGName() As DataTable
        '---------------------------------------------------------
        'Function: FindRegGName
        'Desc. . : Find all registrants in last name order.
        'Parms . : None
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu, groupid "
            query += "from cvregp ORDER BY lname,fname"

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

    Shared Function FindRegGName(ByVal lastName As String, ByVal stateCode As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegGName
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, groupid "
            query += "from cvregp where (ucase(lname) LIKE ucase('" & lastName.Trim & "%')) and stateprov = '" & stateCode & "' "
            query += "ORDER BY lname,fname"

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

    Shared Function FindRegName(ByVal lastName As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegName
        'Desc. . : Find all registrants with the last name passed into it.
        'Parms . : 1.) Last Name
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As String = String.Empty
        Dim firstTime As Boolean = True

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            lastName = lastName.Replace("'", "''")

            Dim r As Integer

            r = lastName.IndexOf("%")

            'If r > 0 Then
            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu "
            query += "from cvregp where (ucase(lname) LIKE ucase('" & lastName.Trim & "%') and nobanqu = '0') ORDER BY lname,fname"
            'Else
            '    query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            '    query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu "
            '    query += "from cvregp where (lname = '" & lastName & "' and nobanqu = '0') ORDER BY lname,fname"
            'End If

            'query = "select * from table(CONVNMTBL(CHAR('" & lastName & "'),CHAR('  '))) as t "
            'query += "order by tablenum"

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

    Shared Function FindRegName() As DataTable
        '---------------------------------------------------------
        'Function: FindRegName
        'Desc. . : Find all registrants in last name order.
        'Parms . : None
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu "
            query += "from cvregp where (nobanqu = '0') ORDER BY lname,fname"

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

    Shared Function FindRegName(ByVal lastName As String, ByVal stateCode As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegName
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl "
            query += "from cvregp where (ucase(lname) LIKE ucase('" & lastName.Trim & "%') and stateprov = '" & stateCode & "' and nobanqu = '0') "
            query += "ORDER BY lname,fname"

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

    Shared Function FindRegGFName(ByVal firstName As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegGFName
        'Desc. . : Find all registrants with the first name passed into it.
        'Parms . : 1.) first Name
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As String = String.Empty
        Dim firstTime As Boolean = True

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            firstName = firstName.Replace("'", "''")

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu, groupid "
            query += "from cvregp where (ucase(fname) LIKE ucase('" & firstName.Trim & "%')) ORDER BY lname,fname"

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

    Shared Function FindRegGFName(ByVal firstName As String, ByVal lastName As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegGFName
        'Desc. . : Find all registrants with the first name and last name passed into it.
        'Parms . : 1.) First Name
        '          2.) Last Name
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As String = String.Empty
        Dim firstTime As Boolean = True

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            firstName = firstName.Replace("'", "''")
            lastName = lastName.Replace("'", "''")

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu, groupid "
            query += "from cvregp where (ucase(fname) LIKE ucase('" & firstName.Trim & "%') and ucase(lname) LIKE ucase('" & lastName.Trim & "%')) "
            query += "ORDER BY lname,fname"

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

    Shared Function FindRegGFName(ByVal firstName As String, ByVal lastName As String, ByVal stateCode As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegFName
        'Desc. . : Find all registrants with the first name and last name and state code passed into it.
        'Parms . : 1.) First Name
        '          2.) Last Name
        '          3.) State Code 
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As String = String.Empty
        Dim firstTime As Boolean = True

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            firstName = firstName.Replace("'", "''")
            lastName = lastName.Replace("'", "''")

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu, groupid "
            query += "from cvregp where (ucase(fname) LIKE ucase('" & firstName.Trim & "%') and ucase(lname) LIKE ucase('" & lastName.Trim & "%') "
            query += "and stateprov = '" & stateCode & "') ORDER BY lname,fname"

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

    Shared Function FindRegGFSName(ByVal firstName As String, ByVal stateCode As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegGFSName
        'Desc. . : Find all registrants with the first name and state passed into it.
        'Parms . : 1.) first Name
        '          2.) state code
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

            firstName = firstName.Replace("'", "''")

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, groupid "
            query += "from cvregp where (ucase(fname) LIKE ucase('" & firstName.Trim & "%') and stateprov = '" & stateCode & "') "
            query += "ORDER BY lname,fname"

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

    Shared Function FindRegFName(ByVal firstName As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegFName
        'Desc. . : Find all registrants with the first name passed into it.
        'Parms . : 1.) first Name
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As String = String.Empty
        Dim firstTime As Boolean = True

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            firstName = firstName.Replace("'", "''")

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu "
            query += "from cvregp where (ucase(fname) LIKE ucase('" & firstName.Trim & "%') and nobanqu = '0') ORDER BY lname,fname"

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

    Shared Function FindRegFName(ByVal firstName As String, ByVal lastName As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegFName
        'Desc. . : Find all registrants with the first name and last name passed into it.
        'Parms . : 1.) First Name
        '          2.) Last Name
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As String = String.Empty
        Dim firstTime As Boolean = True

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            firstName = firstName.Replace("'", "''")
            lastName = lastName.Replace("'", "''")

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu "
            query += "from cvregp where (ucase(fname) LIKE ucase('" & firstName.Trim & "%') and ucase(lname) LIKE ucase('" & lastName.Trim & "%') "
            query += "and nobanqu = '0') ORDER BY lname,fname"

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

    Shared Function FindRegFName(ByVal firstName As String, ByVal lastName As String, ByVal stateCode As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegFName
        'Desc. . : Find all registrants with the first name and last name and state code passed into it.
        'Parms . : 1.) First Name
        '          2.) Last Name
        '          3.) State Code 
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As String = String.Empty
        Dim firstTime As Boolean = True

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            firstName = firstName.Replace("'", "''")
            lastName = lastName.Replace("'", "''")

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu "
            query += "from cvregp where (ucase(fname) LIKE ucase('" & firstName.Trim & "%') and ucase(lname) LIKE ucase('" & lastName.Trim & "%') "
            query += "and stateprov = '" & stateCode & "' and nobanqu = '0') ORDER BY lname,fname"

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

    Shared Function FindRegFSName(ByVal firstName As String, ByVal stateCode As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegFSName
        'Desc. . : Find all registrants with the first name and state passed into it.
        'Parms . : 1.) first Name
        '          2.) state code
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

            firstName = firstName.Replace("'", "''")

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl "
            query += "from cvregp where (ucase(fname) LIKE ucase('" & firstName.Trim & "%') and stateprov = '" & stateCode & "' and nobanqu = '0') "
            query += "ORDER BY lname,fname"

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

    Shared Function FindRegGState(ByVal stateCode As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegState
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, groupid "
            query += "from cvregp where (STATEPROV = '" & stateCode & "') "
            query += "ORDER BY lname,fname"

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

    Shared Function FindRegState(ByVal stateCode As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegState
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl "
            query += "from cvregp where (STATEPROV = '" & stateCode & "' and nobanqu = '0') "
            query += "ORDER BY lname,fname"

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

    Shared Function FindGReg(ByVal bidID As String) As DataTable
        '---------------------------------------------------------
        'Function: FindGReg
        'Desc. . : Find a single registrants by Bid ID.
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu, groupid "
            query += "from cvregp where (bidid = " & bidID & ") ORDER BY lname,fname"

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

    Shared Function FindReg(ByVal bidID As String) As DataTable
        '---------------------------------------------------------
        'Function: FindReg
        'Desc. . : Find a single registrants by Bid ID.
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu, groupid "
            query += "from cvregp where (bidid = " & bidID & " and nobanqu = '0') ORDER BY lname,fname"

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

    Shared Function FindRegGroup(ByVal groupNum As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegGroup
        'Desc. . : Find all registrants for a group.
        'Parms . : 1.) Group Number
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu, groupid "
            query += "from cvregp where (groupid = " & groupNum & ") ORDER BY lname,fname"

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

    Shared Function FindRegTable(ByVal tableNum As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegTable
        'Desc. . : Find all registrants for a table.
        'Parms . : 1.) Table Number
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu "
            query += "from cvregp where (banqtbl = " & tableNum & " and nobanqu = '0') ORDER BY lname,fname"

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

    Shared Function FindTblList(ByVal tableNum As String) As DataTable
        '---------------------------------------------------------
        'Function: FindTblList
        'Desc. . : Find table information
        'Parms . : 1.) State Code
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

            query = "select * from table(CONVSTTBL(CHAR('" & tableNum & "'))) as t "
            query += "order by tablenum"

            'query = "select banqtbl, COUNT(DISTINCT banqtbl) AS NumberOfTables, TRIM(stateprov) stateprov "
            'query += "from cvregp where (STATEPROV = '" & tableState & "' and nobanqu = '0') ORDER BY banqtbl"

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

    Shared Function FindTblRegName(ByVal lastName As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegName
        'Desc. . : Find all registrants with the last name passed into it.
        'Parms . : 1.) Last Name
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As Double = 9999
        Dim firstTime As Boolean = True

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            lastName = lastName.Replace("'", "''")

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu "
            query += "from cvregp where (ucase(lname) LIKE ucase('" & lastName.Trim & "%') and nobanqu = '0') ORDER BY banqtbl,lname,fname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Dim dtTableNames As DataTable = GetTableInfo()

            Dim dtTable As New DataTable
            dtTable.Columns.Add("tabledesc", Type.GetType("System.String"))
            dtTable.Columns.Add("tablenum", Type.GetType("System.Double"))
            dtTable.Columns.Add("tableempty", Type.GetType("System.String"))

            If dt.Rows.Count > 0 Then

                Dim dtWork As New DataTable

                For Each row In dt.Rows
                    If row("banqtbl") <> saveTable Then
                        If firstTime Then
                            saveTable = row("banqtbl")
                            firstTime = False
                        Else
                            dtWork = FindRegTable(saveTable)
                            Dim dr As DataRow = dtTable.NewRow()
                            Dim drN As DataRow() = dtTableNames.Select("tablenum=" + saveTable.ToString.Trim)
                            Dim cr As DataRow = drN(0)
                            dr.Item("tabledesc") = cr("tabledesc").ToString.Trim
                            dr.Item("tablenum") = saveTable
                            dr.Item("tableempty") = dtWork.Rows.Count.ToString.Trim
                            dtTable.Rows.Add(dr)
                            saveTable = row("banqtbl").ToString.Trim
                        End If
                    End If
                Next
                dtWork = FindRegTable(saveTable)
                Dim drF As DataRow = dtTable.NewRow()
                Dim drNm As DataRow() = dtTableNames.Select("tablenum=" + saveTable.ToString.Trim)
                Dim crn As DataRow = drNm(0)
                drF.Item("tabledesc") = crn("tabledesc").ToString.Trim
                drF.Item("tablenum") = saveTable
                drF.Item("tableempty") = dtWork.Rows.Count.ToString.Trim
                dtTable.Rows.Add(drF)
            End If

            Return dtTable

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function FindTblRegName(ByVal lastName As String, ByVal stateCode As String) As DataTable
        '---------------------------------------------------------
        'Function: FindRegName
        'Desc. . : Find all registrants with the last name passed into it.
        'Parms . : 1.) Last Name
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As Double = 9999
        Dim firstTime As Boolean = True
        Dim workTable As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            lastName = lastName.Replace("'", "''")

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu "
            query += "from cvregp where (ucase(lname) LIKE ucase('" & lastName.Trim & "%') and stateprov = '" & stateCode & "' and nobanqu = '0') "
            query += "ORDER BY banqtbl,lname,fname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            Dim dtTableNames As DataTable = GetTableInfo()

            Dim dtTable As New DataTable
            dtTable.Columns.Add("tabledesc", Type.GetType("System.String"))
            dtTable.Columns.Add("tablenum", Type.GetType("System.Double"))
            dtTable.Columns.Add("tableempty", Type.GetType("System.String"))
            dtTable.Columns.Add("tableofficer", Type.GetType("System.String"))

            Dim dtWork As New DataTable

            If dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    workTable = row("banqtbl").ToString.Trim
                    If workTable > "0" And workTable < "111" Then
                        If row("banqtbl").ToString.Trim <> saveTable Then
                            If firstTime Then
                                saveTable = row("banqtbl")
                                firstTime = False
                            Else
                                dtWork = FindRegTable(saveTable)
                                Dim dr As DataRow = dtTable.NewRow()
                                Dim drN As DataRow() = dtTableNames.Select("tablenum=" + saveTable.ToString.Trim)
                                Dim cr As DataRow = drN(0)
                                dr.Item("tabledesc") = cr("tabledesc").ToString.Trim
                                dr.Item("tablenum") = saveTable
                                dr.Item("tableempty") = dtWork.Rows.Count.ToString.Trim
                                dr.Item("tableofficer") = cr("offtable").ToString.Trim
                                dtTable.Rows.Add(dr)
                                saveTable = row("banqtbl")
                            End If
                        End If
                    End If
                Next
                If saveTable > 0 And saveTable < 111 Then
                    dtWork = FindRegTable(saveTable)
                    Dim drF As DataRow = dtTable.NewRow()
                    Dim drNm As DataRow() = dtTableNames.Select("tablenum=" + saveTable.ToString.Trim)
                    Dim crn As DataRow = drNm(0)
                    drF.Item("tabledesc") = crn("tabledesc").ToString.Trim
                    drF.Item("tablenum") = saveTable
                    drF.Item("tableempty") = dtWork.Rows.Count.ToString.Trim
                    drF.Item("tableofficer") = crn("offtable").ToString.Trim
                    dtTable.Rows.Add(drF)
                End If
            End If

            Return dtTable

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function FindTblSeat() As DataTable
        '---------------------------------------------------------
        'Function: FindRegName
        'Desc. . : Find all registrants with the last name passed into it.
        'Parms . : 1.) Last Name
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim saveTable As Double = 9999
        Dim firstTime As Boolean = True

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            'query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(stateprov) stateprov, CONCAT(TRIM(lname),CONCAT(', ',TRIM(fname))) fullname, "
            'query += "CONCAT(TRIM(lname),CONCAT(' - ',TRIM(stateprov))) reginfo, banqtbl, nobanqu "
            'query += "from cvregp where nobanqu = '0' ORDER BY banqtbl,lname,fname"

            'Dim da As New iDB2DataAdapter(query, conn)
            'Dim dta = New DataTable
            'dt.Clear()
            'da.Fill(dt)

            Dim dtTableNames As DataTable = GetTableInfo()

            Dim dtTable As New DataTable
            dtTable.Columns.Add("tabledesc", Type.GetType("System.String"))
            dtTable.Columns.Add("tablenum", Type.GetType("System.Double"))
            dtTable.Columns.Add("tableempty", Type.GetType("System.String"))
            dtTable.Columns.Add("offtable", Type.GetType("System.String"))

            Dim dtWork As New DataTable

            For Each row In dtTableNames.Rows
                dtWork = FindRegTable(row("tablenum").ToString.Trim)
                Dim dr As DataRow = dtTable.NewRow()
                dr.Item("tablenum") = row("tablenum")
                dr.Item("tabledesc") = row("tabledesc").ToString.Trim
                If dtWork.Rows.Count > 0 Then
                    dr.Item("tableempty") = dtWork.Rows.Count.ToString.Trim
                Else
                    dr.Item("tableempty") = "0"
                End If
                dr.Item("offtable") = row("offtable").ToString.Trim
                dtTable.Rows.Add(dr)
            Next

            Return dtTable

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function FindTblState(ByVal tableState As String, ByVal numESeat As Integer) As DataTable
        '---------------------------------------------------------
        'Function: FindTblState
        'Desc. . : Find all Tables for a given state.
        'Parms . : 1.) State Code
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

            query = "select * from table(CONVSTTBL(CHAR('" & tableState & "'))) as t "
            query += "order by tablenum"

            'query = "select banqtbl, COUNT(DISTINCT banqtbl) AS NumberOfTables, TRIM(stateprov) stateprov "
            'query += "from cvregp where (STATEPROV = '" & tableState & "' and nobanqu = '0') ORDER BY banqtbl"

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

    Shared Function FindTblState(ByVal tableState As String) As DataTable
        '---------------------------------------------------------
        'Function: FindTblState
        'Desc. . : Find all Tables for a given state.
        'Parms . : 1.) State Code
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

            query = "select * from table(CONVSTTBL(CHAR('" & tableState & "'))) as t "
            query += "order by tablenum"

            'query = "select banqtbl, COUNT(DISTINCT banqtbl) AS NumberOfTables, TRIM(stateprov) stateprov "
            'query += "from cvregp where (STATEPROV = '" & tableState & "' and nobanqu = '0') ORDER BY banqtbl"

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

    Shared Function FindTables(ByVal stateCode As String) As DataTable
        '---------------------------------------------------------
        'Function: FindTables
        'Desc. . : Find all Tables for a State/Prov.
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

            query = "select * from cvregp where STATEPROV = '" & stateCode & "' ORDER BY banqtbl"

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

    Shared Function GetAddr() As DataTable
        '---------------------------------------------------------
        'Function: GetAddr
        'Desc. . : Loades a data table of name and address for a mailing
        '           with anyone under 18.
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

            query = "select fname, lname, age, country, addr1, addr2, "
            query += "city, stateprov, postcode "
            query += "from cvregp "
            query += "where (age = 0 or age > 17)"

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

    Shared Function GetAddr(ByRef typeCode As String) As DataTable
        '---------------------------------------------------------
        'Function: GetAddr
        'Desc. . : Loades a data table of name and address for a mailing
        '           with anyone under 18.
        'Parms . : Code for head of househole
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

            query = "select fname, lname, age, country, addr1, addr2, "
            query += "city, stateprov, postcode "
            query += "from cvregp "
            query += "where headhouse = '1' "
            query += "order by lname"

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

    Shared Function GetAddrGroup() As DataTable
        '---------------------------------------------------------
        'Function: GetAddrGroup
        'Desc. . : Loades a data table of name and address for a mailing
        '           with one person per group.
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


            query = "SELECT GROUPID, PAYTYPE, fname, lname, CONCAT(TRIM(FNAME), CONCAT(' ', TRIM(LNAME))) AS FULLNAME, "
            query += "ADDR1, ADDR2, CITY, STATEPROV, POSTCODE "
            query += "from cvgrplabp "
            query += "where (paytype != 'Staff Charge        ') "
            query += "order by LNAME, FNAME"

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

    Shared Function GetAddrHist(ByRef hYear As String) As DataTable
        '---------------------------------------------------------
        'Function: GetAddrHist
        'Desc. . : Loades a data table of name and address for a mailing
        '           without anyone under 18.
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

            query = "select fname, lname, age, country, addr1, addr2, "
            query += "city, stateprov, postcode "
            query += "from cvreghp "
            query += "where ((age = 0 or age > 17) and (cvyear = " + hYear + "))"

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

    Shared Function GetBanquet(ByVal tablenum As String) As DataTable
        '---------------------------------------------------------
        'Function: GetBanquetHist
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

            query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, banqtbl, CONCAT(TRIM(fname),CONCAT(' ',TRIM(lname))) fullname "
            query += "from cvregl1 "
            query += "where banqtbl=" + tablenum + " "
            query += "order by banqtbl"

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

    Shared Function GetBanquet() As DataTable
        '---------------------------------------------------------
        'Function: GetBanquetHist
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

            query = "select bidid, stateprov, TRIM(fname) fname, TRIM(lname) lname, banqtbl, CONCAT(TRIM(fname),CONCAT(' ',TRIM(lname))) fullname "
            query += "from cvregl1 "
            'query += "where cvyear=" + hyear + " "
            query += "order by banqtbl, lname"

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

    Shared Function GetBanquetHist(ByVal hyear As String, ByVal tablenum As String) As DataTable
        '---------------------------------------------------------
        'Function: GetBanquetHist
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

            query = "select cvyear, bidid, TRIM(fname) fname, TRIM(lname) lname, banqtbl, CONCAT(TRIM(fname),CONCAT(' ',TRIM(lname))) fullname "
            query += "from cvreghl1 "
            query += "where cvyear=" + hyear + " and banqtbl=" + tablenum + " "
            query += "order by cvyear, banqtbl"

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

    Shared Function GetBanquetHist(ByVal hyear As String) As DataTable
        '---------------------------------------------------------
        'Function: GetBanquetHist
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

            query = "select cvyear, bidid, stateprov, TRIM(fname) fname, TRIM(lname) lname, banqtbl, CONCAT(TRIM(fname),CONCAT(' ',TRIM(lname))) fullname "
            query += "from cvreghl1 "
            query += "where cvyear=" + hyear + " "
            query += "order by cvyear, banqtbl, lname"

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

    Shared Function GetConvActivities() As DataTable
        '---------------------------------------------------------
        'Function: GetConActivities
        'Desc. . : Loades a data table of all Activities Counts.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            Dim rc As Boolean = BuildWorkActivities()

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select TRIM(desc) desc, activ, activnum from cvactivp order by activnum"

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

    Shared Function GetConvNameTag() As DataTable
        '---------------------------------------------------------
        'Function: GetConvNameTag
        'Desc. . : Loades a data table of all unprinted registrants
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

            query = "select * from table(convnamtag()) as t "
            query += "order by tagdsc, tagcnt desc"

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

    Shared Function GetEnvelope() As DataTable
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

            query = "select * from cvregp where envelope='0'"

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


    Shared Function GetNameAddr() As DataTable
        '---------------------------------------------------------
        'Function: GetConActivities
        'Desc. . : Loades a data table of all Activities Counts.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            Dim rc As Boolean = BuildWorkNameAddr()

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from cvlabelsl"

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

    Shared Function GetRegistrantHist(ByVal hyear As String) As DataTable
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

            query = "select cvyear, bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(country) country, TRIM(addr1) addr1, TRIM(city) city, TRIM(stateprov) stateprov, CONCAT(TRIM(fname),CONCAT(' ',TRIM(lname))) fullname "
            query += "from cvreghp "
            query += "where cvyear=" + hyear + " "
            query += "order by lname"

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

    Shared Function GetRegistrantHist(ByVal hyear As String, ByVal bidid As String) As DataTable
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

            query = "select * from cvreghp where (cvyear=" + hyear + " and bidid = " & bidid & ")"

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

    Shared Function GetRegistrantBrowse(ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrantBrowse
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

            query = "select p.*, c.stname, d.volcode, d.voldesc, e.seatwith"
            query += " from cvregp p"
            query += " left outer join cvstatep c on (c.stcountry = p.country and c.stcode = p.stateprov)"
            query += " left outer join cvvolp d on (d.volcode = p.volcode)"
            query += " left outer join cvseatp e on (e.groupid = p.groupid)"
            query += " where (p.bidid = " & bidid & ")"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("VolCdDesc")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim tblstring As String = dr("volcode").ToString.Trim + " - " + dr("voldesc").ToString.Trim
                dr("VolCdDesc") = tblstring

            Next


            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function
    Shared Function GetRegistrantHistBrowse(ByVal hyear As String, ByVal bidid As String) As DataTable
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

            query = "select p.*, c.stname, d.volcode, d.voldesc"
            query += " from cvreghp p"
            query += " left outer join cvstatep c on (c.stcountry = p.country and c.stcode = p.stateprov)"
            query += " left outer join cvvolp d on (d.volcode = p.volcode)"
            query += " where (p.cvyear=" + hyear + " and p.bidid = " & bidid & ")"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("VolCdDesc")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim tblstring As String = dr("volcode").ToString.Trim + " - " + dr("voldesc").ToString.Trim
                dr("VolCdDesc") = tblstring

            Next


            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegActivityInfo(ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegActivityInfo
        'Desc. . : Get registrant activity information for the bid ID passed into it.
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

            'query = "select p.bidid, p.groupid, p.fname, p.lname, p.stateprov, p.country, CONCAT(TRIM(p.fname),CONCAT(' ',TRIM(p.lname))) fullname, p.banqtbl, "
            'query += "p.city, p.position1, p.position2, p.firstconv, p.addr1, p.addr2, "
            query = "select * "
            query += "from cvregitmp "
            query += "where (bidid = " & bidid & ")"

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

    Shared Function GetRegGrpDtl(ByVal grpnum As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegGrpDtl
        'Desc. . : Get group payment detail for the group ID passed into it.
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

            query = "select p.*, c.* "
            query += "from cvgrpdtlp p "
            query += "left outer join cvgrppayp c on (c.groupid = p.groupid) "
            query += "where (p.groupid = " & grpnum & ")"

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

    Shared Function GetRegGroup(ByVal grpnum As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegGroupInfo
        'Desc. . : Get registrant table information for the bid ID passed into it.
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

            query = "select p.*, CONCAT(TRIM(p.fname),CONCAT(' ',TRIM(p.lname))) fullname, "
            query += "c.stname, d.seatwith, e.* "
            query += "from cvregp p "
            query += "left outer join cvstatep c on (c.stcountry = p.country and c.stcode = p.stateprov) "
            query += "left outer join cvseatp d on (d.groupid = p.groupid) "
            query += "left outer join cvgrppayp e on (e.groupid = p.groupid) "
            query += "where (p.groupid = " & grpnum & ")"

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

    Shared Function GetRegGroupInfo(ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegGroupInfo
        'Desc. . : Get registrant table information for the bid ID passed into it.
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

            query = "select p.*, CONCAT(TRIM(p.fname),CONCAT(' ',TRIM(p.lname))) fullname, "
            query += "c.stname, d.seatwith, e.volcode, e.voldesc "
            query += "from cvregp p "
            query += "left outer join cvstatep c on (c.stcountry = p.country and c.stcode = p.stateprov) "
            query += "left outer join cvseatp d on (d.groupid = p.groupid) "
            query += "left outer join cvvolp e on (e.volcode = p.volcode) "
            query += "where (p.bidid = " & bidid & ")"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("VolCdDesc")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim tblstring As String = dr("volcode").ToString.Trim + " - " + dr("voldesc").ToString.Trim
                dr("VolCdDesc") = tblstring

            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegTableInfo(ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegTableInfo
        'Desc. . : Get registrant table information for the bid ID passed into it.
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

            query = "select p.bidid, p.groupid, p.fname, p.lname, p.stateprov, p.country, CONCAT(TRIM(p.fname),CONCAT(' ',TRIM(p.lname))) fullname, p.banqtbl, "
            query += "p.city, p.position1, p.position2, p.firstconv, "
            query += "c.stname, d.seatwith "
            query += "from cvregp p "
            query += "left outer join cvstatep c on (c.stcountry = p.country and c.stcode = p.stateprov) "
            query += "left outer join cvseatp d on (d.groupid = p.groupid) "
            query += "where (p.bidid = " & bidid & ")"

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
                query = "select bidid, TRIM(fname) fname, TRIM(lname) lname, TRIM(country) country, TRIM(addr1) addr1, TRIM(city) city, TRIM(stateprov) stateprov, CONCAT(TRIM(fname),CONCAT(' ',TRIM(lname))) fullname, banqtbl "
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

    Shared Function GetActivityDetails(ByVal actNum As String) As DataTable
        '---------------------------------------------------------
        'Function: GetActivityDetails
        'Desc. . : Loades a data table of all registrants for a specific Activity.
        'Parms . : Activity number
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

            query = "select TRIM(lname) lname, TRIM(fname) fname, "
            query += "TRIM(email) email "
            query += "from cvregp p "
            If actNum = "1" Then
                query += "where activity1 = 1 "
            End If
            If actNum = "2" Then
                query += "where activity2 = 1 "
            End If
            If actNum = "3" Then
                query += "where activity3 = 1 "
            End If
            If actNum = "4" Then
                query += "where activity4 = 1 "
            End If
            If actNum = "5" Then
                query += "where activity5 = 1 "
            End If
            query += "order by lname"

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

    Shared Function GetAttending(ByVal hyear As String, ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetAttending
        'Desc. . : Find all registrants attending items from Bid ID
        'Parms . : 1.) History Year
        '          2.) Bid ID
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

            query = "select * from cvregitmhp where (bidid = " & bidid & " and cvyear = " & hyear & ")"

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

    Shared Function GetAttendingBrowse(ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetAttendingBrowse
        'Desc. . : Find all registrants attending items from Bid ID
        'Parms . : 1.) Bid ID
        '          
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

            query = "select p.bidid, p.groupid, p.itemid, p.itemdesc, p.itemdate, p.iteminfo, p.itemamount,"
            query += " c.tourtime,"
            query += " d.ttitle"
            query += " from cvregitmp p"
            query += " left outer join  cvdatep c on (c.tourid = p.itemid and c.tourdate = p.itemdate)"
            query += " left outer join  cvtoursp d on (d.tourid = p.itemid)"
            query += " where (p.bidid = " & bidid & ")"


            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("TourDesc")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim tblstring As String = dr("ttitle").ToString.Trim + "  (" + dr("tourtime").ToString.Trim + ")"
                dr("TourDesc") = tblstring

            Next


            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetAttendingHistBrowse(ByVal hyear As String, ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetAttendingHistBrowse
        'Desc. . : Find all registrants attending items from Bid ID
        'Parms . : 1.) History Year
        '          2.) Bid ID
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

            query = "select p.cvyear, p.bidid, p.groupid, p.itemid, p.itemdesc, p.itemdate, p.itemamount,"
            query += " c.tourtime,"
            query += " d.ttitle"
            query += " from cvregitmhp p"
            query += " left outer join  cvdatehp c on (c.cvyear = p.cvyear and c.tourid = p.itemid and c.tourdate = p.itemdate)"
            query += " left outer join  cvtourshp d on (d.cvyear = p.cvyear and d.tourid = p.itemid)"
            query += " where (p.cvyear = " & hyear & " and p.bidid = " & bidid & ")"


            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)

            dt.Columns.Add("TourDesc")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                Dim tblstring As String = dr("ttitle").ToString.Trim + "  (" + dr("tourtime").ToString.Trim + ")"
                dr("TourDesc") = tblstring

            Next


            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetAttendingTotal(ByVal groupid As String) As DataTable
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

            query = "select * from cvregitmp where (groupid = " & groupid & ")"

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

    Shared Function GetToureMail() As DataTable
        '---------------------------------------------------------
        'Function: GetToureMail
        'Desc. . : Return a table of all tours and emails.
        'Parms . : 1.) None
        '          
        'Returns : Returns a Data Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select p.bidid, p.itemid, p.itemdesc, c.lname, c.fname, c.email"
            query += " from cvregitmp p"
            query += " left outer join  cvregp c on (c.bidid = p.bidid)"
            query += " where (p.itemid > 0)"
            query += " order by p.itemdesc"

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

    Shared Function GetLdrAlpha() As DataTable
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

            'query = "select * from cvregldp where (bidid = " & bidid & ")"

            query = "SELECT p.LDRDESC, p.LDRGROUP, SUBSTRING(p.LDRDTTM,11,11) as leadtime, "
            query += "CONCAT(TRIM(d.LNAME), CONCAT(', ', TRIM(d.FNAME))) AS fullname, "
            query += "CONCAT(TRIM(d.CITY), CONCAT(', ', TRIM(d.STATEPROV))) AS hometown "
            query += "FROM CONVENTION.CVLDRSHPP p "
            query += "LEFT OUTER JOIN CONVENTION.CVREGLDP c ON c.LDRSHIPID = p.LDRID "
            query += "LEFT OUTER JOIN CONVENTION.CVREGP d ON c.BIDID = d.BIDID "
            query += "WHERE p.LDRGROUP < 99 "
            query += "GROUP BY p.LDRGROUP, p.LDRORDER, p.LDRDESC,p.LDRDTTM,d.LNAME,d.FNAME,d.CITY,d.STATEPROV "
            query += "ORDER BY p.LDRGROUP, p.LDRORDER, p.LDRDESC, fullname"

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

    Shared Function GetLeaderShipBrowse(ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetLeaderShipBrowse
        'Desc. . : Find all registrants leadership workshop items from Bid ID
        'Parms . : 1.) Bid ID
        '          
        'Returns : Returns a Data Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select p.bidid, p.ldrshipid,"
            query += " c.ldrdesc, c.ldrgroup"
            query += " from cvregldp p"
            query += " left outer join  cvldrshpp c on (c.ldrid = p.ldrshipid)"
            query += " where (p.bidid = " & bidid & ")"

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

    Shared Function GetLeaderShipBrowse(ByVal hyear As String, ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetLeaderShipBrowse
        'Desc. . : Find all registrants leadership workshop items from Bid ID
        'Parms . : 1.) History Year
        '          1.) Bid ID
        '
        'Returns : Returns a Data Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select p.bidid, p.ldrshipid, p.cvyear,"
            query += " c.ldrdesc, c.ldrgroup"
            query += " from cvregldHp p"
            query += " left outer join  cvldrshpp c on (c.ldrid = p.ldrshipid)"
            query += " where (p.cvyear = " & hyear & " and p.bidid = " & bidid & ")"

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

    Shared Function GetLeaderShipHistBrowse(ByVal hyear As String, ByVal bidid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetLeaderShipHistBrowse
        'Desc. . : Find all registrants leadership workshop items from Bid ID
        'Parms . : 1.) History Year
        '          1.) Bid ID
        '
        'Returns : Returns a Data Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select p.bidid, p.ldrshipid, p.cvyear,"
            query += " c.ldrdesc, c.ldrgroup"
            query += " from cvregldHp p"
            query += " left outer join  cvldrshpp c on (c.ldrid = p.ldrshipid)"
            query += " where (p.cvyear = " & hyear & " and p.bidid = " & bidid & ")"

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

    Shared Function GetLastSeqNum(ByVal sgroupid As String) As String
        '---------------------------------------------------------
        'Function: GetLastSeqNum
        'Desc. . : Get the last sequence number from payment records 
        'Parms . : 1). Group ID
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

            oCmd.Parameters.Add("groupid", iDB2DbType.iDB2Char, 10)
            oCmd.Parameters("groupid").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("groupid").Value = sgroupid

            oCmd.Parameters.Add("lastseqnum", iDB2DbType.iDB2Char, 3)
            oCmd.Parameters("lastseqnum").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("lastseqnum").Value = " "

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVR013"

            oCmd.ExecuteNonQuery()

            Return oCmd.Parameters("lastseqnum").Value

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetSeatedTotals() As DataTable
        '---------------------------------------------------------
        'Function: GetSeated
        'Desc. . : Loades a data table of totals for banquet worksheet.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim wtseats As Integer = 0
        Dim wunassgnd As Integer = 0
        Dim wempty As Integer = 0

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            dt1 = GetTableInfo()
            wtseats = 0
            For Each row As DataRow In dt1.Rows
                If row("tablenum") <> 0 And row("tablenum") <> 100 Then
                    wtseats += row("tableseats")
                End If
            Next

            dt2 = FindRegTable("0")
            wunassgnd = dt2.Rows.Count

            query = "select banqtbl, nobanqu "
            query += "from cvregp where (banqtbl <> 0 and banqtbl <> 100 and nobanqu = '0') "
            query += "ORDER BY banqtbl"

            Dim da As New iDB2DataAdapter(query, conn)
            dt3.Clear()
            da.Fill(dt3)

            wempty = wtseats - dt3.Rows.Count
            If wempty < 0 Then
                wempty = 0
            End If

            dt.Columns.Add("unassigned", Type.GetType("System.String"))
            dt.Columns.Add("assigned", Type.GetType("System.String"))
            dt.Columns.Add("emptyseats", Type.GetType("System.String"))

            Dim dr As DataRow = dt.NewRow()
            dr("unassigned") = wunassgnd.ToString.Trim
            dr("assigned") = dt3.Rows.Count.ToString.Trim
            dr("emptyseats") = wempty.ToString.Trim
            dt.Rows.Add(dr)

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

    Shared Function GetTableInfo() As DataTable
        '---------------------------------------------------------
        'Function: GetTableDescription
        'Desc. . : Loades a data table of all tables and their information.
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

            query = "select * from cvtablep "
            query += "order by tablenum"

            Dim daNames As New iDB2DataAdapter(query, conn)
            daNames.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetTableMast() As DataTable
        '---------------------------------------------------------
        'Function: GetTableMast
        'Desc. . : Get the table master record for convention
        'Parms . : None
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

            query = "select * from cvtblsetp"

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

    Shared Function GetTableNames(ByVal tablenum As String) As DataTable
        '---------------------------------------------------------
        'Function: GetTableNames
        'Desc. . : Loades a data table of all people ast the table of the bidid.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim dtNames As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            'query = "select banqtbl "
            'query += "from cvregp "
            'query += "where bidid =" + bidid + " and nobanqu = '0'"

            'Dim da As New iDB2DataAdapter(query, conn)
            'dt.Clear()
            'da.Fill(dt)

            'Dim dr As DataRow = dt.Rows.Item(0)
            'tablenum = dr("banqtbl").ToString.Trim

            query = "select lname, CONCAT(TRIM(fname), CONCAT(' ', TRIM(lNAME))) fullname, stateprov, banqtbl "
            query += "from cvregp "
            query += "where banqtbl =" + tablenum + " and nobanqu = '0' "
            query += "order by lname, fullname"

            Dim daNames As New iDB2DataAdapter(query, conn)
            dtNames.Clear()
            daNames.Fill(dtNames)

            Return dtNames

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

            query = "select * from cvregp where (banqtck = '0' and (banqtbl > 0 and banqtbl < 111)) order by lname, fname"

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

    Shared Function GetWorkNameTags(ByVal sel As Boolean) As DataTable
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

            query = "select * from cvregntagp order by lname, fname"

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

    Shared Function GetEMailAddr() As DataTable
        '---------------------------------------------------------
        'Function: GetEMailAddr
        'Desc. . : Loades a data table of all distinct email address.
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

            query = "select distinct(email) from cvregp"

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

    Shared Function GetHEMailAddr(ByVal hYear As String) As DataTable
        '---------------------------------------------------------
        'Function: GetEMailAddr
        'Desc. . : Loades a data table of all distinct email address.
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

            query = "select distinct(email) from cvreghp where cvyear ='" + hYear + "'"

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
            finType = finType.Replace("^", "&")
            finType = finType.Replace("*", "+")

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
                'If finType = "Family Reunion" Then
                '    query = "select TRIM(lname) lname, CONCAT(TRIM(fname),CONCAT(' ', TRIM(lname))) fullname, TRIM(addr1) addr1, "
                '    query += "TRIM(city) city, stateprov, TRIM(country) country, freunion, bidid, paytype "
                '    query += "from cvregp p "
                '    query += "left outer join cvgrppayp d on d.groupid = p.groupid "
                '    query += "where freunion='1' "
                '    query += "order by lname, TRIM(fullname)"
                'Else
                query = "select TRIM(lname) lname, CONCAT(TRIM(fname),CONCAT(' ', TRIM(lname))) fullname, TRIM(addr1) addr1, "
                query += "TRIM(city) city, stateprov, TRIM(country) country, p.bidid, paytype "
                query += "from cvregitmp p left outer join cvregp c on c.bidid = p.bidid "
                query += "left outer join cvgrppayp d on d.groupid = c.groupid "
                query += "where TRIM(itemdesc) = '" + finType + "' "
                query += "order by lname, TRIM(fullname)"
                'End If
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

    Shared Function GetFinalDetails(ByVal hYear As String, ByVal finType As String) As DataTable
        '---------------------------------------------------------
        'Function: GetFinalDetails
        'Desc. . : Loades a data table of all registrants for a specific registration type.
        'Parms . : History Year
        '        : Registration type 
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
            finType = finType.Replace("^", "&")
            finType = finType.Replace("*", "+")

            If finType = "Staff" Or finType = "Complimentary" Then
                If finType = "Staff" Then
                    grpStatus = "DU Staff"
                Else
                    grpStatus = finType
                End If
                query = "select TRIM(lname) lname, CONCAT(TRIM(fname),CONCAT(' ', TRIM(lname))) fullname, TRIM(addr1) addr1, "
                query += "TRIM(city) city, stateprov, TRIM(country) country, bidid, paytype "
                query += "from cvgrppayhp p left outer join cvreghp c on c.groupid = p.groupid "
                query += "where p.cvyear = " + hYear + " and paystatus = '" + grpStatus + "' "
                query += "order by lname, TRIM(fname)"
            Else
                'If finType = "Family Reunion" Then
                '    query = "select TRIM(lname) lname, CONCAT(TRIM(fname),CONCAT(' ', TRIM(lname))) fullname, TRIM(addr1) addr1, "
                '    query += "TRIM(city) city, stateprov, TRIM(country) country, freunion, bidid, paytype "
                '    query += "from cvregp p "
                '    query += "left outer join cvgrppayp d on d.groupid = p.groupid "
                '    query += "where freunion='1' "
                '    query += "order by lname, TRIM(fullname)"
                'Else
                query = "select TRIM(lname) lname, CONCAT(TRIM(fname),CONCAT(' ', TRIM(lname))) fullname, TRIM(addr1) addr1, "
                query += "TRIM(city) city, stateprov, TRIM(country) country, p.bidid, paytype "
                query += "from cvregitmhp p left outer join cvreghp c on c.bidid = p.bidid "
                query += "left outer join cvgrppayhp d on d.groupid = c.groupid "
                query += "where p.cvyear = " + hYear + " and TRIM(itemdesc) = '" + finType + "' "
                query += "order by lname, TRIM(fullname)"
                'End If
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
        Dim rConvGL As Decimal = 0
        Dim rTourCnt As Decimal = 0
        Dim rTourAmt As Decimal = 0
        Dim rTourGL As Decimal = 0

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
                        ndr.Item(4) = 0
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow
                        ndr.Item(0) = "Tours"
                        ndr.Item(1) = 0
                        ndr.Item(2) = 0
                        ndr.Item(3) = 2
                        ndr.Item(4) = 0
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow()
                        ndr.Item(0) = " "
                        ndr.Item(1) = 0
                        ndr.Item(2) = 0
                        ndr.Item(3) = 2
                        ndr.Item(4) = 0
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow
                        ndr.Item(0) = "Total Registered"
                        ndr.Item(1) = rConvCnt
                        ndr.Item(2) = rConvAmt
                        ndr.Item(3) = 2
                        ndr.Item(4) = rConvGL
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow()
                        ndr.Item(0) = " "
                        ndr.Item(1) = 0
                        ndr.Item(2) = 0
                        ndr.Item(3) = 2
                        ndr.Item(4) = 0
                        dt.Rows.InsertAt(ndr, i)
                        i += 5
                    End If
                    If svType = "4" Then
                        ndr = dt.NewRow
                        ndr.Item(0) = "Total Tours"
                        ndr.Item(1) = rTourCnt
                        ndr.Item(2) = rTourAmt
                        ndr.Item(3) = 6
                        ndr.Item(4) = rTourGL
                        dt.Rows.InsertAt(ndr, i)
                        i += 1
                        Exit For
                    End If
                    svType = ckType
                End If
                If ckType = "0" Then
                    rConvCnt = rConvCnt + Convert.ToDouble(dr("itemcnt"))
                    rConvAmt = rConvAmt + Convert.ToDouble(dr("itemamt"))
                    rConvGL = Convert.ToDouble(dr("itemgl"))
                End If
                If ckType = "4" Then
                    rTourCnt = rTourCnt + Convert.ToDouble(dr("itemcnt"))
                    rTourAmt = rTourAmt + Convert.ToDouble(dr("itemamt"))
                    rTourGL = Convert.ToDouble(dr("itemgl"))
                End If
            Next
            ndr = dt.NewRow()
            ndr.Item(0) = " "
            ndr.Item(1) = 0
            ndr.Item(2) = 0
            ndr.Item(3) = 6
            ndr.Item(4) = 0
            dt.Rows.Add(ndr)
            ndr = dt.NewRow()
            ndr.Item(0) = "Total Tours"
            ndr.Item(1) = rTourCnt
            ndr.Item(2) = rTourAmt
            ndr.Item(3) = 6
            ndr.Item(4) = rTourGL
            dt.Rows.Add(ndr)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetConvHFinal(ByVal histyear As String) As DataTable
        '---------------------------------------------------------
        'Function: GetConvHFinal
        'Desc. . : Loades a data table of all convention financials from history files.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim rConvCnt As Decimal = 0
        Dim rConvAmt As Decimal = 0
        Dim rConvGL As Decimal = 0
        Dim rTourCnt As Decimal = 0
        Dim rTourAmt As Decimal = 0
        Dim rTourGL As Decimal = 0

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * from table(convhfinal(CHAR('" + histyear + "'))) as t "
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
                        ndr.Item(4) = 0
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow
                        ndr.Item(0) = "Tours"
                        ndr.Item(1) = 0
                        ndr.Item(2) = 0
                        ndr.Item(3) = 2
                        ndr.Item(4) = 0
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow()
                        ndr.Item(0) = " "
                        ndr.Item(1) = 0
                        ndr.Item(2) = 0
                        ndr.Item(3) = 2
                        ndr.Item(4) = 0
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow
                        ndr.Item(0) = "Total Registered"
                        ndr.Item(1) = rConvCnt
                        ndr.Item(2) = rConvAmt
                        ndr.Item(3) = 2
                        ndr.Item(4) = rConvGL
                        dt.Rows.InsertAt(ndr, i)
                        ndr = dt.NewRow()
                        ndr.Item(0) = " "
                        ndr.Item(1) = 0
                        ndr.Item(2) = 0
                        ndr.Item(3) = 2
                        ndr.Item(4) = 0
                        dt.Rows.InsertAt(ndr, i)
                        i += 5
                    End If
                    If svType = "4" Then
                        ndr = dt.NewRow
                        ndr.Item(0) = "Total Tours"
                        ndr.Item(1) = rTourCnt
                        ndr.Item(2) = rTourAmt
                        ndr.Item(3) = 6
                        ndr.Item(4) = rTourGL
                        dt.Rows.InsertAt(ndr, i)
                        i += 1
                        Exit For
                    End If
                    svType = ckType
                End If
                If ckType = "0" Then
                    rConvCnt = rConvCnt + Convert.ToDouble(dr("itemcnt"))
                    rConvAmt = rConvAmt + Convert.ToDouble(dr("itemamt"))
                    rConvGL = Convert.ToDouble(dr("itemgl"))
                End If
                If ckType = "4" Then
                    rTourCnt = rTourCnt + Convert.ToDouble(dr("itemcnt"))
                    rTourAmt = rTourAmt + Convert.ToDouble(dr("itemamt"))
                    rTourGL = Convert.ToDouble(dr("itemgl"))
                End If
            Next
            ndr = dt.NewRow()
            ndr.Item(0) = " "
            ndr.Item(1) = 0
            ndr.Item(2) = 0
            ndr.Item(3) = 6
            ndr.Item(4) = 0
            dt.Rows.Add(ndr)
            ndr = dt.NewRow()
            ndr.Item(0) = "Total Tours"
            ndr.Item(1) = rTourCnt
            ndr.Item(2) = rTourAmt
            ndr.Item(3) = 6
            ndr.Item(4) = rTourGL
            dt.Rows.Add(ndr)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetConvGroup() As DataTable
        '---------------------------------------------------------
        'Function: GetConvGroup
        'Desc. . : Loades a data table of all convention groups that are out of ballance.
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

            query = "select * from table(convgroup()) as t "
            query += "order by grplname"

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

    Shared Function GetGroupTotal(ByVal groupnum As String) As DataTable
        '---------------------------------------------------------
        'Function: GetGroupTotal
        'Desc. . : Get the total for a Group ID
        'Parms . : group id number
        'Returns : Returns a Total amount.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select groupid, grpamount"
            query += " from cvgrppayp p"
            query += " where groupid = " & groupnum & " "
            'query += " order by lname, fname"

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

            query = "select CONCAT(TRIM(fname),CONCAT(' ',TRIM(lname))) fullname, lname, volcode"
            query += " from cvregp"
            'query += " left outer join cvgrppayp c on (c.groupid = p.groupid)"
            query += " where volcode = 'DU'"
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

    Shared Function GetConvStateList(ByVal stateid As String) As String
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

    Shared Function GetConvStateList() As DataTable
        '---------------------------------------------------------
        'Function: GetConvStateList
        'Desc. . : Loades a data table of Registrants in state name order.
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

            query = "SELECT p.FNAME, p.LNAME, CONCAT(TRIM(p.LNAME), CONCAT(', ', TRIM(p.FNAME))) AS fullname, p.bidid, p.groupid, "
            query += "p.POSITION1, p.POSITION2, CONCAT(TRIM(p.CITY), CONCAT(', ', TRIM(p.STATEPROV))) AS hometown, p.VOLCODE, c.STNAME "
            query += "FROM CVREGP p "
            query += "LEFT OUTER JOIN CVSTATEP c ON c.STCODE = p.STATEPROV "
            query += "ORDER BY c.STNAME, fullname"

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

    Shared Function GetEMailAddrTH(ByVal hYear As String) As DataTable
        '---------------------------------------------------------
        'Function: GetEMailAddrTH
        'Desc. . : Loades a data table of all distinct email address for Shoot tour in 2012.
        'Parms . : History Year
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

            query = "select distinct(d.email) "

            query += "FROM CVTOURSHP p "
            query += " LEFT OUTER JOIN CVREGITMHP c ON (c.ITEMID = p.TOURID AND c.CVYEAR = p.CVYEAR) "
            query += " LEFT OUTER Join CVREGHP d ON (d.BIDID = c.BIDID AND d.CVYEAR = p.CVYEAR) "
            query += "WHERE p.CVYEAR = " + hYear + " and  p.tourid > 5"


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

        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            For ct = 0 To dt.Rows.Count - 1

                wtext = dt.Rows(ct).Item("tourDesc").ToString
                wtext = wtext.Replace("'", "''")
                dt.Rows(ct).Item("tourDesc") = wtext
                wtext = dt.Rows(ct).Item("tourTitle").ToString
                wtext = wtext.Replace("'", "''")
                dt.Rows(ct).Item("tourTitle") = wtext

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
                wtext = dt.Rows(ct).Item("registrationItemAdditional").ToString
                wtext = wtext.Replace("'", "''")
                dt.Rows(ct).Item("registrationItemAdditional") = wtext

                Dim cmd As New iDB2Command("insert into  cvregitmp (bidid,groupid,itemid,itemdesc,itemdate,iteminfo,itemamount) values('@@bidid','@@groupid','@@itemid','@@itemdesc','@@itemdate','@@iteminfo','@@itemamount')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@bidid", dt.Rows(ct).Item("registrationID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@groupid", dt.Rows(ct).Item("registrationGroupCode").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@itemid", dt.Rows(ct).Item("registrationItemTourID").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@itemdesc", dt.Rows(ct).Item("registrationItemDesc").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@itemdate", dt.Rows(ct).Item("regItemDate").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@iteminfo", dt.Rows(ct).Item("registrationItemAdditional").ToString())
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

    Shared Function InsertGroupDetail(ByVal groupCode, ByVal pseq, ByVal paymentType, ByVal lastfour, ByVal exp, ByVal name, ByVal presponse, ByVal payamt) As Boolean
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

            presponse = presponse.Replace("'", "''")

            Dim cmd As New iDB2Command("insert into  cvgrpdtlp (groupid,pseq,ppaytype,plastfour,pexp,pname,presponse,ppayamt) values('@@groupid','@@pseq','@@ppaytype','@@plastfour','@@pexp','@@pname','@@presponse','@@ppayamt')", conn)
            cmd.CommandText = cmd.CommandText.Replace("@@groupid", groupCode)
            cmd.CommandText = cmd.CommandText.Replace("@@pseq", pseq)
            cmd.CommandText = cmd.CommandText.Replace("@@ppaytype", paymentType)
            cmd.CommandText = cmd.CommandText.Replace("@@plastfour", lastfour)
            cmd.CommandText = cmd.CommandText.Replace("@@pexp", exp)
            cmd.CommandText = cmd.CommandText.Replace("@@pname", name)
            cmd.CommandText = cmd.CommandText.Replace("@@presponse", presponse)
            cmd.CommandText = cmd.CommandText.Replace("@@ppayamt", payamt)

            'Execute the record insert
            cmd.ExecuteNonQuery()

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

                dt.Rows(ct).Item("ccResponse") = dt.Rows(ct).Item("ccResponse").Replace("'", "''")

                Dim cmd As New iDB2Command("insert into  cvgrppayp (groupid,paytype,cclastfour,ccexp,ccname,ccresponse,payamount,grpamount,paydatetim, paystatus, source) values('@@groupid','@@paytype','@@cclastfour','@@ccexp','@@ccname','@@ccresponse','@@payamount','@@grpamount','@@paydatetime','@@paystatus','@@source')", conn)
                cmd.CommandText = cmd.CommandText.Replace("@@groupid", dt.Rows(ct).Item("groupCode").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@paytype", dt.Rows(ct).Item("paymentType").ToString().Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@cclastfour", dt.Rows(ct).Item("ccLastFour").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@ccexp", dt.Rows(ct).Item("ccExp").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@ccname", dt.Rows(ct).Item("ccName").ToString().Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@ccresponse", dt.Rows(ct).Item("ccResponse").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@payamount", dt.Rows(ct).Item("totalCharged").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@grpamount", dt.Rows(ct).Item("totalCharged").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@paydatetime", dt.Rows(ct).Item("ccDateTime").ToString())
                cmd.CommandText = cmd.CommandText.Replace("@@paystatus", dt.Rows(ct).Item("paystatus").ToString().Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@source", dt.Rows(ct).Item("source").ToString().Trim())

                'Execute the record insert
                cmd.ExecuteNonQuery()

                Dim cmdD As New iDB2Command("insert into cvgrpdtlp (groupid,pseq,ppaytype,plastfour,pexp,pname,presponse,ppayamt) values('@@groupid','@@pseq','@@ppaytype','@@plastfour','@@pexp','@@pname','@@presponse','@@ppayamt')", conn)
                cmdD.CommandText = cmdD.CommandText.Replace("@@groupid", dt.Rows(ct).Item("groupCode").ToString())
                cmdD.CommandText = cmdD.CommandText.Replace("@@pseq", 1)
                cmdD.CommandText = cmdD.CommandText.Replace("@@ppaytype", dt.Rows(ct).Item("paymentType").ToString().Trim())
                cmdD.CommandText = cmdD.CommandText.Replace("@@plastfour", dt.Rows(ct).Item("ccLastFour").ToString())
                cmdD.CommandText = cmdD.CommandText.Replace("@@pexp", dt.Rows(ct).Item("ccExp").ToString())
                cmdD.CommandText = cmdD.CommandText.Replace("@@pname", dt.Rows(ct).Item("ccName").ToString().Trim())
                cmdD.CommandText = cmdD.CommandText.Replace("@@presponse", dt.Rows(ct).Item("ccResponse").ToString())
                cmdD.CommandText = cmdD.CommandText.Replace("@@ppayamt", dt.Rows(ct).Item("totalCharged").ToString())

                'Execute the record insert
                cmdD.ExecuteNonQuery()
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
        Dim headhouse As String = String.Empty
        Dim convwork As String = String.Empty
        Dim banquetonly As String = String.Empty
        Dim nobanquet As String = String.Empty
        Dim country As String = String.Empty
        Dim zip As String = String.Empty
        Dim fzip As String = String.Empty
        Dim GroupNum As Int64 = 0
        Dim SavGroupNum As Int64 = 0
        Dim prefmaddr As String = String.Empty
        Dim wtext As String
        Dim activity1 As String = String.Empty
        Dim activity2 As String = String.Empty
        Dim activity3 As String = String.Empty
        Dim activity4 As String = String.Empty
        Dim activity5 As String = String.Empty

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

                'If BidNum = 58 Or BidNum = 59 Or BidNum = 182 Or BidNum = 183 Then

                If GroupNum <> SavGroupNum Then
                    SavGroupNum = GroupNum
                    headhouse = "1"
                Else
                    headhouse = "0"
                End If
                If row("firstConvention") Then
                    firstconv = "1"
                Else
                    firstconv = "0"
                End If
                banquetonly = "0"
                nobanquet = "0"
                If row("countryID") = "1" Then
                    country = "USA"
                Else
                    If row("countryID") = "2" Then
                        country = "Canada"
                    Else
                        country = "USA"
                    End If
                End If
                If row("mailingAddress") Then
                    prefmaddr = "Business"
                Else
                    prefmaddr = "Home"
                End If
                If row("attending") = "3" Then
                    banquetonly = "1"
                    nobanquet = "0"
                Else
                    banquetonly = "0"
                    If row("attending") = "6" Then
                        nobanquet = "1"
                    Else
                        nobanquet = "0"
                    End If
                End If
                'If row("convWorker") Then
                '    convwork = "1"
                'Else
                convwork = "0"
                'End If
                'If row("familyReunion") Then
                '    activity1 = "1"
                'Else
                '    activity1 = "0"
                'End If
                If row("activityPlaceholder1") Then
                    activity1 = "1"
                Else
                    activity1 = "0"
                End If
                If row("activityPlaceholder2") Then
                    activity2 = "1"
                Else
                    activity2 = "0"
                End If
                If row("activityPlaceholder3") Then
                    activity3 = "1"
                Else
                    activity3 = "0"
                End If
                If row("activityPlaceholder4") Then
                    activity4 = "1"
                Else
                    activity4 = "0"
                End If
                If row("activityPlaceholder5") Then
                    activity5 = "1"
                Else
                    activity5 = "0"
                End If

                Dim statecode As String = GetStateName(row("stateProvinceID"))
                Dim pos1text As String = row("position0")
                Dim pos2text As String = row("position1")

                If pos1text.Length > 50 Then
                    pos1text = row("position0").ToString.Substring(0, 50).Trim
                End If
                If pos2text.Length > 50 Then
                    pos2text = row("position1").ToString.Substring(0, 50).Trim
                End If

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
                wtext = wtext.Replace(",", " ")
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
                If IsDate(row("dob")) Then
                    Dim txtDate As String
                    txtDate = row("dob").ToString
                    Dim NDate As Date = CDate(txtDate)
                    row("dob") = NDate.ToString("yyyyMMdd")
                Else
                    row("dob") = " "
                End If

                'If BidNum = 571 Or BidNum = 572 Then
                '    row("address1") = "5608 Spring Creek Drive"
                'End If
                'If BidNum = 22 Or BidNum = 23 Then
                '    row("homePhone") = "510-557-0587"
                'End If
                If BidNum = 672 Then
                    'row("homePhone") = "219-730-5332"
                    row("businessPhone") = "910-892-1700"
                End If
                'If BidNum = 805 Then
                '    row("homePhone") = "528180533791"
                '    row("businessPhone") = "528183569331"
                'End If
                'If BidNum = 251 Then
                '    row("firstName") = "Garrett"
                'End If
                'If BidNum = 646 Then
                '    row("homePhone") = "            "
                'End If
                'If BidNum = 397 Then
                '    row("position0") = "Amli At 7 Bridges, Attn: Jim Lelko #311"
                'End If

                query = "insert into cvregp(bidid, Groupid, fname, lname, NICKNAME, email, age,"
                query += " FirstConv, position1, position2, preferAddr, country, ADDR1, ADDR2, CITY, stateprov, postcode,"
                query += " hphone, bphone, nametag, banquet, nobanqu, convlbl, convltr, banqreg, banqtck, verified,"
                query += " newpledge, pledgepd, envelope, noshow, birthdate, headhouse, convwork, earlybrd, activity1, activity2, "
                query += " activity3, activity4, activity5)"
                query += " values(" & BidNum & ", " & GroupNum & ", '" & row("firstName").ToString & "', '"
                query += row("lastName").ToString & "', '" & row("nickname").ToString & " ', '" & row("email").ToString.Trim & " ', "
                query += row("age") & ", '" & firstconv & "', '" & pos1text & "', '" & pos2text & " ', '"
                query += prefmaddr & "', '" & country & "', '" & row("address1").ToString & "', '" & row("address2").ToString & " ', '"
                query += row("city").ToString & "', '" & statecode.Trim & "', '" & row("zip").Trim & "', '"
                query += row("homePhone").ToString.Trim & "', '" & row("businessPhone").ToString.Trim & "', '0', '"
                query += banquetonly & "', '" & nobanquet & "','0','0','0','0','0','0','0','0','0','" & row("dob") & "','"
                query += headhouse & "','" & convwork & "','0','" & activity1 & "','" & activity2 & "','"
                query += activity3 & "','" & activity4 & "','" & activity5 & "')"

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

                'End If

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function InsertRegistrant(ByVal regtype As String, ByVal dt As DataTable, ByVal dta As DataTable, ByVal dtg As DataTable, ByVal dtgd As DataTable) As Boolean
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
        Dim headhouse As String = String.Empty
        Dim activity1 As String = String.Empty
        Dim activity2 As String = String.Empty
        Dim activity3 As String = String.Empty
        Dim activity4 As String = String.Empty
        Dim activity5 As String = String.Empty
        Dim convwork As String = String.Empty
        Dim earlybrd As String = String.Empty
        Dim banquetonly As String = String.Empty
        Dim nobanquet As String = String.Empty
        Dim GroupNum As Int64 = 0
        Dim wtext As String = String.Empty
        Dim CnaID As Int64 = 0

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

                'wtext = rowg("cardname").ToString().Trim()
                'wtext = wtext.Replace("'", "''")
                'rowg("cardname") = wtext.Trim()

                query = "insert into cvgrppayp(groupid, payamount, grpamount, paydatetim, paystatus, source)"
                query += " values(" & GroupNum & ", "
                query += rowg("amountp") & ", " & rowg("amountt") & ", '"
                query += today & "', '" & rowg("paystatus").ToString & "', '" & rowg("source") & "')"

                'query = "insert into cvgrppayp(groupid, paytype, cclastfour, ccexp, ccname, ccresponse, payamount, paydatetim, paystatus, source)"
                'query += " values(" & GroupNum & ", '" & rowg("cardtype").ToString & "', '"
                'query += rowg("cardnum").ToString & "', '" & rowg("cardexp").ToString & " ', '" & rowg("cardname").ToString.Trim & " ', '"
                'query += rowg("cardauth").ToString().Trim() & "', " & rowg("amount") & ", '"
                'query += today & "', '" & rowg("paystatus").ToString & "', '" & rowg("source") & "')"

                Command = New iDB2Command(query, conn)
                If Not conn.State = ConnectionState.Open Then
                    conn.Open()
                End If

                rc = Command.ExecuteNonQuery

                For Each rowgd As DataRow In dtgd.Rows

                    wtext = rowgd("cardname").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    rowgd("cardname") = wtext.Trim()

                    wtext = rowgd("cardauth").ToString().Trim()
                    wtext = wtext.Replace("'", "''")
                    rowgd("cardauth") = wtext.Trim()

                    query = "insert into cvgrpdtlp(groupid, pseq, ppaytype, plastfour, pexp, pname, presponse, ppayamt)"
                    query += " values(" & GroupNum & ", " & rowgd("dtlseq") & ", '" & rowgd("cardtype").ToString & "', '"
                    query += rowgd("cardnum").ToString & "', '" & rowgd("cardexp").ToString & " ', '" & rowgd("cardname").ToString.Trim & " ', '"
                    query += rowgd("cardauth").ToString().Trim() & "', " & rowgd("amount") & ")"

                    Command = New iDB2Command(query, conn)
                    If Not conn.State = ConnectionState.Open Then
                        conn.Open()
                    End If

                    rc = Command.ExecuteNonQuery

                Next

                For Each row As DataRow In dt.Rows

                    BidNum += 1
                    If row("firstconv") Then
                        firstconv = "1"
                    Else
                        firstconv = "0"
                    End If
                    If row("headhouse") Then
                        headhouse = "1"
                    Else
                        headhouse = "0"
                    End If
                    If row("convwork") Then
                        convwork = "1"
                    Else
                        convwork = "0"
                    End If
                    If row("earlybrd") Then
                        earlybrd = "1"
                    Else
                        earlybrd = "0"
                    End If
                    If row("activity1") Then
                        activity1 = "1"
                    Else
                        activity1 = "0"
                    End If
                    If row("activity2") Then
                        activity2 = "1"
                    Else
                        activity2 = "0"
                    End If
                    'If row("activity3") Then
                    '    activity3 = "1"
                    'Else
                    activity3 = "0"
                    'End If
                    'If row("activity4") Then
                    '    activity4 = "1"
                    'Else
                    activity4 = "0"
                    'End If
                    If row("activity5") Then
                        activity5 = "1"
                    Else
                        activity5 = "0"
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

                    CnaID = row("cnaid")

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
                    If row("birthdate") = "" Or row("birthdate") = " " Then
                        row("birthdate") = " "
                    End If


                    query = "insert into cvregp(bidid, Groupid, cna_id, fname, lname, NICKNAME, email, age,"
                    query += " FirstConv, position1, position2, preferAddr, country, ADDR1, ADDR2, CITY, stateprov, postcode,"
                    query += " hphone, bphone, dietrest, banquet, volcode, nobanqu, nametag, convlbl, convltr, banqreg, banqtck, "
                    query += "verified, newpledge, pledgepd, envelope, noshow, birthdate, headhouse, convwork, earlybrd, activity1, "
                    query += "activity2, activity3, activity4, activity5)"
                    query += " values(" & BidNum & ", " & GroupNum & ", " & CnaID & ", '" & row("firstName").ToString & "', '"
                    query += row("lastName").ToString & "', '" & row("nickname").ToString & " ', '" & row("email").ToString().Trim() & " ', "
                    query += row("age") & ", '" & firstconv & "', '" & row("title1") & "', '" & row("title2") & " ', '"
                    query += row("perfmaddr") & "', '" & row("country") & "', '" & row("street1").ToString & "', '" & row("street2").ToString & " ', '"
                    query += row("city").ToString & "', '" & row("state").ToString & "', '" & row("zipcode").Trim & "', '"
                    query += row("phone").ToString.Trim & "', '" & row("busphone").ToString.Trim & "', '"
                    query += row("dietrest").ToString.Trim & "', '" & banquetonly & "', '"
                    query += row("volcode").ToString.Trim & "', '" & nobanquet & "','0','0','0','0','0','0','0','0','0','0','"
                    query += row("birthdate") & "','" & headhouse & "','" & convwork & "','" & earlybrd & "','" & activity1 & "','"
                    query += activity2 & "','" & activity3 & "','" & activity4 & "','" & activity5 & "')"



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
                    If row("leadshp4").ToString() <> "  " And row("leadshp4").ToString() <> "" Then
                        query = "insert into cvregldp(bidid, groupid, ldrshipid) "
                        query += "values(" & BidNum & ", " & GroupNum & ", "
                        query += row("leadshp4") & ")"

                        Command = New iDB2Command(query, conn)
                        rc = Command.ExecuteNonQuery
                    End If

                    If row("leadshp5").ToString() <> "  " And row("leadshp5").ToString() <> "" Then
                        query = "insert into cvregldp(bidid, groupid, ldrshipid) "
                        query += "values(" & BidNum & ", " & GroupNum & ", "
                        query += row("leadshp5") & ")"

                        Command = New iDB2Command(query, conn)
                        rc = Command.ExecuteNonQuery
                    End If

                    If row("leadshp6").ToString() <> "  " And row("leadshp6").ToString() <> "" Then
                        query = "insert into cvregldp(bidid, groupid, ldrshipid) "
                        query += "values(" & BidNum & ", " & GroupNum & ", "
                        query += row("leadshp6") & ")"

                        Command = New iDB2Command(query, conn)
                        rc = Command.ExecuteNonQuery
                    End If

                    'Dim rowa As DataRow() = dta.[Select]("recnumber =" & row("recnumber"))
                    For Each rowa As DataRow In dta.Rows
                        If rowa("recnumber") = row("recnumber") Then

                            wtext = rowa("activity").ToString().Trim()
                            wtext = wtext.Replace("'", "''")
                            rowa("activity") = wtext

                            wtext = rowa("tourteam").ToString().Trim()
                            wtext = wtext.Replace("'", "''")
                            rowa("tourteam") = wtext.Trim()

                            query = "insert into cvregitmp(bidid, groupid, itemid, itemdesc, itemdate, iteminfo, itemamount) "
                            query += "values(" & BidNum & ", " & GroupNum & ", "
                            query += rowa("actnumber") & ", '" & rowa("activity").ToString.Trim & "', '"
                            query += rowa("actdate") & "', '" & rowa("tourteam").ToString.Trim & "', " & rowa("amount") & ")"

                            Command = New iDB2Command(query, conn)
                            rc = Command.ExecuteNonQuery
                        End If
                    Next

                    query = "SELECT SUM(itemamount) as RegAmount FROM cvregitmp WHERE groupid=" & GroupNum

                    Dim da As New iDB2DataAdapter(query, conn)
                    Dim dtAmount = New DataTable
                    dtAmount.Clear()
                    da.Fill(dtAmount)

                    query = "update cvgrppayp set "
                    query += "grpamount=" & dtAmount(0).Item("RegAmount") & " where groupid =" & GroupNum

                    Command = New iDB2Command(query, conn)
                    rc = Command.ExecuteNonQuery


                Next

            Next

            Return rc

        Catch ex As Exception

            mvLastError = ex.Message

            Dim smtpMail As New MailMessage()
            Dim smtp As New SmtpClient
            Dim bodyText As String = String.Empty
            Dim subjectLine As String = String.Empty

            'Build eMail message for Convention Registration
            Dim toeMail As String = "jjackson@ducks.org"
            Dim fromeMail As String = "as400@ducks.org"

            subjectLine = "Error writing new registrant"

            'Build body for eMail.
            bodyText = "<b><font style=""font-size:20pt;"">Confirmation #" & GroupNum & "</font></b><br /><br />"
            bodyText += "Error writing to AS400. Following is the SQL error issued.<br /><br />" & mvLastError & "<br />"
            bodyText += "Good luck."
            
            smtpMail = New MailMessage(fromeMail, toeMail, subjectLine, bodyText)
            smtpMail.Bcc.Add("sjones@ducks.org")


            'send email to RD and Cor.
            'lctrace = 300
            smtpMail.IsBodyHtml = True
            smtpMail.Priority = MailPriority.High
            smtp.Send(smtpMail)

            Return False
        End Try

    End Function

    Shared Function InsertRegCheck(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertRegCheck
        'Desc. . : Loades all the registrants into a check file.
        'Parms . : Table of registrants
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim BidNum As Integer = 0
        Dim GroupNum As Int64 = 0
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

                If BidNum = 251 Then
                    row("firstName") = "Garrett"
                End If

                wtext = row("firstName").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("firstName") = wtext
                wtext = row("lastName").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                wtext = wtext.Replace(",", " ")
                row("lastName") = wtext
                wtext = row("nickname").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("nickname") = wtext
                wtext = row("address1").ToString().Trim()
                wtext = wtext.Replace("'", "''")

                query = "insert into cvregckp(bidid, Groupid, fname, lname, NICKNAME, email)"
                query += " values(" & BidNum & ", " & GroupNum & ", '" & row("firstName").ToString & "', '"
                query += row("lastName").ToString & "', '" & row("nickname").ToString & " ', '" & row("email").ToString.Trim & " ')"

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadConvMenu(ByVal cvyear As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadConvMenu
        'Desc. . : Loades a data table of Convention menus.
        'Parms . : Convention Year
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

            query = "select * from cvmenup where (cvyear=" + cvyear + " and cvactive='Y') order by cvyear, cvid"

            Dim da As New iDB2DataAdapter(query, conn)

            dt.Columns.Add("cvyear", Type.GetType("System.String"))
            Dim workCol2 As DataColumn = dt.Columns.Add("cvid", Type.GetType("System.Int32"))
            Dim workCol As DataColumn = dt.Columns.Add("cvpid", Type.GetType("System.Int32"))
            workCol.AllowDBNull = True
            dt.Columns("cvpid").AllowDBNull = True
            dt.Columns.Add("cvdesc", Type.GetType("System.String"))
            dt.Columns.Add("cvurl", Type.GetType("System.String"))
            dt.Columns.Add("cvimgurl", Type.GetType("System.String"))

            da.Fill(dt)

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                If dr("cvpid") = 0 Then
                    dr("cvpid") = DBNull.Value
                End If
                Dim cvurl As String = dr("cvurl").ToString().Trim()
                If cvurl = "" Then
                    dr("cvurl") = Nothing
                Else
                    dr("cvurl") = cvurl.Trim()
                End If
                Dim cvimgurl As String = dr("cvimgurl").ToString().Trim()
                If cvimgurl = "" Then
                    dr("cvimgurl") = Nothing
                Else
                    dr("cvimgurl") = cvimgurl.Trim()
                End If
                Dim cvdesc As String = dr("cvdesc").ToString().Trim()
                If cvdesc = "" Then
                    dr("cvdesc") = Nothing
                Else
                    dr("cvdesc") = cvdesc.Trim()
                End If
            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing

        End Try

    End Function

    Shared Function LoadConvMenu() As DataTable
        '---------------------------------------------------------
        'Function: LoadConvMenu
        'Desc. . : Loades a data table of Convention menus.
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

            query = "select * from cvhistp order by cvmenuid"

            Dim da As New iDB2DataAdapter(query, conn)

            dt.Columns.Add("cvyear", Type.GetType("System.String"))
            Dim workCol2 As DataColumn = dt.Columns.Add("cvmenuid", Type.GetType("System.Int32"))
            Dim workCol As DataColumn = dt.Columns.Add("cvmenupid", Type.GetType("System.Int32"))
            workCol.AllowDBNull = True
            dt.Columns("cvmenupid").AllowDBNull = True
            dt.Columns.Add("convname", Type.GetType("System.String"))
            dt.Columns.Add("cvurl", Type.GetType("System.String"))

            'dt.Clear()
            da.Fill(dt)

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows.Item(i)
                If dr("cvmenupid") = 0 Then
                    dr("cvmenupid") = DBNull.Value
                End If
                Dim cvurl As String = dr("cvurl").ToString().Trim()
                If cvurl = "" Then
                    dr("cvurl") = Nothing
                Else
                    dr("cvurl") = cvurl.Trim()
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
                Dim dt2 As DataTable = LoadConvMenu()
                count = 0
                Return dt2
            End If

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
            For i As Integer = 0 To dt.Rows.Count + 4
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
                    End If
                    If ckType = "5" Then
                        ndr.Item(0) = "  "
                        ndr.Item(1) = "---- Iceland ----"
                        ndr.Item(2) = "9"
                        dt.Rows.InsertAt(ndr, i)
                    End If
                    If ckType = "6" Then
                        ndr.Item(0) = "  "
                        ndr.Item(1) = "---- Italy ----"
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

            query = "select p.groupid, p.paytype, p.cclastfour, p.ccexp, p.ccname, p.ccresponse, "
            query += "p.payamount, p.paystatus, p.source, SUM(c.itemamount) as RegAmount "
            query += "from cvgrppayp p "
            query += "left outer join cvregitmp c on (c.groupid = p.groupid) "
            query += "where p.groupid =" & groupnum & " "
            query += "group by p.groupid, p.paytype, p.cclastfour, p.ccexp, p.ccname, p.ccresponse, p.payamount, p.paystatus, p.source "
            query += "order by p.groupid"

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

    Shared Function LoadGrpPayRecords(ByVal groupnum As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadGrpPayRecords
        'Desc. . : Loades a data table of convention registration payment records.
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

            query = "select * "
            query += "from cvgrpdtlp "
            query += "where groupid =" & groupnum & " "
            query += "order by groupid, pseq"

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

    Shared Function LoadToursDropDown(ByVal hyear As String, ByVal tourdate As String) As DataTable
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
            query = "select p.cvyear, p.tourdate, p.tourid, p.tourtime,"
            query += " c.ttitle, c.tlength, c.tcosta"
            query += " from cvdatehp p"
            query += " left outer join  cvtourshp c on (c.tourid = p.tourid)"
            query += " where (p.tourdate='" & tourdate & "' and p.cvyear='" & hyear & "') order by p.tourdate, p.tourid"

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


    Shared Function MoveTable(ByVal newtblnum As String, ByVal tblnum As String, ByVal tblname As String, ByVal tbloff As String, ByVal tblseats As String, ByVal tblnote As String) As Boolean
        '---------------------------------------------------------
        'Function: MoveTable
        'Desc. . : Move occupants from one table to a new table
        'Parms . : Table fields
        'Returns : Returns a boolean object.
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

            wtext = tblnote.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            tblnote = wtext.Trim

            query = "update cvtablep set "
            query += "tablenum = " & newtblnum & " , "
            query += "tabledesc = '" & tblname.Trim & "', "
            query += "tableseats = " & tblseats.Trim & ", "
            query += "offtable = '" & tbloff.Trim & "', "
            query += "stickynote = '" & tblnote.Trim & "' "
            query += "where tablenum=" & newtblnum

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            query = "update cvregp set "
            query += "banqtbl=" & newtblnum
            query += " where banqtbl=" & tblnum

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

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
                'query += "paytype='" & row("cardtype").ToString.Trim & "', "
                'query += "cclastfour='" & row("cardnum").ToString.Trim & "', "
                'query += "ccexp='" & row("cardexp").ToString & "', "
                'query += "ccname='" & row("cardname").ToString & "', "
                'query += "ccresponse='" & row("cardauth").ToString & "', "
                query += "payamount=" & row("amountp").ToString & ", "
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

    Shared Function UpdateGrpDetail(ByVal ID As String, ByVal pseq As String, ByVal paytype As String, ByVal lastfour As String, ByVal exp As String, ByVal name As String, ByVal presponse As String, ByVal payamt As String) As Boolean
        '---------------------------------------------------------
        'Function: UpdateGrpDetail
        'Desc. . : Update Group Payment Detail record.
        'Parms . : 1.) GroupID
        '          2.) Sequence number
        '          3.) Payment Type
        '          4.) Credit Card Last 4 numbers
        '          5.) Expire Date month/year
        '          6.) Name
        '          7.) Response code
        '          8.) Payment amount
        '
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            presponse = presponse.Replace("'", "''")

            query = "update cvgrpdtlp set "
            query += "ppaytype='" & paytype.ToString.Trim & "', "
            query += "plastfour='" & lastfour.ToString.Trim & "', "
            query += "pexp='" & exp.ToString.ToString.Trim & "',"
            query += "pname='" & name.ToString.Trim & "', "
            query += "presponse='" & presponse.ToString.Trim & "', "
            query += "ppayamt=" & payamt.ToString.Trim & " "
            query += "where groupid=" & ID & " and pseq=" & pseq

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

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
        Dim headhouse As String = String.Empty
        Dim convwork As String = String.Empty
        Dim earlybrd As String = String.Empty
        Dim activity1 As String = String.Empty
        Dim activity2 As String = String.Empty
        Dim activity3 As String = String.Empty
        Dim activity4 As String = String.Empty
        Dim activity5 As String = String.Empty
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
                If row("headhouse") Then
                    headhouse = "1"
                Else
                    headhouse = "0"
                End If
                If row("convwork") Then
                    convwork = "1"
                Else
                    convwork = "0"
                End If
                If row("earlybrd") Then
                    earlybrd = "1"
                Else
                    earlybrd = "0"
                End If
                If row("activity1") Then
                    activity1 = "1"
                Else
                    activity1 = "0"
                End If
                If row("activity2") Then
                    activity2 = "1"
                Else
                    activity2 = "0"
                End If
                If row("activity3") Then
                    activity3 = "1"
                Else
                    activity3 = "0"
                End If
                If row("activity4") Then
                    activity4 = "1"
                Else
                    activity4 = "0"
                End If
                If row("activity5") Then
                    activity5 = "1"
                Else
                    activity5 = "0"
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
                query += "pledgepd='" & pledgepd & "', "
                query += "birthdate='" & row("birthdate") & "', "
                query += "headhouse='" & headhouse & "', "
                query += "convwork='" & convwork & "', "
                query += "earlybrd='" & earlybrd & "', "
                query += "activity1='" & activity1 & "', "
                query += "activity2='" & activity2 & "', "
                query += "activity3='" & activity3 & "', "
                query += "activity4='" & activity4 & "', "
                query += "activity5='" & activity5 & "' "
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

                        wtext = rowa("iteminfo").ToString().Trim()
                        wtext = wtext.Replace("'", "''")
                        rowa("iteminfo") = wtext.Trim()

                        query = "insert into cvregitmp(bidid, groupid, itemid, itemdesc, itemdate, iteminfo, itemamount) "
                        query += "values(" & rowa("bidid") & ", " & rowa("groupid") & ", "
                        query += rowa("itemid") & ", '" & rowa("itemdesc").ToString.Trim & "', '"
                        query += rowa("itemdate") & "', '" & rowa("iteminfo").ToString.Trim & "', " & rowa("itemamount") & ")"

                        Command = New iDB2Command(query, conn)
                        rc = Command.ExecuteNonQuery

                    Next

                    query = "SELECT SUM(itemamount) as RegAmount FROM cvregitmp WHERE groupid=" & row("groupid")

                    Dim da As New iDB2DataAdapter(query, conn)
                    Dim dtAmount = New DataTable
                    dtAmount.Clear()
                    da.Fill(dtAmount)

                    query = "update cvgrppayp set "
                    query += "grpamount=" & dtAmount(0).Item("RegAmount") & " where groupid =" & row("groupid")

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
        'Function: UpdateTable
        'Desc. . : Update record in CVTABLEP file using SQL
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

    Shared Function UpdateTable(ByVal bidid As String, ByVal tablenum As String) As String
        '---------------------------------------------------------
        'Function: UpdateTable
        'Desc. . : Update record in CVREGP file using SQL
        'Parms . : 1.) Group Seating Data Table
        '
        '---------------------------------------------------------

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim dt As New DataTable
        Dim orgTblNum As String = "0"

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select banqtbl from cvregp "
            query += "where bidid = " & bidid & ""

            Dim da As New iDB2DataAdapter(query, conn)
            dt.Clear()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    orgTblNum = row("banqtbl").ToString.Trim
                Next
            Else
                orgTblNum = "0"
            End If

            query = "update cvregp set "
            query += "banqtbl=" & tablenum
            query += " where bidid=" & bidid

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return orgTblNum

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function UpdateTblMast(ByVal tblnew As Boolean, ByVal tblnum As String, ByVal tblname As String, ByVal tbloff As String, ByVal tblseats As String, ByVal tblnote As String) As Boolean
        '---------------------------------------------------------
        'Function: UpdateTblMast
        'Desc. . : Updates/Inserts the CVTABLEP with changes
        'Parms . : Table fields
        'Returns : Returns a boolean object.
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

            wtext = tblnote.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            tblnote = wtext.Trim

            If tblnew Then
                query = "insert into cvtablep(tablenum, tabledesc, tableseats, offtable, stickynote) "
                query += "values(" & tblnum & ", '"
                query += tblname.Trim & "', "
                query += tblseats.Trim & ", '"
                query += tbloff.Trim & "', '"
                query += tblnote.Trim & "')"
            Else
                query = "update cvtablep set "
                query += "tabledesc = '" & tblname.Trim & "', "
                query += "tableseats = " & tblseats.Trim & ", "
                query += "offtable = '" & tbloff.Trim & "', "
                query += "stickynote = '" & tblnote.Trim & "' "
                query += "where tablenum=" & tblnum
            End If

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateTblSetup(ByVal tblnum As String, ByVal tblname As String, ByVal tblseats As String, ByVal tblnon As String, ByVal tbloff As String) As Boolean
        '---------------------------------------------------------
        'Function: UpdateTblMast
        'Desc. . : Updates/Inserts the CVTABLEP with changes
        'Parms . : Table fields
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


            query = "update cvtblsetp set "
            query += "numtblfl = " & tblnum.Trim & ", "
            query += "newtblnm = '" & tblname.Trim & "', "
            query += "dflseats = " & tblseats.Trim & ", "
            query += "nonasgtbl = " & tblnon.Trim & ", "
            query += "lstofftbl = " & tbloff.Trim

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
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

    Shared Function UpdateEnvelopeFlag() As String
        '---------------------------------------------------------
        'Function: UpdateEnvelopeFlag
        'Desc. . : Updates the Envelope printed flag on CVREGP
        'Parms . : None
        'Returns : Number records updated.
        '---------------------------------------------------------

        Dim oCmd As New iDB2Command
        Dim query As String = String.Empty

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If


            oCmd.Connection = conn

            oCmd.Parameters.Add("numupd", iDB2DbType.iDB2Char, 4)
            oCmd.Parameters("numupd").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("numupd").Value = " "

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CONVR015"

            oCmd.ExecuteNonQuery()

            Return oCmd.Parameters("numupd").Value

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
