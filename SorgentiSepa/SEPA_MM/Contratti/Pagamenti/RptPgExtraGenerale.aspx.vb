Imports System.Data.OleDb
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_Pagamenti_RptPgExtraGenerale
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim sStringaSql As String
    Dim sWhere As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
            Me.lblTitolo.Text = "Report Pagamenti MANUALI "
            Cerca()
        End If
    End Sub
    Private Sub Cerca()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim TotIncass As Decimal = 0
            Dim TotEcced As Decimal = 0
            Dim TotTotale As Decimal = 0
            Dim sValore As String
            Dim sCompara As String
            Dim dt As New Data.DataTable
            Dim swhereOperatori As String = ""
            Dim swhereIdTipoPag As String = ""
            If Session.Item("MOD_AMM_RPT_P_EXTRA") = 1 Then
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
            If Session.Item("LIVELLO") <> 1 Then
                If Session.Item("MOD_AMM_RPT_P_EXTRA") = 0 Then
                    sWhere = sWhere & " AND OPERATORI.ID = " & Session.Item("ID_OPERATORE") & " "
                Else
                    If Session.Item("ID_CAF") = 2 Then
                        swhereOperatori = swhereOperatori & " AND OPERATORI.ID_CAF = 2 "
                    End If
                End If
            End If
            If par.IfEmpty(Request.QueryString("EVDAL"), "Null") <> "Null" Or par.IfEmpty(Request.QueryString("EVAL"), "Null") <> "Null" Then
                Dim condDate As String = ""
                Dim e As Boolean = False
                If par.IfEmpty(Request.QueryString("EVDAL"), "Null") <> "Null" Then
                    sValore = Request.QueryString("EVDAL")
                    If e = True Then
                        condDate = condDate & " AND "
                    End If
                    condDate = condDate & " SUBSTR(DATA_ORA,1,8) >= " & sValore & " "
                    e = True
                End If
                If par.IfEmpty(Request.QueryString("EVAL"), "Null") <> "Null" Then
                    If e = True Then
                        condDate = condDate & " AND "
                    End If
                    sValore = Request.QueryString("EVAL")
                    condDate = condDate & "  SUBSTR(DATA_ORA,1,8) <= " & sValore & " "
                End If
                sWhere = sWhere & " and  " & condDate & " "
            End If
            If par.IfEmpty(Request.QueryString("RIFDAL"), "Null") <> "Null" Then
                sValore = Request.QueryString("RIFDAL")
                sWhere = sWhere & " AND RIFERIMENTO_DA >= " & sValore & " "
            End If
            If par.IfEmpty(Request.QueryString("RIFAL"), "Null") <> "Null" Then
                sValore = Request.QueryString("RIFAL")
                sWhere = sWhere & " AND RIFERIMENTO_A <= " & sValore & " "
            End If
            If par.IfEmpty(Request.QueryString("TIPINC"), "-1") <> "-1" Then
                sValore = Request.QueryString("TIPINC")
                swhereIdTipoPag = swhereIdTipoPag & " AND siscom_mi.INCASSI_EXTRAMAV.ID_TIPO_PAG = " & sValore & " "
            End If
            If par.IfEmpty(Request.QueryString("DPAG"), "Null") <> "Null" Then
                sValore = Request.QueryString("DPAG")
                sWhere = sWhere & " AND siscom_mi.incassi_extramav.data_pagamento >= " & sValore & " "
            End If
            If par.IfEmpty(Request.QueryString("DPAGAL"), "Null") <> "Null" Then
                sValore = Request.QueryString("DPAGAL")
                sWhere = sWhere & " AND siscom_mi.incassi_extramav.data_pagamento <= " & sValore & " "
            End If


            Dim queryMarconi As String = ""
            If Session.Item("MOD_PAG_COMUNE") = "1" And (par.IfEmpty(Request.QueryString("TIPINC"), "-1") = "-1" Or par.IfEmpty(Request.QueryString("TIPINC"), "-1") = "8") Then
                queryMarconi = "SELECT  DISTINCT INCASSI_EXTRAMAV.ID AS ID, " _
                                     & "siscom_mi.GETDATAORA (INCASSI_EXTRAMAV.DATA_ORA) AS DATA_ORA, " _
                                     & "OPERATORI.OPERATORE, " _
                                     & "('<a href=""javascript:window.open(''../Contratto.aspx?LT=1&ID='||siscom_mi.RAPPORTI_UTENZA.ID||'&COD='||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||''',''CONTRATTO'',''height=780,top=0,left=0,width=1160'');void(0);"">'||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||'</a>') AS COD_CONTRATTO, " _
                                     & "('<a href=""javascript:window.open(''../../CONTABILITA/DatiUtenza.aspx?IDANA='|| siscom_mi.soggetti_contrattuali.id_anagrafica|| '&IDCONT='|| siscom_mi.rapporti_utenza.ID|| ''',''EstrattoConto'');void(0);"">'|| (CASE WHEN anagrafica.ragione_sociale IS NULL THEN anagrafica.cognome || ' ' || anagrafica.nome ELSE anagrafica.ragione_sociale END)|| '</a>') AS intestatario, " _
                                     & "TO_CHAR " _
                                     & "(TO_DATE (INCASSI_EXTRAMAV.DATA_PAGAMENTO, 'YYYYMMDD'), " _
                                     & "'DD/MM/YYYY') AS DATA_PAGAMENTO, " _
                                     & "TIPO_PAG_PARZ.DESCRIZIONE AS TIPO, " _
                                     & "INCASSI_EXTRAMAV.MOTIVO_PAGAMENTO AS DESCRIZIONE, " _
                                     & "(TO_CHAR " _
                                     & "(TO_DATE " _
                                     & "(INCASSI_EXTRAMAV.RIFERIMENTO_DA, " _
                                     & "'YYYYMMDD'), " _
                                     & "'DD/MM/YYYY') " _
                                     & "|| ' - ' " _
                                     & "|| TO_CHAR " _
                                     & "(TO_DATE " _
                                     & "(INCASSI_EXTRAMAV.RIFERIMENTO_A, " _
                                     & "'YYYYMMDD'), " _
                                     & "'DD/MM/YYYY') " _
                                     & ") AS RIFERIMENTO_DA_A, " _
                                     & "TRIM (TO_CHAR(importo_incassato,'9G999G999G990D99')) AS importo_incassato,TRIM (TO_CHAR(importo_eccedenza,'9G999G999G990D99')) AS importo_eccedenza," _
                                     & "TRIM (TO_CHAR (IMPORTO, '9G999G999G990D99')) AS TOTALE " _
                                     & "FROM SISCOM_MI.INCASSI_EXTRAMAV,SISCOM_MI.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI, " _
                                     & "OPERATORI, " _
                                     & "SISCOM_MI.RAPPORTI_UTENZA, " _
                                     & "SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI, " _
                                     & "SISCOM_MI.TIPO_PAG_PARZ " _
                                     & "WHERE OPERATORI.ID = INCASSI_EXTRAMAV.ID_OPERATORE " _
                                     & "AND RAPPORTI_UTENZA.ID = INCASSI_EXTRAMAV.ID_CONTRATTO " _
                                     & "AND INCASSI_EXTRAMAV.ID= BOL_BOLLETTE_VOCI_PAGAMENTI.ID_INCASSO_EXTRAMAV and INCASSI_EXTRAMAV.fl_Annullata=0 " _
                                     & "AND TIPO_PAG_PARZ.ID=INCASSI_EXTRAMAV.ID_TIPO_PAG " _
                                     & "AND siscom_mi.SOGGETTI_CONTRATTUALI.ID_CONTRATTO = siscom_mi.RAPPORTI_UTENZA.ID " _
                                     & "AND siscom_mi.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                     & "AND siscom_mi.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                     & " and INCASSI_EXTRAMAV.id_tipo_pag=8 " _
                                 & sWhere _
                                 & " UNION "
            End If


            par.cmd.CommandText = queryMarconi & "SELECT  DISTINCT INCASSI_EXTRAMAV.ID AS ID, " _
                                 & "siscom_mi.GETDATAORA (INCASSI_EXTRAMAV.DATA_ORA) AS DATA_ORA, " _
                                 & "OPERATORI.OPERATORE, " _
                                 & "('<a href=""javascript:window.open(''../Contratto.aspx?LT=1&ID='||siscom_mi.RAPPORTI_UTENZA.ID||'&COD='||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||''',''CONTRATTO'',''height=780,top=0,left=0,width=1160'');void(0);"">'||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||'</a>') AS COD_CONTRATTO, " _
                                 & "('<a href=""javascript:window.open(''../../CONTABILITA/DatiUtenza.aspx?IDANA='|| siscom_mi.soggetti_contrattuali.id_anagrafica|| '&IDCONT='|| siscom_mi.rapporti_utenza.ID|| ''',''EstrattoConto'');void(0);"">'|| (CASE WHEN anagrafica.ragione_sociale IS NULL THEN anagrafica.cognome || ' ' || anagrafica.nome ELSE anagrafica.ragione_sociale END)|| '</a>') AS intestatario, " _
                                 & "TO_CHAR " _
                                 & "(TO_DATE (INCASSI_EXTRAMAV.DATA_PAGAMENTO, 'YYYYMMDD'), " _
                                 & "'DD/MM/YYYY') AS DATA_PAGAMENTO, " _
                                 & "TIPO_PAG_PARZ.DESCRIZIONE AS TIPO, " _
                                 & "INCASSI_EXTRAMAV.MOTIVO_PAGAMENTO AS DESCRIZIONE, " _
                                 & "(TO_CHAR " _
                                 & "(TO_DATE " _
                                 & "(INCASSI_EXTRAMAV.RIFERIMENTO_DA, " _
                                 & "'YYYYMMDD'), " _
                                 & "'DD/MM/YYYY') " _
                                 & "|| ' - ' " _
                                 & "|| TO_CHAR " _
                                 & "(TO_DATE " _
                                 & "(INCASSI_EXTRAMAV.RIFERIMENTO_A, " _
                                 & "'YYYYMMDD'), " _
                                 & "'DD/MM/YYYY') " _
                                 & ") AS RIFERIMENTO_DA_A, " _
                                 & "TRIM (TO_CHAR(importo_incassato,'9G999G999G990D99')) AS importo_incassato,TRIM (TO_CHAR(importo_eccedenza,'9G999G999G990D99')) AS importo_eccedenza," _
                                 & "TRIM (TO_CHAR (IMPORTO, '9G999G999G990D99')) AS TOTALE " _
                                 & "FROM SISCOM_MI.INCASSI_EXTRAMAV,SISCOM_MI.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI, " _
                                 & "OPERATORI, " _
                                 & "SISCOM_MI.RAPPORTI_UTENZA, " _
                                 & "SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI, " _
                                 & "SISCOM_MI.TIPO_PAG_PARZ " _
                                 & "WHERE OPERATORI.ID = INCASSI_EXTRAMAV.ID_OPERATORE " _
                                 & "AND RAPPORTI_UTENZA.ID = INCASSI_EXTRAMAV.ID_CONTRATTO " _
                                 & "AND INCASSI_EXTRAMAV.ID= BOL_BOLLETTE_VOCI_PAGAMENTI.ID_INCASSO_EXTRAMAV and INCASSI_EXTRAMAV.fl_Annullata=0 " _
                                 & "AND TIPO_PAG_PARZ.ID=INCASSI_EXTRAMAV.ID_TIPO_PAG " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.ID_CONTRATTO = siscom_mi.RAPPORTI_UTENZA.ID " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                 & swhereOperatori & " " & swhereIdTipoPag & " " & sWhere _
                                 & "ORDER BY ID ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)
            da.Dispose()
            Dim row As Data.DataRow
            For Each row In dt.Rows
                TotIncass = TotIncass + CDbl(par.IfNull(row.Item("importo_incassato"), 0))
                TotEcced = TotEcced + CDbl(par.IfNull(row.Item("importo_eccedenza"), 0))
                TotTotale = TotTotale + CDbl(par.IfNull(row.Item("totale"), 0))
            Next
            row = dt.NewRow()
            row.Item("DESCRIZIONE") = "T O T A L E"
            row.Item("importo_incassato") = Format(TotIncass, "##,##0.00")
            row.Item("importo_eccedenza") = Format(TotEcced, "##,##0.00")
            row.Item("totale") = Format(TotTotale, "##,##0.00")
            dt.Rows.Add(row)
            Session.Add("dtExport", dt)
            BindGrid()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Report Pagamenti Extra Mav GENERALE:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            End If
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            Me.dgvRptPagExtraMav.DataSource = CType(Session.Item("dtExport"), Data.DataTable)
            Me.dgvRptPagExtraMav.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Report Pagamenti Extra Mav GENERALE:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub dgvRptPagExtraMav_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvRptPagExtraMav.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            dgvRptPagExtraMav.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            If dgvRptPagExtraMav.Items.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim dtExport As Data.DataTable = CType(Session.Item("dtExport"), Data.DataTable)
                For Each rowExport As Data.DataRow In dtExport.Rows
                    rowExport.Item("COD_CONTRATTO") = par.EliminaLink(rowExport.Item("COD_CONTRATTO").ToString)
                    rowExport.Item("intestatario") = par.EliminaLink(rowExport.Item("intestatario").ToString)
                Next
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
