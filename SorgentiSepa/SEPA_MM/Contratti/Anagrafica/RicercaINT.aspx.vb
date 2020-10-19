
Partial Class Contratti_Anagrafica_Ricerca
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtRagione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        txtPiva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

    End Sub
End Class
