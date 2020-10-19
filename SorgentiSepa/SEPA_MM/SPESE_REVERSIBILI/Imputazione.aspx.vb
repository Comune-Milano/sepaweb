Imports Telerik.Web.UI
Partial Class SPESE_REVERSIBILI_Imputazione
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Public Property TotaleEdificio() As String
        Get
            If Not (ViewState("par_TotaleEdificio") Is Nothing) Then
                Return CStr(ViewState("par_TotaleEdificio"))
            Else
                Return "0"
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_TotaleEdificio") = value
        End Set
    End Property
    Public Property TotAnnuoScontato() As String
        Get
            If Not (ViewState("par_TotAnnuoScontato") Is Nothing) Then
                Return CStr(ViewState("par_TotAnnuoScontato"))
            Else
                Return "0"
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_TotAnnuoScontato") = value
        End Set
    End Property
    Public Property TotAnnuoScontatoRett() As String
        Get
            If Not (ViewState("par_TotAnnuoScontatoRett") Is Nothing) Then
                Return CStr(ViewState("par_TotAnnuoScontatoRett"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_TotAnnuoScontatoRette") = value
        End Set

    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If controlloProfilo() Then
            If Not IsPostBack Then
                Page.Title = "Imputazione pulizie"
                CType(Master.FindControl("lblTitoloModulo"), Label).Text = ""

                If Not String.IsNullOrEmpty(Request.QueryString("HiddenIdContratto")) Then
                    CType(Master, Object).NascondiMenu()
                    HiddenContratto.Value = Request.QueryString("HiddenIdContratto")
                    HiddenEsercizio.Value = Request.QueryString("HIDDENPIANO")
                End If
                If Not String.IsNullOrEmpty(Request.QueryString("IDPRENOTAZIONE")) Then
                    CType(Master, Object).NascondiMenu()
                    HiddenPrenotazione.Value = Request.QueryString("IDPRENOTAZIONE")
                    HiddenEsercizio.Value = Request.QueryString("HIDDENPIANO")
                End If
                CType(Master.FindControl("TitoloMaster"), Label).Text = "Schede di imputazione pulizie"
                CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
                connData.apri()
                CaricaGridComplessi()
                CaricaGridEdifici()
                CaricaGridTotale05()
                CaricaGridTotale06()
                CaricaGridTotale07()
                CaricaGridTotale15()

                CaricaGridTotale()
                CaricaAttributi()

                connData.chiudi()
                HFElencoGriglie.Value = dgvComplessi.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvEdifici.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvTotale05.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvTotale06.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvTotale07.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvTotale15.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvTotale.ClientID.ToString.Replace("ctl00", "MasterPage")
            End If
        End If
    End Sub
    Private Sub CaricaGridTotale()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            If Not String.IsNullOrEmpty(HiddenContratto.Value) Then
                par.cmd.CommandText = "SELECT ID,(SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO,  " _
                                    & " (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO,  " _
                                    & "  (SELECT ID_FORNITORE FROM SISCOM_MI.appalti WHERE appalti.id=appalti_lotti_patrimonio.id_appalto) AS ID_FORNITORE, " _
                                    & " COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, REPLACE (MQ_ESTERNI, ',', '.') AS mq_esterni, REPLACE (MQ_PILOTY, ',', '.') AS MQ_PILOTY, NUMERO_BIDONI_CARTA, " _
                                    & " NUMERO_BIDONI_VETRO,NUMERO_BIDONI_UMIDO,0 AS IMPORTO_EDIFICIO, 0 AS IMPORTO_SCALA, " _
                                    & " (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO, " _
                                    & " (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, " _
                                    & " 0 AS TOTALE_EDIFICIO, (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO, " _
                                    & " (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, 0 AS TOT_ANNUO_SCONTATO, " _
                                    & " '' AS TOTALE_LORDO " _
                                    & " FROM SISCOM_MI.EDIFICI_tmp, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                    & " WHERE     EDIFICI_tmp.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                                    & " AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO = " & HiddenContratto.Value _
                                    & " AND ID <> 1 " _
                                    & " AND EDIFICI_TMP.FL_IN_CONDOMINIO = 0 " _
                                    & " and edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value _
                                    & " ORDER BY EDIFICI_TMP.DENOMINAZIONE ASC "
            End If

            Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Dim dt05 As Data.DataTable = Session.Item("DT_TOTALE_05")
            Dim dt06 As Data.DataTable = Session.Item("DT_TOTALE_06")
            Dim dt07 As Data.DataTable = Session.Item("DT_TOTALE_07")
            Dim dt15 As Data.DataTable = Session.Item("DT_TOTALE_15")
            Dim i As Integer = 0
            Dim row05 As Data.DataRow
            Dim row06 As Data.DataRow
            Dim row07 As Data.DataRow
            Dim row15 As Data.DataRow
            For Each riga As Data.DataRow In DT.Rows
                row05 = dt05.Select("ID=" & riga.Item("ID") & " AND ID_GRUPPO=" & riga.Item("ID_GRUPPO"))(0)
                row06 = dt06.Select("ID=" & riga.Item("ID") & " AND ID_GRUPPO=" & riga.Item("ID_GRUPPO"))(0)
                row07 = dt07.Select("ID=" & riga.Item("ID") & " AND ID_GRUPPO=" & riga.Item("ID_GRUPPO"))(0)
                row15 = dt15.Select("ID=" & riga.Item("ID") & " AND ID_GRUPPO=" & riga.Item("ID_GRUPPO"))(0)
                riga.Item("IMPORTO_EDIFICIO") = CDec(par.IfEmpty(row05.Item("IMPORTO_EDIFICIO").ToString, 0)) + CDec(par.IfEmpty(row06.Item("IMPORTO_EDIFICIO").ToString, 0)) + CDec(par.IfEmpty(row07.Item("IMPORTO_EDIFICIO").ToString, 0)) + CDec(par.IfEmpty(row15.Item("IMPORTO_EDIFICIO").ToString, 0))
                riga.Item("IMPORTO_SCALA") = CDec(par.IfEmpty(row05.Item("IMPORTO_SCALA").ToString, 0)) + CDec(par.IfEmpty(row06.Item("IMPORTO_SCALA").ToString, 0)) + CDec(par.IfEmpty(row07.Item("IMPORTO_SCALA").ToString, 0)) + CDec(par.IfEmpty(row15.Item("IMPORTO_SCALA").ToString, 0))
                riga.Item("TOTALE_EDIFICIO") = CDec(par.IfEmpty(row05.Item("TOTALE_EDIFICIO").ToString, 0)) + CDec(par.IfEmpty(row06.Item("TOTALE_EDIFICIO").ToString, 0)) + CDec(par.IfEmpty(row07.Item("TOTALE_EDIFICIO").ToString, 0)) + CDec(par.IfEmpty(row15.Item("TOTALE_EDIFICIO").ToString, 0))
                riga.Item("TOT_ANNUO_SCONTATO") = CDec(par.IfEmpty(row05.Item("TOT_ANNUO_SCONTATO").ToString, 0)) + CDec(par.IfEmpty(row06.Item("TOT_ANNUO_SCONTATO").ToString, 0)) + CDec(par.IfEmpty(row07.Item("TOT_ANNUO_SCONTATO").ToString, 0)) + CDec(par.IfEmpty(row15.Item("TOT_ANNUO_SCONTATO").ToString, 0))
            Next
            Session.Item("DT_TOTALE") = DT
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: CaricaGridTotale - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Private Function controlloProfilo() As Boolean
        'CONTROLLO DELLA SESSIONE OPERATORE E DELL'ABILITAZIONE ALLE SPESE REVERSIBILI
        If Session.Item("OPERATORE") <> "" Then
            If Session.Item("fl_spese_reversibili") = "0" Then
                Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Operatore non abilitato alla gestione delle spese reversibili")
                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
                Return False
            End If
        Else
            RadWindowManager1.RadAlert("Accesso negato o sessione scaduta! E\' necessario rieseguire il login!", 300, 150, "Attenzione", "", "null")
            Return False
        End If
        If String.IsNullOrEmpty(Request.QueryString("HiddenIdContratto")) Then
            If Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = 0 Then
                RadWindowManager1.RadAlert("E\' necessario selezionare almeno una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
                Return False
            End If
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub solaLettura()

    End Sub
    Protected Sub EsportaComplessi_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_COMPLESSI")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_IMP_PULIZIE_COMP", "SCHEDA_IMP_PULIZIE_COMP", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvComplessi.Rebind()
    End Sub

    Protected Sub RefreshComplessi_Click(sender As Object, e As System.EventArgs)
        AggiornaValoriComplessi()
        dgvComplessi.Rebind()
    End Sub

    Protected Sub dgvComplessi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvComplessi.NeedDataSource
        Try
            connData.apri(False)
            Dim DT As Data.DataTable = Session.Item("DT_COMPLESSI")
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: dgvEdifici_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli edifici!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Protected Sub btnSalvaComplessi_Click(sender As Object, e As System.EventArgs) Handles btnSalvaComplessi.Click
        Try
            Dim dt As Data.DataTable = Session.Item("DT_COMPLESSI")
            AggiornaValoriComplessi()
            connData.apri()
            For Each riga As Data.DataRow In dt.Rows
                par.cmd.CommandText = "UPDATE SISCOM_MI.EDIFICI_TMP " _
                                    & " SET    NUMERO_BIDONI_UMIDO        = " & par.IfNull(riga.Item("NUMERO_BIDONI_UMIDO"), 0) & ", " _
                                    & "        NUMERO_BIDONI_VETRO        = " & par.IfNull(riga.Item("NUMERO_BIDONI_VETRO"), 0) & ", " _
                                    & "        NUMERO_BIDONI_CARTA        = " & par.IfNull(riga.Item("NUMERO_BIDONI_CARTA"), 0) & ", " _
                                    & "        MQ_PILOTY                  = " & par.VirgoleInPunti(par.IfNull(riga.Item("MQ_PILOTY"), 0)) & ", " _
                                    & "        MQ_ESTERNI                 = " & par.VirgoleInPunti(par.IfNull(riga.Item("MQ_ESTERNI"), 0)) _
                                    & " WHERE  ID                         = " & riga.Item("ID") _
                                    & " AND    ID_PRENOTAZIONE            = " & riga.Item("ID_PRENOTAZIONE")
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "UPDATE SISCOM_MI.EDIFICI " _
                                   & " SET    NUMERO_BIDONI_UMIDO        = " & par.IfNull(riga.Item("NUMERO_BIDONI_UMIDO"), 0) & ", " _
                                   & "        NUMERO_BIDONI_VETRO        = " & par.IfNull(riga.Item("NUMERO_BIDONI_VETRO"), 0) & ", " _
                                   & "        NUMERO_BIDONI_CARTA        = " & par.IfNull(riga.Item("NUMERO_BIDONI_CARTA"), 0) & ", " _
                                   & "        MQ_PILOTY                  = " & par.VirgoleInPunti(par.IfNull(riga.Item("MQ_PILOTY"), 0)) & ", " _
                                   & "        MQ_ESTERNI                 = " & par.VirgoleInPunti(par.IfNull(riga.Item("MQ_ESTERNI"), 0)) _
                                   & " WHERE  ID                         = " & riga.Item("ID")
                par.cmd.ExecuteNonQuery()

            Next
            RadNotificationNote.Text = "Operazione completata!"
            RadNotificationNote.Show()
            connData.chiudi(True)
            CaricaGridComplessi()
            dgvComplessi.Rebind()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnSalvaComplessi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli edifici!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Protected Sub dgvComplessi_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles dgvComplessi.PageIndexChanged
        AggiornaValoriComplessi()
        dgvComplessi.CurrentPageIndex = e.NewPageIndex
    End Sub

    Private Sub CaricaGridComplessi()
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If
        Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
        par.cmd.CommandText = "select id from siscom_mi.pf_main where id_esercizio_finanziario = " & HiddenEsercizio.Value
        Dim idpiano As Integer = par.cmd.ExecuteScalar
        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.EDIFICI_TMP WHERE ID_PRENOTAZIONE=" & HiddenPrenotazione.Value
        Dim numero As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If numero = 0 Then
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EDIFICI_TMP " _
                                & "(" _
                                & " ID, COD_EDIFICIO, COD_EDIFICIO_GIMI, " _
                                & "    DENOMINAZIONE, ID_COMPLESSO, NUM_PIANI_ENTRO,  " _
                                & "    NUM_PIANI_FUORI, NUM_SCALE, ID_INDIRIZZO_PRINCIPALE,  " _
                                & "    DATA_COSTRUZIONE, DATA_RISTRUTTURAZIONE, COD_TIPOLOGIA_EDIFICIO,  " _
                                & "    COD_UTILIZZO_PRINCIPALE, COD_TIPOLOGIA_COSTRUTTIVA, COD_LIVELLO_POSSESSO,  " _
                                & "    CONDOMINIO, COD_TIPOLOGIA_IMP_RISCALD, SINTESI_EDIFICIO,  " _
                                & "    PIANO_TERRA, SEMINTERRATO, SOTTOTETTO,  " _
                                & "    ATTICO, SUPERATTICO, NUM_MEZZANINI,  " _
                                & "    ID_OPERATORE_INSERIMENTO, ID_OPERATORE_AGGIORNAMENTO, SEZIONE,  " _
                                & "    FOGLIO, NUMERO, COD_COMUNE,  " _
                                & "    NUM_ASCENSORI, FL_PIANO_VENDITA, GEST_RISC_DIR,  " _
                                & "    NOTE, ID_TIPOLOGIA_STRUTTURA, TIPOLOGIA_EDILIZIA_1,  " _
                                & "    TIPOLOGIA_EDILIZIA_2, TIPOLOGIA_EDILIZIA_3, ID_MANCATO_RILIEVO,  " _
                                & "    ID_TIPOLOGIA_EDILE_1, ID_TIPOLOGIA_EDILE_2, ID_TIPOLOGIA_EDILE_3,  " _
                                & "    NUM_PASSI_CARRABILI, DATA_RILASCIO_CPI, DATA_SCADENZA_CPI,  " _
                                & "    SCONTO_COSTO_BASE, ID_ZONA, ID_MICROZONA,  " _
                                & "    COD_RIF_LEG, ID_BM, GESTIONE_EDIFICIO,  " _
                                & "    TIPO_UBICAZIONE_EDIFICIO, UNITA_NON_PROPRIETA, FL_PREVENTIVI,  " _
                                & "    FL_IN_CONDOMINIO, MQ_ESTERNI, MQ_PILOTY,  " _
                                & "    NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO,  " _
                                & "    FL_SPAZI_ESTERNI, ID_PRENOTAZIONE )" _
                                & " (SELECT " _
                                & " ID, COD_EDIFICIO, COD_EDIFICIO_GIMI, " _
                                & "    DENOMINAZIONE, ID_COMPLESSO, NUM_PIANI_ENTRO,  " _
                                & "    NUM_PIANI_FUORI, NUM_SCALE, ID_INDIRIZZO_PRINCIPALE,  " _
                                & "    DATA_COSTRUZIONE, DATA_RISTRUTTURAZIONE, COD_TIPOLOGIA_EDIFICIO,  " _
                                & "    COD_UTILIZZO_PRINCIPALE, COD_TIPOLOGIA_COSTRUTTIVA, COD_LIVELLO_POSSESSO,  " _
                                & "    CONDOMINIO, COD_TIPOLOGIA_IMP_RISCALD, SINTESI_EDIFICIO,  " _
                                & "    PIANO_TERRA, SEMINTERRATO, SOTTOTETTO,  " _
                                & "    ATTICO, SUPERATTICO, NUM_MEZZANINI,  " _
                                & "    ID_OPERATORE_INSERIMENTO, ID_OPERATORE_AGGIORNAMENTO, SEZIONE,  " _
                                & "    FOGLIO, NUMERO, COD_COMUNE,  " _
                                & "    NUM_ASCENSORI, FL_PIANO_VENDITA, GEST_RISC_DIR,  " _
                                & "    NOTE, ID_TIPOLOGIA_STRUTTURA, TIPOLOGIA_EDILIZIA_1,  " _
                                & "    TIPOLOGIA_EDILIZIA_2, TIPOLOGIA_EDILIZIA_3, ID_MANCATO_RILIEVO,  " _
                                & "    ID_TIPOLOGIA_EDILE_1, ID_TIPOLOGIA_EDILE_2, ID_TIPOLOGIA_EDILE_3,  " _
                                & "    NUM_PASSI_CARRABILI, DATA_RILASCIO_CPI, DATA_SCADENZA_CPI,  " _
                                & "    SCONTO_COSTO_BASE, ID_ZONA, ID_MICROZONA,  " _
                                & "    COD_RIF_LEG, ID_BM, GESTIONE_EDIFICIO,  " _
                                & "    TIPO_UBICAZIONE_EDIFICIO, UNITA_NON_PROPRIETA, FL_PREVENTIVI,  " _
                                & "    FL_IN_CONDOMINIO, MQ_ESTERNI, MQ_PILOTY,  " _
                                & "    NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO,  " _
                                & "    FL_SPAZI_ESTERNI " _
                                & "," & HiddenPrenotazione.Value & " FROM SISCOM_MI.EDIFICI)"
            par.cmd.ExecuteNonQuery()
        End If
        Dim filtroContratti As String = ""
        If Not String.IsNullOrEmpty(HiddenContratto.Value) Then
            filtroContratti = "And id in (select distinct id_edificio from siscom_mi.appalti_lotti_patrimonio where id_appalto = " & HiddenContratto.Value & ")"
        End If
        par.cmd.CommandText = "SELECT ID_PRENOTAZIONE,ID, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, " _
                                & "(SELECT COUNT(ID) FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI_TMP.ID AND COD_TIPOLOGIA = 'AL') AS NUM_UNITA, " _
                                & "replace(MQ_ESTERNI,',','.') as mq_esterni,replace(MQ_PILOTY,',','.') as MQ_PILOTY, " _
                                & " NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, " _
                                & " (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI_TMP.ID_COMPLESSO)) AS SEDE_TERRITORIALE," _
                                & " FL_SPAZI_ESTERNI*NVL(MQ_ESTERNI,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU2," _
                                & " FL_SPAZI_ESTERNI*NVL(MQ_PILOTY,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU3, " _
                                & " (NVL(NUMERO_BIDONI_VETRO,0)+NVL(NUMERO_BIDONI_CARTA,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA=1 AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU9A, " _
                                & " 2*(NVL(NUMERO_BIDONI_UMIDO,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA=2 AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU9B " _
                                & " FROM SISCOM_MI.EDIFICI_TMP where EDIFICI_TMP.ID_PRENOTAZIONE=" & HiddenPrenotazione.Value & " AND id <> 1 " & filtroContratti & " ORDER BY EDIFICI_TMP.DENOMINAZIONE ASC"
        Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Session.Item("DT_COMPLESSI") = DT
        If connAperta = True Then
            connData.chiudi(False)
        End If
    End Sub

    Private Sub AggiornaValoriComplessi()
        Try
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("DT_COMPLESSI"), Data.DataTable)
            connData.apri()
            Dim row As Data.DataRow
            For Each item As GridDataItem In dgvComplessi.Items
                Dim mqEsterni As String = CType(item.FindControl("txtMqEsterni"), RadNumericTextBox).Text
                Dim mqPiloty As String = CType(item.FindControl("txtMqPiloty"), RadNumericTextBox).Text
                Dim numBidoniCarta As String = CType(item.FindControl("txtNumBidoniCarta"), RadNumericTextBox).Text
                Dim numBidoniVetro As String = CType(item.FindControl("txtNumBidoniVetro"), RadNumericTextBox).Text
                Dim numBidoniUmido As String = CType(item.FindControl("txtNumBidoniUmido"), RadNumericTextBox).Text
                row = dt.Select("id = " & item("ID").Text)(0)
                If row.Item("NUMERO_BIDONI_UMIDO") <> numBidoniUmido Then
                    'SALVATAGGIO EVENTO NELL' EDIFICIO
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & " VALUES ( " & item("ID").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F303' ,'Modifica valore ''NUMERO BIDONI UMIDO'' da  " & par.IfEmpty(par.PulisciStrSql(row.Item("NUMERO_BIDONI_UMIDO")), "- - -") & "  a  " & par.PulisciStrSql(numBidoniUmido) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                row.Item("NUMERO_BIDONI_UMIDO") = par.IfEmpty(numBidoniUmido, 0)

                If row.Item("NUMERO_BIDONI_VETRO") <> numBidoniVetro Then
                    'SALVATAGGIO EVENTO NELL' EDIFICIO
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & " VALUES ( " & item("ID").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F303' ,'Modifica valore ''NUMERO BIDONI VETRO'' da  " & par.IfEmpty(par.PulisciStrSql(row.Item("NUMERO_BIDONI_VETRO")), "- - -") & "  a  " & par.PulisciStrSql(numBidoniVetro) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                row.Item("NUMERO_BIDONI_VETRO") = par.IfEmpty(numBidoniVetro, 0)

                If row.Item("NUMERO_BIDONI_CARTA") <> numBidoniCarta Then
                    'SALVATAGGIO EVENTO NELL' EDIFICIO
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & " VALUES ( " & item("ID").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F303' ,'Modifica valore ''NUMERO BIDONI CARTA'' da  " & par.IfEmpty(par.PulisciStrSql(row.Item("NUMERO_BIDONI_CARTA")), "- - -") & "  a  " & par.PulisciStrSql(numBidoniCarta) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                row.Item("NUMERO_BIDONI_CARTA") = par.IfEmpty(numBidoniCarta, 0)

                If row.Item("MQ_PILOTY") <> mqPiloty Then
                    'SALVATAGGIO EVENTO NELL' EDIFICIO
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & " VALUES ( " & item("ID").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F303' ,'Modifica valore ''MQ PILOTY'' da  " & par.IfEmpty(par.PulisciStrSql(row.Item("MQ_PILOTY")), "- - -") & "  a  " & par.PulisciStrSql(mqPiloty) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                row.Item("MQ_PILOTY") = par.IfEmpty(mqPiloty, 0)

                If row.Item("MQ_ESTERNI") <> mqEsterni Then
                    'SALVATAGGIO EVENTO NELL' EDIFICIO
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & " VALUES ( " & item("ID").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F303' ,'Modifica valore ''MQ ESTERNI'' da  " & par.IfEmpty(par.PulisciStrSql(row.Item("MQ_ESTERNI")), "- - -") & "  a  " & par.PulisciStrSql(mqEsterni) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                row.Item("MQ_ESTERNI") = par.IfEmpty(mqEsterni, 0)
            Next
            connData.chiudi(True)
            Session.Item("DT_COMPLESSI") = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: AggiornaValoriComplessi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub


#Region "Edifici"
    Protected Sub EsportaEdifici_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_EDIFICI")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_IMP_PULIZIE_EDIFICI", "SCHEDA_IMP_PULIZIE_EDIFICI", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvEdifici.Rebind()
    End Sub

    Protected Sub RefreshEdifici_Click(sender As Object, e As System.EventArgs)
        AggiornaValoriEdifici()
        dgvEdifici.Rebind()
    End Sub

    Protected Sub dgvEdifici_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvEdifici.NeedDataSource
        Try
            connData.apri(False)
            Dim DT As Data.DataTable = Session.Item("DT_EDIFICI")
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: dgvEdifici_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli edifici!!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub


    Protected Sub dgvEdifici_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles dgvEdifici.PageIndexChanged
        AggiornaValoriEdifici()
        dgvEdifici.CurrentPageIndex = e.NewPageIndex
    End Sub

    Private Sub CaricaGridEdifici()
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If
        Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
        par.cmd.CommandText = "select id from siscom_mi.pf_main where id_esercizio_finanziario = " & HiddenEsercizio.Value
        Dim idpiano As Integer = par.cmd.ExecuteScalar

        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE ID_PRENOTAZIONE=" & HiddenPrenotazione.Value
        Dim numero As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If numero = 0 Then
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SCALE_EDIFICI_TMP " _
                & "( " _
                & " ID, ID_EDIFICIO, DESCRIZIONE, " _
                & "    PULIZIA_SCALE, ROTAZIONE_SACCHI, RESA_SACCHI,  " _
                & "    N_PIANI, N_UNITA, ID_PRENOTAZIONE " _
                & ") " _
                & " (SELECT " _
                & " ID, ID_EDIFICIO, DESCRIZIONE, " _
                & "    PULIZIA_SCALE, ROTAZIONE_SACCHI, RESA_SACCHI,  " _
                & "    N_PIANI, N_UNITA " _
                & "," & HiddenPrenotazione.Value & " FROM SISCOM_MI.SCALE_EDIFICI)"
            par.cmd.ExecuteNonQuery()
        End If
        Dim filtroContratti As String = ""
        If Not String.IsNullOrEmpty(HiddenContratto.Value) Then
            filtroContratti = "And ID_EDIFICIO IN (SELECT DISTINCT ID_EDIFICIO FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & HiddenContratto.Value & ")"
        End If
        par.cmd.CommandText = "SELECT ID_PRENOTAZIONE,ID, (SELECT COD_EDIFICIO || ' - ' || DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID = ID_EDIFICIO) AS DENOMINAZIONE, " _
                            & " SCALE_EDIFICI_TMP.N_PIANI, " _
                            & " DESCRIZIONE AS SCALA, (CASE WHEN PULIZIA_SCALE = 1 THEN 'TRUE' ELSE 'FALSE' END) AS PULIZIA_SCALE, " _
                            & " (CASE WHEN ROTAZIONE_SACCHI = 1 THEN 'TRUE' ELSE 'FALSE' END) AS ROTAZIONE_SACCHI, " _
                            & " (CASE WHEN RESA_SACCHI = 1 THEN 'TRUE' ELSE 'FALSE' END) AS RESA_SACCHI, " _
                            & " (SELECT DISTINCT REPLACE(NOME,'SEDE TERRITORIALE','ST') FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID = SCALE_EDIFICI_TMP.ID_EDIFICIO))) AS SEDE_TERRITORIALE," _
                            & " SCALE_EDIFICI_TMP.PULIZIA_SCALE*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ") AND TIPOLOGIA=(SCALE_EDIFICI_TMP.N_PIANI)) AS PU1, " _
                            & " SCALE_EDIFICI_TMP.ROTAZIONE_SACCHI*SCALE_EDIFICI_TMP.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='7' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU7, " _
                            & " SCALE_EDIFICI_TMP.RESA_SACCHI*SCALE_EDIFICI_TMP.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='8' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU8, " _
                            & " 1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='10' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU10," _
                            & " SCALE_EDIFICI_TMP.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU12A," _
                            & " SCALE_EDIFICI_TMP.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU12B," _
                            & " SCALE_EDIFICI_TMP.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU12C," _
                            & " 1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='13' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU13 " _
                            & " FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_PRENOTAZIONE=" & HiddenPrenotazione.Value & " AND ID_EDIFICIO <> 1 " & filtroContratti & "ORDER BY  (SELECT DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID = ID_EDIFICIO) ASC"
        Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Session.Item("DT_EDIFICI") = DT
        If connAperta = True Then
            connData.chiudi(False)
        End If
    End Sub

    Private Sub AggiornaValoriEdifici()
        Try
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("DT_EDIFICI"), Data.DataTable)
            Dim row As Data.DataRow
            For Each item As GridDataItem In dgvEdifici.Items
                Dim puliziaScale As String = CStr(CType(item.FindControl("chkPuliziaScale"), CheckBox).Checked).ToUpper
                Dim rotSacchi As String = CStr(CType(item.FindControl("chkRotSacchi"), CheckBox).Checked).ToUpper
                Dim resaSacchi As String = CStr(CType(item.FindControl("chkResaSacchi"), CheckBox).Checked).ToUpper
                row = dt.Select("id = " & item("ID").Text)(0)
                'SALVATAGGIO EVENTO MODIFICA

                row.Item("PULIZIA_SCALE") = puliziaScale
                row.Item("ROTAZIONE_SACCHI") = rotSacchi
                row.Item("RESA_SACCHI") = resaSacchi
            Next
            Session.Item("DT_EDIFICI") = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: AggiornaCheck - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub


    Protected Sub headerChkScale_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
            hiddenSelTuttiScale.Value = CStr(Not CBool(hiddenSelTuttiScale.Value))
            If hiddenSelTuttiScale.Value.ToUpper = "TRUE" Then
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("PULIZIA_SCALE") = "TRUE"
                Next
            Else
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("PULIZIA_SCALE") = "FALSE"
                Next
            End If
            dgvEdifici.Rebind()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: headerChkScale_CheckedChanged - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Protected Sub headerChkRotSacchi_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
            hiddenSelTuttiRotSacchi.Value = CStr(Not CBool(hiddenSelTuttiRotSacchi.Value))
            If hiddenSelTuttiRotSacchi.Value.ToUpper = "TRUE" Then
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("ROTAZIONE_SACCHI") = "TRUE"
                Next
            Else
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("ROTAZIONE_SACCHI") = "FALSE"
                Next
            End If
            dgvEdifici.Rebind()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: headerChkRotSacchi_CheckedChanged - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Protected Sub headerChkResaSacchi_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
            hiddenSelTuttiResaSacchi.Value = CStr(Not CBool(hiddenSelTuttiResaSacchi.Value))
            If hiddenSelTuttiResaSacchi.Value.ToUpper = "TRUE" Then
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("RESA_SACCHI") = "TRUE"
                Next
            Else
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("RESA_SACCHI") = "FALSE"
                Next
            End If
            dgvEdifici.Rebind()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: headerChkRotSacchi_CheckedChanged - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
