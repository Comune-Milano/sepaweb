
Imports System.IO
Imports Telerik.Web.UI

Partial Class FORNITORI_SegnalazioniCanone
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Public Property iIndiceFornitore() As String
        Get
            If Not (ViewState("par_iIndiceFornitore") Is Nothing) Then
                Return CStr(ViewState("par_iIndiceFornitore"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_iIndiceFornitore") = value
        End Set
    End Property
    Private Sub FORNITORI_SegnalazioniCanone_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
        End If
        If Session.Item("MOD_FORNITORI") <> "1" Then
            Response.Redirect("../AccessoNegato.htm", False)
        End If
        If Session.Item("MOD_FORNITORI_ODL") <> "1" Then
            Response.Redirect("../AccessoNegato.htm", False)
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            VerificaOperatore()
            CaricaViste()
        End If
    End Sub

    Private Sub CaricaViste()
        'La vista serve ad individuare tutto ciò che deve essere visualizzato a seconda del profilo utente
        If Not IsNothing(Session.Item("MOD_BUILDING_MANAGER")) AndAlso Session.Item("MOD_BUILDING_MANAGER") = 1 Then
            RadButtonBuildingManager.Visible = True
        Else
            RadButtonBuildingManager.Visible = False
        End If
        If (Not IsNothing(Session.Item("FL_AUTORIZZAZIONE_ODL")) Or Not IsNothing(Session.Item("FL_SUPERDIRETTORE"))) AndAlso (Session.Item("FL_AUTORIZZAZIONE_ODL") = 1 Or Session.Item("FL_SUPERDIRETTORE") = 1) Then
            RadButtonDirettoreLavori.Visible = True
        Else
            RadButtonDirettoreLavori.Visible = False
        End If
        If Not IsNothing(Session.Item("FL_FQM")) AndAlso Session.Item("FL_FQM") = 1 Then
            RadButtonFieldQualityManager.Visible = True
        Else
            RadButtonFieldQualityManager.Visible = False
        End If
        If Not IsNothing(Session.Item("FL_CP_TECN_AMM")) AndAlso Session.Item("FL_CP_TECN_AMM") = 1 Then
            RadButtonTecnicoAmministrativo.Visible = True
        Else
            RadButtonTecnicoAmministrativo.Visible = False
        End If
        If RadButtonDirettoreLavori.Visible = True Then
            RadButtonDirettoreLavori.Checked = True
        ElseIf RadButtonBuildingManager.Visible = True Then
            RadButtonBuildingManager.Checked = True
        ElseIf RadButtonFieldQualityManager.Visible = True Then
            RadButtonFieldQualityManager.Checked = True
        ElseIf RadButtonTecnicoAmministrativo.Visible = True Then
            RadButtonTecnicoAmministrativo.Checked = True
        End If
    End Sub

    Private Sub dgvSegnalazioni_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvSegnalazioni.NeedDataSource
        Try
            Dim Query As String = EsportaQuerySegnalazioni()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            dgvSegnalazioni.DataSource = dt
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - SegnalazioniCanone - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Function EsportaQuerySegnalazioni() As String
        '& " AND (SEGNALAZIONI.ID_EDIFICIO IN " _
        '& " (SELECT ID_EDIFICIO FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO IN " _
        '& " (SELECT ID FROM APPALTI WHERE ID_FORNITORE IN " & iIndiceFornitore & "))" _
        '& " OR " _
        '& " SEGNALAZIONI.ID_UNITA IN " _
        '& " (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO IN " _
        '& " (SELECT ID_EDIFICIO FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO IN (SELECT ID FROM APPALTI WHERE ID_FORNITORE IN " & iIndiceFornitore & "))))  " _
        Dim filtroFornitore As String = ""
        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
        Dim condizioneStruttura As String = ""
        Dim condizioneVista As String = ""

        If Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
            filtroFornitore = " AND (SELECT ID_GRUPPO " _
                            & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA " _
                            & " WHERE PROGRAMMA_ATTIVITA.ID = SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA) IN (SELECT ID_GRUPPO FROM SISCOM_MI.OPERATORI_FO_APPALTI WHERE ID_OPERATORE = " & Session.Item("ID_OPERATORE") & ") "
        Else
            If Session.Item("FL_SUPERDIRETTORE") = "0" Then
                If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
                ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
                    condizioneStruttura = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") "
                Else
                    'CONDIZIONE PER ESCLUDERE LA VISIONE
                    condizioneStruttura = " AND EDIFICI.ID_COMPLESSO=0 "
                End If

                If RadButtonBuildingManager.Checked = True Then
                    'BUILDING MANAGER
                    condizioneVista = " AND SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,1) = '" & idOperatore & "'"
                ElseIf RadButtonDirettoreLavori.Checked = True Then
                    'DIRETTORE LAVORI
                    'condizioneVista = " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)='" & idOperatore & "')"
                    condizioneVista = " and segnalazioni.id_programma_attivita in (select id from siscom_mi.programma_attivita where siscom_mi.getdlfromappalti(programma_attivita.id_gruppo) = " & idOperatore & ")"
                ElseIf RadButtonFieldQualityManager.Checked = True Then
                    'Coordinatore qualità
                    condizioneVista = condizioneStruttura
                ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                    'TECNICO AMMINISTRATIVO
                    condizioneVista = condizioneStruttura
                End If
            Else
                RadButtonBuildingManager.Visible = False
                RadButtonDirettoreLavori.Visible = False
                RadButtonTecnicoAmministrativo.Visible = False
                RadButtonFieldQualityManager.Visible = False
            End If
        End If
        '***Rif segn. SD 2519/2018***'
        Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
        '***Rif segn. SD 2519/2018***'



        Dim filtroStato As String = ""
        If chkSoloChiuse.Checked Then
            filtroStato = "AND segnalazioni.id_stato = 10"
        Else
            filtroStato = " AND segnalazioni.id_stato  in (0,6,7) "
        End If


        EsportaQuerySegnalazioni = " Select SEGNALAZIONI.ID, " _
                            & " SEGNALAZIONI.ID As NUM, " _
                            & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE As TIPO, " _
                            & " (Case When segnalazioni.id_tipo_segnalazione=1 Then tipologie_guasti.descrizione Else null End) As tipo_int, " _
                            & " NVL((Select LTRIM(RTRIM((Case When ANNO Is Not NULL Then TO_CHAR(ANNO) Else SUBSTR(NUM_REPERTORIO,1,4) End)||LTRIM(RTRIM(TO_CHAR(PROGR,'0000000000'))))) FROM SISCOM_MI.APPALTI WHERE ID IN  (SELECT ID_GRUPPO FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID = SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA)),0) AS REP_ORD, " _
                            & " (SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID IN  (SELECT ID_GRUPPO FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID = SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA)) AS NUM_REPERTORIO, " _
                            & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID = SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA)) AS FORNITORE, " _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=SEGNALAZIONI.ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, segnalazioni.id_stato, " _
                            & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
                            & " (CASE WHEN ID_UNITA IS NOT NULL THEN (SELECT MAX(DESCRIZIONE) FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID= ID_UNITA)) ELSE '' END) AS SCALA, " _
                            & " (CASE WHEN ID_UNITA IS NOT NULL THEN (SELECT MAX(DESCRIZIONE) FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID= ID_UNITA)) ELSE ' ' END) AS PIANO, " _
                            & " (CASE WHEN ID_UNITA IS NOT NULL THEN (SELECT MAX(INTERNO) FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = ID_UNITA) ELSE ' ' END) AS INTERNO, " _
                            & " SEGNALAZIONI.TELEFONO1, SEGNALAZIONI.TELEFONO2, " _
                            & " (CASE " _
                            & " WHEN SEGNALAZIONI.ID_UNITA " _
                            & " IS NOT NULL " _
                            & " THEN " _
                            & " (SELECT MAX (COD_CONTRATTO) " _
                            & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
                            & " WHERE ID IN " _
                            & " (SELECT ID_CONTRATTO " _
                            & " FROM SISCOM_MI. " _
                            & " UNITA_CONTRATTUALE " _
                            & " WHERE UNITA_CONTRATTUALE. " _
                            & " ID_UNITA = " _
                            & " SEGNALAZIONI.ID_UNITA) " _
                            & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
                            & " 1, " _
                            & " 8) " _
                            & "  BETWEEN NVL ( " _
                            & " RAPPORTI_UTENZA. " _
                            & " DATA_DECORRENZA, " _
                            & " '10000000') " _
                            & " AND NVL ( " _
                            & " RAPPORTI_UTENZA. " _
                            & " DATA_RICONSEGNA, " _
                            & " '30000000')) " _
                            & " ELSE " _
                            & " NULL " _
                            & " END) " _
                            & " AS CODICE_RU, " _
                            & "(select count(*) from siscom_mi.segnalazioni_note where id_segnalazione = segnalazioni.id and sollecito = 1) as n_solleciti, " _
                            & " TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD') AS DATA_INSERIMENTO, " _
                            & " TO_DATE (SUBSTR (DATA_PROGRAMMATA_INT2, 1, 8), 'YYYYMMDD') AS DATA_PROGRAMMATA_INT2, " _
                            & " TO_DATE (substr(DATA_PROGRAMMATA_INT,1,8), 'YYYYMMDDHH') AS DATA_PROGRAMMATA_INT, " _
                            & " TO_DATE (SUBSTR (DATA_SOPRALLUOGO, 1, 8), 'YYYYMMDD') AS DATA_SOPRALLUOGO, " _
                            & " TO_DATE (SUBSTR (DATA_EFFETTIVA_INT, 1, 8), 'YYYYMMDD') AS DATA_EFFETTIVA_INT, " _
                            & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & " (case when upper(substr(TAB_FILIALI.NOME, 1, 18)) = 'SEDE TERRITORIALE ' and LENGTH(TAB_FILIALI.NOME)=19 then substr(TAB_FILIALI.NOME, 19, 1) else NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') end)  AS FILIALE, " _
                            & " (CASE WHEN segnalazioni.ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione, (select descrizione from siscom_mi.pericolo_segnalazioni where id = id_pericolo_Segnalazione) as pericolo_segnalazione" _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
                            & ",ID_sEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE " _
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
                            & " ,DATA_ORA_RICHIESTA, SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPOLOGIA, SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA AS CRONOPROGRAMMA, TAB_TIPOLOGIA_SEGNALANTE.DESCRIZIONE as TIPO_SEGNALANTE, OC.operatore   as OPERATORE_CH, " _
                            & " (CASE WHEN (SELECT COUNT (*) FROM SISCOM_MI.ALLEGATI_WS WHERE ID_OGGETTO = SEGNALAZIONI.ID AND STATO = 0) > 0 THEN 'SÌ' ELSE 'NO' END) AS ALLEGATI " _
                            & " FROM siscom_mi.tab_stati_segnalazioni, " _
                            & " siscom_mi.segnalazioni, " _
                            & " siscom_mi.tab_filiali, " _
                            & " siscom_mi.edifici, " _
                            & " siscom_mi.unita_immobiliari, " _
                            & " siscom_mi.TIPOLOGIE_GUASTI, " _
                            & " siscom_mi.combinazione_tipologie, " _
                            & " OPERATORI, siscom_mi.TAB_TIPOLOGIA_SEGNALANTE, OPERATORI OC,  " _
                            & " SISCOM_MI.SEGNALAZIONI_FORNITORI " _
                            & " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                            & filtroStato _
                            & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                            & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                            & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                            & " AND OC.ID(+) = segnalazioni.id_operatore_ch " _
                            & " And SEGNALAZIONI.ID = SEGNALAZIONI_FORNITORI.ID_SEGNALAZIONE(+) " _
                            & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
                            & " /*AND id_Segnalazione_padre is null AND ID_TIPO_SEGNALAZIONE = 1*/ " _
                            & " and combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                            & " and nvl(segnalazioni.id_tipologia_manutenzione,combinazione_tipologie.id_tipo_manutenzione) = 1 " _
                            & " AND segnalazioni.id_tipologia_segnalante = TAB_TIPOLOGIA_SEGNALANTE.ID(+) " _
                            & filtroFornitore _
                            & condizioneVista _
                            & filtroData
        '& "  union " _
        '& " SELECT SEGNALAZIONI.ID, " _
        '& " SEGNALAZIONI.ID AS NUM, " _
        '& " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
        ' & " (case when segnalazioni.id_tipo_segnalazione=1 then tipologie_guasti.descrizione else null end) AS tipo_int, " _
        '& " NVL((SELECT LTRIM(RTRIM((CASE WHEN ANNO IS NOT NULL THEN TO_CHAR(ANNO) ELSE SUBSTR(NUM_REPERTORIO,1,4) END)||LTRIM(RTRIM(TO_CHAR(PROGR,'0000000000'))))) FROM SISCOM_MI.APPALTI WHERE ID IN  (SELECT ID_GRUPPO FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID = SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA)),0) AS REP_ORD, " _
        '& " (SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID IN  (SELECT ID_GRUPPO FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID = SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA)) AS NUM_REPERTORIO, " _
        '& " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID = SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA)) AS FORNITORE, " _
        '& " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=SEGNALAZIONI.ID_TIPO_SEGNALAZIONE) AS TIPO0," _
        '& " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
        '& " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
        '& " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
        '& " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
        '& " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, segnalazioni.id_stato, " _
        '& " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
        '& " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
        '& " (CASE WHEN ID_UNITA IS NOT NULL THEN (SELECT MAX(DESCRIZIONE) FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID= ID_UNITA)) ELSE '' END) AS SCALA, " _
        '& " (CASE WHEN ID_UNITA IS NOT NULL THEN (SELECT MAX(DESCRIZIONE) FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID= ID_UNITA)) ELSE ' ' END) AS PIANO, " _
        '& " (CASE WHEN ID_UNITA IS NOT NULL THEN (SELECT MAX(INTERNO) FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = ID_UNITA) ELSE ' ' END) AS INTERNO, " _
        '& " SEGNALAZIONI.TELEFONO1, SEGNALAZIONI.TELEFONO2, " _
        '& " (CASE " _
        '& " WHEN SEGNALAZIONI.ID_UNITA " _
        '& " IS NOT NULL " _
        '& " THEN " _
        '& " (SELECT MAX (COD_CONTRATTO) " _
        '& " FROM SISCOM_MI.RAPPORTI_UTENZA " _
        '& " WHERE ID IN " _
        '& " (SELECT ID_CONTRATTO " _
        '& " FROM SISCOM_MI. " _
        '& " UNITA_CONTRATTUALE " _
        '& " WHERE UNITA_CONTRATTUALE. " _
        '& " ID_UNITA = " _
        '& " SEGNALAZIONI.ID_UNITA) " _
        '& " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
        '& " 1, " _
        '& " 8) " _
        '& "  BETWEEN NVL ( " _
        '& " RAPPORTI_UTENZA. " _
        '& " DATA_DECORRENZA, " _
        '& " '10000000') " _
        '& " AND NVL ( " _
        '& " RAPPORTI_UTENZA. " _
        '& " DATA_RICONSEGNA, " _
        '& " '30000000')) " _
        '& " ELSE " _
        '& " NULL " _
        '& " END) " _
        '& " AS CODICE_RU, " _
        '& "(select count(*) from siscom_mi.segnalazioni_note where id_segnalazione = segnalazioni.id and sollecito = 1) as n_solleciti, " _
        '& " TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD') AS DATA_INSERIMENTO, " _
        '& " TO_DATE (SUBSTR (DATA_PROGRAMMATA_INT2, 1, 8), 'YYYYMMDD') AS DATA_PROGRAMMATA_INT2, " _
        '& " TO_DATE (substr(DATA_PROGRAMMATA_INT,1,8), 'YYYYMMDDHH') AS DATA_PROGRAMMATA_INT, " _
        '& " TO_DATE (SUBSTR (DATA_SOPRALLUOGO, 1, 8), 'YYYYMMDD') AS DATA_SOPRALLUOGO, " _
        '& " TO_DATE (SUBSTR (DATA_EFFETTIVA_INT, 1, 8), 'YYYYMMDD') AS DATA_EFFETTIVA_INT, " _
        '& " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
        '& " (case when upper(substr(TAB_FILIALI.NOME, 1, 18)) = 'SEDE TERRITORIALE ' and LENGTH(TAB_FILIALI.NOME)=19 then substr(TAB_FILIALI.NOME, 19, 1) else NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') end)  AS FILIALE, " _
        '& " (CASE WHEN segnalazioni.ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
        '& " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
        '& " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione ,(select descrizione from siscom_mi.pericolo_segnalazioni where id = id_pericolo_Segnalazione) as pericolo_segnalazione" _
        '& " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
        '& " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
        '& " ,ID_SEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE" _
        '& ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
        '& ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
        '& " ,DATA_ORA_RICHIESTA,SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPOLOGIA, SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA AS CRONOPROGRAMMA, TAB_TIPOLOGIA_SEGNALANTE.DESCRIZIONE as TIPO_SEGNALANTE, OC.operatore  as OPERATORE_CH, " _
        '& " (CASE WHEN (SELECT COUNT (*) FROM SISCOM_MI.ALLEGATI_WS WHERE ID_OGGETTO = SEGNALAZIONI.ID AND STATO = 0) > 0 THEN 'SÌ' ELSE 'NO' END) AS ALLEGATI " _
        '& " FROM siscom_mi.tab_stati_segnalazioni, " _
        '& " siscom_mi.segnalazioni, " _
        '& " siscom_mi.tab_filiali, " _
        '& " siscom_mi.edifici, " _
        '& " siscom_mi.unita_immobiliari, " _
        '& " siscom_mi.TIPOLOGIE_GUASTI, " _
        '& " siscom_mi.combinazione_tipologie, " _
        '& " OPERATORI, siscom_mi.TAB_TIPOLOGIA_SEGNALANTE, OPERATORI OC, " _
        '& " SISCOM_MI.SEGNALAZIONI_FORNITORI " _
        '& " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
        '& filtroStato _
        '& " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
        '& " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
        '& " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
        '& " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
        '& " AND OC.ID(+) = segnalazioni.id_operatore_ch " _
        '& " And SEGNALAZIONI.ID = SEGNALAZIONI_FORNITORI.ID_SEGNALAZIONE(+) " _
        '& " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
        '& " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONE_PADRE " _
        '& " FROM SISCOM_MI.SEGNALAZIONI " _
        '& " ,siscom_mi.tab_filiali " _
        '& " WHERE ID_SEGNALAZIONE_PADRE IS NOT NULL " _
        '& " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
        '& ") /*AND ID_TIPO_SEGNALAZIONE = 1*/ " _
        '& " and combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
        '& " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
        '& " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
        '& " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
        '& " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
        '& " and combinazione_tipologie.id_tipo_manutenzione = 1 " _
        '& " AND segnalazioni.id_tipologia_segnalante = TAB_TIPOLOGIA_SEGNALANTE.ID(+) " _
        '& filtroFornitore _
        '& condizioneVista _
        '& filtroData 
        'EsportaQuerySegnalazioni = " SELECT SEGNALAZIONI.ID, " _
        '                    & " SEGNALAZIONI.ID AS NUM, " _
        '                    & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
        '                    & " (case when segnalazioni.id_tipo_segnalazione=1 then tipologie_guasti.descrizione else null end) AS tipo_int, " _
        '                    & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=SEGNALAZIONI.ID_TIPO_SEGNALAZIONE) AS TIPO0," _
        '                    & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
        '                    & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
        '                    & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
        '                    & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
        '                    & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, segnalazioni.id_stato, " _
        '                    & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
        '                    & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
        '                    & " (CASE " _
        '                    & " WHEN SEGNALAZIONI.ID_UNITA " _
        '                    & " IS NOT NULL " _
        '                    & " THEN " _
        '                    & " (SELECT MAX (COD_CONTRATTO) " _
        '                    & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
        '                    & " WHERE ID IN " _
        '                    & " (SELECT ID_CONTRATTO " _
        '                    & " FROM SISCOM_MI. " _
        '                    & " UNITA_CONTRATTUALE " _
        '                    & " WHERE UNITA_CONTRATTUALE. " _
        '                    & " ID_UNITA = " _
        '                    & " SEGNALAZIONI.ID_UNITA) " _
        '                    & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
        '                    & " 1, " _
        '                    & " 8) " _
        '                    & "  BETWEEN NVL ( " _
        '                    & " RAPPORTI_UTENZA. " _
        '                    & " DATA_DECORRENZA, " _
        '                    & " '10000000') " _
        '                    & " AND NVL ( " _
        '                    & " RAPPORTI_UTENZA. " _
        '                    & " DATA_RICONSEGNA, " _
        '                    & " '30000000')) " _
        '                    & " ELSE " _
        '                    & " NULL " _
        '                    & " END) " _
        '                    & " AS CODICE_RU, " _
        '                    & "(select count(*) from siscom_mi.segnalazioni_note where id_segnalazione = segnalazioni.id and sollecito = 1) as n_solleciti, " _
        '                    & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
        '                    & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
        '                    & " NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
        '                    & " (CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
        '                    & " NOTE LIKE '%(nota chiusura)%'AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND note LIKE '%(nota chiusura)%')" _
        '                    & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
        '                    & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
        '                    & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
        '                    & ",ID_sEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE " _
        '                    & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
        '                    & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
        '                    & " ,DATA_ORA_RICHIESTA, SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPOLOGIA " _
        '                    & " FROM siscom_mi.tab_stati_segnalazioni, " _
        '                    & " siscom_mi.segnalazioni, " _
        '                    & " siscom_mi.tab_filiali, " _
        '                    & " siscom_mi.edifici, " _
        '                    & " siscom_mi.unita_immobiliari, " _
        '                    & " siscom_mi.TIPOLOGIE_GUASTI, " _
        '                    & " siscom_mi.combinazione_tipologie, " _
        '                    & " OPERATORI " _
        '                    & " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
        '                    & " AND segnalazioni.id_stato  in (0,6,7) " _
        '                    & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
        '                    & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
        '                    & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
        '                    & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
        '                    & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
        '                    & " AND id_Segnalazione_padre is null  /*AND ID_TIPO_SEGNALAZIONE = 1*/ " _
        '                    & " and combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
        '                    & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
        '                    & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
        '                    & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
        '                    & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
        '                    & " and nvl(segnalazioni.id_tipologia_manutenzione,combinazione_tipologie.id_tipo_manutenzione) = 1 " _
        '                    & " and (select id_fornitore from siscom_mi.programma_attivita where programma_attivita.id = SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA) in " & iIndiceFornitore _
        '                    & filtroFornitore _
        '                    & "  union " _
        '                    & " SELECT SEGNALAZIONI.ID, " _
        '                    & " SEGNALAZIONI.ID AS NUM, " _
        '                    & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
        '                     & " (case when segnalazioni.id_tipo_segnalazione=1 then tipologie_guasti.descrizione else null end) AS tipo_int, " _
        '                    & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=SEGNALAZIONI.ID_TIPO_SEGNALAZIONE) AS TIPO0," _
        '                    & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
        '                    & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
        '                    & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
        '                    & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
        '                    & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, segnalazioni.id_stato, " _
        '                    & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
        '                    & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
        '                    & " (CASE " _
        '                    & " WHEN SEGNALAZIONI.ID_UNITA " _
        '                    & " IS NOT NULL " _
        '                    & " THEN " _
        '                    & " (SELECT MAX (COD_CONTRATTO) " _
        '                    & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
        '                    & " WHERE ID IN " _
        '                    & " (SELECT ID_CONTRATTO " _
        '                    & " FROM SISCOM_MI. " _
        '                    & " UNITA_CONTRATTUALE " _
        '                    & " WHERE UNITA_CONTRATTUALE. " _
        '                    & " ID_UNITA = " _
        '                    & " SEGNALAZIONI.ID_UNITA) " _
        '                    & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
        '                    & " 1, " _
        '                    & " 8) " _
        '                    & "  BETWEEN NVL ( " _
        '                    & " RAPPORTI_UTENZA. " _
        '                    & " DATA_DECORRENZA, " _
        '                    & " '10000000') " _
        '                    & " AND NVL ( " _
        '                    & " RAPPORTI_UTENZA. " _
        '                    & " DATA_RICONSEGNA, " _
        '                    & " '30000000')) " _
        '                    & " ELSE " _
        '                    & " NULL " _
        '                    & " END) " _
        '                    & " AS CODICE_RU, " _
        '                    & "(select count(*) from siscom_mi.segnalazioni_note where id_segnalazione = segnalazioni.id and sollecito = 1) as n_solleciti, " _
        '                    & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
        '                    & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
        '                    & " NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
        '                    & " (CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
        '                    & " NOTE LIKE '%(nota chiusura)%'AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND note LIKE '%(nota chiusura)%')" _
        '                    & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
        '                    & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
        '                    & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
        '                    & " ,ID_SEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE" _
        '                    & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
        '                    & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
        '                    & " ,DATA_ORA_RICHIESTA,SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPOLOGIA " _
        '                    & " FROM siscom_mi.tab_stati_segnalazioni, " _
        '                    & " siscom_mi.segnalazioni, " _
        '                    & " siscom_mi.tab_filiali, " _
        '                    & " siscom_mi.edifici, " _
        '                    & " siscom_mi.unita_immobiliari, " _
        '                    & " siscom_mi.TIPOLOGIE_GUASTI, " _
        '                    & " siscom_mi.combinazione_tipologie, " _
        '                    & " OPERATORI " _
        '                    & " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
        '                    & " /*AND segnalazioni.id_stato in (0,6,7)*/ " _
        '                    & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
        '                    & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
        '                    & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
        '                    & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
        '                    & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
        '                    & " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONE_PADRE " _
        '                    & " FROM SISCOM_MI.SEGNALAZIONI " _
        '                    & " ,siscom_mi.tab_filiali " _
        '                    & " WHERE ID_SEGNALAZIONE_PADRE IS NOT NULL " _
        '                    & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
        '                    & ") /*AND ID_TIPO_SEGNALAZIONE = 1*/ " _
        '                    & " and combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
        '                    & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
        '                    & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
        '                    & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
        '                    & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
        '                    & " and combinazione_tipologie.id_tipo_manutenzione = 1 " _
        '                    & " and (select id_fornitore from siscom_mi.programma_attivita where programma_attivita.id = SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA) in " & iIndiceFornitore _
        '                    & filtroFornitore

    End Function

    Private Sub dgvSegnalazioni_PreRender(sender As Object, e As EventArgs) Handles dgvSegnalazioni.PreRender
        dgvSegnalazioni.MasterTableView.GetColumn("PERICOLO_SEGNALAZIONE").Display = False
        dgvSegnalazioni.Rebind()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Private Sub dgvSegnalazioni_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgvSegnalazioni.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            If dataItem("TIPOLOGIA").Text = "1" Then
                Select Case dataItem("ID_PERICOLO_SEGNALAZIONE").Text
                    Case "1"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "2"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "3"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "4"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "0"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case Else
                End Select
                CType(dataItem("DESCRIZIONE"), TableCell).ToolTip = CType(dataItem("DESCRIZIONE"), TableCell).Text
                If Trim(Len(CType(dataItem("DESCRIZIONE"), TableCell).Text)) > 50 Then
                    CType(dataItem("DESCRIZIONE"), TableCell).Text = Mid(CType(dataItem("DESCRIZIONE"), TableCell).Text, 1, 50) & "..."
                End If
            End If
        ElseIf TypeOf e.Item Is GridFilteringItem Then
            Dim ApertaNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                Me.connData = New CM.datiConnessione(par, False, False)
                connData.apri(False)
                ApertaNow = True
            End If
            par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.PERICOLO_SEGNALAZIONI ORDER BY 2 ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Dim item As RadComboBoxItem
            item = New RadComboBoxItem()
            item.Text = "Tutti"
            item.Value = "Tutti"
            TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroPericolo"), RadComboBox).Items.Add(item)
            For Each row As Data.DataRow In dt.Rows
                item = New RadComboBoxItem()
                item.Text = par.IfNull(row.Item("DESCRIZIONE"), "")
                item.Value = par.IfNull(row.Item("DESCRIZIONE"), "")
                Select Case par.IfNull(row.Item("ID"), "").ToString
                    Case "1"
                        item.ImageUrl = "../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-white-128.png"
                    Case "2"
                        item.ImageUrl = "../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-green-128.png"
                    Case "3"
                        item.ImageUrl = "../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-yellow-128.png"
                    Case "4"
                        item.ImageUrl = "../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-red-128.png"
                    Case "0"
                        item.ImageUrl = "../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-blue-128.png"
                End Select
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroPericolo"), RadComboBox).Items.Add(item)
            Next


            If Not String.IsNullOrEmpty(Trim(HFFiltroPericolo.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroPericolo"), RadComboBox).SelectedValue = HFFiltroPericolo.Value.ToString
            End If

            par.caricaComboTelerik("select distinct APPALTI.NUM_REPERTORIO  from SISCOM_MI.APPALTI order by 1 desc", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroRE"), RadComboBox), "NUM_REPERTORIO", "NUM_REPERTORIO", True, "Tutti", "Tutti")
            If Not String.IsNullOrEmpty(Trim(HFFiltroRE.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroRE"), RadComboBox).SelectedValue = HFFiltroRE.Value.ToString
            End If
            'par.caricaComboTelerik("select distinct TAB_FILIALI.ID,  TAB_FILIALI.NOME FROM SISCOM_MI.EDIFICI INNER JOIN SISCOM_MI.COMPLESSI_IMMOBILIARI ON EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID INNER JOIN SISCOM_MI.TAB_FILIALI ON COMPLESSI_IMMOBILIARI.ID_FILIALE = TAB_FILIALI.ID  order by 2", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroST"), RadComboBox), "NOME", "NOME", True, "Tutti", "Tutti")
            par.caricaComboTelerik("select distinct TAB_FILIALI.ID,  case when upper(substr(NOME, 1, 18)) = 'SEDE TERRITORIALE ' and LENGTH(NOME)=19   then substr(TAB_FILIALI.NOME, 19, 1) else TAB_FILIALI.NOME end as nome FROM SISCOM_MI.EDIFICI INNER JOIN SISCOM_MI.COMPLESSI_IMMOBILIARI ON EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID INNER JOIN SISCOM_MI.TAB_FILIALI ON COMPLESSI_IMMOBILIARI.ID_FILIALE = TAB_FILIALI.ID  order by 2", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroST"), RadComboBox), "NOME", "NOME", True, "Tutti", "Tutti")
            If Not String.IsNullOrEmpty(Trim(HFFiltroST.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroST"), RadComboBox).SelectedValue = HFFiltroST.Value.ToString
            End If

            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI ORDER BY ID ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStato"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            If Not String.IsNullOrEmpty(Trim(HFFiltroStato.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStato"), RadComboBox).SelectedValue = HFFiltroStato.Value.ToString
            End If

            par.caricaComboTelerik("SELECT 'SI' AS DESCRIZIONE FROM DUAL UNION SELECT 'NO' AS DESCRIZIONE FROM DUAL", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroAL"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            If Not String.IsNullOrEmpty(Trim(HFFiltroAL.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroAL"), RadComboBox).SelectedValue = HFFiltroAL.Value.ToString
            End If

            par.caricaComboTelerik("select FORNITORI.ragione_sociale from siscom_mi.fornitori  ORDER BY FORnITORI.RAGIONE_SOCIALE ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroFO"), RadComboBox), "RAGIONE_SOCIALE", "RAGIONE_SOCIALE", True, "Tutti", "Tutti")
            If Not String.IsNullOrEmpty(Trim(HFFiltroFO.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroFO"), RadComboBox).SelectedValue = HFFiltroFO.Value.ToString
            End If

        End If
    End Sub

    Private Sub VerificaOperatore()
        Try
            connData.apri()
            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("LIVELLO_WEB"), "0") = "1" Then
                    iIndiceFornitore = "0"
                    IndiceFornitore.Value = "0"
                    OPS.Value = "0"
                Else
                    If par.IfNull(myReader("MOD_FO_ID_FO"), "0") <> "0" Then
                        iIndiceFornitore = "(" & par.IfNull(myReader("MOD_FO_ID_FO"), 0) & ")"
                        IndiceFornitore.Value = par.IfNull(myReader("MOD_FO_ID_FO"), "0")
                        OPS.Value = "1"

                    Else
                        iIndiceFornitore = "0"
                        IndiceFornitore.Value = "0"
                        iIndiceFornitore = "SELECT DISTINCT APPALTI.ID_FORNITORE " _
                                            & "FROM SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.EDIFICI,SISCOM_MI.BUILDING_MANAGER_OPERATORI " _
                                            & "WHERE Appalti.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO = EDIFICI.ID " _
                                            & "And EDIFICI.ID_BM = BUILDING_MANAGER_OPERATORI.ID_BM AND BUILDING_MANAGER_OPERATORI.ID_OPERATORE = " & Session.Item("id_operatore")
                        'Dim myReaderBM As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        'Do While myReaderBM.Read
                        '    iIndiceFornitore = iIndiceFornitore & myReaderBM("ID_FORNITORE") & ","
                        'Loop
                        'myReaderBM.Close()
                        If iIndiceFornitore <> "0" Then
                            iIndiceFornitore = "(" & Mid(iIndiceFornitore, 1, Len(iIndiceFornitore) - 1) & ")"
                            IndiceFornitore.Value = "1"
                        Else
                            iIndiceFornitore = "SELECT DISTINCT APPALTI.ID_FORNITORE FROM SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_DL WHERE APPALTI.ID=APPALTI_DL.ID_GRUPPO AND APPALTI_DL.ID_OPERATORE=" & Session.Item("id_operatore")
                            'Dim myReaderDL As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'Do While myReaderDL.Read
                            '    iIndiceFornitore = iIndiceFornitore & myReaderDL("ID_FORNITORE") & ","
                            'Loop
                            'myReaderDL.Close()
                            If iIndiceFornitore <> "0" Then
                                iIndiceFornitore = "(" & Mid(iIndiceFornitore, 1, Len(iIndiceFornitore)) & ")"
                                IndiceFornitore.Value = "1"
                            End If
                        End If
                        OPS.Value = "0"
                    End If
                End If

            End If
            myReader.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - VerificaOperatore - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadButtonBuildingManager_Click(sender As Object, e As System.EventArgs) Handles RadButtonBuildingManager.Click
        dgvSegnalazioni.Rebind()

    End Sub
    Protected Sub RadButtonDirettoreLavori_Click(sender As Object, e As System.EventArgs) Handles RadButtonDirettoreLavori.Click
        dgvSegnalazioni.Rebind()

    End Sub
    Protected Sub RadButtonFieldQualityManager_Click(sender As Object, e As System.EventArgs) Handles RadButtonFieldQualityManager.Click
        dgvSegnalazioni.Rebind()

    End Sub
    Protected Sub RadButtonTecnicoAmministrativo_Click(sender As Object, e As System.EventArgs) Handles RadButtonTecnicoAmministrativo.Click
        dgvSegnalazioni.Rebind()

    End Sub

    Private Sub btnEsporta_Click(sender As Object, e As EventArgs) Handles btnEsporta.Click
        Try
            'Dim connAperta As Boolean = False
            'If connData.Connessione.State = Data.ConnectionState.Closed Then
            '    connData.apri(False)
            '    connAperta = True
            'End If

            'Dim dt As Data.DataTable = par.getDataTableFilterSortRadGrid(EsportaQuerySegnalazioni(), dgvSegnalazioni)
            'Dim xls As New ExcelSiSol
            'Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SegnalazioniCanone_", "SegnalazioniCanone_", dt, True)

            'par.EffettuaDownloadFile(Me.Page, nomeFile)
            'If connAperta = True Then
            '    connData.chiudi(False)
            'End If


            connData.apri(False)
            Dim dt As New Data.DataTable
            dt = par.getDataTableFilterSortRadGrid(EsportaQuerySegnalazioni, dgvSegnalazioni)
            connData.chiudi(False)
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SegnalazioniCanone", "SegnalazioniCanone", dt)
                If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Si è verificato un errore durante l\'esportazione. Riprovare!", 450, 150, "Attenzione", Nothing)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "key", "function f(){NotificaTelerik('" & CType(Me.Master.FindControl("RadNotificationNote"), RadNotification).ClientID & "', 'Attenzione', '" & par.Messaggio_NoExport & "'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", True)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - Piano - btnDownload_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Function getImgPericoloRichiesto(ByVal Priorita As String) As String
        getImgPericoloRichiesto = ""
        Select Case Priorita
            Case "1"
                getImgPericoloRichiesto = "<img alt='Priorità Segnalazione' src='../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-white-128.png' />"
            Case "2"
                getImgPericoloRichiesto = "<img alt='Priorità Segnalazione' src='../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-green-128.png' />"
            Case "3"
                getImgPericoloRichiesto = "<img alt='Priorità Segnalazione' src='../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-yellow-128.png' />"
            Case "4"
                getImgPericoloRichiesto = "<img alt='Priorità Segnalazione' src='../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-red-128.png' />"
            Case "0"
                getImgPericoloRichiesto = "<img alt='Priorità Segnalazione' src='../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-blue-128.png' />"




        End Select
    End Function

    Private Sub chkSoloChiuse_CheckedChanged(sender As Object, e As EventArgs) Handles chkSoloChiuse.CheckedChanged
        dgvSegnalazioni.Rebind()
    End Sub


End Class
