Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
Imports System.data
Imports System.Web.HttpContext
Imports System.Configuration
Imports System.Net.Mail

Public Class iSeriesIDBadges

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("iSeries"))
    Shared mvLastError As String = ""
    Shared count As Integer

    Shared Function BuildNameBadges(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateRegistrant
        'Desc. . : Updates the information about an attendie.
        'Parms . : Table of registrants, table of activities, table of group info
        'Returns : Returns a boolean object.
        '---------------------------------------------------------
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim idprint As String
        Dim wtext As String
        Dim fullname As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            Dim oCmd As New iDB2Command
            oCmd.Connection = conn
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "NAMEBADGC"

            rc = oCmd.ExecuteNonQuery()

            idprint = "0"
            For Each row As DataRow In dt.Rows
                'strDetail = row("Detail")

                wtext = row("prefix").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("prefix") = wtext
                wtext = row("firstname").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("firstname") = wtext
                wtext = row("lastname").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("lastname") = wtext
                wtext = row("suffix").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("suffix") = wtext
                If row("prefix") <> " " And row("prefix") <> "" Then
                    fullname = row("prefix").ToString.Trim + " " + row("firstname").ToString.Trim + " "
                Else
                    fullname = row("firstname").ToString.Trim + " "
                End If
                If row("middlename") <> " " And row("middlename") <> "" Then
                    fullname += row("middlename").ToString.Trim + " "
                End If
                fullname += row("lastname")
                If row("suffix") <> " " Then
                    fullname += " " + row("suffix").ToString.Trim
                End If
                wtext = row("badgename").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("badgename") = wtext.Trim
                wtext = row("region").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("region") = wtext.Trim
                wtext = row("title").ToString().Trim()
                wtext = wtext.Replace("'", "''")
                row("title") = wtext.Trim

                query = "insert into namebadgp (namprf,fname,mname,lname,namsuf,fulname,badgenam,region,title,idprinted) values('@@namprf','@@fname','@@mname','@@lname','@@namsuf','@@fulname','@@badgenam','@@region','@@title','@@idprinted')"
                Dim cmd As New iDB2Command(query, conn)
                cmd.CommandText = cmd.CommandText.Replace("@@namprf", row("prefix").ToString.Trim)
                cmd.CommandText = cmd.CommandText.Replace("@@fname", row("firstname").Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@mname", row("middlename").Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@lname", row("lastname").ToString.Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@namsuf", row("suffix").ToString.Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@fulname", fullname.Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@badgenam", row("badgename").ToString.Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@region", row("region").ToString.Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@title", row("title").ToString.Trim())
                cmd.CommandText = cmd.CommandText.Replace("@@idprinted", "0")
                'query = "insert into idbadgesp ( "
                'query += "groupcode='" & groupcode.ToString.Trim & "', "
                'query += "recnum=" & id & ", "
                'query += "fname='" & fname.ToString.Trim & "', "
                'query += "lname='" & lname.ToString.Trim & "', "
                'query += "fulname='" & fulname.ToString.Trim & "', "
                'query += "badgenam='" & badgename.ToString.Trim & "', "
                'query += "region='" & region.ToString.Trim & "', "
                'query += "title='" & title.ToString.Trim & "', "
                'query += "idprinted='" & idprint.ToString.Trim & "' "
                'query += "where (recnum=" & id & " and groupcode='" & groupcode & "')"

                'Command = New iDB2Command(query, conn)
                rc = cmd.ExecuteNonQuery()
            Next row

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetLastNum(ByVal grpcode As String) As Int32
        '---------------------------------------------------------
        'Function: GetLastNum
        'Desc. . : Get registrants for the group ID passed into it.
        'Parms . : 1). groupid
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable
        Dim lastnum As Int32

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select * "
            query += "from idbadgesp "
            query += "where (groupcode = '" + grpcode + "') "
            query += "order by recnum"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)
            For Each row As DataRow In dt.Rows
                lastnum = row("recnum")
            Next

            Return lastnum

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function GetRegistrant(ByVal groupid As String) As DataTable
        '---------------------------------------------------------
        'Function: GetRegistrants
        'Desc. . : Get registrants for the group ID passed into it.
        'Parms . : 1). groupid
        '          
        'Returns : Returns a Date Table
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            query = "select groupcode, TRIM(fname) fname, TRIM(lname) lname, TRIM(badgenam) badgenam, "
            query += "TRIM(fulname) fulname, TRIM(title) title, TRIM(region) region, idprinted, recnum "
            query += "from idbadgesp "
            query += "where (groupcode = '" & groupid & "') "
            query += "order by title, lname"

            Dim da As New iDB2DataAdapter(query, conn)
            Dim dta = New DataTable
            dt.Clear()
            da.Fill(dt)
            For Each row As DataRow In dt.Rows
                If row("idprinted") = "1" Then
                    row("idprinted") = True
                Else
                    row("idprinted") = False
                End If
            Next

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function IsConnectionOpen() As Boolean
        '---------------------------------------------------------
        'Function: ReopenConnection
        'Desc. . : Reopen iSeries connection if failed. 
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------
        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

        Catch ex As Exception

        End Try


    End Function

    Shared Function InsertIDBadges(ByVal groupcode As String, ByVal id As String, ByVal fname As String, ByVal lname As String, ByVal fulname As String, ByVal badgename As String, ByVal region As String, ByVal title As String) As Boolean
        '---------------------------------------------------------
        'Function: UpdateRegistrant
        'Desc. . : Updates the information about an attendie.
        'Parms . : Table of registrants, table of activities, table of group info
        'Returns : Returns a boolean object.
        '---------------------------------------------------------
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim idprint As String
        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            idprint = "0"

            wtext = fname.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            fname = wtext
            wtext = lname.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            lname = wtext
            wtext = fulname.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            fulname = wtext
            wtext = badgename.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            badgename = wtext
            wtext = region.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            region = wtext
            wtext = title.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            title = wtext

            query = "insert into idbadgesp (groupcode,recnum,fname,lname,fulname,badgenam,region,title,idprinted) values('@@grpcode',@@recnum,'@@fname','@@lname','@@fulname','@@badgenam','@@region','@@title','@@idprinted')"
            'Dim cmd As New iDB2Command("insert into idbadgesp (groupcode,recnum,fname,lname,fulname,badgenam,region,title,idprinted) values('@@grpcode',@@recnum,'@@fname','@@lname','@@fulname','@@badgenam','@@region','@@title','@@idprinted')", conn)
            Dim cmd As New iDB2Command(query, conn)
            cmd.CommandText = cmd.CommandText.Replace("@@grpcode", groupcode)
            cmd.CommandText = cmd.CommandText.Replace("@@recnum", id)
            cmd.CommandText = cmd.CommandText.Replace("@@fname", fname.Trim())
            cmd.CommandText = cmd.CommandText.Replace("@@lname", lname.Trim())
            cmd.CommandText = cmd.CommandText.Replace("@@fulname", fulname.Trim())
            cmd.CommandText = cmd.CommandText.Replace("@@badgenam", badgename.Trim())
            cmd.CommandText = cmd.CommandText.Replace("@@region", region.Trim())
            cmd.CommandText = cmd.CommandText.Replace("@@title", title.Trim())
            cmd.CommandText = cmd.CommandText.Replace("@@idprinted", "0")
            'query = "insert into idbadgesp ( "
            'query += "groupcode='" & groupcode.ToString.Trim & "', "
            'query += "recnum=" & id & ", "
            'query += "fname='" & fname.ToString.Trim & "', "
            'query += "lname='" & lname.ToString.Trim & "', "
            'query += "fulname='" & fulname.ToString.Trim & "', "
            'query += "badgenam='" & badgename.ToString.Trim & "', "
            'query += "region='" & region.ToString.Trim & "', "
            'query += "title='" & title.ToString.Trim & "', "
            'query += "idprinted='" & idprint.ToString.Trim & "' "
            'query += "where (recnum=" & id & " and groupcode='" & groupcode & "')"

            'Command = New iDB2Command(query, conn)
            rc = cmd.ExecuteNonQuery()

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateIDBadges(ByVal groupcode As String, ByVal id As String, ByVal fname As String, ByVal lname As String, ByVal fulname As String, ByVal badgename As String, ByVal region As String, ByVal title As String, ByVal idprinted As Boolean) As Boolean
        '---------------------------------------------------------
        'Function: UpdateRegistrant
        'Desc. . : Updates the information about an attendie.
        'Parms . : Table of registrants, table of activities, table of group info
        'Returns : Returns a boolean object.
        '---------------------------------------------------------
        Dim Command As iDB2Command
        Dim query As String = String.Empty
        Dim rc As New Boolean
        Dim idprint As String
        Dim wtext As String

        'Reset last error
        mvLastError = ""

        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            If idprinted Then
                idprint = "1"
            Else
                idprint = "0"
            End If

            wtext = fname.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            fname = wtext
            wtext = lname.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            lname = wtext
            wtext = fulname.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            fulname = wtext
            wtext = badgename.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            badgename = wtext
            wtext = region.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            region = wtext
            wtext = title.ToString().Trim()
            wtext = wtext.Replace("'", "''")
            title = wtext

            query = "update idbadgesp set "
            query += "groupcode='" & groupcode.ToString.Trim & "', "
            query += "recnum=" & id & ", "
            query += "fname='" & fname.ToString.Trim & "', "
            query += "lname='" & lname.ToString.Trim & "', "
            query += "fulname='" & fulname.ToString.Trim & "', "
            query += "badgenam='" & badgename.ToString.Trim & "', "
            query += "region='" & region.ToString.Trim & "', "
            query += "title='" & title.ToString.Trim & "', "
            query += "idprinted='" & idprint.ToString.Trim & "' "
            query += "where (recnum=" & id & " and groupcode='" & groupcode & "')"

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateIDBadgeFlag() As Boolean
        '---------------------------------------------------------
        'Function: UpdateRegistrant
        'Desc. . : Updates the information about an attendie.
        'Parms . : Table of registrants, table of activities, table of group info
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

            query = "update idbadgesp set "
            query += "idprinted='1' "
            query += "where (groupcode='HUMB' and idprinted='0')"

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateIDBadgeFlag(ByVal grpcode As String) As Boolean
        '---------------------------------------------------------
        'Function: UpdateRegistrant
        'Desc. . : Updates the information about an attendie.
        'Parms . : Group ID to update
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

            query = "update idbadgesp set "
            query += "idprinted='1' "
            query += "where (groupcode='" + grpcode + "' and idprinted='0')"

            Command = New iDB2Command(query, conn)
            rc = Command.ExecuteNonQuery

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

    Shared Function UpdateEnvelopesFlag(ByVal dt As DataTable) As Boolean
        '---------------------------------------------------------
        'Function: UpdateNameTagFlag
        'Desc. . : Updates the Envelope printed flag on CVREGP
        'Parms . : Table of registrants
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

            For Each row As DataRow In dt.Rows

                query = "update cvregp set "
                query += "envelope = '1' "
                query += "where bidid=" & row("bidid")

                Command = New iDB2Command(query, conn)
                rc = Command.ExecuteNonQuery

            Next

            Return rc

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

End Class
