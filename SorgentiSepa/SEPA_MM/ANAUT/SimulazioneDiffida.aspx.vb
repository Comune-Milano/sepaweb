
Partial Class ANAUT_SimulazioneDiffida
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            HyperLink1.NavigateUrl = "../FileTemp/" & par.DeCripta(Request.QueryString("id")) & ".zip"
            HyperLink1.Text = "Clicca qui per visualizzare il file " & par.DeCripta(Request.QueryString("id")) & ".zip"
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
