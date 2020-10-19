
Partial Class CONS_Ricevuta
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lblData.Text = Format(Now, "dd/MM/yyyy")
        lblGiorno.Text = Request.QueryString("GIORNO")
        lblNome.Text = "Nominativo: " & Request.QueryString("NOMINATIVO")
        Label1.Text = Request.QueryString("DOMANDA")
    End Sub
End Class
