
Partial Class AMMSEPA_Controllo_CancFile
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Button6_Click(sender As Object, e As System.EventArgs) Handles Button6.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If par.VerificaPW(TextBox1.Text) = True Then
                txtFile.Visible = True
                Label1.Visible = True
                Button7.Visible = True
            End If
        End If
    End Sub

    Protected Sub Button7_Click(sender As Object, e As System.EventArgs) Handles Button7.Click
        Try
            My.Computer.FileSystem.DeleteFile(Server.MapPath("../" & txtFile.Text))
            Response.Write("<script>alert('Il file è stato cancellato!');</script>")
        Catch ex As Exception
            Label1.Text = ex.Message
        End Try
    End Sub

    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function
End Class
