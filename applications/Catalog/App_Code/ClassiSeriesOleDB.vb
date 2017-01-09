Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
Imports System.data
Imports System.Data.OleDb

Public Class ClassiSeriesOleDB

    '---------------------------------------------------------
    'Class: ClassiSeriesDataAccess
    'Desc : This is our business object that performs all of 
    '       our database access, program and command calls.
    '
    'Variables Declared at this level can be seen 
    'anywhere in the class
    '---------------------------------------------------------

    Public parms As New ClassSessionManager
    'iSeries Connection variables
    Dim connectionString As ConnectionStringSettingsCollection = ConfigurationManager.ConnectionStrings
    Dim connString As String = ""
    Dim connStringiDB2 As String = ""
    Dim conn As iDB2Connection

    Dim connoledb As OleDbConnection
    Dim oCmd As OleDbCommand

    'Memory variables within class. Usually class level info
    'such as values read from web.config
    Dim mvLibrary As String = ""
    Dim mvLibraryProd As String = ""
    Dim mvConnection As String = ""
    Dim mvConnectioniDB2 As String = ""
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
#Region "Order Entry"

#Region "Get Order Information"

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

#End Region

#Region "Load Order Information"

    Public Function LoadQuotOrderListRD(ByVal rdNum As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadQuotOrderListRD
        'Desc. . : Get all quoted order header information for a RD as a DataTable.
        'Parms . : 1.) RD Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "select p.htnum, p.htcpo, p.htslm1, p.htrcfl, p.htcodt,"
            query += " c.catevdesc, c.catevpo,"
            query += " CONCAT(c.catevdesc,CONCAT(' - ',p.htnum)) dropname"
            query += " from @@LIB.chtmst p"
            query += " left outer join @@LIB.catevname c on (c.catevpo = p.htcpo)"
            query += " where (p.htslm1='0" + rdNum + "' and p.htrcfl='Q')"
            'query += " group by c.catevdesc, p.htcpo, p.htnum,"
            query += " order by c.catevdesc, p.htcpo, p.htnum"

            Dim cmdD As New OleDbCommand(query, connoledb)
            cmdD.CommandText = cmdD.CommandText.Replace("@@LIB", mvLibrary)

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadQuotOrderListEV(ByVal eventKey As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadQuotOrderListEV
        'Desc. . : Get all quoted order header information for an event as a DataTable.
        'Parms . : 1.) Event Key (ST,CHP,EV NUM,EV TYPE)
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select * From @@LIB.chtmst where htcpo=" + eventKey + " and htrcfl=Q"
            Dim cmdD As New OleDbCommand(query, connoledb)
            cmdD.CommandText = cmdD.CommandText.Replace("@@LIB", mvLibrary)

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadQuotOrderDetail(ByVal sOrder As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadQuotOrderDetail
        'Desc. . : Get all quoted order details as a DataTable.
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select * From @@LIB.chtlin where (htnum=" + sOrder + " and htlnst <> 'C')"
            Dim cmdD As New OleDbCommand(query, connoledb)
            cmdD.CommandText = cmdD.CommandText.Replace("@@LIB", mvLibrary)

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadQuotOrderHeader(ByVal sOrder As String) As OleDbDataReader
        '---------------------------------------------------------
        'Function: LoadQuotOrderHeader
        'Desc. . : Get a quoted order header as a DataReader.
        'Parms . : 1.) Order Number
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dr As OleDbDataReader

        'Reset last error
        mvLastError = ""

        Try

            query = "Select p.*, c.*"
            query += " from @@LIB.chtmst p"
            query += " left outer join @@LIB.catffl c on (c.fflord = p.htnum)"
            query += " where (p.htnum = '" + sOrder + "')"

            Dim cmd As New OleDbCommand(query, connoledb)
            cmd.CommandText = cmd.CommandText.Replace("@@LIB", mvLibrary)

            dr = cmd.ExecuteReader()

            Return dr

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadOrderDetail(ByVal sOrder As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadOrderDetail
        'Desc. . : Get order details as a DataTable.
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select * From @@LIB.chtlin where (htnum=" + sOrder + " and htlnst <> 'C')"
            Dim cmdD As New OleDbCommand(query, connoledb)
            cmdD.CommandText = cmdD.CommandText.Replace("@@LIB", mvLibrary)

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadOrderHeader(ByVal sOrder As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadOrderHeader
        'Desc. . : Get order header as a DataTable.
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select p.*, c.*"
            query += " from @@LIB.chtmst p"
            query += " left outer join @@LIB.catffl c on (c.fflord = p.htnum)"
            query += " where (p.htnum = '" + sOrder + "')"

            Dim cmd As New iDB2Command(query, conn)
            cmd.CommandText = cmd.CommandText.Replace("@@LIB", mvLibrary)


            Dim dr As iDB2DataReader
            dr = cmd.ExecuteReader()

            Return dr

            'Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmd)
            'da.Fill(dt)
            'Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadOrderHeaderP(ByVal sOrder As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadOrderHeader
        'Desc. . : Get order header as a DataTable.
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            Dim cmd As New iDB2Command("select * from fsdata50.chtmst where htnum=@htnum", conn)
            cmd.Parameters.Add("@htnum", iDB2DbType.iDB2Char, 8)
            cmd.Parameters("@htnum").Value = sOrder

            Dim dr As iDB2DataReader
            dr = cmd.ExecuteReader()

            Return dr

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function


    Public Function LoadOrderDetailP(ByVal sOrder As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadOrderDetail
        'Desc. . : Get order details as a DataTable.
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            Dim cmdD As New iDB2Command("select htnum, HTLLIN, htqor, htlqsh, htlqbo, htlun, htlpnm, htldsc, htlupr from fsdata50.chtlin where htnum=@htnum", conn)
            cmdD.Parameters.Add("@htnum", iDB2DbType.iDB2Char, 8)
            cmdD.Parameters("@htnum").Value = sOrder

            Dim dr As iDB2DataReader
            dr = cmdD.ExecuteReader()

            Return dr

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadCustomer(ByVal sChapter As String) As OleDbDataReader
        '---------------------------------------------------------
        'Function: LoadCustomer
        'Desc. . : Get chapter billto/shipto info as a DataReader.
        'Parms . : 1.) Chapter Number
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim dr As OleDbDataReader

        'Reset last error
        mvLastError = ""

        Try

            Dim cmd As New OleDbCommand("select * from fsdata50.cstmst where cstnum='" & "     " & sChapter & "'", connoledb)

            dr = cmd.ExecuteReader()

            Return dr

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
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

#End Region

#Region "Insert/Update Order Information"


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
    Public Function SaveOrder(ByVal DTOrder As DataTable, _
        ByVal sShpnam As String, ByVal sShpad1 As String, ByVal sShpad2 As String, ByVal sShpcty As String, ByVal sShpzip5 As String, ByVal sShpzip4 As String, _
        ByVal fflorg As String, ByVal fflattn As String, ByVal ffladdr As String, ByVal fflcity As String, ByVal fflst As String, ByVal fflzip5 As String, ByVal fflzip4 As String, ByVal fflnum As String, ByVal fflexp As String, _
        ByVal sTotcost As Double, ByVal sCatName As String, ByVal uOrderNumber As String) As String

        Try

            parms = System.Web.HttpContext.Current.Session("oSessionManager")

            'Insert order header records with CATR001 call
            Dim sOrder As String
            sOrder = InsertOrderHeaderCATR001( _
                sShpnam, sShpad1, sShpad2, sShpcty, sShpzip5, sShpzip4, _
                fflorg, fflattn, ffladdr, fflcity, fflst, fflzip5, fflzip4, fflnum, fflexp, _
                sTotcost, uOrderNumber)
            If sOrder.Trim = "" Then
                Throw New Exception("Order number error.")
            End If

            'Loop to insert each detail line with call to CATR002
            Dim ct As Long
            For ct = 0 To DTOrder.Rows.Count - 1
                InsertOrderDetailCATR002(sOrder, ct + 1, DTOrder.Rows(ct).Item("ProductID"), DTOrder.Rows(ct).Item("Qty"), sCatName, parms.EventID)
            Next ct

            'Send back order number if completed.
            Return sOrder

        Catch ex As Exception
            'Set class level error message as well
            mvLastError = ex.Message

            Return ""
        End Try

    End Function


    Public Function InsertOrderDetailCATR002(ByVal sHtnum As String, ByVal sHtllin As Integer, ByVal sHtlpnm As String, ByVal sHtqor As Double, ByVal sCatName As String, ByVal sEvtPo As String) As Boolean
        '---------------------------------------------------------
        'Function: InsertOrderDetailCATR002
        'Desc. . : Insert Quoted order detail using program CATR002
        'Parms . : 1.) Order number
        '          2.) Line number
        '          3.) Item number 
        '          4.) Quantity ordered
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try


            'Set command object and connection info 
            'for the command object to current iSeries OLEDB connection
            oCmd = New OleDb.OleDbCommand
            oCmd.Connection = connoledb

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("htnum", OleDb.OleDbType.Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("htnum").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("htnum").Value = sHtnum



            'Set up a numeric parameter amount for as DECIMAL(3.0)
            oCmd.Parameters.Add("htllin", OleDb.OleDbType.Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("htllin").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("htllin").Precision = 3
            'Set number of decimal positions
            oCmd.Parameters("htllin").Scale = 0
            'Set parm value
            oCmd.Parameters("htllin").Value = sHtllin

            'Set up a character parameter rtnmsg as CHAR(20)
            oCmd.Parameters.Add("htlpnm", OleDb.OleDbType.Char, 20)
            'Always set parms as input/output for program call
            oCmd.Parameters("htlpnm").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("htlpnm").Value = sHtlpnm

            'Set up a numeric parameter amount for as DECIMAL(7.0)
            oCmd.Parameters.Add("htqor", OleDb.OleDbType.Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("htqor").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("htqor").Precision = 7
            'Set number of decimal positions
            oCmd.Parameters("htqor").Scale = 0
            'Set parm value
            oCmd.Parameters("htqor").Value = sHtqor

            'Set up a character parameter rtnmsg as CHAR(20)
            oCmd.Parameters.Add("cs33sn", OleDb.OleDbType.Char, 20)
            'Always set parms as input/output for program call
            oCmd.Parameters("cs33sn").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("cs33sn").Value = sCatName

            'Set up a character parameter rtnmsg as CHAR(7)
            oCmd.Parameters.Add("stchp", OleDb.OleDbType.Char, 7)
            'Always set parms as input/output for program call
            oCmd.Parameters("stchp").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("stchp").Value = sEvtPo

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = "{{CALL @@LIB.CATC002 (? ? ? ? ? ?)}}"
            oCmd.CommandText = oCmd.CommandText.Replace("@@LIB", mvLibrary)

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
    Public Function InsertOrderHeaderCATR001( _
        ByVal sShpnam As String, ByVal sShpad1 As String, ByVal sShpad2 As String, ByVal sShpcty As String, ByVal sShpzip5 As String, ByVal sShpzip4 As String, _
        ByVal fflorg As String, ByVal fflattn As String, ByVal ffladdr As String, ByVal fflcity As String, ByVal fflst As String, ByVal fflzip5 As String, ByVal fflzip4 As String, ByVal fflnum As String, ByVal fflexp As String, _
        ByVal sTotcost As Double, ByVal uOrderNumber As String) As String
        '---------------------------------------------------------
        'Function: InsertOrderHeaderCATR001
        'Desc. . : Insert Quoted order header using program CATR001
        'Parms . : 1.) Event chapter
        '          2.) Event date
        '          3.) Volunteer name 
        '          4.) Ship to name
        '          5.) Ship to address line one
        '          6.) Ship to address line two
        '          7.) Ship to city/state
        '          8.) Ship to zip/5
        '          9.) Ship to zip/4
        '         10.) FFL Ship to organization
        '         11.) FFL Ship to name
        '         12.) FFL Ship to address line
        '         13.) FFL Ship to city
        '         14.) FFL Ship to state
        '         15.) FFL Ship to zip/5
        '         16.) FFL Ship to zip/4
        '         17.) Total cost of order
        '         18.) Update order number
        '         19.) New order number - returned
        'Returns : New order number
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try

            'Set default values

            ''Set type to HT if not passed.
            'If sType.Trim = "" Then
            '    sType = "HT"
            'End If

            ''Always make sure seq = 0
            'If iSequence > 0 Then
            '    iSequence = 0
            'End If


            ''Validation
            'If sOrderNumber.Trim <> "" Then
            '    Throw New Exception("Order number cannot be passed into this routine.")
            'End If

            parms = System.Web.HttpContext.Current.Session("oSessionManager")

            'Set command object and connection info 
            'for the command object to current iSeries OLEDB connection
            oCmd = New OleDb.OleDbCommand
            oCmd.Connection = connoledb

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter rtnmsg as CHAR(7)
            oCmd.Parameters.Add("evtpo", OleDb.OleDbType.Char, 7)
            'Always set parms as input/output for program call
            oCmd.Parameters("evtpo").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("evtpo").Value = parms.EventID

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("evdate", OleDb.OleDbType.Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("evdate").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("evdate").Value = parms.EventDate

            'Set up a character parameter rtnmsg as CHAR(10)
            oCmd.Parameters.Add("volnam", OleDb.OleDbType.Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("volnam").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("volnam").Value = parms.UserID

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("shpnam", OleDb.OleDbType.Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("shpnam").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("shpnam").Value = sShpnam

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("shpad1", OleDb.OleDbType.Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("shpad1").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("shpad1").Value = sShpad1

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("shpad2", OleDb.OleDbType.Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("shpad2").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("shpad2").Value = sShpad2

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("shpcty", OleDb.OleDbType.Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("shpcty").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("shpcty").Value = sShpcty

            'Set up a character parameter rtnmsg as CHAR(5)
            oCmd.Parameters.Add("shpzip5", OleDb.OleDbType.Char, 5)
            'Always set parms as input/output for program call
            oCmd.Parameters("shpzip5").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("shpzip5").Value = sShpzip5

            'Set up a character parameter rtnmsg as CHAR(4)
            oCmd.Parameters.Add("shpzip4", OleDb.OleDbType.Char, 4)
            'Always set parms as input/output for program call
            oCmd.Parameters("shpzip4").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("shpzip4").Value = sShpzip4

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("fflorg", OleDb.OleDbType.Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("fflorg").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("fflorg").Value = fflorg

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("fflattn", OleDb.OleDbType.Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("fflattn").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("fflattn").Value = fflattn

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("ffladdr", OleDb.OleDbType.Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("ffladdr").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("ffladdr").Value = ffladdr

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("fflcity", OleDb.OleDbType.Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("fflcity").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("fflcity").Value = fflcity

            'Set up a character parameter rtnmsg as CHAR(2)
            oCmd.Parameters.Add("fflst", OleDb.OleDbType.Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("fflst").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("fflst").Value = fflst

            'Set up a character parameter rtnmsg as CHAR(5)
            oCmd.Parameters.Add("fflzip5", OleDb.OleDbType.Char, 5)
            'Always set parms as input/output for program call
            oCmd.Parameters("fflzip5").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("fflzip5").Value = fflzip5

            'Set up a character parameter rtnmsg as CHAR(4)
            oCmd.Parameters.Add("fflzip4", OleDb.OleDbType.Char, 4)
            'Always set parms as input/output for program call
            oCmd.Parameters("fflzip4").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("fflzip4").Value = fflzip4

            'Set up a character parameter rtnmsg as CHAR(20)
            oCmd.Parameters.Add("fflnum", OleDb.OleDbType.Char, 20)
            'Always set parms as input/output for program call
            oCmd.Parameters("fflnum").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("fflnum").Value = fflnum

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("fflexp", OleDb.OleDbType.Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("fflexp").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("fflexp").Value = fflexp

            'Set up a numeric parameter amount for as DECIMAL(11.2)
            oCmd.Parameters.Add("totcost", OleDb.OleDbType.Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("totcost").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("totcost").Precision = 11
            'Set number of decimal positions
            oCmd.Parameters("totcost").Scale = 2
            'Set parm value
            oCmd.Parameters("totcost").Value = sTotcost

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("neword", OleDb.OleDbType.Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("neword").Direction = ParameterDirection.InputOutput
            'Set parm value
            If uOrderNumber Is Nothing Or uOrderNumber = "" Then
                oCmd.Parameters("neword").Value = " "
            Else
                oCmd.Parameters("neword").Value = uOrderNumber
            End If

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = "{{CALL @@LIB.CATC001 (? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ?)}}"
            oCmd.CommandText = oCmd.CommandText.Replace("@@LIB", mvLibrary)

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Return order number after program call 
            Return oCmd.Parameters("neword").Value

        Catch ex As Exception

            'Set class level error message as well
            mvLastError = ex.Message

            Return ""
        End Try

    End Function

#End Region

#Region "Load Catalog Information"


    Public Function LoadCatsGroupsDataTable(ByVal rdOnly As Boolean, ByVal eventType As String) As DataTable
        '-------------------------------------------------------
        'function  : LoadCatsGroupsDataTable
        'Desc. . . : Load the catalog group buttons to web page.
        '-------------------------------------------------------


        Dim dt As New DataTable

        Try

            Dim query As String = String.Empty

            If rdOnly Then
                If eventType = "S" Then
                    query = "Select catgrp, catdesc From @@LIB.catgroup where (cattype = ' ' or cattype = 'RD' or cattype = 'SP')  order by catdesc"
                Else
                    query = "Select catgrp, catdesc From @@LIB.catgroup where (cattype = ' ' or cattype = 'RD')  order by catdesc"
                End If
            Else
                If eventType = "S" Then
                    query = "Select catgrp, catdesc From @@LIB.catgroup where (cattype = ' ' or cattype = 'SP')  order by catdesc"
                Else
                    query = "Select catgrp, catdesc From @@LIB.catgroup where cattype = ' ' order by catdesc"
                End If
            End If

            'Dim sc As OleDbCommand = New OleDbCommand(query, connoledb)
            'sc.CommandText = sc.CommandText.Replace("@@LIB", mvLibrary)
            'Dim da As OleDbDataAdapter = New OleDbDataAdapter(sc)

            Dim sc As iDB2Command = New iDB2Command(query, conn)
            sc.CommandText = sc.CommandText.Replace("@@LIB", mvLibrary)
            Dim dr As iDB2DataReader
            dr = sc.ExecuteReader()

            dt.Load(dr)
            'Dim da As iDB2DataAdapter = New iDB2DataAdapter(sc)

            'da.Fill(dt)
            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function LoadCatalogDataTable(ByVal evdate As String, ByVal evtype As String, ByVal evstate As String) As DataTable
        '-------------------------------------------------------
        'function  : LoadCatalogDataTable
        'Desc. . . : Load the catalog data to web page.
        '-------------------------------------------------------

        Dim dt As New DataTable
        Dim catalog As String = String.Empty

        Try

            Dim query As String = String.Empty

            'determin which catalog to go after this is determined by event type
            If evtype = "W" Then
                catalog = "WHP"
            Else
                If evtype = "N" Then
                    catalog = "SNO"
                Else
                    catalog = "NAT"
                End If
            End If

            'build select for catalog items
            query = "select p.ccudv2, p.cccon#, p.cceffd, p.ccexpd,"
            query += " c.ccnumb, c.cciseq, c.ccpzon, c.cceffl, c.ccexpl, c.cceop, c.ccmqoh, c.ccruom,"
            query += " d.itmdsc, e.ipcost, (case when (g.expitm is not null) then sum(g.expqty * h.ipcost) else e.ipcost End) as unitcost,"
            query += " sum(f.ibqoh-f.ibqal) as avalqty,"
            query += " i.stdust"
            query += " from @@LIB.cstcon p"
            query += " left outer join @@LIB.cstcol c on (c.cccon# = p.cccon#)"
            query += " left outer join @@LIB.itmmst d on (d.itmnum = c.ccnumb)"
            query += " left outer join @@LIB.itmcmp e on (e.ipitm# = d.itmnum and e.ibbrch = '  ')"
            query += " left outer join @@LIB.ibrbin f on (f.ibitm# = d.itmnum)"
            query += " left outer join fsdata50.itmexp g on (g.expitm = d.itmnum)"
            query += " left outer join @@LIB.itmcmp h on (h.ipitm# = g.toitm and h.ibbrch = '  ')"
            query += " left outer join @@LIB.catartp i on (i.stcode = c.ccruom)"
            query += " where (p.ccudv2 = '" + catalog + "') and (p.cceffd < " + evdate + " and p.ccexpd > " + evdate + ") and ((i.stdust = 'XX') or (i.stdust = '" + evstate + "'))"
            query += " group by p.ccudv2, p.cccon#, p.cceffd, p.ccexpd, c.ccnumb, c.cciseq, c.ccpzon, c.cceffl, c.ccexpl, c.cceop, c.ccmqoh, c.ccruom, d.itmdsc, e.ipcost, g.expitm, i.stdust"
            query += " order by c.ccpzon, c.cciseq"

            'Dim sc As OleDbCommand = New OleDbCommand(query, connoledb)
            'sc.CommandText = sc.CommandText.Replace("@@LIB", mvLibrary)
            'Dim da As OleDbDataAdapter = New OleDbDataAdapter(sc)

            Dim sc As iDB2Command = New iDB2Command(query, conn)
            sc.CommandText = sc.CommandText.Replace("@@LIB", mvLibrary)
            Dim dr As iDB2DataReader
            dr = sc.ExecuteReader()

            dt.Load(dr)
            'Dim da As iDB2DataAdapter = New iDB2DataAdapter(sc)

            'da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function GetCoordinatorInfo(ByVal rdnum As String) As String
        '-------------------------------------------------------
        'function  : GetCoordinatorInfo
        'Desc. . . : Get the Coordinators cna-id.
        '-------------------------------------------------------

        Dim coorcnaid As String = String.Empty
        Dim dr As OleDbDataReader
        Try

            Dim query As String = String.Empty

            'build select for catalog items
            query = "select trkduc, trkrd from fscust50.oergtp08"
            query += " where trkrd = '" + rdnum + "'"

            Dim sc As OleDbCommand = New OleDbCommand(query, connoledb)
            'Dim da As OleDbDataAdapter = New OleDbDataAdapter(sc)

            dr = sc.ExecuteReader()

            If dr.Read Then
                'Coordinator CnaID
                coorcnaid = dr("trkduc")
            End If

            Return coorcnaid

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region

#End Region

#Region "Mis Routines"

    Public Function LoadStateDropDown() As DataTable
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

            query = "select stdust, stname, stfly from evlib.evdustp where (stfly<'90') order by stname"

            Dim cmdD As New OleDbCommand(query, connoledb)
            'cmdD.CommandText = cmdD.CommandText.Replace("@@LIB", mvLibrary)

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
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

    Public Function GetLastError() As String
        '---------------------------------------------------------
        'Function: GetLastError
        'Desc. . : Return last error info
        'Parms . : None
        'Returns : Returns a string variable with message
        '---------------------------------------------------------
        Try
            Return mvLastError
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub New()
        '---------------------------------------------------------
        'Function: New
        'Desc. . : This routine gets triggered automatically
        '          when the iSeries object is created.
        '---------------------------------------------------------

        Try

            'Dim aConfig As System.Configuration.ConfigurationSettings
            'Load web application settings from web.config
            mvLibrary = ConfigurationManager.AppSettings("LIBRARY")
            mvLibraryProd = ConfigurationManager.AppSettings("LIBRARYPROD")
            mvConnection = ConfigurationManager.AppSettings("CONNECTION")
            mvConnectioniDB2 = ConfigurationManager.AppSettings("IDB2CONNECTION")

            'Set connection string 
            'connString = connectionString(mvConnection).ToString()
            connStringiDB2 = connectionString(mvConnectioniDB2).ToString()

            ''Open iSeries connection - DB2
            conn = New iDB2Connection(connStringiDB2)
            conn.Open()

            'Open iSeries connection - OLEDB
            'connoledb = New OleDbConnection(connString)
            'connoledb.Open()

        Catch ex As Exception
            mvLastError = ex.Message
        End Try

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Function GetGroupList(ByVal sWhereCriteria As String) As DataTable
        '---------------------------------------------------------
        'Function: Get Group List
        'Desc. . : Get category groups as a DataReader.
        'Parms . : 1.) Group number or blank for all
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select * From @@LIB.catgroup " + sWhereCriteria + "ORDER BY CATDESC"
            Dim cmdD As New OleDbCommand(query, connoledb)
            cmdD.CommandText = cmdD.CommandText.Replace("@@LIB", mvLibrary)

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetGroup(ByVal sCATGRP As String) As DataTable
        '---------------------------------------------------------
        'Function: Get Group 
        'Desc. . : Get category group as a DataReader.
        'Parms . : 1.) Group number
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select * From @@LIB.catgroup WHERE CATGRP = " + sCATGRP
            Dim cmdD As New OleDbCommand(query, connoledb)
            cmdD.CommandText = cmdD.CommandText.Replace("@@LIB", mvLibrary)

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Public Function InsertGroup(ByVal sCatgrp As String, ByVal sCatdesc As String, ByVal sCattype As String) As Boolean
        '---------------------------------------------------------
        'Function: InsertGroup
        'Desc. . : Insert record in CATGROUP file using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try

            'Bail on parm validation
            If sCatgrp.Trim = "" Then
                Throw New Exception("Must pass group number key.")
            End If

            Dim cmd As New OleDbCommand("insert into @@LIB.catgroup (catgrp,catdesc,cattype) values('@@catgrp','@@catdesc','@@cattype')", connoledb)
            cmd.CommandText = cmd.CommandText.Replace("@@LIB", mvLibrary)
            cmd.CommandText = cmd.CommandText.Replace("@@catgrp", sCatgrp)
            cmd.CommandText = cmd.CommandText.Replace("@@catdesc", sCatdesc)
            cmd.CommandText = cmd.CommandText.Replace("@@cattype", sCattype)

            'Execute the record insert
            cmd.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try


    End Function

    Public Function DeleteGroup(ByVal sCatgrp As String) As Boolean
        '---------------------------------------------------------
        'Function: DeletetGroup
        'Desc. . : Delete a record from CATGROUP file using SQL
        'Parms . : 1.) Groupnumber
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try



            Dim cmd As New OleDbCommand("delete from @@LIB.catgroup where catgrp = " & sCatgrp, connoledb)
            cmd.CommandText = cmd.CommandText.Replace("@@LIB", mvLibrary)

            'Execute the record delete
            cmd.ExecuteNonQuery()

            'Delete succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try


    End Function


    Public Function UpdateGroup(ByVal sCatgrp As String, ByVal sCatdesc As String, ByVal sCattype As String) As Boolean
        '---------------------------------------------------------
        'Function: UpdateGroup
        'Desc. . : Update record in CATGROUP file using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try



            Dim cmd As New OleDbCommand("update @@LIB.catgroup SET " & _
            "CATDESC='" & sCatdesc & "'," & _
            "CATTYPE='" & sCattype & "'" & _
            " WHERE CATGRP=" & sCatgrp, connoledb)
            cmd.CommandText = cmd.CommandText.Replace("@@LIB", mvLibrary)
            'cmd.CommandText = cmd.CommandText.Replace("@@catdesc", sCatdesc)
            'cmd.CommandText = cmd.CommandText.Replace("@@cattype", sCattype)

            'Execute the record insert
            cmd.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try


    End Function

    Public Function GetNextGroupCATR003() As String
        '---------------------------------------------------------
        'Function: GetNextGroupICATR003
        'Desc. . : Get Next Group number for catgroup file
        'Parms . :       
        'Returns : Next group number
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try

            'Set default values


            'Set command object and connection info 
            'for the command object to current iSeries OLEDB connection
            oCmd = New OleDb.OleDbCommand
            oCmd.Connection = connoledb

            'Build iSeries Parameter List in top/bottom seq (ordinal)


            'Set up a character parameter rtnmsg as CHAR(3)
            oCmd.Parameters.Add("newgroup", OleDb.OleDbType.Char, 3)
            'Always set parms as input/output for program call
            oCmd.Parameters("newgroup").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("newgroup").Value = " "

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = "{{CALL @@LIB.CATC003 (?)}}"
            oCmd.CommandText = oCmd.CommandText.Replace("@@LIB", mvLibrary)

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Return group number after program call 
            Return oCmd.Parameters("newgroup").Value

        Catch ex As Exception

            'Set class level error message as well
            mvLastError = ex.Message

            Return ""
        End Try

    End Function

#End Region

End Class
