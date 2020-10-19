
Partial Class Contratti_Pagamenti_DettManualeRiclass
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim idEventoPrincipale As String
    Public connData As CM.datiConnessione = Nothing
    Private Property lettore As Data.OracleClient.OracleDataReader

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        clickConferma.Value = 0

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsNothing(Session.Item("PGMANUALE" & Request.QueryString("CONN"))) Then
            Me.connData = New CM.datiConnessione(par, False, False)
        Else
            Me.connData = CType(HttpContext.Current.Session.Item("PGMANUALE" & Request.QueryString("CONN")), CM.datiConnessione)
            par.SettaCommand(par)
        End If

        If Not IsPostBack Then
            Me.rdbTipoPagam.SelectedValue = 0
            Me.lblNumBolletta.Text = Request.QueryString("NUMBOL")
            Me.ImportoIncasso.Value = Request.QueryString("IMPORTO")
            Me.PagatoDaChiamata.Value = Request.QueryString("TOTPAGATO")
            vIdConnessione.Value = Request.QueryString("CONN")
            txtDataPagamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtImpPagamento.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);return false;")
            txtImpPagamento.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            CaricaCmbTipologia()
            CaricaBolletta()
            AddJavascriptFunction()
            If Request.QueryString("MODIF") = "1" Then
                If ControllaSeInBollVociPag() <> 0 Then
                    VisualizzaModifyIncasso()
                    btnConfirm.Visible = False
                End If
            End If
        End If
    End Sub

    Private Function ControllaSeInBollVociPag() As Integer
        Dim riclassPagata As Integer = 0
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE ID_INCASSO_EXTRAMAV=" & Request.QueryString("IDINC") & " AND ID_VOCE_BOLLETTA IN (SELECT ID FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & Request.QueryString("IDBOL") & ")"
            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If reader.Read Then
                riclassPagata = par.IfNull(reader("ID_VOCE_BOLLETTA"), 0)
            End If
            reader.Close()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ControllaSeInBollVociPag - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try

        Return riclassPagata
    End Function

    Private Sub VisualizzaModifyIncasso()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If

            par.cmd.CommandText = "SELECT SUM(IMPORTO_PAGATO) AS impPAG FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE ID_INCASSO_EXTRAMAV=" & Request.QueryString("IDINC") & " and id_voce_bolletta in (select id from siscom_mi.bol_bollette_voci where id_bolletta = " & Request.QueryString("IDBOL") & ")"
            txtImpPagamento.Text = par.cmd.ExecuteScalar

            txtImpPagamento.Text = Format(CDec(txtImpPagamento.Text), "##,##0.00")

           
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,BOL_BOLLETTE_VOCI_PAGAMENTI.*, T_VOCI_BOLLETTA.DESCRIZIONE,NUMERO_ASSEGNO," _
			& "(case when (BOL_BOLLETTE_VOCI.importo - nvl(imp_pagato,0))<0 then 0 else priorita end)as ordine, " _
                    & "BOL_BOLLETTE.ID_TIPO,BOL_BOLLETTE.ID_CONTRATTO,GRUPPO,T_VOCI_BOLLETTA.TIPO_VOCE, '' AS RESIDUO " _
                    & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI, SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.INCASSI_EXTRAMAV, SISCOM_MI.T_VOCI_BOLLETTA,SISCOM_MI.BOL_BOLLETTE " _
                    & "WHERE INCASSI_EXTRAMAV.ID=BOL_BOLLETTE_VOCI_PAGAMENTI.ID_INCASSO_EXTRAMAV AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE_VOCI_PAGAMENTI.ID_VOCE_BOLLETTA=BOL_BOLLETTE_VOCI.ID " _
                    & "AND ID_INCASSO_EXTRAMAV=" & Request.QueryString("IDINC") & " AND ID_VOCE = T_VOCI_BOLLETTA.ID AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = " & Request.QueryString("IDBOL") _
                    & " ORDER BY ORDINE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)


            ' dt = AggiustaDebitoReale(dt)
            Dim row As Data.DataRow
            totBolletta.Value = 0
            totPagato.Value = 0
            totResiduo.Value = 0
            For Each row In dt.Rows
                txtDataPagamento.Text = par.FormattaData(par.IfNull(row.Item("DATA_PAGAMENTO"), ""))
                txtNotePagamento.Text = par.IfNull(row.Item("NOTE"), "")
                cmbTipoPagamento.SelectedValue = par.IfNull(row.Item("ID_TIPO_INCASSO_EXTRAMAV"), 0)
                txtNumeroPagamento.Text = par.IfNull(row.Item("NUMERO_ASSEGNO"), "")
                txtDataValuta.Text = par.FormattaData(par.IfNull(row.Item("DATA_REGISTRAZIONE"), ""))
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

            Dim sumImpPagato As Decimal = 0


            For Each r As Data.DataRow In dt.Rows
                If par.IfEmpty(r.Item("ID_BOLLETTA").ToString, 0) <> 0 Then
                    For Each ri As DataGridItem In DataGridBollette.Items
                        If par.IfEmpty(ri.Cells(par.IndDGC(DataGridBollette, "ID")).Text.Replace("&nbsp;", ""), "") <> "" Then
                            If par.IfEmpty(ri.Cells(par.IndDGC(DataGridBollette, "ID")).Text.Replace("&nbsp;", ""), "") = r.Item("id").ToString Then
                                sumImpPagato = sumImpPagato + CDec(par.IfEmpty(r.Item("importo_pagato").ToString, 0))
                                CType(ri.Cells(par.IndDGC(DataGridBollette, "PAGAMENTO €.")).FindControl("txtPaga"), TextBox).Text = Format(CDec(par.IfEmpty(r.Item("importo_pagato").ToString, 0)), "##,##0.00")
                            End If
                        Else
                            If par.IfEmpty(ri.Cells(par.IndDGC(DataGridBollette, "ID")).Text.Replace("&nbsp;", ""), "") = "" Then
                                CType(ri.Cells(par.IndDGC(DataGridBollette, "PAGAMENTO €.")).FindControl("txtPaga"), TextBox).Text = sumImpPagato  ' Format(CDec(par.IfEmpty(r.Item("importo_pagato").ToString, 0)), "##,##0.00")

                            End If

                        End If

            Next
                End If
            Next


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

            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: VisualizzaModifyIncasso - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaCmbTipologia()
        '*****************APERTURA CONNESSIONE***************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()
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
        Data.OracleClient.OracleConnection.ClearAllPools()

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
                par.OracleConn = CType(HttpContext.Current.Session.Item("PGMANUALE" & vIdConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                par.cmd = par.OracleConn.CreateCommand()

                'RIPRENDO LA TRANSAZIONE
                'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                'par.cmd.Transaction = par.myTrans

                callEpi.Value = 1


            Else
                IdConnessione = ""
                Me.connData = CType(HttpContext.Current.Session.Item("PGMANUALE" & Request.QueryString("CONN")), CM.datiConnessione)
                Me.connData.RiempiPar(par)

                '*****************APERTURA CONNESSIONE***************
                'If par.OracleConn.State = Data.ConnectionState.Closed Then
                '    par.OracleConn.Open()
                '    par.cmd = par.OracleConn.CreateCommand()
                'End If
            End If

            Dim condEscludiQSind As String = ""
            'If Me.chkEcludeQS.Checked = True Then

            condEscludiQSind = " and gruppo <> 5 "
            'End If

            txtDataPagamento.Text = Request.QueryString("DATAPAG")
            txtNotePagamento.Text = Replace(Request.QueryString("NOTE"), "\'", "'")
            cmbTipoPagamento.SelectedValue = Request.QueryString("TIPOPAG")
            txtNumeroPagamento.Text = Request.QueryString("ASS")
            txtDataValuta.Text = Request.QueryString("DATAREG")

            If cmbTipoPagamento.SelectedValue = 5 Then
                Me.txtNumeroPagamento.Visible = True
                Me.labelNumeroAssegno.Visible = True
            Else
                Me.txtNumeroPagamento.Visible = False
                Me.labelNumeroAssegno.Visible = False
            End If

            '*************12/07/2011 modifico la query perchè RESIDUO con importi negativi viene errato...TRIM(TO_CHAR(NVL(NVL(IMPORTO,0)-NVL(IMP_PAGATO,0),0),'9G999G999G990D99')) AS RESIDUO
            '*************12/07/2011 lo calcolo dopo che sui risultati inseriti nella dt, eseguo la funzione di AggiustaDebitoReale
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*, T_VOCI_BOLLETTA.DESCRIZIONE,(case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end)as ordine, " _
                                & "BOL_BOLLETTE.ID_TIPO,BOL_BOLLETTE.ID_CONTRATTO,GRUPPO,T_VOCI_BOLLETTA.TIPO_VOCE, '' AS RESIDUO " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA,SISCOM_MI.BOL_BOLLETTE " _
                                & "WHERE ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_VOCE = T_VOCI_BOLLETTA.ID AND ID_BOLLETTA = " & Request.QueryString("IDBOL") & condEscludiQSind _
                                & " ORDER BY ORDINE ASC"


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
                Me.cmbTipoPagamento.Enabled = False
                Me.labelNumeroAssegno.Visible = False
                '-------------------------


            End If

            '*********************CHIUSURA CONNESSIONE**********************
            'If IdConnessione = "" Then
            '    par.OracleConn.Close()
            '    Data.OracleClient.OracleConnection.ClearAllPools()
            'End If

            If callChk = False And Me.cmbTipoPagamento.SelectedValue <> 8 Then
                Me.vIdIncassoExtramav = ""
                'Me.cmbTipoPagamento.SelectedValue = "-1"
                'Me.txtNotePagamento.Text = ""
            Else
                If Not String.IsNullOrEmpty(txtImpPagamento.Text) Then
                    RipartisciExtraMav()
                End If
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- btnConfirm_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
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

    Public Function WriteIncasso(ByVal imPagamento As Decimal) As String
        WriteIncasso = ""
        Dim ConnOpenNow As Boolean = False
        Try

            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
                ConnOpenNow = True
            End If
            par.cmd.CommandText = "select siscom_mi.seq_incassi_extramav.nextval from dual"
            WriteIncasso = par.cmd.ExecuteScalar

            par.cmd.CommandText = "insert into siscom_mi.incassi_extramav (id,id_tipo_pag,motivo_pagamento," _
                           & "id_contratto,data_pagamento,data_registrazione,riferimento_da, riferimento_a,fl_annullata,importo," _
                           & "id_operatore,numero_assegno,fl_annullabile,data_ora) values " _
                           & "(" & WriteIncasso & "," & Me.cmbTipoPagamento.SelectedValue & ", " _
                           & "'" & par.PulisciStrSql(Me.txtNotePagamento.Text.ToUpper) & "', " & idContratto.Value & ", " _
                           & "'" & par.AggiustaData(Me.txtDataPagamento.Text) & "','" & par.AggiustaData(Me.txtDataValuta.Text) & "','','',0," & par.VirgoleInPunti(imPagamento) & "," _
                           & Session.Item("ID_OPERATORE") & ",'" & par.PulisciStrSql(txtNumeroPagamento.Text.ToUpper) & "',1,'" & Format(Now, "yyyyMMddHHmmss") & "')"

            par.cmd.ExecuteNonQuery()



            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Data.OracleClient.OracleConnection.ClearAllPools()

            End If


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteIncasso - " & ex.Message

            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Data.OracleClient.OracleConnection.ClearAllPools()
            Else
                '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Rollback()
                End If
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteIncasso" & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
                Data.OracleClient.OracleConnection.ClearAllPools()


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

    Private Sub RipartisciExtraMav()
        Try

            Dim Pagamento As Decimal = 0
            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim ImpResiduo As Decimal = CDec(par.IfEmpty(Me.txtImpPagamento.Text, 0))

            If ImpResiduo <> 0 Then
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

                'If CDec(txtImpPagamento.Text) > CDec(totBolletta.Value) - CDec(totPagato.Value) Then
                '    If confCredito.Value = 1 Then

                '        Response.Write("<script>alert('Attenzione!L\'importo di pagamento inserito di €." & txtImpPagamento.Text _
                '                       & " è superiore al pagabile di €." & Format((CDec(totBolletta.Value) - CDec(totPagato.Value)), "##,##0.00") _
                '                       & "!\nL\'importo di pagamento è stato aggiornato con il massimo importo pagabile!\n\nIl credito maturato " _
                '                       & "verrà inserito in partita gestionale, alla chiusura della finestra!');</script>")
                '        credito.Value = CDec(txtImpPagamento.Text.Replace(".", "")) - (CDec(totBolletta.Value) - CDec(totPagato.Value))
                '        txtImpPagamento.Text = CDec(totBolletta.Value) - CDec(totPagato.Value)
                '        Exit Sub
                '    End If
                'End If
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "RipartisciExtraMav - " & ex.Message
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
                            RipartisciExtraMav()
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

                            RipartisciExtraMav()

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
    Protected Sub btnConfirm_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnConfirm.Click

        clickConferma.Value = 1

        If Me.rdbTipoPagam.SelectedValue = 0 Then
            RipartisciExtraMav()
        End If

        Try
            If Me.cmbTipoPagamento.SelectedValue = "-1" Then
                Response.Write("<script>alert('Selezionare la tipologia del pagamento!');</script>")
                Exit Sub
            End If

            If stopImpSuperiore.Value = 0 Then

                If isCredito.Value = 1 And confCredito.Value = 0 Then
                    CaricaBolletta()
                    'Me.txtDataPagamento.Text = ""
                    Me.txtImpPagamento.Text = ""
                    'Me.txtNumeroPagamento.Text = ""
                    Me.rdbTipoPagam.SelectedValue = 0
                    Me.txtImpPagamento.Visible = True
                    'Me.txtNumeroPagamento.Visible = True
                    Me.txtImpPagamento.Enabled = True
                    'Me.txtNumeroPagamento.Enabled = True
                    'Me.labelNumeroAssegno.Visible = True
                    Me.Label2.Visible = True
                    isCredito.Value = 0
                    confCredito.Value = 0

                    'Me.cmbTipoPagamento.Enabled = True


                    Me.tipoVocPag.Value = 2

                    'Me.labelNumeroAssegno.Visible = False
                    'Me.txtNumeroPagamento.Visible = False
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

                    Me.cmbTipoPagamento.Enabled = True


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
                'If Not IsNothing(Session.Item("IdIncassoNonAttribuito")) And Not IsNothing(Session.Item("ImportoIncassoNonAttribuito")) Then
                '    If CDec(Me.txtImpPagamento.Text.Replace(".", "")) > CDec(Session.Item("ImportoIncassoNonAttribuito")) Then
                '        Response.Write("<script>alert('L\'importo del pagamento non può superare quello dell\'incasso!');</script>")
                '        Exit Sub

                '    End If

                'End If

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
                        par.OracleConn = CType(HttpContext.Current.Session.Item("PGMANUALE" & vIdConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                        par.cmd = par.OracleConn.CreateCommand()

                        'RIPRENDO LA TRANSAZIONE
                        'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Data.OracleClient.OracleTransaction)
                        'par.cmd.Transaction = par.myTrans


                    End If

                Else
                    'IdConnessione = ""
                    ''*****************APERTURA CONNESSIONE***************
                    'If Not IsNothing(Me.connData) Then
                    '    Me.connData.RiempiPar(par)
                    'End If

                    '*************************APERTURA TRANSAZIONE****************************
                    'connData.apriTransazione()
                    If IsNothing(Session.Item("PGMANUALE" & Request.QueryString("CONN"))) Then
                        Me.connData = New CM.datiConnessione(par, False, False)
                    Else
                        Me.connData = CType(HttpContext.Current.Session.Item("PGMANUALE" & Request.QueryString("CONN")), CM.datiConnessione)
                        Me.connData.RiempiPar(par)
                    End If
                End If


                Dim TipoBol As String = TrovaTipoBol()

                Dim i As Integer = 0
                Dim di As DataGridItem
                Dim ImpPagato As Decimal = 0

                If Me.rdbTipoPagam.SelectedValue = 0 Then
                    If String.IsNullOrEmpty(Request.QueryString("IDINCESEGUITO")) Or Request.QueryString("IDINCESEGUITO") = "0" Then
                        'vIdIncassoExtramav = WriteIncasso(CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(credito.Value.ToString.Replace(".", ""), 0)))
                        vIdIncassoExtramav = WriteIncasso(ImportoIncasso.Value)


                    Else
                        vIdIncassoExtramav = Request.QueryString("IDINCESEGUITO")
                    End If

                    'idEventoPrincipale = WriteEvent(True, "null", "F101", CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)), Me.txtDataPagamento.Text, "null", "EVENTO PRINCIPALE " & Me.rdbTipoPagam.SelectedItem.Text.ToUpper & "")
                Else
                    Dim totImp As Decimal = 0
                    For i = 0 To Me.DataGridBollette.Items.Count - 2
                        di = Me.DataGridBollette.Items(i)
                        totImp = totImp + par.IfEmpty(CType(di.Cells(5).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)
                    Next
                    If String.IsNullOrEmpty(Request.QueryString("IDINCESEGUITO")) Or Request.QueryString("IDINCESEGUITO") = "0" Then
                        'vIdIncassoExtramav = WriteIncasso(totImp)
                        vIdIncassoExtramav = WriteIncasso(ImportoIncasso.Value)


                    Else
                        vIdIncassoExtramav = Request.QueryString("IDINCESEGUITO")
                    End If

                    'idEventoPrincipale = WriteEvent(True, "null", "F101", CDec(par.IfEmpty(totImp, 0)), Me.txtDataPagamento.Text, "null", "EVENTO PRINCIPALE " & Me.rdbTipoPagam.SelectedItem.Text.ToUpper & "")
                End If

                '*************26/08/2011 Inserimento della data di pagamento per successivi UNDO************
                WriteDataPagBol(Request.QueryString("IDBOL"), vIdIncassoExtramav)
                Dim importoDaPagare As Decimal = 0
                For i = 0 To Me.DataGridBollette.Items.Count - 2
                    di = Me.DataGridBollette.Items(i)
                    importoDaPagare += CDec(par.IfEmpty(CType(di.Cells(6).FindControl("txtPaga"), TextBox).Text, 0))

                    If par.IfEmpty(CType(di.Cells(5).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0) > 0 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                        & "IMP_PAGATO = " & par.IfEmpty(par.VirgoleInPunti(CDec(par.IfEmpty(CType(di.Cells(6).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(di.Cells(TrovaIndiceColonna(DataGridBollette, "IMP_PAGATO")).Text.Replace(".", ""), 0))), "Null") _
                        & " WHERE ID = " & di.Cells(0).Text
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMPORTO_RICLASSIFICATO_PAGATO = (NVL(IMPORTO_RICLASSIFICATO_PAGATO,0) + " & par.VirgoleInPunti(par.IfEmpty(CType(di.Cells(6).FindControl("txtPaga"), TextBox).Text, 0)) & ") " _
                        & " WHERE ID = " & di.Cells(0).Text
                        par.cmd.ExecuteNonQuery()
                    End If

                    'End If
                    ImpPagato = par.IfEmpty(((CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""))), "0")

                    If Me.rdbTipoPagam.SelectedValue = 0 And ImpPagato > 0 Then
                        'WriteEvent(False, di.Cells(0).Text, "F101", ImpPagato, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE MANUALE - PAGAMENTO SINGOLA VOCE")
                        WriteVociPagamenti(di.Cells(0).Text, Me.txtDataPagamento.Text, txtDataValuta.Text, CDec(ImpPagato), vIdIncassoExtramav, Me.tipoVocPag.Value)

                    ElseIf Me.rdbTipoPagam.SelectedValue = 1 And ImpPagato > 0 Then
                        'WriteEvent(False, di.Cells(0).Text, "F101", ImpPagato, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE MANUALE - PAGAMENTO SINGOLA VOCE")
                        WriteVociPagamenti(di.Cells(0).Text, Me.txtDataPagamento.Text, txtDataValuta.Text, CDec(ImpPagato), vIdIncassoExtramav, Me.tipoVocPag.Value)
                    End If

                Next


                '03-11-2014 AGGIORNO BOLLETTA MOROSITA
                Dim idVoceMor As Long = 0
                Dim idMor As Long = 0
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_VOCE=150 AND ID_BOLLETTA=" & Request.QueryString("IDMOR")
                Dim myReaderVM As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReaderVM.Read Then
                    idVoceMor = par.IfNull(myReaderVM("ID"), -1)
                    idMor = par.IfNull(myReaderVM("ID_BOLLETTA"), -1)
                End If
                myReaderVM.Close()

                PagaMorosita(idMor, importoDaPagare)

                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE " _
                                        & " SET DATA_VALUTA='" & par.AggiustaData(txtDataValuta.Text) _
                                           & "',operatore_pag='" & par.PulisciStrSql(Session.Item("OPERATORE")) _
                                           & "',DATA_PAGAMENTO='" & par.AggiustaData(txtDataPagamento.Text) _
                                           & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1," _
                                           & "DATA_INS_PAGAMENTO='" & Format(Now, "yyyyMMddHHmmss") & "' WHERE ID=" & Request.QueryString("IDMOR")
                par.cmd.ExecuteNonQuery()
                'FINE 03-11-2014


                'AGGIORNO BOLLETTE con data_valuta e data_inserimento_pagamento
                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE " _
                                        & " SET DATA_VALUTA='" & par.AggiustaData(txtDataValuta.Text) _
                                           & "',operatore_pag='" & par.PulisciStrSql(Session.Item("OPERATORE")) _
                                           & "',DATA_PAGAMENTO='" & par.AggiustaData(txtDataPagamento.Text) _
                                           & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1," _
                                           & "DATA_INS_PAGAMENTO='" & Format(Now, "yyyyMMddHHmmss")



                If IdConnessione <> "" Then
                    par.cmd.CommandText = par.cmd.CommandText & "',ID_BOLLETTA_RIC=Null " _
                                                              & " ,ID_MOROSITA=Null " _
                                                              & " WHERE ID=" & Request.QueryString("IDBOL")
                Else
                    par.cmd.CommandText = par.cmd.CommandText & "' WHERE ID=" & Request.QueryString("IDBOL")
                End If

                par.cmd.ExecuteNonQuery()

                Me.tipoVocPag.Value = 2

                Session.Add("idIncassoDaMor", vIdIncassoExtramav)
                If IsNothing(Session.Item("PagInMor")) Then
                    Session.Add("PagInMor", TotStoPerInserire.Value)
                Else
                    Dim newtot As Decimal = 0
                    newtot = Session.Item("PagInMor")
                    newtot += TotStoPerInserire.Value
                    Session.Add("PagInMor", newtot)
                End If

                '----------------------------
                Response.Write("<script>alert('Operazione eseguita correttamente!Il pagamento è stato memorizzato!');</script>")


                '*********************COMMIT OPERAZIONI ESEGUITE E CHIUSURA CONNESSIONE**********************
                'If IdConnessione = "" Then
                '    connData.chiudiTransazione(True)
                'End If
            Else
                stopImpSuperiore.Value = 0
            End If

            CaricaBolletta()
            PagataCompletamente(Request.QueryString("IDBOL"))

            Me.txtImpPagamento.Text = ""

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- btnConfirm_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try
    End Sub

    Private Sub PagaMorosita(ByVal idBollettaMor As Long, ByVal ImpPaga As Decimal)
        Try

            Dim OldIdBolletta As String = 0
            Dim Pagato As Decimal = 0
            Dim TotNegativi As Decimal = 0
            Dim TotPositivi As Decimal = 0
            Dim PercIncidenza As Decimal = 0

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE " _
                        & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) " _
                        & "AND nvl(IMPORTO_TOTALE,0) > 0 " _
                        & "AND BOL_BOLLETTE.ID = " & idBollettaMor & " " _
                        & "ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtBollette As New Data.DataTable
            da.Fill(dtBollette)
            Dim idBolletta As String = "0"

            For Each r As Data.DataRow In dtBollette.Rows

                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.TIPO_VOCE,BOL_BOLLETTE.ID_TIPO,T_VOCI_BOLLETTA.GRUPPO " _
                            & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                            & "WHERE FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) " _
                            & "AND ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                            & "AND BOL_BOLLETTE.ID = " & r.Item("ID") & " " _
                            & "AND nvl(GRUPPO,-1) <> 5 and id_voce=150 " _
                            & "ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC, GRUPPO ASC, TIPO_VOCE ASC"
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
                        Pagato = par.IfNull(row.Item("IMP_PAGATO"), 0)
                        If Pagato = 0 Then
                            If ImpPaga > 0 And ImpPaga >= par.IfNull(row.Item("IMPORTO"), 0) Then
                                'se importo pagamento > importo della voce da pagare, la pago tutta
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()
                                'WriteEvent(False, row.Item("ID"), "F101", par.IfNull(row.Item("IMPORTO"), 0), Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                                WriteVociPagamenti2(row.Item("ID"), Me.txtDataPagamento.Text, CDec(par.IfNull(row.Item("IMPORTO"), 0)), vIdIncassoExtramav, Me.tipoVocPag.Value)


                                '' ''****************FINE AGGIORNAMENTO BOLLETTA DI MOROSITA'**************
                                ImpPaga = ImpPaga - par.IfNull(row.Item("IMPORTO"), 0)

                            ElseIf ImpPaga > 0 And ImpPaga < (par.IfNull(row.Item("IMPORTO"), 0)) Then
                                'se importo pagamento < importo della voce da pagare, la pago per l'importo pagamento che mi resta
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpPaga) _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()

                                'WriteEvent(False, row.Item("ID"), "F101", ImpPaga, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                                WriteVociPagamenti2(row.Item("ID"), Me.txtDataPagamento.Text, CDec(ImpPaga), vIdIncassoExtramav, Me.tipoVocPag.Value)

                                ImpPaga = ImpPaga - ImpPaga

                            ElseIf ImpPaga = 0 Then
                                Exit For
                            End If
                        ElseIf par.IfNull(row.Item("IMPORTO"), 0) > par.IfNull(row.Item("IMP_PAGATO"), 0) Then

                            If ImpPaga > 0 And ImpPaga >= Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2) Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)) & " + NVL(IMP_PAGATO,0) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()

                                'WriteEvent(False, row.Item("ID"), "F101", Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2), Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                                WriteVociPagamenti2(row.Item("ID"), Me.txtDataPagamento.Text, CDec(Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)), vIdIncassoExtramav, Me.tipoVocPag.Value)

                                ImpPaga = ImpPaga - Math.Round((CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))), 2)

                            ElseIf ImpPaga > 0 And ImpPaga < (CDec(par.IfNull(row.Item("IMPORTO"), 0)) - CDec(par.IfNull(row.Item("IMP_PAGATO"), 0))) Then

                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMP_PAGATO = " & par.VirgoleInPunti(ImpPaga) & "+ NVL(IMP_PAGATO,0) " _
                                                    & " WHERE ID = " & row.Item("ID")
                                par.cmd.ExecuteNonQuery()

                                'WriteEvent(False, row.Item("ID"), "F101", ImpPaga, Me.txtDataPagamento.Text, idEventoPrincipale, "IMPUTAZIONE RICLASSIFICATE")
                                WriteVociPagamenti2(row.Item("ID"), Me.txtDataPagamento.Text, CDec(ImpPaga), vIdIncassoExtramav, Me.tipoVocPag.Value)
                                ImpPaga = 0
                            End If

                        End If

                    End If

                Next
                da.Dispose()
                dtMorosita.Dispose()

                Dim PagataIntera As Boolean = False
                par.cmd.CommandText = "select sum(importo) as importo_totale , sum(nvl(imp_pagato,0)) as totale_pagato from siscom_mi.bol_bollette_voci where id_bolletta = " & par.IfNull(r.Item("ID"), 0)
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    If par.IfNull(lettore("importo_totale"), 0) = par.IfNull(lettore("totale_pagato"), 0) Then
                        PagataIntera = True
                    End If
                End If
                lettore.Close()

                If PagataIntera = True Then
                    '***********************memorizzo l'importo pagato inserito dall'utente che ha generato un pagamento totale*********************************
                    par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato_bak = imp_pagato where id_bolletta = " & par.IfNull(r.Item("ID"), 0)
                    par.cmd.ExecuteNonQuery()

                    '*********************aggiorno l'importo pagato con l'importo della voce (anche per le negative) *******************************************
                    par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato = importo where id_bolletta = " & par.IfNull(r.Item("ID"), 0)
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            dtBollette.Dispose()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- PagaRiclassificate" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try
    End Sub

    Private Function TrovaTipoBol() As String

        TrovaTipoBol = ""

        par.cmd.CommandText = "select id_tipo from siscom_mi.bol_bollette where id = " & Request.QueryString("IDBOL")

        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            TrovaTipoBol = par.IfNull(lettore("ID_TIPO"), 0)
        End If
        Return TrovaTipoBol
    End Function

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

    Private Sub WriteVociPagamenti(ByVal idVoceBolletta As Integer, ByVal dataPagamento As String, ByVal dataRegistrazione As String, ByVal importo As Decimal, ByVal idIncassoExt As Integer, ByVal tipo As Integer)

        Try

            par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,DATA_REGISTRAZIONE,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV,DATA_VALUTA) VALUES " _
                           & "(" & idVoceBolletta & ",'" & par.AggiustaData(dataPagamento) & "','" & par.AggiustaData(dataRegistrazione) & "'," & par.VirgoleInPunti(importo) & "," & tipo & "," & idIncassoExt & ",'" & par.AggiustaData(dataRegistrazione) & "')"
            par.cmd.ExecuteNonQuery()

            '18/06/2015 SCRIVE VOCI_PAGAMENTI ANCHE DELLA VOCE DI MOROSITA
            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = (SELECT ID_BOLLETTA_RIC FROM SISCOM_MI.BOL_BOLLETTE WHERE ID = " & Request.QueryString("IDBOL") & ") AND ID_VOCE IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_REPORT = 1)"
            Dim voceMor As Integer = 0
            voceMor = par.cmd.ExecuteScalar
            par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,DATA_REGISTRAZIONE,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV,DATA_VALUTA) VALUES " _
                                & "(" & voceMor & ",'" & par.AggiustaData(Me.txtDataPagamento.Text) & "','" & par.AggiustaData(Me.txtDataPagamento.Text) & "'," & par.VirgoleInPunti(importo) & ",2," & idIncassoExt & ",'" & par.AggiustaData(Me.txtDataValuta.Text) & "')"
            par.cmd.ExecuteNonQuery()


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteVociPagamenti" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub WriteVociPagamenti2(ByVal idVoceBolletta As Integer, ByVal dataPagamento As String, ByVal importo As Decimal, ByVal idIncassoExt As Integer, ByVal tipo As Integer)

        Try


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.bol_bollette_voci_pagamenti2 (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                & "(" & idVoceBolletta & ",'" & par.AggiustaData(dataPagamento) & "'," & par.VirgoleInPunti(importo) & "," & tipo & "," & idIncassoExt & ")"
            par.cmd.ExecuteNonQuery()



        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteVociPagaRicla" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub PagataCompletamente(ByVal idBolletta As String)
        Try
            Dim ConnOpenNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
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
                Data.OracleClient.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- PagataCompletamente" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try

    End Sub

    Private Sub WriteDataPagBol(ByVal idBolletta As String, ByVal idEvento As String)
        Try
            Dim dataPrec As String = ""
            Dim dataValuta As String = ""
            par.cmd.CommandText = "SELECT DATA_PAGAMENTO,DATA_VALUTA FROM SISCOM_MI.BOL_BOLLETTE WHERE ID = " & idBolletta
            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If Lettore.Read Then
                dataPrec = par.IfNull(Lettore("DATA_PAGAMENTO"), "")
                dataValuta = par.IfNull(Lettore("DATA_VALUTA"), "")
            End If
            Lettore.Close()
            'If dataPrec <> "" Then
            '    par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_DATE_PAG (ID_EVENTO,ID_BOLLETTA,DATA_PREC,DATA_VALUTA_PREC) VALUES " _
            '        & "(" & idEvento & ", " & idBolletta & ", '" & dataPrec & "','" & dataValuta & "')"
            '    par.cmd.ExecuteNonQuery()
            'End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- WriteDataPagBol" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try
    End Sub
    'par.IndDGC(DataGridBollette, "ID_VOCE")).Text

    Private Function isPagabileBollo(ByVal idVoce As Integer, ByVal impPagamento As Decimal) As Boolean
        isPagabileBollo = True
        Dim resBollo As Decimal = 0
        Dim ConnOpenNow As Boolean = False
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()
            ConnOpenNow = True
        End If

        par.cmd.CommandText = "select (nvl(importo,0)-nvl(imp_pagato,0)) as resBollo from siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette_voci where t_voci_bolletta.id = bol_bollette_voci.id_voce and t_voci_bolletta.gruppo = 7 and bol_bollette_voci.id =" & idVoce
        resBollo = par.IfNull(par.cmd.ExecuteScalar, 0)
        If resBollo <> 0 Then
            If impPagamento < resBollo Then
                isPagabileBollo = False
                Me.BolloPagParz.Value = 1
            End If
        End If
        If ConnOpenNow = True Then
            par.OracleConn.Close()
            Data.OracleClient.OracleConnection.ClearAllPools()
        End If
    End Function

    Private Function ControllaImporti() As Boolean
        ControllaImporti = True
        Try
            'If Not IsNothing(Session.Item("PagInMor")) Then
            '    TotStoPerInserire.Value = Session.Item("PagInMor")
            'Else
                TotStoPerInserire.Value = 0
            'End If
            For Each di As DataGridItem In DataGridBollette.Items
                If Math.Round(CDec(par.IfEmpty(CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)), 2) > Math.Round(CDec(par.IfEmpty(di.Cells(TrovaIndiceColonna(DataGridBollette, "RESIDUO")).Text.Replace(".", ""), 0)), 2) Then

                    'If Math.Round(CDec(par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)) + CDec(par.IfEmpty(CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)), 2) > Math.Round(CDec(par.IfEmpty(di.Cells(3).Text.Replace(".", ""), 0)), 2) Then
                    'If par.IfEmpty(di.Cells(3).Text, 0) - (CDec(par.IfEmpty(di.Cells(4).Text.Replace(".", ""), 0)) + par.IfEmpty(di.Cells(5).Text, 0)) < 0 Then
                    ControllaImporti = False
                    Response.Write("<script>alert('Impossibile procedere!\nVerificare che la cifra immessa nella colonna PAGAMENTO\nnon superi il pagabile (IMPORTO - IMP.PAGATO) della singola voce!');</script>")
                    Exit For
                End If
                If di.Cells(par.IndDGC(DataGridBollette, "DESCRIZIONE")).Text.ToUpper <> "T O T A L E" Then
                    TotStoPerInserire.Value += Math.Round(CDec(par.IfEmpty(CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)), 2)

                End If
                If par.IfEmpty(di.Cells(par.IndDGC(DataGridBollette, "ID")).Text.Replace("&nbsp;", ""), "") <> "" And Math.Round(CDec(par.IfEmpty(CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)), 2) <> 0 Then
                    If isPagabileBollo(di.Cells(par.IndDGC(DataGridBollette, "ID")).Text, Math.Round(CDec(par.IfEmpty(CType(di.Cells(4).FindControl("txtPaga"), TextBox).Text.Replace(".", ""), 0)), 2)) = False Then
                        ControllaImporti = False
                        Response.Write("<script>alert('INCASSO INTERROTTO PER PAGAMENTO PARZIALE DI UN BOLLO!');</script>")
                        Exit Function
                    End If
                End If
               
            Next
            If TotStoPerInserire.Value > (Math.Round(ImportoIncasso.Value - PagatoDaChiamata.Value, 2)) Then
                Response.Write("<script>alert('Impossibile procedere!\nIl totale del pagamento è superiore al residuo di " & Math.Round(ImportoIncasso.Value - PagatoDaChiamata.Value, 2) & "€. dell\'incasso iniziale!');</script>")
                ControllaImporti = False
            Else
                PagatoDaChiamata.Value = CDec(PagatoDaChiamata.Value) + CDec(TotStoPerInserire.Value)
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            ControllaImporti = False
            lblErrore.Text = "ControllaImporti - " & ex.Message

        End Try
        Return ControllaImporti
    End Function

    Protected Sub cmbTipoPagamento_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoPagamento.SelectedIndexChanged
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
    Protected Sub chkEcludeQS_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkEcludeQS.CheckedChanged
        CaricaBolletta(True)
        If Me.chkEcludeQS.Checked = True Then
            testo.Visible = True
        Else
            testo.Visible = False
        End If
    End Sub

    Protected Sub rdbTipoPagam_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbTipoPagam.SelectedIndexChanged
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
            AddJavascriptFunction()



        End If
    End Sub
End Class
