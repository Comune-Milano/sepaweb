
Partial Class AMMSEPA_Controllo_Cancella
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            My.Computer.FileSystem.DeleteFile(Server.MapPath("../CONTRATTI/VARIE/") & par.DeCripta(Request.QueryString("NOME")))
            Response.Write("<script>opener.form1.submit();</script>")
            Response.Write("Il File " & RicavaFile(Request.QueryString("NOME")) & " è stato cancellato correttamente<br /><br /><a href='javascript:window.close();'>CHIUDI FINESTRA</a>")

        Catch ex As Exception
            Response.Write(ex.Message)
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
