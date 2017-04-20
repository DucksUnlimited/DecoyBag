Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
Imports System.data
Imports System.Web.HttpContext
Imports System.Configuration

Public Class iSeriesCatalog

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("CatiSeries"))
    Shared mvLastError As String = ""

    Shared Function ChkQuotOrderRD(ByVal rdNum As String) As Boolean
        '---------------------------------------------------------
        'Function: ChkQuotOrderRD
        'Desc. . : Check to see if any quoted orders exist and return True/False
        'Parms . : 1.) RD Number
        'Returns : Returns a Boolean.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim rc As Boolean

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "select p.htnum, p.htcpo, p.htslm1, p.htrcfl, p.htcodt,"
            query += " c.catevdesc, c.catevpo,"
            query += " d.approv,"
            query += " CONCAT(c.catevdesc,CONCAT(' - ',p.htnum)) dropname"
            query += " from  chtqrd p"
            query += " left outer join  catevname c on (c.catevpo = p.htcpo)"
            query += " left outer join  catffl d on (d.fflord = p.htnum)"
            query += " where p.htslm1=@rdNum"
            query += " order by c.catevdesc, p.htcpo, p.htnum"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@rdNum", iDB2DbType.iDB2Char, 3)
            cmdD.Parameters("@rdNum").Value = "0" & rdNum

            Dim dr As iDB2DataReader
            dr = cmdD.ExecuteReader()

            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                rc = True
            Else
                rc = False
            End If

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetNextGroupCATR003() As String
        '---------------------------------------------------------
        'Function: GetNextGroupCATR003
        'Desc. . : Get Next Group number for catgroup file
        'Parms . :       
        'Returns : Next group number
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            'Set default values

            'Set command object and connection info 
            'for the command object to current iSeries OLEDB connection
            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Build iSeries Parameter List in top/bottom seq (ordinal)


            'Set up a character parameter rtnmsg as CHAR(3)
            oCmd.Parameters.Add("newgroup", iDB2DbType.iDB2Char, 3)
            'Always set parms as input/output for program call
            oCmd.Parameters("newgroup").Direction = ParameterDirection.InputOutput
            'Set parm value
            oCmd.Parameters("newgroup").Value = " "

            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "CATC003"

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

    Shared Function GetCoordinatorInfo(ByVal rdnum As String) As String
        '-------------------------------------------------------
        'function  : GetCoordinatorInfo
        'Desc. . . : Get the Coordinators cna-id.
        '-------------------------------------------------------

        Dim coorcnaid As String = String.Empty
        Dim dr As iDB2DataReader

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Try

            Dim query As String = String.Empty

            'build select for catalog items
            query = "select trkduc, trkrd from oergtp08"
            query += " where trkrd = '" + rdnum + "'"

            Dim sc As iDB2Command = New iDB2Command(query, conn)

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

    Shared Function GetCat(ByVal sCCCON As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Category 
        'Desc. . : Get category as a DataReader.
        'Parms . : 1.) Category name
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select cccon#, cceffd, ccexpd, ccudv2 From  cstcon WHERE CCCON# = @cccon# and CCCTYP = @ccctyp and CCCUST = @cccust"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@cccon#", iDB2DbType.iDB2Char, 20)
            cmdD.Parameters("@cccon#").Value = sCCCON
            cmdD.Parameters.Add("@ccctyp", iDB2DbType.iDB2Char, 1)
            cmdD.Parameters("@ccctyp").Value = "A"
            cmdD.Parameters.Add("@cccust", iDB2DbType.iDB2Char, 10)
            cmdD.Parameters("@cccust").Value = "          "

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetCatItemList(ByVal sCCCON As String, ByVal sEXPIRED As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Catalog Item List
        'Desc. . : Get catalog detail records as a DataReader.
        'Parms . : 1.) Catalog name
        '          2.) Expired indicator
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        'Dim wkdate = DateTime.Today.AddMonths(-6).ToString("yyyyMMdd")
        Dim wkdate = DateTime.Today.ToString("yyyyMMdd")


        'Reset last error
        mvLastError = ""

        Try

            If sEXPIRED = "0" Then

                query = "select p.ccnumb, p.cceffl, p.ccexpl, p.cciseq,"
                query += " c.catdesc,"
                query += " d.itmdsc,"
                query += " sum(e.ibqoh-e.ibqal) as avalqty"
                query += " from  cstcol p"
                query += " left outer join  catgroup c on (c.catgrp = p.ccpzon)"
                query += " left outer join  itmmst d on (d.itmnum = p.ccnumb)"
                query += " left outer join ibrbin e on (e.ibitm# = d.itmnum"
                query += " and (e.ibbr# = @whs1 or e.ibbr# = @whs2 or e.ibbr# = @whs3 or e.ibbr# = @whs4 or e.ibbr# = @whs5"
                query += " or e.ibbr# = @whs6 or e.ibbr# = @whs7 or e.ibbr# = @whs8 or e.ibbr# = @whs9 or e.ibbr# = @whs10))"
                query += " where p.ccctyp = @ccctyp and p.cccust = @cccust and p.cccon# = @cccon# and p.ccexpl > @wkdate"
                query += " group by p.ccnumb, p.cceffl, p.ccexpl, p.cciseq, c.catdesc, d.itmdsc"
                query += " order by p.cciseq"

            Else

                query = "select p.ccnumb, p.cceffl, p.ccexpl, p.cciseq,"
                query += " c.catdesc,"
                query += " d.itmdsc,"
                query += " sum(e.ibqoh-e.ibqal) as avalqty"
                query += " from  cstcol p"
                query += " left outer join  catgroup c on (c.catgrp = p.ccpzon)"
                query += " left outer join  itmmst d on (d.itmnum = p.ccnumb)"
                query += " left outer join ibrbin e on (e.ibitm# = d.itmnum"
                query += " and (e.ibbr# = @whs1 or e.ibbr# = @whs2 or e.ibbr# = @whs3 or e.ibbr# = @whs4 or e.ibbr# = @whs5"
                query += " or e.ibbr# = @whs6 or e.ibbr# = @whs7 or e.ibbr# = @whs8 or e.ibbr# = @whs9 or e.ibbr# = @whs10))"
                query += " where p.ccctyp = @ccctyp and p.cccust = @cccust and p.cccon# = @cccon# and p.ccexpl <= @wkdate"
                query += " group by p.ccnumb, p.cceffl, p.ccexpl, p.cciseq, c.catdesc, d.itmdsc"
                query += " order by p.cciseq"

            End If

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@ccctyp", iDB2DbType.iDB2Char, 1)
            cmdD.Parameters("@ccctyp").Value = "A"
            cmdD.Parameters.Add("@cccust", iDB2DbType.iDB2Char, 10)
            cmdD.Parameters("@cccust").Value = "    "
            cmdD.Parameters.Add("@cccon#", iDB2DbType.iDB2Char, 20)
            cmdD.Parameters("@cccon#").Value = sCCCON
            cmdD.Parameters.Add("@wkdate", iDB2DbType.iDB2Char, 8)
            cmdD.Parameters("@wkdate").Value = wkdate
            cmdD.Parameters.Add("@whs1", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@whs1").Value = "DU"
            cmdD.Parameters.Add("@whs2", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@whs2").Value = "D1"
            cmdD.Parameters.Add("@whs3", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@whs3").Value = "OW"
            cmdD.Parameters.Add("@whs4", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@whs4").Value = "BI"
            cmdD.Parameters.Add("@whs5", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@whs5").Value = "OS"
            cmdD.Parameters.Add("@whs6", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@whs6").Value = "VC"
            cmdD.Parameters.Add("@whs7", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@whs7").Value = "HQ"
            cmdD.Parameters.Add("@whs8", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@whs8").Value = "CE"
            cmdD.Parameters.Add("@whs9", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@whs9").Value = "Sw"
            cmdD.Parameters.Add("@whs10", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@whs10").Value = "FA"

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetCatList() As DataTable

        '---------------------------------------------------------
        'Function: Get Category List
        'Desc. . : Get category header records as a DataReader.
        'Parms . : 1.) Category name
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select cccon#, cceffd, ccexpd, ccudv2 From  cstcon ORDER BY CCCON#"
            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetEventInvSum() As DataTable
        '-----------------------------------------------
        'Function: 
        'Desc . .: Build a file that lists Item Inventory
        'Parms. .: None
        '
        'Returns : Returns a data table object.

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

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

    Shared Function GetGroupList() As DataTable
        '---------------------------------------------------------
        'Function: Get Group List
        'Desc. . : Get category groups as a DataReader.
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

            query = "Select * From  catgroup ORDER BY CATDESC"

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try
    End Function

    Shared Function GetGroup(ByVal sCATGRP As String) As DataTable
        '---------------------------------------------------------
        'Function: Get Group 
        'Desc. . : Get category group as a DataReader.
        'Parms . : 1.) Group number
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

            query = "Select * From  catgroup WHERE CATGRP = " + sCATGRP

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetItem(ByVal sCCCON As String, ByVal sCCNUMB As String, ByVal sCCEFFL As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Catalog Item 
        'Desc. . : Get item as a DataReader.
        'Parms . : 1.) Catalog name
        '          2.) Item 
        '          3.) Effective date
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select p.ccnumb, p.cceffl, p.ccexpl, p.ccpzon, p.ccruom, p.cceop, p.ccmqoh, p.cciseq,"
            query += " c.catdesc, c.catgrp"
            query += " from  cstcol p"
            query += " left outer join  catgroup c on (c.catgrp = p.ccpzon)"
            query += " where p.cccon# = @cccon# and p.ccctyp = @ccctyp and p.cccust = @cccust and p.ccitct = @ccitct and p.ccnumb = @ccnumb and p.cceffl = @cceffl"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@cccon#", iDB2DbType.iDB2Char, 20)
            cmdD.Parameters("@cccon#").Value = sCCCON
            cmdD.Parameters.Add("@ccctyp", iDB2DbType.iDB2Char, 1)
            cmdD.Parameters("@ccctyp").Value = "A"
            cmdD.Parameters.Add("@cccust", iDB2DbType.iDB2Char, 10)
            cmdD.Parameters("@cccust").Value = "          "
            cmdD.Parameters.Add("@ccitct", iDB2DbType.iDB2Char, 1)
            cmdD.Parameters("@ccitct").Value = "I"
            cmdD.Parameters.Add("@ccnumb", iDB2DbType.iDB2Char, 20)
            cmdD.Parameters("@ccnumb").Value = sCCNUMB
            cmdD.Parameters.Add("@cceffl", iDB2DbType.iDB2Char, 8)
            cmdD.Parameters("@cceffl").Value = sCCEFFL

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetItemDesc(ByVal itemnum As String) As String
        '-------------------------------------------------------
        'Desc. . . : Get the unit of measure for catalog item
        '                 and verify item number is valid
        '-------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim itemdesc As String = String.Empty
        Dim dr As iDB2DataReader
        Try

            Dim query As String = String.Empty

            'build select for item
            query = "select itmdsc from  itmmst"
            query += " where itmnum = '" + itemnum + "'"

            Dim sc As iDB2Command = New iDB2Command(query, conn)

            dr = sc.ExecuteReader()

            If dr.Read Then
                'Item unit of measure
                itemdesc = dr("itmdsc")
            End If

            Return itemdesc

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Shared Function GetItemUOM(ByVal itemnum As String) As String
        '-------------------------------------------------------
        'Desc. . . : Get the unit of measure for catalog item
        '                 and verify item number is valid
        '-------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim itemuom As String = String.Empty
        Dim dr As iDB2DataReader
        Try

            Dim query As String = String.Empty

            'build select for item
            query = "select unit from  itmmst"
            query += " where itmnum = '" + itemnum + "'"

            Dim sc As iDB2Command = New iDB2Command(query, conn)

            dr = sc.ExecuteReader()

            If dr.Read Then
                'Item unit of measure
                itemuom = dr("unit")
            End If

            Return itemuom

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Shared Function GetOrder(ByVal htNum As String) As DataTable
        '---------------------------------------------------------
        'Function: GetOrderList
        'Desc. . : Get order information.
        'Parms . : 1.) Order number
        'Returns : Returns a data table.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        query = "select HTNUM, HTMDM4 from chtmst where htnum='" + htNum + "'"

        Dim cmd As New iDB2Command(query, conn)

        Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmd)
        da.Fill(dt)

        Return dt

    End Function

    Shared Function GetOrderList(ByVal htmdm4 As String) As DataTable
        '---------------------------------------------------------
        'Function: GetOrderList
        'Desc. . : Get list of orders.
        'Parms . : 1.) Order number
        'Returns : Returns a data table.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        query = "select HTNUM, HTMDM4, HTDTE, HTORSC, HTDLBY "
        query += "from chtmst "
        query += "where htmdm4='" + htmdm4 + "' "
        query += "order by htnum"

        Dim cmd As New iDB2Command(query, conn)

        Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmd)
        da.Fill(dt)

        Return dt

    End Function

    Shared Function GetReturnRecords(ByVal sStartDate As String, ByVal sEndDate As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Return Records from order files in BCA
        'Parms . : 1.) Start date
        '          2.) End date
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "select p.htlpnm, p.htldsc, p.cs33fo, p.htnum,"
            query += " c.htdte, c.htcno, c.htorby,"
            query += " d.slsnm,"
            query += " e.mssage"
            query += " from  chtitm6 p"
            query += " left outer join  chtmst c on (c.htnum = p.htnum)"
            query += " left outer join  slsman d on (d.slmnno = c.htslm1)"
            query += " left outer join ordnote e on (e.htnum = p.htnum and e.onseq = 1)"
            query += " where c.htrcfl = @status and c.htdte >= @sdate and c.htdte <= @edate"

            query += " order by p.htlpnm"


            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@status", iDB2DbType.iDB2Char, 1)
            cmdD.Parameters("@status").Value = "B"
            cmdD.Parameters.Add("@sStartDate", iDB2DbType.iDB2Char, 8)
            cmdD.Parameters("@sStartDate").Value = sStartDate
            cmdD.Parameters.Add("@sEndDate", iDB2DbType.iDB2Char, 8)
            cmdD.Parameters("@sEndDate").Value = sEndDate

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function InsertCat(ByVal sCatName As String, ByVal sCatEff As String, ByVal sCatExp As String, ByVal sCatType As String) As Boolean
        '---------------------------------------------------------
        'Function: Insert Category Header
        'Desc. . : Insert record in CSTCON and CSTCOV files using SQL
        'Parms . : 1.) Category name
        '          2.) Effective date
        '          3.) Expiration date
        '          4.) Category type 
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error

        mvLastError = ""

        Try

            'Bail on parm validation

            If sCatName.Trim = "" Then
                Throw New Exception("Must pass category name.")
            End If

            Dim curdate As String = DateTime.Now.ToString("yyyyMMdd")
            Dim cmd As New iDB2Command("insert into cstcon (cccon#,ccsysd,ccctyp,cceffd,ccexpd,ccoeid,ccudv2) values('@@cccon#','@@ccsysd','@@ccctyp', '@@cceffd', '@@ccexpd', '@@ccoeid', '@@ccudv2')", conn)
            cmd.CommandText = cmd.CommandText.Replace("@@cccon#", sCatName)
            cmd.CommandText = cmd.CommandText.Replace("@@ccsysd", curdate)
            cmd.CommandText = cmd.CommandText.Replace("@@ccctyp", "A")
            cmd.CommandText = cmd.CommandText.Replace("@@cceffd", sCatEff)
            cmd.CommandText = cmd.CommandText.Replace("@@ccexpd", sCatExp)
            cmd.CommandText = cmd.CommandText.Replace("@@ccoeid", "Y")
            cmd.CommandText = cmd.CommandText.Replace("@@ccudv2", sCatType)

            'Execute the record insert

            cmd.ExecuteNonQuery()

            Dim cmdD As New iDB2Command("insert into cstcov (cccon#,ccctyp,ccvseq,ccvver) values('@@cccon#','@@ccctyp', '@@ccvseq', '@@ccvver')", conn)
            cmdD.CommandText = cmdD.CommandText.Replace("@@cccon#", sCatName)
            cmdD.CommandText = cmdD.CommandText.Replace("@@ccctyp", "A")
            cmdD.CommandText = cmdD.CommandText.Replace("@@ccvseq", 1)
            cmdD.CommandText = cmdD.CommandText.Replace("@@ccvver", sCatName)

            'Execute the record insert

            cmdD.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function InsertGroup(ByVal sCatgrp As String, ByVal sCatdesc As String, ByVal sCattype As String) As Boolean
        '---------------------------------------------------------
        'Function: InsertGroup
        'Desc. . : Insert record in CATGROUP file using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            'Bail on parm validation
            If sCatgrp.Trim = "" Then
                Throw New Exception("Must pass group number key.")
            End If

            Dim cmd As New iDB2Command("insert into  catgroup (catgrp,catdesc,cattype) values('@@catgrp','@@catdesc','@@cattype')", conn)
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

    Shared Function InsertItem(ByVal sItem As String, ByVal sEffDate As String, ByVal sExpDate As String, ByVal sIncQty As String, ByVal sMaxQty As String, ByVal sArtLoc As String, ByVal sCatGroup As String, ByVal sCatName As String, ByVal sSeqNum As String, ByVal sItemUOM As String) As Boolean
        '---------------------------------------------------------
        'Desc. . : Insert detail record in CSTCOL and CSTCOP files using SQL
        'Parms . : 1.) Item 
        '          2.) Effective date
        '          3.) Expiration date
        '          4.) Incremental quantity
        '          5.) Maximum Order quantity
        '          6.) Art package location
        '          7.) Catalog group
        '          8.) Catalog name
        '          9.) Sequence number
        '         10.) Item unit of measure
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error

        mvLastError = ""

        Try

            'Bail on parm validation

            If sItem.Trim = "" Then
                Throw New Exception("Must pass item number.")
            End If

            Dim cmd As New iDB2Command("insert into cstcol (ccctyp,cceffl,ccexpl,cccon#,ccitct,ccnumb,ccreta,ccexco,ccpzon,ccruom,cceop,ccmqoh,cciseq) values('@@ccctyp','@@cceffl','@@ccexpl', '@@cccon#', '@@ccitct', '@@ccnumb', '@@ccreta', '@@ccexco', '@@ccpzon', '@@ccruom', '@@cceop', '@@ccmqoh', '@@cciseq')", conn)
            cmd.CommandText = cmd.CommandText.Replace("@@ccctyp", "A")
            cmd.CommandText = cmd.CommandText.Replace("@@cceffl", sEffDate)
            cmd.CommandText = cmd.CommandText.Replace("@@ccexpl", sExpDate)
            cmd.CommandText = cmd.CommandText.Replace("@@cccon#", sCatName)
            cmd.CommandText = cmd.CommandText.Replace("@@ccitct", "I")
            cmd.CommandText = cmd.CommandText.Replace("@@ccnumb", sItem)
            cmd.CommandText = cmd.CommandText.Replace("@@ccreta", "Y")
            cmd.CommandText = cmd.CommandText.Replace("@@ccexco", "Y")
            cmd.CommandText = cmd.CommandText.Replace("@@ccpzon", sCatGroup)
            cmd.CommandText = cmd.CommandText.Replace("@@ccruom", sArtLoc)
            cmd.CommandText = cmd.CommandText.Replace("@@cceop", sMaxQty)
            cmd.CommandText = cmd.CommandText.Replace("@@ccmqoh", sIncQty)
            cmd.CommandText = cmd.CommandText.Replace("@@cciseq", sSeqNum)

            'Execute the record insert

            cmd.ExecuteNonQuery()

            Dim cmdD As New iDB2Command("insert into cstcop (ccctyp,cccon#,ccitct,ccnumb,ccdeff,ccdexp,cccuom,ccpbon,ccpfre,cciseq) values('@@ccctyp','@@cccon#', '@@ccitct', '@@ccnumb', '@@ccdeff', '@@ccdexp', '@@cccuom', '@@ccpbon', '@@ccpfre', '@@cciseq')", conn)
            cmdD.CommandText = cmdD.CommandText.Replace("@@ccctyp", "A")
            cmdD.CommandText = cmdD.CommandText.Replace("@@cccon#", sCatName)
            cmdD.CommandText = cmdD.CommandText.Replace("@@ccitct", "I")
            cmdD.CommandText = cmdD.CommandText.Replace("@@ccnumb", sItem)
            cmdD.CommandText = cmdD.CommandText.Replace("@@ccdeff", sEffDate)
            cmdD.CommandText = cmdD.CommandText.Replace("@@ccdexp", sExpDate)
            cmdD.CommandText = cmdD.CommandText.Replace("@@cccuom", sItemUOM)
            cmdD.CommandText = cmdD.CommandText.Replace("@@ccpbon", "A")
            cmdD.CommandText = cmdD.CommandText.Replace("@@ccpfre", "X")
            cmdD.CommandText = cmdD.CommandText.Replace("@@cciseq", sSeqNum)

            'Execute the record insert

            cmdD.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
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

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        'Try

        'Set command object and connection info 
        'for the command object to current iSeries iDB2 connection
        Dim oCmd As New iDB2Command
        oCmd.Connection = conn

        'Build iSeries Parameter List in top/bottom seq (ordinal)

        oCmd.Parameters.Add("htnum", iDB2DbType.iDB2Char, 8)
        oCmd.Parameters("htnum").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("htnum").Value = sHtnum

        oCmd.Parameters.Add("htllin", iDB2DbType.iDB2Decimal)
        oCmd.Parameters("htllin").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("htllin").Precision = 3
        oCmd.Parameters("htllin").Scale = 0
        oCmd.Parameters("htllin").Value = sHtllin

        oCmd.Parameters.Add("htlpnm", iDB2DbType.iDB2Char, 20)
        oCmd.Parameters("htlpnm").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("htlpnm").Value = sHtlpnm

        oCmd.Parameters.Add("htqor", iDB2DbType.iDB2Decimal)
        oCmd.Parameters("htqor").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("htqor").Precision = 7
        oCmd.Parameters("htqor").Scale = 0
        oCmd.Parameters("htqor").Value = sHtqor

        oCmd.Parameters.Add("cs33sn", iDB2DbType.iDB2Char, 20)
        oCmd.Parameters("cs33sn").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("cs33sn").Value = sCatName

        oCmd.Parameters.Add("stchp", iDB2DbType.iDB2Char, 7)
        oCmd.Parameters("stchp").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("stchp").Value = sEvtPo

        oCmd.CommandType = CommandType.StoredProcedure
        oCmd.CommandText = "CATC002"
        oCmd.ExecuteNonQuery()

        'Return true if successful 
        Return True

        'Catch ex As Exception

        '    'Set class level error message as well
        '    mvLastError = ex.Message

        '    Return False

        'End Try

    End Function

    Shared Function InsertOrderHeaderCATR001(ByVal eventID As String, ByVal eventDate As String, ByVal userID As String,
        ByVal sShpnam As String, ByVal sShpad1 As String, ByVal sShpad2 As String, ByVal sShpcty As String, ByVal sShpzip5 As String, ByVal sShpzip4 As String,
        ByVal fflorg As String, ByVal fflattn As String, ByVal ffladdr As String, ByVal fflcity As String, ByVal fflst As String, ByVal fflzip5 As String, ByVal fflzip4 As String, ByVal fflnum As String, ByVal fflexp As String,
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

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        'Try

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

        oCmd.Parameters.Add("@evtpo", iDB2DbType.iDB2Char, 7)
        oCmd.Parameters("@evtpo").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@evtpo").Value = eventID

        oCmd.Parameters.Add("@evdate", iDB2DbType.iDB2Char, 8)
        oCmd.Parameters("@evdate").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@evdate").Value = eventDate

        oCmd.Parameters.Add("@volnam", iDB2DbType.iDB2Char, 10)
        oCmd.Parameters("@volnam").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@volnam").Value = userID

        oCmd.Parameters.Add("@shpnam", iDB2DbType.iDB2Char, 30)
        oCmd.Parameters("@shpnam").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@shpnam").Value = sShpnam

        oCmd.Parameters.Add("@shpad1", iDB2DbType.iDB2Char, 30)
        oCmd.Parameters("@shpad1").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@shpad1").Value = sShpad1

        oCmd.Parameters.Add("@shpad2", iDB2DbType.iDB2Char, 30)
        oCmd.Parameters("@shpad2").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@shpad2").Value = sShpad2

        oCmd.Parameters.Add("@shpcty", iDB2DbType.iDB2Char, 30)
        oCmd.Parameters("@shpcty").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@shpcty").Value = sShpcty

        oCmd.Parameters.Add("@shpzip5", iDB2DbType.iDB2Char, 5)
        oCmd.Parameters("@shpzip5").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@shpzip5").Value = sShpzip5

        oCmd.Parameters.Add("@shpzip4", iDB2DbType.iDB2Char, 4)
        oCmd.Parameters("@shpzip4").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@shpzip4").Value = sShpzip4

        oCmd.Parameters.Add("@fflorg", iDB2DbType.iDB2Char, 30)
        oCmd.Parameters("@fflorg").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@fflorg").Value = fflorg

        oCmd.Parameters.Add("@fflattn", iDB2DbType.iDB2Char, 30)
        oCmd.Parameters("@fflattn").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@fflattn").Value = fflattn

        oCmd.Parameters.Add("@ffladdr", iDB2DbType.iDB2Char, 30)
        oCmd.Parameters("@ffladdr").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@ffladdr").Value = ffladdr

        oCmd.Parameters.Add("@fflcity", iDB2DbType.iDB2Char, 30)
        oCmd.Parameters("@fflcity").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@fflcity").Value = fflcity

        oCmd.Parameters.Add("@fflst", iDB2DbType.iDB2Char, 2)
        oCmd.Parameters("@fflst").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@fflst").Value = fflst

        oCmd.Parameters.Add("@fflzip5", iDB2DbType.iDB2Char, 5)
        oCmd.Parameters("@fflzip5").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@fflzip5").Value = fflzip5

        oCmd.Parameters.Add("@fflzip4", iDB2DbType.iDB2Char, 4)
        oCmd.Parameters("@fflzip4").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@fflzip4").Value = fflzip4

        oCmd.Parameters.Add("@fflnum", iDB2DbType.iDB2Char, 20)
        oCmd.Parameters("@fflnum").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@fflnum").Value = fflnum

        oCmd.Parameters.Add("@fflexp", iDB2DbType.iDB2Char, 8)
        oCmd.Parameters("@fflexp").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@fflexp").Value = fflexp

        oCmd.Parameters.Add("@email", iDB2DbType.iDB2Char, 100)
        oCmd.Parameters("@email").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@email").Value = catema

        oCmd.Parameters.Add("@comnt", iDB2DbType.iDB2Char, 500)
        oCmd.Parameters("@comnt").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@comnt").Value = catcom

        oCmd.Parameters.Add("@approv", iDB2DbType.iDB2Char, 1)
        oCmd.Parameters("@approv").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@approv").Value = approv

        oCmd.Parameters.Add("@totcost", iDB2DbType.iDB2Decimal)
        oCmd.Parameters("@totcost").Direction = ParameterDirection.InputOutput
        oCmd.Parameters("@totcost").Precision = 11
        oCmd.Parameters("@totcost").Scale = 2
        oCmd.Parameters("@totcost").Value = sTotcost

        oCmd.Parameters.Add("@neword", iDB2DbType.iDB2Char, 8)
        oCmd.Parameters("@neword").Direction = ParameterDirection.InputOutput
        If uOrderNumber Is Nothing Or uOrderNumber = "" Then
            oCmd.Parameters("@neword").Value = " "
        Else
            oCmd.Parameters("@neword").Value = uOrderNumber
        End If

        oCmd.CommandType = CommandType.StoredProcedure
        oCmd.CommandText = "CATC001"

        oCmd.ExecuteNonQuery()

        Return oCmd.Parameters("@neword").Value

        'Catch exiDB2 As iDB2Exception
        '    mvLastError = exiDB2.Message + exiDB2.MessageCode.ToString
        '    Return Nothing

        'Catch ex As Exception

        '    'Set class level error message as well
        '    mvLastError = ex.Message

        '    Return ""
        'End Try

    End Function

    Shared Function InsertRDOrderDUW008EC(ByVal sRdNum As String, ByVal sOrder As String, ByVal sAction As String) As Boolean
        '---------------------------------------------------------
        'Function: InsertRDOrderDUW008EC
        'Desc. . : Insert Quoted orders into the ducksystem for action button DUW008EC
        'Parms . : 1.) RD number
        '          2.) Order number
        '          3.) Action 
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

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            oCmd.Parameters.Add("rd", iDB2DbType.iDB2Char, 2)
            oCmd.Parameters("rd").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("rd").Value = sRdNum

            oCmd.Parameters.Add("order", iDB2DbType.iDB2Char, 8)
            oCmd.Parameters("order").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("order").Value = sOrder

            oCmd.Parameters.Add("action", iDB2DbType.iDB2Char, 1)
            oCmd.Parameters("action").Direction = ParameterDirection.InputOutput
            oCmd.Parameters("action").Value = sAction

            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "DUW008EC"

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

    Shared Function LoadQuotOrderListALL() As DataTable
        '---------------------------------------------------------
        'Function: LoadQuotOrderListALL
        'Desc. . : Get all quoted order header information as a DataTable.
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

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

    Shared Function LoadQuotOrderListRD(ByVal rdNum As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadQuotOrderListRD
        'Desc. . : Get all quoted order header information for a RD as a DataTable.
        'Parms . : 1.) RD Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "select p.htnum, p.htcpo, p.htslm1, p.htrcfl, p.htcodt,"
            query += " c.catevdesc, c.catevpo,"
            query += " d.approv,"
            query += " CONCAT(c.catevdesc,CONCAT(' - ',p.htnum)) dropname"
            query += " from  chtqrd p"
            query += " left outer join  catevname c on (c.catevpo = p.htcpo)"
            query += " left outer join  catffl d on (d.fflord = p.htnum)"
            query += " where p.htslm1=@rdNum"
            'query += " group by c.catevdesc, p.htcpo, p.htnum, p.htslm1, p.htrcfl, p.htcodt, c.catevpo"
            query += " order by c.catevdesc, p.htcpo, p.htnum"

            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@rdNum", iDB2DbType.iDB2Char, 3)
            cmdD.Parameters("@rdNum").Value = "0" & rdNum

            Dim dr As iDB2DataReader
            dr = cmdD.ExecuteReader()

            dt.Load(dr)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function


    Shared Function LoadQuotOrderListEV(ByVal eventKey As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadQuotOrderListEV
        'Desc. . : Get all quoted order header information for an event as a DataTable.
        'Parms . : 1.) Event Key (ST,CHP,EV NUM,EV TYPE)
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "Select * From  chtmst where htcpo=@htcpo and htrcfl=@htrcfl"
            Dim cmdD As New iDB2Command(query, conn)
            'cmdD.CommandText = cmdD.CommandText.Replace("@@LIB", mvLibrary)
            cmdD.Parameters.Add("@htcpo", iDB2DbType.iDB2Char, 7)
            cmdD.Parameters("@htcpo").Value = eventKey
            cmdD.Parameters.Add("@htrcfl", iDB2DbType.iDB2Char, 1)
            cmdD.Parameters("@htrcfl").Value = "Q"

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadQuotOrderDetail(ByVal sOrder As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadQuotOrderDetail
        'Desc. . : Get all quoted order details as a DataTable.
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dr As iDB2DataReader
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "Select HTQor, htluct, CS33EC, HTLPNM, htnum, HTLDSC From  chtqlin where htnum=@htnum"
            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@htnum", iDB2DbType.iDB2Char, 8)
            cmdD.Parameters("@htnum").Value = sOrder

            dr = cmdD.ExecuteReader()
            dt.Load(dr)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadQuotOrderHeader(ByVal sOrder As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadQuotOrderHeader
        'Desc. . : Get a quoted order header as a DataReader.
        'Parms . : 1.) Order Number
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dr As iDB2DataReader

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "Select p.*, c.*"
            query += " from  chtmst p"
            query += " left outer join  catffl c on (c.fflord = p.htnum)"
            query += " where p.htnum=@htnum"
            Dim cmd As New iDB2Command(query, conn)
            cmd.Parameters.Add("@htnum", iDB2DbType.iDB2Char, 8)
            cmd.Parameters("@htnum").Value = sOrder

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

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

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

    Shared Function LoadOrderHeader(ByVal sOrder As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadOrderHeader
        'Desc. . : Get order header as a DataTable.
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "Select p.*, c.*"
            query += " from  chtmst p"
            query += " left outer join  catffl c on (c.fflord = p.htnum)"
            query += " where p.htnum=@htnum"

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

    Shared Function LoadCatsGroupsDataTable(ByVal rdOnly As Boolean, ByVal eventType As String) As DataTable
        '-------------------------------------------------------
        'function  : LoadCatsGroupsDataTable
        'Desc. . . : Load the catalog group buttons to web page.
        '-------------------------------------------------------

        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Try

            Dim query As String = String.Empty
            If eventType = "Z" Then
                query = "Select catgrp, catdesc From  catgroup order by catdesc"
            Else
                If rdOnly Then
                    If eventType = "S" Then
                        query = "Select catgrp, catdesc From  catgroup where (cattype = ' ' or cattype = 'RD' or cattype = 'SP')  order by catdesc"
                    Else
                        query = "Select catgrp, catdesc From  catgroup where (cattype = ' ' or cattype = 'RD')  order by catdesc"
                    End If
                Else
                    If eventType = "S" Then
                        query = "Select catgrp, catdesc From  catgroup where (cattype = ' ' or cattype = 'SP')  order by catdesc"
                    Else
                        query = "Select catgrp, catdesc From  catgroup where cattype = ' ' order by catdesc"
                    End If
                End If
            End If
            Dim sc As iDB2Command = New iDB2Command(query, conn)

            Dim dr As iDB2DataReader
            dr = sc.ExecuteReader()
            dt.Load(dr)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function LoadCatalogDataTable(ByVal evdate As String, ByVal evtype As String, ByVal evstate As String) As DataTable
        '-------------------------------------------------------
        'function  : LoadCatalogDataTable
        'Desc. . . : Load the catalog data to web page.
        '-------------------------------------------------------

        Dim dt As New DataTable
        Dim catalog As String = String.Empty

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Try

            Dim query As String = String.Empty

            'determin which catalog to go after this is determined by event type
            'If evtype = "W" Or evtype = "N" Then
            '    catalog = "WHP"
            'Else
            'If evtype = "N" Then
            '    catalog = "SNO"
            'Else
            catalog = "NAT"
            'End If
            'End If

            'build select for catalog items
            query = "select p.ccudv2, p.cccon#, p.cceffd, p.ccexpd,"
            query += " c.ccnumb, c.cciseq, c.ccpzon, c.cceffl, c.ccexpl, c.cceop, c.ccmqoh, c.ccruom,"
            query += " d.itmdsc, e.ipcost, (case when (g.expitm is not null) then sum(g.expqty * h.ipcost) else e.ipcost End) as unitcost,"
            query += " sum(f.ibqoh-f.ibqal) as avalqty,"
            query += " i.stdust"
            query += " from  cstcon p"
            query += " left outer join  cstcol c on (c.cccon# = p.cccon#)"
            query += " left outer join  itmmst d on (d.itmnum = c.ccnumb)"
            query += " left outer join  itmcmp e on (e.ipitm# = d.itmnum and e.ibbrch = '  ')"
            query += " left outer join  ibrbin f on (f.ibitm# = d.itmnum)"
            query += " left outer join  itmexp g on (g.expitm = d.itmnum)"
            query += " left outer join  itmcmp h on (h.ipitm# = g.toitm and h.ibbrch = '  ')"
            query += " left outer join  catartp i on (i.stcode = c.ccruom)"
            query += " where (p.ccudv2 = '" + catalog + "') and (c.cceffl <= " + evdate + " and c.ccexpl >= " + evdate + ") and ((i.stdust = 'XX') or (i.stdust = '" + evstate + "'))"
            query += " group by p.ccudv2, p.cccon#, p.cceffd, p.ccexpd, c.ccnumb, c.cciseq, c.ccpzon, c.cceffl, c.ccexpl, c.cceop, c.ccmqoh, c.ccruom, d.itmdsc, e.ipcost, g.expitm, i.stdust"
            query += " order by c.ccpzon, c.cciseq"

            Dim sc As iDB2Command = New iDB2Command(query, conn)
            Dim dr As iDB2DataReader
            dr = sc.ExecuteReader()

            dt.Load(dr)

            Return dt

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

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            Dim query As String = String.Empty

            query = "select * from cstmst where cstnum='" & "     " & sChapter & "'"

            Dim cmd As New iDB2Command(query, conn)

            dr = cmd.ExecuteReader()

            Return dr

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

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

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

    Shared Function LoadOrderDetailP(ByVal sOrder As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadOrderDetail
        'Desc. . : Get order details as a DataTable.
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "select p.htnum, p.htllin, p.htqor, p.htlqsh, p.htlqbo, p.htlun, p.htlpnm, p.htldsc, p.htluct, c.htlser"
            query += " from chtlin  p"
            query += " left outer join chtser c on (c.htnum = p.htnum and c.htllin = p.htllin)"
            query += " where p.htnum=@htnum"
            'Dim cmdD As New iDB2Command("select htnum, HTLLIN, htqor, htlqsh, htlqbo, htlun, htlpnm, htldsc, htluct from chtlin where htnum=@htnum", conn)
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

    Shared Function LoadOrderHeaderP(ByVal sOrder As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadOrderHeader
        'Desc. . : Get order header as a DataTable.
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

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

    Shared Function LoadStateDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadStateDropDown
        'Desc. . : Loades a data table of state name and codes.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "select stdust, stname, stfly from evdustp where (stfly<'90') order by stname"

            Dim cmdD As New iDB2Command(query, conn)

            Dim dr As iDB2DataReader
            dr = cmdD.ExecuteReader()
            dt.Load(dr)
            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            'Current.Session("LastError") = ex.Message
        End Try

    End Function

    Shared Function LoadUPSTracking(ByVal sOrder As String) As DataTable
        '---------------------------------------------------------
        'Function: LoadUPSTracking
        'Desc. . : Build a data table of tracking numbers for an order
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim dtV As New DataTable
        Dim upsFound As Boolean = False

        'Reset last error
        mvLastError = ""

        Try

            Dim dc1 As New DataColumn("utrack", GetType(String))
            Dim dc2 As New DataColumn("utser", GetType(String))
            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)

            'Dim cmd As New iDB2Command("select utrack, utser from upstgtwl2 where utord=@htnum", conn)
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

    Shared Function SaveOrder(ByVal eventID As String, ByVal eventDate As String, ByVal userID As String, ByVal rdNum As String, ByVal DTOrder As DataTable,
        ByVal sShpnam As String, ByVal sShpad1 As String, ByVal sShpad2 As String, ByVal sShpcty As String, ByVal sShpzip5 As String, ByVal sShpzip4 As String,
        ByVal fflorg As String, ByVal fflattn As String, ByVal ffladdr As String, ByVal fflcity As String, ByVal fflst As String, ByVal fflzip5 As String, ByVal fflzip4 As String, ByVal fflnum As String, ByVal fflexp As String,
        ByVal catema As String, ByVal catcom As String, ByVal approv As String, ByVal sTotcost As Double, ByVal sCatName As String, ByVal uOrderNumber As String) As String

        'Try

        'parms = System.Web.HttpContext.Current.Session("oSessionManager")

        'Insert order header records with CATR001 call
        Dim sOrder As String
        sOrder = InsertOrderHeaderCATR001(eventID, eventDate, userID,
                sShpnam, sShpad1, sShpad2, sShpcty, sShpzip5, sShpzip4,
                fflorg, fflattn, ffladdr, fflcity, fflst, fflzip5, fflzip4, fflnum, fflexp,
                catema, catcom, approv, sTotcost, uOrderNumber)
        If sOrder.Trim = "" Then
            Throw New Exception("Order number error.")
        End If

        'Loop to insert each detail line with call to CATR002
        Dim ct As Long
        For ct = 0 To DTOrder.Rows.Count - 1
            InsertOrderDetailCATR002(sOrder, ct + 1, DTOrder.Rows(ct).Item("ProductID"), DTOrder.Rows(ct).Item("Qty"), sCatName, eventID)
        Next ct

        InsertRDOrderDUW008EC(rdNum.Trim, sOrder.Trim, "A")

        'Send back order number if completed.
        Return sOrder

        'Catch ex As Exception
        '    'Set class level error message as well
        '    mvLastError = ex.Message
        '    Throw ex
        '    Return ""
        'End Try

    End Function

    Shared Function UpdateGroup(ByVal sCatgrp As String, ByVal sCatdesc As String, ByVal sCattype As String) As Boolean
        '---------------------------------------------------------
        'Function: UpdateGroup
        'Desc. . : Update record in CATGROUP file using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            Dim cmd As New iDB2Command("update  catgroup SET " & _
            "CATDESC='" & sCatdesc & "'," & _
            "CATTYPE='" & sCattype & "'" & _
            " WHERE CATGRP=" & sCatgrp, conn)

            'Execute the record insert
            cmd.ExecuteNonQuery()

            'Insert succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try


    End Function

    Shared Function UpdateCat(ByVal sCatName As String, ByVal sCatEff As String, ByVal sCatExp As String, ByVal sCatType As String) As Boolean

        '---------------------------------------------------------
        'Function: Update Category
        'Desc. . : Update record in CSTCON and CSTCOV files using SQL
        'Parms . : 1.) Catalog name
        '          2.) Effective date
        '          3.) Expiration date
        '          4.) Catalog type
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            Dim cmdD As New iDB2Command("update  cstcon SET " &
            "CCEFFD='" & sCatEff & "'," &
            "CCEXPD='" & sCatExp & "'," &
            "CCUDV2='" & sCatType & "'" &
            " WHERE CCCON# = @cccon# and CCCTYP = @ccctyp and CCCUST = @cccust", conn)
            cmdD.Parameters.Add("@cccon#", iDB2DbType.iDB2Char, 20)
            cmdD.Parameters("@cccon#").Value = sCatName
            cmdD.Parameters.Add("@ccctyp", iDB2DbType.iDB2Char, 1)
            cmdD.Parameters("@ccctyp").Value = "A"
            cmdD.Parameters.Add("@cccust", iDB2DbType.iDB2Char, 10)
            cmdD.Parameters("@cccust").Value = "    "

            'Execute the record update
            cmdD.ExecuteNonQuery()

            'Update succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function UpdateItem(ByVal sItem As String, ByVal sEffDate As String, ByVal sExpDate As String, ByVal sIncQty As String, ByVal sMaxQty As String, ByVal sArtLoc As String, ByVal sCatGroup As String, ByVal sCatName As String, ByVal sSeqNum As String, ByVal sCCEFFL As String) As Boolean
        '---------------------------------------------------------
        'Desc. . : Update detail record in CSTCOL and CSTCOP files using SQL
        'Parms . : 1.) Item 
        '          2.) Effective date
        '          3.) Expiration date
        '          4.) Incremental quantity
        '          5.) Maximum Order quantity
        '          6.) Art package location
        '          7.) Catalog group
        '          8.) Catalog name
        '          9.) Sequence number
        '         10.) Saved old effective date as session parm is part of where   
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            Dim cmdD As New iDB2Command("update  cstcol SET " &
            "CCEFFL='" & sEffDate & "'," &
            "CCEXPL='" & sExpDate & "'," &
            "CCPZON='" & sCatGroup & "'," &
            "CCRUOM='" & sArtLoc & "'," &
            "CCEOP='" & sMaxQty & "'," &
            "CCMQOH='" & sIncQty & "'," &
            "CCISEQ='" & sSeqNum & "'" &
            " WHERE CCCTYP = @ccctyp and CCCUST = @cccust and CCCON# = @cccon# and CCITCT = @ccitct and CCNUMB = @ccnumb and CCEFFL = @cceffl", conn)
            cmdD.Parameters.Add("@ccctyp", iDB2DbType.iDB2Char, 1)
            cmdD.Parameters("@ccctyp").Value = "A"
            cmdD.Parameters.Add("@cccust", iDB2DbType.iDB2Char, 10)
            cmdD.Parameters("@cccust").Value = "    "
            cmdD.Parameters.Add("@cccon#", iDB2DbType.iDB2Char, 20)
            cmdD.Parameters("@cccon#").Value = sCatName
            cmdD.Parameters.Add("@ccitct", iDB2DbType.iDB2Char, 1)
            cmdD.Parameters("@ccitct").Value = "I"
            cmdD.Parameters.Add("@ccnumb", iDB2DbType.iDB2Char, 20)
            cmdD.Parameters("@ccnumb").Value = sItem
            cmdD.Parameters.Add("@cceffl", iDB2DbType.iDB2Char, 8)
            cmdD.Parameters("@cceffl").Value = sCCEFFL

            'Execute the record update
            cmdD.ExecuteNonQuery()

            Dim cmd As New iDB2Command("update  cstcop SET " &
            "CCDEFF='" & sEffDate & "'," &
            "CCDEXP='" & sExpDate & "'," &
            "CCISEQ='" & sSeqNum & "'" &
            " WHERE CCCTYP = @ccctyp and CCCUST = @cccust and CCCON# = @cccon# and CCITCT = @ccitct and CCNUMB = @ccnumb and CCDEFF = @cceffl", conn)
            cmd.Parameters.Add("@ccctyp", iDB2DbType.iDB2Char, 1)
            cmd.Parameters("@ccctyp").Value = "A"
            cmd.Parameters.Add("@cccust", iDB2DbType.iDB2Char, 10)
            cmd.Parameters("@cccust").Value = "    "
            cmd.Parameters.Add("@cccon#", iDB2DbType.iDB2Char, 20)
            cmd.Parameters("@cccon#").Value = sCatName
            cmd.Parameters.Add("@ccitct", iDB2DbType.iDB2Char, 1)
            cmd.Parameters("@ccitct").Value = "I"
            cmd.Parameters.Add("@ccnumb", iDB2DbType.iDB2Char, 20)
            cmd.Parameters("@ccnumb").Value = sItem
            cmd.Parameters.Add("@ccdeff", iDB2DbType.iDB2Char, 8)
            cmd.Parameters("@ccdeff").Value = sCCEFFL

            'Execute the record update
            cmd.ExecuteNonQuery()

            'Update succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

End Class
