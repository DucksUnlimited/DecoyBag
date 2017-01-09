Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
Imports System.data
Imports System.Web.HttpContext
Imports System.Configuration

Public Class iSeriesCatalog

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("iSeries"))
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

    Shared Function LoadQuotOrderListRD(ByVal rdNum As String) As DataTable
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

    Shared Function LoadQuotOrderDetail(ByVal sOrder As String) As iDB2DataReader
        '---------------------------------------------------------
        'Function: LoadQuotOrderDetail
        'Desc. . : Get all quoted order details as a DataTable.
        'Parms . : 1.) Order Number
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dr As iDB2DataReader
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select HTQor, htluct, CS33EC, HTLPNM, htnum, HTLDSC From  chtqlin where htnum=@htnum"
            Dim cmdD As New iDB2Command(query, conn)
            cmdD.Parameters.Add("@htnum", iDB2DbType.iDB2Char, 8)
            cmdD.Parameters("@htnum").Value = sOrder

            dr = cmdD.ExecuteReader()
            'dt.Load(dr)
            Return dr

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

    Shared Function GetGroupList() As DataTable
        '---------------------------------------------------------
        'Function: Get Group List
        'Desc. . : Get category groups as a DataReader.
        'Returns : Returns a data reader object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

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

    Shared Function InsertGroup(ByVal sCatgrp As String, ByVal sCatdesc As String, ByVal sCattype As String) As Boolean
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

    Shared Function UpdateGroup(ByVal sCatgrp As String, ByVal sCatdesc As String, ByVal sCattype As String) As Boolean
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

    Shared Function GetNextGroupCATR003() As String
        '---------------------------------------------------------
        'Function: GetNextGroupCATR003
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

End Class
