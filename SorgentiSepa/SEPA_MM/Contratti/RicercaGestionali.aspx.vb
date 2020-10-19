
Partial Class Contratti_RicercaGestionali
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            txtDataEmissDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataEmissAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataRiferDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataRiferAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtCodContr.Attributes.Add("onfocus", "javascript:RendiVisibile();")
            txtCodContr.Attributes.Add("onfocusout", "javascript:RendiNonVisibile();")
            txtNote.Attributes.Add("onfocus", "javascript:RendiVisibile();")
            txtNote.Attributes.Add("onfocusout", "javascript:RendiNonVisibile();")

            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ORDER BY DESCRIZIONE ASC", cmbTipoContr, "COD", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ORDER BY DESCRIZIONE ASC", cmbTipoUI, "COD", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPO_BOLLETTE_GEST WHERE nvl(fl_ripartibile,0)=1 AND FL_VISUALIZZABILE=1 ORDER BY DESCRIZIONE ASC", cmbTipoDoc, "ID", "DESCRIZIONE", True)
        End If
    End Sub

    Protected Sub cmbTipoContr_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoContr.SelectedIndexChanged
        If cmbTipoContr.SelectedValue = "ERP" Then
            cmbProvenASS.Items.Clear()
            cmbProvenASS.Items.Add(New ListItem("- - -", "0"))
            cmbProvenASS.Items.Add(New ListItem("Canone Convenzionato", "12"))
            cmbProvenASS.Items.Add(New ListItem("Forze dell'Ordine", "10"))
            cmbProvenASS.Items.Add(New ListItem("ERP Moderato", "2"))
            cmbProvenASS.Items.Add(New ListItem("ERP Sociale", "1"))
            cmbProvenASS.Visible = "True"
            lblSpecifico.Visible = "True"

        ElseIf cmbTipoContr.SelectedValue = "L43198" Then
            cmbProvenASS.Items.Clear()
            cmbProvenASS.Items.Add(New ListItem("- - -", "-1"))
            cmbProvenASS.Items.Add(New ListItem("Standard", "0"))
            cmbProvenASS.Items.Add(New ListItem("Cooperative", "C"))
            cmbProvenASS.Items.Add(New ListItem("431 P.O.R.", "P"))
            cmbProvenASS.Items.Add(New ListItem("431/98 Art.15 R.R.1/2004", "D"))
            cmbProvenASS.Items.Add(New ListItem("431/98 Speciali", "S"))
            cmbProvenASS.Visible = "True"
            lblSpecifico.Visible = "True"
        End If

        If cmbTipoContr.SelectedValue <> "ERP" And cmbTipoContr.SelectedValue <> "L43198" Then
            cmbProvenASS.Visible = "False"
            lblSpecifico.Visible = "False"
        End If
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If errore.Value = "0" Then
            Response.Write("<script>window.open('RisultatoGestionali.aspx?TDOC=" & par.VaroleDaPassare(Me.cmbTipoDoc.SelectedValue) _
                              & "&EMISS1=" & par.VaroleDaPassare(Me.txtDataEmissDAL.Text) _
                              & "&EMISS2=" & par.VaroleDaPassare(Me.txtDataEmissAL.Text) _
                              & "&RIFER1=" & par.VaroleDaPassare(Me.txtDataRiferDAL.Text) _
                              & "&RIFER2=" & par.VaroleDaPassare(Me.txtDataRiferAL.Text) _
                              & "&TIPOUI=" & par.VaroleDaPassare(Me.cmbTipoUI.SelectedValue) _
                              & "&TIPOCONTR=" & par.VaroleDaPassare(Me.cmbTipoContr.SelectedValue) _
                              & "&TSPEC=" & par.VaroleDaPassare(Me.cmbProvenASS.SelectedValue) _
                              & "&ELAB=" & par.VaroleDaPassare(Valore01(Me.chkElaborare.Checked)) _
                              & "&ORD=" & par.VaroleDaPassare(Me.rdbOrderBY.SelectedValue) _
                              & "&CODCONTR=" & par.VaroleDaPassare(par.IfEmpty(txtCodContr.Text, "")) _
                              & "&NOTE=" & par.VaroleDaPassare(par.IfEmpty(txtNote.Text, "")) _
                              & "&CRED=" & par.VaroleDaPassare(Me.cmbCredDeb.SelectedValue) _
                              & "')</script>")
        End If
    End Sub

End Class
