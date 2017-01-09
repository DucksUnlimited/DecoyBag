Imports Microsoft.VisualBasic
Imports System
Imports System.Data


Public Class ClassiSeriesDatabase

    '------------------------------------------------------------
    'iSeries Access OLEDB Connection String
    '------------------------------------------------------------
    Dim mvConnectionString As String = ""

    '------------------------------------------------------------
    'Default SQL string
    '------------------------------------------------------------
    Dim mvQCUSTCDTSELECT As String = "SELECT * FROM RJSDOTNET.QCUSTCDT"
    Dim mvQCUSTCDTUPDATE As String = "UPDATE RJSDOTNET.QCUSTCDT"
    Dim mvQCUSTCDTDELETE As String = "DELETE FROM RJSDOTNET.QCUSTCDT"
    Dim mvQCUSTCDTINSERT As String = "INSERT INTO RJSDOTNET.QCUSTCDT"

    '------------------------------------------------------------
    'This is sample iSeries connection info
    '------------------------------------------------------------
    Dim mvUserID As String = ""
    Dim mvPassword As String = ""
    Dim mvSystem As String = ""

    '------------------------------------------------------------
    'OLEDB Objects
    '------------------------------------------------------------
    Dim mvDataConnection As New System.Data.OleDb.OleDbConnection
    Dim mvDataAdapter As System.Data.OleDb.OleDbDataAdapter
    Dim mvDataCommand As System.Data.OleDb.OleDbCommand

    '------------------------------------------------------------
    'General status variables
    '------------------------------------------------------------
    Dim mvLastError As String = ""
    Dim bConnected As Boolean

    Public Sub New()

        'Start iSeries database connection on first use

        Try

            mvLastError = ""

            '------------------------------------------------------------
            'Get config settings from web.config file
            '------------------------------------------------------------
            'Dim aConfig As Configuration.ConfigurationSettings
            Dim mvuserid As String = ConfigurationManager.AppSettings("iSeriesUser")
            Dim mvPassword As String = ConfigurationManager.AppSettings("iSeriesPassword")
            Dim mvSystem As String = ConfigurationManager.AppSettings("iSeriesSystem")
            Dim mvConnectionString As String = ConfigurationManager.AppSettings("iSeriesConnectionString")

            '------------------------------------------------------------
            'Replace system and user connection values into OLEDB connection string
            '------------------------------------------------------------
            mvConnectionString = mvConnectionString.Replace("@@SYSTEM", mvSystem)
            mvConnectionString = mvConnectionString.Replace("@@USERID", mvuserid)
            mvConnectionString = mvConnectionString.Replace("@@PASSWORD", mvPassword)

            '------------------------------------------------------------
            'Set connection info and open connection
            '------------------------------------------------------------
            mvDataConnection.ConnectionString = mvConnectionString
            mvDataConnection.Open()

            '------------------------------------------------------------
            'Set connected
            '------------------------------------------------------------
            bConnected = True

        Catch ex As Exception
            'Set error info
            mvLastError = ex.Message
            bConnected = False
        End Try
    End Sub
    Public Function IsConnected() As Boolean
        'Return connection status
        Return bConnected
    End Function
    Public Function ExecuteOLEDBQuery(ByVal cmdtext As String) As DataTable
        '--------------------------------------------------------------------
        'Function: ExecuteOLEDBQuery
        'Purpose.: Run SQL Data Query
        'Desc. . : This function takes an SQL SELECT statement and connection string and 
        '          runs the query to get the data we want to work with.
        '--------------------------------------------------------------------
        Try

            '------------------------------------------------------------
            'Create temporary OLEDB data adapter using OLEDB connection string and SQL statement
            '------------------------------------------------------------
            Dim adapter As New OleDb.OleDbDataAdapter(cmdtext, mvDataConnection)

            '------------------------------------------------------------
            'Create a Data Table to hold our results from the query
            '------------------------------------------------------------
            Dim datatable1 As DataTable = New DataTable

            '------------------------------------------------------------
            'Fill the DataTable with records via the data adapter
            '------------------------------------------------------------
            adapter.Fill(datatable1)

            '------------------------------------------------------------
            'Return the DataTable recordset so the web page can use the data
            '------------------------------------------------------------
            Return datatable1

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Function GetCustomerList(ByVal sWhereCriteria As String) As DataTable

        Dim lcSql As String

        Try

            mvLastError = "" 'Reset last error

            'Make sure we're connected or bail
            If IsConnected() = False Then
                Throw New Exception("Not connected to iSeries.")
            End If

            'Set SQL select
            lcSql = mvQCUSTCDTSELECT & " " & sWhereCriteria

            'Run query and return results
            Return ExecuteOLEDBQuery(lcSql)

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function
    Function GetCustomer(ByVal sCustomer As String) As DataTable

        Dim lcSql As String

        Try

            mvLastError = "" 'Reset last error

            'Make sure we're connected or bail
            If IsConnected() = False Then
                Throw New Exception("Not connected to iSeries.")
            End If

            'Set SQL select
            lcSql = mvQCUSTCDTSELECT & "  WHERE CUSNUM=" & sCustomer

            'Run query and return results
            Return ExecuteOLEDBQuery(lcSql)

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Function UpdateCustomer(ByVal sCUSNUM As String, ByVal sLSTNAM As String, ByVal sINIT As String, ByVal sSTREET As String, ByVal sCity As String, ByVal sState As String, ByVal sZIPCOD As String) As Boolean


        Dim lcSql As String

        Try

            mvLastError = "" 'Reset last error

            'Make sure we're connected or bail
            If IsConnected() = False Then
                Throw New Exception("Not connected to iSeries.")
            End If

            'Bail on parm validation
            If sCUSNUM.Trim = "" Then
                Throw New Exception("Must pass customer number key.")
            End If

            'Set SQL UPDATE
            lcSql = mvQCUSTCDTUPDATE & " SET " & _
            "LSTNAM='" & sLSTNAM & "'," & _
            "INIT='" & sINIT & "'," & _
            "STREET='" & sSTREET & "'," & _
            "CITY='" & sCity & "'," & _
            "STATE='" & sState & "'," & _
            "ZIPCOD=" & sZIPCOD & "" & _
            " WHERE CUSNUM=" & sCUSNUM

            'Run update
            mvDataCommand = New System.Data.OleDb.OleDbCommand(lcSql, mvDataConnection)
            mvDataCommand.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Function DeleteCustomer(ByVal sCUSNUM As String) As Boolean


        Dim lcSql As String

        Try

            mvLastError = "" 'Reset last error

            'Make sure we're connected or bail
            If IsConnected() = False Then
                Throw New Exception("Not connected to iSeries.")
            End If

            'Bail on parm validation
            If sCUSNUM.Trim = "" Then
                Throw New Exception("Must pass customer number key.")
            End If

            'Set SQL DELETE
            lcSql = mvQCUSTCDTDELETE & _
            " WHERE CUSNUM=" & sCUSNUM

            'Run delete
            mvDataCommand = New System.Data.OleDb.OleDbCommand(lcSql, mvDataConnection)
            mvDataCommand.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Function InsertCustomer(ByVal sCUSNUM As String, ByVal sLSTNAM As String, ByVal sINIT As String, ByVal sSTREET As String, ByVal sCity As String, ByVal sState As String, ByVal sZIPCOD As String) As Boolean


        Dim lcSql As String

        Try

            mvLastError = "" 'Reset last error

            'Make sure we're connected or bail
            If IsConnected() = False Then
                Throw New Exception("Not connected to iSeries.")
            End If

            'Bail on parm validation
            If sCUSNUM.Trim = "" Then
                Throw New Exception("Must pass customer number key.")
            End If

            'Set SQL INSERT
            lcSql = mvQCUSTCDTINSERT & " " & _
            "(CUSNUM,LSTNAM,INIT,STREET,CITY,STATE,ZIPCOD) " & _
            " VALUES(" & _
            sCUSNUM & "," & _
            "'" & sLSTNAM & "'," & _
            "'" & sINIT & "'," & _
            "'" & sSTREET & "'," & _
            "'" & sCity & "'," & _
            "'" & sState & "'," & _
            "" & sZIPCOD & "" & _
            ")"

            'Run insert
            mvDataCommand = New System.Data.OleDb.OleDbCommand(lcSql, mvDataConnection)
            mvDataCommand.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Function GetLastError() As String
        Return mvLastError
    End Function

End Class
