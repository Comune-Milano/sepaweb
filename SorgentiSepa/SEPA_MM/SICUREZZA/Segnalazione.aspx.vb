Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports Telerik.Web.UI

Partial Class SICUREZZA_Segnalazione
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
                caricaTutteTipologie()
                caricaCanale()
                caricaAssegnatari()
                CaricaTipologieLivello0()
                CaricaTipologieLivello1()
                CaricaTipologieLivello2()
                CaricaTipologieLivello3()
                CaricaTipologieLivello4()
                'CaricaListaDocumentiDaPortare()


                If Not IsNothing(Request.QueryString("IDS")) Then
                    idSegnalazione.Value = Request.QueryString("IDS")
                    CaricaDatiSegnalazione()
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Non è possibile caricare i dati relativi alla segnalazione selezionata. Contattare l\'amministratore di sistema.", 300, 150, "Attenzione", Nothing, Nothing)

                    Exit Sub
                End If
                ' txtDataCInt.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                'btnInterventi.Attributes.Add("onclick", "javascript:MostraDiv();document.getElementById('CPContenuto_TextBox1').value = '1';")
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPO_INTERVENTO ORDER BY DESCRIZIONE ASC", cmbTipoInterv, "ID", "DESCRIZIONE", True)
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPI_ANNULLO_SEGNALAZIONE ORDER BY DESCRIZIONE ASC", cmbTipiAnnullo, "ID", "DESCRIZIONE", True, "")

                txtOraCInt.Text = Format(Now, "HH:mm")
                txtDataCInt.Text = Format(Now, "dd/MM/yyyy")
                caricaSegnalazioniPadreFigli()
                CaricaInterventi()
            End If
            If Not IsNothing(Request.QueryString("SL")) AndAlso IsNumeric(Request.QueryString("SL")) Then
                solaLettura()
            End If
            If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                tipoAperturaFinestra.Value = "1"
                NoMenu()
            Else
                tipoAperturaFinestra.Value = "0"
            End If
            If Session.Item("id_caf") = 63 Then
                'cmbUrgenza.Enabled = False
                cmbTipoSegnalazioneLivello0.Enabled = False
                cmbTipoSegnalazioneLivello1.Enabled = False
                cmbTipoSegnalazioneLivello2.Enabled = False
                cmbTipoSegnalazioneLivello3.Enabled = False
                cmbTipoSegnalazioneLivello4.Enabled = False
                DropDownListTipologia.Enabled = False
            End If
            If Session.Item("FL_SEC_GEST_COMPLETA") = "0" Then
                btnInterventi.Visible = False
            End If
            If Session.Item("FL_SEC_ASS_OPERATORI") = "0" Then
                cmbAssegnatario.Visible = False
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaInterventi()
        Try
            connData.apri()
            par.cmd.CommandText = " SELECT distinct INTERVENTI_SICUREZZA.ID," _
               & " INTERVENTI_SICUREZZA.ID AS NUM,TIPO_INTERVENTO.DESCRIZIONE AS TIPO, " _
               & " TAB_STATI_INTERVENTI.DESCRIZIONE AS STATO, " _
               & " TO_CHAR(TO_DATE(SUBSTR(data_ora_inserimento,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_APERTURA " _
               & " FROM SISCOM_MI.INTERVENTI_SICUREZZA,SISCOM_MI.TIPOLOGIE_GUASTI,SISCOM_MI.TAB_STATI_INTERVENTI,SISCOM_MI.TIPO_INTERVENTO " _
               & " WHERE " _
               & " TAB_STATI_INTERVENTI.ID(+) = INTERVENTI_SICUREZZA.ID_STATO " _
               & " AND TIPO_INTERVENTO.ID(+)=INTERVENTI_SICUREZZA.ID_TIPO_INTERVENTO " _
               & " AND INTERVENTI_SICUREZZA.id_Segnalazione =" & idSegnalazione.Value _
               & " ORDER BY DATA_APERTURA DESC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Session.Item("ElencoInterventi") = dt

            connData.chiudi()
            RadGridInterventi.Rebind()
            If dt.Rows.Count > 0 Then

            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaInterventi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            If CType(Me.Master.FindControl("fl_sicurezza"), HiddenField).Value = "0" Then
                'par.modalDialogMessage("Sicurezza", "Operatore non abilitato al modulo Sicurezza!", Page, "info", , True)

                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operatore non abilitato al modulo Sicurezza!", 300, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            Else
                If CType(Me.Master.FindControl("fl_sicurezza_sl"), HiddenField).Value = "1" Then
                    solaLettura()
                End If
            End If
            cmbAssegnatario.SelectedValue = -1
            cmbTipoInterv.SelectedValue = -1

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    'Private Sub CaricaListaDocumentiDaPortare()

    '    Try
    '        If cmbTipoSegnalazioneLivello0.SelectedValue = "0" Then
    '            Dim query As String = ""
    '            If cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" Then
    '                query = "SELECT DESCRIZIONE AS DOCUMENTI_RICHIESTI FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI," _
    '                    & "SISCOM_MI.TIPOLOGIE_DOCUMENTI WHERE SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI.ID_TIPOLOGIA_DOCUMENTO=TIPOLOGIE_DOCUMENTI.ID " _
    '                    & " AND ID_TIPO_SEGN_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue _
    '                    & " AND ID_TIPO_SEGN_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue _
    '                    & " ORDER BY DESCRIZIONE"
    '            Else
    '                If cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
    '                    query = "SELECT DESCRIZIONE AS DOCUMENTI_RICHIESTI FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI," _
    '                        & "SISCOM_MI.TIPOLOGIE_DOCUMENTI WHERE SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI.ID_TIPOLOGIA_DOCUMENTO=TIPOLOGIE_DOCUMENTI.ID " _
    '                        & "AND ID_TIPO_SEGN_LIVELLO_2 IS NULL " _
    '                        & "AND ID_TIPO_SEGN_LIVELLO_1= " & cmbTipoSegnalazioneLivello1.SelectedValue _
    '                        & " ORDER BY DESCRIZIONE"
    '                End If
    '            End If
    '            If query <> "" Then
    '                connData.apri(False)
    '                par.cmd.CommandText = query
    '                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '                Dim dt As New Data.DataTable
    '                da.Fill(dt)
    '                da.Dispose()
    '                If dt.Rows.Count > 0 Then
    '                    DataGridDocumentiRichiesti.DataSource = dt
    '                    DataGridDocumentiRichiesti.DataBind()
    '                    DataGridDocumentiRichiesti.Visible = True
    '                    lblDocumentiRichiesti.Visible = False
    '                Else
    '                    DataGridDocumentiRichiesti.Visible = False
    '                    lblDocumentiRichiesti.Visible = True
    '                End If
    '                connData.chiudi(False)
    '            End If
    '            PanelElencoDocumentiRichiesti.Visible = True
    '        Else
    '            PanelElencoDocumentiRichiesti.Visible = False
    '        End If
    '    Catch ex As Exception
    '        connData.chiudi(False)
    '        Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaListaDocumentiDaPortare - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    Private Sub CaricaDatiSegnalazione()
        Try
            If idSegnalazione.Value <> "-1" Then
                connData.apri()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    'flCustode.Value = par.IfNull(lettore("fl_custode"), "0")
                    lblNrich.Text = idSegnalazione.Value
                    lblTitolo.Text = "Segnalazione numero " & idSegnalazione.Value
                    TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME_RS"), "")
                    idTipoSegnalazione.Value = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "-1")
                    If idTipoSegnalazione.Value = "1" Or idTipoSegnalazione.Value = "6" Then
                        Dim idPericoloSegnalazione As Integer = par.IfNull(lettore("ID_PERICOLO_SEGNALAZIONE"), -1)
                        Dim idPericoloSegnalazioneIniz As Integer = par.IfNull(lettore("ID_PERICOLO_SEGNALAZIONE_INIZ"), -1)
                        'If idPericoloSegnalazione <> -2 Then
                        '    Me.cmbUrgenza.SelectedIndex = idPericoloSegnalazione
                        'End If
                        'If idPericoloSegnalazioneIniz <> -2 Then
                        '    Me.cmbUrgenzaIniz.SelectedIndex = idPericoloSegnalazioneIniz
                        'End If
                        'pericoloSegnalazioneOld.Value = cmbUrgenza.SelectedItem.Text
                    End If
                    CaricaTipologieLivello0()
                    cmbTipoSegnalazioneLivello0.SelectedValue = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "-1")
                    tipoLivello1old.Value = cmbTipoSegnalazioneLivello0.SelectedItem.Text
                    'If par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "-1") = "1" Or par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "-1") = "6" Then
                    '    PanelUrgenzaCriticita.Visible = True
                    'Else
                    '    PanelUrgenzaCriticita.Visible = False
                    'End If

                    CaricaTipologieLivello1()
                    cmbTipoSegnalazioneLivello1.SelectedValue = par.IfNull(lettore("ID_TIPO_SEGN_LIVELLO_1"), "-1")
                    tipoLivello1old.Value = cmbTipoSegnalazioneLivello1.SelectedItem.Text

                    CaricaTipologieLivello2()
                    cmbTipoSegnalazioneLivello2.SelectedValue = par.IfNull(lettore("ID_TIPO_SEGN_LIVELLO_2"), "-1")
                    tipoLivello2old.Value = cmbTipoSegnalazioneLivello2.SelectedItem.Text

                    CaricaTipologieLivello3()
                    cmbTipoSegnalazioneLivello3.SelectedValue = par.IfNull(lettore("ID_TIPO_SEGN_LIVELLO_3"), "-1")
                    tipoLivello3old.Value = cmbTipoSegnalazioneLivello3.SelectedItem.Text

                    CaricaTipologieLivello4()
                    cmbTipoSegnalazioneLivello4.SelectedValue = par.IfNull(lettore("ID_TIPO_SEGN_LIVELLO_4"), "-1")
                    tipoLivello4old.Value = cmbTipoSegnalazioneLivello4.SelectedItem.Text

                    TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                    TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO1"), "")
                    TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("TELEFONO2"), "")
                    TextBoxEmailChiamante.Text = par.IfNull(lettore("MAIL"), "")
                    txtDescrizione.Text = par.IfNull(lettore("DESCRIZIONE_RIC"), "")
                    TextBoxDanneggiante.Text = par.IfNull(lettore("DANNEGGIANTE"), "")
                    TextBoxDanneggiato.Text = par.IfNull(lettore("DANNEGGIATO"), "")
                    descrizioneOLD.Value = txtDescrizione.Text
                    DropDownListCanale.SelectedValue = par.IfNull(lettore("ID_CANALE"), 0)
                    If par.IfNull(lettore("fl_sollecito"), 0) = 1 Then
                        lblSollecito.Text = "SOLLECITATA"
                        lblSollecito.ForeColor = Drawing.Color.Red
                    End If
                    If par.IfNull(lettore("fl_falsa"), 0) = 1 Then
                        chkSegnalazioneFalsa.Checked = True
                    Else
                        chkSegnalazioneFalsa.Checked = False
                    End If
                    lblDataInserimento.Text = par.FormattaData(Mid(par.IfNull(lettore("DATA_ORA_RICHIESTA"), ""), 1, 8))
                    lblOraInserimento.Text = Mid(par.IfNull(lettore("DATA_ORA_RICHIESTA"), ""), 9, 2) & ":" & Mid(par.IfNull(lettore("DATA_ORA_RICHIESTA"), "          "), 11, 2)
                    par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE ID = " & par.IfNull(lettore("ID_STATO"), "")
                    Dim readerInt As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If readerInt.Read Then
                        lblStato.Text = par.IfNull(readerInt("DESCRIZIONE"), "")
                    End If
                    readerInt.Close()
                    Dim query As String = ""
                    query = "SELECT EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id = " & par.IfNull(lettore("ID_EDIFICIO"), "")
                    par.caricaComboBox(query, DropDownListEdificio, "ID", "DENOMINAZIONE", False)
                    query = "select id,nome from siscom_mi.tab_filiali where id = " & par.IfNull(lettore("id_struttura"), "-1")
                    par.caricaComboBox(query, DropDownListSedeTerritoriale, "ID", "NOME", False)
                    unita.Value = par.IfNull(lettore("id_unita"), "")
                    If par.IfEmpty(unita.Value, 0) > 0 Then
                        par.cmd.CommandText = "SELECT * FROM siscom_mi.UNITA_IMMOBILIARI  WHERE id = " & unita.Value
                        readerInt = par.cmd.ExecuteReader
                        If readerInt.Read Then
                            lblOggetto.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & par.IfNull(readerInt("COD_UNITA_IMMOBILIARE"), "") & "','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">U.I. cod." & par.IfNull(readerInt("COD_UNITA_IMMOBILIARE"), "") & "</a>"
                            lblOggetto.Visible = True
                            query = "SELECT ID,descrizione FROM siscom_mi.SCALE_EDIFICI WHERE ID = " & par.IfNull(readerInt("id_scala"), "-1")
                            par.caricaComboBox(query, DropDownListScala, "ID", "DESCRIZIONE", False)
                            query = "select COD, descrizione from siscom_mi.TIPO_LIVELLO_PIANO where COD = " & par.IfNull(readerInt("COD_TIPO_LIVELLO_PIANO"), "-1")
                            par.caricaComboBox(query, DropDownListPiano, "COD", "DESCRIZIONE", False)
                            If par.IfEmpty(unita.Value, 0) > 0 Then
                                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE id = " & unita.Value & " ORDER BY INTERNO ASC"

                            ElseIf DropDownListScala.SelectedValue <> "-1" And DropDownListPiano.SelectedValue <> "-1" Then
                                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & " AND ID_SCALA = " & DropDownListScala.SelectedValue & " AND COD_TIPO_LIVELLO_PIANO='" & DropDownListPiano.SelectedValue & "' ORDER BY INTERNO ASC"
                            Else
                                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue & " ORDER BY INTERNO ASC"
                            End If
                            par.caricaComboBox(query, DropDownListInterno, "INTERNO", "INTERNO", False)
                            TextBoxCodiceUnitaImmobiliare.Text = par.IfNull(readerInt("cod_unita_immobiliare"), "")
                        End If
                        readerInt.Close()

                        If par.IfNull(lettore("id_contratto"), "-1") <> "-1" Then
                            par.cmd.CommandText = "SELECT rapporti_utenza.id as ID_CONTRATTO,SISCOM_MI.GETSTATOCONTRATTO2(rapporti_utenza.id,1)AS CONTLINK,rapporti_utenza.cod_contratto FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & par.IfNull(lettore("ID_CONTRATTO"), "-1")
                            readerInt = par.cmd.ExecuteReader
                            If readerInt.Read Then
                                lblContratto.Text = par.IfNull(readerInt("contlink"), "")
                                If lblContratto.Text <> "" Then
                                    'lblContratto.Text = "'<a href=''#'' onclick=""window.open(''../Contratti/Contratto.aspx?ID=" & par.IfNull(lettore("id_contratto"), "-1") & "'',''Contratto" & Format(Now, "yyyyMMddHHmmss") & "'',''height=780,width=1160'');"">'|| stato ||'</a>'"


                                    'lblContratto.Text = lblContratto.Text.ToString.Replace("onclick=""", "onclick=""document.getElementById('presaInCarico').click();")
                                End If
                                If Session.Item("ID_CAF").ToString = "63" Then
                                    lblContratto.Text = lblContratto.Text.Replace("?", "?LT=1&CC=1&")
                                Else
                                    If Session.Item("MODULO_CONT") = "0" Then
                                        lblContratto.Text = par.EliminaLink(lblContratto.Text)
                                    Else
                                        If Session.Item("CONT_LETTURA") = "1" Then
                                            lblContratto.Text = lblContratto.Text.Replace("?", "?LT=1&")
                                        End If
                                    End If
                                End If
                                lblContratto.Text = lblContratto.Text.ToString.Replace("window", "popupWindow=window")
                                lblContratto.Text = lblContratto.Text.ToString.Replace("');", "');popupWindow.focus();")
                                TextBoxCodiceContrattoIntestatario.Text = par.IfNull(readerInt("COD_CONTRATTO"), "")
                            End If
                            readerInt.Close()
                        Else
                            par.cmd.CommandText = "SELECT id_contratto,siscom_mi.Getstatocontratto2(id_contratto,1)as contlink,(select rapporti_utenza.cod_contratto from siscom_mi.rapporti_utenza where rapporti_utenza.id=id_contratto) as cod_Contratto FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & unita.Value & " "
                            readerInt = par.cmd.ExecuteReader
                            If readerInt.Read Then
                                lblContratto.Text = par.IfNull(readerInt("contlink"), "")
                                If lblContratto.Text <> "" Then
                                    'lblContratto.Text = lblContratto.Text.ToString.Replace("onclick=""", "onclick=""document.getElementById('presaInCarico').click();")
                                End If
                                If Session.Item("ID_CAF").ToString = "63" Then
                                    lblContratto.Text = lblContratto.Text.Replace("?", "?LT=1&CC=1&")
                                Else
                                    If Session.Item("MODULO_CONT") = "0" Then
                                        lblContratto.Text = par.EliminaLink(lblContratto.Text)
                                    Else
                                        If Session.Item("CONT_LETTURA") = "1" Then
                                            lblContratto.Text = lblContratto.Text.Replace("?", "?LT=1&")
                                        End If
                                    End If
                                End If
                                lblContratto.Text = lblContratto.Text.ToString.Replace("window", "popupWindow=window")
                                lblContratto.Text = lblContratto.Text.ToString.Replace("');", "');popupWindow.focus();")
                                TextBoxCodiceContrattoIntestatario.Text = par.IfNull(readerInt("COD_CONTRATTO"), "")
                            End If
                            readerInt.Close()
                        End If
                        par.cmd.CommandText = "SELECT ID_ANAGRAFICA  , RAGIONE_SOCIALE,COGNOME ,NOME   FROM siscom_mi.SOGGETTI_CONTRATTUALI ,SISCOM_MI.ANAGRAFICA " _
                                            & "WHERE id_contratto IN (SELECT id_contratto FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & unita.Value & " AND siscom_mi.Getstatocontratto2(id_contratto,0) LIKE '%CORSO%') " _
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
                    'par.cmd.CommandText = "SELECT " _
                    '    & " APPUNTAMENTI_CALL_CENTER.ID," _
                    '    & " APPUNTAMENTI_CALL_CENTER.DATA_APPUNTAMENTO AS DATA_APP," _
                    '    & " ID_SEGNALAZIONE," _
                    '    & " COGNOME," _
                    '    & " APPUNTAMENTI_CALL_CENTER.NOME," _
                    '    & " TAB_FILIALI.NOME AS SEDE_TERRITORIALE," _
                    '    & " TAB_FILIALI.ID AS ID_FILIALE," _
                    '    & " TO_CHAR(TO_DATE(DATA_APPUNTAMENTO,'YYYYMMDD'),'DD/MM/YYYY') AS DATA_APPUNTAMENTO," _
                    '    & " APPUNTAMENTI_SPORTELLI.DESCRIZIONE AS SPORTELLO," _
                    '    & " APPUNTAMENTI_ORARI.ORARIO AS ORARIO," _
                    '    & " APPUNTAMENTI_STATI.DESCRIZIONE AS STATO," _
                    '    & " TELEFONO," _
                    '    & " CELLULARE," _
                    '    & " EMAIL," _
                    '    & " NOTE," _
                    '    & " 'Eliminata dall''operatore '||(SELECT OPERATORE FROM SEPA.OPERATORI A WHERE A.ID=APPUNTAMENTI_cALL_cENTER.ID_OPERATORE_ELIMINAZIONE)||' il '||TO_CHAR(TO_DATE(SUBSTR(DATA_ELIMINAZIONE,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' alle ore '||SUBSTR(DATA_ELIMINAZIONE,9,2)||':'||SUBSTR(DATA_ELIMINAZIONE,11,2) AS ELIMINA,CASE WHEN DATA_ELIMINAZIONE IS NULL THEN 0 ELSE 1 END AS FL_ELIMINA " _
                    '    & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER," _
                    '    & " SISCOM_MI.APPUNTAMENTI_SPORTELLI," _
                    '    & " SISCOM_MI.APPUNTAMENTI_STATI," _
                    '    & " SISCOM_MI.APPUNTAMENTI_ORARI, " _
                    '    & " SISCOM_MI.TAB_FILIALI " _
                    '    & " WHERE ID_sEGNALAZIONE=" & idSegnalazione.Value _
                    '    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID(+) " _
                    '    & " AND APPUNTAMENTI_CALL_CENTER.ID_ORARIO=APPUNTAMENTI_ORARI.ID(+) " _
                    '    & " AND APPUNTAMENTI_CALL_CENTER.ID_STATO_APPUNTAMENTO=APPUNTAMENTI_STATI.ID(+) " _
                    '    & " AND TAB_FILIALI.ID=APPUNTAMENTI_cALL_CENTER.ID_STRUTTURA "
                    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    'Dim dt As New Data.DataTable
                    'da.Fill(dt)
                    'da.Dispose()
                    'If dt.Rows.Count > 0 Then
                    '    DataGridElencoAppuntamenti.DataSource = dt
                    '    DataGridElencoAppuntamenti.DataBind()
                    'End If
                End If
                lettore.Close()
                'If par.IfEmpty(unita.Value, 0) <> "0" Then
                '    par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.IMPIANTI_UI WHERE ID_UNITA = " & unita.Value
                '    lettore = par.cmd.ExecuteReader
                '    If lettore.Read Then
                '        lblImpianto.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('ElencoImpiantiUI.aspx?ID=" & unita.Value & "&T=U','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & "IMPIANTI </a>"
                '    End If
                '    lettore.Close()
                'Else
                '    par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.IMPIANTI_UI WHERE ID_EDIFICIO = " & DropDownListEdificio.SelectedValue
                '    lettore = par.cmd.ExecuteReader
                '    If lettore.Read Then
                '        lblImpianto.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('ElencoImpiantiUI.aspx?ID=" & DropDownListEdificio.SelectedValue & "&T=E','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & "IMPIANTI </a>"
                '    End If
                '    lettore.Close()
                'End If
                CaricaElencoAllegati()
                CaricaTabellaNote(idSegnalazione.Value)
                If Session.Item("FL_SEC_MODIF_SEGN") = "0" Then
                    ImpostaSoloLetturaElementiPagina()
                End If
                'StatoSegnalazione()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI_SOPRALLUOGO WHERE ID_SEGNALAZIONE =" & idSegnalazione.Value
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblStato.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.showModalDialog('Sopralluogo.aspx?IdSegn=" & idSegnalazione.Value & "&LE=1','window', 'status:no;dialogWidth:700px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');" & Chr(34) & ">" & lblStato.Text & "</a>"
                End If
                lettore.Close()
                connData.chiudi()
            Else
                connData.chiudi()
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Non è possibile caricare correttamente i dati", 300, 150, "Attenzione", "Home.aspx", Nothing)

            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaDatiSegnalazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub RiempiNoteChiusura()
        Try
            connData.apri()

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - RiempiNoteChiusura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaElencoAllegati()
        Dim sfondo As String = "#FFFFFF"
        Dim tabellaAllegati As String = ""
        tabellaAllegati &= "<table cellpadding='0' cellspacing='0' style='width:95%;'><tr style='font-family: ARIAL; font-size: 8pt; font-weight: bold'><td width='200px'>ALLEGATO</td><td width='200px'>DESCRIZIONE</td><td width='150px'>DATA-ORA</td></tr>"
        par.cmd.CommandText = "select * from siscom_mi.allegati_segnalazioni where id_segnalazione = " & idSegnalazione.Value
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        da.Dispose()
        Dim nomeFileAll As String = ""
        Dim dataFileAll As String = ""
        Dim descrizioneAll As String = ""
        'Dim idSegnalazione As String = ""
        For Each elemento As Data.DataRow In dt.Rows
            nomeFileAll = par.IfNull(elemento.Item("NOME_FILE"), "")
            dataFileAll = par.IfNull(elemento.Item("DATA_ORA"), "")
            descrizioneAll = par.IfNull(elemento.Item("DESCRIZIONE"), "")
            If IO.File.Exists(Server.MapPath("~\/ALLEGATI\/SEGNALAZIONI\/") & idSegnalazione.Value & "_" & dataFileAll & "-" & nomeFileAll & ".zip") Then
                tabellaAllegati &= "<tr style='height: 16px;font-family: ARIAL; font-size: 8pt;'" _
                & "><td style='border-bottom-style: solid; border-bottom-width: 1px; " _
                & "border-bottom-color: #000000;' width='200px'><a  alt='Download' href='../ALLEGATI/SEGNALAZIONI/" & idSegnalazione.Value & "_" & dataFileAll & "-" & nomeFileAll & ".zip' target='_blank'>" _
                & nomeFileAll & "</a></td><td style='border-bottom-style: solid; border-bottom-width: 1px; " _
                & "border-bottom-color: #000000;' width='200px'>" & descrizioneAll & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='100px'>" & dataFileAll & "</td></tr>"
            End If
        Next
        tabellaAllegati &= "</table>"
        TabellaAllegatiCompleta.Text = tabellaAllegati
    End Sub
    Private Sub StatoSegnalazione()
        par.cmd.CommandText = "select tab_stati_segnalazioni.id,tab_stati_segnalazioni.descrizione from siscom_mi.tab_stati_segnalazioni,siscom_mi.segnalazioni where tab_stati_segnalazioni.id=segnalazioni.id_stato and segnalazioni.id=" & idSegnalazione.Value
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Dim idStato As String = 0
        If lettore.Read Then
            lblStato.Text = par.IfNull(lettore("descrizione"), "")
            idStato = lettore("id")
            If lettore("id") = 2 Or lettore("id") = 10 Then
                btnSollecito.Visible = False
            End If
        End If
        lettore.Close()
        Select Case idStato
            Case -1
                'SEGNALAZIONE COM
                ImpostaSoloLetturaElementiPagina()
                btnAnnulla.Enabled = False
                btnSollecito.Enabled = False
                imgChiudiSegnalazione.Enabled = False
                imgAllega.Enabled = False
                btnSalva.Enabled = False
                btnStampa.Enabled = True

                imgEsci.Enabled = True
            Case 0
                'APERTA
                ImpostaSoloLetturaElementiPagina()
                btnAnnulla.Enabled = True
                btnSollecito.Enabled = True
                imgChiudiSegnalazione.Enabled = True
                imgAllega.Enabled = True
                btnSalva.Enabled = True
                btnStampa.Enabled = True

                imgEsci.Enabled = True
            Case 1
                'PRESA IN CARICO
                ImpostaSoloLetturaElementiPagina()
                btnAnnulla.Enabled = False
                btnSollecito.Enabled = True
                imgChiudiSegnalazione.Enabled = False
                '**********************************************************
                'modifica 17/11/2015 per consentire la chiusura delle 
                'amministrative in stato presa in carico
                If cmbTipoSegnalazioneLivello0.SelectedValue = "0" Then
                    imgChiudiSegnalazione.Enabled = True
                End If
                '**********************************************************
                imgAllega.Enabled = True
                btnSalva.Enabled = True
                btnStampa.Enabled = True

                imgEsci.Enabled = True
            Case 2
                'ANNULLATA
                ImpostaSoloLetturaElementiPagina()
                btnAnnulla.Enabled = False
                btnSollecito.Enabled = False
                imgChiudiSegnalazione.Enabled = False
                imgAllega.Enabled = False
                btnSalva.Enabled = False
                btnStampa.Enabled = True

                imgEsci.Enabled = True
            Case 3
                'VERIFICA
                ImpostaSoloLetturaElementiPagina()
                btnAnnulla.Enabled = False
                btnSollecito.Enabled = True
                imgChiudiSegnalazione.Enabled = False
                imgAllega.Enabled = True
                btnSalva.Enabled = True
                btnStampa.Enabled = True

                imgEsci.Enabled = True
            Case 4
                'ORDINE EMESSO
                ImpostaSoloLetturaElementiPagina()
                btnAnnulla.Enabled = False
                btnSollecito.Enabled = True
                imgChiudiSegnalazione.Enabled = False
                imgAllega.Enabled = False
                btnSalva.Enabled = False
                btnStampa.Enabled = True
                imgEsci.Enabled = True
            Case 5
                'RICHIESTA RESPINTA
                ImpostaSoloLetturaElementiPagina()
                btnAnnulla.Enabled = False
                btnSollecito.Enabled = False
                imgChiudiSegnalazione.Enabled = True
                imgAllega.Enabled = False
                btnSalva.Enabled = False
                btnStampa.Enabled = True
                imgEsci.Enabled = True
                'Me.imgChiudiSegnalazione.Visible = True
                'Me.cmbNoteChiusura.Enabled = True
                'Me.txtDescNoteChiusura.Enabled = True
                'Me.txtOraCInt.Enabled = True
                'Me.txtDataCInt.Enabled = True
            Case 10
                ImpostaSoloLetturaElementiPagina()
                btnAnnulla.Enabled = False
                btnSollecito.Enabled = False
                imgChiudiSegnalazione.Enabled = False
                imgAllega.Enabled = False
                btnSalva.Enabled = False
                btnStampa.Enabled = True
                imgEsci.Enabled = True
        End Select
        'Un operatore COMUNALE può chiudere usando il modulo CALL CENTER solo le richieste che ha generato lui o un altro operatore comunale
        If Session.Item("ID_CAF") = 6 Then
            par.cmd.CommandText = "select id_caf from operatori where id in (select distinct id_operatore from siscom_mi.eventi_segnalazioni where id_segnalazione = " & idSegnalazione.Value & " and cod_evento = 'F55')"
            lettore = par.cmd.ExecuteReader
            While lettore.Read
                If par.IfNull(lettore("id_caf"), 0) <> 6 Then
                    Me.imgChiudiSegnalazione.Visible = False
                    Exit While
                End If
            End While
            lettore.Close()
        End If
    End Sub
    Private Sub CaricaTabellaNote(idSegnal As String)
        Dim tabellaNote As String = ""
        tabellaNote &= "<table cellpadding='0' cellspacing='0' style='width:95%;'><tr style='font-family: ARIAL; font-size: 8pt; font-weight: bold'><td width='100px'>DATA-ORA</td><td width='150px'>OPERATORE</td><td>NOTE</td></tr>"
        par.cmd.CommandText = "SELECT segnalazioni_note.*,operatori.operatore FROM sepa.operatori,siscom_mi.segnalazioni_note where segnalazioni_note.id_segnalazione=" & idSegnal & " and segnalazioni_note.id_operatore=operatori.id (+) order by data_ora desc"
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While lettore.Read
            If par.IfNull(lettore("NOTE"), "").ToString <> "" Then
                tabellaNote &= "<tr style='height: 16px;font-family: ARIAL; font-size: 8pt;'><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='100px'>" & par.FormattaData(Mid(par.IfNull(lettore("data_ora"), ""), 1, 8)) & " " & Mid(par.IfNull(lettore("data_ora"), ""), 9, 2) & ":" & Mid(par.IfNull(lettore("data_ora"), ""), 11, 2) & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='150px'>" & par.IfNull(lettore("operatore"), "") & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;'>" & Replace(par.IfNull(lettore("note"), ""), vbCrLf, "</br>") & "</td></tr>"
            End If
        End While
        lettore.Close()
        tabellaNote &= "</table>"
        If tabellaNote <> "" Then
            TabellaNoteComplete.Text = tabellaNote
        End If
    End Sub
    Private Sub ImpostaSoloLetturaElementiPagina()
        TextBoxCognomeChiamante.Enabled = False
        TextBoxNomeChiamante.Enabled = False
        TextBoxTelefono1Chiamante.Enabled = False
        TextBoxTelefono2Chiamante.Enabled = False
        TextBoxEmailChiamante.Enabled = False
        TextBoxCognomeIntestatario.Enabled = False
        TextBoxNomeIntestatario.Enabled = False
        TextBoxCodiceContrattoIntestatario.Enabled = False
        TextBoxCodiceUnitaImmobiliare.Enabled = False
        'DropDownListComplessoImmobiliare.Enabled = False
        DropDownListEdificio.Enabled = False
        DropDownListScala.Enabled = False
        DropDownListPiano.Enabled = False
        DropDownListInterno.Enabled = False
        DropDownListSedeTerritoriale.Enabled = False
        cmbTipoSegnalazioneLivello1.Enabled = False
        cmbTipoSegnalazioneLivello2.Enabled = False
        cmbTipoSegnalazioneLivello3.Enabled = False
        cmbTipoSegnalazioneLivello4.Enabled = False
        txtDescrizione.Enabled = False
        TextBoxNota.Enabled = True
        'cmbUrgenza.Enabled = True
        TextBoxDanneggiante.Enabled = False
        TextBoxDanneggiato.Enabled = False
    End Sub

    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String, Optional ByVal valoreVecchio As String = "", Optional ByVal valoreNuovo As String = "", Optional idSegn As Integer = 0)
        Dim connOpNow As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri()
                connOpNow = True
            End If

            If idSegn = 0 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,VALORE_OLD,VALORE_NEW) " _
                & "VALUES ( " & idSegnalazione.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & cod & "','" & par.PulisciStrSql(motivo) & "','" & par.PulisciStrSql(valoreVecchio) & "','" & par.PulisciStrSql(valoreNuovo) & "')"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,VALORE_OLD,VALORE_NEW) " _
                & "VALUES ( " & idSegn & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & cod & "','" & par.PulisciStrSql(motivo) & "','" & par.PulisciStrSql(valoreVecchio) & "','" & par.PulisciStrSql(valoreNuovo) & "')"
                par.cmd.ExecuteNonQuery()
            End If
            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
                connData.chiudi()
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - WriteEvent - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click

    End Sub

    Protected Sub btnSalvaAnnullo_Click(sender As Object, e As System.EventArgs) Handles btnSalvaAnnullo.Click
        Try
            'If confermaAnnullaSegnalazione.Value = "1" Then
            connData.apri(True)
            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_OPERATORE_CH=" & Session.Item("ID_OPERATORE") & ", ID_STATO=2, ID_TIPO_ANNULLO=" & par.IfEmpty(cmbTipiAnnullo.SelectedValue, "NULL") & " WHERE ID=" & idSegnalazione.Value
            par.cmd.ExecuteNonQuery()
            StatoSegnalazione()
            CaricaTabellaNote(idSegnalazione.Value)
            WriteEvent("F234", "")
            connData.chiudi(True)

            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)

            'If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
            '    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", "apriSegnaz", Nothing)
            '    'par.modalDialogMessage("Annullamento segnalazione", "Operazione effettuata correttamente.", Me.Page, "successo", "Segnalazione.aspx?NM=1&IDS=" & idSegnalazione.Value, )
            'Else
            '    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", "apriSegnaz", Nothing)
            '    'par.modalDialogMessage("Annullamento segnalazione", "Operazione effettuata correttamente.", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idSegnalazione.Value, )
            'End If
            ''End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - btnSalvaAnnullo_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnSollecito_Click(sender As Object, e As System.EventArgs) Handles btnSollecito.Click
        VisualizzaPaginaSollecito()
    End Sub
    Protected Sub imgChiudiSegnalazione_Click(sender As Object, e As System.EventArgs) Handles imgChiudiSegnalazione.Click
        VisualizzaPaginaChiusuraSegnalazione()
    End Sub

    Protected Sub imgAllega_Click(sender As Object, e As System.EventArgs) Handles imgAllega.Click
        VisualizzaPaginaAllegati()
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If CheckControl() = True Then
            Try
                connData.apri(True)
                Dim valoreTipoIntervento As String = "NULL"
                Dim valoreUrgenza As String = "NULL"
                'If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
                '    valoreUrgenza = cmbUrgenza.SelectedIndex
                'End If
                If cmbTipoSegnalazioneLivello0.SelectedValue <> "0" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_CALL_cENTER SET ID_OPERATORE_ELIMINAZIONE=" & Session.Item("ID_OPERATORE") & ",DATA_ELIMINAZIONE=" & Format(Now, "yyyyMMddHHmmss") & " WHERE ID_SEGNALAZIONE=" & idSegnalazione.Value
                    par.cmd.ExecuteNonQuery()
                End If
                Dim livello0 As String = "NULL"
                If cmbTipoSegnalazioneLivello0.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello0.Items.Count > 0 Then
                    livello0 = cmbTipoSegnalazioneLivello0.SelectedValue
                End If
                Dim livello1 As String = "NULL"
                If cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello1.Items.Count > 0 Then
                    livello1 = cmbTipoSegnalazioneLivello1.SelectedValue
                End If
                Dim livello2 As String = "NULL"
                If cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello2.Items.Count > 0 Then
                    livello2 = cmbTipoSegnalazioneLivello2.SelectedValue
                End If
                Dim livello3 As String = "NULL"
                If cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello3.Items.Count > 0 Then
                    livello3 = cmbTipoSegnalazioneLivello3.SelectedValue
                End If
                Dim livello4 As String = "NULL"
                If cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello4.Items.Count > 0 Then
                    livello4 = cmbTipoSegnalazioneLivello4.SelectedValue
                End If
                Dim fl_segn_falsa As Integer = 0

                If chkSegnalazioneFalsa.Checked Then
                    fl_segn_falsa = 1
                Else
                    fl_segn_falsa = 0
                End If

                par.cmd.CommandText = "update siscom_mi.segnalazioni " _
                    & "set ID_PERICOLO_sEGNALAZIONE=" & valoreUrgenza _
                    & ", DATA_ORA_RICHIESTA = '" & par.AggiustaData(lblDataInserimento.Text) & lblOraInserimento.Text.Replace(":", "").Replace(".", "") _
                    & "', descrizione_ric = '" & par.PulisciStrSql(Me.txtDescrizione.Text) _
                    & "',ID_TIPO_SEGNALAZIONE= " & livello0 _
                    & ",ID_TIPO_SEGN_LIVELLO_1= " & livello1 _
                    & ",ID_TIPO_SEGN_LIVELLO_2= " & livello2 _
                    & ",ID_TIPO_SEGN_LIVELLO_3= " & livello3 _
                    & ",ID_TIPO_SEGN_LIVELLO_4= " & livello4 _
                    & ",ID_CANALE= " & DropDownListCanale.SelectedValue _
                    & ",FL_FALSA=" & fl_segn_falsa _
                    & " where id =" & idSegnalazione.Value
                par.cmd.ExecuteNonQuery()
                If TextBoxNota.Text <> "" Then


                    par.cmd.CommandText = "SELECT NOTE FROM SISCOM_MI.PARAMETRI_sEMAFORI WHERE ID=1"
                    Dim daVerdeAGiallo As Integer = par.IfNull(par.cmd.ExecuteScalar, 10)

                    par.cmd.CommandText = "SELECT NOTE FROM SISCOM_MI.PARAMETRI_sEMAFORI WHERE ID=2"
                    Dim daGialloARosso As Integer = par.IfNull(par.cmd.ExecuteScalar, 6)

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) " _
                                        & "VALUES (" & idSegnalazione.Value & ", '" & par.PulisciStrSql(TextBoxNota.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE=" & idSegnalazione.Value
                    Dim numeroNoteDopoUpdate As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)


                    Select Case numeroNoteDopoUpdate
                        Case daVerdeAGiallo
                            'If cmbUrgenza.SelectedIndex = 2 Then
                            '    cmbUrgenza.SelectedIndex = 3
                            '    par.cmd.CommandText = "update siscom_mi.segnalazioni " _
                            '        & "set ID_PERICOLO_sEGNALAZIONE=" & 3 _
                            '        & " where id =" & idSegnalazione.Value
                            '    par.cmd.ExecuteNonQuery()
                            '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                            '        & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                            '        & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                            '        & " VALUES (" & idSegnalazione.Value & ", " _
                            '        & 2 & ", " _
                            '        & 3 & ", " _
                            '        & Session.Item("ID_OPERATORE") & "," _
                            '        & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                            '        & "'" & par.PulisciStrSql("Cambio priorità per numero soglia note operatore raggiunto.") & "')"
                            '    par.cmd.ExecuteNonQuery()
                            'End If
                        Case daGialloARosso
                            'If cmbUrgenza.SelectedIndex = 3 And cmbUrgenzaIniz.SelectedIndex = 3 Then
                            '    cmbUrgenza.SelectedIndex = 4
                            '    par.cmd.CommandText = "update siscom_mi.segnalazioni " _
                            '        & "set ID_PERICOLO_sEGNALAZIONE=" & 4 _
                            '        & " where id =" & idSegnalazione.Value
                            '    par.cmd.ExecuteNonQuery()
                            '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                            '        & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                            '        & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                            '        & " VALUES (" & idSegnalazione.Value & ", " _
                            '        & 3 & ", " _
                            '        & 4 & ", " _
                            '        & Session.Item("ID_OPERATORE") & "," _
                            '        & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                            '        & "'" & par.PulisciStrSql("Cambio priorità per numero soglia note operatore raggiunto.") & "')"
                            '    par.cmd.ExecuteNonQuery()
                            'End If
                        Case daVerdeAGiallo + daGialloARosso
                            'If cmbUrgenza.SelectedIndex = 3 And cmbUrgenzaIniz.SelectedIndex < 3 Then
                            '    cmbUrgenza.SelectedIndex = 4
                            '    par.cmd.CommandText = "update siscom_mi.segnalazioni " _
                            '        & "set ID_PERICOLO_sEGNALAZIONE=" & 4 _
                            '        & " where id =" & idSegnalazione.Value
                            '    par.cmd.ExecuteNonQuery()
                            '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                            '        & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                            '        & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                            '        & " VALUES (" & idSegnalazione.Value & ", " _
                            '        & 3 & ", " _
                            '        & 4 & ", " _
                            '        & Session.Item("ID_OPERATORE") & "," _
                            '        & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                            '        & "'" & par.PulisciStrSql("Cambio priorità per numero soglia note operatore raggiunto.") & "')"
                            '    par.cmd.ExecuteNonQuery()
                            'End If
                    End Select

                    TextBoxNota.Text = ""
                    WriteEvent("F233", "Inserimento nuova nota")
                End If
                'StatoSegnalazione()
                CaricaTabellaNote(idSegnalazione.Value)
                CaricaElencoAllegati()
                Dim salvaParticolare As Boolean = False
                If pericoloSegnalazioneOld.Value = "" Then
                    pericoloSegnalazioneOld.Value = "- - -"
                End If
                If cmbTipoSegnalazioneLivello1.SelectedValue = "1" Then
                    'If cmbUrgenza.SelectedItem.Text <> pericoloSegnalazioneOld.Value Then
                    '    WriteEvent("F233", "Modifica criticità", pericoloSegnalazioneOld.Value, cmbUrgenza.SelectedItem.Text)
                    '    salvaParticolare = True
                    'End If
                Else
                    If pericoloSegnalazioneOld.Value <> "- - -" Then
                        WriteEvent("F233", "Modifica criticità", pericoloSegnalazioneOld.Value, "- - -")
                        salvaParticolare = True
                    End If
                End If

                If tipoLivello1old.Value = "" Then
                    tipoLivello1old.Value = "- - -"
                End If
                If cmbTipoSegnalazioneLivello1.Items.Count > 0 Then
                    If tipoLivello1old.Value <> cmbTipoSegnalazioneLivello1.SelectedItem.Text Then
                        WriteEvent("F233", "Modifica tipologia della segnalazione livello 1", tipoLivello1old.Value, cmbTipoSegnalazioneLivello1.SelectedItem.Text)
                        salvaParticolare = True
                    End If
                Else
                    If tipoLivello1old.Value <> "- - -" Then
                        WriteEvent("F233", "Modifica tipologia della segnalazione livello 1", tipoLivello1old.Value, "- - -")
                        salvaParticolare = True
                    End If
                End If

                If tipoLivello2old.Value = "" Then
                    tipoLivello2old.Value = "- - -"
                End If
                If cmbTipoSegnalazioneLivello2.Items.Count > 0 Then
                    If tipoLivello2old.Value <> cmbTipoSegnalazioneLivello2.SelectedItem.Text Then
                        WriteEvent("F233", "Modifica tipologia della segnalazione livello 2", tipoLivello2old.Value, cmbTipoSegnalazioneLivello2.SelectedItem.Text)
                        salvaParticolare = True
                    End If
                Else
                    If tipoLivello2old.Value <> "- - -" Then
                        WriteEvent("F233", "Modifica tipologia della segnalazione livello 2", tipoLivello2old.Value, "- - -")
                        salvaParticolare = True
                    End If
                End If

                If tipoLivello3old.Value = "" Then
                    tipoLivello3old.Value = "- - -"
                End If
                If cmbTipoSegnalazioneLivello3.Items.Count > 0 Then
                    If tipoLivello3old.Value <> cmbTipoSegnalazioneLivello3.SelectedItem.Text Then
                        WriteEvent("F233", "Modifica tipologia della segnalazione livello 3", tipoLivello3old.Value, cmbTipoSegnalazioneLivello3.SelectedItem.Text)
                        salvaParticolare = True
                    End If
                Else
                    If tipoLivello3old.Value <> "- - -" Then
                        WriteEvent("F233", "Modifica tipologia della segnalazione livello 3", tipoLivello3old.Value, "- - -")
                        salvaParticolare = True
                    End If
                End If

                If tipoLivello4old.Value = "" Then
                    tipoLivello4old.Value = "- - -"
                End If
                If cmbTipoSegnalazioneLivello4.Items.Count > 0 Then
                    If tipoLivello4old.Value <> cmbTipoSegnalazioneLivello4.SelectedItem.Text Then
                        WriteEvent("F233", "Modifica tipologia della segnalazione livello 4", tipoLivello4old.Value, cmbTipoSegnalazioneLivello4.SelectedItem.Text)
                        salvaParticolare = True
                    End If
                Else
                    If tipoLivello4old.Value <> "- - -" Then
                        WriteEvent("F233", "Modifica tipologia della segnalazione livello 4", tipoLivello4old.Value, "- - -")
                        salvaParticolare = True
                    End If
                End If
                If descrizioneOLD.Value <> txtDescrizione.Text Then
                    WriteEvent("F233", "Modifica descrizione della richiesta", descrizioneOLD.Value, txtDescrizione.Text)
                    salvaParticolare = True
                End If
                If salvaParticolare = False Then
                    WriteEvent("F233", "")
                End If

                connData.chiudi(True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then

                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace("Operazione effettuata correttamente", "'", "\'") & "','success');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace("Operazione effettuata correttamente", "'", "\'") & "','success');", True)

                    'par.modalDialogMessage("Aggiornamento segnalazione", "Operazione effettuata correttamente.", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idSegnalazione.Value, )
                End If

                CType(Me.Master.FindControl("txtModificato"), HiddenField).Value = "0"

            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - btnSalva_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
    End Sub

    Protected Sub imgEsci_Click(sender As Object, e As System.EventArgs) Handles imgEsci.Click
        If Not IsNothing(Request.QueryString("SL")) AndAlso IsNumeric(Request.QueryString("SL")) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
        ElseIf Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
        Else
            Response.Redirect("Home.aspx", False)
        End If
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello1.SelectedIndexChanged
        CaricaTipologieLivello2()
        'CaricaListaDocumentiDaPortare()
        'caricaUrgenzaPredefinita()
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello2.SelectedIndexChanged
        CaricaTipologieLivello3()
        'CaricaListaDocumentiDaPortare()
        'caricaUrgenzaPredefinita()
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello3.SelectedIndexChanged
        CaricaTipologieLivello4()
        'CaricaListaDocumentiDaPortare()
        'caricaUrgenzaPredefinita()
    End Sub
    Protected Sub btnSalvaSollecito_Click(sender As Object, e As System.EventArgs) Handles btnSalvaSollecito.Click
        Try
            connData.apri(True)
            If txtNoteSoll.Text <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,SOLLECITO) VALUES (" & idSegnalazione.Value & ", '" & par.PulisciStrSql(txtNoteSoll.Text) & " (nota di sollecito) ', '" & Format(Now, "yyyyMMddHHmm") & "'," & Session.Item("ID_OPERATORE") & ", 1)"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,SOLLECITO) VALUES (" & idSegnalazione.Value & ", '" & par.PulisciStrSql(txtNoteSoll.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "'," & Session.Item("ID_OPERATORE") & ", 1)"
                par.cmd.ExecuteNonQuery()
            End If
            par.cmd.CommandText = "update siscom_mi.segnalazioni set fl_sollecito = 1 where id = " & idSegnalazione.Value
            par.cmd.ExecuteNonQuery()
            lblSollecito.Text = "SOLLECITATA"
            lblSollecito.ForeColor = Drawing.Color.Red
            StatoSegnalazione()
            CaricaTabellaNote(idSegnalazione.Value)
            txtNoteSoll.Text = ""
            WriteEvent("F242", "")
            connData.chiudi(True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
            If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace("Operazione effettuata correttamente", "'", "\'") & "','success');", True)

                'par.modalDialogMessage("Sollecito", "Sollecito effettuato correttamente.", Me.Page, "successo", "Segnalazione.aspx?NM=1&IDS=" & idSegnalazione.Value, )
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace("Operazione effettuata correttamente", "'", "\'") & "','success');", True)

                ' par.modalDialogMessage("Sollecito", "Sollecito effettuato correttamente.", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idSegnalazione.Value, )
            End If
            VisualizzaPaginaPrincipale()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - btnSollecito_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub VisualizzaPaginaPrincipale()
        MultiView1.ActiveViewIndex = 0
        MultiView2.ActiveViewIndex = 0
        MultiView3.ActiveViewIndex = 0
    End Sub
    Private Sub VisualizzaPaginaSollecito()
        MultiView1.ActiveViewIndex = 1
        MultiView2.ActiveViewIndex = 1
        MultiView3.ActiveViewIndex = 1
    End Sub
    Private Sub VisualizzaPaginaAllegati()
        MultiView1.ActiveViewIndex = 3
        MultiView2.ActiveViewIndex = 3
        MultiView3.ActiveViewIndex = 3
    End Sub
    Private Sub VisualizzaPaginaChiusuraSegnalazione()
        MultiView1.ActiveViewIndex = 2
        MultiView2.ActiveViewIndex = 2
        MultiView3.ActiveViewIndex = 2
    End Sub
    Private Sub VisualizzaPaginaPadreFiglio()
        MultiView1.ActiveViewIndex = 4
        MultiView2.ActiveViewIndex = 4
        MultiView3.ActiveViewIndex = 4
    End Sub
    Private Sub VisualizzaPaginaInterventi()
        MultiView1.ActiveViewIndex = 5
        MultiView2.ActiveViewIndex = 5
        MultiView3.ActiveViewIndex = 5
    End Sub
    Protected Sub btnIndietro_Click(sender As Object, e As System.EventArgs) Handles btnIndietro.Click
        VisualizzaPaginaPrincipale()
    End Sub
    Protected Sub btnIndietro2_Click(sender As Object, e As System.EventArgs) Handles btnIndietro2.Click
        VisualizzaPaginaPrincipale()
    End Sub
    Protected Sub btnIndietro3_Click(sender As Object, e As System.EventArgs) Handles btnIndietro3.Click
        VisualizzaPaginaPrincipale()
    End Sub
    Protected Sub imgChiudiSegnalazione1_Click(sender As Object, e As System.EventArgs) Handles imgChiudiSegnalazione1.Click
        'If confermaChiusura.Value = "1" Then
        Try
            If par.AggiustaData(Me.txtDataCInt.Text) & Me.txtOraCInt.Text.Replace(":", "").Replace(".", "") >= par.AggiustaData(lblDataInserimento.Text) & lblOraInserimento.Text.Replace(":", "").Replace(".", "") Then
                connData.apri(True)
                Dim idAmministrativa As Integer = 0
                If confermaTicketAmministrativo.Value = "1" Then
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_SEGNALAZIONI.NEXTVAL FROM DUAL"
                    idAmministrativa = par.IfNull(par.cmd.ExecuteScalar, 0)
                    par.cmd.CommandText = " INSERT INTO SISCOM_MI.SEGNALAZIONI ( " _
                        & " ID, ID_STATO, ID_COMPLESSO,  " _
                        & " ID_EDIFICIO, ID_UNITA, COGNOME_RS,  " _
                        & " DATA_ORA_RICHIESTA, TELEFONO1, TELEFONO2,  " _
                        & " MAIL, DESCRIZIONE_RIC, ID_OPERATORE_INS,  " _
                        & " ID_OPERATORE_CH, NOTE, NOME,  " _
                        & " TIPO_RICHIESTA, ORIGINE, PASSATA_A,  " _
                        & " ID_TIPOLOGIE, DATA_CHIUSURA, TIPO_PERVENUTA,  " _
                        & " FL_SOLLECITO, ID_STRUTTURA, DATA_IN_CARICO,  " _
                        & " ID_PERICOLO_SEGNALAZIONE, ID_TIPO_SEGNALAZIONE, ID_CONTRATTO,  " _
                        & " ID_TIPO_SEGN_LIVELLO_1, ID_TIPO_SEGN_LIVELLO_2, ID_TIPO_SEGN_LIVELLO_3,  " _
                        & " ID_TIPO_SEGN_LIVELLO_4, DANNEGGIANTE, DANNEGGIATO,  " _
                        & " ID_SEGNALAZIONE_PADRE, ID_PERICOLO_SEGNALAZIONE_INIZ)  " _
                        & " (SELECT  " _
                        & idAmministrativa & ", 0, ID_COMPLESSO,  " _
                        & " ID_EDIFICIO, ID_UNITA, COGNOME_RS,  " _
                        & " '" & Format(Now, "yyyyMMddHHmmss") & "', TELEFONO1, TELEFONO2,  " _
                        & " MAIL, DESCRIZIONE_RIC, ID_OPERATORE_INS,  " _
                        & " ID_OPERATORE_CH, NOTE, NOME,  " _
                        & " TIPO_RICHIESTA, ORIGINE, PASSATA_A,  " _
                        & " ID_TIPOLOGIE, DATA_CHIUSURA, TIPO_PERVENUTA,  " _
                        & " 0, ID_STRUTTURA, DATA_IN_CARICO,  " _
                        & " NULL, 0, ID_CONTRATTO,  " _
                        & " NULL, NULL, NULL,  " _
                        & " NULL, DANNEGGIANTE, DANNEGGIATO,  " _
                        & " NULL, NULL " _
                        & " FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idSegnalazione.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET id_stato=10,DATA_CHIUSURA = '" & par.AggiustaData(Me.txtDataCInt.Text) & Me.txtOraCInt.Text.Replace(":", "").Replace(".", "") & "' " _
                    & " where (id=" & idSegnalazione.Value & " or id in(SELECT ID FROM SISCOM_mi.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idSegnalazione.Value & ")) and id_Stato<>10"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID FROM SISCOM_MI.SEGNALAZIONI " _
                    & " where (id=" & idSegnalazione.Value & " or id in(SELECT ID FROM SISCOM_mi.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idSegnalazione.Value & ")) and id_Stato<>10"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While lettore.Read
                    Dim idS As Integer = par.IfNull(lettore("ID"), 0)
                    If idS > 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,id_tipo_segnalazione_note) " _
                    & "VALUES (" & idS & ", '" & par.PulisciStrSql(Me.txtDescNoteChiusura.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ",2)"
                        par.cmd.ExecuteNonQuery()
                        WriteEvent("F243", "", , , idS)
                    End If
                End While
                lettore.Close()
                StatoSegnalazione()
                CaricaTabellaNote(idSegnalazione.Value)
                connData.chiudi(True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                If confermaTicketAmministrativo.Value = "1" Then
                    If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace("Operazione effettuata correttamente", "'", "\'") & "','success');", True)

                        ' par.modalDialogMessage("Chiusura segnalazione", "Chiusura completata correttamente.", Me.Page, "successo", "Segnalazione.aspx?NM=1&IDS=" & idAmministrativa)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace("Operazione effettuata correttamente", "'", "\'") & "','success');", True)

                        'par.modalDialogMessage("Chiusura segnalazione", "Chiusura completata correttamente.", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idAmministrativa)
                    End If
                Else
                    If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace("Operazione effettuata correttamente", "'", "\'") & "','success');", True)

                        'par.modalDialogMessage("Chiusura segnalazione", "Chiusura completata correttamente.", Me.Page, "successo", "Segnalazione.aspx?NM=1&IDS=" & idSegnalazione.Value)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace("Operazione effettuata correttamente", "'", "\'") & "','success');", True)

                        ' par.modalDialogMessage("Chiusura segnalazione", "Chiusura completata correttamente.", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idSegnalazione.Value)
                    End If
                End If
            Else
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Attenzione...\nLa data e l\'ora di chiusura devono essere successive a quelle di apertura!\nChiusura non completata.", 300, 150, "Attenzione", Nothing, Nothing)
                'par.modalDialogMessage("Sollecito", "Attenzione...\nLa data e l\'ora di chiusura devono essere successive a quelle di apertura!\nChiusura non completata.", Me.Page, "info", , )
            End If
            VisualizzaPaginaPrincipale()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - imgChiudiSegnalazione1_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        'End If
    End Sub
    Protected Sub btnChiudiSegnalazione_Click(sender As Object, e As System.EventArgs) Handles btnChiudiSegnalazione.Click
        'If confermaChiusura.Value = "1" Then
        If cmbTipoSegnalazioneLivello0.SelectedValue = "2" Then
            'callback
            par.modalDialogConfirm("Sicurezza", "Dopo la chiusura del ticket CALLBACK\nvuoi creare un ticket AMMINISTRATIVO?", "Sì", "document.getElementById('confermaTicketAmministrativo').value='1';document.getElementById('imgChiudiSegnalazione1').click();", "No", "document.getElementById('confermaTicketAmministrativo').value='0';document.getElementById('imgChiudiSegnalazione1').click();", Page)
        Else
            confermaTicketAmministrativo.Value = "0"
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "scr1", "document.getElementById('imgChiudiSegnalazione1').click();", True)
        End If
        'End If
    End Sub
    Protected Sub btnAllegaFile_Click(sender As Object, e As System.EventArgs) Handles btnAllegaFile.Click
        Dim nFile As String = ""
        Try
            If FileUpload1.HasFile = True Then
                If txtDescrizioneA.Text <> "" Then
                    connData.apri(True)
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ALLEGATI_SEGNALAZIONI WHERE NOME_FILE = '" & par.PulisciStrSql(Me.txtDescrizioneA.Text.ToUpper) & "' AND ID_SEGNALAZIONE = " & idSegnalazione.Value
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                    Dim oraAllegato As String = Format(Now, "yyyyMMddHHmmss")

                    If lettore.Read Then
                        CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Esiste già un allegato con lo stesso nome!\nImpossibile allegare il file scelto.", 300, 150, "Attenzione", Nothing, Nothing)
                        VisualizzaPaginaPrincipale()
                    Else
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_SEGNALAZIONI (ID,NOME_FILE,ID_SEGNALAZIONE,DATA_ORA,DESCRIZIONE) VALUES " _
                                            & "(SISCOM_MI.SEQ_SEGNALAZIONI.NEXTVAL,'" & par.PulisciStrSql(Me.txtDescrizioneA.Text.ToUpper) & "'," & idSegnalazione.Value & "," _
                                            & "'" & oraAllegato & "', '" & par.PulisciStrSql(Me.txtDescrizioneAll.Text.ToUpper) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    lettore.Close()

                    nFile = Server.MapPath("..\ALLEGATI\SEGNALAZIONI\" & FileUpload1.FileName)
                    FileUpload1.SaveAs(nFile)
                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream
                    Dim zipfic As String
                    zipfic = Server.MapPath("..\ALLEGATI\SEGNALAZIONI\" & idSegnalazione.Value & "_" & oraAllegato & "-" & UCase(txtDescrizioneA.Text) & ".zip")
                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)
                    Dim strFile As String
                    strFile = nFile
                    Dim strmFile As FileStream = File.OpenRead(strFile)
                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                    Dim sFile As String = Path.GetFileName(strFile)
                    Dim theEntry As ZipEntry = New ZipEntry(sFile)
                    Dim fi As New FileInfo(strFile)
                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                    File.Delete(strFile)

                    If Not String.IsNullOrEmpty(Me.txtDescrizioneAll.Text) Then
                        Dim fileDescrizione As String = ""

                        fileDescrizione = Server.MapPath("..\ALLEGATI\SEGNALAZIONI\") & UCase(txtDescrizioneA.Text) & oraAllegato & "_DESCRIZIONE.txt"
                        Dim sr As StreamWriter = New StreamWriter(fileDescrizione, False, System.Text.Encoding.Default)
                        sr.WriteLine("Data del documento:" & par.FormattaData(Format(Now, "yyyyMMdd")) & vbCrLf & "DESCRIZIONE:" & vbCrLf & txtDescrizioneAll.Text.ToUpper)
                        sr.Close()

                        strFile = Server.MapPath("..\ALLEGATI\SEGNALAZIONI\") & UCase(txtDescrizioneA.Text) & oraAllegato & "_DESCRIZIONE.txt"
                        strmFile = File.OpenRead(strFile)
                        Dim abyBuffer12(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        strmFile.Read(abyBuffer12, 0, abyBuffer12.Length)
                        Dim sFile12 As String = Path.GetFileName(strFile)
                        theEntry = New ZipEntry(sFile12)
                        fi = New FileInfo(strFile)
                        theEntry.DateTime = fi.LastWriteTime
                        theEntry.Size = strmFile.Length
                        strmFile.Close()
                        objCrc32.Reset()
                        objCrc32.Update(abyBuffer12)
                        theEntry.Crc = objCrc32.Value
                        strmZipOutputStream.PutNextEntry(theEntry)
                        strmZipOutputStream.Write(abyBuffer12, 0, abyBuffer12.Length)
                        File.Delete(strFile)

                    End If
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione completata correttamente.", 300, 150, "Attenzione", Nothing, Nothing)

                    WriteEvent("F233", "Inserimento allegato")
                    CaricaTabellaNote(idSegnalazione.Value)
                    CaricaElencoAllegati()
                    connData.chiudi(True)
                Else
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Inserire il nome.", 300, 150, "Attenzione", Nothing, Nothing)

                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - btnAllegaFile_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnEventi_Click(sender As Object, e As System.EventArgs) Handles btnEventi.Click
        Response.Redirect("Eventi.aspx?NM=1&IDS=" & idSegnalazione.Value, False)
    End Sub
    Private Sub CaricaTipologieLivello0()
        Try
            Dim connOpenNow As Boolean = False
            Dim query As String = ""
            'If flCustode.Value = "1" Then
            '    query = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id=6 ORDER BY ID"
            'Else
            query = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id=5 ORDER BY ID"
            'End If
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connOpenNow = True
            End If
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello0, "ID", "DESCRIZIONE", True)
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
            End If
            cmbTipoSegnalazioneLivello2.Items.Clear()
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
            cmbTipoSegnalazioneLivello2.Visible = False
            cmbTipoSegnalazioneLivello3.Visible = False
            cmbTipoSegnalazioneLivello4.Visible = False
            lblLivello2.Visible = False
            lblLivello3.Visible = False
            lblLivello4.Visible = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaTipologieLivello1 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello1()
        Try
            Dim connOpenNow As Boolean = False
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE=" & cmbTipoSegnalazioneLivello0.SelectedValue & " ORDER BY DESCRIZIONE"
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connOpenNow = True
            End If
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello1, "ID", "DESCRIZIONE", True)
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
            End If
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaTipologieLivello2 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello2()
        Try
            Dim connOpenNow As Boolean = False
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue & " ORDER BY DESCRIZIONE"
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connOpenNow = True
            End If
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello2, "ID", "DESCRIZIONE", True)
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
            End If
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaTipologieLivello3 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello3()
        Try
            Dim connOpenNow As Boolean = False
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue & " ORDER BY DESCRIZIONE"
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connOpenNow = True
            End If
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello3, "ID", "DESCRIZIONE", True)
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
            End If
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaTipologieLivello3 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello4()
        Try
            Dim connOpenNow As Boolean = False
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue & " ORDER BY DESCRIZIONE"
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connOpenNow = True
            End If
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello4, "ID", "DESCRIZIONE", True)
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
            End If
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaTipologieLivello4 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function CheckControl() As Boolean
        CheckControl = True
        Try
            If String.IsNullOrEmpty(Me.txtDescrizione.Text) Then
                CheckControl = False
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Inserire la descrizione della richiesta", 300, 150, "Attenzione", Nothing, Nothing)
            End If
            If cmbTipoSegnalazioneLivello0.SelectedValue = "-1" Then
                CheckControl = False
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Inserire la tipologia della segnalazione", 300, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CheckControl - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        Return CheckControl
    End Function

    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        Try
            If confermaGenerica.Value = "1" Then
                confermaGenerica.Value = "0"
                If idSelected.Value <> "-1" Then
                    connData.apri()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_CALL_cENTER SET ID_OPERATORE_ELIMINAZIONE=" & Session.Item("ID_OPERATORE") & ",DATA_ELIMINAZIONE=" & Format(Now, "yyyyMMddHHmmss") & " WHERE ID=" & idSelected.Value
                    par.cmd.ExecuteNonQuery()
                    WriteEvent("F238", "")
                    connData.chiudi()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Segnalazione", "Appuntamento eliminato correttamente.", Page, "successo", "Segnalazione.aspx" & Request.Url.Query)
                End If
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - btnElimina_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub solaLettura()
        Try
            Dim CTRL As Control = Nothing
            For Each CTRL In View1.Controls
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
            For Each CTRL In View7.Controls
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
            For Each CTRL In View8.Controls
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
            For Each CTRL In View4.Controls
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
            For Each CTRL In View9.Controls
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
            For Each CTRL In View10.Controls
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

            imgEsci.Enabled = True
            btnStampa.Enabled = True
            btnEventi.Enabled = True
            btnIndietro.Enabled = True
            btnIndietro2.Enabled = True
            btnIndietro3.Enabled = True

            'cmbUrgenza.Enabled = False
            'cmbUrgenzaIniz.Enabled = False
            txtDescrizione.ReadOnly = True
            TextBoxNota.ReadOnly = True
            CType(Me.Master.FindControl("NavigationMenu"), Telerik.Web.UI.RadMenu).Visible = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub NoMenu()
        Try
            CType(Me.Master.FindControl("NavigationMenu"), Telerik.Web.UI.RadMenu).Visible = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - NoMenu - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello0_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello0.SelectedIndexChanged
        CaricaTipologieLivello1()

        'caricaUrgenzaPredefinita()
        'CaricaListaDocumentiDaPortare()
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello4.SelectedIndexChanged
        'CaricaListaDocumentiDaPortare()
        'caricaUrgenzaPredefinita()
    End Sub

    Protected Sub DropDownListTipologia_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListTipologia.SelectedIndexChanged
        'CaricaListaDocumentiDaPortare()
        'caricaUrgenzaPredefinita()
    End Sub

    'Private Sub caricaUrgenzaPredefinita()
    '    Try
    '        Dim condTipologia As String = ""
    '        Dim condTipologia1 As String = ""
    '        Dim condTipologia2 As String = ""
    '        Dim condTipologia3 As String = ""
    '        Dim condTipologia4 As String = ""

    '        Dim tipo0 As Integer = -1
    '        Dim tipo1 As Integer = -1
    '        Dim tipo2 As Integer = -1
    '        Dim tipo3 As Integer = -1
    '        Dim tipo4 As Integer = -1
    '        Dim lettore As Oracle.DataAccess.Client.OracleDataReader
    '        connData.apri(False)
    '        If DropDownListTipologia.SelectedValue.ToString <> "-1" And DropDownListTipologia.SelectedValue.ToString <> "" Then

    '            par.cmd.CommandText = " SELECT ID_TIPO_sEGNALAZIONE, " _
    '                & " ID_TIPO_SEGNALAZIONE_LIVELLO_1, " _
    '                & " ID_TIPO_SEGNALAZIONE_LIVELLO_2, " _
    '                & " ID_TIPO_SEGNALAZIONE_LIVELLO_3, " _
    '                & " ID_TIPO_SEGNALAZIONE_LIVELLO_4  " _
    '                & " FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE WHERE ID=" & DropDownListTipologia.SelectedValue
    '            LETTORE = par.cmd.ExecuteReader
    '            If lettore.Read Then
    '                tipo0 = par.IfNull(lettore("ID_TIPO_sEGNALAZIONE"), -1)
    '                tipo1 = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE_LIVELLO_1"), -1)
    '                tipo2 = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE_LIVELLO_2"), -1)
    '                tipo3 = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE_LIVELLO_3"), -1)
    '                tipo4 = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE_LIVELLO_4"), -1)
    '            End If
    '            lettore.Close()

    '            If tipo0 <> -1 Then
    '                CaricaTipologieLivello0()
    '                cmbTipoSegnalazioneLivello0.SelectedValue = tipo0
    '                cmbTipoSegnalazioneLivello0.Visible = True
    '            End If
    '            If tipo1 <> -1 Then
    '                CaricaTipologieLivello1()
    '                cmbTipoSegnalazioneLivello1.SelectedValue = tipo1
    '                cmbTipoSegnalazioneLivello1.Visible = True
    '            End If
    '            If tipo2 <> -1 Then
    '                CaricaTipologieLivello2()
    '                cmbTipoSegnalazioneLivello2.SelectedValue = tipo2
    '                cmbTipoSegnalazioneLivello2.Visible = True
    '            End If
    '            If tipo3 <> -1 Then
    '                CaricaTipologieLivello3()
    '                cmbTipoSegnalazioneLivello3.SelectedValue = tipo3
    '                cmbTipoSegnalazioneLivello3.Visible = True
    '            End If
    '            If tipo4 <> -1 Then
    '                CaricaTipologieLivello4()
    '                cmbTipoSegnalazioneLivello4.SelectedValue = tipo4
    '                cmbTipoSegnalazioneLivello4.Visible = True
    '            End If

    '            If cmbTipoSegnalazioneLivello0.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello0.SelectedValue <> "" Then
    '                condTipologia = "=" & cmbTipoSegnalazioneLivello0.SelectedValue
    '            Else
    '                condTipologia = " IS NULL "
    '            End If


    '            If cmbTipoSegnalazioneLivello0.SelectedValue = "6" Or cmbTipoSegnalazioneLivello0.SelectedValue = "1" Then
    '                If cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello1.SelectedValue <> "" Then
    '                    condTipologia1 = "=" & cmbTipoSegnalazioneLivello1.SelectedValue
    '                Else
    '                    condTipologia1 = " IS NULL "
    '                End If
    '                If cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello2.SelectedValue <> "" Then
    '                    condTipologia2 = "=" & cmbTipoSegnalazioneLivello2.SelectedValue
    '                Else
    '                    condTipologia2 = " IS NULL "
    '                End If
    '                If cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello3.SelectedValue <> "" Then
    '                    condTipologia3 = "=" & cmbTipoSegnalazioneLivello3.SelectedValue
    '                Else
    '                    condTipologia3 = " IS NULL "
    '                End If
    '                If cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello4.SelectedValue <> "" Then
    '                    condTipologia4 = "=" & cmbTipoSegnalazioneLivello4.SelectedValue
    '                Else
    '                    condTipologia4 = " IS NULL "
    '                End If

    '                par.cmd.CommandText = " SELECT ORARIO_UFFICIO,FUORI_ORARIO_UFFICIO1,FUORI_ORARIO_UFFICIO2 " _
    '                    & " FROM SISCOM_MI.SEMAFORO " _
    '                    & " WHERE ID_TIPOLOGIA_SEGNALAZIONE " & condTipologia _
    '                    & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV1 " & condTipologia1 _
    '                    & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV2 " & condTipologia2 _
    '                    & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV3 " & condTipologia3 _
    '                    & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV4 " & condTipologia4
    '                lettore = par.cmd.ExecuteReader
    '                Dim orarioUfficio As Integer = -1
    '                Dim fuoriorarioUfficio1 As Integer = -1
    '                Dim fuoriOrarioUfficio2 As Integer = -1
    '                If lettore.Read Then
    '                    orarioUfficio = par.IfNull(lettore("ORARIO_UFFICIO"), -1)
    '                    fuoriorarioUfficio1 = par.IfNull(lettore("FUORI_ORARIO_UFFICIO1"), -1)
    '                    fuoriOrarioUfficio2 = par.IfNull(lettore("FUORI_ORARIO_UFFICIO2"), -1)
    '                End If
    '                lettore.Close()

    '                Dim urgenza As Integer = 0
    '                Dim dataOggi As Date = Now
    '                If (CInt(dataOggi.DayOfWeek) >= 1 And CInt(dataOggi.DayOfWeek) < 5 _
    '                    And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") >= "0830" _
    '                    And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") <= "16.45") _
    '                Or (CInt(dataOggi.DayOfWeek) = 5 _
    '                    And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") >= "0830" _
    '                    And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") <= "16.30") Then
    '                    urgenza = orarioUfficio
    '                ElseIf CInt(dataOggi.DayOfWeek) = 6 Or CInt(dataOggi.DayOfWeek) = 7 _
    '                    Or (CInt(dataOggi.DayOfWeek) = 1 And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") < "0830") _
    '                    Or (CInt(dataOggi.DayOfWeek) = 5 And dataOggi.Hour.ToString.PadLeft(2, "0") & dataOggi.Minute.ToString.PadLeft(2, "0") > "16.30") Then
    '                    urgenza = fuoriOrarioUfficio2
    '                Else
    '                    urgenza = fuoriorarioUfficio1
    '                End If

    '                If urgenza <> -1 Then
    '                    Select Case urgenza
    '                        Case 1
    '                            cmbUrgenza.SelectedValue = "Bianco"
    '                        Case 2
    '                            cmbUrgenza.SelectedValue = "Verde"
    '                        Case 3
    '                            cmbUrgenza.SelectedValue = "Giallo"
    '                        Case 4
    '                            cmbUrgenza.SelectedValue = "Rosso"
    '                        Case 0
    '                            cmbUrgenza.SelectedValue = "Blu"
    '                    End Select
    '                    PanelUrgenzaCriticita.Visible = True
    '                Else
    '                    PanelUrgenzaCriticita.Visible = False
    '                End If
    '            Else
    '                PanelUrgenzaCriticita.Visible = False
    '            End If

    '        Else
    '            If cmbTipoSegnalazioneLivello0.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello0.SelectedValue <> "" Then
    '                condTipologia = "=" & cmbTipoSegnalazioneLivello0.SelectedValue
    '            Else
    '                condTipologia = " IS NULL "
    '            End If

    '            If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
    '                If cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello1.SelectedValue <> "" Then
    '                    condTipologia1 = "=" & cmbTipoSegnalazioneLivello1.SelectedValue
    '                Else
    '                    condTipologia1 = " IS NULL "
    '                End If
    '                If cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello2.SelectedValue <> "" Then
    '                    condTipologia2 = "=" & cmbTipoSegnalazioneLivello2.SelectedValue
    '                Else
    '                    condTipologia2 = " IS NULL "
    '                End If
    '                If cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello3.SelectedValue <> "" Then
    '                    condTipologia3 = "=" & cmbTipoSegnalazioneLivello3.SelectedValue
    '                Else
    '                    condTipologia3 = " IS NULL "
    '                End If
    '                If cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello4.SelectedValue <> "" Then
    '                    condTipologia4 = "=" & cmbTipoSegnalazioneLivello4.SelectedValue
    '                Else
    '                    condTipologia4 = " IS NULL "
    '                End If

    '                par.cmd.CommandText = " SELECT ORARIO_UFFICIO,FUORI_ORARIO_UFFICIO1,FUORI_ORARIO_UFFICIO2 " _
    '                    & " FROM SISCOM_MI.SEMAFORO " _
    '                    & " WHERE ID_TIPOLOGIA_SEGNALAZIONE " & condTipologia _
    '                    & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV1 " & condTipologia1 _
    '                    & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV2 " & condTipologia2 _
    '                    & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV3 " & condTipologia3 _
    '                    & " AND ID_TIPOLOGIA_SEGNALAZIONE_LIV4 " & condTipologia4
    '                lettore = par.cmd.ExecuteReader
    '                Dim orarioUfficio As Integer = -1
    '                Dim fuoriorarioUfficio1 As Integer = -1
    '                Dim fuoriOrarioUfficio2 As Integer = -1
    '                If lettore.Read Then
    '                    orarioUfficio = par.IfNull(lettore("ORARIO_UFFICIO"), -1)
    '                    fuoriorarioUfficio1 = par.IfNull(lettore("FUORI_ORARIO_UFFICIO1"), -1)
    '                    fuoriOrarioUfficio2 = par.IfNull(lettore("FUORI_ORARIO_UFFICIO2"), -1)
    '                End If
    '                lettore.Close()

    '                If orarioUfficio <> -1 Then
    '                    Select Case orarioUfficio
    '                        Case 1
    '                            cmbUrgenza.SelectedValue = "Bianco"
    '                        Case 2
    '                            cmbUrgenza.SelectedValue = "Verde"
    '                        Case 3
    '                            cmbUrgenza.SelectedValue = "Giallo"
    '                        Case 4
    '                            cmbUrgenza.SelectedValue = "Rosso"
    '                        Case 0
    '                            cmbUrgenza.SelectedValue = "Blu"
    '                    End Select
    '                    PanelUrgenzaCriticita.Visible = True
    '                Else
    '                    PanelUrgenzaCriticita.Visible = False
    '                End If
    '            Else
    '                PanelUrgenzaCriticita.Visible = False
    '            End If
    '        End If
    '        connData.chiudi(False)
    '    Catch ex As Exception
    '        connData.chiudi(False)
    '        Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - caricaUrgenzaPredefinita - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    Private Sub caricaTutteTipologie()
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
            connData.apri(False)
            par.caricaComboBox(query, DropDownListTipologia, "ID", "DESCRIZIONE", True)
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - caricaTutteTipologie - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    'Protected Sub presaInCarico_Click(sender As Object, e As System.EventArgs) Handles presaInCarico.Click
    '    Try
    '        If CType(Me.Master.FindControl("operatoreFiliale"), HiddenField).Value = "1" Then
    '            connData.apri(True)
    '            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_STATO=1 WHERE ID=" & idSegnalazione.Value & " AND ID_STATO=0"
    '            Dim ris As Integer = par.cmd.ExecuteNonQuery
    '            If ris = 1 Then
    '                connData.chiudi(True)
    '            Else
    '                connData.chiudi(False)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        connData.chiudi(False)
    '        Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - presaInCarico_Click - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    Protected Sub btnRelazionePadreFiglio_Click(sender As Object, e As System.EventArgs) Handles btnRelazionePadreFiglio.Click
        VisualizzaPaginaPadreFiglio()
    End Sub

    Protected Sub btnIndietro5_Click(sender As Object, e As System.EventArgs) Handles btnIndietro5.Click
        VisualizzaPaginaPrincipale()
    End Sub

    Private Sub caricaSegnalazioniPadreFigli()
        Try
            connData.apri()
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
                & " AND segnalazioni.id in (select id_Segnalazione_padre from siscom_mi.segnalazioni where ID=" & idSegnalazione.Value & ") " _
                & " ORDER BY ID_PERICOLO_sEGNALAZIONE DESC,DATA_ORA_RICHIESTA DESC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Session.Item("SegnalazionePadre") = dt
            RadGridSegnalazionePadre.Rebind()
            If dt.Rows.Count = 1 Then
                Dim apertura As String = "window.open('Segnalazione.aspx?NM=1&IDS=" & dt.Rows.Item(0).Item("ID").ToString & "', 'apr' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
                lblPadre.Text = "<a href=""#"" onclick=""javascript:" & apertura & """>" & dt.Rows.Item(0).Item("ID").ToString & "</a>"
                lblPadre.Font.Bold = True
            Else
                lblPadre.Text = "nessuna"
                lblPadre.Font.Bold = False
            End If


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
                & " AND segnalazioni.id_Segnalazione_padre =" & idSegnalazione.Value _
                & " ORDER BY ID_PERICOLO_sEGNALAZIONE DESC,DATA_ORA_RICHIESTA DESC "
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dt = New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Session.Item("SegnalazioniFiglie") = dt
            RadGridSegnalazioniFiglie.Rebind()
            If dt.Rows.Count > 0 Then
                Dim apertura As String = "document.getElementById('apriPaginaRelazioni').click();"
                lblFiglie.Text = "<a href=""#"" onclick=""javascript:" & apertura & """>" & dt.Rows.Count & "</a>"
                lblFiglie.Font.Bold = True
            Else
                lblFiglie.Text = "nessuna"
                lblFiglie.Font.Bold = True
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - caricaSegnalazioniPadreFigli - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridSegnalazionePadre_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridSegnalazionePadre.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridSegnalazionePadre.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('HiddenFieldPadreSelezionato').value='" & dataItem("ID").Text & "';" _
                                             & "document.getElementById('TextBoxPadre').value='Hai selezionato la segnalazione numero " & dataItem("ID").Text & "';")
            dataItem.Attributes.Add("onDblclick", "apriPadre();")
        End If
    End Sub

    Protected Sub RadGridSegnalazionePadre_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridSegnalazionePadre.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("SegnalazionePadre"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("SegnalazionePadre"), Data.DataTable)
    End Sub

    Protected Sub RadGridSegnalazioniFiglie_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridSegnalazioniFiglie.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridSegnalazioniFiglie.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('HiddenFieldFigliaSelezionata').value='" & dataItem("ID").Text & "';" _
                                             & "document.getElementById('TextBoxFiglie').value='Hai selezionato la segnalazione numero " & dataItem("ID").Text & "';")
            dataItem.Attributes.Add("onDblclick", "apriFiglia();")
        End If
    End Sub

    Protected Sub RadGridSegnalazionePadre_PreRender(sender As Object, e As System.EventArgs) Handles RadGridSegnalazionePadre.PreRender
        For Each dataItem As GridDataItem In RadGridSegnalazionePadre.Items
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

    Protected Sub RadGridSegnalazioniFiglie_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridSegnalazioniFiglie.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("SegnalazioniFiglie"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("SegnalazioniFiglie"), Data.DataTable)
    End Sub

    Protected Sub RadGridSegnalazioniFiglie_PreRender(sender As Object, e As System.EventArgs) Handles RadGridSegnalazioniFiglie.PreRender
        For Each dataItem As GridDataItem In RadGridSegnalazioniFiglie.Items
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

    Protected Sub ButtonEliminaPadre_Click(sender As Object, e As System.EventArgs) Handles ButtonEliminaPadre.Click
        If HiddenFieldPadreSelezionato.Value = "0" Then
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Selezionare almeno una segnalazione", 300, 150, "Attenzione", Nothing, Nothing)
        Else
            Try
                If HiddenFieldConfermaEliminaPadre.Value = "1" Then
                    'elimina
                    connData.apri(True)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_SEGNALAZIONE_PADRE = NULL,ID_sTATO=0 WHERE SEGNALAZIONI.ID=" & idSegnalazione.Value _
                        & " AND SEGNALAZIONI.ID_SEGNALAZIONE_PADRE=" & HiddenFieldPadreSelezionato.Value
                    par.cmd.ExecuteNonQuery()
                    WriteEvent("F252", "Eliminazione relazione con segnalazione padre n° " & HiddenFieldPadreSelezionato.Value)
                    connData.chiudi(True)
                    HiddenFieldPadreSelezionato.Value = "0"
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Relazione eliminata correttamente.", 300, 150, "Attenzione", Nothing, Nothing)
                End If
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - ButtonEliminaPadre_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
        HiddenFieldConfermaEliminaPadre.Value = "0"
        caricaSegnalazioniPadreFigli()
        CaricaDatiSegnalazione()
        VisualizzaPaginaPadreFiglio()
    End Sub
    Protected Sub ButtonEliminaFiglie_Click(sender As Object, e As System.EventArgs) Handles ButtonEliminaFiglie.Click
        If HiddenFieldFigliaSelezionata.Value = "0" Then
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Selezionare almeno una segnalazione", 300, 150, "Attenzione", Nothing, Nothing)

        Else
            Try
                If HiddenFieldConfermaEliminaFiglia.Value = "1" Then
                    'elimina
                    connData.apri(True)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_SEGNALAZIONE_PADRE = NULL,ID_STATO=0 WHERE SEGNALAZIONI.ID=" & HiddenFieldFigliaSelezionata.Value _
                        & " AND SEGNALAZIONI.ID_SEGNALAZIONE_PADRE=" & idSegnalazione.Value
                    par.cmd.ExecuteNonQuery()
                    WriteEvent("F252", "Eliminazione relazione con segnalazione figlia n° " & HiddenFieldFigliaSelezionata.Value)
                    connData.chiudi(True)
                    HiddenFieldFigliaSelezionata.Value = "0"
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Relazione eliminata correttamente.", 300, 150, "Attenzione", Nothing, Nothing)

                End If
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - ButtonEliminaFiglie_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
        HiddenFieldConfermaEliminaFiglia.Value = "0"
        caricaSegnalazioniPadreFigli()
        CaricaDatiSegnalazione()
        VisualizzaPaginaPadreFiglio()
    End Sub

    Protected Sub apriPaginaRelazioni_Click(sender As Object, e As System.EventArgs) Handles apriPaginaRelazioni.Click
        VisualizzaPaginaPadreFiglio()
    End Sub

    Private Sub caricaCanale()
        Try
            par.caricaComboBox("SELECT * FROM SISCOM_MI.CANALE ORDER BY ID ASC", DropDownListCanale, "ID", "DESCRIZIONE", False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - caricaCanale - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaAssegnatari()
        Try
            par.caricaComboTelerik("SELECT ID, operatore as nominativo FROM OPERATORI where MOD_SICUREZZA= 1 and FL_SEC_GEST_COMPLETA =1 ORDER BY cognome ASC", cmbAssegnatario, "ID", "NOMINATIVO", True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - caricaAssegnatari - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    'Protected Sub btnInterventi_Click(sender As Object, e As System.EventArgs) Handles btnInterventi.Click
    '    par.modalDialogConfirm("Info", "Sei sicuro di voler procedere?", "Ok", "document.getElementById('btnInterventi1').click();", "Annulla", "", Page)
    'End Sub

    Private Sub InserisciIntervento()
        Try
            Dim idInterv As String = "-1"

            connData.apri(True)
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_INTERVENTI_SICUREZZA.NEXTVAL FROM DUAL"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idInterv = par.IfNull(lettore(0), "-1")
            End If
            lettore.Close()
            idIntervento.Value = idInterv

            If unita.Value = "0" Or unita.Value = "" Then
                unita.Value = "NULL"
            End If

            par.cmd.CommandText = " INSERT INTO SISCOM_MI.INTERVENTI_SICUREZZA (" _
                & " ID," _
                & " ID_SEGNALAZIONE, ASSEGNATARIO, ID_STATO, ID_TIPO_INTERVENTO, ID_UNITA, ID_EDIFICIO," _
                & " DATA_ORA_INSERIMENTO,DATA_PRE_ASSEGNATO) " _
                & " VALUES ( " & idInterv & "," _
                & "  " & idSegnalazione.Value & "  ," _
                & "  " & par.insDbValue(cmbAssegnatario.SelectedItem.Text, True) & "  ,1," _
                & "  " & cmbTipoInterv.SelectedValue & "  ," _
                & "  " & unita.Value & "," _
                & "  " & par.insDbValue(DropDownListEdificio.SelectedValue, False, False, True) & "," _
                & "'" & Format(Now, "yyyyMMddHHmm") & "','" & Format(Now, "yyyyMMdd") & "')"
            par.cmd.ExecuteNonQuery()
            WriteEvent("F258", "" & cmbTipoInterv.SelectedItem.Text)
            connData.chiudi(True)
            CaricaInterventi()

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - InserisciIntervento - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnElencoInterventi_Click(sender As Object, e As System.EventArgs) Handles btnElencoInterventi.Click
        VisualizzaPaginaInterventi()
        CaricaInterventi()
    End Sub

    Protected Sub btnIndietro6_Click(sender As Object, e As System.EventArgs) Handles btnIndietro6.Click
        VisualizzaPaginaPrincipale()
    End Sub

    Protected Sub RadGridInterventi_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridInterventi.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridInterventi.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('HiddenFieldIntervento').value='" & dataItem("ID").Text & "';" _
                                             & "document.getElementById('txtInterventoSelected').value='Hai selezionato l\'intervento numero " & dataItem("ID").Text & "';")
            dataItem.Attributes.Add("onDblclick", "apriIntervento();")
        End If
    End Sub



    Protected Sub RadGridInterventi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridInterventi.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("ElencoInterventi"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("ElencoInterventi"), Data.DataTable)
    End Sub

    Protected Sub btnSalvaTipoInt_Click(sender As Object, e As System.EventArgs) Handles btnSalvaTipoInt.Click
        If cmbTipoInterv.SelectedValue <> "-1" Then
            InserisciIntervento()

            Dim apertura As String = "window.open('NuovoIntervento.aspx?NM=1&IDI=" & idIntervento.Value & "', 'nint' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessuna tipologia selezionata!", 300, 150, "Attenzione", Nothing, Nothing)
            'par.modalDialogMessage("Attenzione", "Nessuna tipologia selezionata!", Me.Page)
        End If
    End Sub

    Protected Sub btnVisualizza6_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza6.Click
        If IsNumeric(HiddenFieldIntervento.Value) AndAlso HiddenFieldIntervento.Value <> "-1" AndAlso HiddenFieldIntervento.Value <> "0" Then
            RadGridInterventi.Rebind()
            Dim apertura As String = "window.open('NuovoIntervento.aspx?NM=1&IDI=" & HiddenFieldIntervento.Value & "', 'nint' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)
        Else
            'par.modalDialogMessage("Sicurezza", "E\' necessario selezionare un intervento.", Me.Page, "info", , )
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessuna intervento selezionato!", 300, 150, "Attenzione", Nothing, Nothing)
        End If
    End Sub


End Class