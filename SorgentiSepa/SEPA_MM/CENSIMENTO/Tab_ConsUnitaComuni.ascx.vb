
Partial Class CENSIMENTO_Tab_ConsUnitaComuni
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub
End Class
