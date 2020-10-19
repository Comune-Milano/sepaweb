
Partial Class Contratti_DownloadREL
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim NomeFile As String = ""

    Private Sub Contratti_DownloadREL_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                NomeFile = Request.QueryString("V")
                'Response.Clear()
                'Response.AddHeader("Content-Disposition", "attachment; filename=""" & NomeFile & """")
                'Response.AddHeader("Content-Length", FileIO.FileSystem.ReadAllBytes(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE") & "\" & NomeFile).Length)
                'Response.ContentType = "text/plain"


                '' leggo dal file e scrivo nello stream di risposta 
                'Response.WriteFile(Server.MapPath("..\FileTemp") & "\" & NomeFile)
                'Response.End()


                HttpContext.Current.Response.ContentType = "application/force-download"
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" & NomeFile)
                HttpContext.Current.Response.BufferOutput = True
                HttpContext.Current.Response.BinaryWrite(FileIO.FileSystem.ReadAllBytes(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE") & "\" & NomeFile))


            Catch ex As Exception
                Response.Write(ex.GetType.ToString & ": " & ex.Message)
            End Try
        End If
    End Sub
End Class
