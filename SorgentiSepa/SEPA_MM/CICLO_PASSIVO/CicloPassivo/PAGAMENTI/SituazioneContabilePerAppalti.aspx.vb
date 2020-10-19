
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_SituazioneContabilePerAppalti
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            'dataInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'dataFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            connData.apri()
            par.caricaComboTelerik("SELECT ID,(CASE WHEN RAGIONE_SOCIALE IS NOT NULL THEN RAGIONE_SOCIALE ELSE COGNOME||' '||NOME END) AS NOME FROM SISCOM_MI.FORNITORI WHERE FL_BLOCCATO=0 ORDER BY RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC", cmbFornitore, "ID", "NOME", True)
            par.caricaComboTelerik("SELECT TAB_FILIALI.NOME, TAB_FILIALI.ID, (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME) AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD ORDER BY NOME ASC", cmbSedeTerritoriale, "ID", "NOME", True)
            par.caricaComboTelerik("SELECT ID, COGNOME || ' ' || NOME AS OPERATORE FROM OPERATORI WHERE ID IN (SELECT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL) ORDER BY OPERATORE ASC", cmbDirLavori, "ID", "OPERATORE", True)
            par.caricaComboTelerik("SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYMMDD'),'DD/MM/YYYY') ||' - '|| " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYMMDD'),'DD/MM/YYYY')||' ('||PF_STATI.DESCRIZIONE||')' AS DESCRIZIONE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                & "WHERE ID_STATO > = 5 AND SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND ID_STATO = PF_STATI.ID ORDER BY T_ESERCIZIO_FINANZIARIO.ID DESC", cmbEsercizio, "ID", "DESCRIZIONE", True)
            connData.chiudi()
        End If
    End Sub
    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim dataI As String = ""
        If Not IsNothing(dataInizio.SelectedDate) Then
            dataI = dataInizio.SelectedDate
        End If
        Dim dataF As String = ""
        If Not IsNothing(dataFine.SelectedDate) Then
            dataF = dataFine.SelectedDate
        End If
        Dim parametri As String = "?IDF=" & cmbFornitore.SelectedValue.ToString
        parametri &= "&IDEF=" & cmbEsercizio.SelectedValue.ToString
        parametri &= "&IDST=" & cmbSedeTerritoriale.SelectedValue.ToString
        parametri &= "&IDT=" & cmbTipologiaContratto.SelectedValue.ToString
        parametri &= "&IDDL=" & cmbDirLavori.SelectedValue
        parametri &= "&CIG=" & txtCIG.Text
        parametri &= "&DI=" & dataI
        parametri &= "&DF=" & dataF
        'Dim script As String = "window.open('RisultatiSituazioneContabilePerAppalti.aspx" & parametri.Replace("'", "\'") & "','SituazioneContabileApp" & Format(Now, "yyyyMMddHHmmss") & "','');"
        'ScriptManager.RegisterStartupScript(Me, Me.GetType, "", script, True)
        Response.Redirect("RisultatiSituazioneContabilePerAppalti.aspx" & parametri.Replace("'", "\'"))
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Redirect("../../pagina_home_ncp.aspx")
    End Sub
End Class
