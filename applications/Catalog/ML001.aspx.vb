Imports System.Data
Imports IBM.Data.DB2.iSeries


Partial Class ML001
    Inherits System.Web.UI.Page

    Dim lab, rpt As Boolean
    Dim connectionString As ConnectionStringSettingsCollection = ConfigurationManager.ConnectionStrings
    Dim connString As String = connectionString("S1032895").ToString
    Dim conn As New iDB2Connection(connString)
    Dim cmdZ As New iDB2Command("select epzip, epzipd from evorgzip order by epzipd", conn)
    Dim Msg As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ArrayFList As New ArrayList
        Dim myFSO As New Scripting.FileSystemObject
        Dim f = myFSO.GetFolder("//S1032895/qifs/Labels")
        Dim i As Integer, fl
        UserId.Focus()
        Session("SystemTitle") = "Membership Labels/Mailing Reports"
        Try

            conn.Open()

            If Not Page.IsPostBack Then

                If Session("PageTitle") Is Nothing Then
                    Session.Add("PageTitle", String.Empty)
                End If
                Session("PageTitle") = "Mailing Labels"

                If Session("UrlStart") Is Nothing Then
                    Session.Add("UrlStart", String.Empty)
                End If
                Session("UrlStart") = Request.UrlReferrer.AbsoluteUri

                If (Request("news").ToUpper.Equals("Y")) Then
                    Session("PageTitle") = "News Release"
                    Label4.Visible = True
                    OrgZip.Visible = True
                    Dim oz As iDB2DataReader
                    oz = cmdZ.ExecuteReader()
                    OrgZip.DataSource = oz
                    OrgZip.DataTextField = "epzipd"
                    OrgZip.DataValueField = "epzip"
                    OrgZip.SelectedValue = 24153
                    OrgZip.DataBind()
                    LCopies.Enabled = False
                    NUDE1.Enabled = False
                    SortTypeL.Enabled = False
                    LabelType.SelectedValue = 1
                    LabelType.Enabled = False
                    Reports.Enabled = False
                    RCopies.Enabled = False
                    NUDE2.Enabled = False
                    SortTypeR.Enabled = False
                End If

                SubmitJob.Visible = False
                i = 0
                For Each fl In f.Files
                    If LCase(Right(fl.Name, 3)) = "txt" Then
                        i = i + 1
                        If UCase(fl.Name) = Request("mailreq").ToUpper Then
                            FileList.SelectedValue = Request("mailreq").ToUpper
                        End If
                        ArrayFList.Add(UCase(fl.Name))
                    End If
                Next
                ArrayFList.Sort()
                FileList.DataSource = ArrayFList
                FileList.DataBind()

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Function EV45001NC() As Boolean

        Try

            Dim oCmd As New iDB2Command
            oCmd.Connection = conn

            'Build iSeries Parameter List in top/bottom seq (ordinal)

            'Set up a character parameter
            oCmd.Parameters.Add("userID", iDB2DbType.iDB2Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("userID").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("FileIn", iDB2DbType.iDB2Char, 20)
            'Always set parms as input/output for program call
            oCmd.Parameters("FileIn").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("JobNam", iDB2DbType.iDB2Char, 10)
            'Always set parms as input/output for program call
            oCmd.Parameters("JobNam").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("LABELS", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("LABELS").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("SCCOP1", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("SCCOP1").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("SLT1", iDB2DbType.iDB2Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("SLT1").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("SCCOL1", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("SCCOL1").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("GRPYN", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("GRPYN").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("REPORTS", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("REPORTS").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("SCCOP3", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("SCCOP3").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("SLT3", iDB2DbType.iDB2Char, 2)
            'Always set parms as input/output for program call
            oCmd.Parameters("SLT3").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("ORGZIP", iDB2DbType.iDB2Char, 5)
            'Always set parms as input/output for program call
            oCmd.Parameters("ORGZIP").Direction = ParameterDirection.InputOutput

            'Set up a character parameter
            oCmd.Parameters.Add("News", iDB2DbType.iDB2Char, 1)
            'Always set parms as input/output for program call
            oCmd.Parameters("News").Direction = ParameterDirection.InputOutput

            'Set all the parameters values
            oCmd.Parameters("userID").Value = UserId.Text.ToUpper
            oCmd.Parameters("FileIn").Value = FileList.SelectedValue
            oCmd.Parameters("JobNam").Value = JobName.Text.ToUpper
            If (Labels.Checked) Then
                oCmd.Parameters("LABELS").Value = "Y"
                oCmd.Parameters("SCCOP1").Value = LCopies.Text
                oCmd.Parameters("SLT1").Value = SortTypeL.Text
                oCmd.Parameters("SCCOL1").Value = LabelType.Text
                oCmd.Parameters("GRPYN").Value = "N"
            Else
                oCmd.Parameters("LABELS").Value = "N"
                oCmd.Parameters("SCCOP1").Value = "0"
                oCmd.Parameters("SLT1").Value = "  "
                oCmd.Parameters("SCCOL1").Value = "4"
                oCmd.Parameters("GRPYN").Value = "N"
            End If

            If (Reports.Checked) Then
                oCmd.Parameters("REPORTS").Value = "Y"
                oCmd.Parameters("SCCOP3").Value = RCopies.Text
                oCmd.Parameters("SLT3").Value = SortTypeR.Text
            Else
                oCmd.Parameters("REPORTS").Value = "N"
                oCmd.Parameters("SCCOP3").Value = "0"
                oCmd.Parameters("SLT3").Value = "  "
            End If

            If (Request("news").ToUpper.Equals("Y")) Then
                oCmd.Parameters("ORGZIP").Value = OrgZip.SelectedValue
                oCmd.Parameters("SLT1").Value = "  "
            Else
                oCmd.Parameters("ORGZIP").Value = " "
            End If
            oCmd.Parameters("News").Value = Request("news").ToUpper



            'Set up program call command line
            'The question marks are parameter markers for the call
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = "EV45001NC"

            'Execute iSeries non-stored procedure program call
            oCmd.ExecuteNonQuery()

            'Return true if successful 
            Return True

        Catch ex As Exception

            'Set class level error message as well
            Msg = ex.Message
            Return False

        End Try

    End Function

    Function CheckUser() As Boolean

        'Dim vuS As String = "DataSource=S1032895;userid=" & Request("UserID").ToUpper & ";password=" & Request("Password").ToUpper
        Dim vuS2 As String = "DataSource=S1032895;userid=" & UserId.Text.ToUpper & ";password=" & Password.Text
        Dim vuC As New iDB2Connection(vuS2)

        Try

            vuC.Open()

        Catch ex As iDB2Exception
            If ex.MessageDetails.Contains("CWBSY0001") Then
                Msg = "User <b> " & Request("userId").ToUpper & "</b> does not exist!"
                UserId.Focus()
            Else
                If ex.MessageDetails.Contains("CWBSY0002") Then
                    Msg = "The password is not correct for the specified user ID."
                    Password.Focus()
                Else
                    If ex.MessageDetails.Contains("CWBSY0003") Then
                        Msg = "The password you typed has expired."
                        Password.Focus()
                    Else
                        If ex.MessageDetails.Contains("CWBSY0011") Then
                            Msg = "Your user ID has been Disabled!"
                            UserId.Focus()
                        Else
                            If ex.MessageDetails.Contains("CWBSY0270") Then
                                Msg = "An incorrect password was specified. If one more incorrect password is specified,<br /> your user profile on the server will be disabled."
                                UserId.Focus()
                            Else
                                Msg = ex.MessageDetails
                            End If
                        End If
                    End If
                End If
            End If

            vuC.Close()
            Return False

        End Try

        vuC.Close()
        Return True

    End Function

    Protected Sub SbmJob_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SbmJob.Click

        Try

            Dim rc As Boolean

            SubmitJob.Visible = True

            rc = CheckUser()

            If rc = False Then
                SubmitJob.Text = Msg
            Else
                rc = EV45001NC()

                If rc = True Then
                    SubmitJob.Text = "Job - <b>" & JobName.Text.ToUpper & "</b> submitted to batch."
                Else
                    SubmitJob.Text = "Job had errors DID NOT SUBMIT <br />" & Msg & "<br />"
                End If
            End If
            conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DuckSystem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DuckSystem.Click

        conn.Close()
        Response.Redirect(Session("UrlStart"))

    End Sub
End Class