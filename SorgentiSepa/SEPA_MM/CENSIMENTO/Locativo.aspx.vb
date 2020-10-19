
Partial Class CENSIMENTO_Locativo
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Label1.Text = Replace(par.SValoreLocativo(Request.QueryString("ID")), vbCrLf, "</br>")
    End Sub
End Class