#End Region


    Private Sub btnSalvaEdifici_Click(sender As Object, e As EventArgs) Handles btnSalvaEdifici.Click
        Try
            Dim dt As Data.DataTable = Session.Item("DT_COMPLESSI")
            AggiornaValoriComplessi()
            connData.apri()
            'Edifici
            Dim dtEdifici As Data.DataTable = Session.Item("DT_EDIFICI")
            AggiornaValoriEdifici()
            connData.apri()
            For Each riga As Data.DataRow In dtEdifici.Rows
                par.cmd.CommandText = "UPDATE SISCOM_MI.SCALE_EDIFICI_TMP " _
                                    & " SET    PULIZIA_SCALE        = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("PULIZIA_SCALE"), 0))) & ", " _
                                    & "        ROTAZIONE_SACCHI     = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("ROTAZIONE_SACCHI"), 0))) & ", " _
                                    & "        RESA_SACCHI          = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("RESA_SACCHI"), 0))) _
                                    & " WHERE  ID                   = " & riga.Item("ID") _
                                    & " AND    ID_PRENOTAZIONE      = " & riga.Item("ID_PRENOTAZIONE")
                par.cmd.ExecuteNonQuery()
            Next
            RadNotificationNote.Text = "Operazione completata!"
            RadNotificationNote.Show()
            connData.chiudi(True)
            CaricaGridEdifici()
            CaricaGridTotale05()
            dgvEdifici.Rebind()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnSalvaComplessi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli edifici!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Private Sub dgvTotale_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvTotale.NeedDataSource
        Try
            Dim DT As Data.DataTable = Session.Item("DT_TOTALE")
            TryCast(sender, RadGrid).DataSource = DT
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: dgvTotale_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub





    Protected Sub EsportaTotale_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_TOTALE")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        dtNuova.Columns.Remove("ID_FORNITORE")
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_IMP_PULIZIE_EDIFICI", "SCHEDA_IMP_PULIZIE_EDIFICI", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvTotale.Rebind()
    End Sub

    Protected Sub RefreshTotale_Click(sender As Object, e As System.EventArgs)
        CaricaGridTotale()
        dgvTotale.Rebind()
    End Sub


    Private Function CaricaAttributi()

        'VENGONO CARICATI GLI ATTRIBUTI "CORRENTE" (VALORE CORRENTE) E "NOME" (NOME DEL CAMPO)
        'MENTRE IL VALORE CORRENTE VIENE CARICATO AUTOMATCAMENTE (SOLO PER CHECKBOX, TEXTBOX E DROPDOWNLIST)
        'IL VALORE DELL'ATTRIBUTO "NOME" VIENE CARICATO MANUALMENTE, IN MODO DA INSERIRE DEL TESTO PIU' 
        'SIGNIFICATIVO E NON SEMPLICEMENTE LA PROPRIETA' TEXT

        Dim CTRL As Control = Nothing
        Dim mpContentPlaceHolder As ContentPlaceHolder
        mpContentPlaceHolder = CType(Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)

        For Each CTRL In mpContentPlaceHolder.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("CORRENTE", UCase(DirectCast(CTRL, TextBox).Text))
            End If
            If TypeOf CTRL Is RadComboBox Then
                If Not IsNothing(DirectCast(CTRL, RadComboBox).SelectedItem) Then
                    DirectCast(CTRL, RadComboBox).Attributes.Add("CORRENTE", UCase(DirectCast(CTRL, RadComboBox).SelectedItem.Text))
                End If
            End If
            If TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("CORRENTE", Valore01(DirectCast(CTRL, CheckBox).Checked))
            End If

        Next


        'For i As Integer = 0 To dgvAppalti.Items.Count - 1
        '    If DirectCast(dgvAppalti.Items(i).FindControl("CheckBox1"), CheckBox).Checked = False Then
        '        row = dt.Select("ID_GRUPPO = " & dgvAppalti.Items(i).Cells(2).Text)(0)
        '        row.Item("CHECKALL") = "FALSE"
        '    Else
        '        row = dt.Select("ID_GRUPPO = " & dgvAppalti.Items(i).Cells(2).Text)(0)
        '        row.Item("CHECKALL") = "TRUE"
        '    End If
        'Next


        'attributi nome da memorizzare
        'DropDownListTipologiaSpesa.Attributes.Add("NOME", "TIPOLOGIA SPESA")
        'TextBoxDescrizioneSpesa.Attributes.Add("NOME", "DESCRIZIONE SPESA")
        'DropDownListCategoria.Attributes.Add("NOME", "CATEGORIA")
        'TextBoxImporto.Attributes.Add("NOME", "IMPORTO")
        'DropDownListTipologiaDivisione.Attributes.Add("NOME", "TIPOLOGIA DI DIVISIONE")
        'DropDownListCriterioRipartizione.Attributes.Add("NOME", "CRITERIO RIPARTIZIONE")
        'DropDownListComplesso.Attributes.Add("NOME", "COMPLESSO")
        'DropDownListLotto.Attributes.Add("NOME", "LOTTO")
        'DropDownListEdificio.Attributes.Add("NOME", "EDIFICIO")
        'DropDownListScala.Attributes.Add("NOME", "SCALA")
        'DropDownListImpianti.Attributes.Add("NOME", "IMPIANTI")
        'DropDownListAggregazione.Attributes.Add("NOME", "AGGREGAZIONE")
        'DropDownListTabellaMillesimale.Attributes.Add("NOME", "TABELLA MILLESIMALE")

