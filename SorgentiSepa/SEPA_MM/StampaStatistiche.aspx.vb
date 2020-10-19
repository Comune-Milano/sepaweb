
Partial Class StampaStatistiche
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If	
        Response.Write(HttpContext.Current.Session.Item("STATISTICHE"))
        HttpContext.Current.Session.Remove("STATISTICHE")
    End Sub
End Class
