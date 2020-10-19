Imports Telerik.Web.UI

Partial Class GESTIONE_CONTATTI_NuovaSegnalazione
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                Select Case Session.Item("ID_CAF")
                    Case "63"
                        'OPERATORE DI CALL CENTER
                        operatoreCC.Value = "1"
                        operatoreFiliale.Value = "0"
                        operatoreFilialeTecnico.Value = "0"
                        operatoreComune.Value = "0"
                    Case "2"
                        'OPERATORE MM
                        operatoreCC.Value = "0"
                        operatoreFiliale.Value = "1"
                        operatoreFilialeTecnico.Value = "0"
                        operatoreComune.Value = "0"
                    Case "6"
                        'OPERATORE COMUNALE
                        operatoreCC.Value = "0"
                        operatoreFiliale.Value = "0"
                        operatoreFilialeTecnico.Value = "0"
                        operatoreComune.Value = "1"
                    Case Else
                        par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
                End Select
                cmbUrgenza.LoadContentFile("Gravita.xml")
                Session.Remove("DataGridSegnalazUnita")
                Session.Remove("DataGridSegnalazEdifici")
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                CaricaCustode()
                ControllaNumeriUtili()
                CaricaComplessi()
                CaricaComplessiChiamante()
                CaricaEdifici()
                CaricaEdificiChiamante()
                CaricaSedeTerritoriale()
                CaricaTutteTipologie()
                CaricaTipologieLivello0()
                caricaCanale()
                caricaTipologiaSegnalante()
                If operatoreCC.Value = "1" Then
                    CheckBoxAttoVandalico.Enabled = False
                End If
                If operatoreComune.Value <> "1" And Session.Item("FL_GC_SUPERVISORE") <> "1" Then
                    CheckBoxDVCA.Enabled = False
                End If
                If operatoreFiliale.Value = "1" Then
                    CheckBoxContattatoFornitore.Enabled = False
                End If
                If connAperta = True Then
                    connData.chiudi(False)
                End If
                DropDownListScala.Enabled = False
                DropDownListInterno.Enabled = False
                DropDownListPiano.Enabled = False
                DropDownListSedeTerritoriale.Enabled = False
                lblDataGridSegnalazioniEdificioSelezionato.Text = "Nessuna segnalazione trovata."
                lblDataGridSegnalazioniUnitaSelezionata.Text = "Nessuna segnalazione trovata."
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
                Exit Sub
            Else
                If CType(Me.Master.FindControl("FLGC_SL"), HiddenField).Value = "1" Then
                    solaLettura()
                End If
            End If

            txtDataProgrammataIntervento.Enabled = False
            txtDataSopralluogo.Enabled = False
            txtDataEffettivaIntervento.Enabled = False
            txtOraProgrammataIntervento.Enabled = False
            txtOraSopralluogo.Enabled = False
            txtOraEffettivaIntervento.Enabled = False
            ModificaOggPage()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub ImageButtonCercaChiamante_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonCercaChiamante.Click
        Try
            If Not String.IsNullOrEmpty(TextBoxCognomeChiamante.Text) Or Not String.IsNullOrEmpty(txtContrattoChiamante.Text) Or Not String.IsNullOrEmpty(TextBoxIndirizzoChiamante.Text) _
                 Or DropDownListEdificioChiamante.SelectedValue <> "-1" Or DropDownListPianoChiamante.SelectedValue <> "-1" Or DropDownListInternoChiamante.SelectedValue <> "-1" Or DropDownListScalaChiamante.SelectedValue <> "-1" Then
                CaricaChiamanti()
                VisualizzaViewRicercaChiamante()
            Else
                par.modalDialogMessage("Identificazione chiamante", "E\' necessario impostare correttamente almeno un campo.", Me.Page, "info", , )
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ImageButtonCercaChiamante_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub ImageButtonCercaIntestatario_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonCercaIntestatario.Click
        Try
            If Not String.IsNullOrEmpty(TextBoxCognomeIntestatario.Text) Or Not String.IsNullOrEmpty(TextBoxCodiceContrattoIntestatario.Text) Or Not String.IsNullOrEmpty(TextBoxIndirizzoIntestatario.Text) _
                Or DropDownListEdificio.SelectedValue <> "-1" Or DropDownListPiano.SelectedValue <> "-1" Or DropDownListInterno.SelectedValue <> "-1" Or DropDownListScala.SelectedValue <> "-1" Then
                CaricaIntestatari()
                VisualizzaViewRicercaIntestatario()
            Else
                par.modalDialogMessage("Identificazione chiamante", "E\' necessario impostare correttamente almeno un campo.", Me.Page, "info", , )
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ImageButtonCercaIntestatario_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaCustode()
        Try
            If Not IsNothing(Request.QueryString("idc")) Then
                Dim idAnagraficaCustodi = Request.QueryString("idc")
                flCustode.Value = "1"
                ButtonNuovaSegnalazioneCustode.Visible = True
                If cmbTipoSegnalazioneLivello0.SelectedValue <> "6" Then
                    CaricaTutteTipologie()
                    CaricaTipologieLivello0()
                End If
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                Dim idAnagraficaSelezionata As String = idAnagraficaCustodi
                idAnagraficaChiamante.Value = idAnagraficaCustodi
                If IsNumeric(idAnagraficaSelezionata) Then
                    par.cmd.CommandText = "SELECT COGNOME,NOME,TELEFONO_AZIENDALE,CELLULARE_AZIENDALE,EMAIL_AZIENDALE FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ID=" & idAnagraficaSelezionata
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME"), "")
                        TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                        TextBoxCognomeChiamante.Enabled = False
                        TextBoxNomeChiamante.Enabled = False
                        TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO_AZIENDALE"), "")
                        TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("CELLULARE_AZIENDALE"), "")
                        TextBoxEmailChiamante.Text = par.IfNull(lettore("EMAIL_AZIENDALE"), "")
                    End If
                    lettore.Close()
                End If
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaCustode - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaNumeriUtili()
        Try
            'connData.apri(False)
            'If DropDownListSedeTerritoriale.SelectedValue <> "-1" And DropDownListSedeTerritoriale.SelectedValue <> "" Then
            '    par.cmd.CommandText = "SELECT NUMERI_UTILI.ID,DESCRIZIONE AS TIPO, VALORE,FASCIA_ORARIA AS FASCIA,TAB_FILIALI.NOME AS SEDE_TERRITORIALE,'' AS ELIMINA " _
            '        & " FROM SISCOM_MI.NUMERI_UTILI, SISCOM_MI.TIPOLOGIE_NUMERI_UTILI, SISCOM_MI.TAB_FILIALI " _
            '        & " WHERE NUMERI_UTILI.ID_TIPOLOGIE_NUMERI_UTILI = TIPOLOGIE_NUMERI_UTILI.ID " _
            '        & " AND TAB_FILIALI.ID(+)=NUMERI_UTILI.ID_STRUTTURA " _
            '        & " AND (ID_sTRUTTURA IS NULL OR ID_sTRUTTURA =" & DropDownListSedeTerritoriale.SelectedValue & " )"
            'Else
            '    par.cmd.CommandText = "SELECT NUMERI_UTILI.ID,DESCRIZIONE AS TIPO, VALORE,FASCIA_ORARIA AS FASCIA,TAB_FILIALI.NOME AS SEDE_TERRITORIALE,'' AS ELIMINA " _
            '        & " FROM SISCOM_MI.NUMERI_UTILI, SISCOM_MI.TIPOLOGIE_NUMERI_UTILI, SISCOM_MI.TAB_FILIALI " _
            '        & " WHERE NUMERI_UTILI.ID_TIPOLOGIE_NUMERI_UTILI = TIPOLOGIE_NUMERI_UTILI.ID " _
            '        & " AND TAB_FILIALI.ID(+)=NUMERI_UTILI.ID_STRUTTURA " _
            '        & " AND ID_sTRUTTURA IS NULL "
            'End If
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim condizioneRicerca As String = ""
            If DropDownListSedeTerritoriale.SelectedValue <> "-1" And DropDownListSedeTerritoriale.SelectedValue <> "" Then
                condizioneRicerca &= " and tab_filiali.id=" & DropDownListSedeTerritoriale.SelectedValue
            End If

            Dim oggi As Date = Now
            Dim giorno As Integer = oggi.DayOfWeek
            Dim ora As Integer = oggi.Hour
            Dim tipologiaFascia As Integer = 0

            Select Case giorno
                Case 1

                    If ora >= 0 And ora < 9 Then
                        tipologiaFascia = 3
                    ElseIf ora >= 9 And ora < 18 Then
                        tipologiaFascia = 1
                    ElseIf ora >= 18 And ora < 24 Then
                        tipologiaFascia = 2
                    End If
                Case 2

                    If ora >= 0 And ora < 9 Then
                        tipologiaFascia = 2
                    ElseIf ora >= 9 And ora < 18 Then
                        tipologiaFascia = 1
                    ElseIf ora >= 18 And ora < 24 Then
                        tipologiaFascia = 2
                    End If
                Case 3

                    If ora >= 0 And ora < 9 Then
                        tipologiaFascia = 2
                    ElseIf ora >= 9 And ora < 18 Then
                        tipologiaFascia = 1
                    ElseIf ora >= 18 And ora < 24 Then
                        tipologiaFascia = 2
                    End If
                Case 4

                    If ora >= 0 And ora < 9 Then
                        tipologiaFascia = 2
                    ElseIf ora >= 9 And ora < 18 Then
                        tipologiaFascia = 1
                    ElseIf ora >= 18 And ora < 24 Then
                        tipologiaFascia = 2
                    End If
                Case 5

                    If ora >= 0 And ora < 9 Then
                        tipologiaFascia = 2
                    ElseIf ora >= 9 And ora < 18 Then
                        tipologiaFascia = 1
                    ElseIf ora >= 18 And ora < 24 Then
                        tipologiaFascia = 2
                    End If
                Case 6

                    If ora >= 0 And ora < 9 Then
                        tipologiaFascia = 2
                    ElseIf ora >= 9 And ora < 18 Then
                        tipologiaFascia = 3
                    ElseIf ora >= 18 And ora < 24 Then
                        tipologiaFascia = 3
                    End If
                Case 7

                    If ora >= 0 And ora < 9 Then
                        tipologiaFascia = 3
                    ElseIf ora >= 9 And ora < 18 Then
                        tipologiaFascia = 3
                    ElseIf ora >= 18 And ora < 24 Then
                        tipologiaFascia = 3
                    End If
            End Select


            'Dim fasciaIniziale As String = DropDownListOraInizio.SelectedValue & ":" & DropDownListMinutiInizio.SelectedValue
            'If Len(fasciaIniziale) = 5 Then
            '    condizioneRicerca &= " AND SUBSTR(FASCIA_ORARIA,1,5)>='" & fasciaIniziale & "'"
            'End If
            'Dim fasciaFinale As String = DropDownListOraFine.SelectedValue & ":" & DropDownListMinutifine.SelectedValue
            'If Len(fasciaFinale) = 5 Then
            '    condizioneRicerca &= " AND SUBSTR(FASCIA_ORARIA,7,5)<='" & fasciaFinale & "'"
            'End If

            If tipologiaFascia <> 0 Then
                'condizioneRicerca &= "AND (ID_TIPO_fascia=" & tipologiaFascia & " OR (ID_TIPO_fascia=4 AND SUBSTR(FASCIA_ORARIA,1,5)>='" & fasciaIniziale & "' AND SUBSTR(FASCIA_ORARIA,7,5)<='" & fasciaFinale & "'))"
                condizioneRicerca &= "AND (ID_TIPO_fascia=" & tipologiaFascia & ")"
            End If

            If cmbTipoSegnalazioneLivello0.SelectedValue <> "-1" Then
                condizioneRicerca &= "AND ID_TIPO_SEGNALAZIONE=" & cmbTipoSegnalazioneLivello0.SelectedValue
            End If

            par.cmd.CommandText = "SELECT NUMERI_UTILI.ID,DESCRIZIONE AS TIPO, VALORE," _
                & " (case when id_tipo_fascia = 1 " _
                & " then 'LUN-VEN 09:00-18:00' " _
                & " when id_tipo_fascia = 2 " _
                & " then 'LUN-VEN 18:00-09:00' " _
                & " when id_tipo_fascia = 3 " _
                & " then 'SAB-DOM' " _
                & " when id_tipo_fascia = 4 " _
                & " THEN giorni ||' '||FASCIA_ORARIA " _
                & " ELSE " _
                & " NULL " _
                & " END " _
                & " ) " _
                & " AS FASCIA, " _
                & " (CASE WHEN TAB_FILIALI.NOME IS NULL THEN 'SEDE CENTRALE' ELSE TAB_FILIALI.NOME END) AS SEDE_TERRITORIALE,'' AS ELIMINA " _
                & " FROM SISCOM_MI.NUMERI_UTILI, SISCOM_MI.TIPOLOGIE_NUMERI_UTILI, SISCOM_MI.TAB_FILIALI " _
                & " WHERE NUMERI_UTILI.ID_TIPOLOGIE_NUMERI_UTILI = TIPOLOGIE_NUMERI_UTILI.ID " _
                & " AND TAB_FILIALI.ID(+)=NUMERI_UTILI.ID_STRUTTURA " _
                & condizioneRicerca
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            DataGridNumeriUtili.DataSource = dt
            DataGridNumeriUtili.DataBind()
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaNumeriUtili - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaChiamanti()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim condizioneChiamante As String = ""
            Dim condizioneChiamanteCustodi As String = ""
            Dim condizioneChiamanteNonNoti As String = ""
            Dim condizioneChiamanteFornitore As String = ""
            Dim condizioneChiamanteGestAutonoma As String = ""
            Dim condizioneChiamanteAmministratore As String = ""
            Dim condizioneSoggettiCoinvolti As String = ""
            Dim cognome = Trim(TextBoxCognomeChiamante.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            Dim nome = Trim(TextBoxNomeChiamante.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            Dim codContratto As String = Trim(txtContrattoChiamante.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            Dim indirizzo As String = Trim(TextBoxIndirizzoChiamante.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            Dim edificio As String = DropDownListEdificioChiamante.SelectedValue
            Dim scala As String = DropDownListScalaChiamante.SelectedValue
            Dim piano As String = DropDownListPianoChiamante.SelectedValue
            Dim interno As String = DropDownListInternoChiamante.SelectedValue
            If edificio = "" Then edificio = "-1"
            If scala = "" Then scala = "-1"
            If piano = "" Then piano = "-1"
            If interno = "" Then interno = "-1"

            If cognome <> "" Then
                condizioneChiamante &= " AND (UPPER(ANAGRAFICA.COGNOME) LIKE '" & cognome & "%' OR UPPER(ANAGRAFICA.RAGIONE_SOCIALE) LIKE '" & cognome & "%') "
                If condizioneChiamanteCustodi = "" Then
                    condizioneChiamanteCustodi &= " WHERE (UPPER(ANAGRAFICA_CUSTODI.COGNOME) LIKE '" & cognome & "%') "
                Else
                    condizioneChiamanteCustodi &= " AND (UPPER(ANAGRAFICA_CUSTODI.COGNOME) LIKE '" & cognome & "%') "
                End If
                If condizioneChiamanteNonNoti = "" Then
                    condizioneChiamanteNonNoti &= " WHERE (UPPER(ANAGRAFICA_CHIAMANTI_NON_NOTI.COGNOME) LIKE '" & cognome & "%') "
                Else
                    condizioneChiamanteNonNoti &= " AND (UPPER(ANAGRAFICA_CHIAMANTI_NON_NOTI.COGNOME) LIKE '" & cognome & "%') "
                End If
                If condizioneChiamanteFornitore = "" Then
                    condizioneChiamanteFornitore &= " WHERE ((UPPER(FORNITORI.COGNOME) LIKE '" & cognome & "%') OR (UPPER(FORNITORI.RAGIONE_SOCIALE) LIKE '" & cognome & "%')) "
                Else
                    condizioneChiamanteFornitore &= " AND ((UPPER(FORNITORI.COGNOME) LIKE '" & cognome & "%') OR (UPPER(FORNITORI.RAGIONE_SOCIALE) LIKE '" & cognome & "%')) "
                End If
                If condizioneChiamanteGestAutonoma = "" Then
                    condizioneChiamanteGestAutonoma &= " WHERE (UPPER(AUTOGESTIONI_ESERCIZI.RAPP_COGNOME) LIKE '" & cognome & "%') "
                Else
                    condizioneChiamanteGestAutonoma &= " AND (UPPER(AUTOGESTIONI_ESERCIZI.RAPP_COGNOME) LIKE '" & cognome & "%') "
                End If
                If condizioneChiamanteAmministratore = "" Then
                    condizioneChiamanteAmministratore &= " WHERE (UPPER(COND_AMMINISTRATORI.COGNOME) LIKE '" & cognome & "%') "
                Else
                    condizioneChiamanteAmministratore &= " AND (UPPER(COND_AMMINISTRATORI.COGNOME) LIKE '" & cognome & "%') "
                End If
                If condizioneSoggettiCoinvolti = "" Then
                    condizioneSoggettiCoinvolti &= " WHERE (UPPER(ANAGRAFICA_SOGG_COINVOLTI.COGNOME_SOGG_COINVOLTO) LIKE '" & cognome & "%') "
                Else
                    condizioneSoggettiCoinvolti &= " AND (UPPER(ANAGRAFICA_SOGG_COINVOLTI.COGNOME_SOGG_COINVOLTO) LIKE '" & cognome & "%') "
                End If
            End If
            If nome <> "" Then
                condizioneChiamante &= " AND (UPPER(ANAGRAFICA.NOME) LIKE '" & nome & "%') "
                If condizioneChiamanteCustodi = "" Then
                    condizioneChiamanteCustodi &= " WHERE (UPPER(ANAGRAFICA_CUSTODI.NOME) LIKE '" & nome & "%') "
                Else
                    condizioneChiamanteCustodi &= " AND (UPPER(ANAGRAFICA_CUSTODI.NOME) LIKE '" & nome & "%') "
                End If
                If condizioneChiamanteNonNoti = "" Then
                    condizioneChiamanteNonNoti &= " WHERE (UPPER(ANAGRAFICA_CHIAMANTI_NON_NOTI.NOME) LIKE '" & nome & "%') "
                Else
                    condizioneChiamanteNonNoti &= " AND (UPPER(ANAGRAFICA_CHIAMANTI_NON_NOTI.NOME) LIKE '" & nome & "%') "
                End If
                If condizioneChiamanteFornitore = "" Then
                    condizioneChiamanteFornitore &= " WHERE (UPPER(FORNITORI.NOME) LIKE '" & nome & "%') "
                Else
                    condizioneChiamanteFornitore &= " AND (UPPER(FORNITORI.NOME) LIKE '" & nome & "%') "
                End If
                If condizioneChiamanteGestAutonoma = "" Then
                    condizioneChiamanteGestAutonoma &= " WHERE (UPPER(AUTOGESTIONI_ESERCIZI.RAPP_NOME) LIKE '" & nome & "%') "
                Else
                    condizioneChiamanteGestAutonoma &= " AND (UPPER(AUTOGESTIONI_ESERCIZI.RAPP_NOME) LIKE '" & nome & "%') "
                End If
                If condizioneChiamanteAmministratore = "" Then
                    condizioneChiamanteAmministratore &= " WHERE (UPPER(COND_AMMINISTRATORI.NOME) LIKE '" & nome & "%') "
                Else
                    condizioneChiamanteAmministratore &= " AND (UPPER(COND_AMMINISTRATORI.NOME) LIKE '" & nome & "%') "
                End If
                If condizioneSoggettiCoinvolti = "" Then
                    condizioneSoggettiCoinvolti &= " WHERE (UPPER(ANAGRAFICA_SOGG_COINVOLTI.NOME_SOGG_COINVOLTO) LIKE '" & nome & "%') "
                Else
                    condizioneSoggettiCoinvolti &= " AND (UPPER(ANAGRAFICA_SOGG_COINVOLTI.NOME_SOGG_COINVOLTO) LIKE '" & nome & "%') "
                End If
            End If
            If Not String.IsNullOrEmpty(codContratto) Then
                condizioneChiamante &= " AND UPPER(RAPPORTI_UTENZA.COD_CONTRATTO) LIKE '%" & codContratto & "%' "
            End If
            'Indirizzo
            If indirizzo <> "" Then
                condizioneChiamante &= " AND (UPPER(INDIRIZZI.DESCRIZIONE) LIKE '" & indirizzo & "%') "
            End If
            If edificio <> "-1" Then
                condizioneChiamante &= " AND UNITA_IMMOBILIARI.ID_EDIFICIO = " & edificio
            End If
            'Scala
            If scala <> "-1" Then
                condizioneChiamante &= " AND SCALE_EDIFICI.ID = " & scala
            End If
            'Interno
            If interno <> "-1" Then
                condizioneChiamante &= " AND UNITA_IMMOBILIARI.INTERNO = " & par.insDbValue(interno, True)
            End If
            'Piano
            If piano <> "-1" Then
                condizioneChiamante &= " AND TIPO_LIVELLO_PIANO.COD = " & piano
            End If

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ID_UNITA,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AS ID_INTE,ANAGRAFICA.ID, SOGGETTI_CONTRATTUALI.ID_CONTRATTO, " _
                & " CASE WHEN RAGIONE_SOCIALE IS NULL THEN COGNOME ||' '|| NOME ELSE RAGIONE_SOCIALE END AS NOMINATIVO,siscom_mi.getintestatari (unita_contrattuale.id_contratto) AS intestatario, " _
                & " TO_CHAR(TO_DATE(SUBSTR(DATA_NASCITA,0,8),'yyyymmdd'),'dd/mm/yyyy')AS DATA_NASCITA, " _
                & " INDIRIZZI.DESCRIZIONE AS INDIRIZZO ,INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RAPPORTI_UTENZA.COD_CONTRATTO AS CODICE_CONTRATTO,ID_ANAGRAFICA " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA ,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI," _
                & " SISCOM_MI.PIANI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI " _
                & " WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                & " AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA " _
                & " AND ID_INDIRIZZO = INDIRIZZI.ID " _
                & " AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID (+) " _
                & " AND UNITA_IMMOBILIARI.cod_tipologia = TIPOLOGIA_UNITA_IMMOBILIARI.cod " _
                & " AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                & " AND UNITA_IMMOBILIARI.ID_PIANO = PIANI.ID(+) " _
                & " AND PIANI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) " _
                & condizioneChiamante _
                & "order by NOMINATIVO ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            DataGridChiamanti.DataSource = dt
            DataGridChiamanti.DataBind()
            If Not String.IsNullOrEmpty(TextBoxCognomeChiamante.Text) Then
                par.cmd.CommandText = "SELECT ID,MATRICOLA," _
                & " COGNOME," _
                & " NOME," _
                & " DECODE(FL_DIPENDENTE_MM,1,'Sì','No') AS DIPENDENTE_MM," _
                & " EMAIL_AZIENDALE AS EMAIL," _
                & " CELLULARE_aZIENDALE AS CELLULARE," _
                & " TELEFONO_aZIENDALE AS TELEFONO " _
                & " FROM SISCOM_MI.ANAGRAFICA_CUSTODI " _
                & condizioneChiamanteCustodi _
                & " ORDER BY COGNOME,NOME ASC"
                Dim dac As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtC As New Data.DataTable
                dac.Fill(dtC)
                dac.Dispose()
                DataGridCustodi.DataSource = dtC
                DataGridCustodi.DataBind()

                par.cmd.CommandText = "SELECT ID," _
                & " COGNOME," _
                & " NOME," _
                & " EMAIL," _
                & " CELLULARE," _
                & " TELEFONO " _
                & " FROM SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI " _
                & condizioneChiamanteNonNoti _
                & " ORDER BY COGNOME,NOME ASC"
                Dim daNN As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtNN As New Data.DataTable
                daNN.Fill(dtNN)
                daNN.Dispose()
                DataGridChiamantiNonNoti.DataSource = dtNN
                DataGridChiamantiNonNoti.DataBind()
                lblElencoCustodi.Visible = True
                DataGridCustodi.Visible = True

                par.cmd.CommandText = "SELECT " _
                & " ID, " _
                & " (CASE WHEN RAGIONE_SOCIALE IS NOT NULL THEN RAGIONE_SOCIALE ELSE COGNOME END) AS COGNOME, " _
                & " (CASE WHEN RAGIONE_SOCIALE IS NOT NULL THEN NULL ELSE NOME END) AS NOME, " _
                & " FORNITORI.MAIL " _
                & " FROM SISCOM_MI.FORNITORI " _
                & condizioneChiamanteFornitore _
                & " ORDER BY COGNOME,NOME ASC"
                Dim daFO As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtFO As New Data.DataTable
                daFO.Fill(dtFO)
                daFO.Dispose()
                DataGridFornitori.DataSource = dtFO
                DataGridFornitori.DataBind()
                lblElencoFornitori.Visible = True
                DataGridCustodi.Visible = True

                par.cmd.CommandText = "SELECT ID,RAPP_COGNOME AS COGNOME,RAPP_NOME AS NOME FROM SISCOM_MI.AUTOGESTIONI_eSERCIZI " _
                & condizioneChiamanteGestAutonoma _
                & " ORDER BY COGNOME,NOME ASC"
                Dim daGA As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtGA As New Data.DataTable
                daGA.Fill(dtGA)
                daGA.Dispose()
                DataGridGestAutonoma.DataSource = dtGA
                DataGridGestAutonoma.DataBind()
                lblGestAutonoma.Visible = True
                DataGridCustodi.Visible = True


                par.cmd.CommandText = "  SELECT ID,COGNOME,NOME,EMAIL,TEL_1 AS TELEFONO,CELL AS CELLULARE FROM SISCOM_MI.COND_AMMINISTRATORI " _
                & condizioneChiamanteAmministratore _
                & " ORDER BY COGNOME,NOME ASC"
                Dim daAM As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtAM As New Data.DataTable
                daAM.Fill(dtAM)
                daAM.Dispose()
                DataGridAmministratoreCond.DataSource = dtAM
                DataGridAmministratoreCond.DataBind()
                lblAmministratoreCond.Visible = True
                DataGridCustodi.Visible = True

                par.cmd.CommandText = "SELECT ID,COGNOME_SOGG_COINVOLTO AS COGNOME,NOME_SOGG_COINVOLTO AS NOME,TELEFONO FROM SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI " _
                & condizioneSoggettiCoinvolti _
                & " ORDER BY COGNOME,NOME ASC"
                Dim daSC As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtSC As New Data.DataTable
                daSC.Fill(dtSC)
                daSC.Dispose()
                DataGridSoggCoinv.DataSource = dtSC
                DataGridSoggCoinv.DataBind()
                lblSoggettiCoinvolti.Visible = True
                DataGridCustodi.Visible = True

            Else
                lblElencoCustodi.Visible = False
                DataGridCustodi.Visible = False
                lblElencoChiamantiNonNoti.Visible = False
                DataGridChiamantiNonNoti.Visible = False
                lblElencoFornitori.Visible = False
                DataGridFornitori.Visible = False
                lblGestAutonoma.Visible = False
                DataGridGestAutonoma.Visible = False
                lblAmministratoreCond.Visible = False
                DataGridAmministratoreCond.Visible = False
                lblSoggettiCoinvolti.Visible = False
                DataGridSoggCoinv.Visible = False
            End If
            idSelectedChiamante.Value = ""
            idAnagraficaChiamante.Value = ""
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaChiamanti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DataGridChiamanti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridChiamanti.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_UNITA")).Text.Replace("'", "\'") & "';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_ANAGRAFICA")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_CONTRATTO")).Text.Replace("'", "\'") & "';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_UNITA")).Text.Replace("'", "\'") & "';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_ANAGRAFICA")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_CONTRATTO")).Text.Replace("'", "\'") & "';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridChiamanti_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGridCustodi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCustodi.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='C';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridCustodi, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='C';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='C';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridCustodi, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='C';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridCustodi_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGridChiamantiNonNoti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridChiamantiNonNoti.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='NN';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridChiamantiNonNoti, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='NN';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='NN';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridChiamantiNonNoti, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='NN';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridChiamantiNonNoti_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Private Sub CaricaIntestatari()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim condizioneIntestatario As String = ""
            Dim cognome = Trim(TextBoxCognomeIntestatario.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            Dim nome = Trim(TextBoxNomeIntestatario.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            Dim codContratto As String = Trim(TextBoxCodiceContrattoIntestatario.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            Dim indirizzo As String = Trim(TextBoxIndirizzoIntestatario.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            Dim edificio As String = DropDownListEdificio.SelectedValue
            Dim scala As String = DropDownListScala.SelectedValue
            Dim piano As String = DropDownListPiano.SelectedValue
            Dim interno As String = DropDownListInterno.SelectedValue
            If edificio = "" Then edificio = "-1"
            If scala = "" Then scala = "-1"
            If piano = "" Then piano = "-1"
            If interno = "" Then interno = "-1"
            If cognome <> "" Then
                condizioneIntestatario &= " AND (UPPER(ANAGRAFICA.COGNOME) LIKE '" & cognome & "%' OR UPPER(ANAGRAFICA.RAGIONE_SOCIALE) LIKE '" & cognome & "%') "
            End If
            If nome <> "" Then
                condizioneIntestatario &= " AND (UPPER(ANAGRAFICA.NOME) LIKE '" & nome & "%') "
            End If
            If Not String.IsNullOrEmpty(codContratto) Then
                condizioneIntestatario &= " AND UPPER(RAPPORTI_UTENZA.COD_CONTRATTO) LIKE '%" & codContratto & "%' "
            End If
            'Indirizzo
            If indirizzo <> "" Then
                condizioneIntestatario &= " AND (UPPER(INDIRIZZI.DESCRIZIONE) LIKE '" & indirizzo & "%') "
            End If
            If edificio <> "-1" Then
                condizioneIntestatario &= " AND UNITA_IMMOBILIARI.ID_EDIFICIO = " & edificio
            End If
            'Scala
            If scala <> "-1" Then
                condizioneIntestatario &= " AND SCALE_EDIFICI.ID = " & scala
            End If
            'Interno
            If interno <> "-1" Then
                condizioneIntestatario &= " AND UNITA_IMMOBILIARI.INTERNO = " & par.insDbValue(interno, True)
            End If
            'Piano
            If piano <> "-1" Then
                condizioneIntestatario &= " AND TIPO_LIVELLO_PIANO.COD = " & piano
            End If

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ID_UNITA,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AS ID_INTE,ANAGRAFICA.ID, SOGGETTI_CONTRATTUALI.ID_CONTRATTO, " _
                & " CASE WHEN RAGIONE_SOCIALE IS NULL THEN COGNOME ||' '|| NOME ELSE RAGIONE_SOCIALE END AS NOMINATIVO,SISCOM_MI.GETINTESTATARI (UNITA_CONTRATTUALE.ID_CONTRATTO) AS INTESTATARIO, " _
                & " TO_CHAR(TO_DATE(SUBSTR(DATA_NASCITA,0,8),'YYYYMMDD'),'DD/MM/YYYY')AS DATA_NASCITA, " _
                & " INDIRIZZI.DESCRIZIONE AS INDIRIZZO ,INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RAPPORTI_UTENZA.COD_CONTRATTO AS CODICE_CONTRATTO,ID_ANAGRAFICA " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA ,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI," _
                & " SISCOM_MI.PIANI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI " _
                & " WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                & " AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA " _
                & " AND ID_INDIRIZZO = INDIRIZZI.ID " _
                & " AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID (+) " _
                & " AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                & " AND COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                & " AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                & " AND UNITA_IMMOBILIARI.ID_PIANO = PIANI.ID(+) " _
                & " AND PIANI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) " _
                & condizioneIntestatario _
                & " ORDER BY NOMINATIVO ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            DataGridIntestatari.DataSource = dt
            DataGridIntestatari.DataBind()
            idSelectedIntestatario.Value = ""
            idAnagraficaIntestatario.Value = ""
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaIntestatari - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DataGridIntestatari_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIntestatari.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoIntestatario) {SelezionatoIntestatario.style.backgroundColor=''} SelezionatoIntestatario=this;this.style.backgroundColor='orange';document.getElementById('idSelectedIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_UNITA")).Text.Replace("'", "\'") & "';document.getElementById('idAnagraficaIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_ANAGRAFICA")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_CONTRATTO")).Text.Replace("'", "\'") & "';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaIntestatario').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoIntestatario) {SelezionatoIntestatario.style.backgroundColor=''} SelezionatoIntestatario=this;this.style.backgroundColor='orange';document.getElementById('idSelectedIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_UNITA")).Text.Replace("'", "\'") & "';document.getElementById('idAnagraficaIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_ANAGRAFICA")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridChiamanti, "ID_CONTRATTO")).Text.Replace("'", "\'") & "';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaIntestatario').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridChiamanti_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnIndietro_Click(sender As Object, e As System.EventArgs) Handles btnIndietro.Click
        VisualizzaViewIdentificazioneChiamante()
    End Sub
    Protected Sub btnConfermaChiamante_Click(sender As Object, e As System.EventArgs) Handles btnConfermaChiamante.Click
        Try
            If idSelectedChiamante.Value = "C" Then
                flCustode.Value = "1"
                'DropDownListCanale.SelectedValue = 4
                ButtonNuovaSegnalazioneCustode.Visible = True
                If cmbTipoSegnalazioneLivello0.SelectedValue <> "6" Then
                    CaricaTutteTipologie()
                    CaricaTipologieLivello0()
                End If
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                Dim idAnagraficaSelezionata As String = idAnagraficaChiamante.Value
                If IsNumeric(idAnagraficaSelezionata) Then
                    par.cmd.CommandText = "SELECT COGNOME,NOME,TELEFONO_AZIENDALE,CELLULARE_AZIENDALE,EMAIL_AZIENDALE FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ID=" & idAnagraficaSelezionata
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME"), "")
                        TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                        TextBoxCognomeChiamante.Enabled = False
                        TextBoxNomeChiamante.Enabled = False
                        TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO_AZIENDALE"), "")
                        TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("CELLULARE_AZIENDALE"), "")
                        TextBoxEmailChiamante.Text = par.IfNull(lettore("EMAIL_AZIENDALE"), "")
                        cmbTipologiaSegnalante.ClearSelection()
                        cmbTipologiaSegnalante.SelectedValue = 7
                    End If
                    lettore.Close()
                End If
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            ElseIf idSelectedChiamante.Value = "NN" Then
                flCustode.Value = "0"
                ButtonNuovaSegnalazioneCustode.Visible = False
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                Dim idAnagraficaSelezionata As String = idAnagraficaChiamante.Value
                If IsNumeric(idAnagraficaSelezionata) Then
                    par.cmd.CommandText = "SELECT COGNOME,NOME,TELEFONO,CELLULARE,EMAIL FROM SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI WHERE ID=" & idAnagraficaSelezionata
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME"), "")
                        TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                        TextBoxCognomeChiamante.Enabled = False
                        TextBoxNomeChiamante.Enabled = False
                        TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO"), "")
                        TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("CELLULARE"), "")
                        TextBoxEmailChiamante.Text = par.IfNull(lettore("EMAIL"), "")
                    End If
                    lettore.Close()
                End If
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            ElseIf idSelectedChiamante.Value = "FO" Then
                flCustode.Value = "0"
                ButtonNuovaSegnalazioneCustode.Visible = False
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                Dim idAnagraficaSelezionata As String = idAnagraficaChiamante.Value
                If IsNumeric(idAnagraficaSelezionata) Then
                    par.cmd.CommandText = " SELECT " _
                        & " ID," _
                        & " (CASE WHEN RAGIONE_SOCIALE IS NOT NULL THEN RAGIONE_SOCIALE ELSE COGNOME END) AS COGNOME," _
                        & " (CASE WHEN RAGIONE_SOCIALE IS NOT NULL THEN NULL ELSE NOME END) AS NOME, " _
                        & " MAIL AS EMAIL FROM SISCOM_MI.FORNITORI WHERE ID=" & idAnagraficaSelezionata
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME"), "")
                        TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                        TextBoxCognomeChiamante.Enabled = False
                        TextBoxNomeChiamante.Enabled = False
                        'TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO"), "")
                        'TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("CELLULARE"), "")
                        TextBoxEmailChiamante.Text = par.IfNull(lettore("EMAIL"), "")
                    End If
                    lettore.Close()
                End If
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            ElseIf idSelectedChiamante.Value = "GA" Then
                flCustode.Value = "0"
                ButtonNuovaSegnalazioneCustode.Visible = False
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                Dim idAnagraficaSelezionata As String = idAnagraficaChiamante.Value
                If IsNumeric(idAnagraficaSelezionata) Then
                    par.cmd.CommandText = "  SELECT ID,RAPP_COGNOME AS COGNOME,RAPP_NOME AS NOME FROM SISCOM_MI.AUTOGESTIONI_eSERCIZI WHERE ID=" & idAnagraficaSelezionata
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME"), "")
                        TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                        TextBoxCognomeChiamante.Enabled = False
                        TextBoxNomeChiamante.Enabled = False
                        'TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO"), "")
                        'TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("CELLULARE"), "")
                        'TextBoxEmailChiamante.Text = par.IfNull(lettore("EMAIL"), "")
                        cmbTipologiaSegnalante.ClearSelection()
                        cmbTipologiaSegnalante.SelectedValue = 6
                    End If
                    lettore.Close()
                End If
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            ElseIf idSelectedChiamante.Value = "AM" Then
                flCustode.Value = "0"
                ButtonNuovaSegnalazioneCustode.Visible = False
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                Dim idAnagraficaSelezionata As String = idAnagraficaChiamante.Value
                If IsNumeric(idAnagraficaSelezionata) Then
                    par.cmd.CommandText = "SELECT ID,COGNOME,NOME,EMAIL,TEL_1 AS TELEFONO,CELL AS CELLULARE FROM SISCOM_MI.COND_AMMINISTRATORI WHERE ID = " & idAnagraficaSelezionata
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME"), "")
                        TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                        TextBoxCognomeChiamante.Enabled = False
                        TextBoxNomeChiamante.Enabled = False
                        TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO"), "")
                        TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("CELLULARE"), "")
                        TextBoxEmailChiamante.Text = par.IfNull(lettore("EMAIL"), "")
                        cmbTipologiaSegnalante.ClearSelection()
                        cmbTipologiaSegnalante.SelectedValue = 5
                    End If
                    lettore.Close()
                End If
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            ElseIf idSelectedChiamante.Value = "SC" Then
                flCustode.Value = "0"
                ButtonNuovaSegnalazioneCustode.Visible = False
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                Dim idAnagraficaSelezionata As String = idAnagraficaChiamante.Value
                If IsNumeric(idAnagraficaSelezionata) Then
                    par.cmd.CommandText = "SELECT ID,COGNOME_SOGG_COINVOLTO AS COGNOME,NOME_SOGG_COINVOLTO AS NOME,TELEFONO FROM SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI WHERE ID = " & idAnagraficaSelezionata
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME"), "")
                        TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                        TextBoxCognomeChiamante.Enabled = False
                        TextBoxNomeChiamante.Enabled = False
                        TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO"), "")
                        'TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("CELLULARE"), "")
                        'TextBoxEmailChiamante.Text = par.IfNull(lettore("EMAIL"), "")
                    End If
                    lettore.Close()
                End If
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            Else
                flCustode.Value = "0"
                ButtonNuovaSegnalazioneCustode.Visible = False
                If IsNumeric(idSelectedChiamante.Value) Then
                    Dim connAperta As Boolean = False
                    If connData.Connessione.State = Data.ConnectionState.Closed Then
                        connData.apri(False)
                        connAperta = True
                    End If
                    Svuota(False, , True)
                    Dim idUnitaSelezionata As String = idSelectedChiamante.Value
                    Dim idAnagraficaSelezionata As String = idAnagraficaChiamante.Value
                    If IsNumeric(idAnagraficaSelezionata) Then
                        par.cmd.CommandText = "SELECT COGNOME,NOME,EMAIL,TELEFONO,TELEFONO_R,RAGIONE_SOCIALE FROM SISCOM_MI.ANAGRAFICA WHERE ID=" & idAnagraficaSelezionata
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            If par.IfNull(lettore("COGNOME"), "") = "" Then
                                TextBoxCognomeChiamante.Text = par.IfNull(lettore("RAGIONE_SOCIALE"), "")
                                TextBoxNomeChiamante.Text = ""
                            Else
                                TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME"), "")
                                TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                            End If
                            TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO"), "")
                            TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("TELEFONO_R"), "")
                            TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("EMAIL"), "")
                            TextBoxCognomeChiamante.Enabled = False
                            TextBoxNomeChiamante.Enabled = False
                            cmbTipologiaSegnalante.ClearSelection()
                            cmbTipologiaSegnalante.SelectedValue = 3
                        End If
                        lettore.Close()
                    End If
                    'If IsNumeric(idUnitaSelezionata) Then
                    '    par.cmd.CommandText = "SELECT (CASE WHEN (RAGIONE_SOCIALE IS NULL) THEN (COGNOME) ELSE (RAGIONE_SOCIALE) END) AS COGNOME_INTESTATARIO, " _
                    '        & " (CASE WHEN (RAGIONE_SOCIALE IS NULL) THEN (NOME) ELSE (NULL) END) AS NOME_INTESTATARIO, " _
                    '        & " RAPPORTI_UTENZA.COD_CONTRATTO, " _
                    '        & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " _
                    '        & " FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_IMMOBILIARI " _
                    '        & " WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                    '        & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                    '        & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                    '        & " AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA " _
                    '        & " AND UNITA_CONTRATTUALE.ID_UNITA= " & idUnitaSelezionata _
                    '        & " /*AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL*/ " _
                    '        & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' "
                    '    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    '    If lettore.Read Then
                    '        TextBoxCognomeIntestatario.Text = par.IfNull(lettore("COGNOME_INTESTATARIO"), "")
                    '        TextBoxNomeIntestatario.Text = par.IfNull(lettore("NOME_INTESTATARIO"), "")
                    '        TextBoxCodiceContrattoIntestatario.Text = par.IfNull(lettore("COD_CONTRATTO"), "")
                    '        TextBoxCodiceUnitaImmobiliare.Text = par.IfNull(lettore("COD_UNITA_IMMOBILIARE"), "")
                    '    End If
                    '    lettore.Close()
                    'End If
                    If connAperta = True Then
                        connData.chiudi(False)
                    End If

                    'CaricaComplessi()
                    'CaricaEdifici()
                    'CaricaScale()
                    'CaricaPiano()
                    'CaricaInterni()
                    'CaricaSedeTerritoriale()
                    'CaricaSegnalazioniEdificioSelezionato()
                    'CaricaSegnalazioniUnitaSelezionata()
                    'ControllaCondominio()
                    'ControllaMoroso()
                    'If DropDownListEdificio.SelectedValue <> "-1" Then
                    '    panelDann.Visible = True
                    'Else
                    '    panelDann.Visible = False
                    '    TextBoxDanneggiante.Text = ""
                    '    TextBoxDanneggiato.Text = ""
                    '    Danneggiante.Value = ""
                    '    Danneggiato.Value = ""
                    'End If
                Else
                    par.modalDialogMessage("Nuova segnalazione", "Selezionare un nominativo.", Page, "info")
                    VisualizzaViewRicercaChiamante()
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - btnConfermaChiamante_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaSegnalazioniUnitaSelezionata(Optional ByVal intestatario As Boolean = False)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim condizioni As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If

            Dim condizioneTipologia As String = ""

            If cmbTipoSegnalazioneLivello0.SelectedValue <> "" Or cmbTipoSegnalazioneLivello1.SelectedValue <> "" Or cmbTipoSegnalazioneLivello2.SelectedValue <> "" Or cmbTipoSegnalazioneLivello3.SelectedValue <> "" Or cmbTipoSegnalazioneLivello4.SelectedValue <> "" Then
                If cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello4.SelectedValue <> "" Then
                    condizioneTipologia = " and id_tipo_Segn_livello_3=" & cmbTipoSegnalazioneLivello3.SelectedValue
                ElseIf cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello3.SelectedValue <> "" Then
                    condizioneTipologia = " and id_tipo_Segn_livello_2=" & cmbTipoSegnalazioneLivello2.SelectedValue
                ElseIf cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello2.SelectedValue <> "" Then
                    condizioneTipologia = " and id_tipo_Segn_livello_1=" & cmbTipoSegnalazioneLivello1.SelectedValue
                ElseIf cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello1.SelectedValue <> "" Then
                    condizioneTipologia = " and id_tipo_Segnalazione=" & cmbTipoSegnalazioneLivello0.SelectedValue
                End If
            End If

            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                condizioni = " AND SEGNALAZIONI.ID_UNITA = " & identificativo
                par.cmd.CommandText = " SELECT SEGNALAZIONI.ID," _
                    & " SEGNALAZIONI.ID AS NUM,ID_PERICOLO_sEGNALAZIONE AS CRITICITA,TIPO_sEGNALAZIONE.DESCRIZIONE AS TIPO,TIPO_sEGNALAZIONE.ID AS ID_TIPO, " _
                    & " REPLACE(TIPOLOGIE_GUASTI.DESCRIZIONE,'/','/ ') AS TIPO_INT,TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                    & " (COGNOME_RS ||' '|| NOME) AS RICHIEDENTE, " _
                    & " TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                    & " DESCRIZIONE_RIC AS DESCRIZIONE, " _
                    & " (select descrizione from siscom_mi.tipo_segnalazione_livello_1 where segnalazioni.id_tipo_segn_livello_1=tipo_segnalazione_livello_1.id) as tipo1," _
                    & " (select descrizione from siscom_mi.tipo_segnalazione_livello_2 where segnalazioni.id_tipo_segn_livello_2=tipo_segnalazione_livello_2.id) as tipo2," _
                    & " (select descrizione from siscom_mi.tipo_segnalazione_livello_3 where segnalazioni.id_tipo_segn_livello_3=tipo_segnalazione_livello_3.id) as tipo3," _
                    & " (select descrizione from siscom_mi.tipo_segnalazione_livello_4 where segnalazioni.id_tipo_segn_livello_4=tipo_segnalazione_livello_4.id) as tipo4, " _
                    & " TIPO_SEGNALAZIONE.ID AS TIPO,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                    & " FROM SISCOM_MI.SEGNALAZIONI,SISCOM_MI.TIPOLOGIE_GUASTI,SISCOM_MI.TAB_STATI_SEGNALAZIONI,SISCOM_MI.TIPO_sEGNALAZIONE " _
                    & " WHERE TIPOLOGIE_GUASTI.ID(+) = SEGNALAZIONI.ID_TIPOLOGIE " _
                    & " AND TAB_STATI_SEGNALAZIONI.ID = SEGNALAZIONI.ID_STATO " _
                    & " AND TIPO_SEGNALAZIONE.ID(+)=SEGNALAZIONI.ID_TIPO_SEGNALAZIONE " _
                    & " and to_date(" & Format(Now, "yyyyMMdd") & ",'yyyyMMdd') - to_date(nvl(substr(data_chiusura,1,8),'30000101'),'yyyyMMdd')<=60 " _
                    & " AND TAB_STATI_sEGNALAZIONI.ID<>2 " _
                    & " AND ID_sEGNALAZIONE_PADRE IS NULL " _
                    & " AND TIPO_SEGNALAZIONE.ID IN (1,6) " _
                    & condizioni & condizioneTipologia & " ORDER BY ID_PERICOLO_sEGNALAZIONE DESC,DATA_ORA_RICHIESTA DESC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    Session.Item("listaSelezioneSegnUnita") = ""
                    lblSegnalazioniUnitaDaunire.Text = "Nessuna segnalazione selezionata"
                    ButtonUnisciSegnalazioni.Visible = True
                Else
                    Session.Item("listaSelezioneSegnUnita") = ""
                    lblSegnalazioniUnitaDaunire.Text = ""
                    ButtonUnisciSegnalazioni.Visible = False
                End If
                Session.Item("DataGridSegnalazUnita") = dt
                RadDataGridSegnalazioniUnitaSelezionata.Rebind()
                lblDataGridSegnalazioniUnitaSelezionata.Text = ""
                RadDataGridSegnalazioniUnitaSelezionata.Visible = True
            Else
                Session.Item("DataGridSegnalazUnita") = Nothing
                RadDataGridSegnalazioniUnitaSelezionata.Rebind()
                lblDataGridSegnalazioniUnitaSelezionata.Text = ""
                RadDataGridSegnalazioniUnitaSelezionata.Visible = False
                Session.Item("listaSelezioneSegnUnita") = ""
                lblSegnalazioniUnitaDaunire.Text = ""
                ButtonUnisciSegnalazioni.Visible = False
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaSegnalazioniUnitaSelezionata - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        If RadioButtonList1.SelectedValue = 0 Then
            'alloggio
            PanelSegnalazioniUnita.Visible = True
            PanelSegnalazioniEdificio.Visible = False
        Else
            'parte comune
            PanelSegnalazioniUnita.Visible = False
            PanelSegnalazioniEdificio.Visible = True
        End If

    End Sub
    Protected Sub RadDataGridSegnalazioniUnitaSelezionata_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadDataGridSegnalazioniUnitaSelezionata.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridSegnalazUnita"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridSegnalazUnita"), Data.DataTable)
    End Sub
    Private Sub CaricaSegnalazioniEdificioSelezionato(Optional ByVal intestatario As Boolean = False)

        Try
            If DropDownListEdificio.SelectedValue <> "-1" Then
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                Dim condizioni As String = ""
                Dim identificativo As String = ""
                If intestatario Then
                    identificativo = idSelectedIntestatario.Value
                Else
                    identificativo = idSelectedChiamante.Value
                End If

                If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                    condizioni = " AND SEGNALAZIONI.ID_UNITA IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = (SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID= " & identificativo & ")) " _
                        & " AND SEGNALAZIONI.ID_UNITA<>" & identificativo
                Else
                    If DropDownListEdificio.SelectedValue <> "-1" Then
                        condizioni = " AND SEGNALAZIONI.ID_UNITA IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & ") "
                    Else
                        lblDataGridSegnalazioniEdificioSelezionato.Text = "Nessuna segnalazione trovata."
                    End If
                End If

                Dim condizioneTipologia As String = ""

                If cmbTipoSegnalazioneLivello0.SelectedValue <> "" Or cmbTipoSegnalazioneLivello1.SelectedValue <> "" Or cmbTipoSegnalazioneLivello2.SelectedValue <> "" Or cmbTipoSegnalazioneLivello3.SelectedValue <> "" Or cmbTipoSegnalazioneLivello4.SelectedValue <> "" Then
                    If cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello4.SelectedValue <> "" Then
                        condizioneTipologia = " and id_tipo_Segn_livello_3=" & cmbTipoSegnalazioneLivello3.SelectedValue
                    ElseIf cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello3.SelectedValue <> "" Then
                        condizioneTipologia = " and id_tipo_Segn_livello_2=" & cmbTipoSegnalazioneLivello2.SelectedValue
                    ElseIf cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello2.SelectedValue <> "" Then
                        condizioneTipologia = " and id_tipo_Segn_livello_1=" & cmbTipoSegnalazioneLivello1.SelectedValue
                    ElseIf cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello1.SelectedValue <> "" Then
                        condizioneTipologia = " and id_tipo_Segnalazione=" & cmbTipoSegnalazioneLivello0.SelectedValue
                    End If
                End If

                If (IsNumeric(identificativo) AndAlso identificativo <> "-1") Or DropDownListEdificio.SelectedValue <> "-1" Then
                    par.cmd.CommandText = " SELECT SEGNALAZIONI.ID," _
                        & " SEGNALAZIONI.ID AS NUM,ID_PERICOLO_sEGNALAZIONE AS CRITICITA,TIPO_sEGNALAZIONE.DESCRIZIONE AS TIPO,TIPO_sEGNALAZIONE.ID AS ID_TIPO, " _
                        & " REPLACE(TIPOLOGIE_GUASTI.DESCRIZIONE,'/','/ ') AS TIPO_INT,TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                        & " (COGNOME_RS ||' '|| NOME) AS RICHIEDENTE, " _
                        & " TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                        & " DESCRIZIONE_RIC AS DESCRIZIONE, " _
                        & " (select descrizione from siscom_mi.tipo_segnalazione_livello_1 where segnalazioni.id_tipo_segn_livello_1=tipo_segnalazione_livello_1.id) as tipo1," _
                        & " (select descrizione from siscom_mi.tipo_segnalazione_livello_2 where segnalazioni.id_tipo_segn_livello_2=tipo_segnalazione_livello_2.id) as tipo2," _
                        & " (select descrizione from siscom_mi.tipo_segnalazione_livello_3 where segnalazioni.id_tipo_segn_livello_3=tipo_segnalazione_livello_3.id) as tipo3," _
                        & " (select descrizione from siscom_mi.tipo_segnalazione_livello_4 where segnalazioni.id_tipo_segn_livello_4=tipo_segnalazione_livello_4.id) as tipo4, " _
                        & " TIPO_SEGNALAZIONE.ID AS TIPO,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                        & " FROM SISCOM_MI.SEGNALAZIONI,SISCOM_MI.TIPOLOGIE_GUASTI,SISCOM_MI.TAB_STATI_SEGNALAZIONI,SISCOM_MI.TIPO_sEGNALAZIONE " _
                        & " WHERE TIPOLOGIE_GUASTI.ID(+) = SEGNALAZIONI.ID_TIPOLOGIE " _
                        & " AND TAB_STATI_SEGNALAZIONI.ID = SEGNALAZIONI.ID_STATO " _
                        & " AND TIPO_SEGNALAZIONE.ID(+)=SEGNALAZIONI.ID_TIPO_SEGNALAZIONE " _
                        & " and to_date(" & Format(Now, "yyyyMMdd") & ",'yyyyMMdd') - to_date(nvl(substr(data_chiusura,1,8),'30000101'),'yyyyMMdd')<=60 " _
                        & " AND TAB_STATI_sEGNALAZIONI.ID<>2 " _
                        & " AND ID_sEGNALAZIONE_PADRE IS NULL " _
                        & " AND TIPO_SEGNALAZIONE.ID IN (1,6) " _
                        & condizioni & condizioneTipologia & " ORDER BY ID_PERICOLO_sEGNALAZIONE DESC,DATA_ORA_RICHIESTA DESC "
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable
                    da.Fill(dt)
                    da.Dispose()
                    If dt.Rows.Count > 0 Then
                        Session.Item("listaSelezioneSegnEdificio") = ""
                        lblSegnalazioniEdificiDaUnire.Text = "Nessuna segnalazione selezionata"
                        ButtonUnisciSegnalazioniEdifici.Visible = True
                    Else
                        Session.Item("listaSelezioneSegnEdificio") = ""
                        lblSegnalazioniEdificiDaUnire.Text = ""
                        ButtonUnisciSegnalazioniEdifici.Visible = False
                    End If
                    Session.Item("DataGridSegnalazEdifici") = dt
                    RadDataGridSegnalazioniEdificioSelezionato.Rebind()
                    lblDataGridSegnalazioniEdificioSelezionato.Text = ""
                    RadDataGridSegnalazioniEdificioSelezionato.Visible = True
                End If
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            Else
                Session.Item("DataGridSegnalazEdifici") = Nothing
                RadDataGridSegnalazioniEdificioSelezionato.Rebind()
                lblDataGridSegnalazioniEdificioSelezionato.Text = ""
                RadDataGridSegnalazioniEdificioSelezionato.Visible = False
                Session.Item("listaSelezioneSegnEdifici") = ""
                lblSegnalazioniEdificiDaUnire.Text = ""
                ButtonUnisciSegnalazioniEdifici.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaSegnalazioniEdificioSelezionato - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaEdifici(Optional ByVal intestatario As Boolean = False)
        Try
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND ID IN (SELECT ID_eDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ") ORDER BY DENOMINAZIONE ASC"
            Else
                If DropDownListComplessoImmobiliare.SelectedValue <> "-1" Then
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND ID_COMPLESSO=" & DropDownListComplessoImmobiliare.SelectedValue & " ORDER BY DENOMINAZIONE ASC"
                Else
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC"
                End If
            End If
            If Trim(TextBoxNomeIntestatario.Text) = "" And Trim(TextBoxCognomeIntestatario.Text) = "" Then
                If DropDownListComplessoImmobiliare.SelectedValue <> "-1" Then
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND ID_COMPLESSO=" & DropDownListComplessoImmobiliare.SelectedValue & " ORDER BY DENOMINAZIONE ASC"
                Else
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC"
                End If
            End If
            par.caricaComboTelerik(query, DropDownListEdificio, "ID", "DENOMINAZIONE", True)
            If DropDownListEdificio.Items.Count = 2 Then
                If Not IsNothing(DropDownListEdificio.Items.FindItemByValue("-1")) Then
                    DropDownListEdificio.Items.Remove(DropDownListEdificio.Items.FindItemByValue("-1"))
                End If
            ElseIf DropDownListEdificio.Items.Count = 1 Then
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1", DropDownListComplessoImmobiliare, "ID", "DENOMINAZIONE", True)
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.EDIFICI WHERE ID<>1", DropDownListEdificio, "ID", "DENOMINAZIONE", True)
                par.modalDialogMessage("Nuova segnalazione", "Non è possibile caricare i dati identificativi dell\'unità immobiliare selezionata.", Page, "info")
            End If
            DropDownListScala.Items.Clear()
            DropDownListScala.Enabled = False
            DropDownListPiano.Items.Clear()
            DropDownListPiano.Enabled = False
            DropDownListInterno.Items.Clear()
            DropDownListInterno.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaEdifici - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaEdificiChiamante(Optional ByVal intestatario As Boolean = False)
        Try
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND ID IN (SELECT ID_eDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ") ORDER BY DENOMINAZIONE ASC"
            Else
                If DropDownListComplessoChiamante.SelectedValue <> "-1" Then
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND ID_COMPLESSO=" & DropDownListComplessoChiamante.SelectedValue & " ORDER BY DENOMINAZIONE ASC"
                Else
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC"
                End If
            End If
            If Trim(TextBoxNomeIntestatario.Text) = "" Then
                If DropDownListComplessoChiamante.SelectedValue <> "-1" Then
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND ID_COMPLESSO=" & DropDownListComplessoChiamante.SelectedValue & " ORDER BY DENOMINAZIONE ASC"
                Else
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC"
                End If
            End If
            par.caricaComboTelerik(query, DropDownListEdificioChiamante, "ID", "DENOMINAZIONE", True)
            If DropDownListEdificioChiamante.Items.Count = 2 Then
                If Not IsNothing(DropDownListEdificioChiamante.Items.FindItemByValue("-1")) Then
                    DropDownListEdificioChiamante.Items.Remove(DropDownListEdificioChiamante.Items.FindItemByValue("-1"))
                End If
            ElseIf DropDownListEdificioChiamante.Items.Count = 1 Then
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1", DropDownListComplessoChiamante, "ID", "DENOMINAZIONE", True)
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.EDIFICI WHERE ID<>1", DropDownListEdificioChiamante, "ID", "DENOMINAZIONE", True)
                par.modalDialogMessage("Nuova segnalazione", "Non è possibile caricare i dati identificativi dell\'unità immobiliare selezionata.", Page, "info")
            End If
            DropDownListScalaChiamante.Items.Clear()
            DropDownListScalaChiamante.Enabled = False
            DropDownListPianoChiamante.Items.Clear()
            DropDownListPianoChiamante.Enabled = False
            DropDownListInternoChiamante.Items.Clear()
            DropDownListInternoChiamante.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaEdificiChiamante - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaScale(Optional ByVal intestatario As Boolean = False)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                query = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ") ORDER BY DESCRIZIONE ASC"
            Else
                query = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificio.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
            End If
            par.caricaComboTelerik(query, DropDownListScala, "ID", "DESCRIZIONE", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If DropDownListScala.Items.Count = 2 Then
                If Not IsNothing(DropDownListScala.Items.FindItemByValue("-1")) Then
                    DropDownListScala.Items.Remove(DropDownListScala.Items.FindItemByValue("-1"))
                End If
            End If
            DropDownListScala.Enabled = True
            DropDownListPiano.Items.Clear()
            DropDownListPiano.Enabled = False
            DropDownListInterno.Items.Clear()
            DropDownListInterno.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaScale - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaScaleChiamante(Optional ByVal intestatario As Boolean = False)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                query = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ") ORDER BY DESCRIZIONE ASC"
            Else
                query = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificioChiamante.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
            End If
            par.caricaComboTelerik(query, DropDownListScalaChiamante, "ID", "DESCRIZIONE", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If DropDownListScalaChiamante.Items.Count = 2 Then
                If Not IsNothing(DropDownListScalaChiamante.Items.FindItemByValue("-1")) Then
                    DropDownListScalaChiamante.Items.Remove(DropDownListScalaChiamante.Items.FindItemByValue("-1"))
                End If
            End If
            DropDownListScalaChiamante.Enabled = True
            DropDownListPianoChiamante.Items.Clear()
            DropDownListPianoChiamante.Enabled = False
            DropDownListInternoChiamante.Items.Clear()
            DropDownListInternoChiamante.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaScaleChiamante - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaInterni(Optional ByVal intestatario As Boolean = False)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & identificativo & " ORDER BY INTERNO ASC"
            Else
                If DropDownListScala.SelectedValue <> "-1" And DropDownListPiano.SelectedValue <> "-1" Then
                    query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & " AND ID_SCALA = " & DropDownListScala.SelectedValue & " AND COD_TIPO_LIVELLO_PIANO='" & DropDownListPiano.SelectedValue & "' ORDER BY INTERNO ASC"
                Else
                    query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & " ORDER BY INTERNO ASC"
                End If
            End If
            par.caricaComboTelerik(query, DropDownListInterno, "INTERNO", "INTERNO", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If DropDownListInterno.Items.Count = 2 Then
                If Not IsNothing(DropDownListInterno.Items.FindItemByValue("-1")) Then
                    DropDownListInterno.Items.Remove(DropDownListInterno.Items.FindItemByValue("-1"))
                End If
            End If
            DropDownListInterno.Enabled = True
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaInterni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaInterniChiamante(Optional ByVal intestatario As Boolean = False)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & identificativo & " ORDER BY INTERNO ASC"
            Else
                If DropDownListScalaChiamante.SelectedValue <> "-1" And DropDownListPianoChiamante.SelectedValue <> "-1" Then
                    query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificioChiamante.SelectedValue & " AND ID_SCALA = " & DropDownListScalaChiamante.SelectedValue & " AND COD_TIPO_LIVELLO_PIANO='" & DropDownListPianoChiamante.SelectedValue & "' ORDER BY INTERNO ASC"
                Else
                    query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificioChiamante.SelectedValue & " ORDER BY INTERNO ASC"
                End If
            End If
            par.caricaComboTelerik(query, DropDownListInternoChiamante, "INTERNO", "INTERNO", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If DropDownListInternoChiamante.Items.Count = 2 Then
                If Not IsNothing(DropDownListInternoChiamante.Items.FindItemByValue("-1")) Then
                    DropDownListInternoChiamante.Items.Remove(DropDownListInternoChiamante.Items.FindItemByValue("-1"))
                End If
            End If
            DropDownListInternoChiamante.Enabled = True
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaInterniChiamante - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaPiano(Optional ByVal intestatario As Boolean = False)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ") ORDER BY DESCRIZIONE ASC"
            Else
                If DropDownListScala.SelectedValue <> "-1" Then
                    query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificio.SelectedValue & " AND ID_sCALA=" & DropDownListScala.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
                Else
                    query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificio.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
                End If
            End If
            par.caricaComboTelerik(query, DropDownListPiano, "COD", "DESCRIZIONE", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If DropDownListPiano.Items.Count = 2 Then
                If Not IsNothing(DropDownListPiano.Items.FindItemByValue("-1")) Then
                    DropDownListPiano.Items.Remove(DropDownListPiano.Items.FindItemByValue("-1"))
                End If
            End If
            DropDownListPiano.Enabled = True
            DropDownListInterno.Items.Clear()
            DropDownListInterno.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaPiano - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaPianoChiamante(Optional ByVal intestatario As Boolean = False)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ") ORDER BY DESCRIZIONE ASC"
            Else
                If DropDownListScalaChiamante.SelectedValue <> "-1" Then
                    query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificioChiamante.SelectedValue & " AND ID_sCALA=" & DropDownListScalaChiamante.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
                Else
                    query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificioChiamante.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
                End If
            End If
            par.caricaComboTelerik(query, DropDownListPianoChiamante, "COD", "DESCRIZIONE", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If DropDownListPianoChiamante.Items.Count = 2 Then
                If Not IsNothing(DropDownListPianoChiamante.Items.FindItemByValue("-1")) Then
                    DropDownListPianoChiamante.Items.Remove(DropDownListPianoChiamante.Items.FindItemByValue("-1"))
                End If
            End If
            DropDownListPianoChiamante.Enabled = True
            DropDownListInternoChiamante.Items.Clear()
            DropDownListInternoChiamante.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaPianoChiamante - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaSedeTerritoriale()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            If IsNumeric(DropDownListEdificio.SelectedValue) AndAlso DropDownListEdificio.SelectedValue <> "-1" Then
                Dim query As String = "SELECT ID, NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID = (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID=" & DropDownListEdificio.SelectedValue & ")) ORDER BY NOME ASC"
                par.caricaComboTelerik(query, DropDownListSedeTerritoriale, "ID", "NOME", True)

                If DropDownListSedeTerritoriale.Items.Count = 2 Then
                    If Not IsNothing(DropDownListSedeTerritoriale.Items.FindItemByValue("-1")) Then
                        DropDownListSedeTerritoriale.Items.Remove(DropDownListSedeTerritoriale.Items.FindItemByValue("-1"))
                    End If
                End If
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaSedeTerritoriale - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DropDownListEdificio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListEdificio.SelectedIndexChanged
        If DropDownListEdificio.SelectedValue <> "-1" Then
            panelDann.Visible = True
        Else
            panelDann.Visible = False
            TextBoxDanneggiante.Text = ""
            TextBoxDanneggiato.Text = ""
            Danneggiante.Value = ""
            Danneggiato.Value = ""
        End If
        CaricaScale()
        CaricaSedeTerritoriale()
        ControllaCondominio()
        ControllaMoroso()
        ControllaNumeriUtili()
    End Sub

    Protected Sub RadDataGridSegnalazioniUnitaSelezionata_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadDataGridSegnalazioniUnitaSelezionata.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadDataGridSegnalazioniUnitaSelezionata.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('idSegnalazioneSelezionataUnita').value='" & dataItem("ID").Text & "';")
            dataItem.Attributes.Add("onDblclick", "ApriSegnalazioneUnita();")
        End If
    End Sub


    Protected Sub RadDataGridSegnalazioniUnitaSelezionata_PreRender(sender As Object, e As System.EventArgs) Handles RadDataGridSegnalazioniUnitaSelezionata.PreRender
        For Each dataItem As GridDataItem In RadDataGridSegnalazioniUnitaSelezionata.Items
            If dataItem("ID_TIPO").Text = "1" Or dataItem("ID_TIPO").Text = "6" Then
                Select Case dataItem("CRITICITA").Text
                    Case "1"
                        dataItem("CRITICITA").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-white-128.png"
                        dataItem("CRITICITA").Controls.Add(img)
                    Case "2"
                        dataItem("CRITICITA").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-green-128.png"
                        dataItem("CRITICITA").Controls.Add(img)
                    Case "3"
                        dataItem("CRITICITA").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-yellow-128.png"
                        dataItem("CRITICITA").Controls.Add(img)
                    Case "4"
                        dataItem("CRITICITA").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-red-128.png"
                        dataItem("CRITICITA").Controls.Add(img)
                    Case "0"
                        dataItem("CRITICITA").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-blue-128.png"
                        dataItem("CRITICITA").Controls.Add(img)
                    Case Else
                End Select
            End If
        Next
    End Sub

    'Protected Sub DataGridSegnalazioniUnitaSelezionata_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridSegnalazioniUnitaSelezionata.ItemDataBound
    '    Try
    '        If e.Item.ItemType = ListItemType.Item Then
    '            '---------------------------------------------------         
    '            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '            '---------------------------------------------------  
    '            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white';}")
    '            e.Item.Attributes.Add("onclick", "if (SelezionatoSegnalazioniUnita) {SelezionatoSegnalazioniUnita.style.backgroundColor='';}SelezionatoSegnalazioniUnita=this;this.style.backgroundColor='orange';document.getElementById('idSegnalazioneSelezionataUnita').value='" & e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "ID")).Text & "';")
    '            e.Item.Attributes.Add("onDblclick", "ApriSegnalazioneUnita();")
    '        End If
    '        If e.Item.ItemType = ListItemType.AlternatingItem Then
    '            '---------------------------------------------------         
    '            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '            '---------------------------------------------------         
    '            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro';}")
    '            e.Item.Attributes.Add("onclick", "if (SelezionatoSegnalazioniUnita) {SelezionatoSegnalazioniUnita.style.backgroundColor='';}SelezionatoSegnalazioniUnita=this;this.style.backgroundColor='orange';document.getElementById('idSegnalazioneSelezionataUnita').value='" & e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "ID")).Text & "';")
    '            e.Item.Attributes.Add("onDblclick", "ApriSegnalazioneUnita();")
    '        End If
    '        If e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "ID_TIPO")).Text = "1" Or e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "ID_TIPO")).Text = "6" Then
    '            Select Case e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "CRITICITA")).Text
    '                Case "1"
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                        & "<tr><td><img src=""Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td></<tr></table>"
    '                Case "2"
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                        & "<tr><td><img src=""Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td></<tr></table>"
    '                Case "3"
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                        & "<tr><td><img src=""Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td></<tr></table>"
    '                Case "4"
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                        & "<tr><td><img src=""Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td></<tr></table>"
    '                Case "0"
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                        & "<tr><td><img src=""Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td></<tr></table>"
    '                Case Else
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniUnitaSelezionata, "CRITICITA")).Text = ""
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridSegnalazioniUnitaSelezionata_ItemDataBound - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    'Protected Sub DataGridSegnalazioniEdificioSelezionato_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridSegnalazioniEdificioSelezionato.ItemDataBound
    '    Try
    '        If e.Item.ItemType = ListItemType.Item Then
    '            '---------------------------------------------------         
    '            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '            '---------------------------------------------------  
    '            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white';}")
    '            e.Item.Attributes.Add("onclick", "if (SelezionatoSegnalazioniEdificio) {SelezionatoSegnalazioniEdificio.style.backgroundColor='';}SelezionatoSegnalazioniEdificio=this;this.style.backgroundColor='orange';document.getElementById('idSegnalazioneSelezionataEdificio').value='" & e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "ID")).Text & "';")
    '            e.Item.Attributes.Add("onDblclick", "ApriSegnalazioneEdificio();")
    '        End If
    '        If e.Item.ItemType = ListItemType.AlternatingItem Then
    '            '---------------------------------------------------         
    '            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '            '---------------------------------------------------         
    '            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro';}")
    '            e.Item.Attributes.Add("onclick", "if (SelezionatoSegnalazioniEdificio) {SelezionatoSegnalazioniEdificio.style.backgroundColor='';}SelezionatoSegnalazioniEdificio=this;this.style.backgroundColor='orange';document.getElementById('idSegnalazioneSelezionataEdificio').value='" & e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "ID")).Text & "';")
    '            e.Item.Attributes.Add("onDblclick", "ApriSegnalazioneEdificio();")
    '        End If
    '        If e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "ID_TIPO")).Text = "1" Or e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "ID_TIPO")).Text = "6" Then
    '            Select Case e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "CRITICITA")).Text
    '                Case "1"
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                        & "<tr><td><img src=""Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td></<tr></table>"
    '                Case "2"
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                        & "<tr><td><img src=""Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td></<tr></table>"
    '                Case "3"
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                        & "<tr><td><img src=""Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td></<tr></table>"
    '                Case "4"
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                        & "<tr><td><img src=""Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td></<tr></table>"
    '                Case "0"
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                        & "<tr><td><img src=""Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td></<tr></table>"
    '                Case Else
    '                    e.Item.Cells(par.IndDGC(DataGridSegnalazioniEdificioSelezionato, "CRITICITA")).Text = ""
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridSegnalazioniEdificioSelezionato_ItemDataBound - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    Protected Sub btnSvuota_Click(sender As Object, e As System.EventArgs) Handles btnSvuota.Click
        Svuota()
    End Sub
    Private Sub Svuota(Optional SvuotaSelezione As Boolean = True, Optional DaChiamante As Boolean = True, Optional soloChiamante As Boolean = False)
        Try
            If soloChiamante = False Then
                If SvuotaSelezione Then
                    idSelectedChiamante.Value = ""
                    idAnagraficaChiamante.Value = ""
                    idSelectedIntestatario.Value = ""
                    idAnagraficaIntestatario.Value = ""
                End If
                If DaChiamante Then
                    TextBoxCognomeChiamante.Text = ""
                    TextBoxNomeChiamante.Text = ""
                    TextBoxTelefono1Chiamante.Text = ""
                    TextBoxTelefono2Chiamante.Text = ""
                    TextBoxEmailChiamante.Text = ""
                    flCustode.Value = "0"
                End If


                TextBoxDanneggiante.Text = ""
                TextBoxDanneggiato.Text = ""

                DropDownListTipologia.Items.Clear()
                cmbTipoSegnalazioneLivello0.Items.Clear()
                cmbTipoSegnalazioneLivello1.Items.Clear()
                cmbTipoSegnalazioneLivello2.Items.Clear()
                cmbTipoSegnalazioneLivello3.Items.Clear()
                cmbTipoSegnalazioneLivello4.Items.Clear()

                CaricaTutteTipologie()
                CaricaTipologieLivello0()

                TextBoxCognomeIntestatario.Text = ""
                TextBoxNomeIntestatario.Text = ""
                TextBoxIndirizzoIntestatario.Text = ""
                TextBoxCodiceUnitaImmobiliare.Text = ""
                TextBoxCodiceContrattoIntestatario.Text = ""
                TextBoxCodiceUnitaImmobiliare.Text = ""
                txtContrattoChiamante.Text = ""
                CaricaComplessi()
                CaricaComplessiChiamante()
                CaricaEdifici()
                CaricaEdificiChiamante()

                DropDownListScala.Items.Clear()
                DropDownListScala.Enabled = False
                DropDownListInterno.Items.Clear()
                DropDownListInterno.Enabled = False
                DropDownListPiano.Items.Clear()
                DropDownListPiano.Enabled = False
                DropDownListSedeTerritoriale.Items.Clear()
                DropDownListSedeTerritoriale.Enabled = False
                lblDataGridSegnalazioniUnitaSelezionata.Text = "Nessuna segnalazione trovata."
                lblDataGridSegnalazioniEdificioSelezionato.Text = "Nessuna segnalazione trovata."
                RadDataGridSegnalazioniEdificioSelezionato.Visible = False
                RadDataGridSegnalazioniUnitaSelezionata.Visible = False
                TextBoxCognomeChiamante.Enabled = True
                TextBoxNomeChiamante.Enabled = True
                lblMorosoSiNo.Text = ""
                lblAbusivoSiNo.Text = ""
            Else
                If SvuotaSelezione Then
                    idSelectedChiamante.Value = ""
                    idAnagraficaChiamante.Value = ""
                End If
                If DaChiamante Then
                    TextBoxCognomeChiamante.Text = ""
                    TextBoxNomeChiamante.Text = ""
                    TextBoxTelefono1Chiamante.Text = ""
                    TextBoxTelefono2Chiamante.Text = ""
                    TextBoxEmailChiamante.Text = ""
                End If
            End If
            If flCustode.Value = "1" Then
                ButtonNuovaSegnalazioneCustode.Visible = True
            Else
                ButtonNuovaSegnalazioneCustode.Visible = False
            End If
            cmbTipologiaSegnalante.ClearSelection()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - Svuota - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello0.SelectedIndexChanged
        DropDownListTipologia.SelectedValue = "-1"
        If cmbTipoSegnalazioneLivello0.SelectedValue = "0" Then
            PanelElencoDocumentiRichiesti.Visible = True
        Else
            PanelElencoDocumentiRichiesti.Visible = False
        End If
        If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
            PanelUrgenzaCriticita.Visible = True
        Else
            PanelUrgenzaCriticita.Visible = False
        End If
        CaricaNumeriUtili()
        CaricaTipologieLivello1()
        caricaUrgenzaPredefinita()
        SvuotaElencoDocumentiDaPortare()
        CaricaSegnalazioniEdificioSelezionato(True)
        CaricaSegnalazioniUnitaSelezionata(True)
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello1.SelectedIndexChanged
        DropDownListTipologia.SelectedValue = "-1"
        CaricaTipologieLivello2()
        caricaUrgenzaPredefinita()
        CaricaListaDocumentiDaPortare()
        CaricaSegnalazioniEdificioSelezionato(True)
        CaricaSegnalazioniUnitaSelezionata(True)
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello2.SelectedIndexChanged
        DropDownListTipologia.SelectedValue = "-1"
        CaricaTipologieLivello3()
        caricaUrgenzaPredefinita()
        CaricaListaDocumentiDaPortare()
        CaricaSegnalazioniEdificioSelezionato(True)
        CaricaSegnalazioniUnitaSelezionata(True)
    End Sub
    Private Sub CaricaTipologieLivello0()
        Try
            Dim query As String = ""
            If flCustode.Value = "1" Then

                query = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id=1  ORDER BY ID"
            Else
                query = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id NOT IN (6,7) ORDER BY ID"
            End If
            'If flCondominio.Value = "1" Then
            '    query = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id=3  ORDER BY ID"
            'End If
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello0, "ID", "DESCRIZIONE", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            cmbTipoSegnalazioneLivello1.Items.Clear()
            cmbTipoSegnalazioneLivello2.Items.Clear()
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
            cmbTipoSegnalazioneLivello1.Visible = False
            cmbTipoSegnalazioneLivello2.Visible = False
            cmbTipoSegnalazioneLivello3.Visible = False
            cmbTipoSegnalazioneLivello4.Visible = False
            lblLivello1.Visible = False
            lblLivello2.Visible = False
            lblLivello3.Visible = False
            lblLivello4.Visible = False


        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaTipologieLivello0 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello1()
        Try
            'Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE=" & cmbTipoSegnalazioneLivello0.SelectedValue & " ORDER BY DESCRIZIONE"
            Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE=" & cmbTipoSegnalazioneLivello0.SelectedValue & " ORDER BY DESCRIZIONE"
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello1, "ID", "DESCRIZIONE", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If cmbTipoSegnalazioneLivello1.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello1.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello1.Items.Remove(cmbTipoSegnalazioneLivello1.Items.FindItemByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello2.Items.Clear()
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
            cmbTipoSegnalazioneLivello1.Visible = True
            cmbTipoSegnalazioneLivello2.Visible = False
            cmbTipoSegnalazioneLivello3.Visible = False
            cmbTipoSegnalazioneLivello4.Visible = False
            lblLivello1.Visible = True
            lblLivello2.Visible = False
            lblLivello3.Visible = False
            lblLivello4.Visible = False
            If cmbTipoSegnalazioneLivello1.Items.Count = 1 And cmbTipoSegnalazioneLivello1.SelectedValue = "-1" Then
                cmbTipoSegnalazioneLivello1.Visible = False
                lblLivello1.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaTipologieLivello1 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello2()
        Try
            'Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue & " ORDER BY DESCRIZIONE"
            Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE_LIVELLO_1,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue & " ORDER BY DESCRIZIONE"
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello2, "ID", "DESCRIZIONE", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If cmbTipoSegnalazioneLivello2.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello2.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello2.Items.Remove(cmbTipoSegnalazioneLivello2.Items.FindItemByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
            cmbTipoSegnalazioneLivello2.Visible = True
            cmbTipoSegnalazioneLivello3.Visible = False
            cmbTipoSegnalazioneLivello4.Visible = False
            lblLivello2.Visible = True
            lblLivello3.Visible = False
            lblLivello4.Visible = False
            If cmbTipoSegnalazioneLivello2.Items.Count = 1 And cmbTipoSegnalazioneLivello2.SelectedValue = "-1" Then
                cmbTipoSegnalazioneLivello2.Visible = False
                lblLivello2.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaTipologieLivello2 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello3()
        Try
            'Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue & " ORDER BY DESCRIZIONE"
            Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE_LIVELLO_2,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue & " ORDER BY DESCRIZIONE"
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello3, "ID", "DESCRIZIONE", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If cmbTipoSegnalazioneLivello3.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello3.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello3.Items.Remove(cmbTipoSegnalazioneLivello3.Items.FindItemByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello3.Visible = True
            cmbTipoSegnalazioneLivello4.Visible = False
            lblLivello3.Visible = True
            lblLivello4.Visible = False
            If cmbTipoSegnalazioneLivello3.Items.Count = 1 And cmbTipoSegnalazioneLivello3.SelectedValue = "-1" Then
                cmbTipoSegnalazioneLivello3.Visible = False
                lblLivello3.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaTipologieLivello3 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello4()
        Try
            'Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue & " ORDER BY DESCRIZIONE"
            Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE_LIVELLO_3,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue & " ORDER BY DESCRIZIONE"
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello4, "ID", "DESCRIZIONE", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If cmbTipoSegnalazioneLivello4.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello4.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello4.Items.Remove(cmbTipoSegnalazioneLivello4.Items.FindItemByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello4.Visible = True
            lblLivello4.Visible = True
            If cmbTipoSegnalazioneLivello4.Items.Count = 1 And cmbTipoSegnalazioneLivello4.SelectedValue = "-1" Then
                cmbTipoSegnalazioneLivello4.Visible = False
                lblLivello4.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaTipologieLivello4 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaListaDocumentiDaPortare()

        Try
            If cmbTipoSegnalazioneLivello0.SelectedValue = "0" Then
                Dim query As String = ""
                If cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello2.SelectedValue <> "" Then
                    query = "SELECT DESCRIZIONE AS DOCUMENTI_RICHIESTI FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI," _
                        & "SISCOM_MI.TIPOLOGIE_DOCUMENTI WHERE SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI.ID_TIPOLOGIA_DOCUMENTO=TIPOLOGIE_DOCUMENTI.ID " _
                        & " AND ID_TIPO_SEGN_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue _
                        & " AND ID_TIPO_SEGN_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue _
                        & " ORDER BY DESCRIZIONE"
                Else
                    If cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello1.SelectedValue <> "" Then
                        query = "SELECT DESCRIZIONE AS DOCUMENTI_RICHIESTI FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI," _
                            & "SISCOM_MI.TIPOLOGIE_DOCUMENTI WHERE SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI.ID_TIPOLOGIA_DOCUMENTO=TIPOLOGIE_DOCUMENTI.ID " _
                            & "AND ID_TIPO_SEGN_LIVELLO_2 IS NULL " _
                            & "AND ID_TIPO_SEGN_LIVELLO_1= " & cmbTipoSegnalazioneLivello1.SelectedValue _
                            & " ORDER BY DESCRIZIONE"
                    End If
                End If
                If query <> "" Then
                    Dim connAperta As Boolean = False
                    If connData.Connessione.State = Data.ConnectionState.Closed Then
                        connData.apri(False)
                        connAperta = True
                    End If
                    par.cmd.CommandText = query
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable
                    da.Fill(dt)
                    da.Dispose()
                    If dt.Rows.Count > 0 Then
                        DataGridDocumentiRichiesti.DataSource = dt
                        DataGridDocumentiRichiesti.DataBind()
                        DataGridDocumentiRichiesti.Visible = True
                        lblDocumentiRichiesti.Visible = False
                    Else
                        DataGridDocumentiRichiesti.Visible = False
                        lblDocumentiRichiesti.Visible = True
                    End If
                    If connAperta = True Then
                        connData.chiudi(False)
                    End If
                End If
                PanelElencoDocumentiRichiesti.Visible = True
            Else
                PanelElencoDocumentiRichiesti.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaListaDocumentiDaPortare - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SvuotaElencoDocumentiDaPortare()
        DataGridDocumentiRichiesti.Visible = False
    End Sub
    Private Sub ControllaCondominio(Optional ByVal intestatario As Boolean = False)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                lblInCondominio.Text = "Unità immobiliare in condominio"
                par.cmd.CommandText = "SELECT ID_CONDOMINIO FROM SISCOM_MI.COND_UI WHERE ID_UI=" & identificativo
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    idCondominio.Value = par.IfNull(lettore("ID_CONDOMINIO"), "")
                    lblInCondominioSiNo.Text = "Sì"
                Else
                    lblInCondominioSiNo.Text = "No"
                End If
                lettore.Close()
            Else
                If DropDownListEdificio.SelectedValue <> "-1" Then
                    Dim condizioneUnita As String = ""
                    condizioneUnita &= " ID_EDIFICIO=" & DropDownListEdificio.SelectedValue
                    If DropDownListScala.Items.Count > 1 AndAlso DropDownListScala.SelectedValue <> "-1" Then
                        condizioneUnita &= " AND ID_SCALA=" & DropDownListScala.SelectedValue
                    End If
                    If DropDownListInterno.Items.Count > 1 AndAlso DropDownListInterno.SelectedValue <> "-1" Then
                        condizioneUnita &= " AND INTERNO='" & DropDownListInterno.SelectedValue & "'"
                    End If
                    If DropDownListPiano.Items.Count > 1 AndAlso DropDownListPiano.SelectedValue <> "-1" Then
                        condizioneUnita &= " AND COD_TIPO_LIVELLO_PIANO='" & DropDownListPiano.SelectedValue & "'"
                    End If
                    Dim numeroUnita As Integer = 0
                    Dim lettoreNumeroUnita As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE " & condizioneUnita
                    lettoreNumeroUnita = par.cmd.ExecuteReader
                    If lettoreNumeroUnita.Read Then
                        numeroUnita = par.IfNull(lettoreNumeroUnita(0), 0)
                    Else
                        numeroUnita = 0
                    End If
                    lettoreNumeroUnita.Close()
                    If numeroUnita = 1 Then
                        par.cmd.CommandText = "SELECT ID_CONDOMINIO FROM SISCOM_MI.COND_UI WHERE ID_UI IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE " & condizioneUnita & " )"
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            idCondominio.Value = par.IfNull(lettore("ID_CONDOMINIO"), "")
                            lblInCondominio.Text = "Unità immobiliare in condominio"
                            lblInCondominioSiNo.Text = "Sì"
                        Else
                            lblInCondominio.Text = "Unità immobiliare in condominio"
                            lblInCondominioSiNo.Text = "No"
                        End If
                        lettore.Close()
                    Else
                        lblInCondominio.Text = "Edificio in condominio"
                        par.cmd.CommandText = "SELECT ID_CONDOMINIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO=" & DropDownListEdificio.SelectedValue
                        Dim lettoreEd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettoreEd.Read Then
                            idCondominio.Value = par.IfNull(lettoreEd("ID_CONDOMINIO"), "")
                            lblInCondominioSiNo.Text = "Sì"
                        Else
                            lblInCondominioSiNo.Text = "No"
                        End If
                        lettoreEd.Close()
                    End If
                Else
                    lblInCondominio.Text = ""
                    lblInCondominioSiNo.Text = ""
                End If
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
            If lblInCondominioSiNo.Text = "Sì" Then
                flCondominio.Value = "1"
                'CaricaTipologieLivello0()
                'cmbTipoSegnalazioneLivello0.SelectedValue = 3
                'cmbTipoSegnalazioneLivello0.Enabled = False
                'CaricaTutteTipologie(1)
                'CaricaTipologieLivello1()
                'CaricaTipologieLivello2()
                'CaricaTipologieLivello3()
                'CaricaTipologieLivello4()
                ControllaAmministratore()
            Else
                flCondominio.Value = "0"
                'CaricaTipologieLivello0()
                'cmbTipoSegnalazioneLivello0.ClearSelection()
                'cmbTipoSegnalazioneLivello0.Enabled = True
                'CaricaTutteTipologie()
                'CaricaTipologieLivello1()
                'CaricaTipologieLivello2()
                'CaricaTipologieLivello3()
                'CaricaTipologieLivello4()
                panelAmministratore.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - controllaCondominio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub ControllaMoroso()
        If IsNumeric(idContrattoIntestatario.Value) AndAlso idContrattoIntestatario.Value > 0 Then
            Try
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                par.cmd.CommandText = "SELECT SISCOM_MI.GETSALDOCONTRATTO(" & idContrattoIntestatario.Value & ") FROM DUAL"
                Dim morosita As Decimal = 0
                morosita = par.IfNull(par.cmd.ExecuteScalar, 0)
                If morosita <> 0 Then
                    lblMorosoSiNo.Text = "€ " & Format(morosita, "#,#00.00")
                    lblMorosoSiNo.Visible = True
                    lblMoroso.Visible = True
                Else
                    lblMorosoSiNo.Text = "No"
                    lblMorosoSiNo.Visible = True
                    lblMoroso.Visible = True
                End If
                Dim tipoContratto As String = ""
                par.cmd.CommandText = "SELECT COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContrattoIntestatario.Value
                tipoContratto = par.IfNull(par.cmd.ExecuteScalar, "")
                If tipoContratto = "NONE" Then
                    lblAbusivoSiNo.Text = "Sì"
                    lblAbusivoSiNo.Visible = True
                    lblAbusivo.Visible = True
                Else
                    lblAbusivoSiNo.Text = "No"
                    lblAbusivoSiNo.Visible = True
                    lblAbusivo.Visible = True
                End If
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ControllaMoroso - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
    End Sub
    Protected Sub DropDownListScala_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListScala.SelectedIndexChanged
        CaricaPiano()
        ControllaCondominio()
        ControllaMoroso()
    End Sub
    Protected Sub DropDownListInterno_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListInterno.SelectedIndexChanged
        ControllaCondominio()
        ControllaMoroso()
    End Sub
    Protected Sub DropDownListPiano_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListPiano.SelectedIndexChanged
        CaricaInterni()
        ControllaCondominio()
        ControllaMoroso()
    End Sub
    Protected Sub DropDownListComplessoImmobiliare_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListComplessoImmobiliare.SelectedIndexChanged
        CaricaEdifici()
        If DropDownListEdificio.SelectedValue <> "-1" Then
            panelDann.Visible = True
        Else
            panelDann.Visible = False
            TextBoxDanneggiante.Text = ""
            TextBoxDanneggiato.Text = ""
            Danneggiante.Value = ""
            Danneggiato.Value = ""
        End If
        CaricaScale()
        CaricaPiano()
        CaricaInterni()
        CaricaSedeTerritoriale()
        ControllaCondominio()
        ControllaMoroso()
    End Sub
    Private Sub CaricaComplessi(Optional ByVal intestatario As Boolean = False)
        Try
            Dim condizioneCustode As String = ""
            If flCustode.Value = "1" AndAlso IsNumeric(idAnagraficaChiamante.Value) AndAlso idAnagraficaChiamante.Value <> "-1" Then
                condizioneCustode = " AND ID IN (SELECT ID_cOMPLESSO FROM SISCOM_MI.PORTIERATO WHERE ID_CUSTODE=" & idAnagraficaChiamante.Value & ")"
            End If
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 AND ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID IN (SELECT ID_eDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ")) " & condizioneCustode & " ORDER BY DENOMINAZIONE ASC"
            Else
                query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneCustode & " ORDER BY DENOMINAZIONE ASC"
            End If
            par.caricaComboTelerik(query, DropDownListComplessoImmobiliare, "ID", "DENOMINAZIONE", True)
            If DropDownListComplessoImmobiliare.Items.Count = 2 Then
                If Not IsNothing(DropDownListComplessoImmobiliare.Items.FindItemByValue("-1")) Then
                    DropDownListComplessoImmobiliare.Items.Remove(DropDownListComplessoImmobiliare.Items.FindItemByValue("-1"))
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaComplessi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaComplessiChiamante(Optional ByVal intestatario As Boolean = False)
        Try
            Dim condizioneCustode As String = ""
            If flCustode.Value = "1" AndAlso IsNumeric(idAnagraficaChiamante.Value) AndAlso idAnagraficaChiamante.Value <> "-1" Then
                condizioneCustode = " AND ID IN (SELECT ID_cOMPLESSO FROM SISCOM_MI.PORTIERATO WHERE ID_CUSTODE=" & idAnagraficaChiamante.Value & ")"
            End If
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 AND ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID IN (SELECT ID_eDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ")) " & condizioneCustode & " ORDER BY DENOMINAZIONE ASC"
            Else
                query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneCustode & " ORDER BY DENOMINAZIONE ASC"
            End If
            par.caricaComboTelerik(query, DropDownListComplessoChiamante, "ID", "DENOMINAZIONE", True)
            If DropDownListComplessoChiamante.Items.Count = 2 Then
                If Not IsNothing(DropDownListComplessoChiamante.Items.FindItemByValue("-1")) Then
                    DropDownListComplessoChiamante.Items.Remove(DropDownListComplessoChiamante.Items.FindItemByValue("-1"))
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaComplessiChiamante - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub ControllaAmministratore()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            If IsNumeric(idCondominio.Value) Then
                par.cmd.CommandText = " SELECT TITOLO || ' ' || COGNOME || ' ' || NOME AS NOME, " _
                    & " TIPO_INDIRIZZO " _
                    & " || ' ' " _
                    & " || INDIRIZZO " _
                    & " || ' ' " _
                    & " || CIVICO " _
                    & " || ', ' " _
                    & " || CAP " _
                    & " || ' ' " _
                    & " || (SELECT NOME || ' (' || SIGLA || ')' " _
                    & " FROM SEPA.COMUNI_NAZIONI " _
                    & " WHERE COMUNI_NAZIONI.COD = COD_COMUNE) " _
                    & " AS INDIRIZZO, " _
                    & " TEL_1, " _
                    & " TEL_2, " _
                    & " CELL, " _
                    & " FAX, " _
                    & " EMAIL, " _
                    & " NOTE, " _
                    & " PARTITA_IVA " _
                    & " FROM siscom_mi.cond_amministrazione, siscom_mi.cond_amministratori " _
                    & " WHERE     cond_amministratori.id = cond_amministrazione.id_amministratore " _
                    & " AND id_condominio = " & idCondominio.Value _
                    & " AND DATA_FINE IS NULL "
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblAmministratore.Text = par.IfNull(lettore("NOME"), "")
                    lblIndirizzo.Text = par.IfNull(lettore("INDIRIZZO"), "")
                    lblTelefono1.Text = par.IfNull(lettore("TEL_1"), "")
                    lblTelefono2.Text = par.IfNull(lettore("TEL_2"), "")
                    lblTelefono3.Text = par.IfNull(lettore("CELL"), "")
                    lblFax.Text = par.IfNull(lettore("FAX"), "")
                    lblEmail.Text = par.IfNull(lettore("EMAIL"), "")
                    lblNote.Text = par.IfNull(lettore("NOTE"), "")
                    lblPartitaIVA.Text = par.IfNull(lettore("PARTITA_IVA"), "")
                    panelAmministratore.Visible = True
                Else
                    panelAmministratore.Visible = False
                End If
                lettore.Close()
            Else
                panelAmministratore.Visible = False
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ControllaAmministratore - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnIndietro2_Click(sender As Object, e As System.EventArgs) Handles btnIndietro2.Click
        VisualizzaViewIdentificazioneChiamante()
    End Sub
    Protected Sub btnConfermaIntestatario_Click(sender As Object, e As System.EventArgs) Handles btnConfermaIntestatario.Click
        If IsNumeric(idSelectedIntestatario.Value) Then
            Try
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                Svuota(False, False)
                Dim idUnitaSelezionata As String = idSelectedIntestatario.Value
                Dim idAnagraficaSelezionata As String = idAnagraficaIntestatario.Value
                If IsNumeric(idAnagraficaSelezionata) Then
                    par.cmd.CommandText = "SELECT COGNOME,NOME,TELEFONO,TELEFONO_R,RAGIONE_SOCIALE FROM SISCOM_MI.ANAGRAFICA WHERE ID=" & idAnagraficaSelezionata
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        If par.IfNull(lettore("COGNOME"), "") = "" Then
                            TextBoxCognomeIntestatario.Text = par.IfNull(lettore("RAGIONE_SOCIALE"), "")
                            TextBoxNomeIntestatario.Text = ""
                        Else
                            TextBoxCognomeIntestatario.Text = par.IfNull(lettore("COGNOME"), "")
                            TextBoxNomeIntestatario.Text = par.IfNull(lettore("NOME"), "")
                        End If
                    End If
                    lettore.Close()
                End If
                If IsNumeric(idUnitaSelezionata) Then
                    par.cmd.CommandText = "SELECT (CASE WHEN (RAGIONE_SOCIALE IS NULL) THEN (COGNOME) ELSE (RAGIONE_SOCIALE) END) AS COGNOME_INTESTATARIO, " _
                        & " (CASE WHEN (RAGIONE_SOCIALE IS NULL) THEN (NOME) ELSE (NULL) END) AS NOME_INTESTATARIO, " _
                        & " RAPPORTI_UTENZA.COD_CONTRATTO, " _
                        & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " _
                        & " FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_IMMOBILIARI " _
                        & " WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                        & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                        & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                        & " AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA " _
                        & " AND UNITA_CONTRATTUALE.ID_UNITA= " & idUnitaSelezionata _
                        & " /*AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL*/ " _
                        & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' "
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TextBoxCognomeIntestatario.Text = par.IfNull(lettore("COGNOME_INTESTATARIO"), "")
                        TextBoxNomeIntestatario.Text = par.IfNull(lettore("NOME_INTESTATARIO"), "")
                        TextBoxCodiceContrattoIntestatario.Text = par.IfNull(lettore("COD_CONTRATTO"), "")
                        TextBoxCodiceUnitaImmobiliare.Text = par.IfNull(lettore("COD_UNITA_IMMOBILIARE"), "")
                    End If
                    lettore.Close()
                End If
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - btnConfermaIntestatario_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
            CaricaComplessi(True)
            'CaricaComplessiChiamante(True)
            CaricaEdifici(True)
            'CaricaEdificiChiamante(True)
            CaricaScale(True)
            'CaricaScaleChiamante(True)
            CaricaPiano(True)
            'CaricaPianoChiamante(True)
            CaricaInterni(True)
            'CaricaInterniChiamante(True)
            CaricaSedeTerritoriale()
            CaricaSegnalazioniEdificioSelezionato(True)
            CaricaSegnalazioniUnitaSelezionata(True)
            ControllaCondominio(True)
            ControllaMoroso()
            connData.chiudi()
            If DropDownListEdificio.SelectedValue <> "-1" Then
                panelDann.Visible = True
            Else
                panelDann.Visible = False
                TextBoxDanneggiante.Text = ""
                TextBoxDanneggiato.Text = ""
                Danneggiante.Value = ""
                Danneggiato.Value = ""
            End If
        Else
            par.modalDialogMessage("Nuova segnalazione", "Selezionare un nominativo.", Page, "info")
            VisualizzaViewRicercaIntestatario()
        End If
    End Sub
    Private Sub VisualizzaViewIdentificazioneChiamante()
        MultiView1.ActiveViewIndex = 0
        MultiView2.ActiveViewIndex = 0
        MultiView3.ActiveViewIndex = 0
    End Sub
    Private Sub VisualizzaViewRicercaChiamante()
        MultiView1.ActiveViewIndex = 1
        MultiView2.ActiveViewIndex = 1
        MultiView3.ActiveViewIndex = 1
    End Sub
    Private Sub VisualizzaViewRicercaIntestatario()
        MultiView1.ActiveViewIndex = 2
        MultiView2.ActiveViewIndex = 2
        MultiView3.ActiveViewIndex = 2
    End Sub
    Private Sub VisualizzaViewDanneggiante()
        MultiView1.ActiveViewIndex = 3
        MultiView2.ActiveViewIndex = 3
        MultiView3.ActiveViewIndex = 3
    End Sub
    Private Sub VisualizzaViewDanneggiato()
        MultiView1.ActiveViewIndex = 4
        MultiView2.ActiveViewIndex = 4
        MultiView3.ActiveViewIndex = 4
    End Sub
    Private Function CheckControl() As Boolean
        CheckControl = True
        Try
            If cmbTipologiaSegnalante.SelectedValue = "-1" Then
                CheckControl = False
                par.modalDialogMessage("Nuova segnalazione", "Inserire la tipologia di segnalante", Me.Page, "info", , )
            End If
            If DropDownListEdificio.SelectedValue = "-1" Then
                CheckControl = False
                par.modalDialogMessage("Nuova segnalazione", "Inserire l\'oggetto della richiesta", Me.Page, "info", , )
            End If
            If String.IsNullOrEmpty(Me.txtDescrizione.Text) Then
                CheckControl = False
                par.modalDialogMessage("Nuova segnalazione", "Inserire la descrizione della richiesta", Me.Page, "info", , )
            End If
            If cmbTipoSegnalazioneLivello0.SelectedValue = "-1" Then
                CheckControl = False
                par.modalDialogMessage("Nuova segnalazione", "Inserire la tipologia della segnalazione", Me.Page, "info", , )
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CheckControl - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        Return CheckControl
    End Function
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If CheckControl() = True Then
            Try

                Dim STATOS As String = "0"
                Dim ORIGINE As String = "A"
                Dim idSegnal As String = "-1"
                If idSegnal = "-1" Then
                    connData.apri(True)
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_SEGNALAZIONI.NEXTVAL FROM DUAL"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        idSegnal = par.IfNull(lettore(0), "-1")
                    End If
                    lettore.Close()
                    Dim valoreUrgenza As String = "NULL"
                    If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
                        valoreUrgenza = cmbUrgenza.SelectedIndex
                    End If
                    Dim edificio As String = "NULL"
                    If DropDownListEdificio.SelectedValue <> "-1" Then
                        edificio = DropDownListEdificio.SelectedValue
                    End If
                    Dim unita As String = "NULL"
                    Dim contratto As String = "NULL"
                    If IsNumeric(idSelectedIntestatario.Value) AndAlso idSelectedIntestatario.Value <> "-1" Then
                        unita = idSelectedIntestatario.Value
                        If IsNumeric(idContrattoIntestatario.Value) Then
                            contratto = idContrattoIntestatario.Value
                        End If
                    Else
                        If IsNumeric(idSelectedChiamante.Value) AndAlso idSelectedChiamante.Value <> "-1" Then
                            unita = idSelectedChiamante.Value
                            If IsNumeric(idContrattoChiamante.Value) Then
                                contratto = idContrattoChiamante.Value
                            End If
                        End If
                    End If
                    Dim struttura As String = "NULL"
                    If DropDownListSedeTerritoriale.SelectedValue <> "-1" And DropDownListSedeTerritoriale.SelectedValue <> "" Then
                        struttura = DropDownListSedeTerritoriale.SelectedValue
                    End If
                    Dim segnalazioneLivello0 As String = "NULL"
                    If IsNumeric(cmbTipoSegnalazioneLivello0.SelectedValue) AndAlso cmbTipoSegnalazioneLivello0.SelectedValue <> "-1" Then
                        segnalazioneLivello0 = cmbTipoSegnalazioneLivello0.SelectedValue
                    End If
                    Dim segnalazioneLivello1 As String = "NULL"
                    If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
                        segnalazioneLivello1 = cmbTipoSegnalazioneLivello1.SelectedValue
                    End If
                    Dim segnalazioneLivello2 As String = "NULL"
                    If IsNumeric(cmbTipoSegnalazioneLivello2.SelectedValue) AndAlso cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" Then
                        segnalazioneLivello2 = cmbTipoSegnalazioneLivello2.SelectedValue
                    End If
                    Dim segnalazioneLivello3 As String = "NULL"
                    If IsNumeric(cmbTipoSegnalazioneLivello3.SelectedValue) AndAlso cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" Then
                        segnalazioneLivello3 = cmbTipoSegnalazioneLivello3.SelectedValue
                    End If
                    Dim segnalazioneLivello4 As String = "NULL"
                    If IsNumeric(cmbTipoSegnalazioneLivello4.SelectedValue) AndAlso cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" Then
                        segnalazioneLivello4 = cmbTipoSegnalazioneLivello4.SelectedValue
                    End If

                    Dim custode As String = "0"
                    If idSelectedChiamante.Value = "C" Then
                        custode = "1"
                    End If

                    Dim FL_DVCA As String = "0"
                    If CheckBoxDVCA.Checked = True Then
                        FL_DVCA = "1"
                    End If
                    Dim FL_AV As String = "0"
                    If CheckBoxAttoVandalico.Checked = True Then
                        FL_AV = "1"
                    End If
                    Dim FL_FS As String = "0"
                    If CheckBoxFalsa.Checked = True Then
                        FL_FS = "1"
                    End If
                    Dim FL_ContattatoFornitore As String = "0"
                    If CheckBoxContattatoFornitore.Checked = True Then
                        FL_ContattatoFornitore = "1"
                    End If
                    Dim FL_VerificaFornitore As String = "0"
                    If CheckBoxVerificaFornitore.Checked = True Then
                        FL_VerificaFornitore = "1"
                    End If
                    Dim dataContattatoFornitore As String = "NULL"
                    Dim oraContattatoFornitore As String = ""
                    If IsDate(TextBoxContattatoFornitore.Text) AndAlso Len(TextBoxContattatoFornitore.Text) = 10 Then
                        dataContattatoFornitore = par.AggiustaData(TextBoxContattatoFornitore.Text)
                    End If
                    If dataContattatoFornitore = "NULL" And TextBoxOraContattatoFornitore.SelectedTime.ToString.Length > 0 Then
                        par.modalDialogMessage("Agenda e Segnalazioni", "Impossibile valorizzare il campo ore/minuti senza aver validato la data Contattato Fornitore Emergenza", Me.Page, "info")
                        Exit Sub
                    Else
                        oraContattatoFornitore = TextBoxOraContattatoFornitore.SelectedTime.ToString
                    End If
                    Dim dataVerificaFornitore As String = "NULL"
                    Dim oraVerificaFornitore As String = ""
                    If IsDate(TextBoxVerificaFornitore.Text) AndAlso Len(TextBoxVerificaFornitore.Text) = 10 Then
                        dataVerificaFornitore = par.AggiustaData(TextBoxVerificaFornitore.Text)
                    End If
                    If dataVerificaFornitore = "NULL" And TextBoxOraVerificaFornitore.SelectedTime.ToString.Length > 0 Then
                        par.modalDialogMessage("Agenda e Segnalazioni", "Impossibile valorizzare il campo ore/minuti senza aver validato la data Verifica Fornitore Emergenza", Me.Page, "info")
                        Exit Sub
                    Else
                        oraVerificaFornitore = TextBoxOraVerificaFornitore.SelectedTime.ToString
                    End If

                    Dim dataSopralluogo As String = "NULL"
                    Dim oraSopralluogo As String = ""
                    If IsDate(txtDataSopralluogo.Text) AndAlso Len(txtDataSopralluogo.Text) = 10 Then
                        dataSopralluogo = par.AggiustaData(txtDataSopralluogo.Text)
                    End If
                    If dataSopralluogo = "NULL" And txtOraSopralluogo.SelectedTime.ToString.Length > 0 Then
                        par.modalDialogMessage("Agenda e Segnalazioni", "Impossibile valorizzare il campo ore/minuti senza aver validato la data Verifica Sopralluogo", Me.Page, "info")
                        Exit Sub
                    Else
                        oraSopralluogo = txtOraSopralluogo.SelectedTime.ToString
                    End If

                    Dim dataProgrIntervento As String = "NULL"
                    Dim dataProgrUltimoIntervento As String = "NULL"
                    Dim oraProgrIntervento As String = ""
                    Dim idProgrammaIntervento As String = "NULL"
                    Dim dataP As String = ""
                    Dim lett As Oracle.DataAccess.Client.OracleDataReader
                    If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue > 0 Then
                    par.cmd.CommandText = "SELECT GETDATA(MIN(DATA)), ID_PROGRAMMA_ATTIVITA " _
                        & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT " _
                        & " WHERE ID_EDIFICIO=" & DropDownListEdificio.SelectedValue _
                        & " AND DATA>'" & Format(Now, "yyyyMMdd") & "' " _
                        & " AND ID_PROGRAMMA_ATTIVITA IN " _
                        & " (SELECT ID " _
                        & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA " _
                        & " WHERE ID_sTATO=1 And ATTIVITA_CRONOPROGRAMMA =" & cmbTipoSegnalazioneLivello1.SelectedValue _
                        & " AND ID_TIPO_CRONOPROGRAMMA = 1) " _
                        & " AND FL_CANCELLATO = 0 GROUP BY ID_PROGRAMMA_ATTIVITA ORDER BY 1"

                        lett = par.cmd.ExecuteReader
                    If lett.Read Then
                        If IsDate(lett(0)) Then
                            dataP = par.IfNull(lett(0), "")
                            idProgrammaIntervento = par.IfNull(lett(1), "")
                        End If
                    End If
                    lett.Close()
                    End If
                    If IsDate(dataP) And dataP <> "" Then
                        txtDataProgrammataIntervento.Text = dataP
                    End If

                    If IsDate(txtDataProgrammataIntervento.Text) AndAlso Len(txtDataProgrammataIntervento.Text) = 10 Then
                        dataProgrIntervento = par.AggiustaData(txtDataProgrammataIntervento.Text)
                    End If
                    If dataProgrIntervento = "NULL" And txtOraProgrammataIntervento.SelectedTime.ToString.Length > 0 Then
                        par.modalDialogMessage("Agenda e Segnalazioni", "Impossibile valorizzare il campo ore/minuti senza aver validato la data Programmata Intervento", Me.Page, "info")
                        Exit Sub
                    Else
                        oraProgrIntervento = txtOraProgrammataIntervento.SelectedTime.ToString
                    End If
                    Dim dataPrec As String = ""
                    If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue > 0 Then

                    par.cmd.CommandText = "SELECT GETDATA(MAX(DATA)) " _
                        & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT " _
                        & " WHERE ID_EDIFICIO=" & DropDownListEdificio.SelectedValue _
                        & " And DATA<='" & Format(Now, "yyyyMMdd") & "' " _
                        & " AND ID_PROGRAMMA_ATTIVITA IN (SELECT ID " _
                        & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA " _
                        & " WHERE ID_sTATO=1 And ATTIVITA_CRONOPROGRAMMA =" & cmbTipoSegnalazioneLivello1.SelectedValue _
                        & " And ID_TIPO_CRONOPROGRAMMA = 1)"
                    lett = par.cmd.ExecuteReader
                    If lett.Read Then
                        If IsDate(par.IfNull(lett(0), 0)) Then
                            dataPrec = par.IfNull(lett(0), 0)
                        End If
                    End If
                    lett.Close()
                    End If
                    If IsDate(dataPrec) And dataPrec <> "" Then
                        txtDataProgrammataUltimoIntervento.Text = dataPrec
                    End If
                    If IsDate(txtDataProgrammataUltimoIntervento.Text) AndAlso Len(txtDataProgrammataUltimoIntervento.Text) = 10 Then
                        dataProgrUltimoIntervento = par.AggiustaData(txtDataProgrammataUltimoIntervento.Text)
                    End If

                    Dim dataEffIntervento As String = "NULL"
                    Dim oraEffIntervento As String = ""
                    If IsDate(txtDataEffettivaIntervento.Text) AndAlso Len(txtDataEffettivaIntervento.Text) = 10 Then
                        dataEffIntervento = par.AggiustaData(txtDataEffettivaIntervento.Text)
                    End If
                    If dataEffIntervento = "NULL" And txtOraEffettivaIntervento.SelectedTime.ToString.Length > 0 Then
                        par.modalDialogMessage("Agenda e Segnalazioni", "Impossibile valorizzare il campo ore/minuti senza aver validato la data Effettiva Intervento", Me.Page, "info")
                        Exit Sub
                    Else
                        oraEffIntervento = txtOraEffettivaIntervento.SelectedTime.ToString
                    End If

                    Dim idAnagrafica As String = "NULL"
                    Dim idAmministratore As String = "NULL"
                    Dim idGestioneAutonoma As String = "NULL"
                    Dim idFornitore As String = "NULL"
                    Dim idChiamantiNonNoti As String = "NULL"
                    Dim idSoggettiCoinvolti As String = "NULL"

                    Select Case idContrattoChiamante.Value
                        Case "NN"
                            idChiamantiNonNoti = CStr(idAnagraficaChiamante.Value)
                        Case "FO"
                            idFornitore = CStr(idAnagraficaChiamante.Value)
                        Case "SC"
                            idSoggettiCoinvolti = CStr(idAnagraficaChiamante.Value)
                        Case "AM"
                            idAmministratore = CStr(idAnagraficaChiamante.Value)
                        Case "GA"
                            idGestioneAutonoma = CStr(idAnagraficaChiamante.Value)
                        Case Else
                            If IsNumeric(idAnagraficaChiamante.Value) Then
                            idAnagrafica = CStr(idAnagraficaChiamante.Value)
                            Else
                                idAnagrafica = "NULL"
                            End If
                    End Select

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI(ID," _
                        & " ID_STATO," _
                        & " ID_EDIFICIO," _
                        & " ID_UNITA," _
                        & " COGNOME_RS," _
                        & " DATA_ORA_RICHIESTA," _
                        & " TELEFONO1," _
                        & " TELEFONO2," _
                        & " MAIL," _
                        & " DESCRIZIONE_RIC," _
                        & " ID_OPERATORE_INS," _
                        & " NOME," _
                        & " TIPO_RICHIESTA," _
                        & " ORIGINE," _
                        & " ID_TIPOLOGIE," _
                        & " TIPO_PERVENUTA," _
                        & " ID_STRUTTURA," _
                        & " ID_CONTRATTO," _
                        & " ID_PERICOLO_SEGNALAZIONE," _
                        & " ID_PERICOLO_SEGNALAZIONE_INIZ," _
                        & " ID_TIPO_SEGNALAZIONE," _
                        & " ID_TIPO_SEGN_LIVELLO_1," _
                        & " ID_TIPO_SEGN_LIVELLO_2," _
                        & " ID_TIPO_SEGN_LIVELLO_3," _
                        & " ID_TIPO_SEGN_LIVELLO_4," _
                        & " ID_ANAGRAFICA," _
                        & " ID_AMMINISTRATORE," _
                        & " ID_GESTIONE_AUTONOMA," _
                        & " ID_FORNITORE," _
                        & " ID_CHIAMANTI_NON_NOTI," _
                        & " ID_SOGGETTI_COINVOLTI," _
                        & " DANNEGGIANTE," _
                        & " DANNEGGIATO," _
                        & " ID_CANALE," _
                        & " FL_CUSTODE," _
                        & " FL_DVCA," _
                        & " FL_AV," _
                        & " FL_FS," _
                        & " FL_CONTATTO_FORNITORE," _
                        & " DATA_CONTATTO_FORNITORE," _
                        & " FL_VERIFICA_FORNITORE," _
                        & " DATA_VERIFICA_FORNITORE," _
                        & " ID_TIPOLOGIA_SEGNALANTE, " _
                        & " DATA_SOPRALLUOGO, " _
                        & " DATA_PROGRAMMATA_INT, " _
                        & " DATA_PROGRAMMATA_INT2, " _
                        & " DATA_EFFETTIVA_INT, " _
                        & " ID_PROGRAMMA_ATTIVITA " _
                        & " ) " _
                        & " VALUES " _
                        & "(" & idSegnal & "," _
                        & STATOS & "," _
                        & DropDownListEdificio.SelectedValue & "," _
                        & unita & "," _
                        & "'" & par.PulisciStrSql(TextBoxCognomeChiamante.Text.ToUpper) & "'," _
                        & "'" & Format(Now, "yyyyMMddHHmm") & "'," _
                        & "'" & par.PulisciStrSql(TextBoxTelefono1Chiamante.Text) & "'," _
                        & "'" & par.PulisciStrSql(TextBoxTelefono2Chiamante.Text) & "'," _
                        & "'" & LCase(par.PulisciStrSql(TextBoxEmailChiamante.Text)) & "'," _
                        & "'" & par.PulisciStrSql(txtDescrizione.Text) & "'," _
                        & Session.Item("ID_OPERATORE") & "," _
                        & "'" & par.PulisciStrSql(TextBoxNomeChiamante.Text.ToUpper) & "'," _
                        & "1" & ",'" & ORIGINE & "',NULL," _
                        & "'Telefonica', " _
                        & struttura & "," _
                        & contratto & "," _
                        & valoreUrgenza & "," _
                        & valoreUrgenza & "," _
                        & segnalazioneLivello0 & "," _
                        & segnalazioneLivello1 & "," _
                        & segnalazioneLivello2 & "," _
                        & segnalazioneLivello3 & "," _
                        & segnalazioneLivello4 & "," _
                        & idAnagrafica & "," _
                        & idAmministratore & "," _
                        & idGestioneAutonoma & "," _
                        & idFornitore & "," _
                        & idChiamantiNonNoti & "," _
                        & idSoggettiCoinvolti & "," _
                        & "'" & par.PulisciStrSql(TextBoxDanneggiante.Text) & "'," _
                        & "'" & par.PulisciStrSql(TextBoxDanneggiato.Text) & "', " _
                        & DropDownListCanale.SelectedValue & ", " _
                        & custode & ", " _
                        & FL_DVCA & ", " _
                        & FL_AV & ", " _
                        & FL_FS & ", " _
                        & FL_ContattatoFornitore & ", " _
                         & dataContattatoFornitore & par.AggiustaOra(oraContattatoFornitore) & ", " _
                        & FL_VerificaFornitore & ", " _
                        & dataVerificaFornitore & par.AggiustaOra(oraVerificaFornitore) & "," _
                        & cmbTipologiaSegnalante.SelectedValue & ", " _
                        & dataSopralluogo & par.AggiustaOra(oraSopralluogo) & "," _
                        & dataProgrIntervento & par.AggiustaOra(oraProgrIntervento) & "," _
                        & dataProgrUltimoIntervento & "," _
                        & dataEffIntervento & par.AggiustaOra(oraEffIntervento) & ", " _
                        & idProgrammaIntervento _
                        & ")"
                    par.cmd.ExecuteNonQuery()
                    idSegnalazione.Value = idSegnal
                    Dim idTipologiaManutenzione As String = RicavaTipologiaManutenzione()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_TIPOLOGIA_MANUTENZIONE = " & par.RitornaNullSeMenoUno(idTipologiaManutenzione) & " WHERE ID = " & idSegnal
                    par.cmd.ExecuteNonQuery()
                    If par.IfEmpty(idTipologiaManutenzione, "0") <> "1" Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET " _
                                            & " DATA_SOPRALLUOGO = '' " _
                                            & " ,DATA_PROGRAMMATA_INT = '' " _
                                            & " ,DATA_PROGRAMMATA_INT2 = '' " _
                                            & " ,DATA_EFFETTIVA_INT = '' " _
                                            & " ,ID_PROGRAMMA_ATTIVITA = '' " _
                                            & " WHERE ID = " & idSegnal
                        par.cmd.ExecuteNonQuery()
                    End If


                    Dim controlloTipoManutenzione As String = ""
                    If segnalazioneLivello0 <> "NULL" Then
                        If controlloTipoManutenzione = "" Then
                            controlloTipoManutenzione = " where id_tipo_segnalazione=" & segnalazioneLivello0
                        Else
                            controlloTipoManutenzione &= " And id_tipo_segnalazione=" & segnalazioneLivello0
                        End If
                    End If
                    If segnalazioneLivello1 <> "NULL" Then
                        If controlloTipoManutenzione = "" Then
                            controlloTipoManutenzione = " where ID_TIPO_SEGNALAZIONE_LIVELLO_1=" & segnalazioneLivello1
                        Else
                            controlloTipoManutenzione &= " And ID_TIPO_SEGNALAZIONE_LIVELLO_1=" & segnalazioneLivello1
                        End If
                    End If
                    If segnalazioneLivello2 <> "NULL" Then
                        If controlloTipoManutenzione = "" Then
                            controlloTipoManutenzione = " where ID_TIPO_SEGNALAZIONE_LIVELLO_2=" & segnalazioneLivello2
                        Else
                            controlloTipoManutenzione &= " And ID_TIPO_SEGNALAZIONE_LIVELLO_2=" & segnalazioneLivello2
                        End If
                    End If
                    If segnalazioneLivello3 <> "NULL" Then
                        If controlloTipoManutenzione = "" Then
                            controlloTipoManutenzione = " where ID_TIPO_SEGNALAZIONE_LIVELLO_3=" & segnalazioneLivello3
                        Else
                            controlloTipoManutenzione &= " And ID_TIPO_SEGNALAZIONE_LIVELLO_3=" & segnalazioneLivello3
                        End If
                    End If
                    If segnalazioneLivello4 <> "NULL" Then
                        If controlloTipoManutenzione = "" Then
                            controlloTipoManutenzione = " where ID_TIPO_SEGNALAZIONE_LIVELLO_4=" & segnalazioneLivello4
                        Else
                            controlloTipoManutenzione &= " And ID_TIPO_SEGNALAZIONE_LIVELLO_4=" & segnalazioneLivello4
                        End If
                    End If
                    par.cmd.CommandText = "SELECT NVL(ID_TIPO_MANUTENZIONE,0) FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE " & controlloTipoManutenzione
                    Dim tipo As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If tipo <> 1 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET DATA_PROGRAMMATA_INT2=NULL," _
                            & " DATA_EFFETTIVA_INT=NULL," _
                            & " DATA_PROGRAMMATA_INT=NULL," _
                            & " DATA_SOPRALLUOGO=NULL " _
                            & " WHERE ID=" & idSegnal
                        par.cmd.ExecuteNonQuery()
                    End If


                    '************ AGGIORNAMENTO CHIAMANTI ****************
                    If idSelectedChiamante.Value = "C" Then
                        If IsNumeric(idAnagraficaChiamante.Value) Then
                            par.cmd.CommandText = "SELECT EMAIL_AZIENDALE, " _
                                & " CELLULARE_AZIENDALE," _
                                & " TELEFONO_AZIENDALE " _
                                & " FROM SISCOM_MI.ANAGRAFICA_CUSTODI " _
                                & " WHERE ID = " & idAnagraficaChiamante.Value.ToString
                            Dim lettoreAna As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            Dim oldEmail As String = ""
                            Dim oldTelefono As String = ""
                            Dim oldCellulare As String = ""
                            If lettoreAna.Read Then
                                oldEmail = par.IfNull(lettoreAna("email_aziendale"), "")
                                oldTelefono = par.IfNull(lettoreAna("TELEFONO_AZIENDALE"), "")
                                oldCellulare = par.IfNull(lettoreAna("CELLULARE_AZIENDALE"), "")
                            End If
                            lettoreAna.Close()
                            If oldEmail <> Trim(TextBoxEmailChiamante.Text) Or oldCellulare <> Trim(TextBoxTelefono2Chiamante.Text) Or oldTelefono <> Trim(TextBoxTelefono1Chiamante.Text) Then
                                par.cmd.CommandText = " UPDATE SISCOM_MI.ANAGRAFICA_CUSTODI " _
                                    & " SET " _
                                    & " EMAIL_AZIENDALE = '" & LCase(par.PulisciStrSql(TextBoxEmailChiamante.Text)) & "', " _
                                    & " CELLULARE_AZIENDALE = '" & par.PulisciStrSql(TextBoxTelefono2Chiamante.Text) & "', " _
                                    & " TELEFONO_AZIENDALE = '" & par.PulisciStrSql(TextBoxTelefono1Chiamante.Text) & "'" _
                                    & " WHERE  ID = " & idAnagraficaChiamante.Value.ToString
                                par.cmd.ExecuteNonQuery()
                                WriteEvent("F251", "Modifica dati anagrafica custodi: Email da " & oldEmail & " a " & TextBoxEmailChiamante.Text & " - Telefono da " & oldTelefono & " a " & TextBoxTelefono1Chiamante.Text & " - Cellulare da " & oldCellulare & " a " & TextBoxTelefono2Chiamante.Text)
                            End If
                        End If
                    ElseIf idSelectedChiamante.Value = "NN" Then
                        If IsNumeric(idAnagraficaChiamante.Value) Then
                            par.cmd.CommandText = "SELECT EMAIL, " _
                                & " CELLULARE," _
                                & " TELEFONO" _
                                & " FROM SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI " _
                                & " WHERE ID = " & idAnagraficaChiamante.Value.ToString
                            Dim lettoreAna As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            Dim oldEmail As String = ""
                            Dim oldTelefono As String = ""
                            Dim oldCellulare As String = ""
                            If lettoreAna.Read Then
                                oldEmail = par.IfNull(lettoreAna("email"), "")
                                oldTelefono = par.IfNull(lettoreAna("TELEFONO"), "")
                                oldCellulare = par.IfNull(lettoreAna("CELLULARE"), "")
                            End If
                            lettoreAna.Close()
                            If oldEmail <> Trim(TextBoxEmailChiamante.Text) Or oldCellulare <> Trim(TextBoxTelefono2Chiamante.Text) Or oldTelefono <> Trim(TextBoxTelefono1Chiamante.Text) Then
                                par.cmd.CommandText = " UPDATE SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI " _
                                    & " SET " _
                                    & " EMAIL= '" & LCase(par.PulisciStrSql(TextBoxEmailChiamante.Text)) & "', " _
                                    & " CELLULARE = '" & par.PulisciStrSql(TextBoxTelefono2Chiamante.Text) & "', " _
                                    & " TELEFONO = '" & par.PulisciStrSql(TextBoxTelefono1Chiamante.Text) & "'" _
                                    & " WHERE  ID = " & idAnagraficaChiamante.Value.ToString
                                par.cmd.ExecuteNonQuery()
                                WriteEvent("F251", "Modifica dati anagrafica chiamanti non noti: Email da " & oldEmail & " a " & TextBoxEmailChiamante.Text & " - Telefono da " & oldTelefono & " a " & TextBoxTelefono1Chiamante.Text & " - Cellulare da " & oldCellulare & " a " & TextBoxTelefono2Chiamante.Text)
                            End If
                        End If
                    Else
                        If IsNumeric(idAnagraficaChiamante.Value) Then
                            par.cmd.CommandText = "SELECT EMAIL, " _
                                & " TELEFONO," _
                                & " TELEFONO_R" _
                                & " FROM SISCOM_MI.ANAGRAFICA " _
                                & " WHERE ID = " & idAnagraficaChiamante.Value.ToString
                            Dim lettoreAna As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            Dim oldEmail As String = ""
                            Dim oldTelefono As String = ""
                            Dim oldCellulare As String = ""
                            If lettoreAna.Read Then
                                oldEmail = par.IfNull(lettoreAna("EMAIL"), "")
                                oldTelefono = par.IfNull(lettoreAna("TELEFONO"), "")
                                oldCellulare = par.IfNull(lettoreAna("TELEFONO_R"), "")
                            End If
                            lettoreAna.Close()
                            If oldEmail <> Trim(TextBoxEmailChiamante.Text) Or oldCellulare <> Trim(TextBoxTelefono2Chiamante.Text) Or oldTelefono <> Trim(TextBoxTelefono1Chiamante.Text) Then
                                par.cmd.CommandText = " UPDATE SISCOM_MI.ANAGRAFICA " _
                                    & " SET " _
                                    & " EMAIL= '" & LCase(par.PulisciStrSql(TextBoxEmailChiamante.Text)) & "', " _
                                    & " TELEFONO_R = '" & par.PulisciStrSql(TextBoxTelefono2Chiamante.Text) & "', " _
                                    & " TELEFONO = '" & par.PulisciStrSql(TextBoxTelefono1Chiamante.Text) & "'" _
                                    & " WHERE  ID = " & idAnagraficaChiamante.Value.ToString
                                par.cmd.ExecuteNonQuery()
                                WriteEvent("F251", "Modifica dati anagrafica: Email da " & oldEmail & " a " & TextBoxEmailChiamante.Text & " - Telefono da " & oldTelefono & " a " & TextBoxTelefono1Chiamante.Text & " - Cellulare da " & oldCellulare & " a " & TextBoxTelefono2Chiamante.Text)
                            End If
                        End If
                    End If
                    If idAnagraficaChiamante.Value = "" Then
                        If Trim(TextBoxCognomeChiamante.Text) <> "" Then
                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI ( " _
                            & " ID, COGNOME, NOME,  " _
                            & " TELEFONO, CELLULARE, EMAIL)  " _
                            & " VALUES (SISCOM_MI.SEQ_ANA_CHIAMANTI_NON_N.NEXTVAL, " _
                            & " '" & par.PulisciStrSql(TextBoxCognomeChiamante.Text) & "', " _
                            & " '" & par.PulisciStrSql(TextBoxNomeChiamante.Text) & "', " _
                            & " '" & par.PulisciStrSql(TextBoxTelefono1Chiamante.Text) & "', " _
                            & " '" & par.PulisciStrSql(TextBoxTelefono2Chiamante.Text) & "', " _
                            & " '" & LCase(par.PulisciStrSql(TextBoxEmailChiamante.Text)) & "' ) "
                            par.cmd.ExecuteNonQuery()
                            WriteEvent("F251", "Inserimento anagrafica chiamante non noto: Email: " & TextBoxEmailChiamante.Text & " - Telefono: " & TextBoxTelefono1Chiamante.Text & " - Cellulare: " & TextBoxTelefono2Chiamante.Text)
                        End If
                    End If
                    '************ AGGIORNAMENTO CHIAMANTI ****************

                    '************ UNIONE SEGNALAZIONI UNITA' *************
                    Dim soglia As Integer = 0
                    par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='SOGLIA_TICKET_FIGLI'"
                    soglia = par.IfNull(par.cmd.ExecuteScalar, 10)

                    Dim listaS = Session.Item("listaSelezioneSegnUnita")
                    If listaS <> "" Then
                        listaS &= "," & idSegnal
                        Dim idPadre As Integer = 0
                        par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (SELECT DISTINCT ID_SEGNALAZIONE_PADRE FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE IN (" & listaS & "))) AND ID IN (" & listaS & ")"
                        idPadre = par.IfNull(par.cmd.ExecuteScalar, 0)
                        If idPadre = 0 Then
                            par.cmd.CommandText = "SELECT MIN(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (" & listaS & ")"
                            idPadre = par.IfNull(par.cmd.ExecuteScalar, 0)
                        End If

                        Dim pericoloIniziale As Integer = 0
                        Dim idStato As Integer = -1
                        par.cmd.CommandText = "SELECT ID_PERICOLO_SEGNALAZIONE_INIZ,id_Stato FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idPadre
                        Dim lettore3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore3.Read Then
                            pericoloIniziale = par.IfNull(lettore3("ID_PERICOLO_SEGNALAZIONE_INIZ"), 0)
                            idStato = par.IfNull(lettore3("ID_STATO"), -1)
                        End If
                        lettore3.Close()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_SEGNALAZIONE_PADRE=" & idPadre & ",ID_sTATO=" & idStato & " WHERE ID IN (" & listaS & ") AND ID NOT IN (" & idPadre & ")"
                        par.cmd.ExecuteNonQuery()

                        Dim numeroFigliDopoUpdate As Integer = 0
                        par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idPadre
                        numeroFigliDopoUpdate = par.IfNull(par.cmd.ExecuteScalar, 0)

                        Dim pericoloFinale As Integer = 0

                        Select Case numeroFigliDopoUpdate
                            Case 0 To soglia - 1
                                pericoloFinale = pericoloIniziale
                            Case soglia To soglia * 2 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 1, 4)
                            Case soglia * 2 To soglia * 3 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 2, 4)
                            Case soglia * 3 To soglia * 4 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 3, 4)
                            Case soglia * 4 To soglia * 5 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 4, 4)
                            Case Else
                                pericoloFinale = 4
                        End Select



                        Dim statoIniziale As Integer = 0
                        par.cmd.CommandText = "SELECT ID_PERICOLO_SEGNALAZIONE FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idPadre
                        statoIniziale = par.IfNull(par.cmd.ExecuteScalar, 0)


                        If pericoloFinale > statoIniziale Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_PERICOLO_SEGNALAZIONE=" & pericoloFinale & " WHERE ID=" & idPadre
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                                & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                                & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                                & " VALUES (" & idPadre & ", " _
                                & statoIniziale & ", " _
                                & pericoloFinale & ", " _
                                & Session.Item("ID_OPERATORE") & "," _
                                & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                & "'" & par.PulisciStrSql("Cambio priorità per numero soglia raggiunto.") & "')"
                            par.cmd.ExecuteNonQuery()
                        End If

                        par.cmd.CommandText = "select id,id_pericolo_Segnalazione from siscom_mi.segnalazioni where id_segnalazione_padre=" & idPadre
                        Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim idPericoloIniziale As Integer = 0
                        While lettore2.Read
                            idPericoloIniziale = par.IfNull(lettore2("ID_PERICOLO_SEGNALAZIONE"), 0)
                            If idPericoloIniziale <> pericoloFinale Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_PERICOLO_SEGNALAZIONE=" & pericoloFinale _
                                & " WHERE ID=" & par.IfNull(lettore2("ID"), 0)
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                                    & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                                    & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                                    & " VALUES (" & par.IfNull(lettore2("ID"), 0) & ", " _
                                    & idPericoloIniziale & ", " _
                                    & pericoloFinale & ", " _
                                    & Session.Item("ID_OPERATORE") & "," _
                                    & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                    & "'" & par.PulisciStrSql("Cambio priorità per unione segnalazione.") & "')"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End While
                        lettore2.Close()

                        WriteEvent("F252", "Unione segnalazioni unità: " & listaS)
                    End If
                    '************ UNIONE SEGNALAZIONI UNITA' *************

                    '************ UNIONE SEGNALAZIONI EDIFICI ************
                    listaS = Session.Item("listaSelezioneSegnEdificio")
                    If listaS <> "" Then
                        listaS &= "," & idSegnal
                        Dim idPadre As Integer = 0
                        par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (SELECT DISTINCT ID_SEGNALAZIONE_PADRE FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE IN (" & listaS & "))) AND ID IN (" & listaS & ")"
                        idPadre = par.IfNull(par.cmd.ExecuteScalar, 0)
                        If idPadre = 0 Then
                            par.cmd.CommandText = "SELECT MIN(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (" & listaS & ")"
                            idPadre = par.IfNull(par.cmd.ExecuteScalar, 0)
                        End If

                        Dim pericoloIniziale As Integer = 0
                        Dim idStato As Integer = -1
                        par.cmd.CommandText = "SELECT ID_PERICOLO_SEGNALAZIONE_INIZ,id_Stato FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idPadre
                        Dim lettore3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore3.Read Then
                            pericoloIniziale = par.IfNull(lettore3("ID_PERICOLO_SEGNALAZIONE_INIZ"), 0)
                            idStato = par.IfNull(lettore3("ID_STATO"), -1)
                        End If
                        lettore3.Close()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_SEGNALAZIONE_PADRE=" & idPadre & ",ID_sTATO=" & idStato & " WHERE ID IN (" & listaS & ") AND ID NOT IN (" & idPadre & ")"
                        par.cmd.ExecuteNonQuery()

                        Dim numeroFigliDopoUpdate As Integer = 0
                        par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idPadre
                        numeroFigliDopoUpdate = par.IfNull(par.cmd.ExecuteScalar, 0)

                        Dim pericoloFinale As Integer = 0

                        Select Case numeroFigliDopoUpdate
                            Case 0 To soglia - 1
                                pericoloFinale = pericoloIniziale
                            Case soglia To soglia * 2 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 1, 4)
                            Case soglia * 2 To soglia * 3 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 2, 4)
                            Case soglia * 3 To soglia * 4 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 3, 4)
                            Case soglia * 4 To soglia * 5 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 4, 4)
                            Case Else
                                pericoloFinale = 4
                        End Select

                        Dim statoIniziale As Integer = 0
                        par.cmd.CommandText = "SELECT ID_PERICOLO_SEGNALAZIONE FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idPadre
                        statoIniziale = par.IfNull(par.cmd.ExecuteScalar, 0)

                        If pericoloFinale > statoIniziale Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_PERICOLO_SEGNALAZIONE=" & pericoloFinale & " WHERE ID=" & idPadre
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                                & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                                & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                                & " VALUES (" & idPadre & ", " _
                                & statoIniziale & ", " _
                                & pericoloFinale & ", " _
                                & Session.Item("ID_OPERATORE") & "," _
                                & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                & "'" & par.PulisciStrSql("Cambio priorità per numero soglia raggiunto.") & "')"
                            par.cmd.ExecuteNonQuery()
                        End If

                        par.cmd.CommandText = "select id,id_pericolo_Segnalazione from siscom_mi.segnalazioni where id_segnalazione_padre=" & idPadre
                        Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim idPericoloIniziale As Integer = 0
                        While lettore2.Read
                            idPericoloIniziale = par.IfNull(lettore2("ID_PERICOLO_SEGNALAZIONE"), 0)
                            If idPericoloIniziale <> pericoloFinale Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_PERICOLO_SEGNALAZIONE=" & pericoloFinale _
                                & " WHERE ID=" & par.IfNull(lettore2("ID"), 0)
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                                    & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                                    & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                                    & " VALUES (" & par.IfNull(lettore2("ID"), 0) & ", " _
                                    & idPericoloIniziale & ", " _
                                    & pericoloFinale & ", " _
                                    & Session.Item("ID_OPERATORE") & "," _
                                    & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                    & "'" & par.PulisciStrSql("Cambio priorità per unione segnalazione.") & "')"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End While
                        lettore2.Close()

                        WriteEvent("F252", "Unione segnalazioni edificio: " & listaS)
                    End If

                    '************ UNIONE SEGNALAZIONI EDIFICI ************

                    connData.chiudi(True)
                    WriteEvent("F232", "")
                    frmModify.Value = "0"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Inserimento segnalazione", "Operazione effettuata correttamente", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idSegnal, )
                Else
                    par.modalDialogMessage("Inserimento segnalazione", "Si è verificato un errore durante l\'inserimento della segnalazione.", Me.Page, "error", , )
                End If

                If idSelectedIntestatario.Value <> "" Then
                    CaricaSegnalazioniEdificioSelezionato(True)
                Else
                    CaricaSegnalazioniEdificioSelezionato()
                End If
                If idSelectedIntestatario.Value <> "" Then
                    CaricaSegnalazioniUnitaSelezionata(True)
                Else
                    CaricaSegnalazioniUnitaSelezionata()
                End If
                VisualizzaViewIdentificazioneChiamante()
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - btnSalva_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
    End Sub
    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String)
        Dim connOpNow As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri()
                connOpNow = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & idSegnalazione.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & cod & "','" & par.PulisciStrSql(motivo) & "')"
            par.cmd.ExecuteNonQuery()
            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
                connData.chiudi()
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - WriteEvent - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub solaLettura()
        Try
            Dim CTRL As Control = Nothing
            For Each CTRL In View2.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next
            For Each CTRL In View5.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next
            For Each CTRL In View11.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next
            For Each CTRL In View3.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next
            For Each CTRL In View6.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next
            For Each CTRL In View12.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub ImageButtonDanneggiante_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonDanneggiante.Click
        If DropDownListEdificio.SelectedValue <> "-1" Then
            Try
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ID_UNITA,SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AS ID_INTE,ANAGRAFICA.ID, SOGGETTI_CONTRATTUALI.ID_CONTRATTO, " _
                    & "CASE WHEN RAGIONE_SOCIALE IS NULL THEN COGNOME ||' '|| NOME ELSE RAGIONE_SOCIALE END AS NOMINATIVO,siscom_mi.getintestatari (unita_contrattuale.id_contratto) AS intestatario, " _
                    & "TO_CHAR(TO_DATE(SUBSTR(DATA_NASCITA,0,8),'yyyymmdd'),'dd/mm/yyyy')AS DATA_NASCITA, " _
                    & " INDIRIZZI.DESCRIZIONE AS INDIRIZZO ,INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RAPPORTI_UTENZA.COD_CONTRATTO AS CODICE_CONTRATTO,ID_ANAGRAFICA " _
                    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA ,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI " _
                    & "WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                    & "AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                    & "AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                    & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA " _
                    & "AND ID_INDIRIZZO = INDIRIZZI.ID " _
                    & "AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID (+) " _
                    & "AND UNITA_IMMOBILIARI.cod_tipologia = TIPOLOGIA_UNITA_IMMOBILIARI.cod " _
                    & "AND UNITA_IMMOBILIARI.ID_EDIFICIO=" & DropDownListEdificio.SelectedValue _
                    & "AND COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                    & "order by NOMINATIVO ASC"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                DataGridDanneggiante.DataSource = dt
                DataGridDanneggiante.DataBind()
                Danneggiante.Value = ""
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ImageButtonDanneggiante_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
            VisualizzaViewDanneggiante()
        Else
            par.modalDialogMessage("Nuova segnalazione", "E\' necessario selezionare un edificio.", Page, "info")
        End If
    End Sub
    Protected Sub ImageButtonDanneggiato_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonDanneggiato.Click
        If DropDownListEdificio.SelectedValue <> "-1" Then
            Try
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ID_UNITA,SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AS ID_INTE,ANAGRAFICA.ID, SOGGETTI_CONTRATTUALI.ID_CONTRATTO, " _
                    & "CASE WHEN RAGIONE_SOCIALE IS NULL THEN COGNOME ||' '|| NOME ELSE RAGIONE_SOCIALE END AS NOMINATIVO,siscom_mi.getintestatari (unita_contrattuale.id_contratto) AS intestatario, " _
                    & "TO_CHAR(TO_DATE(SUBSTR(DATA_NASCITA,0,8),'yyyymmdd'),'dd/mm/yyyy')AS DATA_NASCITA, " _
                    & " INDIRIZZI.DESCRIZIONE AS INDIRIZZO ,INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RAPPORTI_UTENZA.COD_CONTRATTO AS CODICE_CONTRATTO,ID_ANAGRAFICA " _
                    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA ,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI " _
                    & "WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                    & "AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                    & "AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                    & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA " _
                    & "AND ID_INDIRIZZO = INDIRIZZI.ID " _
                    & "AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID (+) " _
                    & "AND UNITA_IMMOBILIARI.cod_tipologia = TIPOLOGIA_UNITA_IMMOBILIARI.cod " _
                    & "AND UNITA_IMMOBILIARI.ID_EDIFICIO=" & DropDownListEdificio.SelectedValue _
                    & "AND COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                    & "order by NOMINATIVO ASC"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                DataGridDanneggiato.DataSource = dt
                DataGridDanneggiato.DataBind()
                Danneggiato.Value = ""
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ImageButtonDanneggiato_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
            VisualizzaViewDanneggiato()
        Else
            par.modalDialogMessage("Nuova segnalazione", "E\' necessario selezionare un edificio.", Page, "info")
        End If
    End Sub
    Protected Sub btnConfermaDanneggiante_Click(sender As Object, e As System.EventArgs) Handles btnConfermaDanneggiante.Click
        TextBoxDanneggiante.Text = Danneggiante.Value
    End Sub
    Protected Sub btnConfermaDanneggiato_Click(sender As Object, e As System.EventArgs) Handles btnConfermaDanneggiato.Click
        TextBoxDanneggiato.Text = Danneggiato.Value
    End Sub
    Protected Sub DataGridDanneggiante_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDanneggiante.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoDanneggiante) {SelezionatoDanneggiante.style.backgroundColor=''} SelezionatoDanneggiante=this;this.style.backgroundColor='orange';document.getElementById('Danneggiante').value='" & e.Item.Cells(par.IndDGC(DataGridDanneggiante, "NOMINATIVO")).Text.Replace("'", "\'") & "';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaDanneggiante').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoDanneggiante) {SelezionatoDanneggiante.style.backgroundColor=''} SelezionatoDanneggiante=this;this.style.backgroundColor='orange';document.getElementById('Danneggiante').value='" & e.Item.Cells(par.IndDGC(DataGridDanneggiante, "NOMINATIVO")).Text.Replace("'", "\'") & "';;")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaDanneggiante').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridDanneggiante_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DataGridDanneggiato_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDanneggiato.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoDanneggiato) {SelezionatoDanneggiato.style.backgroundColor=''} SelezionatoDanneggiato=this;this.style.backgroundColor='orange';document.getElementById('Danneggiato').value='" & e.Item.Cells(par.IndDGC(DataGridDanneggiato, "NOMINATIVO")).Text.Replace("'", "\'") & "';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaDanneggiato').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoDanneggiato) {SelezionatoDanneggiato.style.backgroundColor=''} SelezionatoDanneggiato=this;this.style.backgroundColor='orange';document.getElementById('Danneggiato').value='" & e.Item.Cells(par.IndDGC(DataGridDanneggiato, "NOMINATIVO")).Text.Replace("'", "\'") & "';;")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaDanneggiato').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridDanneggiato_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub ControllaNumeriUtili()
        Dim orarioInizio As String = ""
        Dim orarioFine As String = ""
        Dim fascia As String = ""
        Dim ora_attuale As String = Format(Now, "HHmm")
        For Each elemento As DataGridItem In DataGridNumeriUtili.Items
            fascia = elemento.Cells(par.IndDGC(DataGridNumeriUtili, "FASCIA")).Text.Replace("&nbsp;", "")
            If fascia = "" Then
                elemento.BackColor = Drawing.Color.LawnGreen
            Else
                orarioInizio = fascia.Substring(0, 2) & fascia.Substring(3, 2)
                orarioFine = fascia.Substring(6, 2) & fascia.Substring(9, 2)
                If orarioFine < orarioInizio Then
                    If ora_attuale <= orarioFine Then
                        elemento.BackColor = Drawing.Color.LawnGreen
                    Else
                        elemento.BackColor = Drawing.Color.White
                    End If
                Else
                    If ora_attuale >= orarioInizio And ora_attuale <= orarioFine Then
                        elemento.BackColor = Drawing.Color.LawnGreen
                    Else
                        elemento.BackColor = Drawing.Color.White
                    End If
                End If
            End If
        Next
    End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        If frmModify.Value <> "111" Then
        Response.Redirect("Home.aspx", False)
        Else
            frmModify.Value = "1"
        End If
    End Sub


    Private Sub CaricaTutteTipologie(Optional ByVal edificioIncondominio As Boolean = 0)
        Try
            Dim condizioneCustodi As String = ""
            If flCustode.Value = "1" Then
                If edificioIncondominio Then
                    condizioneCustodi = " WHERE ID_TIPO_SEGNALAZIONE=3"
                Else
                    condizioneCustodi = " WHERE ID_TIPO_SEGNALAZIONE=1"
                End If

            Else
                condizioneCustodi = " WHERE ID_TIPO_SEGNALAZIONE<>6"
            End If
            Dim condizioneEdificioInCondominio As String = ""
            If edificioIncondominio = True Then
                condizioneEdificioInCondominio = " AND ID_TIPO_sEGNALAZIONE = 3"
            End If
            Dim query As String = " SELECT  " _
                & " ID, " _
                & " (SELECT REPLACE(DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_sEGNALAZIONE)  " _
                & " || " _
                & " (CASE WHEN ID_TIPO_SEGNALAZIONE_LIVELLO_1 IS NOT NULL THEN   " _
                & " ' - '||(SELECT  REPLACE(DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_sEGNALAZIONE_LIVELLO_1) " _
                & " ELSE '' END) " _
                & " || " _
                & " (CASE WHEN ID_TIPO_SEGNALAZIONE_LIVELLO_2 IS NOT NULL THEN   " _
                & " ' - '||(SELECT  REPLACE(DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_sEGNALAZIONE_LIVELLO_2) " _
                & " ELSE '' END) " _
                & " || " _
                & " (CASE WHEN ID_TIPO_SEGNALAZIONE_LIVELLO_3 IS NOT NULL THEN   " _
                & " ' - '||(SELECT  REPLACE(DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_sEGNALAZIONE_LIVELLO_3) " _
                & " ELSE '' END) " _
                & " || " _
                & " (CASE WHEN ID_TIPO_SEGNALAZIONE_LIVELLO_4 IS NOT NULL THEN   " _
                & " ' - '||(SELECT  REPLACE(DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_sEGNALAZIONE_LIVELLO_4) " _
                & " ELSE '' END) " _
                & " AS DESCRIZIONE " _
                & " FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                & condizioneCustodi _
                & " AND (ID_TIPO_SEGNALAZIONE_LIVELLO_1 IS NULL OR ID_TIPO_SEGNALAZIONE_LIVELLO_1>=1000) " _
                & condizioneEdificioInCondominio _
                & " ORDER BY 2 ASC "

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboTelerik(query, DropDownListTipologia, "ID", "DESCRIZIONE", True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - caricaTutteTipologie - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello3_SelectedIndexChanged1(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello3.SelectedIndexChanged
        DropDownListTipologia.SelectedValue = "-1"
        CaricaTipologieLivello4()
        caricaUrgenzaPredefinita()
        CaricaListaDocumentiDaPortare()
        CaricaSegnalazioniEdificioSelezionato(True)
        CaricaSegnalazioniUnitaSelezionata(True)
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello4.SelectedIndexChanged
        DropDownListTipologia.SelectedValue = "-1"
        caricaUrgenzaPredefinita()
        CaricaListaDocumentiDaPortare()
        CaricaSegnalazioniEdificioSelezionato(True)
        CaricaSegnalazioniUnitaSelezionata(True)
    End Sub

    Private Sub caricaUrgenzaPredefinita()
        Try
            Dim condTipologia As String = ""
            Dim condTipologia1 As String = ""
            Dim condTipologia2 As String = ""
            Dim condTipologia3 As String = ""
            Dim condTipologia4 As String = ""
            Dim noteCC As String = ""
            Dim tipo0 As Integer = -1
            Dim tipo1 As Integer = -1
            Dim tipo2 As Integer = -1
            Dim tipo3 As Integer = -1
            Dim tipo4 As Integer = -1
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            If DropDownListTipologia.SelectedValue.ToString <> "-1" And DropDownListTipologia.SelectedValue.ToString <> "" Then

                par.cmd.CommandText = " SELECT ID_TIPO_sEGNALAZIONE, " _
                    & " ID_TIPO_SEGNALAZIONE_LIVELLO_1, " _
                    & " ID_TIPO_SEGNALAZIONE_LIVELLO_2, " _
                    & " ID_TIPO_SEGNALAZIONE_LIVELLO_3, " _
                    & " ID_TIPO_SEGNALAZIONE_LIVELLO_4  " _
                    & " FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE WHERE ID=" & DropDownListTipologia.SelectedValue
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    tipo0 = par.IfNull(lettore("ID_TIPO_sEGNALAZIONE"), -1)
                    tipo1 = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE_LIVELLO_1"), -1)
                    tipo2 = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE_LIVELLO_2"), -1)
                    tipo3 = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE_LIVELLO_3"), -1)
                    tipo4 = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE_LIVELLO_4"), -1)
                End If
                lettore.Close()

                If tipo0 <> -1 Then
                    CaricaTipologieLivello0()
                    cmbTipoSegnalazioneLivello0.SelectedValue = tipo0
                    cmbTipoSegnalazioneLivello0.Visible = True
                End If
                If tipo1 <> -1 Then
                    CaricaTipologieLivello1()
                    cmbTipoSegnalazioneLivello1.SelectedValue = tipo1
                    cmbTipoSegnalazioneLivello1.Visible = True
                End If
                If tipo2 <> -1 Then
                    CaricaTipologieLivello2()
                    cmbTipoSegnalazioneLivello2.SelectedValue = tipo2
                    cmbTipoSegnalazioneLivello2.Visible = True
                End If
                If tipo3 <> -1 Then
                    CaricaTipologieLivello3()
                    cmbTipoSegnalazioneLivello3.SelectedValue = tipo3
                    cmbTipoSegnalazioneLivello3.Visible = True
                End If
                If tipo4 <> -1 Then
                    CaricaTipologieLivello4()
                    cmbTipoSegnalazioneLivello4.SelectedValue = tipo4
                    cmbTipoSegnalazioneLivello4.Visible = True
                End If

                If cmbTipoSegnalazioneLivello0.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello0.SelectedValue <> "" Then
                    condTipologia = "=" & cmbTipoSegnalazioneLivello0.SelectedValue
                Else
                    condTipologia = " IS NULL "
                End If


                If cmbTipoSegnalazioneLivello0.SelectedValue = "6" Or cmbTipoSegnalazioneLivello0.SelectedValue = "1" Then
                    If cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello1.SelectedValue <> "" Then
                        condTipologia1 = "=" & cmbTipoSegnalazioneLivello1.SelectedValue
                    Else
                        condTipologia1 = " IS NULL "
                    End If
                    If cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello2.SelectedValue <> "" Then
                        condTipologia2 = "=" & cmbTipoSegnalazioneLivello2.SelectedValue
                    Else
                        condTipologia2 = " IS NULL "
                    End If
                    If cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello3.SelectedValue <> "" Then
                        condTipologia3 = "=" & cmbTipoSegnalazioneLivello3.SelectedValue
                    Else
                        condTipologia3 = " IS NULL "
                    End If
                    If cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello4.SelectedValue <> "" Then
                        condTipologia4 = "=" & cmbTipoSegnalazioneLivello4.SelectedValue
                    Else
                        condTipologia4 = " IS NULL "
                    End If

                    par.cmd.CommandText = " SELECT ORARIO_UFFICIO,FUORI_ORARIO_UFFICIO1,FUORI_ORARIO_UFFICIO2 " _
                        & " FROM SISCOM_MI.SEMAFORO " _
                        & " WHERE ID_TIPOLOGIA_SEGNALAZIONE " & condTipologia _
                        & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV1 " & condTipologia1 _
                        & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV2 " & condTipologia2 _
                        & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV3 " & condTipologia3 _
                        & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV4 " & condTipologia4
                    lettore = par.cmd.ExecuteReader
                    Dim orarioUfficio As Integer = -1
                    Dim fuoriorarioUfficio1 As Integer = -1
                    Dim fuoriOrarioUfficio2 As Integer = -1
                    If lettore.Read Then
                        orarioUfficio = par.IfNull(lettore("ORARIO_UFFICIO"), -1)
                        fuoriorarioUfficio1 = par.IfNull(lettore("FUORI_ORARIO_UFFICIO1"), -1)
                        fuoriOrarioUfficio2 = par.IfNull(lettore("FUORI_ORARIO_UFFICIO2"), -1)
                    End If
                    lettore.Close()

                    Dim urgenza As Integer = 0
                    Dim dataOggi As Date = Now
                    If (CInt(dataOggi.DayOfWeek) >= 1 And CInt(dataOggi.DayOfWeek) < 5 _
                        And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") >= "0830" _
                        And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") <= "16.45") _
                    Or (CInt(dataOggi.DayOfWeek) = 5 _
                        And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") >= "0830" _
                        And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") <= "16.30") Then
                        urgenza = orarioUfficio
                    ElseIf CInt(dataOggi.DayOfWeek) = 6 Or CInt(dataOggi.DayOfWeek) = 7 _
                        Or (CInt(dataOggi.DayOfWeek) = 1 And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") < "0830") _
                        Or (CInt(dataOggi.DayOfWeek) = 5 And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") > "16.30") Then
                        urgenza = fuoriOrarioUfficio2
                    Else
                        urgenza = fuoriorarioUfficio1
                    End If
                    If urgenza <> -1 Then
                        Select Case urgenza
                            Case 1
                                cmbUrgenza.SelectedValue = "Bianco"
                            Case 2
                                cmbUrgenza.SelectedValue = "Verde"
                            Case 3
                                cmbUrgenza.SelectedValue = "Giallo"
                            Case 4
                                cmbUrgenza.SelectedValue = "Rosso"
                            Case 0
                                cmbUrgenza.SelectedValue = "Blu"
                        End Select
                        'PanelUrgenzaCriticita.Visible = True
                    Else
                        'PanelUrgenzaCriticita.Visible = False
                    End If

                Else
                    'PanelUrgenzaCriticita.Visible = False
                End If

            Else
                If cmbTipoSegnalazioneLivello0.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello0.SelectedValue <> "" Then
                    condTipologia = "=" & cmbTipoSegnalazioneLivello0.SelectedValue
                Else
                    condTipologia = " IS NULL "
                End If

                If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
                    If cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello1.SelectedValue <> "" Then
                        condTipologia1 = "=" & cmbTipoSegnalazioneLivello1.SelectedValue
                    Else
                        condTipologia1 = " IS NULL "
                    End If
                    If cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello2.SelectedValue <> "" Then
                        condTipologia2 = "=" & cmbTipoSegnalazioneLivello2.SelectedValue
                    Else
                        condTipologia2 = " IS NULL "
                    End If
                    If cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello3.SelectedValue <> "" Then
                        condTipologia3 = "=" & cmbTipoSegnalazioneLivello3.SelectedValue
                    Else
                        condTipologia3 = " IS NULL "
                    End If
                    If cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello4.SelectedValue <> "" Then
                        condTipologia4 = "=" & cmbTipoSegnalazioneLivello4.SelectedValue
                    Else
                        condTipologia4 = " IS NULL "
                    End If

                    par.cmd.CommandText = " SELECT ORARIO_UFFICIO,FUORI_ORARIO_UFFICIO1,FUORI_ORARIO_UFFICIO2 " _
                        & " FROM SISCOM_MI.SEMAFORO " _
                        & " WHERE ID_TIPOLOGIA_SEGNALAZIONE " & condTipologia _
                        & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV1 " & condTipologia1 _
                        & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV2 " & condTipologia2 _
                        & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV3 " & condTipologia3 _
                        & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV4 " & condTipologia4
                    lettore = par.cmd.ExecuteReader
                    Dim orarioUfficio As Integer = -1
                    Dim fuoriorarioUfficio1 As Integer = -1
                    Dim fuoriOrarioUfficio2 As Integer = -1
                    If lettore.Read Then
                        orarioUfficio = par.IfNull(lettore("ORARIO_UFFICIO"), -1)
                        fuoriorarioUfficio1 = par.IfNull(lettore("FUORI_ORARIO_UFFICIO1"), -1)
                        fuoriOrarioUfficio2 = par.IfNull(lettore("FUORI_ORARIO_UFFICIO2"), -1)
                    End If
                    lettore.Close()

                    If orarioUfficio <> -1 Then
                        Select Case orarioUfficio
                            Case 1
                                cmbUrgenza.SelectedValue = "Bianco"
                            Case 2
                                cmbUrgenza.SelectedValue = "Verde"
                            Case 3
                                cmbUrgenza.SelectedValue = "Giallo"
                            Case 4
                                cmbUrgenza.SelectedValue = "Rosso"
                            Case 0
                                cmbUrgenza.SelectedValue = "Blu"
                        End Select
                        'PanelUrgenzaCriticita.Visible = True
                    Else
                        'PanelUrgenzaCriticita.Visible = False
                    End If
                Else
                    'PanelUrgenzaCriticita.Visible = False
                End If
            End If


            If cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello1.SelectedValue <> "" Then
                condTipologia1 = "=" & cmbTipoSegnalazioneLivello1.SelectedValue
            Else
                condTipologia1 = " IS NULL "
            End If
            If cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello2.SelectedValue <> "" Then
                condTipologia2 = "=" & cmbTipoSegnalazioneLivello2.SelectedValue
            Else
                condTipologia2 = " IS NULL "
            End If
            If cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello3.SelectedValue <> "" Then
                condTipologia3 = "=" & cmbTipoSegnalazioneLivello3.SelectedValue
            Else
                condTipologia3 = " IS NULL "
            End If
            If cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello4.SelectedValue <> "" Then
                condTipologia4 = "=" & cmbTipoSegnalazioneLivello4.SelectedValue
            Else
                condTipologia4 = " IS NULL "
            End If

            par.cmd.CommandText = " SELECT NOTE_1 " _
                        & " FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                        & " WHERE ID_TIPO_SEGNALAZIONE " & condTipologia _
                        & " AND ID_TIPO_SEGNALAZIONE_LIVELLO_1 " & condTipologia1 _
                        & " AND ID_TIPO_SEGNALAZIONE_LIVELLO_2 " & condTipologia2 _
                        & " AND ID_TIPO_SEGNALAZIONE_LIVELLO_3 " & condTipologia3 _
                        & " AND ID_TIPO_SEGNALAZIONE_LIVELLO_4 " & condTipologia4
            NOTE_CC.Text = par.IfNull(par.cmd.ExecuteScalar, "")

            If connAperta = True Then
                connData.chiudi(False)
            End If
            If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
                PanelUrgenzaCriticita.Visible = True
            Else
                PanelUrgenzaCriticita.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - caricaUrgenzaPredefinita - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DropDownListTipologia_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListTipologia.SelectedIndexChanged
        caricaUrgenzaPredefinita()
        CaricaListaDocumentiDaPortare()
        CaricaSegnalazioniEdificioSelezionato(True)
        CaricaSegnalazioniUnitaSelezionata(True)
        CaricaNumeriUtili()
    End Sub

    Protected Sub ImageButtonCopiaDati_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonCopiaDati.Click
        Dim connAperta As Boolean = False
        If IsNumeric(idSelectedChiamante.Value) Then
            Try
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                'Svuota(False)
                Dim idUnitaSelezionata As String = idSelectedChiamante.Value
                If IsNumeric(idUnitaSelezionata) Then
                    Dim condChiamante As String = ""
                    If IsNumeric(idContrattoChiamante.Value) Then
                        condChiamante = " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=" & idContrattoChiamante.Value
                    End If
                    par.cmd.CommandText = "SELECT (CASE WHEN (RAGIONE_SOCIALE IS NULL) THEN (COGNOME) ELSE (RAGIONE_SOCIALE) END) AS COGNOME_INTESTATARIO, " _
                        & " (CASE WHEN (RAGIONE_SOCIALE IS NULL) THEN (NOME) ELSE (NULL) END) AS NOME_INTESTATARIO, " _
                        & " RAPPORTI_UTENZA.COD_CONTRATTO, " _
                        & " RAPPORTI_UTENZA.ID AS ID_CONTRATTO, " _
                        & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                        & " (SELECT DESCRIZIONE FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO) AS INDIRIZZO " _
                        & " FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_IMMOBILIARI " _
                        & " WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                        & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                        & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                        & " AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA " _
                        & " AND UNITA_CONTRATTUALE.ID_UNITA= " & idUnitaSelezionata _
                        & condChiamante _
                        & " /*AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL*/ " _
                        & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' "
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TextBoxCognomeIntestatario.Text = par.IfNull(lettore("COGNOME_INTESTATARIO"), "")
                        TextBoxNomeIntestatario.Text = par.IfNull(lettore("NOME_INTESTATARIO"), "")
                        TextBoxCodiceContrattoIntestatario.Text = par.IfNull(lettore("COD_CONTRATTO"), "")
                        TextBoxCodiceUnitaImmobiliare.Text = par.IfNull(lettore("COD_UNITA_IMMOBILIARE"), "")
                        TextBoxIndirizzoIntestatario.Text = par.IfNull(lettore("INDIRIZZO"), "")
                        idContrattoIntestatario.Value = par.IfNull(lettore("ID_CONTRATTO"), "")
                    End If
                    lettore.Close()
                    idSelectedIntestatario.Value = idSelectedChiamante.Value
                    txtContrattoChiamante.Text = TextBoxCodiceContrattoIntestatario.Text
                End If
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ImageButtonCopiaDati_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
            CaricaComplessi()
            CaricaComplessiChiamante()
            CaricaEdifici()
            CaricaEdificiChiamante()
            CaricaScale()
            CaricaScaleChiamante()
            CaricaPiano()
            CaricaPianoChiamante()
            CaricaInterni()
            CaricaInterniChiamante()
            CaricaSedeTerritoriale()
            CaricaSegnalazioniEdificioSelezionato()
            CaricaSegnalazioniUnitaSelezionata()
            ControllaCondominio()
            ControllaMoroso()
            CaricaNumeriUtili()
            If DropDownListEdificio.SelectedValue <> "-1" Then
                panelDann.Visible = True
            Else
                panelDann.Visible = False
                TextBoxDanneggiante.Text = ""
                TextBoxDanneggiato.Text = ""
                Danneggiante.Value = ""
                Danneggiato.Value = ""
            End If
            'Else
            '    par.modalDialogMessage("Nuova segnalazione", "Selezionare un nominativo.", Page, "info")
            '    VisualizzaViewRicercaChiamante()
        End If
        If flCustode.Value = "1" Then
            CaricaComplessi()
            CaricaComplessiChiamante()
            CaricaEdifici()
            CaricaEdificiChiamante()
            CaricaScale()
            CaricaScaleChiamante()
            CaricaPiano()
            CaricaPianoChiamante()
            CaricaInterni()
            CaricaInterniChiamante()
            CaricaSedeTerritoriale()
            CaricaSegnalazioniEdificioSelezionato()
            CaricaSegnalazioniUnitaSelezionata()
            ControllaCondominio()
            ControllaMoroso()
            CaricaNumeriUtili()
            If DropDownListEdificio.SelectedValue <> "-1" Then
                panelDann.Visible = True
            Else
                panelDann.Visible = False
                TextBoxDanneggiante.Text = ""
                TextBoxDanneggiato.Text = ""
                Danneggiante.Value = ""
                Danneggiato.Value = ""
            End If
        End If
        If connAperta = True Then
            connData.chiudi(False)
        End If
    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        If RadioButtonList1.SelectedValue = 0 Then
            'alloggio
            TextBoxCognomeIntestatario.Style.Add("display", "block")
            TextBoxNomeIntestatario.Style.Add("display", "block")
            TextBoxCodiceContrattoIntestatario.Style.Add("display", "block")
            TextBoxCodiceUnitaImmobiliare.Style.Add("display", "block")
            DropDownListScala.Style.Add("display", "block")
            DropDownListPiano.Style.Add("display", "block")
            DropDownListInterno.Style.Add("display", "block")
            ImageButtonCercaIntestatario.Style.Add("display", "block")
            lblRagioneSocialeIntestario.Style.Add("display", "block")
            lblIntestatario.Style.Add("display", "block")
            lblCodiceContrattoIntestatario.Style.Add("display", "block")
            lblCodiceUnitaIntestatario.Style.Add("display", "block")
            lblScala.Style.Add("display", "block")
            lblPiano.Style.Add("display", "block")
            lblInterno.Style.Add("display", "block")
            lblAbusivo.Style.Add("display", "block")
            lblAbusivoSiNo.Style.Add("display", "block")
            lblMoroso.Style.Add("display", "block")
            lblMorosoSiNo.Style.Add("display", "block")
            PanelSegnalazioniUnita.Visible = True
            PanelSegnalazioniEdificio.Visible = False
        Else
            'parte comune
            TextBoxCognomeIntestatario.Style.Add("display", "none")
            TextBoxNomeIntestatario.Style.Add("display", "none")
            TextBoxCodiceContrattoIntestatario.Style.Add("display", "none")
            TextBoxCodiceUnitaImmobiliare.Style.Add("display", "none")
            DropDownListScala.Style.Add("display", "none")
            DropDownListPiano.Style.Add("display", "none")
            DropDownListInterno.Style.Add("display", "none")
            ImageButtonCercaIntestatario.Style.Add("display", "none")
            lblRagioneSocialeIntestario.Style.Add("display", "none")
            lblIntestatario.Style.Add("display", "none")
            lblCodiceContrattoIntestatario.Style.Add("display", "none")
            lblCodiceUnitaIntestatario.Style.Add("display", "none")
            lblScala.Style.Add("display", "none")
            lblPiano.Style.Add("display", "none")
            lblInterno.Style.Add("display", "none")
            lblAbusivo.Style.Add("display", "none")
            lblAbusivoSiNo.Style.Add("display", "none")
            lblMoroso.Style.Add("display", "none")
            lblMorosoSiNo.Style.Add("display", "none")
            PanelSegnalazioniUnita.Visible = False
            PanelSegnalazioniEdificio.Visible = True
        End If
    End Sub

    Protected Sub ButtonUnisciSegnalazioni_Click(sender As Object, e As System.EventArgs) Handles ButtonUnisciSegnalazioni.Click
        Session.Item("listaSelezioneSegnUnita") = ""
        Dim cont As Integer = 0
        For Each Items As GridDataItem In RadDataGridSegnalazioniUnitaSelezionata.Items
            If CType(Items.FindControl("CheckBoxSegnalazioni"), CheckBox).Checked = True Then
                cont += 1
            End If
        Next
        If cont > 0 Then
            par.modalDialogConfirm("Agenda e Segnalazioni", "Vuoi unire le segnalazioni selezionate?", "Ok", "document.getElementById('ButtonUnisciSegnalazioni1').click();", "Annulla", "", Page)
        Else
            par.modalDialogMessage("Agenda e Segnalazioni", "Selezionare almeno una segnalazione", Me.Page, "info")
        End If
    End Sub

    Protected Sub CheckBoxTutteSegnalazioni_CheckedChanged(sender As Object, e As System.EventArgs)
        If selezionateTutte.Value = "0" Then
            For Each item As GridDataItem In RadDataGridSegnalazioniUnitaSelezionata.Items
                CType(item.FindControl("CheckBoxSegnalazioni"), CheckBox).Checked = True
            Next
            selezionateTutte.Value = "1"
        Else
            For Each item As GridDataItem In RadDataGridSegnalazioniUnitaSelezionata.Items
                CType(item.FindControl("CheckBoxSegnalazioni"), CheckBox).Checked = False
            Next
            selezionateTutte.Value = "0"
        End If
    End Sub

    Protected Sub CheckBoxTutteSegnalazioniEdificio_CheckedChanged(sender As Object, e As System.EventArgs)
        If selezionateTutte.Value = "0" Then
            For Each item As GridDataItem In RadDataGridSegnalazioniEdificioSelezionato.Items
                CType(item.FindControl("CheckBoxSegnalazioni"), CheckBox).Checked = True
            Next
            selezionateTutte.Value = "1"
        Else
            For Each item As GridDataItem In RadDataGridSegnalazioniEdificioSelezionato.Items
                CType(item.FindControl("CheckBoxSegnalazioni"), CheckBox).Checked = False
            Next
            selezionateTutte.Value = "0"
        End If
    End Sub

    Protected Sub ButtonUnisciSegnalazioni1_Click(sender As Object, e As System.EventArgs) Handles ButtonUnisciSegnalazioni1.Click
        Dim listaSegnalazioni As String = ""
        Dim contaPadri As Integer = 0
        For Each Items As GridDataItem In RadDataGridSegnalazioniUnitaSelezionata.Items
            If CType(Items.FindControl("CheckBoxSegnalazioni"), CheckBox).Checked = True Then
                If listaSegnalazioni = "" Then
                    listaSegnalazioni = Items("ID").Text
                    If Items("FIGLI").Text > 0 Then
                        contaPadri += 1
                    End If
                Else
                    listaSegnalazioni &= "," & Items("ID").Text
                    If Items("FIGLI").Text > 0 Then
                        contaPadri += 1
                    End If
                End If
            End If
        Next
        If contaPadri > 1 Then
            Session.Item("listaSelezioneSegnUnita") = ""
            par.modalDialogMessage("Agenda e Segnalazioni", "E\' possibile selezionare al massimo un ticket ""padre""", Me.Page, "info")
        Else
            Session.Item("listaSelezioneSegnUnita") = listaSegnalazioni
        End If
        If Session.Item("listaSelezioneSegnUnita") <> "" Then
            lblSegnalazioniUnitaDaunire.Text = "Lista segnalazioni selezionate: " & Session.Item("listaSelezioneSegnUnita")
        End If
    End Sub

    Protected Sub ButtonUnisciSegnalazioniEdifici_Click(sender As Object, e As System.EventArgs) Handles ButtonUnisciSegnalazioniEdifici.Click
        Session.Item("listaSelezioneSegnEdificio") = ""
        Dim cont As Integer = 0
        For Each Items As GridDataItem In RadDataGridSegnalazioniEdificioSelezionato.Items
            If CType(Items.FindControl("CheckBoxSegnalazioni"), CheckBox).Checked = True Then
                cont += 1
            End If
        Next
        If cont > 0 Then
            par.modalDialogConfirm("Agenda e Segnalazioni", "Vuoi unire le segnalazioni selezionate?", "Ok", "document.getElementById('ButtonUnisciSegnalazioniEdifici2').click();", "Annulla", "", Page)
        Else
            par.modalDialogMessage("Agenda e Segnalazioni", "Selezionare almeno una segnalazione", Me.Page, "info")
        End If
    End Sub

    Protected Sub ButtonUnisciSegnalazioniEdifici2_Click(sender As Object, e As System.EventArgs) Handles ButtonUnisciSegnalazioniEdifici2.Click
        Dim listaSegnalazioni As String = ""
        Dim contaPadri As Integer = 0
        For Each Items As GridDataItem In RadDataGridSegnalazioniEdificioSelezionato.Items
            If CType(Items.FindControl("CheckBoxSegnalazioni"), CheckBox).Checked = True Then
                If listaSegnalazioni = "" Then
                    listaSegnalazioni = Items("ID").Text
                    If Items("FIGLI").Text > 0 Then
                        contaPadri += 1
                    End If
                Else
                    listaSegnalazioni &= "," & Items("ID").Text
                    If Items("FIGLI").Text > 0 Then
                        contaPadri += 1
                    End If
                End If
            End If
        Next
        If contaPadri > 1 Then
            Session.Item("listaSelezioneSegnEdificio") = ""
            par.modalDialogMessage("Agenda e Segnalazioni", "E\' possibile selezionare al massimo un ticket ""padre""", Me.Page, "info")
        Else
            Session.Item("listaSelezioneSegnEdificio") = listaSegnalazioni
        End If
        If Session.Item("listaSelezioneSegnEdificio") <> "" Then
            lblSegnalazioniEdificiDaUnire.Text = "Lista segnalazioni selezionate: " & Session.Item("listaSelezioneSegnEdificio")
        End If
    End Sub

    Protected Sub ButtonNuovaSegnalazioneCustode_Click(sender As Object, e As System.EventArgs) Handles ButtonNuovaSegnalazioneCustode.Click
        If CheckControl() = True Then
            Try

                Dim STATOS As String = "0"
                Dim ORIGINE As String = "A"
                Dim idSegnal As String = "-1"
                If idSegnal = "-1" Then
                    connData.apri(True)
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_SEGNALAZIONI.NEXTVAL FROM DUAL"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        idSegnal = par.IfNull(lettore(0), "-1")
                    End If
                    lettore.Close()
                    Dim valoreUrgenza As String = "NULL"
                    If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
                        valoreUrgenza = cmbUrgenza.SelectedIndex
                    End If
                    Dim edificio As String = "NULL"
                    If DropDownListEdificio.SelectedValue <> "-1" Then
                        edificio = DropDownListEdificio.SelectedValue
                    End If
                    Dim unita As String = "NULL"
                    Dim contratto As String = "NULL"
                    If IsNumeric(idSelectedIntestatario.Value) AndAlso idSelectedIntestatario.Value <> "-1" Then
                        unita = idSelectedIntestatario.Value
                        If IsNumeric(idContrattoIntestatario.Value) Then
                            contratto = idContrattoIntestatario.Value
                        End If
                    Else
                        If IsNumeric(idSelectedChiamante.Value) AndAlso idSelectedChiamante.Value <> "-1" Then
                            unita = idSelectedChiamante.Value
                            If IsNumeric(idContrattoChiamante.Value) Then
                                contratto = idContrattoChiamante.Value
                            End If
                        End If
                    End If
                    Dim struttura As String = "NULL"
                    If DropDownListSedeTerritoriale.SelectedValue <> "-1" And DropDownListSedeTerritoriale.SelectedValue <> "" Then
                        struttura = DropDownListSedeTerritoriale.SelectedValue
                    End If
                    Dim segnalazioneLivello0 As String = "NULL"
                    If IsNumeric(cmbTipoSegnalazioneLivello0.SelectedValue) AndAlso cmbTipoSegnalazioneLivello0.SelectedValue <> "-1" Then
                        segnalazioneLivello0 = cmbTipoSegnalazioneLivello0.SelectedValue
                    End If
                    Dim segnalazioneLivello1 As String = "NULL"
                    If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
                        segnalazioneLivello1 = cmbTipoSegnalazioneLivello1.SelectedValue
                    End If
                    Dim segnalazioneLivello2 As String = "NULL"
                    If IsNumeric(cmbTipoSegnalazioneLivello2.SelectedValue) AndAlso cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" Then
                        segnalazioneLivello2 = cmbTipoSegnalazioneLivello2.SelectedValue
                    End If
                    Dim segnalazioneLivello3 As String = "NULL"
                    If IsNumeric(cmbTipoSegnalazioneLivello3.SelectedValue) AndAlso cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" Then
                        segnalazioneLivello3 = cmbTipoSegnalazioneLivello3.SelectedValue
                    End If
                    Dim segnalazioneLivello4 As String = "NULL"
                    If IsNumeric(cmbTipoSegnalazioneLivello4.SelectedValue) AndAlso cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" Then
                        segnalazioneLivello4 = cmbTipoSegnalazioneLivello4.SelectedValue
                    End If

                    Dim custode As String = "0"
                    If idSelectedChiamante.Value = "C" Then
                        custode = "1"
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI(ID," _
                        & " ID_STATO," _
                        & " ID_EDIFICIO," _
                        & " ID_UNITA," _
                        & " COGNOME_RS," _
                        & " DATA_ORA_RICHIESTA," _
                        & " TELEFONO1," _
                        & " TELEFONO2," _
                        & " MAIL," _
                        & " DESCRIZIONE_RIC," _
                        & " ID_OPERATORE_INS," _
                        & " NOME," _
                        & " TIPO_RICHIESTA," _
                        & " ORIGINE," _
                        & " ID_TIPOLOGIE," _
                        & " TIPO_PERVENUTA," _
                        & " ID_STRUTTURA," _
                        & " ID_CONTRATTO," _
                        & " ID_PERICOLO_SEGNALAZIONE," _
                        & " ID_PERICOLO_SEGNALAZIONE_INIZ," _
                        & " ID_TIPO_SEGNALAZIONE," _
                        & " ID_TIPO_SEGN_LIVELLO_1," _
                        & " ID_TIPO_SEGN_LIVELLO_2," _
                        & " ID_TIPO_SEGN_LIVELLO_3," _
                        & " ID_TIPO_SEGN_LIVELLO_4," _
                        & " DANNEGGIANTE," _
                        & " DANNEGGIATO," _
                        & " FL_CUSTODE," _
                        & " ID_TIPOLOGIA_SEGNALANTE " _
                        & " ) " _
                        & " VALUES " _
                        & "(" & idSegnal & "," _
                        & STATOS & "," _
                        & DropDownListEdificio.SelectedValue & "," _
                        & unita & "," _
                        & "'" & par.PulisciStrSql(TextBoxCognomeChiamante.Text.ToUpper) & "'," _
                        & "'" & Format(Now, "yyyyMMddHHmm") & "'," _
                        & "'" & par.PulisciStrSql(TextBoxTelefono1Chiamante.Text) & "'," _
                        & "'" & par.PulisciStrSql(TextBoxTelefono2Chiamante.Text) & "'," _
                        & "'" & LCase(par.PulisciStrSql(TextBoxEmailChiamante.Text)) & "'," _
                        & "'" & par.PulisciStrSql(txtDescrizione.Text) & "'," _
                        & Session.Item("ID_OPERATORE") & "," _
                        & "'" & par.PulisciStrSql(TextBoxNomeChiamante.Text.ToUpper) & "'," _
                        & "1" & ",'" & ORIGINE & "',NULL," _
                        & "'Telefonica', " _
                        & struttura & "," _
                        & contratto & "," _
                        & valoreUrgenza & "," _
                        & valoreUrgenza & "," _
                        & segnalazioneLivello0 & "," _
                        & segnalazioneLivello1 & "," _
                        & segnalazioneLivello2 & "," _
                        & segnalazioneLivello3 & "," _
                        & segnalazioneLivello4 & "," _
                        & "'" & par.PulisciStrSql(TextBoxDanneggiante.Text) & "'," _
                        & "'" & par.PulisciStrSql(TextBoxDanneggiato.Text) & "', " _
                        & custode & "," _
                        & cmbTipologiaSegnalante.SelectedValue & " " _
                        & ")"
                    par.cmd.ExecuteNonQuery()
                    idSegnalazione.Value = idSegnal
                    '************ AGGIORNAMENTO CHIAMANTI ****************
                    If idSelectedChiamante.Value = "C" Then
                        If IsNumeric(idAnagraficaChiamante.Value) Then
                            par.cmd.CommandText = "SELECT EMAIL_AZIENDALE, " _
                                & " CELLULARE_AZIENDALE," _
                                & " TELEFONO_AZIENDALE " _
                                & " FROM SISCOM_MI.ANAGRAFICA_CUSTODI " _
                                & " WHERE ID = " & idAnagraficaChiamante.Value.ToString
                            Dim lettoreAna As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            Dim oldEmail As String = ""
                            Dim oldTelefono As String = ""
                            Dim oldCellulare As String = ""
                            If lettoreAna.Read Then
                                oldEmail = par.IfNull(lettoreAna("email_aziendale"), "")
                                oldTelefono = par.IfNull(lettoreAna("TELEFONO_AZIENDALE"), "")
                                oldCellulare = par.IfNull(lettoreAna("CELLULARE_AZIENDALE"), "")
                            End If
                            lettoreAna.Close()
                            If oldEmail <> Trim(TextBoxEmailChiamante.Text) Or oldCellulare <> Trim(TextBoxTelefono2Chiamante.Text) Or oldTelefono <> Trim(TextBoxTelefono1Chiamante.Text) Then
                                par.cmd.CommandText = " UPDATE SISCOM_MI.ANAGRAFICA_CUSTODI " _
                                    & " SET " _
                                    & " EMAIL_AZIENDALE = '" & LCase(par.PulisciStrSql(TextBoxEmailChiamante.Text)) & "', " _
                                    & " CELLULARE_AZIENDALE = '" & par.PulisciStrSql(TextBoxTelefono2Chiamante.Text) & "', " _
                                    & " TELEFONO_AZIENDALE = '" & par.PulisciStrSql(TextBoxTelefono1Chiamante.Text) & "'" _
                                    & " WHERE  ID = " & idAnagraficaChiamante.Value.ToString
                                par.cmd.ExecuteNonQuery()
                                WriteEvent("F251", "Modifica dati anagrafica custodi: Email da " & oldEmail & " a " & TextBoxEmailChiamante.Text & " - Telefono da " & oldTelefono & " a " & TextBoxTelefono1Chiamante.Text & " - Cellulare da " & oldCellulare & " a " & TextBoxTelefono2Chiamante.Text)
                            End If
                        End If
                    ElseIf idSelectedChiamante.Value = "NN" Then
                        If IsNumeric(idAnagraficaChiamante.Value) Then
                            par.cmd.CommandText = "SELECT EMAIL, " _
                                & " CELLULARE," _
                                & " TELEFONO" _
                                & " FROM SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI " _
                                & " WHERE ID = " & idAnagraficaChiamante.Value.ToString
                            Dim lettoreAna As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            Dim oldEmail As String = ""
                            Dim oldTelefono As String = ""
                            Dim oldCellulare As String = ""
                            If lettoreAna.Read Then
                                oldEmail = par.IfNull(lettoreAna("email"), "")
                                oldTelefono = par.IfNull(lettoreAna("TELEFONO"), "")
                                oldCellulare = par.IfNull(lettoreAna("CELLULARE"), "")
                            End If
                            lettoreAna.Close()
                            If oldEmail <> Trim(TextBoxEmailChiamante.Text) Or oldCellulare <> Trim(TextBoxTelefono2Chiamante.Text) Or oldTelefono <> Trim(TextBoxTelefono1Chiamante.Text) Then
                                par.cmd.CommandText = " UPDATE SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI " _
                                    & " SET " _
                                    & " EMAIL= '" & LCase(par.PulisciStrSql(TextBoxEmailChiamante.Text)) & "', " _
                                    & " CELLULARE = '" & par.PulisciStrSql(TextBoxTelefono2Chiamante.Text) & "', " _
                                    & " TELEFONO = '" & par.PulisciStrSql(TextBoxTelefono1Chiamante.Text) & "'" _
                                    & " WHERE  ID = " & idAnagraficaChiamante.Value.ToString
                                par.cmd.ExecuteNonQuery()
                                WriteEvent("F251", "Modifica dati anagrafica chiamanti non noti: Email da " & oldEmail & " a " & TextBoxEmailChiamante.Text & " - Telefono da " & oldTelefono & " a " & TextBoxTelefono1Chiamante.Text & " - Cellulare da " & oldCellulare & " a " & TextBoxTelefono2Chiamante.Text)
                            End If
                        End If
                    Else
                        If IsNumeric(idAnagraficaChiamante.Value) Then
                            par.cmd.CommandText = "SELECT EMAIL, " _
                                & " TELEFONO," _
                                & " TELEFONO_R" _
                                & " FROM SISCOM_MI.ANAGRAFICA " _
                                & " WHERE ID = " & idAnagraficaChiamante.Value.ToString
                            Dim lettoreAna As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            Dim oldEmail As String = ""
                            Dim oldTelefono As String = ""
                            Dim oldCellulare As String = ""
                            If lettoreAna.Read Then
                                oldEmail = par.IfNull(lettoreAna("EMAIL"), "")
                                oldTelefono = par.IfNull(lettoreAna("TELEFONO"), "")
                                oldCellulare = par.IfNull(lettoreAna("TELEFONO_R"), "")
                            End If
                            lettoreAna.Close()
                            If oldEmail <> Trim(TextBoxEmailChiamante.Text) Or oldCellulare <> Trim(TextBoxTelefono2Chiamante.Text) Or oldTelefono <> Trim(TextBoxTelefono1Chiamante.Text) Then
                                par.cmd.CommandText = " UPDATE SISCOM_MI.ANAGRAFICA " _
                                    & " SET " _
                                    & " EMAIL= '" & LCase(par.PulisciStrSql(TextBoxEmailChiamante.Text)) & "', " _
                                    & " TELEFONO_R = '" & par.PulisciStrSql(TextBoxTelefono2Chiamante.Text) & "', " _
                                    & " TELEFONO = '" & par.PulisciStrSql(TextBoxTelefono1Chiamante.Text) & "'" _
                                    & " WHERE  ID = " & idAnagraficaChiamante.Value.ToString
                                par.cmd.ExecuteNonQuery()
                                WriteEvent("F251", "Modifica dati anagrafica: Email da " & oldEmail & " a " & TextBoxEmailChiamante.Text & " - Telefono da " & oldTelefono & " a " & TextBoxTelefono1Chiamante.Text & " - Cellulare da " & oldCellulare & " a " & TextBoxTelefono2Chiamante.Text)
                            End If
                        End If
                    End If
                    If idAnagraficaChiamante.Value = "" Then
                        If Trim(TextBoxCognomeChiamante.Text) <> "" Then
                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI ( " _
                            & " ID, COGNOME, NOME,  " _
                            & " TELEFONO, CELLULARE, EMAIL)  " _
                            & " VALUES (SISCOM_MI.SEQ_ANA_CHIAMANTI_NON_N.NEXTVAL, " _
                            & " '" & par.PulisciStrSql(TextBoxCognomeChiamante.Text) & "', " _
                            & " '" & par.PulisciStrSql(TextBoxNomeChiamante.Text) & "', " _
                            & " '" & par.PulisciStrSql(TextBoxTelefono1Chiamante.Text) & "', " _
                            & " '" & par.PulisciStrSql(TextBoxTelefono2Chiamante.Text) & "', " _
                            & " '" & LCase(par.PulisciStrSql(TextBoxEmailChiamante.Text)) & "' ) "
                            par.cmd.ExecuteNonQuery()
                            WriteEvent("F251", "Inserimento anagrafica chiamante non noto: Email: " & TextBoxEmailChiamante.Text & " - Telefono: " & TextBoxTelefono1Chiamante.Text & " - Cellulare: " & TextBoxTelefono2Chiamante.Text)
                        End If
                    End If
                    '************ AGGIORNAMENTO CHIAMANTI ****************

                    '************ UNIONE SEGNALAZIONI UNITA' *************
                    Dim soglia As Integer = 0
                    par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='SOGLIA_TICKET_FIGLI'"
                    soglia = par.IfNull(par.cmd.ExecuteScalar, 10)

                    Dim listaS = Session.Item("listaSelezioneSegnUnita")
                    If listaS <> "" Then
                        listaS &= "," & idSegnal
                        Dim idPadre As Integer = 0
                        par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (SELECT DISTINCT ID_SEGNALAZIONE_PADRE FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE IN (" & listaS & "))) AND ID IN (" & listaS & ")"
                        idPadre = par.IfNull(par.cmd.ExecuteScalar, 0)
                        If idPadre = 0 Then
                            par.cmd.CommandText = "SELECT MIN(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (" & listaS & ")"
                            idPadre = par.IfNull(par.cmd.ExecuteScalar, 0)
                        End If

                        Dim pericoloIniziale As Integer = 0
                        par.cmd.CommandText = "SELECT ID_PERICOLO_SEGNALAZIONE_INIZ FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idPadre
                        pericoloIniziale = par.IfNull(par.cmd.ExecuteScalar, 0)

                        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_SEGNALAZIONE_PADRE=" & idPadre & " WHERE ID IN (" & listaS & ") AND ID NOT IN (" & idPadre & ")"
                        par.cmd.ExecuteNonQuery()

                        Dim numeroFigliDopoUpdate As Integer = 0
                        par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idPadre
                        numeroFigliDopoUpdate = par.IfNull(par.cmd.ExecuteScalar, 0)

                        Dim pericoloFinale As Integer = 0

                        Select Case numeroFigliDopoUpdate
                            Case 0 To soglia - 1
                                pericoloFinale = pericoloIniziale
                            Case soglia To soglia * 2 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 1, 4)
                            Case soglia * 2 To soglia * 3 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 2, 4)
                            Case soglia * 3 To soglia * 4 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 3, 4)
                            Case soglia * 4 To soglia * 5 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 4, 4)
                            Case Else
                                pericoloFinale = 4
                        End Select



                        Dim statoIniziale As Integer = 0
                        par.cmd.CommandText = "SELECT ID_PERICOLO_SEGNALAZIONE FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idPadre
                        statoIniziale = par.IfNull(par.cmd.ExecuteScalar, 0)


                        If pericoloFinale > statoIniziale Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_PERICOLO_SEGNALAZIONE=" & pericoloFinale & " WHERE ID=" & idPadre
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                                & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                                & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                                & " VALUES (" & idPadre & ", " _
                                & statoIniziale & ", " _
                                & pericoloFinale & ", " _
                                & Session.Item("ID_OPERATORE") & "," _
                                & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                & "'" & par.PulisciStrSql("Cambio priorità per numero soglia raggiunto.") & "')"
                            par.cmd.ExecuteNonQuery()
                        End If



                        WriteEvent("F252", "Unione segnalazioni unità: " & listaS)
                    End If
                    '************ UNIONE SEGNALAZIONI UNITA' *************

                    '************ UNIONE SEGNALAZIONI EDIFICI ************
                    listaS = Session.Item("listaSelezioneSegnEdificio")
                    If listaS <> "" Then
                        listaS &= "," & idSegnal
                        Dim idPadre As Integer = 0
                        par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (SELECT DISTINCT ID_SEGNALAZIONE_PADRE FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE IN (" & listaS & "))) AND ID IN (" & listaS & ")"
                        idPadre = par.IfNull(par.cmd.ExecuteScalar, 0)
                        If idPadre = 0 Then
                            par.cmd.CommandText = "SELECT MIN(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (" & listaS & ")"
                            idPadre = par.IfNull(par.cmd.ExecuteScalar, 0)
                        End If

                        Dim pericoloIniziale As Integer = 0
                        par.cmd.CommandText = "SELECT ID_PERICOLO_SEGNALAZIONE_INIZ FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idPadre
                        pericoloIniziale = par.IfNull(par.cmd.ExecuteScalar, 0)

                        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_SEGNALAZIONE_PADRE=" & idPadre & " WHERE ID IN (" & listaS & ") AND ID NOT IN (" & idPadre & ")"
                        par.cmd.ExecuteNonQuery()

                        Dim numeroFigliDopoUpdate As Integer = 0
                        par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idPadre
                        numeroFigliDopoUpdate = par.IfNull(par.cmd.ExecuteScalar, 0)

                        Dim pericoloFinale As Integer = 0

                        Select Case numeroFigliDopoUpdate
                            Case 0 To soglia - 1
                                pericoloFinale = pericoloIniziale
                            Case soglia To soglia * 2 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 1, 4)
                            Case soglia * 2 To soglia * 3 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 2, 4)
                            Case soglia * 3 To soglia * 4 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 3, 4)
                            Case soglia * 4 To soglia * 5 - 1
                                pericoloFinale = Math.Min(pericoloIniziale + 4, 4)
                            Case Else
                                pericoloFinale = 4
                        End Select

                        Dim statoIniziale As Integer = 0
                        par.cmd.CommandText = "SELECT ID_PERICOLO_SEGNALAZIONE FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idPadre
                        statoIniziale = par.IfNull(par.cmd.ExecuteScalar, 0)

                        If pericoloFinale > statoIniziale Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_PERICOLO_SEGNALAZIONE=" & pericoloFinale & " WHERE ID=" & idPadre
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                                & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                                & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                                & " VALUES (" & idPadre & ", " _
                                & statoIniziale & ", " _
                                & pericoloFinale & ", " _
                                & Session.Item("ID_OPERATORE") & "," _
                                & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                & "'" & par.PulisciStrSql("Cambio priorità per numero soglia raggiunto.") & "')"
                            par.cmd.ExecuteNonQuery()
                        End If

                        WriteEvent("F252", "Unione segnalazioni edificio: " & listaS)
                    End If

                    '************ UNIONE SEGNALAZIONI EDIFICI ************

                    connData.chiudi(True)
                    WriteEvent("F232", "")
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Inserimento segnalazione", "Operazione effettuata correttamente", Me.Page, "successo", "NuovaSegnalazione.aspx?idc=" & idAnagraficaChiamante.Value)
                Else
                    par.modalDialogMessage("Inserimento segnalazione", "Si è verificato un errore durante l\'inserimento della segnalazione.", Me.Page, "error", , )
                End If

                If idSelectedIntestatario.Value <> "" Then
                    CaricaSegnalazioniEdificioSelezionato(True)
                Else
                    CaricaSegnalazioniEdificioSelezionato()
                End If
                If idSelectedIntestatario.Value <> "" Then
                    CaricaSegnalazioniUnitaSelezionata(True)
                Else
                    CaricaSegnalazioniUnitaSelezionata()
                End If
                VisualizzaViewIdentificazioneChiamante()
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ButtonNuovaSegnalazioneCustode_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If

    End Sub



    Protected Sub RadDataGridSegnalazioniEdificioSelezionato_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadDataGridSegnalazioniEdificioSelezionato.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadDataGridSegnalazioniEdificioSelezionato.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('idSegnalazioneSelezionataEdificio').value='" & dataItem("ID").Text & "';")
            dataItem.Attributes.Add("onDblclick", "ApriSegnalazioneEdificio();")
        End If
    End Sub

    Protected Sub RadDataGridSegnalazioniEdificioSelezionato_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadDataGridSegnalazioniEdificioSelezionato.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridSegnalazEdifici"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridSegnalazEdifici"), Data.DataTable)
    End Sub

    Protected Sub RadDataGridSegnalazioniEdificioSelezionato_PreRender(sender As Object, e As System.EventArgs) Handles RadDataGridSegnalazioniEdificioSelezionato.PreRender
        For Each dataItem As GridDataItem In RadDataGridSegnalazioniEdificioSelezionato.Items
            If dataItem("ID_TIPO").Text = "1" Or dataItem("ID_TIPO").Text = "6" Then
                Select Case dataItem("CRITICITA").Text
                    Case "1"
                        dataItem("CRITICITA").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-white-128.png"
                        dataItem("CRITICITA").Controls.Add(img)
                    Case "2"
                        dataItem("CRITICITA").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-green-128.png"
                        dataItem("CRITICITA").Controls.Add(img)
                    Case "3"
                        dataItem("CRITICITA").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-yellow-128.png"
                        dataItem("CRITICITA").Controls.Add(img)
                    Case "4"
                        dataItem("CRITICITA").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-red-128.png"
                        dataItem("CRITICITA").Controls.Add(img)
                    Case "0"
                        dataItem("CRITICITA").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-blue-128.png"
                        dataItem("CRITICITA").Controls.Add(img)
                    Case Else
                End Select
            End If
        Next
    End Sub

    Private Sub caricaCanale()
        Try
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.CANALE WHERE FL_AGENDA = 1 ORDER BY ID ASC", DropDownListCanale, "ID", "DESCRIZIONE", False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - caricaCanale - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaTipologiaSegnalante()
        Try
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_SEGNALANTE ORDER BY ID ASC", cmbTipologiaSegnalante, "ID", "DESCRIZIONE", True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - caricaTipologiaSegnalante - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DropDownListComplessoChiamante_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListComplessoChiamante.SelectedIndexChanged
        CaricaEdificiChiamante()
        CaricaScaleChiamante()
        CaricaPianoChiamante()
        CaricaInterniChiamante()
    End Sub

    Protected Sub DropDownListEdificioChiamante_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListEdificioChiamante.SelectedIndexChanged
        CaricaScaleChiamante()
    End Sub

    Private Sub CheckBoxContattatoFornitore_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxContattatoFornitore.CheckedChanged
        If CheckBoxContattatoFornitore.Checked Then
            TextBoxContattatoFornitore.Enabled = True
            TextBoxOraContattatoFornitore.Enabled = True
        Else
            TextBoxContattatoFornitore.Enabled = False
            TextBoxOraContattatoFornitore.Enabled = False
        End If
    End Sub

    Private Sub CheckBoxVerificaFornitore_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxVerificaFornitore.CheckedChanged
        If CheckBoxVerificaFornitore.Checked Then
            TextBoxVerificaFornitore.Enabled = True
            TextBoxOraVerificaFornitore.Enabled = True
        Else
            TextBoxVerificaFornitore.Enabled = False
            TextBoxOraVerificaFornitore.Enabled = False
        End If
    End Sub

    Private Sub DataGridFornitori_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles DataGridFornitori.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='FO';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridFornitori, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='FO';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='FO';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridFornitori, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='FO';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridFornitori_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub DataGridGestAutonoma_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles DataGridGestAutonoma.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='GA';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridGestAutonoma, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='GA';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='GA';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridGestAutonoma, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='GA';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridGestAutonoma_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub DataGridAmministratoreCond_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles DataGridAmministratoreCond.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='AM';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridAmministratoreCond, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='AM';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='AM';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridAmministratoreCond, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='AM';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridAmministratoreCond_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub DataGridSoggCoinv_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles DataGridSoggCoinv.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='SC';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridSoggCoinv, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='SC';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoChiamante) {SelezionatoChiamante.style.backgroundColor=''} SelezionatoChiamante=this;this.style.backgroundColor='orange';document.getElementById('idSelectedChiamante').value='SC';document.getElementById('idAnagraficaChiamante').value='" & e.Item.Cells(par.IndDGC(DataGridSoggCoinv, "ID")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoChiamante').value='SC';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaChiamante').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - DataGridSoggCoinv_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub ModificaOggPage()
        par.SetModifyOggettoPage(TextBoxCognomeChiamante)
        par.SetModifyOggettoPage(TextBoxNomeChiamante)
        par.SetModifyOggettoPage(cmbTipologiaSegnalante)
        par.SetModifyOggettoPage(txtContrattoChiamante)
        par.SetModifyOggettoPage(DropDownListComplessoChiamante)
        par.SetModifyOggettoPage(DropDownListEdificioChiamante)
        par.SetModifyOggettoPage(TextBoxIndirizzoChiamante)
        par.SetModifyOggettoPage(DropDownListScalaChiamante)
        par.SetModifyOggettoPage(DropDownListPianoChiamante)
        par.SetModifyOggettoPage(DropDownListInternoChiamante)
        par.SetModifyOggettoPage(TextBoxTelefono1Chiamante)
        par.SetModifyOggettoPage(TextBoxTelefono2Chiamante)
        par.SetModifyOggettoPage(TextBoxEmailChiamante)

        par.SetModifyOggettoPage(TextBoxCognomeIntestatario)
        par.SetModifyOggettoPage(TextBoxNomeIntestatario)
        par.SetModifyOggettoPage(cmbTipologiaSegnalante)
        par.SetModifyOggettoPage(DropDownListComplessoImmobiliare)
        par.SetModifyOggettoPage(DropDownListEdificio)
        par.SetModifyOggettoPage(TextBoxIndirizzoIntestatario)
        par.SetModifyOggettoPage(DropDownListScala)
        par.SetModifyOggettoPage(DropDownListPiano)
        par.SetModifyOggettoPage(DropDownListInterno)
        par.SetModifyOggettoPage(DropDownListSedeTerritoriale)
        par.SetModifyOggettoPage(TextBoxDanneggiante)
        par.SetModifyOggettoPage(TextBoxDanneggiato)

        par.SetModifyOggettoPage(DropDownListTipologia)

        par.SetModifyOggettoPage(cmbTipoSegnalazioneLivello1)
        par.SetModifyOggettoPage(cmbTipoSegnalazioneLivello2)
        par.SetModifyOggettoPage(cmbTipoSegnalazioneLivello3)
        par.SetModifyOggettoPage(cmbTipoSegnalazioneLivello4)
        par.SetModifyOggettoPage(cmbUrgenza)

        par.SetModifyOggettoPage(DropDownListCanale)
        par.SetModifyOggettoPage(CheckBoxDVCA)
        par.SetModifyOggettoPage(CheckBoxAttoVandalico)
        par.SetModifyOggettoPage(CheckBoxFalsa)
        par.SetModifyOggettoPage(CheckBoxContattatoFornitore)
        par.SetModifyOggettoPage(CheckBoxVerificaFornitore)
        par.SetModifyOggettoPage(txtDataSopralluogo)
        par.SetModifyOggettoPage(txtOraSopralluogo)
        par.SetModifyOggettoPage(txtDataProgrammataIntervento)
        par.SetModifyOggettoPage(txtOraProgrammataIntervento)
        par.SetModifyOggettoPage(txtDataProgrammataUltimoIntervento)
        par.SetModifyOggettoPage(txtDataEffettivaIntervento)
        par.SetModifyOggettoPage(txtOraEffettivaIntervento)

        par.SetModifyOggettoPage(txtDescrizione)
    End Sub

    Private Function RicavaTipologiaManutenzione() As String
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If
        par.cmd.CommandText = "SELECT COMBINAZIONE_TIPOLOGIE.ID_TIPO_MANUTENZIONE " _
                                 & " FROM SISCOM_MI.combinazione_tipologie, SISCOM_MI.SEGNALAZIONI " _
                                 & " WHERE combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                                 & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                                 & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                                 & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                                 & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                                 & " AND SEGNALAZIONI.ID = " & idSegnalazione.Value
        RicavaTipologiaManutenzione = par.IfNull(par.cmd.ExecuteScalar, "-1")
        If connAperta = True Then
            connData.chiudi(False)
        End If
    End Function
End Class
