Imports Telerik.Web.UI
Imports Telerik.Web.UI.GridExcelBuilder

Partial Class GESTIONE_CONTATTI_RicercaSegnalazioni
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
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                caricaStrutture()
                caricaComplessi()
                caricaEdifici()
                caricaFornitori()
                CaricaStatoSegnalazioni()
                CaricaTipoSegnalazione()
                CaricaCanale()
                CaricaTipologiaSegnalante()
                If connAperta = True Then
                    connData.chiudi(False)
                End If
                txtDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Private Sub CaricaCanale()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboBox("SELECT * FROM SISCOM_MI.CANALE WHERE FL_AGENDA = 1 ORDER BY ID ASC", cmbCanale, "ID", "DESCRIZIONE", True)

            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - CaricaCanale - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaTipologiaSegnalante()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_TIPOLOGIA_SEGNALANTE ORDER BY ID ASC", cmbTipoSegnalante, "ID", "DESCRIZIONE", True)

            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - CaricaTipologiaSegnalante - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - Page_LoadComplete - " & ex.Message)
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
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If

            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_STRUTTURA FROM SISCOM_MI.SEGNALAZIONI) ORDER BY NOME ASC", CheckBoxListSedi, "ID", "NOME")
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - caricaStrutture - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaComplessi()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If

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
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - caricaComplessi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaEdifici()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
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
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - caricaEdifici - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaStatoSegnalazioni()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE ID >=0", CheckBoxListStato, "ID", "DESCRIZIONE")
            If connAperta = True Then
                connData.chiudi(False)
            End If
            For Each elemento As ListItem In CheckBoxListStato.Items
                If elemento.Text <> "CHIUSA" Then
                    elemento.Selected = True
                End If
            Next
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - CaricaStatoSegnalazioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaFornitori()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboBox("SELECT ID,RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE ID_sEGNALAZIONI IN (SELECT ID FROM SISCOM_MI.SEGNALAZIONI))) ORDER BY 2", cmbFornitori, "ID", "RAGIONE_SOCIALE")
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - caricaFornitori - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaTipoSegnalazione()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE ID NOT IN (6,7)  ORDER BY ID", CheckBoxListTipoSegnalazione, "ID", "DESCRIZIONE")
            If connAperta = True Then
                connData.chiudi(False)
            End If

            cmbTipoSegnalazioneLivello1.Items.Clear()
            cmbTipoSegnalazioneLivello2.Items.Clear()
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - CaricaTipoSegnalazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        HiddenFieldVista.Value = "1"
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
            par.modalDialogMessage("Agenda e Segnalazioni", "E\' necessario selezionare una segnalazione.", Me.Page, "info", , )
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
    Private Sub CaricaRisultati(Optional ByVal Export As Boolean = False)
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

            If Trim(TextBoxCodiceUI.Text) <> "" Then
                CondizioneRicerca &= " and segnalazioni.ID_UNITA in (select id from siscom_mi.unita_immobiliari where unita_immobiliari.cod_unita_immobiliare like '%" & Replace(TextBoxCodiceUI.Text, "*", "%") & "%')"
            End If
            If Trim(TextBoxCodiceRU.Text) <> "" Then
                CondizioneRicerca &= " and segnalazioni.ID_CONTRATTO in (select id from siscom_mi.RAPPORTI_UTENZA where RAPPORTI_UTENZA.cod_contratto like '%" & Replace(TextBoxCodiceRU.Text, "*", "%") & "%')"
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
            Dim condizioneAppuntamento As String = ""
            If CheckBoxAppuntamento.Checked = True Then
                condizioneAppuntamento = " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONE FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER)"
            End If

            Dim condizioneODL As String = ""
            If CheckBoxSegnConODL.Checked = True Then
                condizioneODL = " AND EXISTS (SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI=SEGNALAZIONI.ID AND MANUTENZIONI.STATO<5) "
            End If
            Dim condizioneAllegati As String = ""
            If chkAllegati.Checked = True Then
                condizioneAllegati = " AND ((SELECT COUNT(*) FROM SISCOM_MI.ALLEGATI_WS WHERE ID_OGGETTO = SEGNALAZIONI.ID AND STATO = 0)>0) "
            End If
            Dim condizioneCanone As String = ""
            If chkSegnCanone.Checked = True Then
                condizioneCanone = " AND  NVL(SEGNALAZIONI.ID_TIPOLOGIA_MANUTENZIONE, (SELECT DISTINCT COMBINAZIONE_TIPOLOGIE.ID_TIPO_MANUTENZIONE " _
                                 & " FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE  " _
                                 & " WHERE COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE(+) = SEGNALAZIONI.ID_TIPO_SEGNALAZIONE " _
                                 & " AND NVL(COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE_LIVELLO_1(+),0) = NVL(SEGNALAZIONI.ID_TIPO_SEGN_LIVELLO_1,0) " _
                                 & " AND NVL(COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE_LIVELLO_2(+),0) = NVL(SEGNALAZIONI.ID_TIPO_SEGN_LIVELLO_2,0) " _
                                 & " AND NVL(COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE_LIVELLO_3(+),0) = NVL(SEGNALAZIONI.ID_TIPO_SEGN_LIVELLO_3,0) " _
                                 & " AND NVL(COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE_LIVELLO_4(+),0) = NVL(SEGNALAZIONI.ID_TIPO_SEGN_LIVELLO_4,0)  " _
                                 & " )) = 1 "
            End If

            Dim condizioneCanale As String = ""
            If cmbCanale.SelectedValue <> "-1" Then
                condizioneCanale = " AND SEGNALAZIONI.ID_CANALE = " & cmbCanale.SelectedValue & " "
            End If

            Dim condizioneTipoSegn As String = ""
            If cmbTipoSegnalante.SelectedValue <> "-1" Then
                condizioneCanale = " AND SEGNALAZIONI.ID_TIPOLOGIA_SEGNALANTE = " & cmbTipoSegnalante.SelectedValue & " "
            End If
            '09/08/2018

            par.cmd.CommandText = " SELECT SEGNALAZIONI.ID, " _
                            & " SEGNALAZIONI.ID AS NUM, " _
                            & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
                            & " '' AS TIPO_INT, " _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_SEGNALANTE WHERE TAB_TIPOLOGIA_SEGNALANTE.ID=ID_TIPOLOGIA_SEGNALANTE) AS TIPO_SEGNALANTE, " _
                            & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                            & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, SEGNALAZIONI.TELEFONO1, SEGNALAZIONI.TELEFONO2, " _
                            & " (CASE " _
                            & " WHEN SEGNALAZIONI.ID_CONTRATTO IS NOT NULL THEN (SELECT COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = SEGNALAZIONI.ID_CONTRATTO) " _
                            & " WHEN SEGNALAZIONI.ID_UNITA " _
                            & " IS NOT NULL " _
                            & " THEN " _
                            & " (select cod_contratto from SISCOM_MI.rapporti_utenza where id = SISCOM_MI.getultimoru(id_unita,1)) " _
                            & " ELSE " _
                            & " NULL " _
                            & " END) " _
                            & " AS CODICE_RU, " _
                            & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                            & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & " NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                            & " (CASE WHEN ID_STATO = 10 THEN (SELECT MAX(NOTE) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
                            & ",ID_sEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE " _
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",NVL(SISCOM_MI.GETDATA(DATA_CHIUSURA),(SELECT GETDATA(MAX(DATA_ORA)) FROM SISCOM_MI.EVENTI_SEGNALAZIONI WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND VALORE_NEW = 'CHIUSA')) AS DATA_CHIUSURA, (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_MANUTENZIONE WHERE ID = SEGNALAZIONI.ID_TIPOLOGIA_MANUTENZIONE) AS CATEGORIZZAZIONE_MANUTENZIONE, " _
                            & " (CASE WHEN (SELECT COUNT(*) FROM SISCOM_MI.ALLEGATI_WS WHERE ID_OGGETTO = SEGNALAZIONI.ID AND STATO = 0)>0 THEN 'SÌ' ELSE 'NO' END) AS ALLEGATI_PRESENTI, ID_PROGRAMMA_ATTIVITA, SISCOM_MI.GETLINKCRONOPROGRAMMA(SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA,1) AS APRI_CRONO, DATA_ORA_RICHIESTA " _
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
                            & condizioneCanale _
                            & condizioneAppuntamento _
                            & condizioneODL _
                            & condizioneAllegati _
                            & condizioneCanone _
                            & " And id_Segnalazione_padre Is null " _
                            & "  union " _
                            & " Select SEGNALAZIONI.ID, " _
                            & " SEGNALAZIONI.ID As NUM, " _
                            & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE As TIPO, " _
                            & " '' AS TIPO_INT, " _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_SEGNALANTE WHERE TAB_TIPOLOGIA_SEGNALANTE.ID=ID_TIPOLOGIA_SEGNALANTE) AS TIPO_SEGNALANTE, " _
                            & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                            & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, SEGNALAZIONI.TELEFONO1, SEGNALAZIONI.TELEFONO2, " _
                            & " (CASE " _
                            & " WHEN SEGNALAZIONI.ID_CONTRATTO IS NOT NULL THEN (SELECT COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = SEGNALAZIONI.ID_CONTRATTO) " _
                            & " WHEN SEGNALAZIONI.ID_UNITA " _
                            & " IS NOT NULL " _
                            & " THEN " _
                            & " (select cod_contratto from SISCOM_MI.rapporti_utenza where id = SISCOM_MI.getultimoru(id_unita,1)) " _
                            & " ELSE " _
                            & " NULL " _
                            & " END) " _
                            & " AS CODICE_RU, " _
                            & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                            & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & " NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                            & " (CASE WHEN ID_STATO = 10 THEN (SELECT MAX(NOTE) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
                            & " ,ID_SEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE" _
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",NVL(SISCOM_MI.GETDATA(DATA_CHIUSURA),(SELECT GETDATA(MAX(DATA_ORA)) FROM SISCOM_MI.EVENTI_SEGNALAZIONI WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND VALORE_NEW = 'CHIUSA')) AS DATA_CHIUSURA, (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_MANUTENZIONE WHERE ID = SEGNALAZIONI.ID_TIPOLOGIA_MANUTENZIONE) AS CATEGORIZZAZIONE_MANUTENZIONE,  " _
                            & " (CASE WHEN (SELECT COUNT(*) FROM SISCOM_MI.ALLEGATI_WS WHERE ID_OGGETTO = SEGNALAZIONI.ID AND STATO = 0)>0 THEN 'SÌ' ELSE 'NO' END) AS ALLEGATI_PRESENTI, ID_PROGRAMMA_ATTIVITA, SISCOM_MI.GETLINKCRONOPROGRAMMA(SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA,1) AS APRI_CRONO, DATA_ORA_RICHIESTA  " _
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
                            & condizioneCanale _
                            & condizioneAppuntamento _
                            & condizioneODL _
                            & condizioneAllegati _
                            & condizioneCanone _
                            & ") "
            '& condizioneOrdinamento
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)
            'DataGridSegnalaz.DataSource = dt
            'DataGridSegnalaz.DataBind()
            Session.Item("DataGridSegnalaz") = dt
            If Export = False Then
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
            End If

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - BindGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
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
            '  Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE_LIVELLO_2,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue & " ORDER BY DESCRIZIONE"
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
            'Dim query As String = "SELECT ID, ID_TIPO_SEGNALAZIONE_LIVELLO_3,REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue & " ORDER BY DESCRIZIONE"
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
                            & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_SEGNALANTE WHERE TAB_TIPOLOGIA_SEGNALANTE.ID=ID_TIPOLOGIA_SEGNALANTE) AS TIPO_SEGNALANTE, " _
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
                            & "(CASE WHEN ID_STATO = 10 THEN (SELECT MAX(NOTE) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & ") ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & ",'FALSE' AS CHECK1 " _
                            & ",(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & ",'' as figli2 " _
                            & ",NVL(ID_SEGNALAZIONE_PADRE,0) AS ID_sEGNALAZIONE_PADRE " _
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",NVL(SISCOM_MI.GETDATA(DATA_CHIUSURA),(SELECT GETDATA(MAX(DATA_ORA)) FROM SISCOM_MI.EVENTI_SEGNALAZIONI WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND VALORE_NEW = 'CHIUSA')) AS DATA_CHIUSURA " _
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


    Protected Sub RadGridSegnalazioni_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridSegnalazioni.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridSegnalaz"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridSegnalaz"), Data.DataTable)
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
            Dim nomeFile1 = xls.EsportaExcelDaDataGridWithDTColorRadGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", RadGridSegnalazioni, dt, True, , , "CRITICITA'")
            '  Dim nomeFile = xls.EsportaExcelDaDataGridWithDTColorRadGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", RadGridSegnalazioni, dt, True, , , "CRITICITA'")
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile1) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                par.modalDialogMessage("Ricerca Segnalazioni", "Esportazione eseguita correttamente.", Page, "successo", "..\/FileTemp\/" & nomeFile1)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=1;", True)
            MultiViewBottoni.ActiveViewIndex = 1
            MultiViewRicerca.ActiveViewIndex = 1
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazione - btnExport_Click - " & ex.Message)
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
                rigadt2.Item("TIPO_INT") = riga.Item("TIPO_INT")
                rigadt2.Item("TIPO0") = riga.Item("TIPO0")
                rigadt2.Item("TIPO1") = riga.Item("TIPO1")
                rigadt2.Item("TIPO2") = riga.Item("TIPO2")
                rigadt2.Item("TIPO3") = riga.Item("TIPO3")
                rigadt2.Item("TIPO4") = riga.Item("TIPO4")
                rigadt2.Item("TIPO_SEGNALANTE") = riga.Item("TIPO_SEGNALANTE")
                rigadt2.Item("STATO") = riga.Item("STATO")
                rigadt2.Item("INDIRIZZO") = riga.Item("INDIRIZZO")
                rigadt2.Item("RICHIEDENTE") = riga.Item("RICHIEDENTE")
                rigadt2.Item("TELEFONO1") = riga.Item("TELEFONO1")
                rigadt2.Item("TELEFONO2") = riga.Item("TELEFONO2")
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
                rigadt2.Item("CATEGORIZZAZIONE_MANUTENZIONE") = riga.Item("CATEGORIZZAZIONE_MANUTENZIONE")
                rigadt2.Item("ALLEGATI_PRESENTI") = riga.Item("ALLEGATI_PRESENTI")
                rigadt2.Item("ID_PROGRAMMA_ATTIVITA") = riga.Item("ID_PROGRAMMA_ATTIVITA")
                rigadt2.Item("DATA_ORA_RICHIESTA") = riga.Item("DATA_ORA_RICHIESTA")

                dt2.Rows.Add(rigadt2)
                If CDec(numeroTicketFigli) > 0 Then

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
                        & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_SEGNALANTE WHERE TAB_TIPOLOGIA_SEGNALANTE.ID=ID_TIPOLOGIA_SEGNALANTE) AS TIPO_SEGNALANTE, " _
                        & "TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                        & "EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                        & "COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, SEGNALAZIONI.TELEFONO1, SEGNALAZIONI.TELEFONO2," _
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
                        & "(CASE WHEN ID_STATO = 10 THEN (SELECT MAX(NOTE) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                        & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                        & ") ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                        & ",'FALSE' AS CHECK1 " _
                        & ",(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                        & ",'' as figli2 " _
                        & ",ID_SEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE " _
                        & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                        & ",NVL(SISCOM_MI.GETDATA(DATA_CHIUSURA),(SELECT GETDATA(MAX(DATA_ORA)) FROM SISCOM_MI.EVENTI_SEGNALAZIONI WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND VALORE_NEW = 'CHIUSA')) AS DATA_CHIUSURA, (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_MANUTENZIONE WHERE ID = SEGNALAZIONI.ID_TIPOLOGIA_MANUTENZIONE) AS CATEGORIZZAZIONE_MANUTENZIONE, " _
                        & " (CASE WHEN (SELECT COUNT(*) FROM SISCOM_MI.ALLEGATI_WS WHERE ID_OGGETTO = SEGNALAZIONI.ID AND STATO = 0)>0 THEN 'SÌ' ELSE 'NO' END) AS ALLEGATI_PRESENTI, ID_PROGRAMMA_ATTIVITA, DATA_ORA_RICHIESTA " _
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
                        rigadt2.Item("TIPO_INT") = par.IfNull(lettore("TIPO_INT"), DBNull.Value)
                        rigadt2.Item("TIPO0") = par.IfNull(lettore("TIPO0"), DBNull.Value)
                        rigadt2.Item("TIPO1") = par.IfNull(lettore("TIPO1"), DBNull.Value)
                        rigadt2.Item("TIPO2") = par.IfNull(lettore("TIPO2"), DBNull.Value)
                        rigadt2.Item("TIPO3") = par.IfNull(lettore("TIPO3"), DBNull.Value)
                        rigadt2.Item("TIPO4") = par.IfNull(lettore("TIPO4"), DBNull.Value)
                        rigadt2.Item("TIPO_SEGNALANTE") = par.IfNull(lettore("TIPO_SEGNALANTE"), DBNull.Value)
                        rigadt2.Item("STATO") = par.IfNull(lettore("STATO"), DBNull.Value)
                        rigadt2.Item("INDIRIZZO") = par.IfNull(lettore("INDIRIZZO"), DBNull.Value)
                        rigadt2.Item("RICHIEDENTE") = par.IfNull(lettore("RICHIEDENTE"), DBNull.Value)
                        rigadt2.Item("TELEFONO1") = par.IfNull(lettore("TELEFONO1"), DBNull.Value)
                        rigadt2.Item("TELEFONO2") = par.IfNull(lettore("TELEFONO2"), DBNull.Value)
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
                        rigadt2.Item("CATEGORIZZAZIONE_MANUTENZIONE") = par.IfNull(lettore("CATEGORIZZAZIONE_MANUTENZIONE"), DBNull.Value)
                        rigadt2.Item("ALLEGATI_PRESENTI") = par.IfNull(lettore("ALLEGATI_PRESENTI"), DBNull.Value)
                        rigadt2.Item("ID_PROGRAMMA_ATTIVITA") = par.IfNull(lettore("ID_PROGRAMMA_ATTIVITA"), DBNull.Value)
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                par.modalDialogMessage("Ricerca Segnalazioni", "Esportazione eseguita correttamente.", Page, "successo", "..\/FileTemp\/" & nomeFile)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=1;", True)
            MultiViewBottoni.ActiveViewIndex = 1
            MultiViewRicerca.ActiveViewIndex = 1
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazione - btnExport_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnExport3_Click(sender As Object, e As System.EventArgs) Handles btnExport3.Click
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT " _
                & " NUM AS ""N°""," _
                & " ID_PERICOLO_SEGNALAZIONE," _
                & " (CASE WHEN ID_PERICOLO_SEGNALAZIONE=0 THEN 'BLU' WHEN ID_PERICOLO_SEGNALAZIONE=1 THEN 'BIANCO' WHEN ID_PERICOLO_SEGNALAZIONE=2 THEN 'VERDE' WHEN ID_PERICOLO_SEGNALAZIONE=3 THEN 'GIALLO' WHEN ID_PERICOLO_SEGNALAZIONE=4 THEN 'ROSSO' ELSE '' END) AS ""CRIT."" ," _
                & " TIPO0 AS ""CATEGORIA""," _
                & " TIPO1 AS ""CATEGORIA 1"", " _
                & " TIPO2 AS ""CATEGORIA 2""," _
                & " TIPO3 AS ""CATEGORIA 3""," _
                & " TIPO4 AS ""CATEGORIA 4""," _
                & " STATO, " _
                & " INDIRIZZO, " _
                & " RICHIEDENTE, " _
                & " CODICE_RU AS ""CODICE CONTRATTO""," _
                & " DATA_INSERIMENTO AS ""DATA INS."", " _
                & " DESCRIZIONE," _
                & " FILIALE AS ""SEDE TERRITORIALE"", " _
                & " REPLACE(NOTE_C,'(nota chiusura) ','') AS ""NOTA DI CHIUSURA""," _
                & " FIGLI AS ""TICKET FIGLI""," _
                & " ID_SEGNALAZIONE_PADRE AS ""N° SEGN. PADRE""," _
                & " DATA_EMISSIONE AS ""DATA EMISSIONE ORDINE""," _
                & " DATA_CHIUSURA AS ""DATA CHIUSURA SEGNALAZIONE"" " _
                & " FROM SISCOM_MI.EXPORT_SEGNALAZIONI " _
                & " UNION " _
                & " SELECT SEGNALAZIONI.ID AS ""N°""," _
                & " ID_PERICOLO_SEGNALAZIONE," _
                & " (CASE WHEN ID_PERICOLO_SEGNALAZIONE=0 THEN 'BLU' WHEN ID_PERICOLO_SEGNALAZIONE=1 THEN 'BIANCO' WHEN ID_PERICOLO_SEGNALAZIONE=2 THEN 'VERDE' WHEN ID_PERICOLO_SEGNALAZIONE=3 THEN 'GIALLO' WHEN ID_PERICOLO_SEGNALAZIONE=4 THEN 'ROSSO' ELSE '' END) AS ""CRIT."" ," _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID = ID_TIPO_SEGNALAZIONE) AS ""CATEGORIA""," _
                & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID = ID_TIPO_SEGN_LIVELLO_1) AS ""CATEGORIA 1""," _
                & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID = ID_TIPO_SEGN_LIVELLO_2) AS ""CATEGORIA 2""," _
                & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID = ID_TIPO_SEGN_LIVELLO_3) AS ""CATEGORIA 3""," _
                & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID = ID_TIPO_SEGN_LIVELLO_4) AS ""CATEGORIA 4""," _
                & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO," _
                & " EDIFICI.DENOMINAZIONE AS INDIRIZZO," _
                & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
                & " (CASE WHEN SEGNALAZIONI.ID_UNITA IS NOT NULL THEN (SELECT MAX (COD_CONTRATTO) FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID IN (SELECT ID_CONTRATTO " _
                & " FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_CONTRATTUALE.ID_UNITA =SEGNALAZIONI.ID_UNITA) " _
                & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, 1, 8) BETWEEN NVL (RAPPORTI_UTENZA.DATA_DECORRENZA,'10000000') AND NVL (RAPPORTI_UTENZA.DATA_RICONSEGNA," _
                & "'30000000')) ELSE NULL END) AS ""CODICE CONTRATTO"", " _
                & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS ""DATA INS.""," _
                & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE," _
                & " NVL(SISCOM_MI.TAB_FILIALI.NOME,'') AS ""SEDE TERRITORIALE""," _
                & " (CASE WHEN ID_STATO = 10 THEN (SELECT MAX(NOTE) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID " _
                & " AND id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX (data_ora) FROM siscom_mi.SEGNALAZIONI_NOTE WHERE id_segnalazione = SEGNALAZIONI.ID " _
                & " AND id_tipo_segnalazione_note=2)) ELSE '' END) AS ""NOTA DI CHIUSURA""," _
                & " (SELECT COUNT (ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE = SEGNALAZIONI.ID) AS ""TICKET FIGLI""," _
                & " ID_SEGNALAZIONE_PADRE AS ""N° SEGN. PADRE""," _
                & " (SELECT MAX(SISCOM_MI.Getdata(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato NOT IN (5,6) AND ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS ""DATA EMISSIONE ORDINE""," _
                & " SISCOM_MI.Getdata(DATA_CHIUSURA) AS ""DATA CHIUSURA SEGNALAZIONE"" " _
                & " FROM siscom_mi.TAB_STATI_SEGNALAZIONI," _
                & " siscom_mi.SEGNALAZIONI, " _
                & " siscom_mi.TAB_FILIALI, " _
                & " siscom_mi.EDIFICI, " _
                & " siscom_mi.UNITA_IMMOBILIARI, " _
                & " siscom_mi.TIPOLOGIE_GUASTI, " _
                & " sepa.OPERATORI " _
                & " WHERE TAB_STATI_SEGNALAZIONI.ID = SEGNALAZIONI.id_stato " _
                & " AND SEGNALAZIONI.id_stato <> -1 " _
                & " AND SEGNALAZIONI.id_struttura = TAB_FILIALI.ID(+) " _
                & " AND siscom_mi.SEGNALAZIONI.id_edificio = siscom_mi.EDIFICI.ID(+) " _
                & " AND siscom_mi.UNITA_IMMOBILIARI.ID(+) = siscom_mi.SEGNALAZIONI.id_unita " _
                & " AND sepa.OPERATORI.ID = SEGNALAZIONI.id_operatore_ins " _
                & " AND SEGNALAZIONI.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
                & " AND SUBSTR(SEGNALAZIONI.DATA_ORA_RICHIESTA,1,8)='" & Format(Now, "yyyyMMdd") & "' " _
                & " ORDER BY 2,20"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            connData.chiudi()
            dt.Columns.RemoveAt(1)
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", dt, True, , )
                If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Ricerca Segnalazioni", "Esportazione eseguita correttamente.", Page, "successo", "..\/FileTemp\/" & nomeFile)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
                End If
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=1;", True)
            End If
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazione - btnExport3_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub btnExportRisultatiRicerca_Click(sender As Object, e As EventArgs) Handles btnExportRisultatiRicerca.Click
        Try
            CaricaRisultati(True)
            Dim dt As Data.DataTable = CType(Session.Item("DataGridSegnalaz"), Data.DataTable)
            Dim dtRisultatiRicerca As New Data.DataTable
            dtRisultatiRicerca.Columns.Add("NUMERO SEGNALAZIONE")
            dtRisultatiRicerca.Columns.Add("CATEGORIA")
            dtRisultatiRicerca.Columns.Add("SOTTO-CATEGORIA 1")
            dtRisultatiRicerca.Columns.Add("SOTTO-CATEGORIA 2")
            dtRisultatiRicerca.Columns.Add("DATA INSERIMENTO")
            dtRisultatiRicerca.Columns.Add("CRITICITÀ")
            dtRisultatiRicerca.Columns.Add("STATO")
            dtRisultatiRicerca.Columns.Add("RICHIEDENTE")
            dtRisultatiRicerca.Columns.Add("CODICE RU")
            dtRisultatiRicerca.Columns.Add("INDIRIZZO")
            dtRisultatiRicerca.Columns.Add("SEDE TERRITORIALE")
            dtRisultatiRicerca.Columns.Add("DESCRIZIONE")
            dtRisultatiRicerca.Columns.Add("DATA EMISSIONE PRIMO ORDINE")
            dtRisultatiRicerca.Columns.Add("DATA PRIMO APPUNTAMENTO")
            dtRisultatiRicerca.Columns.Add("DATA PRIMA ISTANZA")
            dtRisultatiRicerca.Columns.Add("DATA PRIMO INTERVENTO")
            dtRisultatiRicerca.Columns.Add("DATA CHIUSURA")
            dtRisultatiRicerca.Columns.Add("NOTA CHIUSURA")
            connData.apri()
            For Each riga As Data.DataRow In dt.Rows
                Dim rigaRisRicerca As Data.DataRow = dtRisultatiRicerca.NewRow
                rigaRisRicerca.Item("NUMERO SEGNALAZIONE") = par.IfNull(riga.Item("NUM"), "")
                rigaRisRicerca.Item("CATEGORIA") = par.IfNull(riga.Item("TIPO0"), "")
                rigaRisRicerca.Item("SOTTO-CATEGORIA 1") = par.IfNull(riga.Item("TIPO1"), "")
                rigaRisRicerca.Item("SOTTO-CATEGORIA 2") = par.IfNull(riga.Item("TIPO2"), "")
                rigaRisRicerca.Item("DATA INSERIMENTO") = par.IfNull(riga.Item("DATA_INSERIMENTO"), "")
                Dim crit As String = ""
                If Not String.IsNullOrEmpty(riga.Item("ID_PERICOLO_SEGNALAZIONE").ToString) Then
                    Select Case riga.Item("ID_PERICOLO_SEGNALAZIONE")
                        Case "0"
                            crit = "BLU"
                        Case "1"
                            crit = "BIANCO"
                        Case "2"
                            crit = "VERDE"
                        Case "3"
                            crit = "GIALLO"
                        Case "4"
                            crit = "ROSSO"
                    End Select
                    rigaRisRicerca.Item("CRITICITÀ") = crit
                End If
                rigaRisRicerca.Item("STATO") = par.IfNull(riga.Item("STATO"), "")
                rigaRisRicerca.Item("RICHIEDENTE") = par.IfNull(riga.Item("RICHIEDENTE"), "")
                rigaRisRicerca.Item("CODICE RU") = par.IfNull(riga.Item("CODICE_RU"), "")
                rigaRisRicerca.Item("INDIRIZZO") = par.IfNull(riga.Item("INDIRIZZO"), "")
                rigaRisRicerca.Item("SEDE TERRITORIALE") = par.IfNull(riga.Item("FILIALE"), "")
                rigaRisRicerca.Item("DESCRIZIONE") = par.IfNull(riga.Item("DESCRIZIONE"), "")
                par.cmd.CommandText = "SELECT GETDATA(DATA_INIZIO_ORDINE) AS DATA_INIZIO_ORDINE," _
                    & " GETDATA(DATA_INIZIO_INTERVENTO) AS DATA_INIZIO_INTERVENTO " _
                    & " FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI = " & riga.Item("ID") _
                    & " And ID = (SELECT MIN(ID) FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI =  " & riga.Item("ID") & ")"
                Dim dtManu As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                If dtManu.Rows.Count > 0 Then
                    For Each rigaManu As Data.DataRow In dtManu.Rows
                        rigaRisRicerca.Item("DATA EMISSIONE PRIMO ORDINE") = par.IfNull(rigaManu.Item("DATA_INIZIO_ORDINE"), "")
                        rigaRisRicerca.Item("DATA PRIMO INTERVENTO") = par.IfNull(rigaManu.Item("DATA_INIZIO_INTERVENTO"), "")
                    Next
                End If

                par.cmd.CommandText = "SELECT GETDATA(DATA_APPUNTAMENTO) AS DATA_APPUNTAMENTO " _
                                    & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID = (SELECT MIN(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER)"
                Dim dataAppuntamento = par.cmd.ExecuteScalar

                If Not IsNothing(dataAppuntamento) Then
                    rigaRisRicerca.Item("DATA PRIMO INTERVENTO") = par.IfNull(dataAppuntamento, "")
                End If
                rigaRisRicerca.Item("DATA CHIUSURA") = par.IfNull(riga.Item("DATA_CHIUSURA"), "")
                rigaRisRicerca.Item("NOTA CHIUSURA") = par.IfNull(riga.Item("NOTE_C"), "")

                par.cmd.CommandText = " select getdata(data) as data " _
                    & " from sepa.vsa_decisioni_rev_C where cod_decisione=7 and id_domanda = " _
                    & " (select sepa.domande_bando_vsa.id from sepa.domande_bando_Vsa where id_Segnalazione= " & riga.Item("ID") & ")"
                Dim dataPrimaIstanza = par.cmd.ExecuteScalar

                If Not IsNothing(dataPrimaIstanza) Then
                    rigaRisRicerca.Item("DATA PRIMA ISTANZA") = par.IfNull(dataPrimaIstanza, "")
                End If

                dtRisultatiRicerca.Rows.Add(rigaRisRicerca)
            Next
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile1 = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", dtRisultatiRicerca)
            'Dim nomeFile1 = xls.EsportaExcelDaDataGridWithDTColorRadGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", RadGridSegnalazioni, dt, True, , , "CRITICITA'")
            '  Dim nomeFile = xls.EsportaExcelDaDataGridWithDTColorRadGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", RadGridSegnalazioni, dt, True, , , "CRITICITA'")
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile1) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                par.modalDialogMessage("Ricerca Segnalazioni", "Esportazione eseguita correttamente.", Page, "successo", "..\/FileTemp\/" & nomeFile1)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazione - btnExportRisultatiRicerca_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub btnExport123_Click(sender As Object, e As EventArgs) Handles btnExport123.Click
        Try
            CaricaRisultati(True)
            Dim dt As Data.DataTable = CType(Session.Item("DataGridSegnalaz"), Data.DataTable)
            Dim dtRisultatiRicerca As New Data.DataTable
            dtRisultatiRicerca.Columns.Add("NUMERO SEGNALAZIONE")
            dtRisultatiRicerca.Columns.Add("CRITICITÀ")
            dtRisultatiRicerca.Columns.Add("CATEGORIA")
            dtRisultatiRicerca.Columns.Add("SOTTO-CATEGORIA 1")
            dtRisultatiRicerca.Columns.Add("SOTTO-CATEGORIA 2")
            dtRisultatiRicerca.Columns.Add("SOTTO-CATEGORIA 3")
            dtRisultatiRicerca.Columns.Add("SOTTO-CATEGORIA 4")
            dtRisultatiRicerca.Columns.Add("STATO")
            dtRisultatiRicerca.Columns.Add("INDIRIZZO")
            dtRisultatiRicerca.Columns.Add("SCALA")
            dtRisultatiRicerca.Columns.Add("PIANO")
            dtRisultatiRicerca.Columns.Add("RICHIEDENTE")
            dtRisultatiRicerca.Columns.Add("CODICE CONTRATTO")
            dtRisultatiRicerca.Columns.Add("DATA INSERIMENTO")
            dtRisultatiRicerca.Columns.Add("DESCRIZIONE")
            dtRisultatiRicerca.Columns.Add("SEDE TERRITORIALE")
            dtRisultatiRicerca.Columns.Add("N° SEGNALAZIONE PADRE")
            dtRisultatiRicerca.Columns.Add("NOTA CHIUSURA")
            dtRisultatiRicerca.Columns.Add("DATA CHIUSURA SEGNALAZIONE")
            connData.apri()
            For Each riga As Data.DataRow In dt.Rows
                Dim rigaRisRicerca As Data.DataRow = dtRisultatiRicerca.NewRow
                rigaRisRicerca.Item("NUMERO SEGNALAZIONE") = par.IfNull(riga.Item("NUM"), "")
                Dim crit As String = ""
                If Not String.IsNullOrEmpty(riga.Item("ID_PERICOLO_SEGNALAZIONE").ToString) Then
                    Select Case riga.Item("ID_PERICOLO_SEGNALAZIONE")
                        Case "0"
                            crit = "BLU"
                        Case "1"
                            crit = "BIANCO"
                        Case "2"
                            crit = "VERDE"
                        Case "3"
                            crit = "GIALLO"
                        Case "4"
                            crit = "ROSSO"
                    End Select
                    rigaRisRicerca.Item("CRITICITÀ") = crit
                End If
                rigaRisRicerca.Item("CATEGORIA") = par.IfNull(riga.Item("TIPO0"), "")
                rigaRisRicerca.Item("SOTTO-CATEGORIA 1") = par.IfNull(riga.Item("TIPO1"), "")
                rigaRisRicerca.Item("SOTTO-CATEGORIA 2") = par.IfNull(riga.Item("TIPO2"), "")
                rigaRisRicerca.Item("SOTTO-CATEGORIA 3") = par.IfNull(riga.Item("TIPO3"), "")
                rigaRisRicerca.Item("SOTTO-CATEGORIA 4") = par.IfNull(riga.Item("TIPO4"), "")
                rigaRisRicerca.Item("STATO") = par.IfNull(riga.Item("STATO"), "")
                rigaRisRicerca.Item("INDIRIZZO") = par.IfNull(riga.Item("INDIRIZZO"), "")
                par.cmd.CommandText = "SELECT DESCRIZIONE " _
                                    & " FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN " _
                                    & " (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID IN " _
                                    & " (SELECT ID_UNITA FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & riga.Item("ID") & "))"
                Dim scala As String = par.IfEmpty(par.cmd.ExecuteScalar.ToString, "")
                rigaRisRicerca.Item("SCALA") = scala
                par.cmd.CommandText = "SELECT DESCRIZIONE " _
                                    & " FROM SISCOM_MI.TIPO_LIVELLO_PIANO " _
                                    & " WHERE COD IN (SELECT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " _
                                    & " (SELECT ID_UNITA FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & riga.Item("ID") & "))"
                Dim piano As String = par.IfEmpty(par.cmd.ExecuteScalar.ToString, "")
                rigaRisRicerca.Item("PIANO") = piano


                rigaRisRicerca.Item("RICHIEDENTE") = par.IfNull(riga.Item("RICHIEDENTE"), "")
                rigaRisRicerca.Item("CODICE CONTRATTO") = par.IfNull(riga.Item("CODICE_RU"), "")
                rigaRisRicerca.Item("DATA INSERIMENTO") = par.IfNull(riga.Item("DATA_INSERIMENTO"), "")
                rigaRisRicerca.Item("DESCRIZIONE") = par.IfNull(riga.Item("DESCRIZIONE"), "")
                rigaRisRicerca.Item("SEDE TERRITORIALE") = par.IfNull(riga.Item("FILIALE"), "")
                rigaRisRicerca.Item("NOTA CHIUSURA") = par.IfNull(riga.Item("NOTE_C"), "")
                rigaRisRicerca.Item("N° SEGNALAZIONE PADRE") = par.IfNull(riga.Item("ID_SEGNALAZIONE_PADRE"), "")
                rigaRisRicerca.Item("DATA CHIUSURA SEGNALAZIONE") = par.IfNull(riga.Item("DATA_CHIUSURA"), "")
                dtRisultatiRicerca.Rows.Add(rigaRisRicerca)
            Next
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile1 = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", dtRisultatiRicerca)
            'Dim nomeFile1 = xls.EsportaExcelDaDataGridWithDTColorRadGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", RadGridSegnalazioni, dt, True, , , "CRITICITA'")
            '  Dim nomeFile = xls.EsportaExcelDaDataGridWithDTColorRadGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", RadGridSegnalazioni, dt, True, , , "CRITICITA'")
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile1) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                par.modalDialogMessage("Ricerca Segnalazioni", "Esportazione eseguita correttamente.", Page, "successo", "..\/FileTemp\/" & nomeFile1)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazione - btnExport123_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class