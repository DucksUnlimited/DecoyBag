Imports Microsoft.VisualBasic
Imports IBM.Data.DB2.iSeries
Imports System.data
Imports System.Web.HttpContext
Imports System.Configuration

Public Class iSeriesCommon

    Shared conn As New iDB2Connection(ConfigurationManager.AppSettings("iSeries"))
    Shared mvLastError As String = ""

    Shared Function GetLastError() As String
        '---------------------------------------------------------
        'Function: GetLastError
        'Desc. . : Return last error info
        'Parms . : None
        'Returns : Returns a string variable with message
        '---------------------------------------------------------
        Try
            Return mvLastError
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Shared Function GetCoordinatorInfo(ByVal rdnum As String) As String
        '-------------------------------------------------------
        'function  : GetCoordinatorInfo
        'Desc. . . : Get the Coordinators cna-id.
        '-------------------------------------------------------

        Dim coorcnaid As String = String.Empty
        Dim dr As iDB2DataReader
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

    Shared Function LoadStateDropDown() As DataTable
        '---------------------------------------------------------
        'Function: LoadStateDropDown
        'Desc. . : Loades a data table of state name and codes.
        'Parms . : 0
        'Returns : Returns a data table object.
        '---------------------------------------------------------

        Dim query As String = String.Empty
        Dim dt As New DataTable

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
            Return Nothing
        End Try

    End Function

End Class
