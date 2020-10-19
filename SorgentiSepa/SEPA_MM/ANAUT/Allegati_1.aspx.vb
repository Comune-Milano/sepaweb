
Partial Class ANAUT_Allegati_1
    Inherits PageSetIdMode
    Dim NomeFile As String
    Dim TipoFile As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                NomeFile = UCase(Request.QueryString("NOME"))
                TipoFile = UCase(Request.QueryString("EXT"))
                Response.ContentType = "application/force-download"
                Response.AddHeader("Content-Disposition", "attachment;filename=" & NomeFile)
                Response.BufferOutput = True
                Response.BinaryWrite(FileIO.FileSystem.ReadAllBytes(Server.MapPath("EXPORT/") & NomeFile))
                Response.End()

            Catch ex As Exception
                Response.Write(ex.GetType.ToString & ": " & ex.Message)
            End Try
        End If
    End Sub
End Class
