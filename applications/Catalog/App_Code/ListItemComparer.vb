Imports System.Collections
Imports Microsoft.VisualBasic

Public Class ListItemComparer
    Implements system.collections.IComparer
    Public Function Compare(ByVal x As Object, _
          ByVal y As Object) As Integer _
          Implements System.Collections.IComparer.Compare
        Dim a As ListItem = x
        Dim b As ListItem = y
        Dim c As New CaseInsensitiveComparer
        Return c.Compare(a.Text, b.Text)
    End Function
End Class
