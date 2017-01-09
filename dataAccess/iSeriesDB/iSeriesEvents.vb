Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
Imports System.data
Imports System.Web.HttpContext
Imports System.Configuration

Public Class iSeriesEvents

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("iSeries"))
    Shared mvLastError As String = ""

    Shared Function GetEventInvSum() As DataTable
        '-----------------------------------------------
        'Function: 
        'Desc . .: Build a file that lists Item Inventory
        'Parms. .: None
        '
        'Returns : Returns a data table object.

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            'Load data into table

            query = "select *"
            query += " from spp002pf"
            'query += " order as recieved "

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)
            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return (Nothing)
        End Try

    End Function

    Shared Function GetOrgZip() As DataTable
        '-----------------------------------------------
        'Function: 
        'Desc . .: Build a file that lists Item Inventory
        'Parms. .: None
        '
        'Returns : Returns a data table object.

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            'Load data into table

            query = "select epzip, epzipd from evorgzip order by epzipd"

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)
            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return (Nothing)
        End Try

    End Function

    Shared Function EV45001NC(ByVal userid, ByVal filelist, ByVal jobname, _
                       ByVal labels, ByVal LCopies, ByVal SortTypeL, ByVal LabelType, ByVal Group1, _
                       ByVal Reports, ByVal RCopies, ByVal SortTypeR, _
                       ByVal Request, ByVal OrgZip) As Boolean

        Try
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If

            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter
            oCmd.Parameters.Add("userID", iDB2DbType.iDB2Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("userID").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("FileIn", iDB2DbType.iDB2Char, 20)
            'Always set parms as input/output for program call
            oCmd.Parameters("FileIn").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("JobNam", iDB2DbType.iDB2Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("JobNam").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("LABELS", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("LABELS").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("SCCOP1", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("SCCOP1").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("SLT1", iDB2DbType.iDB2Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("SLT1").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("SCCOL1", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("SCCOL1").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("GRPYN", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("GRPYN").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("REPORTS", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("REPORTS").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("SCCOP3", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("SCCOP3").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("SLT3", iDB2DbType.iDB2Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("SLT3").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("ORGZIP", iDB2DbType.iDB2Char, 5)
            'Always set parms as input/output for program call
            oCmd.Parameters("ORGZIP").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("News", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("News").Direction = ParameterDirection.InputOutput

            'Set all the parameters values
            oCmd.Parameters("userID").Value = userid.Text.ToUpper
            oCmd.Parameters("FileIn").Value = filelist.SelectedValue
            oCmd.Parameters("JobNam").Value = jobname.Text.ToUpper
            If (labels.Checked) Then
                oCmd.Parameters("LABELS").Value = "Y"
                oCmd.Parameters("SCCOP1").Value = LCopies.Text
                oCmd.Parameters("SLT1").Value = SortTypeL.Text
                oCmd.Parameters("SCCOL1").Value = LabelType.Text
                If (Group1.Checked) Then
                    oCmd.Parameters("GRPYN").Value = "Y"
                Else
                    oCmd.Parameters("GRPYN").Value = "N"
                End If
            Else
                oCmd.Parameters("LABELS").Value = "N"
                oCmd.Parameters("SCCOP1").Value = "0"
                oCmd.Parameters("SLT1").Value = "  "
                oCmd.Parameters("SCCOL1").Value = "4"
                oCmd.Parameters("GRPYN").Value = "N"
            End If

            If (Reports.Checked) Then
                oCmd.Parameters("REPORTS").Value = "Y"
                oCmd.Parameters("SCCOP3").Value = RCopies.Text
                oCmd.Parameters("SLT3").Value = SortTypeR.Text
            Else
                oCmd.Parameters("REPORTS").Value = "N"
                oCmd.Parameters("SCCOP3").Value = "0"
                oCmd.Parameters("SLT3").Value = "  "
            End If

            If (Request("news").ToUpper.Equals("Y")) Then
                oCmd.Parameters("ORGZIP").Value = OrgZip.SelectedValue
                oCmd.Parameters("SLT1").Value = "  "
            Else
                oCmd.Parameters("ORGZIP").Value = " "
            End If
            oCmd.Parameters("News").Value = Request("news").ToUpper



            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "EV45001NC"

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
