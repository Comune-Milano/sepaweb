Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports Telerik.Web.UI

Partial Class GESTIONE_CONTATTI_Segnalazione
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
                TipoAllegato.Value = par.getIdOggettoAllegatiWs("Segnalazione")
                If Not IsNothing(Request.QueryString("TIPO")) AndAlso Request.QueryString("TIPO") = "1" Then
                    imgEsci.OnClientClick = "self.close();return false;"
                End If
                HFbtnClickGo.Value = btnSalva.ClientID
                cmbUrgenza.LoadContentFile("Gravita.xml")
                cmbUrgenzaIniz.LoadContentFile("Gravita.xml")
                ControlloCustode()
                CaricaTipologiaManutenzione()
                caricaTutteTipologie()
                CaricaTipologiaSegnalante()
                caricaCanale()
                CaricaTipologieLivello0()
                CaricaTipologieLivello1()
                CaricaTipologieLivello2()
                CaricaTipologieLivello3()
                CaricaTipologieLivello4()
                CaricaListaDocumentiDaPortare()
                caricaSegnalazioniPadreFigli()
                VisualizzaAllegatiPresenti()


                If Not IsNothing(Request.QueryString("IDS")) Then
                    idSegnalazione.Value = Request.QueryString("IDS")
                    CaricaDatiSegnalazione()
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Segnalazioni", "Non è possibile caricare i dati relativi alla segnalazione selezionata. Contattare l\'amministratore di sistema.", Page, "info", "Home.aspx")
                    Exit Sub
                End If
                txtDataCInt.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtOraCInt.Text = Format(Now, "HH:mm")
                txtDataCInt.Text = Format(Now, "dd/MM/yyyy")
            End If
            If Not IsNothing(Request.QueryString("SL")) AndAlso IsNumeric(Request.QueryString("SL")) Then
                solaLettura()
            End If
            If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                NoMenu()
            End If
            '*** Modifica richiesta da Zuccaro e anticipata *** 
            'anche gli operatori di call center possono modificare le segnalazioni
            'If Session.Item("id_caf") = 63 Then
            '    cmbUrgenza.Enabled = False
            '    cmbTipoSegnalazioneLivello0.Enabled = False
            '    cmbTipoSegnalazioneLivello1.Enabled = False
            '    cmbTipoSegnalazioneLivello2.Enabled = False
            '    cmbTipoSegnalazioneLivello3.Enabled = False
            '    cmbTipoSegnalazioneLivello4.Enabled = False
            '    DropDownListTipologia.Enabled = False
            'End If
            If Not IsNothing(Request.QueryString("VIEW")) Then
                VisualizzaPaginaAppuntamenti()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - Page_Load - " & ex.Message)
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
            'operatore comunale non può eliminare gli appuntamenti
            If CType(Me.Master.FindControl("operatoreComune"), HiddenField).Value = "1" Then
                For Each elemento As DataGridItem In DataGridElencoAppuntamenti.Items
                    elemento.Cells(par.IndDGC(DataGridElencoAppuntamenti, "ELIMINA")).Text = ""
                Next
            End If
            cmbUrgenzaIniz.Enabled = False
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
            If operatoreCC.Value = "1" Then
                CheckBoxAttoVandalico.Enabled = False
                btnNotaGestionale.Enabled = False
            End If
            If operatoreComune.Value <> "1" And Session.Item("FL_GC_SUPERVISORE") <> "1" Then
                CheckBoxDVCA.Enabled = False
            End If
            If operatoreFiliale.Value = "1" Then
                CheckBoxContattatoFornitore.Enabled = False
            End If
            If operatoreComune.Value = "1" Then
                btnAppuntamento.Visible = False
            End If
            If Session.Item("ID_CAF") <> "2" Then
                btnNotaGestionale.Visible = False
            End If
            If Not IsNothing(Session.Item("MOD_FO_LIMITAZIONI")) AndAlso Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
                fornitoreEsterno.Value = "1"
                abilitaDisabilitaFornitore()
                VerificaDate()
            Else
                lblTipologiaManutenzione.Visible = True

                ImpostaVisibilitaRadioButton()
                cmbTipologiaManutenzione.Enabled = False
                txtDataSopralluogo.Enabled = False
                txtDataProgrammataIntervento.Enabled = False
                txtNoteSopralluogo.Enabled = False
                txtNoteEffIntervento.Enabled = False
                txtDataEffettivaIntervento.Enabled = False
                txtOraSopralluogo.Enabled = False
                txtOraProgrammataIntervento.Enabled = False
                txtOraEffettivaIntervento.Enabled = False
            End If
            ModificaOggPage()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaListaDocumentiDaPortare()

        Try
            If cmbTipoSegnalazioneLivello0.SelectedValue = "0" Then
                Dim query As String = ""
                If cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And Not String.IsNullOrEmpty(cmbTipoSegnalazioneLivello2.SelectedValue) Then
                    query = "SELECT DESCRIZIONE AS DOCUMENTI_RICHIESTI FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI," _
                        & "SISCOM_MI.TIPOLOGIE_DOCUMENTI WHERE SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI.ID_TIPOLOGIA_DOCUMENTO=TIPOLOGIE_DOCUMENTI.ID " _
                        & " AND ID_TIPO_SEGN_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue _
                        & " AND ID_TIPO_SEGN_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue _
                        & " ORDER BY DESCRIZIONE"
                Else
                    If cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And Not String.IsNullOrEmpty(cmbTipoSegnalazioneLivello1.SelectedValue) Then
                        query = "SELECT DESCRIZIONE AS DOCUMENTI_RICHIESTI FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI," _
                            & "SISCOM_MI.TIPOLOGIE_DOCUMENTI WHERE SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI.ID_TIPOLOGIA_DOCUMENTO=TIPOLOGIE_DOCUMENTI.ID " _
                            & "AND ID_TIPO_SEGN_LIVELLO_2 IS NULL " _
                            & "AND ID_TIPO_SEGN_LIVELLO_1= " & cmbTipoSegnalazioneLivello1.SelectedValue _
                            & " ORDER BY DESCRIZIONE"
                    End If
                End If
                If query <> "" Then
                    connData.apri(False)
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
                    connData.chiudi(False)
                End If
                PanelElencoDocumentiRichiesti.Visible = True
            Else
                PanelElencoDocumentiRichiesti.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - CaricaListaDocumentiDaPortare - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaDatiSegnalazione()
        Try
            If idSegnalazione.Value <> "-1" Then
                connData.apri()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    If par.IfNull(lettore("FL_DVCA"), 0) = 1 Then
                        CheckBoxDVCA.Checked = True
                    Else
                        CheckBoxDVCA.Checked = False
                    End If
                    If par.IfNull(lettore("FL_AV"), 0) = 1 Then
                        CheckBoxAttoVandalico.Checked = True
                    Else
                        CheckBoxAttoVandalico.Checked = False
                    End If
                    If par.IfNull(lettore("FL_FS"), 0) = 1 Then
                        CheckBoxFalsa.Checked = True
                    Else
                        CheckBoxFalsa.Checked = False
                    End If
                    If par.IfNull(lettore("FL_CONTATTO_FORNITORE"), 0) = 1 Then
                        CheckBoxContattatoFornitore.Checked = True
                        TextBoxContattatoFornitore.Enabled = True
                        TextBoxOraContattatoFornitore.Enabled = True
                    Else
                        CheckBoxContattatoFornitore.Checked = False
                        TextBoxContattatoFornitore.Enabled = False
                        TextBoxOraContattatoFornitore.Enabled = False
                    End If
                    If par.IfNull(lettore("FL_VERIFICA_FORNITORE"), 0) = 1 Then
                        CheckBoxVerificaFornitore.Checked = True
                        TextBoxVerificaFornitore.Enabled = True
                        TextBoxOraVerificaFornitore.Enabled = True
                    Else
                        CheckBoxVerificaFornitore.Checked = False
                        TextBoxVerificaFornitore.Enabled = False
                        TextBoxOraVerificaFornitore.Enabled = False
                    End If
                    'TextBoxContattatoFornitore.Text = par.FormattaData(par.IfNull(lettore("DATA_CONTATTO_FORNITORE"), ""))
                    TextBoxContattatoFornitore.Text = par.FormattaData(Mid(par.IfNull(lettore("DATA_CONTATTO_FORNITORE"), ""), 1, 8))
                    If Not String.IsNullOrEmpty(Mid(par.IfNull(lettore("DATA_CONTATTO_FORNITORE"), ""), 9, 2)) Then
                        TextBoxOraContattatoFornitore.SelectedTime = New TimeSpan(Mid(par.IfNull(lettore("DATA_CONTATTO_FORNITORE"), ""), 9, 2), Mid(par.IfNull(lettore("DATA_CONTATTO_FORNITORE"), ""), 11, 2), "00")
                    End If

                    'TextBoxVerificaFornitore.Text = par.FormattaData(par.IfNull(lettore("DATA_VERIFICA_FORNITORE"), ""))
                    TextBoxVerificaFornitore.Text = par.FormattaData(Mid(par.IfNull(lettore("DATA_VERIFICA_FORNITORE"), ""), 1, 8))
                    If Not String.IsNullOrEmpty(Mid(par.IfNull(lettore("DATA_VERIFICA_FORNITORE"), ""), 9, 2)) Then
                        TextBoxOraVerificaFornitore.SelectedTime = New TimeSpan(Mid(par.IfNull(lettore("DATA_VERIFICA_FORNITORE"), ""), 9, 2), Mid(par.IfNull(lettore("DATA_VERIFICA_FORNITORE"), ""), 11, 2), "00")
                    End If

                    txtDataSopralluogo.Text = par.FormattaData(Mid(par.IfNull(lettore("DATA_SOPRALLUOGO"), ""), 1, 8))
                    If Not String.IsNullOrEmpty(Mid(par.IfNull(lettore("DATA_SOPRALLUOGO"), ""), 9, 2)) Then
                        txtOraSopralluogo.SelectedTime = New TimeSpan(Mid(par.IfNull(lettore("DATA_SOPRALLUOGO"), ""), 9, 2), Mid(par.IfNull(lettore("DATA_SOPRALLUOGO"), ""), 11, 2), "00")
                    End If

                    txtDataProgrammataIntervento.Text = par.FormattaData(Mid(par.IfNull(lettore("DATA_PROGRAMMATA_INT"), ""), 1, 8))
                    If Not String.IsNullOrEmpty(txtDataProgrammataIntervento.Text) Then
                        lblDataOraProgrammataIntervento.ForeColor = Drawing.Color.Red
                        lblDataOraProgrammataIntervento.Font.Bold = True
                    End If
                    txtDataProgrammataUltimoIntervento.Text = par.FormattaData(Mid(par.IfNull(lettore("DATA_PROGRAMMATA_INT2"), ""), 1, 8))
                    If Not String.IsNullOrEmpty(txtDataProgrammataUltimoIntervento.Text) Then
                        lblDataOraUltimoInterventoEseguito.ForeColor = Drawing.Color.Red
                        lblDataOraUltimoInterventoEseguito.Font.Bold = True
                    End If
                    If Not String.IsNullOrEmpty(Mid(par.IfNull(lettore("DATA_PROGRAMMATA_INT"), ""), 9, 2)) Then
                        txtOraProgrammataIntervento.SelectedTime = New TimeSpan(Mid(par.IfNull(lettore("DATA_PROGRAMMATA_INT"), ""), 9, 2), Mid(par.IfNull(lettore("DATA_PROGRAMMATA_INT"), ""), 11, 2), "00")
                    End If

                    txtDataEffettivaIntervento.Text = par.FormattaData(Mid(par.IfNull(lettore("DATA_EFFETTIVA_INT"), ""), 1, 8))
                    If Not String.IsNullOrEmpty(txtDataEffettivaIntervento.Text) Then
                        lblDataOraEffettivaIntervento.ForeColor = Drawing.Color.Red
                        lblDataOraEffettivaIntervento.Font.Bold = True
                    End If
                    If Not String.IsNullOrEmpty(Mid(par.IfNull(lettore("DATA_EFFETTIVA_INT"), ""), 9, 2)) Then
                        txtOraEffettivaIntervento.SelectedTime = New TimeSpan(Mid(par.IfNull(lettore("DATA_EFFETTIVA_INT"), ""), 9, 2), Mid(par.IfNull(lettore("DATA_EFFETTIVA_INT"), ""), 11, 2), "00")
                    End If

                    'Caricamento ultima nota sopralluogo (per evitare inserimenti doppi)
                    par.cmd.CommandText = "SELECT NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE NOTE LIKE '%nota sopralluogo%' AND ID_SEGNALAZIONE = " & idSegnalazione.Value & " ORDER BY DATA_ORA DESC"
                    hiddenNotaSopralluogo.Value = par.cmd.ExecuteScalar
                    If Not String.IsNullOrEmpty(hiddenNotaSopralluogo.Value) Then
                        hiddenNotaSopralluogo.Value = hiddenNotaSopralluogo.Value.Substring(hiddenNotaSopralluogo.Value.IndexOf(")") + 2)
                        txtNoteSopralluogo.Text = hiddenNotaSopralluogo.Value
                    End If
                    'Caricamento ultima nota data effettivo intervento (per evitare inserimenti doppi)
                    par.cmd.CommandText = "SELECT NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE NOTE LIKE '%nota data effettivo intervento%' AND ID_SEGNALAZIONE = " & idSegnalazione.Value & " ORDER BY DATA_ORA DESC"
                    hiddenNotaEffIntervento.Value = par.cmd.ExecuteScalar
                    If Not String.IsNullOrEmpty(hiddenNotaEffIntervento.Value) Then
                        hiddenNotaEffIntervento.Value = hiddenNotaEffIntervento.Value.Substring(hiddenNotaEffIntervento.Value.IndexOf(")") + 2)
                        txtNoteEffIntervento.Text = hiddenNotaEffIntervento.Value
                    End If


                    flCustode.Value = par.IfNull(lettore("fl_custode"), "0")
                    lblNrich.Text = idSegnalazione.Value
                    lblTitolo.Text = "Segnalazione numero " & idSegnalazione.Value
                    TextBoxCognomeChiamante.Text = par.IfNull(lettore("COGNOME_RS"), "")
                    cmbTipologiaSegnalante.ClearSelection()
                    cmbTipologiaSegnalante.SelectedValue = par.IfNull(lettore("ID_TIPOLOGIA_SEGNALANTE"), "")
                    Dim idTipologiaManutenzione As String = par.IfNull(lettore("ID_TIPOLOGIA_MANUTENZIONE"), "-1")
                    If idTipologiaManutenzione = "-1" Then
                        idTipologiaManutenzione = RicavaTipologiaManutenzione()
                    End If
                    cmbTipologiaManutenzione.ClearSelection()
                    cmbTipologiaManutenzione.SelectedValue = par.IfEmpty(idTipologiaManutenzione, "-1")


                    idTipoSegnalazione.Value = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "-1")
                    If idTipoSegnalazione.Value = "1" Or idTipoSegnalazione.Value = "6" Then
                        Dim idPericoloSegnalazione As Integer = par.IfNull(lettore("ID_PERICOLO_SEGNALAZIONE"), -1)
                        Dim idPericoloSegnalazioneIniz As Integer = par.IfNull(lettore("ID_PERICOLO_SEGNALAZIONE_INIZ"), -1)
                        If idPericoloSegnalazione <> -2 Then
                            Me.cmbUrgenza.SelectedIndex = idPericoloSegnalazione
                        End If
                        If idPericoloSegnalazioneIniz <> -2 Then
                            Me.cmbUrgenzaIniz.SelectedIndex = idPericoloSegnalazioneIniz
                        End If
                        pericoloSegnalazioneOld.Value = cmbUrgenza.SelectedItem.Text
                    End If

                    'Verifica conferma tipologia manutenzione
                    If par.IfNull(lettore("FL_TIPOLOGIA_CONFERMATA"), "0") = "0" Then
                        cmbConfermaTipologiaFornitore.ClearSelection()
                        cmbConfermaTipologiaFornitore.SelectedValue = -1
                    Else
                        cmbConfermaTipologiaFornitore.ClearSelection()
                        cmbConfermaTipologiaFornitore.SelectedValue = 2
                        imgAlert.Visible = False
                        lblTipologiaConfermata.Text = "Tipologia del fornitore confermata"
                    End If
                    'VerificaSLA DataOraSopralluogo 
                    par.cmd.CommandText = "SELECT NVL(SISCOM_MI.GETDIFFDATAORA(" & Left(par.IfNull(lettore("DATA_SOPRALLUOGO"), "0"), 10) & "," & Left(par.IfNull(lettore("DATA_ORA_RICHIESTA"), "0"), 10) & ",2),0) FROM DUAL"
                    Dim differenzaDataSopralluogo As Integer = CInt(par.cmd.ExecuteScalar)
                    imgBallYellowSopralluogo.Visible = False
                    imgBallYellowDataEffettiva.Visible = False
                    Dim idUrgenza As Integer = cmbUrgenza.SelectedIndex
                    Select Case idUrgenza
                        Case "3"
                            'GIALLO
                            If differenzaDataSopralluogo >= 120 Then
                                imgBallYellowSopralluogo.Visible = True
                            Else
                                imgBallYellowSopralluogo.Visible = False
                            End If
                        Case "4"
                            'ROSSA
                            If differenzaDataSopralluogo >= 60 Then
                                imgBallYellowSopralluogo.Visible = True
                            Else
                                imgBallYellowSopralluogo.Visible = False
                            End If
                    End Select

                    'VerificaSLA DataOraEffettivoIntervento
                    par.cmd.CommandText = "SELECT NVL(SISCOM_MI.GETDIFFDATAORA(" & par.IfNull(lettore("DATA_EFFETTIVA_INT"), "0") & "," & par.IfNull(lettore("DATA_ORA_RICHIESTA"), "0") & ",1),0) FROM DUAL"
                    Dim differenzaDataEffettiva As Integer = CInt(par.cmd.ExecuteScalar)
                    idUrgenza = cmbUrgenza.SelectedIndex
                    Select Case idUrgenza
                        Case "2"
                            'VERDE
                            If differenzaDataEffettiva >= 168 Then
                                imgBallYellowDataEffettiva.Visible = True
                            Else
                                imgBallYellowDataEffettiva.Visible = False
                            End If
                        Case "3"
                            'GIALLO
                            If differenzaDataEffettiva >= 72 Then
                                imgBallYellowDataEffettiva.Visible = True
                            Else
                                imgBallYellowDataEffettiva.Visible = False
                            End If
                        Case "4"
                            'ROSSA
                            If differenzaDataEffettiva >= 72 Then
                                imgBallYellowDataEffettiva.Visible = True
                            Else
                                imgBallYellowDataEffettiva.Visible = False
                            End If
                    End Select

                    TextBoxNomeChiamante.Text = par.IfNull(lettore("NOME"), "")
                    TextBoxTelefono1Chiamante.Text = par.IfNull(lettore("TELEFONO1"), "")
                    TextBoxTelefono2Chiamante.Text = par.IfNull(lettore("TELEFONO2"), "")
                    TextBoxEmailChiamante.Text = par.IfNull(lettore("MAIL"), "")
                    txtDescrizione.Text = par.IfNull(lettore("DESCRIZIONE_RIC"), "")
                    TextBoxDanneggiante.Text = par.IfNull(lettore("DANNEGGIANTE"), "")
                    TextBoxDanneggiato.Text = par.IfNull(lettore("DANNEGGIATO"), "")
                    descrizioneOLD.Value = txtDescrizione.Text
                    DropDownListCanale.ClearSelection()
                    DropDownListCanale.SelectedValue = par.IfNull(lettore("ID_CANALE"), 0)
                    If par.IfNull(lettore("fl_sollecito"), 0) = 1 Then
                        lblSollecito.Text = "SOLLECITATA"
                        lblSollecito.ForeColor = Drawing.Color.Red
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

                    ControllaCondominio()
                    CaricaTipologieLivello0()
                    cmbTipoSegnalazioneLivello0.SelectedValue = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "-1")
                    tipoLivello1old.Value = cmbTipoSegnalazioneLivello0.SelectedItem.Text
                    If par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "-1") = "1" Or par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "-1") = "6" Then
                        PanelUrgenzaCriticita.Visible = True
                    Else
                        PanelUrgenzaCriticita.Visible = False
                    End If
                    If par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "-1") = "0" Then
                        btnAggiungiAppuntamento.Enabled = True
                    Else
                        btnAggiungiAppuntamento.Enabled = False
                    End If
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
                            par.cmd.CommandText = "SELECT id_contratto,siscom_mi.Getstatocontratto2(id_contratto,1)as contlink,(select rapporti_utenza.cod_contratto from siscom_mi.rapporti_utenza where rapporti_utenza.id=id_contratto) as cod_Contratto FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & unita.Value & " AND siscom_mi.Getstatocontratto2(id_contratto,0) like '%CORSO%'"
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
                        Dim condContratto As String = ""
                        If par.IfNull(lettore("ID_CONTRATTO"), "-1") <> "-1" Then
                            condContratto = " and unita_contrattuale.id_contratto=" & par.IfNull(lettore("ID_CONTRATTO"), "-1")
                        Else
                            condContratto = " AND siscom_mi.Getstatocontratto2(id_contratto,0) LIKE '%CORSO%' "
                        End If
                        par.cmd.CommandText = "SELECT ID_ANAGRAFICA  , RAGIONE_SOCIALE,COGNOME ,NOME   FROM siscom_mi.SOGGETTI_CONTRATTUALI ,SISCOM_MI.ANAGRAFICA " _
                                            & "WHERE id_contratto IN (SELECT id_contratto FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & unita.Value _
                                            & condContratto & ")" _
                                            & "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'  AND ANAGRAFICA.ID = ID_ANAGRAFICA "
                        readerInt = par.cmd.ExecuteReader
                        If readerInt.Read Then
                            idSelectedIntestatario.Value = par.IfNull(readerInt("ID_ANAGRAFICA"), "")
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
                    par.cmd.CommandText = "SELECT " _
                        & " APPUNTAMENTI_CALL_CENTER.ID," _
                        & " APPUNTAMENTI_CALL_CENTER.DATA_APPUNTAMENTO AS DATA_APP," _
                        & " ID_SEGNALAZIONE," _
                        & " COGNOME," _
                        & " APPUNTAMENTI_CALL_CENTER.NOME," _
                        & " TAB_FILIALI.NOME AS SEDE_TERRITORIALE," _
                        & " TAB_FILIALI.ID AS ID_FILIALE," _
                        & " TO_CHAR(TO_DATE(DATA_APPUNTAMENTO,'YYYYMMDD'),'DD/MM/YYYY') AS DATA_APPUNTAMENTO," _
                        & " APPUNTAMENTI_SPORTELLI.DESCRIZIONE AS SPORTELLO," _
                        & " APPUNTAMENTI_ORARI.ORARIO AS ORARIO," _
                        & " APPUNTAMENTI_STATI.DESCRIZIONE AS STATO," _
                        & " TELEFONO," _
                        & " CELLULARE," _
                        & " EMAIL," _
                        & " NOTE," _
                        & " 'Eliminata dall''operatore '||(SELECT OPERATORE FROM SEPA.OPERATORI A WHERE A.ID=APPUNTAMENTI_cALL_cENTER.ID_OPERATORE_ELIMINAZIONE)||' il '||TO_CHAR(TO_DATE(SUBSTR(DATA_ELIMINAZIONE,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' alle ore '||SUBSTR(DATA_ELIMINAZIONE,9,2)||':'||SUBSTR(DATA_ELIMINAZIONE,11,2) AS ELIMINA,CASE WHEN DATA_ELIMINAZIONE IS NULL THEN 0 ELSE 1 END AS FL_ELIMINA " _
                        & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER," _
                        & " SISCOM_MI.APPUNTAMENTI_SPORTELLI," _
                        & " SISCOM_MI.APPUNTAMENTI_STATI," _
                        & " SISCOM_MI.APPUNTAMENTI_ORARI, " _
                        & " SISCOM_MI.TAB_FILIALI " _
                        & " WHERE ID_sEGNALAZIONE=" & idSegnalazione.Value _
                        & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID(+) " _
                        & " AND APPUNTAMENTI_CALL_CENTER.ID_ORARIO=APPUNTAMENTI_ORARI.ID(+) " _
                        & " AND APPUNTAMENTI_CALL_CENTER.ID_STATO_APPUNTAMENTO=APPUNTAMENTI_STATI.ID(+) " _
                        & " AND TAB_FILIALI.ID=APPUNTAMENTI_cALL_CENTER.ID_STRUTTURA "
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable
                    da.Fill(dt)
                    da.Dispose()
                    If dt.Rows.Count > 0 Then
                        DataGridElencoAppuntamenti.DataSource = dt
                        DataGridElencoAppuntamenti.DataBind()
                    End If
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
                ImpostaSoloLetturaElementiPagina()
                StatoSegnalazione()
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
                par.modalDialogMessage("Caricamento dati segnalazione", "Non è possibile caricare correttamente i dati", Me.Page, "info", "Home.aspx", )
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - CaricaDatiSegnalazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub RiempiNoteChiusura()
        Try
            connData.apri()

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - RiempiNoteChiusura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaElencoAllegati()
        Dim sfondo As String = "#FFFFFF"
        Dim tabellaAllegati As String = ""
        tabellaAllegati &= "<table cellpadding='0' cellspacing='0' style='width:95%;'><tr style='font-family: ARIAL; font-size: 8pt; font-weight: bold'><td width='200px'>ALLEGATO</td><td width='200px'>DESCRIZIONE</td><td width='150px'>DATA-ORA</td></tr>"
        par.cmd.CommandText = "SELECT nome, GETDATAORA(DATA_ORA) AS DATA_ORA FROM SISCOM_MI.ALLEGATI_WS WHERE ID_OGGETTO = " & idSegnalazione.Value & " AND STATO = 0 "
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        da.Dispose()
        Dim nomeFileAll As String = ""
        Dim dataFileAll As String = ""
        Dim descrizioneAll As String = ""
        'Dim idSegnalazione As String = ""
        For Each elemento As Data.DataRow In dt.Rows
            nomeFileAll = par.IfNull(elemento.Item("NOME"), "")
            dataFileAll = par.IfNull(elemento.Item("DATA_ORA"), "")
            ' descrizioneAll = par.IfNull(elemento.Item("DESCRIZIONE"), "")
            If IO.File.Exists(Server.MapPath("~\/ALLEGATI\/SEGNALAZIONI\/") & nomeFileAll) Then
                tabellaAllegati &= "<tr style='height: 16px;font-family: ARIAL; font-size: 8pt;'" _
                & "><td style='border-bottom-style: solid; border-bottom-width: 1px; " _
                & "border-bottom-color: #000000;' width='200px'><a  alt='Download' href='../ALLEGATI/SEGNALAZIONI/" & nomeFileAll & "' target='_blank'>" _
                & nomeFileAll & "</a></td><td style='border-bottom-style: solid; border-bottom-width: 1px; " _
                & "border-bottom-color: #000000;' width='200px'>" & nomeFileAll & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='100px'>" & dataFileAll & "</td></tr>"
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
                btnStampSopr.Enabled = True
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
                btnStampSopr.Enabled = True
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
                btnStampSopr.Enabled = True
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
                btnStampSopr.Enabled = True
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
                btnStampSopr.Enabled = True
                imgEsci.Enabled = True
            Case 4
                'ORDINE EMESSO
                ImpostaSoloLetturaElementiPagina()
                btnAnnulla.Enabled = False
                btnSollecito.Enabled = True
                imgChiudiSegnalazione.Enabled = False
                'imgAllega.Enabled = False
                imgAllega.Enabled = True
                btnSalva.Enabled = False
                btnStampa.Enabled = True
                btnStampSopr.Enabled = True
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
                btnStampSopr.Enabled = True
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
                imgAllega.Enabled = True
                btnSalva.Enabled = False
                btnStampa.Enabled = True
                btnStampSopr.Enabled = True
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
        If Session.Item("ID_CAF") = "2" Then
            par.cmd.CommandText = "SELECT segnalazioni_note.*,operatori.operatore FROM sepa.operatori,siscom_mi.segnalazioni_note where segnalazioni_note.id_segnalazione=" & idSegnal & " and segnalazioni_note.id_operatore=operatori.id (+) order by data_ora desc"
        Else
            par.cmd.CommandText = "SELECT segnalazioni_note.*,operatori.operatore FROM sepa.operatori,siscom_mi.segnalazioni_note where segnalazioni_note.id_segnalazione=" & idSegnal & " and segnalazioni_note.id_operatore=operatori.id (+) and id_tipo_segnalazione_note<>3 order by data_ora desc"
        End If
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
        cmbTipologiaSegnalante.Enabled = False
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
        cmbTipoSegnalazioneLivello1.Enabled = True
        cmbTipoSegnalazioneLivello2.Enabled = True
        cmbTipoSegnalazioneLivello3.Enabled = True
        cmbTipoSegnalazioneLivello4.Enabled = True
        txtDescrizione.Enabled = True
        TextBoxNota.Enabled = True
        cmbUrgenza.Enabled = True
        TextBoxDanneggiante.Enabled = False
        TextBoxDanneggiato.Enabled = False
        'lblAppuntamento.Text = par.EliminaLink(lblAppuntamento.Text)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - WriteEvent - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Try
            If confermaAnnullaSegnalazione.Value = "1" Then
                connData.apri(True)
                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_OPERATORE_CH=" & Session.Item("ID_OPERATORE") & ", ID_STATO=2 WHERE ID=" & idSegnalazione.Value
                par.cmd.ExecuteNonQuery()
                StatoSegnalazione()
                CaricaTabellaNote(idSegnalazione.Value)
                WriteEvent("F234", "")
                connData.chiudi(True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                    par.modalDialogMessage("Annullamento segnalazione", "Operazione effettuata correttamente.", Me.Page, "successo", "Segnalazione.aspx?NM=1&IDS=" & idSegnalazione.Value, )
                Else
                    par.modalDialogMessage("Annullamento segnalazione", "Operazione effettuata correttamente.", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idSegnalazione.Value, )
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - btnAnnulla_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnSollecito_Click(sender As Object, e As System.EventArgs) Handles btnSollecito.Click
        VisualizzaPaginaSollecito()
    End Sub
    Protected Sub imgChiudiSegnalazione_Click(sender As Object, e As System.EventArgs) Handles imgChiudiSegnalazione.Click
        VisualizzaPaginaChiusuraSegnalazione()
    End Sub

    'Protected Sub imgAllega_Click(sender As Object, e As System.EventArgs) Handles imgAllega.Click
    '    VisualizzaPaginaAllegati()
    'End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If CheckControl() = True Then
            Try
                connData.apri(True)
                Dim valoreTipoIntervento As String = "NULL"
                Dim valoreUrgenza As String = "NULL"
                If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
                    valoreUrgenza = cmbUrgenza.SelectedIndex
                End If
                If cmbTipoSegnalazioneLivello0.SelectedValue <> "0" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_CALL_cENTER SET ID_OPERATORE_ELIMINAZIONE=" & Session.Item("ID_OPERATORE") & ",DATA_ELIMINAZIONE=" & Format(Now, "yyyyMMddHHmmss") & " WHERE ID_SEGNALAZIONE=" & idSegnalazione.Value
                    par.cmd.ExecuteNonQuery()
                End If
                Dim livello0 As String = "NULL"
                If cmbTipoSegnalazioneLivello0.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello0.Items.Count > 0 And cmbTipoSegnalazioneLivello0.Visible = True Then
                    livello0 = cmbTipoSegnalazioneLivello0.SelectedValue
                End If
                Dim livello1 As String = "NULL"
                If cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello1.Items.Count > 0 And cmbTipoSegnalazioneLivello1.Visible = True Then
                    livello1 = cmbTipoSegnalazioneLivello1.SelectedValue
                End If
                Dim livello2 As String = "NULL"
                If cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello2.Items.Count > 0 And cmbTipoSegnalazioneLivello2.Visible = True Then
                    livello2 = cmbTipoSegnalazioneLivello2.SelectedValue
                End If
                Dim livello3 As String = "NULL"
                If cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello3.Items.Count > 0 And cmbTipoSegnalazioneLivello3.Visible = True Then
                    livello3 = cmbTipoSegnalazioneLivello3.SelectedValue
                End If
                Dim livello4 As String = "NULL"
                If cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" And cmbTipoSegnalazioneLivello4.Items.Count > 0 And cmbTipoSegnalazioneLivello4.Visible = True Then
                    livello4 = cmbTipoSegnalazioneLivello4.SelectedValue
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
                'Dim dataContattatoFornitore As String = "NULL"
                'If IsDate(TextBoxContattatoFornitore.Text) AndAlso Len(TextBoxContattatoFornitore.Text) = 10 Then
                '    dataContattatoFornitore = "'" & par.AggiustaData(TextBoxContattatoFornitore.Text) & "'"
                'End If

                'Dim dataVerificaFornitore As String = "NULL"
                'If IsDate(TextBoxVerificaFornitore.Text) AndAlso Len(TextBoxVerificaFornitore.Text) = 10 Then
                '    dataVerificaFornitore = "'" & par.AggiustaData(TextBoxVerificaFornitore.Text) & "'"
                'End If
                Dim dataContattatoFornitore As String = "NULL"
                Dim oraContattatoFornitore As String = ""
                If IsDate(TextBoxContattatoFornitore.Text) AndAlso Len(TextBoxContattatoFornitore.Text) = 10 Then
                    dataContattatoFornitore = par.AggiustaData(TextBoxContattatoFornitore.Text)
                End If
                If dataContattatoFornitore = "NULL" And TextBoxOraContattatoFornitore.SelectedTime.ToString.Length > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "attenzione", "alert('Impossibile valorizzare il campo ore/minuti senza aver validato la data\nContattato Fornitore Emergenza');", True)
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
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "attenzione", "alert('Impossibile valorizzare il campo ore/minuti senza aver validato la data\Verifica Fornitore Emergenza');", True)
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
                    par.modalDialogMessage("Agenda e Segnalazioni", "Impossibile valorizzare il campo ore/minuti senza aver validato la data Sopralluogo", Me.Page, "info")
                    Exit Sub
                Else
                    oraSopralluogo = txtOraSopralluogo.SelectedTime.ToString
                End If
                If dataSopralluogo <> "NULL" And String.IsNullOrEmpty(oraSopralluogo) Then
                    par.modalDialogMessage("Agenda e Segnalazioni", "Valorizzare l\'ora di Sopralluogo!", Me.Page, "info")
                    Exit Sub
                End If
                Dim dataProgrIntervento As String = "NULL"
                Dim oraProgrIntervento As String = ""
                If IsDate(txtDataProgrammataIntervento.Text) AndAlso Len(txtDataProgrammataIntervento.Text) = 10 Then
                    dataProgrIntervento = par.AggiustaData(txtDataProgrammataIntervento.Text)
                End If
                If dataProgrIntervento = "NULL" And txtOraProgrammataIntervento.SelectedTime.ToString.Length > 0 Then
                    par.modalDialogMessage("Agenda e Segnalazioni", "Impossibile valorizzare il campo ore/minuti senza aver validato la data Programmata Intervento", Me.Page, "info")
                    Exit Sub
                Else
                    oraProgrIntervento = txtOraProgrammataIntervento.SelectedTime.ToString
                End If
                'SOLO LATO FORNITORE
                'Se cambio la tipologia di segnalazione allora devo NECESSARIAMENTE inserire una nota
                Dim scriviEventoCambioTipologiaSegnalazione As Boolean = False
                Dim flRichiestaModTipologia As String = ""
                Dim vecchiaCategoriaSegnalazione As String = RicavaTipologiaManutenzione()
                If Not IsNothing(Session.Item("MOD_FO_LIMITAZIONI")) AndAlso Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
                    If cmbTipologiaManutenzione.SelectedValue <> vecchiaCategoriaSegnalazione AndAlso String.IsNullOrEmpty(TextBoxNota.Text) Then
                        par.modalDialogMessage("Agenda e Segnalazioni", "Inserire una nota se si cambia la tipologia di segnalazione!", Me.Page, "info")
                        Exit Sub
                    Else
                        scriviEventoCambioTipologiaSegnalazione = True
                        flRichiestaModTipologia = ", FL_RICH_MOD_TIPOLOGIA = 1 "
                    End If
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
                'La data di sopralluogo impone l'inserimento di una nota PUBBLICA
                If dataSopralluogo <> "NULL" And txtNoteSopralluogo.Text.Length = 0 Then
                    par.modalDialogMessage("Agenda e Segnalazioni", "E\' necessario inserire una nota se si valida la data sopralluogo!", Me.Page, "info")
                    Exit Sub
                End If
                'La Data di effettivo intervento impone l'inserimento di una nota PUBBLICA
                If dataEffIntervento <> "NULL" And txtNoteEffIntervento.Text.Length = 0 Then
                    par.modalDialogMessage("Agenda e Segnalazioni", "E\' necessario inserire una nota se si valida la data effettiva intervento!", Me.Page, "info")
                    Exit Sub
                End If
                If dataEffIntervento <> "NULL" And txtNoteEffIntervento.Text.Length > 0 Then
                    par.cmd.CommandText = "SELECT ID_STATO FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
                    Dim idStato As String = par.cmd.ExecuteScalar
                    If idStato <> "6" Then
                        par.modalDialogMessage("Agenda e Segnalazioni", "Prendere prima in carico la segnalazione valorizzando l\'ora programmata intervento!", Me.Page, "info")
                        txtDataEffettivaIntervento.Text = ""
                        txtOraEffettivaIntervento.Clear()
                        Exit Sub
                    End If
                End If

                'Recupero delle date per inserimento in eventi
                Dim dataSopralluogoOdl, dataProgrammataOld, dataEffettivaOdl As String
                dataSopralluogoOdl = ""
                dataProgrammataOld = ""
                dataEffettivaOdl = ""
                Dim oraSopralluogoOdl, oraProgrammataOld, oraEffettivaOdl As String
                oraSopralluogoOdl = ""
                oraProgrammataOld = ""
                oraEffettivaOdl = ""
                par.cmd.CommandText = "SELECT DATA_SOPRALLUOGO, DATA_PROGRAMMATA_INT,DATA_EFFETTIVA_INT FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    dataSopralluogoOdl = par.FormattaData(Mid(par.IfNull(lettore("DATA_SOPRALLUOGO"), ""), 1, 8))
                    If Not String.IsNullOrEmpty(Mid(par.IfNull(lettore("DATA_SOPRALLUOGO"), ""), 9, 2)) Then
                        Dim ora As TimeSpan
                        ora = New TimeSpan(Mid(par.IfNull(lettore("DATA_SOPRALLUOGO"), ""), 9, 2), Mid(par.IfNull(lettore("DATA_SOPRALLUOGO"), ""), 11, 2), "00")
                        oraSopralluogoOdl = ora.ToString
                    End If
                    dataProgrammataOld = par.FormattaData(Mid(par.IfNull(lettore("DATA_PROGRAMMATA_INT"), ""), 1, 8))
                    If Not String.IsNullOrEmpty(Mid(par.IfNull(lettore("DATA_PROGRAMMATA_INT"), ""), 9, 2)) Then
                        Dim ora As TimeSpan
                        ora = New TimeSpan(Mid(par.IfNull(lettore("DATA_PROGRAMMATA_INT"), ""), 9, 2), Mid(par.IfNull(lettore("DATA_PROGRAMMATA_INT"), ""), 11, 2), "00")
                        oraProgrammataOld = ora.ToString
                    End If
                    dataEffettivaOdl = par.FormattaData(Mid(par.IfNull(lettore("DATA_EFFETTIVA_INT"), ""), 1, 8))
                    If Not String.IsNullOrEmpty(Mid(par.IfNull(lettore("DATA_EFFETTIVA_INT"), ""), 9, 2)) Then
                        Dim ora As TimeSpan
                        ora = New TimeSpan(Mid(par.IfNull(lettore("DATA_EFFETTIVA_INT"), ""), 9, 2), Mid(par.IfNull(lettore("DATA_EFFETTIVA_INT"), ""), 11, 2), "00")
                        oraEffettivaOdl = ora.ToString
                    End If
                End If
                lettore.Close()

                Dim idTipologiaManutenzione As String = "NULL"
                Dim aggiorna As Boolean = False
                Dim aggiornaDate As Boolean = False
                Dim dateIntervento As String = ""
                Dim fl_tipologia_confermata As String = ""
                '/////////////////////////
                '// 1218/2019
                Dim ripristinata As Boolean = False
                '/////////////////////////
                If cmbTipologiaManutenzione.Enabled = True Then
                    'Se la tipologia di segnalazione è abilitata allora mi trovo nella segnalazione a livello di FORNITORE
                    idTipologiaManutenzione = cmbTipologiaManutenzione.SelectedValue
                    fl_tipologia_confermata = ", FL_TIPOLOGIA_CONFERMATA = 0 "
                    aggiorna = True
                Else
                    'Se la tipologia di segnalazione non è abilitata mi trovo nella segnalazione a livello di TECNICO   
                    'PER CUI:
                    ' - Se "Conferma tipologia fornitore" CHECKED allora resta la tipologia del fornitore, 
                    ' ma deve essere cambiata necessariamente la categorizzazione della segnalazione
                    ' - Se "Ripristina tipologia iniziale" CHECKED allora ripristino la tipologia nata dalla combinazione tipologia
                    If cmbConfermaTipologiaFornitore.Visible = True Then
                        Select Case cmbConfermaTipologiaFornitore.SelectedValue
                            Case 1
                            'conferma tipologia fornitore
                            idTipologiaManutenzione = cmbTipologiaManutenzione.SelectedValue
                            fl_tipologia_confermata = ", FL_TIPOLOGIA_CONFERMATA = 1 "
                            aggiornaDate = True
                            dateIntervento = " data_sopralluogo = '', data_programmata_int2 = '', data_programmata_int = '', data_effettiva_int = '' "
                            'Verifico che la categorizzazione della segnalazione sia stata cambiata
                            par.cmd.CommandText = "SELECT COMBINAZIONE_TIPOLOGIE.id_tipo_segnalazione, " _
                                            & " nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1,-1) as id_tipo_segnalazione_livello_1, " _
                                            & " nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2,-1) as id_tipo_segnalazione_livello_2, " _
                                            & " nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3,-1) as id_tipo_segnalazione_livello_3, " _
                                            & " nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4,-1) as id_tipo_segnalazione_livello_4 " _
                                            & " FROM SISCOM_MI.combinazione_tipologie, SISCOM_MI.SEGNALAZIONI " _
                                            & " WHERE combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                                            & " AND SEGNALAZIONI.ID = " & idSegnalazione.Value
                            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                            For Each riga As Data.DataRow In dt.Rows
                                If riga.Item("id_tipo_segnalazione") = cmbTipoSegnalazioneLivello0.SelectedValue And
                                    riga.Item("id_tipo_segnalazione_livello_1") = par.IfEmpty(cmbTipoSegnalazioneLivello1.SelectedValue, -1) And
                                    riga.Item("id_tipo_segnalazione_livello_2") = par.IfEmpty(cmbTipoSegnalazioneLivello2.SelectedValue, -1) And
                                    riga.Item("id_tipo_segnalazione_livello_3") = par.IfEmpty(cmbTipoSegnalazioneLivello3.SelectedValue, -1) And
                                    riga.Item("id_tipo_segnalazione_livello_4") = par.IfEmpty(cmbTipoSegnalazioneLivello4.SelectedValue, -1) Then
                                    par.modalDialogMessage("Agenda e Segnalazioni", "E\' necessario cambiare la categorizzazione se si conferma la tipologia del fornitore!", Me.Page, "info")
                                    Exit Sub
                                End If
                            Next
                            aggiorna = True
                            Case 2
                            'RIPRISTINO TIPOLOGIA
                            'Verifico che la categorizzazione della segnalazione sia stata cambiata
                            par.cmd.CommandText = "SELECT COMBINAZIONE_TIPOLOGIE.id_tipo_segnalazione, " _
                                            & " nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1,-1) as id_tipo_segnalazione_livello_1, " _
                                            & " nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2,-1) as id_tipo_segnalazione_livello_2, " _
                                            & " nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3,-1) as id_tipo_segnalazione_livello_3, " _
                                            & " nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4,-1) as id_tipo_segnalazione_livello_4 " _
                                            & " FROM SISCOM_MI.combinazione_tipologie, SISCOM_MI.SEGNALAZIONI " _
                                            & " WHERE combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                                            & " AND SEGNALAZIONI.ID = " & idSegnalazione.Value
                            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                            For Each riga As Data.DataRow In dt.Rows
                                If riga.Item("id_tipo_segnalazione") <> cmbTipoSegnalazioneLivello0.SelectedValue Or
                                   riga.Item("id_tipo_segnalazione_livello_1") <> par.IfEmpty(cmbTipoSegnalazioneLivello1.SelectedValue, -1) Or
                                   riga.Item("id_tipo_segnalazione_livello_2") <> par.IfEmpty(cmbTipoSegnalazioneLivello2.SelectedValue, -1) Or
                                   riga.Item("id_tipo_segnalazione_livello_3") <> par.IfEmpty(cmbTipoSegnalazioneLivello3.SelectedValue, -1) Or
                                   riga.Item("id_tipo_segnalazione_livello_4") <> par.IfEmpty(cmbTipoSegnalazioneLivello4.SelectedValue, -1) Then
                                    par.modalDialogMessage("Agenda e Segnalazioni", "E\' necessario impostare la categoria iniziale se si ripristina la tipologia iniziale!", Me.Page, "info")
                                    Exit Sub
                                End If
                            Next
                            idTipologiaManutenzione = RicavaTipologiaManutenzione()
                            fl_tipologia_confermata = ", FL_TIPOLOGIA_CONFERMATA = 0 "
                            flRichiestaModTipologia = ", FL_RICH_MOD_TIPOLOGIA = 0 "
                            aggiorna = True
                                '1218/2019
                                ripristinata = True
                        End Select

                    End If
                End If
                Dim filtroCrono As String = ""
                Dim idProgrAttivita As String = ""
                par.cmd.CommandText = "SELECT ID_PROGRAMMA_ATTIVITA FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
                Dim lettoreCrono As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreCrono.Read Then
                    idProgrAttivita = par.IfNull(lettoreCrono("ID_PROGRAMMA_ATTIVITA"), "")
                End If
                lettoreCrono.Close()
                Dim dataProgrUltimoIntervento As String = "NULL"
                If String.IsNullOrEmpty(idProgrAttivita) Then

                    '*** Aggancio segnalazione - cronoprogramma ***'
                    dataProgrIntervento = "NULL"

                    Dim aggiornamentoCrono As String = ""
                    oraProgrIntervento = ""
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

                    filtroCrono = ",DATA_PROGRAMMATA_INT= " & dataProgrIntervento & par.AggiustaOra(oraProgrIntervento) _
                                & ", ID_PROGRAMMA_ATTIVITA = " & idProgrammaIntervento _
                                & ",DATA_PROGRAMMATA_INT2 = " & dataProgrUltimoIntervento


                    '*** Aggancio segnalazione - cronoprogramma ***'
                Else
                    If IsDate(txtDataProgrammataIntervento.Text) AndAlso Len(txtDataProgrammataIntervento.Text) = 10 Then
                        dataProgrIntervento = par.AggiustaData(txtDataProgrammataIntervento.Text)
                    End If
                    If dataProgrIntervento = "NULL" And txtOraProgrammataIntervento.SelectedTime.ToString.Length > 0 Then
                        par.modalDialogMessage("Agenda e Segnalazioni", "Impossibile valorizzare il campo ore/minuti senza aver validato la data Programmata Intervento", Me.Page, "info")
                        Exit Sub
                    Else
                        oraProgrIntervento = txtOraProgrammataIntervento.SelectedTime.ToString
                    End If
                    If IsDate(txtDataProgrammataUltimoIntervento.Text) AndAlso Len(txtDataProgrammataUltimoIntervento.Text) = 10 Then
                        dataProgrUltimoIntervento = par.AggiustaData(txtDataProgrammataUltimoIntervento.Text)
                    End If

                    filtroCrono = ",DATA_PROGRAMMATA_INT= " & dataProgrIntervento & par.AggiustaOra(oraProgrIntervento) _
                                & ",DATA_PROGRAMMATA_INT2 = " & dataProgrUltimoIntervento
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
                    & ",FL_DVCA= " & FL_DVCA _
                    & ",FL_AV= " & FL_AV _
                    & ",FL_FS= " & FL_FS _
                    & ",FL_CONTATTO_FORNITORE= " & FL_ContattatoFornitore _
                    & ",FL_VERIFICA_FORNITORE= " & FL_VerificaFornitore _
                    & ",DATA_CONTATTO_FORNITORE= " & dataContattatoFornitore & par.AggiustaOra(oraContattatoFornitore) _
                    & ",DATA_VERIFICA_FORNITORE= " & dataVerificaFornitore & par.AggiustaOra(oraVerificaFornitore) _
                    & ",ID_CANALE= " & DropDownListCanale.SelectedValue _
                    & ",DATA_SOPRALLUOGO= " & dataSopralluogo & par.AggiustaOra(oraSopralluogo) _
                    & ",DATA_EFFETTIVA_INT= " & dataEffIntervento & par.AggiustaOra(oraEffIntervento) _
                    & filtroCrono _
                    & fl_tipologia_confermata _
                    & flRichiestaModTipologia _
                    & " where id =" & idSegnalazione.Value
                par.cmd.ExecuteNonQuery()
                Dim tipoManutenzione As String = RicavaTipologiaManutenzione()
                If par.IfEmpty(tipoManutenzione, "0") <> "1" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET " _
                                            & " DATA_SOPRALLUOGO = '' " _
                                            & " ,DATA_PROGRAMMATA_INT = '' " _
                                            & " ,DATA_PROGRAMMATA_INT2 = '' " _
                                            & " ,DATA_EFFETTIVA_INT = '' " _
                                            & " ,ID_PROGRAMMA_ATTIVITA = '' " _
                                            & " WHERE ID = " & idSegnalazione.Value
                    par.cmd.ExecuteNonQuery()
                    'ElseIf idProgrammaIntervento = "NULL" Then
                    '    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET " _
                    '                        & " DATA_SOPRALLUOGO = '' " _
                    '                        & " ,DATA_PROGRAMMATA_INT = '' " _
                    '                        & " ,DATA_PROGRAMMATA_INT2 = '' " _
                    '                        & " ,DATA_EFFETTIVA_INT = '' " _
                    '                        & " ,ID_PROGRAMMA_ATTIVITA = '' " _
                    '                        & " WHERE ID = " & idSegnalazione.Value
                    '    par.cmd.ExecuteNonQuery()
                End If


                connData.chiudi(True)
                connData.apri(True)
                par.cmd.CommandText = "select fl_tipologia_confermata from siscom_mi.segnalazioni where id = " & idSegnalazione.Value
                Dim tipologia_confermata As Integer = par.cmd.ExecuteScalar
                'se la tipologia non è confermata posso cambiarla, se è confermata allora conta sempre la tipologia del fornitore
                If tipologia_confermata = 0 Then
                    If cmbConfermaTipologiaFornitore.Visible = False And cmbTipologiaManutenzione.Enabled = False Then
                        'Imposto la tipologia in base alla categorizzazione, ma solo se sono GESTORE
                        'Altrimenti vale la tipologia inserita dal fornitore
                        idTipologiaManutenzione = RicavaTipologiaManutenzione()
                        fl_tipologia_confermata = ", FL_TIPOLOGIA_CONFERMATA = 0 "
                        aggiorna = True
                    End If
                End If

                If aggiorna Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_TIPOLOGIA_MANUTENZIONE= " & par.RitornaNullSeMenoUno(idTipologiaManutenzione) _
                    & " where id =" & idSegnalazione.Value
                    par.cmd.ExecuteNonQuery()
                End If
                If aggiornaDate = True Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET " & dateIntervento & " WHERE ID = " & idSegnalazione.Value
                    par.cmd.ExecuteNonQuery()
                End If

                'Eventi
                If scriviEventoCambioTipologiaSegnalazione = True Then
                    Dim categoriaOld As String = ""
                    par.cmd.CommandText = "select upper(descrizione) from siscom_mi.TAB_TIPOLOGIA_MANUTENZIONE where id = " & vecchiaCategoriaSegnalazione
                    categoriaOld = par.cmd.ExecuteScalar
                    WriteEvent("F233", "Modifica tipologia segnalazione", categoriaOld, cmbTipologiaManutenzione.SelectedItem.Text)
                End If
                '//////////////////////////
                '// 1218/2019
                If ripristinata Then
                    'Dim categoriaOld As String = ""
                    'par.cmd.CommandText = "select upper(descrizione) from siscom_mi.TAB_TIPOLOGIA_MANUTENZIONE where id = " & vecchiaCategoriaSegnalazione
                    'categoriaOld = par.cmd.ExecuteScalar
                    WriteEvent("F233", "Richiesta proposta di ricategorizzazione non accettata", "", "")
                End If
                '//////////////////////////
                If txtDataSopralluogo.Text & " - " & oraSopralluogo <> dataSopralluogoOdl & " - " & oraSopralluogoOdl Then
                    WriteEvent("F233", "Modifica data e ora sopralluogo", dataSopralluogoOdl & " - " & oraSopralluogoOdl, txtDataSopralluogo.Text & " - " & oraSopralluogo)
                End If
                If txtDataProgrammataIntervento.Text & " - " & oraProgrIntervento <> dataProgrammataOld & " - " & oraProgrammataOld Then
                    WriteEvent("F233", "Modifica data e ora programma intervento", dataProgrammataOld & " - " & oraProgrammataOld, txtDataProgrammataIntervento.Text & " - " & oraProgrIntervento)
                End If
                If txtDataEffettivaIntervento.Text & " - " & oraEffIntervento <> dataEffettivaOdl & " - " & oraEffettivaOdl Then
                    WriteEvent("F233", "Modifica data e ora effettiva intervento", dataEffettivaOdl & " - " & oraEffettivaOdl, txtDataEffettivaIntervento.Text & " - " & oraEffIntervento)
                End If
                'Se Data e ora PROGRAMMATA intervento è valorizzato, la segnalazione andrà in stato IN CORSO
                If Not String.IsNullOrEmpty(txtDataProgrammataIntervento.Text) AndAlso Not String.IsNullOrEmpty(txtOraProgrammataIntervento.SelectedTime.ToString) Then
                    par.cmd.CommandText = "SELECT ID_STATO FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
                    Dim idStato As String = par.cmd.ExecuteScalar
                    If idStato = "0" Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_STATO = 6 WHERE ID = " & idSegnalazione.Value
                        par.cmd.ExecuteNonQuery()
                        WriteEvent("F233", "Modifica stato segnalazione", "APERTA", "IN CORSO")
                    End If
                End If
                'Se Data e ora EFFETTIVA intervento è valorizzato, la segnalazione andrà in stato CHIUSA
                If Not String.IsNullOrEmpty(txtDataEffettivaIntervento.Text) AndAlso Not String.IsNullOrEmpty(txtOraEffettivaIntervento.SelectedTime.ToString) Then
                    par.cmd.CommandText = "SELECT ID_STATO FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
                    Dim idStato As String = par.cmd.ExecuteScalar
                    If idStato = "6" Then
                        '////////////////////
                        '// 1433/2019 aggiunto ID_OPERATORE_CH
                        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_STATO = 10, data_chiusura = " & Format(Now, "yyyyMMddHHss") & " , ID_OPERATORE_CH = " & Session.Item("ID_OPERATORE") & " WHERE ID = " & idSegnalazione.Value
                        par.cmd.ExecuteNonQuery()
                        WriteEvent("F233", "Modifica stato segnalazione", "IN CORSO", "CHIUSA")
                    End If
                End If
                If txtNoteSopralluogo.Text <> "" AndAlso txtNoteSopralluogo.Text.ToUpper <> hiddenNotaSopralluogo.Value.ToUpper Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) " _
                       & "VALUES (" & idSegnalazione.Value & ", '(nota sopralluogo) " & par.PulisciStrSql(Me.txtNoteSopralluogo.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                    par.cmd.ExecuteNonQuery()
                End If
                If txtNoteEffIntervento.Text <> "" AndAlso txtNoteEffIntervento.Text.ToUpper <> hiddenNotaEffIntervento.Value.ToUpper Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) " _
                       & "VALUES (" & idSegnalazione.Value & ", '(nota data effettivo intervento) " & par.PulisciStrSql(Me.txtNoteEffIntervento.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                    par.cmd.ExecuteNonQuery()
                End If

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
                            If cmbUrgenza.SelectedIndex = 2 Then
                                cmbUrgenza.SelectedIndex = 3
                                par.cmd.CommandText = "update siscom_mi.segnalazioni " _
                                    & "set ID_PERICOLO_sEGNALAZIONE=" & 3 _
                                    & " where id =" & idSegnalazione.Value
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                                    & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                                    & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                                    & " VALUES (" & idSegnalazione.Value & ", " _
                                    & 2 & ", " _
                                    & 3 & ", " _
                                    & Session.Item("ID_OPERATORE") & "," _
                                    & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                    & "'" & par.PulisciStrSql("Cambio priorità per numero soglia note operatore raggiunto.") & "')"
                                par.cmd.ExecuteNonQuery()
                            End If
                        Case daGialloARosso
                            If cmbUrgenza.SelectedIndex = 3 And cmbUrgenzaIniz.SelectedIndex = 3 Then
                                cmbUrgenza.SelectedIndex = 4
                                par.cmd.CommandText = "update siscom_mi.segnalazioni " _
                                    & "set ID_PERICOLO_sEGNALAZIONE=" & 4 _
                                    & " where id =" & idSegnalazione.Value
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                                    & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                                    & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                                    & " VALUES (" & idSegnalazione.Value & ", " _
                                    & 3 & ", " _
                                    & 4 & ", " _
                                    & Session.Item("ID_OPERATORE") & "," _
                                    & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                    & "'" & par.PulisciStrSql("Cambio priorità per numero soglia note operatore raggiunto.") & "')"
                                par.cmd.ExecuteNonQuery()
                            End If
                        Case daVerdeAGiallo + daGialloARosso
                            If cmbUrgenza.SelectedIndex = 3 And cmbUrgenzaIniz.SelectedIndex < 3 Then
                                cmbUrgenza.SelectedIndex = 4
                                par.cmd.CommandText = "update siscom_mi.segnalazioni " _
                                    & "set ID_PERICOLO_sEGNALAZIONE=" & 4 _
                                    & " where id =" & idSegnalazione.Value
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                                    & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                                    & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                                    & " VALUES (" & idSegnalazione.Value & ", " _
                                    & 3 & ", " _
                                    & 4 & ", " _
                                    & Session.Item("ID_OPERATORE") & "," _
                                    & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                    & "'" & par.PulisciStrSql("Cambio priorità per numero soglia note operatore raggiunto.") & "')"
                                par.cmd.ExecuteNonQuery()
                            End If
                    End Select

                    TextBoxNota.Text = ""
                    WriteEvent("F233", "Inserimento nuova nota")
                End If
                StatoSegnalazione()
                CaricaTabellaNote(idSegnalazione.Value)
                CaricaElencoAllegati()
                Dim salvaParticolare As Boolean = False
                If pericoloSegnalazioneOld.Value = "" Then
                    pericoloSegnalazioneOld.Value = "- - -"
                End If
                'If cmbTipoSegnalazioneLivello1.SelectedValue = "1" Then
                If cmbUrgenza.SelectedItem.Text <> pericoloSegnalazioneOld.Value Then
                    WriteEvent("F233", "Modifica criticità", LTrim(pericoloSegnalazioneOld.Value), LTrim(cmbUrgenza.SelectedItem.Text))
                    salvaParticolare = True
                End If
                'Else
                'If pericoloSegnalazioneOld.Value <> "- - -" Then
                '    WriteEvent("F233", "Modifica criticità", pericoloSegnalazioneOld.Value, "- - -")
                '    salvaParticolare = True
                'End If
                'End If

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
                    frmModify.Value = "0"
                    par.modalDialogMessage("Aggiornamento segnalazione", "Operazione effettuata correttamente.", Me.Page, "successo", "Segnalazione.aspx?NM=1&IDS=" & idSegnalazione.Value, )
                Else
                    frmModify.Value = "0"
                    par.modalDialogMessage("Aggiornamento segnalazione", "Operazione effettuata correttamente.", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idSegnalazione.Value, )
                End If
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - btnSalva_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
    End Sub
    Protected Sub btnStampSopr_Click(sender As Object, e As System.EventArgs) Handles btnStampSopr.Click
        Try
            connData.apri(False)
            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\SopralluogoCallC.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim richiesta As String = ""
            Dim note As String = ""
            Dim condominio As String = ""
            Dim gestAuto As String = ""
            Dim sfratto As String = ""
            Dim morosità As String = ""
            Dim sloggio As String = ""
            Dim idContratto As String = ""
            par.cmd.CommandText = "select segnalazioni.*, " _
                & " (select descrizione from SISCOM_MI.TIPO_SEGNALAZIONE where tipo_segnalazione.id=id_tipo_segnalazione) as tipo1, " _
                & " (select descrizione from SISCOM_MI.TIPO_SEGNALAZIONE_livello_2 where tipo_segnalazione_livello_2.id=id_tipo_segn_livello_2) as tipo2, " _
                & " (select descrizione from SISCOM_MI.TIPO_SEGNALAZIONE_livello_3 where tipo_segnalazione_livello_3.id=id_tipo_segn_livello_3) as tipo3, " _
                & " (select descrizione from SISCOM_MI.TIPO_SEGNALAZIONE_livello_4 where tipo_segnalazione_livello_4.id=id_tipo_segn_livello_4) as tipo4 " _
                & " from siscom_mi.segnalazioni where id = " & idSegnalazione.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                contenuto = Replace(contenuto, "$nrichiesta$", par.IfNull(lettore("ID"), ""))
                contenuto = Replace(contenuto, "$datarichiesta$", par.FormattaData(Mid(par.IfNull(lettore("DATA_ORA_RICHIESTA"), "                 "), 1, 8)))
                contenuto = Replace(contenuto, "$descrizione$", par.IfNull(lettore("descrizione_ric"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(lettore("COGNOME_RS"), "") & " " & par.IfNull(lettore("NOME"), ""))
                contenuto = Replace(contenuto, "$numerotelefono1$", par.IfNull(lettore("TELEFONO1"), ""))
                contenuto = Replace(contenuto, "$numerotelefono2$", par.IfNull(lettore("TELEFONO2"), ""))
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI_SOPRALLUOGO WHERE ID_SEGNALAZIONE = " & idSegnalazione.Value
                Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myreader.Read Then
                    contenuto = Replace(contenuto, "$rapporto$", par.IfNull(myreader("rapporto"), ""))
                    contenuto = Replace(contenuto, "$tecnico$", par.IfNull(myreader("tecnico"), ""))
                    If myreader("fl_pericolo") = 1 Then
                        contenuto = Replace(contenuto, "$pericolo$", "SI")
                    ElseIf myreader("fl_pericolo") = 0 Then
                        contenuto = Replace(contenuto, "$pericolo$", "NO")
                    Else
                        contenuto = Replace(contenuto, "$pericolo$", "")
                    End If
                Else
                    contenuto = Replace(contenuto, "$pericolo$", "")
                    contenuto = Replace(contenuto, "$rapporto$", "&nbsp; ")
                    contenuto = Replace(contenuto, "$tecnico$", "")
                End If
                myreader.Close()
                If par.IfNull(lettore("TIPO_RICHIESTA"), "-1") = "1" Then
                    richiesta = "SEGNALAZIONE GUASTI"
                    par.cmd.CommandText = "select descrizione from siscom_mi.tipologie_guasti where id = " & par.IfNull(lettore("id_tipologie"), "0")
                    myreader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        richiesta = richiesta & " - " & par.IfNull(myreader("descrizione"), "")
                    End If
                    myreader.Close()
                ElseIf par.IfNull(lettore("TIPO_RICHIESTA"), "-1") = "0" Then
                    richiesta = "RICHIESTA INFORMAZIONI"
                End If
                Dim tipologie As String = ""
                If par.IfNull(lettore("tipo1"), "") <> "" Then
                    tipologie &= par.IfNull(lettore("tipo1"), "")
                End If
                If par.IfNull(lettore("tipo2"), "") <> "" Then
                    tipologie &= " - " & par.IfNull(lettore("tipo2"), "")
                End If
                If par.IfNull(lettore("tipo3"), "") <> "" Then
                    tipologie &= " - " & par.IfNull(lettore("tipo3"), "")
                End If
                If par.IfNull(lettore("tipo4"), "") <> "" Then
                    tipologie &= " - " & par.IfNull(lettore("tipo4"), "")
                End If
                contenuto = Replace(contenuto, "$tiporichiesta$", tipologie)
                'contenuto = Replace(contenuto, "$tiporichiesta$", richiesta)
                par.cmd.CommandText = "SELECT NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = " & idSegnalazione.Value
                myreader = par.cmd.ExecuteReader
                While myreader.Read
                    note = note & par.IfNull(myreader("note"), "") & "<br/>"
                End While
                myreader.Close()
                contenuto = Replace(contenuto, "$note$", note)
                Dim indirizzo As String = ""
                par.cmd.CommandText = "SELECT COD_EDIFICIO,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID = " & par.IfNull(lettore("id_edificio"), "0")
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    indirizzo = par.IfNull(myreader("denominazione"), "")
                    contenuto = Replace(contenuto, "$edificio$", "EDIFICIO COD." & par.IfNull(myreader("cod_edificio"), ""))
                End If
                myreader.Close()
                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.cod_unita_immobiliare, UNITA_IMMOBILIARI.interno,TIPO_LIVELLO_PIANO.descrizione AS PIANO,SCALE_EDIFICI.descrizione AS SCALA, " _
                                    & "siscom_mi.Getintestatari(id_contratto) AS intestatario " _
                                    & "FROM siscom_mi.UNITA_IMMOBILIARI,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO, siscom_mi.UNITA_CONTRATTUALE " _
                                    & "WHERE UNITA_IMMOBILIARI.id_scala = SCALE_EDIFICI.ID (+) " _
                                    & "AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) " _
                                    & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.id_unita(+)" _
                                    & "AND UNITA_IMMOBILIARI.ID = " & par.IfNull(lettore("id_unita"), 0)
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    indirizzo = indirizzo & " " & "SCALA: " & par.IfNull(myreader("SCALA"), "--") & " PIANO: " & par.IfNull(myreader("PIANO"), "--") & " INTERNO:" & par.IfNull(myreader("interno"), "--")
                    contenuto = Replace(contenuto, "$unita$", "U.I. cod." & par.IfNull(myreader("COD_UNITA_IMMOBILIARE"), ""))
                Else
                    contenuto = Replace(contenuto, "$unita$", "")
                End If
                myreader.Close()
                contenuto = Replace(contenuto, "$indirizzo$", indirizzo)
                par.cmd.CommandText = "Select nome from siscom_mi.tab_filiali Where id = " & par.IfNull(lettore("id_struttura"), "")
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    contenuto = Replace(contenuto, "$struttura$", "STRUTTURA: " & par.IfNull(myreader("nome"), ""))
                End If
                myreader.Close()
                par.cmd.CommandText = "SELECT ID_CONTRATTO, RAPPORTI_UTENZA.COD_CONTRATTO, rapporti_utenza.data_riconsegna, siscom_mi.Getintestatari (id_contratto) AS intestatario, " _
                                    & "SISCOM_MI.Getstatocontratto(ID_CONTRATTO) AS STATO,TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS TIPO_CONTRATTO " _
                                    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE ,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                                    & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = COD_TIPOLOGIA_CONTR_LOC " _
                                    & "AND ID_UNITA =" & par.IfNull(lettore("id_unita"), 0) _
                                    & " AND NVL(DATA_RICONSEGNA,'50000000')=(" _
                                    & "SELECT MAX(NVL(DATA_RICONSEGNA,'50000000')) " _
                                    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE ,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                                    & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = COD_TIPOLOGIA_CONTR_LOC " _
                                    & "AND ID_UNITA =" & par.IfNull(lettore("id_unita"), 0) _
                                    & ")"
                myreader = par.cmd.ExecuteReader
                Dim datiCont As String = ""
                If myreader.Read Then
                    idContratto = par.IfNull(myreader("ID_CONTRATTO"), "")
                    datiCont = "CONTRATTO: " & par.IfNull(myreader("tipo_contratto"), "") & " " & par.IfNull(myreader("cod_contratto"), "") & " Stato Contratto: " & par.IfNull(myreader("stato"), "") & " Saldo: " & Format(par.CalcolaSaldoAttuale(par.IfNull(myreader("ID_CONTRATTO"), "0")), "##,##0.00") & " Euro "
                    contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myreader("intestatario"), ""))
                    If par.IfNull(myreader("data_riconsegna"), "") <> "" Then
                        sloggio = "SLOGGIO: " & par.FormattaData(par.IfNull(myreader("data_riconsegna"), ""))
                    End If
                Else
                    contenuto = Replace(contenuto, "$intestatario$", "")
                End If
                contenuto = Replace(contenuto, "$contratto$", datiCont)
                par.cmd.CommandText = "SELECT condomini.denominazione, (cond_amministratori.cognome || ' ' ||cond_amministratori.nome) AS amministratore " _
                                & "FROM siscom_mi.condomini,siscom_mi.cond_amministratori,siscom_mi.cond_amministrazione " _
                                & "WHERE condomini.ID =cond_amministrazione.id_condominio AND cond_amministratori.ID = id_amministratore AND cond_amministrazione.data_fine IS NULL " _
                                & "AND condomini.ID IN (SELECT id_condominio FROM siscom_mi.cond_edifici WHERE id_edificio = " & par.IfNull(lettore("id_edificio"), 0) & ")"

                myreader = par.cmd.ExecuteReader
                While myreader.Read
                    condominio = condominio & "CONDOMINIO: " & par.IfNull(myreader("denominazione"), "") & " AMMINISTRATORE: " & par.IfNull(myreader("amministratore"), "")
                End While
                myreader.Close()
                contenuto = Replace(contenuto, "$condomino$", condominio)
                contenuto = Replace(contenuto, "$gestauto$", gestAuto)
                contenuto = Replace(contenuto, "$morosità$", morosità)
                contenuto = Replace(contenuto, "$sfratto$", sfratto)
                contenuto = Replace(contenuto, "$sloggio$", sloggio)
                If idContratto <> "" Then
                    par.cmd.CommandText = "SELECT ID_MOROSITA ,(CASE WHEN COD_STATO = 'M20' THEN 'SI' ELSE 'NO' END)AS PRATICA_LEGALE FROM SISCOM_MI.MOROSITA_LETTERE where  cod_stato not in ('M94','M98','M100') and id_contratto =" & idContratto
                    myreader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        morosità = "MESSA IN MORA "
                        If par.IfNull(myreader("pratica_legale"), "NO") = "SI" Then
                            morosità = morosità & "- AVVIATA PRATICA LEGALE"
                        End If
                    End If
                    myreader.Close()
                End If
            End If
            lettore.Close()
            connData.chiudi(False)
            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True
            'sostituire nuovo codice da qui
            Dim nomefile As String = idSegnalazione.Value & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\IMG\"))
            Dim scriptblock As String = "<script language='javascript' type='text/javascript'>window.open('../FileTemp/" & nomefile & ".pdf','','');</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "aperturaF", scriptblock)
            'Response.Write("<script>window.open('../FileTemp/" & nomefile & ".pdf','','');</script>")
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - btnStampSopr_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub imgEsci_Click(sender As Object, e As System.EventArgs) Handles imgEsci.Click
        If frmModify.Value <> "111" Then
            If Not IsNothing(Request.QueryString("SL")) AndAlso IsNumeric(Request.QueryString("SL")) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
            ElseIf Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
            Else
                Response.Redirect("Home.aspx", False)
            End If
        Else
            frmModify.Value = "1"
        End If

    End Sub
    Protected Sub cmbTipoSegnalazioneLivello1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello1.SelectedIndexChanged
        CaricaTipologieLivello2()
        CaricaListaDocumentiDaPortare()
        'caricaUrgenzaPredefinita()
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello2.SelectedIndexChanged
        CaricaTipologieLivello3()
        CaricaListaDocumentiDaPortare()
        'caricaUrgenzaPredefinita()
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello3.SelectedIndexChanged
        CaricaTipologieLivello4()
        CaricaListaDocumentiDaPortare()
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
                par.modalDialogMessage("Sollecito", "Sollecito effettuato correttamente.", Me.Page, "successo", "Segnalazione.aspx?NM=1&IDS=" & idSegnalazione.Value, )
            Else
                par.modalDialogMessage("Sollecito", "Sollecito effettuato correttamente.", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idSegnalazione.Value, )
            End If
            VisualizzaPaginaPrincipale()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - btnSollecito_Click - " & ex.Message)
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
    Private Sub VisualizzaPaginaAppuntamenti()
        MultiView1.ActiveViewIndex = 4
        MultiView2.ActiveViewIndex = 4
        MultiView3.ActiveViewIndex = 4
    End Sub
    Private Sub VisualizzaPaginaPadreFiglio()
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
        If confermaChiusura.Value = "1" Then
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
                    '///////////////////////////////////////////////
                    '// 1433/2019 valorizzato operatore chiusura ID_OPERATORE_CH
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET id_stato=10,DATA_CHIUSURA = '" & par.AggiustaData(Me.txtDataCInt.Text) & Me.txtOraCInt.Text.Replace(":", "").Replace(".", "") & "', ID_OPERATORE_CH = " & Session.Item("ID_OPERATORE") _
                        & " where (id=" & idSegnalazione.Value & " or id in(SELECT ID FROM SISCOM_mi.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idSegnalazione.Value & ")) and id_Stato<>10"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,id_tipo_segnalazione_note) " _
                        & "VALUES (" & idSegnalazione.Value & ", '" & par.PulisciStrSql(Me.txtDescNoteChiusura.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ",2)"
                    par.cmd.ExecuteNonQuery()
                    WriteEvent("F243", "", , , idSegnalazione.Value)

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
                            par.modalDialogMessage("Chiusura segnalazione", "Chiusura completata correttamente.", Me.Page, "successo", "Segnalazione.aspx?NM=1&IDS=" & idAmministrativa)
                        Else
                            par.modalDialogMessage("Chiusura segnalazione", "Chiusura completata correttamente.", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idAmministrativa)
                        End If
                    Else
                        If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                            par.modalDialogMessage("Chiusura segnalazione", "Chiusura completata correttamente.", Me.Page, "successo", "Segnalazione.aspx?NM=1&IDS=" & idSegnalazione.Value)
                        Else
                            par.modalDialogMessage("Chiusura segnalazione", "Chiusura completata correttamente.", Me.Page, "successo", "Segnalazione.aspx?IDS=" & idSegnalazione.Value)
                        End If
                    End If
                Else
                    par.modalDialogMessage("Sollecito", "Attenzione...\nLa data e l\'ora di chiusura devono essere successive a quelle di apertura!\nChiusura non completata.", Me.Page, "info", , )
                End If
                VisualizzaPaginaPrincipale()
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - imgChiudiSegnalazione1_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
    End Sub
    Protected Sub btnChiudiSegnalazione_Click(sender As Object, e As System.EventArgs) Handles btnChiudiSegnalazione.Click
        If confermaChiusura.Value = "1" Then
            If cmbTipoSegnalazioneLivello0.SelectedValue = "2" Then
                'callback
                par.modalDialogConfirm("Agenda e Segnalazioni", "Dopo la chiusura del ticket CALLBACK\nvuoi creare un ticket AMMINISTRATIVO?", "Sì", "document.getElementById('confermaTicketAmministrativo').value='1';document.getElementById('imgChiudiSegnalazione1').click();", "No", "document.getElementById('confermaTicketAmministrativo').value='0';document.getElementById('imgChiudiSegnalazione1').click();", Page)
            Else
                confermaTicketAmministrativo.Value = "0"
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "scr1", "document.getElementById('imgChiudiSegnalazione1').click();", True)
            End If
        End If
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
                        par.modalDialogMessage("Inserimento allegato", "Esiste già un allegato con lo stesso nome!\nImpossibile allegare il file scelto.", Me.Page, "info", , )
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
                    par.modalDialogMessage("Inserimento allegato", "Operazione completata correttamente.", Me.Page, "successo", , )
                    WriteEvent("F233", "Inserimento allegato")
                    CaricaTabellaNote(idSegnalazione.Value)
                    CaricaElencoAllegati()
                    connData.chiudi(True)
                Else
                    par.modalDialogMessage("Inserimento allegato", "Inserire il nome.", Me.Page, "info", , )
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - btnAllegaFile_Click - " & ex.Message)
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
            If flCustode.Value = "1" Then

                query = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id=1  ORDER BY ID"
            Else
                query = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id NOT IN (6,7) ORDER BY ID"
            End If
            'If flCondominio.Value = "1" Then
            '    query = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id=3  ORDER BY ID"
            'End If
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connOpenNow = True
            End If
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello0, "ID", "DESCRIZIONE", True)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - CaricaTipologieLivello1 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello1()
        Try
            Dim connOpenNow As Boolean = False
            Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE=" & cmbTipoSegnalazioneLivello0.SelectedValue & " ORDER BY DESCRIZIONE"
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connOpenNow = True
            End If
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello1, "ID", "DESCRIZIONE", True)
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - CaricaTipologieLivello2 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello2()
        Try
            Dim connOpenNow As Boolean = False
            Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE_LIVELLO_1,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue & " ORDER BY DESCRIZIONE"
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connOpenNow = True
            End If
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello2, "ID", "DESCRIZIONE", True)
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - CaricaTipologieLivello3 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello3()
        Try
            Dim connOpenNow As Boolean = False
            Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE_LIVELLO_2,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue & " ORDER BY DESCRIZIONE"
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connOpenNow = True
            End If
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello3, "ID", "DESCRIZIONE", True)
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - CaricaTipologieLivello3 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello4()
        Try
            Dim connOpenNow As Boolean = False
            Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE_LIVELLO_3,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue & " ORDER BY DESCRIZIONE"
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connOpenNow = True
            End If
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello4, "ID", "DESCRIZIONE", True)
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - CaricaTipologieLivello4 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function CheckControl() As Boolean
        CheckControl = True
        Try
            If String.IsNullOrEmpty(Me.txtDescrizione.Text) Then
                CheckControl = False
                par.modalDialogMessage("Segnalazione", "Inserire la descrizione della richiesta", Me.Page, "info", , )
            End If
            If cmbTipoSegnalazioneLivello0.SelectedValue = "-1" Then
                CheckControl = False
                par.modalDialogMessage("Segnalazione", "Inserire la tipologia della segnalazione", Me.Page, "info", , )
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - CheckControl - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        Return CheckControl
    End Function
    Protected Sub btnAppuntamento_Click(sender As Object, e As System.EventArgs) Handles btnAppuntamento.Click
        VisualizzaPaginaAppuntamenti()
    End Sub
    Protected Sub btnIndietro4_Click(sender As Object, e As System.EventArgs) Handles btnIndietro4.Click
        VisualizzaPaginaPrincipale()
    End Sub
    Protected Sub btnAggiungiAppuntamento_Click(sender As Object, e As System.EventArgs) Handles btnAggiungiAppuntamento.Click
        Response.Redirect("AgendaGestioneContatti.aspx?PROV=SEG&IDS=" & idSegnalazione.Value & "&IDF=" & DropDownListSedeTerritoriale.SelectedValue, False)
    End Sub

    Protected Sub DataGridElencoAppuntamenti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridElencoAppuntamenti.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='yellow';};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
                e.Item.Attributes.Add("onclick", "if (document.getElementById('daElimina').value==0){if(" & e.Item.Cells(par.IndDGC(DataGridElencoAppuntamenti, "FL_ELIMINA")).Text & "==0){if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                    & "document.getElementById('idSelected').value='" & e.Item.Cells(par.IndDGC(DataGridElencoAppuntamenti, "id")).Text & "';" _
                                    & "document.getElementById('idSelectedData').value='" & e.Item.Cells(par.IndDGC(DataGridElencoAppuntamenti, "DATA_APP")).Text & "';" _
                                    & "document.getElementById('idSelectedFiliale').value='" & e.Item.Cells(par.IndDGC(DataGridElencoAppuntamenti, "ID_FILIALE")).Text & "';" _
                                    & "}}")
                e.Item.Attributes.Add("onDblclick", "document.getElementById('CPFooter_btnModifica').click();")

                If e.Item.Cells(par.IndDGC(DataGridElencoAppuntamenti, "FL_ELIMINA")).Text = "0" Then
                    If Session.Item("MOD_SEGNALAZIONI_SL") <> "1" Then
                        e.Item.Cells(par.IndDGC(DataGridElencoAppuntamenti, "ELIMINA")).Text = "<img src=""Immagini/Elimina.png"" alt=""elimina"" onclick=""javascript:EliminaAppuntamento(" & e.Item.Cells(par.IndDGC(DataGridElencoAppuntamenti, "id")).Text & ");"""
                    Else
                        e.Item.Cells(par.IndDGC(DataGridElencoAppuntamenti, "ELIMINA")).Text = ""
                    End If
                End If
                If e.Item.Cells(par.IndDGC(DataGridElencoAppuntamenti, "FL_ELIMINA")).Text = "1" Then
                    e.Item.ForeColor = Drawing.Color.Gray
                End If
                If e.Item.Cells(par.IndDGC(DataGridElencoAppuntamenti, "id")).Text.Replace("&nbsp;", "") = "" Then
                    e.Item.BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD")
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - DataGridElencoAppuntamenti_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click
        Response.Redirect("DettagliAppuntamentiGestioneContatti.aspx?PROV=MOD&DATA=" & idSelectedData.Value & "&FILIALE=" & idSelectedFiliale.Value & "&IDS=" & idSegnalazione.Value, False)
    End Sub
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - btnElimina_Click - " & ex.Message)
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
            For Each CTRL In View14.Controls
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
            For Each CTRL In View15.Controls
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
            For Each elemento As DataGridItem In DataGridElencoAppuntamenti.Items
                elemento.Cells(par.IndDGC(DataGridElencoAppuntamenti, "ELIMINA")).Text = ""
            Next
            btnAppuntamento.Enabled = True
            imgEsci.Enabled = True
            btnStampa.Enabled = True
            btnStampSopr.Enabled = True
            btnEventi.Enabled = True
            btnIndietro.Enabled = True
            btnIndietro2.Enabled = True
            btnIndietro3.Enabled = True
            btnIndietro4.Enabled = True
            cmbUrgenza.Enabled = False
            cmbUrgenzaIniz.Enabled = False
            txtDescrizione.ReadOnly = True
            TextBoxNota.ReadOnly = True
            CType(Me.Master.FindControl("NavigationMenu"), System.Web.UI.WebControls.Menu).Visible = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub NoMenu()
        Try
            CType(Me.Master.FindControl("NavigationMenu"), System.Web.UI.WebControls.Menu).Visible = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - NoMenu - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello0_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello0.SelectedIndexChanged
        CaricaTipologieLivello1()
        If cmbTipoSegnalazioneLivello0.SelectedValue = "1" Or cmbTipoSegnalazioneLivello0.SelectedValue = "6" Then
            PanelUrgenzaCriticita.Visible = True
        Else
            PanelUrgenzaCriticita.Visible = False
        End If
        If cmbTipoSegnalazioneLivello0.SelectedValue = "0" Then
            btnAggiungiAppuntamento.Enabled = True
        Else
            btnAggiungiAppuntamento.Enabled = False
        End If
        'caricaUrgenzaPredefinita()
        CaricaListaDocumentiDaPortare()
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello4.SelectedIndexChanged
        CaricaListaDocumentiDaPortare()
        'caricaUrgenzaPredefinita()
    End Sub

    Protected Sub DropDownListTipologia_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListTipologia.SelectedIndexChanged
        CaricaListaDocumentiDaPortare()
        caricaUrgenzaPredefinita()

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
                        PanelUrgenzaCriticita.Visible = True
                    Else
                        PanelUrgenzaCriticita.Visible = False
                    End If
                Else
                    PanelUrgenzaCriticita.Visible = False
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
                        PanelUrgenzaCriticita.Visible = True
                    Else
                        PanelUrgenzaCriticita.Visible = False
                    End If
                Else
                    PanelUrgenzaCriticita.Visible = False
                End If
            End If
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - caricaUrgenzaPredefinita - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaTutteTipologie(Optional ByVal edificioIncondominio As Boolean = 0, Optional ByVal tipologiaManutenzione As Integer = -1)
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
            Dim condizioneTipologiaManutenzione As String = ""
            If tipologiaManutenzione <> -1 Then
                condizioneTipologiaManutenzione = " AND ID_TIPO_MANUTENZIONE = " & tipologiaManutenzione
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
                & condizioneTipologiaManutenzione _
                & " ORDER BY 2 ASC "
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)

            End If
            par.caricaComboTelerik(query, DropDownListTipologia, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - caricaTutteTipologie - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub ControlloCustode()
        Try
            If Not IsNothing(Request.QueryString("IDS")) Then
                idSegnalazione.Value = Request.QueryString("IDS")
                If idSegnalazione.Value <> "-1" Then
                    connData.apri()
                    par.cmd.CommandText = "SELECT FL_CUSTODE FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
                    flCustode.Value = par.IfNull(par.cmd.ExecuteScalar, "0")
                    connData.chiudi()
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ControlloCustode - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub presaInCarico_Click(sender As Object, e As System.EventArgs) Handles presaInCarico.Click
        Try
            If CType(Me.Master.FindControl("operatoreFiliale"), HiddenField).Value = "1" Then
                connData.apri(True)
                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_STATO=1 WHERE ID=" & idSegnalazione.Value & " AND ID_STATO=0"
                Dim ris As Integer = par.cmd.ExecuteNonQuery
                If ris = 1 Then
                    connData.chiudi(True)
                Else
                    connData.chiudi(False)
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - presaInCarico_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnRelazionePadreFiglio_Click(sender As Object, e As System.EventArgs) Handles btnRelazionePadreFiglio.Click
        VisualizzaPaginaPadreFiglio()
    End Sub

    Protected Sub btnIndietro5_Click(sender As Object, e As System.EventArgs) Handles btnIndietro5.Click
        VisualizzaPaginaPrincipale()
    End Sub

    Private Sub VisualizzaAllegatiPresenti()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.ALLEGATI_WS WHERE ID_OGGETTO = " & idSegnalazione.Value & " AND STATO = 0"
            Dim numero As Integer = CInt(par.cmd.ExecuteScalar)
            If numero > 0 Then
                lblAllegatiPresenti.Text = "Sì"
            Else
                lblAllegatiPresenti.Text = "No"
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - VisualizzaAllegatiPresenti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - caricaSegnalazioniPadreFigli - " & ex.Message)
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
            par.modalDialogMessage("Agenda e Segnalazioni", "Selezionare almeno una segnalazione", Me.Page, "info")
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
                    par.modalDialogMessage("Agenda e Segnalazioni", "Relazione eliminata correttamente.", Me.Page, "successo")
                End If
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ButtonEliminaPadre_Click - " & ex.Message)
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
            par.modalDialogMessage("Agenda e Segnalazioni", "Selezionare almeno una segnalazione", Me.Page, "info")
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
                    par.modalDialogMessage("Agenda e Segnalazioni", "Relazione eliminata correttamente.", Me.Page, "successo")
                End If
            Catch ex As Exception
                connData.chiudi(False)
                Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ButtonEliminaFiglie_Click - " & ex.Message)
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
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.CANALE WHERE FL_AGENDA = 1 ORDER BY ID ASC", DropDownListCanale, "ID", "DESCRIZIONE", False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - caricaCanale - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaTipologiaSegnalante()
        Try
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_TIPOLOGIA_SEGNALANTE ORDER BY DESCRIZIONE ASC", cmbTipologiaSegnalante, "ID", "DESCRIZIONE", False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaTipologiaSegnalante - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaTipologiaManutenzione()
        Try
            If Not IsNothing(Session.Item("MOD_FO_LIMITAZIONI")) AndAlso Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_TIPOLOGIA_MANUTENZIONE ORDER BY DESCRIZIONE ASC", cmbTipologiaManutenzione, "ID", "DESCRIZIONE", False)
            Else
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_TIPOLOGIA_MANUTENZIONE ORDER BY DESCRIZIONE ASC", cmbTipologiaManutenzione, "ID", "DESCRIZIONE", True)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaTipologiaSegnalazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
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

            If par.IfEmpty(DropDownListEdificio.SelectedValue, "-1") <> "-1" Then
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

            If connAperta = True Then
                connData.chiudi(False)
            End If
            If lblInCondominioSiNo.Text = "Sì" Then
                flCondominio.Value = "1"
                'cmbTipoSegnalazioneLivello0.Enabled = False
                'caricaTutteTipologie(1)


            Else
                flCondominio.Value = "0"
                'cmbTipoSegnalazioneLivello0.ClearSelection()
                'cmbTipoSegnalazioneLivello0.Enabled = True
                'caricaTutteTipologie()


            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - controllaCondominio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CheckBoxContattatoFornitore_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxContattatoFornitore.CheckedChanged
        If CheckBoxContattatoFornitore.Checked Then
            TextBoxContattatoFornitore.Enabled = True
            TextBoxOraContattatoFornitore.Enabled = True
        Else
            TextBoxContattatoFornitore.Enabled = False
            TextBoxOraContattatoFornitore.Enabled = False
            TextBoxContattatoFornitore.Text = ""
            TextBoxOraContattatoFornitore.SelectedTime = Nothing
        End If
    End Sub

    Private Sub CheckBoxVerificaFornitore_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxVerificaFornitore.CheckedChanged
        If CheckBoxVerificaFornitore.Checked Then
            TextBoxVerificaFornitore.Enabled = True
            TextBoxOraVerificaFornitore.Enabled = True
        Else
            TextBoxVerificaFornitore.Enabled = False
            TextBoxOraVerificaFornitore.Enabled = False
            TextBoxVerificaFornitore.Text = ""
            TextBoxOraVerificaFornitore.SelectedTime = Nothing
        End If
    End Sub

    Protected Sub btnAggiungiNotaGestionale_Click(sender As Object, e As System.EventArgs) Handles btnAggiungiNotaGestionale.Click
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE, SOLLECITO,ID_TIPO_SEGNALAZIONE_NOTE) VALUES (" & idSegnalazione.Value & ", '" & RadTextBox1.Text & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ", 0, 3)"
            par.cmd.ExecuteNonQuery()
            CaricaTabellaNote(idSegnalazione.Value)
            RadTextBox1.Text = ""
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
        End Try
    End Sub

    Private Sub abilitaDisabilitaFornitore()
        solaLettura()
        txtDataSopralluogo.Enabled = True
        txtDataProgrammataIntervento.Enabled = True
        txtDataEffettivaIntervento.Enabled = True
        txtOraSopralluogo.Enabled = True
        txtOraProgrammataIntervento.Enabled = True
        txtOraEffettivaIntervento.Enabled = True
        TextBoxNota.Enabled = True
        TextBoxNota.ReadOnly = False
        DropDownListTipologia.Enabled = False
        ' cmbTipologiaManutenzione.Enabled = False
        lblConfermaTipologiaFornitore.Visible = False
        cmbConfermaTipologiaFornitore.Visible = False
        imgAlert.Visible = False
        '///////////////////
        '// 1218/2019
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If
        ' Abilito il cambio tipologia per il fornitore solo se non ci sono già state modifiche di stato oppure se 
        ' l'ultimo a cambiarlo è stato lui e lo stato attuale è proprio quello impostato da lui
        par.cmd.CommandText = "select count(*) from siscom_mi.eventi_segnalazioni where motivazione like '%Modifica tipologia segnalazione%' and valore_old<>valore_new and id_segnalazione    = " & idSegnalazione.Value
        Dim conteggio As Long = par.IfNull(par.cmd.ExecuteScalar, 0)
        If conteggio = 0 Then
            'non ci sono state modifcche di stato allora abilito
            cmbTipologiaManutenzione.Enabled = True
        Else
            cmbTipologiaManutenzione.Enabled = False
            ' ultima modifica stato
            par.cmd.CommandText = "select * from siscom_mi.eventi_segnalazioni where motivazione like '%Modifica tipologia segnalazione%'  and valore_old<>valore_new and id_segnalazione = " & idSegnalazione.Value & " and cod_evento = 'F233' and data_ora in ( select max (data_ora) from siscom_mi.eventi_segnalazioni E where E.id_segnalazione = " & idSegnalazione.Value & " and cod_evento = 'F233' and E.valore_old<>E.valore_new and motivazione like '%Modifica tipologia segnalazione%' ) "
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore("ID_OPERATORE"), 0) = Session.Item("ID_OPERATORE") And cmbTipologiaManutenzione.Text = par.IfNull(lettore("VALORE_NEW"), "") Then
                    cmbTipologiaManutenzione.Enabled = True
                Else
                    cmbTipologiaManutenzione.Enabled = False
                End If
            End If
        End If
        If connAperta = True Then
            connData.chiudi(False)
        End If
        '////////////////////


        cmbTipoSegnalazioneLivello0.Enabled = False
        cmbTipoSegnalazioneLivello1.Enabled = False
        cmbTipoSegnalazioneLivello2.Enabled = False
        cmbTipoSegnalazioneLivello3.Enabled = False
        cmbTipoSegnalazioneLivello4.Enabled = False
        DropDownListCanale.Enabled = False
        lblOggetto.Visible = False
        lblContratto.Visible = False
        btnAppuntamento.Visible = False
        btnSalva.Enabled = True
        btnNotaGestionale.Enabled = True
        btnStampSopr.Visible = False
        btnChiudiSegnalazione.Visible = False
        btnStampa.Visible = True
        btnAnnulla.Visible = False
        btnRelazionePadreFiglio.Visible = False
        btnSollecito.Visible = False
        btnAllegaFile.Visible = False

        imgChiudiSegnalazione.Visible = False
        imgAllega.Visible = True
        imgAllega.Enabled = True
        CheckBoxAttoVandalico.Enabled = False
        CheckBoxVerificaFornitore.Enabled = False
        CheckBoxFalsa.Enabled = False
    End Sub
    Private Function RicavaTipologiaManutenzione() As String
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If
        Dim idTipologia As String = "-1"
        If Not IsNothing(Session.Item("MOD_FO_LIMITAZIONI")) AndAlso Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
            'Se l'utenza è di tipo FORNITORE verifica sempre se abbia o meno cambiato la tipologia di segnalazione
            par.cmd.CommandText = "SELECT ID_TIPOLOGIA_MANUTENZIONE FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
            idTipologia = par.IfNull(par.cmd.ExecuteScalar, "-1")
            If idTipologia = "-1" Then
                par.cmd.CommandText = "SELECT COMBINAZIONE_TIPOLOGIE.ID_TIPO_MANUTENZIONE " _
                                            & " FROM SISCOM_MI.combinazione_tipologie, SISCOM_MI.SEGNALAZIONI " _
                                            & " WHERE combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                                            & " AND SEGNALAZIONI.ID = " & idSegnalazione.Value
                RicavaTipologiaManutenzione = par.IfNull(par.cmd.ExecuteScalar, "-1")
            Else
                RicavaTipologiaManutenzione = idTipologia
            End If
        Else
            'Se l'utenza è di tipo TECNICO sovrascrive ad ogni salvataggio la categoria inserita dal fornitore in base
            ' alla combinazione tipologie
            par.cmd.CommandText = "SELECT COMBINAZIONE_TIPOLOGIE.ID_TIPO_MANUTENZIONE " _
                                        & " FROM SISCOM_MI.combinazione_tipologie, SISCOM_MI.SEGNALAZIONI " _
                                        & " WHERE combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                                        & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                                        & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                                        & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                                        & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                                        & " AND SEGNALAZIONI.ID = " & idSegnalazione.Value
            RicavaTipologiaManutenzione = par.IfNull(par.cmd.ExecuteScalar, "-1")
        End If



        If connAperta = True Then
            connData.chiudi(False)
        End If
    End Function

    Private Sub cmbTipologiaManutenzione_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbTipologiaManutenzione.SelectedIndexChanged
        VerificaDate()
    End Sub

    Private Sub VerificaDate()
        Try
            If cmbTipologiaManutenzione.SelectedValue = "1" Then
                txtDataSopralluogo.Enabled = True
                txtOraSopralluogo.Enabled = True
                txtNoteSopralluogo.Enabled = True

                txtDataProgrammataIntervento.Enabled = True
                txtOraProgrammataIntervento.Enabled = True

                txtDataEffettivaIntervento.Enabled = True
                txtOraEffettivaIntervento.Enabled = True
                txtNoteEffIntervento.Enabled = True
            Else
                txtDataSopralluogo.Enabled = False
                txtOraSopralluogo.Enabled = False
                txtNoteSopralluogo.Enabled = False

                txtDataProgrammataIntervento.Enabled = False
                txtOraProgrammataIntervento.Enabled = False

                txtDataEffettivaIntervento.Enabled = False
                txtOraEffettivaIntervento.Enabled = False
                txtNoteEffIntervento.Enabled = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - VerificaDate - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub ImpostaVisibilitaRadioButton()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.cmd.CommandText = "SELECT count(*) " _
                                            & " FROM SISCOM_MI.combinazione_tipologie, SISCOM_MI.SEGNALAZIONI " _
                                            & " WHERE combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                                            & " and segnalazioni.id_tipologia_manutenzione<>combinazione_tipologie.id_tipo_manutenzione " _
                                            & " and segnalazioni.id_tipologia_manutenzione is not null " _
                                             & " and segnalazioni.fl_tipologia_confermata = 0" _
                                            & " and segnalazioni.id = " & idSegnalazione.Value _
                                            & " and segnalazioni.FL_RICH_MOD_TIPOLOGIA = 1"
            Dim numero As Integer = CInt(par.cmd.ExecuteScalar)
            If numero > 0 Then
                If Not IsNothing(Session.Item("FL_AUTORIZZAZIONE_ODL")) AndAlso Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Or
                Not IsNothing(Session.Item("FL_SUPERDIRETTORE")) AndAlso Session.Item("FL_SUPERDIRETTORE") = "1" Then
                    lblConfermaTipologiaFornitore.Visible = True
                    cmbConfermaTipologiaFornitore.Visible = True
                    imgAlert.Visible = True
                Else
                    lblConfermaTipologiaFornitore.Visible = False
                    cmbConfermaTipologiaFornitore.Visible = False
                    imgAlert.Visible = False
                End If
            Else
                lblConfermaTipologiaFornitore.Visible = False
                cmbConfermaTipologiaFornitore.Visible = False
                imgAlert.Visible = False
            End If

            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - ImpostaVisibilitaRadioButton - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub ModificaOggPage()
        par.SetModifyOggettoPage(cmbTipoSegnalazioneLivello1)
        par.SetModifyOggettoPage(cmbTipoSegnalazioneLivello2)
        par.SetModifyOggettoPage(cmbTipoSegnalazioneLivello3)
        par.SetModifyOggettoPage(cmbTipoSegnalazioneLivello4)
        par.SetModifyOggettoPage(cmbUrgenza)
        par.SetModifyOggettoPage(cmbConfermaTipologiaFornitore)
        par.SetModifyOggettoPage(DropDownListCanale)
        par.SetModifyOggettoPage(CheckBoxDVCA)
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
        par.SetModifyOggettoPage(txtNoteSopralluogo)
        par.SetModifyOggettoPage(txtNoteEffIntervento)
        par.SetModifyOggettoPage(TextBoxNota)
        par.SetModifyOggettoPage(txtDescrizione)
    End Sub

    Private Sub cmbConfermaTipologiaFornitore_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbConfermaTipologiaFornitore.SelectedIndexChanged
        Select Case cmbConfermaTipologiaFornitore.SelectedValue
            Case 1
        caricaTutteTipologie(, cmbTipologiaManutenzione.SelectedValue)
            Case 2
        caricaTutteTipologie()
        End Select
    End Sub
End Class