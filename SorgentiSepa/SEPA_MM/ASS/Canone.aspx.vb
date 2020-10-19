Partial Class ASS_Canone
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        LBLTESTO.Text = Replace(Session.Item("canone"), vbCrLf, "<br />")
    End Sub
End Class
