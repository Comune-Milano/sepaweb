Imports Telerik.Web.UI

Partial Class SICUREZZA_NuovaSegnalazione
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
                idSegnalazione.Value = Request.QueryString("IDS")

                If Not IsNothing(idSegnalazione.Value) And idSegnalazione.Value <> "" Then
                    CaricaSegnalazDaIntervento()
                Else
                    Session.Remove("DataGridSegnalazUnita")
                    Session.Remove("DataGridSegnalazEdifici")
                    ControllaNumeriUtili()
                    CaricaCustode()
                    CaricaComplessi()
                    CaricaEdifici()
                    CaricaSedeTerritoriale()
                    CaricaTutteTipologie()
                    CaricaTipologieLivello0()
                    caricaCanale()
                    DropDownListScala.Enabled = False
                    DropDownListInterno.Enabled = False
                    DropDownListPiano.Enabled = False
                    DropDownListSedeTerritoriale.Enabled = False
                    lblDataGridSegnalazioniEdificioSelezionato.Text = "Nessuna segnalazione trovata."
                    'lblDataGridSegnalazioniUnitaSelezionata.Text = "Nessuna segnalazione trovata."
                    If Session.Item("ANAGRAFE") = "0" Then
                        btnAnagrafeSipo.Visible = False
                        btnAnagrafeSipo.Attributes.Clear()
                    Else
                        btnAnagrafeSipo.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            If CType(Me.Master.FindControl("fl_sicurezza"), HiddenField).Value = "0" Then
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operatore non abilitato al modulo Sicurezza!", 300, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            Else
                If CType(Me.Master.FindControl("fl_sicurezza_sl"), HiddenField).Value = "1" Then
                    solaLettura()
                End If
            End If
            If Session.Item("FL_SEC_CREA_SEGN") = "0" Then
                If Session.Item("FL_SEC_GEST_COMPLETA") = "0" Then
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operatore non abilitato!", 300, 150, "Attenzione", "apriPaginaErrore", Nothing)
                End If
            End If
            
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub ImageButtonCercaChiamante_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonCercaChiamante.Click
        Try
            If Trim(TextBoxCognomeChiamante.Text) <> "" Then
                CaricaChiamanti()
                VisualizzaViewRicercaChiamante()
            Else
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("E\' necessario impostare correttamente il campo ""Cognome"".", 300, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - ImageButtonCercaChiamante_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub ImageButtonCercaIntestatario_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonCercaIntestatario.Click
        Try
            If Session.Item("sipo") = "" Then
                Session.Add("sipo", "")
            End If

            If Trim(TextBoxCognomeIntestatario.Text) <> "" Then
                CaricaIntestatari()
                VisualizzaViewRicercaIntestatario()
            Else
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("E\' necessario impostare correttamente il campo ""Cognome"".", 300, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - ImageButtonCercaIntestatario_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaSegnalazDaIntervento()
        Try
            If idSegnalazione.Value <> "-1" Then
                connData.apri()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then

                    TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME_RS"), "")

                    CaricaTipologieLivello0()
                    cmbTipoSegnalazioneLivello0.SelectedValue = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "-1")

                    TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                    TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO1"), "")
                    TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("TELEFONO2"), "")
                    TextBoxEmailChiamante.Text = par.IfNull(lettore("MAIL"), "")
                    txtDescrizione.Text = par.IfNull(lettore("DESCRIZIONE_RIC"), "")
                    TextBoxDanneggiante.Text = par.IfNull(lettore("DANNEGGIANTE"), "")
                    TextBoxDanneggiato.Text = par.IfNull(lettore("DANNEGGIATO"), "")
                    DropDownListCanale.SelectedValue = par.IfNull(lettore("ID_CANALE"), 0)

                    Dim query As String = ""
                    Dim unita As Long = 0
                    query = "SELECT EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id = " & par.IfNull(lettore("ID_EDIFICIO"), "")
                    par.caricaComboBox(query, DropDownListEdificio, "ID", "DENOMINAZIONE", False)
                    query = "select id,nome from siscom_mi.tab_filiali where id = " & par.IfNull(lettore("id_struttura"), "-1")
                    par.caricaComboBox(query, DropDownListSedeTerritoriale, "ID", "NOME", False)
                    unita = par.IfNull(lettore("id_unita"), 0)
                    If par.IfEmpty(unita, 0) > 0 Then
                        par.cmd.CommandText = "SELECT * FROM siscom_mi.UNITA_IMMOBILIARI  WHERE id = " & unita
                        Dim readerInt As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If readerInt.Read Then
                            TextBoxCodiceUnitaImmobiliare.Text = par.IfNull(readerInt("COD_UNITA_IMMOBILIARE"), "")
                            query = "SELECT ID,descrizione FROM siscom_mi.SCALE_EDIFICI WHERE ID = " & par.IfNull(readerInt("id_scala"), "-1")
                            par.caricaComboBox(query, DropDownListScala, "ID", "DESCRIZIONE", False)
                            query = "select COD, descrizione from siscom_mi.TIPO_LIVELLO_PIANO where COD = " & par.IfNull(readerInt("COD_TIPO_LIVELLO_PIANO"), "-1")
                            par.caricaComboBox(query, DropDownListPiano, "COD", "DESCRIZIONE", False)
                            If par.IfEmpty(unita, 0) > 0 Then
                                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE id = " & unita & " ORDER BY INTERNO ASC"
                            ElseIf DropDownListScala.SelectedValue <> "-1" And DropDownListPiano.SelectedValue <> "-1" Then
                                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & " AND ID_SCALA = " & DropDownListScala.SelectedValue & " AND COD_TIPO_LIVELLO_PIANO='" & DropDownListPiano.SelectedValue & "' ORDER BY INTERNO ASC"
                            Else
                                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & " ORDER BY INTERNO ASC"
                            End If
                            par.caricaComboBox(query, DropDownListInterno, "INTERNO", "INTERNO", False)
                            TextBoxCodiceUnitaImmobiliare.Text = par.IfNull(readerInt("cod_unita_immobiliare"), "")
                        End If
                        readerInt.Close()

                        par.cmd.CommandText = "SELECT ID_ANAGRAFICA  , RAGIONE_SOCIALE,COGNOME ,NOME   FROM siscom_mi.SOGGETTI_CONTRATTUALI ,SISCOM_MI.ANAGRAFICA " _
                                            & "WHERE id_contratto IN (SELECT id_contratto FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & unita & " AND siscom_mi.Getstatocontratto2(id_contratto,0) LIKE '%CORSO%') " _
                                            & "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'  AND ANAGRAFICA.ID = ID_ANAGRAFICA "
                        readerInt = par.cmd.ExecuteReader
                        If readerInt.Read Then
                            If par.IfNull(readerInt("RAGIONE_SOCIALE"), "") <> "" Then
                                TextBoxCognomeIntestatario.Text = par.IfNull(readerInt("RAGIONE_SOCIALE"), "")
                            Else
                                TextBoxCognomeIntestatario.Text = par.IfNull(readerInt("COGNOME"), "")
                                TextBoxNomeIntestatario.Text = par.IfNull(readerInt("NOME"), "")
                            End If
                        Else
                            TextBoxCognomeIntestatario.Text = ""
                            TextBoxNomeIntestatario.Text = ""
                        End If
                        readerInt.Close()
                    End If
                End If
                lettore.Close()

                connData.chiudi()
            Else
                connData.chiudi()
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)

                'par.modalDialogMessage("Caricamento dati segnalazione", "Non è possibile caricare correttamente i dati", Me.Page, "info", "Home.aspx", )
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Non è possibile caricare correttamente i dati", 300, 150, "Attenzione", Nothing, Nothing)

            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaSegnalazDaIntervento - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaCustode()
        Try
            If Not IsNothing(Request.QueryString("idc")) Then
                Dim idAnagraficaCustodi = Request.QueryString("idc")
                flCustode.Value = "1"
                'ButtonNuovaSegnalazioneCustode.Visible = True
                If cmbTipoSegnalazioneLivello0.SelectedValue <> "6" Then
                    CaricaTutteTipologie()
                    CaricaTipologieLivello0()
                End If
                connData.apri(False)
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
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaCustode - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SvuotaNumeriUtili()
        DataGridNumeriUtili.Visible = False
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
            connData.apri(False)
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

            connData.apri(False)
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
            If dt.Rows.Count > 0 Then
                DataGridNumeriUtili.DataSource = dt
                DataGridNumeriUtili.DataBind()
                DataGridNumeriUtili.Visible = True
            Else
                DataGridNumeriUtili.Visible = False
            End If
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaNumeriUtili - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaChiamanti()
        Try
            connData.apri(False)
            Dim condizioneChiamante As String = ""
            Dim condizioneChiamanteCustodi As String = ""
            Dim condizioneChiamanteNonNoti As String = ""
            Dim cognome = Trim(TextBoxCognomeChiamante.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            Dim nome = Trim(TextBoxNomeChiamante.Text).ToUpper.Replace("*", "%").Replace("'", "''")
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
            End If
            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ID_UNITA,SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AS ID_INTE,ANAGRAFICA.ID, SOGGETTI_CONTRATTUALI.ID_CONTRATTO, " _
                & " CASE WHEN RAGIONE_SOCIALE IS NULL THEN COGNOME ||' '|| NOME ELSE RAGIONE_SOCIALE END AS NOMINATIVO,siscom_mi.getintestatari (unita_contrattuale.id_contratto) AS intestatario, " _
                & " TO_CHAR(TO_DATE(SUBSTR(DATA_NASCITA,0,8),'yyyymmdd'),'dd/mm/yyyy')AS DATA_NASCITA, " _
                & " INDIRIZZI.DESCRIZIONE AS INDIRIZZO ,INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RAPPORTI_UTENZA.COD_CONTRATTO AS CODICE_CONTRATTO,ID_ANAGRAFICA " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA ,siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI " _
                & " WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                & " AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA " _
                & " AND ID_INDIRIZZO = INDIRIZZI.ID " _
                & " AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID (+) " _
                & " AND UNITA_IMMOBILIARI.cod_tipologia = TIPOLOGIA_UNITA_IMMOBILIARI.cod " _
                & " AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                & condizioneChiamante _
                & "order by NOMINATIVO ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            DataGridChiamanti.DataSource = dt
            DataGridChiamanti.DataBind()

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

            idSelectedChiamante.Value = ""
            idAnagraficaChiamante.Value = ""
            connData.chiudi(False)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - DataGridChiamanti_ItemDataBound - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - DataGridChiamantiNonNoti_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Private Sub CaricaIntestatari()
        Try
            connData.apri(False)
            Dim condizioneIntestatario As String = ""
            Dim cognome = Trim(TextBoxCognomeIntestatario.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            Dim nome = Trim(TextBoxNomeIntestatario.Text).ToUpper.Replace("*", "%").Replace("'", "''")
            If cognome <> "" Then
                condizioneIntestatario &= " AND (UPPER(ANAGRAFICA.COGNOME) LIKE '" & cognome & "%' OR UPPER(ANAGRAFICA.RAGIONE_SOCIALE) LIKE '" & cognome & "%') "
            End If
            If nome <> "" Then
                condizioneIntestatario &= " AND (UPPER(ANAGRAFICA.NOME) LIKE '" & nome & "%') "
            End If
            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ID_UNITA,ANAGRAFICA.cod_fiscale,SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AS ID_INTE,ANAGRAFICA.ID, SOGGETTI_CONTRATTUALI.ID_CONTRATTO, " _
                & " CASE WHEN RAGIONE_SOCIALE IS NULL THEN COGNOME ||' '|| NOME ELSE RAGIONE_SOCIALE END AS NOMINATIVO,SISCOM_MI.GETINTESTATARI (UNITA_CONTRATTUALE.ID_CONTRATTO) AS INTESTATARIO, " _
                & " TO_CHAR(TO_DATE(SUBSTR(DATA_NASCITA,0,8),'YYYYMMDD'),'DD/MM/YYYY')AS DATA_NASCITA, " _
                & " INDIRIZZI.DESCRIZIONE AS INDIRIZZO ,INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,RAPPORTI_UTENZA.COD_CONTRATTO AS CODICE_CONTRATTO,ID_ANAGRAFICA " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA ,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI " _
                & " WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                & " AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA " _
                & " AND ID_INDIRIZZO = INDIRIZZI.ID " _
                & " AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID (+) " _
                & " AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                & " AND COD_TIPOLOGIA_OCCUPANTE='INTE' " _
                & " AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
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
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaIntestatari - " & ex.Message)
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
                e.Item.Attributes.Add("onclick", "if (SelezionatoIntestatario) {SelezionatoIntestatario.style.backgroundColor=''} SelezionatoIntestatario=this;this.style.backgroundColor='orange';document.getElementById('idSelectedIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridIntestatari, "ID_UNITA")).Text.Replace("'", "\'") & "';document.getElementById('idAnagraficaIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridIntestatari, "ID_ANAGRAFICA")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridIntestatari, "ID_CONTRATTO")).Text.Replace("'", "\'") & "';document.getElementById('hfCodFiscaleSelected').value='" & e.Item.Cells(par.IndDGC(DataGridIntestatari, "COD_FISCALE")).Text.Replace("'", "\'") & "';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaIntestatario').click();")
            End If
            If e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='Gainsboro'}")
                e.Item.Attributes.Add("onclick", "if (SelezionatoIntestatario) {SelezionatoIntestatario.style.backgroundColor=''} SelezionatoIntestatario=this;this.style.backgroundColor='orange';document.getElementById('idSelectedIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridIntestatari, "ID_UNITA")).Text.Replace("'", "\'") & "';document.getElementById('idAnagraficaIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridIntestatari, "ID_ANAGRAFICA")).Text.Replace("'", "\'") & "';document.getElementById('idContrattoIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGridIntestatari, "ID_CONTRATTO")).Text.Replace("'", "\'") & "';document.getElementById('hfCodFiscaleSelected').value='" & e.Item.Cells(par.IndDGC(DataGridIntestatari, "COD_FISCALE")).Text.Replace("'", "\'") & "';")
                e.Item.Attributes.Add("onDblClick", "document.getElementById('btnConfermaIntestatario').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - DataGridChiamanti_ItemDataBound - " & ex.Message)
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
                'ButtonNuovaSegnalazioneCustode.Visible = True
                If cmbTipoSegnalazioneLivello0.SelectedValue <> "6" Then
                    CaricaTutteTipologie()
                    CaricaTipologieLivello0()
                End If
                connData.apri(False)
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
                    End If
                    lettore.Close()
                End If
                connData.chiudi(False)
            ElseIf idSelectedChiamante.Value = "NN" Then
                flCustode.Value = "0"
                'ButtonNuovaSegnalazioneCustode.Visible = False
                connData.apri(False)
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
                connData.chiudi(False)
            Else
                flCustode.Value = "0"
                'ButtonNuovaSegnalazioneCustode.Visible = False
                If IsNumeric(idSelectedChiamante.Value) Then
                    connData.apri(False)
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
                    connData.chiudi(False)

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
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Selezionare un nominativo.", 300, 150, "Attenzione", Nothing, Nothing)

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
            connData.apri(False)
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
                    & " AND TIPO_SEGNALAZIONE.ID IN (5) " _
                    & condizioni & " ORDER BY ID_PERICOLO_sEGNALAZIONE DESC,DATA_ORA_RICHIESTA DESC "
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
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaSegnalazioniUnitaSelezionata - " & ex.Message)
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
                connData.apri(False)
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
                        & " AND TIPO_SEGNALAZIONE.ID IN (5) " _
                        & condizioni & " ORDER BY ID_PERICOLO_sEGNALAZIONE DESC,DATA_ORA_RICHIESTA DESC "
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
                connData.chiudi(False)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaSegnalazioniEdificioSelezionato - " & ex.Message)
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
                If copiato.Value = "1" Then
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND ID IN (SELECT ID_eDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ") ORDER BY DENOMINAZIONE ASC"
                Else
                    If DropDownListComplessoImmobiliare.SelectedValue <> "-1" Then
                        query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND ID_COMPLESSO=" & DropDownListComplessoImmobiliare.SelectedValue & " ORDER BY DENOMINAZIONE ASC"
                    Else
                        query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC"
                    End If
                End If
            Else
                If DropDownListComplessoImmobiliare.SelectedValue <> "-1" Then
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND ID_COMPLESSO=" & DropDownListComplessoImmobiliare.SelectedValue & " ORDER BY DENOMINAZIONE ASC"
                Else
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC"
                End If
            End If
            If Trim(TextBoxNomeIntestatario.Text) = "" Then
                If DropDownListComplessoImmobiliare.SelectedValue <> "-1" Then
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND ID_COMPLESSO=" & DropDownListComplessoImmobiliare.SelectedValue & " ORDER BY DENOMINAZIONE ASC"
                Else
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC"
                End If
            End If
            par.caricaComboBox(query, DropDownListEdificio, "ID", "DENOMINAZIONE", True)
            If DropDownListEdificio.Items.Count = 2 Then
                If Not IsNothing(DropDownListEdificio.Items.FindByValue("-1")) Then
                    DropDownListEdificio.Items.Remove(DropDownListEdificio.Items.FindByValue("-1"))
                End If
            ElseIf DropDownListEdificio.Items.Count = 1 Then
                par.caricaComboBox("SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1", DropDownListComplessoImmobiliare, "ID", "DENOMINAZIONE", True)
                par.caricaComboBox("SELECT * FROM SISCOM_MI.EDIFICI WHERE ID<>1", DropDownListEdificio, "ID", "DENOMINAZIONE", True)

                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Non è possibile caricare i dati identificativi dell\'unità immobiliare selezionata.", 300, 150, "Attenzione", Nothing, Nothing)

            End If
            DropDownListScala.Items.Clear()
            DropDownListScala.Enabled = False
            DropDownListPiano.Items.Clear()
            DropDownListPiano.Enabled = False
            DropDownListInterno.Items.Clear()
            DropDownListInterno.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaEdifici - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaScale(Optional ByVal intestatario As Boolean = False)
        Try
            connData.apri(False)
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                If copiato.Value = "1" Then
                    query = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ") ORDER BY DESCRIZIONE ASC"
                Else
                    query = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificio.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
                End If
            Else
                query = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificio.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
            End If
            par.caricaComboBox(query, DropDownListScala, "ID", "DESCRIZIONE", True)
            connData.chiudi(False)
            If DropDownListScala.Items.Count = 2 Then
                If Not IsNothing(DropDownListScala.Items.FindByValue("-1")) Then
                    DropDownListScala.Items.Remove(DropDownListScala.Items.FindByValue("-1"))
                End If
            End If
            DropDownListScala.Enabled = True
            DropDownListPiano.Items.Clear()
            DropDownListPiano.Enabled = False
            DropDownListInterno.Items.Clear()
            DropDownListInterno.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaScale - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaInterni(Optional ByVal intestatario As Boolean = False)
        Try
            connData.apri(False)
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                If copiato.Value = "1" Then
                    query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & identificativo & " ORDER BY INTERNO ASC"
                Else
                    If DropDownListScala.SelectedValue <> "-1" And DropDownListPiano.SelectedValue <> "-1" Then
                        query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & " AND ID_SCALA = " & DropDownListScala.SelectedValue & " AND COD_TIPO_LIVELLO_PIANO='" & DropDownListPiano.SelectedValue & "' ORDER BY INTERNO ASC"
                    Else
                        query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & " ORDER BY INTERNO ASC"
                    End If
                End If
            Else
                If DropDownListScala.SelectedValue <> "-1" And DropDownListPiano.SelectedValue <> "-1" Then
                    query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & " AND ID_SCALA = " & DropDownListScala.SelectedValue & " AND COD_TIPO_LIVELLO_PIANO='" & DropDownListPiano.SelectedValue & "' ORDER BY INTERNO ASC"
                Else
                    query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & " ORDER BY INTERNO ASC"
                End If
            End If
            par.caricaComboBox(query, DropDownListInterno, "INTERNO", "INTERNO", True)
            connData.chiudi(False)
            If DropDownListInterno.Items.Count = 2 Then
                If Not IsNothing(DropDownListInterno.Items.FindByValue("-1")) Then
                    DropDownListInterno.Items.Remove(DropDownListInterno.Items.FindByValue("-1"))
                End If
            End If
            DropDownListInterno.Enabled = True
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaInterni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaPiano(Optional ByVal intestatario As Boolean = False)
        Try
            connData.apri(False)
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                If copiato.Value = "1" Then
                    query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ") ORDER BY DESCRIZIONE ASC"
                Else
                    If DropDownListScala.SelectedValue <> "-1" Then
                        query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificio.SelectedValue & " AND ID_sCALA=" & DropDownListScala.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
                    Else
                        query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificio.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
                    End If
                End If
            Else
                If DropDownListScala.SelectedValue <> "-1" Then
                    query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificio.SelectedValue & " AND ID_sCALA=" & DropDownListScala.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
                Else
                    query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & DropDownListEdificio.SelectedValue & ") ORDER BY DESCRIZIONE ASC"
                End If
            End If
            par.caricaComboBox(query, DropDownListPiano, "COD", "DESCRIZIONE", True)
            connData.chiudi(False)
            If DropDownListPiano.Items.Count = 2 Then
                If Not IsNothing(DropDownListPiano.Items.FindByValue("-1")) Then
                    DropDownListPiano.Items.Remove(DropDownListPiano.Items.FindByValue("-1"))
                End If
            End If
            DropDownListPiano.Enabled = True
            DropDownListInterno.Items.Clear()
            DropDownListInterno.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaPiano - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaSedeTerritoriale()
        Try
            'connData.apri(False)
            If IsNumeric(DropDownListEdificio.SelectedValue) AndAlso DropDownListEdificio.SelectedValue <> "-1" Then
                Dim query As String = "SELECT ID, NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID = (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID=" & DropDownListEdificio.SelectedValue & ")) ORDER BY NOME ASC"
                par.caricaComboBox(query, DropDownListSedeTerritoriale, "ID", "NOME", True)
                'connData.chiudi(False)
                If DropDownListSedeTerritoriale.Items.Count = 2 Then
                    If Not IsNothing(DropDownListSedeTerritoriale.Items.FindByValue("-1")) Then
                        DropDownListSedeTerritoriale.Items.Remove(DropDownListSedeTerritoriale.Items.FindByValue("-1"))
                    End If
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaSedeTerritoriale - " & ex.Message)
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
        CaricaPiano()
        CaricaInterni()
        CaricaSedeTerritoriale()
        ControllaCondominio()
        ControllaMoroso()
        ControllaNumeriUtili()
    End Sub

    'Protected Sub RadDataGridSegnalazioniUnitaSelezionata_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadDataGridSegnalazioniUnitaSelezionata.ItemDataBound
    '    If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '        Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
    '        For Each column As GridColumn In RadDataGridSegnalazioniUnitaSelezionata.MasterTableView.RenderColumns
    '            If (TypeOf column Is GridBoundColumn) Then
    '                dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text
    '            End If
    '        Next
    '        dataItem.Attributes.Add("onclick", "document.getElementById('idSegnalazioneSelezionataUnita').value='" & dataItem("ID").Text & "';")
    '        dataItem.Attributes.Add("onDblclick", "ApriSegnalazioneUnita();")
    '    End If
    'End Sub


    'Protected Sub RadDataGridSegnalazioniUnitaSelezionata_PreRender(sender As Object, e As System.EventArgs) Handles RadDataGridSegnalazioniUnitaSelezionata.PreRender
    '    For Each dataItem As GridDataItem In RadDataGridSegnalazioniUnitaSelezionata.Items
    '        If dataItem("ID_TIPO").Text = "1" Or dataItem("ID_TIPO").Text = "6" Then
    '            Select Case dataItem("CRITICITA").Text
    '                Case "1"
    '                    dataItem("CRITICITA").Controls.Clear()
    '                    Dim img As Image = New Image()
    '                    img.ImageUrl = "Immagini/Ball-white-128.png"
    '                    dataItem("CRITICITA").Controls.Add(img)
    '                Case "2"
    '                    dataItem("CRITICITA").Controls.Clear()
    '                    Dim img As Image = New Image()
    '                    img.ImageUrl = "Immagini/Ball-green-128.png"
    '                    dataItem("CRITICITA").Controls.Add(img)
    '                Case "3"
    '                    dataItem("CRITICITA").Controls.Clear()
    '                    Dim img As Image = New Image()
    '                    img.ImageUrl = "Immagini/Ball-yellow-128.png"
    '                    dataItem("CRITICITA").Controls.Add(img)
    '                Case "4"
    '                    dataItem("CRITICITA").Controls.Clear()
    '                    Dim img As Image = New Image()
    '                    img.ImageUrl = "Immagini/Ball-red-128.png"
    '                    dataItem("CRITICITA").Controls.Add(img)
    '                Case "0"
    '                    dataItem("CRITICITA").Controls.Clear()
    '                    Dim img As Image = New Image()
    '                    img.ImageUrl = "Immagini/Ball-blue-128.png"
    '                    dataItem("CRITICITA").Controls.Add(img)
    '                Case Else
    '            End Select
    '        End If
    '    Next
    'End Sub

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
    '        Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - DataGridSegnalazioniUnitaSelezionata_ItemDataBound - " & ex.Message)
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
    '        Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - DataGridSegnalazioniEdificioSelezionato_ItemDataBound - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    Protected Sub btnSvuota_Click(sender As Object, e As System.EventArgs) Handles btnSvuota.Click
        Svuota()
    End Sub
    Private Sub Svuota(Optional SvuotaSelezione As Boolean = True, Optional DaChiamante As Boolean = True, Optional soloChiamante As Boolean = False, Optional ByVal RicaricaCategorie As Boolean = True)
        Try
            copiato.Value = ""
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
                End If

                'flCustode.Value = "0"
                TextBoxDanneggiante.Text = ""
                TextBoxDanneggiato.Text = ""

                If RicaricaCategorie = True Then
                    DropDownListTipologia.Items.Clear()
                    cmbTipoSegnalazioneLivello0.Items.Clear()
                    cmbTipoSegnalazioneLivello1.Items.Clear()
                    cmbTipoSegnalazioneLivello2.Items.Clear()
                    cmbTipoSegnalazioneLivello3.Items.Clear()
                    cmbTipoSegnalazioneLivello4.Items.Clear()

                    CaricaTutteTipologie()
                    CaricaTipologieLivello0()
                End If

                TextBoxCognomeIntestatario.Text = ""
                TextBoxNomeIntestatario.Text = ""
                TextBoxCodiceContrattoIntestatario.Text = ""
                TextBoxCodiceUnitaImmobiliare.Text = ""
                CaricaComplessi()
                CaricaEdifici()
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
                SvuotaNumeriUtili()
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

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - Svuota - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello0.SelectedIndexChanged
        DropDownListTipologia.SelectedValue = "-1"
        'If cmbTipoSegnalazioneLivello0.SelectedValue = "0" Then
        '    PanelElencoInterventi.Visible = True
        'Else
        '    PanelElencoInterventi.Visible = False
        'End If

        CaricaNumeriUtili()
        CaricaTipologieLivello1()
        caricaUrgenzaPredefinita()

        CaricaSegnalazioniEdificioSelezionato(True)
        'CaricaSegnalazioniUnitaSelezionata(True)
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello1.SelectedIndexChanged
        DropDownListTipologia.SelectedValue = "-1"
        CaricaTipologieLivello2()
        caricaUrgenzaPredefinita()

        CaricaSegnalazioniEdificioSelezionato(True)
        CaricaSegnalazioniUnitaSelezionata(True)
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello2.SelectedIndexChanged
        DropDownListTipologia.SelectedValue = "-1"
        CaricaTipologieLivello3()
        caricaUrgenzaPredefinita()

        CaricaSegnalazioniEdificioSelezionato(True)
        CaricaSegnalazioniUnitaSelezionata(True)
    End Sub
    Private Sub CaricaTipologieLivello0()
        Try
            Dim query As String = ""
            'If flCustode.Value = "1" Then
            '    query = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id=6 ORDER BY ID"
            'Else
            query = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id=5 ORDER BY ID"
            ' End If
            'connData.apri(False)
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello0, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaTipologieLivello0 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello1()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE=" & cmbTipoSegnalazioneLivello0.SelectedValue & " ORDER BY DESCRIZIONE"
            'connData.apri(False)
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello1, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
            If cmbTipoSegnalazioneLivello1.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello1.Items.FindByValue("-1")) Then
                    cmbTipoSegnalazioneLivello1.Items.Remove(cmbTipoSegnalazioneLivello1.Items.FindByValue("-1"))
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
            If cmbTipoSegnalazioneLivello1.Items.Count = 1 Then
                cmbTipoSegnalazioneLivello1.Visible = False
                lblLivello1.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaTipologieLivello1 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello2()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue & " ORDER BY DESCRIZIONE"
            'connData.apri(False)
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello2, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
            If cmbTipoSegnalazioneLivello2.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello2.Items.FindByValue("-1")) Then
                    cmbTipoSegnalazioneLivello2.Items.Remove(cmbTipoSegnalazioneLivello2.Items.FindByValue("-1"))
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
            If cmbTipoSegnalazioneLivello2.Items.Count = 1 Then
                cmbTipoSegnalazioneLivello2.Visible = False
                lblLivello2.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaTipologieLivello2 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello3()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue & " ORDER BY DESCRIZIONE"
            'connData.apri(False)
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello3, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
            If cmbTipoSegnalazioneLivello3.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello3.Items.FindByValue("-1")) Then
                    cmbTipoSegnalazioneLivello3.Items.Remove(cmbTipoSegnalazioneLivello3.Items.FindByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello3.Visible = True
            cmbTipoSegnalazioneLivello4.Visible = False
            lblLivello3.Visible = True
            lblLivello4.Visible = False
            If cmbTipoSegnalazioneLivello3.Items.Count = 1 Then
                cmbTipoSegnalazioneLivello3.Visible = False
                lblLivello3.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaTipologieLivello3 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello4()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue & " ORDER BY DESCRIZIONE"
            'connData.apri(False)
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello4, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
            If cmbTipoSegnalazioneLivello4.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello4.Items.FindByValue("-1")) Then
                    cmbTipoSegnalazioneLivello4.Items.Remove(cmbTipoSegnalazioneLivello4.Items.FindByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello4.Visible = True
            lblLivello4.Visible = True
            If cmbTipoSegnalazioneLivello4.Items.Count = 1 Then
                cmbTipoSegnalazioneLivello4.Visible = False
                lblLivello4.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaTipologieLivello4 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Private Sub ControllaCondominio(Optional ByVal intestatario As Boolean = False)
        Try
            connData.apri(False)
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                If copiato.Value = "1" Then
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
                End If
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
            connData.chiudi(False)
            If lblInCondominioSiNo.Text = "Sì" Then
                'cmbTipoSegnalazioneLivello0.SelectedValue = 3
                ControllaAmministratore()
            Else
                panelAmministratore.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - controllaCondominio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub ControllaMoroso()
        If IsNumeric(idContrattoIntestatario.Value) AndAlso idContrattoIntestatario.Value > 0 Then
            Try
                connData.apri(False)
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
                connData.chiudi(False)
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - ControllaMoroso - " & ex.Message)
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
            'If flCustode.Value = "1" AndAlso IsNumeric(idAnagraficaChiamante.Value) AndAlso idAnagraficaChiamante.Value <> "-1" Then
            '    condizioneCustode = " AND ID IN (SELECT ID_cOMPLESSO FROM SISCOM_MI.PORTIERATO WHERE ID_CUSTODE=" & idAnagraficaChiamante.Value & ")"
            'End If
            Dim query As String = ""
            Dim identificativo As String = ""
            If intestatario Then
                identificativo = idSelectedIntestatario.Value
            Else
                identificativo = idSelectedChiamante.Value
            End If
            If IsNumeric(identificativo) AndAlso identificativo <> "-1" Then
                If copiato.Value = "1" Then
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 AND ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID IN (SELECT ID_eDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & identificativo & ")) " & condizioneCustode & " ORDER BY DENOMINAZIONE ASC"
                Else
                    query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneCustode & " ORDER BY DENOMINAZIONE ASC"
                End If
            Else
                query = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneCustode & " ORDER BY DENOMINAZIONE ASC"
            End If
            par.caricaComboBox(query, DropDownListComplessoImmobiliare, "ID", "DENOMINAZIONE", True)
            If DropDownListComplessoImmobiliare.Items.Count = 2 Then
                If Not IsNothing(DropDownListComplessoImmobiliare.Items.FindByValue("-1")) Then
                    DropDownListComplessoImmobiliare.Items.Remove(DropDownListComplessoImmobiliare.Items.FindByValue("-1"))
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaComplessi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub ControllaAmministratore()
        Try
            connData.apri(False)
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
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - ControllaAmministratore - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnIndietro2_Click(sender As Object, e As System.EventArgs) Handles btnIndietro2.Click
        VisualizzaViewIdentificazioneChiamante()
    End Sub
    Protected Sub btnConfermaIntestatario_Click(sender As Object, e As System.EventArgs) Handles btnConfermaIntestatario.Click
        If IsNumeric(idSelectedIntestatario.Value) Then
            Try
                connData.apri(False)
                Svuota(False, False, , False)
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
                        & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' and id_Anagrafica=" & idAnagraficaSelezionata
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TextBoxCognomeIntestatario.Text = par.IfNull(lettore("COGNOME_INTESTATARIO"), "")
                        TextBoxNomeIntestatario.Text = par.IfNull(lettore("NOME_INTESTATARIO"), "")
                        TextBoxCodiceContrattoIntestatario.Text = par.IfNull(lettore("COD_CONTRATTO"), "")
                        TextBoxCodiceUnitaImmobiliare.Text = par.IfNull(lettore("COD_UNITA_IMMOBILIARE"), "")
                        copiato.Value = "1"
                    End If
                    lettore.Close()
                End If

                connData.chiudi(False)


            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - btnConfermaIntestatario_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
            CaricaComplessi(True)
            CaricaEdifici(True)
            CaricaScale(True)
            CaricaPiano(True)
            CaricaInterni(True)
            CaricaSedeTerritoriale()
            CaricaSegnalazioniEdificioSelezionato(True)
            CaricaSegnalazioniUnitaSelezionata(True)
            ControllaCondominio(True)
            ControllaMoroso()
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
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Selezionare un nominativo.", 300, 150, "Attenzione", Nothing, Nothing)
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
            If DropDownListEdificio.SelectedValue = "-1" Then
                CheckControl = False
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Inserire l\'oggetto della richiesta", 300, 150, "Attenzione", Nothing, Nothing)

            End If
            If String.IsNullOrEmpty(Me.txtDescrizione.Text) Then
                CheckControl = False
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Inserire la descrizione della richiesta", 300, 150, "Attenzione", Nothing, Nothing)

            End If
            If cmbTipoSegnalazioneLivello0.SelectedValue = "-1" Then
                CheckControl = False
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Inserire la tipologia della segnalazione", 300, 150, "Attenzione", Nothing, Nothing)

            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CheckControl - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        Return CheckControl
    End Function

    Private Function RicavaUnita() As Long
        Dim idUI As Long = 0

        Dim condizioneQuery As String = ""
        If DropDownListScala.SelectedValue <> "-1" Then
            condizioneQuery = " and id_scala=" & DropDownListScala.SelectedValue
        End If
        If DropDownListPiano.SelectedValue <> "-1" Then
            condizioneQuery &= " and COD_TIPO_LIVELLO_PIANO='" & DropDownListPiano.SelectedValue & "'"
        End If
        If DropDownListInterno.SelectedValue <> "-1" Then
            condizioneQuery &= " and interno='" & DropDownListInterno.SelectedValue & "'"
        End If

        par.cmd.CommandText = "SELECT unita_immobiliari.id from siscom_mi.unita_immobiliari,siscom_mi.SCALE_EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO where id_unita_principale is null " _
            & " and unita_immobiliari.id_Edificio=" & par.IfNull(DropDownListEdificio.SelectedValue, "-1") & " " & condizioneQuery & " and COD_TIPO_LIVELLO_PIANO=TIPO_LIVELLO_PIANO.cod and SCALE_EDIFICI.id=unita_immobiliari.ID_SCALA"
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            idUI = par.IfNull(lettore(0), "-1")
        End If
        lettore.Close()

        Return idUI
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
                    'If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
                    '    valoreUrgenza = cmbUrgenza.SelectedIndex
                    'End If
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
                            If copiato.Value = "1" Then
                                unita = idSelectedChiamante.Value
                            Else
                                unita = RicavaUnita()
                            End If
                            If IsNumeric(idContrattoChiamante.Value) Then
                                contratto = idContrattoChiamante.Value
                            End If
                        Else
                            unita = RicavaUnita()
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
                        & " ID_CANALE," _
                        & " FL_CUSTODE" _
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
                        & par.IfNull(DropDownListCanale.SelectedValue, 0) & ", " _
                        & par.IfNull(custode, "null") _
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

                    If Session.Item("sipo") <> "" Then
                        Dim arrCF() As String
                        Dim sStringaSql As String = ""
                        Dim trovato As Boolean = False

                        arrCF = Split(Session.Item("sipo"), ";")

                        For i = 0 To UBound(arrCF) - 1
                            'sStringaSql = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            '        & "VALUES (" & idSegnal & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                            '        & "'F166','" & arrCF(i) & "')"
                            'par.cmd.CommandText = sStringaSql
                            'par.cmd.ExecuteNonQuery()
                            WriteEvent("F166", arrCF(i))
                        Next

                        Session.Item("sipo") = ""
                        Session.Remove("sipo")
                    End If


                    WriteEvent("F232", "")
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata correttamente!", 300, 150, "Attenzione", "apriMaschera", Nothing)
                    'par.modalDialogMessage("Inserimento segnalazione", "Operazione effettuata correttamente", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idSegnal, )
                Else
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Si è verificato un errore durante l\'inserimento della segnalazione.", 300, 150, "Attenzione", Nothing, Nothing)

                    ' par.modalDialogMessage("Inserimento segnalazione", "Si è verificato un errore durante l\'inserimento della segnalazione.", Me.Page, "error", , )
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
                Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - btnSalva_Click - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - WriteEvent - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub ImageButtonDanneggiante_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonDanneggiante.Click
        If DropDownListEdificio.SelectedValue <> "-1" Then
            Try
                connData.apri(False)
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
                connData.chiudi(False)
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - ImageButtonDanneggiante_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
            VisualizzaViewDanneggiante()
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("E\' necessario selezionare un edificio.", 300, 150, "Attenzione", Nothing, Nothing)

        End If
    End Sub
    Protected Sub ImageButtonDanneggiato_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonDanneggiato.Click
        If DropDownListEdificio.SelectedValue <> "-1" Then
            Try
                connData.apri(False)
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
                connData.chiudi(False)
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - ImageButtonDanneggiato_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
            VisualizzaViewDanneggiato()
        Else

            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("E\' necessario selezionare un edificio.", 300, 150, "Attenzione", Nothing, Nothing)

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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - DataGridDanneggiante_ItemDataBound - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - DataGridDanneggiato_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub ControllaNumeriUtili()
        Dim orarioInizio As String = ""
        Dim orarioFine As String = ""
        Dim fascia As String = ""
        Dim ora_attuale As String = Format(Now, "HHmm")
        For Each elemento As DataGridItem In DataGridNumeriUtili.Items
            fascia = elemento.Cells(par.IndRDGC(DataGridNumeriUtili, "FASCIA")).Text.Replace("&nbsp;", "")
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
        Response.Redirect("Home.aspx", False)
    End Sub


    Private Sub CaricaTutteTipologie()
        Try
            Dim condizioneCustodi As String = ""
            'If flCustode.Value = "1" Then
            '    condizioneCustodi = " WHERE ID_TIPO_SEGNALAZIONE=6"
            'Else
            condizioneCustodi = " WHERE ID_TIPO_SEGNALAZIONE=5"
            'End If
            Dim query As String = " SELECT  " _
                & " ID, " _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_sEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_sEGNALAZIONE)  " _
                & " || " _
                & " (CASE WHEN ID_TIPO_SEGNALAZIONE_LIVELLO_1 IS NOT NULL THEN   " _
                & " ' - '||(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_sEGNALAZIONE_LIVELLO_1) " _
                & " ELSE '' END) " _
                & " || " _
                & " (CASE WHEN ID_TIPO_SEGNALAZIONE_LIVELLO_2 IS NOT NULL THEN   " _
                & " ' - '||(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_sEGNALAZIONE_LIVELLO_2) " _
                & " ELSE '' END) " _
                & " || " _
                & " (CASE WHEN ID_TIPO_SEGNALAZIONE_LIVELLO_3 IS NOT NULL THEN   " _
                & " ' - '||(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_sEGNALAZIONE_LIVELLO_3) " _
                & " ELSE '' END) " _
                & " || " _
                & " (CASE WHEN ID_TIPO_SEGNALAZIONE_LIVELLO_4 IS NOT NULL THEN   " _
                & " ' - '||(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_sEGNALAZIONE_LIVELLO_4) " _
                & " ELSE '' END) " _
                & " AS DESCRIZIONE " _
                & " FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                & condizioneCustodi _
                & " ORDER BY 2 ASC "

            'connData.apri(False)
            par.caricaComboBox(query, DropDownListTipologia, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - caricaTutteTipologie - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello3_SelectedIndexChanged1(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello3.SelectedIndexChanged
        DropDownListTipologia.SelectedValue = "-1"
        CaricaTipologieLivello4()
        caricaUrgenzaPredefinita()

        CaricaSegnalazioniEdificioSelezionato(True)
        CaricaSegnalazioniUnitaSelezionata(True)
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello4.SelectedIndexChanged
        DropDownListTipologia.SelectedValue = "-1"
        caricaUrgenzaPredefinita()

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

            Dim tipo0 As Integer = -1
            Dim tipo1 As Integer = -1
            Dim tipo2 As Integer = -1
            Dim tipo3 As Integer = -1
            Dim tipo4 As Integer = -1
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            connData.apri(False)
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
                    'If urgenza <> -1 Then
                    '    Select Case urgenza
                    '        Case 1
                    '            cmbUrgenza.SelectedValue = "Bianco"
                    '        Case 2
                    '            cmbUrgenza.SelectedValue = "Verde"
                    '        Case 3
                    '            cmbUrgenza.SelectedValue = "Giallo"
                    '        Case 4
                    '            cmbUrgenza.SelectedValue = "Rosso"
                    '        Case 0
                    '            cmbUrgenza.SelectedValue = "Blu"
                    '    End Select
                    ' **   'PanelUrgenzaCriticita.Visible = True
                    'Else
                    ' **   'PanelUrgenzaCriticita.Visible = False
                    'End If
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

                    'If orarioUfficio <> -1 Then
                    '    Select Case orarioUfficio
                    '        Case 1
                    '            cmbUrgenza.SelectedValue = "Bianco"
                    '        Case 2
                    '            cmbUrgenza.SelectedValue = "Verde"
                    '        Case 3
                    '            cmbUrgenza.SelectedValue = "Giallo"
                    '        Case 4
                    '            cmbUrgenza.SelectedValue = "Rosso"
                    '        Case 0
                    '            cmbUrgenza.SelectedValue = "Blu"
                    '    End Select
                    '**    'PanelUrgenzaCriticita.Visible = True
                    'Else
                    '**    'PanelUrgenzaCriticita.Visible = False
                    'End If
                Else
                    'PanelUrgenzaCriticita.Visible = False
                End If
            End If
            connData.chiudi(False)
            'If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
            '    PanelUrgenzaCriticita.Visible = True
            'Else
            '    PanelUrgenzaCriticita.Visible = False
            'End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - caricaUrgenzaPredefinita - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DropDownListTipologia_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListTipologia.SelectedIndexChanged
        caricaUrgenzaPredefinita()
        CaricaSegnalazioniEdificioSelezionato(True)
        CaricaSegnalazioniUnitaSelezionata(True)
        CaricaNumeriUtili()
    End Sub

    Protected Sub ImageButtonCopiaDati_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonCopiaDati.Click
        If IsNumeric(idSelectedChiamante.Value) Then
            Try
                connData.apri(False)
                'Svuota(False)
                Dim idUnitaSelezionata As String = idSelectedChiamante.Value
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
                    idSelectedIntestatario.Value = idSelectedChiamante.Value
                    copiato.Value = "1"
                End If
                connData.chiudi(False)
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - ImageButtonCopiaDati_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
            CaricaComplessi()
            CaricaEdifici()
            CaricaScale()
            CaricaPiano()
            CaricaInterni()
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
            CaricaEdifici()
            CaricaScale()
            CaricaPiano()
            CaricaInterni()
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
            '    par.modalDialogConfirm("Sicurezza", "Vuoi unire le segnalazioni selezionate?", "Ok", "document.getElementById('ButtonUnisciSegnalazioni1').click();", "Annulla", "", Page)
            'Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Selezionare almeno una segnalazione", 300, 150, "Attenzione", Nothing, Nothing)
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
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("E\' possibile selezionare al massimo un ticket ""padre""", 300, 150, "Attenzione", Nothing, Nothing)
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
            '    par.modalDialogConfirm("Sicurezza", "Vuoi unire le segnalazioni selezionate?", "Ok", "document.getElementById('ButtonUnisciSegnalazioniEdifici2').click();", "Annulla", "", Page)
            'Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Selezionare almeno una segnalazione", 300, 150, "Attenzione", Nothing, Nothing)

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
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("E\' possibile selezionare al massimo un ticket ""padre""", 300, 150, "Attenzione", Nothing, Nothing)
        Else
            Session.Item("listaSelezioneSegnEdificio") = listaSegnalazioni
        End If
        If Session.Item("listaSelezioneSegnEdificio") <> "" Then
            lblSegnalazioniEdificiDaUnire.Text = "Lista segnalazioni selezionate: " & Session.Item("listaSelezioneSegnEdificio")
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
            par.caricaComboBox("SELECT * FROM SISCOM_MI.CANALE ORDER BY ID ASC", DropDownListCanale, "ID", "DESCRIZIONE", False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - caricaCanale - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    'Protected Sub btnAnagrafeSipo_Click(sender As Object, e As System.EventArgs) Handles btnAnagrafeSipo.Click
    '    'Dim Chiave As String = par.getPageId & "_" & Format(Now, "yyyyMMddHHmmss")
    '    Dim codFisc As String = ""
    '    Try
    '        connData.apri()

    '        par.cmd.CommandText = "select cod_fiscale from siscom_mi.anagrafica where id=" & idAnagraficaIntestatario.Value
    '        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '        If lettore.Read Then
    '            codFisc = par.IfNull(lettore("cod_fiscale"), "")
    '        End If
    '        lettore.Close()

    '        'par.cmd.CommandText = "INSERT INTO AU_LIGHT_CONCESSIONI VALUES ('" & Chiave & "')"
    '        'par.cmd.ExecuteNonQuery()

    '        connData.chiudi()

    '        ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "window.open('../Anagrafe.aspx?ID=" & par.CriptaMolto(Format(CDbl(par.IfEmpty(idSegnalazione.Value, "0")), "000000000000000")) & "&CF=" & par.CriptaMolto(codFisc) & "&T=7','Anagrafe','top=0,left=0,width=600,height=400');", True)
    '        MultiView1.ActiveViewIndex = 2
    '        MultiView2.ActiveViewIndex = 2
    '        MultiView3.ActiveViewIndex = 2

    '        'javascript:window.open('../Anagrafe.aspx?CF=" & par.CriptaMolto(codFisc) & "&T=7','Anagrafe','top=0,left=0,width=600,height=400');")

    '        'Response.Write("<script>window.open('../Anagrafica3.aspx?ID=" & par.CriptaMolto(Format(Now, "yyyyMMddHH") & "#" & Format(CDbl(par.IfEmpty(idSegnalazione.Value, "0")), "000000000000000") & "@" & Chiave) & "&CF=" & par.Cripta(codFisc) & "&OP=" & par.Cripta(Mid(Session.Item("operatore"), 1, 16)) & "&C=7','SIPO','');self.close();</script>")


    '    Catch ex As Exception
    '        connData.chiudi()
    '        Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - btnAnagrafeSipo_Click - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try

    'End Sub

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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - DataGridCustodi_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnAnagrafeSipo_Click(sender As Object, e As System.EventArgs) Handles btnAnagrafeSipo.Click
        VisualizzaViewRicercaIntestatario()
    End Sub
End Class
