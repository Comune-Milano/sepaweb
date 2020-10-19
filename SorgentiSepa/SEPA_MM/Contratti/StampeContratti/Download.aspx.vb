
Partial Class Contratti_StampeContratti_Download
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim NomeFile As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                NomeFile = par.DeCripta(Request.QueryString("V"))
                'Response.Clear()
                'Response.ContentType = "application/octet-stream"

                'Response.AddHeader("Content-Disposition", "attachment;filename=" & NomeFile)
                'Response.BufferOutput = True
                'Response.BinaryWrite(FileIO.FileSystem.ReadAllBytes(Server.MapPath("") & "\" & NomeFile))
                'Response.End()

                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=""" & NomeFile & """")
                Response.AddHeader("Content-Length", FileIO.FileSystem.ReadAllBytes(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeContratti") & "\" & NomeFile).Length)
                Response.ContentType = "application/octet-stream"

                ' leggo dal file e scrivo nello stream di risposta 
                Response.WriteFile(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeContratti") & "\" & NomeFile)
                Response.End()


            Catch ex As Exception
                Response.Write(ex.GetType.ToString & ": " & ex.Message)
            End Try
        End If
    End Sub
End Class
