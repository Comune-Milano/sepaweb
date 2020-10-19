
Partial Class SIRAPER_Gestione
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            Me.txtCodiceIstat.Attributes.Add("onkeyup", "javascript:valid(this,'codice');")
            par.caricaComboBox("SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_ENTE_SIRAPER", ddlTipoEnte, "COD", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_AMMINISTRAZIONE_ENTE ORDER BY DESCRIZIONE ASC", ddlTipoAmministrazione, "COD", "DESCRIZIONE", True)
            CaricaParametri()
        End If
    End Sub
    Private Sub CaricaParametri()
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'CODICE ISTAT'"
            txtCodiceIstat.Text = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'RAGIONE SOCIALE'"
            txtRagioneSociale.Text = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'CODICE FISCALE'"
            txtCodFiscale.Text = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'PARTITA IVA'"
            txtPiva.Text = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'SIGLA ENTE'"
            txtSiglaEnte.Text = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'TIPO ENTE'"
            ddlTipoEnte.SelectedValue = par.IfNull(par.cmd.ExecuteScalar, "-1")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'TIPO AMMINISTRAZIONE'"
            ddlTipoAmministrazione.SelectedValue = par.IfNull(par.cmd.ExecuteScalar, "-1")
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Gestione - CaricaParametri - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            If String.IsNullOrEmpty(Trim(txtSiglaEnte.Text)) Or ddlTipoEnte.SelectedValue.ToString = "-1" Or ddlTipoAmministrazione.SelectedValue.ToString = "-1" _
                Or String.IsNullOrEmpty(Trim(txtCodiceIstat.Text)) Or String.IsNullOrEmpty(Trim(txtRagioneSociale.Text)) _
                Or String.IsNullOrEmpty(Trim(txtCodFiscale.Text)) Or String.IsNullOrEmpty(Trim(txtPiva.Text)) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Definire tutti i parametri!');", True)
                Exit Sub
            End If
            connData.apri(True)
            par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER_GESTIONE SET VALORE = " & par.insDbValue(txtCodiceIstat.Text, True) & " WHERE DESCRIZIONE = 'CODICE ISTAT'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER_GESTIONE SET VALORE = " & par.insDbValue(txtRagioneSociale.Text, True) & " WHERE DESCRIZIONE = 'RAGIONE SOCIALE'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER_GESTIONE SET VALORE = " & par.insDbValue(txtCodFiscale.Text, True) & " WHERE DESCRIZIONE = 'CODICE FISCALE'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER_GESTIONE SET VALORE = " & par.insDbValue(txtPiva.Text, True) & " WHERE DESCRIZIONE = 'PARTITA IVA'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER_GESTIONE SET VALORE = " & par.insDbValue(txtSiglaEnte.Text, True) & " WHERE DESCRIZIONE = 'SIGLA ENTE'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER_GESTIONE SET VALORE = " & par.insDbValue(ddlTipoEnte.SelectedValue, False, False, True) & " WHERE DESCRIZIONE = 'TIPO ENTE'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER_GESTIONE SET VALORE = " & par.insDbValue(ddlTipoAmministrazione.SelectedValue, False, False, True) & " WHERE DESCRIZIONE = 'TIPO AMMINISTRAZIONE'"
            par.cmd.ExecuteNonQuery()
            connData.chiudi(True)
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Operazione effettuata correttamente!');", True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Gestione - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub
End Class
