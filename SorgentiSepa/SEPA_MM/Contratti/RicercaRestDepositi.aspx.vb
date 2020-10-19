
Partial Class Contratti_RicercaRestDepositi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            txtCodContr.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtDataEventoAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataEventoDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtNumCDP.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtAnnoCDP.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNumMandato.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtAnnoMandato.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtDataMANA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataEventoDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim DatiDaPassare As String = "CDC=" & txtCodContr.Text & "&DCPD=" & par.AggiustaData(txtDataEventoDAL.Text) & "&DCPA=" & par.AggiustaData(txtDataEventoAL.Text) & "&IDPD=" & txtImportoDa.Text & "&IDPA=" & txtImportoA.Text & "&NCDP=" & txtNumCDP.Text & "&ACDP=" & txtAnnoCDP.Text & "&NMAN=" & txtNumMandato.Text & "&AMAN=" & txtAnnoMandato.Text & "&DMAND=" & par.AggiustaData(txtDataMANDa.Text) & "&DMANA=" & par.AggiustaData(txtDataMANA.Text)

        Response.Write("<script>location.replace('RisultatiRestDep.aspx?" & DatiDaPassare & "');</script>")
    End Sub
End Class
