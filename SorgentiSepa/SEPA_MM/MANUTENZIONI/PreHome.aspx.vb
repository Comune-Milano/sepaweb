
Partial Class MANUTENZIONI_PreHome
    Inherits PageSetIdMode

    Protected Sub ImgManutenz_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgManutenz.Click
        Response.Redirect("menuInterventi.htm")
    End Sub

    Protected Sub ImgConsRilievi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgConsRilievi.Click
        Response.Redirect("menu.htm")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If

    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Write("<script>top.location.href=""../Chiusura.htm""</script>")
    End Sub
End Class
