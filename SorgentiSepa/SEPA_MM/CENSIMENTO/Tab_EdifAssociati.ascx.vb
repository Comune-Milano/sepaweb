
Partial Class CENSIMENTO_Tab_EdifAssociati
    Inherits UserControlSetIdMode
    Protected WithEvents cls1 As New Page
    Protected Sub btnSelezionaTutto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelezionaTutto.Click






    End Sub



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub
End Class
