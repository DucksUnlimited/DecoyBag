
Imports System.Data
Imports System.IO
Imports iSeriesDB.iSeriesCatalog

Partial Class CatMaint004
    Inherits System.Web.UI.Page
    Public parms As New ClassSessionManager
    'Public oiSeries As New ClassiSeriesDataAccess



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rdonly As Boolean
        Dim eventType As String
        Dim toolDesc As String
        rdonly = False
        eventType = "Z"

        Try

            If Not Page.IsPostBack Then
                Dim wkdate As String
                Dim dtcats As DataTable = LoadCatsGroupsDataTable(rdonly, eventType)
                DropDownGroup.DataSource = dtcats
                DropDownGroup.DataTextField = "CATDESC"
                DropDownGroup.DataValueField = "CATGRP"
                DropDownGroup.DataBind()

                If Session("sLOADREC") = "1" Then
                    Dim dtitml As DataTable = GetItem(Session("sCCCON#"), Session("sCCNUMB"), Session("sCCEFFL"))
                    Dim dr1 As DataRow
                    If Not IsNothing(dtitml) Then
                        dr1 = dtitml.Rows(0)

                        Dim imgPath As String = "/Images/" + dr1("ccnumb").ToString().Trim() + ".jpg"
                        ItemImage.ImageUrl = "~/Images/" + dr1("ccnumb").ToString.Trim + ".jpg"
                        If File.Exists(imgPath) Then
                        Else
                            ItemImage.ImageUrl = "~/Images/ImageNotAvailable.jpg"
                        End If
                        Dim txtPath As String = "/Doc/Descriptions/" + dr1("ccnumb").ToString().Trim() + ".txt"
                        'ItemImage.ImageUrl = "~/Images/" + dr1("ccnumb").ToString.Trim + ".jpg"
                        If File.Exists(txtPath) Then
                            ItemDesc.Visible = True
                            Dim objStreamReader As StreamReader
                            objStreamReader = File.OpenText(txtPath)
                            toolDesc = "<ul>"
                            While objStreamReader.Peek <> -1
                                toolDesc += "<li>" + objStreamReader.ReadLine() + "</li>"
                            End While
                            objStreamReader.Close()
                            toolDesc += "</ul>"
                            ItemDesc.Text = toolDesc
                        Else
                            ItemDesc.Visible = False
                        End If

                        TxtItem.Text = dr1("CCNUMB").ToString
                        wkdate = dr1("CCEFFL").ToString
                        ItmEffDate.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                        wkdate = dr1("CCEXPL").ToString
                        ItmExpDate.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                        TxtIncQty.Text = dr1("CCMQOH").ToString
                        TxtMaxQty.Text = dr1("CCEOP").ToString

                        DropDownGroup.Items.Insert(0, " ")
                        'DropDownGroup.SelectedIndex = 0
                        DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dr1("CCRUOM")))
                        DropDownGroup.SelectedIndex = DropDownGroup.Items.IndexOf(DropDownGroup.Items.FindByValue(dr1("CATGRP")))
                        TxtSeqNum.Text = dr1("CCISEQ").ToString
                        Session("sCCEFFL") = dr1("CCEFFL").ToString

                    End If
                    Session("sLOADREC") = "0"

                    TxtItem.Enabled = False

                Else
                    ItemImage.ImageUrl = "~/Images/ImageNotAvailable.jpg"
                    TxtItem.Enabled = True
                    wkdate = DateTime.Now.ToString("yyyyMMdd")
                    ItmEffDate.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                    wkdate = "99991231"
                    ItmExpDate.SelectedDate = Mid$(wkdate, 5, 2) & "/" & Mid$(wkdate, 7, 2) & "/" & Mid$(wkdate, 1, 4)
                    TxtIncQty.Text = "1"
                    TxtMaxQty.Text = "1"


                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ButtonSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        Dim rtnbool As Boolean
        Dim itmUOM As String

        Try

            Select Case Session("sMode")
                Case "INSERT"
                    itmUOM = GetItemUOM(TxtItem.Text.ToUpper.Trim)
                    If Not IsNothing(itmUOM) And Not itmUOM = "" And Not itmUOM = " " Then
                        rtnbool = InsertItem(TxtItem.Text.ToUpper.Trim, Format(ItmEffDate.SelectedDate, "yyyyMMdd"), Format(ItmExpDate.SelectedDate, "yyyyMMdd"), TxtIncQty.Text, TxtMaxQty.Text, DropDownList1.SelectedItem.Value, DropDownGroup.SelectedItem.Value, Session("sCCCON#"), TxtSeqNum.Text, itmUOM)
                    End If
                Case "UPDATE"
                    rtnbool = UpdateItem(TxtItem.Text.ToUpper.Trim, Format(ItmEffDate.SelectedDate, "yyyyMMdd"), Format(ItmExpDate.SelectedDate, "yyyyMMdd"), TxtIncQty.Text, TxtMaxQty.Text, DropDownList1.SelectedItem.Value, DropDownGroup.SelectedItem.Value, Session("sCCCON#"), TxtSeqNum.Text, Session("sCCEFFL"))
            End Select

            If fileup.HasFile Then
                Dim fExt = Path.GetExtension(fileup.FileName).ToLower
                Dim fName = Path.GetFileNameWithoutExtension(fileup.FileName).ToUpper
                Dim fileName = fName.Trim() & fExt
                Dim targetFolder As String = Server.MapPath("Images/")
                fileup.SaveAs(Path.Combine(targetFolder, fileName))
            End If
            If DescUpload.HasFile Then
                Dim fExt = Path.GetExtension(DescUpload.FileName).ToLower
                Dim fName = Path.GetFileNameWithoutExtension(DescUpload.FileName).ToUpper
                Dim fileName = fName.Trim() & fExt
                Dim targetFolder As String = Server.MapPath("Doc/Descriptions/")
                DescUpload.SaveAs(Path.Combine(targetFolder, fileName))
            End If
            If rtnbool = True Then
                Response.Redirect("CatMaint003.aspx")
            Else
                LblError.Text = "Item Error"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        Try

            Response.Redirect("CatMaint003.aspx")

        Catch ex As Exception

        End Try
    End Sub
End Class
