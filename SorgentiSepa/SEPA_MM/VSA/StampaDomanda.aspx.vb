
Partial Class VSA_StampaDomanda
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write(HttpContext.Current.Session.Item("DOMANDA"))
        HttpContext.Current.Session.Remove("DOMANDA")
    End Sub
End Class
