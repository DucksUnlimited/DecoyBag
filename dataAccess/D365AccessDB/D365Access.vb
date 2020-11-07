Imports System.Configuration
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

    Shared Function GetItemUOM(ByVal itemnum As String) As String
        '-------------------------------------------------------
        'Desc. . . : Get the unit of measure for catalog item
        '                 and verify item number is valid
        '-------------------------------------------------------

        If Not conn.State = ConnectionState.Open Then
            conn.Open()
        End If

        Dim itemuom As String = String.Empty
        Dim dr As SqlDataReader
        Try

            Dim query As String = String.Empty

            'build select for item
            query = "select UnitOM from DU_InventOnHand"
            query += " where ITEMNUMBER = '" + itemnum + "'"

            Dim sc As SqlCommand = New SqlCommand(query, conn)

            dr = sc.ExecuteReader()

            If dr.Read Then
                'Item unit of measure
                itemuom = dr("UnitOM")
            End If

            Return itemuom

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
