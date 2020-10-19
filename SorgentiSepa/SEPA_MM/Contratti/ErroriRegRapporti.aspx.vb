
Partial Class Contratti_ErroriRegRapporti
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Label1.Text = Session.Item("REPORTERRORIREG")
            Session.Remove("REPORTERRORIREG")
        End If
        '
    End Sub
End Class
