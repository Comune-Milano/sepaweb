
Partial Class Contratti_Registrazione
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lblStoricoReg.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "ApriStoricoDatiReg();" & Chr(34) & ">Clicca qui per visualizzare lo storico dei dati della registrazione</a>"

    End Sub

    Public Function DisabilitaTutto()
        cmbUfficioRegistro.Enabled = False
        txtSerie.ReadOnly = True
        txtNumRegistrazione.ReadOnly = True
        txtDataRegistrazione.ReadOnly = True
        txtNumRepertorio.ReadOnly = True
        txtDataRepertorio.ReadOnly = True
        txtNumAssegnPg.ReadOnly = True
        txtDataAssegnPG.ReadOnly = True
        cmbModVersamento.Enabled = False
        txtNotereg.Enabled = False
        cmbModoPagamento.Enabled = False
        cmbTipoPosizione.Enabled = False
    End Function

    Public Function Disabilita()

        cmbModVersamento.Enabled = False
    End Function


    Protected Sub btnAnnullaInvio_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnullaInvio.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If HConferma.Value = "1" Then
            CType(Me.Page, Object).AnnullaInvioAE()
        End If
    End Sub

    Protected Sub DataGridLiquidazione_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles DataGridLiquidazione.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('idAvviso').value='" & e.Item.Cells(0).Text & "';")
            e.Item.Attributes.Add("onDblclick", "ModificaAvviso();")
        End If
    End Sub

    Protected Sub ImgDeleteAvviso_Click(sender As Object, e As ImageClickEventArgs) Handles ImgDeleteAvviso.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If HConferma.Value = "1" Then
            CType(Me.Page, Object).EliminaAvviso()
        End If
    End Sub
End Class
