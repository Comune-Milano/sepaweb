
Partial Class ConfermaRegUtente
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            'HyperLink1.NavigateUrl = "mailto:Alessandro.Gobbi@comune.milano.it?subject=Registrazione Utente&body=Data e Ora: " & Now & " Utente da Autorizzare: " & Request.QueryString("OP")
            HyperLink1.NavigateUrl = "AreaPrivata.aspx"
        End If
    End Sub
End Class
