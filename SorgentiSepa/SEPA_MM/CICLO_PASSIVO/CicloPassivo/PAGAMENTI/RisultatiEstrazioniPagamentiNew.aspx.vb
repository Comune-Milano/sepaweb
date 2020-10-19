Imports System.IO
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiEstrazioniPagamentiNew
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Public idPagamento As Integer = 0
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
                    SPF = "0"
                Else
                    SPF = "1"
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
            Dim DataREGFatturaDa As String = ""
            If Not IsNothing(Request.QueryString("DREGFATTDA")) Then
                DataREGFatturaDa = Request.QueryString("DREGFATTDA").ToString
            End If
            Dim DataREGFatturaA As String = ""
            If Not IsNothing(Request.QueryString("DREGFATTA")) Then
                DataREGFatturaA = Request.QueryString("DREGFATTA").ToString
            End If
            Dim DataPagamentoDa As String = ""
            If Not IsNothing(Request.QueryString("DPAGDA")) Then
                DataPagamentoDa = Request.QueryString("DPAGDA").ToString
            End If
            Dim DataPagamentoA As String = ""
            If Not IsNothing(Request.QueryString("DPAGA")) Then
                DataPagamentoA = Request.QueryString("DPAGA").ToString
            End If
            Dim DataCDPDa As String = ""
            If Not IsNothing(Request.QueryString("DCDPDA")) Then
                DataCDPDa = Request.QueryString("DCDPDA").ToString
            End If
            Dim DataCDPA As String = ""
            If Not IsNothing(Request.QueryString("DCDPA")) Then
                DataCDPA = Request.QueryString("DCDPA").ToString
            End If

            'connData.apri()
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
            If Len(DataREGFatturaDa) = 10 AndAlso IsDate(DataREGFatturaDa) Then
                condizioniRicercaFattureMM &= " AND FATTURE_MM.DATA_SCADENZA>='" & par.AggiustaData(DataREGFatturaDa.ToString) & "'"
            End If
            If Len(DataREGFatturaA) = 10 AndAlso IsDate(DataREGFatturaA) Then
                condizioniRicercaFattureMM &= " AND FATTURE_MM.DATA_SCADENZA<='" & par.AggiustaData(DataREGFatturaA.ToString) & "'"
            End If
            If Len(DataPagamentoDa) = 10 AndAlso IsDate(DataPagamentoDa) Then
                    condizioniRicerca &= " AND PAGAMENTI_MM.DATA_PAGAMENTO>='" & par.AggiustaData(DataPagamentoDa) & "' "
            End If
            If Len(DataPagamentoA) = 10 AndAlso IsDate(DataPagamentoA) Then
                condizioniRicerca &= " AND PAGAMENTI_MM.DATA_PAGAMENTO<='" & par.AggiustaData(DataPagamentoA) & "' "
            End If

            Dim condizioneFatturaMM As String = ""
            If condizioniRicercaFattureMM <> "" Then
                condizioneFatturaMM = " AND EXISTS(SELECT ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM WHERE PAGAMENTI.ID=FATTURE_MM.ID_PAGAMENTO " & condizioniRicercaFattureMM & ")"
            End If

            If SPF = "1" Then
                If condizioneFatturaMM = "" Then
                    condizioneFatturaMM = " AND EXISTS(SELECT ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM WHERE PAGAMENTI.ID=FATTURE_MM.ID_PAGAMENTO)"
                End If
            End If

            Dim condizioneRicercaMM As String = ""
            If condizioniRicerca <> "" Then
                condizioneRicercaMM = " AND EXISTS(SELECT ID_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_MM WHERE PAGAMENTI.ID=PAGAMENTI_MM.ID_PAGAMENTO " & condizioniRicerca & ")"
            End If

            Dim condizioneRicercaBP As String = ""
            If Not IsNothing(Request.QueryString("IDPF")) AndAlso Request.QueryString("IDPF") <> "-1" AndAlso IsNumeric(Request.QueryString("IDPF")) Then
                condizioneRicercaBP = " AND EXISTS(SELECT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO AND ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & Request.QueryString("IDPF").ToString & "))"
            End If

            Dim condizioneRicercaBPRit As String = ""
            If Not IsNothing(Request.QueryString("IDPF")) AndAlso Request.QueryString("IDPF") <> "-1" AndAlso IsNumeric(Request.QueryString("IDPF")) Then
                condizioneRicercaBPRit = " AND EXISTS(SELECT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE AND ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & Request.QueryString("IDPF").ToString & "))"
            End If

            Dim condizioneDataCDP As String = ""
            If Len(DataCDPDa) = 10 AndAlso IsDate(DataCDPDa) Then
                condizioneDataCDP &= " AND PAGAMENTI.DATA_EMISSIONE>='" & par.AggiustaData(DataCDPDa) & "' "
            End If
            If Len(DataCDPA) = 10 AndAlso IsDate(DataCDPA) Then
                condizioneDataCDP &= " AND PAGAMENTI.DATA_EMISSIONE<='" & par.AggiustaData(DataCDPA) & "' "
            End If

            connData.apri()
            par.cmd.CommandText = "SELECT PAGAMENTI.ID AS ID_PAGAMENTO, " _
                & " (SELECT COD_FORNITORE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS COD_FORNITORE," _
                & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS RAGIONE_SOCIALE," _
                & " (SELECT COD_FISCALE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS COD_FISCALE," _
                & " PAGAMENTI.PROGR||'/'||PAGAMENTI.ANNO AS NUMERO_CDP," _
                & " PAGAMENTI.ANNO AS ANNO_CDP," _
                & " SISCOM_MI.GETDATA(PAGAMENTI.DATA_EMISSIONE) AS DATA_CDP," _
                & " (SELECT TRIM(TO_CHAR(( SUM( ROUND(NVL(PRENOTAZIONI.IMPONIBILE,0)+NVL(PRENOTAZIONI.IVA,0)/*-NVL(PRENOTAZIONI.ANTICIPO_CONTRATTUALE,0)-round(NVL(PRENOTAZIONI.ANTICIPO_CONTRATTUALE,0)/100*NVL((SELECT nvl(PERC_IVA,0) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=PAGAMENTI.ID_APPALTO)),0),2)*/,2)) ),'999G999G990D99')) FROM SISCOM_MI.PRENOTAZIONI WHERE id_Stato<>-3 and PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID GROUP BY 1) AS TOT, " _
                & " (SELECT TRIM(TO_CHAR(( SUM( ROUND(NVL(PRENOTAZIONI.IMPONIBILE,0)/*-NVL(PRENOTAZIONI.ANTICIPO_CONTRATTUALE,0)*/,2))),'999G999G990D99')) FROM SISCOM_MI.PRENOTAZIONI WHERE id_Stato<>-3 and PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID) AS IMPONIBILE, " _
                & " (SELECT TRIM(TO_CHAR(( SUM( ROUND(NVL(PRENOTAZIONI.IVA,0)/*-round(NVL(PRENOTAZIONI.ANTICIPO_CONTRATTUALE,0)/100*NVL((SELECT nvl(PERC_IVA,0) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=PAGAMENTI.ID_APPALTO)),0),2)*/,2)) ),'999G999G990D99')) FROM SISCOM_MI.PRENOTAZIONI WHERE id_Stato<>-3 and PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID) AS IVA  " _
                & " FROM SISCOM_MI.PAGAMENTI WHERE EXISTS (SELECT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2 AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID) " _
                & condizioneFatturaMM _
                & condizioneRicercaMM _
                & condizioneRicercaBP _
                & condizioneDataCDP _
                & " /*AND PAGAMENTI.PROGR=1208 AND PAGAMENTI.ANNO=2015*/ order by 1,2,3,4,5,7"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            'Dim idPagamento As Integer = 0
            Dim ragioneSociale As String = ""
            Dim contPagamentiMM As Integer = 0
            Dim contFattureMM As Integer = 0
            Dim contPrenotazioniMM As Integer = 0
            Dim dtDef As New Data.DataTable
            dtDef.Columns.Add("ID_PAGAMENTO")
            dtDef.Columns.Add("COD_FORNITORE")
            dtDef.Columns.Add("RAGIONE_SOCIALE")
            dtDef.Columns.Add("COD_FISCALE")
            dtDef.Columns.Add("NUMERO_CDP")
            'dtDef.Columns.Add("ANNO_CDP")
            dtDef.Columns.Add("DATA_CDP")
            dtDef.Columns.Add("IMPONIBILE")
            dtDef.Columns.Add("IVA")
            dtDef.Columns.Add("TOT")
            'PRENOTAZIONI
            dtDef.Columns.Add("ID_VOCE_PF")
            dtDef.Columns.Add("ANNO_PF")
            dtDef.Columns.Add("VOCE_PF")
            dtDef.Columns.Add("CAPITOLO")
            dtDef.Columns.Add("IMPONIBILE_D")
            dtDef.Columns.Add("IVA_D")
            dtDef.Columns.Add("TOTALE_D")
            'FATTURA MM
            dtDef.Columns.Add("ID_FATTURA_MM")
            dtDef.Columns.Add("NUMERO_RDS")
            dtDef.Columns.Add("N_FATT_FORN")
            dtDef.Columns.Add("DATA_FATT")
            dtDef.Columns.Add("DATA_REG")
            dtDef.Columns.Add("COD_OP_CONT")
            dtDef.Columns.Add("IMPORTO_TOTALE")
            'PAGAMENTI MM
            dtDef.Columns.Add("ID_PAGAMENTO_MM")
            dtDef.Columns.Add("NUMERO_PAG")
            dtDef.Columns.Add("DATA_PAG")
            dtDef.Columns.Add("IMPORTO_PAGATO")
            dtDef.Columns.Add("COD_OP_CONTAB")
            dtDef.Columns.Add("CUP")
            dtDef.Columns.Add("CIG")
            Dim riga As Data.DataRow
            riga = dtDef.NewRow
            riga.Item("ID_PAGAMENTO") = ""
            riga.Item("COD_FORNITORE") = "CODICE FORNITORE"
            riga.Item("RAGIONE_SOCIALE") = "RAGIONE SOCIALE"
            riga.Item("COD_FISCALE") = "COD. FISCALE PARTITA IVA"
            riga.Item("NUMERO_CDP") = "NUMERO CDP"
            'riga.Item("ANNO_CDP") = "ANNO"
            riga.Item("DATA_CDP") = "DATA CDP"
            riga.Item("IMPONIBILE") = "IMPONIBILE"
            riga.Item("IVA") = "IVA"
            riga.Item("TOT") = "TOTALE"
            riga.Item("ID_VOCE_PF") = ""
            riga.Item("ANNO_PF") = "ANNO PF"
            riga.Item("VOCE_PF") = "VOCE"
            riga.Item("CAPITOLO") = "CAPITOLO"
            riga.Item("IMPONIBILE_D") = "IMPONIBILE DETTAGLIO"
            riga.Item("IVA_D") = "IVA DETTAGLIO"
            riga.Item("TOTALE_D") = "TOTALE DETTAGLIO"
            riga.Item("ID_FATTURA_MM") = ""
            riga.Item("NUMERO_RDS") = "PROTOCOLLO FATTURA"
            riga.Item("N_FATT_FORN") = "NUMERO FATTURA FORNITORE"
            riga.Item("DATA_FATT") = "DATA FATTURA"
            riga.Item("DATA_REG") = "DATA REGISTRAZIONE"
            riga.Item("COD_OP_CONT") = "CODICE OPERAZIONE CONTABILE"
            riga.Item("IMPORTO_TOTALE") = "IMPORTO"
            riga.Item("ID_PAGAMENTO_MM") = ""
            riga.Item("NUMERO_PAG") = "PROTOCOLLO PAGAMENTO"
            riga.Item("DATA_PAG") = "DATA PAGAMENTO"
            riga.Item("IMPORTO_PAGATO") = "IMPORTO PAGATO"
            riga.Item("COD_OP_CONTAB") = "CODICE OPERAZIONE CONTABILE"
            riga.Item("CUP") = "CUP"
            riga.Item("CIG") = "CIG"
            dtDef.Rows.Add(riga)

            Dim indiceTabella As Integer = 1
            Dim prodotto As Integer = 1
            Dim idPrenotazione As Integer = 0
            Dim lettoreP As Oracle.DataAccess.Client.OracleDataReader
            For Each pagamento As Data.DataRow In dt.Rows
                idPagamento = par.IfNull(pagamento.Item("ID_PAGAMENTO"), 0)
                par.cmd.CommandText = "SELECT " _
                    & " NVL(SUM(COUNT(DISTINCT PRENOTAZIONI.ID_VOCE_PF)),0) " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=" & idPagamento _
                    & " AND ID_STATO=2 " _
                    & " GROUP BY PRENOTAZIONI.ID_VOCE_PF," _
                    & " PRENOTAZIONI.ID_PAGAMENTO"
                contPrenotazioniMM = Math.Max(par.cmd.ExecuteScalar, 1)
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.FATTURE_MM WHERE ID_PAGAMENTO=" & idPagamento & condizioniRicercaFattureMM
                contFattureMM = Math.Max(par.cmd.ExecuteScalar, 1)
                If contFattureMM = 0 Then
                    contFattureMM = 1
                End If
                par.cmd.CommandText = "SELECT COUNT(COUNT(*)) FROM SISCOM_MI.PAGAMENTI_MM,SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_MM.ID=PAGAMENTI_LIQUIDATI.ID_PAGAMENTO_MM AND PAGAMENTI_MM.ID_PAGAMENTO=" & idPagamento & condizioniRicerca _
                    & " GROUP BY ID_VOCE_PF,ID_PAGAMENTO_MM"
                contPagamentiMM = Math.Max(par.cmd.ExecuteScalar, 1)
                If contPagamentiMM = 0 Then
                    contPagamentiMM = 1
                End If
                prodotto = contPrenotazioniMM * contPagamentiMM * contFattureMM
                For i As Integer = 1 To prodotto
                    riga = dtDef.NewRow
                    dtDef.Rows.Add(riga)
                Next
                For i As Integer = indiceTabella To indiceTabella + prodotto - 1
                    dtDef.Rows(i).Item("ID_PAGAMENTO") = pagamento.Item("ID_PAGAMENTO")
                    dtDef.Rows(i).Item("COD_FORNITORE") = pagamento.Item("COD_FORNITORE")
                    dtDef.Rows(i).Item("RAGIONE_SOCIALE") = pagamento.Item("RAGIONE_SOCIALE")
                    dtDef.Rows(i).Item("COD_FISCALE") = pagamento.Item("COD_FISCALE")
                    dtDef.Rows(i).Item("NUMERO_CDP") = pagamento.Item("NUMERO_CDP")
                    'dtDef.Rows(i).Item("ANNO_CDP") = pagamento.Item("ANNO_CDP")
                    dtDef.Rows(i).Item("DATA_CDP") = pagamento.Item("DATA_CDP")
                    dtDef.Rows(i).Item("IMPONIBILE") = pagamento.Item("IMPONIBILE")
                    dtDef.Rows(i).Item("IVA") = pagamento.Item("IVA")
                    dtDef.Rows(i).Item("TOT") = pagamento.Item("TOT")
                Next
                par.cmd.CommandText = "SELECT " _
                    & " PRENOTAZIONI.ID_VOCE_PF AS ID_VOCE_PF," _
                    & " (select substr(inizio,1,4) from siscom_mi.t_esercizio_finanziario,siscom_mi.pf_main,siscom_mi.pf_voci " _
                    & " WHERE PF_VOCI.ID = PRENOTAZIONI.ID_VOCE_PF and pf_voci.id_piano_finanziario=pf_main.id " _
                    & " and pf_main.id_esercizio_finanziario=t_esercizio_finanziario.id)  AS ANNO_PF," _
                    & " (SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF) AS VOCE_PF," _
                    & " (SELECT PF_CAPITOLI.COD FROM SISCOM_MI.PF_CAPITOLI  WHERE ID = (SELECT ID_CAPITOLO FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF)) AS CAPITOLO," _
                    & " TRIM(TO_CHAR((SUM( ROUND(NVL(PRENOTAZIONI.IMPONIBILE,0)+NVL(PRENOTAZIONI.IVA,0)/*-ANTICIPO_CONTRATTUALE-ROUND(ANTICIPO_CONTRATTUALE/100*NVL((SELECT nvl(PERC_IVA,0) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=PRENOTAZIONI.ID_APPALTO)),0),2)*/,2)) ),'999G999G990D99')) AS TOTALE_D, " _
                    & " TRIM(TO_CHAR((SUM( ROUND(NVL(PRENOTAZIONI.IMPONIBILE,0),2)/*-ROUND(NVL(PRENOTAZIONI.ANTICIPO_CONTRATTUALE,0),2)*/)),'999G999G990D99')) AS IMPONIBILE_D, " _
                    & " TRIM(TO_CHAR((SUM( ROUND(NVL(PRENOTAZIONI.IVA,0)/*-ROUND(NVL(PRENOTAZIONI.ANTICIPO_CONTRATTUALE,0)/100*NVL((SELECT nvl(PERC_IVA,0) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=prenotazioni.ID_APPALTO)),0),2)*/,2)) ),'999G999G990D99')) AS IVA_D " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=" & idPagamento _
                    & " AND ID_STATO=2 " _
                    & " GROUP BY PRENOTAZIONI.ID_VOCE_PF,PRENOTAZIONI.ANNO ORDER BY 1"
                lettoreP = par.cmd.ExecuteReader
                Dim indiceInizialeTabella As Integer = indiceTabella
                Dim contLett As Integer = indiceInizialeTabella
                While lettoreP.Read
                    For i As Integer = 0 To prodotto / contPrenotazioniMM - 1
                        dtDef.Rows(contLett).Item("ID_VOCE_PF") = lettoreP("ID_VOCE_PF")
                        dtDef.Rows(contLett).Item("ANNO_PF") = lettoreP("ANNO_PF")
                        dtDef.Rows(contLett).Item("VOCE_PF") = lettoreP("VOCE_PF")
                        dtDef.Rows(contLett).Item("CAPITOLO") = lettoreP("CAPITOLO")
                        dtDef.Rows(contLett).Item("IMPONIBILE_D") = lettoreP("IMPONIBILE_D")
                        dtDef.Rows(contLett).Item("IVA_D") = lettoreP("IVA_D")
                        dtDef.Rows(contLett).Item("TOTALE_D") = lettoreP("TOTALE_D")
                        contLett += 1
                    Next
                End While
                lettoreP.Close()
                par.cmd.CommandText = "SELECT NUMERO AS ID_FATTURA_MM, " _
                    & " NUMERO AS NUMERO_RDS," _
                    & " NUMERO_FATT_FORN AS N_FATT_FORN," _
                    & " SISCOM_MI.GETDATA(DATA_FATTURA) AS DATA_FATT," _
                    & " SISCOM_MI.GETDATA(DATA_SCADENZA) AS DATA_REG," _
                    & " COD_OC AS COD_OP_CONT," _
                    & " TRIM(TO_CHAR(IMPORTO_TOTALE,'999G999G990D99')) AS IMPORTO_TOTALE " _
                    & " FROM SISCOM_MI.FATTURE_MM WHERE ID_PAGAMENTO=" & idPagamento & condizioniRicercaFattureMM _
                    & " ORDER BY 2"
                lettoreP = par.cmd.ExecuteReader
                contLett = indiceInizialeTabella
                While lettoreP.Read
                    For i As Integer = 0 To prodotto / contFattureMM - 1
                        dtDef.Rows(contLett).Item("ID_FATTURA_MM") = lettoreP("ID_FATTURA_MM")
                        dtDef.Rows(contLett).Item("NUMERO_RDS") = lettoreP("NUMERO_RDS")
                        dtDef.Rows(contLett).Item("N_FATT_FORN") = lettoreP("N_FATT_FORN")
                        dtDef.Rows(contLett).Item("DATA_FATT") = lettoreP("DATA_FATT")
                        dtDef.Rows(contLett).Item("DATA_REG") = lettoreP("DATA_REG")
                        dtDef.Rows(contLett).Item("COD_OP_CONT") = lettoreP("COD_OP_CONT")
                        dtDef.Rows(contLett).Item("IMPORTO_TOTALE") = lettoreP("IMPORTO_TOTALE")
                        contLett += 1
                    Next
                End While
                lettoreP.Close()
                par.cmd.CommandText = "SELECT PAGAMENTI_MM.ID AS ID_PAGAMENTO_MM," _
                    & " NUMERO_PAGAMENTO AS NUMERO_PAG," _
                    & " SISCOM_MI.GETDATA(DATA_PAGAMENTO) AS DATA_PAG," _
                    & " /*TRIM(TO_CHAR(IMPORTO_PAGATO/100,'999G999G990D99')) AS IMPORTO_PAGATO,*/" _
                    & " TRIM(TO_CHAR(sum(pagamenti_liquidati.importo),'999G999G990D99')) AS IMPORTO_PAGATO," _
                    & " COD_OC AS COD_OP_CONTAB," _
                    & " /*(SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER(NUMERO_CDP) AND ANNO=TO_NUMBER(ANNO_CDP))) AS CIG,*/" _
                    & " (SELECT CIG FROM APPALTI WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.PAGAMENTI WHERE ID =PAGAMENTI_MM.ID_PAGAMENTO )) AS CIG," _
                    & " /*(SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER(NUMERO_CDP) AND ANNO=TO_NUMBER(ANNO_CDP))) AS CUP*/" _
                    & " (SELECT CUP FROM APPALTI WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.PAGAMENTI WHERE ID =PAGAMENTI_MM.ID_PAGAMENTO )) AS CUP" _
                    & " FROM SISCOM_MI.PAGAMENTI_MM,SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_MM.ID_PAGAMENTO=" & idPagamento & condizioniRicerca _
                    & " and pagamenti_liquidati.id_pagamento_mm=pagamenti_mm.id " _
                    & " group by pagamenti_liquidati.id_pagamento_mm," _
                    & " pagamenti_mm.ID, id_voce_pf, NUMERO_PAGAMENTO, DATA_PAGAMENTO, COD_OC, pagamenti_mm.ID_PAGAMENTO " _
                    & " ORDER BY ID_VOCE_PF "
                lettoreP = par.cmd.ExecuteReader
                contLett = indiceInizialeTabella
                While lettoreP.Read
                    For i As Integer = 0 To prodotto / contPagamentiMM - 1
                        dtDef.Rows(contLett).Item("ID_PAGAMENTO_MM") = lettoreP("ID_PAGAMENTO_MM")
                        dtDef.Rows(contLett).Item("NUMERO_PAG") = lettoreP("NUMERO_PAG")
                        dtDef.Rows(contLett).Item("DATA_PAG") = lettoreP("DATA_PAG")
                        dtDef.Rows(contLett).Item("IMPORTO_PAGATO") = lettoreP("IMPORTO_PAGATO")
                        dtDef.Rows(contLett).Item("COD_OP_CONTAB") = lettoreP("COD_OP_CONTAB")
                        dtDef.Rows(contLett).Item("CUP") = lettoreP("CUP")
                        dtDef.Rows(contLett).Item("CIG") = lettoreP("CIG")
                        contLett += 1
                    Next
                End While
                lettoreP.Close()
                indiceTabella += prodotto
            Next
            
            par.cmd.CommandText = "SELECT PAGAMENTI.ID AS ID_PAGAMENTO, " _
                & " (SELECT COD_FORNITORE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS COD_FORNITORE," _
                & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS RAGIONE_SOCIALE," _
                & " (SELECT COD_FISCALE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS COD_FISCALE," _
                & " PAGAMENTI.PROGR||'/'||PAGAMENTI.ANNO AS NUMERO_CDP," _
                & " PAGAMENTI.ANNO AS ANNO_CDP," _
                & " SISCOM_MI.GETDATA(PAGAMENTI.DATA_EMISSIONE) AS DATA_CDP," _
                & " (SELECT TRIM(TO_CHAR(( SUM( ROUND(NVL(RIT_LEGGE_IVATA,0),2)) ),'999G999G990D99')) FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI.ID) AS TOT, " _
                & " (SELECT TRIM(TO_CHAR(( SUM( ROUND(NVL(RIT_LEGGE_IVATA,0)*100/(100+NVL(PERC_IVA,0)),2)) ),'999G999G990D99')) FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI.ID) AS IMPONIBILE, " _
                & " (SELECT TRIM(TO_CHAR(( SUM( ROUND(NVL(RIT_LEGGE_IVATA,0)-NVL(RIT_LEGGE_IVATA,0)*100/(100+NVL(PERC_IVA,0)),2)) ),'999G999G990D99')) FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI.ID) AS IVA  " _
                & " FROM SISCOM_MI.PAGAMENTI WHERE EXISTS (SELECT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2 AND PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI.ID) " _
                & condizioneFatturaMM _
                & condizioneRicercaMM _
                & condizioneRicercaBPRit _
                & condizioneDataCDP _
                & " /*AND PAGAMENTI.PROGR=1208 AND PAGAMENTI.ANNO=2015*/ "

            Dim daRit As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtRit As New Data.DataTable
            daRit.Fill(dtRit)
            daRit.Dispose()
            'Dim idPagamento As Integer = 0
            ragioneSociale = ""
            contPagamentiMM = 0
            contFattureMM = 0
            contPrenotazioniMM = 0
            Dim dtDefRit As New Data.DataTable
            dtDefRit.Columns.Add("ID_PAGAMENTO")
            dtDefRit.Columns.Add("COD_FORNITORE")
            dtDefRit.Columns.Add("RAGIONE_SOCIALE")
            dtDefRit.Columns.Add("COD_FISCALE")
            dtDefRit.Columns.Add("NUMERO_CDP")
            'dtDef.Columns.Add("ANNO_CDP")
            dtDefRit.Columns.Add("DATA_CDP")
            dtDefRit.Columns.Add("IMPONIBILE")
            dtDefRit.Columns.Add("IVA")
            dtDefRit.Columns.Add("TOT")
            'PRENOTAZIONI
            dtDefRit.Columns.Add("ID_VOCE_PF")
            dtDefRit.Columns.Add("ANNO_PF")
            dtDefRit.Columns.Add("VOCE_PF")
            dtDefRit.Columns.Add("CAPITOLO")
            dtDefRit.Columns.Add("IMPONIBILE_D")
            dtDefRit.Columns.Add("IVA_D")
            dtDefRit.Columns.Add("TOTALE_D")
            'FATTURA MM
            dtDefRit.Columns.Add("ID_FATTURA_MM")
            dtDefRit.Columns.Add("NUMERO_RDS")
            dtDefRit.Columns.Add("N_FATT_FORN")
            dtDefRit.Columns.Add("DATA_FATT")
            dtDefRit.Columns.Add("DATA_REG")
            dtDefRit.Columns.Add("COD_OP_CONT")
            dtDefRit.Columns.Add("IMPORTO_TOTALE")
            'PAGAMENTI MM
            dtDefRit.Columns.Add("ID_PAGAMENTO_MM")
            dtDefRit.Columns.Add("NUMERO_PAG")
            dtDefRit.Columns.Add("DATA_PAG")
            dtDefRit.Columns.Add("IMPORTO_PAGATO")
            dtDefRit.Columns.Add("COD_OP_CONTAB")
            dtDefRit.Columns.Add("CUP")
            dtDefRit.Columns.Add("CIG")
            Dim rigaRit As Data.DataRow
            rigaRit = dtDefRit.NewRow
            rigaRit.Item("ID_PAGAMENTO") = ""
            rigaRit.Item("COD_FORNITORE") = "CODICE FORNITORE"
            rigaRit.Item("RAGIONE_SOCIALE") = "RAGIONE SOCIALE"
            rigaRit.Item("COD_FISCALE") = "COD. FISCALE PARTITA IVA"
            rigaRit.Item("NUMERO_CDP") = "NUMERO CDP"
            'rigaRit.Item("ANNO_CDP") = "ANNO"
            rigaRit.Item("DATA_CDP") = "DATA CDP"
            rigaRit.Item("IMPONIBILE") = "IMPONIBILE"
            rigaRit.Item("IVA") = "IVA"
            rigaRit.Item("TOT") = "TOTALE"
            rigaRit.Item("ID_VOCE_PF") = ""
            rigaRit.Item("ANNO_PF") = "ANNO PF"
            rigaRit.Item("VOCE_PF") = "VOCE"
            rigaRit.Item("CAPITOLO") = "CAPITOLO"
            rigaRit.Item("IMPONIBILE_D") = "IMPONIBILE DETTAGLIO"
            rigaRit.Item("IVA_D") = "IVA DETTAGLIO"
            rigaRit.Item("TOTALE_D") = "TOTALE DETTAGLIO"
            rigaRit.Item("ID_FATTURA_MM") = ""
            rigaRit.Item("NUMERO_RDS") = "PROTOCOLLO FATTURA"
            rigaRit.Item("N_FATT_FORN") = "NUMERO FATTURA FORNITORE"
            rigaRit.Item("DATA_FATT") = "DATA FATTURA"
            rigaRit.Item("DATA_REG") = "DATA REGISTRAZIONE"
            rigaRit.Item("COD_OP_CONT") = "CODICE OPERAZIONE CONTABILE"
            rigaRit.Item("IMPORTO_TOTALE") = "IMPORTO"
            rigaRit.Item("ID_PAGAMENTO_MM") = ""
            rigaRit.Item("NUMERO_PAG") = "PROTOCOLLO PAGAMENTO"
            rigaRit.Item("DATA_PAG") = "DATA PAGAMENTO"
            rigaRit.Item("IMPORTO_PAGATO") = "IMPORTO PAGATO"
            rigaRit.Item("COD_OP_CONTAB") = "CODICE OPERAZIONE CONTABILE"
            rigaRit.Item("CUP") = "CUP"
            rigaRit.Item("CIG") = "CIG"
            'dtDefRit.Rows.Add(rigaRit)

            indiceTabella = 0
            prodotto = 1
            idPrenotazione = 0
            For Each pagamento As Data.DataRow In dtRit.Rows
                idPagamento = par.IfNull(pagamento.Item("ID_PAGAMENTO"), 0)
                par.cmd.CommandText = "SELECT " _
                    & " NVL(SUM(COUNT(DISTINCT PRENOTAZIONI.ID_VOCE_PF)),0) " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO_RIT_LEGGE=" & idPagamento _
                    & " AND ID_STATO=2 " _
                    & " GROUP BY PRENOTAZIONI.ID_VOCE_PF," _
                    & " PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE"
                contPrenotazioniMM = Math.Max(par.cmd.ExecuteScalar, 1)
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.FATTURE_MM WHERE ID_PAGAMENTO=" & idPagamento & condizioniRicercaFattureMM
                contFattureMM = Math.Max(par.cmd.ExecuteScalar, 1)
                If contFattureMM = 0 Then
                    contFattureMM = 1
                End If
                par.cmd.CommandText = "SELECT COUNT(COUNT(*)) FROM SISCOM_MI.PAGAMENTI_MM,SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_MM.ID=PAGAMENTI_LIQUIDATI.ID_PAGAMENTO_MM AND PAGAMENTI_MM.ID_PAGAMENTO=" & idPagamento & condizioniRicerca _
                    & " GROUP BY ID_VOCE_PF,ID_PAGAMENTO_MM"
                contPagamentiMM = Math.Max(par.cmd.ExecuteScalar, 1)
                If contPagamentiMM = 0 Then
                    contPagamentiMM = 1
                End If
                prodotto = contPrenotazioniMM * contPagamentiMM * contFattureMM
                For i As Integer = 1 To prodotto
                    rigaRit = dtDefRit.NewRow
                    dtDefRit.Rows.Add(rigaRit)
                Next
                For i As Integer = indiceTabella To indiceTabella + prodotto - 1
                    dtDefRit.Rows(i).Item("ID_PAGAMENTO") = pagamento.Item("ID_PAGAMENTO")
                    dtDefRit.Rows(i).Item("COD_FORNITORE") = pagamento.Item("COD_FORNITORE")
                    dtDefRit.Rows(i).Item("RAGIONE_SOCIALE") = pagamento.Item("RAGIONE_SOCIALE")
                    dtDefRit.Rows(i).Item("COD_FISCALE") = pagamento.Item("COD_FISCALE")
                    dtDefRit.Rows(i).Item("NUMERO_CDP") = pagamento.Item("NUMERO_CDP")
                    'dtDefRit.Rows(i).Item("ANNO_CDP") = pagamento.Item("ANNO_CDP")
                    dtDefRit.Rows(i).Item("DATA_CDP") = pagamento.Item("DATA_CDP")
                    dtDefRit.Rows(i).Item("IMPONIBILE") = pagamento.Item("IMPONIBILE")
                    dtDefRit.Rows(i).Item("IVA") = pagamento.Item("IVA")
                    dtDefRit.Rows(i).Item("TOT") = pagamento.Item("TOT")
                Next
                par.cmd.CommandText = "SELECT " _
                    & " PRENOTAZIONI.ID_VOCE_PF AS ID_VOCE_PF," _
                    & " PRENOTAZIONI.ANNO AS ANNO_PF," _
                    & " (SELECT PF_VOCI.CODICE FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF) AS VOCE_PF," _
                    & " (SELECT PF_CAPITOLI.COD FROM SISCOM_MI.PF_CAPITOLI  WHERE ID = (SELECT ID_CAPITOLO FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF)) AS CAPITOLO," _
                    & " TRIM(TO_CHAR((SUM( ROUND(NVL(RIT_LEGGE_IVATA,0),2)) ),'999G999G990D99')) AS TOTALE_D, " _
                    & " TRIM(TO_CHAR((SUM( ROUND(NVL(RIT_LEGGE_IVATA,0)*100/(100+NVL(PERC_IVA,0)),2)) ),'999G999G990D99')) AS IMPONIBILE_D, " _
                    & " TRIM(TO_CHAR((SUM( ROUND(NVL(RIT_LEGGE_IVATA,0)-NVL(RIT_LEGGE_IVATA,0)*100/(100+NVL(PERC_IVA,0)),2)) ),'999G999G990D99')) AS IVA_D " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO_RIT_LEGGE=" & idPagamento _
                    & " AND ID_STATO=2 " _
                    & " GROUP BY PRENOTAZIONI.ID_VOCE_PF,PRENOTAZIONI.ANNO ORDER BY 1"
                lettoreP = par.cmd.ExecuteReader
                Dim indiceInizialeTabella As Integer = indiceTabella
                Dim contLett As Integer = indiceInizialeTabella
                While lettoreP.Read
                    For i As Integer = 0 To prodotto / contPrenotazioniMM - 1
                        dtDefRit.Rows(contLett).Item("ID_VOCE_PF") = lettoreP("ID_VOCE_PF")
                        dtDefRit.Rows(contLett).Item("ANNO_PF") = lettoreP("ANNO_PF")
                        dtDefRit.Rows(contLett).Item("VOCE_PF") = lettoreP("VOCE_PF")
                        dtDefRit.Rows(contLett).Item("CAPITOLO") = lettoreP("CAPITOLO")
                        dtDefRit.Rows(contLett).Item("IMPONIBILE_D") = lettoreP("IMPONIBILE_D")
                        dtDefRit.Rows(contLett).Item("IVA_D") = lettoreP("IVA_D")
                        dtDefRit.Rows(contLett).Item("TOTALE_D") = lettoreP("TOTALE_D")
                        contLett += 1
                    Next
                End While
                lettoreP.Close()
                par.cmd.CommandText = "SELECT NUMERO AS ID_FATTURA_MM, " _
                    & " NUMERO AS NUMERO_RDS," _
                    & " NUMERO_FATT_FORN AS N_FATT_FORN," _
                    & " SISCOM_MI.GETDATA(DATA_FATTURA) AS DATA_FATT," _
                    & " SISCOM_MI.GETDATA(DATA_SCADENZA) AS DATA_REG," _
                    & " COD_OC AS COD_OP_CONT," _
                    & " TRIM(TO_CHAR(IMPORTO_TOTALE,'999G999G990D99')) AS IMPORTO_TOTALE " _
                    & " FROM SISCOM_MI.FATTURE_MM WHERE ID_PAGAMENTO=" & idPagamento & condizioniRicercaFattureMM _
                    & " ORDER BY 2"
                lettoreP = par.cmd.ExecuteReader
                contLett = indiceInizialeTabella
                While lettoreP.Read
                    For i As Integer = 0 To prodotto / contFattureMM - 1
                        dtDefRit.Rows(contLett).Item("ID_FATTURA_MM") = lettoreP("ID_FATTURA_MM")
                        dtDefRit.Rows(contLett).Item("NUMERO_RDS") = lettoreP("NUMERO_RDS")
                        dtDefRit.Rows(contLett).Item("N_FATT_FORN") = lettoreP("N_FATT_FORN")
                        dtDefRit.Rows(contLett).Item("DATA_FATT") = lettoreP("DATA_FATT")
                        dtDefRit.Rows(contLett).Item("DATA_REG") = lettoreP("DATA_REG")
                        dtDefRit.Rows(contLett).Item("COD_OP_CONT") = lettoreP("COD_OP_CONT")
                        dtDefRit.Rows(contLett).Item("IMPORTO_TOTALE") = lettoreP("IMPORTO_TOTALE")
                        contLett += 1
                    Next
                End While
                lettoreP.Close()
                par.cmd.CommandText = "SELECT PAGAMENTI_MM.ID AS ID_PAGAMENTO_MM," _
                    & " NUMERO_PAGAMENTO AS NUMERO_PAG," _
                    & " SISCOM_MI.GETDATA(DATA_PAGAMENTO) AS DATA_PAG," _
                    & " /*TRIM(TO_CHAR(IMPORTO_PAGATO/100,'999G999G990D99')) AS IMPORTO_PAGATO,*/" _
                    & " TRIM(TO_CHAR(sum(pagamenti_liquidati.importo),'999G999G990D99')) AS IMPORTO_PAGATO," _
                    & " COD_OC AS COD_OP_CONTAB," _
                    & " /*(SELECT CIG FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER(NUMERO_CDP) AND ANNO=TO_NUMBER(ANNO_CDP))) AS CIG,*/" _
                    & " (SELECT CIG FROM APPALTI WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.PAGAMENTI WHERE ID =PAGAMENTI_MM.ID_PAGAMENTO )) AS CIG," _
                    & " /*(SELECT CUP FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=TO_NUMBER(NUMERO_CDP) AND ANNO=TO_NUMBER(ANNO_CDP))) AS CUP*/" _
                    & " (SELECT CUP FROM APPALTI WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.PAGAMENTI WHERE ID =PAGAMENTI_MM.ID_PAGAMENTO )) AS CUP" _
                    & " FROM SISCOM_MI.PAGAMENTI_MM,SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_MM.ID_PAGAMENTO=" & idPagamento & condizioniRicerca _
                    & " and pagamenti_liquidati.id_pagamento_mm=pagamenti_mm.id " _
                    & " group by pagamenti_liquidati.id_pagamento_mm," _
                    & " pagamenti_mm.ID, id_voce_pf, NUMERO_PAGAMENTO, DATA_PAGAMENTO, COD_OC, pagamenti_mm.ID_PAGAMENTO " _
                    & " ORDER BY ID_VOCE_PF "
                lettoreP = par.cmd.ExecuteReader
                contLett = indiceInizialeTabella
                While lettoreP.Read
                    For i As Integer = 0 To prodotto / contPagamentiMM - 1
                        dtDefRit.Rows(contLett).Item("ID_PAGAMENTO_MM") = lettoreP("ID_PAGAMENTO_MM")
                        dtDefRit.Rows(contLett).Item("NUMERO_PAG") = lettoreP("NUMERO_PAG")
                        dtDefRit.Rows(contLett).Item("DATA_PAG") = lettoreP("DATA_PAG")
                        dtDefRit.Rows(contLett).Item("IMPORTO_PAGATO") = lettoreP("IMPORTO_PAGATO")
                        dtDefRit.Rows(contLett).Item("COD_OP_CONTAB") = lettoreP("COD_OP_CONTAB")
                        dtDefRit.Rows(contLett).Item("CUP") = lettoreP("CUP")
                        dtDefRit.Rows(contLett).Item("CIG") = lettoreP("CIG")
                        contLett += 1
                    Next
                End While
                lettoreP.Close()
                indiceTabella += prodotto
            Next
            connData.chiudi()

            Dim dtAll As New Data.DataTable
            dtAll.Merge(dtDef)
            dtAll.Merge(dtDefRit)

            If dtAll.Rows.Count > 0 Then
                DataGridPagamenti.DataSource = dtAll
                DataGridPagamenti.DataBind()
                DataGridPagamenti.Visible = True
            Else
                DataGridPagamenti.Visible = False
            End If
            'connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - Ricerca - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGridPagamenti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridPagamenti.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Text = "CERTIFICATO DI PAGAMENTO"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Attributes.Add("colspan", 8)
            'e.Item.Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Visible = False
            'e.Item.Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IVA")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "TOT")).Visible = False
            'e.Item.Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Text = "PIANO FINANZIARIO"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Attributes.Add("colspan", 6)
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text = "FATTURE"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Attributes.Add("colspan", 5)
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "DATA_FATT")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "DATA_REG")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text = "PAGAMENTI"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Attributes.Add("colspan", 7)
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
        Dim annoCDPPrecedente As String = ""
        Dim dataCDPPrecedente As String = ""
        Dim imponibilePrecedente As String = ""
        Dim ivaPrecedente As String = ""
        Dim totPrecedente As String = ""
        Dim idPagamentoPrecedente As String = ""
        Dim idVocePrecedente As String = ""
        Dim vocePrecedente As String = ""
        Dim annoPrecedente As String = ""
        Dim capitoloPrecedente As String = ""
        Dim imponibileDPrecedente As String = ""
        Dim ivaDPrecedente As String = ""
        Dim totaleDPrecedente As String = ""
        Dim numeroPrecedente As String = ""
        Dim dataPrecedente As String = ""
        Dim importoPagatoPrecedente As String = ""
        Dim codOperazioneContabilePrecedente As String = ""
        Dim numeroRDSPrecedente As String = ""
        Dim idFatturammPrecedente As String = ""
        Dim nFattFornPrecedente As String = ""
        Dim dataFattPrecedente As String = ""
        Dim dataRegPrecedente As String = ""
        Dim codOpContPrecedente As String = ""
        Dim importoTotalePrecedente As String = ""
        Dim cupPrecedente As String = ""
        Dim cigPrecedente As String = ""
        Dim idPagamentoMMPrecedente As String = ""

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
                'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Attributes.Add("rowspan", 1)
                'ANNOCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Attributes.Add("rowspan", 1)
                DATACDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Attributes.Add("rowspan", 1)
                imponibilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Attributes.Add("rowspan", 1)
                ivaPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Attributes.Add("rowspan", 1)
                totPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Attributes.Add("rowspan", 1)
                idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Attributes.Add("rowspan", 1)
                annoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Text
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
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).Attributes.Add("rowspan", 1)
                numeroRDSPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Attributes.Add("rowspan", 1)
                nFattFornPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Attributes.Add("rowspan", 1)
                dataFattPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_REG")).Attributes.Add("rowspan", 1)
                dataRegPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_REG")).Text
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
                    And dataCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Text _
                    And imponibilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text _
                    And ivaPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text _
                    And totPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text Then
                    'And annoCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Text 

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

                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Visible = False
                    'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).RowSpan) + 1
                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).RowSpan = 1

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
                    And annoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Text _
                    And vocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Text _
                    And capitoloPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Text _
                    And imponibileDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Text _
                    And ivaDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Text _
                    And totaleDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Text Then

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).RowSpan = 1

                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Visible = False
                    'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).RowSpan) + 1
                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).RowSpan = 1

                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Visible = False
                    'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).RowSpan) + 1
                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).RowSpan = 1

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
                    And idFatturammPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).Text _
                    And numeroRDSPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text _
                    And nFattFornPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Text _
                    And dataFattPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Text _
                    And dataRegPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_REG")).Text _
                    And codOpContPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Text _
                    And importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Text Then

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_REG")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_REG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_REG")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_REG")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).RowSpan = 1

                End If


                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                    And idPagamentoMMPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).Text _
                    And idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text _
                    And annoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Text _
                    And numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text _
                    And dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text _
                    And importoPagatoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text _
                    And codOperazioneContabilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Text _
                    And cupPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Text _
                    And cigPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Text Then


                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).RowSpan = 1

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



                'If numeroCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Text Then
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan = 1

                '    If idVocePrecedente <> DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text Then
                '        DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text = Format(CDec(DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text) + CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text), "#,##0.00")
                '        DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text = Format(CDec(DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text) + CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text), "#,##0.00")
                '        DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text = Format(CDec(DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text) + CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text), "#,##0.00")
                '    End If

                'End If

                'If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                '    And numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text _
                '    And dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text Then

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan = 1


                '    'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text = Format(CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text), "#,##0.00")


                'End If


            End If


            If i = DataGridPagamenti.Items.Count - 1 Then
                DataGridPagamenti.Items(i).BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD")
            Else
                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text Then
                    DataGridPagamenti.Items(i).BackColor = DataGridPagamenti.Items(i + 1).BackColor
                Else
                    If DataGridPagamenti.Items(i + 1).BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD") Then
                        DataGridPagamenti.Items(i).BackColor = Drawing.ColorTranslator.FromHtml("#BBBBBB")
                    Else
                        DataGridPagamenti.Items(i).BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD")
                    End If
                End If

            End If

            idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text
            codFornitorePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Text
            ragioneSocialePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Text
            codFiscalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Text
            numeroCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Text
            'annoCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Text
            dataCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Text
            imponibilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text
            ivaPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text
            totPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text

            idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text
            annoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Text
            vocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Text
            capitoloPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Text
            imponibileDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Text
            ivaDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Text
            totaleDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Text

            idPagamentoMMPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).Text
            numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text
            dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text
            importoPagatoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text
            codOperazioneContabilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Text
            cupPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Text
            cigPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Text

            idFatturammPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).Text
            numeroRDSPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text
            nFattFornPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Text
            dataFattPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Text
            dataRegPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_REG")).Text
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

    Protected Sub AggiustaDataTable(ByVal dtDef As Data.DataTable, ByRef dtDef2 As Data.DataTable)
        Dim codFornitorePrecedente As String = ""
        Dim ragioneSocialePrecedente As String = ""
        Dim codFiscalePrecedente As String = ""
        Dim numeroCDPPrecedente As String = ""
        Dim annoCDPPrecedente As String = ""
        Dim dataCDPPrecedente As String = ""
        Dim imponibilePrecedente As String = ""
        Dim ivaPrecedente As String = ""
        Dim totPrecedente As String = ""
        Dim idPagamentoPrecedente As String = ""
        Dim idVocePrecedente As String = ""
        Dim vocePrecedente As String = ""
        Dim annoPrecedente As String = ""
        Dim capitoloPrecedente As String = ""
        Dim imponibileDPrecedente As String = ""
        Dim ivaDPrecedente As String = ""
        Dim totaleDPrecedente As String = ""
        Dim numeroPrecedente As String = ""
        Dim dataPrecedente As String = ""
        Dim importoPagatoPrecedente As String = ""
        Dim codOperazioneContabilePrecedente As String = ""
        Dim numeroRDSPrecedente As String = ""
        Dim idFatturammPrecedente As String = ""
        Dim nFattFornPrecedente As String = ""
        Dim dataFattPrecedente As String = ""
        Dim dataRegistrazionePrecedente As String = ""
        Dim codOpContPrecedente As String = ""
        Dim importoTotalePrecedente As String = ""
        Dim cupPrecedente As String = ""
        Dim cigPrecedente As String = ""
        Dim idPagamentoMMPrecedente As String = ""

        For i As Integer = dtdef.rows.Count - 1 To 0 Step -1

            If i = dtDef.Rows.Count - 1 Then
                dtDef2.Rows(i).Item("ID_PAGAMENTO") = 1
                idPagamentoPrecedente = dtDef2.Rows(i).Item("ID_PAGAMENTO").ToString
                dtDef2.Rows(i).Item("COD_FORNITORE") = 1
                codFornitorePrecedente = dtDef2.Rows(i).Item("COD_FORNITORE").ToString
                dtDef2.Rows(i).Item("RAGIONE_SOCIALE") = 1
                ragioneSocialePrecedente = dtDef2.Rows(i).Item("RAGIONE_SOCIALE").ToString
                dtDef2.Rows(i).Item("COD_FISCALE") = 1
                codFiscalePrecedente = dtDef2.Rows(i).Item("COD_FISCALE").ToString
                dtDef2.Rows(i).Item("NUMERO_CDP") = 1
                numeroCDPPrecedente = dtDef2.Rows(i).Item("NUMERO_CDP").ToString
                'dtDef2.Rows(i).Item("ANNO_CDP") = 1
                'ANNOCDPPrecedente = dtDef2.Rows(i).Item("ANNO_CDP").ToString
                dtDef2.Rows(i).Item("DATA_CDP") = 1
                dataCDPPrecedente = dtDef2.Rows(i).Item("DATA_CDP").ToString
                dtDef2.Rows(i).Item("IMPONIBILE") = 1
                imponibilePrecedente = dtDef2.Rows(i).Item("IMPONIBILE").ToString
                dtDef2.Rows(i).Item("IVA") = 1
                ivaPrecedente = dtDef2.Rows(i).Item("IVA").ToString
                dtDef2.Rows(i).Item("TOT") = 1
                totPrecedente = dtDef2.Rows(i).Item("TOT").ToString
                dtDef2.Rows(i).Item("ID_VOCE_PF") = 1
                idVocePrecedente = dtDef2.Rows(i).Item("ID_VOCE_PF").ToString
                dtDef2.Rows(i).Item("ANNO_PF") = 1
                annoPrecedente = dtDef2.Rows(i).Item("ANNO_PF").ToString
                dtDef2.Rows(i).Item("VOCE_PF") = 1
                vocePrecedente = dtDef2.Rows(i).Item("VOCE_PF").ToString
                dtDef2.Rows(i).Item("CAPITOLO") = 1
                capitoloPrecedente = dtDef2.Rows(i).Item("CAPITOLO").ToString
                dtDef2.Rows(i).Item("IMPONIBILE_D") = 1
                imponibileDPrecedente = dtDef2.Rows(i).Item("IMPONIBILE_D").ToString
                dtDef2.Rows(i).Item("IVA_D") = 1
                ivaDPrecedente = dtDef2.Rows(i).Item("IVA_D").ToString
                dtDef2.Rows(i).Item("TOTALE_D") = 1
                totaleDPrecedente = dtDef2.Rows(i).Item("TOTALE_D").ToString
                dtDef2.Rows(i).Item("NUMERO_PAG") = 1
                numeroPrecedente = dtDef2.Rows(i).Item("NUMERO_PAG").ToString
                dtDef2.Rows(i).Item("DATA_PAG") = 1
                dataPrecedente = dtDef2.Rows(i).Item("DATA_PAG").ToString
                dtDef2.Rows(i).Item("IMPORTO_PAGATO") = 1
                importoPagatoPrecedente = dtDef2.Rows(i).Item("IMPORTO_PAGATO").ToString
                dtDef2.Rows(i).Item("COD_OP_CONTAB") = 1
                codOperazioneContabilePrecedente = dtDef2.Rows(i).Item("COD_OP_CONTAB").ToString
                dtDef2.Rows(i).Item("NUMERO_RDS") = 1
                numeroRDSPrecedente = dtDef2.Rows(i).Item("NUMERO_RDS").ToString
                dtDef2.Rows(i).Item("ID_PAGAMENTO_MM") = 1
                numeroRDSPrecedente = dtDef2.Rows(i).Item("ID_PAGAMENTO_MM").ToString
                dtDef2.Rows(i).Item("N_FATT_FORN") = 1
                nFattFornPrecedente = dtDef2.Rows(i).Item("N_FATT_FORN").ToString
                dtDef2.Rows(i).Item("DATA_fATT") = 1
                dataFattPrecedente = dtDef2.Rows(i).Item("DATA_fATT").ToString
                dtDef2.Rows(i).Item("DATA_REGISTRAZIONE") = 1
                dataRegistrazionePrecedente = dtDef2.Rows(i).Item("DATA_REGISTRAZIONE").ToString
                dtDef2.Rows(i).Item("COD_OP_CONT") = 1
                codOpContPrecedente = dtDef2.Rows(i).Item("COD_OP_CONT").ToString
                dtDef2.Rows(i).Item("IMPORTO_TOTALE") = 1
                importoTotalePrecedente = dtDef2.Rows(i).Item("IMPORTO_TOTALE").ToString
                dtDef2.Rows(i).Item("CUP") = 1
                importoTotalePrecedente = dtDef2.Rows(i).Item("CUP").ToString
                dtDef2.Rows(i).Item("CIG") = 1
                importoTotalePrecedente = dtDef2.Rows(i).Item("CIG").ToString
            Else
                If idPagamentoPrecedente = dtDef2.Rows(i).Item("ID_PAGAMENTO").ToString _
                    And codFornitorePrecedente = dtDef2.Rows(i).Item("COD_FORNITORE").ToString _
                    And ragioneSocialePrecedente = dtDef2.Rows(i).Item("RAGIONE_SOCIALE").ToString _
                    And codFiscalePrecedente = dtDef2.Rows(i).Item("COD_FISCALE").ToString _
                    And numeroCDPPrecedente = dtDef2.Rows(i).Item("NUMERO_CDP").ToString _
                    And dataCDPPrecedente = dtDef2.Rows(i).Item("DATA_CDP").ToString _
                    And imponibilePrecedente = dtDef2.Rows(i).Item("IMPONIBILE").ToString _
                    And ivaPrecedente = dtDef2.Rows(i).Item("IVA").ToString _
                    And totPrecedente = dtDef2.Rows(i).Item("TOT").ToString Then
                    'And annoCDPPrecedente = dtDef2.Rows(i).Item("ANNO_CDP").ToString 

                    'dtDef2.Rows(i+1).Item("COD_FORNITORE")).Visible = False
                    dtDef2.Rows(i).Item("COD_FORNITORE") = Math.Max(1, dtDef2.Rows(i + 1).Item("COD_FORNITORE")) + 1
                    dtDef2.Rows(i + 1).Item("COD_FORNITORE") = 1

                    'dtDef2.Rows(i+1).Item("RAGIONE_SOCIALE")).Visible = False
                    dtDef2.Rows(i).Item("RAGIONE_SOCIALE") = Math.Max(1, dtDef2.Rows(i + 1).Item("RAGIONE_SOCIALE")) + 1
                    dtDef2.Rows(i + 1).Item("RAGIONE_SOCIALE") = 1

                    'dtDef2.Rows(i+1).Item("COD_FISCALE")).Visible = False
                    dtDef2.Rows(i).Item("COD_FISCALE") = Math.Max(1, dtDef2.Rows(i + 1).Item("COD_FISCALE")) + 1
                    dtDef2.Rows(i + 1).Item("COD_FISCALE") = 1

                    'dtDef2.Rows(i+1).Item("NUMERO_CDP")).Visible = False
                    dtDef2.Rows(i).Item("NUMERO_CDP") = Math.Max(1, dtDef2.Rows(i + 1).Item("NUMERO_CDP")) + 1
                    dtDef2.Rows(i + 1).Item("NUMERO_CDP") = 1

                    'dtDef2.Rows(i+1).Item("ANNO_CDP")).Visible = False
                    'dtDef2.Rows(i).Item("ANNO_CDP") = Math.Max(1, dtDef2.Rows(i+1).Item("ANNO_CDP")) + 1
                    'dtDef2.Rows(i+1).Item("ANNO_CDP") = 1

                    'dtDef2.Rows(i+1).Item("DATA_CDP")).Visible = False
                    dtDef2.Rows(i).Item("DATA_CDP") = Math.Max(1, dtDef2.Rows(i + 1).Item("DATA_CDP")) + 1
                    dtDef2.Rows(i + 1).Item("DATA_CDP") = 1

                    'dtDef2.Rows(i+1).Item("IMPONIBILE")).Visible = False
                    dtDef2.Rows(i).Item("IMPONIBILE") = Math.Max(1, dtDef2.Rows(i + 1).Item("IMPONIBILE")) + 1
                    dtDef2.Rows(i + 1).Item("IMPONIBILE") = 1

                    'dtDef2.Rows(i+1).Item("IVA")).Visible = False
                    dtDef2.Rows(i).Item("IVA") = Math.Max(1, dtDef2.Rows(i + 1).Item("IVA")) + 1
                    dtDef2.Rows(i + 1).Item("IVA") = 1

                    'dtDef2.Rows(i+1).Item("TOT")).Visible = False
                    dtDef2.Rows(i).Item("TOT") = Math.Max(1, dtDef2.Rows(i + 1).Item("TOT")) + 1
                    dtDef2.Rows(i + 1).Item("TOT") = 1

                End If


                If idPagamentoPrecedente = dtDef2.Rows(i).Item("ID_PAGAMENTO").ToString _
                    And idVocePrecedente = dtDef2.Rows(i).Item("ID_VOCE_PF").ToString _
                    And annoPrecedente = dtDef2.Rows(i).Item("ANNO_PF").ToString _
                    And vocePrecedente = dtDef2.Rows(i).Item("VOCE_PF").ToString _
                    And capitoloPrecedente = dtDef2.Rows(i).Item("CAPITOLO").ToString _
                    And imponibileDPrecedente = dtDef2.Rows(i).Item("IMPONIBILE_D").ToString _
                    And ivaDPrecedente = dtDef2.Rows(i).Item("IVA_D").ToString _
                    And totaleDPrecedente = dtDef2.Rows(i).Item("TOTALE_D").ToString Then

                    'dtDef2.Rows(i+1).Item("ID_VOCE_PF")).Visible = False
                    dtDef2.Rows(i).Item("ID_VOCE_PF") = Math.Max(1, dtDef2.Rows(i + 1).Item("ID_VOCE_PF")) + 1
                    dtDef2.Rows(i + 1).Item("ID_VOCE_PF") = 1

                    'dtDef2.Rows(i+1).Item("ANNO_PF")).Visible = False
                    'dtDef2.Rows(i).Item("ANNO_PF") = Math.Max(1, dtDef2.Rows(i+1).Item("ANNO_PF")) + 1
                    'dtDef2.Rows(i+1).Item("ANNO_PF") = 1

                    'dtDef2.Rows(i+1).Item("VOCE_PF")).Visible = False
                    'dtDef2.Rows(i).Item("VOCE_PF") = Math.Max(1, dtDef2.Rows(i+1).Item("VOCE_PF")) + 1
                    'dtDef2.Rows(i+1).Item("VOCE_PF") = 1

                    'dtDef2.Rows(i+1).Item("CAPITOLO")).Visible = False
                    dtDef2.Rows(i).Item("CAPITOLO") = Math.Max(1, dtDef2.Rows(i + 1).Item("CAPITOLO")) + 1
                    dtDef2.Rows(i + 1).Item("CAPITOLO") = 1

                    'dtDef2.Rows(i+1).Item("IMPONIBILE_D")).Visible = False
                    dtDef2.Rows(i).Item("IMPONIBILE_D") = Math.Max(1, dtDef2.Rows(i + 1).Item("IMPONIBILE_D")) + 1
                    dtDef2.Rows(i + 1).Item("IMPONIBILE_D") = 1

                    'dtDef2.Rows(i+1).Item("IVA_D")).Visible = False
                    dtDef2.Rows(i).Item("IVA_D") = Math.Max(1, dtDef2.Rows(i + 1).Item("IVA_D")) + 1
                    dtDef2.Rows(i + 1).Item("IVA_D") = 1

                    'dtDef2.Rows(i+1).Item("TOTALE_D")).Visible = False
                    dtDef2.Rows(i).Item("TOTALE_D") = Math.Max(1, dtDef2.Rows(i + 1).Item("TOTALE_D")) + 1
                    dtDef2.Rows(i + 1).Item("TOTALE_D") = 1

                End If


                If idPagamentoPrecedente = dtDef2.Rows(i).Item("ID_PAGAMENTO").ToString _
                    And idFatturammPrecedente = dtDef2.Rows(i).Item("ID_FATTURA_MM").ToString _
                    And numeroRDSPrecedente = dtDef2.Rows(i).Item("NUMERO_RDS").ToString _
                    And nFattFornPrecedente = dtDef2.Rows(i).Item("N_FATT_FORN").ToString _
                    And dataFattPrecedente = dtDef2.Rows(i).Item("DATA_fATT").ToString _
                    And dataRegistrazionePrecedente = dtDef2.Rows(i).Item("DATA_REGISTRAZIONE").ToString _
                    And codOpContPrecedente = dtDef2.Rows(i).Item("COD_OP_CONT").ToString _
                    And importoTotalePrecedente = dtDef2.Rows(i).Item("IMPORTO_TOTALE").ToString Then

                    'dtDef2.Rows(i+1).Item("ID_FATTURA_MM")).Visible = False
                    dtDef2.Rows(i).Item("ID_FATTURA_MM") = Math.Max(1, dtDef2.Rows(i + 1).Item("ID_FATTURA_MM")) + 1
                    dtDef2.Rows(i + 1).Item("ID_FATTURA_MM") = 1

                    'dtDef2.Rows(i+1).Item("NUMERO_RDS")).Visible = False
                    dtDef2.Rows(i).Item("NUMERO_RDS") = Math.Max(1, dtDef2.Rows(i + 1).Item("NUMERO_RDS")) + 1
                    dtDef2.Rows(i + 1).Item("NUMERO_RDS") = 1

                    'dtDef2.Rows(i+1).Item("N_FATT_FORN")).Visible = False
                    dtDef2.Rows(i).Item("N_FATT_FORN") = Math.Max(1, dtDef2.Rows(i + 1).Item("N_FATT_FORN")) + 1
                    dtDef2.Rows(i + 1).Item("N_FATT_FORN") = 1

                    'dtDef2.Rows(i+1).Item("DATA_fATT")).Visible = False
                    dtDef2.Rows(i).Item("DATA_fATT") = Math.Max(1, dtDef2.Rows(i + 1).Item("DATA_fATT")) + 1
                    dtDef2.Rows(i + 1).Item("DATA_fATT") = 1

                    'dtDef2.Rows(i+1).Item("DATA_rEGISTRAZIONE")).Visible = False
                    dtDef2.Rows(i).Item("DATA_rEGISTRAZIONE") = Math.Max(1, dtDef2.Rows(i + 1).Item("DATA_rEGISTRAZIONE")) + 1
                    dtDef2.Rows(i + 1).Item("DATA_rEGISTRAZIONE") = 1

                    'dtDef2.Rows(i+1).Item("COD_OP_CONT")).Visible = False
                    dtDef2.Rows(i).Item("COD_OP_CONT") = Math.Max(1, dtDef2.Rows(i + 1).Item("COD_OP_CONT")) + 1
                    dtDef2.Rows(i + 1).Item("COD_OP_CONT") = 1

                    'dtDef2.Rows(i+1).Item("IMPORTO_TOTALE")).Visible = False
                    dtDef2.Rows(i).Item("IMPORTO_TOTALE") = Math.Max(1, dtDef2.Rows(i + 1).Item("IMPORTO_TOTALE")) + 1
                    dtDef2.Rows(i + 1).Item("IMPORTO_TOTALE") = 1

                End If


                If idPagamentoPrecedente = dtDef2.Rows(i).Item("ID_PAGAMENTO").ToString _
                    And idPagamentoMMPrecedente = dtDef2.Rows(i).Item("ID_PAGAMENTO_MM").ToString _
                    And idVocePrecedente = dtDef2.Rows(i).Item("ID_VOCE_PF").ToString _
                    And annoPrecedente = dtDef2.Rows(i).Item("ANNO_PF").ToString _
                    And numeroPrecedente = dtDef2.Rows(i).Item("NUMERO_PAG").ToString _
                    And dataPrecedente = dtDef2.Rows(i).Item("DATA_PAG").ToString _
                    And importoPagatoPrecedente = dtDef2.Rows(i).Item("IMPORTO_PAGATO").ToString _
                    And codOperazioneContabilePrecedente = dtDef2.Rows(i).Item("COD_OP_CONTAB").ToString _
                    And cupPrecedente = dtDef2.Rows(i).Item("CUP").ToString _
                    And cigPrecedente = dtDef2.Rows(i).Item("CIG").ToString Then


                    'dtDef2.Rows(i+1).Item("ID_PAGAMENTO_MM")).Visible = False
                    dtDef2.Rows(i).Item("ID_PAGAMENTO_MM") = Math.Max(1, dtDef2.Rows(i + 1).Item("ID_PAGAMENTO_MM")) + 1
                    dtDef2.Rows(i + 1).Item("ID_PAGAMENTO_MM") = 1

                    'dtDef2.Rows(i+1).Item("NUMERO_PAG")).Visible = False
                    dtDef2.Rows(i).Item("NUMERO_PAG") = Math.Max(1, dtDef2.Rows(i + 1).Item("NUMERO_PAG")) + 1
                    dtDef2.Rows(i + 1).Item("NUMERO_PAG") = 1

                    'dtDef2.Rows(i+1).Item("DATA_PAG")).Visible = False
                    dtDef2.Rows(i).Item("DATA_PAG") = Math.Max(1, dtDef2.Rows(i + 1).Item("DATA_PAG")) + 1
                    dtDef2.Rows(i + 1).Item("DATA_PAG") = 1

                    'dtDef2.Rows(i+1).Item("IMPORTO_PAGATO")).Visible = False
                    dtDef2.Rows(i).Item("IMPORTO_PAGATO") = Math.Max(1, dtDef2.Rows(i + 1).Item("IMPORTO_PAGATO")) + 1
                    dtDef2.Rows(i + 1).Item("IMPORTO_PAGATO") = 1

                    'dtDef2.Rows(i+1).Item("COD_OP_CONTAB")).Visible = False
                    dtDef2.Rows(i).Item("COD_OP_CONTAB") = Math.Max(1, dtDef2.Rows(i + 1).Item("COD_OP_CONTAB")) + 1
                    dtDef2.Rows(i + 1).Item("COD_OP_CONTAB") = 1

                    'dtDef2.Rows(i+1).Item("CUP")).Visible = False
                    dtDef2.Rows(i).Item("CUP") = Math.Max(1, dtDef2.Rows(i + 1).Item("CUP")) + 1
                    dtDef2.Rows(i + 1).Item("CUP") = 1

                    'dtDef2.Rows(i+1).Item("CIG")).Visible = False
                    dtDef2.Rows(i).Item("CIG") = Math.Max(1, dtDef2.Rows(i + 1).Item("CIG")) + 1
                    dtDef2.Rows(i + 1).Item("CIG") = 1

                End If



                'If numeroCDPPrecedente = dtDef2.Rows(i).Item("NUMERO_CDP").ToString Then
                '    dtDef2.Rows(i+1).Item("NUMERO_CDP")).Visible = False
                '    dtDef2.Rows(i).Item("NUMERO_CDP") = Math.Max(1, dtDef2.Rows(i+1).Item("NUMERO_CDP")) + 1
                '    dtDef2.Rows(i+1).Item("NUMERO_CDP") = 1

                '    dtDef2.Rows(i+1).Item("COD_FORNITORE")).Visible = False
                '    dtDef2.Rows(i).Item("COD_FORNITORE") = Math.Max(1, dtDef2.Rows(i+1).Item("COD_FORNITORE")) + 1
                '    dtDef2.Rows(i+1).Item("COD_FORNITORE") = 1

                '    dtDef2.Rows(i+1).Item("RAGIONE_SOCIALE")).Visible = False
                '    dtDef2.Rows(i).Item("RAGIONE_SOCIALE") = Math.Max(1, dtDef2.Rows(i+1).Item("RAGIONE_SOCIALE")) + 1
                '    dtDef2.Rows(i+1).Item("RAGIONE_SOCIALE") = 1

                '    dtDef2.Rows(i+1).Item("COD_FISCALE")).Visible = False
                '    dtDef2.Rows(i).Item("COD_FISCALE") = Math.Max(1, dtDef2.Rows(i+1).Item("COD_FISCALE")) + 1
                '    dtDef2.Rows(i+1).Item("COD_FISCALE") = 1

                '    dtDef2.Rows(i+1).Item("IMPONIBILE")).Visible = False
                '    dtDef2.Rows(i).Item("IMPONIBILE") = Math.Max(1, dtDef2.Rows(i+1).Item("IMPONIBILE")) + 1
                '    dtDef2.Rows(i+1).Item("IMPONIBILE") = 1

                '    dtDef2.Rows(i+1).Item("IVA")).Visible = False
                '    dtDef2.Rows(i).Item("IVA") = Math.Max(1, dtDef2.Rows(i+1).Item("IVA")) + 1
                '    dtDef2.Rows(i+1).Item("IVA") = 1

                '    dtDef2.Rows(i+1).Item("TOT")).Visible = False
                '    dtDef2.Rows(i).Item("TOT") = Math.Max(1, dtDef2.Rows(i+1).Item("TOT")) + 1
                '    dtDef2.Rows(i+1).Item("TOT") = 1

                '    If idVocePrecedente <> dtDef2.Rows(i).Item("ID_VOCE_PF").ToString Then
                '        dtDef2.Rows(i).Item("IMPONIBILE").ToString = Format(CDec(dtDef2.Rows(i).Item("IMPONIBILE").ToString) + CDec(dtDef2.Rows(i+1).Item("IMPONIBILE").ToString), "#,##0.00")
                '        dtDef2.Rows(i).Item("IVA").ToString = Format(CDec(dtDef2.Rows(i).Item("IVA").ToString) + CDec(dtDef2.Rows(i+1).Item("IVA").ToString), "#,##0.00")
                '        dtDef2.Rows(i).Item("TOT").ToString = Format(CDec(dtDef2.Rows(i).Item("TOT").ToString) + CDec(dtDef2.Rows(i+1).Item("TOT").ToString), "#,##0.00")
                '    End If

                'End If

                'If idPagamentoPrecedente = dtDef2.Rows(i).Item("ID_PAGAMENTO").ToString _
                '    And numeroPrecedente = dtDef2.Rows(i).Item("NUMERO_PAG").ToString _
                '    And dataPrecedente = dtDef2.Rows(i).Item("DATA_PAG").ToString Then

                '    dtDef2.Rows(i+1).Item("NUMERO_PAG")).Visible = False
                '    dtDef2.Rows(i).Item("NUMERO_PAG") = Math.Max(1, dtDef2.Rows(i+1).Item("NUMERO_PAG")) + 1
                '    dtDef2.Rows(i+1).Item("NUMERO_PAG") = 1

                '    dtDef2.Rows(i+1).Item("DATA_PAG")).Visible = False
                '    dtDef2.Rows(i).Item("DATA_PAG") = Math.Max(1, dtDef2.Rows(i+1).Item("DATA_PAG")) + 1
                '    dtDef2.Rows(i+1).Item("DATA_PAG") = 1

                '    dtDef2.Rows(i+1).Item("IMPORTO_PAGATO")).Visible = False
                '    dtDef2.Rows(i).Item("IMPORTO_PAGATO") = Math.Max(1, dtDef2.Rows(i+1).Item("IMPORTO_PAGATO")) + 1
                '    dtDef2.Rows(i+1).Item("IMPORTO_PAGATO") = 1

                '    dtDef2.Rows(i+1).Item("COD_OP_CONTAB")).Visible = False
                '    dtDef2.Rows(i).Item("COD_OP_CONTAB") = Math.Max(1, dtDef2.Rows(i+1).Item("COD_OP_CONTAB")) + 1
                '    dtDef2.Rows(i+1).Item("COD_OP_CONTAB") = 1

                '    dtDef2.Rows(i+1).Item("CUP")).Visible = False
                '    dtDef2.Rows(i).Item("CUP") = Math.Max(1, dtDef2.Rows(i+1).Item("CUP")) + 1
                '    dtDef2.Rows(i+1).Item("CUP") = 1

                '    dtDef2.Rows(i+1).Item("CIG")).Visible = False
                '    dtDef2.Rows(i).Item("CIG") = Math.Max(1, dtDef2.Rows(i+1).Item("CIG")) + 1
                '    dtDef2.Rows(i+1).Item("CIG") = 1


                '    'dtDef2.Rows(i).Item("IMPORTO_PAGATO").ToString = Format(CDec(dtDef2.Rows(i+1).Item("IMPORTO_PAGATO").ToString), "#,##0.00")


                'End If


            End If


            idPagamentoPrecedente = dtDef2.Rows(i).Item("ID_PAGAMENTO").ToString
            codFornitorePrecedente = dtDef2.Rows(i).Item("COD_FORNITORE").ToString
            ragioneSocialePrecedente = dtDef2.Rows(i).Item("RAGIONE_SOCIALE").ToString
            codFiscalePrecedente = dtDef2.Rows(i).Item("COD_FISCALE").ToString
            numeroCDPPrecedente = dtDef2.Rows(i).Item("NUMERO_CDP").ToString
            'annoCDPPrecedente = dtDef2.Rows(i).Item("ANNO_CDP").ToString
            dataCDPPrecedente = dtDef2.Rows(i).Item("DATA_CDP").ToString
            imponibilePrecedente = dtDef2.Rows(i).Item("IMPONIBILE").ToString
            ivaPrecedente = dtDef2.Rows(i).Item("IVA").ToString
            totPrecedente = dtDef2.Rows(i).Item("TOT").ToString

            idVocePrecedente = dtDef2.Rows(i).Item("ID_VOCE_PF").ToString
            annoPrecedente = dtDef2.Rows(i).Item("ANNO_PF").ToString
            vocePrecedente = dtDef2.Rows(i).Item("VOCE_PF").ToString
            capitoloPrecedente = dtDef2.Rows(i).Item("CAPITOLO").ToString
            imponibileDPrecedente = dtDef2.Rows(i).Item("IMPONIBILE_D").ToString
            ivaDPrecedente = dtDef2.Rows(i).Item("IVA_D").ToString
            totaleDPrecedente = dtDef2.Rows(i).Item("TOTALE_D").ToString

            idPagamentoMMPrecedente = dtDef2.Rows(i).Item("ID_PAGAMENTO_MM").ToString
            numeroPrecedente = dtDef2.Rows(i).Item("NUMERO_PAG").ToString
            dataPrecedente = dtDef2.Rows(i).Item("DATA_PAG").ToString
            importoPagatoPrecedente = dtDef2.Rows(i).Item("IMPORTO_PAGATO").ToString
            codOperazioneContabilePrecedente = dtDef2.Rows(i).Item("COD_OP_CONTAB").ToString
            cupPrecedente = dtDef2.Rows(i).Item("CUP").ToString
            cigPrecedente = dtDef2.Rows(i).Item("CIG").ToString

            idFatturammPrecedente = dtDef2.Rows(i).Item("ID_FATTURA_MM").ToString
            numeroRDSPrecedente = dtDef2.Rows(i).Item("NUMERO_RDS").ToString
            nFattFornPrecedente = dtDef2.Rows(i).Item("N_FATT_FORN").ToString
            dataFattPrecedente = dtDef2.Rows(i).Item("DATA_fATT").ToString
            dataRegistrazionePrecedente = dtDef2.Rows(i).Item("DATA_REGISTRAZIONE").ToString
            codOpContPrecedente = dtDef2.Rows(i).Item("COD_OP_CONT").ToString
            importoTotalePrecedente = dtDef2.Rows(i).Item("IMPORTO_TOTALE").ToString

        Next
    End Sub
End Class