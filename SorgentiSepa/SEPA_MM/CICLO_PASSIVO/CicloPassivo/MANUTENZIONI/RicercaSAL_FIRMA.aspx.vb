Imports Telerik.Web.UI

'*** RICERCA I SAL (da cambiare stato della FIRMA, Annullare il sal o Ri-Stampare)

Partial Class MANUTENZIONI_RicercaSAL_FIRMA
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)



        If Not IsPostBack Then

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            caricaStatoFirma()
            CaricaStrutture()
            FiltraCodiceProgetto()
            FiltraNumeroSalVision()
            If connAperta = True Then
                connData.chiudi(False)
            End If


        End If
    End Sub

    Private Sub caricaStatoFirma()
        Me.cmbStato.Items.Clear()
        Me.cmbStato.Items.Add(New RadComboBoxItem("", -1))
        Me.cmbStato.Items.Add(New RadComboBoxItem("NON FIRMATO", 0))
        Me.cmbStato.Items.Add(New RadComboBoxItem("FIRMATO CON RISERVA", 1))
        Me.cmbStato.Items.Add(New RadComboBoxItem("FIRMATO", 2))
        Me.cmbStato.SelectedValue = -1
    End Sub
    Private Sub CaricaStrutture()

        Try

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True

            End If
            Dim query As String = "SELECT  DISTINCT ID," _
                & " NOME FROM SISCOM_MI.TAB_FILIALI " _
                & " WHERE ID IN (SELECT ID_STRUTTURA FROM SISCOM_MI.PRENOTAZIONI " _
                & " WHERE TIPO_PAGAMENTO=3 " _
                & " AND ID_STATO>=2 " _
                & " AND ID_PAGAMENTO IS NOT NULL ) " _
                & " ORDER BY NOME ASC"
            par.caricaComboTelerik(query, cmbStruttura, "ID", "NOME", True, -1, "")
            If cmbStruttura.Items.Count = 2 Then
                cmbStruttura.SelectedValue = cmbStruttura.Items(1).Value
                cmbStruttura.Enabled = False
                CaricaEsercizio()
                FiltraCodiceProgetto()
                FiltraNumeroSalVision()
            Else
                cmbStruttura.SelectedValue = "-1"
                cmbStruttura.Enabled = True
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If


        Catch ex As Exception
            connData.chiudi()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraFornitori()

        Try

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim condizioneAppalto As String = ""
            Dim condizioneCodiceProgettoVision As String = ""
            If cmbAppalto.SelectedValue <> "-1" And cmbAppalto.SelectedValue <> "" Then
                condizioneAppalto = " AND ID_APPALTO=" & cmbAppalto.SelectedValue
            End If

            If DropDownListProgettoVision.SelectedValue <> "-1" And DropDownListProgettoVision.SelectedValue <> "" Then
                condizioneCodiceProgettoVision = "AND FORNITORI.ID IN (SELECT ID_FORNITORE " _
                    & " FROM SISCOM_MI.APPALTI WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI " _
                    & " WHERE ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                    & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                    & " AND STATO=4 " _
                    & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                    & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=2 " _
                    & " AND ID_PAGAMENTO IS NOT NULL) " _
                    & " AND PROGR||'/'||ANNO IN (SELECT CODICE_ODL FROM SISCOM_MI.IMPORT_STR " _
                    & " WHERE CODICE_PROGETTO_VISION='" & DropDownListProgettoVision.SelectedValue.Replace("'", "''") & "')))"
            End If

            Dim query As String = " SELECT ID," _
                & " (CASE WHEN COD_FORNITORE||' - '||TRIM(RAGIONE_SOCIALE) IS NOT NULL THEN RAGIONE_SOCIALE ELSE COD_FORNITORE||' - '||TRIM(COGNOME)||' '||TRIM(NOME) END) AS DESCRIZIONE " _
                & " ,RAGIONE_SOCIALE,COGNOME,NOME " _
                & " FROM SISCOM_MI.FORNITORI " _
                & " WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI " _
                & " WHERE ID IN " _
                & " (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                & " AND STATO=4 " _
                & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=2 " _
                & " AND ID_PAGAMENTO IS NOT NULL) " _
                & condizioneAppalto _
                & ")) " _
                & condizioneCodiceProgettoVision _
                & " ORDER BY RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
            par.caricaComboTelerik(query, cmbFornitore, "ID", "DESCRIZIONE", True, "-1", "")

            If cmbFornitore.Items.Count = 2 Then
                cmbFornitore.SelectedValue = cmbFornitore.Items(1).Value
            Else
                cmbFornitore.SelectedValue = "-1"
            End If

            If connAperta = True Then
                connData.chiudi(False)
            End If

        Catch ex As Exception
            connData.chiudi()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraAppalti(Optional ByVal idAppaltoSelezionato As Integer = 0)

        Try

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If


            Dim condizioneFornitore As String = ""
            Dim condizioneCodiceProgettoVision As String = ""
            If cmbFornitore.SelectedValue <> "-1" And cmbFornitore.SelectedValue <> "" Then
                condizioneFornitore = " AND ID_FORNITORE=" & cmbFornitore.SelectedValue
            End If

            If DropDownListProgettoVision.SelectedValue <> "-1" And DropDownListProgettoVision.SelectedValue <> "" Then
                condizioneCodiceProgettoVision = "AND APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI " _
                    & " WHERE ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                    & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                    & " AND STATO=4 " _
                    & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                    & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=2 " _
                    & " AND ID_PAGAMENTO IS NOT NULL) " _
                    & " AND PROGR||'/'||ANNO IN (SELECT CODICE_ODL FROM " _
                    & " SISCOM_MI.IMPORT_STR WHERE CODICE_PROGETTO_VISION='" & DropDownListProgettoVision.SelectedValue.Replace("'", "''") & "'))"
            End If

            Dim query As String = " SELECT  SISCOM_MI.APPALTI.ID," _
                & " TRIM(SISCOM_MI.APPALTI.NUM_REPERTORIO) || ' - ' || TRIM(SISCOM_MI.APPALTI.DESCRIZIONE) AS DESCRIZIONE " _
                & ",NUM_REPERTORIO " _
                & " FROM SISCOM_MI.APPALTI " _
                & " WHERE ID IN (SELECT DISTINCT(ID_APPALTO) FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                & " AND STATO=4 " _
                & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=2 " _
                & " AND ID_PAGAMENTO IS NOT NULL) " _
                & condizioneFornitore _
                & ") " _
                & condizioneCodiceProgettoVision _
                & "ORDER BY NUM_REPERTORIO"
            par.caricaComboTelerik(query, cmbAppalto, "ID", "DESCRIZIONE", True, "-1", "")
            If cmbAppalto.Items.Count = 2 Then
                cmbAppalto.SelectedValue = cmbAppalto.Items(1).Value
            Else
                cmbAppalto.SelectedValue = "-1"
            End If
            If idAppaltoSelezionato > 0 Then
                cmbAppalto.SelectedValue = idAppaltoSelezionato
            End If

            If connAperta = True Then
                connData.chiudi(False)
            End If

        Catch ex As Exception
            connData.chiudi()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraServizioVoce()

        Try

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If

            Dim condizioneAppalto As String = ""
            If cmbAppalto.SelectedValue <> "-1" Then
                condizioneAppalto = " AND ID_APPALTO=" & cmbAppalto.SelectedValue
            End If
            Dim condizioneCodiceProgettoVision As String = ""
            If DropDownListProgettoVision.SelectedValue <> "-1" And DropDownListProgettoVision.SelectedValue <> "" Then
                condizioneCodiceProgettoVision = "AND ID_APPALTO IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI " _
                    & " WHERE ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                    & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                    & " AND STATO=4 " _
                    & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                    & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=2 " _
                    & " AND ID_PAGAMENTO IS NOT NULL) " _
                    & " AND PROGR||'/'||ANNO IN (SELECT CODICE_ODL FROM " _
                    & " SISCOM_MI.IMPORT_STR WHERE CODICE_PROGETTO_VISION='" & DropDownListProgettoVision.SelectedValue.Replace("'", "''") & "'))"
            End If

            Dim query As String = "SELECT ID," _
                & " TRIM(DESCRIZIONE) AS DESCRIZIONE " _
                & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                & " WHERE ID IN (SELECT DISTINCT(ID_PF_VOCE_IMPORTO) FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                & " AND STATO=4 " _
                & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=2 " _
                & " AND ID_PAGAMENTO IS NOT NULL) " _
                & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") _
                & condizioneAppalto _
                & condizioneCodiceProgettoVision _
                & ") " _
                & " ORDER BY DESCRIZIONE ASC"
            par.caricaComboTelerik(query, cmbServizioVoce, "ID", "DESCRIZIONE", True, "-1", "")
            If cmbServizioVoce.Items.Count = 2 Then
                cmbServizioVoce.SelectedValue = cmbServizioVoce.Items(1).Value
            Else
                cmbServizioVoce.SelectedValue = "-1"
            End If

            If connAperta = True Then
                connData.chiudi(False)
            End If




        Catch ex As Exception
            connData.chiudi()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If
        cmbFornitore.SelectedValue = "-1"
        cmbAppalto.SelectedValue = "-1"
        cmbServizioVoce.SelectedValue = "-1"
        DropDownListProgettoVision.SelectedValue = "-1"
        DropDownListNumeroSALVision.SelectedValue = "-1"
        FiltraFornitori()
        FiltraAppalti()
        FiltraServizioVoce()
        FiltraCodiceProgetto()
        FiltraNumeroSalVision()
        If connAperta = True Then
            connData.chiudi(False)
        End If
    End Sub

