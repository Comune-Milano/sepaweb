
Partial Class Contratti_NuovoContrattoFM
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Write("<script>parent.main.location.replace('RisultatiUnitaAssFuoriMI.aspx?NOME=" & par.VaroleDaPassare(Me.txtNome.Text.ToUpper) & "&COGNOME=" & par.VaroleDaPassare(Me.txtCognome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(Me.txtCF.Text.ToUpper) & "');</script>")
    End Sub
End Class
