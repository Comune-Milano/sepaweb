
Partial Class CONS_RicercaPrenotazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'btnCerca.Attributes.Add("OnClick", "javascript:Attendi();")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If txtPG.Text = "" And txtCF.Text = "" And txtNome.Text = "" Then
            Response.Write("<script>alert('Valorizzare almeno uno dei tre valori!');</script>")
            Exit Sub
        Else
            Response.Redirect("RisultatoRicPre.aspx?CF=" & par.VaroleDaPassare(txtCF.Text) & "&PG=" & par.VaroleDaPassare(txtPG.Text) & "&NOME=" & par.VaroleDaPassare(txtNome.Text))
        End If
    End Sub
End Class
