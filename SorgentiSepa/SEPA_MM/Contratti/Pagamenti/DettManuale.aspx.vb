
Partial Class Contratti_Pagamenti_DettManuale
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim idEventoPrincipale As String
    Public Property ImpForRateiz() As Decimal
        Get
            If Not (ViewState("par_ImpForRateiz") Is Nothing) Then
                Return CDec(ViewState("par_ImpForRateiz"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_ImpForRateiz") = value
        End Set

    End Property
    Public Property vIdIncassoExtramav() As String
        Get
            If Not (ViewState("vIdIncassoExtramav") Is Nothing) Then
                Return CStr(ViewState("vIdIncassoExtramav"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("vIdIncassoExtramav") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		'modifica marco 06/09/2012
		'inizializzazione dell'hiddenfield clickConferma 
        clickConferma.Value = 0
		'-------------------------
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Me.rdbTipoPagam.SelectedValue = 0
            Me.lblNumBolletta.Text = Request.QueryString("NUMBOL")
            txtDataPagamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtImpPagamento.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);return false;")
            txtImpPagamento.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            CaricaCmbTipologia()
            CaricaBolletta()
            AddJavascriptFunction()
            ''****************WRITE-EVENT*****************
            'WriteEvent(Request.QueryString("IDBOL"), "F101", "PAGAMENTO MANUALE - ACCESSO AI DATI DELLA BOLLETTA")

        End If
    End Sub
    Private Sub CaricaBolletta(Optional callChk As Boolean = False)
        Try
            totBolletta.Value = 0
            totPagato.Value = 0
            totResiduo.Value = 0
            If Me.chkEcludeQS.Checked = True And Me.cmbTipoPagamento.SelectedValue <> 8 Then
                Me.chkEcludeQS.Checked = False
            ElseIf Me.chkEcludeQS.Checked = False And Me.cmbTipoPagamento.SelectedValue = 8 Then
                Me.chkEcludeQS.Checked = True
            End If

            If Request.QueryString("IDCON_MOR") <> "" Then
                'MOROSITA (Epifani)
                IdConnessione = Request.QueryString("IDCON_MOR")

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                callEpi.Value = 1


            Else
                IdConnessione = ""

                '*****************APERTURA CONNESSIONE***************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
            End If

            Dim condEscludiQSind As String = ""
            If Me.chkEcludeQS.Checked = True Then
                condEscludiQSind = " and gruppo <> 5 "
            End If

            '*************12/07/2011 modifico la query perchè RESIDUO con importi negativi viene errato...TRIM(TO_CHAR(NVL(NVL(IMPORTO,0)-NVL(IMP_PAGATO,0),0),'9G999G999G990D99')) AS RESIDUO
            '*************12/07/2011 lo calcolo dopo che sui risultati inseriti nella dt, eseguo la funzione di AggiustaDebitoReale
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*, T_VOCI_BOLLETTA.DESCRIZIONE, " _
                                & "BOL_BOLLETTE.ID_TIPO,BOL_BOLLETTE.ID_CONTRATTO,GRUPPO,T_VOCI_BOLLETTA.TIPO_VOCE, '' AS RESIDUO " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA,SISCOM_MI.BOL_BOLLETTE " _
                                & "WHERE ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_VOCE = T_VOCI_BOLLETTA.ID AND ID_BOLLETTA = " & Request.QueryString("IDBOL") & condEscludiQSind _
                                & " ORDER BY GRUPPO ASC, TIPO_VOCE ASC"


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            idContratto.Value = dt.Rows(0).Item("ID_CONTRATTO")

            dt = AggiustaDebitoReale(dt)
            Dim row As Data.DataRow
            For Each row In dt.Rows
                totBolletta.Value = CDec(totBolletta.Value) + CDec(par.IfNull(row.Item("IMPORTO"), 0))
                totPagato.Value = CDec(totPagato.Value) + CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))
                row.Item("IMP_PAGATO") = Format(par.IfNull(row.Item("IMP_PAGATO"), 0), "##,##0.00")
                row.Item("RESIDUO") = Format(par.IfNull(row.Item("IMPORTO"), 0) - par.IfNull(row.Item("IMP_PAGATO"), 0), "##,##0.00")
                totResiduo.Value = CDec(totResiduo.Value) + CDec(par.IfNull(row.Item("RESIDUO"), 0))
            Next

            row = dt.NewRow()
            row.Item("DESCRIZIONE") = "T O T A L E"
            row.Item("IMPORTO") = Format(CDec(totBolletta.Value), "##,##0.00")
            row.Item("IMP_PAGATO") = Format(CDec(totPagato.Value), "##,##0.00")
            row.Item("RESIDUO") = Format(CDec(totResiduo.Value), "##,##0.00")

            dt.Rows.Add(row)

            Me.DataGridBollette.DataSource = dt
            Me.DataGridBollette.DataBind()

            Dim di As DataGridItem
            di = Me.DataGridBollette.Items(Me.DataGridBollette.Items.Count - 1)
            CType(di.Cells(4).FindControl("txtPaga"), TextBox).Visible = True
            CType(di.Cells(4).FindControl("txtPaga"), TextBox).ReadOnly = True
            'CType(di.Cells(4).FindControl("txtPaga"), TextBox).BackColor = Drawing.Color.DodgerBlue
            'CType(di.Cells(4).FindControl("txtPaga"), TextBox).ForeColor = Drawing.Color.White

            di.Cells(1).BackColor = Drawing.Color.DodgerBlue
            di.Cells(2).BackColor = Drawing.Color.DodgerBlue
            di.Cells(3).BackColor = Drawing.Color.DodgerBlue
            di.Cells(4).BackColor = Drawing.Color.DodgerBlue
            di.Cells(5).BackColor = Drawing.Color.DodgerBlue

            di.Cells(1).ForeColor = Drawing.Color.White
            di.Cells(2).ForeColor = Drawing.Color.White
            di.Cells(3).ForeColor = Drawing.Color.White
            di.Cells(4).ForeColor = Drawing.Color.White
            di.Cells(5).ForeColor = Drawing.Color.White

            If totPagato.Value = totBolletta.Value Then
                Dim i As Integer = 0
                For i = 0 To Me.DataGridBollette.Items.Count - 2
                    di = Me.DataGridBollette.Items(i)
                    CType(di.Cells(3).FindControl("txtPaga"), TextBox).Enabled = False
                Next
                Me.rdbTipoPagam.Enabled = False
                Me.txtDataPagamento.Enabled = False
                Me.txtImpPagamento.Enabled = False
                Me.txtNumeroPagamento.Enabled = False
                Me.labelNumeroAssegno.Enabled = False
                Me.btnConfirm.Visible = False
                'modifica marco 06/09/2012
                'nascondere il bottone pagamento e disabilitare 
                'la combo dei tipi pagamento
                Me.txtImpPagamento.Visible = False
                Me.txtNumeroPagamento.Visible = False
                Me.ButtonIncassoNonAttribuito.Visible = False
                Me.cmbTipoPagamento.Enabled = False
                Me.labelNumeroAssegno.Visible = False
                '-------------------------


            End If

            '*********************CHIUSURA CONNESSIONE**********************
            If IdConnessione = "" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            If callChk = False And Me.cmbTipoPagamento.SelectedValue <> 8 Then
                Me.vIdIncassoExtramav = ""
                Me.cmbTipoPagamento.SelectedValue = "-1"
                Me.txtNotePagamento.Text = ""
            Else
                If Not String.IsNullOrEmpty(txtImpPagamento.Text) Then
                    RiparisciExtraMav()
                End If
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaBolletta - " & ex.Message
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- btnConfirm_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try


    End Sub
    Private Sub AddJavascriptFunction()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridBollette.Items.Count - 2
                di = Me.DataGridBollette.Items(i)
                CType(di.Cells(3).FindControl("txtPaga"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
                CType(di.Cells(3).FindControl("txtPaga"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "AddJavascriptFunction - " & ex.Message
        End Try

    End Sub

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConfirm.Click
		'modifica marco 06/09/2012
		'impostare l'hiddenfield clickConferma a 1
        clickConferma.Value = 1
		'se si sta utilizzando il pagamento tramite assegni
		'non attribuiti deve essere chiamata eseguita
		'AggiornaDaText_IncassiNonAttribuiti()
		If Not IsNothing(Session.Item("IdIncassoNonAttribuito")) And Not IsNothing(Session.Item("ImportoIncassoNonAttribuito")) Then
            AggiornaDaText_IncassiNonAttribuiti()
        End If
        '-------------------------
        If Me.rdbTipoPagam.SelectedValue = 0 Then
            RiparisciExtraMav()
        End If

        Try
            '02/07/2012 tipo pagamento
            If Me.cmbTipoPagamento.SelectedValue = "-1" Then
                Response.Write("<script>alert('Selezionare la tipologia del pagamento!');</script>")
                Exit Sub
            End If

            If stopImpSuperiore.Value = 0 Then

                If isCredito.Value = 1 And confCredito.Value = 0 Then
                    CaricaBolletta()
                    Me.txtDataPagamento.Text = ""
                    Me.txtImpPagamento.Text = ""
                    Me.txtNumeroPagamento.Text = ""
                    Me.rdbTipoPagam.SelectedValue = 0
                    Me.txtImpPagamento.Visible = True
                    Me.txtNumeroPagamento.Visible = True
                    Me.txtImpPagamento.Enabled = True
                    Me.txtNumeroPagamento.Enabled = True
                    Me.labelNumeroAssegno.Visible = True
                    Me.Label2.Visible = True
                    isCredito.Value = 0
                    confCredito.Value = 0
                    'modifica marco 06/09/2012
                    'abilitare la combo per la scelta della tipologia
                    'del pagamento
                    Me.cmbTipoPagamento.Enabled = True

                    Session.Remove("ImportoIncassoNonAttribuito")
                    Session.Remove("IdIncassoNonAttribuito")
                    Me.tipoVocPag.Value = 2

                    Me.labelNumeroAssegno.Visible = False
                    Me.txtNumeroPagamento.Visible = False
                    '-------------------------

                    Exit Sub

                End If

                If String.IsNullOrEmpty(Me.txtDataPagamento.Text) Then
                    Response.Write("<script>alert('La data pagamento è obbligatoria!');</script>")
                    CaricaBolletta()
                    Me.txtDataPagamento.Text = ""
                    Me.txtImpPagamento.Text = ""
                    Me.txtNumeroPagamento.Text = ""
                    Me.rdbTipoPagam.SelectedValue = 0
                    Me.txtImpPagamento.Visible = True
                    Me.txtNumeroPagamento.Visible = True
                    Me.txtImpPagamento.Enabled = True
                    Me.txtNumeroPagamento.Enabled = True
                    Me.labelNumeroAssegno.Visible = True
                    Me.Label2.Visible = True
                    isCredito.Value = 0
                    confCredito.Value = 0
                    'modifica marco 06/09/2012
                    Me.cmbTipoPagamento.Enabled = True

                    Session.Remove("ImportoIncassoNonAttribuito")
                    Session.Remove("IdIncassoNonAttribuito")
                    Me.tipoVocPag.Value = 2

                    Me.labelNumeroAssegno.Visible = False
                    Me.txtNumeroPagamento.Visible = False
                    '-------------------------
                    Exit Sub

                End If

                If Me.rdbTipoPagam.SelectedValue = 0 And String.IsNullOrEmpty(Me.txtImpPagamento.Text) Then
                    Response.Write("<script>alert('Inserire l\'importo per il quale si vuole emettere il pagamento!');</script>")
                    Exit Sub
                End If
                If Not IsNothing(Session.Item("IdIncassoNonAttribuito")) And Not IsNothing(Session.Item("ImportoIncassoNonAttribuito")) Then
                    If CDec(Me.txtImpPagamento.Text.Replace(".", "")) > CDec(Session.Item("ImportoIncassoNonAttribuito")) Then
                        Response.Write("<script>alert('L\'importo del pagamento non può superare quello dell\'incasso!');</script>")
                        Exit Sub

                    End If

                End If

                If par.AggiustaData(Me.txtDataPagamento.Text) > Format(Date.Now, "yyyyMMdd") Then
                    Response.Write("<script>alert('La data pagamento non può essere successiva alla data odierna!');</script>")
                    CaricaBolletta()
                    Me.txtDataPagamento.Text = ""
                    Me.txtImpPagamento.Text = ""
                    Me.txtNumeroPagamento.Text = ""
                    Me.rdbTipoPagam.SelectedValue = 0
                    Me.txtImpPagamento.Visible = True
                    Me.txtNumeroPagamento.Visible = True
                    Me.txtImpPagamento.Enabled = True
                    Me.txtNumeroPagamento.Enabled = True
                    Me.labelNumeroAssegno.Visible = True
                    Me.Label2.Visible = True
                    isCredito.Value = 0
                    confCredito.Value = 0
                    'modifica marco 06/09/2012
                    Me.cmbTipoPagamento.Enabled = True
                    '-------------------------
                    Exit Sub

                End If
                If ControllaImporti() = False Then
                    Exit Sub
                End If


                If Request.QueryString("IDCON_MOR") <> "" Then
                    'MOROSITA (Epifani)
                    IdConnessione = Request.QueryString("IDCON_MOR")

                    If par.OracleConn.State = Data.ConnectionState.Open Then
                        Response.Write("IMPOSSIBILE VISUALIZZARE")
                        Exit Sub
                    Else
                        ' RIPRENDO LA CONNESSIONE
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)

                        'RIPRENDO LA TRANSAZIONE
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        ‘‘par.cmd.Transaction = par.myTrans


                    End If

                Else
                    IdConnessione = ""
                    '*****************APERTURA CONNESSIONE***************
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If

                    '*************************APERTURA TRANSAZIONE****************************
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans

                End If


                Dim TipoBol As String = TrovaTipoBol()

                Dim i As Integer = 0
                Dim di As DataGridItem
                Dim ImpPagato As Decimal = 0



                If Me.rdbTipoPagam.SelectedValue = 0 Then
                    vIdIncassoExtramav = WriteIncasso(CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(credito.Value.ToString.Replace(".", ""), 0)))

                    idEventoPrincipale = WriteEvent(True, "null", "F101", CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)), Me.txtDataPagamento.Text, "null", "EVENTO PRINCIPALE " & Me.rdbTipoPagam.SelectedItem.Text.ToUpper & "")
                Else
                    Dim totImp As Decimal = 0
                    For i = 0 To Me.DataGridBollette.Items.Count - 2
                        di = Me.DataGridBollette.Items(i)
                        totImp = totImp + par.IfEmpty(CType(di.Cells(5).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)
                    Next
                    vIdIncassoExtramav = WriteIncasso(totImp)

                    idEventoPrincipale = WriteEvent(True, "null", "F101", CDec(par.IfEmpty(totImp, 0)), Me.txtDataPagamento.Text, "null", "EVENTO PRINCIPALE " & Me.rdbTipoPagam.SelectedItem.Text.ToUpper & "")
                End If

                '*************26/08/2011 Inserimento della data di pagamento per successivi UNDO************
                WriteDataPagBol(Request.QueryString("IDBOL"), idEventoPrincipale)

                If credito.Value > 0 Then

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.CREDITI (ID,ID_CONTRATTO,DATA,IMPORTO,ID_EVENTO_PAGAMENTO) VALUES " _
                        & "(SISCOM_MI.SEQ_CREDITI.NEXTVAL, " & idContratto.Value & ",'" & par.AggiustaData(Me.txtDataPagamento.Text) & "'," & par.VirgoleInPunti(credito.Value.Replace(".", "")) & "," & idEventoPrincipale & ")"
                    par.cmd.ExecuteNonQuery()


                    WriteEvent(True, "null", "F178", credito.Value.Replace(".", ""), "", idEventoPrincipale, "CREDITO IMPORTO PAGAMENTO", idMain:=idEventoPrincipale)
                    credito.Value = 0
                    Session.Add("IDINCASSO", vIdIncassoExtramav)

                End If
                For i = 0 To Me.DataGridBollette.Items.Count - 2
                    di = Me.DataGridBollette.Items(i)
                    If par.IfEmpty(CType(di.Cells(5).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0) > 0 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                        & "IMP_PAGATO = " & par.IfEmpty(par.VirgoleInPunti(CDec(par.IfEmpty(CType(di.Cells(6).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(di.Cells(TrovaIndiceColonna(DataGridBollette, "IMP_PAGATO")).Text.Replace(".", ""), 0))), "Null") _
                        & " WHERE ID = " & di.Cells(0).Text
                        par.cmd.ExecuteNonQuery()

                    End If

                    'End If
                    ImpPagato = par.IfEmpty(((CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""))), "0")

                    If Me.rdbTipoPagam.SelectedValue = 0 And ImpPagato > 0 Then
                        WriteEvent(False, di.Cells(0).Text, "F101", ImpPagato, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE MANUALE - PAGAMENTO SINGOLA VOCE")
                        WriteVociPagamenti(di.Cells(0).Text, Me.txtDataPagamento.Text, CDec(ImpPagato), vIdIncassoExtramav, Me.tipoVocPag.Value)
                        'modifica marco 06/09/2012
                        'se si attribuiscono incassi vanno registrati
                        'nella tabella incassi_Attribuiti
                        If Not IsNothing(Session.Item("importoIncassoNonAttribuito")) And Not IsNothing(Session.Item("IdIncassoNonAttribuito")) Then
                            RegistraIncassiAttribuiti(di.Cells(0).Text, ImpPagato, vIdIncassoExtramav)
                        End If
                        '-------------------------
                    ElseIf Me.rdbTipoPagam.SelectedValue = 1 And ImpPagato > 0 Then
                        WriteEvent(False, di.Cells(0).Text, "F101", ImpPagato, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE MANUALE - PAGAMENTO SINGOLA VOCE")
                        WriteVociPagamenti(di.Cells(0).Text, Me.txtDataPagamento.Text, CDec(ImpPagato), vIdIncassoExtramav, Me.tipoVocPag.Value)
                    End If
                    If di.Cells(1).Text = 150 Or di.Cells(1).Text = 151 Then
                        PagaRiclassificate(Request.QueryString("IDBOL"), ImpPagato)
                    End If

                    If TipoBol = "5" Then
                        '******************27/10/2011 aggiorno importo_pagato della bolletta per l'importo di rateizzazione escluse le spese...
                        '******************06/09/2013 viene spalmata solo la quota capitale sulle bollette che hanno generato la rateizzazione...
                        If di.Cells(1).Text <> 95 And di.Cells(1).Text <> 407 And di.Cells(1).Text <> 678 Then
                            PagaBolRateizzazione(Request.QueryString("IDBOL"), ImpPagato)
                        End If
                    End If

                Next
                'AGGIORNO BOLLETTE con data_valuta e data_inserimento_pagamento
                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE " _
                                        & " SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(txtDataPagamento.Text))) _
                                           & "',operatore_pag='" & par.PulisciStrSql(Session.Item("OPERATORE")) _
                                           & "',DATA_PAGAMENTO='" & par.AggiustaData(txtDataPagamento.Text) _
                                           & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1," _
                                           & "DATA_INS_PAGAMENTO='" & Format(Now, "yyyyMMddHHmmss")



                If IdConnessione <> "" Then
                    'MOROSITA (Epifani)
                    par.cmd.CommandText = par.cmd.CommandText & "',ID_BOLLETTA_RIC=Null " _
                                                              & " ,ID_MOROSITA=Null " _
                                                              & " WHERE ID=" & Request.QueryString("IDBOL")
                Else
                    par.cmd.CommandText = par.cmd.CommandText & "' WHERE ID=" & Request.QueryString("IDBOL")
                End If

                par.cmd.ExecuteNonQuery()
                '            'modifica marco 06/09/2012
                ''se la tipologia di pagamento è assegno e se 
                ''non sono nulli l'id dell'assegno e l'importo dell'assegno
                ''bisogna impostare il flag fl_attribuito a 1
                '            If cmbTipoPagamento.SelectedValue = 5 And Not IsNothing(Session.Item("IdIncassoNonAttribuito")) And Not IsNothing(Session.Item("ImportoIncassoNonAttribuito")) Then
                '                par.cmd.CommandText = "UPDATE SISCOM_MI.INCASSI_NON_ATTRIBUIBILI SET " _
                '                & " Fl_ATTRIBUITO=1,DATA_ATTRIBUZIONE='" & Format(Now, "yyyyMMdd") & "' " _
                '                & " WHERE ID=" & Session.Item("IdIncassoNonAttribuito")
                '                par.cmd.ExecuteNonQuery()
                '            End If




                Me.tipoVocPag.Value = 2

                '----------------------------
                Response.Write("<script>alert('Operazione eseguita correttamente!Il pagamento è stato memorizzato!');</script>")
                'Response.Write("<script>window.close();</script>")

                '*********************COMMIT OPERAZIONI ESEGUITE E CHIUSURA CONNESSIONE**********************
                If IdConnessione = "" Then
                    par.myTrans.Commit()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            Else
                stopImpSuperiore.Value = 0
            End If


            CaricaBolletta()
            PagataCompletamente(Request.QueryString("IDBOL"))

            Me.txtDataPagamento.Text = ""
            Me.txtImpPagamento.Text = ""
            Me.txtNumeroPagamento.Text = ""
            Me.rdbTipoPagam.SelectedValue = 0
            Me.txtImpPagamento.Visible = True
            Me.txtNumeroPagamento.Visible = True
            Me.txtImpPagamento.Enabled = True
            Me.txtNumeroPagamento.Enabled = True
            Me.labelNumeroAssegno.Visible = True
            Me.Label2.Visible = True
            'modifica marco 06/09/2012
            Me.cmbTipoPagamento.Enabled = True
            '-------------------------



        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- btnConfirm_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub txtImpPagamento_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtImpPagamento.TextChanged
        AggiornaDaText()

    End Sub
    Private Function ControllaImporti() As Boolean
        ControllaImporti = True
        Dim totScritto As Decimal = 0
        Try
            For Each di As DataGridItem In DataGridBollette.Items
                If Math.Round(CDec(par.IfEmpty(CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)), 2) > Math.Round(CDec(par.IfEmpty(di.Cells(TrovaIndiceColonna(DataGridBollette, "RESIDUO")).Text.Replace(".", ""), 0)), 2) Then

                    'If Math.Round(CDec(par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)), 2) > Math.Round(CDec(par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)), 2) Then
                    'If par.IfEmpty(di.Cells(3).Text, 0) - (CDec(par.IfEmpty(di.Cells(4).Text.Replace(".", ""), 0)) + par.IfEmpty(di.Cells(5).Text, 0)) < 0 Then
                    ControllaImporti = False
                    Response.Write("<script>alert('Impossibile procedere!\nVerificare che la cifra immessa nella colonna PAGAMENTO\nnon superi il pagabile (IMPORTO - IMP.PAGATO) della singola voce!');</script>")
                    Exit For
                End If
                totScritto += Math.Round(CDec(par.IfEmpty(CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)), 2)
            Next

            If totScritto <= 0 Then
                ControllaImporti = False
                Response.Write("<script>alert('Impossibile procedere!\nVerificare che la somma del PAGAMENTO\nnon sia pari o inferiore a zero!');</script>")

            End If


        Catch ex As Exception
            Me.lblErrore.Visible = True
            ControllaImporti = False
            lblErrore.Text = "ControllaImporti - " & ex.Message

        End Try
        Return ControllaImporti
    End Function
    Protected Sub rdbTipoPagam_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbTipoPagam.SelectedIndexChanged
        If Me.rdbTipoPagam.SelectedValue = 0 Then
            CaricaBolletta()
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridBollette.Items.Count - 2
                di = Me.DataGridBollette.Items(i)
                CType(di.Cells(4).FindControl("txtPaga"), TextBox).Enabled = False
                'CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text = ""
            Next

            Me.txtImpPagamento.Visible = True
            Me.txtNumeroPagamento.Visible = True
            Me.labelNumeroAssegno.Visible = True
            Me.txtImpPagamento.Enabled = True
            Me.txtNumeroPagamento.Enabled = True
            Me.labelNumeroAssegno.Enabled = True
            Me.labelNumeroAssegno.Visible = True
            Me.chkEcludeQS.Visible = True
            Me.Label2.Visible = True
			'modifica marco 06/09/2012
			Me.cmbTipoPagamento.Enabled = True
            Me.ButtonIncassoNonAttribuito.Visible = True
			'-------------------------

        ElseIf Me.rdbTipoPagam.SelectedValue = 1 Then
            Me.chkEcludeQS.Checked = False
            Me.chkEcludeQS_CheckedChanged(sender, e)
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridBollette.Items.Count - 2
                di = Me.DataGridBollette.Items(i)
                'SE L'IMPORTO DA PAGARE DELLA SINGOLA VOCE è NEGATIVO IMPOSTO MANUALMENTE LA CIFRA NEGATIVA E BLOCCO IL CAMPO
                If par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0) <= 0 Then
                    CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text = par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)
                    CType(di.Cells(4).FindControl("txtPaga"), TextBox).Enabled = False
                Else
                    If CDec(par.IfEmpty(di.Cells(4).Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)) < CDec(par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)) Then
                        CType(di.Cells(4).FindControl("txtPaga"), TextBox).Enabled = True
                    ElseIf CDec(par.IfEmpty(di.Cells(4).Text.Replace(".", ""), 0)) < CDec(par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)) Then
                        CType(di.Cells(4).FindControl("txtPaga"), TextBox).Enabled = True
                        CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text = ""
                    End If

                End If
            Next
            Me.txtImpPagamento.Text = ""
            Me.txtNumeroPagamento.Text = ""
            Me.txtImpPagamento.Visible = False
            Me.chkEcludeQS.Visible = False
            Me.chkEcludeQS.Checked = False
            Me.txtNumeroPagamento.Visible = False
            Me.labelNumeroAssegno.Visible = False
            Me.labelNumeroAssegno.Visible = False
            Me.Label2.Visible = False
            'modifica marco 06/09/2012
            Me.ButtonIncassoNonAttribuito.Visible = False
            '-------------------------
            AddJavascriptFunction()



        End If
    End Sub
    Public Sub AggiornaDaText()
        Try
            If CDec(par.IfEmpty(txtImpPagamento.Text, 0)) > 0 Then
                If callEpi.Value = 0 Then
                    If CDbl(txtImpPagamento.Text) = CDec(totBolletta.Value) - CDec(totPagato.Value) Then
                        Dim i As Integer = 0
                        Dim di As DataGridItem
                        For i = 0 To Me.DataGridBollette.Items.Count - 2
                            di = Me.DataGridBollette.Items(i)
                            CType(di.Cells(3).FindControl("txtPaga"), TextBox).Text = Format(CDec(par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)) - CDec(par.IfEmpty(di.Cells(4).Text.Replace(".", ""), 0)), "##,##0.00")
                        Next
                    Else
                        'CaricaBolletta()
                        RiparisciExtraMav()
                    End If
                Else
                    If CDbl(txtImpPagamento.Text) = CDec(totBolletta.Value) - CDec(totPagato.Value) Then
                        Dim i As Integer = 0
                        Dim di As DataGridItem
                        For i = 0 To Me.DataGridBollette.Items.Count - 2
                            di = Me.DataGridBollette.Items(i)
                            CType(di.Cells(3).FindControl("txtPaga"), TextBox).Text = Format(CDec(par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)) - CDec(par.IfEmpty(di.Cells(4).Text.Replace(".", ""), 0)), "##,##0.00")
                        Next
                    Else

                        'Response.Write("<script>alert('Inserire un importo pari o inferiore al pagabile!');</script>")
                        'stopImpSuperiore.Value = 1

                        RiparisciExtraMav()

                        Exit Sub
                    End If
                End If

            Else
                If Me.rdbTipoPagam.SelectedValue = 0 Then
                    Response.Write("<script>alert('Inserire un importo maggiore di zero!');</script>")
                    Exit Sub
                End If

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "AggiornaDaText - " & ex.Message
        End Try


    End Sub
    Private Sub RiparisciExtraMav()
        Try

            Dim Pagamento As Decimal = 0
            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim ImpResiduo As Decimal = CDec(par.IfEmpty(Me.txtImpPagamento.Text, 0))


            For i = 0 To Me.DataGridBollette.Items.Count - 2
                di = Me.DataGridBollette.Items(i)
                If ImpResiduo > 0 Then

                    If di.Cells(4).Text.Replace(".", "") <> di.Cells(3).Text.Replace(".", "") Then

                        If ImpResiduo > 0 And ImpResiduo >= CDec(di.Cells(3).Text.Replace(".", "")) - CDec(di.Cells(4).Text.Replace(".", "")) Then

                            CType(di.Cells(5).FindControl("txtPaga"), TextBox).Text = Format(CDec(di.Cells(3).Text.Replace(".", "")) - CDec(di.Cells(4).Text.Replace(".", "")), "##,##0.00")
                            ImpResiduo = ImpResiduo - CDec(CType(di.Cells(5).FindControl("txtPaga"), TextBox).Text.Replace(".", ""))

                        ElseIf ImpResiduo > 0 And ImpResiduo < CDec(di.Cells(3).Text.Replace(".", "")) - CDec(di.Cells(4).Text.Replace(".", "")) Then

                            CType(di.Cells(5).FindControl("txtPaga"), TextBox).Text = Format(ImpResiduo, "##,##0.00")
                            ImpResiduo = 0

                        End If

                        Pagamento = Pagamento + Math.Round(CDec(par.IfEmpty(CType(di.Cells(5).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)), 2)

                    End If
                Else
                    Exit For
                End If

            Next
            di = Me.DataGridBollette.Items(Me.DataGridBollette.Items.Count - 1)
            CType(di.Cells(5).FindControl("txtPaga"), TextBox).Text = Math.Round(Pagamento, 2)

            If CDec(par.IfEmpty(txtImpPagamento.Text, 0)) > CDec(totBolletta.Value) - CDec(totPagato.Value) Then
                If confCredito.Value = 1 Then

                    Response.Write("<script>alert('Attenzione!L\'importo di pagamento inserito di €." & txtImpPagamento.Text _
                                   & " è superiore al pagabile di €." & Format((CDec(totBolletta.Value) - CDec(totPagato.Value)), "##,##0.00") _
                                   & "!\nL\'importo di pagamento è stato aggiornato con il massimo importo pagabile!\n\nIl credito maturato " _
                                   & "verrà inserito in partita gestionale, alla chiusura della finestra!');</script>")
                    credito.Value = CDec(txtImpPagamento.Text.Replace(".", "")) - (CDec(totBolletta.Value) - CDec(totPagato.Value))
                    txtImpPagamento.Text = CDec(totBolletta.Value) - CDec(totPagato.Value)
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "RiparisciExtraMav - " & ex.Message
        End Try
    End Sub

    Public Function WriteEvent(ByVal TipoPadre As Boolean, ByVal ID_VOCE As String, ByVal CodEvento As String, ByVal Importo As Decimal, ByVal DataPagamento As String, ByVal idEvPadre As String, Optional ByVal Motivazione As String = "", Optional ByVal idMain As String = "")
        Dim idPadre As String = "null"
        Dim ConnOpenNow As Boolean = False
        Try

            If Request.QueryString("IDCON_MOR") <> "" Then
                'MOROSITA (Epifani)
                IdConnessione = Request.QueryString("IDCON_MOR")


                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                Motivazione = Motivazione & " DA MOROSITA'"

            Else
                IdConnessione = ""

                '*****************APERTURA CONNESSIONE***************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    ConnOpenNow = True
                End If
            End If


            If TipoPadre = True Then

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_EVENTI_PAGAMENTI_PARZIALI.NEXTVAL FROM DUAL"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    idPadre = lettore(0)
                End If
                lettore.Close()
                'evento padre
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI (ID,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_CONTRATTO,ID_MAIN,ID_INCASSO_EXTRAMAV,IMPORTO) " _
                    & "VALUES ( " & idPadre & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                    & "'" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "'," & idContratto.Value & "," & par.IfEmpty(idMain, "NULL") & "," & vIdIncassoExtramav & "," & par.VirgoleInPunti(par.IfEmpty(Importo, 0)) & ")"
                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI (ID,ID_EVENTO_PRINCIPALE,ID_VOCE_BOLLETTA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,IMPORTO,DATA_PAGAMENTO,ID_CONTRATTO) " _
                '                    & "VALUES ( " & idPadre & "," & idEvPadre & "," & ID_VOCE & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                '                    & "'" & CodEvento & "','" & Motivazione & "'," & par.VirgoleInPunti(par.IfEmpty(Importo, 0)) & ",'" & par.AggiustaData(DataPagamento) & "'," & idContratto.Value & ")"


            Else
                'evento figlio
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT (ID,ID_EVENTO_PRINCIPALE,ID_VOCE_BOLLETTA,IMPORTO) " _
                                    & "VALUES ( SISCOM_MI.SEQ_EVENTI_PAGAMENTI_PARZ_DETT.NEXTVAL," & idEvPadre & "," & ID_VOCE & ", " _
                                    & " " & par.VirgoleInPunti(par.IfEmpty(Importo, 0)) & ")"
                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI (ID,ID_EVENTO_PRINCIPALE,ID_VOCE_BOLLETTA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,IMPORTO,DATA_PAGAMENTO,ID_CONTRATTO) " _
                '                    & "VALUES ( SISCOM_MI.SEQ_EVENTI_PAGAMENTI_PARZIALI.NEXTVAL," & idEvPadre & "," & ID_VOCE & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                '                    & "'" & CodEvento & "','" & Motivazione & "'," & par.VirgoleInPunti(par.IfEmpty(Importo, 0)) & ",'" & par.AggiustaData(DataPagamento) & "'," & idContratto.Value & ")"

            End If

            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message

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
                Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
        End Try
        Return idPadre
    End Function
    Public Function WriteIncasso(ByVal imPagamento As Decimal) As String
        WriteIncasso = ""
        Dim ConnOpenNow As Boolean = False
        Try

            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            par.cmd.CommandText = "insert into siscom_mi.incassi_extramav (id,id_tipo_pag,motivo_pagamento," _
                                & "id_contratto,data_pagamento,riferimento_da, riferimento_a,fl_annullata,importo,id_operatore,numero_assegno,fl_annullabile) values " _
                                & "(siscom_mi.seq_incassi_extramav.nextval," & Me.cmbTipoPagamento.SelectedValue & ", " _
                                & "'" & par.PulisciStrSql(Me.txtNotePagamento.Text.ToUpper) & "', " & idContratto.Value & ", " _
                                & "'" & par.AggiustaData(Me.txtDataPagamento.Text) & "','','',0," & par.VirgoleInPunti(imPagamento) & "," & Session.Item("ID_OPERATORE") & ",'" & Replace(txtNumeroPagamento.Text, "'", "''") & "',1)"

            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "select siscom_mi.seq_incassi_extramav.currval from dual"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                WriteIncasso = par.IfNull(lettore(0), "")
            End If
            lettore.Close()



            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteIncasso - " & ex.Message

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
                Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteIncasso" & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If

        End Try



        Return WriteIncasso

    End Function
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

    'Private Function AggiustaDebitoReale(ByVal dt As Data.DataTable) As Data.DataTable
    '    AggiustaDebitoReale = dt.Clone

    '    Try

    '        'Dim returnValue() As Data.DataRow
    '        Dim negativiDisponibili As Decimal = 0
    '        Dim impnegativo As Decimal = 0
    '        For Each riga As Data.DataRow In dt.Rows
    '            If riga.Item("IMPORTO") < 0 Then
    '                impnegativo = Math.Abs(par.IfNull(riga.Item("IMPORTO"), 0))
    '                For Each r As Data.DataRow In dt.Rows
    '                    If par.IfNull(riga.Item("GRUPPO"), 4) = par.IfNull(r.Item("GRUPPO"), 4) And riga.Item("ID") <> r.Item("ID") Then
    '                        If impnegativo <= r.Item("IMPORTO") Then
    '                            r.Item("IMPORTO") = par.IfNull(r.Item("IMPORTO"), 0) - impnegativo

    '                            '18/11/2011 Puccettone: modifica per allineamento delle voci a seguito del pagamento totale della bolletta
    '                            If par.IfNull(r.Item("IMP_PAGATO"), 0) > par.IfNull(r.Item("IMPORTO"), 0) Then
    '                                r.Item("IMP_PAGATO") = par.IfNull(r.Item("IMP_PAGATO"), 0) - impnegativo
    '                            End If

    '                            impnegativo = 0

    '                        Else
    '                            impnegativo = impnegativo - par.IfNull(r.Item("IMPORTO"), 0)
    '                            r.Item("IMPORTO") = 0
    '                        End If
    '                    End If
    '                Next
    '                negativiDisponibili = negativiDisponibili + impnegativo

    '            End If
    '        Next

    '        While negativiDisponibili > 0
    '            For Each r As Data.DataRow In dt.Rows
    '                If par.IfNull(r.Item("IMPORTO"), 0) > 0 Then

    '                    If negativiDisponibili <= par.IfNull(r.Item("IMPORTO"), 0) Then
    '                        r.Item("IMPORTO") = par.IfNull(r.Item("IMPORTO"), 0) - negativiDisponibili
    '                        negativiDisponibili = 0
    '                    Else
    '                        negativiDisponibili = negativiDisponibili - par.IfNull(r.Item("IMPORTO"), 0)
    '                        r.Item("IMPORTO") = 0
    '                    End If
    '                End If
    '            Next
    '        End While


    '        For Each r0 As Data.DataRow In dt.Rows
    '            If par.IfNull(r0("IMPORTO"), 0) > 0 Then
    '                AggiustaDebitoReale.Rows.Add(r0.ItemArray)
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = "AggiustaDebitoReale - " & ex.Message
    '    End Try

    '    Return AggiustaDebitoReale

    'End Function

    Private Sub PagaRiclassificate(ByVal idBollettaRic As String, ByVal ImpPaga As Decimal)
        Try

            Dim OldIdBolletta As String = 0
            Dim Pagato As Decimal = 0
            Dim TotNegativi As Decimal = 0
            Dim TotPositivi As Decimal = 0
            Dim PercIncidenza As Decimal = 0
            '16/11/2011 PUCCETTONE MODIFY il gruppo 5 viene escluso dal pagamento delle bollette che sono state riclassificate in una bolletta di MOROSITA

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE " _
                        & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) " _
                        & "AND nvl(IMPORTO_TOTALE,0) > 0 " _
                        & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC = " & idBollettaRic & " " _
                        & "ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC"

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


                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.TIPO_VOCE,BOL_BOLLETTE.ID_TIPO,T_VOCI_BOLLETTA.GRUPPO " _
                            & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                            & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) " _
                            & "AND ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                            & "AND BOL_BOLLETTE.ID = " & r.Item("ID") & " " _
                            & "AND nvl(GRUPPO,-1) <> 5 " _
                            & "ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC, GRUPPO ASC, TIPO_VOCE ASC"
                '& "AND IMPORTO > 0 " _

                Dim row As Data.DataRow
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtMorosita As New Data.DataTable()
                da.Fill(dtMorosita)

                If r.Item("id") = -20415798 Then
                    Beep()
                End If
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
                                'se importo pagamento > importo della voce da pagare, la pago tutta
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMPORTO_RICLASSIFICATO_PAGATO = ((NVL(IMP_PAGATO,0) + NVL(IMPORTO_RICLASSIFICATO,0))-NVL(IMPORTO,0)) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                WriteEvent(False, row.Item("ID"), "F101", par.IfNull(row.Item("IMPORTO"), 0), Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                                WriteVociPagRicla(row.Item("ID"), Me.txtDataPagamento.Text, CDec(par.IfNull(row.Item("IMPORTO"), 0)), vIdIncassoExtramav, Me.tipoVocPag.Value)


                                '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************
                                ImpPaga = ImpPaga - par.IfNull(row.Item("IMPORTO"), 0)

                            ElseIf ImpPaga > 0 And ImpPaga < (par.IfNull(row.Item("IMPORTO"), 0)) Then
                                'se importo pagamento < importo della voce da pagare, la pago per l'importo pagamento che mi resta
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpPaga) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMPORTO_RICLASSIFICATO_PAGATO = " & par.VirgoleInPunti(ImpPaga) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()


                                WriteEvent(False, row.Item("ID"), "F101", ImpPaga, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                                WriteVociPagRicla(row.Item("ID"), Me.txtDataPagamento.Text, CDec(ImpPaga), vIdIncassoExtramav, Me.tipoVocPag.Value)



                                ImpPaga = ImpPaga - ImpPaga

                            ElseIf ImpPaga = 0 Then
                                Exit For
                            End If
                        ElseIf par.IfNull(row.Item("IMPORTO"), 0) > par.IfNull(row.Item("IMP_PAGATO"), 0) Then

                            If ImpPaga > 0 And ImpPaga >= Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2) Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)) & " + NVL(IMP_PAGATO,0) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()

                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET  IMPORTO_RICLASSIFICATO_PAGATO = ((NVL(IMP_PAGATO,0) + NVL(IMPORTO_RICLASSIFICATO,0))-NVL(IMPORTO,0)) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()


                                WriteEvent(False, row.Item("ID"), "F101", Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2), Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                                WriteVociPagRicla(row.Item("ID"), Me.txtDataPagamento.Text, CDec(Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)), vIdIncassoExtramav, Me.tipoVocPag.Value)



                                ImpPaga = ImpPaga - Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)

                            ElseIf ImpPaga > 0 And ImpPaga < (CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))) Then

                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpPaga) & "+ NVL(IMP_PAGATO,0) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()

                                '17/04/2012 aggiorno anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET  IMPORTO_RICLASSIFICATO_PAGATO = NVL(IMPORTO_RICLASSIFICATO_PAGATO,0) + " & par.VirgoleInPunti(ImpPaga) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()

                                WriteEvent(False, row.Item("ID"), "F101", ImpPaga, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                                WriteVociPagamenti(row.Item("ID"), Me.txtDataPagamento.Text, CDec(ImpPaga), vIdIncassoExtramav, Me.tipoVocPag.Value)
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
            Me.lblErrore.Visible = True
            lblErrore.Text = "TrovaIndiceColonna - " & ex.Message
        End Try

        Return TrovaIndiceColonna

    End Function

    Private Function TrovaTipoBol() As String

        TrovaTipoBol = ""

        par.cmd.CommandText = "select id_tipo from siscom_mi.bol_bollette where id = " & Request.QueryString("IDBOL")

        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            TrovaTipoBol = par.IfNull(lettore("ID_TIPO"), 0)
        End If
        Return TrovaTipoBol
    End Function
    Private Sub PagaBolRateizzazione(ByVal idBolletta As Integer, ByVal Importo As Decimal)
        Try
            '***********SELEZIONO TUTTE LE BOLLETTE RATEIZZATE A PARTIRE DALLA BOLLETTA DI RATEIZZAZIONE CHE VIENE PAGATA
            '***********PARTENDO SEMPRE DA QUELLE PIù VECCHIE...
            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_RATEIZZAZIONE = " _
                                & "(SELECT ID_RATEIZZAZIONE FROM SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE ID_BOLLETTA = " & idBolletta & ")" _
                                & " ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtBoll As New Data.DataTable()
            da.Fill(dtBoll)
            ImpForRateiz = Importo
            Dim idBoll As String = "0"
            For Each r As Data.DataRow In dtBoll.Rows
                If ImpForRateiz > 0 Then
                    '25/07/2012 commento il codice che scrive la data pagamento nelle bollette riclassificate

                    ''*************26/08/2011 Inserimento della data di pagamento per successivi UNDO************
                    'If idBoll <> par.IfNull(r.Item("ID"), 0) Then
                    '    idBoll = par.IfNull(r.Item("ID"), 0)
                    '    WriteDataPagBol(idBoll, idEventoPrincipale)
                    'End If


                    PagaVociBolRateizzate(r.Item("ID"))

                    '25/07/2012 se pagata completamente la riclassificata vengono allineati gli importi di pagamento
                    PagataCompletamente(par.IfNull(r.Item("ID"), 0))

                End If
            Next

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "PagaBollMorosita - " & ex.Message

        End Try
    End Sub
    Private Sub PagaVociBolRateizzate(ByVal idBolRateizzata As Integer)
        Try
            Dim OldIdBolletta As String = 0
            Dim Pagato As Decimal = 0
            Dim QVersato As Decimal = 0
            Dim PagatoReale As Decimal = 0

            '16/11/2011 PUCCETTONE MODIFY il gruppo 5 viene escluso dal pagamento delle bollette che sono state riclassificate in una bolletta di MOROSITA
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.TIPO_VOCE,BOL_BOLLETTE.ID_TIPO,GRUPPO " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                                & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) AND " _
                                & "ID_BOLLETTA = BOL_BOLLETTE.ID " _
                                & "AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                                & "AND BOL_BOLLETTE.ID = " & idBolRateizzata & " AND nvl(GRUPPO,-1) <> 5  " _
                                & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL " _
                                & " ORDER BY  GRUPPO ASC, TIPO_VOCE ASC"
            Dim row As Data.DataRow
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)

            dt = AggiustaDebitoReale(dt)



            For Each row In dt.Rows
                QVersato = 0
                'ImpForRateiz della voce di bolletta positivo eseguo algoritmo ripartizione 
                If par.IfNull(row.Item("IMPORTO"), 0) > 0 Then
                    'PagatoReale = par.IfNull(row.Item("IMP_PAGATO"), 0)
                    'PercIncidenza = Math.Round((row.Item("IMPORTO") * 100) / TotPositivi, 6)
                    'If TotNegativi > 0 Then
                    '    row.Item("IMP_PAGATO") = par.IfNull(row.Item("IMP_PAGATO"), 0) + Math.Round((TotNegativi * PercIncidenza) / 100, 6)
                    'End If
                    Pagato = par.IfNull(row.Item("IMP_PAGATO"), 0)

                    If Pagato = 0 Then
                        If ImpForRateiz > 0 And ImpForRateiz >= par.IfNull(row.Item("IMPORTO"), 0) Then
                            'se importo pagamento > importo della voce da pagare, la pago tutta

                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) _
                                                & ",IMPORTO_RICLASSIFICATO_PAGATO = NVL(IMPORTO_RICLASSIFICATO_PAGATO,0) + " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) _
                                                & " WHERE ID = " & par.IfNull(row.Item("ID"), 0)
                            par.cmd.ExecuteNonQuery()
                            WriteEvent(False, par.IfNull(row.Item("ID"), 0), "F101", par.IfNull(row.Item("IMPORTO"), 0), Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                            WriteVociPagRicla(row.Item("ID"), Me.txtDataPagamento.Text, CDec(par.IfNull(row.Item("IMPORTO"), 0)), vIdIncassoExtramav, Me.tipoVocPag.Value)


                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                'PagaRiclassificate(row.Item("ID_BOLLETTA"), ImpForRateiz)
                                PagaRiclassificate(row.Item("ID_BOLLETTA"), par.IfNull(row.Item("IMPORTO"), 0))

                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************
                            ImpForRateiz = ImpForRateiz - par.IfNull(row.Item("IMPORTO"), 0)

                        ElseIf ImpForRateiz > 0 And ImpForRateiz < (par.IfNull(row.Item("IMPORTO"), 0)) Then
                            'se importo pagamento < importo della voce da pagare, la pago per l'importo pagamento che mi resta
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpForRateiz) _
                                                & ",IMPORTO_RICLASSIFICATO_PAGATO = NVL(IMPORTO_RICLASSIFICATO_PAGATO,0) + " & par.VirgoleInPunti(ImpForRateiz) _
                                                & " WHERE ID = " & par.IfNull(row.Item("ID"), 0)
                            par.cmd.ExecuteNonQuery()
                            WriteEvent(False, row.Item("ID"), "F101", ImpForRateiz, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                            WriteVociPagRicla(row.Item("ID"), Me.txtDataPagamento.Text, CDec(ImpForRateiz), vIdIncassoExtramav, Me.tipoVocPag.Value)
                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                PagaRiclassificate(par.IfNull(row.Item("ID_BOLLETTA"), 0), ImpForRateiz)
                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************

                            ImpForRateiz = ImpForRateiz - ImpForRateiz
                        ElseIf ImpForRateiz = 0 Then
                            Exit For
                        End If
                    ElseIf par.IfNull(row.Item("IMPORTO"), 0) > par.IfNull(row.Item("IMP_PAGATO"), 0) Then

                        If ImpForRateiz > 0 And ImpForRateiz >= Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2) Then
                            QVersato = Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(QVersato) & " + NVL(IMP_PAGATO,0) " _
                                                & ",IMPORTO_RICLASSIFICATO_PAGATO = NVL(IMPORTO_RICLASSIFICATO_PAGATO,0) + " & par.VirgoleInPunti(QVersato) _
                                                & " WHERE ID = " & par.IfNull(row.Item("ID"), 0)
                            par.cmd.ExecuteNonQuery()

                            WriteEvent(False, row.Item("ID"), "F101", QVersato, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                            WriteVociPagRicla(row.Item("ID"), Me.txtDataPagamento.Text, CDec(QVersato), vIdIncassoExtramav, Me.tipoVocPag.Value)
                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                'PagaRiclassificate(row.Item("ID_BOLLETTA"), ImpForRateiz)
                                PagaRiclassificate(row.Item("ID_BOLLETTA"), QVersato)

                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************
                            ImpForRateiz = ImpForRateiz - QVersato

                        ElseIf ImpForRateiz > 0 And ImpForRateiz < (CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))) Then

                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpForRateiz) & "+ NVL(IMP_PAGATO,0) " _
                                                & ",IMPORTO_RICLASSIFICATO_PAGATO = NVL(IMPORTO_RICLASSIFICATO_PAGATO,0) + " & par.VirgoleInPunti(ImpForRateiz) _
                                                & " WHERE ID = " & row.Item("ID")
                            par.cmd.ExecuteNonQuery()
                            WriteEvent(False, row.Item("ID"), "F101", ImpForRateiz, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                            WriteVociPagRicla(row.Item("ID"), Me.txtDataPagamento.Text, CDec(ImpForRateiz), vIdIncassoExtramav, Me.tipoVocPag.Value)
                            '' ''****************AGGIORNO IMPORTO PAGATO DELLE BOLELTTE RICLASSIFICATE SE LA BOLLETTA E' DI MOROSITA'**************
                            If par.IfNull(row.Item("ID_VOCE"), 1) = 150 Or par.IfNull(row.Item("ID_VOCE"), 1) = 151 Then
                                PagaRiclassificate(row.Item("ID_BOLLETTA"), ImpForRateiz)
                            End If
                            '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************
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
    Private Sub WriteDataPagBol(ByVal idBolletta As String, ByVal idEvento As String)
        Try
            Dim dataPrec As String = ""
            par.cmd.CommandText = "SELECT DATA_PAGAMENTO FROM SISCOM_MI.BOL_BOLLETTE WHERE ID = " & idBolletta
            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If Lettore.Read Then
                dataPrec = par.IfNull(Lettore("DATA_PAGAMENTO"), "")
            End If
            Lettore.Close()
            If dataPrec <> "" Then

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_DATE_PAG (ID_EVENTO,ID_BOLLETTA,DATA_PREC) VALUES " _
                                    & "(" & idEvento & ", " & idBolletta & ", '" & dataPrec & "')"
                par.cmd.ExecuteNonQuery()
            End If

        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteDataPagBol" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property
    Private Sub CaricaCmbTipologia()
        '*****************APERTURA CONNESSIONE***************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        cmbTipoPagamento.Items.Add(New ListItem("- - -", -1))

        par.cmd.CommandText = "select id, descrizione from siscom_mi.tipo_pag_parz   WHERE UTILIZZABILE=1  order by descrizione asc"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable()
        da.Fill(dt)
        For Each row As Data.DataRow In dt.Rows
            Me.cmbTipoPagamento.Items.Add(New ListItem(par.IfNull(row.Item("descrizione"), " "), par.IfNull(row.Item("id"), -1)))
        Next

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub


#Region "Bollette completamente pagate"

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
#End Region

    'modifica marco 06/09/2012
    Protected Sub ButtonIncassoNonAttribuito_Click(sender As Object, e As System.EventArgs) Handles ButtonIncassoNonAttribuito.Click
		'proveniendo dalla maschera di selezione dell'assegno
		'impostiamo la textbox con l'importo dell'assegno
		'impostiamo come tipologia di pagamento assegno circolare
		'e disabilitiamo la modifica sia dell'importo
        'che della tipologia dell'assegno
        If Not IsNothing(Session.Item("ImportoIncassoNonAttribuito")) And Not IsNothing(Session.Item("IdIncassoNonAttribuito")) Then
            txtImpPagamento.Text = Session.Item("ImportoIncassoNonAttribuito")
            'txtImpPagamento.Enabled = False
            cmbTipoPagamento.SelectedValue = Session.Item("TipoIncassNonAtt")
            cmbTipoPagamento.Enabled = False
            Session.Remove("TipoIncassNonAtt")
            Me.tipoVocPag.Value = 7
            If cmbTipoPagamento.SelectedValue = 5 Then
                Me.txtNumeroPagamento.Style("visibility") = "visible"
                Me.labelNumeroAssegno.Style("visibility") = "visible"
            Else
                Me.txtNumeroPagamento.Style("visibility") = "hidden"
                Me.labelNumeroAssegno.Style("visibility") = "hidden"
            End If
            DettagliIncassoNonAttribuito(Session.Item("IdIncassoNonAttribuito"))

        End If
    End Sub

    Private Sub RegistraIncassiAttribuiti(ByVal idVoceBolletta As String, ByVal importoPagatoVoceBolletta As Decimal, ByVal idIncassoExtraMAV As String)
		'effettua l'insert di tutte le voci delle bollette interessate nella
		'tabella incassi_Attribuiti
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INCASSI_ATTRIBUITI (ID_INCASSO_NON_ATTR,ID_INCASSO_EXTRAMAV,ID_VOCE_BOLLETTA,IMPORTO) " _
                                & "VALUES (" & Session.Item("IdIncassoNonAttribuito") & "," & idIncassoExtraMAV & "," & idVoceBolletta & "," & Replace(CStr(importoPagatoVoceBolletta), ",", ".") & ")"
            par.cmd.ExecuteNonQuery()
            '15/10/2012 nuova gestione incassi non attribuibili
            If Not IsNothing(Session.Item("IdIncassoNonAttribuito")) And Not IsNothing(Session.Item("ImportoIncassoNonAttribuito")) Then
                par.cmd.CommandText = "update siscom_mi.incassi_non_attribuibili set " _
                                    & "importo_residuo = importo_residuo - " & Replace(CStr(importoPagatoVoceBolletta), ",", ".") _
                                    & " where id = " & Session.Item("IdIncassoNonAttribuito")
                par.cmd.ExecuteNonQuery()
            End If
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Incassi attribuiti - " & ex.Message
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
                Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- RegistraIncassiAttribuiti" & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub
    Private Sub WriteVociPagamenti(ByVal idVoceBolletta As Integer, ByVal dataPagamento As String, ByVal importo As Decimal, ByVal idIncassoExt As Integer, ByVal tipo As Integer)

        Try


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                & "(" & idVoceBolletta & ",'" & par.AggiustaData(dataPagamento) & "'," & par.VirgoleInPunti(importo) & "," & tipo & "," & idIncassoExt & ")"
            par.cmd.ExecuteNonQuery()



        Catch ex As Exception

            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If

            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteVociPagamenti" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Private Sub WriteVociPagRicla(ByVal idVoceBolletta As Integer, ByVal dataPagamento As String, ByVal importo As Decimal, ByVal idIncassoExt As Integer, ByVal tipo As Integer)

        Try


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI2 (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                & "(" & idVoceBolletta & ",'" & par.AggiustaData(dataPagamento) & "'," & par.VirgoleInPunti(importo) & "," & tipo & "," & idIncassoExt & ")"
            par.cmd.ExecuteNonQuery()



        Catch ex As Exception

            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If

            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteVociPagaRicla" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Public Sub AggiornaDaText_IncassiNonAttribuiti()
		'è la stessa function di aggiornadatext
		'ma deve essere eseguita solo se si proviene dal bottone
		'conferma
        If clickConferma.Value = 1 Then
            Try
                If CDec(par.IfEmpty(txtImpPagamento.Text, 0)) > 0 Then
                    If callEpi.Value = 0 Then
                        If CDbl(txtImpPagamento.Text) = CDec(totBolletta.Value) - CDec(totPagato.Value) Then
                            Dim i As Integer = 0
                            Dim di As DataGridItem
                            For i = 0 To Me.DataGridBollette.Items.Count - 2
                                di = Me.DataGridBollette.Items(i)
                                CType(di.Cells(3).FindControl("txtPaga"), TextBox).Text = Format(CDec(par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)) - CDec(par.IfEmpty(di.Cells(4).Text.Replace(".", ""), 0)), "##,##0.00")
                            Next
                        Else
                            'CaricaBolletta()
                            RiparisciExtraMav()
                        End If
                    Else
                        If CDbl(txtImpPagamento.Text) = CDec(totBolletta.Value) - CDec(totPagato.Value) Then
                            Dim i As Integer = 0
                            Dim di As DataGridItem
                            For i = 0 To Me.DataGridBollette.Items.Count - 2
                                di = Me.DataGridBollette.Items(i)
                                CType(di.Cells(3).FindControl("txtPaga"), TextBox).Text = Format(CDec(par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)) - CDec(par.IfEmpty(di.Cells(4).Text.Replace(".", ""), 0)), "##,##0.00")
                            Next
                        Else

                            'Response.Write("<script>alert('Inserire un importo pari o inferiore al pagabile!');</script>")
                            'stopImpSuperiore.Value = 1

                            RiparisciExtraMav()

                            Exit Sub
                        End If
                    End If

                Else
                    If Me.rdbTipoPagam.SelectedValue = 0 Then
                        Response.Write("<script>alert('Inserire un importo maggiore di zero!');</script>")
                        Exit Sub
                    End If

                End If

            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = "AggiornaDaText - " & ex.Message
            End Try
        End If
    End Sub

	'---------------------------

    Protected Sub cmbTipoPagamento_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoPagamento.SelectedIndexChanged
        '03/10/2012 modifica marco
        'visualizzo il numero assegno solamente se è stato selezionato l'assegno come tipologia del pagamento
        If cmbTipoPagamento.SelectedValue = 5 Then
            Me.txtNumeroPagamento.Visible = True
            Me.labelNumeroAssegno.Visible = True
        Else
            Me.txtNumeroPagamento.Visible = False
            Me.labelNumeroAssegno.Visible = False
        End If

        If cmbTipoPagamento.SelectedValue = 8 Then
            Me.chkEcludeQS.Checked = True
            Me.chkEcludeQS_CheckedChanged(sender, e)
        Else
            If Me.chkEcludeQS.Checked = True Then
                Me.chkEcludeQS.Checked = False
                Me.chkEcludeQS_CheckedChanged(sender, e)
            End If

        End If
    End Sub
    Private Sub DettagliIncassoNonAttribuito(ByVal idIncassoNonAttribuito As Integer)
        Dim ConnOpenNow As Boolean = False

        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If
            par.cmd.CommandText = "select data_incasso,trim(to_char(importo_residuo,'999G999G990D99')) as importo_residuo " _
                & "from siscom_mi.incassi_non_attribuibili where id = " & idIncassoNonAttribuito
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore("importo_residuo"), 0) > 0 Then
                    Me.lblIncassNonAtt.Text = " del " & par.FormattaData(par.IfNull(lettore("data_incasso"), "00000000")) & " disponibilità €." & par.IfNull(lettore("importo_residuo"), "0,00")
                Else
                    Response.Write("<script>alert(Disponibilità incasso €. 0,00!);</script>")
                End If
            End If
            lettore.Close()
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "Incassi attribuiti - " & ex.Message
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
                Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- DettagliIncassoNonAttribuito" & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        End Try

    End Sub

    Protected Sub chkEcludeQS_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkEcludeQS.CheckedChanged
        CaricaBolletta(True)
        If Me.chkEcludeQS.Checked = True Then
            testo.Visible = True
        Else
            testo.Visible = False
        End If
    End Sub
End Class
