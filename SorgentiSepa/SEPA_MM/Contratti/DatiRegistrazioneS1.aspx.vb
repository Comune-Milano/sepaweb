
Partial Class Contratti_DatiRegistrazioneS1
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub cmbTipoContr_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoContr.SelectedIndexChanged
       
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            txtCodContr.Attributes.Add("onfocus", "javascript:RendiVisibile();")
            txtCodContr.Attributes.Add("onfocusout", "javascript:RendiNonVisibile();")
           

            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ORDER BY DESCRIZIONE ASC", cmbTipoContr, "COD", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ORDER BY DESCRIZIONE ASC", cmbTipoUI, "COD", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPOLOGIA_POSIZIONE ORDER BY DESCRIZIONE ASC", cmbTipoPosizione, "ID", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPOLOGIA_PAGAMENTO ORDER BY DESCRIZIONE ASC", cmbModoPagamento, "ID", "DESCRIZIONE", True)
        End If
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If errore.Value = "0" Then
            If ChTutti.Checked = False Then
                Session.Add("NoteReg", txtNote.Text)
            Else
                Session.Add("NoteReg", "XX")
            End If
            Response.Write("<script>location.href='RisultatoDatiAE.aspx?" _
                              & "TIPOUI=" & par.VaroleDaPassare(Me.cmbTipoUI.SelectedValue) _
                              & "&TIPOCONTR=" & par.VaroleDaPassare(Me.cmbTipoContr.SelectedValue) _
                              & "&CODCONTR=" & par.VaroleDaPassare(par.IfEmpty(txtCodContr.Text, "")) _
                              & "&MP=" & cmbModoPagamento.SelectedValue _
                              & "&TP=" & cmbTipoPosizione.SelectedValue _
                              & "';</script>")
        End If
    End Sub
End Class
