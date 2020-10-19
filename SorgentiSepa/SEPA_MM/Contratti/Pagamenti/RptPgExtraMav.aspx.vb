Imports System.Data.OleDb
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_Pagamenti_RptPgExtraMav
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
                If Session.Item("MOD_AMM_RPT_P_EXTRA") = 0 Then
                    sWhere = sWhere & " AND OPERATORI.ID = " & Session.Item("ID_OPERATORE") & " "
                Else
                    If Session.Item("ID_CAF") = 2 Then
                        swhereOperatori = swhereOperatori & " AND OPERATORI.ID_CAF = 2 "
                    End If
                End If
            End If
            If par.IfEmpty(Request.QueryString("DPAG"), "Null") <> "Null" Then
                sValore = Request.QueryString("DPAG")
                sWhere = sWhere & " AND incassi_extramav.data_pagamento >= " & sValore & " "
                titoloCond = titoloCond & " dal " & par.FormattaData(Request.QueryString("DPAG"))
            End If
            If par.IfEmpty(Request.QueryString("DPAGAL"), "Null") <> "Null" Then
                sValore = Request.QueryString("DPAGAL")
                sWhere = sWhere & " AND incassi_extramav.data_pagamento <= " & sValore & " "
                titoloCond = titoloCond & " al " & par.FormattaData(Request.QueryString("DPAGAL"))
            End If
            If par.IfEmpty(Request.QueryString("TIPINC"), "-1") <> "-1" Then
                sValore = Request.QueryString("TIPINC")
                swhereIdTipoPag = swhereIdTipoPag & " AND INCASSI_EXTRAMAV.ID_TIPO_PAG = " & sValore & " "
            Else
                sWhere = sWhere & " AND INCASSI_EXTRAMAV.ID_TIPO_PAG <>13 "
            End If
            'tabella di generali più campi di dettaglio'

            'par.cmd.CommandText = "SELECT incassi_extramav.ID, " _
            '                   & "(SELECT TO_CHAR(TO_DATE (MIN(data_ora), 'yyyyMMddHH24MISS'),'dd/MM/yyyy hh24:mi.ss') FROM eventi_pagamenti_parziali WHERE id_incasso_extramav = incassi_extramav.ID) AS data_ora, " _
            '                   & "tipo_pag_parz.descrizione AS tipo_pag,incassi_extramav.motivo_pagamento as motivazione, " _
            '                   & "TO_CHAR(TO_DATE (incassi_extramav.data_pagamento, 'yyyyMMdd'),'dd/mm/yyyy' ) AS data_pagamento, " _
            '                   & "(TO_CHAR(TO_DATE (riferimento_da, 'yyyyMMdd'),'dd/mm/yyyy') ||' - '|| TO_CHAR(TO_DATE (riferimento_a, 'yyyyMMdd'), 'dd/mm/yyyy' )) AS riferimento_da_a, " _
            '                   & "'' as cod_contratto, " _
            '                   & "'' as intestatario, " _
            '                   & "'' as bolletta, " _
            '                   & "'' as riferimento, " _
            '                   & "'' AS spese_generali," _
            '                   & "'' AS oneri_accessori," _
            '                   & "'' AS canone_ind_occupaz, " _
            '                   & "'' AS sind_inquilini, " _
            '                   & "'' AS dep_cauz, " _
            '                   & "'' AS altro, " _
            '                   & "TRIM (TO_CHAR(incassi_extramav.importo,'9G999G999G990D99')) as totale " _
            '                   & "FROM incassi_extramav ,OPERATORI,CAF_WEB, " _
            '                   & "tipo_pag_parz " _
            '                   & "WHERE " _
            '                   & "OPERATORI.ID = incassi_extramav.id_operatore AND " _
            '                   & "CAF_WEB.ID = OPERATORI.id_caf " _
            '                   & "AND incassi_extramav.id_tipo_pag = tipo_pag_parz.ID(+) " _
            '                   & "AND incassi_extramav.FL_ANNULLATA = 0 " _
            '                   & sWhere _
            '                   & "ORDER BY ID ASC "

            Dim queryMarconi As String = ""

            If Session.Item("MOD_PAG_COMUNE") = "1" And (par.IfEmpty(Request.QueryString("TIPINC"), "-1") = "-1" Or par.IfEmpty(Request.QueryString("TIPINC"), "-1") = "8") Then
                queryMarconi = "SELECT  DISTINCT INCASSI_EXTRAMAV.ID AS ID, " _
                                 & "GETDATAORA (INCASSI_EXTRAMAV.DATA_ORA) AS DATA_ORA, " _
                                 & "TIPO_PAG_PARZ.DESCRIZIONE AS TIPO, " _
                                 & "INCASSI_EXTRAMAV.MOTIVO_PAGAMENTO AS DESCRIZIONE, " _
                                 & "TO_CHAR " _
                                 & "(TO_DATE (INCASSI_EXTRAMAV.DATA_PAGAMENTO, 'YYYYMMDD'), " _
                                 & "'DD/MM/YYYY') AS DATA_PAGAMENTO, " _
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
                                 & ") AS RIFERIMENTO_DA_A," _
                                 & "('<a href=""javascript:window.open(''../Contratto.aspx?LT=1&ID='||siscom_mi.RAPPORTI_UTENZA.ID||'&COD='||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||''',''CONTRATTO'',''height=780,top=0,left=0,width=1160'');void(0);"">'||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||'</a>') AS COD_CONTRATTO, " _
                                 & "('<a href=""javascript:window.open(''../../CONTABILITA/DatiUtenza.aspx?IDANA='|| siscom_mi.soggetti_contrattuali.id_anagrafica|| '&IDCONT='|| siscom_mi.rapporti_utenza.ID|| ''',''EstrattoConto'');void(0);"">'|| (CASE WHEN anagrafica.ragione_sociale IS NULL THEN anagrafica.cognome || ' ' || anagrafica.nome ELSE anagrafica.ragione_sociale END)|| '</a>') AS intestatario, " _
                                  & "'' as BOLLETTA, '' AS RIFERIMENTO,  '' AS SPESE_GENERALI, '' AS ONERI_ACCESSORI, '' AS CANONE_IND_OCCUPAZ, '' AS SIND_INQUILINI,'' AS DEP_CAUZ, '' AS ALTRO,  " _
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


            par.cmd.CommandText = queryMarconi & " SELECT  DISTINCT INCASSI_EXTRAMAV.ID AS ID, " _
                                 & "GETDATAORA (INCASSI_EXTRAMAV.DATA_ORA) AS DATA_ORA, " _
                                 & "TIPO_PAG_PARZ.DESCRIZIONE AS TIPO, " _
                                 & "INCASSI_EXTRAMAV.MOTIVO_PAGAMENTO AS DESCRIZIONE, " _
                                 & "TO_CHAR " _
                                 & "(TO_DATE (INCASSI_EXTRAMAV.DATA_PAGAMENTO, 'YYYYMMDD'), " _
                                 & "'DD/MM/YYYY') AS DATA_PAGAMENTO, " _
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
                                 & ") AS RIFERIMENTO_DA_A," _
                                 & "('<a href=""javascript:window.open(''../Contratto.aspx?LT=1&ID='||siscom_mi.RAPPORTI_UTENZA.ID||'&COD='||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||''',''CONTRATTO'',''height=780,top=0,left=0,width=1160'');void(0);"">'||siscom_mi.RAPPORTI_UTENZA.COD_CONTRATTO||'</a>') AS COD_CONTRATTO, " _
                                 & "('<a href=""javascript:window.open(''../../CONTABILITA/DatiUtenza.aspx?IDANA='|| siscom_mi.soggetti_contrattuali.id_anagrafica|| '&IDCONT='|| siscom_mi.rapporti_utenza.ID|| ''',''EstrattoConto'');void(0);"">'|| (CASE WHEN anagrafica.ragione_sociale IS NULL THEN anagrafica.cognome || ' ' || anagrafica.nome ELSE anagrafica.ragione_sociale END)|| '</a>') AS intestatario, " _
                                  & "'' as BOLLETTA, '' AS RIFERIMENTO,  '' AS SPESE_GENERALI, '' AS ONERI_ACCESSORI, '' AS CANONE_IND_OCCUPAZ, '' AS SIND_INQUILINI,'' AS DEP_CAUZ, '' AS ALTRO,  " _
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
            rTot.Item("SPESE_GENERALI") = Format(TotSpese, "##,##0.00")
            rTot.Item("ONERI_ACCESSORI") = Format(TotOneri, "##,##0.00")
            rTot.Item("CANONE_IND_OCCUPAZ") = Format(TotCanone, "##,##0.00")
            rTot.Item("SIND_INQUILINI") = Format(TotSindac, "##,##0.00")
            rTot.Item("DEP_CAUZ") = Format(TotDepCauz, "##,##0.00")
            rTot.Item("ALTRO") = Format(TotAltro, "##,##0.00")
            rTot.Item("importo_incassato") = Format(TotIncass, "##,##0.00")
            rTot.Item("importo_eccedenza") = Format(TotEcced, "##,##0.00")
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
            Me.lblTitolo.Text = "Report Pagamenti Manuali <br/> " & titoloCond & "<br/> OPERATORE: " & NameOp
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
        Try
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
            'prende eventi_pagamenti_parziali.id...questo dato è da trovare nelle nuove tabelle'
            sWhere = "and id_incasso_extramav = " & idIncasso
            Dim varname1 As String = ""
            varname1 = "SELECT DISTINCT incassi_extramav.ID, " _
                     & "getdataora (incassi_extramav.data_ora) AS data_ora," _
                     & "'" & tipo_incasso & "' as tipo_pag," _
                     & " incassi_extramav.motivo_pagamento AS descrizione, " _
                     & "'' AS data_pagamento, " _
                     & "'-' AS riferimento_da_a, " _
                     & "('<a href=""javascript:window.open(''../Contratto.aspx?LT=1&ID='||RAPPORTI_UTENZA.ID||'&COD='||RAPPORTI_UTENZA.COD_CONTRATTO||''',''CONTRATTO'',''height=780,top=0,left=0,width=1160'');void(0);"">'||RAPPORTI_UTENZA.COD_CONTRATTO||'</a>') AS COD_CONTRATTO, " _
                     & "('<a href=""javascript:window.open(''../../CONTABILITA/DatiUtenza.aspx?IDANA='|| soggetti_contrattuali.id_anagrafica|| '&IDCONT='|| rapporti_utenza.ID|| ''',''EstrattoConto'');void(0);"">'|| (CASE WHEN anagrafica.ragione_sociale IS NULL THEN anagrafica.cognome || ' ' || anagrafica.nome ELSE anagrafica.ragione_sociale END)|| '</a>') AS intestatario," _
                     & " '' AS bolletta, '' AS riferimento, '' AS spese_generali, " _
                     & " '' AS oneri_accessori, '' AS canone_ind_occupaz, " _
                     & " '' AS sind_inquilini, '' AS dep_cauz, '' AS altro," _
                        & "TRIM (TO_CHAR(importo_incassato,'9G999G999G990D99')) AS importo_incassato,TRIM (TO_CHAR(importo_eccedenza,'9G999G999G990D99')) AS importo_eccedenza," _
                     & "TRIM (TO_CHAR (importo, '9G999G999G990D99')) AS totale " _
                     & "FROM siscom_mi.incassi_extramav, " _
                     & "siscom_mi.anagrafica, " _
                     & "siscom_mi.soggetti_contrattuali, " _
                     & "siscom_mi.rapporti_utenza, " _
                     & "siscom_mi.bol_bollette_voci_pagamenti, " _
                     & "siscom_mi.tipo_pag_parz " _
                     & "WHERE rapporti_utenza.ID = incassi_extramav.id_contratto " _
                     & "AND incassi_extramav.ID = " _
                     & "bol_bollette_voci_pagamenti.id_incasso_extramav " _
                     & "AND tipo_pag_parz.ID = incassi_extramav.id_tipo_pag " _
                     & "AND siscom_mi.soggetti_contrattuali.id_contratto = " _
                     & "siscom_mi.rapporti_utenza.ID " _
                     & "AND siscom_mi.soggetti_contrattuali.id_anagrafica = anagrafica.ID " _
                     & "AND siscom_mi.soggetti_contrattuali.cod_tipologia_occupante = " _
                     & "'INTE' " _
                     & "AND incassi_extramav.ID =" & idIncasso _
                     & "ORDER BY data_ora ASC"
            par.cmd.CommandText = varname1
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtEventIcasso As New Data.DataTable()
            da.Fill(dtEventIcasso)
            da.Dispose()
            Dim dtlettore As Data.DataTable
            Dim addRow As Data.DataRow
            Dim i As Integer = 0
            Dim ii As Integer = 0
            Dim k As Integer = 0
            For Each riga As Data.DataRow In dtEventIcasso.Rows
                'dtCopia.Rows.Add(riga.ItemArray)
                varname1 = ""
                varname1 = "SELECT    bol_bollette_voci_pagamenti.id_voce_bolletta,bol_bollette.ID AS id_bolletta, " _
                         & "('<a href=""javascript:window.open(''../../Contabilita/DettaglioBolletta.aspx?IDCONT='||BOL_BOLLETTE.ID_CONTRATTO||'&IDBOLL='||BOL_BOLLETTE.ID||'&IDANA='||SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA||''',''Dettagli'','''');void(0)"">'||BOL_BOLLETTE.NUM_BOLLETTA||'</a>') AS NUM_BOLLETTA," _
                         & "('Dal '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' al '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS ""RIFERIMENTO"", " _
                         & "bol_bollette_voci_pagamenti.importo_pagato AS IMPORTO, " _
                         & "(SELECT TRIM " _
                         & "(TO_CHAR(NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato, " _
                         & "0)), 0 ),'9G999G999G990D99')) " _
                         & "FROM siscom_mi.t_voci_bolletta " _
                         & "WHERE id_incasso_extramav = " & idIncasso _
                         & "AND bol_bollette_voci.ID = " _
                         & "siscom_mi.bol_bollette_voci_pagamenti.id_voce_bolletta " _
                         & "AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                         & "AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                         & "AND t_voci_bolletta.gruppo = 2) AS spese_generali, " _
                         & "(SELECT TRIM " _
                         & "(TO_CHAR(NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato,0)),0),'9G999G999G990D99')) " _
                         & "FROM siscom_mi.t_voci_bolletta " _
                         & "WHERE id_incasso_extramav = " & idIncasso _
                         & "AND bol_bollette_voci.ID = " _
                         & "siscom_mi.bol_bollette_voci_pagamenti.id_voce_bolletta " _
                         & "AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                         & "AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                         & "AND t_voci_bolletta.gruppo = 3) AS oneri_accessori, " _
                         & "(SELECT TRIM(TO_CHAR(NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato,0)),0),'9G999G999G990D99')) " _
                         & "FROM siscom_mi.t_voci_bolletta " _
                         & "WHERE id_incasso_extramav = " & idIncasso _
                         & "AND bol_bollette_voci.ID = " _
                         & "bol_bollette_voci_pagamenti.id_voce_bolletta " _
                         & "AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                         & "AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                         & "AND t_voci_bolletta.gruppo = 4) AS canone_ind_occupaz, " _
                         & "(SELECT TRIM (TO_CHAR(NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato,0)),0), '9G999G999G990D99')) " _
                         & "FROM siscom_mi.t_voci_bolletta " _
                         & "WHERE id_incasso_extramav = " & idIncasso _
                         & "AND bol_bollette_voci.ID = " _
                         & "bol_bollette_voci_pagamenti.id_voce_bolletta " _
                         & "AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                         & "AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                         & "AND t_voci_bolletta.gruppo = 5) AS sind_inquilini, " _
                         & "(SELECT TRIM(TO_CHAR(NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato,0)),0),'9G999G999G990D99')) " _
                         & "FROM siscom_mi.t_voci_bolletta " _
                         & "WHERE id_incasso_extramav = " & idIncasso _
                         & "AND bol_bollette_voci.ID = " _
                         & "bol_bollette_voci_pagamenti.id_voce_bolletta " _
                         & "AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                         & "AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                         & "AND t_voci_bolletta.gruppo = 6) AS dep_cauz, " _
                         & "(SELECT TRIM (TO_CHAR (NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato,0)),0),'9G999G999G990D99')) " _
                         & "FROM siscom_mi.t_voci_bolletta " _
                         & "WHERE id_incasso_extramav = " & idIncasso _
                         & "AND bol_bollette_voci.ID = " _
                         & "bol_bollette_voci_pagamenti.id_voce_bolletta " _
                         & "AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                         & "AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                         & "AND t_voci_bolletta.gruppo NOT IN (2, 3, 4, 5, 6)) AS altro " _
                         & "FROM siscom_mi.bol_bollette_voci_pagamenti, " _
                         & "siscom_mi.bol_bollette_voci, " _
                         & "siscom_mi.bol_bollette, " _
                         & "siscom_mi.soggetti_contrattuali " _
                         & "WHERE id_incasso_extramav = " & idIncasso _
                         & "AND bol_bollette_voci.ID = siscom_mi.bol_bollette_voci_pagamenti.id_voce_bolletta " _
                         & "AND bol_bollette.ID = bol_bollette_voci.id_bolletta " _
                         & "AND soggetti_contrattuali.id_contratto = bol_bollette.id_contratto " _
                         & "AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
                         & "ORDER BY id_bolletta ASC"
                par.cmd.CommandText = varname1
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
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
                    addRow.Item("BOLLETTA") = "Num. " & par.IfNull(lettore.Item("NUM_BOLLETTA"), "")
                    addRow.Item("RIFERIMENTO") = par.IfNull(lettore.Item("RIFERIMENTO"), "")
                    'addRow.Item("IMPORTO_INCASSATO") = Math.Round(CDec(par.IfNull(lettore.Item("IMPORTO_INCASSATO"), 0)), 2)
                    'RTotIncass += Math.Round(CDec(par.IfNull(lettore.Item("IMPORTO_INCASSATO"), "0")), 2)
                    'addRow.Item("IMPORTO_ECCEDENZA") = Math.Round(CDec(par.IfNull(lettore.Item("IMPORTO_ECCEDENZA"), 0)), 2)
                    'RTotEcced += Math.Round(CDec(par.IfNull(lettore.Item("IMPORTO_ECCEDENZA"), "0")), 2)
                    addRow.Item("TOTALE") = Math.Round(CDec(par.IfNull(lettore.Item("IMPORTO"), 0)), 2)
                    RTotTotale += Math.Round(CDec(par.IfNull(lettore.Item("IMPORTO"), "0")), 2)
                    addRow.Item("SPESE_GENERALI") = Math.Round(CDec(par.IfNull(lettore.Item("SPESE_GENERALI"), 0)), 2)
                    RTotSpese += Math.Round(CDec(par.IfNull(lettore.Item("SPESE_GENERALI"), 0)), 2)
                    addRow.Item("ONERI_ACCESSORI") = Math.Round(CDec(par.IfNull(lettore.Item("ONERI_ACCESSORI"), 0)), 2)
                    RTotOneri += Math.Round(CDec(par.IfNull(lettore.Item("ONERI_ACCESSORI"), 0)), 2)
                    addRow.Item("CANONE_IND_OCCUPAZ") = Math.Round(CDec(par.IfNull(lettore.Item("CANONE_IND_OCCUPAZ"), 0)), 2)
                    RTotCanone += Math.Round(CDec(par.IfNull(lettore.Item("CANONE_IND_OCCUPAZ"), 0)), 2)
                    addRow.Item("SIND_INQUILINI") = Math.Round(CDec(par.IfNull(lettore.Item("SIND_INQUILINI"), 0)), 2)
                    RTotSindac += Math.Round(CDec(par.IfNull(lettore.Item("SIND_INQUILINI"), 0)), 2)
                    addRow.Item("DEP_CAUZ") = Math.Round(CDec(par.IfNull(lettore.Item("DEP_CAUZ"), 0)), 2)
                    RTotDepCauz += Math.Round(CDec(par.IfNull(lettore.Item("DEP_CAUZ"), 0)), 2)
                    addRow.Item("ALTRO") = Math.Round(CDec(par.IfNull(lettore.Item("ALTRO"), "0")), 2)
                    RTotAltro += Math.Round(CDec(par.IfNull(lettore.Item("ALTRO"), 0)), 2)
                    dtCopia.Rows.Add(addRow)
                Next
                TotSpese = TotSpese + RTotSpese
                TotOneri = TotOneri + RTotOneri
                TotCanone = TotCanone + RTotCanone
                TotSindac = TotSindac + RTotSindac
                TotDepCauz = TotDepCauz + RTotDepCauz
                TotAltro = TotAltro + RTotAltro
                TotIncass = TotIncass + RTotIncass
                TotEcced = TotEcced + RTotEcced
                TotTotale = TotTotale + RTotTotale
            Next
        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Report Pagamenti Extra Mav:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
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
