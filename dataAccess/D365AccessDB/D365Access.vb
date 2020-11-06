'Imports System
Imports System.Configuration
'Imports System.Data
Imports System.Data.SqlClient


Public Class D365Access
    
    Shared ReadOnly conn As New SqlConnection(ConfigurationManager.AppSettings("D365SQL"))
    Shared mvLastError As String = ""
    
    Shared Function GetD365Items() As DataTable

        '---------------------------------------------------------
        'Function: Get D365 Inventory items 
        'Desc. . : Get Inventory items as a Data Table.
        'Parms . : 1.) 
        'Returns : Returns a data table.
        '---------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim query As String = String.Empty
        Dim dt As New DataTable

        'Reset last error
        mvLastError = ""

        Try

            query = "Select ITEMNUMBER, PRODUCTNAME as itmdsc, CATEGORY, WAREHOUSE,ONHANDQUANTITY as avalqty,"
            query += " SALESPRICE From DU_InventOnHand Order By ItemNumber"

            Dim da As SqlDataAdapter = New SqlDataAdapter(query, conn)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            mvLastError = ex.Message
            Return Nothing
        End Try

    End Function

End Class
