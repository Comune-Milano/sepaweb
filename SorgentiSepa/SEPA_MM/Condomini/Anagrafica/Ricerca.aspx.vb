
Partial Class Condomini_Anagrafica_Ricerca
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Redirect("RisultatiAmminist.aspx?NOME=" & par.VaroleDaPassare(Me.txtNome.Text.ToUpper) & "&COGNOME=" & par.VaroleDaPassare(Me.txtCognome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(Me.txtCodFiscale.Text.ToUpper) & "&PIVA=" & par.VaroleDaPassare(Me.txtPIVA.Text.ToUpper))

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../Portale.aspx""</script>")
            Exit Sub
        End If

        txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtCodFiscale.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

    End Sub
End Class
