   <script language="VB" runat="server">

       Sub DoUpload(Sender As Object, e As System.EventArgs)
          Dim sPath as String
          Dim sFile as String
          Dim sFullPath as String
          Dim sSplit() as String
          Dim sPathFriendly as String
          
         
          
          'Upload to same path as script
          'Internet Anonymous User must have write permissions
           'sPath = Server.MapPath('Images/Inventory')
           sPath = Server.MapPath(".") & "\Images"
         If Right(sPath, 1) <> "\" then 
			sPathFriendly = sPath 'Friendly path name for display
			sPath = sPath & "\"
		Else
			sPathFriendly = Left(sPath, Len(sPath) - 1)
		End If
			
          
          'Save as same file name being posted
          'The code below resolves the file name
          '(removes path info)
          sFile = txtUpload.PostedFile.FileName
          sSplit = Split(sFile, "\")
          sFile = sSplit(Ubound(sSplit))
          
          
          sFullPath = sPath & sFile
		  Try
          txtUpload.PostedFile.SaveAs(sFullPath)
         lblResults.Text = "Upload of File " & sFile & " to " & sPathFriendly & " succeeded"
         
         Catch Ex as Exception
			
			lblResults.Text = "Upload of File " & sFile & " to " & sPathFriendly & " failed for the following reason: " & Ex.Message
         Finally
			lblResults.Font.Bold = True
			lblResults.Visible = true
         End Try
		
       End Sub

    </script>
<html>

    <head>
        <title></title>
    </head>

    <body>

      <form enctype="multipart/form-data" runat="server">
			
          <font face="+1"><b>

           Select File To Upload:</b></font>
           <input id="txtUpload" type="file" runat="server"/>
			<p align="center">
          <asp:button id="btnUpload" Text="Upload File" OnClick="DoUpload" runat="server"/>
			</p>
       <!--   <hr noshade> -->

          <asp:label id="lblResults" Visible="false" runat="server"/>
          <p>
              &nbsp;</p>
          <p>
              &nbsp;</p>
          <p>
              &nbsp;</p>
          <p>
              &nbsp;</p>
          <p>
              <asp:Table ID="Table1" runat="server" Height="328px" Width="1024px" BorderWidth="4" CellSpacing="5" BorderStyle="Dotted" >
                <asp:TableHeaderRow runat="server" TableSection="TableHeader" HorizontalAlign="Center" BorderStyle="Solid">
                    <asp:TableHeaderCell ID="F1" runat="server"  Text="Field 1" VerticalAlign="Middle" Width="20%"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="F2" runat="server" Text="Field 2"></asp:TableHeaderCell>                    
                </asp:TableHeaderRow>
                <asp:TableRow runat="server" HorizontalAlign="Right" BorderStyle="Solid">
                    <asp:TableCell ID="C1" runat="server" Text="1" AssociatedHeaderCellID="F1"></asp:TableCell>
                    <asp:TableCell ID="C2" runat="server" Text="2" AssociatedHeaderCellID="F2" HorizontalAlign="Left"></asp:TableCell>
                </asp:TableRow>  
              </asp:Table>
              &nbsp;</p> 
          <p>
              &nbsp;</p>

     </form>

   </body>
</html>
