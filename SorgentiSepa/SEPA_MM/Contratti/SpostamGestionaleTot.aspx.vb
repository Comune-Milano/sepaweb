
Partial Class Contratti_SpostamGestionaleTot
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim lstBolliParz As New System.Collections.Generic.List(Of String)

    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("par_vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnessione") = value
        End Set

    End Property
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False Then
            idContratto = Request.QueryString("IDCONTR")
            idBolletta = Request.QueryString("IDBOLL")
            vIdConnessione = Format(Now, "yyyyMMddHHmmss")
            listaBollette = Request.QueryString("LBOLL")

            If Request.QueryString("TIPO") = "DEB" Then
                ElaborazioneTotDebito(idContratto, idBolletta)
            Else
                'If Session("MOD_NEW_ELAB_GEST") = "1" Then
                    ElaborazioneTotCreditoNUOVA(idContratto, idBolletta)
                ' Else
                '    ElaborazioneTotCredito2(idContratto, idBolletta)
                'End If
                End If
            End If
    End Sub

    Public Property listaBollette() As String
        Get
            If Not (ViewState("par_listaBollette") Is Nothing) Then
                Return CStr(ViewState("par_listaBollette"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_listaBollette") = value
        End Set

    End Property

    Public Property idTipoPagamento() As Long
        Get
            If Not (ViewState("par_idTipoPagamento") Is Nothing) Then
                Return CLng(ViewState("par_idTipoPagamento"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idTipoPagamento") = value
        End Set

    End Property

    Public Property idContratto() As Long
        Get
            If Not (ViewState("par_idContratto") Is Nothing) Then
                Return CLng(ViewState("par_idContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idContratto") = value
        End Set

    End Property

    Public Property impBollo() As Decimal
        Get
            If Not (ViewState("par_impBollo") Is Nothing) Then
                Return CLng(ViewState("par_impBollo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_impBollo") = value
        End Set

    End Property

    Public Property idEventoPrincipale() As Long
        Get
            If Not (ViewState("par_idEventoPrincipale") Is Nothing) Then
                Return CLng(ViewState("par_idEventoPrincipale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEventoPrincipale") = value
        End Set

    End Property

    Public Property idIncasso() As Long
        Get
            If Not (ViewState("par_idIncasso") Is Nothing) Then
                Return CLng(ViewState("par_idIncasso"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idIncasso") = value
        End Set

    End Property

    Public Property idBolletta() As Long
        Get
            If Not (ViewState("par_idBolletta") Is Nothing) Then
                Return CLng(ViewState("par_idBolletta"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idBolletta") = value
        End Set

    End Property

    Private Sub CreaNuovaEmissione()
        Try
            Response.Write("<script>alert('Nuova emissione! IDBOLL:" & idBolletta & " IDCONTR:" & idContratto & "')</script>")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ElaborazioneTotCredito(ByVal idContr As Long, ByVal idBollGest As Long)
        Try
            'BISOGNA PRIMA PROCEDERE AL CONTROLLO DELLE MOROSITA
            'EMETTERE BOLLETTI RISULTANTI GIA' INCASSATI DALLA QUOTA DI CREDITO
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            Dim importoCredito As Decimal = 0
            Dim importoMorosita As Decimal = 0
            Dim diffCreditoMoros As Decimal = 0

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & idBollGest
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                importoCredito = par.IfNull(myReader0("IMPORTO_TOTALE"), 0)
            End If
            myReader0.Close()

            par.cmd.CommandText = "select distinct(bol_bollette.id),bol_bollette.*,bol_bollette_voci.* from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci where bol_bollette.id=bol_bollette_voci.id_bolletta and id_contratto=" & idContr & " and data_scadenza<='" & Format(Now, "yyyyMMdd") & "' and (importo_pagato is null or importo_pagato=0 or importo_totale>importo_pagato) and nvl(IMPORTO_RUOLO, 0) = 0 /*and fl_sollecito=1*/ order by data_emissione asc"
            Dim daMoros As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtMoros As New Data.DataTable
            daMoros = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daMoros.Fill(dtMoros)
            daMoros.Dispose()
            If dtMoros.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtMoros.Rows

                    'PROCEDO ALLA COPERTURA DELLE BOLLETTE SCADUTE E SOLLECITATE
                    If par.IfNull(row.Item("IMPORTO"), 0) > par.IfNull(row.Item("IMP_PAGATO"), 0) Then
                        importoMorosita = par.IfNull(row.Item("IMPORTO"), 0) - par.IfNull(row.Item("IMP_PAGATO"), 0)
                        If importoCredito <> 0 Then
                            diffCreditoMoros = importoMorosita - importoCredito

                            If diffCreditoMoros >= 0 Then
                                'ATTRAVERSO IL CREDITO, INCASSO LA PRIMA BOLLETTA (scaduta e sollecitata)
                                par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(importoCredito) & " where id=" & row.Item("ID")
                                par.cmd.ExecuteNonQuery()

                                importoCredito = 0
                            Else
                                par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(importoMorosita) & " where id=" & row.Item("ID")
                                par.cmd.ExecuteNonQuery()

                                importoCredito = importoCredito - importoMorosita
                            End If

                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                            & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'," _
                            & " WHERE ID=" & row.Item("ID")
                            par.cmd.ExecuteNonQuery()
                        Else
                            Exit For
                        End If
                    End If
                Next
            End If

            Dim strScelta As String = ""

            If importoCredito > 0 Then
                'PROCEDURA PER RESTITUIRE IL CREDITO NELLE PROSSIME BOLLETTE
                strScelta = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK per visualizzare la dichiarazione.');if (conf){window.open('SpostamGestionaleTot.aspx?IDBOLL=' " + idBollGest + "'&IDCONTR=' " + idContr + " '&TIPO=CRED');}" _
                & "else{window.close();}</script>"
                Response.Write(strScelta)

                'RestituzCreditoInBoll(idBollGest, idContr)
            End If

            'AGGIORNO IL DOCUMENTO COME CONTABILE E TIPO APPLICAZIONE = 1 (: spostamento parziale)
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET FL_CONTABILE=1 AND TIPO_APPLICAZIONE=2 WHERE ID=" & idBollGest
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Public Function WriteEvent(ByVal TipoPadre As Boolean, ByVal ID_VOCE As String, ByVal CodEvento As String, ByVal Importo As Decimal, ByVal DataPagamento As String, ByVal idEvPadre As String, ByVal vIdIncassoExtramav As Long, ByVal idContratto As Long, Optional ByVal Motivazione As String = "", Optional ByVal idMain As String = "") As String
        Dim idPadre As String = "null"
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            If TipoPadre = True Then

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_EVENTI_PAGAMENTI_PARZIALI.NEXTVAL FROM DUAL"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    idPadre = lettore(0)
                End If
                lettore.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI (ID,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_CONTRATTO,ID_MAIN,ID_INCASSO_EXTRAMAV,IMPORTO) " _
                & "VALUES ( " & idPadre & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & CodEvento & "','" & Motivazione & "'," & idContratto & "," & par.IfEmpty(idMain, "NULL") & "," & vIdIncassoExtramav & "," & par.VirgoleInPunti(par.IfEmpty(Importo, 0)) & ")"

                'End If
                par.cmd.ExecuteNonQuery()
            Else
                'evento figlio
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT (ID,ID_EVENTO_PRINCIPALE,ID_VOCE_BOLLETTA,IMPORTO) " _
                                    & "VALUES ( SISCOM_MI.SEQ_EVENTI_PAGAMENTI_PARZ_DETT.NEXTVAL," & idEvPadre & "," & ID_VOCE & ", " _
                                    & " " & par.VirgoleInPunti(par.IfEmpty(Importo, 0)) & ")"
                par.cmd.ExecuteNonQuery()
            End If

            'If ConnOpenNow = True Then
            '    '*********************CHIUSURA CONNESSIONE**********************
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            'End If

            Return idPadre

        Catch ex As Exception

            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Rollback()
                End If

                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteEvent" & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try

    End Function

    Private Function isPagabileBollo(ByVal idVoce As Integer, ByVal impPagamento As Decimal, ByRef resBollo As Decimal) As Boolean
        isPagabileBollo = True
        'Dim resBollo As Decimal = 0
        par.cmd.CommandText = "select (nvl(importo,0)-nvl(imp_pagato,0)) as resBollo from siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette_voci where t_voci_bolletta.id = bol_bollette_voci.id_voce and t_voci_bolletta.gruppo = 7 and bol_bollette_voci.id =" & idVoce
        resBollo = par.IfNull(par.cmd.ExecuteScalar, 0)
        If resBollo <> 0 Then
            If impPagamento < resBollo Then
                lstBolliParz.Add(idVoce)
                isPagabileBollo = False
                Me.BolloPagParz.Value = 1
            Else
                lstBolliParz.Remove(idVoce)
            End If
        Else
            lstBolliParz.Remove(idVoce)
        End If


    End Function

    Private Sub ElaborazioneTotCredito2(ByVal idContr As Long, ByVal idBollGest As Long)
        Try
            'BISOGNA PRIMA PROCEDERE AL CONTROLLO DELLE MOROSITA
            'EMETTERE BOLLETTI RISULTANTI GIA' INCASSATI DALLA QUOTA DI CREDITO
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNGEST" & vIdConnessione, par.OracleConn) 'MEMORIZZAZIONE CONNESSIONE IN VARIABILE DI SESSIONE
            End If

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)
            '‘par.cmd.Transaction = par.myTrans

            Dim importoCreditoIniziale As Decimal = 0
            Dim importoCredito As Decimal = 0
            Dim importoCreditoRateizz As Decimal = 0
            Dim importoMorosita As Decimal = 0
            Dim diffCreditoMoros As Decimal = 0

            'Dim idIncasso As Long = 0
            Dim impNuovoPAGATO As Decimal = 0
            Dim idTipoBoll As Long = 0
            Dim tipoPagParz As Long = 0
            Dim statoContratto As String = ""

            Dim TotBollettePagabile As Decimal = 0
            Dim ripartizione As Boolean = False
            Dim nMesi As Integer = 0
            'par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO,0)-NVL(IMP_PAGATO,0)) AS TOT " _
            '                    & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
            '                    & "WHERE FL_ANNULLATA = '0' " _
            '                    & "AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) AND IMPORTO_TOTALE > 0 AND " _
            '                    & "ID_BOLLETTA = BOL_BOLLETTE.ID and data_scadenza<='" & Format(Now, "yyyyMMdd") & "' " _
            '                    & "AND ID_VOCE = T_VOCI_BOLLETTA.ID AND ID_RATEIZZAZIONE IS NULL " _
            '                    & "AND BOL_BOLLETTE.ID_CONTRATTO = " & idContr & " " _
            '                    & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL "
            'Dim lettoreT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If lettoreT.Read Then
            '    TotBollettePagabile = Format(par.IfNull(lettoreT("TOT"), 0), "##,##0.00")
            'End If
            'lettoreT.Close()
            TotBollettePagabile = par.CalcolaSaldoAttuale(idContr)


            par.cmd.CommandText = "SELECT SISCOM_MI.GETSTATOCONTRATTO(ID) as statoContr FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContr & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                statoContratto = par.IfNull(myReader0("statoContr"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=43"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                nMesi = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()


            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE BOL_BOLLETTE_GEST.ID=BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST AND BOL_BOLLETTE_GEST.ID=" & idBollGest
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID=" & idBollGest
            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci As New Data.DataTable
            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci.Fill(dtVoci)
            daVoci.Dispose()
            If dtVoci.Rows.Count > 0 Then
                For Each row0 As Data.DataRow In dtVoci.Rows
                    importoCreditoIniziale = Math.Abs(par.IfNull(row0.Item("IMPORTO_TOTALE"), 0))
                    importoCredito = Math.Abs(par.IfNull(row0.Item("IMPORTO_TOTALE"), 0))
                    'RIF mail amisano 25/08/2017...perchè l'intero importo a credito, viene messo nella variabile per pagare le bollette riclassificate in rateizzazione?
                    ' Le riclassificate possono essere pagato solo per la parte di quota capitali.
                    importoCreditoRateizz = importoCreditoIniziale

                    idTipoBoll = par.IfNull(row0.Item("ID_TIPO"), 0)

                    If TotBollettePagabile <> 0 Then

                        If TotBollettePagabile < importoCreditoIniziale Then
                            'importoCreditoIniziale = TotBollettePagabile
                            'importoCredito = TotBollettePagabile
                        End If
                    End If

                    If idTipoBoll = 4 Then
                        tipoPagParz = 12
                    Else
                        tipoPagParz = 11
                    End If

                    Dim noteEvento As String = ""

                    Select Case idTipoBoll
                        Case 50, 51, 52
                            idTipoPagamento = 5
                            noteEvento = "C/C 59"
                        Case 53
                            idTipoPagamento = 6
                            noteEvento = "C/C 60"
                        Case Else
                            idTipoPagamento = 4
                    End Select

                    Dim condizioneBoll As String = ""
                    If listaBollette <> "" Then
                        condizioneBoll = " AND BOL_BOLLETTE.ID IN (" & listaBollette & ") AND bol_bollette_voci.id_voce not in (126,182) "
                    End If

                    Dim condizioneFacility As String = ""
                    If listaBollette = "" Then
                        condizioneFacility = " AND siscom_mi.bol_bollette.id NOT IN (select id_bolletta from siscom_mi.bol_bollette_voci where id_voce in (126,182)) "
                    End If

                    '16/07/2015 Eliminata condizione che esclude le quote sindacali come richiesto dal comune
                    par.cmd.CommandText = "select bol_bollette.id,bol_bollette.*,bol_bollette_voci.*, (case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end) as ordine,BOL_BOLLETTE_VOCI.ID AS ID_VOCE1,priorita,(CASE WHEN NVL(ID_TIPO,0)=4 OR NVL(ID_TIPO,0)=5 THEN '1' ELSE '0' END) AS RICLASS from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci,siscom_mi.T_VOCI_BOLLETTA where bol_bollette.id=bol_bollette_voci.id_bolletta and id_contratto=" & idContr & " and ADD_MONTHS(TO_DATE (bol_bollette.data_scadenza, 'yyyyMMdd'), " & nMesi & ") <= TO_DATE ('" & Format(Now, "yyyyMMdd") & "', 'yyyyMMdd') and abs(importo)>abs(nvl(imp_pagato,0)) and abs(importo)>0 and fl_annullata=0 and id_tipo<> 22 and " _
                        & "NVL (id_rateizzazione, 0) = 0 AND NVL (id_bolletta_ric, 0) = 0 " & condizioneBoll & condizioneFacility & " AND BOL_BOLLETTE_VOCI.id_voce = T_VOCI_BOLLETTA.ID(+) AND nvl(IMPORTO_RUOLO,0) = 0 " _
                        & " /*AND bol_bollette.id NOT IN (SELECT id_bolletta FROM siscom_mi.bol_bollette_voci bbv,siscom_mi.bol_bollette bb WHERE bbv.id_bolletta = bb.id and bb.id_contratto=bol_bollette.id_contratto AND bbv.id_voce IN (126, 182)) Condizione per escludere le bollette con voce di Compenso Facility (SD 164/2017)*/" _
                        & "order by " _
                        & "riclass ASC,data_scadenza asc,data_emissione asc " _
                        & " ,BOL_BOLLETTE_VOCI.id_bolletta ASC " _
                        & " ,ordine ASC"
                    Dim daMoros1 As Oracle.DataAccess.Client.OracleDataAdapter
                    Dim dtMoros1 As New Data.DataTable
                    daMoros1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    daMoros1.Fill(dtMoros1)
                    daMoros1.Dispose()
                    If dtMoros1.Rows.Count > 0 Then
                        ripartizione = True
                        '(0)*** AGGIORNO LA VOCE degli INCASSI EXTRAMAV ***
                        par.cmd.CommandText = "INSERT INTO siscom_mi.incassi_extramav (ID, id_tipo_pag, motivo_pagamento, id_contratto,data_pagamento, riferimento_da, riferimento_a, fl_annullata,importo, id_operatore,id_bolletta_gest,data_ora)" _
                            & "VALUES (siscom_mi.seq_incassi_extramav.NEXTVAL," & tipoPagParz & ",'RIPARTIZ. CREDITO GESTIONALE " & noteEvento & "'," & idContr & ",'" & Format(Now, "yyyyMMdd") & "','','',0," & par.VirgoleInPunti(importoCredito) & "," & Session.Item("ID_OPERATORE") & "," & idBollGest & ",'" & Format(Now, "yyyyMMddHHmmss") & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select siscom_mi.seq_incassi_extramav.currval from dual"
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            idIncasso = par.IfNull(lettore(0), "")
                        End If
                        lettore.Close()

                        'idEventoPrincipale = WriteEvent(True, "null", "F205", importoCredito, Format(Now, "dd/MM/yyyy"), "null", idIncasso, idContr, "COPERTURA VOCI BOLLETTA DA CREDITO GESTIONALE " & noteEvento & "")

                        For Each row1 As Data.DataRow In dtMoros1.Rows
                            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "' AND ID=" & row1.Item("ID_BOLLETTA")
                            'lettore = par.cmd.ExecuteReader
                            'If Not lettore.Read Then
                            If importoCredito <> 0 Then
                                importoCreditoRateizz = importoCredito
                                'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                '            & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1" _
                                '            & " WHERE ID=" & row1.Item("ID_BOLLETTA")
                                'par.cmd.ExecuteNonQuery()


                                'RIF mail amisano 25/08/2017 a cosa serve questo blocco di codice??? non sono ancora state pagate le rate...
                                If row1.Item("ID_TIPO") = "5" Then
                                    If importoCreditoRateizz <> 0 Then
                                        'RIF mail amisano 25/08/2017 la funzione successiva, quando si paga una rata, non ripartisce nulla sulle riclassificate.
                                        ' Se in alcuni casi ha ripartito è perchè una rata era parzialmente pagata quindi ha pagato nuovamente delle riclassificate
                                        ' senza verificare l'importo per cui la rata è stata pagata dall'incasso in corso!

                                        'RIF mail amisano 25/08/2017 La chiamata alla funzione PagaBolRateizzazione deve avvenire solo per le quote capitali o gli anticipi dei piani di rientro!
                                        'PagaBolRateizzazione(row1.Item("ID_BOLLETTA"), importoCreditoIniziale)
                                        If row1.Item("ID_VOCE") = "676" Or row1.Item("ID_VOCE") = "677" Then
                                            PagaBolRateizzazione(row1.Item("ID_BOLLETTA"), importoCreditoRateizz)
                                        End If

                                    End If
                                End If
                            End If
                            '        End If
                            'lettore.Close()

                            If row1.Item("ID_TIPO") <> "4" Then
                                importoMorosita = par.IfNull(row1.Item("IMPORTO"), 0) - par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                If importoCredito <> 0 Then

                                    diffCreditoMoros = importoMorosita - Math.Abs(importoCredito)

                                    If diffCreditoMoros >= 0 Then
                                        If isPagabileBollo(row1.Item("ID_VOCE1").ToString, importoCredito, impBollo) = False Then
                                            'salta il pagamento e va alla voce successiva
                                            Continue For
                                        End If

                                        '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO in BOL_BOLLETTE_VOCI_PAGAM ***
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                        & "(" & row1.Item("ID_VOCE1") & ",'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(Math.Round(importoCredito, 2)) & "," & idTipoPagamento & "," & idIncasso & ")"
                                        par.cmd.ExecuteNonQuery()

                                        'WriteEvent(False, row1.Item("ID_VOCE1"), "F205", importoCredito, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContr, "")

                                        importoCredito = importoCredito + par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                        'ATTRAVERSO IL CREDITO, INIZIO AD INCASSARE LE BOLLETTE (scaduta e sollecitata) PARTENDO DALLE PIù VECCHIE

                                        '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(importoCredito) & " where id=" & row1.Item("ID_VOCE1")
                                        par.cmd.ExecuteNonQuery()

                                        lstBolliParz.Remove(row1.Item("ID_VOCE1"))


                                        importoCredito = 0


                                    Else
                                        impNuovoPAGATO = importoMorosita + par.IfNull(row1.Item("IMP_PAGATO"), 0)

                                        If isPagabileBollo(row1.Item("ID_VOCE1").ToString, impNuovoPAGATO, impBollo) = False Then
                                            'salta il pagamento e va alla voce successiva
                                            Continue For
                                        End If

                                        '(1)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(impNuovoPAGATO) & " where id=" & row1.Item("ID_VOCE1")
                                        par.cmd.ExecuteNonQuery()

                                        lstBolliParz.Remove(row1.Item("ID_VOCE1"))

                                        '(2)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                        If importoMorosita <> 0 Then
                                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                            & "(" & row1.Item("ID_VOCE1") & ",'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(Math.Round(importoMorosita, 2)) & "," & idTipoPagamento & "," & idIncasso & ")"
                                            par.cmd.ExecuteNonQuery()
                                        End If


                                        'SEGNALAZIONE 476/2016 NON POSSO PAGARE I BOLLI PARZIALMENTE
                                        importoCredito = Math.Abs(importoCredito) - importoMorosita

                                    End If

                                Else
                                    Exit For
                                End If
                            Else
                                'NEL CASO DI BOLLETTE RICLASSIFICATE (MOROSITA'/RATEIZZAZIONE)
                                If importoCredito <> 0 Then
                                    If row1.Item("ID_TIPO") = "4" Then
                                        PagaVociBolRiclass(row1.Item("ID_BOLLETTA"), importoCredito)
                                    Else
                                        'PagaBolRateizzazione(row1.Item("ID_BOLLETTA"), importoCredito)
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                                & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1" _
                                                & " WHERE ID=" & row1.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()

                        Next
                    End If
                Next
            End If

            If idBollGest <> 0 Then
                ValorizzColonneImporti(idBollGest)
            End If

            Dim errScriveInSchema As Boolean = False

            Dim importoCreditoRest As Decimal = 0
            'importoCredito = TotBollettePagabile
            'If ripartizione = True Then
            If ripartizione = False Then
                importoCredito = importoCreditoRateizz
            End If
            importoCredito = Math.Round(importoCredito, 2)
            If BolloPagParz.Value = "1" And importoCreditoIniziale < impBollo Then
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)
                Response.Write("<script>alert('Ripartizione interrotta per pagamento parziale delle voce bollo!');self.close();</script>")
                Exit Sub
            End If


            If ripartizione = True Then
                If importoCredito > 0 Then
                    'If Math.Abs(importoCredito) > 0 Then

                    'importoCreditoIniziale = importoCreditoRateizz
                    importoCreditoRest = importoCredito
                    If ripartizione = False Then
                        importoCreditoRateizz = 0
                    End If
                    If statoContratto = "CHIUSO" Or listaBollette <> "" Then
                        CreditoResidInGest(idBollGest, importoCreditoRest, idContr)
                    Else
                        RestituzCreditoInBoll(idBollGest, importoCreditoRest, idContr, errScriveInSchema)
                        If errScriveInSchema = True Then
                            par.myTrans.Rollback()
                            par.myTrans = par.OracleConn.BeginTransaction()
                            HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)
                            Response.Write("<script>self.close();if (typeof opener.opener.opener != 'undefined'){opener.close();}</script>")
                            Exit Sub
                        End If
                    End If

                    If conferma0.Value = "1" Then

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F207','IMPORTO CREDITO INIZIALE EURO " & par.VirgoleInPunti(importoCreditoIniziale * -1) & " / COMPENSATO EURO " & par.VirgoleInPunti((importoCreditoIniziale - importoCreditoRest) * -1) & " / ECCEDENZA EURO " & par.VirgoleInPunti((importoCreditoRest) * (-1)) & "')"
                        par.cmd.ExecuteNonQuery()

                        If statoContratto = "CHIUSO" Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                            par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='P' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                            par.cmd.ExecuteNonQuery()
                        End If

                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey123", "aggiornaRU();", True)

                        '*******************RICHIAMO LA CONNESSIONE*********************
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        '*******************RICHIAMO LA TRANSAZIONE*********************
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        '‘par.cmd.Transaction = par.myTrans
                        If Not IsNothing(par.myTrans) Then
                            par.myTrans.Commit()
                        End If
                        par.OracleConn.Close()
                        par.OracleConn.Dispose()

                        '''Response.Write("<script>self.close();opener.document.getElementById('imgSalva').click();</script>")
                    Else
                        If importoCreditoIniziale = importoCreditoRest Then
                            'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='P' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                            'par.cmd.ExecuteNonQuery()
                        Else
                            'IL RESTO DEL CREDITO E' STATO UTILIZZATO ATTRAVERSO LA BOLLETTE PAGATE
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F02','COPERTURA BOLLETTE SCADUTE/SOLLECITATE PER UN TOTALE DI " & ((importoCreditoIniziale - importoCreditoRest)) & "')"
                            par.cmd.ExecuteNonQuery()

                        End If
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey123", "aggiornaRU();", True)

                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        '*******************RICHIAMO LA TRANSAZIONE*********************
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        '‘par.cmd.Transaction = par.myTrans
                        If Not IsNothing(par.myTrans) Then
                            par.myTrans.Commit()
                        End If
                        par.OracleConn.Close()
                        par.OracleConn.Dispose()

                    End If
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F206','IMPORTO CREDITO INIZIALE EURO " & par.VirgoleInPunti(importoCreditoIniziale * -1) & " / COMPENSATO EURO " & par.VirgoleInPunti((importoCreditoIniziale - importoCreditoRest) * -1) & " / ECCEDENZA EURO " & par.VirgoleInPunti((importoCreditoRest) * (-1)) & "')"
                    par.cmd.ExecuteNonQuery()

                    'AGGIORNO IL DOCUMENTO COME CONTABILE E TIPO APPLICAZIONE = 1 (: spostamento parziale)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                    par.cmd.ExecuteNonQuery()

                    'Response.Write("<script>self.close();if (typeof opener.opener.opener != 'undefined') {opener.opener.document.getElementById('imgSalva').click();opener.close();} else {opener.document.getElementById('imgSalva').click();}</script>")
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey123", "aggiornaRU();", True)


                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans
                    If Not IsNothing(par.myTrans) Then
                        par.myTrans.Commit()
                    End If
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                End If

            Else
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)
                Response.Write("<script>alert('Credito non utilizzato! Nessuna bolletta disponibile per la ripartizione.');self.close();</script>")
                Exit Sub
            End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub



    Private Sub ElaborazioneTotCreditoNUOVA(ByVal idContr As Long, ByVal idBollGest As Long)
        Try
            'BISOGNA PRIMA PROCEDERE AL CONTROLLO DELLE MOROSITA
            'EMETTERE BOLLETTI RISULTANTI GIA' INCASSATI DALLA QUOTA DI CREDITO
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNGEST" & vIdConnessione, par.OracleConn) 'MEMORIZZAZIONE CONNESSIONE IN VARIABILE DI SESSIONE
            End If

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)
            '‘par.cmd.Transaction = par.myTrans

            Dim importoCreditoIniziale As Decimal = 0
            Dim importoCredito As Decimal = 0
            Dim importoCreditoRateizz As Decimal = 0
            Dim importoMorosita As Decimal = 0
            Dim diffCreditoMoros As Decimal = 0

            'Dim idIncasso As Long = 0
            Dim impNuovoPAGATO As Decimal = 0
            Dim idTipoBoll As Long = 0
            Dim tipoPagParz As Long = 0
            Dim statoContratto As String = ""

            Dim TotBollettePagabile As Decimal = 0
            Dim ripartizione As Boolean = False
            Dim nMesi As Integer = 0
            'par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO,0)-NVL(IMP_PAGATO,0)) AS TOT " _
            '                    & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
            '                    & "WHERE FL_ANNULLATA = '0' " _
            '                    & "AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) AND IMPORTO_TOTALE > 0 AND " _
            '                    & "ID_BOLLETTA = BOL_BOLLETTE.ID and data_scadenza<='" & Format(Now, "yyyyMMdd") & "' " _
            '                    & "AND ID_VOCE = T_VOCI_BOLLETTA.ID AND ID_RATEIZZAZIONE IS NULL " _
            '                    & "AND BOL_BOLLETTE.ID_CONTRATTO = " & idContr & " " _
            '                    & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL "
            'Dim lettoreT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If lettoreT.Read Then
            '    TotBollettePagabile = Format(par.IfNull(lettoreT("TOT"), 0), "##,##0.00")
            'End If
            'lettoreT.Close()
            TotBollettePagabile = par.CalcolaSaldoAttuale(idContr)


            par.cmd.CommandText = "SELECT SISCOM_MI.GETSTATOCONTRATTO(ID) as statoContr FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContr & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                statoContratto = par.IfNull(myReader0("statoContr"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=43"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                nMesi = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()
						
			Dim condizioneSql As String = ""
           
           


            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE BOL_BOLLETTE_GEST.ID=BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST AND BOL_BOLLETTE_GEST.ID=" & idBollGest
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID=" & idBollGest
            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci As New Data.DataTable
            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci.Fill(dtVoci)
            daVoci.Dispose()
            If dtVoci.Rows.Count > 0 Then
                For Each row0 As Data.DataRow In dtVoci.Rows
                    importoCreditoIniziale = Math.Abs(par.IfNull(row0.Item("IMPORTO_TOTALE"), 0))
                    importoCredito = Math.Abs(par.IfNull(row0.Item("IMPORTO_TOTALE"), 0))
                    importoCreditoRateizz = importoCreditoIniziale
                    idTipoBoll = par.IfNull(row0.Item("ID_TIPO"), 0)

                    If TotBollettePagabile <> 0 Then

                        If TotBollettePagabile < importoCreditoIniziale Then
                            'importoCreditoIniziale = TotBollettePagabile
                            'importoCredito = TotBollettePagabile
                        End If
                    End If

                    If idTipoBoll = 4 Then
                        tipoPagParz = 12
                    Else
                        tipoPagParz = 11
                    End If

                    Dim noteEvento As String = ""

                    Select Case idTipoBoll
                        Case 50, 51, 52
                            idTipoPagamento = 5
                            noteEvento = "C/C 59"
                        Case 53
                            idTipoPagamento = 6
                            noteEvento = "C/C 60"
                        Case Else
                            idTipoPagamento = 4
                    End Select

                    Dim condizioneBoll As String = ""
                    If listaBollette <> "" Then
                        condizioneBoll = " AND BOL_BOLLETTE.ID IN (" & listaBollette & ") AND bol_bollette_voci.id_voce not in (126, 182) "
						 If Session.Item("FL_FORZA_SCADENZA") = 0 Then
							condizioneSql = " and ADD_MONTHS(TO_DATE (bol_bollette.data_scadenza, 'yyyyMMdd'), " & nMesi & ") <= TO_DATE ('" & Format(Now, "yyyyMMdd") & "', 'yyyyMMdd') "
						End If
					else
						condizioneSql = " and ADD_MONTHS(TO_DATE (bol_bollette.data_scadenza, 'yyyyMMdd'), " & nMesi & ") <= TO_DATE ('" & Format(Now, "yyyyMMdd") & "', 'yyyyMMdd') "
                    End If

                    Dim condizioneFacility As String = ""
                    If listaBollette = "" Then
                        condizioneFacility = " AND siscom_mi.bol_bollette.id NOT IN (select id_bolletta from siscom_mi.bol_bollette_voci where id_voce in (126,182)) "
                    End If

                    'AND nvl(IMPORTO_RUOLO,0) = 12' errore PERCHè FISSO A 12???
                    '16/07/2015 Eliminata condizione che esclude le quote sindacali come richiesto dal comune
                    par.cmd.CommandText = "select bol_bollette.id,bol_bollette.*,bol_bollette_voci.*, (case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end) as ordine,BOL_BOLLETTE_VOCI.ID AS ID_VOCE1,priorita,(CASE WHEN NVL(ID_TIPO,0)=4 OR NVL(ID_TIPO,0)=5 THEN '1' ELSE '0' END) AS RICLASS " _
						& " from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci,siscom_mi.T_VOCI_BOLLETTA where bol_bollette.id=bol_bollette_voci.id_bolletta and id_contratto=" & idContr & " " _
						& condizioneSql _
						& " and abs(importo)>abs(nvl(imp_pagato,0)) and abs(importo)>0 and fl_annullata=0 and id_tipo<> 22 and " _
                        & "NVL (id_rateizzazione, 0) = 0 AND NVL (id_bolletta_ric, 0) = 0 " & condizioneBoll & condizioneFacility & " AND BOL_BOLLETTE_VOCI.id_voce = T_VOCI_BOLLETTA.ID(+) AND nvl(IMPORTO_RUOLO,0) = 0 " _
                        & "order by " _
                        & "riclass ASC,data_scadenza asc,data_emissione asc " _
                        & " ,BOL_BOLLETTE_VOCI.id_bolletta ASC " _
                        & " ,ordine ASC"
                    Dim daMoros1 As Oracle.DataAccess.Client.OracleDataAdapter
                    Dim dtMoros1 As New Data.DataTable
                    daMoros1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    daMoros1.Fill(dtMoros1)
                    daMoros1.Dispose()
                    If dtMoros1.Rows.Count > 0 Then
                        ripartizione = True
                        '(0)*** AGGIORNO LA VOCE degli INCASSI EXTRAMAV ***
                        par.cmd.CommandText = "INSERT INTO siscom_mi.incassi_extramav (ID, id_tipo_pag, motivo_pagamento, id_contratto,data_pagamento, riferimento_da, riferimento_a, fl_annullata,importo, id_operatore,id_bolletta_gest,data_ora)" _
                            & "VALUES (siscom_mi.seq_incassi_extramav.NEXTVAL," & tipoPagParz & ",'RIPARTIZ. CREDITO GESTIONALE " & noteEvento & "'," & idContr & ",'" & Format(Now, "yyyyMMdd") & "','','',0," & par.VirgoleInPunti(importoCredito) & "," & Session.Item("ID_OPERATORE") & "," & idBollGest & ",'" & Format(Now, "yyyyMMddHHmmss") & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select siscom_mi.seq_incassi_extramav.currval from dual"
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            idIncasso = par.IfNull(lettore(0), "")
                        End If
                        lettore.Close()

                        'idEventoPrincipale = WriteEvent(True, "null", "F205", importoCredito, Format(Now, "dd/MM/yyyy"), "null", idIncasso, idContr, "COPERTURA VOCI BOLLETTA DA CREDITO GESTIONALE " & noteEvento & "")

                        For Each row1 As Data.DataRow In dtMoros1.Rows

                            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "' AND ID=" & row1.Item("ID_BOLLETTA")
                            'lettore = par.cmd.ExecuteReader
                            'If Not lettore.Read Then
                            If importoCredito <> 0 Then
                                importoCreditoRateizz = importoCredito
                                'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                '            & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1" _
                                '            & " WHERE ID=" & row1.Item("ID_BOLLETTA")
                                'par.cmd.ExecuteNonQuery()


                                'RIF mail amisano 25/08/2017 a cosa serve questo blocco di codice??? non sono ancora state pagate le rate...
                                If row1.Item("ID_TIPO") = "5" Then
                                    If importoCreditoRateizz <> 0 Then
                                        'RIF mail amisano 25/08/2017 la funzione successiva, quando si paga una rata, non ripartisce nulla sulle riclassificate.
                                        ' Se in alcuni casi ha ripartito è perchè una rata era parzialmente pagata quindi ha pagato nuovamente delle riclassificate
                                        ' senza verificare l'importo per cui la rata è stata pagata dall'incasso in corso!

                                        'RIF mail amisano 25/08/2017 La chiamata alla funzione PagaBolRateizzazione deve avvenire solo per le quote capitali o gli anticipi dei piani di rientro!
                                        'PagaBolRateizzazione(row1.Item("ID_BOLLETTA"), importoCreditoIniziale)
                                        If row1.Item("ID_VOCE") = "676" Or row1.Item("ID_VOCE") = "677" Then
                                            PagaBolRateizzazione(row1.Item("ID_BOLLETTA"), importoCreditoRateizz)
                                        End If

                                    End If
                                End If
                            End If
                            '        End If
                            'lettore.Close()

                            If row1.Item("ID_TIPO") <> "4" Then
                                importoMorosita = par.IfNull(row1.Item("IMPORTO"), 0) - par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                If importoCredito <> 0 Then

                                    diffCreditoMoros = importoMorosita - Math.Abs(importoCredito)

                                    If diffCreditoMoros >= 0 Then
                                        If isPagabileBollo(row1.Item("ID_VOCE1").ToString, importoCredito, impBollo) = False Then
                                            'salta il pagamento e va alla voce successiva
                                            Continue For
                                        End If

                                        '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO in BOL_BOLLETTE_VOCI_PAGAM ***
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                        & "(" & row1.Item("ID_VOCE1") & ",'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(Math.Round(importoCredito, 2)) & "," & idTipoPagamento & "," & idIncasso & ")"
                                        par.cmd.ExecuteNonQuery()

                                        'WriteEvent(False, row1.Item("ID_VOCE1"), "F205", importoCredito, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContr, "")

                                        importoCredito = importoCredito + par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                        'ATTRAVERSO IL CREDITO, INIZIO AD INCASSARE LE BOLLETTE (scaduta e sollecitata) PARTENDO DALLE PIù VECCHIE

                                        '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(importoCredito) & " where id=" & row1.Item("ID_VOCE1")
                                        par.cmd.ExecuteNonQuery()

                                        importoCredito = 0


                                    Else
                                        impNuovoPAGATO = importoMorosita + par.IfNull(row1.Item("IMP_PAGATO"), 0)

                                        If isPagabileBollo(row1.Item("ID_VOCE1").ToString, impNuovoPAGATO, impBollo) = False Then
                                            'salta il pagamento e va alla voce successiva
                                            Continue For
                                        End If

                                        '(1)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(impNuovoPAGATO) & " where id=" & row1.Item("ID_VOCE1")
                                        par.cmd.ExecuteNonQuery()

                                        '(2)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                        If importoMorosita <> 0 Then
                                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                            & "(" & row1.Item("ID_VOCE1") & ",'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(Math.Round(importoMorosita, 2)) & "," & idTipoPagamento & "," & idIncasso & ")"
                                            par.cmd.ExecuteNonQuery()
                                        End If

                                        'SEGNALAZIONE 476/2016 NON POSSO PAGARE I BOLLI PARZIALMENTE

                                        importoCredito = Math.Abs(importoCredito) - importoMorosita

                                    End If

                                Else
                                    Exit For
                                End If
                            Else
                                'NEL CASO DI BOLLETTE RICLASSIFICATE (MOROSITA'/RATEIZZAZIONE)
                                If importoCredito <> 0 Then
                                    If row1.Item("ID_TIPO") = "4" Then
                                        PagaVociBolRiclass(row1.Item("ID_BOLLETTA"), importoCredito)
                                    Else
                                        'PagaBolRateizzazione(row1.Item("ID_BOLLETTA"), importoCredito)
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                                & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1" _
                                                & " WHERE ID=" & row1.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()
                        Next
                    End If
                Next
            End If

            If idBollGest <> 0 Then
                ValorizzColonneImporti(idBollGest)
            End If

            Dim errScriveInSchema As Boolean = False

            Dim importoCreditoRest As Decimal = 0
            'importoCredito = TotBollettePagabile
            'If ripartizione = True Then
            If ripartizione = False Then
                importoCredito = importoCreditoRateizz
            End If

            If BolloPagParz.Value = "1" And importoCreditoIniziale < impBollo Then
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)
                Response.Write("<script>alert('Ripartizione interrotta per pagamento parziale delle voce bollo!');self.close();</script>")
                Exit Sub
            End If


            'If ripartizione = True Then
            If importoCredito > 0 Then
                    'If Math.Abs(importoCredito) > 0 Then

                    'importoCreditoIniziale = importoCreditoRateizz
                    importoCreditoRest = importoCredito
                    If ripartizione = False Then
                        importoCreditoRateizz = 0
                    End If
                    If statoContratto = "CHIUSO" Or par.IfEmpty(Request.QueryString("GEST"), "1") = "1" Then
                        CreditoResidInGest(idBollGest, importoCreditoRest, idContr)
                    Else
                        RestituzCreditoInBoll(idBollGest, importoCreditoRest, idContr, errScriveInSchema)
                        If errScriveInSchema = True Then
                            par.myTrans.Rollback()
                            par.myTrans = par.OracleConn.BeginTransaction()
                            HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)
                            Response.Write("<script>self.close();if (typeof opener.opener.opener != 'undefined'){opener.close();}</script>")
                            Exit Sub
                        End If
                    End If

                    If conferma0.Value = "1" Then

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F207','IMPORTO CREDITO INIZIALE EURO " & par.VirgoleInPunti(importoCreditoIniziale * -1) & " / COMPENSATO EURO " & par.VirgoleInPunti((importoCreditoIniziale - importoCreditoRest)) & " / ECCEDENZA EURO " & par.VirgoleInPunti((importoCreditoRest) * (-1)) & "')"
                        par.cmd.ExecuteNonQuery()

                    If statoContratto = "CHIUSO" Or par.IfEmpty(Request.QueryString("GEST"), "1") = "1" Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                            par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='P' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                            par.cmd.ExecuteNonQuery()
                        End If

                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey123", "aggiornaRU();", True)

                        '*******************RICHIAMO LA CONNESSIONE*********************
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        '*******************RICHIAMO LA TRANSAZIONE*********************
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        '‘par.cmd.Transaction = par.myTrans
                        If Not IsNothing(par.myTrans) Then
                            par.myTrans.Commit()
                        End If
                        par.OracleConn.Close()
                        par.OracleConn.Dispose()

                        '''Response.Write("<script>self.close();opener.document.getElementById('imgSalva').click();</script>")
                    Else
                        If importoCreditoIniziale = importoCreditoRest Then
                            'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='P' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                            'par.cmd.ExecuteNonQuery()
                        Else
                            'IL RESTO DEL CREDITO E' STATO UTILIZZATO ATTRAVERSO LA BOLLETTE PAGATE
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F02','COPERTURA BOLLETTE SCADUTE/SOLLECITATE PER UN TOTALE DI " & ((importoCreditoIniziale - importoCreditoRest)) & "')"
                            par.cmd.ExecuteNonQuery()

                        End If
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey123", "aggiornaRU();", True)

                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        '*******************RICHIAMO LA TRANSAZIONE*********************
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        '‘par.cmd.Transaction = par.myTrans
                        If Not IsNothing(par.myTrans) Then
                            par.myTrans.Commit()
                        End If
                        par.OracleConn.Close()
                        par.OracleConn.Dispose()

                    End If
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F206','IMPORTO CREDITO INIZIALE EURO " & par.VirgoleInPunti(importoCreditoIniziale * -1) & " / COMPENSATO EURO " & par.VirgoleInPunti((importoCreditoIniziale - importoCreditoRest) * -1) & " / ECCEDENZA EURO " & par.VirgoleInPunti((importoCreditoRest) * (-1)) & "')"
                    par.cmd.ExecuteNonQuery()

                    'AGGIORNO IL DOCUMENTO COME CONTABILE E TIPO APPLICAZIONE = 1 (: spostamento parziale)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
                    par.cmd.ExecuteNonQuery()

                    'Response.Write("<script>self.close();if (typeof opener.opener.opener != 'undefined') {opener.opener.document.getElementById('imgSalva').click();opener.close();} else {opener.document.getElementById('imgSalva').click();}</script>")
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey123", "aggiornaRU();", True)


                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans
                    If Not IsNothing(par.myTrans) Then
                        par.myTrans.Commit()
                    End If
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                End If

            'Else
            '    If statoContratto = "CHIUSO" Or par.IfEmpty(Request.QueryString("GEST"), "1") = "1" Then
            '        CreditoResidInGest(idBollGest, importoCreditoRest, idContr)
            '    Else
            '        RestituzCreditoInBoll(idBollGest, importoCreditoRest, idContr, errScriveInSchema)
            '        If errScriveInSchema = True Then
            '            par.myTrans.Rollback()
            '            par.myTrans = par.OracleConn.BeginTransaction()
            '            HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)
            '            Response.Write("<script>self.close();if (typeof opener.opener.opener != 'undefined'){opener.close();}</script>")
            '            Exit Sub
            '        Else
            '            If statoContratto = "CHIUSO" Then
            '                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
            '                par.cmd.ExecuteNonQuery()
            '            Else
            '                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='P' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
            '                par.cmd.ExecuteNonQuery()
            '            End If

            '            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey123", "aggiornaRU();", True)

            '            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            '            par.SettaCommand(par)
            '            '*******************RICHIAMO LA TRANSAZIONE*********************
            '            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '            '‘par.cmd.Transaction = par.myTrans
            '            If Not IsNothing(par.myTrans) Then
            '                par.myTrans.Commit()
            '            End If
            '            par.OracleConn.Close()
            '            par.OracleConn.Dispose()
            '        End If
            '    End If


            'End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ValorizzColonneImporti(ByVal idBollGest As Decimal)

        Dim incassato As Decimal = 0
        Dim credito As Decimal = 0
        Dim importoEccedenza As Decimal = 0


        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID=" & idBollGest
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            credito = par.IfNull(lettore("IMPORTO_TOTALE"), 0)
        End If
        lettore.Close()

        If idIncasso <> 0 Then
            par.cmd.CommandText = "SELECT id_bolletta,round(SUM(importo_pagato),2) AS incassato " _
                        & "FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI " _
                        & "WHERE FL_NO_REPORT=0 AND id_incasso_extramav = " & idIncasso & " " _
                        & "GROUP BY id_bolletta "
            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci As New Data.DataTable
            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci.Fill(dtVoci)
            daVoci.Dispose()

            par.cmd.CommandText = "SELECT id_voce_bolletta,data_pagamento,id_bolletta,round(importo_pagato,2) AS incassato " _
                        & "FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI " _
                        & "WHERE id_incasso_extramav = " & idIncasso & " " _
                        & ""
            Dim daVoci2 As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci2 As New Data.DataTable
            daVoci2 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci2.Fill(dtVoci2)
            daVoci2.Dispose()
            If dtVoci.Rows.Count > 0 Then
                For Each rowB As Data.DataRow In dtVoci.Rows
                    incassato = incassato + par.IfNull(rowB.Item("incassato"), 0)
                Next
            End If
        End If
        importoEccedenza = Math.Abs(credito) - Math.Abs(incassato)
        'RIF MAIL AMISANO 25/08/2017 IMPORTO_eccedenza qui viene scritto negativo, mentre da incassi manuali è positivo (pagato 100 Inca 80 eccedenza 20)
        par.cmd.CommandText = "update siscom_mi.incassi_extramav set importo_incassato = " & par.insDbValue(incassato, False) & ", " _
                & "importo_eccedenza=" & par.insDbValue((importoEccedenza) * -1, False) & " where id = " & idIncasso
        par.cmd.ExecuteNonQuery()

    End Sub

    Private Sub PagaBolRateizzazione(ByVal idBolletta As Integer, ByRef ImpForRateiz As Decimal)
        Try
            Dim quotaCapitale As Decimal = 0
            'RIF MAIL AMISANO 25/08/2017 MANCAVA NVL(IMP_PAGATO,0) E COMUNQUE NON SI CAPISCE PERCHE' FACCIA QUESTO
            par.cmd.CommandText = "SELECT (IMPORTO-nvl(IMP_PAGATO,0)) AS IMP FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_VOCE in (676,677) and ID_BOLLETTA=" & idBolletta
            Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderI.Read Then
                quotaCapitale = par.IfNull(myReaderI("IMP"), 0)
                'RIF MAIL AMISANO 25/08/2017 SE PAGO PARZIALMENTE UNA RATA, 
                'NON POSSO USARE RIPARTIRE L'INTERO IMPORTO DELL'INCASSO SULLE RICLA, MA SOLO QUELLO INERENTE ALLA QUOTA CAPITALI. MANCAVA L'IF...
                If ImpForRateiz > quotaCapitale Then
                    ImpForRateiz = quotaCapitale
                End If
            End If
            myReaderI.Close()

            par.cmd.CommandText = "SELECT ID,IMPORTO_RIC_B FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_RATEIZZAZIONE = " _
                                & "(SELECT ID_RATEIZZAZIONE FROM SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE ID_BOLLETTA = " & idBolletta & ")" _
                                & " ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtBoll As New Data.DataTable()
            da.Fill(dtBoll)
            'ImpForRateiz = Importo
            Dim idBoll As String = "0"
            For Each r As Data.DataRow In dtBoll.Rows
                If ImpForRateiz > 0 Then
                    'If par.IfNull(r.Item("IMPORTO_RIC_B"), 0) > 0 Then


                    'End If
                    PagaVociBolRiclass(r.Item("ID"), ImpForRateiz)

                    '25/07/2012 se pagata completamente la riclassificata vengono allineati gli importi di pagamento

                    PagataCompletamente(par.IfNull(r.Item("ID"), 0))

                End If

            Next

        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- PagaVociBolRateizzate" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub PagaVociBolRiclass(ByVal idBolRateizzata As Integer, ByRef ImpForRateiz As Decimal)
        Try
            Dim OldIdBolletta As String = 0
            Dim Pagato As Decimal = 0
            Dim QVersato As Decimal = 0
            Dim PagatoReale As Decimal = 0

            '16/07/2015 Eliminata condizione che esclude le quote sindacali come richiesto dal comune
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.TIPO_VOCE,(case when (importo - nvl(imp_pagato,0))<0 then 1000000 + abs((importo - nvl(imp_pagato,0))) else (importo - nvl(imp_pagato,0)) end) as ordine,BOL_BOLLETTE.ID_TIPO,GRUPPO " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                                & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) AND " _
                                & "ID_BOLLETTA = BOL_BOLLETTE.ID " _
                                & "AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                                & "AND BOL_BOLLETTE.ID = " & idBolRateizzata & " " _
                                & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL " _
                                & " ORDER BY ordine DESC"
            Dim row As Data.DataRow
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)

            dt = AggiustaDebitoReale(dt)

            For Each row In dt.Rows
                QVersato = 0
                'ImpForRateiz della voce di bolletta positivo eseguo algoritmo ripartizione 
                If par.IfNull(row.Item("IMPORTO"), 0) > 0 Then

                    Pagato = par.IfNull(row.Item("IMP_PAGATO"), 0)

                    If Pagato = 0 Then
                        If isPagabileBollo(row.Item("ID").ToString, ImpForRateiz, impBollo) = False Then
                            'salta il pagamento e va alla voce successiva
                            Continue For
                        End If
                        If ImpForRateiz > 0 And ImpForRateiz >= par.IfNull(row.Item("IMPORTO"), 0) Then
                            'se importo pagamento > importo della voce da pagare, la pago tutta
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) _
                                                & " WHERE ID = " & par.IfNull(row.Item("ID"), 0)
                            par.cmd.ExecuteNonQuery()

                            lstBolliParz.Remove(row.Item("ID"))

                            'WriteEvent(False, row.Item("ID"), "F205", par.IfNull(row.Item("IMPORTO"), 0), Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto)

                            WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), CDec(par.IfNull(row.Item("IMPORTO"), 0)), idIncasso, idTipoPagamento)

                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                PagaRiclassMoros(row.Item("ID_BOLLETTA"), ImpForRateiz)
                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************

                            '******************06/09/2013 viene spalmata solo la quota capitale sulle bollette che hanno generato la rateizzazione...
                            If par.IfNull(row.Item("ID_TIPO"), 1) = 5 Then
                                If par.IfNull(row.Item("ID_VOCE"), 1) <> 95 And par.IfNull(row.Item("ID_VOCE"), 1) <> 407 And par.IfNull(row.Item("ID_VOCE"), 1) <> 678 Then
                                    PagaBolRateizzazione(row.Item("ID_BOLLETTA"), ImpForRateiz)
                                End If
                            End If

                            ImpForRateiz = ImpForRateiz - par.IfNull(row.Item("IMPORTO"), 0)

                        ElseIf ImpForRateiz > 0 And ImpForRateiz < (par.IfNull(row.Item("IMPORTO"), 0)) Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()
                            'se importo pagamento < importo della voce da pagare, la pago per l'importo pagamento che mi resta
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpForRateiz) _
                                                & " WHERE ID = " & par.IfNull(row.Item("ID"), 0)
                            par.cmd.ExecuteNonQuery()

                            lstBolliParz.Remove(row.Item("ID"))

                            'WriteEvent(False, row.Item("ID"), "F205", ImpForRateiz, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto)

                            WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), ImpForRateiz, idIncasso, idTipoPagamento)
                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                PagaRiclassMoros(par.IfNull(row.Item("ID_BOLLETTA"), 0), ImpForRateiz)
                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************

                            '******************06/09/2013 viene spalmata solo la quota capitale sulle bollette che hanno generato la rateizzazione...
                            If par.IfNull(row.Item("ID_TIPO"), 1) = 5 Then
                                If par.IfNull(row.Item("ID_VOCE"), 1) <> 95 And par.IfNull(row.Item("ID_VOCE"), 1) <> 407 And par.IfNull(row.Item("ID_VOCE"), 1) <> 678 Then
                                    PagaBolRateizzazione(row.Item("ID_BOLLETTA"), ImpForRateiz)
                                End If
                            End If
                            ImpForRateiz = ImpForRateiz - ImpForRateiz
                        ElseIf ImpForRateiz = 0 Then
                            Exit For
                        End If
                    ElseIf par.IfNull(row.Item("IMPORTO"), 0) > par.IfNull(row.Item("IMP_PAGATO"), 0) Then

                        If ImpForRateiz > 0 And ImpForRateiz > Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2) Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()

                            QVersato = Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(QVersato) & " + NVL(IMP_PAGATO,0) " _
                                & " WHERE ID = " & par.IfNull(row.Item("ID"), 0)
                            par.cmd.ExecuteNonQuery()

                            lstBolliParz.Remove(row.Item("ID"))

                            'WriteEvent(False, row.Item("ID"), "F205", QVersato, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto)

                            WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), QVersato, idIncasso, idTipoPagamento)
                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                PagaRiclassMoros(row.Item("ID_BOLLETTA"), ImpForRateiz)
                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************

                            '******************06/09/2013 viene spalmata solo la quota capitale sulle bollette che hanno generato la rateizzazione...
                            If par.IfNull(row.Item("ID_TIPO"), 1) = 5 Then
                                If par.IfNull(row.Item("ID_VOCE"), 1) <> 95 And par.IfNull(row.Item("ID_VOCE"), 1) <> 407 And par.IfNull(row.Item("ID_VOCE"), 1) <> 678 Then
                                    PagaBolRateizzazione(row.Item("ID_BOLLETTA"), ImpForRateiz)
                                End If
                            End If
                            ImpForRateiz = ImpForRateiz - QVersato

                        ElseIf ImpForRateiz > 0 And ImpForRateiz <= (CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))) Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpForRateiz) & "+ NVL(IMP_PAGATO,0) " _
                                                & " WHERE ID = " & row.Item("ID")
                            par.cmd.ExecuteNonQuery()

                            lstBolliParz.Remove(row.Item("ID"))

                            'WriteEvent(False, row.Item("ID"), "F205", ImpForRateiz, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, idContratto)

                            WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), ImpForRateiz, idIncasso, idTipoPagamento)
                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                PagaRiclassMoros(row.Item("ID_BOLLETTA"), ImpForRateiz)
                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************

                            '******************06/09/2013 viene spalmata solo la quota capitale sulle bollette che hanno generato la rateizzazione...
                            If par.IfNull(row.Item("ID_TIPO"), 1) = 5 Then
                                If par.IfNull(row.Item("ID_VOCE"), 1) <> 95 And par.IfNull(row.Item("ID_VOCE"), 1) <> 407 And par.IfNull(row.Item("ID_VOCE"), 1) <> 678 Then
                                    PagaBolRateizzazione(row.Item("ID_BOLLETTA"), ImpForRateiz)
                                End If
                            End If
                            ImpForRateiz = 0
                        End If

                    End If

                End If

            Next
            da.Dispose()
            dt.Dispose()
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- PagaVociBolRateizzate" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Private Sub PagaRiclassMoros(ByVal idBollettaRic As String, ByVal ImpPaga As Decimal)
        Try

            Dim OldIdBolletta As String = 0
            Dim Pagato As Decimal = 0
            Dim TotNegativi As Decimal = 0
            Dim TotPositivi As Decimal = 0
            Dim PercIncidenza As Decimal = 0

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID,  (case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end) as ordine " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                        & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) " _
                        & "AND ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                        & "AND IMPORTO > 0 " _
                        & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC = " & idBollettaRic & " " _
                        & "AND T_VOCI_BOLLETTA.COMPETENZA <> 3 " _
                        & "GROUP BY BOL_BOLLETTE.ID,  (case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end) " _
                        & "ORDER BY ordine ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtBollette As New Data.DataTable
            da.Fill(dtBollette)
            Dim idBolletta As String = "0"

            For Each r As Data.DataRow In dtBollette.Rows

                ''*************26/08/2011 Inserimento della data di pagamento per successivi UNDO************
                'If idBolletta <> par.IfNull(r.Item("ID"), 0) Then
                '    idBolletta = par.IfNull(r.Item("ID"), 0)
                '    WriteDataPagBol(idBolletta, idEventoPrincipale)
                'End If

                '16/07/2015 Eliminata condizione che esclude le quote sindacali come richiesto dal comune
                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.TIPO_VOCE,(case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end) as ordine,BOL_BOLLETTE.ID_TIPO,T_VOCI_BOLLETTA.GRUPPO " _
                            & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                            & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) " _
                            & "AND ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_VOCE = T_VOCI_BOLLETTA.ID AND NVL(BOL_BOLLETTE_VOCI.IMPORTO_RICLASSIFICATO,0) <> 0 " _
                            & "AND BOL_BOLLETTE.ID = " & r.Item("ID") & " " _
                            & "AND T_VOCI_BOLLETTA.COMPETENZA <> 3 " _
                            & "ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC, ordine asc "
                '& "AND IMPORTO > 0 " _

                Dim row As Data.DataRow
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtMorosita As New Data.DataTable()
                da.Fill(dtMorosita)

                '25/07/2012 viene allineato il debito alle voci negative presenti nella bolletta (B.M.)
                dtMorosita = AggiustaDebitoReale(dtMorosita)


                For Each row In dtMorosita.Rows

                    'Importo della voce di bolletta positivo eseguo algoritmo ripartizione 
                    If row.Item("IMPORTO") > 0 Then
                        'PagatoReale = par.IfNull(row.Item("IMP_PAGATO"), 0)
                        'PercIncidenza = Math.Round((row.Item("IMPORTO") * 100) / TotPositivi, 6)
                        'If TotNegativi > 0 Then
                        '    row.Item("IMP_PAGATO") = par.IfNull(row.Item("IMP_PAGATO"), 0) + Math.Round((TotNegativi * PercIncidenza) / 100, 6)
                        'End If
                        Pagato = par.IfNull(row.Item("IMP_PAGATO"), 0)
                        If Pagato = 0 Then
                            If ImpPaga > 0 And ImpPaga >= par.IfNull(row.Item("IMPORTO"), 0) Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                                par.cmd.ExecuteNonQuery()


                                'se importo pagamento > importo della voce da pagare, la pago tutta
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                lstBolliParz.Remove(row.Item("ID"))

                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMPORTO_RICLASSIFICATO_PAGATO = ((NVL(IMP_PAGATO,0) + NVL(IMPORTO_RICLASSIFICATO,0))-NVL(IMPORTO,0)) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), CDec(par.IfNull(row.Item("IMPORTO"), 0)), idIncasso, idTipoPagamento)


                                '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************
                                ImpPaga = ImpPaga - par.IfNull(row.Item("IMPORTO"), 0)

                            ElseIf ImpPaga > 0 And ImpPaga < (par.IfNull(row.Item("IMPORTO"), 0)) Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                                par.cmd.ExecuteNonQuery()

                                'se importo pagamento < importo della voce da pagare, la pago per l'importo pagamento che mi resta
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpPaga) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                lstBolliParz.Remove(row.Item("ID"))

                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMPORTO_RICLASSIFICATO_PAGATO = " & par.VirgoleInPunti(ImpPaga) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()


                                WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), ImpPaga, idIncasso, idTipoPagamento)



                                ImpPaga = ImpPaga - ImpPaga

                            ElseIf ImpPaga = 0 Then
                                Exit For
                            End If
                        ElseIf par.IfNull(row.Item("IMPORTO"), 0) > par.IfNull(row.Item("IMP_PAGATO"), 0) Then

                            If ImpPaga > 0 And ImpPaga > Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2) Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)) & " + NVL(IMP_PAGATO,0) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                lstBolliParz.Remove(row.Item("ID"))

                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET  IMPORTO_RICLASSIFICATO_PAGATO = ((NVL(IMP_PAGATO,0) + NVL(IMPORTO_RICLASSIFICATO,0))-NVL(IMPORTO,0)) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()


                                WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0)), idIncasso, idTipoPagamento)

                                ImpPaga = ImpPaga - Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)

                            ElseIf ImpPaga > 0 And ImpPaga <= (CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))) Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.FormattaData(Format(Now, "yyyyMMdd"))))) _
                                    & "',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "'" _
                                    & " WHERE ID=" & row.Item("ID_BOLLETTA")
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpPaga) & "+ NVL(IMP_PAGATO,0) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                lstBolliParz.Remove(row.Item("ID"))

                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET  IMPORTO_RICLASSIFICATO_PAGATO = NVL(IMPORTO_RICLASSIFICATO_PAGATO,0) + " & par.VirgoleInPunti(ImpPaga) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()

                                WriteVociPagRicla(row.Item("ID"), Format(Now, "dd/MM/yyyy"), ImpPaga, idIncasso, idTipoPagamento)
                                ImpPaga = 0
                            End If
                        End If
                    End If
                Next
                da.Dispose()
                dtMorosita.Dispose()

                '25/07/2012 se pagata completamente la riclassificata vengono allineati gli importi di pagamento (B.M.)
                PagataCompletamente(par.IfNull(r.Item("ID"), 0))

            Next
            dtBollette.Dispose()
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- PagaRiclassificate" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub WriteVociPagamenti(ByVal idVoceBolletta As Integer, ByVal dataPagamento As String, ByVal importo As Decimal, ByVal idIncassoExt As Integer, ByVal tipo As Integer)

        Try

            If importo > 0 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                    & "(" & idVoceBolletta & ",'" & par.AggiustaData(dataPagamento) & "'," & par.VirgoleInPunti(Math.Round(importo, 2)) & "," & tipo & "," & idIncassoExt & ")"
                par.cmd.ExecuteNonQuery()
            End If


        Catch ex As Exception

            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If

            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteEvent" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Private Sub WriteVociPagRicla(ByVal idVoceBolletta As Integer, ByVal dataPagamento As String, ByVal importo As Decimal, ByVal idIncassoExt As Integer, ByVal tipo As Integer)

        Try
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.bol_bollette_voci_pagamenti (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                & "(" & idVoceBolletta & ",'" & par.AggiustaData(dataPagamento) & "'," & par.VirgoleInPunti(Math.Round(importo, 2)) & "," & tipo & "," & idIncassoExt & ")"
            par.cmd.ExecuteNonQuery()

        Catch ex As Exception

            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If

            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteEvent" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Private Function AggiustaDebitoReale(ByVal dt As Data.DataTable) As Data.DataTable 'ORIGINALE
        AggiustaDebitoReale = dt.Clone
        'Dim returnValue() As Data.DataRow
        Dim negativiDisponibili As Decimal = 0
        Dim impnegativo As Decimal = 0
        Dim valida As Boolean = True


        For Each riga As Data.DataRow In dt.Rows
            If par.IfNull(riga.Item("IMPORTO"), 0) < 0 And (par.IfNull(riga.Item("IMP_PAGATO"), 0) <> par.IfNull(riga.Item("IMPORTO"), 0)) Then
                impnegativo = Math.Abs(par.IfNull(riga.Item("IMPORTO"), 0))
                For Each r As Data.DataRow In dt.Rows
                    If par.IfNull(riga.Item("GRUPPO"), 4) = par.IfNull(r.Item("GRUPPO"), 4) And par.IfNull(riga.Item("ID"), 0) <> par.IfNull(r.Item("ID"), 0) Then
                        If impnegativo <= (par.IfNull(r.Item("IMPORTO"), 0) - par.IfNull(r.Item("IMP_PAGATO"), 0)) Then
                            r.Item("IMPORTO") = par.IfNull(r.Item("IMPORTO"), 0) - impnegativo
                            'If par.IfNull(r.Item("IMP_PAGATO"), 0) <> 0 Then
                            '    r.Item("IMP_PAGATO") = par.IfNull(r.Item("IMP_PAGATO"), 0) - impnegativo
                            'End If

                            impnegativo = 0
                        Else
                            If par.IfNull(r.Item("IMP_PAGATO"), 0) <> 0 Then
                                impnegativo = impnegativo - (par.IfNull(r.Item("IMPORTO"), 0) - par.IfNull(r.Item("IMP_PAGATO"), 0))

                            Else
                                impnegativo = impnegativo - par.IfNull(r.Item("IMPORTO"), 0)

                            End If
                            r.Item("IMPORTO") = 0
                        End If
                    End If
                Next
                negativiDisponibili = negativiDisponibili + impnegativo

            End If
        Next

        While negativiDisponibili > 0
            For Each r As Data.DataRow In dt.Rows
                If par.IfNull(r.Item("IMPORTO"), 0) > 0 Then

                    If negativiDisponibili <= (par.IfNull(r.Item("IMPORTO"), 0) - par.IfNull(r.Item("IMP_PAGATO"), 0)) Then
                        r.Item("IMPORTO") = par.IfNull(r.Item("IMPORTO"), 0) - negativiDisponibili
                        negativiDisponibili = 0
                    Else
                        If par.IfNull(r.Item("IMP_PAGATO"), 0) <> 0 Then
                            negativiDisponibili = negativiDisponibili - (par.IfNull(r.Item("IMPORTO"), 0) - par.IfNull(r.Item("IMP_PAGATO"), 0))
                        Else
                            negativiDisponibili = negativiDisponibili - par.IfNull(r.Item("IMPORTO"), 0)
                        End If
                        r.Item("IMPORTO") = 0
                    End If
                End If
            Next
        End While


        For Each r0 As Data.DataRow In dt.Rows
            If par.IfNull(r0("IMPORTO"), 0) > 0 Then
                AggiustaDebitoReale.Rows.Add(r0.ItemArray)
            End If
        Next

        Return AggiustaDebitoReale
    End Function

    Private Sub PagataCompletamente(ByVal idBolletta As String)
        Try
            Dim ConnOpenNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim PagataIntera As Boolean = False

            par.cmd.CommandText = "select sum(importo) as importo_totale , sum(nvl(imp_pagato,0)) as totale_pagato from siscom_mi.bol_bollette_voci where id_bolletta = " & idBolletta
            lettore = par.cmd.ExecuteReader

            If lettore.Read Then
                If par.IfNull(lettore("importo_totale"), 0) = par.IfNull(lettore("totale_pagato"), 0) Then
                    PagataIntera = True
                End If
            End If
            lettore.Close()

            If PagataIntera = True Then
                '***********************memorizzo l'importo pagato inserito dall'utente che ha generato un pagamento totale*********************************
                par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato_bak = imp_pagato where id_bolletta = " & idBolletta
                par.cmd.ExecuteNonQuery()

                '*********************aggiorno l'importo pagato con l'importo della voce (anche per le negative) *******************************************
                par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato = importo, importo_riclassificato_pagato = importo_riclassificato where id_bolletta = " & idBolletta
                par.cmd.ExecuteNonQuery()

            End If

            If ConnOpenNow = True Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- PagataCompletamente" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Public Property dtdatiInsert() As Data.DataTable
        Get
            If Not (ViewState("dtdatiInsert") Is Nothing) Then
                Return ViewState("dtdatiInsert")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtdatiInsert") = value
        End Set
    End Property

    Private Sub RestituzCreditoInBoll(ByVal idBollGest As Long, ByVal importoCreditoRest As Decimal, ByVal idContr As Long, ByRef errore As Boolean)
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim importoIniziale As Decimal = 0
            Dim num_rate As Decimal = 0
            Dim dataDecorr As String = ""
            Dim IDUNITA As Long = 0

            Dim importoCredito As Decimal = 0

            Dim importoSpesa300 As Decimal = 0
            Dim importoSpesa301 As Decimal = 0
            Dim importoSpesa302 As Decimal = 0
            Dim importoSpesa303 As Decimal = 0
            Dim importoSpesaTOT As Decimal = 0

            Dim BOLLO_BOLLETTA As Decimal = 0
            Dim SPESEPOSTALI As Decimal = 0
            Dim SPMAV As Decimal = 0
            Dim vociAutomatiche As Decimal = 0


            Dim importoNewBolletta As Decimal = 0

            Dim rataProssima As Integer = 0

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContr & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                importoIniziale = par.IfNull(myReader0("IMP_CANONE_INIZIALE"), 0)
                num_rate = par.IfNull(myReader0("NRO_RATE"), 0)
                dataDecorr = par.IfNull(myReader0("DATA_DECORRENZA"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & idContr & ""
            Dim myReaderU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderU.Read Then
                IDUNITA = par.IfNull(myReaderU("ID_UNITA"), 0)
            End If
            myReaderU.Close()

            Dim prossimaBolletta As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.rapporti_utenza_prossima_bol WHERE ID_CONTRATTO=" & idContr
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                prossimaBolletta = par.IfNull(myReader0("PROSSIMA_BOLLETTA"), "")
                rataProssima = CInt(Mid(prossimaBolletta, 5, 2))
            End If
            myReader0.Close()

            Dim annoSchema As Integer = Year(Now)
            Dim esercFinanz As Integer = 0

            If Mid(prossimaBolletta, 1, 4) <> Year(Now) Then
                esercFinanz = par.RicavaEsercizio(Mid(prossimaBolletta, 1, 4))

                par.cmd.CommandText = "select * from siscom_mi.pf_main where ID_ESERCIZIO_FINANZIARIO =" & esercFinanz
                myReader0 = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    If par.IfNull(myReader0("APPLICAZIONE_BOL"), 0) = 1 Then
                        annoSchema = Mid(prossimaBolletta, 1, 4)
                    Else
                        annoSchema = Year(Now)
                    End If
                End If
                myReader0.Close()
            End If

            par.cmd.CommandText = "SELECT * from SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=" & idContratto & " AND ANNO=" & annoSchema & ""
            Dim daSpese As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtSpese As New Data.DataTable
            daSpese = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daSpese.Fill(dtSpese)
            daSpese.Dispose()
            Dim impVoceNegativo As Decimal = 0
            For Each row As Data.DataRow In dtSpese.Rows
                Select Case row.Item("ID_VOCE")
                    Case "300"
                        importoSpesa300 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "301"
                        importoSpesa301 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "302"
                        importoSpesa302 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "303"
                        importoSpesa303 = row.Item("IMPORTO_SINGOLA_RATA")
                End Select
                If row.Item("IMPORTO_SINGOLA_RATA") < 0 Then
                    If rataProssima <= (row.Item("DA_RATA") + row.Item("PER_RATE")) Then
                        impVoceNegativo += row.Item("IMPORTO_SINGOLA_RATA")
                    End If
                End If
            Next

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                BOLLO_BOLLETTA = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=17"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                SPESEPOSTALI = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                SPMAV = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            vociAutomatiche = BOLLO_BOLLETTA + SPESEPOSTALI + SPMAV

            Dim ESERCIZIOF As Long = 0

            Dim dataProssimoPeriodo As Integer = 0

            Dim rate As Integer = 0
            Dim importoDifferenza As Decimal = 0
            Dim importoCredRateizz As Decimal = 0

            ESERCIZIOF = par.RicavaEsercizioCorrente

            importoSpesaTOT = importoSpesa300 + importoSpesa301 + importoSpesa302 + importoSpesa303 + vociAutomatiche + impVoceNegativo

            'IMPORTO ANNUALE: voci canone + eventuali altre spese + spese fisse
            importoNewBolletta = Format((importoIniziale / num_rate) + importoSpesaTOT, "##,##0.00")

            If importoNewBolletta < 0 Then
                errore = True
                Response.Write("<script>alert('Errore importo stimato prossima bolletta!');</script>")
                Exit Sub
            End If

            Dim annoBolletta As Integer = Mid(prossimaBolletta, 1, 4)

            Dim mesePrecUltimaBolletta As String = ""

            If Mid(prossimaBolletta, 5, 1) = "0" Then
                If Mid(prossimaBolletta, 5, 4) = "01" Then
                    mesePrecUltimaBolletta = "12"
                Else
                    mesePrecUltimaBolletta = "0" & Mid(prossimaBolletta, 6, 4) - 1
                End If
            Else
                mesePrecUltimaBolletta = Mid(prossimaBolletta, 5, 4) - 1
            End If

            'If mesePrecUltimaBolletta = "12" Then
            '    annoBolletta = annoBolletta - 1
            'End If


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE IMPORTO<>0 AND ID_BOLLETTA_GEST=" & idBollGest & "order by importo desc"
            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci As New Data.DataTable
            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci.Fill(dtVoci)
            daVoci.Dispose()

            Dim j As Integer = 0


            For Each row As Data.DataRow In dtVoci.Rows
                If j = 0 Then
                    j = dtVoci.Rows.Count
                Else
                    j = j - 1
                End If

                importoCredito = importoCreditoRest / j

                If importoCredito > Math.Abs(row.Item("importo")) Then
                    importoCredito = Math.Abs(row.Item("importo"))
                End If

                importoDifferenza = importoNewBolletta - importoCredito
                If importoDifferenza < 0 Then
                    Dim i As Integer = 0
                    While importoCredito > importoNewBolletta
                        rate = rate + (importoCredito / importoNewBolletta)
                        rate = Format(rate, "0")
                        importoCredito = importoCredito / rate
                        i = i + 1
                    End While

                    importoCredito = importoCreditoRest / rate

                    par.scritturaSchemaBollette(idContr, IDUNITA, row.Item("ID_VOCE"), Math.Round(importoCreditoRest, 2) * -1, rate, annoBolletta & Format(CInt(rataProssima), "00"), , row.Item("ID"), annoBolletta & Format(rataProssima, "00"), 2)

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F184','RIPARTIZIONE PARZIALE DI IMPORTO A CREDITO GESTIONALE')"
                    par.cmd.ExecuteNonQuery()

                Else
                    rate = 1
                    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) Values (siscom_mi.seq_bol_schema.nextval, " & idContr & ", " & IDUNITA & ", " & ESERCIZIOF & ", " & row.Item("ID_VOCE") & ", " & (par.VirgoleInPunti(Format(importoCredito, "##,##0.00") * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(Format(importoCredito, "##,##0.00") * (-1))) & " , " & annoBolletta & "," & par.IfNull(row.Item("ID"), 0) & ")"
                    par.cmd.ExecuteNonQuery()
                End If
                importoCreditoRest = importoCreditoRest - importoCredito
            Next
            'If conferma0.Value = "1" Then
            '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            '                & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            '                & "'F184','RIPARTIZIONE PARZIALE DI IMPORTO A CREDITO GESTIONALE')"
            '    par.cmd.ExecuteNonQuery()
            'End If

            If dtdatiInsert.Rows.Count > 0 Then
                ConfEsegui()
            End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Sub RestituzCreditoInBollNUOVA(ByVal idBollGest As Long, ByVal importoCreditoRest As Decimal, ByVal idContr As Long, ByRef errore As Boolean)
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim importoIniziale As Decimal = 0
            Dim num_rate As Decimal = 0
            Dim dataDecorr As String = ""
            Dim IDUNITA As Long = 0

            Dim importoCredito As Decimal = 0

            Dim importoSpesa300 As Decimal = 0
            Dim importoSpesa301 As Decimal = 0
            Dim importoSpesa302 As Decimal = 0
            Dim importoSpesa303 As Decimal = 0
            Dim importoSpesaTOT As Decimal = 0

            Dim BOLLO_BOLLETTA As Decimal = 0
            Dim SPESEPOSTALI As Decimal = 0
            Dim SPMAV As Decimal = 0
            Dim vociAutomatiche As Decimal = 0


            Dim importoNewBolletta As Decimal = 0

            Dim rataProssima As Integer = 0

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContr & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                importoIniziale = par.IfNull(myReader0("IMP_CANONE_INIZIALE"), 0)
                num_rate = par.IfNull(myReader0("NRO_RATE"), 0)
                dataDecorr = par.IfNull(myReader0("DATA_DECORRENZA"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & idContr & ""
            Dim myReaderU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderU.Read Then
                IDUNITA = par.IfNull(myReaderU("ID_UNITA"), 0)
            End If
            myReaderU.Close()

            Dim prossimaBolletta As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.rapporti_utenza_prossima_bol WHERE ID_CONTRATTO=" & idContr
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                prossimaBolletta = par.IfNull(myReader0("PROSSIMA_BOLLETTA"), "")
                rataProssima = CInt(Mid(prossimaBolletta, 5, 2))
            End If
            myReader0.Close()

            Dim annoSchema As Integer = Year(Now)
            Dim esercFinanz As Integer = 0

            If Mid(prossimaBolletta, 1, 4) <> Year(Now) Then
                esercFinanz = par.RicavaEsercizio(Mid(prossimaBolletta, 1, 4))

                par.cmd.CommandText = "select * from siscom_mi.pf_main where ID_ESERCIZIO_FINANZIARIO =" & esercFinanz
                myReader0 = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    If par.IfNull(myReader0("APPLICAZIONE_BOL"), 0) = 1 Then
                        annoSchema = Mid(prossimaBolletta, 1, 4)
                    Else
                        annoSchema = Year(Now)
                    End If
                Else
                    annoSchema = Mid(prossimaBolletta, 1, 4)
                End If
                myReader0.Close()
            End If

            'Modifica Pellegri Scrittura nello Schema

            Dim rateMax As Integer = 0
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_GEST_CREDITO WHERE ID=1"
            Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderC.Read Then
                rateMax = par.IfNull(myReaderC("N_MESI"), 1)
            End If
            myReaderC.Close()


            Dim dtVociGest As Data.DataTable

            par.cmd.CommandText = "SELECT sum(importo) FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & idBollGest
            Dim CreditoTotale As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)

            Dim anno As Integer = annoSchema
            Dim rata As Integer = rataProssima
            Dim creditoTotaleResiduo As Decimal = 0
            For i As Integer = rataProssima To rataProssima + rateMax - 1
                If i > rataProssima Then
                    importoCreditoRest = creditoTotaleResiduo
                End If

                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI_GEST.id,id_voce,round(sum(importo)*(" & par.VirgoleInPunti(importoCreditoRest) & ")/" & par.VirgoleInPunti(CreditoTotale) & ",2) as importo FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & idBollGest & " group by BOL_BOLLETTE_VOCI_GEST.id,id_voce ORDER BY ID_VOCE ASC"
                Dim daVociGest As Oracle.DataAccess.Client.OracleDataAdapter
                daVociGest = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                dtVociGest = New Data.DataTable
                daVociGest.Fill(dtVociGest)
                daVociGest.Dispose()

                creditoTotaleResiduo = 0
                For Each row As Data.DataRow In dtVociGest.Rows
                    creditoTotaleResiduo += CDec(row.Item("importo"))
                Next

                If creditoTotaleResiduo <= 0 Then
                    Exit For
                End If

                rata = i Mod 12
                If rata = 0 Then
                    rata = 12
                End If
                If i > 12 Then
                    anno = annoSchema + 1
                End If

                Dim impSchema As Decimal = 0
                importoSpesaTOT = 0
                importoNewBolletta = 0

                par.cmd.CommandText = "select sum(importo_singola_Rata) as importo_singola_rata from siscom_mi.bol_schema,siscom_mi.t_voci_bolletta where " & rata & " >= da_rata " _
                    & " and (" & rata & "- da_rata) <= (per_rate - 1) and t_voci_bolletta.id=bol_schema.id_voce and  bol_schema.id_contratto=" & idContratto & " and anno=" & anno
                impSchema = par.IfNull(par.cmd.ExecuteScalar, 0)


                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    BOLLO_BOLLETTA = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=17"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    SPESEPOSTALI = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    SPMAV = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

                vociAutomatiche = BOLLO_BOLLETTA + SPESEPOSTALI + SPMAV

                Dim ESERCIZIOF As Long = 0

                Dim dataProssimoPeriodo As Integer = 0

                Dim rate As Integer = 0
                Dim importoDifferenza As Decimal = 0
                Dim importoCredRateizz As Decimal = 0

                ESERCIZIOF = par.RicavaEsercizioCorrente

                importoSpesaTOT = impSchema + vociAutomatiche

                'IMPORTO ANNUALE: voci canone + eventuali altre spese + spese fisse
                importoNewBolletta = Math.Round(importoIniziale / num_rate, 2) + importoSpesaTOT

                If importoNewBolletta < 0 Then
                    errore = True
                    Response.Write("<script>alert('Errore importo stimato prossima bolletta!');</script>")
                    Exit Sub
                End If

                par.cmd.CommandText = "SELECT nvl(imp_minimo_Bolletta,0) from SISCOM_MI.TAB_GEST_CREDITO WHERE ID=1"
                Dim impMinimoBoll As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)

                Dim importoMassimoRimborsabile As Decimal = importoNewBolletta - impMinimoBoll
                Dim importoDaRestituire As Decimal = 0

                If importoMassimoRimborsabile >= creditoTotaleResiduo Then
                    importoDaRestituire = creditoTotaleResiduo
                Else
                    importoDaRestituire = importoMassimoRimborsabile
                End If

                If importoDaRestituire > 0 Then
                    'tutte le voci vengono rimborsate al 100%
                    Dim totale As Decimal = 0
                    Dim indice As Integer = 0
                    Dim importoDaInserire As Decimal = 0
                    For Each row As Data.DataRow In dtVociGest.Rows
                        indice += 1
                        If indice < dtVociGest.Rows.Count Then
                            importoDaInserire = Math.Round(row.Item("importo") * importoDaRestituire / creditoTotaleResiduo, 2)
                            totale = totale + importoDaInserire
                        Else
                            importoDaInserire = importoDaRestituire - totale
                        End If
                        par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA " _
                            & " (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                            & " IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, " _
                            & " ANNO,ID_VOCE_BOLLETTA_GEST) " _
                            & " Values " _
                            & " (siscom_mi.seq_bol_schema.nextval, " & idContr _
                            & ", " & IDUNITA & ", " & ESERCIZIOF & ", " & row.Item("ID_VOCE") _
                            & ", " & par.VirgoleInPunti(importoDaInserire * (-1)) _
                            & " , " & rata & ",1, " _
                            & par.VirgoleInPunti(importoDaInserire * (-1)) _
                            & " , " & anno & "," & par.IfNull(row.Item("ID"), 0) & ")"
                        par.cmd.ExecuteNonQuery()
                    Next
                    creditoTotaleResiduo = creditoTotaleResiduo - importoDaRestituire
                    'CreditoTotale += importoDaRestituire
                End If
            Next

            If creditoTotaleResiduo > 0 Then
                Dim idIncassoExtraMav As String = ""
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.nextval FROM DUAL"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    If idIncasso > 0 Then
                        idIncassoExtraMav = idIncasso
                    End If
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST ( " _
                        & " ID, ID_CONTRATTO, ID_ESERCIZIO_F,  " _
                        & " ID_UNITA, ID_ANAGRAFICA, RIFERIMENTO_DA,  " _
                        & " RIFERIMENTO_A, IMPORTO_TOTALE, DATA_EMISSIONE,  " _
                        & " DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,  " _
                        & " NOTE,ID_EVENTO_PAGAMENTO)  " _
                        & " (SELECT " & myReader1(0) & ", " _
                        & " " & idContr & ", " _
                        & " ID_ESERCIZIO_F, " _
                        & " ID_UNITA, " _
                        & " ID_ANAGRAFICA, " _
                        & " RIFERIMENTO_DA, " _
                        & " RIFERIMENTO_A, " _
                        & " " & par.VirgoleInPunti((creditoTotaleResiduo) * -1) & ", " _
                        & " DATA_EMISSIONE, " _
                        & " DATA_PAGAMENTO, " _
                        & " DATA_VALUTA, " _
                        & " ID_TIPO, " _
                        & " 'Importo residuo in seguito a ripartizione credito " & idBollGest & "'," & idIncassoExtraMav & " " _
                        & " FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID = " & idBollGest & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO, IMP_PAGATO, FL_ACCERTATO) VALUES " _
                        & " (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL, " & myReader1(0) & ", " & dtVociGest.Rows(0).Item("ID_VOCE") & ", " & par.VirgoleInPunti(creditoTotaleResiduo) & ", NULL, NULL)"
                    par.cmd.ExecuteNonQuery()
                End If
                myReader1.Close()
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F184','IMPORTO A CREDITO GESTIONALE EURO " & CreditoTotale & " ELABORATO PARZIALMENTE (A SCALARE SULLE FUTURE EMISSIONI)')"
            par.cmd.ExecuteNonQuery()

            'Dim annoBolletta As Integer = Mid(prossimaBolletta, 1, 4)

            'Dim mesePrecUltimaBolletta As String = ""

            'If Mid(prossimaBolletta, 5, 1) = "0" Then
            '    If Mid(prossimaBolletta, 5, 4) = "01" Then
            '        mesePrecUltimaBolletta = "12"
            '    Else
            '        mesePrecUltimaBolletta = "0" & Mid(prossimaBolletta, 6, 4) - 1
            '    End If
            'Else
            '    mesePrecUltimaBolletta = Mid(prossimaBolletta, 5, 4) - 1
            'End If

            'If mesePrecUltimaBolletta = "12" Then
            '    annoBolletta = annoBolletta - 1
            'End If

            'Dim msg As String
            ''Dim title As String
            ''Dim style As MsgBoxStyle
            ''Dim response As MsgBoxResult

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & idBollGest
            'Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            'Dim dtVoci As New Data.DataTable
            'daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            'daVoci.Fill(dtVoci)
            'daVoci.Dispose()
            ''Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) Values (siscom_mi.seq_bol_schema.nextval, " & idContr & ", " & IDUNITA & ", " & ESERCIZIOF & ", " & row.Item("ID_VOCE") & ", " & (par.VirgoleInPunti(Format(importoCreditoRest, "##,##0.00") * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(Format(importoCredito, "##,##0.00") * (-1))) & " , " & annoBolletta & "," & par.IfNull(row.Item("ID"), 0) & ")
            'dtdatiInsert = New Data.DataTable
            'dtdatiInsert.Columns.Add("id_contratto")
            'dtdatiInsert.Columns.Add("id_unita")
            'dtdatiInsert.Columns.Add("esercizio_f")
            'dtdatiInsert.Columns.Add("id_voce")
            'dtdatiInsert.Columns.Add("importo_cred")
            'dtdatiInsert.Columns.Add("rataProssima")
            'dtdatiInsert.Columns.Add("rate")
            'dtdatiInsert.Columns.Add("importo_cred_sing")
            'dtdatiInsert.Columns.Add("anno")
            'dtdatiInsert.Columns.Add("id_voce_boll_gest")

            'Dim r As Data.DataRow

            'For Each row As Data.DataRow In dtVoci.Rows
            '    importoCredito = importoCreditoRest / dtVoci.Rows.Count

            '    importoDifferenza = importoNewBolletta - importoCredito
            '    If importoDifferenza < 0 Then
            '        Dim i As Integer = 0
            '        While importoCredito > importoNewBolletta
            '            Rate = Rate() + (importoCredito / importoNewBolletta)
            '            Rate = Format(Rate, "0")
            '            importoCredito = importoCredito / Rate()
            '            i = i + 1
            '        End While

            '        importoCredito = importoCreditoRest / Rate()

            '        If Rate() > rateMax Then

            '            'RIEMPIO DATATABLE CON DATI CHE SERVONO
            '            r = dtdatiInsert.NewRow()
            '            r.Item("id_contratto") = idContr
            '            r.Item("id_unita") = IDUNITA
            '            r.Item("esercizio_f") = ESERCIZIOF
            '            r.Item("id_voce") = row.Item("ID_VOCE")
            '            r.Item("importo_cred") = par.VirgoleInPunti(Format(importoCreditoRest, "##,##0.00") * (-1))
            '            r.Item("rataProssima") = rataProssima
            '            r.Item("rate") = Rate()
            '            r.Item("importo_cred_sing") = par.VirgoleInPunti(Format(importoCredito, "##,##0.00") * (-1))
            '            r.Item("anno") = annoBolletta
            '            r.Item("id_voce_boll_gest") = par.IfNull(row.Item("ID"), 0)
            '            dtdatiInsert.Rows.Add(r)

            '        Else
            '            par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) Values (siscom_mi.seq_bol_schema.nextval, " & idContr & ", " & IDUNITA & ", " & ESERCIZIOF & ", " & row.Item("ID_VOCE") & ", " & (par.VirgoleInPunti(Format(importoCreditoRest, "##,##0.00") * (-1))) & " , " & rataProssima & ", " & Rate() & ", " & (par.VirgoleInPunti(Format(importoCredito, "##,##0.00") * (-1))) & " , " & annoBolletta & "," & par.IfNull(row.Item("ID"), 0) & ")"
            '            par.cmd.ExecuteNonQuery()

            '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            '                & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            '                & "'F184','RIPARTIZIONE PARZIALE DI IMPORTO A CREDITO GESTIONALE')"
            '            par.cmd.ExecuteNonQuery()
            '        End If
            '    Else
            '        Rate = 1
            '        par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) Values (siscom_mi.seq_bol_schema.nextval, " & idContr & ", " & IDUNITA & ", " & ESERCIZIOF & ", " & row.Item("ID_VOCE") & ", " & (par.VirgoleInPunti(Format(importoCredito, "##,##0.00") * (-1))) & " , " & rataProssima & ", " & Rate() & ", " & (par.VirgoleInPunti(Format(importoCredito, "##,##0.00") * (-1))) & " , " & annoBolletta & "," & par.IfNull(row.Item("ID"), 0) & ")"
            '        par.cmd.ExecuteNonQuery()
            '    End If
            'Next

            'If dtdatiInsert.Rows.Count > 0 Then
            '    ConfEsegui()
            'End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CreditoResidInGest(ByVal idBollGest As Long, ByVal importoCreditoRest As Decimal, ByVal idContr As Long)
        '*******************RICHIAMO LA CONNESSIONE*********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        '*******************RICHIAMO LA TRANSAZIONE*********************
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans


        Dim ID_VOCE As Integer = 0
        Dim MiaData As Date = DateAdd(DateInterval.Second, 1, Now)
        Dim idIncassoExtraMav As String = "NULL"
        If idIncasso > 0 Then
            idIncassoExtraMav = idIncasso
        End If
        Try
            If importoCreditoRest <> 0 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & idBollGest & ""
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & idBollGest
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        ID_VOCE = myReader2("ID_VOCE")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA, RIFERIMENTO_DA, RIFERIMENTO_A, IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO, TIPO_APPLICAZIONE, DATA_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE, ID_EVENTO_PAGAMENTO) VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL, " & par.IfNull(myReader("ID_CONTRATTO"), "NULL") & ", 29, " & par.IfNull(myReader("ID_UNITA"), "NULL") & ", " & par.IfNull(myReader("ID_ANAGRAFICA"), "NULL") & ",'" & par.IfNull(myReader("RIFERIMENTO_DA"), "NULL") & "', '" & par.IfNull(myReader("RIFERIMENTO_A"), "NULL") & "', " & par.VirgoleInPunti(importoCreditoRest * -1) & ", '" & par.IfNull(myReader("DATA_EMISSIONE"), "NULL") & "', NULL,NULL, " & par.IfNull(myReader("ID_TIPO"), "NULL") & ", 'N', NULL, '','Importo residuo in seguito a ripartizione credito " & idBollGest & "', " & idIncassoExtraMav & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO, IMP_PAGATO, FL_ACCERTATO) VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL, " & myReader1(0) & ", " & ID_VOCE & ", " & par.VirgoleInPunti(importoCreditoRest * -1) & ", NULL, NULL)"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(MiaData, "yyyyMMddHHmmss") & "'," _
                                    & "'F204','ECCEDENZA DI EURO " & importoCreditoRest & " A SEGUITO DI RIPARTIZIONE CREDITO GESTIONALE')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader1.Close()
                End If
                myReader.Close()
            End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub



    Private Sub ElaborazioneTotDebito(ByVal idContr As Long, ByVal idBollGest As Long)
        Try
            '***** APPLICA INTERAMENTE COME NUOVA EMISSIONE *****
            Dim IDUNITA As Long = 0
            Dim anno As Integer = 0
            Dim INIZIO As String = ""
            Dim FINE As String = ""
            Dim idTIPO As Integer = 0
            Dim noteGEST As String = ""
            Dim importoGest As Decimal = 0
            Dim idAnagrafica As Long = 0
            Dim Cognome As String = ""
            Dim Nome As String = ""
            Dim dataEmissGest As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & idContr & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                IDUNITA = par.IfNull(myReader0("ID_UNITA"), 0)
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE COD_TIPOLOGIA_OCCUPANTE='INTE' AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.ID AND ID_CONTRATTO=" & idContr
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idAnagrafica = par.IfNull(myReader0("ID_ANAGRAFICA"), 0)
                Cognome = par.IfNull(myReader0("cognome"), "")
                Nome = par.IfNull(myReader0("nome"), "")
            End If
            myReader0.Close()

            Dim Nome1 As String = ""
            Dim Nome2 As String = ""

            'NUOVA TABELLA 1
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & idBollGest
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                INIZIO = par.IfNull(myReader0("RIFERIMENTO_DA"), "")
                dataEmissGest = par.IfNull(myReader0("DATA_EMISSIONE"), "")
                FINE = par.IfNull(myReader0("RIFERIMENTO_A"), "")
                idTIPO = par.IfNull(myReader0("ID_TIPO"), 0)
                noteGEST = par.IfNull(myReader0("NOTE"), "")
                importoGest = par.IfNull(myReader0("IMPORTO_TOTALE"), 0)
            End If
            myReader0.Close()

            dataEmissGest = Format(Now, "yyyyMMdd")

            par.cmd.CommandText = "select RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & idContr & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
            Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderS.Read Then
                'If UCase(Cognome & " " & Nome) <> UCase(par.IfNull(myReaderS("PRESSO_COR"), "")) Then
                Nome1 = Cognome & " " & Nome
                Nome2 = par.IfNull(myReaderS("PRESSO_COR"), "")
                'Else
                'Nome1 = Cognome & " " & Nome
                'End If
                par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                & "(ID, N_RATA,DATA_EMISSIONE, DATA_SCADENZA," _
                & "NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                & "ID_UNITA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                & "FL_STAMPATO, ID_COMPLESSO," _
                & "ANNO, OPERATORE_PAG, ID_EDIFICIO, RIF_FILE,ID_TIPO) " _
                & "Values " _
                & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL,99,'" & dataEmissGest _
                & "', '" & dataEmissGest & "','" & par.PulisciStrSql(noteGEST) & "'," _
                & "" & idContr _
                & " ," & par.RicavaEsercizioCorrente & ", " _
                & IDUNITA _
                & ",'', " & idAnagrafica _
                & ", '" & par.PulisciStrSql(Nome1) & "', " _
                & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReaderS("CIVICO_COR"), ""))) _
                & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") _
                & "', '" & par.PulisciStrSql(Nome2) & "', '" & INIZIO _
                & "', '" & FINE & "', " _
                & "'0', " & myReaderS("ID_COMPLESSO") & ", " _
                & Year(Now) & ", '', " & myReaderS("ID_EDIFICIO") & ",'MOD',1)"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReaderA.Read Then
                    Dim ID_BOLLETTA As Long = myReaderA(0)

                    'NUOVA TABELLA 2
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & idBollGest
                    Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
                    Dim dtVoci As New Data.DataTable
                    daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    daVoci.Fill(dtVoci)
                    daVoci.Dispose()
                    For Each row As Data.DataRow In dtVoci.Rows
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                            & "," & row.Item("ID_VOCE") & "" _
                                            & "," & par.VirgoleInPunti(row.Item("IMPORTO")) & ",'')"
                        par.cmd.ExecuteNonQuery()
                    Next
                End If
                myReaderA.Close()



            End If
            myReaderS.Close()

            'AGGIORNO CON TIPO APPLICAZIONE = T (: spostamento totale)
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T',DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F206','IMPORTO ELABORATO: EURO " & par.VirgoleInPunti(importoGest) & "')"
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>self.close();opener.document.getElementById('imgSalva').click();</script>")

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnConfermaRate_Click(sender As Object, e As System.EventArgs) Handles btnConfermaRate.Click
        ConfEsegui()

    End Sub
    Private Sub ConfEsegui()
        If dtdatiInsert.Rows.Count > 0 Then
            If riga.Value < dtdatiInsert.Rows.Count Then
                For I As Integer = riga.Value To dtdatiInsert.Rows.Count
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "ConfermaRate(" & dtdatiInsert.Rows(I).Item("rate") & ");", True)
                    Exit For
                Next
            Else
                'Response.Write("<script>self.close();opener.document.getElementById('imgSalva').click();</script>")
                Response.Write("<script>self.close();if (typeof opener.opener != 'unknown') {opener.opener.document.getElementById('imgSalva').click();opener.close();} else {opener.document.getElementById('imgSalva').click();}</script>")

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGEST" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Commit()
                End If
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End If
        End If
    End Sub
    Protected Sub Esegui_Click(sender As Object, e As System.EventArgs) Handles Esegui.Click
        'eseguo la insert, incremeto riga.value, richiamo conferma
        If dtdatiInsert.Rows.Count > 0 Then
            If riga.Value <= dtdatiInsert.Rows.Count Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNGEST" & vIdConnessione, par.OracleConn) 'MEMORIZZAZIONE CONNESSIONE IN VARIABILE DI SESSIONE
                End If

                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSGEST" & vIdConnessione, par.myTrans)


                par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, " _
                                    & "IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) " _
                                    & "Values (siscom_mi.seq_bol_schema.nextval, " & dtdatiInsert.Rows(riga.Value).Item("id_contratto") & ", " _
                                    & dtdatiInsert.Rows(riga.Value).Item("id_unita") & ", " & dtdatiInsert.Rows(riga.Value).Item("esercizio_f") & ", " _
                                    & dtdatiInsert.Rows(riga.Value).Item("id_voce") & ", " & dtdatiInsert.Rows(riga.Value).Item("importo_cred") & " , " _
                                    & dtdatiInsert.Rows(riga.Value).Item("rataProssima") & ", " & dtdatiInsert.Rows(riga.Value).Item("rate") & ", " _
                                    & dtdatiInsert.Rows(riga.Value).Item("importo_cred_sing") & " , " & dtdatiInsert.Rows(riga.Value).Item("anno") & "," _
                                    & dtdatiInsert.Rows(riga.Value).Item("id_voce_boll_gest") & ")"
                par.cmd.ExecuteNonQuery()




                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & dtdatiInsert.Rows(riga.Value).Item("id_contratto") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F184','RIPARTIZIONE PARZIALE DI IMPORTO A CREDITO GESTIONALE')"
                par.cmd.ExecuteNonQuery()

                riga.Value = riga.Value + 1

                ConfEsegui()


            End If

        End If


    End Sub
End Class
