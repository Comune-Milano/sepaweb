Imports Telerik.Web.UI

Partial Class GESTIONE_CONTATTI_RicercaSegnalazioniUn
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
                caricaStrutture()
                caricaComplessi()
                caricaEdifici()
                caricaFornitori()
                CaricaStatoSegnalazioni()
                CaricaTipoSegnalazione()
                txtDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni Unione - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Uscita", "validNavigation=true;", True)
            'If CType(Me.Master.FindControl("operatoreFiliale"), HiddenField).Value = "1" Then
            '    'Se l'operatore è un operatore di filiale può operare solo all'interno della sua sede territoriale di appartenenza
            '    If Session.Item("ID_CAF") = 6 Then
            '        If Not IsPostBack Then
            '            caricaComplessi()
            '            caricaEdifici()
            '            cmbComplesso.Focus()
            '        End If
            '    Else
            '        If Not IsNothing(cmbSedeTerritoriale.Items.FindByValue(Session.Item("ID_STRUTTURA"))) Then
            '            If CType(Me.Master.FindControl("supervisore"), HiddenField).Value = "0" Then
            '                cmbSedeTerritoriale.SelectedValue = Session.Item("ID_STRUTTURA")
            '                cmbSedeTerritoriale.Enabled = False
            '            End If
            '            If Not IsPostBack Then
            '                caricaComplessi()
            '                caricaEdifici()
            '                cmbComplesso.Focus()
            '            End If
            '        Else
            '            par.modalDialogMessage("Agenda e Segnalazioni", "Non è possibile caricare la sede territoriale di appartenenza. Contattare l\'amministratore di sistema.", Page, "info", "Home.aspx")
            '        End If
            '    End If
            'End If
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni Unione - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        If HiddenFieldVista.Value = "1" Then
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
        Else
            MultiViewRicerca.ActiveViewIndex = 0
            MultiViewBottoni.ActiveViewIndex = 0

        End If
    End Sub
    Private Sub caricaStrutture()
        Try
            connData.apri()
            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC", CheckBoxListSedi, "ID", "NOME")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni Unione - caricaStrutture - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaComplessi()
        Try
            connData.apri()
            Dim condizioneSedeTerritoriale As String = ""
            Dim listaSedi As String = ""
            For Each item As ListItem In CheckBoxListSedi.Items
                If item.Selected = True Then
                    If listaSedi = "" Then
                        listaSedi = item.Value
                    Else
                        listaSedi &= "," & item.Value
                    End If
                End If
            Next
            If listaSedi <> "" Then
                condizioneSedeTerritoriale = " AND ID_FILIALE in (" & listaSedi & ")"
            End If
            par.caricaComboBox("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneSedeTerritoriale & " ORDER BY DENOMINAZIONE", cmbComplesso, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni Unione - caricaComplessi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaEdifici()
        Try
            connData.apri()
            Dim condizioneComplesso As String = ""
            If cmbComplesso.SelectedValue <> "-1" Then
                condizioneComplesso = " AND ID_COMPLESSO=" & cmbComplesso.SelectedValue
            Else
                Dim listaSedi As String = ""
                For Each item As ListItem In CheckBoxListSedi.Items
                    If item.Selected = True Then
                        If listaSedi = "" Then
                            listaSedi = item.Value
                        Else
                            listaSedi &= "," & item.Value
                        End If
                    End If
                Next
                If listaSedi <> "" Then
                    condizioneComplesso = " AND ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE in ( " & listaSedi & ")) "
                End If
            End If
            par.caricaComboBox("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 " & condizioneComplesso & " ORDER BY DENOMINAZIONE", cmbEdificio, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni Unione - caricaEdifici - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaStatoSegnalazioni()
        Try
            connData.apri()
            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE ID >=0", CheckBoxListStato, "ID", "DESCRIZIONE")
            connData.chiudi()
            For Each elemento As ListItem In CheckBoxListStato.Items
                If elemento.Text <> "CHIUSA" Then
                    elemento.Selected = True
                End If
            Next
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni Unione - CaricaStatoSegnalazioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaFornitori()
        Try
            connData.apri()
            par.caricaComboBox("SELECT ID,RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE ID_sEGNALAZIONI IN (SELECT ID FROM SISCOM_MI.SEGNALAZIONI))) ORDER BY 2", cmbFornitori, "ID", "RAGIONE_SOCIALE")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni Unione - caricaFornitori - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaTipoSegnalazione()
        Try
            connData.apri()
            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE ORDER BY ID", CheckBoxListTipoSegnalazione, "ID", "DESCRIZIONE")
            connData.chiudi()
            cmbTipoSegnalazioneLivello1.Items.Clear()
            cmbTipoSegnalazioneLivello2.Items.Clear()
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni Unione - CaricaTipoSegnalazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        CaricaRisultati()
        MultiViewRicerca.ActiveViewIndex = 1
        MultiViewBottoni.ActiveViewIndex = 1
    End Sub
    Protected Sub cmbTipoSegnalazione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello1.SelectedIndexChanged
        CaricaTipologieLivello2()
    End Sub
    Protected Sub cmbComplesso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        caricaEdifici()
        cmbEdificio.Focus()
    End Sub

    'Protected Sub DataGridSegnalaz_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridSegnalaz.ItemDataBound

    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

    '        If CDec(e.Item.Cells(par.IndDGC(DataGridSegnalaz, "FIGLI2")).Text) > 0 Then

    '            Dim condizioneOrdinamento As String = "ORDER BY ID_PERICOLO_SEGNALAZIONE DESC,DATA_ORA_RICHIESTA ASC"

    '            par.cmd.CommandText = "SELECT SEGNALAZIONI.ID, " _
    '                & "SEGNALAZIONI.ID AS NUM, " _
    '                & "SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
    '                & "'' AS TIPO_INT, " _
    '                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
    '                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
    '                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
    '                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
    '                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
    '                & "TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
    '                & "EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
    '                & "COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
    '                & " (CASE " _
    '                & " WHEN SEGNALAZIONI.ID_UNITA " _
    '                & " IS NOT NULL " _
    '                & " THEN " _
    '                & " (SELECT MAX (COD_CONTRATTO) " _
    '                & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
    '                & " WHERE ID IN " _
    '                & " (SELECT ID_CONTRATTO " _
    '                & " FROM SISCOM_MI. " _
    '                & " UNITA_CONTRATTUALE " _
    '                & " WHERE UNITA_CONTRATTUALE. " _
    '                & " ID_UNITA = " _
    '                & " SEGNALAZIONI.ID_UNITA) " _
    '                & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
    '                & " 1, " _
    '                & " 8) " _
    '                & "  BETWEEN NVL ( " _
    '                & " RAPPORTI_UTENZA. " _
    '                & " DATA_DECORRENZA, " _
    '                & " '10000000') " _
    '                & " AND NVL ( " _
    '                & " RAPPORTI_UTENZA. " _
    '                & " DATA_RICONSEGNA, " _
    '                & " '30000000')) " _
    '                & " ELSE " _
    '                & " NULL " _
    '                & " END) " _
    '                & " AS CODICE_RU, " _
    '                & "TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
    '                & "REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
    '                & "NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
    '                & "(CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
    '                & "id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
    '                & ") ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
    '                & ",'FALSE' AS CHECK1 " _
    '                & ",(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
    '                & "FROM siscom_mi.tab_stati_segnalazioni, " _
    '                & "siscom_mi.segnalazioni, " _
    '                & "siscom_mi.tab_filiali, " _
    '                & "siscom_mi.edifici, " _
    '                & "siscom_mi.unita_immobiliari, " _
    '                & "siscom_mi.TIPOLOGIE_GUASTI, " _
    '                & "OPERATORI " _
    '                & "WHERE ID_SEGNALAZIONE_PADRE=" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "id")).Text _
    '                & " and tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
    '                & " AND segnalazioni.id_stato <> -1 " _
    '                & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
    '                & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
    '                & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
    '                & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
    '                & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
    '                & condizioneOrdinamento



    '            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

    '            Dim dt As New Data.DataTable
    '            da.Fill(dt)

    '            For Each item As Data.DataRow In dt.Rows

    '                If item("TIPO").ToString = "1" Or item("TIPO").ToString = "6" Then
    '                    Select Case item("ID_PERICOLO_SEGNALAZIONE").ToString
    '                        Case "1"
    '                            item("TIPO_INT") = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                                & "<tr><td><img src=""Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
    '                                & "<td>" & item("TIPO_INT") & "</td></<tr></table>"
    '                        Case "2"
    '                            item("TIPO_INT") = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                                & "<tr><td><img src=""Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
    '                                & "<td>" & item("TIPO_INT") & "</td></<tr></table>"
    '                        Case "3"
    '                            item("TIPO_INT") = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                                & "<tr><td><img src=""Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
    '                                & "<td>" & item("TIPO_INT") & "</td></<tr></table>"
    '                        Case "4"
    '                            item("TIPO_INT") = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                                & "<tr><td><img src=""Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
    '                                & "<td>" & item("TIPO_INT") & "</td></<tr></table>"
    '                        Case "0"
    '                            item("TIPO_INT") = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                                & "<tr><td><img src=""Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
    '                                & "<td>" & item("TIPO_INT") & "</td></<tr></table>"
    '                        Case Else
    '                    End Select
    '                End If

    '            Next

    '            If dt.Rows.Count > 0 Then

    '                Dim NewDg As New DataGrid
    '                NewDg.AutoGenerateColumns = False

    '                Dim num As New BoundColumn
    '                num.DataField = "NUM"
    '                num.HeaderText = "N°"
    '                num.HeaderStyle.Width = 50
    '                NewDg.Columns.Add(num)

    '                Dim tipo As New BoundColumn
    '                tipo.DataField = "TIPO_INT"
    '                tipo.HeaderText = "CRITICITA'"
    '                tipo.HeaderStyle.Width = 50
    '                tipo.ItemStyle.HorizontalAlign = HorizontalAlign.Center
    '                NewDg.Columns.Add(tipo)

    '                Dim tipo0 As New BoundColumn
    '                tipo0.DataField = "TIPO0"
    '                tipo0.HeaderText = "CATEGORIA"
    '                tipo0.HeaderStyle.Width = 100
    '                NewDg.Columns.Add(tipo0)

    '                Dim tipo1 As New BoundColumn
    '                tipo1.DataField = "TIPO1"
    '                tipo1.HeaderText = "CATEGORIA 1"
    '                tipo1.HeaderStyle.Width = 100
    '                NewDg.Columns.Add(tipo1)

    '                Dim tipo2 As New BoundColumn
    '                tipo2.DataField = "TIPO2"
    '                tipo2.HeaderText = "CATEGORIA 2"
    '                tipo2.HeaderStyle.Width = 100
    '                NewDg.Columns.Add(tipo2)


    '                Dim tipo3 As New BoundColumn
    '                tipo3.DataField = "TIPO3"
    '                tipo3.HeaderText = "CATEGORIA 3"
    '                tipo3.HeaderStyle.Width = 100
    '                NewDg.Columns.Add(tipo3)

    '                Dim tipo4 As New BoundColumn
    '                tipo4.DataField = "TIPO4"
    '                tipo4.HeaderText = "CATEGORIA 4"
    '                tipo4.HeaderStyle.Width = 100
    '                NewDg.Columns.Add(tipo4)

    '                Dim stato As New BoundColumn
    '                stato.DataField = "STATO"
    '                stato.HeaderText = "STATO"
    '                stato.HeaderStyle.Width = 100
    '                NewDg.Columns.Add(stato)

    '                Dim indirizzo As New BoundColumn
    '                indirizzo.DataField = "INDIRIZZO"
    '                indirizzo.HeaderText = "INDIRIZZO"
    '                indirizzo.HeaderStyle.Width = 100
    '                NewDg.Columns.Add(indirizzo)

    '                Dim richiedente As New BoundColumn
    '                richiedente.DataField = "RICHIEDENTE"
    '                richiedente.HeaderText = "RICHIEDENTE"
    '                richiedente.HeaderStyle.Width = 100
    '                NewDg.Columns.Add(richiedente)

    '                Dim codice_ru As New BoundColumn
    '                codice_ru.DataField = "CODICE_RU"
    '                codice_ru.HeaderText = "CODICE CONTRATTO"
    '                codice_ru.HeaderStyle.Width = 150
    '                NewDg.Columns.Add(codice_ru)

    '                Dim data_inserimento As New BoundColumn
    '                data_inserimento.DataField = "DATA_INSERIMENTO"
    '                data_inserimento.HeaderText = "DATA INS."
    '                data_inserimento.HeaderStyle.Width = 100
    '                NewDg.Columns.Add(data_inserimento)

    '                Dim descrizione As New BoundColumn
    '                descrizione.DataField = "DESCRIZIONE"
    '                descrizione.HeaderText = "DESCRIZIONE"
    '                descrizione.HeaderStyle.Width = 460
    '                NewDg.Columns.Add(descrizione)

    '                Dim filiale As New BoundColumn
    '                filiale.DataField = "FILIALE"
    '                filiale.HeaderText = "SEDE TERRITORIALE"
    '                filiale.HeaderStyle.Width = 100
    '                NewDg.Columns.Add(filiale)

    '                Dim note_c As New BoundColumn
    '                note_c.DataField = "NOTE_C"
    '                note_c.HeaderText = "NOTE DI CHIUSURA"
    '                note_c.HeaderStyle.Width = 200
    '                NewDg.Columns.Add(note_c)

    '                'Dim figli As New BoundColumn
    '                'figli.DataField = "FIGLI"
    '                'figli.HeaderText = "TICKET FIGLI"
    '                'figli.HeaderStyle.Width = 100
    '                'NewDg.Columns.Add(figli)

    '                NewDg.Width = Unit.Percentage(100)

    '                NewDg.DataSource = dt
    '                NewDg.DataBind()

    '                SetProps(NewDg)

    '                Dim sw As New System.IO.StringWriter
    '                Dim htw As New System.Web.UI.HtmlTextWriter(sw)
    '                NewDg.RenderControl(htw)

    '                Dim DivStart As String = "<DIV id='nipote" + e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID")).Text + "' style='DISPLAY: none;'>"
    '                Dim DivBody As String = sw.ToString().Replace("cellpadding=""0""", "cellpadding=""1""").Replace("cellspacing=""0""", "cellspacing=""1""").Replace("border=""1""", "border=""0""")
    '                Dim DivEnd As String = "</DIV>"
    '                Dim FullDIV As String = DivStart + DivBody + DivEnd

    '                Dim LastCellPosition As Integer = e.Item.Cells.Count - 2
    '                Dim NewCellPosition As Integer = e.Item.Cells.Count

    '                e.Item.Cells(0).ID = "CellInfo" + e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID")).Text



    '                If e.Item.ItemType = ListItemType.Item Then
    '                    e.Item.Cells(LastCellPosition).Text() = e.Item.Cells(LastCellPosition).Text + "</td><tr><td bgcolor='#ffffff' style='width:10px;'></td><td bgcolor='#ffffff' style='width:50px;'></td><td colspan='" + NewCellPosition.ToString() + "'>" + FullDIV
    '                Else
    '                    e.Item.Cells(LastCellPosition).Text = e.Item.Cells(LastCellPosition).Text + "</td><tr><td bgcolor='#ffffff' style='width:10px;'></td><td bgcolor='#ffffff' style='width:50px;'></td><td colspan='" + NewCellPosition.ToString() + "'>" + FullDIV
    '                End If

    '                e.Item.Cells(0).Attributes("onclick") = "HideShowPanel('nipote" + e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID")).Text + "'); ChangePlusMinusText('" + e.Item.Cells(0).ClientID + "'); "
    '                e.Item.Cells(0).Attributes("onmouseover") = "this.style.cursor='pointer'"
    '                e.Item.Cells(0).Attributes("onmouseout") = "this.style.cursor='pointer'"
    '            Else
    '                e.Item.Cells(0).Text = ""
    '            End If

    '            'If e.Item.Cells(TrovaIndiceColonna(DataGrid1, "FL_ANNULLATA")).Text = 1 Then
    '            '    e.Item.BackColor = Drawing.Color.Red
    '            'End If
    '        Else
    '            e.Item.Cells(0).Text = ""
    '        End If
    '    Else
    '        e.Item.Cells(0).Text = ""
    '    End If

    '    If e.Item.ItemType = ListItemType.Item Then
    '        CType(e.Item.FindControl("CheckBox1"), CheckBox).Attributes.Add("onchange", "javascript:document.getElementById('HiddenFieldRigaSelezionata').value='" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID")).Text & "';")
    '        CType(e.Item.FindControl("CheckBox1"), CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('HiddenFieldRigaSelezionata').value='" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID")).Text & "';")
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------  
    '        'e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        'e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white';}")
    '        'e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='orange';document.getElementById('CPContenuto_TextBox1').value='Hai selezionato la segnalazione N°" & e.Item.Cells(1).Text & "';document.getElementById('idSegnalazione').value='" & e.Item.Cells(0).Text & "';")
    '        'e.Item.Attributes.Add("onDblclick", "Apri();")
    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        CType(e.Item.FindControl("CheckBox1"), CheckBox).Attributes.Add("onchange", "javascript:document.getElementById('HiddenFieldRigaSelezionata').value='" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID")).Text & "';")
    '        CType(e.Item.FindControl("CheckBox1"), CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('HiddenFieldRigaSelezionata').value='" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID")).Text & "';")
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        'e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        'e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white';}")
    '        'e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='orange';document.getElementById('CPContenuto_TextBox1').value='Hai selezionato la segnalazione N°" & e.Item.Cells(1).Text & "';document.getElementById('idSegnalazione').value='" & e.Item.Cells(0).Text & "';")
    '        'e.Item.Attributes.Add("onDblclick", "Apri();")
    '    End If

    '    If e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO")).Text = "1" Or e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO")).Text = "6" Then
    '        Select Case e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID_PERICOLO_SEGNALAZIONE")).Text
    '            Case "1"
    '                e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                    & "<tr><td><img src=""Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
    '                    & "<td>" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text & "</td></<tr></table>"
    '            Case "2"
    '                e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                    & "<tr><td><img src=""Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
    '                    & "<td>" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text & "</td></<tr></table>"
    '            Case "3"
    '                e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                    & "<tr><td><img src=""Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
    '                    & "<td>" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text & "</td></<tr></table>"
    '            Case "4"
    '                e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                    & "<tr><td><img src=""Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
    '                    & "<td>" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text & "</td></<tr></table>"
    '            Case "0"
    '                e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
    '                    & "<tr><td><img src=""Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
    '                    & "<td>" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text & "</td></<tr></table>"
    '            Case Else
    '        End Select
    '    End If

    'End Sub

    Protected Sub RadGridSegnalazioni_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridSegnalazioni.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridSegnalazioni.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            Dim idSegnalazionePadre As String = 0
            If dataItem("ID_SEGNALAZIONE_PADRE").Text = "" Then
                idSegnalazionePadre = "0"
            Else
                idSegnalazionePadre = dataItem("ID_SEGNALAZIONE_PADRE").Text
            End If
            If idSegnalazionePadre = "0" Then
                CType(dataItem.FindControl("CheckBox1"), CheckBox).Attributes.Add("onchange", "javascript:document.getElementById('HiddenFieldRigaSelezionata').value='" & dataItem("id").Text & "';")
                CType(dataItem.FindControl("CheckBox1"), CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('HiddenFieldRigaSelezionata').value='" & dataItem("id").Text & "';")
            End If
            If dataItem("TIPO").Text = "1" Or dataItem("TIPO").Text = "6" Then
                Select Case dataItem("ID_PERICOLO_SEGNALAZIONE").Text
                    Case "1"
                        dataItem("TIPO_INT").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-white-128.png"
                        dataItem("TIPO_INT").Controls.Add(img)
                    Case "2"
                        dataItem("TIPO_INT").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-green-128.png"
                        dataItem("TIPO_INT").Controls.Add(img)
                    Case "3"
                        dataItem("TIPO_INT").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-yellow-128.png"
                        dataItem("TIPO_INT").Controls.Add(img)
                    Case "4"
                        dataItem("TIPO_INT").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-red-128.png"
                        dataItem("TIPO_INT").Controls.Add(img)
                    Case "0"
                        dataItem("TIPO_INT").Controls.Clear()
                        Dim img As Image = New Image()
                        img.ImageUrl = "Immagini/Ball-blue-128.png"
                        dataItem("TIPO_INT").Controls.Add(img)
                    Case Else
                End Select

            End If
        End If
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub

    'Public Sub SetProps(ByVal DG As System.Web.UI.WebControls.DataGrid)
    '    '************************************************************************** 
    '    DG.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8")
    '    DG.Font.Bold = False
    '    DG.Font.Name = "Arial"

    '    '******************************Professional 2********************************* 

    '    'Border Props 
    '    DG.GridLines = GridLines.Both
    '    DG.CellPadding = 0
    '    DG.CellSpacing = 0
    '    DG.BorderColor = System.Drawing.Color.FromName("#CCCCCC")
    '    DG.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1)


    '    'Header Props 
    '    DG.HeaderStyle.BackColor = System.Drawing.Color.Gray
    '    DG.HeaderStyle.ForeColor = System.Drawing.Color.Black
    '    DG.HeaderStyle.Font.Bold = True
    '    DG.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
    '    DG.HeaderStyle.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8")
    '    DG.HeaderStyle.Font.Name = "Arial"
    '    DG.ItemStyle.BackColor = System.Drawing.Color.LightGray
    'End Sub


    'Protected Sub RadGridSegnalazioni_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGridSegnalazioni.PageIndexChanged
    '    Try
    '        AggiornaDataTableAggregazioni()
    '        'If e.NewPageIndex >= 0 Then
    '        '    RadGridSegnalazioni.CurrentPageIndex = e.NewPageIndex
    '        '    If CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable).Rows.Count > 0 Then
    '        '        'RadGridSegnalazioni.DataSource = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
    '        '        RadGridSegnalazioni.Rebind()
    '        '        MultiViewRicerca.ActiveViewIndex = 1
    '        '        MultiViewBottoni.ActiveViewIndex = 1
    '        '    End If
    '        'End If
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: Morosità - Ricerca Segnalazioni Unione - RadGridSegnalazioni_PageIndexChanged - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    'Protected Sub DataGridSegnalaz_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridSegnalaz.PageIndexChanged
    '    'If e.NewPageIndex >= 0 Then
    '    '    DataGridSegnalaz.CurrentPageIndex = e.NewPageIndex
    '    '    CaricaRisultati()
    '    'End If

    '    Try
    '        AggiornaDataTableAggregazioni()
    '        If e.NewPageIndex >= 0 Then
    '            DataGridSegnalaz.CurrentPageIndex = e.NewPageIndex
    '            If CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable).Rows.Count > 0 Then
    '                DataGridSegnalaz.DataSource = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
    '                DataGridSegnalaz.DataBind()
    '                MultiViewRicerca.ActiveViewIndex = 1
    '                MultiViewBottoni.ActiveViewIndex = 1
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: Morosità - Ricerca Segnalazioni Unione - DataGridContratti_PageIndexChanged - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try

    'End Sub
    'Protected Sub AggiornaDataTableAggregazioni()
    '    Try
    '        Dim dt As Data.DataTable = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
    '        Dim row As Data.DataRow
    '        For i As Integer = 0 To DataGridSegnalaz.Items.Count - 1
    '            row = dt.Select("id = " & DataGridSegnalaz.Items(i).Cells(par.IndDGC(DataGridSegnalaz, "ID")).Text)(0)
    '            If CType(DataGridSegnalaz.Items(i).FindControl("CheckBox1"), CheckBox).Checked = True Then
    '                row.Item("CHECK1") = "TRUE"
    '            Else
    '                row.Item("CHECK1") = "FALSE"
    '            End If
    '        Next
    '        Session.Item("AGGREGAZIONE_COMPLETA") = dt
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: Morosità - Ricerca Segnalazioni Unione - AggiornaDataTableAggregazione - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    Protected Sub CheckBox1_CheckedChanged1(sender As Object, e As System.EventArgs)
        If HiddenFieldRigaSelezionata.Value.ToString <> "0" Then
            Dim dt As Data.DataTable = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
            Dim row As Data.DataRow
            row = dt.Select("id = " & HiddenFieldRigaSelezionata.Value)(0)
            If row.Item("CHECK1").ToString = "TRUE" Then
                row.Item("CHECK1") = "FALSE"
            Else
                row.Item("CHECK1") = "TRUE"
            End If
            Session.Item("AGGREGAZIONE_COMPLETA") = dt
            RadGridSegnalazioni.Rebind()
        End If
        MultiViewRicerca.ActiveViewIndex = 1
        MultiViewBottoni.ActiveViewIndex = 1
    End Sub

    'Protected Sub AggiornaDataTableAggregazioni()
    '    Try
    '        Dim dt As Data.DataTable = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
    '        Dim row As Data.DataRow
    '        Dim dataItem As GridDataItem
    '        For i As Integer = 0 To RadGridSegnalazioni.MasterTableView.Items.Count - 1
    '            dataItem = RadGridSegnalazioni.Items(i)
    '            row = dt.Select("id = " & dataItem("ID").Text)(0)
    '            If CType(RadGridSegnalazioni.Items(i).FindControl("CheckBox1"), CheckBox).Checked = True Then
    '                row.Item("CHECK1") = "TRUE"
    '            Else
    '                row.Item("CHECK1") = "FALSE"
    '            End If
    '        Next
    '        Session.Item("AGGREGAZIONE_COMPLETA") = dt
    '        RadGridSegnalazioni.Rebind()
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: Morosità - Ricerca Segnalazioni Unione - AggiornaDataTableAggregazione - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    Protected Sub RadGridSegnalazioni_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGridSegnalazioni.PageIndexChanged
        'AggiornaDataTableAggregazioni()
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub

    Protected Sub RadGridSegnalazioni_PageSizeChanged(sender As Object, e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles RadGridSegnalazioni.PageSizeChanged
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub
    'Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
    '    If IsNumeric(idSegnalazione.Value) AndAlso idSegnalazione.Value <> "-1" Then
    '        Response.Redirect("Segnalazione.aspx?IDS=" & idSegnalazione.Value, False)
    '    Else
    '        par.modalDialogMessage("Apertura segnalazione selezionata", "E\' necessario selezionare una segnalazione.", Me.Page, "info", , )
    '        MultiViewRicerca.ActiveViewIndex = 1
    '        MultiViewBottoni.ActiveViewIndex = 1
    '    End If
    'End Sub
    'Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
    '    Dim xls As New ExcelSiSol
    '    Dim nomeFile = xls.EsportaExcelDaDataGridWithDTColor(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", DataGridSegnalaz, CType(Session.Item("DataGridSegnalaz"), Data.DataTable), True, , , "CRITICITA'")
    '    If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
    '        par.modalDialogMessage("Agenda", "Esportazione eseguita correttamente.", Page, "successo", "..\/FileTemp\/" & nomeFile)
    '    Else
    '        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
    '    End If
    '    'Response.Write("<script>window.open('../FileTemp/" & par.EsportaExcelDaDTWithDatagrid(CType(Session.Item("DataGridSegnalaz"), Data.DataTable), Me.DataGridSegnalaz, "ExportSegnalazioni") & "');</script>")
    'End Sub
    Protected Sub btnNuovaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnNuovaRicerca.Click
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=0;", True)
        MultiViewRicerca.ActiveViewIndex = 0
        MultiViewBottoni.ActiveViewIndex = 0
    End Sub
    Private Sub CaricaRisultati()
        Try
            Dim dt As New Data.DataTable
            Dim CondizioneRicerca As String = ""
            If Trim(TextBoxNumero.Text) <> "" AndAlso IsNumeric(TextBoxNumero.Text) Then
                CondizioneRicerca &= " AND SEGNALAZIONI.ID=" & TextBoxNumero.Text
            End If
            Dim listaTipologie As String = ""
            Dim contaTipologie As Integer = 0
            For Each item As ListItem In CheckBoxListTipoSegnalazione.Items
                If item.Selected = True Then
                    contaTipologie += 1
                    If listaTipologie = "" Then
                        listaTipologie = item.Value
                    Else
                        listaTipologie &= "," & item.Value
                    End If
                End If
            Next
            If contaTipologie = 1 Then
                If listaTipologie <> "" Then
                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGNALAZIONE in (" & listaTipologie & ")"
                End If
                If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue
                End If
                If IsNumeric(cmbTipoSegnalazioneLivello2.SelectedValue) AndAlso cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" Then
                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue
                End If
                If IsNumeric(cmbTipoSegnalazioneLivello3.SelectedValue) AndAlso cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" Then
                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue
                End If
                If IsNumeric(cmbTipoSegnalazioneLivello4.SelectedValue) AndAlso cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" Then
                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_4=" & cmbTipoSegnalazioneLivello4.SelectedValue
                End If
            ElseIf contaTipologie > 1 Then
                If listaTipologie <> "" Then
                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGNALAZIONE in (" & listaTipologie & ")"
                End If
            End If



            If listaTipologie = "1" Or listaTipologie = "6" Then
                Dim urgenza As String = ""
                Select Case UCase(DropDownListUrgenza.SelectedValue)
                    Case "---"
                        urgenza = "-1"
                    Case "BIANCO"
                        urgenza = "1"
                    Case "VERDE"
                        urgenza = "2"
                    Case "GIALLO"
                        urgenza = "3"
                    Case "ROSSO"
                        urgenza = "4"
                    Case "BLU"
                        urgenza = "0"
                End Select
                If urgenza <> "-1" Then
                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE=" & urgenza
                End If
            End If
            Dim dataMin As String = ""
            Dim dataMax As String = ""
            If txtDal.Text <> "" Then
                dataMin = par.AggiustaData(txtDal.Text)
                If TextBoxOreDal.Text <> "" Then
                    dataMin &= TextBoxOreDal.Text.ToString.PadLeft(2, "0")
                    If TextBoxMinutiDal.Text <> "" Then
                        dataMin &= TextBoxMinutiDal.Text.ToString.PadLeft(2, "0")
                    Else
                        dataMin &= "00"
                    End If
                Else
                    dataMin &= "0000"
                End If
            End If
            If dataMin <> "" Then
                CondizioneRicerca &= " AND SUBSTR(DATA_ORA_RICHIESTA,1,12)>='" & dataMin & "'  "
            End If
            If txtAl.Text <> "" Then
                dataMax = par.AggiustaData(txtAl.Text)
                If TextBoxOreAl.Text <> "" Then
                    dataMax &= TextBoxOreAl.Text.ToString.PadLeft(2, "0")
                    If TextBoxMinutiAl.Text <> "" Then
                        dataMax &= TextBoxMinutiAl.Text.ToString.PadLeft(2, "0")
                    Else
                        dataMax &= "00"
                    End If
                Else
                    dataMax &= "2400"
                End If
            End If
            If dataMax <> "" Then
                CondizioneRicerca &= " AND SUBSTR(DATA_ORA_RICHIESTA,1,12)<='" & dataMax & "' "
            End If
            Dim listaSedi As String = ""
            For Each item As ListItem In CheckBoxListSedi.Items
                If item.Selected = True Then
                    If listaSedi = "" Then
                        listaSedi = item.Value
                    Else
                        listaSedi &= "," & item.Value
                    End If
                End If
            Next
            If listaSedi <> "" Then
                CondizioneRicerca &= " AND TAB_FILIALI.ID in (" & listaSedi & ") "
            End If

            If cmbEdificio.SelectedValue <> "-1" Then
                CondizioneRicerca &= " AND SEGNALAZIONI.ID_EDIFICIO = " & cmbEdificio.SelectedValue
            End If
            If cmbComplesso.SelectedValue <> "-1" Then
                CondizioneRicerca &= " and segnalazioni.id_edificio in (select id from siscom_mi.edifici where id_complesso= " & cmbComplesso.SelectedValue & ")"
            End If
            If Trim(txtSegnalante.Text) <> "" Then
                Dim listaSegnalante As String()
                listaSegnalante = txtSegnalante.Text.ToString.Split(" ")
                Dim segnalantiCompleta As String = txtSegnalante.Text.ToString.Replace(" ", "")
                CondizioneRicerca &= " AND ("
                If listaSegnalante.Length = 1 Then
                    CondizioneRicerca &= " upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(0))) & "' "
                    CondizioneRicerca &= " OR upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(0))) & "' "
                    CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','')||REPLACE(UPPER(SEGNALAZIONI.NOME),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                    CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.NOME),' ','')||REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                Else
                    CondizioneRicerca &= "("
                    For i As Integer = 0 To listaSegnalante.Length - 1 Step 1
                        If i = 0 Then
                            CondizioneRicerca &= " upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                        Else
                            CondizioneRicerca &= " OR upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                        End If
                    Next
                    CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','')||REPLACE(UPPER(SEGNALAZIONI.NOME),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                    CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.NOME),' ','')||REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                    CondizioneRicerca &= ") AND ("
                    For i As Integer = 0 To listaSegnalante.Length - 1 Step 1
                        If i = 0 Then
                            CondizioneRicerca &= " upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                        Else
                            CondizioneRicerca &= " OR upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                        End If
                    Next
                    CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','')||REPLACE(UPPER(SEGNALAZIONI.NOME),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                    CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.NOME),' ','')||REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                    CondizioneRicerca &= ")"
                End If
                CondizioneRicerca &= " )"
            End If

            If cmbFornitori.SelectedValue <> "-1" Then
                CondizioneRicerca &= " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE APPALTI.ID_FORNITORE=" & cmbFornitori.SelectedValue & "))"
            End If

            Dim listaStati As String = ""
            For Each elemento As ListItem In CheckBoxListStato.Items
                If elemento.Selected = True Then
                    If listaStati = "" Then
                        listaStati = elemento.Value
                    Else
                        listaStati &= "," & elemento.Value
                    End If
                End If
            Next
            If listaStati <> "" Then
                CondizioneRicerca &= "AND SEGNALAZIONI.ID_STATO IN (" & listaStati & ") "
            End If
            Dim ordine As String = ""
            If Not IsNothing(Request.QueryString("ORD")) Then
                ordine = Request.QueryString("ORD")
            End If
            Dim condizioneOrdinamento As String = "ORDER BY ID_PERICOLO_SEGNALAZIONE DESC,DATA_ORA_RICHIESTA DESC"
            'Select Case ordine
            '    Case "0"
            '        'STATO
            '        condizioneOrdinamento = "ORDER BY iD_STATO,ID_PERICOLO_SEGNALAZIONE DESC,ID_TIPO_SEGNALAZIONE,DATA_ORA_RICHIESTA ASC"
            '    Case "1"
            '        'URGENZA
            '        condizioneOrdinamento = "ORDER BY ID_PERICOLO_SEGNALAZIONE DESC,ID_STATO,ID_TIPO_SEGNALAZIONE,DATA_ORA_RICHIESTA ASC"
            '    Case "2"
            '        'TIPO SEGNALAZIONE
            '        condizioneOrdinamento = "ORDER BY ID_TIPO_SEGNALAZIONE,ID_STATO,ID_PERICOLO_SEGNALAZIONE DESC,DATA_ORA_RICHIESTA ASC"
            '    Case Else
            'End Select

            Dim condizionePadri As String = ""
            If CheckBoxSegnalazioniFigli.Checked = True Then
                condizionePadri = " AND EXISTS (SELECT B.ID_sEGNALAZIONE_PADRE FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID)"
            End If

            par.cmd.CommandText = "SELECT SEGNALAZIONI.ID, " _
                            & "SEGNALAZIONI.ID AS NUM, " _
                            & "SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
                            & "'' AS TIPO_INT, " _
                            & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & "TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                            & "EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & "COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
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
                            & "TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                            & "REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & "NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                            & "(CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & ") ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & ",'FALSE' AS CHECK1 " _
                            & ",(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
                            & ",(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & "FROM siscom_mi.tab_stati_segnalazioni, " _
                            & "siscom_mi.segnalazioni, " _
                            & "siscom_mi.tab_filiali, " _
                            & "siscom_mi.edifici, " _
                            & "siscom_mi.unita_immobiliari, " _
                            & "siscom_mi.TIPOLOGIE_GUASTI, " _
                            & "OPERATORI " _
                            & "WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                            & "AND segnalazioni.id_stato <> -1 " _
                            & "AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & "AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                            & "AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                            & "AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                            & "AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
                            & CondizioneRicerca _
                            & condizionePadri _
                            & " and id_Segnalazione_padre is null " _
                            & condizioneOrdinamento

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)
            'RadGridSegnalazioni.DataSource = dt
            Session.Item("AGGREGAZIONE_COMPLETA") = dt
            RadGridSegnalazioni.CurrentPageIndex = 0
            RadGridSegnalazioni.Rebind()
            If dt.Rows.Count > 1 Then
                lblRisultati.Text = "Trovate - " & dt.Rows.Count & " segnalazioni"
            ElseIf dt.Rows.Count = 1 Then
                lblRisultati.Text = "Trovata - " & dt.Rows.Count & " segnalazione"
            ElseIf dt.Rows.Count = 0 Then
                lblRisultati.Text = "Nessuna segnalazione trovata"
            End If
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni Unione - BindGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadGridSegnalazioni_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridSegnalazioni.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
    End Sub

    Protected Sub RadGridSegnalazioni_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadGridSegnalazioni.DetailTableDataBind
        'Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        'Select Case e.DetailTableView.Name
        '    Case "Orders"
        '        If True Then
        '            Dim query As String = "SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & dataItem("ID").Text
        '            e.DetailTableView.DataSource = GetDataTable(query)
        '            Exit Select
        '        End If
        'End Select
        Try
            Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
            Select Case e.DetailTableView.Name
                Case "Dettagli"
                    If True Then

                        Dim condizioneOrdinamento As String = "ORDER BY ID_PERICOLO_SEGNALAZIONE DESC,DATA_ORA_RICHIESTA DESC"


                        Dim CondizioneRicerca As String = ""
                        'If Trim(TextBoxNumero.Text) <> "" AndAlso IsNumeric(TextBoxNumero.Text) Then
                        '    CondizioneRicerca &= " AND SEGNALAZIONI.ID=" & TextBoxNumero.Text
                        'End If
                        Dim listaTipologie As String = ""
                        Dim contaTipologie As Integer = 0
                        For Each item As ListItem In CheckBoxListTipoSegnalazione.Items
                            If item.Selected = True Then
                                contaTipologie += 1
                                If listaTipologie = "" Then
                                    listaTipologie = item.Value
                                Else
                                    listaTipologie &= "," & item.Value
                                End If
                            End If
                        Next
                        If contaTipologie = 1 Then
                            If listaTipologie <> "" Then
                                CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGNALAZIONE in (" & listaTipologie & ")"
                            End If
                            If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
                                CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue
                            End If
                            If IsNumeric(cmbTipoSegnalazioneLivello2.SelectedValue) AndAlso cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" Then
                                CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue
                            End If
                            If IsNumeric(cmbTipoSegnalazioneLivello3.SelectedValue) AndAlso cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" Then
                                CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue
                            End If
                            If IsNumeric(cmbTipoSegnalazioneLivello4.SelectedValue) AndAlso cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" Then
                                CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_4=" & cmbTipoSegnalazioneLivello4.SelectedValue
                            End If
                        ElseIf contaTipologie > 1 Then
                            If listaTipologie <> "" Then
                                CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGNALAZIONE in (" & listaTipologie & ")"
                            End If
                        End If



                        If listaTipologie = "1" Or listaTipologie = "6" Then
                            Dim urgenza As String = ""
                            Select Case UCase(DropDownListUrgenza.SelectedValue)
                                Case "---"
                                    urgenza = "-1"
                                Case "BIANCO"
                                    urgenza = "1"
                                Case "VERDE"
                                    urgenza = "2"
                                Case "GIALLO"
                                    urgenza = "3"
                                Case "ROSSO"
                                    urgenza = "4"
                                Case "BLU"
                                    urgenza = "0"
                            End Select
                            If urgenza <> "-1" Then
                                CondizioneRicerca &= " AND SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE=" & urgenza
                            End If
                        End If
                        Dim dataMin As String = ""
                        Dim dataMax As String = ""
                        If txtDal.Text <> "" Then
                            dataMin = par.AggiustaData(txtDal.Text)
                            If TextBoxOreDal.Text <> "" Then
                                dataMin &= TextBoxOreDal.Text.ToString.PadLeft(2, "0")
                                If TextBoxMinutiDal.Text <> "" Then
                                    dataMin &= TextBoxMinutiDal.Text.ToString.PadLeft(2, "0")
                                Else
                                    dataMin &= "00"
                                End If
                            Else
                                dataMin &= "0000"
                            End If
                        End If
                        If dataMin <> "" Then
                            CondizioneRicerca &= " AND SUBSTR(DATA_ORA_RICHIESTA,1,12)>='" & dataMin & "'  "
                        End If
                        If txtAl.Text <> "" Then
                            dataMax = par.AggiustaData(txtAl.Text)
                            If TextBoxOreAl.Text <> "" Then
                                dataMax &= TextBoxOreAl.Text.ToString.PadLeft(2, "0")
                                If TextBoxMinutiAl.Text <> "" Then
                                    dataMax &= TextBoxMinutiAl.Text.ToString.PadLeft(2, "0")
                                Else
                                    dataMax &= "00"
                                End If
                            Else
                                dataMax &= "2400"
                            End If
                        End If
                        If dataMax <> "" Then
                            CondizioneRicerca &= " AND SUBSTR(DATA_ORA_RICHIESTA,1,12)<='" & dataMax & "' "
                        End If
                        Dim listaSedi As String = ""
                        For Each item As ListItem In CheckBoxListSedi.Items
                            If item.Selected = True Then
                                If listaSedi = "" Then
                                    listaSedi = item.Value
                                Else
                                    listaSedi &= "," & item.Value
                                End If
                            End If
                        Next
                        If listaSedi <> "" Then
                            CondizioneRicerca &= " AND TAB_FILIALI.ID in (" & listaSedi & ") "
                        End If

                        If cmbEdificio.SelectedValue <> "-1" Then
                            CondizioneRicerca &= " AND SEGNALAZIONI.ID_EDIFICIO = " & cmbEdificio.SelectedValue
                        End If
                        If cmbComplesso.SelectedValue <> "-1" Then
                            CondizioneRicerca &= " and segnalazioni.id_edificio in (select id from siscom_mi.edifici where id_complesso= " & cmbComplesso.SelectedValue & ")"
                        End If
                        If Trim(txtSegnalante.Text) <> "" Then
                            Dim segnalantiCompleta As String = txtSegnalante.Text.ToString.Replace(" ", "")
                            Dim listaSegnalante As String()
                            listaSegnalante = txtSegnalante.Text.ToString.Split(" ")

                            CondizioneRicerca &= " AND ("
                            If listaSegnalante.Length = 1 Then
                                CondizioneRicerca &= " upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(0))) & "' "
                                CondizioneRicerca &= " OR upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(0))) & "' "
                                CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','')||REPLACE(UPPER(SEGNALAZIONI.NOME),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                                CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.NOME),' ','')||REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                            Else
                                CondizioneRicerca &= "("
                                For i As Integer = 0 To listaSegnalante.Length - 1 Step 1
                                    If i = 0 Then
                                        CondizioneRicerca &= " upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                                    Else
                                        CondizioneRicerca &= " OR upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                                    End If
                                Next
                                CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','')||REPLACE(UPPER(SEGNALAZIONI.NOME),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                                CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.NOME),' ','')||REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                                CondizioneRicerca &= ") AND ("
                                For i As Integer = 0 To listaSegnalante.Length - 1 Step 1
                                    If i = 0 Then
                                        CondizioneRicerca &= " upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                                    Else
                                        CondizioneRicerca &= " OR upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                                    End If
                                Next
                                CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','')||REPLACE(UPPER(SEGNALAZIONI.NOME),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                                CondizioneRicerca &= " OR REPLACE(UPPER(SEGNALAZIONI.NOME),' ','')||REPLACE(UPPER(SEGNALAZIONI.COGNOME_RS),' ','') LIKE '%" & segnalantiCompleta.ToUpper & "%' "
                                CondizioneRicerca &= ")"
                            End If
                            CondizioneRicerca &= " )"
                        End If

                        If cmbFornitori.SelectedValue <> "-1" Then
                            CondizioneRicerca &= " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE APPALTI.ID_FORNITORE=" & cmbFornitori.SelectedValue & "))"
                        End If

                        Dim listaStati As String = ""
                        For Each elemento As ListItem In CheckBoxListStato.Items
                            If elemento.Selected = True Then
                                If listaStati = "" Then
                                    listaStati = elemento.Value
                                Else
                                    listaStati &= "," & elemento.Value
                                End If
                            End If
                        Next
                        If listaStati <> "" Then
                            CondizioneRicerca &= "AND SEGNALAZIONI.ID_STATO IN (" & listaStati & ") "
                        End If


                        par.cmd.CommandText = "SELECT SEGNALAZIONI.ID, " _
                            & "SEGNALAZIONI.ID AS NUM, " _
                            & "SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
                            & "'' AS TIPO_INT, " _
                            & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & "TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                            & "EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & "COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
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
                            & "TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                            & "REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & "NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                            & "(CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & ") ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & ",'FALSE' AS CHECK1 " _
                            & ",(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & ",'' as figli2 " _
                            & ",NVL(ID_SEGNALAZIONE_PADRE,0) AS ID_sEGNALAZIONE_PADRE " _
                            & "FROM siscom_mi.tab_stati_segnalazioni, " _
                            & "siscom_mi.segnalazioni, " _
                            & "siscom_mi.tab_filiali, " _
                            & "siscom_mi.edifici, " _
                            & "siscom_mi.unita_immobiliari, " _
                            & "siscom_mi.TIPOLOGIE_GUASTI, " _
                            & "OPERATORI " _
                            & "WHERE ID_SEGNALAZIONE_PADRE=" & dataItem("id").Text _
                            & " and tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                            & " AND segnalazioni.id_stato <> -1 " _
                            & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                            & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                            & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                            & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
                            & CondizioneRicerca _
                            & condizioneOrdinamento

                        e.DetailTableView.DataSource = GetDataTable(par.cmd.CommandText)
                        MultiViewRicerca.ActiveViewIndex = 1
                        MultiViewBottoni.ActiveViewIndex = 1
                        Exit Select
                    End If
            End Select
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=1;", True)
        Catch ex As Exception

        End Try
    End Sub
    Public Function GetDataTable(query As String) As Data.DataTable
        Dim myDataTable As New Data.DataTable
        par.cmd.CommandText = query
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(myDataTable)
        Return myDataTable
    End Function
    Private Sub CaricaTipologieLivello1()
        Try

            Dim tipologiaSelezionata As String = ""
            Dim contaTip As Integer = 0
            For Each Item As ListItem In CheckBoxListTipoSegnalazione.Items
                If Item.Selected = True Then
                    tipologiaSelezionata = Item.Value
                    contaTip += 1
                End If
            Next
            If contaTip = 1 Then
                'Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE=" & tipologiaSelezionata & " ORDER BY DESCRIZIONE"
                Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE=" & tipologiaSelezionata & " ORDER BY DESCRIZIONE"
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
            'connData.apri(False)
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello3, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
            If cmbTipoSegnalazioneLivello3.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello3.Items.FindByValue("-1")) Then
                    cmbTipoSegnalazioneLivello3.Items.Remove(cmbTipoSegnalazioneLivello3.Items.FindByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello4.Items.Clear()
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
            'connData.apri(False)
            par.caricaComboBox(query, cmbTipoSegnalazioneLivello4, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
            If cmbTipoSegnalazioneLivello4.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello4.Items.FindByValue("-1")) Then
                    cmbTipoSegnalazioneLivello4.Items.Remove(cmbTipoSegnalazioneLivello4.Items.FindByValue("-1"))
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaTipologieLivello4 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello2.SelectedIndexChanged
        CaricaTipologieLivello3()
    End Sub
    Protected Sub cmbTipoSegnalazioneLivello3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello3.SelectedIndexChanged
        CaricaTipologieLivello4()
    End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub
    Protected Sub CheckBoxListSedi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListSedi.SelectedIndexChanged
        caricaComplessi()
        caricaEdifici()
        cmbComplesso.Focus()
    End Sub

    Protected Sub CheckBoxListTipoSegnalazione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListTipoSegnalazione.SelectedIndexChanged
        Dim contaSelezionati As Integer = 0
        Dim listaTipologie As String = ""
        For Each item As ListItem In CheckBoxListTipoSegnalazione.Items
            If item.Selected = True Then
                contaSelezionati += 1
                listaTipologie = item.Value
            End If
        Next
        If contaSelezionati = 1 Then
            CaricaTipologieLivello1()
            If contaSelezionati = 1 Then
                If listaTipologie = "1" Or listaTipologie = "6" Then
                    DropDownListUrgenza.Enabled = True
                    TextBoxNumero.Focus()
                Else
                    DropDownListUrgenza.Enabled = False
                End If
            End If
        Else
            DropDownListUrgenza.Enabled = False
            cmbTipoSegnalazioneLivello1.Items.Clear()
            cmbTipoSegnalazioneLivello2.Items.Clear()
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
        End If
    End Sub

    
    Protected Sub RadButtonSelezionaTutti_Click(sender As Object, e As System.EventArgs) Handles RadButtonSelezionaTutti.Click
        Try
            Dim dt As Data.DataTable = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
            If dt.Rows.Count > 0 Then
                For Each riga As Data.DataRow In dt.Rows
                    riga.Item("CHECK1") = "TRUE"
                Next
            End If
            Session.Item("AGGREGAZIONE_COMPLETA") = dt
            RadGridSegnalazioni.Rebind()
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Morosità - RicercaAggregazione - ButtonSelezionaTutti_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonDeselezionaTutti_Click(sender As Object, e As System.EventArgs) Handles RadButtonDeselezionaTutti.Click
        Try
            Dim dt As Data.DataTable = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
            If dt.Rows.Count > 0 Then
                For Each riga As Data.DataRow In dt.Rows
                    riga.Item("CHECK1") = "FALSE"
                Next
            End If
            Session.Item("AGGREGAZIONE_COMPLETA") = dt
            RadGridSegnalazioni.Rebind()
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Morosità - RicercaDebitori - ButtonDeselezionaTutti_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub ButtonUnisciSegnalazioni_Click(sender As Object, e As System.EventArgs) Handles ButtonUnisciSegnalazioni.Click
        Try
            Dim listaSegnalazioni As String = ""
            Dim contaPadri As Integer = 0
            Dim tabella As Data.DataTable = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
            For Each Items As Data.DataRow In tabella.Rows
                If Items.Item("CHECK1").ToString = "TRUE" Then
                    If listaSegnalazioni = "" Then
                        listaSegnalazioni = Items.Item("ID").ToString
                        If Items.Item("FIGLI").ToString > 0 Then
                            contaPadri += 1
                        End If
                    Else
                        listaSegnalazioni &= "," & Items.Item("ID").ToString
                        If Items.Item("FIGLI").ToString > 0 Then
                            contaPadri += 1
                        End If
                    End If
                End If
            Next
            If contaPadri > 1 Then
                par.modalDialogMessage("Agenda e Segnalazioni", "E\' possibile selezionare al massimo un ticket ""padre""", Me.Page, "info")
            Else
                '************ UNIONE SEGNALAZIONI EDIFICI ************
                If listaSegnalazioni <> "" Then
                    Dim idPadre As Integer = 0
                    connData.apri(True)
                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (SELECT DISTINCT ID_SEGNALAZIONE_PADRE FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (SELECT ID FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE IN (" & listaSegnalazioni & "))) AND ID IN (" & listaSegnalazioni & ")"
                    idPadre = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If idPadre = 0 Then
                        par.cmd.CommandText = "SELECT MIN(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID IN (" & listaSegnalazioni & ")"
                        idPadre = par.IfNull(par.cmd.ExecuteScalar, 0)
                    End If

                    Dim pericoloIniziale As Integer = 0
                    Dim idStato As Integer = -1
                    par.cmd.CommandText = "SELECT ID_PERICOLO_SEGNALAZIONE_INIZ,id_stato FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idPadre
                    Dim lettore3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore3.Read Then
                        pericoloIniziale = par.IfNull(lettore3("ID_PERICOLO_SEGNALAZIONE_INIZ"), 0)
                        idStato = par.IfNull(lettore3("id_stato"), -1)
                    End If
                    lettore3.Close()


                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set " _
                        & " ID_SEGNALAZIONE_PADRE=" & idPadre _
                        & " ,id_stato=" & idStato _
                        & " WHERE ID IN (" & listaSegnalazioni & ") AND ID NOT IN (" & idPadre & ")"
                    par.cmd.ExecuteNonQuery()

                    Dim numeroFigliDopoUpdate As Integer = 0
                    par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_SEGNALAZIONE_PADRE=" & idPadre
                    numeroFigliDopoUpdate = par.IfNull(par.cmd.ExecuteScalar, 0)

                    Dim pericoloFinale As Integer = 0

                    Dim soglia As Integer = 0
                    par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='SOGLIA_TICKET_FIGLI'"
                    soglia = par.IfNull(par.cmd.ExecuteScalar, 10)

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
                        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_PERICOLO_SEGNALAZIONE=" & pericoloFinale _
                            & " WHERE ID=" & idPadre
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
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim idPericoloIniziale As Integer = 0
                    While lettore.Read
                        idPericoloIniziale = par.IfNull(lettore("ID_PERICOLO_SEGNALAZIONE"), 0)
                        If idPericoloIniziale <> pericoloFinale Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI set ID_PERICOLO_SEGNALAZIONE=" & pericoloFinale _
                            & " WHERE ID=" & par.IfNull(lettore("ID"), 0)
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.STATI_URGENZA_SEGNALAZIONI (" _
                                & " ID_SEGNALAZIONE, STATO_INIZIALE, STATO_FINALE, " _
                                & " ID_OPERATORE, DATA_ORA, MOTIVAZIONE) " _
                                & " VALUES (" & par.IfNull(lettore("ID"), 0) & ", " _
                                & idPericoloIniziale & ", " _
                                & pericoloFinale & ", " _
                                & Session.Item("ID_OPERATORE") & "," _
                                & "'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                & "'" & par.PulisciStrSql("Cambio priorità per unione segnalazione.") & "')"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End While
                    lettore.Close()
                    WriteEvent("F252", "Unione segnalazioni: " & listaSegnalazioni)
                    connData.chiudi(True)
                    par.modalDialogMessage("Agenda e Segnalazioni", "Segnalazioni unite correttamente", Me.Page, "successo")
                End If
                '************ UNIONE SEGNALAZIONI EDIFICI ************
            End If
            CaricaRisultati()
            MultiViewBottoni.ActiveViewIndex = 1
            MultiViewRicerca.ActiveViewIndex = 1
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - ButtonUnisciSegnalazioni_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String)
        Dim connOpNow As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri()
                connOpNow = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (NULL," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
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

    Protected Sub ButtonUnisci_Click(sender As Object, e As System.EventArgs) Handles ButtonUnisci.Click
        Dim cont As Integer = 0
        Dim tabella As Data.DataTable = CType(Session.Item("AGGREGAZIONE_COMPLETA"), Data.DataTable)
        For Each Items As Data.DataRow In tabella.Rows
            If Items.Item("CHECK1").ToString = "TRUE" Then
                cont += 1
            End If
        Next
        If cont > 0 Then
            par.modalDialogConfirm("Agenda e Segnalazioni", "Vuoi unire le segnalazioni selezionate?", "Ok", "document.getElementById('ButtonUnisciSegnalazioni').click();", "Annulla", "", Page)
        Else
            par.modalDialogMessage("Agenda e Segnalazioni", "Selezionare almeno una segnalazione", Me.Page, "info")
        End If
    End Sub

    'Protected Sub DataGridSegnalaz_PreRender(sender As Object, e As System.EventArgs) Handles DataGridSegnalaz.PreRender
    '    For Each riga As DataGridItem In DataGridSegnalaz.Items
    '        If CDec(riga.Cells(par.IndDGC(DataGridSegnalaz, "FIGLI2")).Text) = 0 Then
    '            riga.Cells(0).Text = ""
    '        End If
    '    Next
    'End Sub

    Protected Sub RadGridSegnalazioni_PreRender(sender As Object, e As System.EventArgs) Handles RadGridSegnalazioni.PreRender
        RadGridSegnalazioni.Height = CInt(AltezzaRadGrid.Value)
        RadGridSegnalazioni.Width = CInt(LarghezzaRadGrid.Value)
        HideExpandColumnRecursive(RadGridSegnalazioni.MasterTableView)
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
        If Not Page.IsPostBack Then
            RadGridSegnalazioni.MasterTableView.Items(0).Expanded = True
            RadGridSegnalazioni.MasterTableView.Items(0).ChildItem.NestedTableViews(0).Items(0).Expanded = True
        End If
    End Sub

    Public Sub HideExpandColumnRecursive(ByVal tableView As GridTableView)
        Dim nestedViewItems As GridItem() = tableView.GetItems(GridItemType.NestedView)
        For Each nestedViewItem As GridNestedViewItem In nestedViewItems
            For Each nestedView As GridTableView In nestedViewItem.NestedTableViews
                Dim cell As TableCell = nestedView.ParentItem("ExpandColumn")
                If nestedView.Items.Count = 0 Then
                    cell.Controls(0).Visible = False
                    cell.Text = "&nbsp"
                    nestedViewItem.Visible = False
                End If
                If nestedView.HasDetailTables Then
                    HideExpandColumnRecursive(nestedView)
                End If
            Next
        Next
    End Sub

End Class