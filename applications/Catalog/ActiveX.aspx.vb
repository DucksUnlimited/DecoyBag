Imports system
Imports System.IO
Imports System.Net
Imports System.Data



Partial Class ActiveX
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim objDucks As New ClassDucksSystemCalls
        Dim objGen As New GeneralRoutines
        Dim dtChapterInfo As DataTable

        'Only do this code if not a postback 
        If Not IsPostBack Then
            dtChapterInfo = objDucks.GetStateChapterList("47")
            DropDownList1.DataSource = dtChapterInfo
            DropDownList1.DataTextField = "ChapterName"
            DropDownList1.DataValueField = "StateChapKey"
            DropDownList1.DataBind()
            objGen.SortDropDown(DropDownList1)
        End If



    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged

    End Sub
  

End Class
