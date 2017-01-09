<%@ Page Language="VB" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.IO" %>

<script language="VB" runat=server>

Class DirectoryLister

    Public Shared Function Go( dirToList As String ) As String

    ' declare the StreamReader, for accessing the file
    Dim strWriter As StringWriter = New StringWriter()
    Console.SetOut(strWriter)

    Try
        Dim dir As DirectoryInfo = New DirectoryInfo( dirToList )

        Console.WriteLine()
        Console.WriteLine()
        Console.WriteLine("Following is a listing for directory: {0}", Path.GetFullPath(dir.ToString()))
        Console.WriteLine()
            Console.WriteLine("{0, -25} {1,-12:N0} {2, -12} {3,-20:g}", "Name", _
                ("Size").PadLeft(12), "Type", "Creation Time")
            Console.WriteLine("------------------------------------------------------------------")

        Dim fsi As FileSystemInfo

        For Each fsi In dir.GetFileSystemInfos()
            Try
            Dim creationTime As DateTime = fsi.CreationTime
            Dim subLength As Integer = 25

                If TypeOf fsi Is FileInfo Then

                Dim f As FileInfo = CType( fsi, FileInfo )

                ' this if statement simply ensures that we do not shorten the name of the file too much!
                If (f.Name.Length < subLength) Then
                    subLength = f.Name.Length
                End If

                    Dim name As String = f.Name.Substring(0, subLength)

                        Dim size As Long = f.Length

                '  format the output to the screen
                    Console.WriteLine("{0, -25} {1,-12:N0} {2, -12} {3,-20:g}", _
                        name, (size & " KB").PadLeft(12), "File", creationTime)

                Else ' it must be a directory

                Dim d As DirectoryInfo = CType( fsi, DirectoryInfo )

                ' this if statement simply ensures that we do not shorten the name of the file too much!
                If (d.Name.Length < subLength) Then
                    subLength = d.Name.Length
                End If

                Dim dirName As String = d.Name.Substring(0, subLength)

                '  format the output to the screen
                        Console.WriteLine("{0, -25} {1,-12:N0} {2, -12} {3,-20:g}", _
                    dirName, ("").PadLeft(12), "File Folder", creationTime)
                End If
            Catch E As Exception
            ' ignore the error, and try the next item...
            End Try
        Next fsi

    Catch E As Exception
        Console.WriteLine("--- PROCESS ENDED ---")
        Console.WriteLine(e.Message)
        return strWriter.ToString()
    End Try

    Go = strWriter.ToString()
    End Function
End Class

Sub btnDir_Click(Source As Object, E As EventArgs)

    output.Text = "<pre>" & DirectoryLister.Go( txtDir.Text ) & "</pre>"
End Sub

</script>

<html>
<head>
<link rel="stylesheet" href="http://ww1.ducksystem.com/NATURAL/99723934.CSS" type="text/css"/>
<link rel="stylesheet" href="http://ww1.ducksystem.com/NATURAL/99723933.CSS" type="text/css" media="print"/>
</head>

<body style="background-color: #f6e4c6">
<form id="Form1" method=post runat="server">

    <p>
    <table>
    <tr align=left><td><b>The following example prints the folders and files for the specified directory, to the screen.</b> It uses the FileInfo and DirectoryInfo objects to obtain this information, and uses formatting to make sure it is displayed correctly.
<hr>
    </td></tr>

    <tr><td>For Example:<br>
    <asp:label id="txtDir" text="//S1032895/qifs/Labels" runat="server"/> <asp:button id="btnDir" text="List Directory" onclick="btnDir_Click" runat="server"/></td>
    </tr>
    <tr><td><h4><asp:label id="output" runat="server"/></h4></td></tr>
    </table>

</form>
</body>
</html>
