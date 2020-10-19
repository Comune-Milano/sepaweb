
Partial Class ANAUT_ListaCarico
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Response.Expires = 0
            Response.Write(HttpContext.Current.Session.Item("CARICO"))
            HttpContext.Current.Session.Remove("CARICO")
        Catch EX As Exception
            Response.Write("ERRORE: " + EX.Message)
        End Try

    End Sub
End Class
