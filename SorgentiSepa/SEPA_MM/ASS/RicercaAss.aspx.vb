
Partial Class ASS_RicercaOfferta
    Inherits PageSetIdMode
    Dim par As New CM.Global()




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtOfferta.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCoRs.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        End If
    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Redirect("RisultatoRicAss.aspx?CR=" & par.Cripta(UCase(txtCoRs.Text)) & "&OF=" & par.VaroleDaPassare(txtOfferta.Text))
    End Sub
End Class
