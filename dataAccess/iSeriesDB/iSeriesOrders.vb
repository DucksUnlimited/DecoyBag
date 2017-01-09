Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
Imports System.data
Imports System.Web.HttpContext
Imports System.Configuration

Public Class iSeriesOrders

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("iSeries"))
    Shared mvLastError As String = ""

    Shared Function Get900Charges(ByVal sYear As String, ByVal sLocation As String) As DataTable

        '---------------------------------------------------------
        'Function: Get RD 900 Charges Detail Records 
        'Desc. . : Get records as a DataReader.
        'Parms . : 1.) Fical year
        '          2.) Location 
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = " Select *"
            query += " from  duc900p"
            query += " where (fiscal = @year and glloc = @location)"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@year", iDB2DbType.iDB2Char, 4)
            cmdD.Parameters("@year").Value = sYear
            cmdD.Parameters.Add("@location", iDB2DbType.iDB2Char, 6)
            cmdD.Parameters("@location").Value = sLocation

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetEVOrders() As DataTable

        '---------------------------------------------------------
        'Function: Get New Event Orders from Brandspring 
        'Desc. . : Get records as a DataTable.
        'Parms . : 1.) 
        '          2.)  
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim blankord As String = "        "

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = " Select p.*,  CONCAT(TRIM(c.itmnum),CONCAT(' - ',TRIM(c.itmdsc))) itemdesc"
            query += " from  evorders p"
            query += " left outer join  itmmst c on (c.itmnum = p.itemid)"
            query += " where ordnum =' ' and canid='N'"

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetEVOrdersBCA() As DataTable

        '---------------------------------------------------------
        'Function: Get Event Orders from Brandspring that are in BCA
        'Desc. . : Get records as a DataTable.
        'Parms . : 1.) 
        '          2.)  
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim blankord As String = "        "

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = " Select p.*,  CONCAT(TRIM(c.itmnum),CONCAT(' - ',TRIM(c.itmdsc))) itemdesc"
            query += " from  evorders p"
            query += " left outer join  itmmst c on (c.itmnum = p.itemid)"
            query += " where ordnum <>' ' and canid='N'"

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetVSLines() As DataTable

        '---------------------------------------------------------
        'Function: Get Un-Processed Shipment records from Evternal Vendors 
        'Desc. . : Get records as a DataTable.
        'Parms . : 1.) 
        '          2.)  
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim blankord As String = "        "

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = " Select p.*,  CONCAT(TRIM(c.vcode),CONCAT(' - ',TRIM(c.vname))) itemdesc"
            query += " from  venshipp p"
            query += " left outer join  venshpnmp c on (c.vcode = p.vencode)"
            query += " where p.vstatus = ' '"

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadOrderHeaderOrg(ByVal sOrder As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadOrderHeaderOrg
        'Desc. . : Get order header info for Original Order Number as a DataReader.
        'Parms . : 1.) Order Number
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty

        'Reset last error
        mvLastError = ""

        Try

            query = "select HTNUM, HTMDM4, HTDTE, HTORSC, HTDLBY"
            query += " from  chtmst"
            query += " where htmdm4=@htmdm4"

            Dim cmd As New iDB2Command(query, conn)
            cmd.Parameters.Add("@htmdm4", iDB2DbType.iDB2Char, 8)
            cmd.Parameters("@htmdm4").Value = sOrder.Trim()

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            Dim dr As iDB2DataReader
            dr = cmd.ExecuteReader()

            Return dr

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadOrderHeaderP(ByVal sOrder As String) As iDB2DataReader

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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select p.*, c.approv"
            query += " from chtmst  p"
            query += " left outer join catffl c on (c.fflord = p.htnum)"
            query += " where p.htnum=@htnum"
            'Dim cmd As New iDB2Command("select * from chtmst where htnum=@htnum", conn)
            Dim cmd As New iDB2Command(query, conn)
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

    Shared Function LoadOrderDetail(ByVal sOrder As String) As iDB2DataReader
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

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "Select htlpnm, htldsc, htqor, htluct, cs33ec From  chtqlin where htnum=@htnum"
            Dim cmdD As New iDB2Command(query, conn)
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

    Shared Function LoadUPSTracking(ByVal sOrder As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadUPSTracking
        'Desc. . : Build a data table of tracking numbers for an order
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim dtV As New DataTable
        Dim upsFound As Boolean = False

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            Dim dc1 As New DataColumn("utrack", GetType(String))
            Dim dc2 As New DataColumn("utser", GetType(String))
            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)

            Dim cmd As New iDB2Command("select utrack, utser from upstgtwl2 where ucid=@htnum", conn)
            cmd.Parameters.Add("@htnum", iDB2DbType.iDB2Char, 30)
            cmd.Parameters("@htnum").Value = sOrder

            Dim dr As iDB2DataReader
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                dt.Load(dr)
                upsFound = True
            End If
            dr.Close()

            Dim cmd2 As New iDB2Command("select vtracnum from venshipp where vordnum=@ornum", conn)
            cmd2.Parameters.Add("@ornum", iDB2DbType.iDB2Char, 8)
            cmd2.Parameters("@ornum").Value = sOrder

            Dim drV As iDB2DataReader
            drV = cmd2.ExecuteReader()
            If drV.HasRows Then
                dtV.Load(drV)
                For Each row As DataRow In dtV.Rows
                    Dim R As DataRow = dt.NewRow
                    R("utrack") = row("vtracnum")
                    R("utser") = " "
                    dt.Rows.Add(R)
                Next row
                upsFound = True
            End If
            If Not upsFound Then
                Dim drw As DataRow
                drw = dt.NewRow
                drw("utrack") = "No Tracking Information Found for this Order"
                drw("utser") = " "
                dt.Rows.Add(drw)
            End If

            drV.Close()

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadOrderDetailP(ByVal sOrder As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadOrderDetailP
        'Desc. . : Get order details as a DataTable.
        'Parms . : 1.) Order Number
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

            query = "select p.htnum, p.htllin, p.htqor, p.htlqsh, p.htlqbo, p.htlun, p.htlpnm, p.htldsc, p.htluct, c.htlser"
            query += " from chtlin  p"
            query += " left outer join chtser c on (c.htnum = p.htnum and c.htllin = p.htllin)"
            query += " where p.htnum=@htnum"

            Dim cmdD As New iDB2Command(query, conn)
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

    Shared Function LoadCustomer(ByVal sChapter As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadCustomer
        'Desc. . : Get chapter billto/shipto info as a DataReader.
        'Parms . : 1.) Chapter Number
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim dr As iDB2DataReader

        'Reset last error
        mvLastError = ""

        Try

            Dim cmd As New iDB2Command("select * from cstmst where cstnum='" & "     " & sChapter & "'", conn)

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            dr = cmd.ExecuteReader()

            Return dr

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function SaveOrder(ByVal DTOrder As DataTable, ByVal EventID As String, ByVal EventDate As String, ByVal UserID As String, _
        ByVal sShpnam As String, ByVal sShpad1 As String, ByVal sShpad2 As String, ByVal sShpcty As String, ByVal sShpzip5 As String, ByVal sShpzip4 As String, _
        ByVal fflorg As String, ByVal fflattn As String, ByVal ffladdr As String, ByVal fflcity As String, ByVal fflst As String, ByVal fflzip5 As String, ByVal fflzip4 As String, ByVal fflnum As String, ByVal fflexp As String, _
        ByVal catema As String, ByVal catcom As String, ByVal approv As String, ByVal sTotcost As Double, ByVal sCatName As String, ByVal uOrderNumber As String) As String

        Try

            'parms = System.Web.HttpContext.Current.Session("oSessionManager")

            'Insert order header records with CATR001 call
            Dim sOrder As String
            sOrder = InsertOrderHeaderCATR001(EventID, EventDate, UserID, _
                sShpnam, sShpad1, sShpad2, sShpcty, sShpzip5, sShpzip4, _
                fflorg, fflattn, ffladdr, fflcity, fflst, fflzip5, fflzip4, fflnum, fflexp, _
                catema, catcom, approv, sTotcost, uOrderNumber)
            If sOrder.Trim = "" Then
                Throw New Exception("ORDER NOT COMPLETED! An error has occured in completing your order. Please contact your RD with Chapter & Event information, also try to place your order again after 10:00pm central time.")
            End If

            'Loop to insert each detail line with call to CATR002
            Dim ct As Long
            For ct = 0 To DTOrder.Rows.Count - 1
                InsertOrderDetailCATR002(sOrder, ct + 1, DTOrder.Rows(ct).Item("ProductID"), DTOrder.Rows(ct).Item("Qty"), sCatName, EventID)
            Next ct

            'Send back order number if completed.
            Return sOrder

        Catch ex As Exception
            'Set class level error message as well
            mvLastError = ex.Message

            Return "--------"
        End Try

    End Function

    Shared Function InsertOrderDetailCATR002(ByVal sHtnum As String, ByVal sHtllin As Integer, ByVal sHtlpnm As String, ByVal sHtqor As Double, ByVal sCatName As String, ByVal sEvtPo As String) As Boolean
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
            'for the command object to current iSeries iDB2 connection
            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("htnum", iDB2DbType.iDB2Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("htnum").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("htnum").Value = sHtnum

            'Set up a numeric parameter amount for as DECIMAL(3.0)
            oCmd.Parameters.Add("htllin", iDB2DbType.iDB2Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("htllin").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("htllin").Precision = 3
            'Set number of decimal positions
            oCmd.Parameters("htllin").Scale = 0
            'Set parm value
            oCmd.Parameters("htllin").Value = sHtllin

            'Set up a character parameter rtnmsg as CHAR(20)
            oCmd.Parameters.Add("htlpnm", iDB2DbType.iDB2Char, 20)
            'Always set parms as input/output for program call
            oCmd.Parameters("htlpnm").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("htlpnm").Value = sHtlpnm

            'Set up a numeric parameter amount for as DECIMAL(7.0)
            oCmd.Parameters.Add("htqor", iDB2DbType.iDB2Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("htqor").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("htqor").Precision = 7
            'Set number of decimal positions
            oCmd.Parameters("htqor").Scale = 0
            'Set parm value
            oCmd.Parameters("htqor").Value = sHtqor

            'Set up a character parameter rtnmsg as CHAR(20)
            oCmd.Parameters.Add("cs33sn", iDB2DbType.iDB2Char, 20)
            'Always set parms as input/output for program call
            oCmd.Parameters("cs33sn").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("cs33sn").Value = sCatName

            'Set up a character parameter rtnmsg as CHAR(7)
            oCmd.Parameters.Add("stchp", iDB2DbType.iDB2Char, 7)
            'Always set parms as input/output for program call
            oCmd.Parameters("stchp").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("stchp").Value = sEvtPo

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CATC002"
            'oCmd.CommandText = oCmd.CommandText.Replace("@@LIB", mvLibraryPgm)

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

    Shared Function InsertOrderHeaderCATR001(ByVal EventID As String, ByVal EventDate As String, ByVal UserID As String, _
        ByVal sShpnam As String, ByVal sShpad1 As String, ByVal sShpad2 As String, ByVal sShpcty As String, ByVal sShpzip5 As String, ByVal sShpzip4 As String, _
        ByVal fflorg As String, ByVal fflattn As String, ByVal ffladdr As String, ByVal fflcity As String, ByVal fflst As String, ByVal fflzip5 As String, ByVal fflzip4 As String, ByVal fflnum As String, ByVal fflexp As String, _
        ByVal catema As String, ByVal catcom As String, ByVal approv As String, ByVal sTotcost As Double, ByVal uOrderNumber As String) As String
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
        '         17.) Order Creator's eMail address
        '         18.) Order comments
        '         19.) Total cost of order
        '         20.) Update order number
        '         21.) New order number - returned
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

            'parms = System.Web.HttpContext.Current.Session("oSessionManager")

            'Set command object and connection info 
            'for the command object to current iSeries iDB2 connection
            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter rtnmsg as CHAR(7)
            oCmd.Parameters.Add("@evtpo", iDB2DbType.iDB2Char, 7)
            'Always set parms as input/output for program call
            oCmd.Parameters("@evtpo").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@evtpo").Value = EventID

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("@evdate", iDB2DbType.iDB2Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("@evdate").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@evdate").Value = EventDate

            'Set up a character parameter rtnmsg as CHAR(10)
            oCmd.Parameters.Add("@volnam", iDB2DbType.iDB2Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("@volnam").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@volnam").Value = UserID

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("@shpnam", iDB2DbType.iDB2Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("@shpnam").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@shpnam").Value = sShpnam

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("@shpad1", iDB2DbType.iDB2Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("@shpad1").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@shpad1").Value = sShpad1

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("@shpad2", iDB2DbType.iDB2Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("@shpad2").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@shpad2").Value = sShpad2

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("@shpcty", iDB2DbType.iDB2Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("@shpcty").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@shpcty").Value = sShpcty

            'Set up a character parameter rtnmsg as CHAR(5)
            oCmd.Parameters.Add("@shpzip5", iDB2DbType.iDB2Char, 5)
            'Always set parms as input/output for program call
            oCmd.Parameters("@shpzip5").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@shpzip5").Value = sShpzip5

            'Set up a character parameter rtnmsg as CHAR(4)
            oCmd.Parameters.Add("@shpzip4", iDB2DbType.iDB2Char, 4)
            'Always set parms as input/output for program call
            oCmd.Parameters("@shpzip4").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@shpzip4").Value = sShpzip4

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("@fflorg", iDB2DbType.iDB2Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("@fflorg").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@fflorg").Value = fflorg

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("@fflattn", iDB2DbType.iDB2Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("@fflattn").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@fflattn").Value = fflattn

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("@ffladdr", iDB2DbType.iDB2Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("@ffladdr").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@ffladdr").Value = ffladdr

            'Set up a character parameter rtnmsg as CHAR(30)
            oCmd.Parameters.Add("@fflcity", iDB2DbType.iDB2Char, 30)
            'Always set parms as input/output for program call
            oCmd.Parameters("@fflcity").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@fflcity").Value = fflcity

            'Set up a character parameter rtnmsg as CHAR(2)
            oCmd.Parameters.Add("@fflst", iDB2DbType.iDB2Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("@fflst").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@fflst").Value = fflst

            'Set up a character parameter rtnmsg as CHAR(5)
            oCmd.Parameters.Add("@fflzip5", iDB2DbType.iDB2Char, 5)
            'Always set parms as input/output for program call
            oCmd.Parameters("@fflzip5").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@fflzip5").Value = fflzip5

            'Set up a character parameter rtnmsg as CHAR(4)
            oCmd.Parameters.Add("@fflzip4", iDB2DbType.iDB2Char, 4)
            'Always set parms as input/output for program call
            oCmd.Parameters("@fflzip4").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@fflzip4").Value = fflzip4

            'Set up a character parameter rtnmsg as CHAR(20)
            oCmd.Parameters.Add("@fflnum", iDB2DbType.iDB2Char, 20)
            'Always set parms as input/output for program call
            oCmd.Parameters("@fflnum").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@fflnum").Value = fflnum

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("@fflexp", iDB2DbType.iDB2Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("@fflexp").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@fflexp").Value = fflexp

            'Set up a character parameter rtnmsg as CHAR(100)
            oCmd.Parameters.Add("@email", iDB2DbType.iDB2Char, 100)
            'Always set parms as input/output for program call
            oCmd.Parameters("@email").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@email").Value = catema

            'Set up a character parameter rtnmsg as CHAR(500)
            oCmd.Parameters.Add("@comnt", iDB2DbType.iDB2Char, 500)
            'Always set parms as input/output for program call
            oCmd.Parameters("@comnt").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@comnt").Value = catcom

            'Set up a character parameter rtnmsg as CHAR(1)
            oCmd.Parameters.Add("@approv", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("@approv").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@approv").Value = approv

            'Set up a numeric parameter amount for as DECIMAL(11.2)
            oCmd.Parameters.Add("@totcost", iDB2DbType.iDB2Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("@totcost").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("@totcost").Precision = 11
            'Set number of decimal positions
            oCmd.Parameters("@totcost").Scale = 2
            'Set parm value
            oCmd.Parameters("@totcost").Value = sTotcost

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("@neword", iDB2DbType.iDB2Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("@neword").Direction = ParameterDirection.InputOutput
            'Set parm value
            If uOrderNumber Is Nothing Or uOrderNumber = "" Then
                oCmd.Parameters("@neword").Value = " "
            Else
                oCmd.Parameters("@neword").Value = uOrderNumber
            End If

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CATC001"

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Return order number after program call 
            Return oCmd.Parameters("@neword").Value

        Catch exiDB2 As iDB2Exception
            mvLastError = exiDB2.Message + exiDB2.MessageCode.ToString
            Return Nothing

        Catch ex As Exception

            'Set class level error message as well
            mvLastError = ex.Message

            Return "--------"
        End Try

    End Function

    Shared Function GetReturnRecords(ByVal sStartDate As String, ByVal sEndDate As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Return Records from order files in BCA
        'Parms . : 1.) Start date
        '          2.) End date
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        'Dim seq As String = "000000001"


        'Reset last error
        mvLastError = ""

        Try

            query = "select p.htlpnm, p.htldsc, p.cs33fo, p.htnum,"
            query += " c.htdte, c.htcno, c.htorby,"
            query += " d.slsnm,"
            query += " e.mssage"
            'query += " from  chtlin p"
            query += " from  chtitm6 p"
            query += " left outer join  chtmst c on (c.htnum = p.htnum)"
            query += " left outer join  slsman d on (d.slmnno = c.htslm1)"
            query += " left outer join ordnote e on (e.htnum = p.htnum and e.onseq = 1)"
            'query += " where (p.cs33fo = @rem1 or p.cs33fo = @rem2 or p.cs33fo = @rem3) and c.htrcfl = @status and c.htdte >= @sdate and c.htdte <= @edate"
            query += " where c.htrcfl = @status and c.htdte >= @sdate and c.htdte <= @edate"

            query += " order by p.htlpnm"


            Dim cmdD As New iDB2Command(query, conn)
            'cmdD.Parameters.Add("@rem1", iDB2DbType.iDB2Char, 2)
            'cmdD.Parameters("@rem1").Value = "DM"
            'cmdD.Parameters.Add("@rem2", iDB2DbType.iDB2Char, 2)
            'cmdD.Parameters("@rem2").Value = "NS"
            'cmdD.Parameters.Add("@rem3", iDB2DbType.iDB2Char, 2)
            'cmdD.Parameters("@rem3").Value = "WI"
            cmdD.Parameters.Add("@status", iDB2DbType.iDB2Char, 1)
            cmdD.Parameters("@status").Value = "B"
            cmdD.Parameters.Add("@sStartDate", iDB2DbType.iDB2Char, 8)
            cmdD.Parameters("@sStartDate").Value = sStartDate
            cmdD.Parameters.Add("@sEndDate", iDB2DbType.iDB2Char, 8)
            cmdD.Parameters("@sEndDate").Value = sEndDate
            'cmdD.Parameters.Add("@seq", iDB2DbType.iDB2Char, 9)
            'cmdD.Parameters("@seq").Value = seq
            'cmdD.Parameters.Add("@qty", iDB2DbType.iDB2Char, 7)
            'cmdD.Parameters("@qty").Value = "0"

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function
    Shared Function LoadQuotOrderListALL() As DataTable
        '---------------------------------------------------------
        'Function: LoadQuotOrderListALL
        'Desc. . : Get all quoted order header information as a DataTable.
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try
            query = "select p.htnum, p.htcno, p.htcpo, p.htdte, p.htslm1, p.htorby, p.htcodt, p.htmdm4,"
            query += " c.approv"
            query += " from  chtqrd p"
            query += " left outer join  catffl c on (c.fflord = p.htnum)"
            query += " order by p.htnum"


            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadHeldOrderListALL() As DataTable
        '---------------------------------------------------------
        'Function: LoadHeldOrderListALL
        'Desc. . : Get all held order header information as a DataTable.
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try
            query = "select htnum, htcno, htcpo, htdte, htslm1, htorby, htcodt, htmdm4, htmff1"
            query += " from  chtmstl"
            query += " order by htnum"


            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function


    Shared Function DeleteEvOrders(ByVal evid As String, ByVal evdate As String, ByVal itemid As String) As Boolean
        '---------------------------------------------------------
        'Function: DeleteEvOrders
        'Desc. . : Delete a record from EVORDERS
        'Parms . : Event Id
        '        : Event Date
        '        : Item Id
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

            query = "delete from evorders where (evid ='" & evid & "' and evdate =" & evdate & " and itemid ='" & itemid & "')"
            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateEvOrders(ByVal evid As String, ByVal evdate As String, ByVal itemid As String, ByVal newitem As String, ByVal newquantity As String) As Boolean
        '---------------------------------------------------------
        'Function: UpdateEvOrders
        'Desc. . : Updates the information about an event in EVORDERS.
        'Parms . : Event ID
        '          Event Date
        '          Original Item ID
        '          New Item ID
        '          New Quantity
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

            query = "update evorders set "
            query += "itemid='" & newitem & "', "
            query += "itemqty=" & newquantity & " "
            query += "where (evid='" & evid & "' and evdate=" & evdate & " and itemid='" & itemid & "')"

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateVSOrders(ByVal vencode As String, ByVal vordnum As String, ByVal vtracnum As String, ByVal newordnum As String) As Boolean
        '---------------------------------------------------------
        'Function: UpdateVSOrders
        'Desc. . : Updates the information about an external vendor shipment in VENSHIPP.
        'Parms . : Vendor Code
        '          Event Order Number
        '          UPS Tracking Number
        '          New Order Number
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

            query = "update venshipp set "
            query += "vordnum=" & newordnum & " "
            query += "where (vencode='" & vencode & "' and vordnum=" & vordnum & " and vtracnum='" & vtracnum & "')"

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function BuildEventOrders(ByVal dtH As DataTable, ByVal dtD As DataTable) As Boolean

        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As Boolean

        Try

            Dim sOrder As String
            sOrder = InsertEventOrderHeader(dtH.Rows(0).Item("evid"), dtH.Rows(0).Item("evdate"), dtH.Rows(0).Item("whrse"), dtH.Rows(0).Item("user"))
            If sOrder.Trim = "" Then
                Throw New Exception("Order number error.")
            End If

            'Loop to insert each detail line with call to CATR002
            Dim ct As Long
            For ct = 0 To dtD.Rows.Count - 1
                rc = InsertEventOrderDetail(sOrder, ct + 1, dtD.Rows(ct).Item("itemid"), dtH.Rows(0).Item("whrse"), dtD.Rows(ct).Item("quantity"))
            Next ct

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "update evorders set "
            query += "ordnum='" & sOrder & "' "
            query += "where (evid='" & dtH.Rows(0).Item("orgevid") & "' and evdate=" & dtH.Rows(0).Item("evdate") & ")"

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return True

        Catch ex As Exception

            mvLastError = ex.Message
            Return False

        End Try

    End Function

    Shared Function InsertEventOrderHeader(ByVal EventID As String, ByVal EventDate As String, ByVal WareHouse As String, ByVal UserID As String) As String
        '---------------------------------------------------------
        'Function: InsertEventOrderHeader
        'Desc. . : Insert Event order header using program CATR001
        'Parms . : 1.) Event ID
        '          2.) Event date
        '          3.) Ware House 
        '          4.) User ID for Add
        'Returns : New order number
        '---------------------------------------------------------


        'Reset last error
        mvLastError = ""

        Try

            'Set command object and connection info 
            'for the command object to current iSeries iDB2 connection
            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter rtnmsg as CHAR(7)
            oCmd.Parameters.Add("@evtpo", iDB2DbType.iDB2Char, 7)
            'Always set parms as input/output for program call
            oCmd.Parameters("@evtpo").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@evtpo").Value = EventID

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("@evdate", iDB2DbType.iDB2Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("@evdate").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@evdate").Value = EventDate

            'Set up a character parameter rtnmsg as CHAR(2)
            oCmd.Parameters.Add("@warehouse", iDB2DbType.iDB2Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("@warehouse").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@warehouse").Value = WareHouse

            'Set up a character parameter rtnmsg as CHAR(10)
            oCmd.Parameters.Add("@volnam", iDB2DbType.iDB2Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("@volnam").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("@volnam").Value = UserID

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("@neword", iDB2DbType.iDB2Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("@neword").Direction = ParameterDirection.InputOutput
            'Set parm value
            'If uOrderNumber Is Nothing Or uOrderNumber = "" Then
            oCmd.Parameters("@neword").Value = " "
            'Else
            'oCmd.Parameters("@neword").Value = uOrderNumber
            'End If

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "EVTR001"

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Return order number after program call 
            Return oCmd.Parameters("@neword").Value

        Catch exiDB2 As iDB2Exception
            mvLastError = exiDB2.Message + exiDB2.MessageCode.ToString
            Return Nothing

        Catch ex As Exception

            'Set class level error message as well
            mvLastError = ex.Message

            Return ""
        End Try

    End Function

    Shared Function InsertEventOrderDetail(ByVal sHtnum As String, ByVal sHtllin As Integer, ByVal sHtlpnm As String, ByVal sordnum As String, ByVal sHtqor As Double) As Boolean
        '---------------------------------------------------------
        'Function: InsertEventOrderDetail
        'Desc. . : Insert Event order detail using program EVTR002
        'Parms . : 1.) Order number
        '          2.) Line number
        '          3.) Item number 
        '          4.) Quantity ordered
        '---------------------------------------------------------

        'Reset last error
        mvLastError = ""

        Try

            'Set command object and connection info 
            'for the command object to current iSeries iDB2 connection
            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter rtnmsg as CHAR(8)
            oCmd.Parameters.Add("htnum", iDB2DbType.iDB2Char, 8)
            'Always set parms as input/output for program call
            oCmd.Parameters("htnum").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("htnum").Value = sHtnum

            'Set up a numeric parameter amount for as DECIMAL(3.0)
            oCmd.Parameters.Add("htllin", iDB2DbType.iDB2Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("htllin").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("htllin").Precision = 3
            'Set number of decimal positions
            oCmd.Parameters("htllin").Scale = 0
            'Set parm value
            oCmd.Parameters("htllin").Value = sHtllin

            'Set up a character parameter rtnmsg as CHAR(20)
            oCmd.Parameters.Add("htlpnm", iDB2DbType.iDB2Char, 20)
            'Always set parms as input/output for program call
            oCmd.Parameters("htlpnm").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("htlpnm").Value = sHtlpnm

            'Set up a character parameter rtnmsg as CHAR(2)
            oCmd.Parameters.Add("ordwh", iDB2DbType.iDB2Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("ordwh").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("ordwh").Value = sordnum

            'Set up a numeric parameter amount for as DECIMAL(7.0)
            oCmd.Parameters.Add("htqor", iDB2DbType.iDB2Decimal)
            'Always set parms as input/output for program call
            oCmd.Parameters("htqor").Direction = ParameterDirection.InputOutput
            'Set number of digits
            oCmd.Parameters("htqor").Precision = 7
            'Set number of decimal positions
            oCmd.Parameters("htqor").Scale = 0
            'Set parm value
            oCmd.Parameters("htqor").Value = sHtqor


            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "EVTR002"
            'oCmd.CommandText = oCmd.CommandText.Replace("@@LIB", mvLibraryPgm)

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
