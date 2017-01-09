'Imports IBM.Data.DB2.iSeries
Imports System.Collections
Imports System.Data
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports commonFunctions.ListItemComparer


Public Class GeneralRoutines
    Inherits System.Web.UI.Page

    'Public parms As New ClassSessionManager
    Shared ar As ListItem()

    Friend Property DT() As DataTable
        Get
            If Session("DT") Is Nothing Then
                Session.Add("DT", New DataTable())
            End If

            Return DirectCast(Session("DT"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("DT") = value
        End Set
    End Property

    Friend Property DTOrder() As DataTable
        Get
            If Session("DTOrder") Is Nothing Then
                Session.Add("DTOrder", New DataTable())
            End If

            Return DirectCast(Session("DTOrder"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("DTOrder") = value
        End Set
    End Property

    Shared Sub SortDropDown(ByVal dd As DropDownList)
        Dim i As Long = 0
        For Each li As ListItem In dd.Items
            ReDim Preserve ar(i)
            ar(i) = li
            i += 1
        Next
        Dim ar1 As Array = ar

        ar1.Sort(ar1, New ListItemComparer)
        dd.Items.Clear()
        dd.Items.AddRange(ar1)
    End Sub

    Shared Function sortTable(ByVal table As DataTable, ByVal colname As String) As DataTable

        Dim newDT As DataTable = table.Clone()
        Dim rowCount As Integer = table.Rows.Count

        ' Select table rows in sorted order by the column name sent in
        Dim foundRows As DataRow() = table.[Select](Nothing, colname)

        ' Load sorted table into new table in sorted order
        Dim i As Integer = 0
        ' Add row to new table
        While i < rowCount
            Dim arr As Object() = New Object(table.Columns.Count - 1) {}
            Dim j As Integer = 0
            ' Add row columns into new table row
            While j < table.Columns.Count
                arr(j) = foundRows(i)(j)
                System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
            End While
            Dim data_row As DataRow = newDT.NewRow()
            data_row.ItemArray = arr
            newDT.Rows.Add(data_row)
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While

        Return newDT

    End Function

    Shared Function counter() As String

        Dim input As String = Nothing
        Dim mycounter As String = ""

        Try

            ' Check to see if file exist if not create it and initialize it to 0
            Dim fExist As Boolean = File.Exists("C:/counter.txt")
            If Not fExist Then
                Dim fs As New FileStream("C:/counter.txt", FileMode.Create, FileAccess.Write)
                Dim tw1 As TextWriter = New StreamWriter(fs)
                tw1.WriteLine("0")
                tw1.Close()
                tw1.Dispose()
                fs.Dispose()
            End If
            'read counter file 
            Dim re As StreamReader = File.OpenText("C:/counter.txt")
            While re.Peek <> -1
                input = re.ReadLine()
                mycounter = mycounter + input
            End While
            re.Close()
            re.Dispose()
            ' Add 1 to counter and update file with new counter 
            Dim myInt As Integer = Integer.Parse(mycounter)
            myInt = myInt + 1
            Dim tw As TextWriter = New StreamWriter("C:/counter.txt")
            tw.WriteLine(Convert.ToString(myInt))
            tw.Close()
            tw.Dispose()

            Dim sr As StreamReader = File.OpenText("C:/counter.txt")
            sr = File.OpenText("C:/counter.txt")
            input = Nothing
            mycounter = ""
            While sr.Peek <> -1
                input = sr.ReadLine()
                mycounter = mycounter + input
            End While
            sr.Close()
            sr.Dispose()
            Return mycounter

        Catch ex As Exception

            Return ex.Message

        End Try

    End Function

    Shared Function ConvertField(ByVal field As String) As String

        Dim i As Integer
        Dim letter As String
        Dim newfield As String = String.Empty
        Dim lastchar As String

        lastchar = " "

        For i = 1 To Len(field)
            If lastchar = " " Then
                If Mid(field, i, 1) <> " " Then
                    letter = Mid(field, i, 1).ToUpper
                    newfield += letter
                Else
                    newfield += Mid(field, i, 1)
                End If
                lastchar = Mid(field, i, 1)
            Else
                newfield += Mid(field, i, 1)
                lastchar = Mid(field, i, 1)
            End If
        Next

        Return newfield

    End Function

    Shared Function ConvertName(ByVal name As String) As String

        Dim i As Integer
        Dim letter As String
        Dim newname As String = String.Empty
        Dim lastchar As String

        lastchar = " "

        For i = 1 To Len(name)
            If lastchar = " " Or lastchar = "'" Then
                If (Mid(name, i, 1) <> " " And Mid(name, i, 1) <> "'") Then
                    letter = Mid(name, i, 1).ToUpper
                    newname += letter
                Else
                    newname += Mid(name, i, 1)
                End If
                lastchar = Mid(name, i, 1)
            Else
                newname += Mid(name, i, 1)
                lastchar = Mid(name, i, 1)
            End If
        Next

        Return newname

    End Function

    Public Sub InitializeSessionVairables()

        If Session("UpdateOrd") Is Nothing Then
            Session.Add("UpdateOrd", String.Empty)
        End If
        If Session("PageTitle") Is Nothing Then
            Session.Add("PageTitle", String.Empty)
        End If

        If Session("SystemTitle") Is Nothing Then
            Session.Add("SystemTitle", String.Empty)
        End If

        If Session("EventTitle") Is Nothing Then
            Session.Add("EventTitle", String.Empty)
        End If

        Session.Add("DTState", New DataTable())

        Session.Add("UpdateOrderNumber", String.Empty)
        Session.Add("UpdateOrder", Boolean.FalseString)
        Session("UpdateOrder") = False
        Session.Add("Approved", Boolean.FalseString)
        Session("Approved") = False
        Session.Add("FFLRequired", Boolean.FalseString)
        Session("FFLRequired") = False
        Session.Add("FFLCatagory", String.Empty)

        Session.Add("LBLbillchapter", String.Empty)
        Session.Add("LBLcsname", String.Empty)
        Session.Add("LBLcsadd1", String.Empty)
        Session.Add("LBLcsadd2", String.Empty)
        Session.Add("LBLcsctst", String.Empty)
        Session.Add("LBLcszipa", String.Empty)
        Session.Add("LBLcszip2", String.Empty)
        Session.Add("LBLshpchapter", String.Empty)
        Session.Add("LBLcshp1", String.Empty)
        Session.Add("LBLcshp2", String.Empty)
        Session.Add("LBLcshp3", String.Empty)
        Session.Add("LBLcshp6", String.Empty)
        Session.Add("LBLcshpza", String.Empty)
        Session.Add("LBLcshpz2", String.Empty)
        Session.Add("eMailAddr", String.Empty)
        Session.Add("comments", String.Empty)
        Session.Add("FFLOrganization", String.Empty)
        Session.Add("FFLName", String.Empty)
        Session.Add("FFLAddr", String.Empty)
        Session.Add("FFLCity", String.Empty)
        Session.Add("FFLState", String.Empty)
        Session.Add("FFLZip5", String.Empty)
        Session.Add("FFLZip4", String.Empty)
        Session.Add("FFLNumber", String.Empty)
        Session.Add("FFLExpirDate", String.Empty)


    End Sub

    Shared Function TranslateEventCode(ByVal evcd As String) As String

        Dim evnum As String = evcd
        'translate 1 character code to a 3 digit number
        If evcd.ToString > "9" Then
            Select Case evnum
                Case "A"
                    evcd = "010"
                Case "B"
                    evcd = "011"
                Case "C"
                    evcd = "012"
                Case "D"
                    evcd = "013"
                Case "E"
                    evcd = "014"
                Case "F"
                    evcd = "015"
                Case "G"
                    evcd = "016"
                Case "H"
                    evcd = "017"
                Case "I"
                    evcd = "018"
                Case "J"
                    evcd = "019"
                Case "K"
                    evcd = "020"
                Case "L"
                    evcd = "021"
                Case "M"
                    evcd = "022"
                Case "N"
                    evcd = "023"
                Case "O"
                    evcd = "024"
                Case "P"
                    evcd = "025"
                Case "Q"
                    evcd = "026"
                Case "R"
                    evcd = "027"
                Case "S"
                    evcd = "028"
                Case "T"
                    evcd = "029"
                Case "U"
                    evcd = "030"
                Case "V"
                    evcd = "031"
                Case "W"
                    evcd = "032"
                Case "X"
                    evcd = "033"
                Case "Y"
                    evcd = "034"
                Case "Z"
                    evcd = "035"
            End Select
        Else
            evcd = "00" + evnum
        End If

        Return evcd

    End Function

    Shared Function TransEventCodeNtoA(ByVal evcd As String) As String

        Dim evnum As String = evcd
        'translate 3 character code to a 1 alpha character
        Select Case evnum
            Case "010"
                evcd = "A"
            Case "011"
                evcd = "B"
            Case "012"
                evcd = "C"
            Case "013"
                evcd = "D"
            Case "014"
                evcd = "E"
            Case "015"
                evcd = "F"
            Case "016"
                evcd = "G"
            Case "017"
                evcd = "H"
            Case "018"
                evcd = "I"
            Case "019"
                evcd = "J"
            Case "020"
                evcd = "K"
            Case "021"
                evcd = "L"
            Case "022"
                evcd = "M"
            Case "023"
                evcd = "N"
            Case "024"
                evcd = "O"
            Case "025"
                evcd = "P"
            Case "026"
                evcd = "Q"
            Case "027"
                evcd = "R"
            Case "028"
                evcd = "S"
            Case "029"
                evcd = "T"
            Case "030"
                evcd = "U"
            Case "031"
                evcd = "V"
            Case "032"
                evcd = "W"
            Case "033"
                evcd = "X"
            Case "034"
                evcd = "Y"
            Case "035"
                evcd = "Z"
            Case Else
                evcd = evnum.Substring(2, 1)
        End Select

        Return evcd

    End Function

    Shared Function TransEventType2to1(ByVal evcd As String) As String

        Dim evtype As String = evcd
        'translate 3 character code to a 1 alpha character
        Select Case evtype
            Case "WA"
                evcd = "A"
            Case "T "
                evcd = "B"
            Case "D "
                evcd = "D"
            Case "SB"
                evcd = "E"
            Case "W "
                evcd = "G"
            Case "DL"
                evcd = "L"
            Case "H "
                evcd = "M"
            Case "NO"
                evcd = "N"
            Case "S "
                evcd = "S"
            Case "GB"
                evcd = "T"
            Case "WP"
                evcd = "W"
            Case "VC"
                evcd = "V"
            Case Else
                evcd = evtype.Substring(0, 1)
        End Select

        Return evcd

    End Function

    Shared Sub InzConventionTable()

        'Dim DTConv As New DTConv()
        'Dim DTConvAttend As New DTConvAttend()
        'Dim DTConvPay As New DTConvPay()


        'DTConvPay.Dispose()
        'DTConvPay.Columns.Clear()
        'DTConvPay.Reset()

        'DTConvPay.Columns.Add("groupnum", Type.GetType("System.Int64"))
        'DTConvPay.Columns.Add("cardtype", Type.GetType("System.String"))
        'DTConvPay.Columns.Add("amount", Type.GetType("System.Double"))
        'DTConvPay.Columns.Add("paystatus", Type.GetType("System.String"))
        'DTConvPay.Columns.Add("source", Type.GetType("System.String"))

    End Sub
End Class