Imports Telerik.Web.UI
Imports Telerik.Web.UI.Scheduler
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb
Imports System.Collections.Generic

Partial Class FORNITORI_Default
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim itemXX As RadPanelItem '= RadPanelBar1.Items(2)
    Dim RadAgenda As RadScheduler '= TryCast(itemXX.Items(0).FindControl("RadAgenda"), RadScheduler)

    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)
        Dim item As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        TryCast(TryCast(sender, CheckBox).NamingContainer, GridItem).Selected = TryCast(sender, CheckBox).Checked
        Dim checkHeader As Boolean = True
        For Each dataItem As GridDataItem In Griglia.MasterTableView.Items
            If Not TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked Then
                checkHeader = False
                Exit For
            End If
        Next
        Dim headerItem As GridHeaderItem = TryCast(Griglia.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        TryCast(headerItem.FindControl("headerChkbox"), CheckBox).Checked = checkHeader
    End Sub
    Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
        Dim item As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
        For Each dataItem As GridDataItem In Griglia.MasterTableView.Items
            TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked = headerCheckBox.Checked
            dataItem.Selected = headerCheckBox.Checked

            '///////////////////
            Dim box As CheckBox = CType(sender, CheckBox)
            Dim target As Hashtable = Nothing

            target = CustomersChecked(dataItem("ID_MANUTENZIONE").Text)

            If box.Checked Then
                target(dataItem("ID_MANUTENZIONE").Text) = True
            Else
                target(dataItem("ID_MANUTENZIONE").Text) = Nothing
            End If
            '/////////////////////////
        Next
    End Sub

    Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As AjaxRequestEventArgs)
        'If e.Argument = TextBox1.ClientID Then
        '    Label1.Text = TextBox1.Text
        'End If
    End Sub

    Protected Sub RadAgenda_AppointmentDataBound(sender As Object, e As Telerik.Web.UI.SchedulerEventArgs) 'Handles RadAgenda.AppointmentDataBound
        e.Appointment.AllowDelete = False
        e.Appointment.AllowEdit = False
        e.Appointment.Font.Name = "arial"
        e.Appointment.Font.Size = 7

        If e.Appointment.Description.IndexOf("1", StringComparison.OrdinalIgnoreCase) >= 0 Then 'DA VERIFICARE
            '//// 1433/2019 Da verdino a Giallo
            'e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#d4ffbc")
            e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#eeff03")
            e.Appointment.ToolTip = e.Appointment.Subject & " - ORDINE DA VERIFICARE "
            iTotNumDaVerificare = iTotNumDaVerificare + 1
        End If
        If e.Appointment.Description.IndexOf("2", StringComparison.OrdinalIgnoreCase) >= 0 Then ' IN CARICO
            e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#f18888")
            e.Appointment.ToolTip = e.Appointment.Subject & " - ORDINE IN CARICO"
            iTotNumInCarico = iTotNumInCarico + 1
        End If
        'If e.Appointment.Description.IndexOf("6", StringComparison.OrdinalIgnoreCase) >= 0 Then 'ANNULLATO
        '    e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#b51a00")
        '    e.Appointment.ToolTip = e.Appointment.Subject & " - ORDINE ANNULLATO"
        '    iTotNumAnnullato = iTotNumAnnullato + 1
        'End If
        If e.Appointment.Description.IndexOf("10", StringComparison.OrdinalIgnoreCase) >= 0 Then 'IN CARICO MA ANNULLATO
            e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#b51a00")
            e.Appointment.ToolTip = e.Appointment.Subject & " - ORDINE IN CARICO MA RITARDO"
            iTotNumAnnullato = iTotNumAnnullato + 1
        End If
        If e.Appointment.Description.IndexOf("8", StringComparison.OrdinalIgnoreCase) >= 0 Then 'EVASO
            '//// 1433/2019 Da Bianco a Verde
            'e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
            e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#08920f")
            e.Appointment.ToolTip = e.Appointment.Subject & " - ORDINE EVASO"
            iTotEvaso = iTotEvaso + 1
        End If
        If e.Appointment.Description.IndexOf("9", StringComparison.OrdinalIgnoreCase) >= 0 Then '
            e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffaa00")
            e.Appointment.ToolTip = e.Appointment.Subject & " - RICHIESTA CONSUNTIVAZIONE"
            iTotRC = iTotRC + 1
        End If
        If e.Appointment.Description.IndexOf("3", StringComparison.OrdinalIgnoreCase) >= 0 Then 'CONSUNTIVATO
            e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#297fb8")
            e.Appointment.ToolTip = e.Appointment.Subject & " - ORDINE DA CONSUNTIVARE"
            iTotDC = iTotDC + 1
        End If

        If sOrdine <> "" Then
            If UCase(e.Appointment.Subject) = UCase(sOrdine) Then
                RadAgenda.SelectedDate = e.Appointment.Start
                e.Appointment.BackColor = Drawing.Color.Red
                sOrdine = ""
            End If
            If InStr(UCase(e.Appointment.Subject), UCase(sOrdine)) > 0 Then
                RadAgenda.SelectedDate = e.Appointment.Start
                e.Appointment.BackColor = Drawing.Color.Red
                sOrdine = ""
            End If
        End If

        If e.Appointment.Visible = True Then
            e.Appointment.CssClass = "ChangeCursor"
        End If

        RadAgenda.AllowDelete = False
        RadAgenda.AllowEdit = False
        RadAgenda.AllowInsert = False

    End Sub

    Protected Sub RadAgenda_NavigationComplete(sender As Object, e As Telerik.Web.UI.SchedulerNavigationCompleteEventArgs) 'Handles RadAgenda.NavigationComplete

        Session.Add("g4", RadAgenda.VisibleRangeStart.ToString)

        Dim itemX As RadPanelItem = RadPanelBar1.Items(2)
        Dim cmbGiorni As RadDropDownList = TryCast(itemX.Items(0).FindControl("cmbGiorni"), RadDropDownList)
        Dim label2 As Label = TryCast(itemX.Items(0).FindControl("Label2"), Label)
        Dim Label3 As Label = TryCast(itemX.Items(0).FindControl("Label3"), Label)
        If RadAgenda.SelectedView = SchedulerViewType.MonthView Then
            cmbGiorni.Visible = False
            label2.Visible = False
            Label3.Visible = False
        Else
            cmbGiorni.Visible = True
            label2.Visible = True
            Label3.Visible = True
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
    End Sub

    Private Sub CaricaStatoSegnalazioni()
        Try

            Dim item As RadPanelItem = RadPanelBar1.Items(0)
            Dim CheckList As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)

            connData.apri()
            '// 1433/2019
            par.caricaCheckBoxList("SELECT ID,DECODE(DESCRIZIONE,'DA VERIFICARE','DA VERIFICARE','IN CARICO','IN CARICO','EVASO','EVASO','RICHIESTA CONSUNTIVO ODL','RICHIESTA CONSUNTIVAZIONE','DA CONTABILIZZARE','DA CONSUNTIVARE','IN CARICO MA RITARDO','IN CARICO MA RITARDO') AS DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO WHERE PROGR<>0 ORDER BY PROGR ASC", CheckList, "ID", "DESCRIZIONE")
            'par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO WHERE PROGR<>0 ORDER BY PROGR ASC", CheckList, "ID", "DESCRIZIONE")
            connData.chiudi()
            For Each elemento As ListItem In CheckList.Items
                elemento.Selected = True
            Next
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - CaricaStatoSegnalazioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

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
            iTotNumBozza = 0
            iTotNumAnnullato = 0
            iTotNumDaVerificare = 0
            iTotEvaso = 0
            iTotDC = 0
            iTotRC = 0
            iTotNumInCarico = 0

            itemXX = RadPanelBar1.Items(2)
            RadAgenda = TryCast(itemXX.Items(0).FindControl("RadAgenda"), RadScheduler)

            If Not IsPostBack Then
                RadPanelBar1.Items(1).Expanded = True
                RadPanelBar1.Items(2).Expanded = True
                RadPanelBar1.Items(0).Visible = False  ' Hanno chiesto di non visualizzare più i filtri 
                '                RadPanelBar1.Items(2).Expanded = True

                CaricaViste()
                CaricaStatoSegnalazioni()
                sPrimoIngresso = "1"
                CaricaMaschera()
                sPrimoIngresso = "0"

                '                RadPanelBar1.Items(2).Expanded = False

            End If



        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza: Fornitori - Ordini - Carica - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try

    End Sub

    Private Sub CaricaMaschera()
        Try
                VerificaOperatore()

            If Not RadButtonDirettoreLavori.Checked Then
                Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
                Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonContab"), RadButton)
                PULSANTE.Visible = False
            Else
                Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
                Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonContab"), RadButton)
                PULSANTE.Visible = True
            End If

            'If OPS.Value = "1" Then
            '    Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
            '    Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonContab"), RadButton)
            '    PULSANTE.Visible = False
            'Else
            '    Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
            '    Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonContab"), RadButton)
            '    PULSANTE.Visible = True
            'End If

            If RadButtonBuildingManager.Checked Or RadButtonFieldQualityManager.Checked Or RadButtonTecnicoAmministrativo.Checked Then
                Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
                Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonAccetta"), RadButton)
                PULSANTE.Visible = False
            Else
                Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
                Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonAccetta"), RadButton)

                If OPS.Value = "1" Then
                    PULSANTE.Visible = False
                Else
                    PULSANTE.Visible = True
                End If
            End If
            If RadButtonBuildingManager.Checked Or RadButtonFieldQualityManager.Checked Or RadButtonTecnicoAmministrativo.Checked Then
                Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
                Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonContesta"), RadButton)
                PULSANTE.Visible = False
            Else
                    Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
                Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonContesta"), RadButton)
                If OPS.Value = "1" Then
                    PULSANTE.Visible = False
                Else
                    PULSANTE.Visible = True
                End If
            End If

            If Session.Item("MOD_FORNITORI_SLE") = "1" Then
                Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
                    Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonContab"), RadButton)
                    PULSANTE.Visible = False
                End If
                If Session.Item("MOD_FORNITORI_SLE") = "1" Then
                    Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
                Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonAccetta"), RadButton)
                PULSANTE.Visible = False
            End If

            If Session.Item("MOD_FORNITORI_SLE") = "1" Then
                Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
                Dim PULSANTE As RadButton = TryCast(item1.Items(0).FindControl("RadButtonContesta"), RadButton)
                    PULSANTE.Visible = False
                End If
                Dim item As RadPanelItem = RadPanelBar1.Items(0)
                Dim cmbList As RadComboBox = TryCast(item.Items(0).FindControl("cmbFornitori"), RadComboBox)
                If iIndiceFornitore <> "0" Then
                par.caricaComboTelerik("select FORNITORI.ID,FORNITORI.ragione_sociale " _
                                       & " from siscom_mi.fornitori where FORNITORI.ID IN " _
                                       & " (SELECT DISTINCT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE ID IN " & iIndiceFornitore & ") " _
                                       & " ORDER BY FORNITORI.RAGIONE_SOCIALE ASC", cmbList, "ID", "RAGIONE_SOCIALE", True, "-1", "---")
            Else
                    'segn. 414/2017
                    par.caricaComboTelerik("select FORNITORI.ID,FORNITORI.ragione_sociale from siscom_mi.fornitori where FORNITORI.ID in (select distinct id_fornitore from siscom_mi.APPALTI WHERE nvl(MODULO_FORNITORI,0)=1) ORDER BY FORNITORI.RAGIONE_SOCIALE ASC", cmbList, "ID", "RAGIONE_SOCIALE", True, "-1", "---")
                    'par.caricaComboTelerik("select FORNITORI.ID,FORNITORI.ragione_sociale from siscom_mi.fornitori ORDER BY FORNITORI.RAGIONE_SOCIALE ASC", cmbList, "ID", "RAGIONE_SOCIALE", True, "-1", "---")
                End If

                If iIndiceFornitore <> "0" Then
                    cmbList = TryCast(item.Items(0).FindControl("cmbDL"), RadComboBox)
                par.caricaComboTelerik("select distinct id,operatori.cognome||' '||operatori.nome as DL,cognome,nome from siscom_mi.APPALTI_DL,operatori where operatori.id=appalti_dl.id_operatore AND APPALTI_DL.DATA_FINE_INCARICO = '30000000' and appalti_dl.id_gruppo in (select id_gruppo from siscom_mi.appalti where appalti.id in " & iIndiceFornitore & ") order by cognome asc,nome asc", cmbList, "ID", "DL", True, "-1", "---")
                    cmbList = TryCast(item.Items(0).FindControl("cmbBM"), RadComboBox)
                par.caricaComboTelerik("select distinct operatori.id,operatori.cognome||' '||operatori.nome as BM,cognome,nome from siscom_mi.BUILDING_MANAGER,siscom_mi.BUILDING_MANAGER_OPERATORI,operatori where BUILDING_MANAGER_OPERATORI.id_bm=BUILDING_MANAGER.id and operatori.id=BUILDING_MANAGER_OPERATORI.id_operatore and BUILDING_MANAGER_OPERATORI.id_bm in (select distinct(edifici.id_bm) from siscom_mi.appalti_lotti_patrimonio,siscom_mi.appalti,siscom_mi.edifici where appalti.id=appalti_lotti_patrimonio.id_appalto and edifici.id=appalti_lotti_patrimonio.id_edificio and appalti.id in " & iIndiceFornitore & ") order by cognome asc,nome asc", cmbList, "ID", "BM", True, "-1", "---")
                Else
                    cmbList = TryCast(item.Items(0).FindControl("cmbDL"), RadComboBox)
                    'par.caricaComboTelerik("select distinct id,operatori.cognome||' '||operatori.nome as DL,cognome,nome from siscom_mi.APPALTI_DL,operatori where operatori.id=appalti_dl.id_operatore order by cognome asc,nome asc", cmbList, "ID", "DL", True, "-1", "---")
                    par.caricaComboTelerik("select distinct id,operatori.cognome||' '||operatori.nome as DL,cognome,nome from operatori where operatori.FL_AUTORIZZAZIONE_ODL=1 order by cognome asc,nome asc", cmbList, "ID", "DL", True, "-1", "---")
                    cmbList = TryCast(item.Items(0).FindControl("cmbBM"), RadComboBox)
                    par.caricaComboTelerik("select distinct operatori.id,operatori.cognome||' '||operatori.nome as BM,cognome,nome from siscom_mi.BUILDING_MANAGER,siscom_mi.BUILDING_MANAGER_OPERATORI,operatori where BUILDING_MANAGER_OPERATORI.id_bm=BUILDING_MANAGER.id and operatori.id=BUILDING_MANAGER_OPERATORI.id_operatore order by cognome asc,nome asc", cmbList, "ID", "BM", True, "-1", "---")
                End If


            Dim itemX As RadPanelItem = RadPanelBar1.Items(2)
            Dim cmbGiorni As RadDropDownList = TryCast(itemX.Items(0).FindControl("cmbGiorni"), RadDropDownList)

            ' Carico solo la prima volta
            If cmbGiorni.Items.Count < 3 Then



                cmbGiorni.Items.Add(New Telerik.Web.UI.DropDownListItem("7", "7"))
                cmbGiorni.Items.Add(New Telerik.Web.UI.DropDownListItem("14", "14"))
                cmbGiorni.Items.Add(New Telerik.Web.UI.DropDownListItem("21", "21"))
            End If

                If IsNothing(Request.Cookies(Session.Item("ID_OPERATORE") & "_FoDaView")) = False Then
                    cmbGiorni.SelectedText = Request.Cookies(Session.Item("ID_OPERATORE") & "_FoDaView").Value
                Else
                    cmbGiorni.SelectedText = "14"
                    SettaCookie()
                End If
                RicavaOrdini()
                sPrimoIngresso = "0"
                sStrSqlRisultati = sStrSql

                iTotNumAnnullatoT = iTotNumAnnullato / 2

                iTotNumDaVerificareT = iTotNumDaVerificare / 2
                iTotNumInCaricoT = iTotNumInCarico / 2
                iTotEvasoT = iTotEvaso / 2
                iTotRCT = iTotRC / 2
                iTotDCT = iTotDC / 2
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)



        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza: Fornitori - Ordini - CaricaMaschera - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try

    End Sub

    Private Function CaricaCriteri() As String
        Try
            CaricaCriteri = ""
            Dim sStrStato As String = ""
            Dim bTrovato As Boolean = False
            Dim sCompara As String = ""
            Dim bTrovatoData As Boolean = False

            Dim item As RadPanelItem = RadPanelBar1.Items(0)

            Dim cmbList As RadComboBox = TryCast(item.Items(0).FindControl("cmbFornitori"), RadComboBox)
            If cmbList.Text <> "---" Then
                If cmbList.SelectedValue <> "" Then
                    If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                    CaricaCriteri = CaricaCriteri & " APPALTI.ID in (select id from siscom_mi.appalti where id_fornitore  = " & cmbList.SelectedValue & ") "
                    bTrovato = True
                Else
                    If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                    CaricaCriteri = CaricaCriteri & " FORNITORI.RAGIONE_SOCIALE LIKE '%" & cmbList.Text & "%' "
                    bTrovato = True
                End If
            Else
                If iIndiceFornitore <> "0" Then
                    CaricaCriteri = CaricaCriteri & " APPALTI.ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID IN " & iIndiceFornitore & ")"
                    bTrovato = True
                End If
            End If

            cmbList = TryCast(item.Items(0).FindControl("cmbDL"), RadComboBox)
            'If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" And cmbList.Text = "---" And Session.Item("FL_SUPERDIRETTORE") = "0" Then
            '    cmbList.Text = Session.Item("NOME_OPERATORE")
            '    cmbList.Items.FindItemByValue(Session.Item("ID_OPERATORE")).Selected = True
            '    cmbList.Enabled = False
            'End If

            If cmbList.Text <> "---" Then
                If cmbList.SelectedValue <> "" Then
                    If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                    CaricaCriteri = CaricaCriteri & " APPALTI.ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE ID_OPERATORE= " & cmbList.SelectedValue & " AND APPALTI_DL.DATA_FINE_INCARICO = '30000000') "
                    bTrovato = True
                Else
                    cmbList.Text = "---"
                End If
            End If

            cmbList = TryCast(item.Items(0).FindControl("cmbBM"), RadComboBox)
            If cmbList.Text <> "---" Then
                If cmbList.SelectedValue <> "" Then
                    If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                    CaricaCriteri = CaricaCriteri & " MANUTENZIONI.ID_STRUTTURA IN (SELECT ID_STRUTTURA FROM SISCOM_MI.BUILDING_MANAGER,SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE BUILDING_MANAGER.ID=BUILDING_MANAGER_OPERATORI.ID_BM AND BUILDING_MANAGER_OPERATORI.ID_OPERATORE= " & cmbList.SelectedValue & ") "
                    bTrovato = True
                Else
                    cmbList.Text = "---"
                End If
            End If
            'MANUTENZIONI.ID_STRUTTURA IN (SELECT ID_STRUTTURA FROM SISCOM_MI.BUILDING_MANAGER,SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE BUILDING_MANAGER.ID=BUILDING_MANAGER_OPERATORI.ID_BM AND BUILDING_MANAGER_OPERATORI.ID_OPERATORE=1)

            Dim CheckList As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)
            For Each elemento As ListItem In CheckList.Items
                If elemento.Selected = True Then
                    sStrStato = sStrStato & elemento.Value & ","
                End If
            Next
            If sStrStato <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                sStrStato = "   SEGNALAZIONI_FORNITORI.ID_STATO IN (" & Mid(sStrStato, 1, Len(sStrStato) - 1) & ") "
                bTrovato = True
            End If
            CaricaCriteri = CaricaCriteri & sStrStato

            sOrdine = ""
            Dim ControlloTesto As RadTextBox = TryCast(item.Items(0).FindControl("txtNumIntDa"), RadTextBox)

            'If idSelRisultatiODL.Value <> "" And idSelRisultatiREP.Value <> "" Then
            '    ControlloTesto.Text = idSelRisultatiODL.Value
            '    sOrdine = UCase(ControlloTesto.Text)
            '    sRepertorio = ""
            '    ControlloTesto = TryCast(item.Items(0).FindControl("txtNumRepertorio"), RadTextBox)
            '    ControlloTesto.Text = idSelRisultatiREP.Value
            '    sRepertorio = UCase(ControlloTesto.Text)
            '    If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
            '    'CaricaCriteri = CaricaCriteri & " 'REP. '||APPALTI.NUM_REPERTORIO||' ODL '||MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO='REP. " & sRepertorio & " ODL " & sOrdine & "' "
            '    CaricaCriteri = CaricaCriteri & " 'ODL '||MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO||' REP. '||APPALTI.NUM_REPERTORIO ='ODL " & sOrdine & " REP. " & sRepertorio & "' "
            '    bTrovato = True
            '    'sOrdine = "REP. " & sRepertorio & " ODL " & sOrdine
            '    sOrdine = "ODL " & sOrdine & " REP. " & sRepertorio
            'Else
            '    If idSelRisultatiODL.Value <> "" Then
            '        ControlloTesto.Text = idSelRisultatiODL.Value
            '    End If
            '    If ControlloTesto.Text <> "" Then
            '        sOrdine = "ODL " & UCase(ControlloTesto.Text)
            '        If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
            '        CaricaCriteri = CaricaCriteri & " 'ODL '||MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO='" & sOrdine & "' "
            '        bTrovato = True
            '    End If

            '    sRepertorio = ""
            '    ControlloTesto = TryCast(item.Items(0).FindControl("txtNumRepertorio"), RadTextBox)
            '    If idSelRisultatiREP.Value <> "" Then
            '        ControlloTesto.Text = idSelRisultatiREP.Value
            '    End If
            '    If ControlloTesto.Text <> "" Then
            '        sRepertorio = UCase(ControlloTesto.Text)
            '        If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
            '        CaricaCriteri = CaricaCriteri & " APPALTI.NUM_REPERTORIO='" & sRepertorio & "' "
            '        bTrovato = True
            '        sOrdine = sOrdine & "REP. " & sRepertorio
            '    End If
            'End If


            Dim Pulsante As CheckBox = TryCast(item.Items(0).FindControl("chkNonRegolare"), CheckBox)
            If Pulsante.Checked = True Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " SEGNALAZIONI_FORNITORI.ID IN (SELECT ID_SEGNALAZIONE FROM SISCOM_MI.SEGNALAZIONI_FO_IRR) "
                bTrovato = True
            End If
            sStrStato = ""


            Pulsante = TryCast(item.Items(0).FindControl("chANNULLATO"), CheckBox)
            If Pulsante.Checked = True Then
                If sStrStato <> "" Then sStrStato = sStrStato & " OR "
                sStrStato = sStrStato & " MANUTENZIONI.STATO=10 "
            End If
            If sStrStato <> "" Then
                sStrStato = " ( " & sStrStato & " ) AND SEGNALAZIONI_FORNITORI.ID_STATO=6 "

                If bTrovato = True Then
                    CaricaCriteri = CaricaCriteri & " AND " & sStrStato
                Else
                    CaricaCriteri = sStrStato
                End If
            End If

            '/////////////////////////////
            '// 1433/2019
            Dim itemG As RadPanelItem = RadPanelBar1.Items(0)
            Dim cmbListG As RadDropDownList = TryCast(itemG.Items(0).FindControl("cmbGravita"), RadDropDownList)
            If cmbListG.SelectedText <> "---" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " PERICOLO_SEGNALAZIONI.ID=" & cmbListG.SelectedValue & " "
                bTrovato = True
            End If

            Dim DATA_RICHIESTA_FILTRO As String = "(CASE  WHEN MANUTENZIONI.PROGR IS NULL THEN (SUBSTR (DATA_ORA_RICHIESTA, 1, 8))  ELSE  (SELECT MAX(SUBSTR(EVENTI_MANUTENZIONE.DATA_ORA,1,8)) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE COD_EVENTO='F91' AND ID_MANUTENZIONE=MANUTENZIONI.ID)  END)"
            Dim ControlloData As RadDatePicker = TryCast(item.Items(0).FindControl("txtRicDA"), RadDatePicker)
            If ControlloData.SelectedDate.ToString() <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " SUBSTR(" & DATA_RICHIESTA_FILTRO & ",7,4)||SUBSTR(" & DATA_RICHIESTA_FILTRO & ",4,2)||SUBSTR(" & DATA_RICHIESTA_FILTRO & ",1,2)>= '" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
                bTrovato = True
            End If
            ControlloData = TryCast(item.Items(0).FindControl("txtRicA"), RadDatePicker)
            If ControlloData.SelectedDate.ToString() <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " SUBSTR(" & DATA_RICHIESTA_FILTRO & ",7,4)||SUBSTR(" & DATA_RICHIESTA_FILTRO & ",4,2)||SUBSTR(" & DATA_RICHIESTA_FILTRO & ",1,2)<='" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
                bTrovato = True
            End If
            ControlloData = TryCast(item.Items(0).FindControl("txtFineLavoriDA"), RadDatePicker)
            If ControlloData.SelectedDate.ToString() <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO>= '" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
                bTrovato = True
            End If
            ControlloData = TryCast(item.Items(0).FindControl("txtFineLavoriA"), RadDatePicker)
            If ControlloData.SelectedDate.ToString() <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO<='" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
                bTrovato = True
            End If

            ControlloData = TryCast(item.Items(0).FindControl("txtPGIDA"), RadDatePicker)
            If ControlloData.SelectedDate.ToString() <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " MANUTENZIONI.DATA_PGI>= '" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
                bTrovato = True
            End If

            ControlloData = TryCast(item.Items(0).FindControl("txtPGIA"), RadDatePicker)
            If ControlloData.SelectedDate.ToString() <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " MANUTENZIONI.DATA_PGI<= '" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
                bTrovato = True
            End If

            ControlloData = TryCast(item.Items(0).FindControl("txtTDLDA"), RadDatePicker)
            If ControlloData.SelectedDate.ToString() <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " MANUTENZIONI.DATA_TDL>= '" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
                bTrovato = True
            End If

            ControlloData = TryCast(item.Items(0).FindControl("txtTDLA"), RadDatePicker)
            If ControlloData.SelectedDate.ToString() <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " MANUTENZIONI.DATA_TDL<= '" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
                bTrovato = True
            End If

            Dim ControlloScost As RadTextBox = TryCast(item.Items(0).FindControl("txtScostamentoDPIL"), RadTextBox)
            If ControlloScost.Text <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " ( TO_DATE (SUBSTR(MANUTENZIONI.DATA_PGI,1,8), 'yyyymmdd') - " & ControlloScost.Text & ">= TO_DATE(SUBSTR(MANUTENZIONI.DATA_INIZIO_INTERVENTO,1,8), 'yyyymmdd') ) "
            End If
            Dim ControlloScost1 As RadTextBox = TryCast(item.Items(0).FindControl("txtScostamentoDPFL"), RadTextBox)
            If ControlloScost1.Text <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " ( TO_DATE (SUBSTR(MANUTENZIONI.DATA_TDL,1,8), 'yyyymmdd') - " & ControlloScost1.Text & ">= TO_DATE(SUBSTR(MANUTENZIONI.DATA_FINE_INTERVENTO,1,8), 'yyyymmdd') ) "
            End If



            '///////////////////////////////////
            sOrdine = ""
            Dim lcheched As String = ""
            Dim itemS As RadPanelItem = RadPanelBar1.Items(1)
            Dim Griglia As RadGrid = TryCast(itemS.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
            For Each dataItem As GridDataItem In Griglia.MasterTableView.Items
                '///////////////////////////////
                Dim box As CheckBox = CType(dataItem.FindControl("CheckBox1"), CheckBox)
                Dim target As Hashtable = Nothing

                target = CustomersChecked(dataItem("ID_MANUTENZIONE").Text)

                If box.Checked Then
                    target(dataItem("ID_MANUTENZIONE").Text) = True
                Else
                    target(dataItem("ID_MANUTENZIONE").Text) = Nothing
                End If
                '////////////////////////////////

                If TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked = True Then
                    If dataItem("NUM_ODL").Text <> "" And dataItem("NUM_REPERTORIO").Text <> "" Then
                        If lcheched <> "" Then lcheched = lcheched & " OR "
                        lcheched = lcheched & " 'ODL '||MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO||' REP. '||APPALTI.NUM_REPERTORIO ='ODL " & dataItem("NUM_ODL").Text & " REP. " & dataItem("NUM_REPERTORIO").Text & "' "
                        sOrdine = UCase(dataItem("NUM_ODL").Text)
                        sOrdine = "ODL " & sOrdine & " REP. " & dataItem("NUM_REPERTORIO").Text
                    Else
                        If dataItem("NUM_ODL").Text <> "" Then
                            If lcheched <> "" Then lcheched = lcheched & " OR "
                            CaricaCriteri = CaricaCriteri & " 'ODL '||MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO='" & dataItem("NUM_ODL").Text & "' "
                            sOrdine = "ODL " & UCase(dataItem("NUM_ODL").Text)

                        End If
                        If dataItem("NUM_REPERTORIO").Text <> "" Then
                            If lcheched <> "" Then lcheched = lcheched & " OR "
                            CaricaCriteri = CaricaCriteri & " APPALTI.NUM_REPERTORIO='" & dataItem("NUM_REPERTORIO").Text & "' "
                            sOrdine = sOrdine & "REP. " & dataItem("NUM_REPERTORIO").Text
                        End If
                    End If
                End If
            Next

            If lcheched <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " (" & lcheched & ")"
            End If
            '/////////////////////////////////////


            '/////////////////////////////

            If CaricaCriteri <> "" Then CaricaCriteri = CaricaCriteri & " and "
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - CaricaCriteri - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function

    Private Function RicavaFornitore(ByVal ODL As String) As String
        RicavaFornitore = ""
        Try
            connData.apri()
            par.cmd.CommandText = "select fornitori.ragione_sociale from siscom_mi.manutenzioni,siscom_mi.fornitori,SISCOM_MI.APPALTI where APPALTI.ID=MANUTENZIONI.ID_APPALTO AND fornitori.id=APPALTI.id_fornitore and progr||'/'||anno='" & ODL & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                RicavaFornitore = par.IfNull(myReader("ragione_sociale"), "")
            End If
            myReader.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - RicavaFornitori - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function

    Private Sub VerificaOperatore()
        Try
            connData.apri()
            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("LIVELLO_WEB"), "0") = "1" Or par.IfNull(myReader("FL_SUPERDIRETTORE"), "0") = "1" Then
                    iIndiceFornitore = "0"
                    IndiceFornitore.Value = "0"
                    OPS.Value = "0"
                Else
                    If par.IfNull(myReader("MOD_FO_ID_FO"), "0") <> "0" Then
                        iIndiceFornitore = "(select id from siscom_mi.appalti where id_fornitore = " & par.IfNull(myReader("MOD_FO_ID_FO"), 0) & ")"
                        IndiceFornitore.Value = "1"
                        OPS.Value = "1"
                    Else
                        iIndiceFornitore = ""
                        IndiceFornitore.Value = ""
                        If RadButtonBuildingManager.Checked Then
                            OPS.Value = "1"
                            IndiceFornitore.Value = "0"
                            iIndiceFornitore = "SELECT DISTINCT APPALTI.ID as id_appalto " _
                                            & "FROM SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.EDIFICI,SISCOM_MI.BUILDING_MANAGER_OPERATORI " _
                                            & "WHERE Appalti.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO = EDIFICI.ID " _
                                            & "And EDIFICI.ID_BM = BUILDING_MANAGER_OPERATORI.ID_BM AND BUILDING_MANAGER_OPERATORI.ID_OPERATORE = " & Session.Item("id_operatore")
                            '    Dim myReaderBM As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'Do While myReaderBM.Read
                            '        iIndiceFornitore = iIndiceFornitore & myReaderBM("ID_APPALTO") & ","
                            'Loop
                            'myReaderBM.Close()
                        End If
                        If RadButtonDirettoreLavori.Checked Then
                            OPS.Value = "0"
                            iIndiceFornitore = "SELECT DISTINCT APPALTI.ID as id_appalto FROM SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_DL WHERE APPALTI.ID=APPALTI_DL.ID_GRUPPO AND APPALTI_DL.DATA_FINE_INCARICO = '30000000' AND APPALTI_DL.ID_OPERATORE=" & Session.Item("id_operatore")
                            'Dim myReaderDL As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'Do While myReaderDL.Read
                            '    iIndiceFornitore = iIndiceFornitore & myReaderDL("ID_APPALTO") & ","
                            'Loop
                            'myReaderDL.Close()
                        End If
                        Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
                        If RadButtonFieldQualityManager.Checked Then
                            OPS.Value = "1"
                                iIndiceFornitore = "0"
                                IndiceFornitore.Value = "0"
                            'If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
                            '    iIndiceFornitore = "0"
                            '    IndiceFornitore.Value = "0"
                            'ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
                            '    iIndiceFornitore = "SELECT DISTINCT APPALTI.ID as id_appalto FROM SISCOM_MI.APPALTI " _
                            '    & " WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_EDIFICIO IN " _
                            '    & " (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
                            'End If
                        End If
                        If RadButtonTecnicoAmministrativo.Checked Then
                            OPS.Value = "1"
                            If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
                                iIndiceFornitore = "0"
                                IndiceFornitore.Value = "0"
                            ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
                                iIndiceFornitore = "SELECT DISTINCT APPALTI.ID as id_appalto FROM SISCOM_MI.APPALTI " _
                                & " WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_EDIFICIO IN " _
                                & " (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
                                'Dim myReaderFQM As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                'Do While myReaderFQM.Read
                                '    iIndiceFornitore = iIndiceFornitore & myReaderFQM("ID_APPALTO") & ","
                                'Loop
                                'myReaderFQM.Close()
                            End If
                        End If
                        If iIndiceFornitore <> "0" And iIndiceFornitore <> "" Then
                            iIndiceFornitore = "(" & Mid(iIndiceFornitore, 1, Len(iIndiceFornitore)) & ")"
                            IndiceFornitore.Value = "1"
                        ElseIf iIndiceFornitore = "" Then
                            iIndiceFornitore = "-1"
                            IndiceFornitore.Value = "-1"
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

    Public Property bDettagliTrovatoElemento() As String
        Get
            If Not (ViewState("par_bDettagliTrovatoElemento") Is Nothing) Then
                Return CStr(ViewState("par_bDettagliTrovatoElemento"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_bDettagliTrovatoElemento") = value
        End Set
    End Property

    Private Sub RicavaOrdini()
        Try
            Dim dtAgenda As New System.Data.DataTable
            Dim s As String = CaricaCriteri() ' hanno voluto togliere i filtri

            RadAgenda.DataStartField = "DATA_INIZIO_INTERVENTO"
            RadAgenda.DataSubjectField = "RIFERIMENTO"
            '/// Nota :  In agenda nonn essendo gestito l'orario visualizzava un giono in meno perchè data fine = a mezzanotte 
            RadAgenda.DataEndField = "DATA_FINE_INTERVENTO_AGENDA"
            RadAgenda.DataKeyField = "ID_MANUTENZIONE"
            RadAgenda.DataDescriptionField = "STATO_S"

            Dim SS As String = ""
            'If OPS.Value = "1" Then
            '    SS = "  manutenzioni.id_appalto in (select id from SISCOM_MI.appalti where modulo_fornitori=1 AND MODULO_FORNITORI_GE=1) AND "
            'Else
            '    SS = "  manutenzioni.id_appalto in (select id from SISCOM_MI.appalti where modulo_fornitori=1) AND "
            'End If
            Dim stringaFiltroAppalti As String = ""
            If Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
                stringaFiltroAppalti = " AND APPALTI.ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.OPERATORI_FO_APPALTI WHERE ID_OPERATORE = " & Session.Item("ID_OPERATORE") & ") "
            End If

            If OPS.Value = "1" Then
                SS = "  APPALTI.modulo_fornitori=1 AND APPALTI.MODULO_FORNITORI_GE=1 AND "
            Else
                SS = "  APPALTI.modulo_fornitori=1 AND "
            End If

            Dim sStrSql1 As String = ""
            Dim filtro As String = ""
            If RadButtonBuildingManager.Checked = True Then
                filtro &= " and siscom_mi.getbuildingmanager(manutenzioni.id,1) like '%" & Session.Item("ID_OPERATORE") & "%' "
            End If
            If RadButtonTecnicoAmministrativo.Checked Then
                filtro &= " and " _
                       & " ( " _
                       & " case when manutenzioni.id_complesso is not null then (select distinct id_filiale from SISCOM_MI.complessi_immobiliari where id = manutenzioni.id_complesso) " _
                       & " when manutenzioni.id_edificio is not null then (select id_filiale from siscom_mi.complessi_immobiliari where id in (select distinct id_complesso from SISCOM_MI.edifici where id = manutenzioni.id_edificio)) " _
                       & " when MANUTENZIONI.ID_UNITA_IMMOBILIARI is not null then (select distinct id_filiale from SISCOM_MI.complessi_immobiliari where id in (select id_complesso from SISCOM_MI.edifici where edifici.id = (select id_edificio from SISCOM_MI.unita_immobiliari where id = manutenzioni.ID_UNITA_IMMOBILIARI))) " _
                       & " end " _
                       & " ) = " & Session.Item("ID_STRUTTURA")
            End If

            sStrSql = "select ('ODL '||MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO||' REP. '||APPALTI.NUM_REPERTORIO) AS RIFERIMENTO," _
               & "APPALTI.NUM_REPERTORIO,MANUTENZIONI.DESCRIZIONE AS DESCR_MA,FORNITORI.RAGIONE_SOCIALE,FORNITORI.ID AS IDF,MANUTENZIONI.ID AS ID_MANUTENZIONE, " _
               & "MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO AS NUM_ODL, " _
               & "(CASE WHEN MANUTENZIONI.DATA_PGI IS NOT NULL THEN TO_DATE (MANUTENZIONI.DATA_PGI || '08', 'yyyymmddHH') ELSE (case when DATA_INIZIO_INTERVENTO is not null then TO_DATE (DATA_INIZIO_INTERVENTO || '08', 'yyyymmddHH') else TO_DATE((SELECT SUBSTR(DATA_ORA,1,8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND COD_EVENTO='F91'),'yyyymmdd') end) END) AS DATA_INIZIO_INTERVENTO_1, " _
               & "(CASE " _
               & "WHEN (MANUTENZIONI.DATA_TDL < to_char(sysdate,'yyyyMMdd') AND SEGNALAZIONI_FORNITORI.ID_STATO=10 AND SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO IS NULL) then sysdate WHEN MANUTENZIONI.DATA_TDL IS NOT NULL " _
               & " THEN TO_DATE (MANUTENZIONI.DATA_TDL || '09', 'yyyymmddHH') " _
               & "ELSE (case when DATA_INIZIO_INTERVENTO Is Not null then TO_DATE (DATA_INIZIO_INTERVENTO || '09', 'yyyymmddHH') else TO_DATE((SELECT SUBSTR(DATA_ORA,1,8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND COD_EVENTO='F91'),'yyyymmdd') end) END) AS DATA_FINE_INTERVENTO_1, " _
               & " (CASE WHEN SEGNALAZIONI_FORNITORI.ID_STATO=8 THEN -9 WHEN SEGNALAZIONI_FORNITORI.ID_STATO=9 THEN -10 WHEN SEGNALAZIONI_FORNITORI.ID_STATO=3 THEN -3 ELSE MANUTENZIONI.STATO END) AS STATO, TO_DATE((SELECT SUBSTR(DATA_ORA,1,8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND COD_EVENTO='F91'),'yyyymmdd') AS DATA_INIZIO_RICERCA,NVL(SEGNALAZIONI_FORNITORI.ID_STATO,0) AS STATO_S,SEGNALAZIONI.ID AS IDS,TAB_STATI_SEGNALAZIONI_FO.DESCRIZIONE AS DESCR_STATO, " _
               & " (CASE  " _
               & " WHEN MANUTENZIONI.PROGR IS NULL THEN (SELECT COMPLESSI_IMMOBILIARI.COD_COMPLESSO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID(+) = SEGNALAZIONI.ID_COMPLESSO) " _
               & " ELSE " _
               & " (SELECT COMPLESSI_IMMOBILIARI.COD_COMPLESSO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID(+) = MANUTENZIONI.ID_COMPLESSO) " _
               & " END) AS COD_COMPLESSO, " _
               & " (CASE  " _
               & " WHEN MANUTENZIONI.PROGR IS NULL THEN (SELECT EDIFICI.COD_EDIFICIO FROM SISCOM_MI.EDIFICI WHERE ID(+) = SEGNALAZIONI.ID_EDIFICIO) " _
               & " ELSE " _
               & " (SELECT EDIFICI.COD_EDIFICIO FROM SISCOM_MI.EDIFICI WHERE ID(+) = MANUTENZIONI.ID_EDIFICIO) " _
               & " END) AS COD_EDIFICIO, " _
               & " " _
               & " (CASE " _
               & " WHEN MANUTENZIONI.PROGR IS NULL THEN ((CASE " _
               & "              WHEN SEGNALAZIONI.ID_EDIFICIO IS NOT NULL " _
               & " THEN " _
               & " (SELECT    INDIRIZZI.DESCRIZIONE " _
               & " || ' ' " _
               & " || INDIRIZZI.CIVICO " _
               & " || ' ' " _
               & " || INDIRIZZI.CAP " _
               & " || ' - ' " _
               & " || COMUNI_NAZIONI.NOME " _
               & " FROM SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI,SISCOM_MI.EDIFICI " _
               & " WHERE     COMUNI_NAZIONI.COD = INDIRIZZI.COD_COMUNE " _
               & " AND INDIRIZZI.ID = EDIFICI.ID_INDIRIZZO_PRINCIPALE AND INDIRIZZI.ID=SEGNALAZIONI.ID_EDIFICIO) " _
               & " ELSE " _
               & " (SELECT    INDIRIZZI.DESCRIZIONE " _
               & " || ' ' " _
               & " || INDIRIZZI.CIVICO " _
               & " || ' ' " _
               & " || INDIRIZZI.CAP " _
               & " || ' - ' " _
               & " || COMUNI_NAZIONI.NOME " _
               & " FROM SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI,SISCOM_MI.UNITA_IMMOBILIARI " _
               & " WHERE     COMUNI_NAZIONI.COD = INDIRIZZI.COD_COMUNE " _
               & " AND INDIRIZZI.ID = UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=SEGNALAZIONI.ID_UNITA) " _
               & " END)) " _
               & " ELSE " _
               & " ((CASE " _
               & "              WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL " _
               & " THEN " _
               & " (SELECT    INDIRIZZI.DESCRIZIONE " _
               & " || ' ' " _
               & " || INDIRIZZI.CIVICO " _
               & " || ' ' " _
               & " || INDIRIZZI.CAP " _
               & " || ' - ' " _
               & " || COMUNI_NAZIONI.NOME " _
               & " FROM SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI,SISCOM_MI.EDIFICI " _
               & " WHERE     COMUNI_NAZIONI.COD = INDIRIZZI.COD_COMUNE " _
               & " AND INDIRIZZI.ID = EDIFICI.ID_INDIRIZZO_PRINCIPALE AND EDIFICI.ID=MANUTENZIONI.ID_EDIFICIO) " _
               & " ELSE " _
               & " (SELECT    INDIRIZZI.DESCRIZIONE " _
               & " || ' ' " _
               & " || INDIRIZZI.CIVICO " _
               & " || ' ' " _
               & " || INDIRIZZI.CAP " _
               & " || ' - ' " _
               & " || COMUNI_NAZIONI.NOME " _
               & " FROM SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI,SISCOM_MI.COMPLESSI_IMMOBILIARI " _
               & " WHERE     COMUNI_NAZIONI.COD = INDIRIZZI.COD_COMUNE " _
               & " AND INDIRIZZI.ID = COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO AND COMPLESSI_IMMOBILIARI.ID=MANUTENZIONI.ID_COMPLESSO) " _
               & " END)) " _
               & " END) AS INDIRIZZO, MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO AS ODL1," _
               & " SISCOM_MI.GETBUILDINGMANAGER(MANUTENZIONI.ID) AS BUILDING_MANAGER, " _
               & " (SELECT (CASE WHEN UPPER (SUBSTR (TAB_FILIALI.NOME, 1, 18)) = 'SEDE TERRITORIALE ' " _
               & " AND LENGTH(TAB_FILIALI.NOME) = 19 THEN SUBSTR (TAB_FILIALI.NOME, 19, 1) ELSE TAB_FILIALI.NOME END) " _
               & " FROM SISCOM_MI.TAB_FILIALI WHERE ID = SISCOM_MI.GETSTMANUTENZIONE(MANUTENZIONI.ID)) AS ST, " _
               & " (CASE  " _
               & " WHEN MANUTENZIONI.PROGR IS NULL THEN (SEGNALAZIONI.DESCRIZIONE_RIC) " _
               & " ELSE " _
               & " (MANUTENZIONI.DESCRIZIONE) " _
               & " END) AS DESCRIZIONE_ANOMALIA, " _
               & "TO_DATE (SUBSTR(MANUTENZIONI.DATA_INIZIO_INTERVENTO,1,8), 'yyyymmdd') AS DATA_INIZIO_INTERVENTO, " _
               & "TO_DATE (SUBSTR(MANUTENZIONI.DATA_FINE_INTERVENTO,1,8), 'yyyymmdd') AS DATA_FINE_INTERVENTO, " _
               & "TO_DATE (SUBSTR(MANUTENZIONI.DATA_FINE_INTERVENTO,1,8), 'yyyymmdd') + 1 AS DATA_FINE_INTERVENTO_AGENDA, " _
               & "TO_DATE (SUBSTR(MANUTENZIONI.DATA_PGI,1,8), 'yyyymmdd') AS DATAPGI, " _
               & "TO_DATE (SUBSTR(MANUTENZIONI.DATA_TDL,1,8), 'yyyymmdd') AS DATATDL, " _
               & "(case when (nvl(TO_DATE (SUBSTR(MANUTENZIONI.DATA_PGI,1,8), 'yyyymmdd'),'') = '' or nvl(MANUTENZIONI.DATA_INIZIO_INTERVENTO,'') = '') then 'VUOTO' when MANUTENZIONI.DATA_PGI > MANUTENZIONI.DATA_INIZIO_INTERVENTO then    ( case when (   (select count(ID_SEGNALAZIONE_FO) from SISCOM_MI.EVENTI_SEGNALAZIONI_FO  where id_segnalazione_fo = SEGNALAZIONI_FORNITORI.ID and cod_evento='F272'  AND MOTIVAZIONE = to_char(to_date(MANUTENZIONI.DATA_PGI, 'yyyyMMdd'),  'dd/MM/yyyy') || ' - ' || to_char(to_date(MANUTENZIONI.DATA_TDL, 'yyyyMMdd'), 'dd/MM/yyyy') )  ) > 0 then 'VERDE' ELSE 'ROSSO' end )     WHEN  MANUTENZIONI.DATA_PGI <= MANUTENZIONI.DATA_INIZIO_INTERVENTO  THEN  'VERDE'   ELSE 'VUOTO' end) AS DATAPGI_1, " _
               & "(case when (nvl(TO_DATE (SUBSTR(MANUTENZIONI.DATA_TDL,1,8), 'yyyymmdd'),'') = '' or nvl(MANUTENZIONI.DATA_FINE_INTERVENTO,'') = '') then 'VUOTO' when MANUTENZIONI.DATA_TDL > MANUTENZIONI.DATA_FINE_INTERVENTO then ( case when (   (select count(ID_SEGNALAZIONE_FO) from SISCOM_MI.EVENTI_SEGNALAZIONI_FO  where id_segnalazione_fo = SEGNALAZIONI_FORNITORI.ID and cod_evento='F272' AND MOTIVAZIONE = to_char(to_date(MANUTENZIONI.DATA_PGI, 'yyyyMMdd'),  'dd/MM/yyyy') || ' - ' || to_char(to_date(MANUTENZIONI.DATA_TDL, 'yyyyMMdd'), 'dd/MM/yyyy') )  ) > 0 then 'VERDE' ELSE 'ROSSO' end )    WHEN  MANUTENZIONI.DATA_TDL <= MANUTENZIONI.DATA_FINE_INTERVENTO  THEN  'VERDE'   ELSE 'VUOTO' end) AS DATATDL_1, " _
               & "(select case when count(nvl(ID_SEGNALAZIONE_FO,0))>0 then 'SI' else 'NO' end from SISCOM_MI.EVENTI_SEGNALAZIONI_FO where id_segnalazione_fo = SEGNALAZIONI_FORNITORI.ID and cod_evento='F272' AND MOTIVAZIONE = to_char(to_date(MANUTENZIONI.DATA_PGI, 'yyyyMMdd'),  'dd/MM/yyyy') || ' - ' || to_char(to_date(MANUTENZIONI.DATA_TDL, 'yyyyMMdd'), 'dd/MM/yyyy') ) as DATE_ACCETTATE, " _
               & "TO_DATE (SUBSTR(SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO,1,8), 'yyyymmdd') AS DATA_FINE_DITTA, " _
               & "(CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI_FO_IRR WHERE VISIBILE=1 AND ID_SEGNALAZIONE=SEGNALAZIONI_FORNITORI.ID)>0 THEN 'SI' ELSE 'NO' END) AS IRREGOLARITA," _
               & " NVL(   (SELECT WM_CONCAT(DISTINCT SEGNALAZIONI_FO_ALL_TIPI.DESCRIZIONE)  FROM SISCOM_MI.SEGNALAZIONI_FO_ALLEGATI,SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI, SISCOM_MI.SEGNALAZIONI_FORNITORI  F WHERE SEGNALAZIONI_FO_ALL_TIPI.ID(+)=SEGNALAZIONI_FO_ALLEGATI.ID_TIPO AND SEGNALAZIONI_FO_ALLEGATI.ID_SEGNALAZIONE=F.ID  and F.ID =SEGNALAZIONI_FORNITORI.ID  GROUP BY SEGNALAZIONI_FORNITORI.ID ) , 'NO') AS ALLEGATI, " _
               & "SEGNALAZIONI_FORNITORI.ID AS IDENTIFICATIVO "
            sStrSql1 = sStrSql

            If OPS.Value <> "1" Then
                sStrSql = sStrSql & ", '<a href=''javascript:void(0);'' onclick=""window.open(''DettaglioOrdine.aspx?T=X&D='||MANUTENZIONI.PROGR||'_'||MANUTENZIONI.ANNO||''',''Intervento_'||MANUTENZIONI.ID||''','''');"">'||MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO||'</a>' AS ODL "
            Else
                sStrSql = sStrSql & ", '<a href=''javascript:void(0);'' onclick=""window.open(''Intervento.aspx?D='|| SEGNALAZIONI_FORNITORI.ID || ''',''Intervento_'|| MANUTENZIONI.ID ||''','''');"">'||MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO||'</a>' AS ODL "
                '                sStrSql = sStrSql & ", MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO AS ODL "
                'ApriModuloStandard('Intervento.aspx?D=' + document.getElementById('idSegnalazione').value, 'Intervento_' + document.getElementById('idSegnalazione').value);

            End If

            sStrSql = sStrSql _
                   & "FROM " _
                   & "SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI,SISCOM_MI.SEGNALAZIONI_FORNITORI,SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO,  SISCOM_MI.SEGNALAZIONI, SISCOM_MI.PERICOLO_SEGNALAZIONI " _
               & "WHERE " _
               & s _
               & " " & SS _
                   & " SEGNALAZIONI.ID(+) = SEGNALAZIONI_FORNITORI.ID_SEGNALAZIONE " _
                   & filtro _
                   & " AND PERICOLO_SEGNALAZIONI.ID (+) = SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE And " _
                   & " TAB_STATI_SEGNALAZIONI_FO.ID(+)=SEGNALAZIONI_FORNITORI.ID_STATO And FORNITORI.ID = appalti.id_fornitore And SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE(+)=MANUTENZIONI.ID And manutenzioni.id_appalto=appalti.id And MANUTENZIONI.STATO<>6 And MANUTENZIONI.ID_PAGAMENTO Is NULL And MANUTENZIONI.STATO<>2 And MANUTENZIONI.STATO<>5 And MANUTENZIONI.STATO<>0 " _
 			   & stringaFiltroAppalti _
                   & "ORDER BY FORNITORI.RAGIONE_SOCIALE, DATA_INIZIO_INTERVENTO DESC"

            sStrSql1 = sStrSql1 _
                   & "FROM " _
                   & "SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI,SISCOM_MI.SEGNALAZIONI_FORNITORI,SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO,  SISCOM_MI.SEGNALAZIONI, SISCOM_MI.PERICOLO_SEGNALAZIONI " _
                   & "WHERE " _
                   & s _
                   & " " & SS _
                   & " SEGNALAZIONI.ID(+) = SEGNALAZIONI_FORNITORI.ID_SEGNALAZIONE " _
                   & filtro _
                   & " And PERICOLO_SEGNALAZIONI.ID (+) = SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE And " _
                   & " TAB_STATI_SEGNALAZIONI_FO.ID(+)=SEGNALAZIONI_FORNITORI.ID_STATO And FORNITORI.ID = appalti.id_fornitore And SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE(+)=MANUTENZIONI.ID And manutenzioni.id_appalto=appalti.id And MANUTENZIONI.STATO<>6 And MANUTENZIONI.ID_PAGAMENTO Is NULL And MANUTENZIONI.STATO<>2 And MANUTENZIONI.STATO<>5 And MANUTENZIONI.STATO<>0 " _
                   & stringaFiltroAppalti _
                   & "ORDER BY FORNITORI.RAGIONE_SOCIALE, DATA_INIZIO_INTERVENTO DESC"

            Session.Add("g1", sStrSql1)
            dtAgenda = par.getDataTableGrid(sStrSql)

            RadAgenda.DataSource = dtAgenda
            RadAgenda.DataBind()
            RadAgenda.SelectedView = SchedulerViewType.MonthView 'SchedulerViewType.TimelineView
            RadAgenda.GroupingDirection = DirectCast([Enum].Parse(GetType(GroupingDirection), "Vertical"), GroupingDirection)

            RadAgenda.ResourceTypes.Clear()
            Dim restype1 As New ResourceType("RAGIONE_SOCIALE")
            Session.Add("g2", "select distinct FORNITORI.RAGIONE_SOCIALE from SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.SEGNALAZIONI_FORNITORI, SISCOM_MI.SEGNALAZIONI, SISCOM_MI.PERICOLO_SEGNALAZIONI WHERE " & s & " " & SS & " SEGNALAZIONI.ID(+) = SEGNALAZIONI_FORNITORI.ID_SEGNALAZIONE And PERICOLO_SEGNALAZIONI.ID (+) = SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE And  manutenzioni.id_appalto=appalti.id And FORNITORI.ID= APPALTI.ID_FORNITORE And SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE(+)=MANUTENZIONI.ID And MANUTENZIONI.STATO<>6 And MANUTENZIONI.ID_PAGAMENTO Is NULL And MANUTENZIONI.STATO<>2 And MANUTENZIONI.STATO<>0 And MANUTENZIONI.STATO<>5 order by fornitori.ragione_sociale")
            restype1.DataSource = par.getDataTableGrid("select distinct FORNITORI.RAGIONE_SOCIALE from SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.SEGNALAZIONI_FORNITORI, SISCOM_MI.SEGNALAZIONI, SISCOM_MI.PERICOLO_SEGNALAZIONI WHERE " & s & " " & SS & " SEGNALAZIONI.ID(+) = SEGNALAZIONI_FORNITORI.ID_SEGNALAZIONE And PERICOLO_SEGNALAZIONI.ID (+) = SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE And manutenzioni.id_appalto=appalti.id And FORNITORI.ID= APPALTI.ID_FORNITORE And SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE(+)=MANUTENZIONI.ID And MANUTENZIONI.STATO<>6 And MANUTENZIONI.ID_PAGAMENTO Is NULL  And MANUTENZIONI.STATO<>2 And MANUTENZIONI.STATO<>0 And MANUTENZIONI.STATO<>5 order by fornitori.ragione_sociale")
            restype1.KeyField = "RAGIONE_SOCIALE"
            restype1.TextField = "RAGIONE_SOCIALE"
            restype1.ForeignKeyField = "RAGIONE_SOCIALE"
            RadAgenda.ResourceTypes.Add(restype1)
            RadAgenda.GroupBy = "RAGIONE_SOCIALE"

            Dim itemX As RadPanelItem = RadPanelBar1.Items(2)
            Dim cmbGiorni As RadDropDownList = TryCast(itemX.Items(0).FindControl("cmbGiorni"), RadDropDownList)

            RadAgenda.TimelineView.NumberOfSlots = cmbGiorni.SelectedItem.Text

            Session.Add("g3", cmbGiorni.SelectedItem.Text)
            If sPrimoIngresso = "1" Then
                Session.Add("g4", RadAgenda.VisibleRangeStart.ToString)
                Select Case cmbGiorni.SelectedItem.Value
                    Case 7
                        RadAgenda.SelectedDate = DateAdd(DateInterval.Day, -1, Session.Item("G4"))
                    Case 14
                        RadAgenda.SelectedDate = DateAdd(DateInterval.Day, -3, Session.Item("G4"))
                    Case 21
                        RadAgenda.SelectedDate = DateAdd(DateInterval.Day, -5, Session.Item("G4"))
                End Select
            End If

            Dim label2 As Label = TryCast(itemX.Items(0).FindControl("Label2"), Label)
            Dim Label3 As Label = TryCast(itemX.Items(0).FindControl("Label3"), Label)
            If RadAgenda.SelectedView = SchedulerViewType.MonthView Then
                cmbGiorni.Visible = False
                label2.Visible = False
                Label3.Visible = False
            Else
                cmbGiorni.Visible = True
                label2.Visible = True
                Label3.Visible = True
            End If

            'Select Case cmbGiorni.SelectedItem.Value
            '    Case 7
            '        RadAgenda.SelectedDate = DateAdd(DateInterval.Day, -1, RadAgenda.VisibleRangeStart)
            '    Case 14
            '        RadAgenda.SelectedDate = DateAdd(DateInterval.Day, -3, RadAgenda.VisibleRangeStart)
            '    Case 21
            '        RadAgenda.SelectedDate = DateAdd(DateInterval.Day, -5, RadAgenda.VisibleRangeStart)
            'End Select
            RadAgenda.DataBind()
            RadAgenda.Visible = True
            If dtAgenda.Rows.Count = 0 Then
                'VisualizzaAlert("Nessun elemento da visualizzare", 2)
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Segnalazioni - RicavaOrdini - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnAvviaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnAvviaRicerca.Click
        idSelRisultatiODL.Value = ""
        idSelRisultatiREP.Value = ""
        RicavaOrdini()
        sStrSqlRisultati = sStrSql
        Dim item As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        HFFiltroFO.Value = "Tutti"
        Griglia.Rebind()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
    End Sub

    Private Sub AzzeraFiltri()
        Dim item As RadPanelItem = RadPanelBar1.Items(0)

        Dim CheckList As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)

        For i = 0 To CheckList.Items.Count - 1
            CheckList.Items(i).Selected = True
            If i = 0 Then
                CheckList.Items(i).Value = "1"
            ElseIf i = 1 Then
                CheckList.Items(i).Value = "2"
            ElseIf i = 2 Then
                ' CheckList.Items(i).Value = "6"
                CheckList.Items(i).Value = "8"
            ElseIf i = 3 Then
                CheckList.Items(i).Value = "9"
            ElseIf i = 4 Then
                CheckList.Items(i).Value = "3"
            ElseIf i = 5 Then
                CheckList.Items(i).Value = "10"
            End If

            'Select Case par.IfNull(myReader("STATO_S"), "")
            '    Case "1"
            '        ROW.Item("STATO") = " - ORDINE DA VERIFICARE "
            '    Case "2"
            '        ROW.Item("STATO") = " - ORDINE IN CARICO "
            '    Case "6"
            '        ROW.Item("STATO") = " - ORDINE ANNULLATO "
            '    Case "8"
            '        ROW.Item("STATO") = " - ORDINE EVASO "
            '    Case "9"
            '        ROW.Item("STATO") = " - RICHIESTA CONSUNTIVAZIONE "
            '    Case "3"
            '        ROW.Item("STATO") = " - ORDINE DA CONSUNTIVARE "
            '    Case "10"
            '        ROW.Item("STATO") = " - IN CARICO MA RITARDO "
            '    Case Else
            '        ROW.Item("STATO") = " - "
            'End Select

        Next

        If iIndiceFornitore = "0" Then
            Dim cmbList As RadComboBox = TryCast(item.Items(0).FindControl("cmbFornitori"), RadComboBox)
            cmbList.Text = "---"
        End If

        Dim cmbList1 As RadComboBox = TryCast(item.Items(0).FindControl("cmbDL"), RadComboBox)
        cmbList1.Text = "---"

        cmbList1 = TryCast(item.Items(0).FindControl("cmbBM"), RadComboBox)
        cmbList1.Text = "---"

        Dim ControlloTesto As RadTextBox = TryCast(item.Items(0).FindControl("txtNumIntDa"), RadTextBox)
        ControlloTesto.Text = ""

        ControlloTesto = TryCast(item.Items(0).FindControl("txtNumRepertorio"), RadTextBox)
        ControlloTesto.Text = ""

        Dim Pulsante As CheckBox = TryCast(item.Items(0).FindControl("chANNULLATO"), CheckBox)
        Pulsante.Checked = False

        idSelRisultatiODL.Value = ""
        idSelRisultatiREP.Value = ""

        '////////////////////////////
        '// 143/2019
        Dim ControlloData As RadDatePicker = TryCast(item.Items(0).FindControl("txtRicDA"), RadDatePicker)
        ControlloData.Clear()

        ControlloData = TryCast(item.Items(0).FindControl("txtFineLavoriA"), RadDatePicker)
        ControlloData.Clear()
        ControlloData = TryCast(item.Items(0).FindControl("txtFineLavoriDA"), RadDatePicker)
        ControlloData.Clear()
        ControlloData = TryCast(item.Items(0).FindControl("txtRicA"), RadDatePicker)
        ControlloData.Clear()
        ControlloData = TryCast(item.Items(0).FindControl("txtPGIDA"), RadDatePicker)
        ControlloData.Clear()
        ControlloData = TryCast(item.Items(0).FindControl("txtPGIA"), RadDatePicker)
        ControlloData.Clear()
        ControlloData = TryCast(item.Items(0).FindControl("txtTDLDA"), RadDatePicker)
        ControlloData.Clear()
        ControlloData = TryCast(item.Items(0).FindControl("txtTDLA"), RadDatePicker)
        ControlloData.Clear()

        Dim item2 As RadPanelItem = RadPanelBar1.Items(0)
        Dim cmbList2 As RadDropDownList = TryCast(item2.Items(0).FindControl("cmbGravita"), RadDropDownList)
        cmbList2.SelectedText = "---"

        ControlloTesto = TryCast(item.Items(0).FindControl("txtScostamentoDPIL"), RadTextBox)
        ControlloTesto.Text = ""

        ControlloTesto = TryCast(item.Items(0).FindControl("txtScostamentoDPFL"), RadTextBox)
        ControlloTesto.Text = ""
        '////////////////////////////
    End Sub
    Protected Sub btnAzzeraFiltri_Click(sender As Object, e As System.EventArgs) Handles btnAzzeraFiltri.Click
        AzzeraFiltri()
        RicavaOrdini()
        sStrSqlRisultati = sStrSql


        Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item1.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        HFFiltroFO.Value = "Tutti"
        Griglia.Rebind()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
    End Sub


    Protected Sub RadAgenda_PreRender(sender As Object, e As System.EventArgs) 'Handles RadAgenda.PreRender
        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)

        Dim item As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)

        Griglia.MasterTableView.GetColumn("DATAPGI_1").Display = False
        Griglia.MasterTableView.GetColumn("DATATDL_1").Display = False
        'Griglia.MasterTableView.GetColumn("DATE_ACCETTATE").Display = False

        Dim itemX As RadPanelItem = RadPanelBar1.Items(2)
        Dim lblNumEmesso As Label = TryCast(itemX.Items(0).FindControl("lblNumEmesso"), Label)
        Dim lblNumIntegrato As Label = TryCast(itemX.Items(0).FindControl("lblNumIntegrato"), Label)
        Dim lblNumAnnullato As Label = TryCast(itemX.Items(0).FindControl("lblNumAnnullato"), Label)
        Dim lblFineLavori As Label = TryCast(itemX.Items(0).FindControl("lblFineLavori"), Label)
        Dim lblRichiestaC As Label = TryCast(itemX.Items(0).FindControl("lblRichiestaC"), Label)
        Dim lblDaContabilizzare As Label = TryCast(itemX.Items(0).FindControl("lblDaContabilizzare"), Label)

        If iTotNumBozza + iTotNumAnnullato + iTotNumDaVerificare + iTotNumInCarico + iTotEvaso + iTotRC + iTotDC <> 0 Then
            If iDivisore = 0 Then
                iDivisore = (iTotNumAnnullato + iTotNumDaVerificare + iTotNumInCarico + iTotEvaso) / (iTotNumAnnullatoT + iTotNumDaVerificareT + iTotNumInCaricoT + iTotEvasoT)
            End If
            lblTitolo.Text = "Calendario Interventi e Lavori" ' (" & (iTotNumBozza + iTotNumAnnullato + iTotNumConsuntivato + iTotNumEmesso + iTotNumEP + iTotNumIntegrato + iTotEvaso) / iDivisore & " di " & (iTotNumBozzaT + iTotNumAnnullatoT + iTotNumConsuntivatoT + iTotNumEmessoT + iTotNumEPT + iTotNumIntegratoT + iTotEvasoT) & " nella lista)"

            lblNumEmesso.Text = "DA VERIFICARE (" & iTotNumDaVerificare / iDivisore & " di " & iTotNumDaVerificareT & ")"
            lblNumIntegrato.Text = "IN CARICO (" & iTotNumInCarico / iDivisore & " di " & iTotNumInCaricoT & ")"
            lblNumAnnullato.Text = "IN CARICO MA RITARDO (" & iTotNumAnnullato / iDivisore & " di " & iTotNumAnnullatoT & ")"
            lblFineLavori.Text = "EVASO (" & iTotEvaso / iDivisore & " di " & iTotEvasoT & ")"
            lblRichiestaC.Text = "RICHIESTA CONSUNTIVAZIONE (" & iTotRC / iDivisore & " di " & iTotRCT & ")"
            lblDaContabilizzare.Text = "DA CONSUNTIVARE (" & iTotDC / iDivisore & " di " & iTotDCT & ")"
        Else
            lblNumEmesso.Text = "DA VERIFICARE (" & 0 / iDivisore & " di " & iTotNumDaVerificareT & ")"
            lblNumIntegrato.Text = "IN CARICO (" & 0 / iDivisore & " di " & iTotNumInCaricoT & ")"
            lblNumAnnullato.Text = "IN CARICO MA RITADO (" & 0 / iDivisore & " di " & iTotNumAnnullatoT & ")"
            lblFineLavori.Text = "EVASO (" & 0 / iDivisore & " di " & iTotEvasoT & ")"
            lblRichiestaC.Text = "RICHIESTA CONSUNTIVAZIONE (" & 0 / iDivisore & " di " & iTotRCT & ")"
            lblDaContabilizzare.Text = "DA CONSUNTIVARE (" & 0 / iDivisore & " di " & iTotDCT & ")"
        End If

    End Sub

    '    Protected Sub cmbGiorni_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles cmbGiorni.SelectedIndexChanged
    Protected Sub cmbGiorni_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs)
        Dim itemX As RadPanelItem = RadPanelBar1.Items(2)
        Dim cmbGiorni As RadDropDownList = TryCast(itemX.Items(0).FindControl("cmbGiorni"), RadDropDownList)
        Session.Item("g3") = cmbGiorni.SelectedItem.Text
        RadAgenda.TimelineView.NumberOfSlots = cmbGiorni.SelectedItem.Text

        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)

        SettaCookie()

        Select Case cmbGiorni.SelectedItem.Value
            Case 7
                RadAgenda.SelectedDate = DateAdd(DateInterval.Day, -1, Now)
            Case 14
                RadAgenda.SelectedDate = DateAdd(DateInterval.Day, -3, Now)
            Case 21
                RadAgenda.SelectedDate = DateAdd(DateInterval.Day, -5, Now)
        End Select
    End Sub

    Private Sub SettaCookie()
        Dim cookie As HttpCookie = New HttpCookie(Session.Item("ID_OPERATORE") & "_FoDaView")
        Dim itemX As RadPanelItem = RadPanelBar1.Items(2)
        Dim cmbGiorni As RadDropDownList = TryCast(itemX.Items(0).FindControl("cmbGiorni"), RadDropDownList)
        cookie.Value = cmbGiorni.SelectedText
        cookie.Expires = DateTime.Now.AddDays(10)
        Response.SetCookie(cookie)
    End Sub

    Protected Sub DataBoundTab(sender As Object, e As Telerik.Web.UI.GridItemEventArgs)
        Try
            '///////////////////
            '// 1433/2019
            idSegnalazione.Value = "0"

            If TypeOf e.Item Is GridPagerItem Then
                Dim pagerItem As GridPagerItem = TryCast(e.Item, GridPagerItem)
                NumeroElementi = pagerItem.Paging.DataSourceCount
            End If
            If isExporting.Value = "1" Then
                If e.Item.ItemIndex > 0 Then
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                        Dim context As RadProgressContext = RadProgressContext.Current
                        If context.SecondaryTotal <> NumeroElementi Then
                            context.SecondaryTotal = NumeroElementi
                        End If
                        context.SecondaryValue = kk
                        context.SecondaryPercent = Int((kk * 100) / NumeroElementi)
                        kk = kk + 1

                        context.CurrentOperationText = "Export excel in corso"
                    End If

                End If
            End If
            '////////////////////

            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                ' ManutenzioniProgr   MANUTENZIONI.PROGR||'_'||MANUTENZIONI.ANNO
                'e.Item.Attributes.Add("onclick", "document.getElementById('idSelRisultati').value='" & dataItem("DATA_INIZIO_RICERCA").Text & "';document.getElementById('idSelRisultatiODL').value='" & dataItem("NUM_ODL").Text & "';document.getElementById('idSelRisultatiREP').value='" & dataItem("NUM_REPERTORIO").Text & "';document.getElementById('idSegnalazione').value='" & dataItem("IDENTIFICATIVO").Text & "';")
                e.Item.Attributes.Add("onclick", "document.getElementById('idSelRisultati').value='" & dataItem("DATA_INIZIO_RICERCA").Text & "';document.getElementById('idSelRisultatiODL').value='" & dataItem("NUM_ODL").Text & "';document.getElementById('idSelRisultatiREP').value='" & dataItem("NUM_REPERTORIO").Text & "';document.getElementById('idSegnalazione').value='" & dataItem("IDENTIFICATIVO").Text & "';document.getElementById('ManutenzioniProgr').value='" & dataItem("ODL1").Text.Replace("/", "_") & "';")

            End If
            If TypeOf e.Item Is GridFilteringItem Then
                If e.Item.OwnerTableView.Name = "ElencoTab" Then
                    Dim item As RadPanelItem = RadPanelBar1.Items(1)
                    Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
                    Dim filtroComboFornitore As String = ""
                    Dim filtroComboBM As String = ""
                    If RadButtonBuildingManager.Checked = True Then
                        filtroComboFornitore = " and siscom_mi.getbuildingmanager(manutenzioni.id,1) like '%" & Session.Item("ID_OPERATORE") & "%' "
                        filtroComboBM = "AND ID = " & Session.Item("ID_OPERATORE")
                    End If
                    If RadButtonTecnicoAmministrativo.Checked Then
                        filtroComboFornitore &= " and " _
                       & " ( " _
                       & " case when manutenzioni.id_complesso is not null then (select distinct id_filiale from SISCOM_MI.complessi_immobiliari where id = manutenzioni.id_complesso) " _
                       & " when manutenzioni.id_edificio is not null then (select id_filiale from siscom_mi.complessi_immobiliari where id in (select distinct id_complesso from SISCOM_MI.edifici where id = manutenzioni.id_edificio)) " _
                       & " when MANUTENZIONI.ID_UNITA_IMMOBILIARI is not null then (select distinct id_filiale from SISCOM_MI.complessi_immobiliari where id in (select id_complesso from SISCOM_MI.edifici where edifici.id = (select id_edificio from SISCOM_MI.unita_immobiliari where id = manutenzioni.ID_UNITA_IMMOBILIARI))) " _
                       & " end " _
                       & " ) = " & Session.Item("ID_STRUTTURA")
                        filtroComboBM = " AND OPERATORI.ID IN (SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA = '30000101' AND ID_BM IN (SELECT ID FROM SISCOM_MI.BUILDING_MANAGER WHERE ID_STRUTTURA = " & Session.Item("ID_STRUTTURA") & "))"
                    End If

                    If IndiceFornitore.Value <> "0" Then
                        par.caricaComboTelerik("SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI " _
                                             & " WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE MODULO_FORNITORI = 1 AND ID IN " _
                                             & " (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID IN " _
                                             & " (SELECT ID_MANUTENZIONE FROM SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE " _
                                             & " SEGNALAZIONI_FORNITORI.ID_STATO IN (1,2,9,10)) AND " _
                                             & "  MANUTENZIONI.ID_PAGAMENTO IS NULL AND MANUTENZIONI.STATO NOT IN (0, 2, 5) " _
                                             & filtroComboFornitore _
                                             & ")) ORDER BY 1 ", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroFO"), RadComboBox), "RAGIONE_SOCIALE", "RAGIONE_SOCIALE", True, "Tutti", "Tutti")
                        par.caricaComboTelerik("SELECT COGNOME || ' ' || NOME AS BM FROM SEPA.OPERATORI " _
                                               & " WHERE SEPA.OPERATORI.ID IN (SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI) " & filtroComboBM & " ORDER BY 1" _
                            , TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroBM"), RadComboBox), "BM", "BM", True, "Tutti", "Tutti")
                    Else
                        'par.caricaComboTelerik("select FORNITORI.ragione_sociale from siscom_mi.fornitori where FORNITORI.ID in (select distinct id_fornitore from siscom_mi.APPALTI WHERE MODULO_FORNITORI=1) ORDER BY FORnITORI.RAGIONE_SOCIALE ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroFO"), RadComboBox), "RAGIONE_SOCIALE", "RAGIONE_SOCIALE", True, "Tutti", "Tutti")
                        par.caricaComboTelerik("select FORNITORI.ragione_sociale from siscom_mi.fornitori  ORDER BY FORnITORI.RAGIONE_SOCIALE ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroFO"), RadComboBox), "RAGIONE_SOCIALE", "RAGIONE_SOCIALE", True, "Tutti", "Tutti")
                        par.caricaComboTelerik("select distinct operatori.id,operatori.cognome||' '||operatori.nome as BM,cognome,nome from siscom_mi.BUILDING_MANAGER,siscom_mi.BUILDING_MANAGER_OPERATORI,operatori where BUILDING_MANAGER_OPERATORI.id_bm=BUILDING_MANAGER.id and operatori.id=BUILDING_MANAGER_OPERATORI.id_operatore order by cognome asc,nome asc", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroBM"), RadComboBox), "BM", "BM", True, "Tutti", "Tutti")
                    End If
                    par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO ORDER BY ID ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStato"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
                    If Not String.IsNullOrEmpty(Trim(HFFiltroStato.Value.ToString)) Then
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStato"), RadComboBox).SelectedValue = HFFiltroStato.Value.ToString
                    End If
                    If Not String.IsNullOrEmpty(Trim(HFFiltroFO.Value.ToString)) Then
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroFO"), RadComboBox).SelectedValue = HFFiltroFO.Value.ToString
                    End If
                    If Not String.IsNullOrEmpty(Trim(HFFiltroBM.Value.ToString)) Then
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroBM"), RadComboBox).SelectedValue = HFFiltroBM.Value.ToString
                    End If

                    par.caricaComboTelerik("SELECT 'SI' AS DESCRIZIONE FROM DUAL UNION SELECT 'NO' AS DESCRIZIONE FROM DUAL", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroDAte"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
                    If Not String.IsNullOrEmpty(Trim(HFFiltroDate.Value.ToString)) Then
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroDAte"), RadComboBox).SelectedValue = HFFiltroDate.Value.ToString
                    End If
                    par.caricaComboTelerik("SELECT 'SI' AS DESCRIZIONE FROM DUAL UNION SELECT 'NO' AS DESCRIZIONE FROM DUAL", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroDAte1"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
                    If Not String.IsNullOrEmpty(Trim(HFFiltroDate1.Value.ToString)) Then
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroDAte1"), RadComboBox).SelectedValue = HFFiltroDate1.Value.ToString
                End If
                    If Session.Item("ID_STRUTTURA") = "105" Then

                    'par.caricaComboTelerik("select ID,  case when upper(substr(NOME, 1, 18)) = 'SEDE TERRITORIALE ' and LENGTH(NOME)=19   then substr(TAB_FILIALI.NOME, 19, 1) else TAB_FILIALI.NOME end as nome from SISCOM_MI.tab_filiali", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroST"), RadComboBox), "NOME", "NOME", True, "Tutti", "Tutti")
                    par.caricaComboTelerik("SELECT DISTINCT TAB_FILIALI.ID,  " _
                                           & " CASE WHEN UPPER(SUBSTR(NOME, 1, 18)) = 'SEDE TERRITORIALE ' AND LENGTH(NOME)=19 THEN " _
                                           & "SUBSTR(TAB_FILIALI.NOME, 19, 1) ELSE TAB_FILIALI.NOME END AS NOME " _
                                           & " FROM SISCOM_MI.EDIFICI INNER JOIN SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                           & " ON EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID INNER JOIN SISCOM_MI.TAB_FILIALI " _
                                               & " ON COMPLESSI_IMMOBILIARI.ID_FILIALE = TAB_FILIALI.ID ORDER BY 2", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroST"), RadComboBox), "NOME", "NOME", True, "Tutti", "Tutti")
                    Else
                        If String.IsNullOrEmpty(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                            par.caricaComboTelerik("SELECT DISTINCT TAB_FILIALI.ID,  " _
                                               & " CASE WHEN UPPER(SUBSTR(NOME, 1, 18)) = 'SEDE TERRITORIALE ' AND LENGTH(NOME)=19 THEN " _
                                               & "SUBSTR(TAB_FILIALI.NOME, 19, 1) ELSE TAB_FILIALI.NOME END AS NOME " _
                                               & " FROM SISCOM_MI.EDIFICI INNER JOIN SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                               & " ON EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID INNER JOIN SISCOM_MI.TAB_FILIALI " _
                                               & " ON COMPLESSI_IMMOBILIARI.ID_FILIALE = TAB_FILIALI.ID ORDER BY 2", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroST"), RadComboBox), "NOME", "NOME", True, "Tutti", "Tutti")
                        Else
                        par.caricaComboTelerik("SELECT DISTINCT TAB_FILIALI.ID,  " _
                                               & " CASE WHEN UPPER(SUBSTR(NOME, 1, 18)) = 'SEDE TERRITORIALE ' AND LENGTH(NOME)=19 THEN " _
                                               & "SUBSTR(TAB_FILIALI.NOME, 19, 1) ELSE TAB_FILIALI.NOME END AS NOME " _
                                               & " FROM SISCOM_MI.EDIFICI INNER JOIN SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                               & " ON EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID INNER JOIN SISCOM_MI.TAB_FILIALI " _
                                           & " ON COMPLESSI_IMMOBILIARI.ID_FILIALE = TAB_FILIALI.ID  WHERE TAB_FILIALI.ID =  " & Session.Item("ID_STRUTTURA") & " ORDER BY 2", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroST"), RadComboBox), "NOME", "NOME", True, "Tutti", "Tutti")
                        End If
                        'par.caricaComboTelerik("select ID,  case when upper(substr(NOME, 1, 18)) = 'SEDE TERRITORIALE ' and LENGTH(NOME)=19   then substr(TAB_FILIALI.NOME, 19, 1) else TAB_FILIALI.NOME end as nome from SISCOM_MI.tab_filiali", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroST"), RadComboBox), "NOME", "NOME", True, "Tutti", "Tutti")
                    End If

                    If Not String.IsNullOrEmpty(Trim(HFFiltroST.Value.ToString)) Then
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroST"), RadComboBox).SelectedValue = HFFiltroST.Value.ToString
                    End If

                    par.caricaComboTelerik("select SEGNALAZIONI_FO_ALL_TIPI.DESCRIZIONE from SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroAL"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
                    If Not String.IsNullOrEmpty(Trim(HFFiltroAL.Value.ToString)) Then
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroAL"), RadComboBox).SelectedValue = HFFiltroAL.Value.ToString
                    End If

                    par.caricaComboTelerik("SELECT progr, anno, NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE MODULO_FORNITORI = 1 AND ID IN " _
                                             & " (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID IN " _
                                             & " (SELECT ID_MANUTENZIONE FROM SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE " _
                                             & " SEGNALAZIONI_FORNITORI.ID_STATO IN (1,2,9,10)) AND " _
                                             & "  MANUTENZIONI.ID_PAGAMENTO IS NULL AND MANUTENZIONI.STATO NOT IN (0, 2, 5) " _
                                             & filtroComboFornitore _
                                             & ") ORDER BY 2 desc,1 DESC ", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroRE"), RadComboBox), "NUM_REPERTORIO", "NUM_REPERTORIO", True, "Tutti", "Tutti")
                    If Not String.IsNullOrEmpty(Trim(HFFiltroRE.Value.ToString)) Then
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroRE"), RadComboBox).SelectedValue = HFFiltroRE.Value.ToString
                    End If

                    '//////////////////////////////////////////

                    'If iIndiceFornitore <> "0" Then
                    'cmbList = TryCast(item.Items(0).FindControl("cmbBM"), RadComboBox)
                    'par.caricaComboTelerik("select distinct operatori.id,operatori.cognome||' '||operatori.nome as BM,cognome,nome from siscom_mi.BUILDING_MANAGER,siscom_mi.BUILDING_MANAGER_OPERATORI,operatori where BUILDING_MANAGER_OPERATORI.id_bm=BUILDING_MANAGER.id and operatori.id=BUILDING_MANAGER_OPERATORI.id_operatore and BUILDING_MANAGER_OPERATORI.id_bm in (select distinct(edifici.id_bm) from siscom_mi.appalti_lotti_patrimonio,siscom_mi.appalti,siscom_mi.edifici where appalti.id=appalti_lotti_patrimonio.id_appalto and edifici.id=appalti_lotti_patrimonio.id_edificio and appalti.id in " & iIndiceFornitore & ") order by cognome asc,nome asc", cmbList, "ID", "BM", True, "-1", "---")
                    'Else
                    'cmbList = TryCast(item.Items(0).FindControl("cmbBM"), RadComboBox)
                    'par.caricaComboTelerik("select distinct operatori.id,operatori.cognome||' '||operatori.nome as BM,cognome,nome from siscom_mi.BUILDING_MANAGER,siscom_mi.BUILDING_MANAGER_OPERATORI,operatori where BUILDING_MANAGER_OPERATORI.id_bm=BUILDING_MANAGER.id and operatori.id=BUILDING_MANAGER_OPERATORI.id_operatore order by cognome asc,nome asc", cmbList, "ID", "BM", True, "-1", "---")
                    'End If


                    Dim ApertaNow As Boolean = False
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        Me.connData = New CM.datiConnessione(par, False, False)
                        connData.apri(False)
                        ApertaNow = True
                    End If
                    par.cmd.CommandText = "SELECT 1 as id, 'ROSSO' as descrizione FROM DUAL union SELECT 0 as id, 'VERDE' as descrizione FROM DUAL union SELECT 3 as id, 'VUOTO' as descrizione FROM DUAL ORDER BY 1 DESC"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                    Dim dt As New Data.DataTable
                    da.Fill(dt)
                    da.Dispose()

                    Dim item1 As RadComboBoxItem
                    item1 = New RadComboBoxItem()
                    item1.Text = "Tutti"
                    item1.Value = "Tutti"
                    TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroPericolo"), RadComboBox).Items.Add(item1)
                    For Each row As Data.DataRow In dt.Rows
                        item1 = New RadComboBoxItem()
                        item1.Text = par.IfNull(row.Item("DESCRIZIONE"), "")
                        item1.Value = par.IfNull(row.Item("DESCRIZIONE"), "")
                        Select Case par.IfNull(row.Item("ID"), "").ToString
                            Case "0"
                                item1.ImageUrl = "../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-green-128.png"
                                item1.ForeColor = Drawing.Color.Green
                            Case "1"
                                item1.ImageUrl = "../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-red-128.png"
                                item1.ForeColor = Drawing.Color.Red
                        End Select
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroPericolo"), RadComboBox).Items.Add(item1)
                    Next
                    If Not String.IsNullOrEmpty(Trim(HFFiltroPericolo.Value.ToString)) Then
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroPericolo"), RadComboBox).SelectedValue = HFFiltroPericolo.Value.ToString
                    End If

                    Dim item2 As RadComboBoxItem
                    item2 = New RadComboBoxItem()
                    item2.Text = "Tutti"
                    item2.Value = "Tutti"
                    TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroPericolo_1"), RadComboBox).Items.Add(item2)
                    For Each row As Data.DataRow In dt.Rows
                        item2 = New RadComboBoxItem()
                        item2.Text = par.IfNull(row.Item("DESCRIZIONE"), "")
                        item2.Value = par.IfNull(row.Item("DESCRIZIONE"), "")
                        Select Case par.IfNull(row.Item("ID"), "").ToString
                            Case "0"
                                item2.ImageUrl = "../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-green-128.png"
                                item2.ForeColor = Drawing.Color.Green
                            Case "1"
                                item2.ImageUrl = "../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-red-128.png"
                                item2.ForeColor = Drawing.Color.Red
                        End Select
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroPericolo_1"), RadComboBox).Items.Add(item2)
                    Next
                    If Not String.IsNullOrEmpty(Trim(HFFiltroPericolo_1.Value.ToString)) Then
                        TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroPericolo_1"), RadComboBox).SelectedValue = HFFiltroPericolo_1.Value.ToString
                    End If


                    '/////////////////////////////////////////
                End If
            End If

            '//////////////////////
            '1433/2019
            If TypeOf e.Item Is GridDataItem Then
                Dim dataBoundItem As GridDataItem = TryCast(e.Item, GridDataItem)

                If dataBoundItem("DATAPGI").Text <> "" And dataBoundItem("DATAPGI").Text <> "&nbsp;" Then
                    If CType(dataBoundItem("DATAPGI").Text, DateTime) > CType(dataBoundItem("DATA_INIZIO_INTERVENTO").Text, DateTime) Then
                        If CType(dataBoundItem("DATE_ACCETTATE").Text, String) <> "SI" Then
                            dataBoundItem("DATAPGI").ForeColor = Drawing.Color.Red
                            dataBoundItem("DATAPGI").Font.Bold = True
                        Else
                            dataBoundItem("DATAPGI").ForeColor = Drawing.Color.Green
                            dataBoundItem("DATAPGI").Font.Bold = True
                        End If
                    Else
                        dataBoundItem("DATAPGI").ForeColor = Drawing.Color.Green
                        dataBoundItem("DATAPGI").Font.Bold = True
                    End If
                End If

                If dataBoundItem("DATATDL").Text <> "" And dataBoundItem("DATATDL").Text <> "&nbsp;" Then
                    If CType(dataBoundItem("DATATDL").Text, DateTime) > CType(dataBoundItem("DATA_FINE_INTERVENTO").Text, DateTime) Then
                        If CType(dataBoundItem("DATE_ACCETTATE").Text, String) <> "SI" Then
                            dataBoundItem("DATATDL").ForeColor = Drawing.Color.Red
                            dataBoundItem("DATATDL").Font.Bold = True
                        Else
                            dataBoundItem("DATATDL").ForeColor = Drawing.Color.Green
                            dataBoundItem("DATATDL").Font.Bold = True
                        End If
                    Else
                        dataBoundItem("DATATDL").ForeColor = Drawing.Color.Green
                        dataBoundItem("DATATDL").Font.Bold = True
                    End If
                End If
            End If
            '//////////////////////

            '////////////////////////////////////
            '// 2063/2019 
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                'Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                'Dim isChecked As Object = Nothing
                'isChecked = CustomersChecked(dataItem("CheckBox1").Text)

                'Dim box As CheckBox = CType(dataItem.FindControl("CheckBox1"), CheckBox)
                'box.Checked = CType(isChecked, Boolean) = True

                'If Not (isChecked Is Nothing) Then
                'box.Checked = CType(isChecked, Boolean) = True
                'End If

                If TypeOf e.Item Is GridDataItem Then
                    Dim item As GridDataItem = TryCast(e.Item, GridDataItem) ' CType(ConversionHelpers.AsWorkaround(e.Item, GetType(GridDataItem)), GridDataItem)
                    Dim box As CheckBox = CType(item.FindControl("CheckBox1"), CheckBox)
                    Dim isChecked As Hashtable = Nothing

                    'If item.OwnerTableView.DataMember = "Customers" Then
                    isChecked = CustomersChecked(item("ID_MANUTENZIONE").Text)
                    'End If

                    If Not (isChecked Is Nothing) Then
                        If isChecked.Count > 0 Then
                            box.Checked = isChecked(item("ID_MANUTENZIONE").Text) = True
                        Else
                            box.Checked = False
                        End If
                    Else
                        box.Checked = False
            End If

                End If

            End If
            '///////////////////////////////////


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CaricaTab(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs)
        Try
            If sStrSqlRisultati <> "" Then
                Dim item As RadPanelItem = RadPanelBar1.Items(1)
                Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)

                Griglia.DataSource = par.getDataTableGrid(sStrSqlRisultati)
                'For Each Filter As GridFilteringItem In Griglia.Items.
                '    TryCast(TryCast(Filter, GridFilteringItem).FindControl("RadComboBoxFiltroFO"), RadComboBox).SelectedValue = HFFiltroFO.Value.ToString
                'Next
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - OrdiniGestore - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    'Protected Sub RadGridRisultati_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridRisultati.NeedDataSource
    '    Try
    '        If sStrSql <> "" Then
    '            RadGridRisultati.DataSource = par.getDataTableGrid(sStrSql)
    '            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "function(sender, args){openWindow(sender, args, 'MasterPage_CPContenuto_RadWindowRisultati');}", True)
    '        End If
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: Fornitori - OrdiniGestore - NeedDataSource - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", True)
    '    End Try
    'End Sub

    Public Sub RichiestaMassivaContabilita(sender As Object, e As System.EventArgs)
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)
            Dim item As RadPanelItem = RadPanelBar1.Items(1)
            Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
            Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
            For Each dataItem As GridDataItem In Griglia.MasterTableView.Items
                If dataItem("STATO_S").Text = "8" And TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked = True Then
                    ' Cosimo correzione: mi sembra che prendere IDS nella where sia sbagliato ... occorre prnedere IDENTIFICATIVO
                    'par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET FL_PR_CONTAB=1,ID_STATO=9 WHERE ID=" & dataItem("IDS").Text
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET FL_PR_CONTAB=1,ID_STATO=9 WHERE ID=" & dataItem("IDENTIFICATIVO").Text
                    par.cmd.ExecuteNonQuery()
                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & dataItem("IDS").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F271','')"
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & dataItem("IDENTIFICATIVO").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F271','')"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            connData.chiudi(True)
            VisualizzaAlert("Operazione effettuata", 1)
            Griglia.Rebind()
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Richiesta Massiva Rich. Consuntivazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try


    End Sub

    Public Sub RichiestaMassivaAccetta(sender As Object, e As System.EventArgs)
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)
            Dim item As RadPanelItem = RadPanelBar1.Items(1)
            Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
            Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
            For Each dataItem As GridDataItem In Griglia.MasterTableView.Items
                If dataItem("DATE_ACCETTATE").Text <> "SI" And TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & dataItem("IDENTIFICATIVO").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F272','" & par.PulisciStrSql("" & dataItem("DATAPGI").Text & " - " & dataItem("DATATDL").Text) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            connData.chiudi(True)
            VisualizzaAlert("Operazione effettuata", 1)
            Griglia.Rebind()
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Accettazione Massiva Date - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub RichiestaMassivaContesta(sender As Object, e As System.EventArgs)
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)
            Dim item As RadPanelItem = RadPanelBar1.Items(1)
            Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
            Dim headerCheckBox As CheckBox = TryCast(sender, CheckBox)
            For Each dataItem As GridDataItem In Griglia.MasterTableView.Items
                If dataItem("DATE_ACCETTATE").Text <> "SI" And TryCast(dataItem.FindControl("CheckBox1"), CheckBox).Checked = True Then
                    par.cmd.CommandText = "Insert into SISCOM_MI.SEGNALAZIONI_FO_IRR (ID,ID_SEGNALAZIONE, DATA_ORA, ID_TIPO, DESCRIZIONE,RIFERIMENTO) Values (SISCOM_MI.SEQ_SEGNALAZIONI_FO_IRR.NEXTVAL," & dataItem("IDENTIFICATIVO").Text & ", '" & Format(Now, "yyyyMMddHHmmss") & "',0, 'Le date " & dataItem("DATAPGI").Text & " - " & dataItem("DATATDL").Text & " non sono state accettate.','" & par.AggiustaData(dataItem("DATAPGI").Text) & par.AggiustaData(dataItem("DATATDL").Text) & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & dataItem("IDENTIFICATIVO").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F268','" & par.PulisciStrSql("MODIFICA PGI/TDL NON ACCETTATA - " & dataItem("DATAPGI").Text & " - " & dataItem("DATATDL").Text) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            connData.chiudi(True)
            VisualizzaAlert("Operazione effettuata", 1)
            Griglia.Rebind()
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Contestazione Massiva date - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub CaricaInAgenda(sender As Object, e As System.EventArgs)
        If idSelRisultati.Value <> "" Then
            'RadPanelBar1.Items(2).Expanded = True
            RicavaOrdini()
            Dim item As RadPanelItem = RadPanelBar1.Items(1)
            Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
            Griglia.Rebind()
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
        Else
            VisualizzaAlert("Selezionare un elemento dalla lista", 2)
        End If
    End Sub

    Protected Sub RadButtonEsciAgg1_Click(sender As Object, e As System.EventArgs) Handles RadButtonEsciAgg1.Click
        CaricaInAgenda(sender, e)
    End Sub

    Public Property iTotOrdini() As Integer
        Get
            If Not (ViewState("par_iTotOrdini") Is Nothing) Then
                Return CInt(ViewState("par_iTotOrdini"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotOrdini") = value
        End Set
    End Property

    Public Property iTotNumBozza() As Integer
        Get
            If Not (ViewState("par_iTotNumBozza") Is Nothing) Then
                Return CInt(ViewState("par_iTotNumBozza"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotNumBozza") = value
        End Set
    End Property

    Public Property iDivisore() As Integer
        Get
            If Not (ViewState("par_iDivisore") Is Nothing) Then
                Return CInt(ViewState("par_iDivisore"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iDivisore") = value
        End Set
    End Property

    Public Property iTotNumDaVerificare() As Integer
        Get
            If Not (ViewState("par_iTotNumDaVerificare") Is Nothing) Then
                Return CInt(ViewState("par_iTotNumDaVerificare"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotNumDaVerificare") = value
        End Set
    End Property

    Public Property iTotNumDaVerificareT() As Integer
        Get
            If Not (ViewState("par_iTotNumDaVerificareT") Is Nothing) Then
                Return CInt(ViewState("par_iTotNumDaVerificareT"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotNumDaVerificareT") = value
        End Set
    End Property

    Public Property iTotNumInCarico() As Integer
        Get
            If Not (ViewState("par_iTotNumInCarico") Is Nothing) Then
                Return CInt(ViewState("par_iTotNumInCarico"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotNumInCarico") = value
        End Set
    End Property

    Public Property iTotNumAnnullato() As Integer
        Get
            If Not (ViewState("par_iTotNumAnnullato") Is Nothing) Then
                Return CInt(ViewState("par_iTotNumAnnullato"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotNumAnnullato") = value
        End Set
    End Property

    Public Property iTotRC() As Integer
        Get
            If Not (ViewState("par_iTotRC") Is Nothing) Then
                Return CInt(ViewState("par_iTotRC"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotRC") = value
        End Set
    End Property

    Public Property iTotDC() As Integer
        Get
            If Not (ViewState("par_iTotDC") Is Nothing) Then
                Return CInt(ViewState("par_iTotDC"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotDC") = value
        End Set
    End Property

    Public Property iTotEvaso() As Integer
        Get
            If Not (ViewState("par_iTotEvaso") Is Nothing) Then
                Return CInt(ViewState("par_iTotEvaso"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotEvaso") = value
        End Set
    End Property

    Public Property sRepertorio() As String
        Get
            If Not (ViewState("par_sRepertorio") Is Nothing) Then
                Return CStr(ViewState("par_sRepertorio"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sRepertorio") = value
        End Set
    End Property

    Public Property sOrdine() As String
        Get
            If Not (ViewState("par_sOrdine") Is Nothing) Then
                Return CStr(ViewState("par_sOrdine"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sOrdine") = value
        End Set
    End Property

    Public Property sStrSql() As String
        Get
            If Not (ViewState("par_sStrSql") Is Nothing) Then
                Return CStr(ViewState("par_sStrSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSql") = value
        End Set
    End Property

    Public Property sStrSqlRisultati() As String
        Get
            If Not (ViewState("par_sStrSqlRisultati") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlRisultati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlRisultati") = value
        End Set
    End Property

    Public Property bTrovatoElemento() As String
        Get
            If Not (ViewState("par_bTrovatoElemento") Is Nothing) Then
                Return CStr(ViewState("par_bTrovatoElemento"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_bTrovatoElemento") = value
        End Set
    End Property

    Public Property iTotOrdiniT() As Integer
        Get
            If Not (ViewState("par_iTotOrdiniT") Is Nothing) Then
                Return CInt(ViewState("par_iTotOrdiniT"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotOrdiniT") = value
        End Set
    End Property

    Public Property iTotNumInCaricoT() As Integer
        Get
            If Not (ViewState("par_iTotNumInCaricoT") Is Nothing) Then
                Return CInt(ViewState("par_iTotNumInCaricoT"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotNumInCaricoT") = value
        End Set
    End Property

    Public Property iTotNumAnnullatoT() As Integer
        Get
            If Not (ViewState("par_iTotNumAnnullatoT") Is Nothing) Then
                Return CInt(ViewState("par_iTotNumAnnullatoT"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotNumAnnullatoT") = value
        End Set
    End Property

    Public Property iTotEvasoT() As Integer
        Get
            If Not (ViewState("par_iTotEvasoT") Is Nothing) Then
                Return CInt(ViewState("par_iTotEvasoT"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotEvasoT") = value
        End Set
    End Property

    Public Property iTotDCT() As Integer
        Get
            If Not (ViewState("par_iTotDCT") Is Nothing) Then
                Return CInt(ViewState("par_iTotDCT"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotDCT") = value
        End Set
    End Property

    Public Property iTotRCT() As Integer
        Get
            If Not (ViewState("par_iTotRCT") Is Nothing) Then
                Return CInt(ViewState("par_iTotRCT"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotRCT") = value
        End Set
    End Property

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

    Public Property sPrimoIngresso() As String
        Get
            If Not (ViewState("par_sPrimoIngresso") Is Nothing) Then
                Return CStr(ViewState("par_sPrimoIngresso"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPrimoIngresso") = value
        End Set
    End Property

    Protected Sub ImageButtonANNULLATO_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) 'Handles ImageButtonANNULLATO.Click
        AzzeraFiltri()

        'Dim item As RadPanelItem = RadPanelBar1.Items(0)
        'Dim Pulsante As CheckBox = TryCast(item.Items(0).FindControl("chANNULLATO"), CheckBox)
        'Pulsante.Checked = True

        Dim item As RadPanelItem = RadPanelBar1.Items(0)
        Dim Pulsante As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)
        For Each elemento As ListItem In Pulsante.Items
            If elemento.Value = "10" Then
                elemento.Selected = True
            Else
                elemento.Selected = False
            End If
        Next

        RicavaOrdini()
        sStrSqlRisultati = sStrSql
        Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item1.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        Griglia.Rebind()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
    End Sub


    Protected Sub ImageButtonEMESSO_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) 'Handles ImageButtonEMESSO.Click
        AzzeraFiltri()

        Dim item As RadPanelItem = RadPanelBar1.Items(0)
        Dim Pulsante As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)
        For Each elemento As ListItem In Pulsante.Items
            If elemento.Value = "1" Then
                elemento.Selected = True
            Else
                elemento.Selected = False
            End If
        Next
        RicavaOrdini()
        sStrSqlRisultati = sStrSql
        Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item1.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        Griglia.Rebind()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)

    End Sub

    Protected Sub ImageButtonINTEGRATO_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) 'Handles ImageButtonINTEGRATO.Click
        AzzeraFiltri()
        Dim item As RadPanelItem = RadPanelBar1.Items(0)
        Dim Pulsante As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)
        For Each elemento As ListItem In Pulsante.Items
            If elemento.Value = "2" Then
                elemento.Selected = True
            Else
                elemento.Selected = False
            End If
        Next
        RicavaOrdini()
        sStrSqlRisultati = sStrSql
        Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item1.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        Griglia.Rebind()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
    End Sub

    Private Sub VisualizzaAlert(ByVal TestoMessaggio As String, ByVal Tipo As Integer)
        Select Case Tipo
            Case 1
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','success');", True)
            Case 2
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "warn", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','warn');", True)
            Case 3
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "error", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','error');", True)
        End Select
    End Sub

    Protected Sub ImageButtonFINE_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) 'Handles ImageButtonFINE.Click
        AzzeraFiltri()
        Dim item As RadPanelItem = RadPanelBar1.Items(0)
        Dim Pulsante As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)
        For Each elemento As ListItem In Pulsante.Items
            If elemento.Value = "8" Then
                elemento.Selected = True
            Else
                elemento.Selected = False
            End If
        Next
        RicavaOrdini()
        sStrSqlRisultati = sStrSql
        Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item1.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        Griglia.Rebind()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
    End Sub

    Protected Sub ImageButtonRC_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) 'Handles ImageButtonRC.Click
        AzzeraFiltri()
        Dim item As RadPanelItem = RadPanelBar1.Items(0)
        Dim Pulsante As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)
        For Each elemento As ListItem In Pulsante.Items
            If elemento.Value = "9" Then
                elemento.Selected = True
            Else
                elemento.Selected = False
            End If
        Next
        RicavaOrdini()
        sStrSqlRisultati = sStrSql
        Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item1.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        Griglia.Rebind()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
    End Sub

    Protected Sub ImageButtonContab_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) 'Handles ImageButtonContab.Click
        AzzeraFiltri()
        Dim item As RadPanelItem = RadPanelBar1.Items(0)
        Dim Pulsante As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)
        For Each elemento As ListItem In Pulsante.Items
            If elemento.Value = "3" Then
                elemento.Selected = True
            Else
                elemento.Selected = False
            End If
        Next
        RicavaOrdini()
        sStrSqlRisultati = sStrSql
        Dim item1 As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item1.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        Griglia.Rebind()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
    End Sub



    Protected Sub RadButtonConfermaExport_Click(sender As Object, e As System.EventArgs) Handles RadButtonConfermaExport.Click
        Dim dt As New System.Data.DataTable
        Dim dtG As New System.Data.DataTable
        Dim Intervallo As String = ""
        Dim sLinea As String = ""
        Dim Chiave As String = Format(Now, "yyyyMMddHHmmss")
        Dim ROW As System.Data.DataRow
        Try
            connData.apri(True)
            If RadioButtonVistaCorrente.Checked = True Then
                Intervallo = " (CASE WHEN DATA_INIZIO_INTERVENTO IS NOT NULL THEN DATA_INIZIO_INTERVENTO ELSE (SELECT SUBSTR (DATA_ORA, 1, 8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE = MANUTENZIONI.ID AND COD_EVENTO = 'F91') END)>='" & Format(RadAgenda.VisibleRangeStart, "yyyyMMdd") & "' AND (CASE WHEN DATA_INIZIO_INTERVENTO IS NOT NULL THEN DATA_INIZIO_INTERVENTO ELSE (SELECT SUBSTR (DATA_ORA, 1, 8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE = MANUTENZIONI.ID AND COD_EVENTO = 'F91') END)<='" & Format(RadAgenda.VisibleRangeEnd, "yyyyMMdd") & "'"
                par.cmd.CommandText = Replace(UCase(Session.Item("G1")), "ORDER BY FORNITORI.RAGIONE_SOCIALE,DATA_INIZIO_INTERVENTO DESC", " AND") & Intervallo
            Else
                par.cmd.CommandText = Session.Item("G1")
            End If

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                dtG.Columns.Add("RAGIONE_SOCIALE")
                dtG.Columns.Add("GIORNO")
                dtG.Columns.Add("STATO")
                dtG.Columns.Add("DATO")


                Do While myReader.Read
                    ROW = dtG.NewRow()
                    ROW.Item("RAGIONE_SOCIALE") = par.IfNull(myReader("ragione_sociale"), "")
                    ROW.Item("GIORNO") = Format(par.IfNull(myReader("data_inizio_INTERVENTO"), ""), "dd/MM/yyyy")
                    Select Case par.IfNull(myReader("STATO_S"), "")
                        Case "1"
                            ROW.Item("STATO") = " - ORDINE DA VERIFICARE "
                        Case "2"
                            ROW.Item("STATO") = " - ORDINE IN CARICO "
                        Case "6"
                            ROW.Item("STATO") = " - ORDINE ANNULLATO "
                        Case "8"
                            ROW.Item("STATO") = " - ORDINE EVASO "
                        Case "9"
                            ROW.Item("STATO") = " - RICHIESTA CONSUNTIVAZIONE "
                        Case "3"
                            ROW.Item("STATO") = " - ORDINE DA CONSUNTIVARE "
                        Case "10"
                            ROW.Item("STATO") = " - IN CARICO MA RITARDO "
                        Case Else
                            ROW.Item("STATO") = " - "
                    End Select

                    ROW.Item("DATO") = "ODL:" & par.IfNull(myReader("num_odl"), "") & " - REP." & par.IfNull(myReader("num_repertorio"), "")
                    dtG.Rows.Add(ROW)

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_FO_EXPORT VALUES ('" & Chiave & "','" & par.PulisciStrSql(par.IfNull(myReader("ragione_sociale"), "")) & "','" & par.IfNull(myReader("data_inizio_ricerca"), "") & "','" & ROW.Item("STATO") & "','ODL:" & par.IfNull(myReader("num_odl"), "") & " - REP." & par.IfNull(myReader("num_repertorio"), "") & "')"
                    par.cmd.ExecuteNonQuery()
                Loop
                dt.Columns.Add("RAGIONE_SOCIALE")

                par.cmd.CommandText = "select distinct giorno from SISCOM_MI.SEGNALAZIONI_FO_EXPORT where chiave='" & Chiave & "' order by giorno asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    dt.Columns.Add(myReader1("GIORNO"))
                Loop
                myReader1.Close()

                par.cmd.CommandText = "select DISTINCT RAGIONE_SOCIALE from SISCOM_MI.SEGNALAZIONI_FO_EXPORT where chiave='" & Chiave & "' order by RAGIONE_SOCIALE asc"
                myReader1 = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    ROW = dt.NewRow()
                    ROW.Item("RAGIONE_SOCIALE") = myReader1("RAGIONE_SOCIALE")
                    dt.Rows.Add(ROW)
                Loop
                myReader1.Close()

                Dim RS As String = ""
                Dim i As Integer = 0
                Dim TESTO As String = ""

                For Each riga As Data.DataRow In dt.Rows
                    RS = riga.Item("RAGIONE_SOCIALE")
                    i = 0
                    For Each colonna As Data.DataColumn In dt.Columns
                        If i > 0 Then
                            TESTO = ""
                            Dim Qresult As Data.DataRow() = dtG.Select("RAGIONE_SOCIALE='" & par.PulisciStrSql(RS) & "' and GIORNO='" & colonna.ToString() & "'")
                            For Each riga1 As Data.DataRow In Qresult
                                TESTO = TESTO & riga1("DATO") & riga1("STATO") & vbCrLf
                            Next
                            riga.Item(colonna) = TESTO
                        End If
                        i = i + 1
                    Next
                Next
            Else
                VisualizzaAlert("Nessun appuntamento nell'intervallo", 1)
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
            End If
            myReader.Close()
            connData.chiudi(True)
            Dim xls As New ExcelSiSol

            Dim nomeFile As String = xls.EsportaExcelDaDTCalLavori(ExcelSiSol.Estensione.Office2007_xlsx, "ExportCalendario", "Calendario", dt, , , )
            If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                'Response.Redirect("../FileTemp/" & nomeFile, False)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If


            'dt.Columns.Add("RAGIONE SOCIALE")
            'ROW = dt.NewRow()
            'ROW.Item("SPORTELLO") = myReader("SPORTELLO")
            'dt.Rows.Add(ROW)

            'dt.Columns.Add("SPORTELLO")

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - export - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Protected Sub btnExpor_Click(sender As Object, e As System.EventArgs) Handles btnExpor.Click
        Dim dt As New System.Data.DataTable
        Dim dtG As New System.Data.DataTable
        Dim Intervallo As String = ""
        Dim sLinea As String = ""
        Dim Chiave As String = Format(Now, "yyyyMMddHHmmss")
        Dim ROW As System.Data.DataRow
        Try
            connData.apri(True)

            'Intervallo = " (CASE WHEN DATA_INIZIO_INTERVENTO IS NOT NULL THEN DATA_INIZIO_INTERVENTO ELSE (SELECT SUBSTR (DATA_ORA, 1, 8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE = MANUTENZIONI.ID AND COD_EVENTO = 'F91') END)>='" & Format(RadAgenda.VisibleRangeStart, "yyyyMMdd") & "' AND (CASE WHEN DATA_INIZIO_INTERVENTO IS NOT NULL THEN DATA_INIZIO_INTERVENTO ELSE (SELECT SUBSTR (DATA_ORA, 1, 8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE = MANUTENZIONI.ID AND COD_EVENTO = 'F91') END)<='" & Format(DateAdd(DateInterval.Day, -1, RadAgenda.VisibleRangeEnd), "yyyyMMdd") & "'"
            Intervallo = "   (CASE WHEN MANUTENZIONI.DATA_PGI IS NULL THEN NVL (MANUTENZIONI.DATA_INIZIO_INTERVENTO,(SELECT SUBSTR (DATA_ORA, 1, 8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE = MANUTENZIONI.ID AND COD_EVENTO = 'F91')) ELSE MANUTENZIONI.DATA_PGI END) >='" & Format(RadAgenda.VisibleRangeStart, "yyyyMMdd") & "' AND (CASE WHEN MANUTENZIONI.DATA_PGI IS NULL THEN NVL (MANUTENZIONI.DATA_INIZIO_INTERVENTO,(SELECT SUBSTR (DATA_ORA, 1, 8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE = MANUTENZIONI.ID AND COD_EVENTO = 'F91')) ELSE MANUTENZIONI.DATA_PGI END) <='" & Format(DateAdd(DateInterval.Day, -1, RadAgenda.VisibleRangeEnd), "yyyyMMdd") & "'"
            par.cmd.CommandText = Replace(UCase(Session.Item("G1")), "ORDER BY FORNITORI.RAGIONE_SOCIALE,DATA_INIZIO_INTERVENTO DESC", " AND") & Intervallo


            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                dtG.Columns.Add("RAGIONE_SOCIALE")
                dtG.Columns.Add("GIORNO")
                dtG.Columns.Add("STATO")
                dtG.Columns.Add("DATO")


                Do While myReader.Read
                    ROW = dtG.NewRow()
                    ROW.Item("RAGIONE_SOCIALE") = par.IfNull(myReader("ragione_sociale"), "")
                    ROW.Item("GIORNO") = Format(par.IfNull(myReader("data_inizio_INTERVENTO"), ""), "dd/MM/yyyy")
                    Select Case par.IfNull(myReader("STATO_S"), "")
                        Case "1"
                            ROW.Item("STATO") = " - ORDINE DA VERIFICARE "
                        Case "2"
                            ROW.Item("STATO") = " - ORDINE IN CARICO "
                        Case "6"
                            ROW.Item("STATO") = " - ORDINE ANNULLATO "
                        Case "8"
                            ROW.Item("STATO") = " - ORDINE EVASO "
                        Case "9"
                            ROW.Item("STATO") = " - RICHIESTA CONSUNTIVAZIONE "
                        Case "3"
                            ROW.Item("STATO") = " - ORDINE DA CONSUNTIVARE "
                        Case "10"
                            ROW.Item("STATO") = " - IN CARICO MA RITARDO "
                        Case Else
                            ROW.Item("STATO") = " - "
                    End Select
                    ROW.Item("DATO") = "ODL:" & par.IfNull(myReader("num_odl"), "") & " - REP." & par.IfNull(myReader("num_repertorio"), "")
                    dtG.Rows.Add(ROW)

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_FO_EXPORT VALUES ('" & Chiave & "','" & par.PulisciStrSql(par.IfNull(myReader("ragione_sociale"), "")) & "','" & Mid(par.IfNull(myReader("data_inizio_INTERVENTO"), ""), 1, 10) & "','" & ROW.Item("STATO") & "','ODL:" & par.IfNull(myReader("num_odl"), "") & " - REP." & par.IfNull(myReader("num_repertorio"), "") & "')"
                    par.cmd.ExecuteNonQuery()
                Loop
                dt.Columns.Add("RAGIONE_SOCIALE")

                par.cmd.CommandText = "select distinct giorno from SISCOM_MI.SEGNALAZIONI_FO_EXPORT where chiave='" & Chiave & "' order by giorno asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    dt.Columns.Add(myReader1("GIORNO"))
                Loop
                myReader1.Close()

                Dim dt1 As New System.Data.DataTable
                dt1 = dt

                par.cmd.CommandText = "select DISTINCT RAGIONE_SOCIALE from SISCOM_MI.SEGNALAZIONI_FO_EXPORT where chiave='" & Chiave & "' order by RAGIONE_SOCIALE asc"
                'par.cmd.CommandText = "select DISTINCT RAGIONE_SOCIALE,GIORNO from SISCOM_MI.SEGNALAZIONI_FO_EXPORT where chiave='" & Chiave & "' order by RAGIONE_SOCIALE asc,GIORNO ASC"
                myReader1 = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    ROW = dt.NewRow()
                    ROW.Item("RAGIONE_SOCIALE") = myReader1("RAGIONE_SOCIALE")
                    dt.Rows.Add(ROW)
                Loop
                myReader1.Close()

                Dim RS As String = ""
                Dim i As Integer = 0
                Dim TESTO As String = ""
                Dim K As Integer = 0

                For Each riga As Data.DataRow In dt.Rows
                    RS = riga.Item("RAGIONE_SOCIALE")
                    i = 0
                    For Each colonna As Data.DataColumn In dt.Columns
                        If i > K Then
                            TESTO = ""
                            Dim Qresult As Data.DataRow() = dtG.Select("RAGIONE_SOCIALE='" & par.PulisciStrSql(RS) & "' and GIORNO='" & colonna.ToString() & "'")
                            For Each riga1 As Data.DataRow In Qresult
                                TESTO = TESTO & riga1("DATO") & riga1("STATO") & vbCrLf



                            Next
                            riga.Item(colonna) = TESTO
                        End If
                        i = i + 1
                    Next
                Next
            Else
                VisualizzaAlert("Nessun appuntamento nell'intervallo", 2)
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
            End If
            myReader.Close()
            connData.chiudi(True)

            If dtG.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol

                Dim nomeFile As String = xls.EsportaExcelDaDTCalLavori(ExcelSiSol.Estensione.Office2007_xlsx, "ExportCalendario", "Calnedario", dt, , , )
                If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();AggiungiLink();", True)
                Else

                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "errore", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
                End If
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - export - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

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
    Public Property NumeroElementi() As Integer
        Get
            If Not (ViewState("par_NumeroElementi") Is Nothing) Then
                Return CStr(ViewState("par_NumeroElementi"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_NumeroElementi") = value
        End Set
    End Property

    Public Property kk() As Integer
        Get
            If Not (ViewState("par_kk") Is Nothing) Then
                Return CStr(ViewState("par_kk"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_kk") = value
        End Set
    End Property

    Private Sub Ricarica()
        CaricaMaschera()
        Dim item As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        HFFiltroFO.Value = "Tutti"
        Griglia.Rebind()
    End Sub
    Private Sub RadButtonDirettoreLavori_Click(sender As Object, e As EventArgs) Handles RadButtonDirettoreLavori.Click
        Ricarica()
    End Sub

    Private Sub RadButtonFieldQualityManager_Click(sender As Object, e As EventArgs) Handles RadButtonFieldQualityManager.Click
        Ricarica()
    End Sub

    Private Sub RadButtonTecnicoAmministrativo_Click(sender As Object, e As EventArgs) Handles RadButtonTecnicoAmministrativo.Click
        Ricarica()
    End Sub

    Private Sub RadButtonBuildingManager_Click(sender As Object, e As EventArgs) Handles RadButtonBuildingManager.Click
        Ricarica()
    End Sub

    Private Sub btnExportGriglia_Click(sender As Object, e As EventArgs) Handles btnExportGriglia.Click
        Dim item As RadPanelItem = RadPanelBar1.Items(1)
        Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)

        If Griglia.Items.Count > 0 Then
            isExporting.Value = "1"
            Dim context As RadProgressContext = RadProgressContext.Current
            context.SecondaryTotal = 0
            context.SecondaryValue = 0
            context.SecondaryPercent = 0

            Griglia.ExportSettings.Excel.FileExtension = "xls"
            Griglia.ExportSettings.Excel.Format = GridExcelExportFormat.Biff
            Griglia.ExportSettings.IgnorePaging = True
            Griglia.PageSize = NumeroElementi
            Griglia.Rebind()
            Griglia.ExportSettings.ExportOnlyData = False
            Griglia.ExportSettings.OpenInNewWindow = True

            Griglia.MasterTableView.ExportToExcel()

        Else
            VisualizzaAlert("Nessun elemento da esportare", 2)
        End If

        'Dim connAperta As Boolean = False
        'If connData.Connessione.State = Data.ConnectionState.Closed Then
        '    connData.apri(False)
        '    connAperta = True
        'End If
        'Dim item As RadPanelItem = RadPanelBar1.Items(1)
        'Dim Griglia As RadGrid = TryCast(item.Items(0).FindControl("RadGridRisultatiTab"), RadGrid)
        ''Dim dt As Data.DataTable = par.getDataTableGrid(sStrSqlRisultati)
        'Dim dt As Data.DataTable = par.getDataTableFilterSortRadGrid(sStrSqlRisultati, Griglia)
        'Dim xls As New ExcelSiSol
        'Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ElencoTabellare_", "ElencoTabellare_", dt, True)

        'par.EffettuaDownloadFile(Me.Page, nomeFile)
        'If connAperta = True Then
        '    connData.chiudi(False)
        'End If
    End Sub

    Protected Sub Page_PreRenderComplete(sender As Object, e As System.EventArgs) Handles Me.PreRenderComplete
        If isExporting.Value = "1" Then
            Dim context As RadProgressContext = RadProgressContext.Current
            context.CurrentOperationText = "Export in corso..."
            context("ProgressDone") = True
            context.OperationComplete = True
            context.SecondaryTotal = 0
            context.SecondaryValue = 0
            context.SecondaryPercent = 0
            isExporting.Value = "0"
        End If

    End Sub

    Protected Sub Next_Appointment(ByVal sender As Object, ByVal e As EventArgs)
        Dim nGG As Integer = Session.Item("g3")
        Dim appointments As Appointment() = RadAgenda.Appointments.ToArray()
        Array.Sort(appointments, New AppointmentComparer())

        Dim itemX As RadPanelItem = RadPanelBar1.Items(2)
        Dim cmbGiorni As RadDropDownList = TryCast(itemX.Items(0).FindControl("cmbGiorni"), RadDropDownList)
        Session.Item("g3") = cmbGiorni.SelectedItem.Text

        If cmbGiorni.Visible = False Then
            nGG = 0
        End If

        For Each appointment As Appointment In appointments
            Dim appointmentStart As DateTime = RadAgenda.UtcToDisplay(appointment.Start)

            If cmbGiorni.Visible = False Then
                If appointmentStart.AddDays(-nGG) > RadAgenda.SelectedDate And (appointmentStart.Month <> RadAgenda.SelectedDate.Month) Then
                    RadAgenda.SelectedDate = appointmentStart.Date
                    Exit For
                End If

            Else
                If appointmentStart.AddDays(-nGG) > RadAgenda.SelectedDate Then
                    RadAgenda.SelectedDate = appointmentStart.Date
                    Exit For
                End If

            End If

        Next
    End Sub
    Protected Sub Previous_Appointment(ByVal sender As Object, ByVal e As EventArgs)
        Dim nGG As Integer = Session.Item("g3")
        Dim appointments As Appointment() = RadAgenda.Appointments.ToArray()
        Array.Sort(appointments, New AppointmentComparer())
        Array.Reverse(appointments)

        Dim itemX As RadPanelItem = RadPanelBar1.Items(2)
        Dim cmbGiorni As RadDropDownList = TryCast(itemX.Items(0).FindControl("cmbGiorni"), RadDropDownList)
        Session.Item("g3") = cmbGiorni.SelectedItem.Text

        If cmbGiorni.Visible = False Then
            nGG = 0
        End If

        For Each appointment As Appointment In appointments
            Dim appointmentStart As DateTime = RadAgenda.UtcToDisplay(appointment.Start)

            If cmbGiorni.Visible = False Then
                If (appointmentStart.AddDays(nGG) < RadAgenda.SelectedDate) And (appointmentStart.Month <> RadAgenda.SelectedDate.Month) Then
                    RadAgenda.SelectedDate = appointmentStart.Date
                    Exit For
                End If
            Else
                If (appointmentStart.AddDays(nGG) < RadAgenda.SelectedDate) Then
                    RadAgenda.SelectedDate = appointmentStart.Date
                    Exit For
                End If
            End If
        Next
    End Sub

    '//////////////////////////////////////////////////////////////
    Private ReadOnly Property CustomersChecked(id As String) As Hashtable
        Get
            Dim res As Object = ViewState("_cc" & id)
            If (res Is Nothing) Then
                res = New Hashtable
                ViewState("_cc" & id) = res
            End If
            Return CType(res, Hashtable)
        End Get
    End Property

    Private Function SetCheck(id As String, Value As Boolean) As Boolean

        ViewState("_cc" & id) = Value

        Return Value
    End Function
    Private Function GetCheck(id As String) As Boolean
        Dim lRet As Boolean
        lRet = ViewState("_cc" & id)

        Return lRet
    End Function

    Protected Sub CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim box As CheckBox = CType(sender, CheckBox)
        Dim item As GridDataItem = CType(box.NamingContainer, GridDataItem)
        Dim target As Hashtable = Nothing

        'If (item.OwnerTableView.DataMember = "Customers") Then
        '    target = CustomersChecked
        'ElseIf (item.OwnerTableView.DataMember = "Customers1") Then
        '    target = Customers1Checked
        'Else
        '    target = Customers2Checked
        'End If

        'target = CustomersChecked(item.Cells(2).Text)
        target = CustomersChecked(item("ID_MANUTENZIONE").Text)

        If box.Checked Then
            target(item("ID_MANUTENZIONE").Text) = True
        Else
            target(item("ID_MANUTENZIONE").Text) = Nothing
        End If

    End Sub
    'Private Sub RadGridRisultatiTab_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) 'Handles RadGridRisultatiTab.ItemDataBound

    '    If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '        Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
    '        Dim isChecked As Object = Nothing
    '        isChecked = CustomersChecked(dataItem("CheckBox1").Text)

    '        Dim box As CheckBox = CType(dataItem.FindControl("CheckBox1"), CheckBox)
    '        box.Checked = CType(isChecked, Boolean) = True

    '        If Not (isChecked Is Nothing) Then
    '            box.Checked = CType(isChecked, Boolean) = True
    '        End If

    '    End If
    'End Sub

    '//////////////////////////////////////////////////////////////

End Class


Class AppointmentComparer
    Inherits Comparer(Of Object)
    Public Overrides Function Compare(ByVal x As Object, ByVal y As Object) As Integer
        Dim first As Appointment = TryCast(x, Appointment)
        Dim second As Appointment = TryCast(y, Appointment)

        If first Is Nothing OrElse second Is Nothing Then
            Throw New InvalidOperationException("Can't compare null object(s).")
        End If

        If first.Start < second.Start Then
            Return -1
        End If

        If first.Start > second.Start Then
            Return 1
        End If

        If first.[End] > second.[End] Then
            Return -1
        End If

        Return 0
    End Function
End Class
