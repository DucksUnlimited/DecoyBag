Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
'Imports System.data
'Imports System.Web.HttpContext
Imports System.Configuration


Public Class iSeriesGroup1

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("G1iSeries"))
    Shared mvLastError As String = ""

    Shared Function InsertGroup1FileG1WEBUPLD(ByVal inpmbr As String, ByVal xidno As String, ByVal xab1 As String, ByVal xab2 As String, ByVal xab3 As String, ByVal xab4 As String, ByVal xab5 As String, ByVal xzip As String, ByVal xidnoa As String) As Boolean
        '---------------------------------------------------------
        'Function: InsertGroup1FileG1WEBUPLD
        'Desc. . : Insert records into "inpmbr" in GROUP1PF file
        'Parms . : 1.) Group1 File Member
        '          2.) ID number (usually blank)
        '          3.) Label Name
        '          4.) Label Address Line 1
        '          5.) Label Address Line 2
        '          6.) Label Address Line 3
        '          7.) City State Zip
        '          8.) Zip
        '          9.) Second ID (usually blank)
        'Returns : Boolean indicator
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            '  Member name for Group1
            'Set up a character parameter inpmbr as CHAR(10)
            oCmd.Parameters.Add("inpmbr", iDB2DbType.iDB2Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("inpmbr").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("inpmbr").Value = inpmbr

            'Set up a character parameter xidno as CHAR(8)
            oCmd.Parameters.Add("xidno", iDB2DbType.iDB2Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("xidno").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("xidno").Value = xidno

            'Set up a character parameter xab1 as CHAR(55)
            oCmd.Parameters.Add("xab1", iDB2DbType.iDB2Char, 55)
            'Always set parms as input/output for program call
            oCmd.Parameters("xab1").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("xab1").Value = xab1

            'Set up a character parameter xab2 as CHAR(40)
            oCmd.Parameters.Add("xab2", iDB2DbType.iDB2Char, 40)
            'Always set parms as input/output for program call
            oCmd.Parameters("xab2").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("xab2").Value = xab2

            'Set up a character parameter xab3 as CHAR(40)
            oCmd.Parameters.Add("xab3", iDB2DbType.iDB2Char, 40)
            'Always set parms as input/output for program call
            oCmd.Parameters("xab3").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("xab3").Value = xab3

            'Set up a character parameter xab4 as CHAR(40)
            oCmd.Parameters.Add("xab4", iDB2DbType.iDB2Char, 40)
            'Always set parms as input/output for program call
            oCmd.Parameters("xab4").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("xab4").Value = xab4

            'Set up a character parameter xab5 as CHAR(40)
            oCmd.Parameters.Add("xab5", iDB2DbType.iDB2Char, 40)
            'Always set parms as input/output for program call
            oCmd.Parameters("xab5").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("xab5").Value = xab5

            'Set up a character parameter xzip as CHAR(9)
            oCmd.Parameters.Add("xzip", iDB2DbType.iDB2Char, 9)
            'Always set parms as input/output for program call
            oCmd.Parameters("xzip").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("xzip").Value = xzip

            'Set up a character parameter xidnoa as CHAR(8)
            oCmd.Parameters.Add("xidnoa", iDB2DbType.iDB2Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("xidnoa").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("xidnoa").Value = xidnoa

            'Set up program call command line
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "G1CWEBUPLD"

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertGroup1Detail(ByVal inpmbr As String, ByVal jobid As String, ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: InsertGroup1Detail
        'Desc. . : Clear/Add member to GROUP1PF, Upload the data table of records to member, Run the Group1
        '           jobs to produce 1up labels
        'Parms . : 1.) Member to receive data
        '          2.) G1 Job ID to process the uploaded file
        '          3.) Data table of records to upload
        '---------------------------------------------------------

        Dim ct As Long

        'Reset last error
        mvLastError = ""

        Try

            ReSetGroup1MbrG1CWEBMBR(inpmbr)

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            For ct = 0 To dt.Rows.Count - 1

                InsertGroup1FileG1WEBUPLD(inpmbr.ToUpper, dt.Rows(ct).Item("ID").ToString(), dt.Rows(ct).Item("Name").ToString(), dt.Rows(ct).Item("Addr1").ToString(), dt.Rows(ct).Item("Addr2").ToString(), dt.Rows(ct).Item("Addr3").ToString(), dt.Rows(ct).Item("CityStZip").ToString(), dt.Rows(ct).Item("Zip").ToString(), dt.Rows(ct).Item("Fields").ToString())

            Next ct

            RunGroup1LabelsG1CWEBSBM(inpmbr.ToUpper.Trim, jobid)

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function GetGroup1PFMembers() As DataTable
        '---------------------------------------------------------
        'Function: BuildGroup1Members
        'Desc. . : Build a file that has a list of members in GROUP1PF
        'Parms . : None
        '
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

            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Set up program call command line
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "DSPFDGRP1"

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Load member names into a table

            query = "Select mlname"
            query += " from  dspfdgrp1"
            query += " order by mlname "

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetGroup1Jobs() As DataTable
        '---------------------------------------------------------
        'Function: Get Group1 Job IDs
        'Desc. . : Build a list of Group1 jobs that can be used to create labels
        'Parms . : None
        '
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

            'Load job ids into table

            query = "Select jobid"
            query += " from  c1cpjblf"
            query += " where jobfct='000JBDEF' and jobid <> '@IVP@'"
            query += " order by jobid "

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function ReSetGroup1MbrG1CWEBMBR(ByVal inpmbr As String)
        '---------------------------------------------------------
        'Function: ReSetGroup1MbrG1CWEBMBR
        'Desc. . : Create or clear the member in GROUP1PF that is about to receive data
        'Parms . : 1.) Group1 Member
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter rtnmsg as CHAR(10)
            oCmd.Parameters.Add("inpmbr", iDB2DbType.iDB2Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("inpmbr").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("inpmbr").Value = inpmbr

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "G1CWEBMBR"

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function RunGroup1LabelsG1CWEBSBM(ByVal inpmbr As String, ByVal jobid As String)
        '---------------------------------------------------------
        'Function: ReSetGroup1MbrG1CWEBMBR
        'Desc. . : Create or clear the member in GROUP1PF that is about to receive data
        'Parms . : 1.) Group1 Member
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter rtnmsg as CHAR(10)
            oCmd.Parameters.Add("inpmbr", iDB2DbType.iDB2Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("inpmbr").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("inpmbr").Value = inpmbr

            'Set up a character parameter rtnmsg as CHAR(5)
            oCmd.Parameters.Add("jobid", iDB2DbType.iDB2Char, 5)
            'Always set parms as input/output for program call
            oCmd.Parameters("jobid").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("jobid").Value = jobid

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "G1CWEBSBM"

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

End Class
