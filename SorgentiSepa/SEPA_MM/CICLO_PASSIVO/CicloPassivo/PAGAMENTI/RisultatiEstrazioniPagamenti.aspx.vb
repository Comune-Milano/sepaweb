Imports System.IO
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiEstrazioniPagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            Response.Flush()
            Ricerca()
        End If
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Dim xls As New ExcelSiSol
        Dim nomeFile As String = xls.EsportaExcelDaDataGridParziale(ExcelSiSol.Estensione.Office2007_xlsx, "ExportPagamenti", "ExportPagamenti", DataGridPagamenti, , , , 1, , True)
        If File.Exists(Server.MapPath("~/FileTemp/" & nomeFile)) Then
            Response.Redirect("../../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub
    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub
    Private Sub Ricerca()
        Try
            Dim SPF As String = ""
            If Not IsNothing(Request.QueryString("SPF")) Then
                If UCase(Request.QueryString("SPF").ToString) = "FALSE" Then
                    SPF = "(+)"
                End If
            End If
            Dim SPL As String = ""
            If Not IsNothing(Request.QueryString("SPL")) Then
                If UCase(Request.QueryString("SPL").ToString) = "FALSE" Then
                    SPL = "(+)"
                End If
            End If
            Dim Codoc As String = ""
            If Not IsNothing(Request.QueryString("CODOC")) Then
                Codoc = Request.QueryString("CODOC").ToString
            End If
            Dim DataFatturaDa As String = ""
            If Not IsNothing(Request.QueryString("DFATTDA")) Then
                DataFatturaDa = Request.QueryString("DFATTDA").ToString
            End If
            Dim DataFatturaA As String = ""
            If Not IsNothing(Request.QueryString("DFATTA")) Then
                DataFatturaA = Request.QueryString("DFATTA").ToString
            End If
            Dim DataPagamentoDa As String = ""
            If Not IsNothing(Request.QueryString("DPAGDA")) Then
                DataPagamentoDa = Request.QueryString("DPAGDA").ToString
            End If
            Dim DataPagamentoA As String = ""
            If Not IsNothing(Request.QueryString("DPAGA")) Then
                DataPagamentoA = Request.QueryString("DPAGA").ToString
            End If

            connData.apri()
            Dim condizioniRicerca As String = ""
            Dim condizioniRicercaFattureMM As String = ""
            If Codoc <> "" Then
                Dim array() As String = Codoc.ToString.Split(",")
                Dim lista As String = ""
                For Each elemento As String In array
                    If lista = "" Then
                        lista = "'" & elemento & "'"
                    Else
                        lista &= ",'" & elemento & "'"
                    End If
                Next
                condizioniRicercaFattureMM &= " AND fatture_mm.COD_OC in (" & lista & ")"
            End If
            If Len(DataFatturaDa) = 10 AndAlso IsDate(DataFatturaDa) Then
                condizioniRicercaFattureMM &= " AND FATTURE_MM.DATA_FATTURA>='" & par.AggiustaData(DataFatturaDa.ToString) & "'"
            End If
            If Len(DataFatturaA) = 10 AndAlso IsDate(DataFatturaA) Then
                    condizioniRicercaFattureMM &= " AND FATTURE_MM.DATA_FATTURA<='" & par.AggiustaData(DataFatturaA.ToString) & "'"
            End If
            If Len(DataPagamentoDa) = 10 AndAlso IsDate(DataPagamentoDa) Then
                condizioniRicerca &= " AND PAGAMENTI_MM.DATA_PAGAMENTO>='" & par.AggiustaData(DataPagamentoDa) & "' "
            End If
            If Len(DataPagamentoA) = 10 AndAlso IsDate(DataPagamentoA) Then
                condizioniRicerca &= " AND PAGAMENTI_MM.DATA_PAGAMENTO<='" & par.AggiustaData(DataPagamentoDa) & "' "
            End If

            par.cmd.CommandText = "SELECT " _
                & " NULL AS ID_PAGAMENTO, " _
                & " NULL AS ID_VOCE_PF, " _
                & " 'CODICE FORNITORE' AS COD_FORNITORE, " _
                & " 'RAGIONE SOCIALE' AS RAGIONE_SOCIALE, " _
                & " 'COD. FISCALE PARTITA IVA' AS COD_FISCALE, " _
                & " 'NUMERO CDP' AS NUMERO_CDP, " _
                & " 'TOTALE' AS TOT, " _
                & " 'IMPONIBILE' AS IMPONIBILE, " _
                & " 'IVA' AS IVA, " _
                & " NULL AS FATTURE, " _
                & " NULL AS VOCE, " _
                & " NULL AS PAGAMENTI, " _
                & " 'PROTOCOLLO PAGAMENTO' AS NUMERO_pag, " _
                & " 'DATA PAGAMENTO' AS DATA_PAG, " _
                & " 'CODICE OPERAZIONE CONTABILE' AS COD_OP_CONTAB, " _
                & " NULL AS DESCR_OC, " _
                & " 'IMPORTO PAGATO' AS IMPORTO_PAGATO, " _
                & " 'VOCE' AS VOCE_pf, " _
                & " 'CAPITOLO' AS CAPITOLO, " _
                & " 'IMPONIBILE DETTAGLIO' AS imponibile_d, " _
                & " 'IVA DETTAGLIO' AS iva_d, " _
                & " 'TOTALE DETTAGLIO' AS totale_d, " _
                & " 'PROTOCOLLO FATTURA' AS NUMERO_RDS, " _
                & " 'NUMERO FATTURA FORNITORE' AS N_FATT_FORN, " _
                & " 'DATA FATTURA' AS DATA_FATT, " _
                & " 'CODICE OPERAZIONE CONTABILE' AS COD_OP_CONT, " _
                & " NULL AS DESCRIZIONE_OC, " _
                & " 'IMPORTO' AS IMPORTO_TOTALE, " _
                & " 'CUP' AS CUP, " _
                & " 'CIG' AS CIG " _
                & " FROM DUAL UNION " _
                & " SELECT distinct PAGAMENTI.ID AS ID_PAGAMENTO,prenotazioni.ID_VOCE_PF," _
                & " (SELECT COD_FORNITORE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS COD_FORNITORE," _
                & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS RAGIONE_SOCIALE," _
                & " (SELECT COD_FISCALE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS COD_FISCALE," _
                & " PAGAMENTI.PROGR||'/'||PAGAMENTI.ANNO AS NUMERO_CDP," _
                & " TRIM(TO_CHAR(( SUM( ROUND( IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0),2)) ),'999G999G990D99')) AS TOT, " _
                & " TRIM(TO_CHAR(( SUM( ROUND((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))*100/(NVL(PERC_IVA,0) + 100),2)) ),'999G999G990D99')) AS IMPONIBILE, " _
                & " TRIM(TO_CHAR(( SUM( ROUND((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))-((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))*100/(NVL(PERC_IVA,0) + 100)),2)) ),'999G999G990D99')) AS IVA, " _
                & " '' AS FATTURE,'' AS VOCE,'' AS PAGAMENTI, " _
                & " ANNO_PAGAMENTO||'/'||NUMERO_PAGAMENTO AS NUMERO_pag, " _
                & " SISCOM_MI.GETDATA(DATA_PAGAMENTO) AS DATA_PAG, " _
                & " PAGAMENTI_MM.COD_OC AS COD_OP_CONTAB," _
                & " PAGAMENTI_MM.DESCRIZIONE_OC AS DESCR_OC," _
                & " TRIM(TO_CHAR(sum(ROUND(PAGAMENTI_LIQUIDATI.IMPORTO,2)),'999G999G990D99')) AS IMPORTO_PAGATO, " _
                & " PF_VOCI.codice AS VOCE_pf," _
                & " PF_CAPITOLI.cod AS CAPITOLO, " _
                & " TRIM(TO_CHAR((SUM( ROUND((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))*100/(NVL(PERC_IVA,0) + 100),2))),'999G999G990D99')) AS imponibile_d, " _
                & " TRIM(TO_CHAR((SUM( ROUND((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))-((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))*100/(NVL(PERC_IVA,0) + 100)),2))),'999G999G990D99')) AS iva_d, " _
                & " TRIM(TO_CHAR((SUM( ROUND( IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0),2))),'999G999G990D99')) AS totale_d,  " _
                & " FATTURE_MM.ANNO||'/'||FATTURE_MM.NUMERO AS NUMERO_RDS," _
                & " NUMERO_FATT_FORN AS N_FATT_FORN," _
                & " siscom_mi.GETDATA(DATA_FATTURA) AS DATA_FATT," _
                & " fatture_mm.COD_OC AS COD_OP_CONT,fatture_mm.DESCRIZIONE_OC," _
                & " TRIM(TO_CHAR(SUM(IMPORTO_TOTALE),'999G999G990D99')) AS IMPORTO_TOTALE, " _
                & " PAGAMENTI_MM.CUP AS CUP, " _
                & " PAGAMENTI_MM.CIG AS CIG " _
                & " FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,siscom_mi.pagamenti_mm,siscom_mi.pagamenti_liquidati,siscom_mi.pf_capitoli,siscom_mi.pf_voci,siscom_mi.fatture_mm " _
                & " WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID " _
                & " AND PRENOTAZIONI.ID_STATO = 2 " _
                & " and pagamenti_mm.id" & SPL & "=pagamenti_liquidati.id_pagamento_mm " _
                & " And pagamenti_liquidati.id_pagamento" & SPL & " = pagamenti.id " _
                & " and prenotazioni.id_voce_pf=pagamenti_liquidati.id_voce_pf " _
                & " AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
                & " AND PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO " _
                & " AND fatture_mm.id_pagamento" & SPF & "=pagamenti.id " _
                & condizioniRicerca _
                & condizioniRicercaFattureMM _
                & " GROUP BY PAGAMENTI.ID,PAGAMENTI.ID_FORNITORE,PAGAMENTI.PROGR,PAGAMENTI.ANNO,PRENOTAZIONI.ID_VOCE_PF, " _
                & " pagamenti_mm.id, " _
                & " anno_pagamento, " _
                & " numero_pagamento," _
                & " data_pagamento," _
                & " pagamenti_mm.cod_oc, " _
                & " pagamenti_mm.DESCRIZIONE_OC, " _
                & " PF_VOCI.codice," _
                & " PF_CAPITOLI.cod, " _
                & " FATTURE_MM.NUMERO,FATTURE_MM.ANNO,FATTURE_MM.DATA_FATTURA,FATTURE_MM.COD_OC,NUMERO_FATT_FORN,fatture_mm.DESCRIZIONE_OC, " _
                & " pagamenti_mm.cup," _
                & " pagamenti_mm.cig " _
                & " ORDER BY 1 NULLS FIRST,2"
            'PAGAMENTI.ID IN (SELECT ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM " & condizioniRicercaFattureMM & ")" & condizioniRicerca & " AND
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Session.Item("dtPagEx") = dt
            da.Dispose()
            If dt.Rows.Count > 0 Then
                DataGridPagamenti.DataSource = dt
                DataGridPagamenti.DataBind()
                DataGridPagamenti.Visible = True
            Else
                DataGridPagamenti.Visible = False
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - Ricerca - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGridPagamenti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridPagamenti.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Text = "CERTIFICATO DI PAGAMENTO"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Attributes.Add("colspan", 7)
            'e.Item.Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IVA")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "TOT")).Visible = False
            'e.Item.Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Text = "PIANO FINANZIARIO"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Attributes.Add("colspan", 5)
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text = "FATTURE"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Attributes.Add("colspan", 5)
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "DATA_FATT")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text = "PAGAMENTI"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Attributes.Add("colspan", 6)
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "CUP")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "CIG")).Visible = False
        End If
    End Sub

    Protected Sub DataGridPagamenti_PreRender(sender As Object, e As System.EventArgs) Handles DataGridPagamenti.PreRender

        Dim codFornitorePrecedente As String = ""
        Dim ragioneSocialePrecedente As String = ""
        Dim codFiscalePrecedente As String = ""
        Dim numeroCDPPrecedente As String = ""
        Dim imponibilePrecedente As String = ""
        Dim ivaPrecedente As String = ""
        Dim totPrecedente As String = ""
        Dim idPagamentoPrecedente As String = ""
        Dim idVocePrecedente As String = ""
        Dim vocePrecedente As String = ""
        Dim capitoloPrecedente As String = ""
        Dim imponibileDPrecedente As String = ""
        Dim ivaDPrecedente As String = ""
        Dim totaleDPrecedente As String = ""
        Dim numeroPrecedente As String = ""
        Dim dataPrecedente As String = ""
        Dim importoPagatoPrecedente As String = ""
        Dim codOperazioneContabilePrecedente As String = ""
        Dim numeroRDSPrecedente As String = ""
        Dim nFattFornPrecedente As String = ""
        Dim dataFattPrecedente As String = ""
        Dim codOpContPrecedente As String = ""
        Dim importoTotalePrecedente As String = ""
        Dim cupPrecedente As String = ""
        Dim cigPrecedente As String = ""

        For i As Integer = DataGridPagamenti.Items.Count - 1 To 0 Step -1

            If i = DataGridPagamenti.Items.Count - 1 Then
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Attributes.Add("rowspan", 1)
                idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Attributes.Add("rowspan", 1)
                codFornitorePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Attributes.Add("rowspan", 1)
                ragioneSocialePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Attributes.Add("rowspan", 1)
                codFiscalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Attributes.Add("rowspan", 1)
                numeroCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Attributes.Add("rowspan", 1)
                imponibilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Attributes.Add("rowspan", 1)
                ivaPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Attributes.Add("rowspan", 1)
                totPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Attributes.Add("rowspan", 1)
                idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Attributes.Add("rowspan", 1)
                vocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Attributes.Add("rowspan", 1)
                capitoloPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Attributes.Add("rowspan", 1)
                imponibileDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Attributes.Add("rowspan", 1)
                ivaDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Attributes.Add("rowspan", 1)
                totaleDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Attributes.Add("rowspan", 1)
                numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Attributes.Add("rowspan", 1)
                dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Attributes.Add("rowspan", 1)
                importoPagatoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Attributes.Add("rowspan", 1)
                codOperazioneContabilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Attributes.Add("rowspan", 1)
                numeroRDSPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Attributes.Add("rowspan", 1)
                nFattFornPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Attributes.Add("rowspan", 1)
                dataFattPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Attributes.Add("rowspan", 1)
                codOpContPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Attributes.Add("rowspan", 1)
                importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Text

                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Attributes.Add("rowspan", 1)
                importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Attributes.Add("rowspan", 1)
                importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Text

            Else
                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                    And codFornitorePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Text _
                    And ragioneSocialePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Text _
                    And codFiscalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Text _
                    And numeroCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Text _
                    And imponibilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text _
                    And ivaPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text _
                    And totPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text Then

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan = 1

                End If


                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                    And idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text _
                    And vocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Text _
                    And capitoloPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Text _
                    And imponibileDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Text _
                    And ivaDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Text _
                    And totaleDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Text Then

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).RowSpan = 1

                End If


                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                    And idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text _
                    And numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text _
                    And dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text _
                    And importoPagatoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text _
                    And codOperazioneContabilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Text _
                    And cupPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Text _
                    And cigPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Text Then

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan = 1

                End If

                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                    And numeroRDSPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text _
                    And nFattFornPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Text _
                    And dataFattPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Text _
                    And codOpContPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Text _
                    And importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Text Then

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).RowSpan = 1

                End If

                If numeroCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Text Then
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan = 1

                    If idVocePrecedente <> DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text Then
                        DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text = Format(CDec(DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text) + CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text), "#,##0.00")
                        DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text = Format(CDec(DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text) + CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text), "#,##0.00")
                        DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text = Format(CDec(DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text) + CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text), "#,##0.00")
                    End If

                End If

                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                    And numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text _
                    And dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text Then

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan = 1


                    'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text = Format(CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text), "#,##0.00")
                    

                End If
               

            End If


            If i = DataGridPagamenti.Items.Count - 1 Then
                DataGridPagamenti.Items(i).BackColor = Drawing.Color.White
            Else
                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text Then
                    DataGridPagamenti.Items(i).BackColor = DataGridPagamenti.Items(i + 1).BackColor
                Else
                    If DataGridPagamenti.Items(i + 1).BackColor = Drawing.Color.White Then
                        DataGridPagamenti.Items(i).BackColor = Drawing.Color.LightGray
                    Else
                        DataGridPagamenti.Items(i).BackColor = Drawing.Color.White
                    End If
                End If

            End If

            idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text
            codFornitorePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Text
            ragioneSocialePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Text
            codFiscalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Text
            numeroCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Text
            imponibilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text
            ivaPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text
            totPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text

            idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text
            vocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Text
            capitoloPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Text
            imponibileDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Text
            ivaDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Text
            totaleDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Text

            numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text
            dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text
            importoPagatoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text
            codOperazioneContabilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Text

            numeroRDSPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text
            nFattFornPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Text
            dataFattPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Text
            codOpContPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Text
            importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Text

            If i = 0 Then
                DataGridPagamenti.Items(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#507CD1")
                DataGridPagamenti.Items(i).ForeColor = Drawing.Color.White
                DataGridPagamenti.Items(i).Font.Bold = True
                For K As Integer = 0 To DataGridPagamenti.Columns.Count - 1
                    DataGridPagamenti.Items(i).Cells(K).HorizontalAlign = HorizontalAlign.Center
                Next
            End If
        Next
    End Sub
End Class