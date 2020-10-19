Imports Telerik.Web.UI
Partial Class SPESE_REVERSIBILI_ImputazioneBkp
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
                CType(Master.FindControl("TitoloMaster"), Label).Text = "Consuntivi e Conguagli - Schede di imputazione - Pulizie"
                CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
                connData.apri()
                CaricaGridComplessi()
                CaricaGridEdifici()
                CaricaGridAppalti()

                CaricaGridTotale05()
                CaricaGridTotale06()
                CaricaGridTotale07()
                CaricaGridTotale15()

                CaricaGridTotale()
                CaricaAttributi()

                connData.chiudi()
                HFElencoGriglie.Value = dgvComplessi.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvEdifici.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvAppalti.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvTotale05.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvTotale06.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvTotale07.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvTotale15.ClientID.ToString.Replace("ctl00", "MasterPage") & "," _
                   & dgvTotale.ClientID.ToString.Replace("ctl00", "MasterPage")

            End If
        End If
    End Sub

    'Private Sub CaricaGridTotale()
    '    Try
    '        Dim connAperta As Boolean = False
    '        If connData.Connessione.State = Data.ConnectionState.Closed Then
    '            connData.apri(False)
    '            connAperta = True
    '        End If
    '        Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
    '        par.cmd.CommandText = "SELECT ID," _
    '                            & " (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO," _
    '                            & " (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO," _
    '                            & " (SELECT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_FORNITORE," _
    '                            & " COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE,replace(MQ_ESTERNI,',','.') as mq_esterni,replace(MQ_PILOTY,',','.') as MQ_PILOTY, " _
    '                            & " NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, " _
    '                            & " (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI.ID_COMPLESSO)) AS SEDE_TERRITORIALE," _
    '                            & " NVL(MQ_ESTERNI,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='2') " _
    '                            & " +NVL(MQ_PILOTY,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='3') " _
    '                            & " +(NVL(NUMERO_BIDONI_VETRO,0)+NVL(NUMERO_BIDONI_CARTA,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='1') " _
    '                            & " +2*(NVL(NUMERO_BIDONI_UMIDO,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='2') AS IMPORTO_EDIFICIO, " _
    '                            & " NVL((SELECT " _
    '                            & " SUM(SCALE_EDIFICI.PULIZIA_SCALE*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='1' AND TIPOLOGIA=(SCALE_EDIFICI.N_PIANI)))" _
    '                            & " +SUM(SCALE_EDIFICI.ROTAZIONE_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='7'))" _
    '                            & " +SUM(7*SCALE_EDIFICI.RESA_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='8'))" _
    '                            & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='10'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='1'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='2'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='3'))" _
    '                            & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='13'))" _
    '                            & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0) AS IMPORTO_SCALA, " _
    '                            & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
    '                            & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
    '                            & " NVL(NVL(MQ_ESTERNI,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='2') " _
    '                            & " +NVL(MQ_PILOTY,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='3') " _
    '                            & " +(NVL(NUMERO_BIDONI_VETRO,0)+NVL(NUMERO_BIDONI_CARTA,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='1') " _
    '                            & " +2*(NVL(NUMERO_BIDONI_UMIDO,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='2') " _
    '                            & "+ " _
    '                            & " (SELECT " _
    '                            & " SUM(SCALE_EDIFICI.PULIZIA_SCALE*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='1' AND TIPOLOGIA=(SCALE_EDIFICI.N_PIANI)))" _
    '                            & " +SUM(SCALE_EDIFICI.ROTAZIONE_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='7'))" _
    '                            & " +SUM(7*SCALE_EDIFICI.RESA_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='8'))" _
    '                            & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='10'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='1'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='2'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='3'))" _
    '                            & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='13'))" _
    '                            & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0) AS TOTALE_EDIFICIO, " _
    '                            & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
    '                            & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
    '                            & " NVL(NVL(MQ_ESTERNI,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='2') " _
    '                            & " +NVL(MQ_PILOTY,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='3') " _
    '                            & " +(NVL(NUMERO_BIDONI_VETRO,0)+NVL(NUMERO_BIDONI_CARTA,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='1') " _
    '                            & " +2*(NVL(NUMERO_BIDONI_UMIDO,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='2') " _
    '                            & "+ " _
    '                            & " (SELECT " _
    '                            & " SUM(SCALE_EDIFICI.PULIZIA_SCALE*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='1' AND TIPOLOGIA=(SCALE_EDIFICI.N_PIANI)))" _
    '                            & " +SUM(SCALE_EDIFICI.ROTAZIONE_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='7'))" _
    '                            & " +SUM(7*SCALE_EDIFICI.RESA_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='8'))" _
    '                            & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='10'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='1'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='2'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='3'))" _
    '                            & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='13'))" _
    '                            & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID) " _
    '                            & " * (1- nvl((SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
    '                            & " * (1+ nvl((SELECT SCONTO_PUL FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05'),0)/100) " _
    '                            & " * (1+ nvl((SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100),0) AS TOT_ANNUO_SCONTATO," _
    '                            & "(SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As SCONTO," _
    '                            & " (Select IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As IVA," _
    '                            & " NVL(NVL(MQ_ESTERNI,0)*(Select IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='2') " _
    '                            & " +NVL(MQ_PILOTY,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='3') " _
    '                            & " +(NVL(NUMERO_BIDONI_VETRO,0)+NVL(NUMERO_BIDONI_CARTA,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='1') " _
    '                            & " +2*(NVL(NUMERO_BIDONI_UMIDO,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='2') " _
    '                            & "+ " _
    '                            & " (SELECT " _
    '                            & " SUM(SCALE_EDIFICI.PULIZIA_SCALE*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='1' AND TIPOLOGIA=(SCALE_EDIFICI.N_PIANI)))" _
    '                            & " +SUM(SCALE_EDIFICI.ROTAZIONE_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='7'))" _
    '                            & " +SUM(7*SCALE_EDIFICI.RESA_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='8'))" _
    '                            & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='10'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='1'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='2'))" _
    '                            & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='3'))" _
    '                            & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='13'))" _
    '                            & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID) " _
    '                            & " * (1- nvl((SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
    '                            & " * (1+ nvl((SELECT SCONTO_PUL FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05'),0)/100) " _
    '                            & " * (1+ nvl((SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
    '                            & " ,0) * (SELECT ROUND (NVL (IMPORTO_RETTIFICA, 0), 10) FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE id_gruppo=appalti_lotti_patrimonio.ID_APPALTO " _
    '                            & " And ID_PF = " & idElaborazione _
    '                            & " And ID_TIPO_SPESA = 1) " _
    '                            & " As TOT_ANNUO_SCONTATO_RETT, '' AS TOTALE_LORDO " _
    '                            & " FROM SISCOM_MI.EDIFICI,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
    '                            & " WHERE EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
    '                            & " AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1) " _
    '                            & " AND ID <> 1 AND EDIFICI.FL_IN_CONDOMINIO = 0  ORDER BY EDIFICI.DENOMINAZIONE ASC"
    '        Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
    '        Session.Item("DT_TOTALE") = DT
    '        If connAperta = True Then
    '            connData.chiudi(False)
    '        End If
    '    Catch ex As Exception
    '        connData.chiudi()
    '        Session.Add("ERRORE", "Provenienza: CaricaGridTotale - " & ex.Message)

    '    End Try
    'End Sub

    Private Sub CaricaGridTotale()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "SELECT ID," _
                                & " (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO," _
                                & " (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO," _
                                & " (SELECT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_FORNITORE," _
                                & " COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE,replace(MQ_ESTERNI,',','.') as mq_esterni,replace(MQ_PILOTY,',','.') as MQ_PILOTY, " _
                                & " NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, " _
                                & " 0 AS IMPORTO_EDIFICIO, " _
                                & " 0 AS IMPORTO_SCALA, " _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
                                & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
                                & " 0 AS TOTALE_EDIFICIO, " _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
                                & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
                                & " 0 AS TOT_ANNUO_SCONTATO," _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As SCONTO," _
                                & " (Select IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As IVA," _
                                & " 0 As TOT_ANNUO_SCONTATO_RETT, '' AS TOTALE_LORDO " _
                                & " FROM SISCOM_MI.EDIFICI,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                & " WHERE EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                                & " AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1) " _
                                & " AND ID <> 1 AND EDIFICI.FL_IN_CONDOMINIO = 0  ORDER BY EDIFICI.DENOMINAZIONE ASC"
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
                riga.Item("TOT_ANNUO_SCONTATO_RETT") = CDec(par.IfEmpty(row05.Item("TOT_ANNUO_SCONTATO_RETT").ToString, 0)) + CDec(par.IfEmpty(row06.Item("TOT_ANNUO_SCONTATO_RETT").ToString, 0)) + CDec(par.IfEmpty(row07.Item("TOT_ANNUO_SCONTATO_RETT").ToString, 0)) + CDec(par.IfEmpty(row15.Item("TOT_ANNUO_SCONTATO_RETT").ToString, 0))
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
        If Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = 0 Then
            RadWindowManager1.RadAlert("E\' necessario selezionare almeno una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
            Return False
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
        par.cmd.CommandText = "SELECT ID, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE,replace(MQ_ESTERNI,',','.') as mq_esterni,replace(MQ_PILOTY,',','.') as MQ_PILOTY, " _
                                & " NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, " _
                                & " (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI.ID_COMPLESSO)) AS SEDE_TERRITORIALE," _
                                & " FL_SPAZI_ESTERNI*NVL(MQ_ESTERNI,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='2') AS PU2," _
                                & " FL_SPAZI_ESTERNI*NVL(MQ_PILOTY,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='3') AS PU3, " _
                                & " (NVL(NUMERO_BIDONI_VETRO,0)+NVL(NUMERO_BIDONI_CARTA,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='1') AS PU9A, " _
                                & " 2*(NVL(NUMERO_BIDONI_UMIDO,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='2') AS PU9B " _
                                & " FROM SISCOM_MI.EDIFICI where id <> 1  ORDER BY EDIFICI.DENOMINAZIONE ASC"
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
            Dim row As Data.DataRow
            For Each item As GridDataItem In dgvComplessi.Items
                Dim mqEsterni As String = CType(item.FindControl("txtMqEsterni"), RadNumericTextBox).Text
                Dim mqPiloty As String = CType(item.FindControl("txtMqPiloty"), RadNumericTextBox).Text
                Dim numBidoniCarta As String = CType(item.FindControl("txtNumBidoniCarta"), RadNumericTextBox).Text
                Dim numBidoniVetro As String = CType(item.FindControl("txtNumBidoniVetro"), RadNumericTextBox).Text
                Dim numBidoniUmido As String = CType(item.FindControl("txtNumBidoniUmido"), RadNumericTextBox).Text
                row = dt.Select("id = " & item("ID").Text)(0)
                row.Item("NUMERO_BIDONI_UMIDO") = par.IfEmpty(numBidoniUmido, 0)
                row.Item("NUMERO_BIDONI_VETRO") = par.IfEmpty(numBidoniVetro, 0)
                row.Item("NUMERO_BIDONI_CARTA") = par.IfEmpty(numBidoniCarta, 0)
                row.Item("MQ_PILOTY") = par.IfEmpty(mqPiloty, 0)
                row.Item("MQ_ESTERNI") = par.IfEmpty(mqEsterni, 0)
            Next
            Session.Item("DT_COMPLESSI") = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: AggiornaValoriComplessi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Private Sub AggiornaValoriAppalti()
        Try
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("DT_APPALTI"), Data.DataTable)
            Dim row As Data.DataRow
            For Each item As GridDataItem In dgvAppalti.Items
                Dim mese05 As String = CType(item.FindControl("txtMese05"), RadNumericTextBox).Text
                Dim mese06 As String = CType(item.FindControl("txtMese06"), RadNumericTextBox).Text
                Dim mese07 As String = CType(item.FindControl("txtMese07"), RadNumericTextBox).Text
                Dim mese15 As String = CType(item.FindControl("txtMese15"), RadNumericTextBox).Text
                row = dt.Select("ID_GRUPPO = " & item("ID_GRUPPO").Text)(0)
                row.Item("MESE05") = par.IfEmpty(mese05, 0)
                row.Item("MESE06") = par.IfEmpty(mese06, 0)
                row.Item("MESE07") = par.IfEmpty(mese07, 0)
                row.Item("MESE15") = par.IfEmpty(mese15, 0)
            Next
            Session.Item("DT_APPALTI") = dt
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
        par.cmd.CommandText = "SELECT ID, (SELECT COD_EDIFICIO || ' - ' || DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID = ID_EDIFICIO) AS DENOMINAZIONE, " _
                            & " DESCRIZIONE AS SCALA, (CASE WHEN PULIZIA_SCALE = 1 THEN 'TRUE' ELSE 'FALSE' END) AS PULIZIA_SCALE, " _
                            & " (CASE WHEN ROTAZIONE_SACCHI = 1 THEN 'TRUE' ELSE 'FALSE' END) AS ROTAZIONE_SACCHI, " _
                            & " (CASE WHEN RESA_SACCHI = 1 THEN 'TRUE' ELSE 'FALSE' END) AS RESA_SACCHI, " _
                            & " (SELECT DISTINCT REPLACE(NOME,'SEDE TERRITORIALE','ST') FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID = SCALE_EDIFICI.ID_EDIFICIO))) AS SEDE_TERRITORIALE," _
                            & " SCALE_EDIFICI.PULIZIA_SCALE*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='1' AND TIPOLOGIA=(SCALE_EDIFICI.N_PIANI)) AS PU1, " _
                            & " SCALE_EDIFICI.ROTAZIONE_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='7') AS PU7, " _
                            & " 7*SCALE_EDIFICI.RESA_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='8') AS PU8, " _
                            & " 1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='10') AS PU10," _
                            & " SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='1') AS PU12A," _
                            & " SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='2') AS PU12B," _
                            & " SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='3') AS PU12C," _
                            & " 1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='13') AS PU13 " _
                            & " FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO <> 1 ORDER BY  (SELECT DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID = ID_EDIFICIO) ASC"
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



    Protected Sub EsportaAppalti_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_APPALTI")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.Remove("ID_GRUPPO")
        dtNuova.Columns.Remove("ID_GRUPPO_COMP")
        dtNuova.Columns.Remove("ID_FORNITORE")
        dtNuova.Columns.Remove("CHECKALL")
        dtNuova.Columns.Remove("CHECKALL1")
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_IMP_ASCENSORE", "SCHEDA_IMP_ASCENSORE", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvAppalti.Rebind()
    End Sub

    Protected Sub RefreshAppalti_Click(sender As Object, e As System.EventArgs)

        dgvAppalti.Rebind()
    End Sub

    Private Sub dgvAppalti_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvAppalti.NeedDataSource
        Try
            Dim DT As Data.DataTable = Session.Item("DT_APPALTI")
            TryCast(sender, RadGrid).DataSource = DT
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: dgvAppalti_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Private Sub CaricaGridAppalti()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
            Dim idpiano As Integer = CInt(par.IfNull(par.cmd.ExecuteScalar, 0))
            If idpiano = 0 Then
                'piano finanziario non selezionato in sede di creazione dell'elaborazione
                par.cmd.CommandText = "SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN"
                idpiano = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If

            par.cmd.CommandText = "SELECT (SELECT RAGIONE_SOCIALE " _
                                & " FROM SISCOM_MI.FORNITORI " _
                                & " WHERE FORNITORI.ID = APPALTI.ID_FORNITORE) " _
                                & " AS FORNITORE, " _
                                & " /*(SELECT MAX (B.ID_GRUPPO) " _
                                & " FROM SISCOM_MI.APPALTI B " _
                                & " WHERE B.ID_FORNITORE = (SELECT ID " _
                                & " FROM SISCOM_MI.FORNITORI " _
                                & " WHERE FORNITORI.ID = APPALTI.ID_FORNITORE))*/ " _
                                & " APPALTI.ID AS ID_GRUPPO, " _
                                & " APPALTI.NUM_REPERTORIO AS REPERTORIO, " _
                                & " /*(SELECT WM_CONCAT (DISTINCT B.NUM_REPERTORIO) " _
                                & " FROM SISCOM_MI.APPALTI B " _
                                & " WHERE B.ID_FORNITORE = (SELECT ID " _
                                & " FROM SISCOM_MI.FORNITORI " _
                                & " WHERE FORNITORI.ID = APPALTI.ID_FORNITORE)) " _
                                & " AS REPERTORIO,*/ " _
                                & " /*(SELECT WM_CONCAT (DISTINCT B.ID_GRUPPO) " _
                                & " FROM SISCOM_MI.APPALTI B " _
                                & " WHERE B.ID_FORNITORE = (SELECT ID " _
                                & " FROM SISCOM_MI.FORNITORI " _
                                & " WHERE FORNITORI.ID = APPALTI.ID_FORNITORE))*/ " _
                                & " (SELECT MAX (B.ID_GRUPPO) " _
                                & " FROM SISCOM_MI.APPALTI B " _
                                & " WHERE B.ID_FORNITORE = (SELECT ID " _
                                & " FROM SISCOM_MI.FORNITORI " _
                                & " WHERE FORNITORI.ID = APPALTI.ID_FORNITORE)) " _
                                & " AS ID_GRUPPO_COMP, " _
                                & " SUM (IMPORTO_APPROVATO) AS IMPORTO, " _
                                & " (SELECT ID " _
                                & " FROM SISCOM_MI.FORNITORI " _
                                & " WHERE FORNITORI.ID = APPALTI.ID_FORNITORE) " _
                                & " AS ID_FORNITORE, " _
                                & " (SELECT MAX (SCONTO_CANONE) / 100 " _
                                & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                & " WHERE     IMPORTO_CANONE > 0 " _
                                & " AND ID_APPALTO IN (SELECT B.ID " _
                                & " FROM SISCOM_MI.APPALTI B " _
                                & " WHERE B.ID_FORNITORE = " _
                                & " (SELECT ID " _
                                & " FROM SISCOM_MI.FORNITORI " _
                                & " WHERE FORNITORI.ID = " _
                                & " APPALTI.ID_FORNITORE))) " _
                                & " AS SCONTO, " _
                                & " (SELECT MAX (IVA_CANONE) / 100 " _
                                & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                & " WHERE     IMPORTO_CANONE > 0 " _
                                & " AND ID_APPALTO IN (SELECT B.ID " _
                                & " FROM SISCOM_MI.APPALTI B " _
                                & " WHERE B.ID_FORNITORE = " _
                                & " (SELECT ID " _
                                & " FROM SISCOM_MI.FORNITORI " _
                                & " WHERE FORNITORI.ID = " _
                                & " APPALTI.ID_FORNITORE))) " _
                                & " AS IVA, " _
                                & "(CASE WHEN (SELECT COUNT(*) FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE IMPUTAZIONE_APPALTI.ID_GRUPPO = APPALTI.ID AND IMPUTAZIONE_APPALTI.ID_FORNITORE=APPALTI.ID_FORNITORE AND ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & ")=1 THEN 'TRUE' ELSE 'FALSE' END)  AS CHECKALL, " _
                                & " (CASE " _
                                & " WHEN (SELECT COUNT (*) " _
                                & " FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                                & " WHERE IMPUTAZIONE_APPALTI.ID_GRUPPO = APPALTI.ID AND IMPUTAZIONE_APPALTI.ID_FORNITORE = " _
                                & " APPALTI.ID_FORNITORE " _
                                & " AND ID_TIPO_SPESA = 1 " _
                                & " AND ID_PF = " & idElaborazione & ") = 1 " _
                                & " THEN " _
                                & " 'TRUE' " _
                                & " ELSE " _
                                & " 'FALSE' " _
                                & " END) " _
                                & " AS CHECKALL, 'SELEZIONATO' AS NOME, '' AS CORRENTE, " _
                                & "(SELECT nvl(MESI,1) FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE IMPUTAZIONE_PULIZIE.ID_GRUPPO = APPALTI.ID AND VOCE = '05' ) AS MESE05, " _
                                & "(SELECT nvl(MESI,1) FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE IMPUTAZIONE_PULIZIE.ID_GRUPPO = APPALTI.ID And VOCE = '07' ) AS MESE07, " _
                                & "(SELECT nvl(MESI,1) FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE IMPUTAZIONE_PULIZIE.ID_GRUPPO = APPALTI.ID And VOCE = '15' ) AS MESE15, " _
                                & "(SELECT nvl(MESI,1) FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE IMPUTAZIONE_PULIZIE.ID_GRUPPO = APPALTI.ID And VOCE = '06' ) AS MESE06 " _
                                & " FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.APPALTI " _
                                & " WHERE     TIPO_PAGAMENTO = 6 " _
                                & " AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO IN (SELECT ID " _
                                & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                & " WHERE ID_VOCE_SERVIZIO IN (SELECT ID " _
                                & " FROM SISCOM_MI.TAB_SERVIZI_VOCI " _
                                & " WHERE ID_SERVIZIO = " _
                                & " 1)) " _
                                & " AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT ID " _
                                & " FROM SISCOM_MI.PF_VOCI " _
                                & " WHERE ID_PIANO_FINANZIARIO = " & idpiano & ") " _
                                & " AND PRENOTAZIONI.ID_APPALTO = APPALTI.ID " _
                                & " AND PRENOTAZIONI.ID_STATO = 2 " _
                                & " AND ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID=" & idpiano & ")) " _
                                & " GROUP BY APPALTI.ID_FORNITORE,APPALTI.ID,APPALTI.NUM_REPERTORIO"
            Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Session.Item("DT_APPALTI") = DT

            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnApplica_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Protected Sub chkSelTutti_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim dtdett As Data.DataTable = Session.Item("DT_APPALTI")
        For Each r As Data.DataRow In dtdett.Rows
            r.Item("CHECKALL") = CStr(Not CBool(r.Item("CHECKALL")))
        Next
        dgvAppalti.Rebind()
    End Sub

    Private Sub btnApplica_Click(sender As Object, e As EventArgs) Handles btnApplica.Click
        Try
            CheckBox()
            AggiornaValoriAppalti()
            Dim DT As Data.DataTable = Session.Item("DT_APPALTI")
            Dim continua As Boolean = False
            For Each riga As Data.DataRow In DT.Rows
                If riga("CHECKALL") = True Then
                    continua = True
                    Exit For
                End If
            Next
            If continua Then
                connData.apri(True)
                Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                par.cmd.CommandText = "SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
                Dim idpiano As Integer = CInt(par.IfNull(par.cmd.ExecuteScalar, 0))
                If idpiano = 0 Then
                    'piano finanziario non selezionato in sede di creazione dell'elaborazione
                    par.cmd.CommandText = "SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN"
                    idpiano = par.IfNull(par.cmd.ExecuteScalar, 0)
                End If
                Dim i As Integer = 0
                Dim dtFiltrata As New Data.DataTable
                Dim view As New Data.DataView(DT)
                'selezione bollette scadute, se non trovo scadute prendo le non scadute
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione
                par.cmd.ExecuteNonQuery()
                view.RowFilter = "CHECKALL = 'TRUE'"
                dtFiltrata = view.ToTable
                'VERIFICA CHE I MESI SIANO STATI RIEMPITI CORRETTAMENTE
                For Each riga As Data.DataRow In dtFiltrata.Rows
                    If par.IfNull(riga.Item("MESE05"), "0") = "0" Or par.IfNull(riga.Item("MESE06"), "0") = "0" _
                        Or par.IfNull(riga.Item("MESE07"), "0") = "0" Or par.IfNull(riga.Item("MESE15"), "0") = "0" Then
                        RadWindowManager1.RadAlert("Impossibile procedere!Valorizzare le date degli appalti selezionati", 300, 150, "Attenzione", "", "null")
                        connData.chiudi()
                        Exit Sub
                    End If
                Next
                Dim continuaInsert As Boolean = True
                Dim idAppalti As String = ""
                For Each riga As Data.DataRow In dtFiltrata.Rows
                    idAppalti &= riga.Item("ID_GRUPPO") & ","
                Next
                If Not String.IsNullOrEmpty(idAppalti) Then
                    idAppalti = idAppalti.Substring(0, idAppalti.LastIndexOf(","))
                Else
                    idAppalti = "-1"
                End If

                'par.cmd.CommandText = "SELECT COUNT(*), id_edificio " _
                '                    & " FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE  " _
                '                    & " ID_APPALTO IN (" & idAppalti & ") " _
                '                    & " GROUP BY ID_EDIFICIO " _
                '                    & " HAVING COUNT(*)>1 "
                'Dim dtAppalti As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                'If dtAppalti.Rows.Count = 0 Then
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione
                par.cmd.ExecuteNonQuery()
                Dim ris As Integer = 0
                For Each riga As Data.DataRow In dtFiltrata.Rows
                    If riga.Item("CHECKALL") = True Then
                        'INSERT
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.IMPUTAZIONE_APPALTI ( " _
                            & " ID_GRUPPO, ID_PF, ID_TIPO_SPESA,ID_FORNITORE,SCONTO,IVA,IMPORTO_CANONI_EMESSI,ID_GRUPPO_COMPLESSIVO) " _
                            & "VALUES ( " & riga.Item("ID_GRUPPO") & "," _
                            & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," _
                            & "1," & riga.Item("id_fornitore") & "," & par.VirgoleInPunti(riga.Item("sconto") * 100) & "," & par.VirgoleInPunti(riga.Item("iva") * 100) & "," & par.VirgoleInPunti(riga.Item("importo")) & ",'" & riga.Item("ID_GRUPPO") & "' )"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE ( " _
                            & " REPERTORIO, VOCE, IMPORTO_TOTALE,  " _
                            & " IMPORTO_CALCOLATO, PERCENTUALE, MESI,  " _
                            & " ID_GRUPPO, ID_PF, SCONTO_PUL)  " _
                            & " (SELECT '' AS REPERTORIO,TRIM(SUBSTR(PF_VOCI_IMPORTO.DESCRIZIONE,1,2)) AS VOCE, SUM(IMPORTO_APPROVATO),NULL,NULL," & par.IfNull(riga.Item("MESE05"), 0) & "," & riga.Item("ID_GRUPPO") & "," & idElaborazione _
                            & " ,APPALTI_LOTTI_sERVIZI.PERC_ONERI_SIC_CAN " _
                            & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_LOTTI_sERVIZI,SISCOM_MI.PF_VOCI_IMPORTO " _
                            & " WHERE     TIPO_PAGAMENTO = 6 " _
                            & " AND PF_VOCI_IMPORTO.ID=PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                            & " AND APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                            & " AND APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI.ID " _
                            & " AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO IN (SELECT ID " _
                            & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                            & " WHERE ID_VOCE_SERVIZIO IN (SELECT ID " _
                            & " FROM SISCOM_MI.TAB_SERVIZI_VOCI " _
                            & " WHERE ID_SERVIZIO = " _
                            & " 1)) " _
                            & " AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO = " & idpiano & ") " _
                            & " AND PRENOTAZIONI.ID_APPALTO = APPALTI.ID " _
                            & " AND PRENOTAZIONI.ID_STATO = 2 " _
                            & " AND TRIM(SUBSTR(PF_VOCI_IMPORTO.DESCRIZIONE,1,2)) ='05' " _
                            & " AND APPALTI.ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) " _
                            & " AND APPALTI.ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & riga.Item("ID_GRUPPO") & ") " _
                            & " GROUP BY PF_VOCI_IMPORTO.DESCRIZIONE,APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CAN) "
                        ris = par.cmd.ExecuteNonQuery()
                        If ris = 0 Then
                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE ( " _
                                & "    REPERTORIO, VOCE, IMPORTO_TOTALE,  " _
                                & "    IMPORTO_CALCOLATO, PERCENTUALE, MESI,  " _
                                & "    ID_GRUPPO, ID_PF)  " _
                                & " VALUES (NULL /* REPERTORIO */, " _
                                & "  '05'/* VOCE */, " _
                                & "  0/* IMPORTO_TOTALE */, " _
                                & "  0/* IMPORTO_CALCOLATO */, " _
                                & "  1/* PERCENTUALE */, " _
                                & "  1/* MESI */, " _
                                & "  " & riga.Item("ID_GRUPPO") & "/* ID_GRUPPO */, " _
                                & idElaborazione & "  /* ID_PF */ ) "
                            par.cmd.ExecuteNonQuery()
                        End If
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE ( " _
                            & " REPERTORIO, VOCE, IMPORTO_TOTALE,  " _
                            & " IMPORTO_CALCOLATO, PERCENTUALE, MESI,  " _
                            & " ID_GRUPPO, ID_PF, SCONTO_PUL)  " _
                            & " (SELECT '' AS REPERTORIO,TRIM(SUBSTR(PF_VOCI_IMPORTO.DESCRIZIONE,1,2)) AS VOCE, SUM(IMPORTO_APPROVATO),NULL,NULL," & par.IfNull(riga.Item("MESE06"), 0) & "," & riga.Item("ID_GRUPPO") & "," & idElaborazione _
                            & " ,APPALTI_LOTTI_sERVIZI.PERC_ONERI_SIC_CAN " _
                            & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_LOTTI_sERVIZI,SISCOM_MI.PF_VOCI_IMPORTO " _
                            & " WHERE     TIPO_PAGAMENTO = 6 " _
                            & " AND PF_VOCI_IMPORTO.ID=PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                            & " AND APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                            & " AND APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI.ID " _
                            & " AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO IN (SELECT ID " _
                            & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                            & " WHERE ID_VOCE_SERVIZIO IN (SELECT ID " _
                            & " FROM SISCOM_MI.TAB_SERVIZI_VOCI " _
                            & " WHERE ID_SERVIZIO = " _
                            & " 1)) " _
                            & " AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO = " & idpiano & ") " _
                            & " AND PRENOTAZIONI.ID_APPALTO = APPALTI.ID " _
                            & " AND PRENOTAZIONI.ID_STATO = 2 " _
                            & " AND TRIM(SUBSTR(PF_VOCI_IMPORTO.DESCRIZIONE,1,2)) ='06' " _
                            & " AND APPALTI.ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) " _
                            & " AND APPALTI.ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & riga.Item("ID_GRUPPO") & ") " _
                            & " GROUP BY PF_VOCI_IMPORTO.DESCRIZIONE,APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CAN) "
                        ris = par.cmd.ExecuteNonQuery()
                        If ris = 0 Then
                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE ( " _
                                & "    REPERTORIO, VOCE, IMPORTO_TOTALE,  " _
                                & "    IMPORTO_CALCOLATO, PERCENTUALE, MESI,  " _
                                & "    ID_GRUPPO, ID_PF)  " _
                                & " VALUES (NULL /* REPERTORIO */, " _
                                & "  '06'/* VOCE */, " _
                                & "  0/* IMPORTO_TOTALE */, " _
                                & "  0/* IMPORTO_CALCOLATO */, " _
                                & "  1/* PERCENTUALE */, " _
                                & "  1/* MESI */, " _
                                & "  " & riga.Item("ID_GRUPPO") & "/* ID_GRUPPO */, " _
                                & idElaborazione & "  /* ID_PF */ ) "
                            par.cmd.ExecuteNonQuery()
                        End If
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE ( " _
                            & " REPERTORIO, VOCE, IMPORTO_TOTALE,  " _
                            & " IMPORTO_CALCOLATO, PERCENTUALE, MESI,  " _
                            & " ID_GRUPPO, ID_PF, SCONTO_PUL)  " _
                            & " (SELECT '' AS REPERTORIO,TRIM(SUBSTR(PF_VOCI_IMPORTO.DESCRIZIONE,1,2)) AS VOCE, SUM(IMPORTO_APPROVATO),NULL,NULL," & par.IfNull(riga.Item("MESE15"), 0) & "," & riga.Item("ID_GRUPPO") & "," & idElaborazione _
                            & " ,APPALTI_LOTTI_sERVIZI.PERC_ONERI_SIC_CAN " _
                            & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_LOTTI_sERVIZI,SISCOM_MI.PF_VOCI_IMPORTO " _
                            & " WHERE     TIPO_PAGAMENTO = 6 " _
                            & " AND PF_VOCI_IMPORTO.ID=PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                            & " AND APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                            & " AND APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI.ID " _
                            & " AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO IN (SELECT ID " _
                            & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                            & " WHERE ID_VOCE_SERVIZIO IN (SELECT ID " _
                            & " FROM SISCOM_MI.TAB_SERVIZI_VOCI " _
                            & " WHERE ID_SERVIZIO = " _
                            & " 1)) " _
                            & " AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO = " & idpiano & ") " _
                            & " AND PRENOTAZIONI.ID_APPALTO = APPALTI.ID " _
                            & " AND PRENOTAZIONI.ID_STATO = 2 " _
                            & " AND TRIM(SUBSTR(PF_VOCI_IMPORTO.DESCRIZIONE,1,2)) ='15' " _
                            & " AND APPALTI.ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) " _
                            & " AND APPALTI.ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & riga.Item("ID_GRUPPO") & ") " _
                            & " GROUP BY PF_VOCI_IMPORTO.DESCRIZIONE,APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CAN) "
                        ris = par.cmd.ExecuteNonQuery()
                        If ris = 0 Then
                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE ( " _
                                & "    REPERTORIO, VOCE, IMPORTO_TOTALE,  " _
                                & "    IMPORTO_CALCOLATO, PERCENTUALE, MESI,  " _
                                & "    ID_GRUPPO, ID_PF)  " _
                                & " VALUES (NULL /* REPERTORIO */, " _
                                & "  '15'/* VOCE */, " _
                                & "  0/* IMPORTO_TOTALE */, " _
                                & "  0/* IMPORTO_CALCOLATO */, " _
                                & "  1/* PERCENTUALE */, " _
                                & "  1/* MESI */, " _
                                & "  " & riga.Item("ID_GRUPPO") & "/* ID_GRUPPO */, " _
                                & idElaborazione & "  /* ID_PF */ ) "
                            par.cmd.ExecuteNonQuery()
                        End If
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE ( " _
                           & " REPERTORIO, VOCE, IMPORTO_TOTALE,  " _
                           & " IMPORTO_CALCOLATO, PERCENTUALE, MESI,  " _
                           & " ID_GRUPPO, ID_PF, SCONTO_PUL)  " _
                           & " (SELECT '' AS REPERTORIO,TRIM(SUBSTR(PF_VOCI_IMPORTO.DESCRIZIONE,1,2)) AS VOCE, SUM(IMPORTO_APPROVATO),NULL,NULL," & par.IfNull(riga.Item("MESE07"), 0) & "," & riga.Item("ID_GRUPPO") & "," & idElaborazione _
                           & " ,APPALTI_LOTTI_sERVIZI.PERC_ONERI_SIC_CAN " _
                           & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_LOTTI_sERVIZI,SISCOM_MI.PF_VOCI_IMPORTO " _
                           & " WHERE     TIPO_PAGAMENTO = 6 " _
                           & " AND PF_VOCI_IMPORTO.ID=PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                           & " AND APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                           & " AND APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI.ID " _
                           & " AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO IN (SELECT ID " _
                           & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                           & " WHERE ID_VOCE_SERVIZIO IN (SELECT ID " _
                           & " FROM SISCOM_MI.TAB_SERVIZI_VOCI " _
                           & " WHERE ID_SERVIZIO = " _
                           & " 1)) " _
                           & " AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO = " & idpiano & ") " _
                           & " AND PRENOTAZIONI.ID_APPALTO = APPALTI.ID " _
                           & " AND PRENOTAZIONI.ID_STATO = 2 " _
                           & "AND TRIM(SUBSTR(PF_VOCI_IMPORTO.DESCRIZIONE,1,2)) ='07' " _
                           & " AND APPALTI.ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) " _
                           & " AND APPALTI.ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & riga.Item("ID_GRUPPO") & ") " _
                           & " GROUP BY PF_VOCI_IMPORTO.DESCRIZIONE,APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CAN) "
                        ris = par.cmd.ExecuteNonQuery()
                        If ris = 0 Then
                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE ( " _
                                & "    REPERTORIO, VOCE, IMPORTO_TOTALE,  " _
                                & "    IMPORTO_CALCOLATO, PERCENTUALE, MESI,  " _
                                & "    ID_GRUPPO, ID_PF)  " _
                                & " VALUES (NULL /* REPERTORIO */, " _
                                & "  '07'/* VOCE */, " _
                                & "  0/* IMPORTO_TOTALE */, " _
                                & "  0/* IMPORTO_CALCOLATO */, " _
                                & "  1/* PERCENTUALE */, " _
                                & "  1/* MESI */, " _
                                & "  " & riga.Item("ID_GRUPPO") & "/* ID_GRUPPO */, " _
                                & idElaborazione & "  /* ID_PF */ ) "
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If
                Next
                MemorizzaAttributi()
                CaricaGridTotale05()
                'CALCOLO DELLA RETTIFICA
                Dim dtTotale As Data.DataTable = Session.Item("DT_TOTALE_05")
                par.cmd.CommandText = "SELECT ID_GRUPPO,ID_PF FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND VOCE = '05'"
                Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim totale As Decimal = 0
                While Lettore.Read
                    totale = 0
                    For Each riga As Data.DataRow In dtTotale.Rows
                        If par.IfNull(riga.Item("ID_GRUPPO"), "-1") = par.IfNull(Lettore(0), 0) Then
                            totale += par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0)
                        End If
                    Next
                    If totale <> 0 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                            & " SET IMPORTO_CALCOLATO=MESI*" & par.VirgoleInPunti(totale) _
                            & " ,PERCENTUALE=IMPORTO_TOTALE/MESI/" & par.VirgoleInPunti(totale) _
                            & " WHERE ID_PF=" & idElaborazione _
                            & " AND ID_GRUPPO=" & par.IfNull(Lettore(0), 0) _
                            & " AND VOCE='05'"
                        par.cmd.ExecuteNonQuery()
                    End If
                End While
                Lettore.Close()
                CaricaGridTotale06()
                dtTotale = Session.Item("DT_TOTALE_06")
                par.cmd.CommandText = "SELECT ID_GRUPPO,ID_PF FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND VOCE = '06'"
                Lettore = par.cmd.ExecuteReader
                totale = 0
                While Lettore.Read
                    totale = 0
                    For Each riga As Data.DataRow In dtTotale.Rows
                        If par.IfNull(riga.Item("ID_GRUPPO"), "-1") = par.IfNull(Lettore(0), 0) Then
                            totale += par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0)
                        End If
                    Next
                    If totale <> 0 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                            & " SET IMPORTO_CALCOLATO=MESI*" & par.VirgoleInPunti(totale) _
                            & " ,PERCENTUALE=IMPORTO_TOTALE/MESI/" & par.VirgoleInPunti(totale) _
                            & " WHERE ID_PF=" & idElaborazione _
                            & " AND ID_GRUPPO=" & par.IfNull(Lettore(0), 0) _
                            & " AND VOCE='06'"
                        par.cmd.ExecuteNonQuery()
                    End If
                End While
                Lettore.Close()
                CaricaGridTotale07()
                dtTotale = Session.Item("DT_TOTALE_07")
                par.cmd.CommandText = "SELECT ID_GRUPPO,ID_PF FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND VOCE = '07'"
                Lettore = par.cmd.ExecuteReader
                totale = 0
                While Lettore.Read
                    totale = 0
                    For Each riga As Data.DataRow In dtTotale.Rows
                        If par.IfNull(riga.Item("ID_GRUPPO"), "-1") = par.IfNull(Lettore(0), 0) Then
                            totale += par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0)
                        End If
                    Next
                    If totale <> 0 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                            & " SET IMPORTO_CALCOLATO=MESI*" & par.VirgoleInPunti(totale) _
                            & " ,PERCENTUALE=IMPORTO_TOTALE/MESI/" & par.VirgoleInPunti(totale) _
                            & " WHERE ID_PF=" & idElaborazione _
                            & " AND ID_GRUPPO=" & par.IfNull(Lettore(0), 0) _
                            & " AND VOCE='07'"
                        par.cmd.ExecuteNonQuery()
                    End If
                End While
                Lettore.Close()
                CaricaGridTotale15()
                dtTotale = Session.Item("DT_TOTALE_15")
                par.cmd.CommandText = "SELECT ID_GRUPPO,ID_PF FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND VOCE = '15'"
                Lettore = par.cmd.ExecuteReader
                totale = 0
                While Lettore.Read
                    totale = 0
                    For Each riga As Data.DataRow In dtTotale.Rows
                        If par.IfNull(riga.Item("ID_GRUPPO"), "-1") = par.IfNull(Lettore(0), 0) Then
                            totale += par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0)
                        End If
                    Next
                    If totale <> 0 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                            & " SET IMPORTO_CALCOLATO=MESI*" & par.VirgoleInPunti(totale) _
                            & " ,PERCENTUALE=IMPORTO_TOTALE/MESI/" & par.VirgoleInPunti(totale) _
                            & " WHERE ID_PF=" & idElaborazione _
                            & " AND ID_GRUPPO=" & par.IfNull(Lettore(0), 0) _
                            & " AND VOCE='15'"
                        par.cmd.ExecuteNonQuery()
                    End If
                End While
                Lettore.Close()

                RadNotificationNote.Text = "Operazione completata correttamente!"
                RadNotificationNote.Show()
                'Else
                'RadWindowManager1.RadAlert("Impossibile procedere! ", 300, 150, "Attenzione", "", "null")
                'End If
                connData.chiudi(True)
                connData.apri()
                CaricaGridAppalti()
                CaricaAttributi()
                dgvAppalti.Rebind()

                CaricaGridTotale()
                dgvTotale.Rebind()

                connData.chiudi()
                'HiddenCheck.Value = "0"
            Else
                RadWindowManager1.RadAlert("Impossibile procedere! <br />Selezionare almeno un elemento da abbinare.", 300, 150, "Attenzione", "", "null")
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnApplica_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio degli appalti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Protected Sub CheckBox()
        Try
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("DT_APPALTI"), Data.DataTable)
            Dim row As Data.DataRow
            For i As Integer = 0 To dgvAppalti.Items.Count - 1
                If DirectCast(dgvAppalti.Items(i).FindControl("CheckBox1"), CheckBox).Checked = False Then
                    row = dt.Select("ID_GRUPPO = " & dgvAppalti.Items(i).Cells(2).Text)(0)
                    row.Item("CHECKALL") = "FALSE"
                Else
                    row = dt.Select("ID_GRUPPO = " & dgvAppalti.Items(i).Cells(2).Text)(0)
                    row.Item("CHECKALL") = "TRUE"
                End If
            Next
            Session.Item("DT_APPALTI") = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - CheckBox - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

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
                par.cmd.CommandText = "UPDATE SISCOM_MI.SCALE_EDIFICI " _
                                    & " SET    PULIZIA_SCALE        = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("PULIZIA_SCALE"), 0))) & ", " _
                                    & "        ROTAZIONE_SACCHI        = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("ROTAZIONE_SACCHI"), 0))) & ", " _
                                    & "        RESA_SACCHI        = " & Convert.ToInt32(CBool(par.IfNull(riga.Item("RESA_SACCHI"), 0))) _
                                    & " WHERE  ID                         = " & riga.Item("ID")
                par.cmd.ExecuteNonQuery()
            Next
            RadNotificationNote.Text = "Operazione completata!"
            RadNotificationNote.Show()
            connData.chiudi(True)
            CaricaGridEdifici()
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

    'Private Sub btnCalcolaRett_Click(sender As Object, e As EventArgs) Handles btnCalcolaRett.Click
    '    Try
    '        Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
    '        Dim dt As Data.DataTable = Session.Item("DT_TOTALE")
    '        connData.apri()
    '        par.cmd.CommandText = "SELECT DISTINCT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_PF=" & idElaborazione & " AND ID_TIPO_SPESA = 1"
    '        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '        Dim totale As Decimal = 0
    '        While Lettore.Read
    '            totale = 0
    '            For Each riga As Data.DataRow In dt.Rows
    '                If par.IfNull(riga.Item("ID_FORNITORE"), "-1") = par.IfNull(Lettore(0), 0) Then
    '                    totale += par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0)
    '                End If
    '            Next
    '            If totale <> 0 Then
    '                par.cmd.CommandText = "UPDATE SISCOM_MI.IMPUTAZIONE_APPALTI " _
    '                & " SET IMPORTO_CANONI_CALCOLATI=" & par.VirgoleInPunti(totale) _
    '                & " ,IMPORTO_RETTIFICA=IMPORTO_CANONI_EMESSI/" & par.VirgoleInPunti(totale) _
    '                & " WHERE ID_PF=" & idElaborazione & " AND ID_TIPO_SPESA=1 AND ID_FORNITORE=" & par.IfNull(Lettore(0), 0)
    '                par.cmd.ExecuteNonQuery()
    '            End If

    '        End While
    '        Lettore.Close()
    '        connData.chiudi()
    '        CaricaGridTotale()
    '        dgvTotale.Rebind()
    '    Catch ex As Exception
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            connData.chiudi(False)
    '        End If
    '        Session.Add("ERRORE", "Provenienza: " & Page.Title & " - btnCalcolaRett_Click - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    Private Sub btnCalcolaCons_Click(sender As Object, e As EventArgs) Handles btnCalcolaCons.Click
        Try
            connData.apri(True)
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
            Dim idpiano As Integer = CInt(par.IfNull(par.cmd.ExecuteScalar, 0))
            If idpiano = 0 Then
                'piano finanziario non selezionato in sede di creazione dell'elaborazione
                par.cmd.CommandText = "SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN"
                idpiano = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.PF_CONS_RIPARTIZIONI WHERE ID_PF = " & idElaborazione & " AND DESCRIZIONE LIKE 'DA IMPUTAZIONE_PULIZIE#%'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.IMPUTAZIONE_PULIZIE_ED WHERE ID_PF=" & idElaborazione
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.IMPUTAZIONE_PULIZIE_COMP WHERE ID_PF=" & idElaborazione
            par.cmd.ExecuteNonQuery()

            '05 edifici
            par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE_ED (  " _
                & "     ID_EDIFICIO, COD_EDIFICIO, DENOMINAZIONE_EDIFICIO,   " _
                & "     ID_COMPLESSO,   " _
                & "      REPERTORIO, SCONTO_CANONE, P_ONERI,   " _
                & "     IVA_CANONE, PU1, PU7,   " _
                & "     PU8, PU10, PU12_A,   " _
                & "     PU12_B, PU12_C, PU13, ID_PF, MESI,RETTIFICA)   " _
                & "  (SELECT edifici.id, " _
                & "          cod_edificio, " _
                & "          edifici.denominazione, " _
                & "          edifici.id_complesso, " _
                & "          (SELECT id_gruppo " _
                & "             FROM siscom_mi.appalti " _
                & "            WHERE id = appalti_lotti_patrimonio.id_appalto) " _
                & "             AS id_appalto, " _
                & "          (SELECT SCONTO " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "            WHERE     ID_TIPO_SPESA = 1 " _
                & "                  AND ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) " _
                & "             AS SCONTO, " _
                & "          (SELECT SCONTO_PUL " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05') " _
                & "             AS P_ONERI, " _
                & "          (SELECT IVA " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "            WHERE     ID_TIPO_SPESA = 1 " _
                & "                  AND ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) " _
                & "             AS IVA, " _
                & "          NVL ( " _
                & "             (SELECT SUM ( " _
                & "                          SCALE_EDIFICI.PULIZIA_SCALE " _
                & "                        * (SELECT IMPORTO " _
                & "                             FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                            WHERE     PREZZO = '1' " _
                & "                                  AND TIPOLOGIA = " _
                & "                                         (SCALE_EDIFICI.N_PIANI))) " _
                & "                FROM SISCOM_MI.SCALE_EDIFICI " _
                & "               WHERE SCALE_EDIFICI.ID_eDIFICIO = EDIFICI.ID), " _
                & "             0) " _
                & "             AS pu1, " _
                & "          0 AS PU7, " _
                & "          0 AS PU8, " _
                & "          0 AS PU10, " _
                & "          0 AS PU12A, " _
                & "          0 AS PU12B, " _
                & "          0 AS PU12C, " _
                & "          0 AS PU13 " _
                & "," & idElaborazione _
                & ",(SELECT MESI " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05') " _
                & ",(SELECT PERCENTUALE " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05') " _
                & "     FROM SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                & "    WHERE     EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                & "          AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO " _
                & "                                                        FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "                                                       WHERE ID_TIPO_SPESA = 1) " _
                & "          AND ID <> 1 " _
                & "          AND EDIFICI.FL_IN_CONDOMINIO = 0 " _
                & "          )"
            par.cmd.ExecuteNonQuery()

            '05 complessi
            par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE_COMP (  " _
                & "     ID_EDIFICIO, COD_EDIFICIO, DENOMINAZIONE_EDIFICIO,   " _
                & "     ID_COMPLESSO,   " _
                & "      REPERTORIO, SCONTO_CANONE,P_ONERI,   " _
                & "     IVA_CANONE, PU2, PU3,   " _
                & "     PU9_A, PU9_B,ID_PF, MESI,RETTIFICA)   " _
                & " (SELECT edifici.id, " _
                & "          cod_edificio, " _
                & "          edifici.denominazione, " _
                & "          edifici.id_complesso, " _
                & "          (SELECT id_gruppo " _
                & "             FROM siscom_mi.appalti " _
                & "            WHERE id = appalti_lotti_patrimonio.id_appalto) " _
                & "             AS id_appalto, " _
                & "          (SELECT SCONTO " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "            WHERE     ID_TIPO_SPESA = 1 " _
                & "                  AND ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) " _
                & "             AS SCONTO, " _
                & "          (SELECT SCONTO_PUL " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05') " _
                & "             AS P_ONERI, " _
                & "          (SELECT IVA " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "            WHERE     ID_TIPO_SPESA = 1 " _
                & "                  AND ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) " _
                & "             AS IVA, " _
                & "              FL_SPAZI_ESTERNI " _
                & "               * NVL (MQ_ESTERNI, 0) " _
                & "               * (SELECT IMPORTO " _
                & "                    FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                   WHERE PREZZO = '2') AS PU2, " _
                & "                    FL_SPAZI_ESTERNI " _
                & "               * NVL (MQ_PILOTY, 0) " _
                & "               * (SELECT IMPORTO " _
                & "                    FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                   WHERE PREZZO = '3') AS PU3, " _
                & "                    0 AS PU9_A, " _
                & "                  0 AS PU9_B " _
                & "," & idElaborazione _
                & ",(SELECT MESI " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05') " _
                & ",(SELECT PERCENTUALE " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05') " _
                & "                      FROM SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                & "    WHERE     EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                & "          AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO " _
                & "                                                        FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "                                                       WHERE ID_TIPO_SPESA = 1) " _
                & "          AND ID <> 1 " _
                & "          AND EDIFICI.FL_IN_CONDOMINIO = 0)  "
            par.cmd.ExecuteNonQuery()

            '06 edifici
            par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE_ED (  " _
                & "     ID_EDIFICIO, COD_EDIFICIO, DENOMINAZIONE_EDIFICIO,   " _
                & "     ID_COMPLESSO,   " _
                & "      REPERTORIO, SCONTO_CANONE, P_ONERI,   " _
                & "     IVA_CANONE, PU1, PU7,   " _
                & "     PU8, PU10, PU12_A,   " _
                & "     PU12_B, PU12_C, PU13,ID_PF, MESI,RETTIFICA)   " _
                & " (SELECT edifici.id, " _
                & "          cod_edificio, " _
                & "          edifici.denominazione, " _
                & "          edifici.id_complesso, " _
                & "          (SELECT id_gruppo " _
                & "             FROM siscom_mi.appalti " _
                & "            WHERE id = appalti_lotti_patrimonio.id_appalto) " _
                & "             AS id_appalto, " _
                & "          (SELECT SCONTO " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "            WHERE     ID_TIPO_SPESA = 1 " _
                & "                  AND ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) " _
                & "             AS SCONTO, " _
                & "          (SELECT SCONTO_PUL " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='06') " _
                & "             AS P_ONERI, " _
                & "          (SELECT IVA " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "            WHERE     ID_TIPO_SPESA = 1 " _
                & "                  AND ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) " _
                & "             AS IVA, " _
                & "          0 " _
                & "             AS pu1, " _
                & "  " _
                & "          NVL ( " _
                & "               (SELECT   SUM ( " _
                & "                              SCALE_EDIFICI.ROTAZIONE_SACCHI " _
                & "                            * SCALE_EDIFICI.N_UNITA " _
                & "                            * (SELECT IMPORTO " _
                & "                                 FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                                WHERE PREZZO = '7')) " _
                & "  " _
                & "                  FROM SISCOM_MI.SCALE_EDIFICI " _
                & "                 WHERE SCALE_EDIFICI.ID_eDIFICIO = EDIFICI.ID), " _
                & "               0) AS PU7, " _
                & "          NVL ( " _
                & "               (SELECT    SUM ( " _
                & "                              7 " _
                & "                            * SCALE_EDIFICI.RESA_SACCHI " _
                & "                            * SCALE_EDIFICI.N_UNITA " _
                & "                            * (SELECT IMPORTO " _
                & "                                 FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                                WHERE PREZZO = '8')) " _
                & "                  FROM SISCOM_MI.SCALE_EDIFICI " _
                & "                 WHERE SCALE_EDIFICI.ID_eDIFICIO = EDIFICI.ID), " _
                & "               0) AS PU8, " _
                & "          0 AS PU10, " _
                & "          0 AS PU12A, " _
                & "          0 AS PU12B, " _
                & "          0 AS PU12C, " _
                & "          0 AS PU13 " _
                & "," & idElaborazione _
                & ",(SELECT MESI " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='06') " _
                & ",(SELECT PERCENTUALE " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='06') " _
                & "     FROM SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                & "    WHERE     EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                & "          AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO " _
                & "                                                        FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "                                                       WHERE ID_TIPO_SPESA = 1) " _
                & "          AND ID <> 1 " _
                & "  " _
                & "          AND EDIFICI.FL_IN_CONDOMINIO = 0) "
            par.cmd.ExecuteNonQuery()

            '07 complessi
            par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE_COMP (  " _
                & "     ID_EDIFICIO, COD_EDIFICIO, DENOMINAZIONE_EDIFICIO,   " _
                & "     ID_COMPLESSO,   " _
                & "      REPERTORIO, SCONTO_CANONE,P_ONERI,   " _
                & "     IVA_CANONE, PU2, PU3,   " _
                & "     PU9_A, PU9_B,ID_PF, MESI,RETTIFICA)   " _
                & " (SELECT edifici.id, " _
                & "          cod_edificio, " _
                & "          edifici.denominazione, " _
                & "          edifici.id_complesso, " _
                & "          (SELECT id_gruppo " _
                & "             FROM siscom_mi.appalti " _
                & "            WHERE id = appalti_lotti_patrimonio.id_appalto) " _
                & "             AS id_appalto, " _
                & "          (SELECT SCONTO " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "            WHERE     ID_TIPO_SPESA = 1 " _
                & "                  AND ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) " _
                & "             AS SCONTO, " _
                & "          (SELECT SCONTO_PUL " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='07') " _
                & "             AS P_ONERI, " _
                & "          (SELECT IVA " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "            WHERE     ID_TIPO_SPESA = 1 " _
                & "                  AND ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) " _
                & "             AS IVA, " _
                & "             0 AS PU2, " _
                & "                   0 AS PU3, " _
                & "                    (NVL (NUMERO_BIDONI_VETRO, 0) + NVL (NUMERO_BIDONI_CARTA, 0)) " _
                & "               * (SELECT IMPORTO " _
                & "                    FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                   WHERE PREZZO = '9' AND TIPOLOGIA = '1') AS PU9_A, " _
                & "                   2*(NVL (NUMERO_BIDONI_UMIDO, 0)) " _
                & "               * (SELECT IMPORTO " _
                & "                    FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                   WHERE PREZZO = '9' AND TIPOLOGIA = '2') AS PU9_B " _
                & "," & idElaborazione _
                & ",(SELECT MESI " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='07') " _
                & ",(SELECT PERCENTUALE " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='07') " _
                & "                      FROM SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                & "    WHERE     EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                & "          AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO " _
                & "                                                        FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "                                                       WHERE ID_TIPO_SPESA = 1) " _
                & "          AND ID <> 1 " _
                & "          AND EDIFICI.FL_IN_CONDOMINIO = 0)  "
            par.cmd.ExecuteNonQuery()

            '15 EDIFICI
            par.cmd.CommandText = " INSERT INTO SISCOM_MI.IMPUTAZIONE_PULIZIE_ED (  " _
                & "     ID_EDIFICIO, COD_EDIFICIO, DENOMINAZIONE_EDIFICIO,   " _
                & "     ID_COMPLESSO,   " _
                & "      REPERTORIO, SCONTO_CANONE, P_ONERI,   " _
                & "     IVA_CANONE, PU1, PU7,   " _
                & "     PU8, PU10, PU12_A,   " _
                & "     PU12_B, PU12_C, PU13,ID_PF, MESI,RETTIFICA)   " _
                & " (SELECT edifici.id, " _
                & "          cod_edificio, " _
                & "          edifici.denominazione, " _
                & "          edifici.id_complesso, " _
                & "          (SELECT id_gruppo " _
                & "             FROM siscom_mi.appalti " _
                & "            WHERE id = appalti_lotti_patrimonio.id_appalto) " _
                & "             AS id_appalto, " _
                & "          (SELECT SCONTO " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "            WHERE     ID_TIPO_SPESA = 1 " _
                & "                  AND ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) " _
                & "             AS SCONTO, " _
                & "          (SELECT SCONTO_PUL " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='15') " _
                & "             AS P_ONERI, " _
                & "          (SELECT IVA " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "            WHERE     ID_TIPO_SPESA = 1 " _
                & "                  AND ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) " _
                & "             AS IVA, " _
                & "          0 " _
                & "             AS pu1, " _
                & "  " _
                & "         0 AS PU7, " _
                & "          0 AS PU8, " _
                & "           NVL ( " _
                & "             (SELECT   SUM (  1 " _
                & "                            * (SELECT IMPORTO " _
                & "                                 FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                                WHERE PREZZO = '10')) " _
                & "  " _
                & "                FROM SISCOM_MI.SCALE_EDIFICI " _
                & "               WHERE SCALE_EDIFICI.ID_eDIFICIO = EDIFICI.ID), " _
                & "             0) " _
                & "              AS PU10, " _
                & "           NVL ( " _
                & "             (SELECT   SUM ( " _
                & "                            SCALE_EDIFICI.N_UNITA " _
                & "                          * (SELECT IMPORTO " _
                & "                               FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                              WHERE PREZZO = '12' AND TIPOLOGIA = '1')) " _
                & "  " _
                & "                FROM SISCOM_MI.SCALE_EDIFICI " _
                & "               WHERE SCALE_EDIFICI.ID_eDIFICIO = EDIFICI.ID), " _
                & "             0) " _
                & "             AS PU12A, " _
                & "           NVL ( " _
                & "             (SELECT    SUM ( " _
                & "                            SCALE_EDIFICI.N_UNITA " _
                & "                          * (SELECT IMPORTO " _
                & "                               FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                              WHERE PREZZO = '12' AND TIPOLOGIA = '2')) " _
                & "                                                      FROM SISCOM_MI.SCALE_EDIFICI " _
                & "               WHERE SCALE_EDIFICI.ID_eDIFICIO = EDIFICI.ID), " _
                & "             0) " _
                & "              AS PU12B, " _
                & "           NVL ( " _
                & "             (SELECT    SUM ( " _
                & "                            SCALE_EDIFICI.N_UNITA " _
                & "                          * (SELECT IMPORTO " _
                & "                               FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                              WHERE PREZZO = '12' AND TIPOLOGIA = '3')) " _
                & "  " _
                & "                FROM SISCOM_MI.SCALE_EDIFICI " _
                & "               WHERE SCALE_EDIFICI.ID_eDIFICIO = EDIFICI.ID), " _
                & "             0) " _
                & "              AS PU12C, " _
                & "           NVL ( " _
                & "             (SELECT   SUM (  1 " _
                & "                            * (SELECT IMPORTO " _
                & "                                 FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                & "                                WHERE PREZZO = '13')) " _
                & "                FROM SISCOM_MI.SCALE_EDIFICI " _
                & "               WHERE SCALE_EDIFICI.ID_eDIFICIO = EDIFICI.ID), " _
                & "             0) " _
                & "             AS PU13 " _
                & "," & idElaborazione _
                & ",(SELECT MESI " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF =" & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='15') " _
                & ",(SELECT PERCENTUALE " _
                & "             FROM SISCOM_MI.IMPUTAZIONE_PULIZIE " _
                & "            WHERE     ID_PF = " & idElaborazione _
                & "                  AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='15') " _
                & "     FROM SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                & "    WHERE     EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                & "          AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO " _
                & "                                                        FROM SISCOM_MI.IMPUTAZIONE_APPALTI " _
                & "                                                       WHERE ID_TIPO_SPESA = 1) " _
                & "          AND ID <> 1 " _
                & "                    AND EDIFICI.FL_IN_CONDOMINIO = 0) "
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE SISCOM_MI.IMPUTAZIONE_PULIZIE_ED  " _
                & " SET PU1_1=ROUND(PU1*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2), " _
                & " PU7_1=ROUND(PU7*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2), " _
                & " PU8_1=ROUND(PU8*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2), " _
                & " PU10_1=ROUND(PU10*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2), " _
                & " PU12_A_1=ROUND(PU12_A*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2), " _
                & " PU12_B_1=ROUND(PU12_B*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2), " _
                & " PU12_C_1=ROUND(PU12_C*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2), " _
                & " PU13_1=ROUND(PU13*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2) " _
                & " WHERE ID_PF=" & idElaborazione
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = " UPDATE SISCOM_MI.IMPUTAZIONE_PULIZIE_COMP  " _
                & " SET PU2_SCONTATO=ROUND(PU2*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2), " _
                & " PU3_SCONTATO=ROUND(PU3*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2), " _
                & " PU9_A_SCONTATO=ROUND(PU9_A*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2), " _
                & " PU9_B_SCONTATO=ROUND(PU9_B*MESI*RETTIFICA*(1-NVL(SCONTO_CANONE,0)/100)*(1+NVL(P_ONERI,0)/100)*(1+NVL(IVA_CANONE,0)/100),2) " _
                & " WHERE ID_PF=" & idElaborazione
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SISCOM_MI.CONSUNTIVO_IMPUTAZIONE_PULIZIE"
            par.cmd.CommandType = Data.CommandType.StoredProcedure
            par.cmd.Parameters.Add("ris", 0).Direction = Data.ParameterDirection.ReturnValue
            par.cmd.Parameters.Add("idPf", idElaborazione)
            par.cmd.Parameters.Add("rip", 4)
            par.cmd.Parameters.Add("idPiano", idpiano)
            par.cmd.ExecuteNonQuery()
            Dim risOp As Integer = par.cmd.Parameters("ris").Value
            Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
            par.cmd.Parameters.Clear()
            par.cmd.CommandType = Data.CommandType.Text
            If risOp > 0 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                        & " VALUES (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & Tempo & "', '-'," _
                        & "'-', '-' , 3)"
                par.cmd.ExecuteNonQuery()
            End If
            connData.chiudi(True)
            RadWindowManager1.RadAlert("Operazione effettuata: importati " & risOp & " elementi da imputazione pulizie.", 300, 150, "Attenzione", "function f(sender,args){location.href='ProspettoConsuntivi.aspx?';}", "null")
            ''RadNotificationNote.Text = "Operazione effettuata: importati " & ris & " consuntivi ascensore"
            'RadNotificationNote.Show()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - btnCalcolaCons_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
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





        Dim dt As New Data.DataTable
        dt = CType(Session.Item("DT_APPALTI"), Data.DataTable)
        Dim row As Data.DataRow
        dgvAppalti.AllowPaging = False
        dgvAppalti.Rebind()
        For i As Integer = 0 To dgvAppalti.Items.Count - 1
            row = dt.Select("ID_GRUPPO = " & dgvAppalti.Items(i).Cells(2).Text)(0)
            row.Item("CORRENTE") = CStr(CBool(DirectCast(dgvAppalti.Items(i).FindControl("CheckBox1"), CheckBox).Checked))

        Next
        dgvAppalti.AllowPaging = True
        dgvAppalti.Rebind()
        Session.Item("DT_APPALTI") = dt
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
            par.cmd.CommandText = "SELECT ID," _
                                & " (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO," _
                                & " (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO," _
                                & " (SELECT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_FORNITORE," _
                                & " COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE,replace(MQ_ESTERNI,',','.') as mq_esterni,replace(MQ_PILOTY,',','.') as MQ_PILOTY, " _
                                & " NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, " _
                                & " (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI.ID_COMPLESSO)) AS SEDE_TERRITORIALE," _
                                & " FL_SPAZI_ESTERNI*NVL(MQ_ESTERNI,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='2') " _
                                & " +FL_SPAZI_ESTERNI*NVL(MQ_PILOTY,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='3') AS IMPORTO_EDIFICIO, " _
                                & " NVL((SELECT " _
                                & " SUM(SCALE_EDIFICI.PULIZIA_SCALE*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='1' AND TIPOLOGIA=(SCALE_EDIFICI.N_PIANI)))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0) AS IMPORTO_SCALA, " _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
                                & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
                                & " FL_SPAZI_ESTERNI*NVL(MQ_ESTERNI,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='2') " _
                                & " +FL_SPAZI_ESTERNI*NVL(MQ_PILOTY,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='3') " _
                                & " +NVL((SELECT " _
                                & " SUM(SCALE_EDIFICI.PULIZIA_SCALE*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='1' AND TIPOLOGIA=(SCALE_EDIFICI.N_PIANI)))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0) AS TOTALE_EDIFICIO, " _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
                                & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
                                & " (FL_SPAZI_ESTERNI*NVL(MQ_ESTERNI,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='2') " _
                                & " +FL_SPAZI_ESTERNI*NVL(MQ_PILOTY,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='3') " _
                                & " +NVL((SELECT " _
                                & " SUM(SCALE_EDIFICI.PULIZIA_SCALE*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='1' AND TIPOLOGIA=(SCALE_EDIFICI.N_PIANI)))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0)) " _
                                & " * (1- nvl((SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (1+ nvl((SELECT SCONTO_PUL FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05'),0)/100) " _
                                & " * (1+ nvl((SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) AS TOT_ANNUO_SCONTATO," _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As SCONTO," _
                                & " (Select IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As IVA," _
                                & " (FL_SPAZI_ESTERNI*NVL(MQ_ESTERNI,0)*(Select IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='2') " _
                                & " +FL_SPAZI_ESTERNI*NVL(MQ_PILOTY,0)*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='3') " _
                                & " +NVL((SELECT " _
                                & " SUM(SCALE_EDIFICI.PULIZIA_SCALE*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='1' AND TIPOLOGIA=(SCALE_EDIFICI.N_PIANI)))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0)) " _
                                & " * (1- nvl((SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (1+ nvl((SELECT SCONTO_PUL FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05'),0)/100) " _
                                & " * (1+ nvl((SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (SELECT ROUND (NVL (PERCENTUALE, 0), 10) FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE id_gruppo=appalti_lotti_patrimonio.ID_APPALTO " _
                                & " And ID_PF = " & idElaborazione _
                                & " And VOCE = '05') " _
                                & " *nvl((SELECT MESI FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='05'),0) " _
                                & " As TOT_ANNUO_SCONTATO_RETT, '' AS TOTALE_LORDO " _
                                & " FROM SISCOM_MI.EDIFICI,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                & " WHERE EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                                & " AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1) " _
                                & " AND ID <> 1 AND EDIFICI.FL_IN_CONDOMINIO = 0  ORDER BY EDIFICI.DENOMINAZIONE ASC"
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
            par.cmd.CommandText = "SELECT ID," _
                                & " (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO," _
                                & " (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO," _
                                & " (SELECT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_FORNITORE," _
                                & " COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE,replace(MQ_ESTERNI,',','.') as mq_esterni,replace(MQ_PILOTY,',','.') as MQ_PILOTY, " _
                                & " NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, " _
                                & " (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI.ID_COMPLESSO)) AS SEDE_TERRITORIALE," _
                                & " NVL((SELECT " _
                                & " SUM(SCALE_EDIFICI.ROTAZIONE_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='7'))" _
                                & " +SUM(7*SCALE_EDIFICI.RESA_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='8'))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0) AS IMPORTO_SCALA, " _
                                & " 0 AS IMPORTO_EDIFICIO," _
                                & " NVL((SELECT " _
                                & " SUM(SCALE_EDIFICI.ROTAZIONE_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='7'))" _
                                & " +SUM(7*SCALE_EDIFICI.RESA_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='8'))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0) AS TOTALE_EDIFICIO, " _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
                                & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
                                & " NVL((SELECT " _
                                & " SUM(SCALE_EDIFICI.ROTAZIONE_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='7'))" _
                                & " +SUM(7*SCALE_EDIFICI.RESA_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='8'))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0) " _
                                & " * (1- nvl((SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (1+ nvl((SELECT SCONTO_PUL FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='06'),0)/100) " _
                                & " * (1+ nvl((SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) AS TOT_ANNUO_SCONTATO," _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As SCONTO," _
                                & " (Select IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As IVA," _
                                & " NVL((SELECT " _
                                & " SUM(SCALE_EDIFICI.ROTAZIONE_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='7'))" _
                                & " +SUM(7*SCALE_EDIFICI.RESA_SACCHI*SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='8'))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0) " _
                                & " * (1- nvl((SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (1+ nvl((SELECT SCONTO_PUL FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='06'),0)/100) " _
                                & " * (1+ nvl((SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (SELECT ROUND (NVL (PERCENTUALE, 0), 10) FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE id_gruppo=appalti_lotti_patrimonio.ID_APPALTO " _
                                & " And ID_PF = " & idElaborazione _
                                & " And VOCE = '06') " _
                                & " *nvl((SELECT MESI FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='06'),0) " _
                                & " As TOT_ANNUO_SCONTATO_RETT, '' AS TOTALE_LORDO " _
                                & " FROM SISCOM_MI.EDIFICI,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                & " WHERE EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                                & " AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1) " _
                                & " AND ID <> 1 AND EDIFICI.FL_IN_CONDOMINIO = 0  ORDER BY EDIFICI.DENOMINAZIONE ASC"
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
            par.cmd.CommandText = "SELECT ID," _
                                & " (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO," _
                                & " (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO," _
                                & " (SELECT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_FORNITORE," _
                                & " COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE,replace(MQ_ESTERNI,',','.') as mq_esterni,replace(MQ_PILOTY,',','.') as MQ_PILOTY, " _
                                & " NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, " _
                                & " (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI.ID_COMPLESSO)) AS SEDE_TERRITORIALE," _
                                & " (NVL(NUMERO_BIDONI_VETRO,0)+NVL(NUMERO_BIDONI_CARTA,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='1') " _
                                & " +2*(NVL(NUMERO_BIDONI_UMIDO,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='2') AS IMPORTO_EDIFICIO, " _
                                & " 0 AS IMPORTO_SCALA, " _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
                                & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
                                & " (NVL(NUMERO_BIDONI_VETRO,0)+NVL(NUMERO_BIDONI_CARTA,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='1') " _
                                & " +2*(NVL(NUMERO_BIDONI_UMIDO,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='2') " _
                                & " AS TOTALE_EDIFICIO, " _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
                                & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
                                & " ((NVL(NUMERO_BIDONI_VETRO,0)+NVL(NUMERO_BIDONI_CARTA,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='1') " _
                                & " +2*(NVL(NUMERO_BIDONI_UMIDO,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='2') " _
                                & "  ) * (1- nvl((SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (1+ nvl((SELECT SCONTO_PUL FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='07'),0)/100) " _
                                & " * (1+ nvl((SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) AS TOT_ANNUO_SCONTATO," _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As SCONTO," _
                                & " (Select IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As IVA," _
                                & " ((NVL(NUMERO_BIDONI_VETRO,0)+NVL(NUMERO_BIDONI_CARTA,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='1') " _
                                & " +2*(NVL(NUMERO_BIDONI_UMIDO,0))*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='9' AND TIPOLOGIA='2') " _
                                & " )* (1- nvl((SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (1+ nvl((SELECT SCONTO_PUL FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='07'),0)/100) " _
                                & " * (1+ nvl((SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (SELECT ROUND (NVL (PERCENTUALE, 0), 10) FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE id_gruppo=appalti_lotti_patrimonio.ID_APPALTO " _
                                & " And ID_PF = " & idElaborazione _
                                & " And VOCE = '07') " _
                                & " *nvl((SELECT MESI FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='07'),0) " _
                                & " As TOT_ANNUO_SCONTATO_RETT, '' AS TOTALE_LORDO " _
                                & " FROM SISCOM_MI.EDIFICI,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                & " WHERE EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                                & " AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1) " _
                                & " AND ID <> 1 AND EDIFICI.FL_IN_CONDOMINIO = 0  ORDER BY EDIFICI.DENOMINAZIONE ASC"
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
            par.cmd.CommandText = "SELECT ID," _
                                & " (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO," _
                                & " (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO," _
                                & " (SELECT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_FORNITORE," _
                                & " COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE,replace(MQ_ESTERNI,',','.') as mq_esterni,replace(MQ_PILOTY,',','.') as MQ_PILOTY, " _
                                & " NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, " _
                                & " (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI.ID_COMPLESSO)) AS SEDE_TERRITORIALE," _
                                & " NVL((SELECT " _
                                & " SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='10'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='1'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='2'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='3'))" _
                                & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='13'))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID),0) AS IMPORTO_SCALA,0 AS IMPORTO_EDIFICIO, " _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
                                & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
                                & " (SELECT " _
                                & " SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='10'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='1'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='2'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='3'))" _
                                & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='13'))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID) AS TOTALE_EDIFICIO, " _
                                & " (SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS SCONTO," _
                                & " (SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS IVA," _
                                & " (SELECT " _
                                & " SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='10'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='1'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='2'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='3'))" _
                                & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='13'))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID) " _
                                & " * (1- nvl((SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (1+ nvl((SELECT SCONTO_PUL FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='15'),0)/100) " _
                                & " * (1+ nvl((SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) AS TOT_ANNUO_SCONTATO," _
                                & "(SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As SCONTO," _
                                & " (Select IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) As IVA," _
                                & " (SELECT " _
                                & " SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='10'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='1'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='2'))" _
                                & " +SUM(SCALE_EDIFICI.N_UNITA*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='12' AND TIPOLOGIA='3'))" _
                                & " +SUM(1*(SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO='13'))" _
                                & " FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_eDIFICIO=EDIFICI.ID) " _
                                & " * (1- nvl((SELECT SCONTO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 AND ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (1+ nvl((SELECT SCONTO_PUL FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='15'),0)/100) " _
                                & " * (1+ nvl((SELECT IVA FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1 And ID_PF=" & idElaborazione & " And ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO),0)/100) " _
                                & " * (SELECT ROUND (NVL (PERCENTUALE, 0), 10) FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE id_gruppo=appalti_lotti_patrimonio.ID_APPALTO " _
                                & " And ID_PF = " & idElaborazione _
                                & " And VOCE = '15')" _
                                & " *nvl((SELECT MESI FROM SISCOM_MI.IMPUTAZIONE_PULIZIE WHERE ID_PF=" & idElaborazione & " AND ID_GRUPPO = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO AND VOCE='15'),0) " _
                                & " As TOT_ANNUO_SCONTATO_RETT, '' AS TOTALE_LORDO " _
                                & " FROM SISCOM_MI.EDIFICI,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                & " WHERE EDIFICI.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                                & " AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO IN (SELECT ID_GRUPPO FROM SISCOM_MI.IMPUTAZIONE_APPALTI WHERE ID_TIPO_SPESA=1) " _
                                & " AND ID <> 1 AND EDIFICI.FL_IN_CONDOMINIO = 0  ORDER BY EDIFICI.DENOMINAZIONE ASC"
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
