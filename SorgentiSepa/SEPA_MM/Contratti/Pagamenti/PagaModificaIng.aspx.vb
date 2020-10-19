
Partial Class Contratti_Pagamenti_PagaModificaIng
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsNothing(Session.Item("PGMANUALEING" & vIdConnessione.Value)) Then
            Me.connData = New CM.datiConnessione(par, False, False)
        Else
            Me.connData = CType(HttpContext.Current.Session.Item("PGMANUALEING" & vIdConnessione.Value), CM.datiConnessione)
            par.SettaCommand(par)
        End If
        If Not IsPostBack Then

            If IsNothing(Session.Item("PGMANUALEING" & vIdConnessione.Value)) Then
                vIdConnessione.Value = Format(Now, "yyyyMMddHHmmss")
            End If
            vIdContratto.Value = Request.QueryString("IDCONT")
            vIdAnagrafica.Value = Request.QueryString("IDANA")

            idIncasso.Value = Request.QueryString("IDINCASSO")

            Me.txtDataPagamento.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtDataPagamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataRegistrazione.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtDataRegistrazione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.txtImpPagamento.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            Me.txtImpPagamento.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);GestTableResidui(this);return false;")

            rdbTipoModifica.SelectedValue = 0
            Me.rdbTipoIncasso.SelectedValue = 0
            Me.tipoPagValue.Value = 0
            CaricaInfo()
            CaricaBollNonPagate()
            If idIncasso.Value > 0 Then
                CaricaDatiIncasso()
                CaricaTipoPagamento()
            End If
            SettaTipoMod()
            If contrLocked.Value = 1 Then
                FrmSoloLettura()
            End If
            If Request.QueryString("FLANNULLATO") = 1 Then
                FrmSoloLettura()
            End If

        End If
    End Sub
    Private Sub CaricaTipoPagamento()
        par.cmd.CommandText = "select ID_TIPO_PAG from siscom_mi.INCASSI_INGIUNZIONE where id = " & idIncasso.Value
        Dim modalitaPag As Integer = par.IfNull(par.cmd.ExecuteScalar, -1)
        Dim descModalita As String = ""

        If IsNothing(Me.cmbTipoPagamento.Items.FindByValue(modalitaPag)) Then
            par.cmd.CommandText = "select descrizione from siscom_mi.TIPO_PAG_INGIUNZIONE where id = " & modalitaPag
            descModalita = par.cmd.ExecuteScalar
            cmbTipoPagamento.Items.Add(New ListItem(par.IfNull(descModalita, " "), par.IfNull(modalitaPag, -1)))
            Me.cmbTipoPagamento.SelectedValue = modalitaPag
        Else
            Me.cmbTipoPagamento.SelectedValue = par.IfNull(modalitaPag, "-1")
        End If
    End Sub
    Private Sub CaricaDatiIncasso()
        Try
            Me.rdbTipoIncasso.Enabled = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If
            par.cmd.CommandText = "select * from siscom_mi.INCASSI_INGIUNZIONE where id = " & idIncasso.Value
            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If reader.Read Then
                Me.txtDataPagamento.Text = par.FormattaData(par.IfNull(reader("data_pagamento"), ""))
                Me.txtImpPagamento.Text = Format(CDec(par.IfNull(reader("importo"), 0)), "##,##0.00")
                Me.txtDataRegistrazione.Text = par.FormattaData(par.IfNull(reader("data_registrazione"), ""))
                If String.IsNullOrEmpty(Me.txtDataRegistrazione.Text) Then
                    Me.txtDataRegistrazione.Text = Me.txtDataPagamento.Text
                End If
                Me.SumSelected.Value = CDec(0) 'CDec(par.IfNull(reader("importo"), 0))
                Me.txtPagResoconto.Text = Me.txtImpPagamento.Text
                Me.txtSommaSel.Text = Format(CDec(0), "##,##0.00") 'Format(CDec(SumSelected.Value), "##,##0.00")
                If CDec(Me.txtPagResoconto.Text.Replace(".", "")) - CDec(SumSelected.Value) < 0 Then
                    Me.txtResResoconto.Text = Format(CDec(0), "##,##0.00")
                Else
                    Me.txtResResoconto.Text = Format(CDec(0), "##,##0.00") 'Format(CDec(Me.txtPagResoconto.Text.Replace(".", "")) - CDec(SumSelected.Value), "##,##0.00")
                End If

                Me.txtnote.Text = par.IfNull(reader("MOTIVO_PAGAMENTO"), "")

            End If
            reader.Close()
            par.cmd.CommandText = "SELECT id_bolletta,sum(nvl(importo_pagato,0)) as importo_pagato FROM siscom_mi.BOL_BOLLETTE_PAGAMENTI_ING WHERE ID_INCASSO_ING = " & idIncasso.Value & " group by id_bolletta order by id_bolletta asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ATTENZIONE!\nL\'incasso che si vuole modificare ha delle anomalie sui dati contattare l\'amministratore!\nOPERAZIONE INTERROTTA!');", True)
                Me.rdbTipoModifica.Enabled = False
                FrmSoloLettura()
            End If
            For Each ri As DataGridItem In dgvBollVoci.Items
                For Each r As Data.DataRow In dt.Rows
                    If ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text = r.Item("ID_BOLLETTA").ToString Then
                        CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text = Format(CDec(r.Item("importo_pagato")), "##,##0.00")
                        CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).BackColor = Drawing.Color.LightBlue
                        CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = True
                    End If
                Next
            Next
            For Each ri As DataGridItem In dgvBollVoci.Items
                If CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).BackColor <> Drawing.Color.LightBlue Then
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = False
                End If
                If par.IfEmpty(CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text, 0) = 0 Then
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = False
                End If
            Next

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - CaricaDatiIncasso - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
    End Sub



    Private Sub SettaTipoMod()
        If rdbTipoModifica.SelectedValue = 0 Then
            'M.Teresa 09/07/2015 - Modifica per impedire che si possa entrare nella textbox col cursore
            'Me.txtImpPagamento.ReadOnly = True
            txtImpPagamento.Enabled = False
            'Me.dgvBollVoci.Enabled = False
            Me.SumSelected.Value = 0
            For Each ri As DataGridItem In dgvBollVoci.Items
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).ReadOnly = True

            Next

        Else
            'M.Teresa 09/07/2015 - Modifica per impedire che si possa entrare nella textbox col cursore
            'Me.txtImpPagamento.ReadOnly = False
            txtImpPagamento.Enabled = True
            Me.dgvBollVoci.Enabled = True
            Me.SumSelected.Value = 0 'Me.txtImpPagamento.Text

            For Each ri As DataGridItem In dgvBollVoci.Items
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).ReadOnly = False


            Next
        End If
    End Sub

    Private Sub CaricaInfo()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If

            par.cmd.CommandText = "SELECT distinct anagrafica.ragione_sociale,UNITA_CONTRATTUALE.ID_UNITA, " _
                    & "RAPPORTI_UTENZA.ID AS ID_CONTRATTO, RAPPORTI_UTENZA.COD_CONTRATTO AS COD_CONT, " _
                    & "RAPPORTI_UTENZA.ID_AU,ANAGRAFICA.ID AS ""ID_ANAGRAFICA"", " _
                    & "CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"", " _
                    & "CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END AS ""CFIVA"", " _
                    & "('<a href=""#"" onclick=""javascript:validNavigation=true;window.open(''../Contratto.aspx?LT=1&ID='||RAPPORTI_UTENZA.ID||'&COD='||RAPPORTI_UTENZA.COD_CONTRATTO||''',''CONTRATTO'',''height=780,top=0,left=0,width=1160'');validNavigation=false;void(0);"">'||RAPPORTI_UTENZA.COD_CONTRATTO||'</a>') AS COD_CONTRATTO," _
                    & "('<a href=""#"" onclick=""javascript:validNavigation=true;window.open(''../../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');validNavigation=false;void(0);"">'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>') AS COD_UNITA_IMMOBILIARE, " _
                    & "(INDIRIZZI.DESCRIZIONE||','||INDIRIZZI.CIVICO||'') AS ""INDIRIZZO"", " _
                    & "UNITA_IMMOBILIARI.INTERNO,(SELECT descrizione FROM siscom_mi.scale_edifici WHERE ID=unita_immobiliari.id_scala) AS ""SCALA""," _
                    & "INDIRIZZI.CAP, TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""LIV_PIANO"", " _
                    & "TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS ""TIPO_CONTRATTO"", " _
                    & "TIPOLOGIA_CONTRATTO_LOCAZIONE.RIF_LEGISLATIVO, TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_DECORRENZA"", " _
                    & "TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_RICONSEGNA ,'yyyymmdd'),'dd/mm/yyyy') AS ""DATA_FINE_LOC""," _
                    & "siscom_mi.Getstatocontratto(RAPPORTI_UTENZA.ID) AS ""STATO"" " _
                    & "FROM siscom_mi.RAPPORTI_UTENZA, siscom_mi.TIPOLOGIA_CONTRATTO_LOCAZIONE, " _
                    & "siscom_mi.SOGGETTI_CONTRATTUALI, siscom_mi.ANAGRAFICA, " _
                    & "siscom_mi.UNITA_IMMOBILIARI, siscom_mi.UNITA_CONTRATTUALE, " _
                    & "siscom_mi.INDIRIZZI, siscom_mi.TIPO_LIVELLO_PIANO,siscom_mi.IDENTIFICATIVI_CATASTALI  " _
                    & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                    & "AND RAPPORTI_UTENZA.ID= SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                    & "AND RAPPORTI_UTENZA.cod_tipologia_contr_loc = TIPOLOGIA_CONTRATTO_LOCAZIONE.cod AND UNITA_IMMOBILIARI. ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                    & "AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO= TIPO_LIVELLO_PIANO.COD(+) AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) " _
                    & "AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND ID_ANAGRAFICA = " & vIdAnagrafica.Value & " AND RAPPORTI_UTENZA.ID = " & vIdContratto.Value




            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then

                txtintestatario.Text = par.IfNull(dt.Rows(0).Item("INTESTATARIO"), "")
                Me.vIdUnita.Value = par.IfNull(dt.Rows(0).Item("ID_UNITA"), 0)
                par.caricaComboBox("select id, UPPER(descrizione) as descrizione from siscom_mi.TIPO_PAG_INGIUNZIONE order by descrizione asc", cmbTipoPagamento, "id", "descrizione")

            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - CaricaInfo - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
    End Sub

    Private Sub FrmSoloLettura()
        Try
            Me.txtDataPagamento.Enabled = False
            Me.txtDataRegistrazione.Enabled = False
            Me.txtImpPagamento.Enabled = False
            Me.rdbTipoIncasso.Enabled = False
            Me.btnSalvaPag.Visible = False
            Me.rdbTipoModifica.Enabled = False
            Me.cmbTipoPagamento.Enabled = False

            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "SELEZIONE")).Visible = False
            Me.tblAutomatica.Style.Value = "border: 1px solid #66FF33; background-color: #F2EBBF; visibility: visible;"
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - AggiungiVoci - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Function PagamentoAutomatico(ByRef errore As String) As Boolean
        PagamentoAutomatico = False
        errore = "PagamentoAutomatico"
        While impWritePagamento.Value > 0

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.id,importo_ingiunzione,round(NVL (imp_ingiunzione_pag, 0),2) as imp_ingiunzione_pag, " _
                                & "(importo_ingiunzione - round(NVL (imp_ingiunzione_pag, 0),2)) as residuo,data_scadenza,data_emissione, " _
                                & "(CASE WHEN data_scadenza < TRIM(TO_CHAR(SYSDATE, 'YYYYMMDD')) THEN 1 ELSE 0 END) AS scaduta " _
                                & "FROM siscom_mi.BOL_BOLLETTE " _
                                & "WHERE " _
                                & "round(NVL (imp_ingiunzione_pag, 0),2) < round(importo_ingiunzione,2) AND nvl(importo_ingiunzione,0) > 0 " _
                                & "AND FL_ANNULLATA = '0' " _
                                & "AND BOL_BOLLETTE.ID_CONTRATTO = " & vIdContratto.Value _
                                & " ORDER BY scaduta DESC " _
                                & " , BOL_BOLLETTE.DATA_SCADENZA ASC " _
                                & " ,BOL_BOLLETTE.data_emissione ASC " _
                                & " ,BOL_BOLLETTE.id ASC " _
                                & " ,(importo_ingiunzione-NVL(imp_ingiunzione_pag,0)) DESC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then

                Dim dtFiltrata As Data.DataTable
                dtFiltrata = TrovaScadute(dt) '<--debito scaduto
                If dtFiltrata.Rows.Count > 1 Then
                    'dtFiltrata = PrioritaVoce(dtFiltrata) '<-- meno garantito (ossia priorita voce)
                    If dtFiltrata.Rows.Count > 1 Then
                        dtFiltrata = PiuAntico(dtFiltrata) '<-- più oneroso (residuo da pagare più grande)
                        If dtFiltrata.Rows.Count > 1 Then
                            dtFiltrata = PiuOnerosi(dtFiltrata) ' <-- più antico (data min emissione)
                            If dtFiltrata.Rows.Count > 1 Then
                                'pago le voci in base al criterio della proporzionalità
                                PagaProporzionalmente(dtFiltrata, errore)
                            Else
                                'PAGO LA VOCE TROVATA
                                PagaProporzionalmente(dtFiltrata, errore)
                            End If
                        Else
                            'PAGO LA VOCE TROVATA
                            PagaProporzionalmente(dtFiltrata, errore)

                        End If
                    Else
                        'PAGO LA VOCE TROVATA
                        PagaProporzionalmente(dtFiltrata, errore)

                    End If
                Else
                    'PAGO LA VOCE TROVATA
                    PagaProporzionalmente(dtFiltrata, errore)
                End If
            Else
                Exit While
            End If

        End While


        PagamentoAutomatico = True
        errore = ""
    End Function

    Private Function TrovaScadute(ByVal dt As Data.DataTable) As Data.DataTable
        Dim view As New Data.DataView(dt)
        'selezione bollette scadute, se non trovo scadute prendo le non scadute
        view.RowFilter = "SCADUTA = 1"
        TrovaScadute = view.ToTable
        If TrovaScadute.Rows.Count = 0 Then
            view.RowFilter = "SCADUTA = 0"
            TrovaScadute = view.ToTable
        End If
    End Function

    Private Function PiuOnerosi(ByVal dt As Data.DataTable) As Data.DataTable
        PiuOnerosi = New Data.DataTable
        Dim view As New Data.DataView(dt)
        Dim maxImporto As Decimal = dt.Compute("Max(residuo)", String.Empty)
        view.RowFilter = "RESIDUO = '" & maxImporto & "'"
        PiuOnerosi = view.ToTable

    End Function
    Private Function PiuAntico(ByVal dt As Data.DataTable) As Data.DataTable
        PiuAntico = New Data.DataTable
        Dim view As New Data.DataView(dt)
        Dim minEmissione As String = dt.Compute("min(data_emissione)", String.Empty)
        view.RowFilter = "DATA_EMISSIONE = " & minEmissione
        PiuAntico = view.ToTable
    End Function

    Private Sub PagaProporzionalmente(ByVal dt As Data.DataTable, ByRef errore As String)
        errore = "PagaProporzionalmente"

        Dim resVoci As Decimal = par.IfEmpty(dt.Compute("SUM(RESIDUO)", String.Empty).ToString, 0)
        Dim rPagamento As Data.DataTable 'datatable con un solo rigo, pers sfruttare il pagamento delle voci
        resVoci = Math.Round(resVoci, 2)
        If impWritePagamento.Value > resVoci Then
            Dim impPag As Decimal = 0
            For Each rvoce As Data.DataRow In dt.Rows
                impPag = Math.Round(CDec(par.IfEmpty(rvoce.Item("residuo").ToString, 0)), 2)
                impWritePagamento.Value = impWritePagamento.Value - impPag
                Dim view As New Data.DataView(dt)
                view.RowFilter = "ID = " & rvoce.Item("id")
                rPagamento = view.ToTable
                PagaElVoci(rPagamento, impPag, errore, True)
            Next
        ElseIf impWritePagamento.Value <= resVoci Then
            Dim propPagVoce As Decimal = 0
            Dim i As Integer = 0
            Dim writePagIniziale As Decimal = CDec(impWritePagamento.Value)
            Dim resPagIniziale As Decimal = CDec(impWritePagamento.Value)
            For Each rProp As Data.DataRow In dt.Rows
                If i < dt.Rows.Count - 1 Then
                    propPagVoce = Math.Round((CDec(par.IfEmpty(rProp.Item("residuo").ToString, 0)) * writePagIniziale) / resVoci, 2)
                    impWritePagamento.Value = impWritePagamento.Value - propPagVoce
                    resPagIniziale = resPagIniziale - propPagVoce
                    Dim view As New Data.DataView(dt)
                    view.RowFilter = "ID = " & rProp.Item("id")
                    rPagamento = view.ToTable
                    PagaElVoci(rPagamento, propPagVoce, errore, True)
                ElseIf i = dt.Rows.Count - 1 Then
                    If resPagIniziale > 0 Then
                        impWritePagamento.Value = impWritePagamento.Value - resPagIniziale
                        Dim view As New Data.DataView(dt)
                        view.RowFilter = "ID = " & rProp.Item("id")
                        rPagamento = view.ToTable
                        PagaElVoci(rPagamento, resPagIniziale, errore, True)
                    End If
                End If
                i += 1
            Next
        End If

    End Sub

    Private Sub PagaTutteLeVoci(ByVal dtVoci As Data.DataTable, ByRef errore As String)
        errore = "PagaTutteLeVoci"
        Dim impPagato As Decimal = 0
        Dim IsMor As Boolean = False
        For Each rVoce As Data.DataRow In dtVoci.Rows
            impPagato = Math.Round(par.IfEmpty(rVoce("importo_ingiunzione").ToString, 0) - par.IfEmpty(rVoce("imp_ingiunzione_pag").ToString, 0), 2)
            If impPagato <> 0 Then
                par.cmd.CommandText = "update siscom_mi.bol_bollette set imp_ingiunzione_pag = (nvl(imp_ingiunzione_pag,0) + " & par.VirgoleInPunti(impPagato) & ") where id =  " & rVoce("id").ToString
                par.cmd.ExecuteNonQuery()

                WriteVociPagamenti(impPagato, rVoce("id").ToString, "")
            End If
        Next
        errore = ""
    End Sub

    Private Sub PagaElVoci(ByVal dtVoci As Data.DataTable, ByVal importo As Decimal, ByRef errore As String, Optional isAutomatico As Boolean = False)
        errore = "PagaVoci"
        Dim disponibilita As Decimal = importo
        Dim impPagato As Decimal = 0
        Dim pagabile As Decimal = 0

        Dim isMor As Boolean = False
        For Each rVoce As Data.DataRow In dtVoci.Rows

            pagabile = 0
            impPagato = 0
            pagabile = Math.Round(par.IfEmpty(rVoce("importo_ingiunzione").ToString, 0) - par.IfEmpty(rVoce("imp_ingiunzione_pag").ToString, 0), 2)
            If pagabile > 0 Then
                If disponibilita > pagabile Then
                    impPagato = pagabile
                Else
                    impPagato = disponibilita

                End If
                par.cmd.CommandText = "update siscom_mi.bol_bollette set imp_ingiunzione_pag = (nvl(imp_ingiunzione_pag,0) + " & par.VirgoleInPunti(impPagato) & ") where id =  " & rVoce("id").ToString
                par.cmd.ExecuteNonQuery()

                WriteVociPagamenti(impPagato, rVoce("id").ToString, "")

                disponibilita = disponibilita - impPagato
            End If
        Next
        If disponibilita > 0 Then
            impWritePagamento.Value = impWritePagamento.Value + disponibilita
        End If
        errore = ""
    End Sub

    Private Sub WriteVociPagamenti(ByVal pagato As Decimal, ByVal voce As Integer, ByVal voceRata As String)
        If pagato <> 0 Then
            par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_PAGAMENTI_ING (ID_BOLLETTA,DATA_PAGAMENTO,DATA_OPERAZIONE,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_ING,DATA_VALUTA) VALUES " _
                    & "(" & voce & ",'" & par.AggiustaData(Me.txtDataPagamento.Text) & "','" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "'," & par.VirgoleInPunti(pagato) & "," & cmbTipoPagamento.SelectedValue & "," & Me.idIncasso.Value & ",'" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "')"
            par.cmd.ExecuteNonQuery()
        End If
    End Sub


    Private Sub CaricaBollNonPagate()
        Try

            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If

            par.cmd.CommandText = "SELECT  BOL_BOLLETTE.ID AS ID_BOLLETTA," _
                                & "('<a href=""#"" onclick=""javascript:validNavigation=true;window.open(''../../Contabilita/DettaglioBolletta.aspx?IDCONT='||ID_CONTRATTO||'&IDBOLL='||BOL_BOLLETTE.ID||'&IDANA=" & Request.QueryString("IDANA") & "'',''DET_'||NUM_BOLLETTA||''','''');validNavigation=false;void(0);"">'||NUM_BOLLETTA||'</a>') as NUM_BOLLETTA," _
                                & "BOL_BOLLETTE.N_RATA,GETDATA(DATA_EMISSIONE) AS DATA_EMISSIONE," _
                                & "(SELECT ACRONIMO FROM SISCOM_MI.TIPO_BOLLETTE,SISCOM_MI.BOL_BOLLETTE BOLBOLLETTE WHERE TIPO_BOLLETTE.ID(+)=BOLBOLLETTE.ID_TIPO AND BOL_BOLLETTE.ID=BOLBOLLETTE.ID) as TIPOBOLL,GETDATA(DATA_SCADENZA)AS DATA_SCADENZA,TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMP_INGIUNZIONE_PAG,0),'9G999G999G990D99'))AS IMP_INGIUNZIONE_PAG,TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMPORTO_INGIUNZIONE,0),'9G999G999G990D99'))AS IMPORTO_INGIUNZIONE, " _
                                & "(GETDATA(riferimento_da)||' al '||GETDATA(riferimento_a)) AS RIFERIMENTO,GETDATA(DATA_PAGAMENTO) AS DATA_PAGAMENTO,TRIM(TO_CHAR(NVL((NVL(IMPORTO_INGIUNZIONE,0)) - (NVL(IMP_INGIUNZIONE_PAG,0)),0),'9G999G999G990D99')) AS RESIDUO, " _
                                & "TIPO_BOLL_INGIUNZIONE.DESCRIZIONE AS INGIUNZIONE FROM siscom_mi.BOL_BOLLETTE,siscom_mi.TIPO_BOLL_INGIUNZIONE " _
                                & "WHERE TIPO_BOLL_INGIUNZIONE.ID(+)=ID_TIPO_INGIUNZIONE And FL_ANNULLATA = '0' AND ID_BOLLETTA_RIC IS NULL AND ID_RATEIZZAZIONE IS NULL AND NVL(IMPORTO_INGIUNZIONE,0) > 0 " _
                                & "AND BOL_BOLLETTE.ID_STATO <> -1 AND  BOL_BOLLETTE.ID_CONTRATTO = " & vIdContratto.Value & " ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC ,BOL_BOLLETTE.data_emissione ASC,BOL_BOLLETTE.ID ASC "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            da.Dispose()
            Dim dtFinale As New Data.DataTable

            dtFinale = dt

            '************AZZERO TOTALI***********
            totPagabile.Value = 0
            totPagato.Value = 0
            totResiduo.Value = 0
            '************AZZERO TOTALI***********
            Dim row As Data.DataRow
            For Each row In dt.Rows
                totPagabile.Value += CDec((par.IfNull(row.Item("importo_ingiunzione"), 0)))
                totPagato.Value += CDec(par.IfNull(row.Item("imp_ingiunzione_pag"), 0))
                totResiduo.Value += CDec(par.IfNull(row.Item("RESIDUO"), 0))
            Next


            row = dtFinale.NewRow()
            row.Item("NUM_BOLLETTA") = "TOTALE"
            row.Item("importo_ingiunzione") = Format(CDec(totPagabile.Value), "##,##0.00")
            row.Item("imp_ingiunzione_pag") = Format(CDec(totPagato.Value), "##,##0.00")
            row.Item("RESIDUO") = Format(CDec(totResiduo.Value), "##,##0.00")

            dtFinale.Rows.Add(row)
            Me.dgvBollVoci.DataSource = dtFinale
            Me.dgvBollVoci.DataBind()


            GestioneGrafica(dtFinale)
            JsFunzioniDgv(dgvBollVoci)
            GestioneTipoIncasso(Me.rdbTipoIncasso.SelectedValue)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - CaricaBollNonPagate - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
    End Sub


    Private Sub JsFunzioniDgv(ByVal dgv As DataGrid)
        For Each ri As DataGridItem In dgv.Items
            CType(ri.Cells(par.IndDGC(dgv, "SELEZIONE")).FindControl("chkSel"), CheckBox).Attributes.Add("onclick", "javascript:sommaChek(this," & ri.Cells(par.IndDGC(dgv, "RESIDUO")).Text.Replace(".", "").Replace(",", ".") & ");")
        Next
    End Sub

    Private Sub GestioneGrafica(ByVal dtFinale As Data.DataTable)
        If rdbTipoModifica.SelectedValue = 1 Then


            ''**************GESTIONE GRAFICA ******************
            Dim idBolletta As Integer = 0
            Dim idVoce As Integer = 0
            For Each ri As DataGridItem In dgvBollVoci.Items
                idBolletta = -1
                idVoce = -1
                idBolletta = par.IfEmpty(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text.Replace("&nbsp;", ""), -1)


                If idBolletta = -1 And idVoce = -1 Then
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Visible = False


                    ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).Font.Bold = False
                    ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).ForeColor = Drawing.Color.Navy
                    ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).HorizontalAlign = HorizontalAlign.Center

                    ri.Cells(par.IndDGC(dgvBollVoci, "importo_ingiunzione")).Font.Bold = False
                    ri.Cells(par.IndDGC(dgvBollVoci, "importo_ingiunzione")).ForeColor = Drawing.Color.Navy

                    ri.Cells(par.IndDGC(dgvBollVoci, "imp_ingiunzione_pag")).Font.Bold = False
                    ri.Cells(par.IndDGC(dgvBollVoci, "imp_ingiunzione_pag")).ForeColor = Drawing.Color.Navy

                    ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Bold = False
                    ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).ForeColor = Drawing.Color.Navy

                ElseIf idVoce <> -1 Then
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Visible = False

                    ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).Font.Bold = False
                    ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).Font.Size = 8
                    ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).HorizontalAlign = HorizontalAlign.Left


                    ri.Cells(par.IndDGC(dgvBollVoci, "importo_ingiunzione")).Font.Bold = False
                    ri.Cells(par.IndDGC(dgvBollVoci, "importo_ingiunzione")).Font.Size = 8


                    ri.Cells(par.IndDGC(dgvBollVoci, "imp_ingiunzione_pag")).Font.Bold = False
                    ri.Cells(par.IndDGC(dgvBollVoci, "imp_ingiunzione_pag")).Font.Size = 8


                    ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Bold = False
                    ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Size = 8
                ElseIf idBolletta <> -1 Then
                    If ri.Cells(par.IndDGC(dgvBollVoci, "imp_ingiunzione_pag")).Text = ri.Cells(par.IndDGC(dgvBollVoci, "importo_ingiunzione")).Text Then
                        CType(ri.Cells(par.IndDGC(dgvBollVoci, "SELEZIONE")).FindControl("chkSel"), CheckBox).Checked = True
                        CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Visible = False
                    End If
                End If
            Next
        End If

        '************************* ULTIMA RIGA BOLLETTE NON PAGATE **********************************
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(0).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(0).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(0).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).BackColor = Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).BackColor

        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "importo_ingiunzione")).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "importo_ingiunzione")).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "importo_ingiunzione")).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "importo_ingiunzione")).BackColor = Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).BackColor

        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "imp_ingiunzione_pag")).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "imp_ingiunzione_pag")).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "imp_ingiunzione_pag")).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "imp_ingiunzione_pag")).BackColor = Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).BackColor

        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).BackColor = Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).BackColor

        '************************* ULTIMA RIGA BOLLETTE NON PAGATE **********************************

    End Sub


    Protected Sub chkSelAll_CheckedChanged(sender As Object, e As System.EventArgs)
        If Not String.IsNullOrEmpty(Me.txtImpPagamento.Text) Then

            SelUnsel(sender.checked)
        Else

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ATTENZIONE!\nInserire l\'importo del pagamento prima di selezionare le bollette!');", True)
            sender.checked = False
        End If

    End Sub

    Private Sub SelUnsel(ByVal seleziona As Boolean)
        If rdbTipoIncasso.SelectedValue = 0 Then
            SumSelected.Value = 0
            If seleziona = True Then
                For Each ri As DataGridItem In dgvBollVoci.Items
                    If CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Visible = True Then
                        CType(ri.Cells(par.IndDGC(dgvBollVoci, "SELEZIONE")).FindControl("chkSel"), CheckBox).Checked = True
                        SumSelected.Value = SumSelected.Value + CDec(ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Text.Replace(".", ""))
                    End If
                Next
            Else
                For Each ri As DataGridItem In dgvBollVoci.Items
                    If CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Visible = True Then
                        CType(ri.Cells(par.IndDGC(dgvBollVoci, "SELEZIONE")).FindControl("chkSel"), CheckBox).Checked = False
                        If SumSelected.Value > 0 Then
                            SumSelected.Value = SumSelected.Value - CDec(ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Text.Replace(".", ""))
                        End If
                    End If

                Next
            End If
        End If

    End Sub
    Private Sub GestioneTipoIncasso(ByVal tipo As Integer)
        Dim idBolletta As Integer = 0
        Dim idVoce As Integer = 0
        Me.SumSelected.Value = 0
        Me.bollette.Style.Value = Me.bollette.Style.Value & " visibility: visible;"
        Me.tblAutomatica.Style.Value = Me.tblAutomatica.Style.Value & " visibility: visible;"


        If rdbTipoModifica.SelectedValue = 1 Then
            Me.dgvBollVoci.Visible = True
            If tipo = -1 Then
                Me.tblAutomatica.Style.Value = "border: 1px solid #66FF33;  background-color: #F2EBBF;visibility: visible;"
                Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "SELEZIONE")).Visible = False
                Me.tblAutomatica.Style.Value = Me.tblAutomatica.Style.Value & " visibility: hidden;"
            ElseIf tipo = 0 Then
                Me.txtImpPagamento.ReadOnly = False
                For Each ri As DataGridItem In dgvBollVoci.Items
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "SELEZIONE")).FindControl("chkSel"), CheckBox).Checked = False
                Next
                Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "SELEZIONE")).Visible = True
                Me.tblAutomatica.Style.Value = "border: 1px solid #66FF33;  background-color: #F2EBBF;visibility: visible;"
            End If
        Else
            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "SELEZIONE")).Visible = False
        End If
    End Sub


    Protected Sub rdbTipoModifica_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbTipoModifica.SelectedIndexChanged
        If Me.rdbTipoModifica.SelectedValue = 1 Then
            CaricaBollNonPagate()
        Else
            CaricaBollNonPagate()
        End If
        If idIncasso.Value > 0 Then
            CaricaDatiIncasso()
            CaricaTipoPagamento()
        End If

        SettaTipoMod()
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        HFExitForce.Value = "1"
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "x", "validNavigation=true;self.close();", True)
    End Sub

    Protected Sub cmbTipoPagamento_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoPagamento.SelectedIndexChanged

        CaricaBollNonPagate()

        If idIncasso.Value > 0 Then
            CaricaDatiIncasso()
        End If
    End Sub

    Protected Sub rdbTipoIncasso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbTipoIncasso.SelectedIndexChanged
        GestioneTipoIncasso(Me.rdbTipoIncasso.SelectedValue)
        Me.tipoPagValue.Value = Me.rdbTipoIncasso.SelectedValue
    End Sub

    Protected Sub btnSalvaPag_Click(sender As Object, e As System.EventArgs) Handles btnSalvaPag.Click
        If rdbTipoModifica.SelectedValue = 1 Then
            ModPagamento()
        ElseIf rdbTipoModifica.SelectedValue = 0 Then
            ModIncasso()
        End If
    End Sub

    Private Sub ModIncasso()
        Dim errore As String = ""
        Try
            If Me.confPagamento.Value = 1 Then
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                End If
                'conferma MODIFICA PAGAMENTO
                If Controlli() = True Then ' controlli campi obbligatori
                    Me.impWritePagamento.Value = CDec(Me.txtImpPagamento.Text.Replace(".", ""))

                    AggiornaDatiIncasso()

                    connData.chiudiTransazione(True)
                    'riapro la transazione per successivi comandi
                    connData.apriTransazione()

                    '*******************messaggio finale
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('MODIFICA ESEGUITA CORRETTAMENTE!');opener.validNavigation=true;opener.document.getElementById('flReload').value=1;opener.document.getElementById('form1').submit();", True)
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudiTransazione(False)
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: ModIncasso - " & errore & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try

    End Sub

    Private Function EliminaPagamento() As Boolean
        EliminaPagamento = False

        par.cmd.CommandText = "update SISCOM_MI.INCASSI_INGIUNZIONE set fl_annullata = 1 where id = " & idIncasso.Value
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "select data_pagamento from SISCOM_MI.INCASSI_INGIUNZIONE where id = " & idIncasso.Value
        Dim dataPagIncasso As String = par.cmd.ExecuteScalar
        par.cmd.CommandText = "SELECT id_bolletta,sum(nvl(importo_pagato,0)) as importo_pagato,ID_TIPO_PAGAMENTO FROM SISCOM_MI.BOL_BOLLETTE_PAGAMENTI_ING WHERE ID_INCASSO_ING = " & idIncasso.Value & " group by id_bolletta,ID_TIPO_PAGAMENTO"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable()
        da.Fill(dt)
        For Each r As Data.DataRow In dt.Rows
            par.cmd.CommandText = "update SISCOM_MI.bol_bollette set imp_ingiunzione_pag = (nvl(imp_ingiunzione_pag,0) + " & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & ") where id = " & r.Item("id_bolletta")
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_PAGAMENTI_ING (ID_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_ING,DATA_OPERAZIONE) VALUES " _
                    & "(" & r.Item("id_bolletta") & ",'" & dataPagIncasso & "'," & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & "," & r.Item("ID_TIPO_PAGAMENTO") & "," & idIncasso.Value & ",'" & Format(Now, "yyyyMMdd") & "')"
            par.cmd.ExecuteNonQuery()
        Next

        par.cmd.CommandText = "update SISCOM_MI.INCASSI_INGIUNZIONE set fl_annullata = 0 where id = " & idIncasso.Value
        par.cmd.ExecuteNonQuery()

        EliminaPagamento = True
    End Function

    Private Function PagamentoSemiAutomatico(ByRef errore As String) As Boolean
        PagamentoSemiAutomatico = False
        errore = "PagamentoAutomatico"
        For Each ri As DataGridItem In dgvBollVoci.Items
            If CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Visible = True Then
                If CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Checked = True _
                    And Me.impWritePagamento.Value > 0 Then
                    PagaBolletta(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text, Me.impWritePagamento.Value, errore)
                End If
            End If
        Next
        PagamentoSemiAutomatico = True
        errore = ""

    End Function
    Private Sub PagaBolletta(ByVal idBolletta As Integer, ByVal importo As Decimal, ByRef errore As String)
        errore = "PagaVociBolletta"

        par.cmd.CommandText = "select bol_bollette.id,importo_ingiunzione,nvl(imp_ingiunzione_pag,0) as imp_ingiunzione_pag from siscom_mi.bol_bollette " _
                            & " where id = " & idBolletta & "  order by importo_ingiunzione ASC"

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable()
        da.Fill(dt)
        Dim Pagabile As Decimal = dt.Compute("SUM(importo_ingiunzione)", String.Empty) - dt.Compute("SUM(imp_ingiunzione_pag)", String.Empty)
        If Pagabile > 0 Then
            If importo >= Pagabile Then
                PagaTutteLeVoci(dt, errore)
                Me.impWritePagamento.Value = impWritePagamento.Value - Pagabile
            ElseIf importo < Pagabile Then
                PagaElVoci(dt, importo, errore)
                Me.impWritePagamento.Value = Math.Round(CDec(Me.impWritePagamento.Value) - importo, 2)
            End If
        End If
        errore = ""
    End Sub


    Private Sub ModPagamento()
        Dim errore As String = ""
        Try
            If Me.confPagamento.Value = 1 Then 'conferma scrittura pagamento
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                End If

                If Controlli() = True Then ' controlli campi obbligatori
                    '*******************apertura transazione
                    'connData.apriTransazione()
                    Me.impWritePagamento.Value = CDec(Me.txtImpPagamento.Text.Replace(".", ""))
                    If AggiornaDatiIncasso() = True Then
                        If EliminaPagamento() = True Then

                            Select Case rdbTipoIncasso.SelectedValue

                                Case 0
                                    If PagamentoSemiAutomatico(errore) = False Then
                                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                                        connData.chiudiTransazione(False)
                                        'riapro la transazione per successivi comandi
                                        connData.apriTransazione()
                                        Exit Sub
                                    End If
                            End Select

                            If Me.ResCredito.Value > 0 Then 'SE DOPO IL PAGAMENTO HO ANCORA DISPONIBILITA' SCRIVO IN PARTITA GESTIONALE
                                Me.ResCredito.Value = Math.Round(CDec(ResCredito.Value), 2)
                            End If


                        End If
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                        connData.chiudiTransazione(False)
                        'riapro la transazione per successivi comandi
                        connData.apriTransazione()

                        Exit Sub
                    End If


                    connData.chiudiTransazione(True)
                    'riapro la transazione per successivi comandi
                    connData.apriTransazione()
                    ''*******************messaggio finale
                    '*******************messaggio finale
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('MODIFICA ESEGUITA CORRETTAMENTE!');opener.document.getElementById('flReload').value=1;opener.document.getElementById('form1').submit();self.close();", True)

                    ResetCampi()
                Else
                    SelUnsel(False)
                End If

            Else
                SelUnsel(False)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudiTransazione(False)
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: btnSalvaPag_Click - " & errore & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
    End Sub


    Private Sub ResetCampi()
        Me.txtImpPagamento.Text = ""
        Me.txtDataPagamento.Text = ""
        Me.txtDataRegistrazione.Text = ""

        Me.cmbTipoPagamento.SelectedValue = -1
        Me.tipoPagValue.Value = Me.rdbTipoIncasso.SelectedValue
        Me.SumSelected.Value = 0
        Me.ResCredito.Value = 0


        Me.txtnote.Text = ""
        Me.idIncasso.Value = 0
        Me.impWritePagamento.Value = 0

        Me.BolloPagParz.Value = "0"


        CaricaBollNonPagate()


    End Sub

    Private Function AggiornaDatiIncasso() As Boolean
        AggiornaDatiIncasso = False

        par.cmd.CommandText = "UPDATE siscom_mi.INCASSI_INGIUNZIONE SET IMPORTO = " & par.VirgoleInPunti(Me.impWritePagamento.Value) & ", DATA_PAGAMENTO = '" & par.AggiustaData(Me.txtDataPagamento.Text) & "' , DATA_REGISTRAZIONE = '" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "', " _
                            & "DATA_ORA='" & Format(Now, "yyyyMMddHHmmss") & "',ID_TIPO_PAG =" & Me.cmbTipoPagamento.SelectedValue & " ,MOTIVO_PAGAMENTO = '" & par.PulisciStrSql(Me.txtnote.Text) & "',ID_OPERATORE = " & Session.Item("id_operatore") & " " _
                            & "WHERE ID = " & idIncasso.Value
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "UPDATE siscom_mi.BOL_BOLLETTE_PAGAMENTI_ING SET DATA_PAGAMENTO = '" & par.AggiustaData(Me.txtDataPagamento.Text) & "', DATA_OPERAZIONE =  '" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "', DATA_VALUTA = '" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "' WHERE ID_INCASSO_ING  = " & idIncasso.Value
        par.cmd.ExecuteNonQuery()
        AggiornaDatiIncasso = True
    End Function

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Private Function Controlli() As Boolean
        Dim idBolControl As String = "" '<-- inserisco gli id delle bollette per le quali devo controllare che la data emissione non sia inferiore a quella di pagamento e/o valuta
        Controlli = False
        Dim anomaliaBolRat As Boolean = False
        Dim msgAnomalia As String = ""
        If String.IsNullOrEmpty(Me.txtDataPagamento.Text) Then
            msgAnomalia &= "\n- Definire la DATA PAGAMENTO"
            Controlli = False
        End If
        If String.IsNullOrEmpty(Me.txtDataRegistrazione.Text) Then
            msgAnomalia &= "\n- Definire la DATA VALUTA"
            Controlli = False
        End If
        If Me.cmbTipoPagamento.SelectedValue = -1 Then
            msgAnomalia &= "\n- Scegliere la Modalità di pagamento"
            Controlli = False

        End If

        If (par.IfEmpty(Me.txtImpPagamento.Text, 0)) = 0 Then
            msgAnomalia &= "\n- Definire l\'importo di pagamento"
            Controlli = False
        End If

        If rdbTipoModifica.SelectedValue = 1 Then
            If Me.rdbTipoIncasso.SelectedValue >= 0 Then
                If CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)) > CDec(par.IfEmpty(Me.SumSelected.Value, 0)) Then
                    msgAnomalia &= "\n- Non è consentito inserire un pagamento con importo a credito!"
                    Controlli = False
                End If

                If SumSelected.Value <> txtImpPagamento.Text Then
                    msgAnomalia &= "\n- La somma degli importi è diversa dall\'importo del pagamento"
                    Controlli = False
                End If
            End If

            If Me.rdbTipoIncasso.SelectedValue = 0 Then

                Dim NumSelected As Integer = 0
                For Each ri As DataGridItem In dgvBollVoci.Items
                    If CType(ri.Cells(par.IndDGC(dgvBollVoci, "SELEZIONE")).FindControl("chkSel"), CheckBox).Checked = True Then
                        NumSelected += 1
                        idBolControl += ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text & ","
                    End If
                Next
                If NumSelected = 0 Then
                    msgAnomalia &= "\n- Selezionare almeno una bolletta"
                    Controlli = False
                End If


            End If
        End If
        If Not String.IsNullOrEmpty(msgAnomalia) Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ATTENZIONE!" & msgAnomalia & "\nOPERAZIONE ANNULLATA!');", True)
            Exit Function
        End If

        Controlli = True
    End Function
End Class
