
Partial Class Contratti_DettagliFO
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Label2.Text = Session.Item("DETTAGLICANONE")
    End Sub
End Class
