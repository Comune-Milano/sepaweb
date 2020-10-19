
Partial Class ANAUT_DistintaScarico
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        Response.Write(Session.Item(Session.Item("ID_OPERATORE")))
        Session.Remove(Session.Item("ID_OPERATORE"))
        Response.Expires = 0
    End Sub
End Class