FINE:
    End Function

    Private Function MemorizzaAttributi() As Boolean
        Dim ELENCOERRORI As String = ""
        Try
            Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
            Dim CTRL As Control = Nothing
            Dim mpContentPlaceHolder As ContentPlaceHolder
            mpContentPlaceHolder = CType(Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)

            'For Each CTRL In mpContentPlaceHolder.Controls
            '    If TypeOf CTRL Is TextBox Then
            '        If DirectCast(CTRL, TextBox).Text.ToUpper <> DirectCast(CTRL, TextBox).Attributes("CORRENTE").ToUpper.ToString Then
            '            If ScriviLogSpese(DirectCast(CTRL, TextBox).Attributes("NOME").ToUpper.ToString & " DELLA VOCE PROSPETTO: " & par.PulisciStrSql(TextBoxDescrizioneSpesa.Text.ToUpper), DirectCast(CTRL, TextBox).Attributes("CORRENTE").ToUpper.ToString, DirectCast(CTRL, TextBox).Text.ToUpper, 2, Tempo) = False Then
            '                ELENCOERRORI = ELENCOERRORI & DirectCast(CTRL, TextBox).Attributes("NOME").ToUpper.ToString & "<br/>"
            '            End If
            '        End If
            '    End If
            '    If TypeOf CTRL Is RadComboBox Then
            '        If Not IsNothing(DirectCast(CTRL, RadComboBox).SelectedItem) Then
            '            If DirectCast(CTRL, RadComboBox).SelectedItem.Text.ToUpper <> DirectCast(CTRL, RadComboBox).Attributes("CORRENTE").ToUpper.ToString Then
            '                If ScriviLogSpese(DirectCast(CTRL, RadComboBox).Attributes("NOME").ToUpper.ToString & " DELLA VOCE PROSPETTO: " & par.PulisciStrSql(TextBoxDescrizioneSpesa.Text.ToUpper), DirectCast(CTRL, RadComboBox).Attributes("CORRENTE").ToUpper.ToString, DirectCast(CTRL, RadComboBox).SelectedItem.Text.ToUpper, 2, Tempo) = False Then
            '                    ELENCOERRORI = ELENCOERRORI & DirectCast(CTRL, RadComboBox).Attributes("NOME").ToUpper.ToString & "<br/>"
            '                End If
            '            End If
            '        End If

            '    End If
            'Next
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("DT_APPALTI"), Data.DataTable)
            For Each riga As Data.DataRow In dt.Rows

                If riga.Item("CHECKALL").ToString.ToUpper <> riga.Item("CORRENTE").ToString.ToUpper Then
                    If ScriviLogSpese(riga.Item("NOME").ToUpper.ToString, riga.Item("CORRENTE").ToUpper.ToString, riga.Item("CHECKALL"), 2, Tempo) = False Then
                        ELENCOERRORI = ELENCOERRORI & DirectCast(CTRL, TextBox).Attributes("NOME").ToUpper.ToString & "<br/>"
                    End If
                End If

            Next


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - MemorizzaAttributi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Private Function ScriviLogSpese(ByVal CAMPO As String, ByVal VAL_PRECEDENTE As String, ByVal VAL_IMPOSTATO As String, OPERAZIONE As Integer, tempo As String) As Boolean
        Try

            par.cmd.CommandText = "Insert into SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                                & " Values (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & tempo & "', '" & par.PulisciStrSql(CAMPO) _
                                & "', '" & par.PulisciStrSql(VAL_PRECEDENTE) & "', '" & par.PulisciStrSql(VAL_IMPOSTATO) & "', " & OPERAZIONE & ")"
            par.cmd.ExecuteNonQuery()

            ScriviLogSpese = True
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - ScriviLogSpese - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function

    Private Sub SPESE_REVERSIBILI_Imputazione_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "setVisibilitaPulsanti();", True)
    End Sub

    Private Sub CaricaGridTotale05()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "select id from siscom_mi.pf_main where id_esercizio_finanziario = " & HiddenEsercizio.Value
            Dim idpiano As Integer = par.cmd.ExecuteScalar
            If Not String.IsNullOrEmpty(HiddenContratto.Value) Then
                par.cmd.CommandText = "SELECT ID, (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO, " _
                    & " (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO,  " _
                    & "  (SELECT ID_FORNITORE FROM SISCOM_MI.appalti WHERE appalti.id=appalti_lotti_patrimonio.id_appalto) AS ID_FORNITORE, " _
                    & " COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, " _
                    & " REPLACE (MQ_ESTERNI, ',', '.') AS mq_esterni, " _
                    & " REPLACE (MQ_PILOTY, ',', '.') AS MQ_PILOTY, " _
                    & " NUMERO_BIDONI_CARTA," _
                    & " NUMERO_BIDONI_VETRO," _
                    & " NUMERO_BIDONI_UMIDO," _
                    & " (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.edifici_tmp.ID_COMPLESSO)) AS SEDE_TERRITORIALE," _
                    & " FL_SPAZI_ESTERNI * " _
                    & " NVL (MQ_ESTERNI, 0) * " _
                    & " (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) " _
                    & " +  FL_SPAZI_ESTERNI * " _
                    & " NVL (MQ_PILOTY, 0) * " _
                    & " (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS IMPORTO_EDIFICIO,  " _
                    & " NVL ( (SELECT SUM ( SCALE_edifici_tmp.PULIZIA_SCALE * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     PREZZO = '1' AND TIPOLOGIA = (SCALE_edifici_tmp.N_PIANI) AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) " _
                    & " FROM SISCOM_MI.SCALE_edifici_tmp WHERE SCALE_edifici_tmp.ID_eDIFICIO = edifici_tmp.ID and SCALE_edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value & "), 0) AS IMPORTO_SCALA," _
                    & " (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO," _
                    & " (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, " _
                    & " FL_SPAZI_ESTERNI * NVL (MQ_ESTERNI, 0) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) +   FL_SPAZI_ESTERNI * NVL (MQ_PILOTY, 0) * (SELECT IMPORTO " _
                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) + NVL ( (SELECT SUM ( SCALE_edifici_tmp.PULIZIA_SCALE * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     PREZZO = '1' " _
                    & " AND TIPOLOGIA = (SCALE_edifici_tmp.N_PIANI) AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_edifici_tmp WHERE SCALE_edifici_tmp.ID_eDIFICIO = edifici_tmp.ID and SCALE_edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value & "), 0) AS TOTALE_EDIFICIO, " _
                    & " (    FL_SPAZI_ESTERNI * NVL (MQ_ESTERNI, 0) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) +   FL_SPAZI_ESTERNI * NVL (MQ_PILOTY, 0) * (SELECT IMPORTO " _
                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) + NVL ( (SELECT SUM ( SCALE_edifici_tmp.PULIZIA_SCALE * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     PREZZO = '1' " _
                    & " AND TIPOLOGIA = (SCALE_edifici_tmp.N_PIANI) AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_edifici_tmp WHERE SCALE_edifici_tmp.ID_eDIFICIO = edifici_tmp.ID and SCALE_edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value & "), 0)) * (  1 -   NVL ( (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), 0) / 100)  * (  1 +   NVL ( (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), 0) / 100) AS TOT_ANNUO_SCONTATO," _
                    & " '' AS TOTALE_LORDO FROM SISCOM_MI.edifici_tmp, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                    & " WHERE     edifici_tmp.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                    & " AND ID <> 1 AND  ID_APPALTO = " & HiddenContratto.Value & " AND edifici_tmp.FL_IN_CONDOMINIO = 0 " _
                    & " and edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value _
                    & " ORDER BY edifici_tmp.DENOMINAZIONE ASC "

            End If

            Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Session.Item("DT_TOTALE_05") = DT
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: CaricaGridTotale - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Private Sub CaricaGridTotale06()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "select id from siscom_mi.pf_main where id_esercizio_finanziario = " & HiddenEsercizio.Value
            Dim idpiano As Integer = par.cmd.ExecuteScalar
            If Not String.IsNullOrEmpty(HiddenContratto.Value) Then
                par.cmd.CommandText = "SELECT ID, (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO, (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI " _
                                    & " WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO,  (SELECT ID_FORNITORE FROM SISCOM_MI.appalti WHERE appalti.id=appalti_lotti_patrimonio.id_appalto) AS ID_FORNITORE, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, REPLACE (MQ_ESTERNI, ',', '.') AS mq_esterni, REPLACE (MQ_PILOTY, ',', '.') AS MQ_PILOTY, NUMERO_BIDONI_CARTA, " _
                                    & " NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI  " _
                                    & " WHERE ID = SISCOM_MI.EDIFICI_TMP.ID_COMPLESSO)) AS SEDE_TERRITORIALE, NVL ( (SELECT   SUM (  SCALE_EDIFICI_TMP.ROTAZIONE_SACCHI * SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '7' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.RESA_SACCHI * SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '8' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value & "), 0) AS IMPORTO_SCALA, 0 AS IMPORTO_EDIFICIO, NVL ( (SELECT   SUM (  SCALE_EDIFICI_TMP.ROTAZIONE_SACCHI " _
                                    & " * SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '7' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.RESA_SACCHI * SCALE_EDIFICI_TMP.N_UNITA " _
                                    & " * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '8' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value & "), 0) AS TOTALE_EDIFICIO, " _
                                    & " (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO, (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, NVL ( (SELECT   SUM (  SCALE_EDIFICI_TMP.ROTAZIONE_SACCHI * SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '7' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.RESA_SACCHI * SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '8' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) " _
                                    & " FROM SISCOM_MI.SCALE_EDIFICI_TMP  WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value & "), 0) * (  1 -   NVL ( (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), 0)/ 100) * (  1 +   NVL ( (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto),0) / 100) AS TOT_ANNUO_SCONTATO, '' AS TOTALE_LORDO FROM SISCOM_MI.EDIFICI_tmp, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                    & " WHERE     EDIFICI_TMP.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO AND ID <> 1 AND ID_APPALTO = " & HiddenContratto.Value & " AND EDIFICI_TMP.FL_IN_CONDOMINIO = 0 " _
                                    & " and edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value _
                                    & " ORDER BY EDIFICI_TMP.DENOMINAZIONE ASC "

            End If

            Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Session.Item("DT_TOTALE_06") = DT
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: CaricaGridTotale - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Private Sub CaricaGridTotale07()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "select id from siscom_mi.pf_main where id_esercizio_finanziario = " & HiddenEsercizio.Value
            Dim idpiano As Integer = par.cmd.ExecuteScalar
            If Not String.IsNullOrEmpty(HiddenContratto.Value) Then

                par.cmd.CommandText = "SELECT ID, (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO, (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI " _
                                    & " WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO,  (SELECT ID_FORNITORE FROM SISCOM_MI.appalti WHERE appalti.id=appalti_lotti_patrimonio.id_appalto) AS ID_FORNITORE,  COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, REPLACE (MQ_ESTERNI, ',', '.') AS mq_esterni,  " _
                                    & " REPLACE (MQ_PILOTY, ',', '.') AS MQ_PILOTY, NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI_TMP.ID_COMPLESSO)) AS SEDE_TERRITORIALE, (NVL (NUMERO_BIDONI_VETRO, 0) + NVL (NUMERO_BIDONI_CARTA, 0)) * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '9' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) +   2 * (NVL (NUMERO_BIDONI_UMIDO, 0)) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '9' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS IMPORTO_EDIFICIO, 0 AS IMPORTO_SCALA, (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO, (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) " _
                                    & " AS IVA, (NVL (NUMERO_BIDONI_VETRO, 0) + NVL (NUMERO_BIDONI_CARTA, 0)) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '9' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) +   2 " _
                                    & " * (NVL (NUMERO_BIDONI_UMIDO, 0)) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '9' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS TOTALE_EDIFICIO, (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO, (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, (    (NVL (NUMERO_BIDONI_VETRO, 0) + NVL (NUMERO_BIDONI_CARTA, 0)) * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '9' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) +   2 * (NVL (NUMERO_BIDONI_UMIDO, 0)) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '9' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) * (  1 -   NVL ( (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), " _
                                    & " 0) / 100) * (  1 +   NVL ( (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), 0) / 100) " _
                                    & " AS TOT_ANNUO_SCONTATO, '' AS TOTALE_LORDO FROM SISCOM_MI.EDIFICI_tmp, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO  " _
                                    & " WHERE     EDIFICI_TMP.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO aND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO = " & HiddenContratto.Value & " AND ID <> 1 AND EDIFICI_TMP.FL_IN_CONDOMINIO = 0 " _
                                    & " and edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value _
                                    & " ORDER BY EDIFICI_TMP.DENOMINAZIONE ASC "

            End If

            Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Session.Item("DT_TOTALE_07") = DT
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: CaricaGridTotale - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Private Sub CaricaGridTotale15()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "select id from siscom_mi.pf_main where id_esercizio_finanziario = " & HiddenEsercizio.Value
            Dim idpiano As Integer = par.cmd.ExecuteScalar
            If Not String.IsNullOrEmpty(HiddenContratto.Value) Then
                par.cmd.CommandText = "SELECT ID, " _
                                    & " (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO, (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI " _
                                    & " WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO,  (SELECT ID_FORNITORE FROM SISCOM_MI.appalti WHERE appalti.id=appalti_lotti_patrimonio.id_appalto) AS ID_FORNITORE,  COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, REPLACE (MQ_ESTERNI, ',', '.') AS mq_esterni,  " _
                                    & " REPLACE (MQ_PILOTY, ',', '.') AS MQ_PILOTY, NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI " _
                                    & " WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI_TMP.ID_COMPLESSO)) AS SEDE_TERRITORIALE, NVL ( (SELECT   SUM (  1 " _
                                    & " * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '10' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '12' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) " _
                                    & " + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ") )) + SUM (  1 " _
                                    & " * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '13' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value & "), " _
                                    & " 0) AS IMPORTO_SCALA, 0 AS IMPORTO_EDIFICIO, (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) " _
                                    & " AS SCONTO, (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, " _
                                    & " (SELECT   SUM (  1 * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '10' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '12' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) " _
                                    & " + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  1 * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '13' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value & ") AS TOTALE_EDIFICIO, (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO,  " _
                                    & " (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, (SELECT   SUM (  1 " _
                                    & " * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '10' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '12' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) " _
                                    & " + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  1 * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '13' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value & ") * (  1 -   NVL ( (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), 0) / 100) " _
                                    & " * (  1 +   NVL ( (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto),0) / 100) AS TOT_ANNUO_SCONTATO, " _
                                    & " '' AS TOTALE_LORDO FROM SISCOM_MI.EDIFICI_tmp, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE     EDIFICI_TMP.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =  " & HiddenContratto.Value _
                                    & " AND ID <> 1 AND EDIFICI_TMP.FL_IN_CONDOMINIO = 0 " _
                                    & " and edifici_tmp.id_prenotazione = " & HiddenPrenotazione.Value _
                                    & " ORDER BY EDIFICI_TMP.DENOMINAZIONE ASC "
            End If

            Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Session.Item("DT_TOTALE_15") = DT
            If connAperta = True Then
                connData.chiudi(False)
            End If

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: CaricaGridTotale - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Protected Sub dgvTotale05_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvTotale05.NeedDataSource
        Try
            Dim DT As Data.DataTable = Session.Item("DT_TOTALE_05")
            TryCast(sender, RadGrid).DataSource = DT
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: dgvAppalti_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Protected Sub dgvTotale06_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvTotale06.NeedDataSource
        Try
            Dim DT As Data.DataTable = Session.Item("DT_TOTALE_06")
            TryCast(sender, RadGrid).DataSource = DT
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: dgvAppalti_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub



    Protected Sub dgvTotale07_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvTotale07.NeedDataSource
        Try
            Dim DT As Data.DataTable = Session.Item("DT_TOTALE_07")
            TryCast(sender, RadGrid).DataSource = DT
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: dgvAppalti_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Protected Sub dgvTotale15_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvTotale15.NeedDataSource
        Try
            Dim DT As Data.DataTable = Session.Item("DT_TOTALE_15")
            TryCast(sender, RadGrid).DataSource = DT
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: dgvAppalti_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Protected Sub EsportaTot05_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_TOTALE_05")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_IMP_TOTALE_05", "SCHEDA_IMP_TOTALE_05", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvTotale05.Rebind()
    End Sub

    Protected Sub EsportaTot06_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_TOTALE_06")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_IMP_TOTALE_05", "SCHEDA_IMP_TOTALE_05", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvTotale06.Rebind()
    End Sub

    Protected Sub EsportaTot07_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_TOTALE_07")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_IMP_TOTALE_05", "SCHEDA_IMP_TOTALE_05", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvTotale07.Rebind()
    End Sub

    Protected Sub EsportaTot15_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_TOTALE_15")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_IMP_TOTALE_05", "SCHEDA_IMP_TOTALE_05", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvTotale15.Rebind()
    End Sub

    Protected Sub RefreshTot05_Click(sender As Object, e As System.EventArgs)
        CaricaGridTotale05()
        dgvTotale05.Rebind()
    End Sub

    Protected Sub RefreshTot06_Click(sender As Object, e As System.EventArgs)
        CaricaGridTotale06()
        dgvTotale06.Rebind()
    End Sub

    Protected Sub RefreshTot07_Click(sender As Object, e As System.EventArgs)
        CaricaGridTotale07()
        dgvTotale07.Rebind()
    End Sub

    Protected Sub RefreshTot15_Click(sender As Object, e As System.EventArgs)
        CaricaGridTotale15()
        dgvTotale15.Rebind()
    End Sub

End Class
