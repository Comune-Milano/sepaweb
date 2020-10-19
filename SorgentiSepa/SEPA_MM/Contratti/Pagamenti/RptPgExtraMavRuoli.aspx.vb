Imports System.Data.OleDb
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_Pagamenti_RptPgExtraMavRuoli
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim sStringaSql As String
    Dim sWhere As String
    Dim TotIncass As Decimal = 0
    Dim TotEcced As Decimal = 0
    Dim TotTotale As Decimal = 0
    Dim TotSpese As Decimal = 0
    Dim TotOneri As Decimal = 0
    Dim TotCanone As Decimal = 0
    Dim TotSindac As Decimal = 0
    Dim TotDepCauz As Decimal = 0
    Dim TotAltro As Decimal = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='divPre' style='position:absolute; background-color:#ffffff; text-align:center; width:100%; height:95%; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br/><br/><br/><br/><br/><br/><br/><br/><img src='../../Contabilita/IMMCONTABILITA/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()
            FindIncassi()
        End If
    End Sub
    Private Sub FindIncassi()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim sValore As String
            Dim sCompara As String
            Dim titoloCond As String = ""
            If Session.Item("MOD_REPORT_RUOLI") = 1 Then
                If par.IfEmpty(Request.QueryString("OPERATORE"), "Null") <> "Null" And par.IfEmpty(Request.QueryString("OPERATORE"), "Null") <> "-1" Then
                    sValore = Request.QueryString("OPERATORE")
                    If InStr(sValore, "*") Then
                        sCompara = " LIKE "
                        Call par.ConvertiJolly(sValore)
                    Else
                        sCompara = " = "
                    End If
                    sWhere = sWhere & " AND operatori.id " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                End If
            End If
            If par.IfEmpty(Request.QueryString("EVDAL"), "Null") <> "Null" Or par.IfEmpty(Request.QueryString("EVAL"), "Null") <> "Null" Then
                Dim condDate As String = ""
                Dim e As Boolean = True
                If par.IfEmpty(Request.QueryString("EVDAL"), "Null") <> "Null" Then
                    sValore = Request.QueryString("EVDAL")
                    If e = True Then
                        condDate = condDate & " AND "
                    End If
                    condDate = condDate & " SUBSTR(DATA_ORA,1,8) >= " & sValore & " "
                    e = True
                    titoloCond = titoloCond & " - Periodo operazione dal " & par.FormattaData(Request.QueryString("EVDAL"))
                End If
                If par.IfEmpty(Request.QueryString("EVAL"), "Null") <> "Null" Then
                    If e = True Then
                        condDate = condDate & " AND "
                    End If
                    sValore = Request.QueryString("EVAL")
                    condDate = condDate & "  SUBSTR(DATA_ORA,1,8) <= " & sValore & " "
                    titoloCond = titoloCond & " al " & par.FormattaData(Request.QueryString("EVAL"))
                End If
                sWhere = sWhere & "  " & condDate & " "
            End If
            If par.IfEmpty(Request.QueryString("RIFDAL"), "Null") <> "Null" Then
                sValore = Request.QueryString("RIFDAL")
                sWhere = sWhere & " AND RIFERIMENTO_DA >= " & sValore & " "
                titoloCond = titoloCond & " - Periodo riferimento dal " & par.FormattaData(Request.QueryString("RIFDAL"))
            End If
            If par.IfEmpty(Request.QueryString("RIFAL"), "Null") <> "Null" Then
                sValore = Request.QueryString("RIFAL")
                sWhere = sWhere & " AND RIFERIMENTO_A <= " & sValore & " "
                titoloCond = titoloCond & " al " & par.FormattaData(Request.QueryString("RIFAL"))
            End If
            If Session.Item("LIVELLO") <> 1 Then
                If Session.Item("MOD_REPORT_RUOLI") = 0 Then
                    sWhere = sWhere & " AND OPERATORI.ID = " & Session.Item("ID_OPERATORE") & " "
                Else
                    If Session.Item("ID_CAF") = 2 Then
                        sWhere = sWhere & " AND OPERATORI.ID_CAF = 2 "
                    End If
                End If
            End If
            If par.IfEmpty(Request.QueryString("DPAG"), "Null") <> "Null" Then
                sValore = Request.QueryString("DPAG")
                sWhere = sWhere & " AND INCASSI_RUOLI.data_pagamento >= " & sValore & " "
                titoloCond = titoloCond & " dal " & par.FormattaData(Request.QueryString("DPAG"))
            End If
            If par.IfEmpty(Request.QueryString("DPAGAL"), "Null") <> "Null" Then
                sValore = Request.QueryString("DPAGAL")
                sWhere = sWhere & " AND INCASSI_RUOLI.data_pagamento <= " & sValore & " "
                titoloCond = titoloCond & " al " & par.FormattaData(Request.QueryString("DPAGAL"))
            End If
            If par.IfEmpty(Request.QueryString("TIPINC"), "-1") <> "-1" Then
                sValore = Request.QueryString("TIPINC")
                sWhere = sWhere & " AND INCASSI_RUOLI.ID_TIPO_PAG = " & sValore & " "
            End If
            If par.IfEmpty(Request.QueryString("S"), "-1") = "1" Then
                sValore = Request.QueryString("S")
                sWhere = sWhere & " AND INCASSI_RUOLI.fl_Sgravio = " & sValore & " "
            End If
            'tabella di generali più campi di dettaglio'

            par.cmd.CommandText = "SELECT  DISTINCT INCASSI_RUOLI.ID AS ID, " _
                                 & "GETDATAORA (INCASSI_RUOLI.DATA_ORA) AS DATA_ORA, " _
                                 & "TIPO_PAG_RUOLO.DESCRIZIONE AS TIPO, " _
                                 & "INCASSI_RUOLI.MOTIVO_PAGAMENTO AS DESCRIZIONE, " _
                                 & " (CASE WHEN NVL (FL_SGRAVIO, 0) =0 THEN 'NO' ELSE 'SI' END) AS sgravio," _
                                 & "TO_CHAR " _
                                 & "(TO_DATE (INCASSI_RUOLI.DATA_PAGAMENTO, 'YYYYMMDD'), " _
                                 & "'DD/MM/YYYY') AS DATA_PAGAMENTO, " _
                                 & "'' AS RIFERIMENTO_DA_A," _
                                 & "('<a href=""javascript:window.open(''../Contratto.aspx?LT=1&ID='||siscom_mi.RAPPORTI_UTENZA.ID||'&COD='||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||''',''CONTRATTO'',''height=780,top=0,left=0,width=1160'');void(0);"">'||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||'</a>') AS COD_CONTRATTO, " _
                                 & "('<a href=""javascript:window.open(''../../CONTABILITA/DatiUtenza.aspx?IDANA='|| siscom_mi.soggetti_contrattuali.id_anagrafica|| '&IDCONT='|| siscom_mi.rapporti_utenza.ID|| ''',''EstrattoConto'');void(0);"">'|| (CASE WHEN anagrafica.ragione_sociale IS NULL THEN anagrafica.cognome || ' ' || anagrafica.nome ELSE anagrafica.ragione_sociale END)|| '</a>') AS intestatario, " _
                                 & "'' AS BOLLETTA,'' AS RIFERIMENTO,TRIM (TO_CHAR (IMPORTO, '9G999G999G990D99')) AS TOTALE " _
                                 & "FROM SISCOM_MI.INCASSI_RUOLI,SISCOM_MI.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI, " _
                                 & "OPERATORI, " _
                                 & "SISCOM_MI.RAPPORTI_UTENZA, " _
                                 & "SISCOM_MI.BOL_BOLLETTE_PAGAMENTI_RUOLO, " _
                                 & "SISCOM_MI.TIPO_PAG_RUOLO " _
                                 & "WHERE OPERATORI.ID = INCASSI_RUOLI.ID_OPERATORE " _
                                 & "AND RAPPORTI_UTENZA.ID = INCASSI_RUOLI.ID_CONTRATTO " _
                                 & "AND INCASSI_RUOLI.ID= BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_INCASSO_RUOLO and nvl(INCASSI_RUOLI.fl_Annullata,0)=0 " _
                                 & "AND TIPO_PAG_RUOLO.ID=INCASSI_RUOLI.ID_TIPO_PAG " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.ID_CONTRATTO = siscom_mi.RAPPORTI_UTENZA.ID " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                 & sWhere _
                                 & "ORDER BY ID ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtIncasso As New Data.DataTable()
            da.Fill(dtIncasso)
            da.Dispose()
            '+++++++++++++clone tabella principale++++++++++++
            Dim dtCopia As Data.DataTable
            dtCopia = dtIncasso.Clone
            dtCopia.TableName = "Export"
            For Each row As Data.DataRow In dtIncasso.Rows
                dtCopia.Rows.Add(row.ItemArray)
                Cerca2(row.Item("id"), par.IfNull(row.Item("tipo"), ""), dtCopia)
            Next
            Dim rTot As Data.DataRow
            rTot = dtCopia.NewRow()
            rTot.Item("DESCRIZIONE") = "T O T A L E"

            rTot.Item("totale") = Format(TotTotale, "##,##0.00")
            dtCopia.Rows.Add(rTot)
            Session.Add("dtExport", dtCopia)
            BindGrid()

            par.cmd.CommandText = "SELECT OPERATORE FROM OPERATORI WHERE ID = " & Request.QueryString("OPERATORE")
            Dim NameOp As String = ""
            Dim lettor As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettor.Read Then
                NameOp = par.IfNull(lettor("OPERATORE"), "")
            End If
            lettor.Close()
            Me.lblTitolo.Text = "Report Pagamenti Manuali Ruoli<br/> " & titoloCond & "<br/> OPERATORE: " & NameOp
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Report Pagamenti Extra Mav:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub Cerca2(ByVal idIncasso As String, ByVal tipo_incasso As String, ByRef dtCopia As Data.DataTable)

        '*****************APERTURA CONNESSIONE***************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim RTotIncass As Decimal = 0
        Dim RTotEcced As Decimal = 0
        Dim RTotTotale As Decimal = 0
        Dim RTotSpese As Decimal = 0
        Dim RTotOneri As Decimal = 0
        Dim RTotCanone As Decimal = 0
        Dim RTotSindac As Decimal = 0
        Dim RTotDepCauz As Decimal = 0
        Dim RTotAltro As Decimal = 0
        
        Dim varname1 As String = ""
        'varname1 = "SELECT DISTINCT INCASSI_RUOLI.ID, " _
        '         & "getdataora (INCASSI_RUOLI.data_ora) AS data_ora," _
        '         & "'" & tipo_incasso & "' as tipo_pag," _
        '         & " INCASSI_RUOLI.motivo_pagamento AS descrizione, " _
        '                              & "('<a href=""javascript:window.open(''../../Contabilita/DettaglioBolletta.aspx?IDCONT='||BOL_BOLLETTE.ID_CONTRATTO||'&IDBOLL='||BOL_BOLLETTE.ID||'&IDANA='||SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA||''',''Dettagli'','''');void(0)"">'||BOL_BOLLETTE.NUM_BOLLETTA||'</a>') AS BOLLETTA," _
        '         & "'' AS data_pagamento, " _
        '         & "'-' AS riferimento_da_a, " _
        '         & "('<a href=""javascript:window.open(''../Contratto.aspx?LT=1&ID='||RAPPORTI_UTENZA.ID||'&COD='||RAPPORTI_UTENZA.COD_CONTRATTO||''',''CONTRATTO'',''height=780,top=0,left=0,width=1160'');void(0);"">'||RAPPORTI_UTENZA.COD_CONTRATTO||'</a>') AS COD_CONTRATTO, " _
        '         & "('<a href=""javascript:window.open(''../../CONTABILITA/DatiUtenza.aspx?IDANA='|| soggetti_contrattuali.id_anagrafica|| '&IDCONT='|| rapporti_utenza.ID|| ''',''EstrattoConto'');void(0);"">'|| (CASE WHEN anagrafica.ragione_sociale IS NULL THEN anagrafica.cognome || ' ' || anagrafica.nome ELSE anagrafica.ragione_sociale END)|| '</a>') AS intestatario," _
        '         & " '' AS riferimento, " _
        '            & "TRIM (TO_CHAR(importo,'9G999G999G990D99')) AS importo_incassato," _
        '         & "TRIM (TO_CHAR (importo, '9G999G999G990D99')) AS totale " _
        '         & "FROM siscom_mi.INCASSI_RUOLI, " _
        '         & "siscom_mi.anagrafica, " _
        '         & "siscom_mi.soggetti_contrattuali, " _
        '         & "siscom_mi.rapporti_utenza,siscom_mi.bol_bollette, " _
        '         & "siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO, " _
        '         & "siscom_mi.TIPO_PAG_RUOLO " _
        '         & "WHERE rapporti_utenza.ID = INCASSI_RUOLI.id_contratto " _
        '         & "AND INCASSI_RUOLI.ID = " _
        '         & "BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_INCASSO_RUOLO " _
        '         & "AND TIPO_PAG_RUOLO.ID = INCASSI_RUOLI.id_tipo_pag " _
        '         & "AND siscom_mi.soggetti_contrattuali.id_contratto = siscom_mi.rapporti_utenza.ID " _
        '         & " AND bol_bollette.ID = siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO.id_bolletta " _
        '         & "AND siscom_mi.soggetti_contrattuali.id_anagrafica = anagrafica.ID " _
        '         & "AND siscom_mi.soggetti_contrattuali.cod_tipologia_occupante = " _
        '         & "'INTE' " _
        '         & "AND INCASSI_RUOLI.ID =" & idIncasso _
        '         & "ORDER BY data_ora ASC"
        'par.cmd.CommandText = varname1
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        'Dim dtEventIcasso As New Data.DataTable()
        'da.Fill(dtEventIcasso)
        'da.Dispose()
        Dim dtlettore As Data.DataTable
        Dim addRow As Data.DataRow
        Dim i As Integer = 0
        Dim ii As Integer = 0
        Dim k As Integer = 0
        'For Each riga As Data.DataRow In dtEventIcasso.Rows
        'dtCopia.Rows.Add(riga.ItemArray)
        varname1 = ""
        varname1 = "SELECT bol_bollette.ID AS id_bolletta, " _
                 & "('<a href=""javascript:window.open(''../../Contabilita/DettaglioBolletta.aspx?IDCONT='||BOL_BOLLETTE.ID_CONTRATTO||'&IDBOLL='||BOL_BOLLETTE.ID||'&IDANA='||SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA||''',''Dettagli'','''');void(0)"">'||BOL_BOLLETTE.NUM_BOLLETTA||'</a>') AS NUM_BOLLETTA," _
                 & "('Dal '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' al '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS ""RIFERIMENTO"", " _
                 & "BOL_BOLLETTE_PAGAMENTI_RUOLO.importo_pagato AS IMPORTO ,'' AS SGRAVIO " _
                 & "FROM siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO, " _
                 & "siscom_mi.bol_bollette, " _
                 & "siscom_mi.soggetti_contrattuali " _
                 & "WHERE ID_INCASSO_RUOLO = " & idIncasso _
                 & " AND bol_bollette.ID = siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO.id_bolletta " _
                 & "AND soggetti_contrattuali.id_contratto = bol_bollette.id_contratto " _
                 & "AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
                 & "ORDER BY id_bolletta ASC"
        par.cmd.CommandText = varname1
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        dtlettore = New Data.DataTable
        da.Fill(dtlettore)
        ii = dtCopia.Rows.Count - 1
        RTotSpese = 0
        RTotOneri = 0
        RTotCanone = 0
        RTotSindac = 0
        RTotDepCauz = 0
        RTotAltro = 0
        RTotIncass = 0
        RTotEcced = 0
        RTotTotale = 0
        For Each lettore As Data.DataRow In dtlettore.Rows
            addRow = dtCopia.NewRow
            addRow.Item("tipo") = tipo_incasso
            addRow.Item("BOLLETTA") = "Num. " & par.IfNull(lettore.Item("NUM_BOLLETTA"), "---")
            addRow.Item("RIFERIMENTO") = par.IfNull(lettore.Item("RIFERIMENTO"), "")
            addRow.Item("TOTALE") = Math.Round(CDec(par.IfNull(lettore.Item("IMPORTO"), 0)), 2)
            RTotTotale += Math.Round(CDec(par.IfNull(lettore.Item("IMPORTO"), "0")), 2)

            dtCopia.Rows.Add(addRow)
        Next

        TotIncass = TotIncass + RTotIncass

        TotTotale = TotTotale + RTotTotale
        'Next

    End Sub
    Private Sub BindGrid()
        Try
            Me.dgvRptPagExtraMav.DataSource = CType(Session.Item("dtExport"), Data.DataTable)
            Me.dgvRptPagExtraMav.DataBind()
            SettaColori()
        Catch ex As Exception
            Session.Add("ERRORE", "Report Pagamenti Extra Mav:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub dgvRptPagExtraMav_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvRptPagExtraMav.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            dgvRptPagExtraMav.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Private Sub SettaColori()
        Dim di As DataGridItem
        For Each di In dgvRptPagExtraMav.Items
            If di.Cells(TrovaIndiceColonna(dgvRptPagExtraMav, "COD_CONTRATTO")).Text <> "&nbsp;" And di.Cells(TrovaIndiceColonna(dgvRptPagExtraMav, "BOLLETTA")).Text = "&nbsp;" Then
                di.BackColor = Drawing.Color.GreenYellow
                di.ForeColor = System.Drawing.Color.Black
            End If
            If di.Cells(TrovaIndiceColonna(dgvRptPagExtraMav, "COD_CONTRATTO")).Text <> "&nbsp;" Then
                di.BackColor = Drawing.Color.GreenYellow
                di.ForeColor = System.Drawing.Color.Black
            End If
            If di.Cells(TrovaIndiceColonna(dgvRptPagExtraMav, "BOLLETTA")).Text <> "&nbsp;" Then
                di.BackColor = Drawing.Color.LightGoldenrodYellow
                di.ForeColor = System.Drawing.Color.Black
            End If
        Next
    End Sub
    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                    TrovaIndiceColonna = Indice
                    Exit For
                End If
                Indice = Indice + 1
            Next
        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Report Pagamenti Extra Mav:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
        Return TrovaIndiceColonna
    End Function
    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            If dgvRptPagExtraMav.Items.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim dtExport As Data.DataTable = CType(Session.Item("dtExport"), Data.DataTable)
                For Each rowExport As Data.DataRow In dtExport.Rows
                    rowExport.Item("COD_CONTRATTO") = par.EliminaLink(rowExport.Item("COD_CONTRATTO").ToString)
                    rowExport.Item("intestatario") = par.EliminaLink(rowExport.Item("intestatario").ToString)
                    rowExport.Item("BOLLETTA") = par.EliminaLink(rowExport.Item("BOLLETTA").ToString)
                Next
                'Dim f As String = par.EsportaExcelDaDTWithDatagrid(dtExport, dgvRptPagExtraMav, "ExportPg", , , , False)
                'Dim f1 As String = xls.EsportaExcelDaDataGridWithDT(ExcelSiSol.Estensione.Office2003_xls, "AA", "aaa", dgvRptPagExtraMav, dtExport)
                Dim nomeFile As String = xls.EsportaExcelDaDataGridWithDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportPgExtramav", "ExportPgExtramav", dgvRptPagExtraMav, dtExport)
                If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                    Response.Redirect("../../FileTemp/" & nomeFile, False)

                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun risultato da esportare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Report Pagamenti Extra Mav GENERALE:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub


End Class
