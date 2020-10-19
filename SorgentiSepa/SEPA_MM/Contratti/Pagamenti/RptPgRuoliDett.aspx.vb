Imports System.IO

Partial Class Contratti_Pagamenti_RptPgRuoliDett
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
            Me.lblTitolo.Text = "Report bollette a RUOLO"
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
            Dim dt As New Data.DataTable
            Dim swhereOperatori As String = ""
            Dim swhereIdTipoPag As String = ""

            Dim CodContr As String = ""
            Dim sCompara As String = ""
            If Not IsNothing(Request.QueryString("CodContratto")) Then
                CodContr = Request.QueryString("CodContratto")
            End If
            Dim condizioneCodContr As String = ""
            If CodContr <> "" Then
                sValore = CodContr
                If InStr(CodContr, "*") Then
                    sCompara = " LIKE "
                    par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                condizioneCodContr = " AND RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            If par.IfEmpty(Request.QueryString("S"), "-1") = "1" Then
                sValore = Request.QueryString("S")
                sWhere = sWhere & "   AND EXISTS " _
                & " (SELECT BOL_BOLLETTE.ID FROM BOL_BOLLETTE_PAGAMENTI_RUOLO" _
                & "    WHERE(BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_BOLLETTA = BOL_BOLLETTE.ID)" _
                & " AND EXISTS (SELECT INCASSI_RUOLI.ID FROM INCASSI_RUOLI WHERE FL_SGRAVIO=1 AND INCASSI_RUOLI.ID=BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_INCASSO_RUOLO)" _
                & "  )"
            End If
            If par.IfEmpty(Request.QueryString("RIFDAL"), "Null") <> "Null" Then
                sValore = Request.QueryString("RIFDAL")
                sWhere = sWhere & " AND RIFERIMENTO_DA >= '" & sValore & "' "
            End If
            If par.IfEmpty(Request.QueryString("RIFAL"), "Null") <> "Null" Then
                sValore = Request.QueryString("RIFAL")
                sWhere = sWhere & " AND RIFERIMENTO_A <= '" & sValore & "' "
            End If

            Dim dPag As String = ""
            Dim dPagAl As String = ""
            Dim stringDate As String = ""
            If par.IfEmpty(Request.QueryString("S"), "-1") <> "1" Then
                If par.IfEmpty(Request.QueryString("DPAG"), "Null") <> "Null" Then
                    dPag = Request.QueryString("DPAG")
                    'sWhere = sWhere & " AND siscom_mi.INCASSI_RUOLI.data_pagamento >= '" & sValore & "' "
                End If
                If par.IfEmpty(Request.QueryString("DPAGAL"), "Null") <> "Null" Then
                    dPagAl = Request.QueryString("DPAGAL")
                    'sWhere = sWhere & " AND siscom_mi.INCASSI_RUOLI.data_pagamento <= '" & sValore & "' "
                End If
                If dPag <> "" Then
                    stringDate = "siscom_mi.INCASSI_RUOLI.data_pagamento >= '" & dPag & "' and "
                End If
                If dPagAl <> "" Then
                    stringDate &= "siscom_mi.INCASSI_RUOLI.data_pagamento <= '" & dPagAl & "' and "
                End If
                If dPag <> "" Or dPagAl <> "" Then
                    sWhere = sWhere & "   AND EXISTS " _
                   & " (SELECT BOL_BOLLETTE.ID FROM siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO" _
                   & "    WHERE(BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_BOLLETTA = BOL_BOLLETTE.ID)" _
                   & " AND EXISTS (SELECT INCASSI_RUOLI.ID FROM siscom_mi.INCASSI_RUOLI WHERE " & stringDate & " INCASSI_RUOLI.ID=BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_INCASSO_RUOLO)" _
                   & "  )"
                End If
            End If

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID, " _
                                 & "('<a href=""javascript:window.open(''../Contratto.aspx?LT=1&ID='||siscom_mi.RAPPORTI_UTENZA.ID||'&COD='||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||''',''CONTRATTO'',''height=780,top=0,left=0,width=1160'');void(0);"">'||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||'</a>') AS COD_CONTRATTO, " _
                                 & "('<a href=""javascript:window.open(''../../CONTABILITA/DatiUtenza.aspx?IDANA='|| siscom_mi.soggetti_contrattuali.id_anagrafica|| '&IDCONT='|| siscom_mi.rapporti_utenza.ID|| ''',''EstrattoConto'');void(0);"">'|| (CASE WHEN anagrafica.ragione_sociale IS NULL THEN anagrafica.cognome || ' ' || anagrafica.nome ELSE anagrafica.ragione_sociale END)|| '</a>') AS intestatario, " _
                                 & "NUM_RUOLO, " _
                                 & "('<a href=""javascript:window.open(''../../Contabilita/DettaglioBolletta.aspx?IDCONT='||BOL_BOLLETTE.ID_CONTRATTO||'&IDBOLL='||BOL_BOLLETTE.ID||'&IDANA='||SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA||''',''Dettagli'','''');void(0)"">'||BOL_BOLLETTE.NUM_BOLLETTA||'</a>') AS NUM_BOLLETTA," _
                 & "('Dal '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' al '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS ""RIFERIMENTO"", " _
                 & "    (SELECT max(getdata(INCASSI_RUOLI.data_pagamento))" _
             & " FROM siscom_mi.INCASSI_RUOLI, siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO " _
             & " WHERE INCASSI_RUOLI.ID =" _
                   & "    BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_INCASSO_RUOLO " _
                         & "    AND BOL_BOLLETTE_PAGAMENTI_RUOLO.id_bolletta =" _
                                  & "  bol_bollette.id) as data_pagamento," _
                & "   (SELECT MAX(descrizione)" _
          & "  FROM siscom_mi.TIPO_PAG_RUOLO, siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO" _
          & "  WHERE ID = BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_TIPO_PAGAMENTO " _
         & "      AND BOL_BOLLETTE_PAGAMENTI_RUOLO.id_bolletta = bol_bollette.id)" _
         & " AS TIPO," _
                                 & "TRIM (TO_CHAR(nvl(importo_RUOLO,0),'9G999G999G990D99')) AS IMPORTO_RUOLO,TRIM (TO_CHAR(nvl(IMP_RUOLO_PAGATO,0),'9G999G999G990D99')) AS IMP_RUOLO_PAGATO," _
                                 & "TRIM (TO_CHAR ((nvl(importo_RUOLO,0)-nvl(IMP_RUOLO_PAGATO,0)), '9G999G999G990D99')) AS RESIDUO, " _
                                  & " (CASE " _
                & "         WHEN NVL (" _
                & "                 (SELECT MAX(FL_SGRAVIO)" _
                & "                    FROM siscom_mi.INCASSI_RUOLI, siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO" _
                & "                   WHERE     INCASSI_RUOLI.ID =" _
                & "                                BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_INCASSO_RUOLO" _
                & "                         AND BOL_BOLLETTE_PAGAMENTI_RUOLO.id_bolletta =" _
                & "                                bol_bollette.id)," _
                & "                 0) = 0" _
                & "         THEN" _
                & "            'NO'" _
                & "         ELSE" _
                & "            'SI'" _
                & "      END)" _
                & "        AS sgravio " _
                                 & "FROM SISCOM_MI.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI, " _
                                 & "SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.BOL_BOLLETTE" _
                                 & " " _
                                 & "WHERE " _
     & "    NVL (BOL_BOLLETTE.IMPORTO_RUOLO, 0) > 0" _
     & "    AND bol_bollette.id_contratto = rapporti_utenza.id " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.ID_CONTRATTO = siscom_mi.RAPPORTI_UTENZA.ID " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                 & swhereOperatori & " " & swhereIdTipoPag & " " & condizioneCodContr & " " & sWhere _
                                 & " "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)
            da.Dispose()
            Dim row As Data.DataRow
            For Each row In dt.Rows
                TotIncass = TotIncass + CDbl(par.IfNull(row.Item("importo_RUOLO"), 0))
                TotEcced = TotEcced + CDbl(par.IfNull(row.Item("IMP_RUOLO_PAGATO"), 0))
                TotTotale = TotTotale + CDbl(par.IfNull(row.Item("RESIDUO"), 0))
            Next
            row = dt.NewRow()
            row.Item("TIPO") = "T O T A L E"
            row.Item("importo_RUOLO") = Format(TotIncass, "##,##0.00")
            row.Item("IMP_RUOLO_PAGATO") = Format(TotEcced, "##,##0.00")
            row.Item("RESIDUO") = Format(TotTotale, "##,##0.00")
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
                Session.Add("ERRORE", "Report Ruoli DETTAGLI:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            End If
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            Me.dgvRptPagExtraMav.DataSource = CType(Session.Item("dtExport"), Data.DataTable)
            Me.dgvRptPagExtraMav.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Report Ruoli DETTAGLI:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            If dgvRptPagExtraMav.Items.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim dtExport As Data.DataTable = CType(Session.Item("dtExport"), Data.DataTable)
                For Each rowExport As Data.DataRow In dtExport.Rows
                    rowExport.Item("COD_CONTRATTO") = par.EliminaLink(rowExport.Item("COD_CONTRATTO").ToString)
                    rowExport.Item("intestatario") = par.EliminaLink(rowExport.Item("intestatario").ToString)
                    rowExport.Item("num_bolletta") = par.EliminaLink(rowExport.Item("num_bolletta").ToString)
                Next
                Dim nomeFile As String = xls.EsportaExcelDaDataGridWithDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportPgRuoli", "ExportPgRuoli", dgvRptPagExtraMav, dtExport)
                If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                    Response.Redirect("../../FileTemp/" & nomeFile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun risultato da esportare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Report Ruoli GENERALE:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub dgvRptPagExtraMav_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvRptPagExtraMav.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            dgvRptPagExtraMav.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
End Class
