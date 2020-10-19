
Partial Class Contratti_DettagliGestionaleCrediti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            idBoll = Request.QueryString("IDBOLL")
            CaricaDettagliCredito()
        End If
    End Sub

    Public Property idBoll() As Long
        Get
            If Not (ViewState("par_idBoll") Is Nothing) Then
                Return CLng(ViewState("par_idBoll"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idBoll") = value
        End Set
    End Property

    Public Property idContr() As Long
        Get
            If Not (ViewState("par_idContr") Is Nothing) Then
                Return CLng(ViewState("par_idContr"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idContr") = value
        End Set
    End Property

    Private Sub CaricaDettagliCredito()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim dtPerDatagr As New Data.DataTable
            'Dim idContr As Long = 0

            Dim RIGA As System.Data.DataRow
            Dim ripartizParz As Boolean = False

            dtPerDatagr.Columns.Add("ID")
            dtPerDatagr.Columns.Add("TIPO_DOC")
            dtPerDatagr.Columns.Add("IMPORTO")
            dtPerDatagr.Columns.Add("TIPO_APPL")
            dtPerDatagr.Columns.Add("N_RATE")
            dtPerDatagr.Columns.Add("DA_RATA")
            dtPerDatagr.Columns.Add("IMP_RATA")
            dtPerDatagr.Columns.Add("DATA_APPL")
            dtPerDatagr.Columns.Add("OPERATORE")
            dtPerDatagr.Columns.Add("TOT_IMPORTO")
            dtPerDatagr.Columns.Add("CREDITO_RESIDUO")
            dtPerDatagr.Columns.Add("ID_BOLLETTA")

            Dim elencoVoci As String = ""
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI_GEST.* FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST " _
                & " WHERE BOL_BOLLETTE_GEST.ID=BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST and BOL_BOLLETTE_GEST.ID=" & idBoll
            Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtDocGest0 As New Data.DataTable
            da0.Fill(dtDocGest0)
            da0.Dispose()
            If dtDocGest0.Rows.Count > 0 Then
                For i As Integer = 0 To dtDocGest0.Rows.Count - 1
                    elencoVoci = elencoVoci & par.IfNull(dtDocGest0.Rows(i).Item("ID"), -1) & ","
                Next
            End If

            If elencoVoci <> "" Then
                elencoVoci = "(" & Mid(elencoVoci, 1, Len(elencoVoci) - 1) & ")"
            End If

            ' MODIFICA SEGNALAZIONE 1559/2017

            'par.cmd.CommandText = "SELECT BOL_BOLLETTE_GEST.*,TIPO_BOLLETTE_GEST.descrizione FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST" _
            '    & " WHERE BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID AND BOL_BOLLETTE_GEST.ID=" & idBoll

            par.cmd.CommandText = "select bol_bollette_gest.id,DESCRIZIONE,IMPORTO_TOTALE,DATA_APPLICAZIONE,ID_OPERATORE_APPLICAZIONE," _
                                & " ID_BOLLETTA,BOL_BOLLETTE_GEST.ID_CONTRATTO,TIPO_APPLICAZIONE,BOL_SCHEMA.ID AS ID_SCHEMA" _
                                & " FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST,SISCOM_MI.BOL_SCHEMA" _
                                & " where BOL_SCHEMA.ID_VOCE_BOLLETTA_GEST(+) = BOL_BOLLETTE_VOCI_GEST.ID AND BOL_BOLLETTE_GEST.ID = BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST AND BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID AND BOL_BOLLETTE_GEST.ID= " & idBoll _
                                & " GROUP BY bol_bollette_gest.id,DESCRIZIONE,IMPORTO_TOTALE,DATA_APPLICAZIONE,ID_OPERATORE_APPLICAZIONE,ID_BOLLETTA,BOL_BOLLETTE_GEST.ID_CONTRATTO,TIPO_APPLICAZIONE,BOL_SCHEMA.ID"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtDocGest As New Data.DataTable
            da.Fill(dtDocGest)
            da.Dispose()

            'If dtDocGest.Rows.Count - 1 > 0 Then


            For i As Integer = 0 To dtDocGest.Rows.Count - 1
                RIGA = dtPerDatagr.NewRow()
                RIGA.Item("ID") = idBoll
                RIGA.Item("TIPO_DOC") = par.IfNull(dtDocGest.Rows(i).Item("DESCRIZIONE"), "")
                RIGA.Item("IMPORTO") = Format(CDec(par.IfNull(dtDocGest.Rows(i).Item("IMPORTO_TOTALE"), 0)), "##,##0.00")
                RIGA.Item("DATA_APPL") = par.FormattaData(par.IfNull(dtDocGest.Rows(i).Item("DATA_APPLICAZIONE"), ""))
                RIGA.Item("OPERATORE") = par.IfNull(dtDocGest.Rows(i).Item("ID_OPERATORE_APPLICAZIONE"), "")
                RIGA.Item("ID_BOLLETTA") = par.IfNull(dtDocGest.Rows(i).Item("ID_BOLLETTA"), "")
                idContr = par.IfNull(dtDocGest.Rows(i).Item("ID_CONTRATTO"), 0)

                Select Case par.IfNull(dtDocGest.Rows(i).Item("TIPO_APPLICAZIONE"), "")
                    Case "N"
                        RIGA.Item("TIPO_APPL") = "Nessuna"
                        RIGA.Item("N_RATE") = ""
                        RIGA.Item("IMP_RATA") = ""
                        RIGA.Item("DA_RATA") = ""
                    Case "P"
                        RIGA.Item("TIPO_APPL") = "Parziale"
                    Case "T"
                        If IsNumeric(RIGA.Item("ID_BOLLETTA")) Then
                            RIGA.Item("TIPO_APPL") = "Rimborso"
                        Else
                            RIGA.Item("TIPO_APPL") = "Totale"
                        End If
                End Select

                par.cmd.CommandText = "SELECT * FROM OPERATORI WHERE ID=" & par.IfNull(dtDocGest.Rows(i).Item("ID_OPERATORE_APPLICAZIONE"), 0)
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    RIGA.Item("OPERATORE") = par.IfNull(myReader2("OPERATORE"), "")
                End If
                myReader2.Close()

                If elencoVoci <> "" Then
                    ' MODIFICA SEGNALAZIONE 1559/2017
                    'par.cmd.CommandText = "SELECT sum(importo) as impTOT,da_Rata,per_Rate FROM SISCOM_MI.BOL_SCHEMA WHERE ID_VOCE_BOLLETTA_GEST in " & elencoVoci & " group by importo,da_rata,PER_RATE"
                    par.cmd.CommandText = "SELECT sum(importo) as impTOT,da_Rata,per_Rate FROM SISCOM_MI.BOL_SCHEMA WHERE id = " & par.IfNull(dtDocGest.Rows(i).Item("ID_SCHEMA"), "0") & " group by importo,da_rata,PER_RATE"

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        RIGA.Item("N_RATE") = par.IfNull(myReader1("PER_RATE"), "")
                        RIGA.Item("IMP_RATA") = Format(CDec(par.IfNull(myReader1("impTOT"), "")), "##,##0.00")
                        RIGA.Item("DA_RATA") = MesiProssimaBollett(par.IfNull(myReader1("DA_RATA"), 1))
                        ripartizParz = True
                    Else
                        RIGA.Item("N_RATE") = ""
                        RIGA.Item("IMP_RATA") = ""
                        RIGA.Item("DA_RATA") = ""
                    End If
                    myReader1.Close()
                End If

                Dim imp_coperto As Decimal = 0
                Dim credResiduo As Decimal = 0
                Dim idEventoParziale As Long = 0
                Dim ElencoIDboll As String = ""
                Dim idAnagrafe As Long = 0

                CaricaBollCoperte(ElencoIDboll, imp_coperto, idEventoParziale)

                ''CERCO BOLLETTE PAGATE IN QUELLA DATA
                'par.cmd.CommandText = "SELECT SUM(bol_bollette_voci_pagamenti.importo_pagato) as IMP_COPERTO,bol_bollette.id as id_boll FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.INCASSI_EXTRAMAV " _
                '    & " WHERE BOL_BOLLETTE.ID_CONTRATTO=" & idContr & " AND BOL_BOLLETTE_VOCI_PAGAMENTI.DATA_PAGAMENTO='" & par.IfNull(dtDocGest.Rows(i).Item("DATA_APPLICAZIONE"), "") & "' AND BOL_BOLLETTE_VOCI_PAGAMENTI.ID_INCASSO_EXTRAMAV=INCASSI_EXTRAMAV.ID " _
                '    & " AND INCASSI_EXTRAMAV.ID_TIPO_PAG=11 and NVL (BOL_BOLLETTE.id_rateizzazione, 0) = 0 AND NVL (BOL_BOLLETTE.id_bolletta_ric, 0) = 0 and incassi_extramav.ID in (select id from siscom_mi.incassi_extramav where importo=" & par.VirgoleInPunti(Math.Abs(CDec(RIGA.Item("IMPORTO")))) & ") AND BOL_BOLLETTE_VOCI_PAGAMENTI.ID_VOCE_BOLLETTA=BOL_BOLLETTE_VOCI.ID AND BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID" _
                '    & " GROUP BY bol_bollette.ID"
                'Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                'Dim dtBollPagate As New Data.DataTable
                'da2.Fill(dtBollPagate)
                'da2.Dispose()
                ''******** FAR PASSARE L'ELENCO DEGLI ID_VOCE PIUTTOSTO CHE L'ID_BOLLETTA ********
                'For j As Integer = 0 To dtBollPagate.Rows.Count - 1
                '    imp_coperto = imp_coperto + dtBollPagate.Rows(j).Item("IMP_COPERTO")
                '    ElencoIDboll = ElencoIDboll & dtBollPagate.Rows(j).Item("ID_BOLL") & ","
                'Next

                ''CERCO BOLLETTE PAGATE IN QUELLA DATA RICLASSIFICATE
                'par.cmd.CommandText = "SELECT SUM(bol_bollette_voci_pagamenti2.importo_pagato) as IMP_COPERTO,bol_bollette.id as id_boll FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI2,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.INCASSI_EXTRAMAV " _
                '    & " WHERE BOL_BOLLETTE.ID_CONTRATTO=" & idContr & " AND BOL_BOLLETTE_VOCI_PAGAMENTI2.DATA_PAGAMENTO='" & par.IfNull(dtDocGest.Rows(i).Item("DATA_APPLICAZIONE"), "") & "' AND BOL_BOLLETTE_VOCI_PAGAMENTI2.ID_INCASSO_EXTRAMAV=INCASSI_EXTRAMAV.ID " _
                '    & " AND INCASSI_EXTRAMAV.ID_TIPO_PAG=11 and NVL (BOL_BOLLETTE.id_rateizzazione, 0) = 0 AND NVL (BOL_BOLLETTE.id_bolletta_ric, 0) = 0 and incassi_extramav.ID in (select id from siscom_mi.incassi_extramav where importo=" & par.VirgoleInPunti(Math.Abs(CDec(RIGA.Item("IMPORTO")))) & ") AND BOL_BOLLETTE_VOCI_PAGAMENTI2.ID_VOCE_BOLLETTA=BOL_BOLLETTE_VOCI.ID AND BOL_BOLLETTE_VOCI.ID_BOLLETTA=BOL_BOLLETTE.ID" _
                '    & " GROUP BY bol_bollette.ID"
                'Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                'Dim dtBollPagate3 As New Data.DataTable
                'da3.Fill(dtBollPagate3)
                'da3.Dispose()
                'For k As Integer = 0 To dtBollPagate3.Rows.Count - 1
                '    imp_coperto = imp_coperto + dtBollPagate3.Rows(k).Item("IMP_COPERTO")
                '    ElencoIDboll = ElencoIDboll & dtBollPagate3.Rows(k).Item("ID_BOLL") & ","
                'Next

                'If imp_coperto > Math.Abs(CDec(RIGA.Item("IMPORTO"))) Then
                '    imp_coperto = Math.Abs(CDec(RIGA.Item("IMPORTO")))
                'End If
                If ElencoIDboll <> "" Then
                    ElencoIDboll = "(" & Mid(ElencoIDboll, 1, Len(ElencoIDboll) - 1) & ")"
                End If

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO=" & idContr & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                Dim myReaderInt As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderInt.Read Then
                    idAnagrafe = par.IfNull(myReaderInt("ID_ANAGRAFICA"), 0)
                End If
                myReaderInt.Close()

                Session.Add("IDBollCOP", ElencoIDboll)

                If imp_coperto <> 0 Then
                    RIGA.Item("TOT_IMPORTO") = "<a href=""javascript:window.open('../Contabilita/DettaglioBolletta2.aspx?IDCONT=" & idContr & "&IDANA=" & idAnagrafe & "&IDE=" & idEventoParziale & "','DettBolletta','');void(0);"">" & Format(imp_coperto, "##,##0.00") & "</a>"
                Else
                    RIGA.Item("TOT_IMPORTO") = Format(imp_coperto, "#,##0.00")
                End If

                credResiduo = Math.Abs(par.IfNull(dtDocGest.Rows(i).Item("IMPORTO_TOTALE"), 0)) - imp_coperto
                If IsNumeric(RIGA.Item("ID_BOLLETTA")) Then
                    credResiduo = 0
                End If
                RIGA.Item("CREDITO_RESIDUO") = Format(credResiduo, "##,##0.00")
                'End If
                dtPerDatagr.Rows.Add(RIGA)
                If ripartizParz = False Then
                    Exit For
                End If
            Next
            'Else

            'End If

            DataGrDettaglio.DataSource = dtPerDatagr
            DataGrDettaglio.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaBollCoperte(ByRef ElencoIDboll As String, ByRef impCoperto As Decimal, ByRef idIncassoExtraMav As Long)
        Try
            'Dim idIncassoExtraMav As Long = 0


            par.cmd.CommandText = "SELECT incassi_extramav.ID, id_operatore," _
                & " (SELECT TO_DATE (MIN (data_ora), 'yyyyMMddHH24MISS')" _
                & "  FROM siscom_mi.eventi_pagamenti_parziali" _
                & "  WHERE id_incasso_extramav = incassi_extramav.ID) AS data_ora," _
                & "  sepa.operatori.operatore, sepa.caf_web.cod_caf, " _
                & "  tipo_pag_parz.descrizione AS tipo, motivo_pagamento," _
                & "  TO_CHAR (TO_DATE (incassi_extramav.data_pagamento, 'yyyyMMdd')," _
                & "           'dd/mm/yyyy'" _
                & "          ) AS data_pagamento," _
                & "  TO_CHAR (TO_DATE (incassi_extramav.riferimento_da, 'yyyyMMdd')," _
                & "           'dd/mm/yyyy'" _
                & "         ) AS riferimento_da," _
                & "  TO_CHAR (TO_DATE (incassi_extramav.riferimento_a, 'yyyyMMdd')," _
                & "           'dd/mm/yyyy'" _
                & "          ) AS riferimento_a," _
                & "  TRIM (TO_CHAR (importo, '9G999G999G990D99')) AS importo," _
                & "  fl_annullata" _
                & " FROM siscom_mi.tipo_pag_parz," _
                & "  siscom_mi.incassi_extramav," _
                & "  sepa.caf_web," _
                & "  sepa.operatori," _
                & "  siscom_mi.bol_bollette_gest" _
                & " WHERE siscom_mi.tipo_pag_parz.ID(+) = incassi_extramav.id_tipo_pag" _
                & " AND incassi_extramav.id_contratto = " & idContr & "" _
                & " AND incassi_extramav.id_operatore = sepa.operatori.ID" _
                & " AND sepa.caf_web.ID = sepa.operatori.id_caf" _
                & " AND bol_bollette_gest.id=incassi_extramav.ID_BOLLETTA_GEST " _
                & " AND bol_bollette_gest.id=" & idBoll _
                & " ORDER BY ID DESC"
            Dim daI As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtI As New Data.DataTable
            daI.Fill(dtI)
            daI.Dispose()

            If dtI.Rows.Count > 0 Then


                If dtI.Rows.Count > 0 Then
                    idIncassoExtraMav = par.IfNull(dtI.Rows(0).Item("ID"), 0)
                Else
                    If dtI.Rows(0).Item("MOTIVO_PAGAMENTO") <> "RIPARTIZ. CREDITO DA STORNO" Then
                        idIncassoExtraMav = par.IfNull(dtI.Rows(0).Item("ID"), 0)
                    End If
                End If


                'par.cmd.CommandText = "SELECT   eventi_pagamenti_parziali.ID," _
                '        & " TO_DATE (siscom_mi.eventi_pagamenti_parziali.data_ora," _
                '        & " 'yyyyMMddHH24MISS'" _
                '        & " ) AS data_ora," _
                '        & " cod_evento, siscom_mi.tab_eventi.descrizione AS evento," _
                '        & "  motivazione AS desc_evento," _
                '        & "  CASE" _
                '        & "   WHEN eventi_pagamenti_parziali.importo IS NULL" _
                '        & "   THEN ((SELECT TRIM" _
                '        & "  (TO_CHAR" _
                '        & "   (SUM (eventi_pagamenti_parz_dett.importo)," _
                '        & "   '9G999G999G990D99'" _
                '        & "   )" _
                '        & "   )" _
                '        & "  FROM siscom_mi.eventi_pagamenti_parz_dett," _
                '        & "  siscom_mi.bol_bollette_voci," _
                '        & "  siscom_mi.bol_bollette" _
                '        & "  WHERE id_evento_principale = eventi_pagamenti_parziali.ID" _
                '        & "  AND bol_bollette.ID = bol_bollette_voci.id_bolletta" _
                '        & "   AND bol_bollette_voci.ID = eventi_pagamenti_parz_dett.id_voce_bolletta" _
                '        & "  " _
                '        & "  ))" _
                '        & "  ELSE TRIM (TO_CHAR (eventi_pagamenti_parziali.importo," _
                '        & "   '9G999G999G990D99'" _
                '        & "   )" _
                '        & " )" _
                '        & "  END AS importo_evento" _
                '        & "  FROM siscom_mi.eventi_pagamenti_parziali, siscom_mi.tab_eventi" _
                '        & "  WHERE siscom_mi.eventi_pagamenti_parziali.cod_evento = siscom_mi.tab_eventi.cod" _
                '        & "   AND siscom_mi.eventi_pagamenti_parziali.id_incasso_extramav = " & idIncassoExtraMav & "" _
                '        & " ORDER BY eventi_pagamenti_parziali.data_ora ASC,eventi_pagamenti_parziali.cod_evento ASC"
                'Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader0.Read Then
                '    idEventoParziale = par.IfNull(myReader0("ID"), 0)
                'End If
                'myReader0.Close()

                par.cmd.CommandText = "SELECT bol_bollette.id" _
                    & " FROM siscom_mi.bol_bollette," _
                    & " siscom_mi.bol_bollette_voci," _
                    & " siscom_mi.t_voci_bolletta," _
                    & " siscom_mi.bol_bollette_voci_pagamenti" _
                    & " WHERE bol_bollette_voci_pagamenti.id_voce_bolletta = bol_bollette_voci.ID" _
                    & " AND bol_bollette_voci.id_voce = t_voci_bolletta.ID" _
                    & " AND id_incasso_extramav = " & idIncassoExtraMav & "" _
                    & " AND bol_bollette.ID = bol_bollette_voci.id_bolletta" _
                    & " " _
                    & " " _
                    & " group by bol_bollette.id" _
                    & " order by bol_bollette.id asc"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtDocGest As New Data.DataTable
                da.Fill(dtDocGest)
                da.Dispose()
                For i As Integer = 0 To dtDocGest.Rows.Count - 1
                    ElencoIDboll = ElencoIDboll & dtDocGest.Rows(i).Item("ID") & ","
                Next


                par.cmd.CommandText = "SELECT sum(sum(bol_bollette_voci_pagamenti.importo_pagato)) as imp_coperto" _
                    & " FROM siscom_mi.bol_bollette," _
                    & " siscom_mi.bol_bollette_voci," _
                    & " siscom_mi.t_voci_bolletta," _
                    & " siscom_mi.bol_bollette_voci_pagamenti" _
                    & " WHERE bol_bollette_voci_pagamenti.id_voce_bolletta = bol_bollette_voci.ID" _
                    & " AND bol_bollette_voci.id_voce = t_voci_bolletta.ID" _
                    & " AND id_incasso_extramav = " & idIncassoExtraMav & "" _
                    & " AND bol_bollette.ID = bol_bollette_voci.id_bolletta" _
                    & " and nvl(bol_bollette_voci_pagamenti.fl_no_report,0)=0 " _
                    & " " _
                    & " group by bol_bollette.id"
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    impCoperto = par.IfNull(myReader0("imp_coperto"), 0)
                End If
                myReader0.Close()

            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function MesiProssimaBollett(ByVal NumRata As Integer) As String
        Dim Mese As String = ""

        Select Case NumRata
            Case "1"
                Mese = "Gennaio"
            Case "2"
                Mese = "Febbraio"
            Case "3"
                Mese = "Marzo"
            Case "4"
                Mese = "Aprile"
            Case "5"
                Mese = "Maggio"
            Case "6"
                Mese = "Giugno"
            Case "7"
                Mese = "Luglio"
            Case "8"
                Mese = "Agosto"
            Case "9"
                Mese = "Settembre"
            Case "10"
                Mese = "Ottobre"
            Case "11"
                Mese = "Novembre"
            Case "12"
                Mese = "Dicembre"
        End Select

        Return Mese

    End Function
End Class
