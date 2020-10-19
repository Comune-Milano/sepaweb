
Partial Class ASS_ExportElencoComune
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim FileName As String = ""

        FileName = "Disponibilita_" & Format(Now, "yyyyMMddHHmmss") & ".xls"

        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("content-disposition", "inline; filename=" & FileName)
        Response.Write(par.DeCripta(Session.Item("FILEXLS")))
        Response.Flush()
        Response.End()
        Session.Remove("FILEXLS")
    End Sub
End Class
