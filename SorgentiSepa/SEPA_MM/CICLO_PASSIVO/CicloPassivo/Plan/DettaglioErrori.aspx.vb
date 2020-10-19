
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_DettaglioErrori
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Write("<p style='font-family: aria; font-size: 9pt'>" & Request.QueryString("D") & Session.Item("Dettagli") & "</p>")
        Session.Remove("Dettagli")
    End Sub
End Class
