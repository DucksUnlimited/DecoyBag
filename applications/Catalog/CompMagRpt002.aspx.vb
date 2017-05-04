Imports System.Data
Imports System.Web.UI.WebControls
Imports iSeriesDB.iSeriesCompMag

Partial Class CompMagRpt002
    Inherits System.Web.UI.Page
    Public parms As New ClassSessionManager

    Dim mvLastError As String
#Region "Properties"

    Private Property DT() As DataTable
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


    Private Property DT2() As DataTable
        Get
            If Session("DT2") Is Nothing Then
                Session.Add("DT2", New DataTable())
            End If

            Return DirectCast(Session("DT2"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("DT2") = value
        End Set
    End Property


#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Reset last error
        mvLastError = ""

        Try
            ' get session manager object
            parms = Session("oSessionManager")

            If Not Page.IsPostBack Then


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

                Session("SystemTitle") = " "
                Session("UpdateOrd") = "Comp. Magazine Last Send Info"
                Session("EventTitle") = " "
                Session("PageTitle") = " "


                DT = GetCompMagDate()
                Dim dr1 As DataRow
                If Not IsNothing(DT) Then
                    dr1 = DT.Rows(0)
                    TxtSendDate.Text = dr1("MLCHGD").ToString
                    TxtSendTime.Text = dr1("MLCHGT").ToString
                End If

                DT2 = GetCompMagLog()

                If DT2.Rows.Count > 0 Then
                    GridView1.DataSource = DT2
                    GridView1.DataBind()
                    GridView1.Visible = True
                Else
                    GridView1.Visible = False
                End If


            End If


        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try


    End Sub

    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        'Reset last error
        mvLastError = ""


        Try
            Response.Redirect("CompMagMenu001.aspx")

        Catch ex As Exception
            mvLastError = ex.Message
            'Return Nothing

        End Try


    End Sub
End Class
