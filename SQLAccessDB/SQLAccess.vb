Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Public Class SQLAccess
    
    Shared ReadOnly conn As New SqlConnection(ConfigurationManager.AppSettings("CatSQL"))
    Shared mvLastError As String = ""
    
    Shared Function GetCat(sCCCON As String) As DataTable

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

            query = "Select cccon#, cceffd, ccexpd, ccudv2 From  cstcon WHERE CCCON# = '" + scccon + "' and CCCTYP = 'A' and CCCUST = ' '"

            Dim da As SqlDataAdapter = New SqlDataAdapter(query, conn)

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
                query += " c.catdesc"
                'query += " d.itmdsc,"
                'query += " sum(e.ibqoh-e.ibqal) as avalqty"
                query += " from  cstcol p"
                query += " left outer join  catgroup c on (c.catgrp = p.ccpzon)"
                'query += " left outer join  itmmst d on (d.itmnum = p.ccnumb)"
                'query += " left outer join ibrbin e on (e.ibitm# = d.itmnum"
                'query += " and (e.ibbr# = @whs1 or e.ibbr# = @whs2 or e.ibbr# = @whs3 or e.ibbr# = @whs4 or e.ibbr# = @whs5"
                'query += " or e.ibbr# = @whs6 or e.ibbr# = @whs7 or e.ibbr# = @whs8 or e.ibbr# = @whs9 or e.ibbr# = @whs10))"
                query += " where p.ccctyp = @ccctyp and p.cccust = @cccust and p.cccon# = @cccon# and p.ccexpl > @wkdate"
                query += " group by p.ccnumb, p.cceffl, p.ccexpl, p.cciseq, c.catdesc"
                query += " order by p.cciseq"

            Else

                query = "select p.ccnumb, p.cceffl, p.ccexpl, p.cciseq,"
                query += " c.catdesc"
                'query += " d.itmdsc,"
                'query += " sum(e.ibqoh-e.ibqal) as avalqty"
                query += " from  cstcol p"
                query += " left outer join  catgroup c on (c.catgrp = p.ccpzon)"
                'query += " left outer join  itmmst d on (d.itmnum = p.ccnumb)"
                'query += " left outer join ibrbin e on (e.ibitm# = d.itmnum"
                'query += " and (e.ibbr# = @whs1 or e.ibbr# = @whs2 or e.ibbr# = @whs3 or e.ibbr# = @whs4 or e.ibbr# = @whs5"
                'query += " or e.ibbr# = @whs6 or e.ibbr# = @whs7 or e.ibbr# = @whs8 or e.ibbr# = @whs9 or e.ibbr# = @whs10))"
                query += " where p.ccctyp = @ccctyp and p.cccust = @cccust and p.cccon# = @cccon# and p.ccexpl <= @wkdate"
                query += " group by p.ccnumb, p.cceffl, p.ccexpl, p.cciseq, c.catdesc"
                query += " order by p.cciseq"

            End If

            Dim cmdD As New sqlCommand(query, conn)
            cmdD.Parameters.Add("@ccctyp", SqlDbType.VarChar, 1)
            cmdD.Parameters("@ccctyp").Value = "A"
            cmdD.Parameters.Add("@cccust", SqlDbType.VarChar, 10)
            cmdD.Parameters("@cccust").Value = "    "
            cmdD.Parameters.Add("@cccon#", SqlDbType.VarChar, 20)
            cmdD.Parameters("@cccon#").Value = sCCCON
            cmdD.Parameters.Add("@wkdate", SqlDbType.VarChar, 8)
            cmdD.Parameters("@wkdate").Value = wkdate

            Dim da As sqlDataAdapter = New sqlDataAdapter(cmdD)

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

            Dim da As SqlDataAdapter = New SqlDataAdapter(query, conn)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetGroup(sCATGRP As String) As DataTable
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

            Dim da As SqlDataAdapter = New SqlDataAdapter(query, conn)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
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

            query = "Select * From catgroup ORDER BY CATDESC"

            Dim da As SQLDataAdapter = New SqlDataAdapter(query, conn)
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try
    End Function

    Shared Function GetNextGroupNumber() As String
        '---------------------------------------------------------
        'Function: GetNextGroupNumber
        'Desc. . : Get Next Group number for catgroup file
        'Parms . :       
        'Returns : Next group number
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim nextgroupnumber As Integer

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "select TOP 1 catgrp + 5 as NextGrp From CatGroup "
            query += "order by [CatGrp] desc"

            Dim da As New SqlDataAdapter(query, conn)

            dt.Clear()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows.Item(0)
                nextgroupnumber = dr("NextGrp")
            Else
                nextgroupnumber = 005
            End If
            'Return next group number after program call 
            Return nextgroupnumber.ToString("000")

        Catch ex As Exception

            'Set class level error message as well
            mvLastError = ex.Message

            Return ""
        End Try

    End Function

    Shared Function InsertCat(sCatName As String, sCatEff As String, sCatExp As String, sCatType As String) As Boolean
        '---------------------------------------------------------
        'Function: Insert Category Header
        'Desc. . : Insert record in CSTCON and CSTCOV files using SQL
        'Parms . : 1.) Category name
        '          2.) Effective date
        '          3.) Expiration date
        '          4.) Category type 
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim rc As Boolean

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

            Dim curdate As String = Now.ToString("yyyyMMdd")
            query = "insert into cstcon (cccon#,ccsysd,ccctyp,cceffd,ccexpd,ccoeid,ccudv2) values('"
            query += sCatName + "','" + curdate + "','A'" + sCatEff + "','" + sCatEff + "','" + sCatExp + "','Y')"
            Dim cmd As New SqlCommand(query, conn)

            'Execute the record insert

            rc = cmd.ExecuteNonQuery()

            query = "insert into cstcov (cccon#,ccctyp,ccvseq,ccvver) values('" + sCatName + "','A',1,'" + sCatName + "')"
            Dim cmdD As New SqlCommand(query, conn)

            'Execute the record insert

            rc = cmdD.ExecuteNonQuery()

            'Insert succeeded
            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return rc
        End Try

    End Function

    Shared Function InsertGroup(sCatgrp As String, sCatdesc As String, sCattype As String) As Boolean
        '---------------------------------------------------------
        'Function: InsertGroup
        'Desc. . : Insert record in CATGROUP file using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim rc As Boolean

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

            query = "insert into  catgroup (catgrp,catdesc,cattype) values('" + sCatgrp + "', '"
            query += sCatdesc + "', '" + sCattype + "')"
            Dim cmd As New SqlCommand(query, conn)

            'Execute the record insert
            rc = cmd.ExecuteNonQuery()

            'Insert succeeded
            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return rc
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

        Dim query As String = String.Empty
        Dim rc As Boolean

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "update  cstcon SET " & "CCEFFD='" & sCatEff & "'," & "CCEXPD='" & sCatExp & "'," & "CCUDV2='" & sCatType & "'"
            query += " WHERE CCCON# = '" + sCatName & "' and CCCTYP = 'A' and CCCUST = ' '"
            Dim cmdD As New SqlCommand(query, conn)

            'Execute the record update
            cmdD.ExecuteNonQuery()

            'Update succeeded
            Return True

        Catch ex As Exception
            mvLastError = ex.Message
            Return False
        End Try

    End Function

    Shared Function UpdateGroup(sCatgrp As String, sCatdesc As String, sCattype As String) As Boolean
        '---------------------------------------------------------
        'Function: UpdateGroup
        'Desc. . : Update record in CATGROUP file using SQL
        'Parms . : 1.) Groupnumber
        '          2.) Group description
        '          3.) Group type 
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim rc As Boolean

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        'Reset last error
        mvLastError = ""

        Try

            query = "update  catgroup SET CATDESC='" + sCatdesc + "', CATTYPE='" + sCattype + "'"
            Dim cmd As New SqlCommand(query, conn)

            'Execute the record insert
            rc = cmd.ExecuteNonQuery()

            'Insert succeeded
            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return rc
        End Try


    End Function

End Class
