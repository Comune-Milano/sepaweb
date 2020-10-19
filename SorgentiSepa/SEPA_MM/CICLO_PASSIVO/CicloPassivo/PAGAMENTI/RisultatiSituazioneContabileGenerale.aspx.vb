
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiSituazioneContabileGenerale
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
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
            par.cmd.CommandText = "select substr(inizio,1,4) as anno,inizio as inizioanno1 ,fine as fineanno1 " _
                & " from siscom_mi.pf_main,siscom_mi.t_Esercizio_finanziario " _
                & " where(PF_MAIN.ID_ESERCIZIO_FINANZIARIO = t_Esercizio_finanziario.id) " _
                & " and pf_main.id=" & Request.QueryString("ID")
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim anno1 As String = ""
            Dim inizioAnno1 As String = ""
            Dim fineAnno1 As String = ""
            Dim anno2 As String = ""
            Dim inizioAnno2 As String = ""
            Dim fineAnno2 As String = ""
            If lettore.Read Then
                anno1 = par.IfNull(lettore("anno"), 0)
                inizioAnno1 = par.IfNull(lettore("inizioanno1"), 0)
                fineAnno1 = par.IfNull(lettore("fineanno1"), 0)
            End If
            lettore.Close()
            anno2 = CStr(CInt(anno1) + 1)
            inizioAnno2 = Replace(inizioAnno1, anno1, anno2)
            fineAnno2 = Replace(fineAnno1, anno1, anno2)

            If Not IsNothing(Request.QueryString("dal")) AndAlso IsDate(Request.QueryString("dal")) Then
                fineAnno1 = par.FormatoDataDB(Request.QueryString("dal"))
            End If

            par.cmd.CommandText = "SELECT " _
                & "(CASE WHEN ((CASE WHEN PF_VOCI.ID IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) THEN 1 ELSE 0 END))=1 THEN((SELECT PF_CAPITOLI.COD FROM SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO)) ELSE NULL END) AS CAPITOLO, " _
                & "PF_VOCI.CODICE, " _
                & "PF_VOCI.DESCRIZIONE, " _
                & "TRIM(TO_CHAR(NVL((SELECT SUM(PF_VOCI_STRUTTURA.VALORE_LORDO) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS BUDGET, " _
                & "TRIM(TO_CHAR(NVL((SELECT SUM(PF_VOCI_STRUTTURA.VALORE_LORDO)+SUM(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO)+SUM(VARIAZIONI) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS ASSESTATO, " _
                & "TRIM(TO_CHAR(NVL((SELECT SUM(PF_VOCI_STRUTTURA.VALORE_LORDO)+SUM(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO)+SUM(VARIAZIONI) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
                & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) " _
                & "AS RESIDUO, " _
                & "TRIM(TO_CHAR(NVL((SELECT SUM(PF_VOCI_STRUTTURA.VALORE_LORDO)+SUM(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO)+SUM(VARIAZIONI) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
                & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) " _
                & "AS RESIDUO_TOTALE, " _
                & "/*TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PRENOTATO, " _
                & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=0 AND PRENOTAZIONI.TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PRENOTATO_ODL, " _
                & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=0 AND PRENOTAZIONI.TIPO_PAGAMENTO=6 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PRENOTATO_CANONE, " _
                & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=0 AND PRENOTAZIONI.TIPO_PAGAMENTO=1 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PRENOTATO_CONDOMINI, " _
                & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=0 AND PRENOTAZIONI.TIPO_PAGAMENTO NOT IN (3,6,1) AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PRENOTATO_ALTRO,*/ " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=1&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND  ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS PRENOTATO, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=1&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=3&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND ID_STATO=0 AND PRENOTAZIONI.TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS PRENOTATO_ODL, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=1&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=6&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND ID_STATO=0 AND PRENOTAZIONI.TIPO_PAGAMENTO=6 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS PRENOTATO_CANONE, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=1&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND ID_STATO=0 AND PRENOTAZIONI.TIPO_PAGAMENTO=1 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS PRENOTATO_CONDOMINI, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=1&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=0&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND ID_STATO=0 AND PRENOTAZIONI.TIPO_PAGAMENTO NOT IN (3,6,1) AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS PRENOTATO_ALTRO, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=2&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS CONSUNTIVATO1, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=3&ANNO1=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS CONSUNTIVATO2, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=4&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS CONSUNTIVATO, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=5&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS RITCONSUNTIVATO1, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=6&ANNO1=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS RITCONSUNTIVATO2, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=7&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) ||'</a>' AS RITCONSUNTIVATO, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=8&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS CERTIFICATO1, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=9&ANNO1=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS CERTIFICATO2, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=10&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS CERTIFICATO, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=18&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS RITCERTIFICATO1, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=19&ANNO1=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS RITCERTIFICATO2, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=20&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS RITCERTIFICATO, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=11&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(PAGAMENTI_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.DATA_MANDATO BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) ||'</a>' AS PAGATO1, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=12&ANNO1=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(PAGAMENTI_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.DATA_MANDATO >='" & inizioAnno2 & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) ||'</a>' AS PAGATO2, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=13&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(PAGAMENTI_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.DATA_MANDATO BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(PAGAMENTI_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.DATA_MANDATO >='" & inizioAnno2 & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) ||'</a>' AS PAGATO, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=14&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(PAGAMENTI_RIT_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE PAGAMENTI_RIT_LIQUIDATI.DATA_MANDATO BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PAGAMENTI_RIT_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS RITPAGATO1, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=15&ANNO1=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(PAGAMENTI_RIT_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE PAGAMENTI_RIT_LIQUIDATI.DATA_MANDATO >='" & inizioAnno2 & "' AND PAGAMENTI_RIT_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS RITPAGATO2, " _
                & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGenerale.aspx" & Request.Url.Query & "&T=16&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(PAGAMENTI_RIT_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE PAGAMENTI_RIT_LIQUIDATI.DATA_MANDATO BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PAGAMENTI_RIT_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(PAGAMENTI_RIT_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE PAGAMENTI_RIT_LIQUIDATI.DATA_MANDATO >='" & inizioAnno2 & "' AND PAGAMENTI_RIT_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS RITPAGATO " _
                & "FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " ORDER BY PF_VOCI.CODICE "

            

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                DataGridEs.DataSource = dt
                For Each colonna As DataGridColumn In DataGridEs.Columns
                    colonna.HeaderText = Replace(colonna.HeaderText, "ANNO1", anno1)
                    colonna.HeaderText = Replace(colonna.HeaderText, "ANNO2", anno2)
                Next
                DataGridEs.DataBind()
                ImageButtonEsporta.Visible = True
                DataGridEs.Visible = True
                lblRis.Visible = False
            Else
                ImageButtonEsporta.Visible = False
                DataGridEs.Visible = False
                lblRis.Visible = True
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
            'Dim nomeFile = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSituazioneContabile", "ExportSituazioneContabile", DataGridEs)
            Dim nomeFile = par.EsportaExcelAutomaticoDaDataGrid(DataGridEs, "ExportSituazioneContabile", , , "", False)
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
End Class
