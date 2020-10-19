
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_RicercaSegnalazioniNOEmesso
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            DropDownListUrgenza.LoadContentFile("Gravita.xml")
            controlloBM()
            caricaFornitori()
            caricaStrutture()
            CaricaBManager()
            caricaComplessi()
            caricaEdifici()
            CaricaStatoSegnalazioni()
            CaricaTipoSegnalazione()
            CaricaCanale()
            CaricaTipologiaSegnalante()
            cmbSedeTerritoriale.Focus()
            txtDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub

    Private Sub CaricaCanale()
        Try
            connData.apri()
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.CANALE WHERE FL_AGENDA = 1 ORDER BY ID ASC", cmbCanale, "ID", "DESCRIZIONE", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaStrutture()
        Try
            connData.apri()
            'PROVENIENZA DA AGENDA SEGNALAZIONI
            If Session.Item("ID_STRUTTURA") = "-1" Then
                par.caricaComboTelerik("SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID>100 AND ID_TIPO_ST=0 ORDER BY NOME", cmbSedeTerritoriale, "ID", "NOME", True)
            Else
                par.cmd.CommandText = "SELECT ID_TIPO_ST FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & Session.Item("ID_STRUTTURA")
                Dim tipo As Integer = 0
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    tipo = par.IfNull(lettore(0), 0)
                End If
                lettore.Close()
                If tipo = 2 Then
                    par.caricaComboTelerik("SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID>100 AND ID_TIPO_ST=0 ORDER BY NOME", cmbSedeTerritoriale, "ID", "NOME", True)
                Else
                    par.caricaComboTelerik("SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & Session.Item("ID_STRUTTURA") & " AND TAB_FILIALI.ID>100 AND ID_TIPO_ST=0 ORDER BY NOME", cmbSedeTerritoriale, "ID", "NOME", False)
                End If
            End If
            connData.chiudi()
            If cmbSedeTerritoriale.Items.Count = 1 Then
                If cmbSedeTerritoriale.SelectedValue <> idStruttura.Value Then
                    Dim item1 As New RadComboBoxItem()
                    item1.Text = strutturaNome.Value
                    item1.Value = idStruttura.Value
                    cmbSedeTerritoriale.Items.Add(item1)
                    'cmbSedeTerritoriale.Items.Add(New ListItem(strutturaNome.Value, idStruttura.Value))
                    cmbSedeTerritoriale.SelectedValue = idStruttura.Value
                End If
            Else
                Try
                    cmbSedeTerritoriale.SelectedValue = idStruttura.Value
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaFornitori()
        Try
            connData.apri()
            par.caricaComboTelerik("SELECT ID,RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI IN (SELECT ID FROM SISCOM_MI.SEGNALAZIONI))) ORDER BY RAGIONE_SOCIALE", DropDownListFornitore, "ID", "RAGIONE_SOCIALE", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologiaSegnalante()
        Try
            connData.apri()
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_TIPOLOGIA_SEGNALANTE ORDER BY ID ASC", DropDownListTipoSegnalante, "ID", "DESCRIZIONE", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaComplessi()
        Try
            connData.apri()
            Dim condizioneSedeTerritoriale As String = ""
            If cmbSedeTerritoriale.SelectedValue <> "-1" Then
                condizioneSedeTerritoriale = " AND ID_FILIALE= " & cmbSedeTerritoriale.SelectedValue
            End If
            Dim condizioneBM As String = ""
            If par.IfEmpty(cmbBManager.SelectedValue, -1) <> "-1" Then
                condizioneBM = " AND COMPLESSI_IMMOBILIARI.ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID_BM = " & par.IfEmpty(cmbBManager.SelectedValue, -1) & ")"
            End If
            par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneSedeTerritoriale & condizioneBM & " ORDER BY DENOMINAZIONE", cmbComplesso, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaEdifici()
        Try
            connData.apri()
            Dim condizioneComplesso As String = ""
            If cmbComplesso.SelectedValue <> "-1" Then
                condizioneComplesso = " AND ID_COMPLESSO=" & cmbComplesso.SelectedValue
            Else
                If cmbSedeTerritoriale.SelectedValue <> "-1" Then
                    condizioneComplesso = " AND ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE= " & cmbSedeTerritoriale.SelectedValue & ") "
                End If
            End If
            Dim condizioneBM As String = ""
            If par.IfEmpty(cmbBManager.SelectedValue, -1) <> "-1" Then
                condizioneBM = " AND iD_BM = " & par.IfEmpty(cmbBManager.SelectedValue, -1)
            End If
            par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 " & condizioneComplesso & condizioneBM & " ORDER BY DENOMINAZIONE", cmbEdificio, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaStatoSegnalazioni()
        Try
            connData.apri()
            'par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE ID >=0 AND ID<>10", CheckBoxListStato, "ID", "DESCRIZIONE")
            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE descrizione<>'ORDINE EMESSO' AND ID >=0", CheckBoxListStato, "ID", "DESCRIZIONE")
            connData.chiudi()
            For Each elemento As ListItem In CheckBoxListStato.Items
                If elemento.Text <> "CHIUSA" Then
                    elemento.Selected = True
                End If
            Next
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipoSegnalazione()
        Try
            connData.apri()
            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE ID=1 ORDER BY ID", CheckBoxListTipoSegnalazione, "ID", "DESCRIZIONE")
            CheckBoxListTipoSegnalazione.Items(0).Selected = True
            connData.chiudi()
            cmbTipoSegnalazioneLivello1.Items.Clear()
            cmbTipoSegnalazioneLivello2.Items.Clear()
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
            CaricaSottoCategorie()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
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
                Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE=" & tipologiaSelezionata & " ORDER BY DESCRIZIONE"
                'connData.apri(False)
                par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello1, "ID", "DESCRIZIONE", True)
                'connData.chiudi(False)
                If cmbTipoSegnalazioneLivello1.Items.Count = 2 Then
                    If Not IsNothing(cmbTipoSegnalazioneLivello1.Items.FindItemByValue("-1")) Then
                        cmbTipoSegnalazioneLivello1.Items.Remove(cmbTipoSegnalazioneLivello1.Items.FindItemByValue("-1"))
                    End If
                End If
                cmbTipoSegnalazioneLivello2.Items.Clear()
                cmbTipoSegnalazioneLivello3.Items.Clear()
                cmbTipoSegnalazioneLivello4.Items.Clear()
            End If


        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello2()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_1=" & cmbTipoSegnalazioneLivello1.SelectedValue & " ORDER BY DESCRIZIONE"
            'connData.apri(False)
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello2, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
            If cmbTipoSegnalazioneLivello2.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello2.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello2.Items.Remove(cmbTipoSegnalazioneLivello2.Items.FindItemByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello3.Items.Clear()
            cmbTipoSegnalazioneLivello4.Items.Clear()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello3()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2=" & cmbTipoSegnalazioneLivello2.SelectedValue & " ORDER BY DESCRIZIONE"
            'connData.apri(False)
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello3, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
            If cmbTipoSegnalazioneLivello3.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello3.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello3.Items.Remove(cmbTipoSegnalazioneLivello3.Items.FindItemByValue("-1"))
                End If
            End If
            cmbTipoSegnalazioneLivello4.Items.Clear()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologieLivello4()
        Try
            Dim query As String = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_3=" & cmbTipoSegnalazioneLivello3.SelectedValue & " ORDER BY DESCRIZIONE"
            'connData.apri(False)
            par.caricaComboTelerik(query, cmbTipoSegnalazioneLivello4, "ID", "DESCRIZIONE", True)
            'connData.chiudi(False)
            If cmbTipoSegnalazioneLivello4.Items.Count = 2 Then
                If Not IsNothing(cmbTipoSegnalazioneLivello4.Items.FindItemByValue("-1")) Then
                    cmbTipoSegnalazioneLivello4.Items.Remove(cmbTipoSegnalazioneLivello4.Items.FindItemByValue("-1"))
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    'Private Sub CaricaTipo()
    '    Try
    '        connData.apri()
    '        par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI ", cmbTipo, "ID", "DESCRIZIONE", True)
    '        connData.chiudi()
    '    Catch ex As Exception
    '        connData.chiudi()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Redirect("../../../Errore.aspx", False)
    '    End Try
    'End Sub
Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub
Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
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
        Dim listaTipi As String = ""
        For Each elemento As ListItem In CheckBoxListTipoSegnalazione.Items
            If elemento.Selected = True Then
                If listaTipi = "" Then
                    listaTipi = elemento.Value
                Else
                    listaTipi &= "," & elemento.Value
                End If
            End If
        Next
        'Response.Write("<script>document.location.href='RisultatiSegn.aspx?FOR=" & DropDownListFornitore.SelectedValue & "&COMP=" & cmbComplesso.SelectedValue & "&TIPO=" & cmbTipoSegnalazione.SelectedValue & "&T=" & cmbTipo.SelectedItem.Value & "&D=" & par.FormattaData(txtDal.Text) & "&A=" & par.FormattaData(txtAl.Text) & "&F=" & cmbSedeTerritoriale.SelectedItem.Value & "&E=" & cmbEdificio.SelectedItem.Value & "&O=" & txtSegnalante.Text & "&STAT=" & listaStati & "&URG=" & DropDownListUrgenza.SelectedValue & "&NUM=" & TextBoxNumero.Text & "&MINDA=" & TextBoxMinutiDal.Text & "&MINA=" & TextBoxMinutiAl.Text & "&OREA=" & TextBoxOreAl.Text & "&OREDA=" & TextBoxOreDal.Text & "&ORDINE=" & RadioButtonListOrdine.SelectedValue & "';</script>")
        Dim urg As String = ""
        If DropDownListUrgenza.Enabled = True Then
            urg = DropDownListUrgenza.SelectedValue
        End If
        Response.Write("<script>document.location.href='RisultatiSegnNOEmesso.aspx?FOR=" _
                       & DropDownListFornitore.SelectedValue _
                       & "&COMP=" & cmbComplesso.SelectedValue _
                       & "&D=" & par.FormattaData(txtDal.Text) _
                       & "&A=" & par.FormattaData(txtAl.Text) _
                       & "&F=" & cmbSedeTerritoriale.SelectedItem.Value _
                       & "&E=" & cmbEdificio.SelectedItem.Value _
                       & "&O=" & txtSegnalante.Text _
                       & "&STAT=" & listaStati _
                       & "&URG=" & urg _
                       & "&NUM=" & TextBoxNumero.Text _
                       & "&MINDA=" & TextBoxMinutiDal.Text _
                       & "&MINA=" & TextBoxMinutiAl.Text _
                       & "&OREA=" & TextBoxOreAl.Text _
                       & "&OREDA=" & TextBoxOreDal.Text _
                       & "&CANALE=" & cmbCanale.SelectedValue _
                       & "&CAT0=" & listaTipi _
                       & "&CAT1=" & cmbTipoSegnalazioneLivello1.SelectedValue _
                       & "&CAT2=" & cmbTipoSegnalazioneLivello2.SelectedValue _
                       & "&CAT3=" & cmbTipoSegnalazioneLivello3.SelectedValue _
                       & "&CAT4=" & cmbTipoSegnalazioneLivello4.SelectedValue _
                       & "&IDBM=" & par.IfEmpty(cmbBManager.SelectedValue, -1) _
                       & "&IDTIPOSEGN=" & DropDownListTipoSegnalante.SelectedValue _
                       & "';</script>")
    End Sub

    Private Sub CaricaBManager()
        Try
            connData.apri()
            Dim CondStruttura As String = ""
            If Me.cmbSedeTerritoriale.SelectedValue <> "-1" Then
                CondStruttura = " where  ID_STRUTTURA = " & Me.cmbSedeTerritoriale.SelectedValue
            End If
            par.cmd.CommandText = "SELECT BUILDING_MANAGER.ID,((CODICE )|| " _
                                & "(CASE WHEN (SELECT OPERATORE FROM OPERATORI WHERE ID =(SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE ID_BM = BUILDING_MANAGER.ID AND TIPO_OPERATORE = 1 AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') AND NVL (inizio_validita, '29991231') <= TO_CHAR (SYSDATE, 'YYYYMMDD'))) " _
                                & "IS NOT NULL THEN  ' - '||(SELECT operatori.cognome||' '||operatori.nome FROM OPERATORI WHERE ID =(SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE ID_BM = BUILDING_MANAGER.ID AND TIPO_OPERATORE = 1 AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD') AND NVL (inizio_validita, '29991231') <= TO_CHAR (SYSDATE, 'YYYYMMDD') )) " _
                                & "ELSE '' END)|| " _
                                & "(CASE WHEN (SELECT OPERATORE FROM OPERATORI WHERE ID =(SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE ID_BM = BUILDING_MANAGER.ID AND TIPO_OPERATORE = 2 AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD')AND NVL (inizio_validita, '29991231') <= TO_CHAR (SYSDATE, 'YYYYMMDD'))) " _
                                & "IS NOT NULL THEN  ' - '||(SELECT operatori.cognome||' '||operatori.nome FROM OPERATORI WHERE ID =(SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE ID_BM = BUILDING_MANAGER.ID AND TIPO_OPERATORE = 2 AND NVL(fine_validita,'29991231') >= TO_CHAR(SYSDATE,'YYYYMMDD')AND NVL (inizio_validita, '29991231') <= TO_CHAR (SYSDATE, 'YYYYMMDD'))) " _
                                & "ELSE '' END) " _
                                & ")AS MANAGER " _
                                & "FROM SISCOM_MI.BUILDING_MANAGER " & CondStruttura & " order by  BUILDING_MANAGER.codice asc"
            par.caricaComboTelerik(par.cmd.CommandText, cmbBManager, "ID", "MANAGER", True)


            connData.chiudi()
            Try
                cmbBManager.SelectedValue = idBM.Value
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cmbSedeTerritoriale_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbSedeTerritoriale.SelectedIndexChanged
        CaricaBManager()
        caricaComplessi()
        caricaEdifici()
        'cmbComplesso.Focus()
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        caricaEdifici()
        cmbEdificio.Focus()
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello1.SelectedIndexChanged
        CaricaTipologieLivello2()
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello2.SelectedIndexChanged
        CaricaTipologieLivello3()
    End Sub

    Protected Sub cmbTipoSegnalazioneLivello3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazioneLivello3.SelectedIndexChanged
        CaricaTipologieLivello4()
    End Sub

    Protected Sub CheckBoxListTipoSegnalazione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListTipoSegnalazione.SelectedIndexChanged
        CaricaSottoCategorie()
    End Sub
    Private Sub CaricaSottoCategorie()
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
    Private Sub controlloBM()
        Try
            connData.apri()
            Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
            'se ce ne sono di più prendiamo il primo
            par.cmd.CommandText = "SELECT ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE ID_OPERATORE=" & idOperatore & " AND FINE_VALIDITA = 30000101"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idBM.Value = par.IfNull(lettore(0), "-1")
            End If
            lettore.Close()
            If idBM.Value <> "-1" Then
                par.cmd.CommandText = "SELECT ID_sTRUTTURA,(select nome from siscom_mi.tab_filiali where tab_filiali.id=id_Struttura) as nome FROM SISCOM_MI.BUILDING_MANAGER WHERE ID = " & idBM.Value
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    idStruttura.Value = par.IfNull(lettore("id_Struttura"), "-1")
                    strutturaNome.Value = par.IfNull(lettore("nome"), "")
                End If
                lettore.Close()
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub cmbBManager_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbBManager.SelectedIndexChanged
        caricaComplessi()
        caricaEdifici()
    End Sub
End Class
