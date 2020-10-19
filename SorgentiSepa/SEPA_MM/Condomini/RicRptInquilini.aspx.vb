Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RicRptInquilini
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaAmministratori()
            CaricaCondomini()
        End If
    End Sub
    Private Sub CaricaAmministratori()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "select id, (cognome || ' ' || nome ) as amministratore from siscom_mi.cond_amministratori order by cognome asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.chkAmministratori.DataSource = dt
            Me.chkAmministratori.DataBind()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: CaricaAmministratori " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub CaricaCondomini()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "select id, denominazione from siscom_mi.condomini order by denominazione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.chkCondomini.DataSource = dt
            Me.chkCondomini.DataBind()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: CaricaCondomini " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnSelAmm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelAmm.Click
        If SelAmminist.Value = 0 Then
            For Each i As ListItem In chkAmministratori.Items
                i.Selected = True
            Next
            SelAmminist.Value = 1
        Else
            For Each i As ListItem In chkAmministratori.Items
                i.Selected = False
            Next
            SelAmminist.Value = 0
        End If
    End Sub
    Protected Sub btnSelCondomini_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelCondomini.Click
        If SelCondomini.Value = 0 Then
            For Each i As ListItem In chkCondomini.Items
                i.Selected = True
            Next
            SelCondomini.Value = 1
        Else
            For Each i As ListItem In chkCondomini.Items
                i.Selected = False
            Next
            SelCondomini.Value = 0
        End If
        CaricaCheckCondomini()
    End Sub
    Private Sub CaricaCheckCondomini()
        Try
            chkAmministratori.Items.Clear()
            Dim StringaCheckCondomini As String = ""
            For Each Items As ListItem In chkCondomini.Items
                If Items.Selected = True Then
                    StringaCheckCondomini = StringaCheckCondomini & Items.Value & ","
                End If
            Next
            If StringaCheckCondomini <> "" Then
                StringaCheckCondomini = Left(StringaCheckCondomini, Len(StringaCheckCondomini) - 1)
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select id, (cognome || ' ' || nome ) as amministratore " _
                                       & "from siscom_mi.cond_amministratori " _
                                       & "where siscom_mi.cond_amministratori.id in (select id_amministratore from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.id_condominio in (" & StringaCheckCondomini & ") and siscom_mi.cond_amministrazione.DATA_FINE is null) " _
                                       & "order by cognome asc"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                Me.chkAmministratori.DataSource = dt
                Me.chkAmministratori.DataBind()
                Dim StringaCheckAmministratori As String = ""
                For Each Items As ListItem In chkCondomini.Items
                    StringaCheckAmministratori = StringaCheckAmministratori & Items.Value & ","
                Next
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                CaricaAmministratori()
                CaricaCondomini()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: CaricaCheckCondomini " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub chkCondomini_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCondomini.SelectedIndexChanged
        CaricaCheckCondomini()
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.href='pagina_home.aspx';</script>")
    End Sub
    Private Sub ExportXLS()
        Try
            Dim CondSelezionati As String = ""
            Dim ammSelezionati As String = ""
            For Each i As ListItem In chkCondomini.Items
                If i.Selected = True Then
                    CondSelezionati += i.Value & ","
                End If
            Next
            For Each i As ListItem In chkAmministratori.Items
                If i.Selected = True Then
                    ammSelezionati += i.Value & ","
                End If
            Next
            If ammSelezionati = "" And CondSelezionati = "" Then
                Response.Write("<script>alert('Selezionare almeno un criterio per effettuare il report')</script>")
                Exit Sub
            End If
            If ammSelezionati <> "" Then
                ammSelezionati = ammSelezionati.Substring(0, ammSelezionati.LastIndexOf(","))
            End If
            If CondSelezionati <> "" Then
                CondSelezionati = CondSelezionati.Substring(0, CondSelezionati.LastIndexOf(","))
            End If
            '*******************APERURA CONNESSIONE*********************182
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/
            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/SELEZIONE UNITA IN CONDOMINIO ED AVENTI UN CONTRATTO

            par.cmd.CommandText = "SELECT   CONDOMINI.denominazione AS condominio,UNITA_IMMOBILIARI.cod_unita_immobiliare AS cod_unita," _
                                & "		 (INDIRIZZI.descrizione||','||INDIRIZZI.civico) AS indirizzo," _
                                & "		 UNITA_IMMOBILIARI.interno AS interno," _
                                & "         SCALE_EDIFICI.descrizione AS scala," _
                                & "		 IDENTIFICATIVI_CATASTALI.foglio,IDENTIFICATIVI_CATASTALI.numero,IDENTIFICATIVI_CATASTALI.sub," _
                                & "		 TIPO_LIVELLO_PIANO.descrizione AS piano," _
                                & "         TIPOLOGIA_UNITA_IMMOBILIARI.descrizione AS tipologia," _
                                & "         (CASE" _
                                & "             WHEN siscom_mi.Getstatocontratto2" _
                                & "                                             (UNITA_CONTRATTUALE.id_contratto," _
                                & "                                              0" _
                                & "                                             ) = 'CHIUSO'" _
                                & "                THEN ''" _
                                & "             ELSE siscom_mi.Getintestatari (UNITA_CONTRATTUALE.id_contratto," _
                                & "                                            0)" _
                                & "          END" _
                                & "         ) AS intestatario," _
                                & "		 TIPO_DISPONIBILITA.descrizione AS stato_occupazione," _
                                & "         (CASE" _
                                & "             WHEN ANAGRAFICA.ragione_sociale IS NOT NULL" _
                                & "                THEN ragione_sociale" _
                                & "             ELSE RTRIM (LTRIM (cognome || ' ' || nome))" _
                                & "          END" _
                                & "         ) AS nominativo," _
                                & "            RAPPORTI_UTENZA.tipo_cor" _
                                & "         || ' '" _
                                & "         || RAPPORTI_UTENZA.via_cor" _
                                & "         || ' '" _
                                & "         || RAPPORTI_UTENZA.civico_cor" _
                                & "         || ' - '" _
                                & "         || RAPPORTI_UTENZA.cap_cor" _
                                & "         || ' '" _
                                & "         || RAPPORTI_UTENZA.luogo_cor AS recapito," _
                                & "		 ANAGRAFICA.cod_fiscale,ANAGRAFICA.PARTITA_IVA,ANAGRAFICA.telefono," _
                                & "		 " _
                                & "         (SELECT    COND_AMMINISTRATORI.cognome" _
                                & "                 || ' '" _
                                & "                 || COND_AMMINISTRATORI.nome" _
                                & "            FROM siscom_mi.COND_AMMINISTRATORI," _
                                & "                 siscom_mi.COND_AMMINISTRAZIONE" _
                                & "           WHERE siscom_mi.COND_AMMINISTRATORI.ID =" _
                                & "                              siscom_mi.COND_AMMINISTRAZIONE.id_amministratore" _
                                & "             AND id_condominio = COND_UI.id_condominio" _
                                & "             AND data_fine IS NULL) AS amministratore," _
                                & "         (CASE" _
                                & "             WHEN UNITA_CONTRATTUALE.id_contratto IS NULL" _
                                & "                THEN ''" _
                                & "             ELSE siscom_mi.Getstatocontratto2" _
                                & "                                             (UNITA_CONTRATTUALE.id_contratto," _
                                & "                                              0" _
                                & "                                             )" _
                                & "          END" _
                                & "         ) AS stato_contratto," _
                                & "         posizione_bilancio, '' AS num_comp_nucleo, '' AS num_ospiti," _
                                & "         TO_CHAR (mil_pro, '9G999G990D9999') AS mil_pro," _
                                & "         TO_CHAR (mil_asc, '9G999G990D9999') AS mil_asc," _
                                & "         TO_CHAR (mil_compro, '9G999G990D9999') AS mil_compro," _
                                & "         TO_CHAR (mil_gest, '9G999G990D9999') AS mil_gest," _
                                & "         TO_CHAR (mil_risc, '9G999G990D9999') AS mil_risc," _
                                & "         TO_CHAR (mill_pres_ass, '9G999G990D9999') AS mill_pres_ass," _
                                & "         COND_UI.NOTE" _
                                & "    FROM siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI," _
                                & "         siscom_mi.UNITA_IMMOBILIARI," _
                                & "         siscom_mi.EDIFICI," _
                                & "         siscom_mi.COND_UI," _
                                & "         siscom_mi.UNITA_CONTRATTUALE," _
                                & "         siscom_mi.ANAGRAFICA," _
                                & "         siscom_mi.INDIRIZZI," _
                                & "         siscom_mi.SCALE_EDIFICI," _
                                & "         siscom_mi.TIPO_DISPONIBILITA," _
                                & "         siscom_mi.RAPPORTI_UTENZA," _
                                & "         siscom_mi.CONDOMINI," _
                                & "		 	siscom_mi.IDENTIFICATIVI_CATASTALI," _
                                & "		 	siscom_mi.TIPO_LIVELLO_PIANO		 " _
                                & "   WHERE UNITA_IMMOBILIARI.cod_tipologia = TIPOLOGIA_UNITA_IMMOBILIARI.cod " _
                                & "     AND UNITA_IMMOBILIARI.id_indirizzo = INDIRIZZI.ID " _
                                & "     AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.id_scala " _
                                & "     AND UNITA_IMMOBILIARI.id_edificio = EDIFICI.ID " _
                                & "     AND UNITA_IMMOBILIARI.cod_tipo_disponibilita = TIPO_DISPONIBILITA.cod(+) " _
                                & "     AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.id_unita(+) " _
                                & "	 AND UNITA_CONTRATTUALE.id_contratto = siscom_mi.Getultimoru(UNITA_IMMOBILIARI.ID) " _
                                & "     AND COND_UI.id_ui(+) = UNITA_IMMOBILIARI.ID " _
                                & "     AND cod_tipo_disponibilita <> 'VEND' " _
                                & "     AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.id_contratto " _
                                & "     AND COND_UI.id_intestario = ANAGRAFICA.ID(+) " _
                                & "	 AND UNITA_IMMOBILIARI.id_unita_principale IS NULL " _
                                & "     AND CONDOMINI.ID = COND_UI.id_condominio " _
                                & "	 AND IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                                & "	 AND TIPO_LIVELLO_PIANO.cod(+) = UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO "

            If CondSelezionati <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & "" _
                                    & "AND cond_ui.id_condominio in (" & CondSelezionati & ")  AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL  "
                '& "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (" & CondSelezionati & "))) "
            End If
            If ammSelezionati <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & "" _
                                    & "AND (cond_ui.id_condominio in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null)  AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL ) " ' _
                '& "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null))) "
            End If

            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/
            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/UNION UNITA IN CONDOMINIO SENZA UN CONTRATTO
            par.cmd.CommandText = par.cmd.CommandText & "  UNION " _
                                                        & "SELECT CONDOMINI.denominazione AS condominio," _
                                                        & "       UNITA_IMMOBILIARI.cod_unita_immobiliare AS cod_unita," _
                                                        & "       (INDIRIZZI.descrizione || ',' || INDIRIZZI.civico) AS indirizzo," _
                                                        & "       UNITA_IMMOBILIARI.interno AS interno," _
                                                        & "       SCALE_EDIFICI.descrizione AS scala, IDENTIFICATIVI_CATASTALI.foglio," _
                                                        & "       IDENTIFICATIVI_CATASTALI.numero, IDENTIFICATIVI_CATASTALI.sub," _
                                                        & "       TIPO_LIVELLO_PIANO.descrizione AS piano," _
                                                        & "       TIPOLOGIA_UNITA_IMMOBILIARI.descrizione AS tipologia," _
                                                        & "       '' AS intestatario," _
                                                        & "       TIPO_DISPONIBILITA.descrizione AS stato_occupazione," _
                                                        & "       (CASE" _
                                                        & "           WHEN ANAGRAFICA.ragione_sociale IS NOT NULL" _
                                                        & "              THEN ragione_sociale" _
                                                        & "           ELSE RTRIM (LTRIM (cognome || ' ' || nome))" _
                                                        & "        END" _
                                                        & "       ) AS nominativo," _
                                                        & "       '' AS recapito, ANAGRAFICA.cod_fiscale, ANAGRAFICA.partita_iva," _
                                                        & "       ANAGRAFICA.telefono," _
                                                        & "       (SELECT    COND_AMMINISTRATORI.cognome" _
                                                        & "               || ' '" _
                                                        & "               || COND_AMMINISTRATORI.nome" _
                                                        & "          FROM siscom_mi.COND_AMMINISTRATORI, siscom_mi.COND_AMMINISTRAZIONE" _
                                                        & "         WHERE siscom_mi.COND_AMMINISTRATORI.ID =" _
                                                        & "                              siscom_mi.COND_AMMINISTRAZIONE.id_amministratore" _
                                                        & "           AND id_condominio = COND_UI.id_condominio" _
                                                        & "           AND data_fine IS NULL) AS amministratore," _
                                                        & "       '' AS stato_contratto, posizione_bilancio, '' AS num_comp_nucleo," _
                                                        & "       '' AS num_ospiti, TO_CHAR (mil_pro, '9G999G990D9999') AS mil_pro," _
                                                        & "       TO_CHAR (mil_asc, '9G999G990D9999') AS mil_asc," _
                                                        & "       TO_CHAR (mil_compro, '9G999G990D9999') AS mil_compro," _
                                                        & "       TO_CHAR (mil_gest, '9G999G990D9999') AS mil_gest," _
                                                        & "       TO_CHAR (mil_risc, '9G999G990D9999') AS mil_risc," _
                                                        & "       TO_CHAR (mill_pres_ass, '9G999G990D9999') AS mill_pres_ass," _
                                                        & "       COND_UI.NOTE" _
                                                        & "  FROM siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI," _
                                                        & "       siscom_mi.UNITA_IMMOBILIARI," _
                                                        & "       siscom_mi.EDIFICI," _
                                                        & "       siscom_mi.COND_UI," _
                                                        & "       siscom_mi.ANAGRAFICA," _
                                                        & "       siscom_mi.INDIRIZZI," _
                                                        & "       siscom_mi.SCALE_EDIFICI," _
                                                        & "       siscom_mi.TIPO_DISPONIBILITA," _
                                                        & "       siscom_mi.CONDOMINI," _
                                                        & "       siscom_mi.IDENTIFICATIVI_CATASTALI," _
                                                        & "       siscom_mi.TIPO_LIVELLO_PIANO " _
                                                        & " WHERE UNITA_IMMOBILIARI.cod_tipologia = TIPOLOGIA_UNITA_IMMOBILIARI.cod" _
                                                        & "   AND UNITA_IMMOBILIARI.id_indirizzo = INDIRIZZI.ID" _
                                                        & "   AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.id_scala" _
                                                        & "   AND UNITA_IMMOBILIARI.id_edificio = EDIFICI.ID" _
                                                        & "   AND UNITA_IMMOBILIARI.cod_tipo_disponibilita = TIPO_DISPONIBILITA.cod(+) " _
                                                        & "   AND COND_UI.id_ui(+) = UNITA_IMMOBILIARI.ID" _
                                                        & "   AND cod_tipo_disponibilita <> 'VEND'" _
                                                        & "   AND COND_UI.id_intestario = ANAGRAFICA.ID(+)" _
                                                        & "   AND UNITA_IMMOBILIARI.id_unita_principale IS NULL" _
                                                        & "   AND CONDOMINI.ID = COND_UI.id_condominio" _
                                                        & "   AND IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.id_catastale" _
                                                        & "   AND TIPO_LIVELLO_PIANO.cod(+) = UNITA_IMMOBILIARI.cod_tipo_livello_piano" _
                                                        & "   AND UNITA_IMMOBILIARI.ID NOT IN (SELECT id_unita FROM SISCOM_MI.UNITA_CONTRATTUALE) "

            If CondSelezionati <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & "" _
                                    & "AND cond_ui.id_condominio in (" & CondSelezionati & ")  AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL  "
                '& "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (" & CondSelezionati & "))) "
            End If
            If ammSelezionati <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & "" _
                                    & "AND (cond_ui.id_condominio in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null)  AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL ) " ' _
                '& "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null))) "
            End If


            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/
            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/UNION UNITA FACENTI PARTE DI UN EDIFICIO IN CONDOMINIO E UNITA NON IN CONDOMINIO CON UN CONTRATTO

            par.cmd.CommandText = par.cmd.CommandText & " UNION " _
                                & "SELECT   '-- NON IN GESTIONE CONDOMINIALE --' AS condominio," _
                                & "         unita_immobiliari.cod_unita_immobiliare AS cod_unita," _
                                & "         (indirizzi.descrizione || ',' || indirizzi.civico) AS indirizzo," _
                                & "         unita_immobiliari.interno AS interno," _
                                & "         scale_edifici.descrizione AS scala, identificativi_catastali.foglio," _
                                & "         identificativi_catastali.numero, identificativi_catastali.sub," _
                                & "         tipo_livello_piano.descrizione AS piano," _
                                & "         tipologia_unita_immobiliari.descrizione AS tipologia," _
                                & "         (CASE" _
                                & "             WHEN siscom_mi.getstatocontratto2" _
                                & "                                             (unita_contrattuale.id_contratto," _
                                & "                                              0" _
                                & "                                             ) = 'CHIUSO'" _
                                & "                THEN ''" _
                                & "             ELSE siscom_mi.getintestatari (unita_contrattuale.id_contratto," _
                                & "                                            0)" _
                                & "          END" _
                                & "         ) AS intestatario," _
                                & "         tipo_disponibilita.descrizione AS stato_occupazione," _
                                & "         (CASE" _
                                & "             WHEN anagrafica.ragione_sociale IS NOT NULL" _
                                & "                THEN ragione_sociale" _
                                & "             ELSE RTRIM (LTRIM (cognome || ' ' || nome))" _
                                & "          END" _
                                & "         ) AS nominativo," _
                                & "            rapporti_utenza.tipo_cor" _
                                & "         || ' '" _
                                & "         || rapporti_utenza.via_cor" _
                                & "         || ' '" _
                                & "         || rapporti_utenza.civico_cor" _
                                & "         || ' - '" _
                                & "         || rapporti_utenza.cap_cor" _
                                & "         || ' '" _
                                & "         || rapporti_utenza.luogo_cor AS recapito," _
                                & "         anagrafica.cod_fiscale, anagrafica.partita_iva, anagrafica.telefono," _
                                & "         (SELECT    cond_amministratori.cognome" _
                                & "                 || ' '" _
                                & "                 || cond_amministratori.nome" _
                                & "            FROM siscom_mi.cond_amministratori," _
                                & "                 siscom_mi.cond_amministrazione" _
                                & "           WHERE siscom_mi.cond_amministratori.ID =" _
                                & "                              siscom_mi.cond_amministrazione.id_amministratore" _
                                & "             " _
                                & "             AND data_fine IS NULL) AS amministratore," _
                                & "         (CASE" _
                                & "             WHEN unita_contrattuale.id_contratto IS NULL" _
                                & "                THEN ''" _
                                & "             ELSE siscom_mi.getstatocontratto2" _
                                & "                                             (unita_contrattuale.id_contratto," _
                                & "                                              0" _
                                & "                                             )" _
                                & "          END" _
                                & "         ) AS stato_contratto," _
                                & "         '' AS posizione_bilancio, '' AS num_comp_nucleo, '' AS num_ospiti," _
                                & "         '' AS mil_pro," _
                                & "         '' AS mil_asc," _
                                & "         '' AS mil_compro," _
                                & "         '' AS mil_gest," _
                                & "        ''AS mil_risc," _
                                & "         '' AS mill_pres_ass," _
                                & "         '' AS note" _
                                & "    FROM siscom_mi.tipologia_unita_immobiliari," _
                                & "         siscom_mi.unita_immobiliari," _
                                & "         siscom_mi.edifici," _
                                & "         siscom_mi.unita_contrattuale," _
                                & "         siscom_mi.anagrafica," _
                                & "         siscom_mi.indirizzi," _
                                & "         siscom_mi.scale_edifici," _
                                & "         siscom_mi.tipo_disponibilita," _
                                & "         siscom_mi.rapporti_utenza," _
                                & "        siscom_mi.soggetti_contrattuali," _
                                & "         siscom_mi.identificativi_catastali," _
                                & "         siscom_mi.tipo_livello_piano" _
                                & "   WHERE unita_immobiliari.cod_tipologia = tipologia_unita_immobiliari.cod" _
                                & "     AND unita_immobiliari.id_indirizzo = indirizzi.ID" _
                                & "     AND scale_edifici.ID(+) = unita_immobiliari.id_scala" _
                                & "     AND unita_immobiliari.id_edificio = edifici.ID" _
                                & "     AND unita_immobiliari.cod_tipo_disponibilita = tipo_disponibilita.cod(+) " _
                                & "     AND unita_immobiliari.ID = unita_contrattuale.id_unita(+)" _
                                & "     AND unita_contrattuale.id_contratto = siscom_mi.getultimoru (unita_immobiliari.ID) " _
                                & "     AND cod_tipo_disponibilita <> 'VEND' " _
                                & "     AND rapporti_utenza.ID(+) = unita_contrattuale.id_contratto " _
                                & "	 AND soggetti_contrattuali.id_contratto(+) = unita_contrattuale.id_contratto " _
                                & "	 AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
                                & "     AND soggetti_contrattuali.id_anagrafica = anagrafica.ID(+) " _
                                & "     AND unita_immobiliari.id_unita_principale IS NULL " _
                                & "     AND identificativi_catastali.ID(+) = unita_immobiliari.id_catastale " _
                                & "     AND tipo_livello_piano.cod(+) = unita_immobiliari.cod_tipo_livello_piano "
            If CondSelezionati <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & "" _
                                & "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (" & CondSelezionati & ")) " _
                                & "AND unita_immobiliari.ID NOT IN (SELECT id_ui FROM siscom_mi.cond_ui WHERE ID_CONDOMINIO in (" & CondSelezionati & ")) "
            End If
            If ammSelezionati <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & "" _
                                & "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null)) " _
                                & "AND unita_immobiliari.ID NOT IN (SELECT id_ui FROM siscom_mi.cond_ui WHERE ID_CONDOMINIO in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null)) "
            End If

            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/
            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/UNION UNITA FACENTI PARTE DI UN EDIFICIO IN CONDOMINIO E UNITA NON IN CONDOMINIO SENZA UN CONTRATTO
            par.cmd.CommandText = par.cmd.CommandText & "  UNION " _
                                                        & "SELECT   '-- NON IN GESTIONE CONDOMINIALE --' AS condominio," _
                                                        & "         UNITA_IMMOBILIARI.cod_unita_immobiliare AS cod_unita," _
                                                        & "         (INDIRIZZI.descrizione || ',' || INDIRIZZI.civico) AS indirizzo," _
                                                        & "         UNITA_IMMOBILIARI.interno AS interno," _
                                                        & "         SCALE_EDIFICI.descrizione AS scala, IDENTIFICATIVI_CATASTALI.foglio," _
                                                        & "         IDENTIFICATIVI_CATASTALI.numero, IDENTIFICATIVI_CATASTALI.sub," _
                                                        & "         TIPO_LIVELLO_PIANO.descrizione AS piano," _
                                                        & "         TIPOLOGIA_UNITA_IMMOBILIARI.descrizione AS tipologia," _
                                                        & "         '' AS intestatario," _
                                                        & "         TIPO_DISPONIBILITA.descrizione AS stato_occupazione," _
                                                        & "         '' AS nominativo," _
                                                        & "		 '' AS recapito," _
                                                        & "         '' AS cod_fiscale, '' AS partita_iva, '' AS telefono," _
                                                        & "		 '' AS amministratore," _
                                                        & "         '' AS stato_contratto," _
                                                        & "         '' AS posizione_bilancio, '' AS num_comp_nucleo, '' AS num_ospiti," _
                                                        & "         '' AS mil_pro," _
                                                        & "         '' AS mil_asc," _
                                                        & "         '' AS mil_compro," _
                                                        & "         '' AS mil_gest," _
                                                        & "         '' AS mil_risc," _
                                                        & "         '' AS mill_pres_ass,'' as note        " _
                                                        & "    FROM siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI," _
                                                        & "         siscom_mi.UNITA_IMMOBILIARI," _
                                                        & "         siscom_mi.EDIFICI," _
                                                        & "         siscom_mi.INDIRIZZI," _
                                                        & "         siscom_mi.SCALE_EDIFICI," _
                                                        & "         siscom_mi.TIPO_DISPONIBILITA," _
                                                        & "         siscom_mi.IDENTIFICATIVI_CATASTALI," _
                                                        & "         siscom_mi.TIPO_LIVELLO_PIANO" _
                                                        & "   WHERE UNITA_IMMOBILIARI.cod_tipologia = TIPOLOGIA_UNITA_IMMOBILIARI.cod" _
                                                        & "     AND UNITA_IMMOBILIARI.id_indirizzo = INDIRIZZI.ID" _
                                                        & "     AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.id_scala" _
                                                        & "     AND UNITA_IMMOBILIARI.id_edificio = EDIFICI.ID" _
                                                        & "     AND UNITA_IMMOBILIARI.cod_tipo_disponibilita = TIPO_DISPONIBILITA.cod(+)" _
                                                        & "     AND cod_tipo_disponibilita <> 'VEND'" _
                                                        & "     AND UNITA_IMMOBILIARI.id_unita_principale IS NULL" _
                                                        & "     AND IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.id_catastale " _
                                                        & "     AND TIPO_LIVELLO_PIANO.cod(+) = UNITA_IMMOBILIARI.cod_tipo_livello_piano " _
                                                        & "	    AND UNITA_IMMOBILIARI.ID NOT IN (SELECT id_unita FROM SISCOM_MI.UNITA_CONTRATTUALE)	"

            If CondSelezionati <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & "" _
                                & "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (" & CondSelezionati & ")) " _
                                & "AND unita_immobiliari.ID NOT IN (SELECT id_ui FROM siscom_mi.cond_ui WHERE ID_CONDOMINIO in (" & CondSelezionati & ")) "
            End If
            If ammSelezionati <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & "" _
                                & "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null)) " _
                                & "AND unita_immobiliari.ID NOT IN (SELECT id_ui FROM siscom_mi.cond_ui WHERE ID_CONDOMINIO in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null)) "
            End If





            'par.cmd.CommandText = par.cmd.CommandText & "AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL "
            par.cmd.CommandText = par.cmd.CommandText & "ORDER BY cod_unita asc, INTERNO ASC, SCALA ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Dim xls As New ExcelSiSol
            Dim Nome As String = "ExportInquilini"
            Dim nomefile1 As String = ""
            Try


                'nomefile1 = par.EsportaExcelAutomaticoDaGridViewAutogenerato(GridView1, "SelectExport", False, False)
                nomefile1 = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, Nome, Nome, dt, True, "../FileTemp/", False)
                If File.Exists(Server.MapPath("~\FileTemp\") & nomefile1) Then
                    'Response.Write("<script>window.open('../FileTemp/" & nomefile1 & "','');</script>")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomefile1 & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)

                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If

            Catch ex As Exception
                Response.Write("<script>alert('" & ex.Message & "')</script>")

            End Try





            'Me.dgvExport.Visible = True
            'Me.dgvExport.DataSource = dt
            'Me.dgvExport.DataBind()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Esporta()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: btnExp " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub
    Protected Sub btnExportXls_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportXls.Click
        ExportXLS()

        '    Try
        '        Dim CondSelezionati As String = ""
        '        Dim ammSelezionati As String = ""
        '        For Each i As ListItem In chkCondomini.Items
        '            If i.Selected = True Then
        '                CondSelezionati += i.Value & ","
        '            End If
        '        Next
        '        For Each i As ListItem In chkAmministratori.Items
        '            If i.Selected = True Then
        '                ammSelezionati += i.Value & ","
        '            End If
        '        Next
        '        If ammSelezionati = "" And CondSelezionati = "" Then
        '            Response.Write("<script>alert('Selezionare almeno un criterio per effettuare il report')</script>")
        '            Exit Sub
        '        End If
        '        If ammSelezionati <> "" Then
        '            ammSelezionati = ammSelezionati.Substring(0, ammSelezionati.LastIndexOf(","))
        '        End If
        '        If CondSelezionati <> "" Then
        '            CondSelezionati = CondSelezionati.Substring(0, CondSelezionati.LastIndexOf(","))
        '        End If
        '        '*******************APERURA CONNESSIONE*********************182
        '        If par.OracleConn.State = Data.ConnectionState.Closed Then
        '            par.OracleConn.Open()
        '            par.SettaCommand(par)
        '        End If
        '        par.cmd.CommandText = "SELECT   unita_immobiliari.cod_unita_immobiliare AS COD_UNITA, tipologia_unita_immobiliari.descrizione AS TIPOLOGIA, unita_immobiliari.interno AS INTERNO, " _
        '                            & "scale_edifici.descrizione AS SCALA, tipo_disponibilita.descrizione AS STATO_OCCUPAZIONE, (CASE WHEN siscom_mi.getstatocontratto2(unita_contrattuale.id_contratto,0) = 'CHIUSO' THEN '' ELSE siscom_mi.getintestatari (unita_contrattuale.id_contratto,0) End) AS intestatario, " _
        '                            & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (cognome || ' ' || nome)) End ) AS nominativo, condomini.denominazione as Condominio, " _
        '                            & "(select cond_amministratori.cognome || ' ' || cond_amministratori.nome from siscom_mi.cond_amministratori,siscom_mi.cond_amministrazione where siscom_mi.cond_amministratori.id=siscom_mi.cond_amministrazione.id_amministratore " _
        '                            & "and id_condominio=cond_ui.id_condominio and data_fine is null) as Amministratore,rapporti_utenza.tipo_cor " _
        '                            & "|| ' ' || rapporti_utenza.via_cor || ' ' || rapporti_utenza.civico_cor || ' - ' || rapporti_utenza.cap_cor || ' ' || rapporti_utenza.luogo_cor AS RECAPITO, " _
        '                            & "(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.getstatocontratto2 (unita_contrattuale.id_contratto,0) End) As Stato, " _
        '                            & "posizione_bilancio, '' AS num_comp_nucleo, '' AS num_ospiti, TO_CHAR (mil_pro, '9G999G990D9999') AS mil_pro, TO_CHAR (mil_asc, '9G999G990D9999') AS mil_asc, " _
        '                            & "TO_CHAR (mil_compro, '9G999G990D9999') AS mil_compro,TO_CHAR (mil_gest, '9G999G990D9999') AS mil_gest,TO_CHAR (mil_risc, '9G999G990D9999') AS mil_risc,TO_CHAR (mill_pres_ass, '9G999G990D9999') AS mill_pres_ass, COND_UI.NOTE " _
        '                            & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, " _
        '                            & "SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA, SISCOM_MI.INDIRIZZI," _
        '                            & "SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.TIPO_DISPONIBILITA, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.CONDOMINI " _
        '                            & "WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
        '                            & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID AND SCALE_EDIFICI.ID(+)= UNITA_IMMOBILIARI.ID_SCALA " _
        '                            & "AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD " _
        '                            & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+)  AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID " _
        '                            & "AND COD_TIPO_DISPONIBILITA <> 'VEND' AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO AND  COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) "
        '        If CondSelezionati <> "" Then
        '            par.cmd.CommandText = par.cmd.CommandText & "" _
        '                                & "AND cond_ui.id_condominio in (" & CondSelezionati & ")  AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL  "
        '            '& "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (" & CondSelezionati & "))) "
        '        End If
        '        If ammSelezionati <> "" Then
        '            par.cmd.CommandText = par.cmd.CommandText & "" _
        '                                & "AND (cond_ui.id_condominio in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null)  AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL ) " ' _
        '            '& "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null))) "
        '        End If
        '        par.cmd.CommandText = par.cmd.CommandText & " AND CONDOMINI.ID = COND_UI.ID_CONDOMINIO UNION " _
        '                            & "SELECT   unita_immobiliari.cod_unita_immobiliare AS COD_UNITA, tipologia_unita_immobiliari.descrizione AS TIPOLOGIA, unita_immobiliari.interno AS INTERNO, scale_edifici.descrizione AS SCALA, " _
        '                            & "tipo_disponibilita.descrizione AS STATO_OCCUPAZIONE,(CASE WHEN siscom_mi.getstatocontratto2(unita_contrattuale.id_contratto,0) = 'CHIUSO' THEN '' ELSE siscom_mi.getintestatari (unita_contrattuale.id_contratto,0) End) AS intestatario, " _
        '                            & "'' AS nominativo, '' as Condominio, '' As Amministratore, rapporti_utenza.tipo_cor || ' ' || rapporti_utenza.via_cor || ' ' || rapporti_utenza.civico_cor || ' - ' || rapporti_utenza.cap_cor || ' ' || rapporti_utenza.luogo_cor AS RECAPITO, " _
        '                            & "(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.getstatocontratto2 (unita_contrattuale.id_contratto,0) End ) As Stato, " _
        '                            & "'' AS posizione_bilancio, '' AS num_comp_nucleo,'' AS num_ospiti, '' AS mil_pro, '' AS mil_asc, '' AS mil_compro,'' AS mil_gest, '' AS mil_risc, '' AS mill_pres_ass, '' AS NOTE " _
        '                            & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, " _
        '                            & "SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI, " _
        '                            & "SISCOM_MI.TIPO_DISPONIBILITA, SISCOM_MI.RAPPORTI_UTENZA " _
        '                            & "WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID " _
        '                            & "AND SCALE_EDIFICI.ID(+)= UNITA_IMMOBILIARI.ID_SCALA  AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
        '                            & "AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+)  " _
        '                            & "AND COD_TIPO_DISPONIBILITA <> 'VEND' AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO "
        '        If CondSelezionati <> "" Then
        '            par.cmd.CommandText = par.cmd.CommandText & "" _
        '                            & "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (" & CondSelezionati & ")) " _
        '                            & "AND unita_immobiliari.ID NOT IN (SELECT id_ui FROM siscom_mi.cond_ui WHERE ID_CONDOMINIO in (" & CondSelezionati & ")) "
        '        End If
        '        If ammSelezionati <> "" Then
        '            par.cmd.CommandText = par.cmd.CommandText & "" _
        '                            & "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null)) " _
        '                            & "AND unita_immobiliari.ID NOT IN (SELECT id_ui FROM siscom_mi.cond_ui WHERE ID_CONDOMINIO in (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & ammSelezionati & ") and siscom_mi.cond_amministrazione.DATA_FINE is null)) "
        '        End If
        '        par.cmd.CommandText = par.cmd.CommandText & "AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
        '                            & "ORDER BY cod_unita asc, INTERNO ASC, SCALA ASC"
        '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '        Dim dt As New Data.DataTable
        '        da.Fill(dt)
        '        Me.dgvExport.Visible = True
        '        Me.dgvExport.DataSource = dt
        '        Me.dgvExport.DataBind()
        '        '*********************CHIUSURA CONNESSIONE**********************
        '        par.OracleConn.Close()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '        Esporta()
        '    Catch ex As Exception
        '        If par.OracleConn.State = Data.ConnectionState.Open Then
        '            '*********************CHIUSURA CONNESSIONE**********************
        '            par.OracleConn.Close()
        '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '        End If
        '        Session.Add("ERRORE", "Provenienza: btnExp " & Page.Title & " - " & ex.Message)
        '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        '    End Try
    End Sub
    'Private Sub Esporta()
    '    Try
    '        Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(Me.dgvExport, "ExportInquilini", , , , False)
    '        If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
    '            'Response.Redirect("..\/FileTemp/" & nomefile, False)
    '            'Response.Expires = -1
    '            Response.Write("<script>avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomefile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}</script>")
    '        Else
    '            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
    '        End If
    '        Me.dgvExport.Visible = False


    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: btnExp " & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try
    'End Sub
    Protected Sub chkAmministratori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAmministratori.SelectedIndexChanged
        CaricaCheckAmministratori()
    End Sub
    Private Sub CaricaCheckAmministratori()
        Try
            chkCondomini.Items.Clear()
            Dim StringaCheckAmministratori As String = ""
            For Each Items As ListItem In chkAmministratori.Items
                If Items.Selected = True Then
                    StringaCheckAmministratori = StringaCheckAmministratori & Items.Value & ","
                End If
            Next
            If StringaCheckAmministratori <> "" Then
                StringaCheckAmministratori = Left(StringaCheckAmministratori, Len(StringaCheckAmministratori) - 1)
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select id, denominazione from siscom_mi.condomini " _
                                    & "WHERE SISCOM_MI.CONDOMINI.ID IN (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & StringaCheckAmministratori & ") and siscom_mi.cond_amministrazione.DATA_FINE is null) " _
                                    & "order by denominazione asc"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                Me.chkCondomini.DataSource = dt
                Me.chkCondomini.DataBind()
            Else
                CaricaAmministratori()
                CaricaCondomini()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: caricacheckamministratori " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
