Imports System.Data.OleDb
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Contratti_Pagamenti_RptPgExtraMavCSV
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim sStringaSql As String
    Dim sWhere As String
    Dim dtCopia As Data.DataTable
    Dim TotTotale As Decimal = 0
    Dim TotSpese As Decimal = 0
    Dim TotOneri As Decimal = 0
    Dim TotCanone As Decimal = 0
    Dim TotSindac As Decimal = 0
    Dim TotDepCauz As Decimal = 0
    Dim TotAltro As Decimal = 0
    Public percentuale As Long = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        'ISTRUZIONE PER LASCIARE LA SCHERMATA IN PRIMO PIANO
        Response.Write("<script>self.focus();</script>")
        Dim Str As String = ""
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "..\barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
        Str = Str & "<br /><input id='perc' type='text' style ='font-family: Arial, Helvetica, sans-serif;font-size: 7pt;font-weight: 700; width: 30px;color: #0000FF;text-align: center;' readonly='readonly'></div><br/><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';document.getElementById('perc').value = tempo + '%';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"
        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()
            Try
                FindIncassi()
                'CREAZIONE FILE CSV
                Dim nomefile As String = par.DataTableALCSV(dtCopia, "RptPgExtraMav", ";")
                If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                    Response.Write("<script language=javascript>document.getElementById('dvvvPre').style.visibility = 'hidden';")
                    Response.Write("</script>")
                    Response.Write("<a href='#' onclick=" & Chr(34) & "window.open('../../FileTemp/" & nomefile & "','Expt', '')" & Chr(34) & ">Clicca QUI per scaricare il file.</a>")
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante la creazione del file CSV. Riprovare!')</script>")
                End If


            Catch ex As Exception
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Report Pagamenti Extra Mav:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            End Try
        End If
    End Sub

    Private Sub FindIncassi()
        Try

            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim NUMERORIGHE As Long = 0
            par.cmd.CommandText = "SELECT  DISTINCT INCASSI_EXTRAMAV.ID AS ID, OPERATORI.OPERATORE, " _
                                 & "TO_CHAR(TO_CHAR(GETDATAORA (INCASSI_EXTRAMAV.DATA_ORA))) AS DATA_ORA, " _
                                 & "TO_CHAR(TO_CHAR(TO_DATE (INCASSI_EXTRAMAV.DATA_PAGAMENTO, 'YYYYMMDD'), " _
                                 & "'DD/MM/YYYY')) AS DATA_PAGAMENTO, " _
                                 & "TIPO_PAG_PARZ.DESCRIZIONE AS TIPO_PAG, " _
                                 & "INCASSI_EXTRAMAV.MOTIVO_PAGAMENTO AS MOTIVAZIONE, " _
                                 & "TO_CHAR " _
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
                                 & "rapporti_utenza.cod_contratto AS COD_CONTRATTO, " _
                                 & "(CASE WHEN ANAGRAFICA.RAGIONE_SOCIALE IS NULL THEN ANAGRAFICA.COGNOME ||' '|| ANAGRAFICA.NOME ELSE ANAGRAFICA.RAGIONE_SOCIALE END) AS INTESTATARIO," _
                                 & "'' as bolletta, " _
                                 & "'' as riferimento, " _
                                 & "'' AS spese_generali," _
                                 & "'' AS oneri_accessori," _
                                 & "'' AS canone_ind_occupaz, " _
                                 & "'' AS sind_inquilini, " _
                                 & "'' AS dep_cauz, " _
                                 & "'' AS altro, " _
                                 & "TRIM (TO_CHAR (IMPORTO, '9G999G999G990D99')) AS TOTALE " _
                                 & "FROM SISCOM_MI.INCASSI_EXTRAMAV,SISCOM_MI.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI, " _
                                 & "OPERATORI, " _
                                 & "SISCOM_MI.RAPPORTI_UTENZA, " _
                                 & "SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI, " _
                                 & "SISCOM_MI.TIPO_PAG_PARZ " _
                                 & "WHERE OPERATORI.ID = INCASSI_EXTRAMAV.ID_OPERATORE " _
                                 & "AND RAPPORTI_UTENZA.ID = INCASSI_EXTRAMAV.ID_CONTRATTO " _
                                 & "AND INCASSI_EXTRAMAV.ID= BOL_BOLLETTE_VOCI_PAGAMENTI.ID_INCASSO_EXTRAMAV " _
                                 & "AND TIPO_PAG_PARZ.ID=INCASSI_EXTRAMAV.ID_TIPO_PAG " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.ID_CONTRATTO = siscom_mi.RAPPORTI_UTENZA.ID " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                 & "AND siscom_mi.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                 & "ORDER BY ID ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtIncasso As New Data.DataTable()
            da.Fill(dtIncasso)
            da.Dispose()
            '+++++++++++++clone tabella principale++++++++++++
            dtCopia = dtIncasso.Clone
            dtCopia.TableName = "Export"
            Dim Contatore As Long = 0
            NUMERORIGHE = dtIncasso.Rows.Count
            For Each row As Data.DataRow In dtIncasso.Rows
                dtCopia.Rows.Add(row.ItemArray)
                Cerca2(row.Item("id"))
                Contatore = Contatore + 1
                percentuale = (Contatore * 100) / NUMERORIGHE
                Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                Response.Flush()
            Next
            Dim rTot As Data.DataRow
            rTot = dtCopia.NewRow()
            rTot.Item("MOTIVAZIONE") = "T O T A L E"
            rTot.Item("SPESE_GENERALI") = Format(TotSpese, "##,##0.00")
            rTot.Item("ONERI_ACCESSORI") = Format(TotOneri, "##,##0.00")
            rTot.Item("CANONE_IND_OCCUPAZ") = Format(TotCanone, "##,##0.00")
            rTot.Item("SIND_INQUILINI") = Format(TotSindac, "##,##0.00")
            rTot.Item("DEP_CAUZ") = Format(TotDepCauz, "##,##0.00")
            rTot.Item("ALTRO") = Format(TotAltro, "##,##0.00")
            rTot.Item("totale") = Format(TotTotale, "##,##0.00")
            dtCopia.Rows.Add(rTot)
            dtCopia.Columns.Remove("ID")
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Report Pagamenti Extra Mav:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try


    End Sub
    Private Sub Cerca2(ByVal idIncasso As String)
        Try

            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim RTotTotale As Decimal = 0
            Dim RTotSpese As Decimal = 0
            Dim RTotOneri As Decimal = 0
            Dim RTotCanone As Decimal = 0
            Dim RTotSindac As Decimal = 0
            Dim RTotDepCauz As Decimal = 0
            Dim RTotAltro As Decimal = 0
            Dim varname1 As String = ""
            varname1 = "SELECT    bol_bollette_voci_pagamenti.id_voce_bolletta,bol_bollette.ID AS id_bolletta, " _
                      & "BOL_BOLLETTE.NUM_BOLLETTA," _
                      & "('Dal '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' al '||TO_CHAR(TO_DATE(BOL_BOLLETTE.riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS ""RIFERIMENTO"", " _
                      & "         bol_bollette_voci_pagamenti.importo_pagato AS IMPORTO, " _
                      & "         (SELECT TRIM " _
                      & "                    (TO_CHAR " _
                      & "                         (NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato, " _
                      & "                                         0) " _
                      & "                                   ), " _
                      & "                               0 " _
                      & "                              ), " _
                      & "                          '9G999G999G990D99' " _
                      & "                         ) " _
                      & "                    ) " _
                      & "            FROM siscom_mi.t_voci_bolletta " _
                      & "           WHERE id_incasso_extramav = " & idIncasso _
                      & "             AND bol_bollette_voci.ID = " _
                      & "                                   siscom_mi.bol_bollette_voci_pagamenti.id_voce_bolletta " _
                      & "             AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                      & "             AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                      & "             AND t_voci_bolletta.gruppo = 2) AS spese_generali, " _
                      & "         (SELECT TRIM " _
                      & "                    (TO_CHAR " _
                      & "                         (NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato, " _
                      & "                                         0) " _
                      & "                                   ), " _
                      & "                               0 " _
                      & "                              ), " _
                      & "                          '9G999G999G990D99' " _
                      & "                         ) " _
                      & "                    ) " _
                      & "            FROM siscom_mi.t_voci_bolletta " _
                      & "           WHERE id_incasso_extramav = " & idIncasso _
                      & "             AND bol_bollette_voci.ID = " _
                      & "                                   siscom_mi.bol_bollette_voci_pagamenti.id_voce_bolletta " _
                      & "             AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                      & "             AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                      & "             AND t_voci_bolletta.gruppo = 3) AS oneri_accessori, " _
                      & "         (SELECT TRIM " _
                      & "                    (TO_CHAR " _
                      & "                         (NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato, " _
                      & "                                         0) " _
                      & "                                   ), " _
                      & "                               0 " _
                      & "                              ), " _
                      & "                          '9G999G999G990D99' " _
                      & "                         ) " _
                      & "                    ) " _
                      & "            FROM siscom_mi.t_voci_bolletta " _
                      & "           WHERE id_incasso_extramav = " & idIncasso _
                      & "             AND bol_bollette_voci.ID = " _
                      & "                                   bol_bollette_voci_pagamenti.id_voce_bolletta " _
                      & "             AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                      & "             AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                      & "             AND t_voci_bolletta.gruppo = 4) AS canone_ind_occupaz, " _
                      & "         (SELECT TRIM " _
                      & "                    (TO_CHAR " _
                      & "                         (NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato, " _
                      & "                                         0) " _
                      & "                                   ), " _
                      & "                               0 " _
                      & "                              ), " _
                      & "                          '9G999G999G990D99' " _
                      & "                         ) " _
                      & "                    ) " _
                      & "            FROM siscom_mi.t_voci_bolletta " _
                      & "           WHERE id_incasso_extramav = " & idIncasso _
                      & "             AND bol_bollette_voci.ID = " _
                      & "                                   bol_bollette_voci_pagamenti.id_voce_bolletta " _
                      & "             AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                      & "             AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                      & "             AND t_voci_bolletta.gruppo = 5) AS sind_inquilini, " _
                      & "         (SELECT TRIM " _
                      & "                    (TO_CHAR " _
                      & "                         (NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato, " _
                      & "                                         0) " _
                      & "                                   ), " _
                      & "                               0 " _
                      & "                              ), " _
                      & "                          '9G999G999G990D99' " _
                      & "                         ) " _
                      & "                    ) " _
                      & "            FROM siscom_mi.t_voci_bolletta " _
                      & "           WHERE id_incasso_extramav = " & idIncasso _
                      & "             AND bol_bollette_voci.ID = " _
                      & "                                   bol_bollette_voci_pagamenti.id_voce_bolletta " _
                      & "             AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                      & "             AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                      & "             AND t_voci_bolletta.gruppo = 6) AS dep_cauz, " _
                      & "         (SELECT TRIM " _
                      & "                    (TO_CHAR " _
                      & "                         (NVL (SUM (NVL (bol_bollette_voci_pagamenti.importo_pagato, " _
                      & "                                         0) " _
                      & "                                   ), " _
                      & "                               0 " _
                      & "                              ), " _
                      & "                          '9G999G999G990D99' " _
                      & "                         ) " _
                      & "                    ) " _
                      & "            FROM siscom_mi.t_voci_bolletta " _
                      & "           WHERE id_incasso_extramav = " & idIncasso _
                      & "             AND bol_bollette_voci.ID = " _
                      & "                                   bol_bollette_voci_pagamenti.id_voce_bolletta " _
                      & "             AND t_voci_bolletta.ID = bol_bollette_voci.id_voce " _
                      & "             AND bol_bollette_voci.id_bolletta = bol_bollette.ID " _
                      & "             AND t_voci_bolletta.gruppo NOT IN (2, 3, 4, 5, 6)) AS altro " _
                      & "    FROM siscom_mi.bol_bollette_voci_pagamenti, " _
                      & "         siscom_mi.bol_bollette_voci, " _
                      & "         siscom_mi.bol_bollette, " _
                      & "         siscom_mi.soggetti_contrattuali " _
                      & "   WHERE id_incasso_extramav = " & idIncasso _
                      & "     AND bol_bollette_voci.ID = siscom_mi.bol_bollette_voci_pagamenti.id_voce_bolletta " _
                      & "     AND bol_bollette.ID = bol_bollette_voci.id_bolletta " _
                      & "     AND soggetti_contrattuali.id_contratto = bol_bollette.id_contratto " _
                      & "     AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
                      & "ORDER BY id_bolletta ASC"
            par.cmd.CommandText = varname1


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dtEventIcasso As New Data.DataTable()

            'da.Fill(dtEventIcasso)
            'da.Dispose()
            Dim dtlettore As Data.DataTable

            Dim addRow As Data.DataRow

            Dim i As Integer = 0
            Dim ii As Integer = 0
            Dim k As Integer = 0
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtlettore = New Data.DataTable
            da.Fill(dtlettore)
            'lettore = par.cmd.ExecuteReader
            ii = dtCopia.Rows.Count - 1
            RTotSpese = 0
            RTotOneri = 0
            RTotCanone = 0
            RTotSindac = 0
            RTotDepCauz = 0
            RTotAltro = 0
            RTotTotale = 0
            For Each lettore As Data.DataRow In dtlettore.Rows

                addRow = dtCopia.NewRow
                addRow.Item("BOLLETTA") = "Num. " & par.IfNull(lettore.Item("NUM_BOLLETTA"), "")
                addRow.Item("RIFERIMENTO") = par.IfNull(lettore.Item("RIFERIMENTO"), "")

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
            TotTotale = TotTotale + RTotTotale

            'Next

        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Report Pagamenti Extra Mav:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub

End Class