Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RptInquilini
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Public Property TipoCond() As String
        Get
            If Not (ViewState("par_TipoCond") Is Nothing) Then
                Return CStr(ViewState("par_TipoCond"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_TipoCond") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Cerca()
        End If
    End Sub


    Private Sub Cerca()

        Dim TotProp As Double = 0
        Dim TotComProp As Double = 0
        Dim TotGest As Double = 0
        Dim TotRiscald As Double = 0
        Dim TotPresAss As Double = 0
        '*******************APERURA CONNESSIONE*********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim sStringaSQL As String = ""
        par.cmd.CommandText = "SELECT TIPOLOGIA,DENOMINAZIONE,NOME as CITTA FROM SISCOM_MI.CONDOMINI, COMUNI_NAZIONI WHERE CONDOMINI.ID = " & Request.QueryString("IDCONDOMINIO") & " AND COMUNI_NAZIONI.COD = CONDOMINI.COD_COMUNE"
        Dim Reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If Reader.Read Then
            Me.lblTitle.Text = "CONDOMINIO : " & Reader("DENOMINAZIONE") & " - " & Reader("CITTA")
            TipoCond = Reader("TIPOLOGIA")
        End If
        Reader.Close()

        Select Case Request.QueryString("CHIAMA")
            Case "INQ"
                'MODIFICA DEL 07/07/2015 SEGNALAZIONE 938/2015
                'sStringaSQL = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO , NVL(ID_CONTRATTO,'') AS ID_CONTRATTO,RAPPORTI_UTENZA.DATA_DECORRENZA, UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ," _
                '            & " TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,TO_CHAR(MIL_PRO,'9G999G990D9999') AS MIL_PRO,TO_CHAR(MIL_ASC,'9G999G990D9999')AS MIL_ASC," _
                '            & " TO_CHAR(MIL_COMPRO,'9G999G990D9999')AS MIL_COMPRO,TO_CHAR(MIL_GEST,'9G999G990D9999')AS MIL_GEST,TO_CHAR(MIL_RISC,'9G999G990D9999') AS MIL_RISC," _
                '            & " (CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  " _
                '            & " SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select,/*(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE" _
                '            & " (CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)<>'IN CORSO'" _
                '            & " THEN (CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)= 'IN CORSO ABUSIVO' THEN " _
                '            & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ('O.A. - '||ragione_sociale)  ELSE (RTRIM(LTRIM('O.A. - '||COGNOME ||' ' ||NOME))) END) " _
                '            & " WHEN (SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) = 'IN CORSO ABUSIVO (S.T.)'" _
                '            & " OR SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) = 'IN CORSO (S.T.)' )" _
                '            & " THEN (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ('S.T. - '||ragione_sociale)  " _
                '            & " ELSE (RTRIM(LTRIM('S.T. - '||COGNOME ||' ' ||NOME))) END)" _
                '            & " ELSE SISCOM_MI.TIPO_DISPONIBILITA.DESCRIZIONE END) ELSE" _
                '            & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END)END)END)AS INTESTATARIO,*/" _
                '            & " COND_UI.POSIZIONE_BILANCIO,SCALE_EDIFICI.DESCRIZIONE,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,NVL(INDIRIZZI.CIVICO,'') AS CIVICO_COR,UNITA_IMMOBILIARI.INTERNO, " _
                '            & " (CASE WHEN cod_tipo_disponibilita <> 'LIBE' THEN (INDIRIZZI.DESCRIZIONE|| ', '|| RAPPORTI_UTENZA.civico_cor)ELSE '' END) AS indirizzo, rapporti_utenza.luogo_cor AS CITTA, rapporti_utenza.cap_cor as CAP,(CASE WHEN cod_tipo_disponibilita <> 'LIBE' THEN siscom_mi.Getintestatari (RAPPORTI_UTENZA.ID) ELSE 'Libero' END) AS intestatario " _
                '            & " FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.ANAGRAFICA" _
                '            & ",SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPO_DISPONIBILITA" _
                '            & " WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID" _
                '            & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND'" _
                '            & " AND (cond_ui.id_condominio=" & Request.QueryString("IDCONDOMINIO") & " ) AND ANAGRAFICA.ID(+)=COND_UI.ID_INTESTARIO AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA" _
                '            & " AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO AND" _
                '            & " UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD ORDER BY POSIZIONE_BILANCIO ASC,  ID_UI ASC,DATA_DECORRENZA DESC"

                sStringaSQL = "SELECT   rapporti_utenza.cod_contratto, NVL (id_contratto, " _
                            & "                                             '') AS id_contratto, " _
                            & "         rapporti_utenza.data_decorrenza, unita_immobiliari.ID AS id_ui, " _
                            & "         unita_immobiliari.cod_unita_immobiliare, " _
                            & "         tipologia_unita_immobiliari.descrizione AS tipologia, " _
                            & "         TO_CHAR (mil_pro, '9G999G990D9999') AS mil_pro, " _
                            & "         TO_CHAR (mil_asc, '9G999G990D9999') AS mil_asc, " _
                            & "         TO_CHAR (mil_compro, '9G999G990D9999') AS mil_compro, " _
                            & "         TO_CHAR (mil_gest, '9G999G990D9999') AS mil_gest, " _
                            & "         TO_CHAR (mil_risc, '9G999G990D9999') AS mil_risc, " _
                            & "         TO_CHAR (mill_pres_ass, '9G999G990D9999') AS mill_pres_ass, " _
                            & "         cond_ui.note AS note, " _
                            & "         (CASE " _
                            & "             WHEN unita_contrattuale.id_contratto IS NULL " _
                            & "                THEN 'LIBERO' " _
                            & "             ELSE siscom_mi.getstatocontratto2(unita_contrattuale.id_contratto,0) " _
                            & "          END " _
                            & "         ) AS stato, " _
                            & "         (CASE " _
                            & "             WHEN unita_contrattuale.id_contratto IS NULL " _
                            & "                THEN '' " _
                            & "             ELSE siscom_mi.getstatocontratto2(unita_contrattuale.id_contratto,0) " _
                            & "          END " _
                            & "         ) AS stato_dt_select, /*(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE (CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)<>'IN CORSO' THEN (CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)= 'IN CORSO ABUSIVO' THEN  (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ('O.A. - '||ragione_sociale)  ELSE (RTRIM(LTRIM('O.A. - '||COGNOME ||' ' ||NOME))) END)  WHEN (SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) = 'IN CORSO ABUSIVO (S.T.)' OR SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) = 'IN CORSO (S.T.)' ) THEN (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ('S.T. - '||ragione_sociale)   ELSE (RTRIM(LTRIM('S.T. - '||COGNOME ||' ' ||NOME))) END) ELSE SISCOM_MI.TIPO_DISPONIBILITA.DESCRIZIONE END) ELSE (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END)END)END)AS INTESTATARIO,*/ " _
                            & "         cond_ui.posizione_bilancio, scale_edifici.descrizione, " _
                            & "         tipo_livello_piano.descrizione AS piano, " _
                            & "         NVL (indirizzi.civico, '') AS civico_cor, unita_immobiliari.interno, " _
                            & "         (CASE " _
                            & "             WHEN cod_tipo_disponibilita <> 'LIBE' " _
                            & "                THEN (   indirizzi.descrizione " _
                            & "                      || ', ' " _
                            & "                      || rapporti_utenza.civico_cor " _
                            & "                     ) " _
                            & "             ELSE '' " _
                            & "          END " _
                            & "         ) AS indirizzo, " _
                            & "         rapporti_utenza.luogo_cor AS citta, rapporti_utenza.cap_cor AS cap, " _
                            & "         (CASE " _
                            & "             WHEN cod_tipo_disponibilita <> 'LIBE' " _
                            & "                THEN siscom_mi.getintestatari (rapporti_utenza.ID) " _
                            & "             ELSE 'Libero' " _
                            & "          END " _
                            & "         ) AS intestatario, " _
                            & "         (CASE " _
                            & "             WHEN cod_tipo_disponibilita <> 'LIBE' " _
                            & "                THEN (SELECT NVL(COD_FISCALE,PARTITA_IVA) FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = UNITA_CONTRATTUALE.ID_CONTRATTO AND COD_TIPOLOGIA_OCCUPANTE = 'INTE') " _
                            & "             ELSE 'Libero' " _
                            & "          END " _
                            & "         ) AS CF_IVA, " _
                            & "         identificativi_catastali.foglio, " _
                            & "         identificativi_catastali.numero AS mappale, " _
                            & "         identificativi_catastali.sub, " _
                            & "         (SELECT COUNT (*) " _
                            & "            FROM siscom_mi.soggetti_contrattuali " _
                            & "           WHERE id_contratto = rapporti_utenza.ID) AS soggetti, " _
                            & "         (SELECT COUNT (*) " _
                            & "            FROM siscom_mi.ospiti " _
                            & "           WHERE id_contratto = rapporti_utenza.ID) AS ospiti " _
                            & "    FROM siscom_mi.unita_immobiliari, " _
                            & "         siscom_mi.unita_contrattuale, " _
                            & "         siscom_mi.cond_ui, " _
                            & "         siscom_mi.anagrafica, " _
                            & "         siscom_mi.scale_edifici, " _
                            & "         siscom_mi.tipo_livello_piano, " _
                            & "         siscom_mi.rapporti_utenza, " _
                            & "         siscom_mi.tipo_disponibilita, " _
                            & "         siscom_mi.indirizzi, " _
                            & "         siscom_mi.tipologia_unita_immobiliari, " _
                            & "         siscom_mi.edifici, " _
                            & "         siscom_mi.identificativi_catastali " _
                            & "   WHERE  cond_ui.id_ui = unita_immobiliari.ID " _
                            & "   		  AND cond_ui.id_condominio = " & Request.QueryString("IDCONDOMINIO") & " " _
                            & "     	  AND unita_immobiliari.ID = unita_contrattuale.id_unita(+)  " _
                            & "     AND anagrafica.ID(+) = cond_ui.id_intestario " _
                            & "     AND scale_edifici.ID(+) = unita_immobiliari.id_scala " _
                            & "     AND unita_immobiliari.cod_tipo_livello_piano = tipo_livello_piano.cod(+) " _
                            & "     AND rapporti_utenza.ID(+) = unita_contrattuale.id_contratto " _
                            & "     AND unita_immobiliari.cod_tipo_disponibilita = tipo_disponibilita.cod(+) " _
                            & "     AND unita_immobiliari.id_indirizzo = indirizzi.ID(+) " _
                            & "     AND unita_immobiliari.cod_tipologia = tipologia_unita_immobiliari.cod " _
                            & "     AND cod_tipo_disponibilita <> 'VEND' " _
                            & "     AND unita_immobiliari.id_edificio = edifici.ID " _
                            & "     AND identificativi_catastali.ID(+) = unita_immobiliari.id_catastale " _
                            & "	 AND unita_contrattuale.id_contratto(+) = siscom_mi.GETULTIMORU(unita_immobiliari.ID) " _
                            & " UNION " _
                            & "SELECT   rapporti_utenza.cod_contratto, NVL (UNITA_CONTRATTUALE.id_contratto, " _
                            & "                                             '') AS id_contratto, " _
                            & "         rapporti_utenza.data_decorrenza, unita_immobiliari.ID AS id_ui, " _
                            & "         unita_immobiliari.cod_unita_immobiliare, " _
                            & "         tipologia_unita_immobiliari.descrizione AS tipologia, " _
                            & "         '' AS mil_pro, " _
                            & "         '' AS mil_asc, " _
                            & "         '' AS mil_compro, " _
                            & "         '' AS mil_gest, " _
                            & "         '' AS mil_risc, " _
                            & "         '' AS mill_pres_ass, " _
                            & "         '' AS note, " _
                            & "         (CASE " _
                            & "             WHEN unita_contrattuale.id_contratto IS NULL " _
                            & "                THEN 'LIBERO' " _
                            & "             ELSE siscom_mi.getstatocontratto2 " _
                            & "                                             (unita_contrattuale.id_contratto, " _
                            & "                                              0 " _
                            & "                                             ) " _
                            & "          END " _
                            & "         ) AS stato, " _
                            & "         (CASE " _
                            & "             WHEN unita_contrattuale.id_contratto IS NULL " _
                            & "                THEN '' " _
                            & "             ELSE siscom_mi.getstatocontratto2 " _
                            & "                                             (unita_contrattuale.id_contratto, " _
                            & "                                              0 " _
                            & "                                             ) " _
                            & "          END " _
                            & "         ) AS stato_dt_select, /*(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE (CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)<>'IN CORSO' THEN (CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)= 'IN CORSO ABUSIVO' THEN  (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ('O.A. - '||ragione_sociale)  ELSE (RTRIM(LTRIM('O.A. - '||COGNOME ||' ' ||NOME))) END)  WHEN (SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) = 'IN CORSO ABUSIVO (S.T.)' OR SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) = 'IN CORSO (S.T.)' ) THEN (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ('S.T. - '||ragione_sociale)   ELSE (RTRIM(LTRIM('S.T. - '||COGNOME ||' ' ||NOME))) END) ELSE SISCOM_MI.TIPO_DISPONIBILITA.DESCRIZIONE END) ELSE (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END)END)END)AS INTESTATARIO,*/ " _
                            & "         '' AS posizione_bilancio, scale_edifici.descrizione, " _
                            & "         tipo_livello_piano.descrizione AS piano, " _
                            & "         NVL (indirizzi.civico, '') AS civico_cor, unita_immobiliari.interno, " _
                            & "         (CASE " _
                            & "             WHEN cod_tipo_disponibilita <> 'LIBE' " _
                            & "                THEN (   indirizzi.descrizione " _
                            & "                      || ', ' " _
                            & "                      || rapporti_utenza.civico_cor " _
                            & "                     )" _
                            & "             ELSE '' " _
                            & "          END " _
                            & "         ) AS indirizzo, " _
                            & "         rapporti_utenza.luogo_cor AS citta, rapporti_utenza.cap_cor AS cap, " _
                            & "         (CASE " _
                            & "             WHEN cod_tipo_disponibilita <> 'LIBE' " _
                            & "                THEN siscom_mi.getintestatari (rapporti_utenza.ID) " _
                            & "             ELSE 'Libero' " _
                            & "          END " _
                            & "         ) AS intestatario, " _
                            & "         (CASE " _
                            & "             WHEN cod_tipo_disponibilita <> 'LIBE' " _
                            & "                THEN (SELECT NVL(COD_FISCALE,PARTITA_IVA) FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = UNITA_CONTRATTUALE.ID_CONTRATTO AND COD_TIPOLOGIA_OCCUPANTE = 'INTE') " _
                            & "             ELSE 'Libero' " _
                            & "          END " _
                            & "         ) AS CF_IVA, " _
                            & "         identificativi_catastali.foglio, " _
                            & "         identificativi_catastali.numero AS mappale, " _
                            & "         identificativi_catastali.sub, " _
                            & "         (SELECT COUNT (*) " _
                            & "            FROM siscom_mi.soggetti_contrattuali " _
                            & "           WHERE id_contratto = rapporti_utenza.ID) AS soggetti, " _
                            & "         (SELECT COUNT (*) " _
                            & "            FROM siscom_mi.ospiti" _
                            & "           WHERE id_contratto = rapporti_utenza.ID) AS ospiti " _
                            & " FROM siscom_mi.unita_immobiliari, " _
                            & " siscom_mi.unita_contrattuale, " _
                            & " siscom_mi.soggetti_contrattuali, " _
                            & " siscom_mi.anagrafica, " _
                            & " siscom_mi.scale_edifici, " _
                            & "         siscom_mi.tipo_livello_piano, " _
                            & "		  siscom_mi.rapporti_utenza, " _
                            & "         siscom_mi.tipo_disponibilita, " _
                            & "		 siscom_mi.indirizzi, " _
                            & "		  siscom_mi.tipologia_unita_immobiliari, " _
                            & "		  siscom_mi.edifici, " _
                            & "		  siscom_mi.identificativi_catastali " _
                            & "WHERE " _
                            & "  unita_immobiliari.id_edificio IN (SELECT id_edificio " _
                            & "                                             FROM siscom_mi.cond_edifici " _
                            & "                                            WHERE id_condominio = " & Request.QueryString("IDCONDOMINIO") & ") " _
                            & "AND unita_immobiliari.ID = unita_contrattuale.id_unita(+) " _
                            & "AND soggetti_contrattuali.id_contratto(+) = unita_contrattuale.id_unita " _
                            & "AND soggetti_contrattuali.cod_tipologia_occupante(+) = 'INTE' " _
                            & "AND soggetti_contrattuali.id_contratto(+) = unita_contrattuale.id_contratto " _
                            & "		  AND soggetti_contrattuali.cod_tipologia_occupante(+) = 'INTE' " _
                            & "     AND anagrafica.ID(+) = soggetti_contrattuali.id_anagrafica " _
                            & "	 AND scale_edifici.ID(+) = unita_immobiliari.id_scala " _
                            & "     AND unita_immobiliari.cod_tipo_livello_piano = tipo_livello_piano.cod(+) " _
                            & "	 AND rapporti_utenza.ID(+) = unita_contrattuale.id_contratto " _
                            & "     AND unita_immobiliari.cod_tipo_disponibilita = tipo_disponibilita.cod(+) " _
                            & "	      AND unita_immobiliari.id_indirizzo = indirizzi.ID(+) " _
                            & "		  AND unita_immobiliari.cod_tipologia(+) = tipologia_unita_immobiliari.cod " _
                            & "     AND cod_tipo_disponibilita <> 'VEND' " _
                            & "AND unita_immobiliari.id_edificio = edifici.ID " _
                            & " AND identificativi_catastali.ID(+) = unita_immobiliari.id_catastale " _
                            & " AND unita_contrattuale.id_contratto(+) = siscom_mi.GETULTIMORU(unita_immobiliari.ID) " _
                            & "AND unita_immobiliari.ID NOT IN (SELECT ID_UI FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO") & ") " _
                            & "ORDER BY posizione_bilancio ASC "



            Case "PREV"

                sStringaSQL = "SELECT NVL(ID_CONTRATTO,'') AS ID_CONTRATTO, UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ," _
                & " TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,TO_CHAR(MIL_PRO,'9G999G990D9999') AS MIL_PRO,TO_CHAR(MIL_ASC,'9G999G990D9999')AS MIL_ASC," _
                & " TO_CHAR(MIL_COMPRO,'9G999G990D9999')AS MIL_COMPRO,TO_CHAR(MIL_GEST,'9G999G990D9999')AS MIL_GEST,TO_CHAR(MIL_RISC,'9G999G990D9999') AS MIL_RISC," _
                & " (CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  " _
                & " SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select,(CASE WHEN siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO)IS NULL" _
                & " THEN(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END)" _
                & " WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)= 'IN CORSO ABUSIVO' THEN 'OCC. ABUSIVO' ELSE" _
                & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) END)  AS INTESTATARIO," _
                & " COND_UI_PREVENTIVI.POSIZIONE_BILANCIO,SCALE_EDIFICI.DESCRIZIONE,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,NVL(RAPPORTI_UTENZA.CIVICO_COR,'') AS CIVICO_COR," _
                & " UNITA_IMMOBILIARI.INTERNO, " _
                & " to_char (MILL_PRES_ASS,'9G999G990D9999') AS MILL_PRES_ASS," _
                & " COND_UI.NOTE AS NOTE " _
                & " FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI_PREVENTIVI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.ANAGRAFICA" _
                & ",SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.RAPPORTI_UTENZA" _
                & " WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID" _
                & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI_PREVENTIVI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND'" _
                & " AND (cond_ui_PREVENTIVI.ID_GESTIONE=" & Request.QueryString("IDGESTIONE") & " ) AND ANAGRAFICA.ID(+)=COND_UI_PREVENTIVI.ID_INTESTARIO AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA" _
                & " AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO ORDER BY POSIZIONE_BILANCIO ASC"


            Case "CONSU"

                sStringaSQL = "SELECT NVL(ID_CONTRATTO,'') AS ID_CONTRATTO, UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ," _
                & " TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,TO_CHAR(MIL_PRO,'9G999G990D9999') AS MIL_PRO,TO_CHAR(MIL_ASC,'9G999G990D9999')AS MIL_ASC," _
                & " TO_CHAR(MIL_COMPRO,'9G999G990D9999')AS MIL_COMPRO,TO_CHAR(MIL_GEST,'9G999G990D9999')AS MIL_GEST,TO_CHAR(MIL_RISC,'9G999G990D9999') AS MIL_RISC," _
                & " (CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  " _
                & " SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select,(CASE WHEN siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO)IS NULL" _
                & " THEN(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END)" _
                & " WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)= 'IN CORSO ABUSIVO' THEN 'OCC. ABUSIVO' ELSE" _
                & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) END)  AS INTESTATARIO," _
                & " COND_UI_CONSUNTIVI.POSIZIONE_BILANCIO,SCALE_EDIFICI.DESCRIZIONE,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,NVL(RAPPORTI_UTENZA.CIVICO_COR,'') AS CIVICO_COR," _
                & "UNITA_IMMOBILIARI.INTERNO, " _
                & " to_char (MILL_PRES_ASS,'9G999G990D9999') AS MILL_PRES_ASS," _
                & " COND_UI.NOTE AS NOTE " _
                & " FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI_CONSUNTIVI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.ANAGRAFICA" _
                & ",SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.RAPPORTI_UTENZA" _
                & " WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID" _
                & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI_CONSUNTIVI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND'" _
                & " AND (cond_ui_CONSUNTIVI.ID_GESTIONE=" & Request.QueryString("IDGESTIONE") & " ) AND ANAGRAFICA.ID(+)=COND_UI_CONSUNTIVI.ID_INTESTARIO AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA" _
                & " AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO ORDER BY POSIZIONE_BILANCIO ASC"

        End Select


        'sStringaSQL = sStringaSQL & " AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO") & ")"


        par.cmd.CommandText = sStringaSQL

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(dt)
        If Request.QueryString("CHIAMA") = "INQ" Then
            dt = FiltraContrattiVeri(dt)
        End If
        Dim row As Data.DataRow

        For Each row In dt.Rows
            TotProp = TotProp + CDbl(par.IfNull(row.Item("MIL_PRO"), 0))
            TotComProp = TotComProp + CDbl(par.IfNull(row.Item("MIL_COMPRO"), 0))
            TotGest = TotGest + CDbl(par.IfNull(row.Item("MIL_GEST"), 0))
            TotRiscald = TotRiscald + CDbl(par.IfNull(row.Item("MIL_RISC"), 0))
            TotPresAss = TotPresAss + CDbl(par.IfNull(row.Item("MILL_PRES_ASS"), 0))
        Next

        row = dt.NewRow()
        row.Item("POSIZIONE_BILANCIO") = "T O T A L E"
        row.Item("MIL_PRO") = Format(TotProp, "0.0000")
        row.Item("MIL_COMPRO") = Format(TotComProp, "0.0000")
        row.Item("MIL_GEST") = Format(TotGest, "0.0000")
        row.Item("MIL_RISC") = Format(TotRiscald, "0.0000")

        dt.Rows.Add(row)

        DataGridInquilini.DataSource = dt
        DataGridInquilini.DataBind()

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Session.Add("MIADT", dt)


        NascondiColonne()


    End Sub
    Private Function FiltraContrattiVeri(ByVal Table As Data.DataTable) As Data.DataTable
        FiltraContrattiVeri = Table.Clone()
        Dim idUi As Integer = 0
        Try
            Dim rSelect As Data.DataRow()

            For i As Integer = 0 To Table.Rows.Count - 1
                If par.IfNull(Table.Rows(i).Item("COD_CONTRATTO"), "") <> "" Then
                    If Table.Rows(i).Item("ID_UI") <> idUi Then
                        rSelect = Table.Select("ID_UI = " & Table.Rows(i).Item("ID_UI") & " AND STATO_DT_SELECT LIKE '%IN CORSO%'")
                        If rSelect.Length > 0 Then
                            FiltraContrattiVeri.Rows.Add(rSelect(0).ItemArray)
                            idUi = rSelect(0).Item("ID_UI")
                        Else
                            FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
                            idUi = Table.Rows(i).Item("ID_UI")
                        End If
                    End If
                Else
                    FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
                End If
            Next

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " FiltraContrattiVeri"
        End Try
        Return FiltraContrattiVeri
    End Function


    Private Sub NascondiColonne()

        '    If TipoCond = "C" Then

        '        '  Me.DataGridInquilini.Columns(12).Visible = False
        '        'Me.DataGridInquilini.Columns(16).Visible = False
        '        'Me.DataGridInquilini.Columns(14).Visible = False

        '    ElseIf TipoCond = "S" Then

        '        Me.DataGridInquilini.Columns(13).Visible = False
        '        Me.DataGridInquilini.Columns(14).Visible = False
        '        Me.DataGridInquilini.Columns(16).Visible = False
        '        '  Me.DataGridInquilini.Columns(14).Visible = False


        '    ElseIf TipoCond = "T" Then
        '        Me.DataGridInquilini.Columns(13).Visible = False
        '        Me.DataGridInquilini.Columns(14).Visible = False
        '        '  Me.DataGridInquilini.Columns(12).Visible = False
        '        Me.DataGridInquilini.Columns(16).Visible = False

        '    End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        ExportAsCondominio()
        'If TipoCond = "C" Then
        '    ExportAsCondominio()
        'ElseIf TipoCond = "S" Then

        '    ExportAsSuperCondominio()
        'ElseIf TipoCond = "T" Then

        '    ExportAsCentTermica()
        'End If
    End Sub

    Private Sub ExportAsCondominio()
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "CondExp_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "POSIZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CITTA'", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "CAP", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "CIVICO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "INTERNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "SCALA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "PIANO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "TIPO", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "NOMINATIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "INTESTATARIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "INTESTATARIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "MILL.PROP.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "MILL. ASC.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "MILL. COMP.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "MILL. RISC.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "MILL. GEST.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "MILL. PRES. ASS", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "NOTE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "COD.CONTRATTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "FOGLIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "MAPPALE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 22, "SUB", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 23, "N.COMPONENTI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 24, "N.OSPITI", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CITTA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CAP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO_COR"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTE_ATTUALE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CF_IVA"), "")))

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_PRO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_ASC"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_COMPRO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_RISC"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_GEST"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MILL_PRES_ASS"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOTE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FOGLIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MAPPALE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SUB"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SOGGETTI"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("OSPITI"), "")))
                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            File.Delete(strFile)
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub ExportAsSuperCondominio()
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "CondExp_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "POSIZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CITTA'", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "CAP", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "CIVICO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "INTERNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "SCALA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "PIANO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "TIPO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "NOMINATIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "MILL. COMP.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "MILL. RISC.", 12)
                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CITTA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CAP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO_COR"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_COMPRO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_RISC"), "")))
                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            File.Delete(strFile)
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub ExportAsCentTermica()
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "CondExp_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "POSIZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "CITTA'", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "CAP", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "CIVICO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "INTERNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "SCALA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "PIANO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "TIPO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "NOMINATIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "MILL. COMP.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "MILL/SUP RISC.", 12)
                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CITTA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CAP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO_COR"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_COMPRO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_RISC"), "")))

                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            File.Delete(strFile)
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnExport0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport0.Click
        Dim Html As String = ""
        Dim stringWriter As New System.IO.StringWriter
        Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try
            DataGridInquilini.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString


            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False

            Dim nomefile As String = "Exp_Inquilini_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile("<p>" & Me.lblTitle.Text & "</p>" & Html, url & nomefile)
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpMorosita','');</script>")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
