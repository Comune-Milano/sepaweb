
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_DettaglioRisultatiSituazioneContabileGenerale
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Public totale As Decimal = 0
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            caricaRisultati()
            If Not IsNothing(Request.QueryString("ES")) Then
                lblTitolo.Text &= " " & Request.QueryString("ES")
            End If
        End If
    End Sub

    Private Sub caricaRisultati()
        Try
            Dim id As String = "0"
            If Not IsNothing(Request.QueryString("ID")) Then
                id = Request.QueryString("ID")
            End If
            connData.apri()
            If Request.QueryString("T") = "1" Then
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                'PRENOTATO
                Dim TIPO As Integer = -1
                Dim condizioneTipo As String = ""
                If Not IsNothing(Request.QueryString("TIPO")) Then
                    Select Case Request.QueryString("TIPO")
                        Case -1
                            condizioneTipo = ""
                        Case 0
                            condizioneTipo = " AND PRENOTAZIONI.TIPO_PAGAMENTO NOT IN (1,3,6) "
                        Case 1
                            condizioneTipo = " AND PRENOTAZIONI.TIPO_PAGAMENTO=1 "
                        Case 3
                            condizioneTipo = " AND PRENOTAZIONI.TIPO_PAGAMENTO=3 "
                        Case 6
                            condizioneTipo = " AND PRENOTAZIONI.TIPO_PAGAMENTO=6 "
                        Case Else
                            condizioneTipo = ""
                    End Select
                End If
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(NVL(IMPORTO_PRENOTATO,0),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND ID_STATO=0 " & condizioneTipo & " AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                    If Not IsNothing(Request.QueryString("TIPO")) Then
                        Select Case Request.QueryString("TIPO")
                            Case -1
                                condizioneTipo = ""
                            Case 0
                                condizioneTipo = " AND PRENOTAZIONI.TIPO_PAGAMENTO NOT IN (1,3,6) "
                                DataGridEs.Columns(par.IndDGC(DataGridEs, "ODL")).Visible = False
                                DataGridEs.Columns(par.IndDGC(DataGridEs, "DATA_SCADENZA")).Visible = False
                            Case 1
                                condizioneTipo = " AND PRENOTAZIONI.TIPO_PAGAMENTO=1 "
                                DataGridEs.Columns(par.IndDGC(DataGridEs, "ODL")).Visible = False
                                DataGridEs.Columns(par.IndDGC(DataGridEs, "DATA_SCADENZA")).Visible = False
                            Case 3
                                condizioneTipo = " AND PRENOTAZIONI.TIPO_PAGAMENTO=3 "
                                DataGridEs.Columns(par.IndDGC(DataGridEs, "DATA_SCADENZA")).Visible = False
                            Case 6
                                condizioneTipo = " AND PRENOTAZIONI.TIPO_PAGAMENTO=6 "
                                DataGridEs.Columns(par.IndDGC(DataGridEs, "ODL")).Visible = False
                            Case Else
                                condizioneTipo = ""
                        End Select
                    End If
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "2" Then
                'CONSUNTIVATO ANNO1
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "3" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno2 As String = Request.QueryString("ANNO1")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE>= " & inizioAnno2 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "4" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                Dim inizioAnno2 As String = Request.QueryString("ANNO3")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " UNION " _
                    & "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(IMPORTO_APPROVATO, 2)- ROUND (NVL(RIT_LEGGE_IVATA, 0), 2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE>= " & inizioAnno2 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "5" Then
                'CONSUNTIVATO ANNO1
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " AND ROUND(NVL(RIT_LEGGE_IVATA,0),2)<>0 " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "6" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno2 As String = Request.QueryString("ANNO1")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE>= " & inizioAnno2 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " AND ROUND(NVL(RIT_LEGGE_IVATA,0),2)<>0 " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "7" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                Dim inizioAnno2 As String = Request.QueryString("ANNO3")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " AND ROUND(NVL(RIT_LEGGE_IVATA,0),2)<>0 " _
                    & " UNION " _
                    & "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(NVL(IMPORTO_PRENOTATO,0),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE>= " & inizioAnno2 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " AND ROUND(NVL(RIT_LEGGE_IVATA,0),2)<>0 " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "8" Then
                'CONSUNTIVATO ANNO1
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "9" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno2 As String = Request.QueryString("ANNO1")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE>= " & inizioAnno2 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "10" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                Dim inizioAnno2 As String = Request.QueryString("ANNO3")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " UNION " _
                    & "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE>= " & inizioAnno2 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "18" Then
                'CONSUNTIVATO ANNO1
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "19" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno2 As String = Request.QueryString("ANNO1")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE>= " & inizioAnno2 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "20" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                Dim inizioAnno2 As String = Request.QueryString("ANNO3")
                par.cmd.CommandText = "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " UNION " _
                    & "SELECT " _
                    & " ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=ID_VOCE_PF) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE>= " & inizioAnno2 & "  AND PRENOTAZIONI.ID_VOCE_PF IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "11" Then
                'CONSUNTIVATO ANNO1
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                par.cmd.CommandText = "SELECT " _
                    & " PRENOTAZIONI.ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=prenotazioni.id_Struttura) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(PAGAMENTI_LIQUIDATI.IMPORTO,2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.id_prenotazione=PRENOTAZIONI.ID and pagamenti_liquidati.id_Struttura=prenotazioni.id_struttura and pagamenti_liquidati.id_voce_pf=prenotazioni.id_voce_pf  /*AND ID_STATO>=2*/ AND DATA_MANDATO BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.id_voce_pf IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "12" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno2 As String = Request.QueryString("ANNO1")
                par.cmd.CommandText = "SELECT " _
                    & " PRENOTAZIONI.ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=prenotazioni.id_Struttura) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(PAGAMENTI_LIQUIDATI.IMPORTO,2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.id_prenotazione=PRENOTAZIONI.ID and pagamenti_liquidati.id_Struttura=prenotazioni.id_struttura and pagamenti_liquidati.id_voce_pf=prenotazioni.id_voce_pf  /*AND ID_STATO>=2*/ AND DATA_MANDATO>= " & inizioAnno2 & "  AND PRENOTAZIONI.id_voce_pf IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "13" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                Dim inizioAnno2 As String = Request.QueryString("ANNO3")
                par.cmd.CommandText = "SELECT " _
                    & " PRENOTAZIONI.ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=prenotazioni.id_Struttura) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(PAGAMENTI_LIQUIDATI.IMPORTO,2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.id_prenotazione=PRENOTAZIONI.ID and pagamenti_liquidati.id_Struttura=prenotazioni.id_struttura and pagamenti_liquidati.id_voce_pf=prenotazioni.id_voce_pf  /*AND ID_STATO>=2*/ AND DATA_MANDATO BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.id_voce_pf IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " UNION " _
                    & "SELECT " _
                    & " prenotazioni.ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=prenotazioni.id_Struttura) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(PAGAMENTI_LIQUIDATI.importo,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.id_prenotazione=PRENOTAZIONI.ID and pagamenti_liquidati.id_Struttura=prenotazioni.id_struttura and pagamenti_liquidati.id_voce_pf=prenotazioni.id_voce_pf  /*AND ID_STATO>=2*/ AND DATA_MANDATO>= " & inizioAnno2 & "  AND PRENOTAZIONI.id_voce_pf IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "14" Then
                'CONSUNTIVATO ANNO1
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                par.cmd.CommandText = "SELECT " _
                    & " PRENOTAZIONI.ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=prenotazioni.id_Struttura) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(pagamenti_rit_liquidati.IMPORTO,2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.pagamenti_rit_liquidati WHERE PAGAMENTI_rit_LIQUIDATI.id_prenotazione=PRENOTAZIONI.ID and pagamenti_rit_liquidati.id_Struttura=prenotazioni.id_struttura and pagamenti_rit_liquidati.id_voce_pf=prenotazioni.id_voce_pf  /*AND ID_STATO>=2*/ AND DATA_MANDATO BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.id_voce_pf IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "15" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno2 As String = Request.QueryString("ANNO1")
                par.cmd.CommandText = "SELECT " _
                    & " PRENOTAZIONI.ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=prenotazioni.id_Struttura) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(pagamenti_rit_liquidati.IMPORTO,2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.pagamenti_rit_liquidati WHERE PAGAMENTI_rit_LIQUIDATI.id_prenotazione=PRENOTAZIONI.ID and pagamenti_rit_liquidati.id_Struttura=prenotazioni.id_struttura and pagamenti_rit_liquidati.id_voce_pf=prenotazioni.id_voce_pf  /*AND ID_STATO>=2*/ AND DATA_MANDATO>= " & inizioAnno2 & "  AND PRENOTAZIONI.id_voce_pf IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            ElseIf Request.QueryString("T") = "16" Then
                'CONSUNTIVATO ANNO2
                Dim inizioAnno1 As String = Request.QueryString("ANNO1")
                Dim fineAnno1 As String = Request.QueryString("ANNO2")
                Dim inizioAnno2 As String = Request.QueryString("ANNO3")
                par.cmd.CommandText = "SELECT " _
                    & " PRENOTAZIONI.ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=prenotazioni.id_Struttura) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(pagamenti_rit_liquidati.IMPORTO,2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.pagamenti_rit_liquidati WHERE PAGAMENTI_rit_LIQUIDATI.id_prenotazione=PRENOTAZIONI.ID and pagamenti_rit_liquidati.id_Struttura=prenotazioni.id_struttura and pagamenti_rit_liquidati.id_voce_pf=prenotazioni.id_voce_pf  /*AND ID_STATO>=2*/ AND DATA_MANDATO BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & "  AND PRENOTAZIONI.id_voce_pf IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " UNION " _
                    & "SELECT " _
                    & " prenotazioni.ID " _
                    & " ,(SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  CODICE " _
                    & " ,(SELECT PF_VOCI.DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=prenotazioni.id_voce_pf) AS  VOCE " _
                    & " ,(SELECT TIPO_PAGAMENTI.DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE TIPO_PAGAMENTI.ID=TIPO_PAGAMENTO) AS  TIPO_PAGAMENTO " _
                    & " ,PRENOTAZIONI.DESCRIZIONE " _
                    & " ,(SELECT TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=prenotazioni.id_Struttura) AS SEDE_TERRITORIALE " _
                    & " ,TRIM(TO_CHAR(ROUND(PAGAMENTI_LIQUIDATI.importo,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS IMPORTO_PRENOTATO " _
                    & " ,(SELECT APPALTI.NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=ID_APPALTO) AS REPERTORIO " _
                    & " ,(SELECT FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS FORNITORE " _
                    & " ,(select manutenzioni.progr||'/'||manutenzioni.anno from siscom_mi.manutenzioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) as odl " _
                    & " ,SISCOM_MI.GETDATA(PRENOTAZIONI.DATA_SCADENZA) AS DATA_SCADENZA " _
                    & " FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.pagamenti_rit_liquidati WHERE PAGAMENTI_rit_LIQUIDATI.id_prenotazione=PRENOTAZIONI.ID and pagamenti_rit_liquidati.id_Struttura=prenotazioni.id_struttura and pagamenti_rit_liquidati.id_voce_pf=prenotazioni.id_voce_pf  /*AND ID_STATO>=2*/ AND DATA_MANDATO>= " & inizioAnno2 & "  AND PRENOTAZIONI.id_voce_pf IN " _
                    & " (SELECT iD FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND PF_VOCI.ID IN " _
                    & " (SELECT B.ID FROM SISCOM_MI.PF_VOCI B WHERE CONNECT_BY_ISLEAF=1 CONNECT BY PRIOR B.ID=B.ID_VOCE_MADRE START WITH B.ID=" & Request.QueryString("ID_vOCE") & ")) " _
                    & " ORDER BY 2 ASC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEs.DataSource = dt
                    DataGridEs.DataBind()
                    ImageButtonEsporta.Visible = True
                    DataGridEs.Visible = True
                    lblRis.Visible = False
                Else
                    ImageButtonEsporta.Visible = False
                    DataGridEs.Visible = False
                    lblRis.Visible = True
                End If
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - caricaRisultati - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub ImageButtonEsporta_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsporta.Click
        Try
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSituazioneContabile", "ExportSituazioneContabile", DataGridEs)
            If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
                Response.Redirect("../../../FileTemp/" & nomeFile, False)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('..\/..\/..\/FileTemp\/" & nomeFile & "','','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - ImageButtonEsporta_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGridEs_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridEs.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            totale += CDec(e.Item.Cells(par.IndDGC(DataGridEs, "IMPORTO_PRENOTATO")).Text)
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(par.IndDGC(DataGridEs, "IMPORTO_PRENOTATO")).Text = Format(totale, "#,##0.00")
        End If
    End Sub
End Class
