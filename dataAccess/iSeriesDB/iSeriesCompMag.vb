Imports IBM.Data.DB2.iSeries
Imports System.Configuration

Public Class iSeriesCompMag

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("iSeries"))
    Shared mvLastError As String = ""

    Shared Function GetMagName(ByVal sFName As String, ByVal sTName As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Complimentary Magazine Records 
        'Desc. . : Get records as a DataReader.
        'Parms . : 1.) From name
        '          2.) To name 
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = " Select *"
            query += " from  magcomp"
            query += " where (cpsort >= @from and cpsort <= @to)"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@from", iDB2DbType.iDB2Char, 20)
            cmdD.Parameters("@from").Value = sFName
            cmdD.Parameters.Add("@to", iDB2DbType.iDB2Char, 20)
            cmdD.Parameters("@to").Value = sTName

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetMagCity(ByVal sCity As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Complimentary Magazine Records 
        'Desc. . : Get records as a DataReader.
        'Parms . : 1.) City
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = " Select *"
            query += " from  magcomp"
            query += " where (cpcity = @city)"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@city", iDB2DbType.iDB2Char, 21)
            cmdD.Parameters("@city").Value = sCity

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetMagState(ByVal sState As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Complimentary Magazine Records 
        'Desc. . : Get records as a DataReader.
        'Parms . : 1.) State
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = " Select *"
            query += " from  magcomp"
            query += " where (cpstat = @state)"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@state", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@state").Value = sState

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetMagZip(ByVal sZip As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Complimentary Magazine Records 
        'Desc. . : Get records as a DataReader.
        'Parms . : 1.) Zip
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = " Select *"
            query += " from  magcomp"
            query += " where (cpZip = @zip)"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@zip", iDB2DbType.iDB2Char, 5)
            cmdD.Parameters("@zip").Value = sZip

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetMagSequence(ByVal sSequence As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Complimentary Magazine Records 
        'Desc. . : Get records as a DataReader.
        'Parms . : 1.) Sequence number
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = " Select *"
            query += " from  magcomp"
            query += " where (cpseq = @sequence)"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@sequence", iDB2DbType.iDB2Char, 4)
            cmdD.Parameters("@sequence").Value = sSequence

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetMagDept(ByVal sFDept As String, ByVal sTDept As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Complimentary Magazine Records 
        'Desc. . : Get records as a DataReader.
        'Parms . : 1.) From dept
        '          2.) To dept 
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = " Select *"
            query += " from  magcomp"
            query += " where (cpdept >= @from and cpdept <= @to)"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@from", iDB2DbType.iDB2Char, 25)
            cmdD.Parameters("@from").Value = sFDept
            cmdD.Parameters.Add("@to", iDB2DbType.iDB2Char, 25)
            cmdD.Parameters("@to").Value = sTDept

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetMaxMagSeq() As String
        '---------------------------------------------------------
        'Function: GetMaxMagSeq
        'Desc. . : Get highest sequence number from magcomp file
        'Parms . :       
        'Returns : Highest sequence number
        '---------------------------------------------------------

        Dim SeqNum As String = String.Empty
        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "select max(cpseq)as maxseq from magcomp"

            Dim cmdD As New iDB2Command(query, conn)
            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)
            dt.Clear()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim dr1 As DataRow = dt.Rows(0)
                SeqNum = dr1("maxseq")
            End If
            Return SeqNum

        Catch ex As Exception

            'Set class level error message as well
            mvLastError = ex.Message
            Return Nothing

        End Try

    End Function

    Shared Function InsertMagComp(ByVal sLine1 As String, ByVal sLine2 As String, ByVal sLine3 As String, ByVal sLine4 As String, ByVal sLine5 As String, ByVal sDCity As String, ByVal sDState As String, ByVal sDZip As String, ByVal sFCity As String, ByVal sCode As String, ByVal sSeq As String, ByVal sDate As String, ByVal sDept As String, ByVal sSort As String) As Boolean

        '---------------------------------------------------------
        'Desc. . : Insert detail record in magcomp file using SQL
        'Parms . : 1.) Line 1 
        '          2.) Line 2
        '          3.) Line 3
        '          4.) Line 4
        '          5.) Line 5
        '          6.) Domestic city
        '          7.) Domestic state
        '          8.) Domestic zip
        '          9.) Foreign ciyt/st/zip
        '         10.) Domestic/foreign code
        '         11.) Sequence number
        '         12.) Effective date
        '         13.) Department
        '         14.) Name sort
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            Dim cmd As New iDB2Command("insert into magcomp (cplin1,cplin2,cplin3,cplin4,cplin5,cpcity,cpstat,cpzip,cpcnty,cpcode,cpgrp,cpseq,cpdate,cpdept,cpsort) values('@@cplin1','@@cplin2','@@cplin3', '@@cplin4#', '@@cplin5', '@@cpcity', '@@cpstat', '@@cpzip', '@@cpcnty', '@@cpcode', '@@cpgrp', '@@cpseq', '@@cpdate', '@@cpdept', '@@cpsort')", conn)
            cmd.CommandText = cmd.CommandText.Replace("@@cplin1", sLine1.Replace("'", "''"))
            cmd.CommandText = cmd.CommandText.Replace("@@cplin2", sLine2.Replace("'", "''"))
            cmd.CommandText = cmd.CommandText.Replace("@@cplin3", sLine3.Replace("'", "''"))
            cmd.CommandText = cmd.CommandText.Replace("@@cplin4", sLine4.Replace("'", "''"))
            cmd.CommandText = cmd.CommandText.Replace("@@cplin5", sLine5.Replace("'", "''"))
            cmd.CommandText = cmd.CommandText.Replace("@@cpcity", sDCity.Replace("'", "''"))
            cmd.CommandText = cmd.CommandText.Replace("@@cpstat", sDState)
            cmd.CommandText = cmd.CommandText.Replace("@@cpzip", sDZip)
            cmd.CommandText = cmd.CommandText.Replace("@@cpcnty", sFCity.Replace("'", "''"))
            cmd.CommandText = cmd.CommandText.Replace("@@cpcode", sCode)
            cmd.CommandText = cmd.CommandText.Replace("@@cpgrp", "013")
            cmd.CommandText = cmd.CommandText.Replace("@@cpseq", sSeq)
            cmd.CommandText = cmd.CommandText.Replace("@@cpdate", sDate)
            cmd.CommandText = cmd.CommandText.Replace("@@cpdept", sDept)
            cmd.CommandText = cmd.CommandText.Replace("@@cpsort", sSort.Replace("'", "''"))

            'Execute the record insert

            cmd.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function UpdatetMagComp(ByVal sLine1 As String, ByVal sLine2 As String, ByVal sLine3 As String, ByVal sLine4 As String, ByVal sLine5 As String, ByVal sDCity As String, ByVal sDState As String, ByVal sDZip As String, ByVal sFCity As String, ByVal sCode As String, ByVal sSeq As String, ByVal sDate As String, ByVal sDept As String, ByVal sSort As String) As Boolean

        '---------------------------------------------------------
        'Desc. . : Update detail record in magcomp file using SQL
        'Parms . : 1.) Line 1 
        '          2.) Line 2
        '          3.) Line 3
        '          4.) Line 4
        '          5.) Line 5
        '          6.) Domestic city
        '          7.) Domestic state
        '          8.) Domestic zip
        '          9.) Foreign ciyt/st/zip
        '         10.) Domestic/foreign code
        '         11.) Sequence number
        '         12.) Effective date
        '         13.) Department
        '         14.) Name sort
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try
            Dim cmd As New iDB2Command("update  magcomp SET " & _
            "CPLIN1='" & sLine1.Replace("'", "''") & "'," & _
            "CPLIN2='" & sLine2.Replace("'", "''") & "'," & _
            "CPLIN3='" & sLine3.Replace("'", "''") & "'," & _
            "CPLIN4='" & sLine4.Replace("'", "''") & "'," & _
            "CPLIN5='" & sLine5.Replace("'", "''") & "'," & _
            "CPCITY='" & sDCity.Replace("'", "''") & "'," & _
            "CPSTAT='" & sDState & "'," & _
            "CPZIP='" & sDZip & "'," & _
            "CPCNTY='" & sFCity.Replace("'", "''") & "'," & _
            "CPCODE='" & sCode & "'," & _
            "CPDATE='" & sDate & "'," & _
            "CPDEPT='" & sDept & "'," & _
            "CPSORT='" & sSort.Replace("'", "''") & "'" & _
            " WHERE CPSEQ=" & sSeq, conn)

            'Execute the record update
            cmd.ExecuteNonQuery()

            'Update succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function DeleteMagComp(ByVal sSeq As String) As Boolean

        '---------------------------------------------------------
        'Desc. . : Delete detail record in magcomp file using SQL
        'Parms . : 1.) Sequence number 
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try
            Dim cmd As New iDB2Command("delete from magcomp where cpseq = " & sSeq, conn)
            'Execute the record update

            cmd.ExecuteNonQuery()

            'Delete succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetMagStateRprt() As DataTable

        '---------------------------------------------------------
        'Function: Get Complimentary Magazine State Summary Report 
        'Desc. . : Get records as a DataReader.
        'Parms . : none
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = " Select cpstatm, COUNT(*)as names"
            query += " from  mgp100d"
            query += " group by cpstatm"
            query += " order by cpstatm"

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetCompMagDate() As DataTable

        '---------------------------------------------------------
        'Function: Get Complimentary Magazine Last Sent Date 
        'Desc. . : Get records as a DataReader.
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = " Select mlchgd, mlchgt"
            query += " from  magcomp2"
            query += " where (mlname = @name)"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@name", iDB2DbType.iDB2Char, 10)
            cmdD.Parameters("@name").Value = "FRYLOGMAG"

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetCompMagLog() As DataTable

        '---------------------------------------------------------
        'Function: Get Complimentary Magazine FTP Log 
        'Desc. . : Get records as a DataReader.
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = " Select *"
            query += " from  compmaglog"

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function CallCompMagCL1() As Boolean
        '---------------------------------------------------------
        'Function: CallCompMagCL1
        'Desc. . : Call program CompMagCL1
        'Parms . : 1.)
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            'Set command object and connection info 
            'for the command object to current iSeries iDB2 connection
            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "COMPMAGCL1"

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Return true if successful 
            Return True

        Catch ex As Exception

            'Set class level error message as well
            mvLastError = ex.Message

            Return False

        End Try

    End Function

End Class
