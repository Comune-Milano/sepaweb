
Partial Class GESTIONE_CONTATTI_DettagliAppuntamentiGestioneContatti
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
                If Not IsNothing(Request.QueryString("IDS")) Then
                    idSegnalazione.Value = Request.QueryString("IDS")
                    If idSegnalazione.Value = "-1" Then
                        btnAggiungiAppuntamento.Visible = False
                    Else
                        btnAggiungiAppuntamento.Visible = True
                    End If
                Else
                    btnAggiungiAppuntamento.Visible = False
                End If
                If Not IsNothing(Request.QueryString("FILIALE")) Then
                    If IsNumeric(Request.QueryString("FILIALE")) Then
                        idStrutturaPredefinita.Value = Request.QueryString("FILIALE")
                    End If
                End If
                TextBoxDataAppuntamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                If Not IsNothing(Request.QueryString("DATA")) Then
                    TextBoxDataAppuntamento.Text = par.FormattaData(Request.QueryString("DATA"))
                    TextBoxDataAppuntamentoIns.Text = par.FormattaData(Request.QueryString("DATA"))
                    lblTitolo1.Text = "Dettagli appuntamenti del " & par.FormattaData(Request.QueryString("DATA"))
                    lblTitolo2.Text = "Dettagli appuntamenti del " & par.FormattaData(Request.QueryString("DATA"))
                    lblTitolo3.Text = "Dettagli appuntamenti del " & par.FormattaData(Request.QueryString("DATA"))
                End If
                par.caricaComboBox("SELECT TAB_FILIALI.ID,NOME||'-'||DESCRIZIONE||' '||CIVICO||','||LOCALITA AS NOME FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND ID_TIPO_ST=0 ORDER BY NOME", cmbFilialeMod, "ID", "NOME", False)
                If Not IsNothing(cmbFilialeMod.Items.FindByValue(idStrutturaPredefinita.Value)) Then
                    cmbFilialeMod.SelectedValue = idStrutturaPredefinita.Value
                End If
                par.caricaComboBox("SELECT TAB_FILIALI.ID,NOME||'-'||DESCRIZIONE||' '||CIVICO||','||LOCALITA AS NOME FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND ID_TIPO_ST=0 ORDER BY NOME", cmbFiliale, "ID", "NOME", False)
                If Not IsNothing(cmbFiliale.Items.FindByValue(idStrutturaPredefinita.Value)) Then
                    cmbFiliale.SelectedValue = idStrutturaPredefinita.Value
                End If
                par.caricaComboBox("SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ID", cmbOrario, "ID", "ORARIO", False)
                par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_STATI WHERE ID>=5 ORDER BY ID", cmbStato, "ID", "DESCRIZIONE", False)
                caricaEsiti()
                par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE ID_FILIALE=" & cmbFiliale.SelectedValue & " ORDER BY ID", cmbSportello, "ID", "DESCRIZIONE", False)
                par.caricaComboBox("SELECT TAB_FILIALI.ID,NOME||'-'||DESCRIZIONE||' '||CIVICO||','||LOCALITA AS NOME FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND ID_TIPO_ST=0 ORDER BY NOME", cmbFilialeIns, "ID", "NOME", False)
                If Not IsNothing(cmbFilialeIns.Items.FindByValue(idStrutturaPredefinita.Value)) Then
                    cmbFilialeIns.SelectedValue = idStrutturaPredefinita.Value
                End If
                par.caricaComboBox("SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ID", cmbOrarioIns, "ID", "ORARIO", False)
                par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE ID_FILIALE=" & cmbFilialeIns.SelectedValue & " ORDER BY ID", cmbSportelloIns, "ID", "DESCRIZIONE", False)
                If idStrutturaPredefinita.Value = 119 Or idStrutturaPredefinita.Value = 120 Or idStrutturaPredefinita.Value = 121 Then
                    cmbFiliale.Enabled = False
                    cmbFilialeMod.Enabled = False
                    cmbFilialeIns.Enabled = False
                End If
                If btnAggiungiAppuntamento.Visible = True Then
                    If Not IsNothing(Request.QueryString("INDORA")) And Not IsNothing(Request.QueryString("INDSPO")) Then
                        indiceSportello.Value = Request.QueryString("INDSPO")
                        indiceOrario.Value = Request.QueryString("INDORA")
                    End If
                    Aggiungi()
                Else
                    CaricaAppuntamenti()
                End If
            End If
            Dim Script As String = "if((document.getElementById('CPContenuto_PanelTipo')!=null)&&(document.getElementById('yPosTipo')!=null)){document.getElementById('CPContenuto_PanelTipo').scrollTop = document.getElementById('yPosTipo').value;}"
            ScriptManager.RegisterStartupScript(PanelTipo, GetType(Panel), Page.ClientID, Script, True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            'If CType(Me.Master.FindControl("operatoreFiliale"), HiddenField).Value = "1" Then
            ''Se l'operatore è un operatore di filiale può operare solo all'interno della sua sede territoriale di appartenenza
            'If Not IsNothing(cmbFilialeMod.Items.FindByValue(Session.Item("ID_STRUTTURA"))) Then
            '    If CType(Me.Master.FindControl("supervisore"), HiddenField).Value = "0" Then
            '        cmbFilialeMod.SelectedValue = Session.Item("ID_STRUTTURA")
            '        cmbFilialeMod.Enabled = False
            '    End If
            'Else
            '    If CType(Me.Master.FindControl("supervisore"), HiddenField).Value = "0" Then
            '        par.modalDialogMessage("Agenda e Segnalazioni", "Non è possibile caricare la sede territoriale di appartenenza. Contattare l\'amministratore di sistema.", Page, "info", "Home.aspx")
            '        Exit Sub
            '    End If
            'End If
            'If Not IsNothing(cmbFiliale.Items.FindByValue(Session.Item("ID_STRUTTURA"))) Then
            '    If CType(Me.Master.FindControl("supervisore"), HiddenField).Value = "0" Then
            '        cmbFiliale.SelectedValue = Session.Item("ID_STRUTTURA")
            '        cmbFiliale.Enabled = False
            '    End If
            'Else
            '    If CType(Me.Master.FindControl("supervisore"), HiddenField).Value = "0" Then
            '        par.modalDialogMessage("Agenda e Segnalazioni", "Non è possibile caricare la sede territoriale di appartenenza. Contattare l\'amministratore di sistema.", Page, "info", "Home.aspx")
            '        Exit Sub
            '    End If
            'End If
            'If Not IsNothing(cmbFilialeIns.Items.FindByValue(Session.Item("ID_STRUTTURA"))) Then
            '    If CType(Me.Master.FindControl("supervisore"), HiddenField).Value = "0" Then
            '        cmbFilialeIns.SelectedValue = Session.Item("ID_STRUTTURA")
            '        cmbFilialeIns.Enabled = False
            '    End If
            'Else
            '    If CType(Me.Master.FindControl("supervisore"), HiddenField).Value = "0" Then
            '        par.modalDialogMessage("Agenda e Segnalazioni", "Non è possibile caricare la sede territoriale di appartenenza. Contattare l\'amministratore di sistema.", Page, "info", "Home.aspx")
            '        Exit Sub
            '    End If
            'End If
            'End If
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
                Exit Sub
            Else
                If CType(Me.Master.FindControl("operatoreComune"), HiddenField).Value = "1" Or CType(Me.Master.FindControl("FLGC_SL"), HiddenField).Value = "1" Then
                    solaLettura()
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaAppuntamenti()
        Try
            connData.apri()
            Dim condizioneFiliali As String = ""
            If cmbFilialeMod.SelectedValue <> "-1" Then
                condizioneFiliali = " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbFilialeMod.SelectedValue
            End If

            'Dim condizioneSportello As String = ""
            'If indiceSportello.Value <> "-1" Then
            '    condizioneSportello = " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE=" & indiceSportello.Value & " AND ID_FILIALE=" & cmbFilialeMod.SelectedValue & ")"
            'End If

            'Dim condizioneOrario As String = ""
            'If indiceOrario.Value <> "-1" Then
            '    condizioneOrario = " AND APPUNTAMENTI_CALL_CENTER.ID_ORARIO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_ORARI WHERE INDICE=" & indiceSportello.Value & ")"
            'End If



            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ID ASC"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim daOrari As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtOrari As New Data.DataTable
            daOrari.Fill(dtOrari)
            daOrari.Dispose()
            Dim dt As New Data.DataTable
            dt.Columns.Clear()
            dt.Columns.Add("ID")
            dt.Columns.Add("NOME")
            dt.Columns.Add("OPERATORE")
            dt.Columns.Add("DATA_INSERIMENTO")
            dt.Columns.Add("DATA_APPUNTAMENTO")
            dt.Columns.Add("ORA_APPUNTAMENTO")
            dt.Columns.Add("SPORTELLO")
            dt.Columns.Add("APPUNTAMENTO_CON")
            dt.Columns.Add("TELEFONO")
            dt.Columns.Add("CELLULARE")
            dt.Columns.Add("EMAIL")
            dt.Columns.Add("NOTE")
            dt.Columns.Add("STATO")
            dt.Columns.Add("SEGNALAZIONE")
            dt.Columns.Add("NUMERO")
            dt.Columns.Add("CONTRATTO")
            dt.Columns.Add("ELIMINA")
            dt.Columns.Add("FL_ELIMINA")
            Dim riga As Data.DataRow
            Dim LettoreApp As Oracle.DataAccess.Client.OracleDataReader
            For i As Integer = 1 To 4 Step 1
                For Each elemento As Data.DataRow In dtOrari.Rows
                    par.cmd.CommandText = "SELECT APPUNTAMENTI_CALL_CENTER.ID, " _
                        & " TO_CHAR(TO_DATE(DATA_APPUNTAMENTO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_APPUNTAMENTO," _
                        & " OPERATORI.OPERATORE, " _
                        & " TAB_FILIALI.NOME, " _
                        & "'" & elemento.Item("ORARIO") & "' AS ORA_APPUNTAMENTO," _
                        & " APPUNTAMENTI_SPORTELLI.ID AS ID_SPORTELLO," _
                        & " APPUNTAMENTI_SPORTELLI.INDICE AS SPORTELLO," _
                        & " CASE WHEN DATA_MODIFICA IS NULL THEN TO_CHAR(TO_DATE(SUBSTR(DATA_INSERIMENTO,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2) " _
                        & " ELSE TO_CHAR(TO_DATE(SUBSTR(DATA_MODIFICA,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' '||SUBSTR(DATA_MODIFICA,9,2)||':'||SUBSTR(DATA_MODIFICA,11,2) END AS DATA_INSERIMENTO, " _
                        & " APPUNTAMENTI_CALL_CENTER.COGNOME||' '||APPUNTAMENTI_CALL_CENTER.NOME APPUNTAMENTO_CON," _
                        & " APPUNTAMENTI_CALL_CENTER.TELEFONO," _
                        & " APPUNTAMENTI_CALL_CENTER.CELLULARE," _
                        & " APPUNTAMENTI_CALL_CENTER.EMAIL,(SELECT DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_STATI WHERE APPUNTAMENTI_STATI.ID=APPUNTAMENTI_CALL_cENTER.ID_sTATO_APPUNTAMENTO) AS STATO," _
                        & " NOTE,(SELECT DESCRIZIONE_RIC FROM SISCOM_MI.SEGNALAZIONI WHERE SEGNALAZIONI.ID=APPUNTAMENTI_CALL_CENTER.ID_SEGNALAZIONE) AS SEGNALAZIONE,'Eliminata dall''operatore '||(SELECT OPERATORE FROM SEPA.OPERATORI A WHERE A.ID=APPUNTAMENTI_cALL_cENTER.ID_OPERATORE_ELIMINAZIONE)||' il '||TO_CHAR(TO_DATE(SUBSTR(DATA_ELIMINAZIONE,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' alle ore '||SUBSTR(DATA_ELIMINAZIONE,9,2)||':'||SUBSTR(DATA_ELIMINAZIONE,11,2) AS ELIMINA,CASE WHEN DATA_ELIMINAZIONE IS NULL THEN 0 ELSE 1 END AS FL_ELIMINA," _
                        & "(CASE WHEN ID_ESITO_APPUNTAMENTO = 6 THEN ('<a href=""#"" onclick=""javascript:window.open(''../Contratti/Contratto.aspx?ID='|| (select id_contratto from siscom_mi.segnalazioni where segnalazioni.id=ID_SEGNALAZIONE) || ''','''',''scrollbars=no,resizable=yes,height=780,width=1160'');"">'|| (select COD_CONTRATTO FROM siscom_mi.RAPPORTI_UTENZA WHERE ID IN (SELECT id_contratto from siscom_mi.segnalazioni where segnalazioni.id=ID_SEGNALAZIONE))|| '</a>') ELSE '' END) AS CONTRATTO," _
                        & " '<a href=""#"" onclick=""javascript:window.open(''Segnalazione.aspx?IDS='||ID_SEGNALAZIONE||''','''',''scrollbars=no,resizable=yes'');"">'||ID_SEGNALAZIONE||'</a>' AS NUMERO " _
                        & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SEPA.OPERATORI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                        & " WHERE SEPA.OPERATORI.ID=APPUNTAMENTI_CALL_CENTER.ID_OPERATORE " _
                        & " AND TAB_FILIALI.ID=APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA " _
                        & " AND DATA_APPUNTAMENTO = '" & Request.QueryString("DATA") & "'" _
                        & condizioneFiliali _
                        & " AND APPUNTAMENTI_SPORTELLI.ID=APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO " _
                        & " AND APPUNTAMENTI_SPORTELLI.INDICE=" & i _
                        & " AND ID_ORARIO=" & elemento.Item("ID") _
                        & " ORDER BY ID_SPORTELLO,SPORTELLO,DATA_ELIMINAZIONE ASC,ORA_APPUNTAMENTO ASC"
                    LettoreApp = par.cmd.ExecuteReader
                    Dim contFLelimina As Integer = 0
                    Dim cont As Integer = 0
                    If LettoreApp.HasRows Then
                        While LettoreApp.Read
                            cont += 1
                            riga = dt.NewRow
                            riga.Item("ID") = par.IfNull(LettoreApp("ID"), "")
                            riga.Item("NOME") = par.IfNull(LettoreApp("NOME"), "")
                            riga.Item("OPERATORE") = par.IfNull(LettoreApp("OPERATORE"), "")
                            riga.Item("DATA_INSERIMENTO") = par.IfNull(LettoreApp("DATA_INSERIMENTO"), "")
                            riga.Item("DATA_APPUNTAMENTO") = par.IfNull(LettoreApp("DATA_APPUNTAMENTO"), "")
                            riga.Item("ORA_APPUNTAMENTO") = par.IfNull(LettoreApp("ORA_APPUNTAMENTO"), "")
                            riga.Item("SPORTELLO") = par.IfNull(LettoreApp("SPORTELLO"), "")
                            riga.Item("APPUNTAMENTO_CON") = par.IfNull(LettoreApp("APPUNTAMENTO_CON"), "")
                            riga.Item("TELEFONO") = par.IfNull(LettoreApp("TELEFONO"), "")
                            riga.Item("CELLULARE") = par.IfNull(LettoreApp("CELLULARE"), "")
                            riga.Item("EMAIL") = par.IfNull(LettoreApp("EMAIL"), "")
                            riga.Item("NOTE") = par.IfNull(LettoreApp("NOTE"), "")
                            riga.Item("STATO") = par.IfNull(LettoreApp("STATO"), "")
                            riga.Item("SEGNALAZIONE") = par.IfNull(LettoreApp("SEGNALAZIONE"), "")
                            riga.Item("NUMERO") = par.IfNull(LettoreApp("NUMERO"), "")
                            riga.Item("CONTRATTO") = par.IfNull(LettoreApp("CONTRATTO"), "")
                            riga.Item("ELIMINA") = par.IfNull(LettoreApp("ELIMINA"), "")
                            riga.Item("FL_ELIMINA") = par.IfNull(LettoreApp("FL_ELIMINA"), "")
                            dt.Rows.Add(riga)
                            If par.IfNull(LettoreApp("FL_ELIMINA"), "") = "1" Then
                                contFLelimina += 1
                            End If
                        End While
                        If cont = contFLelimina Then
                            riga = dt.NewRow
                            riga.Item("ID") = ""
                            riga.Item("NOME") = ""
                            riga.Item("OPERATORE") = ""
                            riga.Item("DATA_INSERIMENTO") = ""
                            riga.Item("DATA_APPUNTAMENTO") = ""
                            riga.Item("SPORTELLO") = i
                            riga.Item("ORA_APPUNTAMENTO") = elemento.Item("ORARIO")
                            riga.Item("APPUNTAMENTO_CON") = ""
                            riga.Item("TELEFONO") = ""
                            riga.Item("CELLULARE") = ""
                            riga.Item("EMAIL") = ""
                            riga.Item("NOTE") = ""
                            riga.Item("STATO") = ""
                            riga.Item("SEGNALAZIONE") = ""
                            riga.Item("NUMERO") = ""
                            riga.Item("CONTRATTO") = ""
                            riga.Item("ELIMINA") = ""
                            riga.Item("FL_ELIMINA") = ""
                            dt.Rows.Add(riga)
                        End If
                    Else
                        riga = dt.NewRow
                        riga.Item("ID") = ""
                        riga.Item("NOME") = ""
                        riga.Item("OPERATORE") = ""
                        riga.Item("DATA_INSERIMENTO") = ""
                        riga.Item("DATA_APPUNTAMENTO") = ""
                        riga.Item("SPORTELLO") = i
                        riga.Item("ORA_APPUNTAMENTO") = elemento.Item("ORARIO")
                        riga.Item("APPUNTAMENTO_CON") = ""
                        riga.Item("TELEFONO") = ""
                        riga.Item("CELLULARE") = ""
                        riga.Item("EMAIL") = ""
                        riga.Item("NOTE") = ""
                        riga.Item("STATO") = ""
                        riga.Item("SEGNALAZIONE") = ""
                        riga.Item("NUMERO") = ""
                        riga.Item("CONTRATTO") = ""
                        riga.Item("ELIMINA") = ""
                        riga.Item("FL_ELIMINA") = ""
                        dt.Rows.Add(riga)
                    End If
                    LettoreApp.Close()
                Next
            Next
            DataGridElencoAppuntamentiTotale.DataSource = dt
            DataGridElencoAppuntamentiTotale.DataBind()
            dt.Rows.Clear()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - CaricaAppuntamenti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DataGridElencoAppuntamentiTotale_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridElencoAppuntamentiTotale.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='yellow';};this.style.cursor='pointer';")
                e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
                e.Item.Attributes.Add("onclick", "if (document.getElementById('daElimina').value==0){if(" & e.Item.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "FL_ELIMINA")).Text & "==0){if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                    & "document.getElementById('idSelected').value='" & e.Item.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "ID")).Text & "';document.getElementById('CPFooter_btnAggiornaForm').click();}}")
                If e.Item.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "FL_ELIMINA")).Text = "0" Then
                    If Session.Item("MOD_SEGNALAZIONI_SL") <> "1" Then
                        e.Item.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "ELIMINA")).Text = "<img src=""Immagini/Elimina.png"" alt=""elimina"" onclick=""javascript:EliminaAppuntamento(" & e.Item.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "ID")).Text & ");"""
                    Else
                        e.Item.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "ELIMINA")).Text = ""
                    End If
                End If
                If e.Item.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "FL_ELIMINA")).Text = "1" Then
                    e.Item.ForeColor = Drawing.Color.Gray
                End If
                If e.Item.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "ID")).Text.Replace("&nbsp;", "") = "" Then
                    e.Item.BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD")
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - DataGridElencoAppuntamentiTotale_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub cmbFiliale_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbFilialeMod.SelectedIndexChanged
        Try
            connData.apri()
            par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE ID_FILIALE=" & cmbFilialeMod.SelectedValue & " ORDER BY ID", cmbSportello, "ID", "DESCRIZIONE", False)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - DataGridElencoAppuntamentiTotale_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        CaricaAppuntamenti()
    End Sub
    Protected Sub ButtonSalva_Click(sender As Object, e As System.EventArgs) Handles ButtonSalva.Click
        Try
            If idSelected.Value <> "-1" Then
                Dim errore As Boolean = False
                Dim testoErrore As String = ""
                'If Trim(TextBoxOra.Text) = "" Or Trim(TextBoxMinuto.Text) = "" Then
                '    errore = True
                '    testoErrore &= "L\'orario appuntamento è obbligatorio.\n"
                'End If
                'If IsNumeric(TextBoxOra.Text) AndAlso (CInt(TextBoxOra.Text) < 0 Or CInt(TextBoxOra.Text) > 23) Then
                '    errore = True
                '    testoErrore &= "Inserire l\'ora correttamente.\n"
                'End If
                'If IsNumeric(TextBoxMinuto.Text) AndAlso (CInt(TextBoxMinuto.Text) < 0 Or CInt(TextBoxMinuto.Text) > 59) Then
                '    errore = True
                '    testoErrore &= "Inserire i minuti correttamente.\n"
                'End If
                If cmbFilialeMod.SelectedValue = "-1" Then
                    errore = True
                    testoErrore &= "Selezionare una filiale.\n"
                End If
                If Trim(TextBoxCognome.Text) = "" Then
                    errore = True
                    testoErrore &= "Il cognome è obbligatorio.\n"
                End If
                If Trim(TextBoxTelefono.Text) = "" Then
                    errore = True
                    testoErrore &= "Il campo ""Telefono 1"" è obbligatorio.\n"
                End If
                Dim id_esito As String = "NULL"
                If cmbEsito.SelectedValue <> "-1" And cmbEsito.SelectedValue <> "" Then
                    id_esito = cmbEsito.SelectedValue
                End If
                If id_esito = "5" Then
                    If Trim(txtNoteSegnalazione.Text) = "" Then
                        errore = True
                        testoErrore &= "Il campo ""Nota Segnalazione"" è obbligatorio.\n"
                    End If
                    If radioNotaPubblica.Checked = False Then
                        errore = True
                        testoErrore &= "Il campo ""Tipologia nota segnalazione*"" deve essere obbligatoriamente ""Nota Pubblica"".\n"
                    End If
                End If
                If Not errore Then
                    Dim dataOdierna As Date = Format(Now, "dd/MM/yyyy")
                    Dim dataAppuntamento As Date = TextBoxDataAppuntamento.Text
                    If dataAppuntamento >= dataOdierna Or TextBoxDataAppuntamento.Enabled = False Then
                        connData.apri()
                        If Not par.IsFestivo(TextBoxDataAppuntamento.Text, True, cmbOrario.SelectedValue, cmbFiliale.SelectedValue, cmbSportello.SelectedValue) Then
                            Try
                                par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_CALL_CENTER " _
                                    & " SET " _
                                    & "ID_ORARIO=" & cmbOrario.SelectedValue _
                                    & ",ID_STRUTTURA=" & cmbFiliale.SelectedValue _
                                    & ",ID_SPORTELLO=" & cmbSportello.SelectedValue _
                                    & ",DATA_APPUNTAMENTO='" & par.FormatoDataDB(TextBoxDataAppuntamento.Text) & "'" _
                                    & ",DATA_MODIFICA='" & Format(Now, "yyyyMMddHHmmss") & "'" _
                                    & ",ID_OPERATORE_MODIFICA=" & Session.Item("ID_OPERATORE") _
                                    & ",NOME='" & par.PulisciStrSql(UCase(TextBoxNome.Text)) & "'" _
                                    & ",TELEFONO='" & par.PulisciStrSql(UCase(TextBoxTelefono.Text)) & "'" _
                                    & ",CELLULARE='" & par.PulisciStrSql(UCase(TextBoxCellulare.Text)) & "'" _
                                    & ",EMAIL='" & par.PulisciStrSql(UCase(TextBoxEmail.Text)) & "'" _
                                    & ",NOTE='" & par.PulisciStrSql(UCase(TextBoxNote.Text)) & "'" _
                                    & ",COGNOME='" & par.PulisciStrSql(UCase(TextBoxCognome.Text)) & "'" _
                                    & ",ID_STATO_APPUNTAMENTO=" & cmbStato.SelectedValue _
                                    & ",ID_ESITO_APPUNTAMENTO=" & id_esito _
                                    & " WHERE ID=" & idSelected.Value
                                par.cmd.ExecuteNonQuery()
                                If Not String.IsNullOrEmpty(txtNoteSegnalazione.Text) Then
                                    Dim tipoNotaSegnalazione As String = ""
                                    If radioNotaGestionale.Checked = True Then
                                        tipoNotaSegnalazione = "3"
                                    ElseIf radioNotaPubblica.Checked Then
                                        tipoNotaSegnalazione = "1"
                                    End If
                                    par.cmd.CommandText = "SELECT ID FROM SEPA.OPERATORI WHERE OPERATORE = '" & Session.Item("OPERATORE") & "'"
                                    Dim idOperatore As String = par.cmd.ExecuteScalar
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE ( " _
                                                        & "ID_TIPO_SEGNALAZIONE_NOTE, SOLLECITO, ID_OPERATORE,  " _
                                                        & "DATA_ORA, NOTE, ID_SEGNALAZIONE)  " _
                                                        & "VALUES (  " _
                                                        & tipoNotaSegnalazione & " /* ID_TIPO_SEGNALAZIONE_NOTE */, " _
                                                        & "0 /* SOLLECITO */, " _
                                                        & idOperatore & " /* ID_OPERATORE */, " _
                                                        & Format(Now, "yyyyMMddHHmm") & " /* DATA_ORA */, " _
                                                        & "'" & par.PulisciStrSql(txtNoteSegnalazione.Text) & "' /* NOTE */, " _
                                                        & idSegnalazione.Value & " /* ID_SEGNALAZIONE */ )"
                                    par.cmd.ExecuteNonQuery()
                                End If
                                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET id_stato=10,DATA_CHIUSURA = '" & Format(Now, "yyyyMMddHHmmss") & "' " _
                                    & " where (id=" & idSegnalazione.Value & " or id in(SELECT ID FROM SISCOM_mi.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idSegnalazione.Value & ")) and id_Stato<>10"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID FROM SISCOM_MI.SEGNALAZIONI " _
                                    & " where (id=" & idSegnalazione.Value & " or id in(SELECT ID FROM SISCOM_mi.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idSegnalazione.Value & ")) and id_Stato<>10"
                                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                While lettore.Read
                                    Dim idS As Integer = par.IfNull(lettore("ID"), 0)
                                    If idS > 0 Then
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,id_tipo_segnalazione_note) " _
                                            & "VALUES (" & idS & ", 'Segnalazione evasa a sportello', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ",2)"
                                        par.cmd.ExecuteNonQuery()
                                        WriteEvent("F243", "", , , idS)
                                    End If
                                End While
                                lettore.Close()
                                CaricaAppuntamenti()
                            Catch ex As Exception
                                connData.chiudi()
                                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                                par.modalDialogMessage("Dettagli appuntamenti", "Slot appuntamento già occupato.", Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                            End Try
                            WriteEvent("F237", "")
                            connData.chiudi()
                            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                            par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento modificato correttamente.", Page, "successo", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                        Else
                            connData.chiudi()
                            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                            par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento non modificato! Gli sportelli\nnella data e nell\'ora selezionati sono chiusi.", Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                        par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento non modificato!\nNon è più possibile prendere appuntamento\nnella giornata selezionata.", Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento non modificato!\n" & testoErrore, Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                par.modalDialogMessage("Dettagli appuntamenti", "Selezionare l\'appuntamento da modificare.", Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - ButtonSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnAggiornaForm_Click(sender As Object, e As System.EventArgs) Handles btnAggiornaForm.Click
        Try
            If idSelected.Value <> "-1" Then

                connData.apri()
                par.cmd.CommandText = "SELECT APPUNTAMENTI_CALL_CENTER.*,TO_CHAR(TO_dATE(DATA_APPUNTAMENTO,'YYYY/MM/DD'),'DD/MM/YYYY') AS DATA_APP FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID=" & idSelected.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim dataOdierna As Date = Format(Now, "dd/MM/yyyy")
                Dim dataAppuntamentoOld As Date
                If lettore.Read Then
                    cmbOrario.SelectedValue = lettore("ID_ORARIO")
                    cmbSportello.SelectedValue = lettore("ID_SPORTELLO")
                    cmbFiliale.SelectedValue = lettore("ID_STRUTTURA")
                    TextBoxDataAppuntamento.Text = lettore("DATA_APP")
                    dataAppuntamentoOld = TextBoxDataAppuntamento.Text
                    TextBoxCognome.Text = lettore("COGNOME")
                    TextBoxNome.Text = par.IfNull(lettore("NOME"), "")
                    TextBoxTelefono.Text = par.IfNull(lettore("TELEFONO"), "")
                    TextBoxCellulare.Text = par.IfNull(lettore("CELLULARE"), "")
                    TextBoxEmail.Text = par.IfNull(lettore("EMAIL"), "")
                    TextBoxNote.Text = par.IfNull(lettore("NOTE"), "")
                    cmbStato.SelectedValue = par.IfNull(lettore("ID_STATO_APPUNTAMENTO"), "")
                    caricaEsiti()
                    If cmbEsito.Enabled = True Then
                        cmbEsito.SelectedValue = par.IfNull(lettore("ID_ESITO_APPUNTAMENTO"), "")
                    End If
                    lblNumeroSegnalazione.Text = par.IfNull(lettore("ID_SEGNALAZIONE"), "-")
                    idSegnalazione.Value = par.IfNull(lettore("ID_SEGNALAZIONE"), "-1")
                    par.cmd.CommandText = "SELECT ID_STATO FROM siscom_mi.SEGNALAZIONI WHERE ID = " & idSegnalazione.Value
                    Dim statoSegnalazione As String = par.cmd.ExecuteScalar
                    If statoSegnalazione <> "10" Then
                        btnChiudiSegnalazione.Visible = True
                    Else
                        btnChiudiSegnalazione.Visible = False
                End If
                    CaricaListaDocumentiDaPortare()
                End If
                lettore.Close()
                connData.chiudi()
                If dataAppuntamentoOld <= dataOdierna Then
                    TextBoxDataAppuntamento.Enabled = False
                    cmbFiliale.Enabled = False
                    cmbOrario.Enabled = False
                    cmbSportello.Enabled = False
                    TextBoxCognome.Enabled = False
                    TextBoxNome.Enabled = False
                    TextBoxTelefono.Enabled = True
                    TextBoxCellulare.Enabled = True
                    TextBoxEmail.Enabled = True
                    TextBoxNote.Enabled = True
                    cmbStato.Enabled = True
                End If
                For Each elemento As DataGridItem In DataGridElencoAppuntamentiTotale.Items
                    If idSelected.Value = elemento.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "ID")).Text.ToString Then
                        elemento.BackColor = Drawing.ColorTranslator.FromHtml("#FF9900")
                    Else
                        elemento.BackColor = Drawing.Color.White
                    End If
                Next
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                par.modalDialogMessage("Dettagli appuntamenti", "Nessun appuntamento selezionato.", Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - btnAggiornaForm_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
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
                    par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento eliminato correttamente.", Page, "successo", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                End If
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - btnElimina_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub soloLettura()
        Try
            TextBoxDataAppuntamento.Enabled = False
            TextBoxNome.Enabled = False
            TextBoxCognome.Enabled = False
            TextBoxNote.Enabled = False
            TextBoxTelefono.Enabled = False
            TextBoxCellulare.Enabled = False
            TextBoxEmail.Enabled = False
            cmbSportello.Enabled = False
            cmbOrario.Enabled = False
            cmbStato.Enabled = False
            ButtonSalva.Visible = False
            cmbFilialeMod.Enabled = False
            For Each elemento As DataGridItem In DataGridElencoAppuntamentiTotale.Items
                elemento.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "ELIMINA")).Text = ""
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - soloLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnIndietro_Click(sender As Object, e As System.EventArgs) Handles btnIndietro.Click
        visualizzaPaginaPrincipale()
    End Sub
    Protected Sub btnIndietro2_Click(sender As Object, e As System.EventArgs) Handles btnIndietro2.Click
        If Not IsNothing(Request.QueryString("INDORA")) And Not IsNothing(Request.QueryString("INDSPO")) Then
            Response.Redirect("AgendaGestioneContatti.aspx?PROV=SEG&IDS=" & idSegnalazione.Value & "&IDF=" & cmbFilialeIns.SelectedValue, False)
        Else
            visualizzaPaginaPrincipale()
        End If
    End Sub
    Private Sub visualizzaPaginaPrincipale()
        MultiView1.ActiveViewIndex = 0
        MultiView2.ActiveViewIndex = 0
        MultiView3.ActiveViewIndex = 0
    End Sub
    Private Sub visualizzaPaginaModifica()
        MultiView1.ActiveViewIndex = 1
        MultiView2.ActiveViewIndex = 1
        MultiView3.ActiveViewIndex = 1
    End Sub
    Private Sub visualizzaPaginaInserimento()
        MultiView1.ActiveViewIndex = 2
        MultiView2.ActiveViewIndex = 2
        MultiView3.ActiveViewIndex = 2
    End Sub
    Protected Sub btnInserisciAppuntamento_Click(sender As Object, e As System.EventArgs) Handles btnInserisciAppuntamento.Click
        Try
            Dim errore As Boolean = False
            Dim testoErrore As String = ""
            'If Trim(TextBoxOra.Text) = "" Or Trim(TextBoxMinuto.Text) = "" Then
            '    errore = True
            '    testoErrore &= "L\'orario appuntamento è obbligatorio.\n"
            'End If
            'If IsNumeric(TextBoxOra.Text) AndAlso (CInt(TextBoxOra.Text) < 0 Or CInt(TextBoxOra.Text) > 23) Then
            '    errore = True
            '    testoErrore &= "Inserire l\'ora correttamente.\n"
            'End If
            'If IsNumeric(TextBoxMinuto.Text) AndAlso (CInt(TextBoxMinuto.Text) < 0 Or CInt(TextBoxMinuto.Text) > 59) Then
            '    errore = True
            '    testoErrore &= "Inserire i minuti correttamente.\n"
            'End If
            If cmbFilialeIns.SelectedValue = "-1" Then
                errore = True
                testoErrore &= "Selezionare una filiale.\n"
            End If
            If Trim(TextBoxDataAppuntamentoIns.Text) = "" Or Len(TextBoxDataAppuntamentoIns.Text) <> 10 Then
                errore = True
                testoErrore &= "Data appuntamento obbligatoria.\n"
            End If
            If Trim(TextBoxCognomeIns.Text) = "" Then
                errore = True
                testoErrore &= "Il cognome è obbligatorio.\n"
            End If
            If Trim(TextBoxTelefonoIns.Text) = "" Then
                errore = True
                testoErrore &= "Il campo ""Telefono 1"" è obbligatorio.\n"
            End If
            Dim inserimentoOK As Boolean = False
            If Not errore Then
                connData.apri()
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_SEGNALAZIONE=" & idSegnalazione.Value _
                    & " AND DATA_ELIMINAZIONE IS NULL"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim numeroSegnalazioni As Integer = 0
                If lettore.Read Then
                    numeroSegnalazioni = par.IfNull(lettore(0), 0)
                End If
                lettore.Close()
                'If numeroSegnalazioni = 0 Then
                Dim dataOdierna As Date = Format(Now, "dd/MM/yyyy")
                Dim dataAppuntamento As Date = TextBoxDataAppuntamentoIns.Text
                If dataAppuntamento >= dataOdierna Then

                    If Not par.IsFestivo(TextBoxDataAppuntamentoIns.Text, True, cmbOrarioIns.SelectedValue, cmbFilialeIns.SelectedValue, cmbSportelloIns.SelectedValue) Then
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPUNTAMENTI_CALL_CENTER ( " _
                            & " ID, DATA_APPUNTAMENTO, DATA_INSERIMENTO, ID_ORARIO, ID_SPORTELLO , ID_STRUTTURA,  " _
                            & " ID_OPERATORE, NOME, COGNOME,  " _
                            & " TELEFONO, CELLULARE, EMAIL, NOTE, ID_SEGNALAZIONE)  " _
                            & " VALUES (SISCOM_MI.SEQ_APPUNTAMENTI_CALL_CENTER.NEXTVAL, " _
                            & " '" & par.FormatoDataDB(TextBoxDataAppuntamentoIns.Text) & "', " _
                            & " '" & Format(Now, "yyyyMMddHHmmss") & "', " _
                            & cmbOrarioIns.SelectedValue & ", " _
                            & cmbSportelloIns.SelectedValue & ", " _
                            & cmbFilialeIns.SelectedValue & " , " _
                            & Session.Item("ID_OPERATORE") & ", " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxNomeIns.Text)) & "', " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxCognomeIns.Text)) & "', " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxTelefonoIns.Text)) & "', " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxCellulareIns.Text)) & "', " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxEmailIns.Text)) & "', " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxNoteIns.Text)) & "', " _
                            & idSegnalazione.Value & ")"
                        Try
                            par.cmd.ExecuteNonQuery()
                            WriteEvent("F235", "")
                            connData.chiudi()
                            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                            'par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento inserito correttamente.", Page, "successo", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                            par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento inserito correttamente.", Page, "successo")
                            inserimentoOK = True
                        Catch ex As Exception
                            connData.chiudi()
                            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                            par.modalDialogMessage("Dettagli appuntamenti", "Slot appuntamento già occupato.", Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                        End Try
                    Else
                        connData.chiudi()
                        ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                        par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento non inserito! Gli sportelli\nnella data e nell\'ora selezionati sono chiusi.", Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento non inserito!\nNon è più possibile prendere appuntamento\nnella giornata selezionata.", Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                End If
                'Else
                '    par.cmd.CommandText = "SELECT TO_CHAR(TO_DATE(DATA_APPUNTAMENTO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA FROM SISCOM_MI.APPUNTAMENTI_CALL_cENTER WHERE ID_sEGNALAZIONE=" & idSegnalazione.Value & " AND DATA_ELIMINAZIONE IS NULL"
                '    Dim lettoreData As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                '    Dim data As String = ""
                '    If lettoreData.Read Then
                '        data = par.IfNull(lettoreData("DATA"), "")
                '    End If
                '    lettoreData.Close()
                '    connData.chiudi()
                '    If data = "" Then
                '        par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento già esistente!\nModificare l\'appuntamento esistente.", Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                '    Else
                '        par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento già esistente in data " & data & "!\nModificare l\'appuntamento esistente.", Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
                '    End If
                'End If

                If Not IsNothing(Request.QueryString("INDORA")) And Not IsNothing(Request.QueryString("INDSPO")) And inserimentoOK = True Then
                    Response.Redirect("Segnalazione.aspx?IDS=" & idSegnalazione.Value & "&VIEW=1", False)
                Else
                    CaricaAppuntamenti()
                End If
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                par.modalDialogMessage("Dettagli appuntamenti", "Appuntamento non inserito!\n" & testoErrore, Page, "info", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - btnInserisciAppuntamento_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnModificaAppuntamento_Click(sender As Object, e As System.EventArgs) Handles btnModificaAppuntamento.Click
        Try
            If idSelected.Value = "-1" Then
                par.modalDialogMessage("Dettagli appuntamenti", "E\' ncecessario selezionare un appuntamento.", Page, "info")
            Else
                TextBoxNome.Enabled = False
                TextBoxCognome.Enabled = False
                visualizzaPaginaModifica()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - btnModificaAppuntamento_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnAggiungiAppuntamento_Click(sender As Object, e As System.EventArgs) Handles btnAggiungiAppuntamento.Click
        Aggiungi()
    End Sub
    Private Sub Aggiungi()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT COGNOME_RS,NOME,TELEFONO1,TELEFONO2,MAIL FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idSegnalazione.Value
            Dim LettSegnalazione As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettSegnalazione.Read Then
                TextBoxCognomeIns.Text = par.IfNull(LettSegnalazione("COGNOME_RS"), "")
                TextBoxNomeIns.Text = par.IfNull(LettSegnalazione("NOME"), "")
                TextBoxCognomeIns.Enabled = False
                TextBoxNomeIns.Enabled = False
                TextBoxTelefonoIns.Text = par.IfNull(LettSegnalazione("TELEFONO1"), "")
                TextBoxCellulareIns.Text = par.IfNull(LettSegnalazione("TELEFONO2"), "")
                TextBoxEmailIns.Text = par.IfNull(LettSegnalazione("MAIL"), "")
                cmbFilialeIns.SelectedValue = cmbFilialeMod.SelectedValue
                CaricaListaDocumentiDaPortareINS()
            End If
            LettSegnalazione.Close()
            If indiceSportello.Value <> "-1" Then
                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE=" & indiceSportello.Value & " AND ID_FILIALE=" & cmbFilialeIns.SelectedValue
                cmbSportelloIns.SelectedValue = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If
            If indiceOrario.Value <> "-1" Then
                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.APPUNTAMENTI_ORARI WHERE INDICE=" & indiceOrario.Value
                cmbOrarioIns.SelectedValue = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - btnAggiungiAppuntamento_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        visualizzaPaginaInserimento()
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
                & "VALUES (" & idSegnalazione.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
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
    Protected Sub btnIndietro3_Click(sender As Object, e As System.EventArgs) Handles btnIndietro3.Click
        If Not IsNothing(Request.QueryString("PROV")) AndAlso Request.QueryString("PROV") = "MOD" Then
            Response.Redirect("Segnalazione.aspx?IDS=" & idSegnalazione.Value, False)
        Else
            If idSegnalazione.Value <> "-1" Then
                If Not IsNothing(Request.QueryString("INDORA")) And Not IsNothing(Request.QueryString("INDSPO")) Then
                    Response.Redirect("AgendaGestioneContatti.aspx?PROV=SEG&IDS=" & idSegnalazione.Value & "&IDF=" & cmbFilialeIns.SelectedValue & "&STRUTT=" & Request.QueryString("FILIALE"), False)
                Else
                    Response.Redirect("AgendaGestioneContatti.aspx?PROV=SEG&IDS=" & idSegnalazione.Value & "&STRUTT=" & Request.QueryString("FILIALE"), False)
                End If
            Else
                Response.Redirect("AgendaGestioneContatti.aspx?STRUTT=" & Request.QueryString("FILIALE"), False)
            End If
            End If
    End Sub
    Private Sub solaLettura()
        Try
            Dim CTRL As Control = Nothing
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
            For Each elemento As DataGridItem In DataGridElencoAppuntamentiTotale.Items
                elemento.Cells(par.IndDGC(DataGridElencoAppuntamentiTotale, "ELIMINA")).Text = ""
            Next
            btnIndietro3.Enabled = True
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx")
    End Sub
    Protected Sub cmbFiliale_SelectedIndexChanged1(sender As Object, e As System.EventArgs) Handles cmbFiliale.SelectedIndexChanged
        visualizzaPaginaModifica()
    End Sub
    Protected Sub cmbFilialeIns_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbFilialeIns.SelectedIndexChanged
        visualizzaPaginaInserimento()
    End Sub

    Protected Sub btnChiudiSegnalazione_Click(sender As Object, e As System.EventArgs) Handles btnChiudiSegnalazione.Click
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT SEGNALAZIONI.ID FROM SISCOM_MI.SEGNALAZIONI " _
               & " where (id=" & idSegnalazione.Value & " or id in(SELECT ID FROM SISCOM_mi.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idSegnalazione.Value & ")) and id_Stato<>10"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                Dim idS As Integer = par.IfNull(lettore("ID"), 0)
                If idS > 0 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,id_tipo_segnalazione_note) " _
                        & "VALUES (" & idS & ", 'Segnalazione chiusa da appuntamenti', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ",2)"
                    par.cmd.ExecuteNonQuery()
                    WriteEvent("F243", "")
                End If
            End While
            lettore.Close()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET id_stato=10,DATA_CHIUSURA = '" & Format(Now, "yyyyMMddHHmmss") & "' " _
                        & " where (id=" & idSegnalazione.Value & " or id in(SELECT ID FROM SISCOM_mi.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idSegnalazione.Value & ")) and id_Stato<>10"
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
            par.modalDialogMessage("Dettagli appuntamenti", "Segnalazione chiusa correttamente.", Page, "successo", "DettagliAppuntamentiGestioneContatti.aspx" & Request.Url.Query)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Dettagli Appuntamenti - WriteEvent - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbStato_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbStato.SelectedIndexChanged
        caricaEsiti()
        visualizzaPaginaModifica()
    End Sub

    Private Sub CaricaListaDocumentiDaPortareINS()
        Try
            Dim dt As Data.DataTable = GrigliaDocumentiDaPortare()

            If dt.Rows.Count > 0 Then
                DataGridDocumentiRichiestiIns.DataSource = dt
                DataGridDocumentiRichiestiIns.DataBind()
                DataGridDocumentiRichiestiIns.Visible = True
                lblDocumentiRichiestiIns.Visible = False
                PanelElencoDocumentiRichiestiIns.Visible = True
            Else
                DataGridDocumentiRichiestiIns.Visible = False
                lblDocumentiRichiestiIns.Visible = True
                PanelElencoDocumentiRichiestiIns.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaListaDocumentiDaPortareINS - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaListaDocumentiDaPortare()
        Try
            Dim dt As Data.DataTable = GrigliaDocumentiDaPortare()

            If dt.Rows.Count > 0 Then
                DataGridDocumentiRichiesti.DataSource = dt
                DataGridDocumentiRichiesti.DataBind()
                DataGridDocumentiRichiesti.Visible = True
                lblDocumentiRichiesti.Visible = False
                PanelElencoDocumentiRichiesti.Visible = True
            Else
                DataGridDocumentiRichiesti.Visible = False
                lblDocumentiRichiesti.Visible = True
                PanelElencoDocumentiRichiesti.Visible = False
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaListaDocumentiDaPortare - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Private Function GrigliaDocumentiDaPortare() As Data.DataTable
        Try
            Dim dt As New Data.DataTable

            Dim TipoSegnalazioneLivello0 As String = ""
            Dim TipoSegnalazioneLivello1 As String = ""
            Dim TipoSegnalazioneLivello2 As String = ""

            par.cmd.CommandText = "select id_tipo_segnalazione, id_tipo_segn_livello_1, id_tipo_segn_livello_2 " _
                & " from siscom_mi.segnalazioni where id = " & idSegnalazione.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read() Then
                TipoSegnalazioneLivello0 = par.IfNull(myReader("id_tipo_segnalazione"), "")
                TipoSegnalazioneLivello1 = par.IfNull(myReader("id_tipo_segn_livello_1"), "")
                TipoSegnalazioneLivello2 = par.IfNull(myReader("id_tipo_segn_livello_2"), "")
            End If
            myReader.Close()
            If TipoSegnalazioneLivello0 = "0" Then
                Dim query As String = ""
                If TipoSegnalazioneLivello2 <> "-1" And TipoSegnalazioneLivello2 <> "" Then
                    query = "SELECT DESCRIZIONE AS DOCUMENTI_RICHIESTI FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI," _
                        & "SISCOM_MI.TIPOLOGIE_DOCUMENTI WHERE SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI.ID_TIPOLOGIA_DOCUMENTO=TIPOLOGIE_DOCUMENTI.ID " _
                        & " AND ID_TIPO_SEGN_LIVELLO_2=" & TipoSegnalazioneLivello2 _
                        & " AND ID_TIPO_SEGN_LIVELLO_1=" & TipoSegnalazioneLivello1 _
                        & " ORDER BY DESCRIZIONE"
                Else
                    If TipoSegnalazioneLivello1 <> "-1" And TipoSegnalazioneLivello1 <> "" Then
                        query = "SELECT DESCRIZIONE AS DOCUMENTI_RICHIESTI FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI," _
                            & "SISCOM_MI.TIPOLOGIE_DOCUMENTI WHERE SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI.ID_TIPOLOGIA_DOCUMENTO=TIPOLOGIE_DOCUMENTI.ID " _
                            & "AND ID_TIPO_SEGN_LIVELLO_2 IS NULL " _
                            & "AND ID_TIPO_SEGN_LIVELLO_1= " & TipoSegnalazioneLivello1 _
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
                    da.Fill(dt)
                    da.Dispose()
                    If connAperta = True Then
                        connData.chiudi(False)
                    End If
                End If
                'Else
                '    PanelElencoDocumentiRichiesti.Visible = False
                '    PanelElencoDocumentiRichiestiIns.Visible = False
            End If
            Return dt
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - GrigliaDocumentiDaPortare - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try


    End Function

    Private Sub caricaEsiti()
        If cmbStato.SelectedValue = "6" Then
            par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.ESITO_APPUNTAMENTI_CC where id>=5 ORDER BY ID", cmbEsito, "ID", "DESCRIZIONE", False)
            cmbEsito.Enabled = True
        ElseIf cmbStato.SelectedValue = "7" Then
            par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.ESITO_APPUNTAMENTI_CC where id<5 ORDER BY ID", cmbEsito, "ID", "DESCRIZIONE", False)
            cmbEsito.Enabled = True
        Else
            cmbEsito.Items.Clear()
            cmbEsito.Items.Add(New ListItem("", "-1"))
            cmbEsito.Enabled = False
        End If
    End Sub
End Class
