
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_EstrazioneCDP
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
            caricaElencoEsercizi()
            caricaElencoFornitori()
            caricaAppalti()
            caricaVoceDGR()
            'TextBoxDataCDPda.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxDataCDPa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub
    Private Sub caricaElencoEsercizi()
        Try
            connData.apri()
            Dim query As String = "SELECT PF_MAIN.ID ID,GETDATA(INIZIO)||'-'||GETDATA(FINE)|| ' (' || DESCRIZIONE || ')' AS PERIODO " _
                & " FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI " _
                & " WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                & " AND PF_STATI.ID=PF_MAIN.ID_STATO " _
                & " AND ID_STATO>=5 ORDER BY PF_MAIN.ID DESC"
            par.caricaComboTelerik(query, DropDownListEsercizioContabile, "ID", "PERIODO", False)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - caricaElencoEsercizi - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaElencoFornitori()
        Try
            connData.apri()
            Dim condizionePF As String = ""
            If DropDownListEsercizioContabile.SelectedValue <> "-1" Then
                condizionePF = " AND ID IN (SELECT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID_PIANO_FINANZIARIO=" & DropDownListEsercizioContabile.SelectedValue & "))"
            End If
            Dim QUERY As String = "SELECT ID,RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE EXISTS (SELECT ID_FORNITORE FROM SISCOM_MI.PAGAMENTI WHERE PAGAMENTI.ID_FORNITORE=FORNITORI.ID " & condizionePF & ") ORDER BY 2"
            par.caricaComboTelerik(QUERY, DropDownListFornitori, "ID", "RAGIONE_SOCIALE", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - caricaElencoFornitori - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaAppalti()
        Try
            connData.apri()
            Dim condizionePF As String = ""
            If DropDownListEsercizioContabile.SelectedValue <> "-1" Then
                condizionePF = "and APPALTI.id_lotto in (select id from siscom_mi.lotti where LOTTI.ID_ESERCIZIO_FINANZIARIO in (select id_esercizio_finanziario from siscom_mi.pf_main where pf_main.id=" & DropDownListEsercizioContabile.SelectedValue & "))"
            End If
            Dim condizioneFornitore As String = ""
            If DropDownListFornitori.SelectedValue <> "-1" Then
                condizioneFornitore = " AND APPALTi.ID_FORNITORE=" & DropDownListFornitori.SelectedValue
            End If
            Dim QUERY As String = "select distinct id_appalto,num_Repertorio from siscom_mi.pagamenti,siscom_mi.appalti where id_appalto is not null and appalti.id=pagamenti.id_appalto " & condizionePF & condizioneFornitore & " ORDER BY 2"
            par.caricaComboTelerik(QUERY, DropDownListRepertorio, "id_appalto", "num_Repertorio", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - caricaAppalti - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaVoceDGR()
        Try
            connData.apri()
            Dim condizionePF As String = ""
            If DropDownListEsercizioContabile.SelectedValue <> "-1" Then
                condizionePF = "and APPALTI.id_lotto in (select id from siscom_mi.lotti where LOTTI.ID_ESERCIZIO_FINANZIARIO in (select id_esercizio_finanziario from siscom_mi.pf_main where pf_main.id=" & DropDownListEsercizioContabile.SelectedValue & "))"
            End If
            Dim condizioneFornitore As String = ""
            If DropDownListFornitori.SelectedValue <> "-1" Then
                condizioneFornitore = " AND APPALTI.ID_FORNITORE=" & DropDownListFornitori.SelectedValue
            End If
            Dim condizioneAppalti As String = ""
            If DropDownListRepertorio.SelectedValue <> "-1" Then
                condizioneAppalti = " AND APPALTi.ID=" & DropDownListRepertorio.SelectedValue
            End If
            Dim QUERY As String = "SELECT DISTINCT PF_VOCI_IMPORTO.ID,PF_VOCI_IMPORTO.DESCRIZIONE FROM SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI WHERE PAGAMENTI.ID = PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI_IMPORTO.ID=PRENOTAZIONI.ID_VOCE_PF_IMPORTO AND APPALTI.ID=PRENOTAZIONI.ID_APPALTO " & condizioneFornitore & condizioneAppalti & condizionePF & " ORDER BY 2"
            par.caricaComboTelerik(QUERY, DropDownListVoceDGR, "id", "descrizione", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - caricaVoceDGR - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim DataCDPda As String = ""
        If Not IsNothing(TextBoxDataCDPda.SelectedDate) Then
            DataCDPda = TextBoxDataCDPda.SelectedDate
        End If
        Dim DataCDPa As String = ""
        If Not IsNothing(TextBoxDataCDPa.SelectedDate) Then
            DataCDPa = TextBoxDataCDPa.SelectedDate
        End If
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "apri", "window.open('RisultatiEstrazioneCDP.aspx?" _
                       & "FOR=" & DropDownListFornitori.SelectedValue _
                       & "&DGR=" & DropDownListVoceDGR.SelectedValue _
                       & "&PF=" & DropDownListEsercizioContabile.SelectedValue _
                       & "&REP=" & DropDownListRepertorio.SelectedValue _
                       & "&DA=" & DataCDPda _
                       & "&A=" & DataCDPa _
                       & "','EstrazionePagamenti" & Format(Now, "yyyyMMddHHmmss") & "','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no');", True)
    End Sub
    Protected Sub DropDownListEsercizioContabile_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListEsercizioContabile.SelectedIndexChanged
        caricaElencoFornitori()
        caricaAppalti()
        caricaVoceDGR()
    End Sub

    Protected Sub DropDownListFornitori_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListFornitori.SelectedIndexChanged
        caricaAppalti()
        caricaVoceDGR()
    End Sub
    Protected Sub DropDownListRepertorio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListRepertorio.SelectedIndexChanged
        caricaVoceDGR()
    End Sub

     Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Redirect("../../pagina_home_ncp.aspx")
    End Sub

  
End Class