Protected Sub cmbFornitore_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbFornitore.SelectedIndexChanged
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If
        FiltraAppalti()
        FiltraServizioVoce()
        If connAperta = True Then
            connData.chiudi(False)
        End If
    End Sub
Protected Sub cmbAppalto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAppalto.SelectedIndexChanged
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If
        FiltraFornitori()
        FiltraAppalti(cmbAppalto.SelectedValue)
        FiltraServizioVoce()
        If connAperta = True Then
            connData.chiudi(False)
        End If
    End Sub
 
		Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click

        Try

            Response.Write("<script>location.replace('RisultatiSAL_FIRMA.aspx?FO=" & Me.cmbFornitore.SelectedValue.ToString _
                                                                          & "&SV=" & Me.cmbServizioVoce.SelectedValue.ToString _
                                                                          & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                          & "&STR=" & Me.cmbStruttura.SelectedValue.ToString _
                                                                          & "&ST=" & Me.cmbStato.SelectedValue.ToString _
                                                                          & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                          & "&ADP=" & Me.txtADP.Text _
                                                                          & "&ANNO=" & Me.txtANNO.Text _
                & "&CPV=" & DropDownListProgettoVision.SelectedValue _
                & "&NSV=" & DropDownListNumeroSALVision.SelectedValue _
                                                                         & "');</script>")


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub cmbStruttura_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If

        CaricaEsercizio()

        If connAperta = True Then
            connData.chiudi(False)
        End If

    End Sub

    Private Sub CaricaEsercizio()

        Try

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If

            Dim query As String = "SELECT SISCOM_MI.PF_MAIN.ID, " _
                & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYMMDD'),'DD/MM/YYYY')||' - '||" _
                & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYMMDD'),'DD/MM/YYYY')||' ('|| " _
                & " SISCOM_MI.PF_STATI.DESCRIZIONE||')' AS DESCRIZIONE " _
                & " FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                & " WHERE SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                & " AND SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                & " AND SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                & " AND  SISCOM_MI.PF_MAIN.ID IN " _
                & " (SELECT DISTINCT(ID_PIANO_FINANZIARIO) " _
                & " FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
                & " AND STATO=4 " _
                & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=2 " _
                & " AND ID_PAGAMENTO IS NOT NULL) " _
                & " AND ID_PAGAMENTO IS NOT NULL " _
                & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") _
                & ") ORDER BY 1 DESC "
            par.caricaComboTelerik(query, cmbEsercizio, "ID", "DESCRIZIONE", False)
            If cmbEsercizio.SelectedValue <> "" And cmbEsercizio.SelectedValue <> "-1" Then
                cmbFornitore.SelectedValue = "-1"
                cmbAppalto.SelectedValue = "-1"
                cmbServizioVoce.SelectedValue = "-1"

                FiltraFornitori()
                FiltraAppalti()
                FiltraServizioVoce()
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If


        Catch ex As Exception
            connData.chiudi()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    

   
    
Protected Sub DropDownListProgettoVision_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles DropDownListProgettoVision.SelectedIndexChanged
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If
        par.caricaComboTelerik("select distinct numero_sal from siscom_mi.import_str where codice_progetto_vision='" & DropDownListProgettoVision.SelectedValue & "'", DropDownListNumeroSALVision, "numero_sal", "numero_sal", True, "-1", " ")
		cmbFornitore.SelectedValue = "-1"
        cmbAppalto.SelectedValue = "-1"
        cmbServizioVoce.SelectedValue = "-1"
        FiltraFornitori()
        FiltraAppalti()
        FiltraServizioVoce()
        FiltraCodiceProgetto(DropDownListProgettoVision.SelectedValue)
        FiltraNumeroSalVision()
        If connAperta = True Then
            connData.chiudi(False)
        End If
    End Sub
    Protected Sub DropDownListNumeroSALVision_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListNumeroSALVision.SelectedIndexChanged
        cmbFornitore.SelectedValue = "-1"
        cmbAppalto.SelectedValue = "-1"
        cmbServizioVoce.SelectedValue = "-1"
        FiltraFornitori()
        FiltraAppalti()
        FiltraServizioVoce()
        FiltraCodiceProgetto(DropDownListProgettoVision.SelectedValue)
        FiltraNumeroSalVision(DropDownListNumeroSALVision.SelectedValue)
    End Sub
    
    Private Sub FiltraCodiceProgetto(Optional ByVal codiceProgetto As String = "")
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = "SELECT DISTINCT CODICE_PROGETTO_VISION " _
                & " FROM SISCOM_MI.IMPORT_STR " _
                & " WHERE CODICE_ODL IN (SELECT PROGR||'/'||ANNO " _
                & " FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE MANUTENZIONI.ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                & " AND STATO=4 " _
                & " AND ID_PAGAMENTO IS NOT NULL " _
                & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") _
                & ")"
            par.caricaComboTelerik(query, DropDownListProgettoVision, "CODICE_PROGETTO_VISION", "CODICE_PROGETTO_VISION", True, "-1", "")
            'If DropDownListProgettoVision.Items.Count = 2 Then
            '    DropDownListProgettoVision.SelectedValue = DropDownListProgettoVision.Items(1).Value
            'End If
            If codiceProgetto <> "" Then
                DropDownListProgettoVision.SelectedValue = codiceProgetto
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub FiltraNumeroSalVision(Optional ByVal numeroSal As String = "")
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = "SELECT DISTINCT NUMERO_SAL FROM SISCOM_MI.IMPORT_STR WHERE CODICE_PROGETTO_VISION='" & DropDownListProgettoVision.SelectedValue & "'"
            par.caricaComboTelerik(query, DropDownListNumeroSALVision, "NUMERO_SAL", "NUMERO_SAL", True, "-1", "")
If numeroSal <> "" Then
                DropDownListNumeroSALVision.SelectedValue = numeroSal
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_NCP.aspx""</script>")

    End Sub
End Class
