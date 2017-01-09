Imports Microsoft.VisualBasic
Imports System.data
Imports System.Data.OleDb

Public Class ClassHoldoledb

    Public parms As New ClassSessionManager
    'iSeries Connection variables
    Dim connectionString As ConnectionStringSettingsCollection = ConfigurationManager.ConnectionStrings
    Dim connString As String = ""

    Dim connoledb As OleDbConnection
    Dim oCmd As OleDbCommand
    Dim mvLibrary As String = ""
    Dim mvConnection As String = ""
    Dim mvLastError As String = ""

    'Sample return parm structure for program call
    Public Structure rtnparms1
        Dim parm1 As String
        Dim parm2 As Double
        Dim bCompleted As Boolean
        Dim sMessage As String
    End Structure

    Public Structure rtnCatalog
        Dim dt As DataTable
        Dim catname As String
    End Structure

    Public Structure rtnDSSWR006
        Dim sType As String
        Dim sBranch1 As String
        Dim sBranch2 As String
        Dim sOrderNumber As String
        Dim iSequence As Integer
        Dim sProgramName As String
    End Structure

    Function GetNextOrderMultParmSWR006(ByVal sType As String, ByVal sBranch1 As String, ByVal sBranch2 As String, ByVal sOrderNumber As String, ByVal iSequence As Integer, ByVal sProgramName As String) As rtnDSSWR006
        '---------------------------------------------------------
        'Function: GetNextOrderSWR006
        'Desc. . : Call get next order number program SWR006
        'Parms . : 1.) Type (If blank, default to HT)
        '          2.) Branch 1 (Warehouse)
        '          3.) Branch 2 (Warehouse)
        '          4.) Order number
        '          5.) Sequence
        '          6.) Program name
        'Returns : Returns a data structure with all iseries parm values
        '---------------------------------------------------------

        Dim lclDSSWR006 As New rtnDSSWR006  'Set up return parm list

        'Reset last error
        mvLastError = ""

        Try

            'Set default values

            'Set type to HT if not passed.
            If sType.Trim = "" Then
                sType = "HT"
            End If

            'Always make sure seq = 0
            If iSequence > 0 Then
                iSequence = 0
            End If


            'Validation
            If sOrderNumber.Trim <> "" Then
                Throw New Exception("Order number cannot be passed into this routine.")
            End If


            'Set command object and connection info 
            'for the command object to current iSeries OLEDB connection
            oCmd = New OleDb.OleDbCommand
            oCmd.Connection = connoledb

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter rtnmsg as CHAR(2)
            oCmd.Parameters.Add("type", OleDb.OleDbType.Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("type").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("type").Value = sType

            'Set up a character parameter rtnmsg as CHAR(2)
            oCmd.Parameters.Add("br1", OleDb.OleDbType.Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("br1").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("br1").Value = sBranch1

            'Set up a character parameter rtnmsg as CHAR(2)
            oCmd.Parameters.Add("br2", OleDb.OleDbType.Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("br2").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("br2").Value = sBranch2

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("no", OleDb.OleDbType.Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("no").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("no").Value = sOrderNumber

            'Set up a numeric parameter amount for as DECIMAL(5.0)
            oCmd.Parameters.Add("seq", OleDb.OleDbType.Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("seq").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("seq").Precision = 5
            'Set number of decimal positions
            oCmd.Parameters("seq").Scale = 0
            'Set parm value
            oCmd.Parameters("seq").Value = iSequence

            'Set up a character parameter rtnmsg as CHAR(10)
            oCmd.Parameters.Add("pgm", OleDb.OleDbType.Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("pgm").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("pgm").Value = sProgramName

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = "{{CALL @@LIB.SWR006DD (? ? ? ? ? ?)}}"
            oCmd.CommandText = oCmd.CommandText.Replace("@@LIB", mvLibrary)

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Get parms from iSeries after program call 
            lclDSSWR006.sType = oCmd.Parameters("type").Value
            lclDSSWR006.sBranch1 = oCmd.Parameters("br1").Value
            lclDSSWR006.sBranch2 = oCmd.Parameters("br2").Value
            lclDSSWR006.sOrderNumber = oCmd.Parameters("no").Value
            lclDSSWR006.iSequence = oCmd.Parameters("seq").Value
            lclDSSWR006.sOrderNumber = oCmd.Parameters("no").Value
            lclDSSWR006.sProgramName = oCmd.Parameters("pgm").Value

            'Return true if successful
            Return lclDSSWR006

        Catch ex As Exception

            'Send back empty DS
            lclDSSWR006.sType = ""
            lclDSSWR006.sBranch1 = ""
            lclDSSWR006.sBranch2 = ""
            lclDSSWR006.sOrderNumber = ""
            lclDSSWR006.iSequence = 0
            lclDSSWR006.sOrderNumber = ""
            lclDSSWR006.sProgramName = ""

            'Set class level error message as well
            mvLastError = ex.Message

            Return lclDSSWR006

        End Try

    End Function
    Function GetNextOrderSWR006(ByVal sType As String, ByVal sBranch1 As String, ByVal sBranch2 As String, ByVal sOrderNumber As String, ByVal iSequence As Integer, ByVal sProgramName As String) As String

        '---------------------------------------------------------
        'Function: GetNextOrderSWR006
        'Desc. . : Call get next order number program SWR006
        '          and send back order as multiple parms
        'Parms . : 1.) Type
        '          2.) Branch 1 (Warehouse)
        '          3.) Branch 3 (Warehouse)
        '          4.) Order number
        '          5.) Sequence
        '          6.) Program name
        'Returns : Returns order as a string
        '---------------------------------------------------------

        'Reset last error
        mvLastError = ""

        Try

            'Set default values

            'Set type to HT if not passed.
            If sType.Trim = "" Then
                sType = "HT"
            End If

            'Always make sure seq = 0
            If iSequence > 0 Then
                iSequence = 0
            End If


            'Validation
            If sOrderNumber.Trim <> "" Then
                Throw New Exception("Order number cannot be passed into this routine.")
            End If


            'Set command object and connection info 
            'for the command object to current iSeries OLEDB connection
            oCmd = New OleDb.OleDbCommand
            oCmd.Connection = connoledb

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter rtnmsg as CHAR(2)
            oCmd.Parameters.Add("type", OleDb.OleDbType.Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("type").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("type").Value = sType

            'Set up a character parameter rtnmsg as CHAR(2)
            oCmd.Parameters.Add("br1", OleDb.OleDbType.Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("br1").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("br1").Value = sBranch1

            'Set up a character parameter rtnmsg as CHAR(2)
            oCmd.Parameters.Add("br2", OleDb.OleDbType.Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("br2").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("br2").Value = sBranch2

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("no", OleDb.OleDbType.Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("no").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("no").Value = sOrderNumber

            'Set up a numeric parameter amount for as DECIMAL(5.0)
            oCmd.Parameters.Add("seq", OleDb.OleDbType.Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("seq").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("seq").Precision = 5
            'Set number of decimal positions
            oCmd.Parameters("seq").Scale = 0
            'Set parm value
            oCmd.Parameters("seq").Value = iSequence

            'Set up a character parameter rtnmsg as CHAR(10)
            oCmd.Parameters.Add("pgm", OleDb.OleDbType.Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("pgm").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("pgm").Value = sProgramName

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = "{{CALL @@LIB.SWR006 (? ? ? ? ? ?)}}"
            oCmd.CommandText = oCmd.CommandText.Replace("@@LIB", mvLibrary)

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Return order number after program call 
            Return oCmd.Parameters("no").Value

        Catch ex As Exception


            'Set class level error message as well
            mvLastError = ex.Message

            Return ""

        End Try

    End Function

    Public Function LoadCustomerDS(ByVal sChapter As String) As DataSet
        '---------------------------------------------------------
        'Function: LoadCustomerDS
        'Desc. . : Get chapter billto/shipto info as a DataReader.
        'Parms . : 1.) Chapter Number
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim ds1 As New DataSet
        Dim dt1 As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            'Build SQL 
            Dim adapter As New OleDbDataAdapter("select * from fsdata50.cstmst where cstnum='" & "     " & sChapter & "'", connoledb)

            'Fill the data table
            adapter.Fill(dt1)

            'Return the data set
            ds1.Tables.Add(dt1)

            Return ds1

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function InsertOrderDetail(ByVal sHtnum As String, ByVal sHtllin As Integer, ByVal sHtlpnm As String, ByVal sHtlun As String, ByVal sHtqor As Integer, ByVal sHtluct As Double, ByVal sHtldsc As String) As Boolean
        '---------------------------------------------------------
        'Function: InsertOrderDetail
        'Desc. . : Insert Quoted order detail using SQL
        'Parms . : 1.) Order number
        '          2.) Line number
        '          3.) Item number 
        '          4.) Unit of measure
        '          5.) Quantity ordered
        '          6.) Unit cost
        '          7.) Item description
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""



        'Reset last error
        mvLastError = ""

        Try



            Dim cmd As New OleDbCommand("insert into @@LIB.chtlin (htnum,htllin,htlpnm,htlun,htqor,htluct,htldsc) values('@@htnum','@@htllin','@@htlpnm','@@htlun','@@htqor','@@htluct','@@htldsc')", connoledb)
            cmd.CommandText = cmd.CommandText.Replace("@@htnum", sHtnum)
            cmd.CommandText = cmd.CommandText.Replace("@@htllin", sHtllin)
            cmd.CommandText = cmd.CommandText.Replace("@@htlpnm", sHtlpnm)
            cmd.CommandText = cmd.CommandText.Replace("@@htlun", sHtlun)
            cmd.CommandText = cmd.CommandText.Replace("@@htqor", sHtqor)
            cmd.CommandText = cmd.CommandText.Replace("@@htluct", sHtluct)
            cmd.CommandText = cmd.CommandText.Replace("@@htldsc", sHtldsc)
            cmd.CommandText = cmd.CommandText.Replace("@@LIB", mvLibrary)

            'Execute the record insert
            cmd.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try


    End Function

    Public Function InsertOrderHeader(ByVal sEvtpo As String, ByVal sEvdate As String, ByVal sVolnam As String, ByVal sShpnam As String, ByVal sShpad1 As String, ByVal sShpad2 As String, ByVal sShpcty As String, ByVal sShpzip5 As String, ByVal sShpzip4 As String, ByVal sTotcost As Double, ByVal sNeword As String) As String
        '---------------------------------------------------------
        'Function: InsertOrderHeader
        'Desc. . : Insert Quoted order header using SQL
        'Parms . : 1.) Event chapter
        '          2.) Event date
        '          3.) Volunteer name 
        '          4.) Ship to name
        '          5.) Ship to address line one
        '          6.) Ship to address line two
        '          7.) Ship to city/state
        '          8.) Ship to zip/5
        '          9.) Ship to zip/4
        '         10.) Total cost of order
        '         11.) New order number - returned
        'Returns : New order number
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        'Reset last error
        mvLastError = ""

        Try

            'NOTE: We are assuming that sEvtpo will be passed in 
            'as a 7 character field. Jere will make this happen :-)

            ' Get order number from iseries
            Dim sHtnum As String = GetNextOrderSWR006("HT", "", "", "", 0, "")

            'Make sure order number was returned
            If sHtnum.Trim = "" Then
                Throw New Exception("No order number from SWR006")
            End If

            'Get first 5 characters of Evpto and lead it with 5 blanks.
            Dim sWrkEvtpo As String = "     " & sEvtpo.Substring(0, 5)

            Dim cmd As New OleDbCommand("insert into @@LIB.chtmst (htnum,htcno,htcpo,htcodt,htorby,htsnam,htsad1,htsad2,htscty,htszip,htszp2,htcost) values('@@htnum','@@htcno','@@htcpo','@@htcodt','@@htorby','@@htsnam','@@htsad1','@@htsad2','@@htscty','@@htszip','@@htszp2','@@htcost')", connoledb)
            cmd.CommandText = cmd.CommandText.Replace("@@htnum", sHtnum)
            cmd.CommandText = cmd.CommandText.Replace("@@htcno", sWrkEvtpo)
            cmd.CommandText = cmd.CommandText.Replace("@@htcpo", sEvtpo)
            cmd.CommandText = cmd.CommandText.Replace("@@htcodt", sEvdate)
            cmd.CommandText = cmd.CommandText.Replace("@@htorby", sVolnam)
            cmd.CommandText = cmd.CommandText.Replace("@@htsnam", sShpnam)
            cmd.CommandText = cmd.CommandText.Replace("@@htsad1", sShpad1)
            cmd.CommandText = cmd.CommandText.Replace("@@htsad2", sShpad2)
            cmd.CommandText = cmd.CommandText.Replace("@@htscty", sShpcty)
            cmd.CommandText = cmd.CommandText.Replace("@@htszip", sShpzip5)
            cmd.CommandText = cmd.CommandText.Replace("@@htszp2", sShpzip4)
            cmd.CommandText = cmd.CommandText.Replace("@@htcost", sTotcost)
            cmd.CommandText = cmd.CommandText.Replace("@@LIB", mvLibrary)

            'Execute the record insert
            cmd.ExecuteNonQuery()

            'Return the order number to caller
            Return sHtnum

        Catch ex As Exception
            mvLastError = ex.Message
            Return ""
        End Try


    End Function

    Function CallProgramTESTCL1() As rtnparms1

        '---------------------------------------------------------
        'Function: CallprogramTESTCL1
        'Desc. . : Sample to call iSeries program with parms
        'Parms . : iSeries formatted command line
        'Returns : Returns true/false on success
        '---------------------------------------------------------

        Dim rtnparms As New rtnparms1 'Set up return parm list

        'Reset last error
        mvLastError = ""

        Try

            'Set command object and connection info 
            'for the command object to current 
            'OLEDB connection
            oCmd = New OleDb.OleDbCommand
            oCmd.Connection = connoledb

            'Set up a character parameter rtnmsg as CHAR(50)
            oCmd.Parameters.Add("rtnmsg", OleDb.OleDbType.Char, 50)
            'Always set parms as input/output for program call
            oCmd.Parameters("rtnmsg").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("rtnmsg").Value = ""

            'Set up a numeric parameter amount for as DECIMAL(15.2)
            oCmd.Parameters.Add("amount", OleDb.OleDbType.Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("amount").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("amount").Precision = 15
            'Set number of decimal positions
            oCmd.Parameters("amount").Scale = 2
            'Set parm value to 200.00
            'This amount will be incremented in the CL
            oCmd.Parameters("amount").Value = 200.0

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = "{{CALL RJSDOTNET.TESTCL1 (? ?)}}"

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Get parms from iSeries after program call 
            rtnparms.parm1 = oCmd.Parameters("rtnmsg").Value
            rtnparms.parm2 = oCmd.Parameters("amount").Value
            rtnparms.bCompleted = True
            rtnparms.sMessage = "Completed normally."

            'Return true if successful
            Return rtnparms

        Catch ex As Exception
            rtnparms.parm1 = ""
            rtnparms.parm2 = 0
            rtnparms.bCompleted = False
            rtnparms.sMessage = ex.Message
            'Set class level error message as well
            mvLastError = ex.Message
            Return rtnparms
        End Try

    End Function

    Function CallCommand(ByVal sCommand As String) As Boolean
        '---------------------------------------------------------
        'Function: CallCommand
        'Desc. . : Run iSeries command 
        'Parms . : iSeries formatted command line
        'Returns : Returns true/false on success
        '---------------------------------------------------------

        Dim iSeriesCmd As String = ""
        Dim iSeriesCmdLength As String = ""

        'Reset last error
        mvLastError = ""

        Try

            'Set command object and connection info 
            'for the command object to current 
            'OLEDB connection
            oCmd = New OleDb.OleDbCommand
            oCmd.Connection = connoledb

            'Set up iSeries command line call
            iSeriesCmd = sCommand
            'Format the length to 15.5 for command call
            iSeriesCmdLength = Format(iSeriesCmd.Trim().Length, "0000000000.00000")

            'Set up stored procedure command call
            'Seems to be a limitation on passing values 
            'That contain spaces or single quotes
            iSeriesCmd = iSeriesCmd.Replace("'", "''")
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = "call qsys.qcmdexc('" & Trim(iSeriesCmd) & "'," & Trim(iSeriesCmdLength) & ")"

            'Execute iSeries command now
            oCmd.ExecuteNonQuery()

            'Return true if successful
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function


    Public Sub New()
        '---------------------------------------------------------
        'Function: New
        'Desc. . : This routine gets triggered automatically
        '          when the iSeries object is created.
        '---------------------------------------------------------

        Try

            'Dim aConfig As System.Configuration.ConfigurationManager
            'Load web application settings from web.config
            'mvLibrary = aConfig.AppSettings("LIBRARY")
            'mvLibraryProd = aConfig.AppSettings("LIBRARYPROD")
            mvConnection = ConfigurationManager.AppSettings("CONNECTION")

            'Set connection string 
            connectionString = ConfigurationManager.ConnectionStrings
            connString = connectionString("S1032895").ToString()


            'Open iSeries connection - OLEDB
            connoledb = New OleDbConnection(connString)
            connoledb.Open()

        Catch ex As Exception
            mvLastError = ex.Message
        End Try

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
