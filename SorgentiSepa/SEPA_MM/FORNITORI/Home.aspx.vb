
Imports Telerik.Web.UI

Partial Class FORNITORI_Default
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        Session.Item("MLoading") = ""
        If Not IsPostBack Then
            CaricaViste()

        End If
    End Sub

    Private Sub FORNITORI_Default_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        If Not String.IsNullOrEmpty(Request.QueryString("FORNITORI")) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "location.href='CaricaPiani.aspx';", True)
        End If
        RicavaNotifiche()
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
        If RadButtonBuildingManager.Visible = True Then
            RadButtonBuildingManager.Checked = True
        ElseIf RadButtonDirettoreLavori.Visible = True Then
            RadButtonDirettoreLavori.Checked = True
        ElseIf RadButtonFieldQualityManager.Visible = True Then
            RadButtonFieldQualityManager.Checked = True
        ElseIf RadButtonTecnicoAmministrativo.Visible = True Then
            RadButtonTecnicoAmministrativo.Checked = True
        End If
    End Sub
    Private Sub RicavaNotifiche()
        Dim NumeroNotifiche As Long = 0
        Dim literalNotifiche As Literal = CType(Me.Master.FindControl("RadNotification1"), RadNotification).ContentContainer.FindControl("lbl")
        Dim TestoNotifiche As String = ""
        Try
            Dim s As String = ""
            connData.apri(False)
            If RadButtonBuildingManager.Checked = True Then
                CType(Me.Master.FindControl("lblOperatore"), Label).Text = Session.Item("OPERATORE") & " (BM)"
                s &= " siscom_mi.getbuildingmanager(MANUTENZIONI.ID,1) like '%" & Session.Item("ID_OPERATORE") & "%' AND "
            End If
            If RadButtonDirettoreLavori.Checked = True And Session.Item("FL_SUPERDIRETTORE") <> "1" Then
                CType(Me.Master.FindControl("lblOperatore"), Label).Text = Session.Item("OPERATORE") & " (DL)"
                Dim filtro As String = "SELECT DISTINCT APPALTI.ID AS ID_APPALTO " _
                                       & " FROM SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_DL " _
                                       & " WHERE APPALTI.ID=APPALTI_DL.ID_GRUPPO And APPALTI_DL.ID_OPERATORE=" & Session.Item("id_operatore")
                s = " SEGNALAZIONI_FORNITORI.ID_APPALTO IN (" & filtro & ") And "
                CType(Me.Master.FindControl("lblOperatore"), Label).Text = Session.Item("OPERATORE") & " (DL)"
            End If
            If RadButtonDirettoreLavori.Checked = True Or RadButtonFieldQualityManager.Checked = True Or Session.Item("FL_SUPERDIRETTORE") = "1" Then
                If RadButtonFieldQualityManager.Checked = True Then
                    CType(Me.Master.FindControl("lblOperatore"), Label).Text = Session.Item("OPERATORE") & " (FQM)"
                End If
                If Session.Item("FL_SUPERDIRETTORE") = "1" Then
                    CType(Me.Master.FindControl("lblOperatore"), Label).Text = Session.Item("OPERATORE") & " (GESTORE DL)"
                End If
                Dim filtroDL As String = ""
                If Session.Item("FL_SUPERDIRETTORE") <> "1" And RadButtonFieldQualityManager.Checked = False Then
                    filtroDL = "AND ID_PROGRAMMA_ATTIVITA IN (SELECT ID FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO = '30000000' AND ID_OPERATORE = " & Session.Item("ID_OPERATORE") & "))"
                End If
                Dim idTipologia As String = "-1"
                par.cmd.CommandText = "SELECT count(*) " _
                                                        & " FROM SISCOM_MI.combinazione_tipologie, SISCOM_MI.SEGNALAZIONI " _
                                                        & " WHERE combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                                                        & " And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                                                        & " And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                                                        & " And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                                                        & " And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                                                        & " And segnalazioni.id_tipologia_manutenzione<>combinazione_tipologie.id_tipo_manutenzione " _
                                                        & " And segnalazioni.id_tipologia_manutenzione Is Not null And segnalazioni.fl_tipologia_confermata = 0 " _
                                                        & " And fl_rich_mod_tipologia = 1 " _
                                                        & filtroDL
                Dim RicavaNumeroManutenzioniAlert = CInt(par.IfNull(par.cmd.ExecuteScalar, "0"))
                If RicavaNumeroManutenzioniAlert > 0 Then
                    TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Segnalazioni a canone con tipologia modificata dal fornitore!", "CenterPage2('../GESTIONE_CONTATTI/SegnalazioniAlert.aspx?NM=1', 'Segnalazioni');", False)
                    NumeroNotifiche = NumeroNotifiche + 1
                End If
            End If
            If RadButtonTecnicoAmministrativo.Checked = True Then
                s = " ( " _
                  & " case when manutenzioni.id_complesso is not null then (select distinct id_filiale from SISCOM_MI.complessi_immobiliari where id = manutenzioni.id_complesso) " _
                  & " when manutenzioni.id_edificio is not null then (select id_filiale from siscom_mi.complessi_immobiliari where id in (select distinct id_complesso from SISCOM_MI.edifici where id = manutenzioni.id_edificio)) " _
                  & " when MANUTENZIONI.ID_UNITA_IMMOBILIARI is not null then (select distinct id_filiale from SISCOM_MI.complessi_immobiliari where id in (select id_complesso from SISCOM_MI.edifici where edifici.id = (select id_edificio from SISCOM_MI.unita_immobiliari where id = manutenzioni.ID_UNITA_IMMOBILIARI))) " _
                  & " end " _
                  & " ) = " & Session.Item("ID_STRUTTURA") & " AND "
                CType(Me.Master.FindControl("lblOperatore"), Label).Text = Session.Item("OPERATORE") & " (TA)"
            End If
            Dim lOperatoreTIPO As Integer = 0
            Dim lOperatoreDitta As Integer = 0
            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
            Dim myReaderFO As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderFO.Read Then
                If par.IfNull(myReaderFO("MOD_FO_ID_FO"), "0") <> "0" Then
                    CType(Me.Master.FindControl("lblOperatore"), Label).Text = Session.Item("OPERATORE") & " (FORNITORE)"
                    lOperatoreTIPO = 2
                End If
                lOperatoreDitta = par.IfNull(myReaderFO("MOD_FO_ID_FO"), -1)
            End If
            myReaderFO.Close()
            If lOperatoreTIPO = 2 Then
                If lOperatoreDitta > 0 Then
                    s = " SEGNALAZIONI_FORNITORI.ID_appalto in (select id from siscom_mi.appalti where id_fornitore = " & lOperatoreDitta & ") AND "
                End If
            End If
            'SEGNALAZIONI LATO FORNITORE
            If Session.Item("MOD_FORNITORI_SLE") = "0" Then
                If lOperatoreTIPO = 2 Then

                    par.cmd.CommandText = "select SEGNALAZIONI_FORNITORI.ID AS IDS,MANUTENZIONI.PROGR,MANUTENZIONI.ANNO " _
                                    & " from siscom_mi.SEGNALAZIONI_FORNITORI, SISCOM_MI.MANUTENZIONI " _
                                    & " where  MANUTENZIONI.STATO not in (0,2,5,6) AND MANUTENZIONI.ID_PAGAMENTO IS NULL  and " & s & " nvl(fl_rdo,0)=1 And MANUTENZIONI.ID=SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE And SEGNALAZIONI_FORNITORI.ID Not IN (SELECT ID_SEGNALAZIONE FROM SISCOM_MI.SEGNALAZIONI_FO_PREVENTIVI) And manutenzioni.stato<>2 And manutenzioni.id_appalto in (select id from SISCOM_MI.appalti where modulo_fornitori=1 And modulo_fornitori_ge=1) order by SEGNALAZIONI_FORNITORI.id desc"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Preventivo richiesto per la segnalazione " & myReader("progr") & "/" & myReader("anno") & " !", "ApriModuloStandard('Intervento.aspx?D=' +" & myReader("IDS") & ", 'Intervento_" & myReader("IDS") & "'), 'Segnalazioni');", False)
                        NumeroNotifiche = NumeroNotifiche + 1
                    Loop
                    myReader.Close()
                    par.cmd.CommandText = "select SEGNALAZIONI_FORNITORI.ID AS IDS,MANUTENZIONI.PROGR,MANUTENZIONI.ANNO " _
                                        & " from siscom_mi.SEGNALAZIONI_FORNITORI,SISCOM_MI.MANUTENZIONI " _
                                        & " where MANUTENZIONI.STATO not in (0,2,5,6) AND MANUTENZIONI.ID_PAGAMENTO IS NULL  and " & s & " SEGNALAZIONI_FORNITORI.id_stato=1 And nvl(fl_rdo,0)=0 And MANUTENZIONI.ID=SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE And SEGNALAZIONI_FORNITORI.ID Not IN (SELECT ID_SEGNALAZIONE FROM SISCOM_MI.SEGNALAZIONI_FO_PREVENTIVI) And manutenzioni.stato<>2 And manutenzioni.id_appalto in (select id from SISCOM_MI.appalti where modulo_fornitori=1 And modulo_fornitori_ge=1) order by SEGNALAZIONI_FORNITORI.id desc"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Ordine Emesso " & myReader("progr") & "/" & myReader("anno") & " !", "ApriModuloStandard('Intervento.aspx?D=' +" & myReader("IDS") & ", 'Intervento_" & myReader("IDS") & "')", False)
                        NumeroNotifiche = NumeroNotifiche + 1
                    Loop
                    myReader.Close()
                    par.cmd.CommandText = "select SEGNALAZIONI_FORNITORI.ID AS IDS,MANUTENZIONI.PROGR,MANUTENZIONI.ANNO " _
                                        & " from siscom_mi.SEGNALAZIONI_FORNITORI,SISCOM_MI.MANUTENZIONI " _
                                        & " where MANUTENZIONI.STATO not in (0,2,5,6) AND MANUTENZIONI.ID_PAGAMENTO IS NULL  and " & s & " MANUTENZIONI.ID=SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE And SEGNALAZIONI_FORNITORI.id_stato=9 And SEGNALAZIONI_FORNITORI.FL_PR_CONTAB=1 And manutenzioni.stato<>2 And manutenzioni.id_appalto in (select id from SISCOM_MI.appalti where modulo_fornitori=1 And modulo_fornitori_ge=1) order by SEGNALAZIONI_FORNITORI.id desc"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Richiesto consuntivo ODL per ordine " & myReader("progr") & "/" & myReader("anno") & " !", "ApriModuloStandard('Intervento.aspx?D=' +" & myReader("IDS") & ", 'Intervento_" & myReader("IDS") & "')", False)
                        NumeroNotifiche = NumeroNotifiche + 1
                    Loop
                    myReader.Close()

                    par.cmd.CommandText = "SELECT SEGNALAZIONI_FORNITORI.ID AS IDS,MANUTENZIONI.PROGR,MANUTENZIONI.ANNO " _
                                        & " FROM siscom_mi.SEGNALAZIONI_FORNITORI, SISCOM_MI.MANUTENZIONI,SISCOM_MI.SEGNALAZIONI_FO_IRR " _
                                        & " WHERE MANUTENZIONI.STATO not in (0,2,5,6) AND MANUTENZIONI.ID_PAGAMENTO IS NULL  and " & s & " MANUTENZIONI.STATO Not IN (2,5,6) And SEGNALAZIONI_FO_IRR.id_segnalazione=segnalazioni_fornitori.id And MANUTENZIONI.ID = SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE And SEGNALAZIONI_FO_IRR.VISIBILE=1 And manutenzioni.id_appalto in (select id from SISCOM_MI.appalti where modulo_fornitori=1 And modulo_fornitori_ge=1) ORDER BY SEGNALAZIONI_FORNITORI.id DESC"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Inserita Non Conformità per ordine " & myReader("progr") & "/" & myReader("anno") & " !", "ApriModuloStandard('Intervento.aspx?D=' +" & myReader("IDS") & ", 'Intervento_" & myReader("IDS") & "')", False)
                        NumeroNotifiche = NumeroNotifiche + 1
                    Loop
                    myReader.Close()

                    '//////////////////////////////////////
                    '// 1218/2019
                    par.cmd.CommandText = "select siscom_mi.eventi_segnalazioni.ID_SEGNALAZIONE as IDS " _
                                        & " from siscom_mi.SEGNALAZIONI, siscom_mi.eventi_segnalazioni " _
                                        & " WHERE eventi_segnalazioni.id_segnalazione = SEGNALAZIONI.id And  " _
                                        & " (Select ID_GRUPPO  FROM SISCOM_MI.PROGRAMMA_ATTIVITA  " _
                                        & " WHERE PROGRAMMA_ATTIVITA.ID = SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA) " _
                                        & " In (Select ID_GRUPPO FROM SISCOM_MI.OPERATORI_FO_APPALTI " _
                                        & " WHERE ID_OPERATORE = " & Session.Item("id_operatore") & ") " _
                                        & " And SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802' " _
                                        & " AND segnalazioni.id_stato  in (0,6,7)  " _
                                        & " And motivazione Like '%Richiesta proposta di ricategorizzazione non accettata%'"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Richiesta proposta di ricategorizzazione non accettata " & myReader("IDS"), "CenterPage2('../GESTIONE_CONTATTI/Segnalazione.aspx?TIPO=1&NM=1&IDS=' +" & myReader("IDS") & ", 'Intervento_" & myReader("IDS") & "', 1200, 800)", False)
                        NumeroNotifiche = NumeroNotifiche + 1
                    Loop
                    myReader.Close()
                    '/////////////////////////////////////

                Else
                    par.cmd.CommandText = "select SEGNALAZIONI_FORNITORI.ID AS IDS, MANUTENZIONI.PROGR, MANUTENZIONI.ANNO " _
                                        & " from siscom_mi.SEGNALAZIONI_FORNITORI,SISCOM_MI.MANUTENZIONI " _
                                        & " where MANUTENZIONI.STATO not in (0,2,5,6) AND MANUTENZIONI.ID_PAGAMENTO IS NULL  and " & s & " MANUTENZIONI.ID=SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE " _
                                        & "  And SEGNALAZIONI_FORNITORI.id_stato=3 And SEGNALAZIONI_FORNITORI.FL_PR_CONTAB=2 " _
                                        & " And manutenzioni.stato<>2 And manutenzioni.id_appalto in " _
                                        & " (select id from SISCOM_MI.appalti where modulo_fornitori=1 And modulo_fornitori_ge=1) " _
                                        & " order by SEGNALAZIONI_FORNITORI.id desc"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Richiesto consuntivo ODL per ordine " & myReader("progr") & "/" & myReader("anno") & " !", "ApriModuloStandard('DettaglioOrdine.aspx?T=X&D=" & myReader("PROGR") & "_" & myReader("ANNO") & "', 'Intervento_" & myReader("IDS") & "')", False)
                        NumeroNotifiche = NumeroNotifiche + 1
                    Loop
                    myReader.Close()
                    par.cmd.CommandText = "select SEGNALAZIONI_FORNITORI.ID AS IDS,MANUTENZIONI.PROGR,MANUTENZIONI.ANNO " _
                                        & " from siscom_mi.SEGNALAZIONI_FORNITORI,SISCOM_MI.MANUTENZIONI " _
                                        & " where  MANUTENZIONI.STATO not in (0,2,5,6) AND MANUTENZIONI.ID_PAGAMENTO IS NULL  and " & s & " MANUTENZIONI.ID=SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE " _
                                        & " And SEGNALAZIONI_FORNITORI.id_stato=8 And SEGNALAZIONI_FORNITORI.FL_PR_CONTAB=0 " _
                                        & " And manutenzioni.stato<>2 And manutenzioni.id_appalto in " _
                                        & " (select id from SISCOM_MI.appalti where modulo_fornitori=1 And modulo_fornitori_ge=1) " _
                                        & " order by SEGNALAZIONI_FORNITORI.id desc"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Verificare ordine evaso " & myReader("progr") & "/" & myReader("anno") & "!", "ApriModuloStandard('DettaglioOrdine.aspx?T=X&D=" & myReader("PROGR") & "_" & myReader("ANNO") & "', 'Intervento_" & myReader("IDS") & "')", False)
                        NumeroNotifiche = NumeroNotifiche + 1
                    Loop
                    myReader.Close()

                    par.cmd.CommandText = "select SEGNALAZIONI_FORNITORI.ID AS IDS,MANUTENZIONI.PROGR,MANUTENZIONI.ANNO,data_inizio_intervento," _
                                        & " manutenzioni.data_fine_intervento,data_pgi,data_tdl," _
                                        & " segnalazioni_fornitori.data_fine_intervento as fine_lavori " _
                                        & " from siscom_mi.manutenzioni,siscom_mi.segnalazioni_fornitori " _
                                        & " where  MANUTENZIONI.STATO not in (0,2,5,6) AND MANUTENZIONI.ID_PAGAMENTO IS NULL  and " & s _
                                        & " manutenzioni.id=segnalazioni_fornitori.id_manutenzione " _
                                        & " And (data_tdl>manutenzioni.data_fine_intervento Or " _
                                        & " segnalazioni_fornitori.data_fine_intervento>manutenzioni.data_fine_intervento) " _
                                        & " And manutenzioni.data_fine_intervento Is Not null And " _
                                        & " manutenzioni.id_appalto in (select id from SISCOM_MI.appalti where modulo_fornitori=1 " _
                                        & " And modulo_fornitori_ge=1) " _
                                        & " order by SEGNALAZIONI_FORNITORI.id desc"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        par.cmd.CommandText = "select * from siscom_mi.eventi_segnalazioni_fo where cod_evento in ('F272','F268') and ID_SEGNALAZIONE_FO=" & myReader("IDS") & " AND MOTIVAZIONE LIKE '%" & par.FormattaData(par.IfNull(myReader("DATA_PGI"), "19000101")) & " - " & par.FormattaData(par.IfNull(myReader("DATA_TDL"), "19000101")) & "%'"
                        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader5.HasRows = False Then
                            If par.IfNull(myReader("fine_lavori"), "29991231") > par.IfNull(myReader("DATA_TDL"), "29991231") Then
                                TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Data TDL o Fine Intervento Fornitore oltre i limiti " & myReader("progr") & "/" & myReader("anno") & " !", "ApriModuloStandard('DettaglioOrdine.aspx?T=X&D=" & myReader("PROGR") & "_" & myReader("ANNO") & "', 'Intervento_" & myReader("IDS") & "')", False)
                                NumeroNotifiche = NumeroNotifiche + 1
                            End If
                        End If
                        myReader5.Close()
                    Loop
                    myReader.Close()
                    par.cmd.CommandText = "select SEGNALAZIONI_FORNITORI.ID AS IDS,MANUTENZIONI.PROGR,MANUTENZIONI.ANNO " _
                                        & " from siscom_mi.SEGNALAZIONI_FORNITORI,SISCOM_MI.MANUTENZIONI where  MANUTENZIONI.STATO not in (0,2,5,6) AND MANUTENZIONI.ID_PAGAMENTO IS NULL and " _
                                        & s & " MANUTENZIONI.ID=SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE And SEGNALAZIONI_FORNITORI.id_stato=1 " _
                                        & " And nvl(manutenzioni.data_inizio_intervento,'19000101')<='" & Format(Now, "yyyyMMdd") & "'" _
                                        & " and manutenzioni.id_appalto in (select id from SISCOM_MI.appalti where modulo_fornitori=1 " _
                                        & " and modulo_fornitori_ge=1) order by SEGNALAZIONI_FORNITORI.id desc"
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Verificare ordine NON preso in carico " & myReader("progr") & "/" & myReader("anno") & " !", "ApriModuloStandard('DettaglioOrdine.aspx?T=X&D=" & myReader("PROGR") & "_" & myReader("ANNO") & "', 'Intervento_" & myReader("IDS") & "')", False)
                        NumeroNotifiche = NumeroNotifiche + 1
                    Loop
                    myReader.Close()
                End If
            End If
            connData.chiudi(False)
            If NumeroNotifiche = 0 Then
                lblNote.Text = ""
            Else
                lblNote.Text = TestoNotifiche
                'par.NotificaTelerik(TestoNotifiche, CType(Me.Master.FindControl("RadNotification1"), RadNotification), Page)
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "key", "function f(){NotificaTelerik('" & RadNotificationNote.ClientID & "', 'Sep@Web', ''); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", True)
            End If
        Catch ex As Exception
            NumeroNotifiche = 0
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Sub RadButtonDirettoreLavori_Click(sender As Object, e As EventArgs) Handles RadButtonDirettoreLavori.Click
        RicavaNotifiche()
    End Sub
    Private Sub RadButtonFieldQualityManager_Click(sender As Object, e As EventArgs) Handles RadButtonFieldQualityManager.Click
        RicavaNotifiche()
    End Sub

    Private Sub RadButtonTecnicoAmministrativo_Click(sender As Object, e As EventArgs) Handles RadButtonTecnicoAmministrativo.Click
        RicavaNotifiche()
    End Sub
    Private Sub RadButtonBuildingManager_Click(sender As Object, e As EventArgs) Handles RadButtonBuildingManager.Click
        RicavaNotifiche()
    End Sub
End Class
