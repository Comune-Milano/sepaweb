Imports Telerik.Web.UI
Imports Telerik.Web.UI.GridExcelBuilder
Imports Telerik.Web.UI.PersistenceFramework


Partial Class FORNITORI_Default
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim valoreperc As Integer

    Private Sub CaricaTipoRichieste()
        'Try
        '    Dim item As RadPanelItem = RadPanelBar1.Items(1)
        '    Dim CheckList As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListTipoR"), CheckBoxList)

        '    connData.apri()
        '    par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_TIPO_RICHIESTA ORDER BY ID ASC", CheckList, "ID", "DESCRIZIONE")
        '    connData.chiudi()
        '    For Each elemento As ListItem In CheckList.Items
        '        elemento.Selected = True
        '    Next
        'Catch ex As Exception
        '    connData.chiudi()
        '    Session.Add("ERRORE", "Provenienza: Fornitori - CaricaTipoRichieste - " & ex.Message)
        '    Response.Redirect("../Errore.aspx", False)
        'End Try
    End Sub
    Private Sub CaricaStatoSegnalazioni()
        Try

            Dim item As RadPanelItem = RadPanelBar1.Items(0)
            Dim CheckList As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)

            connData.apri()
            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO WHERE PROGR<>0 ORDER BY PROGR ASC", CheckList, "ID", "DESCRIZIONE")

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

    Private Sub VerificaOperatore()
        Try
            connData.apri()
            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("LIVELLO_WEB"), "0") = "1" Then
                    iIndiceFornitore = "0"
                    sStrAppaltiOperatori = "  "
                Else
                    If par.IfNull(myReader("MOD_FO_ID_FO"), "0") <> "0" Then
                        iIndiceFornitore = "(select id from siscom_mi.appalti where id_fornitore = " & par.IfNull(myReader("MOD_FO_ID_FO"), 0) & ")"
                        sStrAppaltiOperatori = " APPALTI.ID_FORNITORE IN (" & par.IfNull(myReader("MOD_FO_ID_FO"), "0") & ") AND "
                    Else
                        iIndiceFornitore = "0"

                        iIndiceFornitore = "SELECT DISTINCT APPALTI.ID AS ID_APPALTO " _
                                            & "FROM SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.EDIFICI,SISCOM_MI.BUILDING_MANAGER_OPERATORI " _
                                            & "WHERE Appalti.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO = EDIFICI.ID " _
                                            & "And EDIFICI.ID_BM = BUILDING_MANAGER_OPERATORI.ID_BM AND BUILDING_MANAGER_OPERATORI.ID_OPERATORE = " & Session.Item("id_operatore")
                        'Dim myReaderBM As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        'Do While myReaderBM.Read
                        '    iIndiceFornitore = iIndiceFornitore & myReaderBM("ID_APPALTO") & ","
                        'Loop
                        'myReaderBM.Close()
                        If iIndiceFornitore <> "0" Then
                            iIndiceFornitore = "(" & Mid(iIndiceFornitore, 1, Len(iIndiceFornitore) - 1) & ")"
                            sStrAppaltiOperatori = " APPALTI.ID_FORNITORE IN " & iIndiceFornitore & " AND "
                        Else
                            iIndiceFornitore = "SELECT DISTINCT APPALTI.ID AS ID_APPALTO FROM SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_DL WHERE APPALTI.ID=APPALTI_DL.ID_GRUPPO AND APPALTI_DL.ID_OPERATORE=" & Session.Item("id_operatore")
                            'Dim myReaderDL As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'Do While myReaderDL.Read
                            '    iIndiceFornitore = iIndiceFornitore & myReaderDL("ID_APPALTO") & ","
                            'Loop
                            'myReaderDL.Close()
                            If iIndiceFornitore <> "0" Then
                                iIndiceFornitore = "(" & Mid(iIndiceFornitore, 1, Len(iIndiceFornitore)) & ")"
                                sStrAppaltiOperatori = " APPALTI.ID_FORNITORE IN " & iIndiceFornitore & " AND "
                            End If
                        End If
                        'OPS.Value = "0"
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

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        RadPersistenceManager1.StorageProvider = New SessionStorageProvider()
    End Sub


    'Private Sub VerificaOperatore()
    '    Try
    '        connData.apri()
    '        par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
    '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader.Read Then
    '            If par.IfNull(myReader("MOD_FO_ID_FO"), "0") <> "0" Then
    '                sStrAppaltiOperatori = " APPALTI_FORNITORI.ID_FORNITORE=" & par.IfNull(myReader("MOD_FO_ID_FO"), "0") & " AND "
    '            Else
    '                sStrAppaltiOperatori = ""
    '            End If
    '        End If
    '        myReader.Close()
    '        connData.chiudi()
    '    Catch ex As Exception
    '        connData.chiudi()
    '        Session.Add("ERRORE", "Provenienza: Fornitori - VerificaOperatore - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try

    'End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            If Session.Item("MOD_FORNITORI") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            If Session.Item("MOD_FORNITORI_RDO") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                VerificaOperatore()
                CaricaStatoSegnalazioni()
                CaricaTipoRichieste()
                'Dim item As RadPanelItem = RadPanelBar1.Items(1)
                'Dim cmbList As RadDropDownList = TryCast(item.Items(0).FindControl("cmbPriorita"), RadDropDownList)
                'par.caricaDropdownListTelerik("select id,descrizione from siscom_mi.SLA_PRIORITA order by descrizione desc", cmbList, "ID", "DESCRIZIONE", True, "-1", "---")

                Dim itemOp As RadPanelItem = RadPanelBar1.Items(0)
                Dim cmbListOp As RadComboBox = TryCast(itemOp.Items(0).FindControl("cmbFornitori"), RadComboBox)
                If iIndiceFornitore <> "0" Then
                    par.caricaComboTelerik("select FORNITORI.ID,FORNITORI.ragione_sociale " _
                                           & " from siscom_mi.fornitori where FORNITORI.ID IN " _
                                           & " (SELECT DISTINCT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE ID IN " & iIndiceFornitore & ") " _
                                           & " ORDER BY FORNITORI.RAGIONE_SOCIALE ASC", cmbListOp, "ID", "RAGIONE_SOCIALE", True, "-1", "---")
                    'cmbListOp.Enabled = False
                Else
                    par.caricaComboTelerik("select FORNITORI.ID,FORNITORI.ragione_sociale from siscom_mi.fornitori where FORNITORI.ID in (select distinct id_fornitore from siscom_mi.APPALTI WHERE nvl(MODULO_FORNITORI,0)=1) ORDER BY FORNITORI.RAGIONE_SOCIALE ASC", cmbListOp, "ID", "RAGIONE_SOCIALE", True, "-1", "---")
                End If

                BindGrid()
                dgvSegnalazioni.Rebind()
            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza: Fornitori - Segnalazioni - Carica - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub CaricaProfilo()
        If Session.Item("StorageProviderKey") Is Nothing Then
            SessionStorageProvider.StorageProviderKey = Session.Item("ID_OPERATORE")
            RadPersistenceManager1.SaveState()
            Session.Add("StorageProviderKey", Session.Item("ID_OPERATORE"))
        Else
            SessionStorageProvider.StorageProviderKey = Session.Item("ID_OPERATORE")
            RadPersistenceManager1.LoadState()
        End If

    End Sub

    Private Function CaricaCriteri() As String
        CaricaCriteri = ""
        Dim sStrStato As String = ""
        sIntervalloDataRichiesta = ""
        Dim bTrovato As Boolean = False
        Dim bTrovatoData As Boolean = False

        Dim item As RadPanelItem = RadPanelBar1.Items(0)
        Dim CheckList As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)

        If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
        For Each elemento As ListItem In CheckList.Items
            If elemento.Selected = True Then
                sStrStato = sStrStato & elemento.Value & ","
            End If
            bTrovato = True
        Next
        If sStrStato <> "" Then
            sStrStato = " TAB_STATI_SEGNALAZIONI_FO.ID IN (" & Mid(sStrStato, 1, Len(sStrStato) - 1) & ") "
        End If
        CaricaCriteri = CaricaCriteri & sStrStato


        Dim itemG As RadPanelItem = RadPanelBar1.Items(0)
        Dim cmbListG As RadDropDownList = TryCast(itemG.Items(0).FindControl("cmbGravita"), RadDropDownList)
        If cmbListG.SelectedText <> "---" Then
            If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
            CaricaCriteri = CaricaCriteri & " PERICOLO_SEGNALAZIONI.ID=" & cmbListG.SelectedValue & " "
            bTrovato = True
        End If

        Dim cmbListOp As RadComboBox = TryCast(item.Items(0).FindControl("cmbFornitori"), RadComboBox)
        If cmbListOp.Text <> "---" Then
            If cmbListOp.SelectedValue <> "" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " APPALTI.ID_FORNITORE = " & cmbListOp.SelectedValue & " "
                bTrovato = True
            Else
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " FORNITORI.RAGIONE_SOCIALE LIKE '%" & cmbListOp.Text & "%' "
                bTrovato = True
            End If
        Else
            If iIndiceFornitore <> "0" Then
                If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
                CaricaCriteri = CaricaCriteri & " APPALTI.ID IN " & iIndiceFornitore & " "
                bTrovato = True
            End If
        End If

        If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
            If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
            CaricaCriteri = CaricaCriteri & " APPALTI.ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE ID_OPERATORE= " & Session.Item("ID_OPERATORE") & ") "
        End If

        'APPALTI.ID_FORNITORE


        Dim ControlloData As RadDatePicker = TryCast(item.Items(0).FindControl("txtRicDA"), RadDatePicker)
        'If ControlloData.SelectedDate.ToString() <> "" Then
        '    If bTrovatoData = True Then sIntervalloDataRichiesta = sIntervalloDataRichiesta & " AND "
        '    sIntervalloDataRichiesta = sIntervalloDataRichiesta & " SUBSTR(DATA_RICHIESTA,7,4)||SUBSTR(DATA_RICHIESTA,4,2)||SUBSTR(DATA_RICHIESTA,1,2)>= '" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
        '    bTrovatoData = True
        'End If
        'ControlloData = TryCast(item.Items(0).FindControl("txtRicA"), RadDatePicker)
        'If ControlloData.SelectedDate.ToString() <> "" Then
        '    If bTrovatoData = True Then sIntervalloDataRichiesta = sIntervalloDataRichiesta & " AND "
        '    sIntervalloDataRichiesta = sIntervalloDataRichiesta & " SUBSTR(DATA_RICHIESTA,7,4)||SUBSTR(DATA_RICHIESTA,4,2)||SUBSTR(DATA_RICHIESTA,1,2)<='" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
        '    bTrovatoData = True
        'End If

        If ControlloData.SelectedDate.ToString() <> "" Then
            If bTrovatoData = True Then sIntervalloDataRichiesta = sIntervalloDataRichiesta & " AND "
            sIntervalloDataRichiesta = sIntervalloDataRichiesta & " SUBSTR(DATA_RICHIESTA_FILTRO,7,4)||SUBSTR(DATA_RICHIESTA_FILTRO,4,2)||SUBSTR(DATA_RICHIESTA_FILTRO,1,2)>= '" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
            bTrovatoData = True
        End If
        ControlloData = TryCast(item.Items(0).FindControl("txtRicA"), RadDatePicker)
        If ControlloData.SelectedDate.ToString() <> "" Then
            If bTrovatoData = True Then sIntervalloDataRichiesta = sIntervalloDataRichiesta & " AND "
            sIntervalloDataRichiesta = sIntervalloDataRichiesta & " SUBSTR(DATA_RICHIESTA_FILTRO,7,4)||SUBSTR(DATA_RICHIESTA_FILTRO,4,2)||SUBSTR(DATA_RICHIESTA_FILTRO,1,2)<='" & Format(ControlloData.SelectedDate, "yyyyMMdd") & "' "
            bTrovatoData = True
        End If

        'Dim ControlloTesto As RadTextBox = TryCast(item.Items(0).FindControl("txtNumIntDa"), RadTextBox)
        'If ControlloTesto.Text <> "" Then
        '    If bTrovatoData = True Then sIntervalloDataRichiesta = sIntervalloDataRichiesta & " AND "
        '    sIntervalloDataRichiesta = sIntervalloDataRichiesta & " nvl(IDS,0)>= " & ControlloTesto.Text & " "
        '    bTrovatoData = True
        'End If
        'ControlloTesto = TryCast(item.Items(0).FindControl("txtNumIntA"), RadTextBox)
        'If ControlloTesto.Text <> "" Then
        '    If bTrovatoData = True Then sIntervalloDataRichiesta = sIntervalloDataRichiesta & " AND "
        '    sIntervalloDataRichiesta = sIntervalloDataRichiesta & " nvl(IDS,0)<= " & ControlloTesto.Text & " "
        '    bTrovatoData = True
        'End If
        If sIntervalloDataRichiesta <> "" Then sIntervalloDataRichiesta = " WHERE " & sIntervalloDataRichiesta

        'filtri avanzati
        sStrStato = ""
        'If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
        'item = RadPanelBar1.Items(1)
        'CheckList = TryCast(item.Items(0).FindControl("CheckBoxListTipoR"), CheckBoxList)

        'For Each elemento As ListItem In CheckList.Items
        '    If elemento.Selected = True Then
        '        sStrStato = sStrStato & elemento.Value & ","
        '    End If
        '    bTrovato = True
        'Next
        'If sStrStato <> "" Then
        '    sStrStato = " TAB_TIPO_RICHIESTA.ID IN (" & Mid(sStrStato, 1, Len(sStrStato) - 1) & ") "
        'End If
        'CaricaCriteri = CaricaCriteri & sStrStato
        'item = RadPanelBar1.Items(1)
        'Dim Pulsante As CheckBox = TryCast(item.Items(0).FindControl("chkRichiedente"), CheckBox)
        'If Pulsante.Checked = True Then
        '    If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
        '    CaricaCriteri = CaricaCriteri & " UPPER(COGNOME_RS||' '||NOME)<>REPLACE(SISCOM_MI.GETINTESTATARI(NVL(ID_CONTRATTO,0)),';','') "
        '    bTrovato = True
        'End If

        Dim item1 As RadPanelItem = RadPanelBar1.Items(0)
        'Dim cmbList As RadDropDownList = TryCast(item1.Items(0).FindControl("cmbPriorita"), RadDropDownList)
        'If cmbList.SelectedText <> "---" Then
        '    If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
        '    CaricaCriteri = CaricaCriteri & " APPALTI.ID IN (SELECT DISTINCT ID_APPALTO FROM SISCOM_MI.SLA_TEMPI WHERE ID_PRIORITA=" & cmbList.SelectedValue & ") "
        '    bTrovato = True
        'End If

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

        '//////////////////////////////
        '// 1433/2019
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
        '/////////////////////////////


    End Function

    Private Sub BindGrid()
        Dim s As String = ""
        Dim stringaFiltroAppalti As String = ""
        sStrAppalti = ""
        sStrAppalti = sStrAppaltiOperatori & CaricaCriteri()
        If sStrAppalti <> "" Then sStrAppalti = sStrAppalti & " AND "

        If iIndiceFornitore <> "0" Then
            s = " AND APPALTI.MODULO_FORNITORI_GE = 1 "
            stringaFiltroAppalti = " AND APPALTI.ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.OPERATORI_FO_APPALTI WHERE ID_OPERATORE = " & Session.Item("ID_OPERATORE") & ") "
        Else
            s = ""
        End If



        sStrSql = "SELECT * FROM (SELECT (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI_SOPRALLUOGO WHERE ID_SEGNALAZIONE=NVL(SEGNALAZIONI.ID,-1))>0 THEN 'SI' ELSE 'NO' END) AS RSP,PERICOLO_SEGNALAZIONI.DESCRIZIONE AS GRAVITA, " _
                & " TAB_STATI_SEGNALAZIONI_FO.DESCRIZIONE AS STATO, " _
                & " SEGNALAZIONI.ID AS IDS,SEGNALAZIONI_FORNITORI.ID AS IDENTIFICATIVO, " _
                & " (CASE  " _
                & " WHEN MANUTENZIONI.PROGR IS NULL THEN (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'yyyymmdd')) " _
                & " ELSE " _
                & " (SELECT MAX(TO_DATE (SUBSTR(EVENTI_MANUTENZIONE.DATA_ORA,1,8), 'yyyymmdd')) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE COD_EVENTO='F91' AND ID_MANUTENZIONE=MANUTENZIONI.ID) " _
                & " END) AS DATA_RICHIESTA, " _
                & " (CASE  " _
                & " WHEN MANUTENZIONI.PROGR IS NULL THEN (SUBSTR (DATA_ORA_RICHIESTA, 1, 8)) " _
                & " ELSE " _
                & " (SELECT MAX(SUBSTR(EVENTI_MANUTENZIONE.DATA_ORA,1,8)) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE COD_EVENTO='F91' AND ID_MANUTENZIONE=MANUTENZIONI.ID) " _
                & " END) AS DATA_RICHIESTA_FILTRO, " _
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
                & " END) AS INDIRIZZO,MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO AS ODL1,"
        If iIndiceFornitore = "0" Then
            sStrSql = sStrSql & "'<a href=''javascript:void(0);'' onclick=""window.open(''DettaglioOrdine.aspx?T=X&D='||MANUTENZIONI.PROGR||'_'||MANUTENZIONI.ANNO||''',''Intervento_'||MANUTENZIONI.ID||''','''');"">'||MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO||'</a>' AS ODL,"
        Else
            sStrSql = sStrSql & " MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO AS ODL, "
        End If
        sStrSql = sStrSql _
                & " '' AS RDO, " _
                & " (CASE  " _
                & " WHEN MANUTENZIONI.PROGR IS NULL THEN (SEGNALAZIONI.DESCRIZIONE_RIC) " _
                & " ELSE " _
                & " (MANUTENZIONI.DESCRIZIONE) " _
                & " END) AS DESCRIZIONE_ANOMALIA, " _
                & " SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE, " _
                & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
                & " APPALTI.NUM_REPERTORIO AS N_APPALTO, " _
                & " SEGNALAZIONI.ID_SEGNALAZIONE_PADRE, " _
                & "TO_DATE (SUBSTR(MANUTENZIONI.DATA_INIZIO_INTERVENTO,1,8), 'yyyymmdd') AS DATA_INIZIO_INTERVENTO," _
                & "TO_DATE (SUBSTR(MANUTENZIONI.DATA_FINE_INTERVENTO,1,8), 'yyyymmdd') AS DATA_FINE_INTERVENTO," _
                & "TO_DATE (SUBSTR(MANUTENZIONI.DATA_PGI,1,8), 'yyyymmdd') AS DATAPGI," _
                & "TO_DATE (SUBSTR(MANUTENZIONI.DATA_TDL,1,8), 'yyyymmdd') AS DATATDL, " _
                & "TO_DATE (SUBSTR(SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO,1,8), 'yyyymmdd') AS DATA_FINE_DITTA, " _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE, " _
                & "(CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI_FO_IRR WHERE VISIBILE=1 AND ID_SEGNALAZIONE=SEGNALAZIONI_FORNITORI.ID)>0 THEN 'SI' ELSE 'NO' END) AS IRREGOLARITA," _
                & " SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID) AS BUILDING_MANAGER, " _
                & " (SELECT TAB_FILIALI.NOME FROM SISCOM_MI.EDIFICI INNER JOIN SISCOM_MI.COMPLESSI_IMMOBILIARI ON EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID INNER JOIN SISCOM_MI.TAB_FILIALI ON COMPLESSI_IMMOBILIARI.ID_FILIALE = TAB_FILIALI.ID WHERE EDIFICI.ID = SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO) AS ST, " _
                & " NVL(   (SELECT WM_CONCAT(DISTINCT SEGNALAZIONI_FO_ALL_TIPI.DESCRIZIONE)  FROM SISCOM_MI.SEGNALAZIONI_FO_ALLEGATI,SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI, SISCOM_MI.SEGNALAZIONI_FORNITORI  F WHERE SEGNALAZIONI_FO_ALL_TIPI.ID(+)=SEGNALAZIONI_FO_ALLEGATI.ID_TIPO AND SEGNALAZIONI_FO_ALLEGATI.ID_SEGNALAZIONE=F.ID  and F.ID =SEGNALAZIONI_FORNITORI.ID  GROUP BY SEGNALAZIONI_FORNITORI.ID ) , 'NO') AS ALLEGATI " _
                & " FROM SISCOM_MI.SEGNALAZIONI, " _
                & " SISCOM_MI.PERICOLO_SEGNALAZIONI, " _
                & " SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO, " _
                & " SISCOM_MI.MANUTENZIONI, " _
                & " SISCOM_MI.APPALTI, " _
                & " SISCOM_MI.SEGNALAZIONI_FORNITORI," _
                & " SISCOM_MI.TAB_TIPO_RICHIESTA,SISCOM_MI.FORNITORI " _
                & " WHERE      " & sStrAppalti _
                & " FORNITORI.ID (+) = APPALTI.ID_FORNITORE " _
                & stringaFiltroAppalti _
                & " AND MANUTENZIONI.STATO Not IN (2,4,5,6) And id_Segnalazione_padre Is NULL And " _
                & " PERICOLO_SEGNALAZIONI.ID (+) = SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE " _
                & " And TAB_STATI_SEGNALAZIONI_FO.ID(+) = SEGNALAZIONI_FORNITORI.ID_STATO " _
                & " And MANUTENZIONI.ID(+) = SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE " _
                & " And APPALTI.MODULO_FORNITORI = 1 " & s _
                & " And APPALTI.ID (+) = SEGNALAZIONI_FORNITORI.ID_APPALTO " _
                & " And SEGNALAZIONI.ID(+) = SEGNALAZIONI_FORNITORI.ID_SEGNALAZIONE " _
                & " And TAB_TIPO_RICHIESTA.ID (+)=SEGNALAZIONI_FORNITORI.ID_TIPO_RICHIESTA) " _
                & sIntervalloDataRichiesta _
                & " ORDER BY 3 DESC"
    End Sub

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

    Public Property sIntervalloDataRichiesta() As String
        Get
            If Not (ViewState("par_sIntervalloDataRichiesta") Is Nothing) Then
                Return CStr(ViewState("par_sIntervalloDataRichiesta"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIntervalloDataRichiesta") = value
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

    Public Property sStrAppalti() As String
        Get
            If Not (ViewState("par_sStrAppalti") Is Nothing) Then
                Return CStr(ViewState("par_sStrAppalti"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrAppalti") = value
        End Set
    End Property

    Public Property sStrAppaltiOperatori() As String
        Get
            If Not (ViewState("par_sStrAppaltiOperatori") Is Nothing) Then
                Return CStr(ViewState("par_sStrAppaltiOperatori"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrAppaltiOperatori") = value
        End Set
    End Property

    Protected Sub dgvSegnalazioni_ColumnsReorder(sender As Object, e As Telerik.Web.UI.GridColumnsReorderEventArgs) Handles dgvSegnalazioni.ColumnsReorder

    End Sub

    Protected Sub dgvSegnalazioni_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles dgvSegnalazioni.DetailTableDataBind
        'Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        'Select Case e.DetailTableView.Name
        '    Case "Dettagli"
        '        If True Then
        '            Dim IndiceS As String = Replace(dataItem("IDS").Text, "&nbsp;", "-1")

        '            Dim sStrSql1 As String = "SELECT * FROM (SELECT PERICOLO_SEGNALAZIONI.DESCRIZIONE AS GRAVITA, " _
        '        & " TAB_STATI_SEGNALAZIONI_FO.DESCRIZIONE AS STATO, " _
        '        & " SEGNALAZIONI.ID AS IDS,segnalazioni_fornitori.id as IDENTIFICATIVO, " _
        '        & " (CASE  " _
        '        & " WHEN MANUTENZIONI.PROGR Is NULL THEN (TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'yyyymmdd'),'dd/mm/yyyy')) " _
        '        & " ELSE " _
        '        & " (SELECT MAX(TO_CHAR (TO_DATE (SUBSTR(EVENTI_MANUTENZIONE.DATA_ORA,1,8), 'yyyymmdd'),'dd/mm/yyyy')) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE COD_EVENTO='F91' AND ID_MANUTENZIONE=MANUTENZIONI.ID) " _
        '        & " END) AS DATA_RICHIESTA, " _
        '        & " (CASE  " _
        '        & " WHEN MANUTENZIONI.PROGR IS NULL THEN (SELECT COMPLESSI_IMMOBILIARI.COD_COMPLESSO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID(+) = SEGNALAZIONI.ID_COMPLESSO) " _
        '        & " ELSE " _
        '        & " (SELECT COMPLESSI_IMMOBILIARI.COD_COMPLESSO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID(+) = MANUTENZIONI.ID_COMPLESSO) " _
        '        & " END) AS COD_COMPLESSO, " _
        '        & " (CASE  " _
        '        & " WHEN MANUTENZIONI.PROGR IS NULL THEN (SELECT EDIFICI.COD_EDIFICIO FROM SISCOM_MI.EDIFICI WHERE ID(+) = SEGNALAZIONI.ID_EDIFICIO) " _
        '        & " ELSE " _
        '        & " (SELECT EDIFICI.COD_EDIFICIO FROM SISCOM_MI.EDIFICI WHERE ID(+) = MANUTENZIONI.ID_EDIFICIO) " _
        '        & " END) AS COD_EDIFICIO, " _
        '        & " " _
        '        & " (CASE " _
        '        & " WHEN MANUTENZIONI.PROGR IS NULL THEN ((CASE " _
        '        & "              WHEN SEGNALAZIONI.ID_EDIFICIO IS NOT NULL " _
        '        & " THEN " _
        '        & " (SELECT    INDIRIZZI.DESCRIZIONE " _
        '        & " || ' ' " _
        '        & " || INDIRIZZI.CIVICO " _
        '        & " || ' ' " _
        '        & " || INDIRIZZI.CAP " _
        '        & " || ' - ' " _
        '        & " || COMUNI_NAZIONI.NOME " _
        '        & " FROM SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI,SISCOM_MI.EDIFICI " _
        '        & " WHERE     COMUNI_NAZIONI.COD = INDIRIZZI.COD_COMUNE " _
        '        & " AND INDIRIZZI.ID = EDIFICI.ID_INDIRIZZO_PRINCIPALE AND INDIRIZZI.ID=SEGNALAZIONI.ID_EDIFICIO) " _
        '        & " ELSE " _
        '        & " (SELECT    INDIRIZZI.DESCRIZIONE " _
        '        & " || ' ' " _
        '        & " || INDIRIZZI.CIVICO " _
        '        & " || ' ' " _
        '        & " || INDIRIZZI.CAP " _
        '        & " || ' - ' " _
        '        & " || COMUNI_NAZIONI.NOME " _
        '        & " FROM SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI,SISCOM_MI.UNITA_IMMOBILIARI " _
        '        & " WHERE     COMUNI_NAZIONI.COD = INDIRIZZI.COD_COMUNE " _
        '        & " AND INDIRIZZI.ID = UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=SEGNALAZIONI.ID_UNITA) " _
        '        & " END)) " _
        '        & " ELSE " _
        '        & " ((CASE " _
        '        & "              WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL " _
        '        & " THEN " _
        '        & " (SELECT    INDIRIZZI.DESCRIZIONE " _
        '        & " || ' ' " _
        '        & " || INDIRIZZI.CIVICO " _
        '        & " || ' ' " _
        '        & " || INDIRIZZI.CAP " _
        '        & " || ' - ' " _
        '        & " || COMUNI_NAZIONI.NOME " _
        '        & " FROM SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI,SISCOM_MI.EDIFICI " _
        '        & " WHERE     COMUNI_NAZIONI.COD = INDIRIZZI.COD_COMUNE " _
        '        & " AND INDIRIZZI.ID = EDIFICI.ID_INDIRIZZO_PRINCIPALE AND EDIFICI.ID=MANUTENZIONI.ID_EDIFICIO) " _
        '        & " ELSE " _
        '        & " (SELECT    INDIRIZZI.DESCRIZIONE " _
        '        & " || ' ' " _
        '        & " || INDIRIZZI.CIVICO " _
        '        & " || ' ' " _
        '        & " || INDIRIZZI.CAP " _
        '        & " || ' - ' " _
        '        & " || COMUNI_NAZIONI.NOME " _
        '        & " FROM SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI,SISCOM_MI.COMPLESSI_IMMOBILIARI " _
        '        & " WHERE     COMUNI_NAZIONI.COD = INDIRIZZI.COD_COMUNE " _
        '        & " AND INDIRIZZI.ID = COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO AND COMPLESSI_IMMOBILIARI.ID=MANUTENZIONI.ID_COMPLESSO) " _
        '        & " END)) " _
        '        & " END) AS INDIRIZZO,         " _
        '        & " MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO AS ODL, " _
        '        & " '' AS RDO, " _
        '        & " (CASE  " _
        '        & " WHEN MANUTENZIONI.PROGR IS NULL THEN (SEGNALAZIONI.DESCRIZIONE_RIC) " _
        '        & " ELSE " _
        '        & " (MANUTENZIONI.DESCRIZIONE) " _
        '        & " END) AS DESCRIZIONE_ANOMALIA, " _
        '        & " SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE, " _
        '        & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
        '        & " APPALTI.NUM_REPERTORIO AS N_APPALTO, " _
        '        & " SEGNALAZIONI.ID_SEGNALAZIONE_PADRE " _
        '        & " FROM SISCOM_MI.SEGNALAZIONI, " _
        '        & " SISCOM_MI.PERICOLO_SEGNALAZIONI, " _
        '        & " SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO, " _
        '        & " SISCOM_MI.MANUTENZIONI, " _
        '        & " SISCOM_MI.APPALTI, " _
        '        & " SISCOM_MI.SEGNALAZIONI_FORNITORI, " _
        '        & " SISCOM_MI.TAB_TIPO_RICHIESTA " _
        '        & " WHERE      " & sStrAppalti _
        '        & " APPALTI.ID=MANUTENZIONI.ID_APPALTO AND NVL(id_Segnalazione_padre,-1)=" & IndiceS _
        '        & " AND PERICOLO_SEGNALAZIONI.ID (+) = SEGNALAZIONI.ID_PERICOLO_SEGNALAZIONE " _
        '        & " AND TAB_STATI_SEGNALAZIONI_FO.ID(+) = SEGNALAZIONI_FORNITORI.ID_STATO " _
        '        & " AND MANUTENZIONI.ID(+) = SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE " _
        '        & " AND APPALTI.MODULO_FORNITORI = 1 " _
        '        & " AND APPALTI.ID(+) = SEGNALAZIONI_FORNITORI.ID_APPALTO " _
        '        & " AND SEGNALAZIONI.ID(+) = SEGNALAZIONI_FORNITORI.ID_SEGNALAZIONE " _
        '        & " AND TAB_TIPO_RICHIESTA.ID (+)=SEGNALAZIONI_FORNITORI.ID_TIPO_RICHIESTA) " _
        '        & sIntervalloDataRichiesta _
        '        & " ORDER BY 3 DESC"


        '            '& " NVL(id_Segnalazione_padre,0)=" & IndiceS _
        '            e.DetailTableView.DataSource = par.getDataTableGrid(sStrSql1)

        '        End If
        'End Select
        'If dataItem("TIPO").Text = "1" Or dataItem("TIPO").Text = "6" Then
        '    Select Case dataItem("ID_PERICOLO_SEGNALAZIONE").Text
        '        Case "1"
        '            dataItem("GRAVITA").Controls.Clear()
        '            Dim img As Image = New Image()
        '            img.ImageUrl = "Immagini/Ball-white-128.png"
        '            dataItem("GRAVITA").Controls.Add(img)
        '        Case "2"
        '            dataItem("GRAVITA").Controls.Clear()
        '            Dim img As Image = New Image()
        '            img.ImageUrl = "Immagini/Ball-green-128.png"
        '            dataItem("GRAVITA").Controls.Add(img)
        '        Case "3"
        '            dataItem("GRAVITA").Controls.Clear()
        '            Dim img As Image = New Image()
        '            img.ImageUrl = "Immagini/Ball-yellow-128.png"
        '            dataItem("GRAVITA").Controls.Add(img)
        '        Case "4"
        '            dataItem("GRAVITA").Controls.Clear()
        '            Dim img As Image = New Image()
        '            img.ImageUrl = "Immagini/Ball-red-128.png"
        '            dataItem("GRAVITA").Controls.Add(img)
        '        Case "0"
        '            dataItem("GRAVITA").Controls.Clear()
        '            Dim img As Image = New Image()
        '            img.ImageUrl = "Immagini/Ball-blue-128.png"
        '            dataItem("GRAVITA").Controls.Add(img)
        '        Case Else
        '    End Select
        'End If
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

    Protected Sub dgvSegnalazioni_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvSegnalazioni.ItemCreated

    End Sub


    Protected Sub dgvSegnalazioni_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvSegnalazioni.ItemDataBound
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



        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            'If par.IfEmpty(isExporting.Value, "0") = "0" Then
            '    NumeroElementi = NumeroElementi + 1
            'End If
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In dgvSegnalazioni.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    If column.UniqueName <> "DATAPGI" And column.UniqueName <> "DATATDL" Then
                        If dataItem(column.UniqueName).Text <> "&nbsp;" Then
                            dataItem(column.UniqueName).ToolTip = Replace(dataItem(column.UniqueName).Text, "&nbsp;", "---")
                        End If
                    End If
                    If column.UniqueName = "ODL" Then
                        dataItem(column.UniqueName).ToolTip = "Visualizza dettagli ordine"
                    End If
                End If
            Next
            e.Item.Attributes.Add("onclick", "document.getElementById('idSegnalazione').value='" & dataItem("IDENTIFICATIVO").Text & "'")

            'If dataItem("TIPO").Text = "1" Or dataItem("TIPO").Text = "6" Then
            '    Select Case dataItem("ID_PERICOLO_SEGNALAZIONE").Text
            '        Case "1"
            '            dataItem("GRAVITA").Controls.Clear()
            '            Dim img As Image = New Image()
            '            img.ImageUrl = "Immagini/Ball-white-128.png"
            '            dataItem("GRAVITA").Controls.Add(img)
            '        Case "2"
            '            dataItem("GRAVITA").Controls.Clear()
            '            Dim img As Image = New Image()
            '            img.ImageUrl = "Immagini/Ball-green-128.png"
            '            dataItem("GRAVITA").Controls.Add(img)
            '        Case "3"
            '            dataItem("GRAVITA").Controls.Clear()
            '            Dim img As Image = New Image()
            '            img.ImageUrl = "Immagini/Ball-yellow-128.png"
            '            dataItem("GRAVITA").Controls.Add(img)
            '        Case "4"
            '            dataItem("GRAVITA").Controls.Clear()
            '            Dim img As Image = New Image()
            '            img.ImageUrl = "Immagini/Ball-red-128.png"
            '            dataItem("GRAVITA").Controls.Add(img)
            '        Case "0"
            '            dataItem("GRAVITA").Controls.Clear()
            '            Dim img As Image = New Image()
            '            img.ImageUrl = "Immagini/Ball-blue-128.png"
            '            dataItem("GRAVITA").Controls.Add(img)
            '        Case Else
            '    End Select
            'End If
        End If
        If TypeOf e.Item Is GridFilteringItem Then
            If e.Item.OwnerTableView.Name = "ElencoSegnalazioni" Then
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO ORDER BY ID ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStato"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
                par.caricaComboTelerik("SELECT DISTINCT ((CASE WHEN MANUTENZIONI.PROGR IS NULL THEN ((CASE WHEN SEGNALAZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT INDIRIZZI.DESCRIZIONE||' '|| INDIRIZZI.CIVICO||' '||INDIRIZZI.CAP||' - '||COMUNI_NAZIONI.NOME FROM SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI, SISCOM_MI.EDIFICI WHERE COMUNI_NAZIONI.COD =INDIRIZZI.COD_COMUNE AND INDIRIZZI.ID = EDIFICI.ID_INDIRIZZO_PRINCIPALE AND INDIRIZZI.ID =SEGNALAZIONI.ID_EDIFICIO)  ELSE (SELECT    INDIRIZZI.DESCRIZIONE|| ' '|| INDIRIZZI.CIVICO|| ' '|| INDIRIZZI.CAP|| ' - '|| COMUNI_NAZIONI.NOME FROM SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI,SISCOM_MI.UNITA_IMMOBILIARI WHERE  COMUNI_NAZIONI.COD = INDIRIZZI.COD_COMUNE AND INDIRIZZI.ID = UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID = SEGNALAZIONI.ID_UNITA) END)) ELSE ((CASE WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT INDIRIZZI.DESCRIZIONE|| ' '|| INDIRIZZI.CIVICO|| ' '|| INDIRIZZI.CAP|| ' - '|| COMUNI_NAZIONI.NOME FROM SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI, SISCOM_MI.EDIFICI WHERE COMUNI_NAZIONI.COD =INDIRIZZI.COD_COMUNE AND INDIRIZZI.ID =EDIFICI.ID_INDIRIZZO_PRINCIPALE AND EDIFICI.ID =MANUTENZIONI.ID_EDIFICIO) ELSE (SELECT    INDIRIZZI.DESCRIZIONE|| ' '|| INDIRIZZI.CIVICO|| ' '|| INDIRIZZI.CAP|| ' - '|| COMUNI_NAZIONI.NOME FROM SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMUNI_NAZIONI.COD =INDIRIZZI.COD_COMUNE AND INDIRIZZI.ID = COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO AND COMPLESSI_IMMOBILIARI.ID =MANUTENZIONI.ID_COMPLESSO) END)) END)) AS INDIRIZZO FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI,SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE id_Segnalazione_padre IS NULL AND MANUTENZIONI.ID(+) = SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE AND APPALTI.MODULO_FORNITORI = 1 AND APPALTI.MODULO_FORNITORI_GE = 1 AND APPALTI.ID(+) = SEGNALAZIONI_FORNITORI.ID_APPALTO AND SEGNALAZIONI.ID(+) =SEGNALAZIONI_FORNITORI.ID_SEGNALAZIONE ORDER BY INDIRIZZO ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroIndirizzo"), RadComboBox), "INDIRIZZO", "INDIRIZZO", True, "Tutti", "Tutti")
                If Not String.IsNullOrEmpty(Trim(HFFiltroStato.Value.ToString)) Then
                    TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStato"), RadComboBox).SelectedValue = HFFiltroStato.Value.ToString
                End If
                If Not String.IsNullOrEmpty(Trim(HFFiltroIndirizzo.Value.ToString)) Then
                    TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroIndirizzo"), RadComboBox).SelectedValue = HFFiltroIndirizzo.Value.ToString
                End If
                'If Not String.IsNullOrEmpty(Trim(HFFiltroSopr.Value.ToString)) Then
                '    TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroSopr"), RadComboBox).SelectedValue = HFFiltroSopr.Value.ToString
                'End If

                'TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStato"), RadComboBox).Enabled = False
            End If
        End If

        '//////////////////////
        '1433/2019
        If TypeOf e.Item Is GridDataItem Then
            Dim dataBoundItem As GridDataItem = TryCast(e.Item, GridDataItem)

            If dataBoundItem("DATAPGI").Text <> "" And dataBoundItem("DATAPGI").Text <> "&nbsp;" Then
                If CType(dataBoundItem("DATAPGI").Text, DateTime) > CType(dataBoundItem("DATA_INIZIO_INTERVENTO").Text, DateTime) Then
                    dataBoundItem("DATAPGI").ForeColor = Drawing.Color.Red
                    dataBoundItem("DATAPGI").Font.Bold = True
                Else
                    dataBoundItem("DATAPGI").ForeColor = Drawing.Color.Green
                    dataBoundItem("DATAPGI").Font.Bold = True
                End If
            End If

            If dataBoundItem("DATATDL").Text <> "" And dataBoundItem("DATATDL").Text <> "&nbsp;" Then
                If CType(dataBoundItem("DATATDL").Text, DateTime) > CType(dataBoundItem("DATA_FINE_INTERVENTO").Text, DateTime) Then
                    dataBoundItem("DATATDL").ForeColor = Drawing.Color.Red
                    dataBoundItem("DATATDL").Font.Bold = True
                Else
                    dataBoundItem("DATATDL").ForeColor = Drawing.Color.Green
                    dataBoundItem("DATATDL").Font.Bold = True
                End If
            End If
        End If
        '//////////////////////


    End Sub

    'Public Sub SalvaStruttura(sender As Object, e As System.EventArgs)

    'End Sub

    Protected Sub dgvSegnalazioni_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvSegnalazioni.NeedDataSource
        If sStrSql <> "" Then
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSql)
            'Dim dt As System.Data.DataTable = TryCast(sender, RadGrid).DataSource
            'NumeroElementi = dt.Rows.Count
        End If
    End Sub

    Public Sub HideExpandColumnRecursive(ByVal tableView As GridTableView)
        Dim nestedViewItems As GridItem() = tableView.GetItems(GridItemType.NestedView)
        For Each nestedViewItem As GridNestedViewItem In nestedViewItems
            For Each nestedView As GridTableView In nestedViewItem.NestedTableViews
                Dim cell As TableCell = nestedView.ParentItem("ExpandColumn")
                If nestedView.Items.Count = 0 Then
                    If cell.Controls.Count > 0 Then
                        cell.Controls(0).Visible = False
                        cell.Text = "&nbsp"
                        nestedViewItem.Visible = False
                    End If
                End If
                If nestedView.HasDetailTables Then
                    HideExpandColumnRecursive(nestedView)
                End If
            Next
        Next
    End Sub

    Protected Sub dgvSegnalazioni_PreRender(sender As Object, e As System.EventArgs) Handles dgvSegnalazioni.PreRender
        'HideExpandColumnRecursive(dgvSegnalazioni.MasterTableView)
        If Not Page.IsPostBack Then
            If dgvSegnalazioni.Items.Count > 0 Then
                dgvSegnalazioni.MasterTableView.Items(0).Expanded = False
            End If
        End If
        If isExporting.Value = "1" Then
            For Each Filter As GridFilteringItem In dgvSegnalazioni.MasterTableView.GetItems(GridItemType.FilteringItem)
                Filter.Visible = False
            Next
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Protected Sub RadPanelBar1_PreRender(sender As Object, e As System.EventArgs) Handles RadPanelBar1.PreRender
        Dim selectedItem As RadPanelItem = RadPanelBar1.SelectedItem
        If Not (selectedItem Is Nothing) Then

        End If
    End Sub

    Protected Sub btnAvviaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnAvviaRicerca.Click

        BindGrid()
        dgvSegnalazioni.Rebind()

    End Sub

    Protected Sub btnAzzeraFiltri_Click(sender As Object, e As System.EventArgs) Handles btnAzzeraFiltri.Click
        Dim i As Integer = 0
        Dim item As RadPanelItem = RadPanelBar1.Items(0)
        Dim CheckList As CheckBoxList = TryCast(item.Items(0).FindControl("CheckBoxListStato"), CheckBoxList)

        For i = 0 To CheckList.Items.Count - 1
            CheckList.Items(i).Selected = True
        Next

        Dim ControlloData As RadDatePicker = TryCast(item.Items(0).FindControl("txtRicDA"), RadDatePicker)
        ControlloData.Clear()

        ControlloData = TryCast(item.Items(0).FindControl("txtRicA"), RadDatePicker)
        ControlloData.Clear()

        'Dim ControlloTesto As RadTextBox = TryCast(item.Items(0).FindControl("txtNumIntDa"), RadTextBox)
        'ControlloTesto.Text = ""

        'ControlloTesto = TryCast(item.Items(0).FindControl("txtNumIntA"), RadTextBox)
        'ControlloTesto.Text = ""

        'filtri avanzati
        'item = RadPanelBar1.Items(1)
        'CheckList = TryCast(item.Items(0).FindControl("CheckBoxListTipoR"), CheckBoxList)
        'For i = 0 To CheckList.Items.Count - 1
        '    CheckList.Items(i).Selected = True
        'Next

        'Dim Pulsante As CheckBox = TryCast(item.Items(0).FindControl("chkRichiedente"), CheckBox)
        'Pulsante.Checked = False

        ControlloData = TryCast(item.Items(0).FindControl("txtPGIDA"), RadDatePicker)
        ControlloData.Clear()

        ControlloData = TryCast(item.Items(0).FindControl("txtPGIA"), RadDatePicker)
        ControlloData.Clear()

        ControlloData = TryCast(item.Items(0).FindControl("txtTDLDA"), RadDatePicker)
        ControlloData.Clear()

        ControlloData = TryCast(item.Items(0).FindControl("txtTDLA"), RadDatePicker)
        ControlloData.Clear()

        ControlloData = TryCast(item.Items(0).FindControl("txtFineLavoriA"), RadDatePicker)
        ControlloData.Clear()
        ControlloData = TryCast(item.Items(0).FindControl("txtFineLavoriDA"), RadDatePicker)
        ControlloData.Clear()

        Dim item1 As RadPanelItem = RadPanelBar1.Items(0)
        'Dim cmbList As RadDropDownList = TryCast(item1.Items(0).FindControl("cmbPriorita"), RadDropDownList)
        'cmbList.SelectedText = "---"

        If iIndiceFornitore = "0" Then
            item = RadPanelBar1.Items(0)
            Dim cmbListF As RadComboBox = TryCast(item.Items(0).FindControl("cmbFornitori"), RadComboBox)
            cmbListF.Text = "---"
        End If

        Dim item2 As RadPanelItem = RadPanelBar1.Items(0)
        Dim cmbList1 As RadDropDownList = TryCast(item2.Items(0).FindControl("cmbGravita"), RadDropDownList)
        cmbList1.SelectedText = "---"

        BindGrid()
        dgvSegnalazioni.Rebind()
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

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        'SessionStorageProvider.StorageProviderKey = Session.Item("ID_OPERATORE")
        'RadPersistenceManager1.SaveState()
        'Session.Add("StorageProviderKey", Session.Item("ID_OPERATORE"))
        'dgvSegnalazioni.Rebind()

        If dgvSegnalazioni.Items.Count > 0 Then
            isExporting.Value = "1"
            Dim context As RadProgressContext = RadProgressContext.Current
            context.SecondaryTotal = 0
            context.SecondaryValue = 0
            context.SecondaryPercent = 0

            dgvSegnalazioni.ExportSettings.Excel.FileExtension = "xls"
            dgvSegnalazioni.ExportSettings.Excel.Format = GridExcelExportFormat.Biff
            dgvSegnalazioni.ExportSettings.IgnorePaging = True
            dgvSegnalazioni.PageSize = NumeroElementi
            dgvSegnalazioni.Rebind()
            dgvSegnalazioni.ExportSettings.ExportOnlyData = False
            dgvSegnalazioni.ExportSettings.OpenInNewWindow = True

            ''//////////////////////////
            ''// copy Style
            'For Each item As GridDataItem In dgvSegnalazioni.Items
            '    'item("CompanyName").Font.Name = "Calibri"
            '    'item("CompanyName").Style("font-size") = "8pt"
            '    'item("CompanyName").Style("background-color") = "#FFF"
            '    'item("CompanyName").Style("vertical-align") = "middle"
            '    'item.Font.Name = "Calibri"
            '    item("DATAPGI").ForeColor = Drawing.Color.Red
            'Next
            ''//////////////////////////

            dgvSegnalazioni.MasterTableView.ExportToExcel()

        Else
            VisualizzaAlert("Nessun elemento da esportare", 2)
        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        CaricaProfilo()
        dgvSegnalazioni.Rebind()
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

    'Protected Sub btnSalvaStr_Click(sender As Object, e As System.EventArgs) Handles btnSalvaStr.Click
    '    SessionStorageProvider.StorageProviderKey = Session.Item("ID_OPERATORE")
    '    RadPersistenceManager1.SaveState()
    '    Session.Add("StorageProviderKey", Session.Item("ID_OPERATORE"))
    '    'dgvSegnalazioni.Rebind()
    'End Sub
End Class

Public Class SessionStorageProvider
    Implements IStateStorageProvider
    Private session As System.Web.SessionState.HttpSessionState = HttpContext.Current.Session
    Shared storageKey As String
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Public Shared WriteOnly Property StorageProviderKey() As String
        Set(value As String)
            storageKey = value
        End Set
    End Property

    Public Sub SaveStateToStorage(key As String, serializedState As String) Implements IStateStorageProvider.SaveStateToStorage
        ''session(storageKey) = serializedState
        'Dim cookie As HttpCookie = New HttpCookie("StorageProviderKey")
        'cookie.Value = serializedState
        'cookie.Expires = DateTime.Now.AddDays(100)
        'HttpContext.Current.Response.SetCookie(cookie)

        'par.OracleConn.Open()
        'par.SettaCommand(par)

        'par.cmd.CommandText = "select * from operatori_fornitori where id_operatore=" & session.Item("id_operatore")
        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        'If myReader.Read Then
        '    par.cmd.CommandText = "delete from operatori_fornitori where id_operatore=" & session.Item("id_operatore")
        '    par.cmd.ExecuteNonQuery()
        '    par.cmd.CommandText = "insert into operatori_fornitori values (" & session.Item("id_operatore") & ",:TEXT_DATA)"
        '    Dim paramData As New Oracle.DataAccess.Client.OracleParameter
        '    With paramData
        '        .Direction = Data.ParameterDirection.Input
        '        .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Clob
        '        .ParameterName = "TEXT_DATA"
        '        .Value = par.PulisciStrSql(serializedState)
        '    End With
        '    par.cmd.Parameters.Add(paramData)
        '    par.cmd.ExecuteNonQuery()
        '    paramData = Nothing
        'Else
        '    par.cmd.CommandText = "insert into operatori_fornitori values (" & session.Item("id_operatore") & ",:TEXT_DATA)"
        '    Dim paramData As New Oracle.DataAccess.Client.OracleParameter
        '    With paramData
        '        .Direction = Data.ParameterDirection.Input
        '        .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Clob
        '        .ParameterName = "TEXT_DATA"
        '        .Value = par.PulisciStrSql(serializedState)
        '    End With
        '    par.cmd.Parameters.Add(paramData)
        '    par.cmd.ExecuteNonQuery()
        '    paramData = Nothing
        'End If
        'myReader.Close()
        'par.OracleConn.Close()
    End Sub

    Public Function LoadStateFromStorage(key As String) As String Implements IStateStorageProvider.LoadStateFromStorage
        'par.OracleConn.Open()
        'par.SettaCommand(par)

        'par.cmd.CommandText = "select struttura from operatori_fornitori where id_operatore=" & session.Item("id_operatore")
        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        'If myReader.Read Then
        '    Dim blob As Oracle.DataAccess.Types.OracleClob = myReader.GetOracleClob(0)
        '    If blob.IsNull = False Then
        '        If blob.Value() <> "" Then
        '            Return blob.Value().ToString
        '        End If
        '    End If
        '    blob.Close()
        'End If
        'myReader.Close()

        'par.OracleConn.Close()

        ''If IsNothing(HttpContext.Current.Request.Cookies("StorageProviderKey")) = False Then
        ''    Return HttpContext.Current.Request.Cookies("StorageProviderKey").Value
        ''End If
    End Function
End Class


