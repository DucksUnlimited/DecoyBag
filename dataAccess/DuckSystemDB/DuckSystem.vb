Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Net
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Configuration
Imports commonFunctions.GeneralRoutines

Partial Public Class DuckSystem

    Shared sURL As String
    Shared wrGETURL As WebRequest
    Shared objStream As Stream
    Shared sLastError As String
    'Shared objGen As GeneralRoutines

    Shared Function GetEventData(ByVal eventkey As String, ByVal eventdate As String, ByVal rdid As String, ByVal coorid As String, ByVal userid As String) As DataTable
        Try

            'Build event key for ducksystem
            Dim pscyeKey As String = (eventkey.Substring(0, 2) + "0" + eventkey.Substring(2, 3))
            Dim year As Long = eventdate.Substring(0, 4)
            Dim month As Long = eventdate.Substring(4, 2)
            Dim eventcode As String
            If month > 7 Then
                year = year + 1
            End If
            eventcode = TranslateEventCode(eventkey.Substring(5, 1))
            pscyeKey += (year.ToString + eventcode)
            sURL = "http://www.ducksystem.com/cgibin/www.cgi?&tp=IS&pg=SPW185E&kp=" + pscyeKey + eventdate + rdid + coorid + userid
            'Set URL 

            wrGETURL = WebRequest.Create(sURL)
            objStream = wrGETURL.GetResponse.GetResponseStream()

            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            Dim sLineType As String = ""
            Dim array As ArrayList = New ArrayList
            Dim i As Integer = 0
            Dim dt1 As New DataTable

            'Add our data table columns
            dt1.Columns.Add("ParmType", Type.GetType("System.String"))
            dt1.Columns.Add("ParmData", Type.GetType("System.String"))

            Dim dr1 As DataRow = Nothing
            Do While Not sLine Is Nothing
                'Read line
                sLine = objReader.ReadLine
                'If data returned, Load parameters from DuckSystem
                If Not sLine Is Nothing Then
                    'Load parameters from DuckSystem
                    dr1 = dt1.NewRow()
                    'Set fields for data row
                    dr1.Item("ParmType") = sLine.Substring(0, 5)
                    dr1.Item("ParmData") = sLine.Substring(5).Trim()

                    'Add the row to the array
                    dt1.Rows.Add(dr1)

                End If

            Loop

            Return dt1
            'Send data table back to caller
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Shared Function GetMemberData(ByVal statecode As String, ByVal lastname As String, ByVal firstname As String) As DataTable

        'Reset last error
        Dim mvLastError As String = ""

        mvLastError = ""

        Try

            sURL = "http://www.ducksystem.com/cgibin/www.cgi?&tp=IS&pg=CBW861E&kp=" + statecode + "," + lastname + "," + firstname
            'Set URL 

            wrGETURL = WebRequest.Create(sURL)
            objStream = wrGETURL.GetResponse.GetResponseStream()

            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            Dim sLineType As String = ""
            Dim array As ArrayList = New ArrayList
            Dim i As Integer = 0
            Dim dt1 As New DataTable

            'Add our data table columns
            dt1.Columns.Add("firstname", Type.GetType("System.String"))
            dt1.Columns.Add("lastname", Type.GetType("System.String"))
            dt1.Columns.Add("nickname", Type.GetType("System.String"))
            dt1.Columns.Add("street1", Type.GetType("System.String"))
            dt1.Columns.Add("street2", Type.GetType("System.String"))
            dt1.Columns.Add("city", Type.GetType("System.String"))
            dt1.Columns.Add("state", Type.GetType("System.String"))
            dt1.Columns.Add("country", Type.GetType("System.String"))
            dt1.Columns.Add("zipcode", Type.GetType("System.String"))
            dt1.Columns.Add("phone", Type.GetType("System.String"))
            dt1.Columns.Add("busphone", Type.GetType("System.String"))
            dt1.Columns.Add("cnaid", Type.GetType("System.String"))
            dt1.Columns.Add("email", Type.GetType("System.String"))

            Dim dr1 As DataRow = Nothing
            Do While Not sLine Is Nothing
                'Read line
                sLine = objReader.ReadLine
                'If data returned, Load parameters from DuckSystem
                If Not sLine Is Nothing Then
                    'Load parameters from DuckSystem
                    dr1 = dt1.NewRow()
                    'Set fields for data row
                    dr1.Item("firstname") = sLine.Substring(0, 30)
                    dr1.Item("lastname") = sLine.Substring(30, 40)
                    dr1.Item("nickname") = sLine.Substring(70, 30)
                    dr1.Item("street1") = sLine.Substring(100, 60)
                    dr1.Item("street2") = sLine.Substring(160, 60)
                    dr1.Item("city") = sLine.Substring(220, 40)
                    dr1.Item("state") = sLine.Substring(260, 2)
                    dr1.Item("country") = sLine.Substring(262, 30)
                    dr1.Item("zipcode") = sLine.Substring(292, 15)
                    dr1.Item("phone") = sLine.Substring(307, 10)
                    dr1.Item("busphone") = sLine.Substring(317, 10)
                    dr1.Item("email") = sLine.Substring(327, 70)
                    dr1.Item("cnaid") = sLine.Substring(397, 9)

                    'Add the row to the array
                    dt1.Rows.Add(dr1)

                End If

            Loop

            Return dt1
            'Send data table back to caller
        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try
    End Function

    Shared Function GetHouseholdData(ByVal cnaid As String) As DataTable

        'Reset last error
        Dim mvLastError As String = ""

        mvLastError = ""

        Try

            sURL = "http://www.ducksystem.com/cgibin/www.cgi?&tp=IS&pg=CBW862E&kp=" + cnaid
            'Set URL 

            wrGETURL = WebRequest.Create(sURL)
            objStream = wrGETURL.GetResponse.GetResponseStream()

            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            Dim sLineType As String = ""
            Dim array As ArrayList = New ArrayList
            Dim i As Integer = 0
            Dim dt1 As New DataTable

            'Add our data table columns
            dt1.Columns.Add("firstname", Type.GetType("System.String"))
            dt1.Columns.Add("lastname", Type.GetType("System.String"))
            dt1.Columns.Add("nickname", Type.GetType("System.String"))
            dt1.Columns.Add("street1", Type.GetType("System.String"))
            dt1.Columns.Add("street2", Type.GetType("System.String"))
            dt1.Columns.Add("city", Type.GetType("System.String"))
            dt1.Columns.Add("state", Type.GetType("System.String"))
            dt1.Columns.Add("country", Type.GetType("System.String"))
            dt1.Columns.Add("zipcode", Type.GetType("System.String"))
            dt1.Columns.Add("phone", Type.GetType("System.String"))
            dt1.Columns.Add("busphone", Type.GetType("System.String"))
            dt1.Columns.Add("cnaid", Type.GetType("System.String"))
            dt1.Columns.Add("email", Type.GetType("System.String"))

            Dim dr1 As DataRow = Nothing
            Do While Not sLine Is Nothing
                'Read line
                sLine = objReader.ReadLine
                'If data returned, Load parameters from DuckSystem
                If Not sLine Is Nothing Then
                    'Load parameters from DuckSystem
                    dr1 = dt1.NewRow()
                    'Set fields for data row
                    dr1.Item("firstname") = sLine.Substring(0, 30)
                    dr1.Item("lastname") = sLine.Substring(30, 40)
                    dr1.Item("nickname") = sLine.Substring(70, 30)
                    dr1.Item("street1") = sLine.Substring(100, 60)
                    dr1.Item("street2") = sLine.Substring(160, 60)
                    dr1.Item("city") = sLine.Substring(220, 40)
                    dr1.Item("state") = sLine.Substring(260, 2)
                    dr1.Item("country") = sLine.Substring(262, 30)
                    dr1.Item("zipcode") = sLine.Substring(292, 15)
                    dr1.Item("phone") = sLine.Substring(307, 10)
                    dr1.Item("busphone") = sLine.Substring(317, 10)
                    dr1.Item("email") = sLine.Substring(327, 70)
                    dr1.Item("cnaid") = sLine.Substring(397, 9)

                    'Add the row to the array
                    dt1.Rows.Add(dr1)

                End If

            Loop

            Return dt1
            'Send data table back to caller
        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try
    End Function

    Shared Function GetChapterListState(ByVal sState As String) As DataTable

        Try

            'Set URL 
            sURL = "http://www.ducksystem.com/cgibin/www.cgi?&tp=IS&pg=CBW284ES&ex=" + sState

            wrGETURL = WebRequest.Create(sURL)
            objStream = wrGETURL.GetResponse.GetResponseStream()

            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            Dim array As ArrayList = New ArrayList
            Dim i As Integer = 0
            Dim dt1 As New DataTable

            'Add our data table columns
            dt1.Columns.Add("State", Type.GetType("System.String"))
            dt1.Columns.Add("ChapterNumber", Type.GetType("System.String"))
            dt1.Columns.Add("ChapterName", Type.GetType("System.String"))
            dt1.Columns.Add("RDAdminLoc", Type.GetType("System.String"))

            'Read each record and add to data table
            Dim dr1 As DataRow = Nothing
            Do While Not sLine Is Nothing
                ' i += 1
                'Read line
                sLine = objReader.ReadLine
                'If data returned, parse into data columns
                If Not sLine Is Nothing Then
                    'Create new data row
                    dr1 = dt1.NewRow()
                    'Set fields for data row
                    dr1.Item("State") = sLine.Substring(0, 2)
                    dr1.Item("ChapterNumber") = sLine.Substring(2, 4)
                    dr1.Item("ChapterName") = sLine.Substring(6, 30)
                    dr1.Item("RDAdminLoc") = sLine.Substring(36, 6)
                    'Add the row to the array
                    dt1.Rows.Add(dr1)
                End If

            Loop

            'Send data table back to caller
            Return dt1

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Shared Function GetRDListState(ByVal sState As String) As DataTable

        Try

            'Set URL 
            sURL = "http://www.ducksystem.com/cgibin/www.cgi?&tp=IS&pg=CBW325E&ex=" + sState

            wrGETURL = WebRequest.Create(sURL)
            objStream = wrGETURL.GetResponse.GetResponseStream()

            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            Dim array As ArrayList = New ArrayList
            Dim i As Integer = 0
            Dim dt1 As New DataTable

            'Add our data table columns
            dt1.Columns.Add("RdNumber", Type.GetType("System.String"))
            dt1.Columns.Add("Location", Type.GetType("System.String"))
            dt1.Columns.Add("CnaId", Type.GetType("System.String"))
            dt1.Columns.Add("RdFName", Type.GetType("System.String"))
            dt1.Columns.Add("RdLName", Type.GetType("System.String"))

            'Read each record and add to data table
            Dim dr1 As DataRow = Nothing
            Do While Not sLine Is Nothing
                ' i += 1
                'Read line
                sLine = objReader.ReadLine
                'If data returned, parse into data columns
                If Not sLine Is Nothing Then
                    'Create new data row
                    dr1 = dt1.NewRow()
                    'Set fields for data row
                    dr1.Item("RdNumber") = sLine.Substring(0, 2)
                    dr1.Item("Location") = sLine.Substring(2, 6)
                    dr1.Item("CnaId") = sLine.Substring(8, 9)
                    dr1.Item("RdFName") = sLine.Substring(17, 30)
                    dr1.Item("RdLName") = sLine.Substring(47)
                    'Add the row to the array
                    dt1.Rows.Add(dr1)
                End If

            Loop

            'Send data table back to caller
            Return dt1

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Shared Function GetRDLocation(ByVal sRDId As String) As String

        Try

            'Set URL 
            sURL = "http://www.ducksystem.com/cgibin/www.cgi?&tp=IS&pg=CBW326E&ex=0" + sRDId

            wrGETURL = WebRequest.Create(sURL)
            objStream = wrGETURL.GetResponse.GetResponseStream()

            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""

            'Read line
            sLine = objReader.ReadLine

            'Send location back to caller
            Return sLine

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Class


