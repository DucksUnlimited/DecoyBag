Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
Imports System.data
Imports System.Web.HttpContext
Imports System.Configuration

Public Class iSeriesInventory

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("iSeries"))
    Shared mvLastError As String = ""

    Shared Function LoadCatalogDataTable(ByVal evdate As String, ByVal evtype As String, ByVal evstate As String) As DataTable
        '-------------------------------------------------------
        'function  : LoadCatalogDataTable
        'Desc. . . : Load the catalog data to web page.
        '-------------------------------------------------------

        Dim dt As New DataTable
        Dim catalog As String = String.Empty

        Try

            Dim query As String = String.Empty

            'determin which catalog to go after this is determined by event type
            If evtype = "W" Or evtype = "N" Then
                catalog = "WHP"
            Else
                'If evtype = "N" Then
                '    catalog = "SNO"
                'Else
                catalog = "NAT"
                'End If
            End If

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
            query += " where (p.ccudv2 = '" + catalog + "') and (c.cceffl < " + evdate + " and c.ccexpl > " + evdate + ") and ((i.stdust = 'XX') or (i.stdust = '" + evstate + "'))"
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

    Shared Function GetCatList() As DataTable

        '---------------------------------------------------------
        'Function: Get Category List
        'Desc. . : Get category header records as a DataReader.
        'Parms . : 1.) Category name
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

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

    Shared Function GetCat(ByVal sCCCON As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Category 
        'Desc. . : Get category as a DataReader.
        'Parms . : 1.) Category name
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

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

    Shared Function InsertCat(ByVal sCatName As String, ByVal sCatEff As String, ByVal sCatExp As String, ByVal sCatType As String) As Boolean

        '---------------------------------------------------------
        'Function: Insert Category Header
        'Desc. . : Insert record in CSTCON and CSTCOV files using SQL
        'Parms . : 1.) Category name
        '          2.) Effective date
        '          3.) Expiration date
        '          4.) Category type 
        '---------------------------------------------------------

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

    Shared Function UpdateCat(ByVal sCatName As String, ByVal sCatEff As String, ByVal sCatExp As String, ByVal sCatType As String) As Boolean

        '---------------------------------------------------------
        'Function: Update Category
        'Desc. . : Update record in CSTCON and CSTCOV files using SQL
        'Parms . : 1.) Catalog name
        '          2.) Effective date
        '          3.) Expiration date
        '          4.) Catalog type
        '---------------------------------------------------------

        'Reset last error
        mvLastError = ""

        Try

            Dim cmdD As New iDB2Command("update  cstcon SET " & _
            "CCEFFD='" & sCatEff & "'," & _
            "CCEXPD='" & sCatExp & "'," & _
            "CCUDV2='" & sCatType & "'" & _
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

    Shared Function GetItem(ByVal sCCCON As String, ByVal sCCNUMB As String, ByVal sCCEFFL As String) As DataTable

        '---------------------------------------------------------
        'Function: Get Catalog Item 
        'Desc. . : Get item as a DataReader.
        'Parms . : 1.) Catalog name
        '          2.) Item 
        '          3.) Effective date
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

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

    Shared Function GetItemUOM(ByVal itemnum As String) As String
        '-------------------------------------------------------
        'Desc. . . : Get the unit of measure for catalog item
        '                 and verify item number is valid
        '-------------------------------------------------------

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

        'Reset last error
        mvLastError = ""

        Try

            Dim cmdD As New iDB2Command("update  cstcol SET " & _
            "CCEFFL='" & sEffDate & "'," & _
            "CCEXPL='" & sExpDate & "'," & _
            "CCPZON='" & sCatGroup & "'," & _
            "CCRUOM='" & sArtLoc & "'," & _
            "CCEOP='" & sMaxQty & "'," & _
            "CCMQOH='" & sIncQty & "'," & _
            "CCISEQ='" & sSeqNum & "'" & _
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

            Dim cmd As New iDB2Command("update  cstcop SET " & _
            "CCDEFF='" & sEffDate & "'," & _
            "CCDEXP='" & sExpDate & "'," & _
            "CCISEQ='" & sSeqNum & "'" & _
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

    Shared Function GetRDInvQOH(ByVal sRDID As String) As DataTable

        '---------------------------------------------------------
        'Function: Get RD Inventory Status 
        'Desc. . : Get records as a DataReader.
        'Parms . : 1.) RD Number
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try
            query = "select p.ibbr#, p.ibitm#, p.ibqoh,"
            query += " c.itmdsc,"
            query += " d.ipcost,"
            query += " cast((d.ipcost * p.ibqoh) as decimal(9,2)) as extcst"
            query += " from  ibrbin p"
            query += " left outer join  itmmst c on (c.itmnum = p.ibitm#)"
            query += " left outer join  itmcmp d on (d.ipitm# = p.ibitm# and d.ibbrch = p.ibbr#)"
            query += " where (p.ibbr# = @rdid  and p.ibqoh > 0)"

            query += " order by p.ibitm#"


            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@rdid", iDB2DbType.iDB2Char, 2)
            cmdD.Parameters("@rdid").Value = sRDID

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetInvitationItem() As DataTable
        '-------------------------------------------------------
        'Desc. . . : Get the item number & description for
        '             invitations only
        '-------------------------------------------------------

        Dim dt As New DataTable

        Try

            Dim query As String = String.Empty

            'build select for item
            query = "select itmnum, itmdsc, CONCAT(TRIM(itmnum),CONCAT(' - ',TRIM(itmdsc))) itemdesc"
            query += " from  itmmst"
            query += " where itmnum >= 'P111                ' and itmnum <= 'P130                '"

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetVendorShip() As DataTable
        '-------------------------------------------------------
        'Desc. . . : Get the item number & description for
        '             invitations only
        '-------------------------------------------------------

        Dim dt As New DataTable

        Try

            Dim query As String = String.Empty

            'build select for item
            query = "select itmnum, itmdsc, CONCAT(TRIM(itmnum),CONCAT(' - ',TRIM(itmdsc))) itemdesc"
            query += " from  itmmst"
            query += " where itmnum >= 'P111                ' and itmnum <= 'P130                '"

            Dim cmdD As New iDB2Command(query, conn)

            Dim da As iDB2DataAdapter = New iDB2DataAdapter(cmdD)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

End Class
