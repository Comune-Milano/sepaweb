
Partial Class ASS_RicercaDichFuoriMI
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            par.caricaComboBox("SELECT * FROM T_STATI_DICHIARAZIONE ORDER BY COD ASC", cmbStato, "cod", "descrizione", )
            par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI where cod in (select cod_comune from siscom_mi.indirizzi,siscom_mi.complessi_immobiliari where ID_INDIRIZZO_RIFERIMENTO=indirizzi.id) ORDER BY NOME ASC", cmbComuneAss, "ID", "NOME", True)
            abbin.Value = Request.QueryString("AB")
            cmbStato.Items.Add("TUTTI")
            cmbStato.Items.FindByText("TUTTI").Selected = True
        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If abbin.Value = "1" Then
            Response.Redirect("RisultRicercaDichFuoriMI2.aspx?C=" & txtCognome.Text & "&N=" & txtNome.Text & "&CF=" & txtCF.Text & "&PG=" & txtPG.Text & "&STATO=" & cmbStato.SelectedValue & "&COMAS=" & cmbComuneAss.SelectedValue & "&AB=" & abbin.Value, False)
        Else
            Response.Redirect("RisultRicercaDichFuoriMI.aspx?C=" & txtCognome.Text & "&N=" & txtNome.Text & "&CF=" & txtCF.Text & "&PG=" & txtPG.Text & "&STATO=" & cmbStato.SelectedValue & "&COMAS=" & cmbComuneAss.SelectedValue, False)
        End If
    End Sub
End Class
