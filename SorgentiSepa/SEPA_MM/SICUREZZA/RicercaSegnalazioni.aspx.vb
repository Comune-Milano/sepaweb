Imports Telerik.Web.UI
Imports Telerik.Web.UI.GridExcelBuilder

Partial Class SICUREZZA_RicercaSegnalazioni
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
                CaricaStatoSegnalazioni()
                CaricaTipoSegnalazione()
                CaricaTipologieLivello1()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
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
            '            par.modalDialogMessage("Sicurezza", "Non è possibile caricare la sede territoriale di appartenenza. Contattare l\'amministratore di sistema.", Page, "info", "Home.aspx")
            '        End If
            '    End If
            'End If
            If CType(Me.Master.FindControl("fl_sicurezza"), HiddenField).Value = "0" Then
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operatore non abilitato al modulo Sicurezza!", 300, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - Page_LoadComplete - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - caricaStrutture - " & ex.Message)
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
            par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneSedeTerritoriale & " ORDER BY DENOMINAZIONE", cmbComplesso, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - caricaComplessi - " & ex.Message)
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
            par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 " & condizioneComplesso & " ORDER BY DENOMINAZIONE", cmbEdificio, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - caricaEdifici - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - CaricaStatoSegnalazioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaTipoSegnalazione()
        Try
            connData.apri()
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE where id=5 ORDER BY ID", cmbTipoSegnalazione, "ID", "DESCRIZIONE", False)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - CaricaTipoSegnalazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaTipologieLivello1()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE=" & cmbTipoSegnalazione.SelectedValue & " ORDER BY DESCRIZIONE"

            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello1, "ID", "DESCRIZIONE", True)
           
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Nuova Segnalazione - CaricaTipologieLivello1 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        HiddenFieldVista.Value = "1"
        CaricaRisultati()
        MultiViewRicerca.ActiveViewIndex = 1
        MultiViewBottoni.ActiveViewIndex = 1
    End Sub
    
    Protected Sub cmbComplesso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        caricaEdifici()
        cmbEdificio.Focus()
    End Sub
    'Protected Sub DataGridSegnalaz_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridSegnalaz.ItemDataBound

    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

    '        If CDec(e.Item.Cells(par.IndDGC(DataGridSegnalaz, "FIGLI2")).Text) > 0 Then

    '            Dim condizioneOrdinamento As String = "ORDER BY ID_PERICOLO_SEGNALAZIONE DESC,DATA_ORA_RICHIESTA ASC"


    '            Dim CondizioneRicerca As String = ""
    '            'If Trim(TextBoxNumero.Text) <> "" AndAlso IsNumeric(TextBoxNumero.Text) Then
    '            '    CondizioneRicerca &= " AND SEGNALAZIONI.ID=" & TextBoxNumero.Text
    '            'End If
    '            Dim listaTipologie As String = ""
    '            Dim contaTipologie As Integer = 0
    '            For Each item As ListItem In CheckBoxListTipoSegnalazione.Items
    '                If item.Selected = True Then
    '                    contaTipologie += 1
    '                    If listaTipologie = "" Then
    '                        listaTipologie = item.Value
    '                    Else
    '                        listaTipologie &= "," & item.Value
    '                    End If
    '                End If
    '            Next
    '            If contaTipologie = 1 Then
    '                If listaTipologie <> "" Then
    '                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGNALAZIONE in (" & listaTipologie & ")"
    '                End If
    '                If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
    '                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue
    '                End If
    '                If IsNumeric(cmbTipoSegnalazioneLivello2.SelectedValue) AndAlso cmbTipoSegnalazioneLivello2.SelectedValue <> "-1" Then
    '                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue
    '                End If
    '                If IsNumeric(cmbTipoSegnalazioneLivello3.SelectedValue) AndAlso cmbTipoSegnalazioneLivello3.SelectedValue <> "-1" Then
    '                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue
    '                End If
    '                If IsNumeric(cmbTipoSegnalazioneLivello4.SelectedValue) AndAlso cmbTipoSegnalazioneLivello4.SelectedValue <> "-1" Then
    '                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_4=" & cmbTipoSegnalazioneLivello4.SelectedValue
    '                End If
    '            ElseIf contaTipologie > 1 Then
    '                If listaTipologie <> "" Then
    '                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGNALAZIONE in (" & listaTipologie & ")"
    '                End If
    '            End If



    '            If listaTipologie = "1" Or listaTipologie = "6" Then
    '                Dim urgenza As String = ""
    '                Select Case UCase(DropDownListUrgenza.SelectedValue)
    '                    Case "---"
    '                        urgenza = "-1"
    '                    Case "BIANCO"
    '                        urgenza = "1"
    '                    Case "VERDE"
    '                        urgenza = "2"
    '                    Case "GIALLO"
    '                        urgenza = "3"
    '                    Case "ROSSO"
    '                        urgenza = "4"
    '                    Case "BLU"
    '                        urgenza = "0"
    '                End Select
    '                If urgenza <> "-1" Then
    '                    CondizioneRicerca &= " AND SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE=" & urgenza
    '                End If
    '            End If
    '            Dim dataMin As String = ""
    '            Dim dataMax As String = ""
    '            If txtDal.Text <> "" Then
    '                dataMin = par.AggiustaData(txtDal.Text)
    '                If TextBoxOreDal.Text <> "" Then
    '                    dataMin &= TextBoxOreDal.Text.ToString.PadLeft(2, "0")
    '                    If TextBoxMinutiDal.Text <> "" Then
    '                        dataMin &= TextBoxMinutiDal.Text.ToString.PadLeft(2, "0")
    '                    Else
    '                        dataMin &= "00"
    '                    End If
    '                Else
    '                    dataMin &= "0000"
    '                End If
    '            End If
    '            If dataMin <> "" Then
    '                CondizioneRicerca &= " AND SUBSTR(DATA_ORA_RICHIESTA,1,12)>='" & dataMin & "'  "
    '            End If
    '            If txtAl.Text <> "" Then
    '                dataMax = par.AggiustaData(txtAl.Text)
    '                If TextBoxOreAl.Text <> "" Then
    '                    dataMax &= TextBoxOreAl.Text.ToString.PadLeft(2, "0")
    '                    If TextBoxMinutiAl.Text <> "" Then
    '                        dataMax &= TextBoxMinutiAl.Text.ToString.PadLeft(2, "0")
    '                    Else
    '                        dataMax &= "00"
    '                    End If
    '                Else
    '                    dataMax &= "2400"
    '                End If
    '            End If
    '            If dataMax <> "" Then
    '                CondizioneRicerca &= " AND SUBSTR(DATA_ORA_RICHIESTA,1,12)<='" & dataMax & "' "
    '            End If
    '            Dim listaSedi As String = ""
    '            For Each item As ListItem In CheckBoxListSedi.Items
    '                If item.Selected = True Then
    '                    If listaSedi = "" Then
    '                        listaSedi = item.Value
    '                    Else
    '                        listaSedi &= "," & item.Value
    '                    End If
    '                End If
    '            Next
    '            If listaSedi <> "" Then
    '                CondizioneRicerca &= " AND TAB_FILIALI.ID in (" & listaSedi & ") "
    '            End If

    '            If cmbEdificio.SelectedValue <> "-1" Then
    '                CondizioneRicerca &= " AND SEGNALAZIONI.ID_EDIFICIO = " & cmbEdificio.SelectedValue
    '            End If
    '            If cmbComplesso.SelectedValue <> "-1" Then
    '                CondizioneRicerca &= " and segnalazioni.id_edificio in (select id from siscom_mi.edifici where id_complesso= " & cmbComplesso.SelectedValue & ")"
    '            End If
    '            If Trim(txtSegnalante.Text) <> "" Then
    '                Dim listaSegnalante As String()
    '                listaSegnalante = txtSegnalante.Text.ToString.Split(" ")

    '                CondizioneRicerca &= " AND ("
    '                If listaSegnalante.Length = 1 Then
    '                    CondizioneRicerca &= " upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(0))) & "' "
    '                    CondizioneRicerca &= " OR upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(0))) & "' "
    '                Else
    '                    CondizioneRicerca &= "("
    '                    For i As Integer = 0 To listaSegnalante.Length - 1 Step 1
    '                        If i = 0 Then
    '                            CondizioneRicerca &= " upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
    '                        Else
    '                            CondizioneRicerca &= " OR upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
    '                        End If
    '                    Next
    '                    CondizioneRicerca &= ") AND ("
    '                    For i As Integer = 0 To listaSegnalante.Length - 1 Step 1
    '                        If i = 0 Then
    '                            CondizioneRicerca &= " upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
    '                        Else
    '                            CondizioneRicerca &= " OR upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
    '                        End If
    '                    Next
    '                    CondizioneRicerca &= ")"
    '                End If
    '                CondizioneRicerca &= " )"
    '            End If

    '            If cmbFornitori.SelectedValue <> "-1" Then
    '                CondizioneRicerca &= " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE APPALTI.ID_FORNITORE=" & cmbFornitori.SelectedValue & "))"
    '            End If

    '            Dim listaStati As String = ""
    '            For Each elemento As ListItem In CheckBoxListStato.Items
    '                If elemento.Selected = True Then
    '                    If listaStati = "" Then
    '                        listaStati = elemento.Value
    '                    Else
    '                        listaStati &= "," & elemento.Value
    '                    End If
    '                End If
    '            Next
    '            If listaStati <> "" Then
    '                CondizioneRicerca &= "AND SEGNALAZIONI.ID_STATO IN (" & listaStati & ") "
    '            End If


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
    '                & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
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
    '                & CondizioneRicerca _
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
    '                'figli.ItemStyle.HorizontalAlign = HorizontalAlign.Right
    '                'NewDg.Columns.Add(figli)


    '                NewDg.Width = Unit.Percentage(100)

    '                NewDg.DataSource = dt
    '                NewDg.DataBind()


    '                For Each elemento As DataGridItem In NewDg.Items
    '                    elemento.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '                    elemento.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='#cccccc';}")
    '                    elemento.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='orange';document.getElementById('CPContenuto_TextBox1').value='Hai selezionato la segnalazione N°" & elemento.Cells(0).Text & "';document.getElementById('idSegnalazione').value='" & elemento.Cells(0).Text & "';")
    '                    elemento.Attributes.Add("onDblclick", "Apri();")
    '                Next


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
    '                    e.Item.Cells(LastCellPosition).Text() = e.Item.Cells(LastCellPosition).Text + "</td><tr><td bgcolor='#ffffff' style='width:50px;'></td><td colspan='" + NewCellPosition.ToString() + "'>" + FullDIV
    '                Else
    '                    e.Item.Cells(LastCellPosition).Text = e.Item.Cells(LastCellPosition).Text + "</td><tr><td bgcolor='#ffffff' style='width:50px;'></td><td colspan='" + NewCellPosition.ToString() + "'>" + FullDIV
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
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------  
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white';}")
    '        e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='orange';document.getElementById('CPContenuto_TextBox1').value='Hai selezionato la segnalazione N°" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "NUM")).Text & "';document.getElementById('idSegnalazione').value='" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID")).Text & "';")
    '        e.Item.Attributes.Add("onDblclick", "Apri();")
    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='orange') {this.style.backgroundColor='white';}")
    '        e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='orange';document.getElementById('CPContenuto_TextBox1').value='Hai selezionato la segnalazione N°" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "NUM")).Text & "';document.getElementById('idSegnalazione').value='" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID")).Text & "';")
    '        e.Item.Attributes.Add("onDblclick", "Apri();")
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

    'Protected Sub DataGridSegnalaz_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridSegnalaz.PageIndexChanged
    '    If e.NewPageIndex >= 0 Then
    '        DataGridSegnalaz.CurrentPageIndex = e.NewPageIndex
    '        CaricaRisultati()
    '    End If
    'End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If IsNumeric(idSegnalazione.Value) AndAlso idSegnalazione.Value <> "-1" Then
            RadGridSegnalazioni.Rebind()
            Dim apertura As String = "window.open('Segnalazione.aspx?NM=1&IDS=" & idSegnalazione.Value & "', 'apr' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)
            'Response.Redirect("Segnalazione.aspx?IDS=" & idSegnalazione.Value, False)
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("E\' necessario selezionare una segnalazione.", 300, 150, "Attenzione", Nothing, Nothing)

            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
        End If
    End Sub

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

            listaTipologie = cmbTipoSegnalazione.SelectedValue
            If listaTipologie <> "" Then
                CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGNALAZIONE in (" & listaTipologie & ")"
            End If
            If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
                CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue
            End If
           
            'If listaTipologie = "1" Or listaTipologie = "6" Then
            '    Dim urgenza As String = ""
            '    Select Case UCase(DropDownListUrgenza.SelectedValue)
            '        Case "---"
            '            urgenza = "-1"
            '        Case "BIANCO"
            '            urgenza = "1"
            '        Case "VERDE"
            '            urgenza = "2"
            '        Case "GIALLO"
            '            urgenza = "3"
            '        Case "ROSSO"
            '            urgenza = "4"
            '        Case "BLU"
            '            urgenza = "0"
            '    End Select
            '    If urgenza <> "-1" Then
            '        CondizioneRicerca &= " AND SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE=" & urgenza
            '    End If
            'End If
            Dim dataMin As String = ""
            Dim dataMax As String = ""
            If Not IsNothing(txtDal.SelectedDate) Then
                dataMin = par.AggiustaData(txtDal.SelectedDate)
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
            If Not IsNothing(txtAl.SelectedDate) Then
                dataMax = par.AggiustaData(txtAl.SelectedDate)
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

            'If cmbFornitori.SelectedValue <> "-1" Then
            '    CondizioneRicerca &= " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE APPALTI.ID_FORNITORE=" & cmbFornitori.SelectedValue & "))"
            'End If

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
            Dim condizioneOrdinamento As String = "ORDER BY 18 DESC, 19 DESC"
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

            par.cmd.CommandText = " SELECT SEGNALAZIONI.ID, " _
                            & " SEGNALAZIONI.ID AS NUM, " _
                            & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                            & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
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
                            & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                            & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & " NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                            & " (CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
                            & ",ID_sEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE " _
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
                            & " ,DATA_ORA_RICHIESTA " _
                            & " FROM siscom_mi.tab_stati_segnalazioni, " _
                            & " siscom_mi.segnalazioni, " _
                            & " siscom_mi.tab_filiali, " _
                            & " siscom_mi.edifici, " _
                            & " siscom_mi.unita_immobiliari, " _
                            & " siscom_mi.TIPOLOGIE_GUASTI, " _
                            & " OPERATORI " _
                            & " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                            & " AND segnalazioni.id_stato <> -1 " _
                            & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                            & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                            & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                            & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
                            & CondizioneRicerca _
                            & condizionePadri _
                            & " AND id_Segnalazione_padre is null " _
                            & "  union " _
                            & " SELECT SEGNALAZIONI.ID, " _
                            & " SEGNALAZIONI.ID AS NUM, " _
                            & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                            & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
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
                            & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                            & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & " NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                            & " (CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
                            & " ,ID_SEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE" _
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
                            & " ,DATA_ORA_RICHIESTA " _
                            & " FROM siscom_mi.tab_stati_segnalazioni, " _
                            & " siscom_mi.segnalazioni, " _
                            & " siscom_mi.tab_filiali, " _
                            & " siscom_mi.edifici, " _
                            & " siscom_mi.unita_immobiliari, " _
                            & " siscom_mi.TIPOLOGIE_GUASTI, " _
                            & " OPERATORI " _
                            & " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                            & " AND segnalazioni.id_stato <> -1 " _
                            & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                            & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                            & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                            & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
                            & " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONE_PADRE " _
                            & " FROM SISCOM_MI.SEGNALAZIONI " _
                            & " ,siscom_mi.tab_filiali " _
                            & " WHERE ID_SEGNALAZIONE_PADRE IS NOT NULL " _
                            & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & CondizioneRicerca _
                            & condizionePadri _
                            & ") " _
                            & condizioneOrdinamento
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)
            'DataGridSegnalaz.DataSource = dt
            'DataGridSegnalaz.DataBind()
            Session.Item("DataGridSegnalaz") = dt
            RadGridSegnalazioni.CurrentPageIndex = 0
            RadGridSegnalazioni.Rebind()
            If dt.Rows.Count > 1 Then
                lblRisultati.Text = "Trovate - " & dt.Rows.Count & " segnalazioni"
            ElseIf dt.Rows.Count = 1 Then
                lblRisultati.Text = "Trovata - " & dt.Rows.Count & " segnalazione"
            ElseIf dt.Rows.Count = 0 Then
                'lblRisultati.Text = "Nessuna segnalazione trovata"
                lblRisultati.Text = ""
            End If
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - BindGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    
   
    
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub
    Protected Sub CheckBoxListSedi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListSedi.SelectedIndexChanged
        caricaComplessi()
        caricaEdifici()
        cmbComplesso.Focus()
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

                        listaTipologie = cmbTipoSegnalazione.SelectedValue

                        If listaTipologie <> "" Then
                            CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGNALAZIONE in (" & listaTipologie & ")"
                        End If
                        If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
                            CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue
                        End If
                        
                        'If listaTipologie = "1" Or listaTipologie = "6" Then
                        '    Dim urgenza As String = ""
                        '    Select Case UCase(DropDownListUrgenza.SelectedValue)
                        '        Case "---"
                        '            urgenza = "-1"
                        '        Case "BIANCO"
                        '            urgenza = "1"
                        '        Case "VERDE"
                        '            urgenza = "2"
                        '        Case "GIALLO"
                        '            urgenza = "3"
                        '        Case "ROSSO"
                        '            urgenza = "4"
                        '        Case "BLU"
                        '            urgenza = "0"
                        '    End Select
                        '    If urgenza <> "-1" Then
                        '        CondizioneRicerca &= " AND SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE=" & urgenza
                        '    End If
                        'End If
                        Dim dataMin As String = ""
                        Dim dataMax As String = ""
                        If Not IsNothing(txtDal.SelectedDate) AndAlso txtDal.SelectedDate <> "" Then
                            dataMin = par.AggiustaData(txtDal.SelectedDate)
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
                        If Not IsNothing(txtAl.SelectedDate) AndAlso txtAl.SelectedDate <> "" Then
                            dataMax = par.AggiustaData(txtAl.SelectedDate)
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

                        'If cmbFornitori.SelectedValue <> "-1" Then
                        '    CondizioneRicerca &= " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE APPALTI.ID_FORNITORE=" & cmbFornitori.SelectedValue & "))"
                        'End If

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
                            & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
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
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
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

    Protected Sub RadGridSegnalazioni_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridSegnalazioni.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridSegnalazioni.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text
                End If
            Next
            e.Item.Attributes.Add("onclick", "document.getElementById('CPContenuto_TextBox1').value='Hai selezionato la segnalazione N°" & dataItem("NUM").Text & "';document.getElementById('idSegnalazione').value='" & dataItem("ID").Text & "'")
            e.Item.Attributes.Add("onDblclick", "Apri();")
            'If dataItem("TIPO").Text = "1" Or dataItem("TIPO").Text = "6" Then
            '    Select Case dataItem("ID_PERICOLO_SEGNALAZIONE").Text
            '        Case "1"
            '            dataItem("TIPO_INT").Controls.Clear()
            '            Dim img As Image = New Image()
            '            img.ImageUrl = "Immagini/Ball-white-128.png"
            '            dataItem("TIPO_INT").Controls.Add(img)
            '        Case "2"
            '            dataItem("TIPO_INT").Controls.Clear()
            '            Dim img As Image = New Image()
            '            img.ImageUrl = "Immagini/Ball-green-128.png"
            '            dataItem("TIPO_INT").Controls.Add(img)
            '        Case "3"
            '            dataItem("TIPO_INT").Controls.Clear()
            '            Dim img As Image = New Image()
            '            img.ImageUrl = "Immagini/Ball-yellow-128.png"
            '            dataItem("TIPO_INT").Controls.Add(img)
            '        Case "4"
            '            dataItem("TIPO_INT").Controls.Clear()
            '            Dim img As Image = New Image()
            '            img.ImageUrl = "Immagini/Ball-red-128.png"
            '            dataItem("TIPO_INT").Controls.Add(img)
            '        Case "0"
            '            dataItem("TIPO_INT").Controls.Clear()
            '            Dim img As Image = New Image()
            '            img.ImageUrl = "Immagini/Ball-blue-128.png"
            '            dataItem("TIPO_INT").Controls.Add(img)
            '        Case Else
            '    End Select
            'End If
        End If
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub

    Protected Sub RadGridSegnalazioni_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridSegnalazioni.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridSegnalaz"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridSegnalaz"), Data.DataTable)
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub

    Protected Sub RadGridSegnalazioni_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGridSegnalazioni.PageIndexChanged
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub

    Protected Sub RadGridSegnalazioni_PageSizeChanged(sender As Object, e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles RadGridSegnalazioni.PageSizeChanged
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Try
            Dim dt As Data.DataTable = CType(Session.Item("DataGridSegnalaz"), Data.DataTable)
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDataGridWithDTColorRadGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", RadGridSegnalazioni, dt, True, , , "CRITICITA'")
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "validNavigation=true;avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                'par.modalDialogMessage("Ricerca Segnalazioni", "Esportazione eseguita correttamente.", Page, "successo", "..\/FileTemp\/" & nomeFile)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=1;", True)
            MultiViewBottoni.ActiveViewIndex = 1
            MultiViewRicerca.ActiveViewIndex = 1
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazione - btnExport_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridSegnalazioni_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGridSegnalazioni.PreRender
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

    Protected Sub btnExport2_Click(sender As Object, e As System.EventArgs) Handles btnExport2.Click
        Try
            connData.apri()
            Dim dt As Data.DataTable = CType(Session.Item("DataGridSegnalaz"), Data.DataTable)
            Dim dt2 As New Data.DataTable
            dt2 = dt.Clone()
            Dim numeroTicketFigli As Integer = 0
            Dim rigadt2 As Data.DataRow
            For Each riga As Data.DataRow In dt.Rows
                numeroTicketFigli = par.IfNull(riga.Item("FIGLI"), 0)
                rigadt2 = dt2.NewRow
                rigadt2.Item("ID") = riga.Item("ID")
                rigadt2.Item("NUM") = riga.Item("NUM")
                rigadt2.Item("TIPO") = riga.Item("TIPO")
                'rigadt2.Item("TIPO_INT") = riga.Item("TIPO_INT")
                rigadt2.Item("TIPO0") = riga.Item("TIPO0")
                rigadt2.Item("TIPO1") = riga.Item("TIPO1")
                rigadt2.Item("TIPO2") = riga.Item("TIPO2")
                rigadt2.Item("TIPO3") = riga.Item("TIPO3")
                rigadt2.Item("TIPO4") = riga.Item("TIPO4")
                rigadt2.Item("STATO") = riga.Item("STATO")
                rigadt2.Item("INDIRIZZO") = riga.Item("INDIRIZZO")
                rigadt2.Item("RICHIEDENTE") = riga.Item("RICHIEDENTE")
                rigadt2.Item("CODICE_RU") = riga.Item("CODICE_RU")
                rigadt2.Item("DATA_INSERIMENTO") = riga.Item("DATA_INSERIMENTO")
                rigadt2.Item("DESCRIZIONE") = riga.Item("DESCRIZIONE")
                rigadt2.Item("FILIALE") = riga.Item("FILIALE")
                rigadt2.Item("NOTE_C") = riga.Item("NOTE_C")
                rigadt2.Item("ID_PERICOLO_sEGNALAZIONE") = riga.Item("ID_PERICOLO_sEGNALAZIONE")
                rigadt2.Item("FIGLI") = riga.Item("FIGLI")
                rigadt2.Item("FIGLI2") = riga.Item("FIGLI2")
                If par.IfNull(riga.Item("ID_sEGNALAZIONE_PADRE"), 0) = 0 Then
                    rigadt2.Item("ID_sEGNALAZIONE_PADRE") = DBNull.Value
                Else
                    rigadt2.Item("ID_sEGNALAZIONE_PADRE") = riga.Item("ID_sEGNALAZIONE_PADRE")
                End If
                rigadt2.Item("DATA_EMISSIONE") = riga.Item("DATA_EMISSIONE")
                rigadt2.Item("DATA_CHIUSURA") = riga.Item("DATA_CHIUSURA")
                rigadt2.Item("DATA_ORA_RICHIESTA") = riga.Item("DATA_ORA_RICHIESTA")
                dt2.Rows.Add(rigadt2)
                If CDec(numeroTicketFigli) > 0 Then

                    Dim condizioneOrdinamento As String = "ORDER BY ID_PERICOLO_SEGNALAZIONE DESC,DATA_ORA_RICHIESTA DESC"
                    Dim CondizioneRicerca As String = ""
                    'If Trim(TextBoxNumero.Text) <> "" AndAlso IsNumeric(TextBoxNumero.Text) Then
                    '    CondizioneRicerca &= " AND SEGNALAZIONI.ID=" & TextBoxNumero.Text
                    'End If
                    Dim listaTipologie As String = ""


                    listaTipologie = cmbTipoSegnalazione.SelectedValue

                    If listaTipologie <> "" Then
                        CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGNALAZIONE in (" & listaTipologie & ")"
                    End If
                    If IsNumeric(cmbTipoSegnalazioneLivello1.SelectedValue) AndAlso cmbTipoSegnalazioneLivello1.SelectedValue <> "-1" Then
                        CondizioneRicerca &= " AND SEGNALAZIONI.ID_TIPO_sEGN_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue
                    End If
                    

                    'If listaTipologie = "1" Or listaTipologie = "6" Then
                    '    Dim urgenza As String = ""
                    '    Select Case UCase(DropDownListUrgenza.SelectedValue)
                    '        Case "---"
                    '            urgenza = "-1"
                    '        Case "BIANCO"
                    '            urgenza = "1"
                    '        Case "VERDE"
                    '            urgenza = "2"
                    '        Case "GIALLO"
                    '            urgenza = "3"
                    '        Case "ROSSO"
                    '            urgenza = "4"
                    '        Case "BLU"
                    '            urgenza = "0"
                    '    End Select
                    '    If urgenza <> "-1" Then
                    '        CondizioneRicerca &= " AND SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE=" & urgenza
                    '    End If
                    'End If
                    Dim dataMin As String = ""
                    Dim dataMax As String = ""
                    If Not IsNothing(txtDal.SelectedDate) AndAlso txtDal.SelectedDate <> "" Then
                        dataMin = par.AggiustaData(txtDal.SelectedDate)
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
                    If Not IsNothing(txtAl.SelectedDate) AndAlso txtAl.SelectedDate <> "" Then
                        dataMax = par.AggiustaData(txtAl.SelectedDate)
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

                    'If cmbFornitori.SelectedValue <> "-1" Then
                    '    CondizioneRicerca &= " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE APPALTI.ID_FORNITORE=" & cmbFornitori.SelectedValue & "))"
                    'End If

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
                        & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                        & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                        & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                        & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                        & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
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
                        & ",ID_SEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE " _
                        & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                        & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
                        & "FROM siscom_mi.tab_stati_segnalazioni, " _
                        & "siscom_mi.segnalazioni, " _
                        & "siscom_mi.tab_filiali, " _
                        & "siscom_mi.edifici, " _
                        & "siscom_mi.unita_immobiliari, " _
                        & "siscom_mi.TIPOLOGIE_GUASTI, " _
                        & "OPERATORI " _
                        & "WHERE ID_SEGNALAZIONE_PADRE=" & par.IfNull(riga.Item("ID"), 0) _
                        & " and tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                        & " AND segnalazioni.id_stato <> -1 " _
                        & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                        & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                        & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                        & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                        & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
                        & CondizioneRicerca _
                        & condizioneOrdinamento

                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    While lettore.Read
                        rigadt2 = dt2.NewRow
                        rigadt2.Item("ID") = par.IfNull(lettore("ID"), DBNull.Value)
                        rigadt2.Item("NUM") = par.IfNull(lettore("NUM"), DBNull.Value)
                        rigadt2.Item("TIPO") = par.IfNull(lettore("TIPO"), DBNull.Value)
                        'rigadt2.Item("TIPO_INT") = par.IfNull(lettore("TIPO_INT"), DBNull.Value)
                        rigadt2.Item("TIPO0") = par.IfNull(lettore("TIPO0"), DBNull.Value)
                        rigadt2.Item("TIPO1") = par.IfNull(lettore("TIPO1"), DBNull.Value)
                        rigadt2.Item("TIPO2") = par.IfNull(lettore("TIPO2"), DBNull.Value)
                        rigadt2.Item("TIPO3") = par.IfNull(lettore("TIPO3"), DBNull.Value)
                        rigadt2.Item("TIPO4") = par.IfNull(lettore("TIPO4"), DBNull.Value)
                        rigadt2.Item("STATO") = par.IfNull(lettore("STATO"), DBNull.Value)
                        rigadt2.Item("INDIRIZZO") = par.IfNull(lettore("INDIRIZZO"), DBNull.Value)
                        rigadt2.Item("RICHIEDENTE") = par.IfNull(lettore("RICHIEDENTE"), DBNull.Value)
                        rigadt2.Item("CODICE_RU") = par.IfNull(lettore("CODICE_RU"), DBNull.Value)
                        rigadt2.Item("DATA_INSERIMENTO") = par.IfNull(lettore("DATA_INSERIMENTO"), DBNull.Value)
                        rigadt2.Item("DESCRIZIONE") = par.IfNull(lettore("DESCRIZIONE"), DBNull.Value)
                        rigadt2.Item("FILIALE") = par.IfNull(lettore("FILIALE"), DBNull.Value)
                        rigadt2.Item("NOTE_C") = par.IfNull(lettore("NOTE_C"), DBNull.Value)
                        rigadt2.Item("ID_PERICOLO_sEGNALAZIONE") = par.IfNull(lettore("ID_PERICOLO_sEGNALAZIONE"), DBNull.Value)
                        rigadt2.Item("FIGLI") = par.IfNull(lettore("FIGLI"), DBNull.Value)
                        rigadt2.Item("FIGLI2") = par.IfNull(lettore("FIGLI2"), DBNull.Value)
                        If par.IfNull(lettore("ID_sEGNALAZIONE_PADRE"), 0) = 0 Then
                            rigadt2.Item("ID_sEGNALAZIONE_PADRE") = DBNull.Value
                        Else
                            rigadt2.Item("ID_sEGNALAZIONE_PADRE") = par.IfNull(lettore("ID_sEGNALAZIONE_PADRE"), 0)
                        End If
                        rigadt2.Item("DATA_EMISSIONE") = par.IfNull(lettore("DATA_EMISSIONE"), DBNull.Value)
                        rigadt2.Item("DATA_CHIUSURA") = par.IfNull(lettore("DATA_CHIUSURA"), DBNull.Value)
                        rigadt2.Item("DATA_ORA_RICHIESTA") = DBNull.Value
                        dt2.Rows.Add(rigadt2)
                    End While
                    lettore.Close()
                End If
            Next
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDataGridWithDTColorRadGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", RadGridSegnalazioni, dt2, True, , , "CRITICITA'")
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                'ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "validNavigation=true;avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)

            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=1;", True)
            MultiViewBottoni.ActiveViewIndex = 1
            MultiViewRicerca.ActiveViewIndex = 1
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazione - btnExport_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnExport3_Click(sender As Object, e As System.EventArgs) Handles btnExport3.Click
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EXPORT_SEGNALAZIONI ORDER BY 18,24"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            connData.chiudi()
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeFile = xls.EsportaExcelDaDataGridWithDTColorRadGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", RadGridSegnalazioni, dt, True, , , "CRITICITA'")
                If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "validNavigation=true;avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
                End If
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=1;", True)
            End If
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazione - btnExport3_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class