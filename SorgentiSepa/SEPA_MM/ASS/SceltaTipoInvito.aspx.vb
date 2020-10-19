
Partial Class ASS_SceltaTipoInvito
    Inherits PageSetIdMode
    Dim par As New CM.Global()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        cmbStato.Items.Add("ERP-IDONEE")
        cmbStato.Items.Add("ERP-Art14")
        cmbStato.Items.Add("ERP-Art15 lett.A")
        cmbStato.Items.Add("BANDO CAMBI")
        cmbStato.Items.Add("CAMBI EMERGENZA")
        If Not IsPostBack Then
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Redirect("Inviti.aspx?C=" & par.VaroleDaPassare(UCase(txtCognome.Text)) & "&N=" & par.VaroleDaPassare(UCase(txtNome.Text)) & "&PG=" & par.VaroleDaPassare(txtPG.Text) & "&TIPO=" & Replace(cmbStato.SelectedItem.Text, "ERP-", ""))
    End Sub
End Class
