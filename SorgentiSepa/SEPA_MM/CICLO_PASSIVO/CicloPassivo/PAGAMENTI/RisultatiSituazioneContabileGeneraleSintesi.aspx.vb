Imports Telerik.Web.UI
Imports Telerik.Web.UI.GridExcelBuilder

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiSituazioneContabileGeneraleSintesi
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
            'caricaRisultati()
            If Not IsNothing(Request.QueryString("ES")) Then
                lblTitolo.Text &= " " & Request.QueryString("ES")
            End If
        End If
    End Sub
    'Private Sub caricaRisultati()
    '    Try
    '        Dim id As String = "0"
    '        If Not IsNothing(Request.QueryString("ID")) Then
    '            id = Request.QueryString("ID")
    '        End If
    '        connData.apri()
    '        par.cmd.CommandText = "select substr(inizio,1,4) as anno,inizio as inizioanno1 ,fine as fineanno1 " _
    '            & " from siscom_mi.pf_main,siscom_mi.t_Esercizio_finanziario " _
    '            & " where(PF_MAIN.ID_ESERCIZIO_FINANZIARIO = t_Esercizio_finanziario.id) " _
    '            & " and pf_main.id=" & Request.QueryString("ID")
    '        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '        Dim anno1 As String = ""
    '        Dim inizioAnno1 As String = ""
    '        Dim fineAnno1 As String = ""
    '        Dim anno2 As String = ""
    '        Dim inizioAnno2 As String = ""
    '        Dim fineAnno2 As String = ""
    '        If lettore.Read Then
    '            anno1 = par.IfNull(lettore("anno"), 0)
    '            inizioAnno1 = par.IfNull(lettore("inizioanno1"), 0)
    '            fineAnno1 = par.IfNull(lettore("fineanno1"), 0)
    '        End If
    '        lettore.Close()
    '        anno2 = CStr(CInt(anno1) + 1)
    '        inizioAnno2 = Replace(inizioAnno1, anno1, anno2)
    '        fineAnno2 = Replace(fineAnno1, anno1, anno2)

    '        If Not IsNothing(Request.QueryString("dal")) AndAlso IsDate(Request.QueryString("dal")) Then
    '            fineAnno1 = par.FormatoDataDB(Request.QueryString("dal"))
    '        End If

    '        par.cmd.CommandText = "SELECT PF_VOCI.ID," _
    '            & "(CASE WHEN ((CASE WHEN PF_VOCI.ID IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) THEN 1 ELSE 0 END))=1 THEN((SELECT PF_CAPITOLI.COD FROM SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO)) ELSE NULL END) AS CAPITOLO, " _
    '            & "PF_VOCI.CODICE, " _
    '            & "PF_VOCI.DESCRIZIONE, " _
    '            & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS BUDGET, " _
    '            & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS ASSESTATO, " _
    '            & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
    '            & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
    '            & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
    '            & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
    '            & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) " _
    '            & "AS RESIDUO, " _
    '            & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
    '            & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
    '            & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
    '            & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
    '            & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
    '            & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) " _
    '            & "AS RESIDUO_TOTALE, " _
    '            & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGeneraleSintesi.aspx" & Request.Url.Query & "&T=1&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND  ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS PRENOTATO, " _
    '            & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGeneraleSintesi.aspx" & Request.Url.Query & "&T=4&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS CONSUNTIVATO, " _
    '            & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGeneraleSintesi.aspx" & Request.Url.Query & "&T=7&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) ||'</a>' AS RITCONSUNTIVATO, " _
    '            & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGeneraleSintesi.aspx" & Request.Url.Query & "&T=10&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS CERTIFICATO, " _
    '            & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGeneraleSintesi.aspx" & Request.Url.Query & "&T=13&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(PAGAMENTI_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.DATA_MANDATO BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(PAGAMENTI_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.DATA_MANDATO >='" & inizioAnno2 & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) ||'</a>' AS PAGATO, " _
    '            & "'' AS IVA, " _
    '            & "'' AS TOTPAGATO " _
    '            & "FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " ORDER BY PF_VOCI.CODICE "

    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim dt As New Data.DataTable
    '        da.Fill(dt)
    '        da.Dispose()
    '        'Dim dtFinale As New Data.DataTable
    '        'dtFinale = dt.Clone
    '        'Dim riga As Data.DataRow
    '        'For Each elemento As Data.DataRow In dt.Rows
    '        '    riga = dtFinale.NewRow
    '        '    riga.Item("ID") = elemento.Item("ID")
    '        '    riga.Item("CAPITOLO") = elemento.Item("CAPITOLO")
    '        '    riga.Item("CODICE") = elemento.Item("CAPITOLO")
    '        '    riga.Item("DESCRIZIONE") = elemento.Item("CAPITOLO")
    '        '    riga.Item("BUDGET") = elemento.Item("CAPITOLO")
    '        '    riga.Item("ASSESTATO") = elemento.Item("CAPITOLO")
    '        '    riga.Item("RESIDUO") = elemento.Item("CAPITOLO")
    '        '    riga.Item("RESIDUO_TOTALE") = elemento.Item("CAPITOLO")
    '        '    riga.Item("PRENOTATO") = elemento.Item("CAPITOLO")
    '        '    riga.Item("CONSUNTIVATO") = elemento.Item("CAPITOLO")
    '        '    riga.Item("RITCONSUNTIVATO") = elemento.Item("CAPITOLO")
    '        '    riga.Item("CERTIFICATO") = elemento.Item("CAPITOLO")
    '        '    riga.Item("PAGATO") = elemento.Item("CAPITOLO")
    '        '    riga.Item("IVA") = elemento.Item("CAPITOLO")
    '        '    riga.Item("TOTPAGATO") = elemento.Item("CAPITOLO")
    '        '    dtFinale.Rows.Add(riga)
    '        'Next
    '        If dt.Rows.Count > 0 Then
    '            DataGridEs.DataSource = dt
    '            DataGridEs.DataBind()
    '            ImageButtonEsporta.Visible = True
    '            DataGridEs.Visible = True
    '        Else
    '            ImageButtonEsporta.Visible = False
    '            DataGridEs.Visible = False
    '        End If
    '        connData.chiudi()
    '    Catch ex As Exception
    '        connData.chiudi()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - caricaRisultati - " & ex.Message)
    '        Response.Redirect("../../../Errore.aspx", False)
    '    End Try
    'End Sub

    
    Protected Sub DataGridEs_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles DataGridEs.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Dettagli"
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
                'connData.chiudi()
                anno2 = CStr(CInt(anno1) + 1)
                inizioAnno2 = Replace(inizioAnno1, anno1, anno2)
                fineAnno2 = Replace(fineAnno1, anno1, anno2)

                If Not IsNothing(Request.QueryString("dal")) AndAlso IsDate(Request.QueryString("dal")) Then
                    fineAnno1 = par.FormatoDataDB(Request.QueryString("dal"))
                End If

                Dim cdpal As String = ""
                Dim fatal As String = ""
                Dim regfatal As String = ""
                Dim pagal As String = ""
                If Not IsNothing(Request.QueryString("cdpal")) AndAlso IsDate(Request.QueryString("cdpal")) Then
                    cdpal = par.FormatoDataDB(Request.QueryString("cdpal"))
                End If
                If Not IsNothing(Request.QueryString("fatal")) AndAlso IsDate(Request.QueryString("fatal")) Then
                    fatal = par.FormatoDataDB(Request.QueryString("fatal"))
                End If
                If Not IsNothing(Request.QueryString("regfatal")) AndAlso IsDate(Request.QueryString("regfatal")) Then
                    regfatal = par.FormatoDataDB(Request.QueryString("regfatal"))
                End If
                If Not IsNothing(Request.QueryString("pagal")) AndAlso IsDate(Request.QueryString("pagal")) Then
                    pagal = par.FormatoDataDB(Request.QueryString("pagal"))
                End If
                Dim cdpdal As String = ""
                Dim fatdal As String = ""
                Dim regfatdal As String = ""
                Dim pagdal As String = ""
                If Not IsNothing(Request.QueryString("cdpdal")) AndAlso IsDate(Request.QueryString("cdpdal")) Then
                    cdpdal = par.FormatoDataDB(Request.QueryString("cdpdal"))
                End If
                If Not IsNothing(Request.QueryString("fatdal")) AndAlso IsDate(Request.QueryString("fatdal")) Then
                    fatdal = par.FormatoDataDB(Request.QueryString("fatdal"))
                End If
                If Not IsNothing(Request.QueryString("regfatdal")) AndAlso IsDate(Request.QueryString("regfatdal")) Then
                    regfatdal = par.FormatoDataDB(Request.QueryString("regfatdal"))
                End If
                If Not IsNothing(Request.QueryString("pagdal")) AndAlso IsDate(Request.QueryString("pagdal")) Then
                    pagdal = par.FormatoDataDB(Request.QueryString("pagdal"))
                End If

                Dim dataFattura As String = ""
                If Len(fatdal) = 8 Then
                    dataFattura &= " AND FATTURE_MM.DATA_FATTURA>='" & fatdal.ToString & "'"
                End If
                If Len(regfatdal) = 8 Then
                    dataFattura &= " AND FATTURE_MM.DATA_SCADENZA>='" & regfatdal.ToString & "'"
                End If
                If Len(fatal) = 8 Then
                    dataFattura &= " AND FATTURE_MM.DATA_FATTURA<='" & fatal.ToString & "'"
                End If
                If Len(regfatal) = 8 Then
                    dataFattura &= " AND FATTURE_MM.DATA_SCADENZA<='" & regfatal.ToString & "'"
                End If

                Dim dataPagamento As String = ""
                If Len(pagdal) = 8 Then
                    dataPagamento &= " AND PAGAMENTI_MM.DATA_PAGAMENTO>='" & pagdal & "' "
                End If
                If Len(pagal) = 8 Then
                    dataPagamento &= " AND PAGAMENTI_MM.DATA_PAGAMENTO<='" & pagal & "' "
                End If

                Dim condizioneDataFattura As String = ""
                Dim condizioneDataFattura2 As String = ""
                If dataFattura <> "" Then
                    condizioneDataFattura = " EXISTS(SELECT ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM WHERE PRENOTAZIONI.ID_PAGAMENTO=FATTURE_MM.ID_PAGAMENTO " & dataFattura & ") AND "
                    condizioneDataFattura2 = " AND EXISTS(SELECT ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM WHERE PRENOTAZIONI.ID_PAGAMENTO=FATTURE_MM.ID_PAGAMENTO " & dataFattura & ") "
                End If
                Dim condizioneDataPagamento As String = ""
                If dataPagamento <> "" Then
                    condizioneDataPagamento = " EXISTS(SELECT ID_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI_MM.ID_PAGAMENTO " & dataPagamento & ") AND "
                End If

                Dim condizioneCdp As String = ""
                Dim condizioneCdpPagamentiMM As String = ""
                Dim condizioneCdpFattureMM As String = ""
                If cdpdal <> "" Then
                    condizioneCdp &= " EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE>='" & cdpdal.ToString & "') AND "
                    condizioneCdpPagamentiMM &= " EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE PAGAMENTI_MM.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE>='" & cdpdal.ToString & "') AND "
                    condizioneCdpFattureMM &= " AND EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE FATTURE_MM.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE>='" & cdpdal.ToString & "') "
                End If
                If cdpal <> "" Then
                    condizioneCdp &= " EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE<='" & cdpal.ToString & "') AND "
                    condizioneCdpPagamentiMM &= " EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE PAGAMENTI_MM.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE<='" & cdpal.ToString & "') AND "
                    condizioneCdpFattureMM &= " AND EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE FATTURE_MM.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE<='" & cdpal.ToString & "') "
                End If
                Dim condizioneFatture As String = ""
                If condizioneCdpFattureMM <> "" Or condizioneDataFattura2 <> "" Then
                    condizioneFatture = condizioneCdpFattureMM & condizioneDataFattura2
                End If
                par.cmd.CommandText = "SELECT PF_VOCI.ID," _
                    & "(CASE WHEN ((CASE WHEN PF_VOCI.ID IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) THEN 1 ELSE 0 END))=1 THEN((SELECT PF_CAPITOLI.COD FROM SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO)) ELSE NULL END) AS CAPITOLO, " _
                    & "PF_VOCI.CODICE, " _
                    & "PF_VOCI_IMPORTO.DESCRIZIONE, " _
                    & "NULL/*NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)*/ AS BUDGET, " _
                    & "NULL/*NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)*/ AS ASSESTATO, " _
                    & "NULL/*NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
                    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                    & "*/AS RESIDUO, " _
                    & "NULL/*NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
                    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                    & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO=0 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                    & "*/AS RESIDUO_TOTALE, " _
                    & "NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2))-SUM(ROUND(ANTICIPO_CONTRATTUALE_CON_IVA,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND  ID_STATO=0 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS PRENOTATO, " _
                    & "NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS CONSUNTIVATO, " _
                    & "NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS RITCONSUNTIVATO, " _
                    & "NVL((SELECT SUM(nvl(imponibile,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(nvl(imponibile,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IMPONIBILE_CERTIFICATO, " _
                    & "NVL((SELECT SUM(nvl(IVA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(nvl(IVA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IVA_CERTIFICATA, " _
                    & "NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2))-SUM(ROUND(ANTICIPO_CONTRATTUALE_CON_IVA,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2))-SUM(ROUND(ANTICIPO_CONTRATTUALE_CON_IVA,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS CERTIFICATO, " _
                    & "NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0)*100/(100+NVL(PERC_IVA,0)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_cERT_RIT_LEGGE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0)*100/(100+NVL(PERC_IVA,0)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERT_RIT_LEGGE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IMPONIBILE_CERTIFICATO_RIT, " _
                    & "NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0)-nvl(RIT_LEGGE_IVATA,0)*100/(100+NVL(PERC_IVA,0)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_cERT_RIT_LEGGE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0)-nvl(RIT_LEGGE_IVATA,0)*100/(100+NVL(PERC_IVA,0)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERT_RIT_LEGGE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IVA_CERTIFICATA_RIT, " _
                    & "NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_cERT_RIT_LEGGE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERT_RIT_LEGGE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS CERTIFICATO_RIT, " _
                    & "NVL(ROUND((SELECT sum((IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0))/(SELECT SUM(IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI B WHERE B.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)*FATTURE_MM.IMPORTO_TOTALE) FROM SISCOM_MI.FATTURE_MM,SISCOM_MI.PRENOTAZIONI WHERE FATTURE_MM.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO AND ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) " & condizioneFatture & "),2),0) AS FATTURATO, " _
                    & "NVL(ROUND((SELECT sum((NVL(RIT_LEGGE_IVATA,0))/(SELECT SUM(NVL(RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI B WHERE B.ID_PAGAMENTO_RIT_LEGGE=PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE)*FATTURE_MM.IMPORTO_TOTALE) FROM SISCOM_MI.FATTURE_MM,SISCOM_MI.PRENOTAZIONI WHERE FATTURE_MM.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE AND ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) " & condizioneFatture & "),2),0) AS FATTURATO_RIT, " _
                    & "/*NVL((SELECT SUM(IMPONIBILE_LIQUIDATO) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS PAGATO,*/ " _
                    & "NVL((SELECT SUM(nvl(IMPORTO_LIQUIDATO,0)+round(NVL(IMPORTO_RIT_LIQUIDATO,0)*100/(100+NVL(PERC_IVA,0)),2)-nvl(iva_liquidata,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS PAGATO, " _
                    & "NVL((SELECT SUM(nvl(IVA_LIQUIDATA,0)+NVL(IMPORTO_RIT_LIQUIDATO,0)-round(NVL(IMPORTO_RIT_LIQUIDATO,0)*100/(100+NVL(PERC_IVA,0)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IVA, " _
                    & "NVL((SELECT SUM((CASE WHEN NOT EXISTS (SELECT FATTURE_MM.ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM WHERE FATTURE_MM.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO) THEN (case when imponibile<>0 then round((nvl(IMPORTO_LIQUIDATO,0)-nvl(iva_liquidata,0))/imponibile*iva,2) else 0 end) ELSE (case when exists (SELECT FATTURE_MM.ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM WHERE FATTURE_MM.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO AND COD_OC NOT IN (100,102)) THEN (case when imponibile<>0 then round((nvl(IMPORTO_LIQUIDATO,0)-nvl(iva_liquidata,0))/imponibile*iva,2) else 0 end) ELSE 0 END) END)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IVA_SPLIT, " _
                    & "NVL((SELECT SUM(nvl(IMPORTO_LIQUIDATO,0)+NVL(IMPORTO_RIT_LIQUIDATO,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS TOTPAGATO " _
                    & " FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_PIANO_FINANZIARIO=" & id & " and pf_voci.id=" & dataItem.Item("ID").Text & " AND PF_VOCI.ID=PF_VOCI_IMPORTO.ID_VOCE " _
                    & " AND (SELECT COUNT(*) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO<>-3 AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID)>0 " _
                    & " ORDER BY 3 "
                'par.cmd.CommandText = "SELECT PF_VOCI.ID," _
                '    & "(CASE WHEN ((CASE WHEN PF_VOCI.ID IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) THEN 1 ELSE 0 END))=1 THEN((SELECT PF_CAPITOLI.COD FROM SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO)) ELSE NULL END) AS CAPITOLO, " _
                '    & "PF_VOCI.CODICE, " _
                '    & "PF_VOCI.DESCRIZIONE, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS BUDGET, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS ASSESTATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
                '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) " _
                '    & "AS RESIDUO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
                '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO=0 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) " _
                '    & "AS RESIDUO_TOTALE, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND  ID_STATO=0 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PRENOTATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS CONSUNTIVATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS RITCONSUNTIVATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(imponibile,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(nvl(imponibile,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS IMPONIBILE_CERTIFICATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(IVA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(nvl(IVA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS IVA_CERTIFICATA, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS CERTIFICATO, " _
                '    & "TRIM(TO_CHAR(NVL(ROUND((SELECT sum((IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0))/(SELECT SUM(IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI B WHERE B.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)*FATTURE_MM.IMPORTO_TOTALE) FROM SISCOM_MI.FATTURE_MM,SISCOM_MI.PRENOTAZIONI WHERE FATTURE_MM.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO AND ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) " & condizioneCdpFattureMM & condizioneDataFattura & "),2),0),'999G999G990D99')) AS FATTURATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(IMPONIBILE_LIQUIDATO) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PAGATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(IVA_LIQUIDATA) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS IVA, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(IMPORTO_LIQUIDATO) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 and PRENOTAZIONI.ID_VOCE_PF_importo=pf_voci_importo.id AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS TOTPAGATO " _
                '    & " FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_PIANO_FINANZIARIO=" & id & " and pf_voci.id=" & dataItem.Item("ID").Text & " AND PF_VOCI.ID=PF_VOCI_IMPORTO.ID_VOCE " _
                '    & " AND (SELECT COUNT(*) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO<>-3 AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID)>0 " _
                '    & " ORDER BY 3 "
                'TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)
                'par.cmd.CommandText = "SELECT PF_VOCI.ID," _
                '    & "(CASE WHEN ((CASE WHEN PF_VOCI.ID IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) THEN 1 ELSE 0 END))=1 THEN((SELECT PF_CAPITOLI.COD FROM SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO)) ELSE NULL END) AS CAPITOLO, " _
                '    & "PF_VOCI.CODICE, " _
                '    & "PF_VOCI_IMPORTO.DESCRIZIONE, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS BUDGET, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS ASSESTATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
                '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) " _
                '    & "AS RESIDUO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
                '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                '    & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) " _
                '    & "AS RESIDUO_TOTALE, " _
                '    & "/*'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGeneraleSintesi.aspx" & Request.Url.Query & "&T=1&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND  ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS PRENOTATO, " _
                '    & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGeneraleSintesi.aspx" & Request.Url.Query & "&T=4&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS CONSUNTIVATO, " _
                '    & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGeneraleSintesi.aspx" & Request.Url.Query & "&T=7&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) ||'</a>' AS RITCONSUNTIVATO, " _
                '    & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGeneraleSintesi.aspx" & Request.Url.Query & "&T=10&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99'))||'</a>' AS CERTIFICATO, " _
                '    & "'<a href=""javascript:window.open(''DettaglioRisultatiSituazioneContabileGeneraleSintesi.aspx" & Request.Url.Query & "&T=13&ANNO1=" & inizioAnno1 & "&ANNO2=" & fineAnno1 & "&ANNO3=" & inizioAnno2 & "&TIPO=-1&ID_VOCE='||PF_VOCI.ID||''',''_blank'','''');void(0);"">'||TRIM(TO_CHAR(NVL((SELECT SUM(PAGAMENTI_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.DATA_MANDATO BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(PAGAMENTI_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.DATA_MANDATO >='" & inizioAnno2 & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) ||'</a>' AS PAGATO,*/" _
                '    & " " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND  ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PRENOTATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS CONSUNTIVATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS RITCONSUNTIVATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(IMPONIBILE_CERTIFICATO,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(nvl(IMPONIBILE_CERTIFICATO,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS IMPONIBILE_CERTIFICATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(IVA_CERTIFICATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(nvl(IVA_CERTIFICATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS IVA_CERTIFICATA, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS CERTIFICATO, " _
                '    & "/*TRIM(TO_CHAR(NVL((SELECT SUM(PAGAMENTI_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.DATA_MANDATO BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL(SELECT SUM(PAGAMENTI_LIQUIDATI.IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.DATA_MANDATO >='" & inizioAnno2 & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PAGATO,*/" _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(IMPONIBILE_LIQUIDATO) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(IMPONIBILE_LIQUIDATO) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PAGATO, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(IVA_LIQUIDATA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(IVA_LIQUIDATA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS IVA, " _
                '    & "TRIM(TO_CHAR(NVL((SELECT SUM(IMPONIBILE_LIQUIDATO+IVA_LIQUIDATA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(IMPONIBILE_LIQUIDATO+IVA_LIQUIDATA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS TOTPAGATO " _
                '    & " FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_PIANO_FINANZIARIO=" & id & " and pf_voci.id=" & dataItem.Item("ID").Text & " AND PF_VOCI.ID=PF_VOCI_IMPORTO.ID_VOCE " _
                '    & " AND (SELECT COUNT(*) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO<>-3 AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID AND PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID)>0 " _
                '    & " ORDER BY 3 "
                e.DetailTableView.DataSource = par.getDataTableGrid(par.cmd.CommandText)
                connData.chiudi()
        End Select
    End Sub

    'Protected Sub ImageButtonEsporta_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsporta.Click
    '    Try
    '        Dim xls As New ExcelSiSol
    '        'Dim nomeFile = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSituazioneContabile", "ExportSituazioneContabile", DataGridEs)
    '        Dim nomeFile = par.EsportaExcelAutomaticoDaDataGrid(DataGridEs, "ExportSituazioneContabile", , , "", False)
    '        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
    '            Response.Redirect("../../../FileTemp/" & nomeFile, False)
    '            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('..\/..\/..\/FileTemp\/" & nomeFile & "','','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
    '        Else
    '            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
    '            'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
    '        End If
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - ImageButtonEsporta_Click - " & ex.Message)
    '        Response.Redirect("../../../Errore.aspx", False)
    '    End Try
    'End Sub

    Protected Sub DataGridEs_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGridEs.NeedDataSource
        'If e.RebindReason = GridRebindReason.InitialLoad Or e.RebindReason = GridRebindReason.ExplicitRebind Then
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
            'connData.chiudi()
            anno2 = CStr(CInt(anno1) + 1)
            inizioAnno2 = Replace(inizioAnno1, anno1, anno2)
            fineAnno2 = Replace(fineAnno1, anno1, anno2)

            If Not IsNothing(Request.QueryString("dal")) AndAlso IsDate(Request.QueryString("dal")) Then
                fineAnno1 = par.FormatoDataDB(Request.QueryString("dal"))
            End If

            Dim cdpal As String = ""
            Dim fatal As String = ""
            Dim regfatal As String = ""
            Dim pagal As String = ""
            If Not IsNothing(Request.QueryString("cdpal")) AndAlso IsDate(Request.QueryString("cdpal")) Then
                cdpal = par.FormatoDataDB(Request.QueryString("cdpal"))
            End If
            If Not IsNothing(Request.QueryString("fatal")) AndAlso IsDate(Request.QueryString("fatal")) Then
                fatal = par.FormatoDataDB(Request.QueryString("fatal"))
            End If
            If Not IsNothing(Request.QueryString("regfatal")) AndAlso IsDate(Request.QueryString("regfatal")) Then
                regfatal = par.FormatoDataDB(Request.QueryString("regfatal"))
            End If
            If Not IsNothing(Request.QueryString("pagal")) AndAlso IsDate(Request.QueryString("pagal")) Then
                pagal = par.FormatoDataDB(Request.QueryString("pagal"))
            End If
            Dim cdpdal As String = ""
            Dim fatdal As String = ""
            Dim regfatdal As String = ""
            Dim pagdal As String = ""
            If Not IsNothing(Request.QueryString("cdpdal")) AndAlso IsDate(Request.QueryString("cdpdal")) Then
                cdpdal = par.FormatoDataDB(Request.QueryString("cdpdal"))
            End If
            If Not IsNothing(Request.QueryString("fatdal")) AndAlso IsDate(Request.QueryString("fatdal")) Then
                fatdal = par.FormatoDataDB(Request.QueryString("fatdal"))
            End If
            If Not IsNothing(Request.QueryString("regfatdal")) AndAlso IsDate(Request.QueryString("regfatdal")) Then
                regfatdal = par.FormatoDataDB(Request.QueryString("regfatdal"))
            End If
            If Not IsNothing(Request.QueryString("pagdal")) AndAlso IsDate(Request.QueryString("pagdal")) Then
                pagdal = par.FormatoDataDB(Request.QueryString("pagdal"))
            End If

            Dim dataFattura As String = ""
            If Len(fatdal) = 8 Then
                dataFattura &= " AND FATTURE_MM.DATA_FATTURA>='" & fatdal.ToString & "'"
            End If
            If Len(regfatdal) = 8 Then
                dataFattura &= " AND FATTURE_MM.DATA_SCADENZA>='" & regfatdal.ToString & "'"
            End If
            If Len(fatal) = 8 Then
                dataFattura &= " AND FATTURE_MM.DATA_FATTURA<='" & fatal.ToString & "'"
            End If
            If Len(regfatal) = 8 Then
                dataFattura &= " AND FATTURE_MM.DATA_SCADENZA<='" & regfatal.ToString & "'"
            End If

            Dim dataPagamento As String = ""
            If Len(pagdal) = 8 Then
                dataPagamento &= " AND PAGAMENTI_MM.DATA_PAGAMENTO>='" & pagdal & "' "
            End If
            If Len(pagal) = 8 Then
                dataPagamento &= " AND PAGAMENTI_MM.DATA_PAGAMENTO<='" & pagal & "' "
            End If

            Dim condizioneDataFattura As String = ""
            Dim condizioneDataFattura2 As String = ""
            If dataFattura <> "" Then
                condizioneDataFattura = " EXISTS(SELECT ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM WHERE PRENOTAZIONI.ID_PAGAMENTO=FATTURE_MM.ID_PAGAMENTO " & dataFattura & ") AND "
                condizioneDataFattura2 = " AND EXISTS(SELECT ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM WHERE PRENOTAZIONI.ID_PAGAMENTO=FATTURE_MM.ID_PAGAMENTO " & dataFattura & ") "
            End If
            Dim condizioneDataPagamento As String = ""
            If dataPagamento <> "" Then
                condizioneDataPagamento = " EXISTS(SELECT ID_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI_MM.ID_PAGAMENTO " & dataPagamento & ") AND "
            End If

            Dim condizioneCdp As String = ""
            Dim condizioneCdpPagamentiMM As String = ""
            Dim condizioneCdpFattureMM As String = ""
            If cdpdal <> "" Then
                condizioneCdp &= " EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE>='" & cdpdal.ToString & "') AND "
                condizioneCdpPagamentiMM &= " EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE PAGAMENTI_MM.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE>='" & cdpdal.ToString & "') AND "
                condizioneCdpFattureMM &= " AND  EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE FATTURE_MM.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE>='" & cdpdal.ToString & "')  "
            End If
            If cdpal <> "" Then
                condizioneCdp &= " EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE<='" & cdpal.ToString & "') AND "
                condizioneCdpPagamentiMM &= " EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE PAGAMENTI_MM.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE<='" & cdpal.ToString & "') AND "
                condizioneCdpFattureMM &= " AND  EXISTS (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE FATTURE_MM.ID_PAGAMENTO=PAGAMENTI.ID AND PAGAMENTI.DATA_EMISSIONE<='" & cdpal.ToString & "')  "
            End If
            Dim condizioneFatture As String = ""
            If condizioneCdpFattureMM <> "" Or condizioneDataFattura2 <> "" Then
                condizioneFatture = condizioneCdpFattureMM & condizioneDataFattura2
            End If

            'par.cmd.CommandText = "SELECT PF_VOCI.ID," _
            '    & "(CASE WHEN ((CASE WHEN PF_VOCI.ID IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) THEN 1 ELSE 0 END))=1 THEN((SELECT PF_CAPITOLI.COD FROM SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO)) ELSE NULL END) AS CAPITOLO, " _
            '    & "PF_VOCI.CODICE, " _
            '    & "PF_VOCI.DESCRIZIONE, " _
            '    & "NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS BUDGET, " _
            '    & "/*TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS BUDGET, */" _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS ASSESTATO, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
            '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
            '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
            '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
            '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) " _
            '    & "AS RESIDUO, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
            '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
            '    & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
            '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
            '    & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
            '    & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) " _
            '    & "AS RESIDUO_TOTALE, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND  ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PRENOTATO, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS CONSUNTIVATO, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS RITCONSUNTIVATO, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(imponibile,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(nvl(imponibile,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS IMPONIBILE_CERTIFICATO, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(IVA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(nvl(IVA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS IVA_CERTIFICATA, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS CERTIFICATO, " _
            '    & "TRIM(TO_CHAR(NVL(ROUND((SELECT sum((IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0))/(SELECT SUM(IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI B WHERE B.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)*FATTURE_MM.IMPORTO_TOTALE) FROM SISCOM_MI.FATTURE_MM,SISCOM_MI.PRENOTAZIONI WHERE FATTURE_MM.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO AND ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) " & condizioneCdpFattureMM & condizioneDataFattura & "),2),0),'999G999G990D99')) AS FATTURATO, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(IMPONIBILE_LIQUIDATO) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS PAGATO, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(IVA_LIQUIDATA) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS IVA, " _
            '    & "TRIM(TO_CHAR(NVL((SELECT SUM(IMPORTO_LIQUIDATO) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0),'999G999G990D99')) AS TOTPAGATO " _
            '    & "FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " AND CODICE='2.02.01' ORDER BY PF_VOCI.CODICE "
            'connData.apri()
            par.cmd.CommandText = "SELECT PF_VOCI.ID FROM  SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " ORDER BY CODICE ASC"
            Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            While lettore2.Read

                par.cmd.CommandText = "SELECT PF_VOCI.ID," _
                & "(CASE WHEN ((CASE WHEN PF_VOCI.ID IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) THEN 1 ELSE 0 END))=1 THEN((SELECT PF_CAPITOLI.COD FROM SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO)) ELSE NULL END) AS CAPITOLO, " _
                & "PF_VOCI.CODICE, " _
                & "PF_VOCI.DESCRIZIONE, " _
                    & "NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS BUDGET, " _
                    & "NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS ASSESTATO, " _
                    & "NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
                & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "AS RESIDUO, " _
                    & "NVL((SELECT SUM(nvl(PF_VOCI_STRUTTURA.VALORE_LORDO,0))+SUM(nvl(PF_VOCI_STRUTTURA.ASSESTAMENTO_VALORE_LORDO,0))+SUM(nvl(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_sTRUTTURA WHERE PF_VOCI_STRUTTURA.ID_VOCE IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)  " _
                & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) " _
                & "AS RESIDUO_TOTALE, " _
                    & "NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2))-SUM(ROUND(ANTICIPO_CONTRATTUALE_CON_IVA,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " DATA_PRENOTAZIONE BETWEEN " & inizioAnno1 & " AND " & fineAnno1 & " AND  ID_STATO=0 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS PRENOTATO, " _
                    & "NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0)+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS CONSUNTIVATO, " _
                & "NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=1 AND DATA_CONSUNTIVAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=1 AND DATA_CONSUNTIVAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS RITCONSUNTIVATO, " _
                    & "NVL((SELECT SUM(nvl(imponibile,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(nvl(imponibile,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IMPONIBILE_CERTIFICATO, " _
                    & "NVL((SELECT SUM(nvl(IVA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(nvl(IVA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IVA_CERTIFICATA, " _
                    & "NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2))-SUM(ROUND(ANTICIPO_CONTRATTUALE_CON_IVA,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 AND DATA_CERTIFICAZIONE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2))-SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2))-SUM(ROUND(NVL(ANTICIPO_CONTRATTUALE_CON_IVA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERTIFICAZIONE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS CERTIFICATO, " _
                    & "NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0)*100/(100+nvl(perc_iva,0)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 AND DATA_CERT_RIT_LEGGE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0)*100/(100+nvl(perc_iva,0)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IMPONIBILE_CERTIFICATO_RIT, " _
                    & "NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0)-nvl(RIT_LEGGE_IVATA,0)*100/(100+nvl(perc_iva,0)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 AND DATA_CERT_RIT_LEGGE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0)-nvl(RIT_LEGGE_IVATA,0)*100/(100+nvl(perc_iva,0)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IVA_CERTIFICATA_RIT, " _
                    & "NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & " ID_STATO>=2 AND DATA_CERT_RIT_LEGGE BETWEEN '" & inizioAnno1 & "' AND '" & fineAnno1 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) +NVL((SELECT SUM(round(nvl(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO>=2 AND DATA_CERT_RIT_LEGGE >='" & inizioAnno2 & "' AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS CERTIFICATO_RIT, " _
                    & "NVL(ROUND((SELECT sum((IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0))/(SELECT SUM(IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI B WHERE B.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)*FATTURE_MM.IMPORTO_TOTALE) FROM SISCOM_MI.FATTURE_MM,SISCOM_MI.PRENOTAZIONI WHERE FATTURE_MM.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO AND ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) " & condizioneFatture & "),2),0) AS FATTURATO, " _
                    & "NVL(ROUND((SELECT sum((NVL(RIT_LEGGE_IVATA,0))/(SELECT SUM(NVL(RIT_LEGGE_IVATA,0))                                     FROM SISCOM_MI.PRENOTAZIONI B WHERE B.ID_PAGAMENTO_RIT_LEGGE=PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE)*FATTURE_MM.IMPORTO_TOTALE) FROM SISCOM_MI.FATTURE_MM,SISCOM_MI.PRENOTAZIONI WHERE FATTURE_MM.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE AND ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID) " & condizioneFatture & "),2),0) AS FATTURATO_RIT, " _
                & "/*NVL((SELECT SUM(IMPONIBILE_LIQUIDATO) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS PAGATO, */" _
                    & "NVL((SELECT SUM(nvl(IMPORTO_LIQUIDATO,0)+round(NVL(IMPORTO_RIT_LIQUIDATO,0)*100/(100+NVL(PERC_IVA,0)),2)-nvl(iva_liquidata,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS PAGATO, " _
                    & "NVL((SELECT SUM(nvl(IVA_LIQUIDATA,0)+NVL(IMPORTO_RIT_LIQUIDATO,0)-round(NVL(IMPORTO_RIT_LIQUIDATO,0)*100/(100+NVL(PERC_IVA,0)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IVA, " _
                & "NVL((SELECT SUM((CASE WHEN NOT EXISTS (SELECT FATTURE_MM.ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM WHERE FATTURE_MM.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO) THEN (case when imponibile<>0 then round((nvl(IMPORTO_LIQUIDATO,0)-nvl(iva_liquidata,0))/imponibile*iva,2) else 0 end) ELSE (case when exists (SELECT FATTURE_MM.ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM WHERE FATTURE_MM.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO AND COD_OC NOT IN (100,102)) THEN (case when imponibile<>0 then round((nvl(IMPORTO_LIQUIDATO,0)-nvl(iva_liquidata,0))/imponibile*iva,2) else 0 end) ELSE 0 END) END)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS IVA_SPLIT, " _
                    & "NVL((SELECT SUM(nvl(IMPORTO_LIQUIDATO,0)+NVL(IMPORTO_RIT_LIQUIDATO,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneCdp & condizioneDataFattura & condizioneDataPagamento & " ID_STATO>=2 AND PRENOTAZIONI.ID_VOCE_PF IN (SELECT A.ID FROM SISCOM_MI.PF_VOCI A WHERE CONNECT_bY_ISLEAF=1 CONNECT BY PRIOR A.ID=A.ID_VOCE_MADRE START WITH A.ID=PF_VOCI.ID)),0) AS TOTPAGATO " _
                    & "FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & id & " and pf_voci.id=" & lettore2("id") & " ORDER BY PF_VOCI.CODICE "
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)
                da.Dispose()
            End While
            lettore2.Close()
            connData.chiudi()
            TryCast(sender, RadGrid).DataSource = dt
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " DataGridEs_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
        'End If
    End Sub

    Protected Sub DataGridEs_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGridEs.ItemCommand
        'If e.CommandName = RadGrid.ExportToExcelCommandName Then
        '    For Each item As GridDataItem In DataGridEs.MasterTableView.Items
        '        item.Expanded = True
        '    Next
        '    DataGridEs.ExportSettings.OpenInNewWindow = True
        '    DataGridEs.MasterTableView.ExportToExcel()
        'End If
        If e.CommandName = RadGrid.ExportToExcelCommandName Then
            DataGridEs.ExportSettings.FileName = "filename"
            DataGridEs.ExportSettings.IgnorePaging = True
            DataGridEs.ExportSettings.ExportOnlyData = True
            DataGridEs.ExportSettings.OpenInNewWindow = True
            DataGridEs.MasterTableView.UseAllDataFields = True
            DataGridEs.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML
            'DataGridEs.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx
            'DataGridEs.ExportSettings.Excel.FileExtension = "xlsx"

            'DataGridEs.MasterTableView.HierarchyDefaultExpanded = True
            ' first level
            'DataGridEs.MasterTableView.DetailTables(0).HierarchyDefaultExpanded = True
            ' second level 


            DataGridEs.MasterTableView.ExportToExcel()
        End If
    End Sub

    Protected Sub dgvDocumenti_ExcelMLExportRowCreated(sender As Object, e As Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLRowCreatedArgs) Handles DataGridEs.ExcelMLExportRowCreated
        Try
            e.Row.Cells.GetCellByName("BUDGET").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("ASSESTATO").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("RESIDUO").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("RESIDUO_TOTALE").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("PRENOTATO").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("CONSUNTIVATO").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("RITCONSUNTIVATO").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("IMPONIBILE_CERTIFICATO").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("IVA_CERTIFICATA").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("CERTIFICATO").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("IMPONIBILE_CERTIFICATO_RIT").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("IVA_CERTIFICATA_RIT").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("CERTIFICATO_RIT").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("FATTURATO").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("PAGATO").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("IVA").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("IVA_SPLIT").StyleValue = "idSt"
            e.Row.Cells.GetCellByName("TOTPAGATO").StyleValue = "idSt"
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub dgvDocumenti_ExcelMLExportStylesCreated(sender As Object, e As Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs) Handles DataGridEs.ExcelMLExportStylesCreated
        Try
            Dim style2 As StyleElement = New StyleElement("idSt")
            style2.NumberFormat.FormatType = NumberFormatType.Currency
            e.Styles.Add(style2)
        Catch ex As Exception
        End Try
    End Sub
    'Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
    '    Dim dt As New Data.DataTable
    '    dt.Columns.Add("CAPITOLO")
    '    dt.Columns.Add("CODICE")
    '    dt.Columns.Add("DESCRIZIONE")
    '    dt.Columns.Add("BUDGET")
    '    dt.Columns.Add("ASSESTATO")
    '    dt.Columns.Add("RESIDUO")
    '    dt.Columns.Add("RESIDUO_TOTALE")
    '    dt.Columns.Add("PRENOTATO")
    '    dt.Columns.Add("CONSUNTIVATO")
    '    dt.Columns.Add("RITCONSUNTIVATO")
    '    dt.Columns.Add("IMPONIBILE_CERTIFICATO")
    '    dt.Columns.Add("IVA_CERTIFICATA")
    '    dt.Columns.Add("CERTIFICATO")
    '    dt.Columns.Add("FATTURATO")
    '    dt.Columns.Add("PAGATO")
    '    dt.Columns.Add("IVA")
    '    dt.Columns.Add("TOTPAGATO")
    '    Dim riga As Data.DataRow
    '    For Each elemento As GridDataItem In DataGridEs.Items
    '        riga = dt.NewRow
    '        dt.Columns.Add("CAPITOLO")
    '        dt.Columns.Add("CODICE")
    '        dt.Columns.Add("DESCRIZIONE")
    '        dt.Columns.Add("BUDGET")
    '        dt.Columns.Add("ASSESTATO")
    '        dt.Columns.Add("RESIDUO")
    '        dt.Columns.Add("RESIDUO_TOTALE")
    '        dt.Columns.Add("PRENOTATO")
    '        dt.Columns.Add("CONSUNTIVATO")
    '        dt.Columns.Add("RITCONSUNTIVATO")
    '        dt.Columns.Add("IMPONIBILE_CERTIFICATO")
    '        dt.Columns.Add("IVA_CERTIFICATA")
    '        dt.Columns.Add("CERTIFICATO")
    '        dt.Columns.Add("FATTURATO")
    '        dt.Columns.Add("PAGATO")
    '        dt.Columns.Add("IVA")
    '        dt.Columns.Add("TOTPAGATO")

    '    Next


    'End Sub


End Class
