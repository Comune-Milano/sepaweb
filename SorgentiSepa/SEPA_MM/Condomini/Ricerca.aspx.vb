
Partial Class Contratti_Anagrafica_Ricerca
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        'Response.Redirect("RisultatiRicerca.aspx?NOME=" & par.VaroleDaPassare(Me.txtNome.Text.ToUpper) & "&COGNOME=" & par.VaroleDaPassare(Me.txtCognome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(Me.txtCF.Text.ToUpper) & "&RAGSOCIALE=" & par.VaroleDaPassare(Me.txtRagione.Text.ToUpper) & "&PIVA=" & par.VaroleDaPassare(Me.txtPiva.Text.ToUpper))
        Response.Redirect("RisultatiRicerca.aspx?NOME=" & par.VaroleDaPassare(Me.txtNome.Text.ToUpper) & "&COGNOME=" & par.VaroleDaPassare(Me.txtCognome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(Me.txtCF.Text.ToUpper) & "&PIVA=" & par.VaroleDaPassare(Me.TxtPIva.Text.ToUpper))

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../Portale.aspx""</script>")
            Exit Sub
        End If

        txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        'txtRagione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        'txtPiva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

    End Sub
End Class
