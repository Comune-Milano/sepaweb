
Partial Class Contratti_Pagamenti_IncassiRuolo
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsNothing(Session.Item("PGMANUALERUOLI" & vIdConnessione.Value)) Then
            Me.connData = New CM.datiConnessione(par, False, False)
        Else
            Me.connData = CType(HttpContext.Current.Session.Item("PGMANUALERUOLI" & vIdConnessione.Value), CM.datiConnessione)
            par.SettaCommand(par)
        End If
        If Not IsPostBack Then

            ''lock db
            PageID.Value = par.getPageId
            If IsNothing(Session.Item("PGMANUALERUOLI" & vIdConnessione.Value)) Then
                vIdConnessione.Value = Format(Now, "yyyyMMddHHmmss")
            End If
            vIdContratto.Value = Request.QueryString("IDCONT")
            vIdAnagrafica.Value = Request.QueryString("IDANA")

            OPEN_RU.Value = Request.QueryString("OPRU")


            Me.txtDataPagamento.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtDataPagamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataRegistrazione.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtDataRegistrazione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")



            Me.txtImpPagamento.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            Me.txtImpPagamento.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")

            Me.rdbTipoIncasso.SelectedValue = -1
            Me.tipoPagValue.Value = -1
            CaricaInfo()
            CaricaBollNonPagate()
            If contrLocked.Value = 1 Then
                FrmSoloLettura()
                If Not IsNothing(HttpContext.Current.Session.Item("PGMANUALERUOLI" & vIdConnessione.Value)) Then
                    CType(HttpContext.Current.Session.Item("PGMANUALERUOLI" & vIdConnessione.Value), CM.datiConnessione).chiudi(False)
                    HttpContext.Current.Session.Remove("PGMANUALERUOLI" & vIdConnessione.Value)
                End If
            End If
        Else
            If flReload.Value = 1 Then
                ResetCampi()

                flReload.Value = 0
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "x", "validNavigation=true;", True)

            End If
        End If
    End Sub

    Private Sub FrmSoloLettura()
        Try
            Me.txtDataPagamento.Enabled = False
            Me.txtDataRegistrazione.Enabled = False
            Me.txtImpPagamento.Enabled = False
            Me.rdbTipoIncasso.Enabled = False
            Me.btnSalvaPag.Visible = False

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

    Private Sub CaricaInfo()
        Try
            ' *******************APERURA CONNESSIONE E MANTENIMENTO DELLA STESSA*********************
            connData.apri(False)
            connData.apriTransazione()
            HttpContext.Current.Session.Add("PGMANUALERUOLI" & vIdConnessione.Value, connData)

            hiddenLockCorrenti.Value = "RAPPORTI_UTENZA_" & vIdContratto.Value
            Dim risultato = par.EseguiLock(PageID.Value, hiddenLockCorrenti.Value)
            Select Case risultato.esito
                Case SepacomLock.EsitoLockUnlock.Success
                    'par.modalDialogMessage("Attenzione", "successo", Me.Page)
                Case SepacomLock.EsitoLockUnlock.InUso
                    par.modalDialogMessage("Attenzione", "In uso da altro operatore", Me.Page)
                    SoloLettura.Value = 1
                    FrmSoloLettura()
                Case Else
                    ' ''Beep()
            End Select

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
                    & "AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND ID_ANAGRAFICA = " & vIdAnagrafica.Value & " AND RAPPORTI_UTENZA.ID = " & Request.QueryString("IDCONT")




            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then

                txtintestatario.Text = par.IfNull(dt.Rows(0).Item("INTESTATARIO"), "")
                Me.vIdUnita.Value = par.IfNull(dt.Rows(0).Item("ID_UNITA"), 0)
                Me.DataGridContratto.DataSource = dt
                Me.DataGridContratto.DataBind()
                par.caricaComboBox("select id, UPPER(descrizione) as descrizione from siscom_mi.TIPO_PAG_RUOLO order by descrizione asc", cmbTipoPagamento, "id", "descrizione")
            End If
            'par.cmd.CommandText = "select nvl(valore,'19000101') from siscom_mi.parametri_bolletta where id = 48"
            'Me.bloccoData.Value = par.IfNull(par.cmd.ExecuteScalar, "19000101")
            'par.cmd.CommandText = "select nvl(valore,'19000101') from siscom_mi.parametri_bolletta where id = 49"
            'Me.bloccoRangeData.Value = par.IfNull(par.cmd.ExecuteScalar, "19000101")

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - CaricaInfo - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
    End Sub

    Private Sub ResetCampi()
        Me.txtImpPagamento.Text = ""
        Me.txtDataPagamento.Text = ""
        Me.txtDataRegistrazione.Text = ""

        If ObbFiltro.Value = 1 Then
            Me.rdbTipoIncasso.SelectedValue = "-1"
            OldSelTipoPag.Value = "-1"
        End If

        Me.cmbTipoPagamento.SelectedValue = -1
        Me.tipoPagValue.Value = Me.rdbTipoIncasso.SelectedValue
        Me.SumSelected.Value = 0
        Me.ResCredito.Value = 0

        Me.NonAttrib.Value = 0
        Me.txtnote.Text = ""
        Me.idIncasso.Value = 0
        Me.impWritePagamento.Value = 0
        Me.FiltNumBol.Value = ""
        OldSelTipoPag.Value = "-1"
        Me.BolloPagParz.Value = "0"
        txtqs.Visible = False
        chkSgravio.Checked = False
        CaricaBollNonPagate()
        txtqs.Visible = False

    End Sub

    Private Sub CaricaBollNonPagate()
        Try

            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If

            par.cmd.CommandText = "select count(bol_bollette_voci.id) from siscom_mi.bol_bollette_voci,siscom_mi.bol_bollette " _
                                & " where id_bolletta = bol_bollette.id and FL_ANNULLATA = '0' AND ID_BOLLETTA_RIC IS NULL AND ID_RATEIZZAZIONE IS NULL " _
                                & " AND NVL(IMPORTO_RUOLO,0) > 0 " _
                                & " AND round(nvl(imp_ruolo_pagato,0) ,2) < round(IMPORTO_RUOLO ,2) " _
                                & " AND BOL_BOLLETTE.ID_STATO <> -1 AND  BOL_BOLLETTE.ID_CONTRATTO = " & vIdContratto.Value _
                                & " ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC ,BOL_BOLLETTE.data_emissione ASC,BOL_BOLLETTE.ID ASC "
            Dim resConta As Integer = 0
            resConta = par.cmd.ExecuteScalar
            If resConta > 500 Then
                ObbFiltro.Value = 1
            End If
            par.cmd.CommandText = "SELECT  BOL_BOLLETTE.ID AS ID_BOLLETTA," _
                                & "('<a href=""#"" onclick=""javascript:validNavigation=true;window.open(''../../Contabilita/DettaglioBolletta.aspx?IDCONT='||ID_CONTRATTO||'&IDBOLL='||ID||'&IDANA=" & Request.QueryString("IDANA") & "'',''DET_'||NUM_BOLLETTA||''','''');validNavigation=false;void(0);"">'||NUM_BOLLETTA||'</a>') as NUM_BOLLETTA," _
                                & "BOL_BOLLETTE.N_RATA,GETDATA(DATA_EMISSIONE) AS DATA_EMISSIONE," _
                                & "(SELECT ACRONIMO FROM SISCOM_MI.TIPO_BOLLETTE,SISCOM_MI.BOL_BOLLETTE BOLBOLLETTE WHERE TIPO_BOLLETTE.ID(+)=BOLBOLLETTE.ID_TIPO AND BOL_BOLLETTE.ID=BOLBOLLETTE.ID) as TIPOBOLL,GETDATA(DATA_SCADENZA)AS DATA_SCADENZA,TRIM(TO_CHAR(NVL(BOL_BOLLETTE.imp_ruolo_pagato,0),'9G999G999G990D99'))AS imp_ruolo_pagato,TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMPORTO_RUOLO,0),'9G999G999G990D99'))AS IMPORTO_RUOLO, " _
                                & "(GETDATA(riferimento_da)||' al '||GETDATA(riferimento_a)) AS RIFERIMENTO,GETDATA(DATA_PAGAMENTO) AS DATA_PAGAMENTO,TRIM(TO_CHAR(NVL((NVL(IMPORTO_RUOLO,0)) - (NVL(imp_ruolo_pagato,0)),0),'9G999G999G990D99')) AS RESIDUO " _
                                & "FROM siscom_mi.BOL_BOLLETTE " _
                                & "WHERE FL_ANNULLATA = '0' AND ID_BOLLETTA_RIC IS NULL AND ID_RATEIZZAZIONE IS NULL AND NVL(IMPORTO_RUOLO,0) > 0 " _
                                & "AND BOL_BOLLETTE.ID_STATO <> -1 AND  BOL_BOLLETTE.ID_CONTRATTO = " & vIdContratto.Value & " ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC ,BOL_BOLLETTE.data_emissione ASC,BOL_BOLLETTE.ID ASC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            da.Dispose()
            If dt.Select("TIPOBOLL = 'RAT'").Length > 0 Then
                containsRat.Value = 1
            Else
                containsRat.Value = 0
            End If
            Dim dtFinale As New Data.DataTable


            dtFinale = dt

            '************AZZERO TOTALI***********
            totPagabile.Value = 0
            totPagato.Value = 0
            totResiduo.Value = 0
            '************AZZERO TOTALI***********
            Dim row As Data.DataRow
            For Each row In dt.Rows
                totPagabile.Value += CDec((par.IfNull(row.Item("IMPORTO_RUOLO"), 0)))
                totPagato.Value += CDec(par.IfNull(row.Item("imp_ruolo_pagato"), 0))
                totResiduo.Value += CDec(par.IfNull(row.Item("RESIDUO"), 0))
            Next


            row = dtFinale.NewRow()
            row.Item("NUM_BOLLETTA") = "TOTALE"
            row.Item("IMPORTO_RUOLO") = Format(CDec(totPagabile.Value), "##,##0.00")
            row.Item("imp_ruolo_pagato") = Format(CDec(totPagato.Value), "##,##0.00")
            row.Item("RESIDUO") = Format(CDec(totResiduo.Value), "##,##0.00")

            dtFinale.Rows.Add(row)
            Me.dgvBollVoci.DataSource = dtFinale
            Me.dgvBollVoci.DataBind()

            Me.lblTotRuolo.Text = "€." & Format(CDec(totPagabile.Value), "##,##0.00")
            Me.lblPagatoRuolo.Text = "€." & Format(CDec(totPagato.Value), "##,##0.00")
            Me.lblRuoloDaPag.Text = "€." & Format(CDec(totResiduo.Value), "##,##0.00")
            GestioneGrafica(dtFinale)
            JsFunzioniDgv(dgvBollVoci)
            'If String.IsNullOrEmpty(filtro) Then
            GestioneTipoIncasso(Me.rdbTipoIncasso.SelectedValue)
            'End If
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

                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_RUOLO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_RUOLO")).ForeColor = Drawing.Color.Navy

                ri.Cells(par.IndDGC(dgvBollVoci, "imp_ruolo_pagato")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "imp_ruolo_pagato")).ForeColor = Drawing.Color.Navy

                ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).ForeColor = Drawing.Color.Navy

            ElseIf idVoce <> -1 Then
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Visible = False

                ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).Font.Size = 8
                ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).HorizontalAlign = HorizontalAlign.Left


                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_RUOLO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_RUOLO")).Font.Size = 8


                ri.Cells(par.IndDGC(dgvBollVoci, "imp_ruolo_pagato")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "imp_ruolo_pagato")).Font.Size = 8


                ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Size = 8
            ElseIf idBolletta <> -1 Then
                If ri.Cells(par.IndDGC(dgvBollVoci, "imp_ruolo_pagato")).Text = ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_RUOLO")).Text Then
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Visible = False
                End If
            End If
        Next


        '************************* ULTIMA RIGA BOLLETTE NON PAGATE **********************************
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(0).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(0).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(0).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).BackColor = Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).BackColor

        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_RUOLO")).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_RUOLO")).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_RUOLO")).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_RUOLO")).BackColor = Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).BackColor

        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "imp_ruolo_pagato")).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "imp_ruolo_pagato")).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "imp_ruolo_pagato")).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "imp_ruolo_pagato")).BackColor = Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).BackColor

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


        If Me.rdbTipoIncasso.SelectedValue >= 0 Then
            If CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)) > CDec(par.IfEmpty(Me.SumSelected.Value, 0)) Then
                msgAnomalia &= "\n- Non è consentito inserire un pagamento con importo a credito!"
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

        If Not String.IsNullOrEmpty(msgAnomalia) Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ATTENZIONE!" & msgAnomalia & "\nOPERAZIONE ANNULLATA!');", True)
            Exit Function
        End If
        If Me.cmbTipoPagamento.SelectedValue <> 5 Then
            Me.txtNumAssegno.Text = ""
        End If

        Controlli = True
    End Function

    Private Function PagamentoAutomatico(ByRef errore As String) As Boolean
        PagamentoAutomatico = False
        errore = "PagamentoAutomatico"
        While impWritePagamento.Value > 0

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.id,importo_ruolo,round(NVL (imp_ruolo_pagato, 0),2) as imp_ruolo_pagato, " _
                                & "(importo_ruolo - round(NVL (imp_ruolo_pagato, 0),2)) as residuo,data_scadenza,data_emissione, " _
                                & "(CASE WHEN data_scadenza < TRIM(TO_CHAR(SYSDATE, 'YYYYMMDD')) THEN 1 ELSE 0 END) AS scaduta " _
                                & "FROM siscom_mi.BOL_BOLLETTE " _
                                & "WHERE " _
                                & "round(NVL (imp_ruolo_pagato, 0),2) < round(IMPORTO_RUOLO,2) AND nvl(IMPORTO_RUOLO,0) > 0 " _
                                & "AND FL_ANNULLATA = '0' " _
                                & "AND BOL_BOLLETTE.ID_CONTRATTO = " & vIdContratto.Value _
                                & " ORDER BY scaduta DESC " _
                                & " , BOL_BOLLETTE.DATA_SCADENZA ASC " _
                                & " ,BOL_BOLLETTE.data_emissione ASC " _
                                & " ,BOL_BOLLETTE.id ASC " _
                                & " ,(importo_ruolo-NVL(imp_ruolo_pagato,0)) DESC "
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
            impPagato = Math.Round(par.IfEmpty(rVoce("importo_ruolo").ToString, 0) - par.IfEmpty(rVoce("imp_ruolo_pagato").ToString, 0), 2)
            If impPagato <> 0 Then
                par.cmd.CommandText = "update siscom_mi.bol_bollette set imp_ruolo_pagato = (nvl(imp_ruolo_pagato,0) + " & par.VirgoleInPunti(impPagato) & ") where id =  " & rVoce("id").ToString
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
            pagabile = Math.Round(par.IfEmpty(rVoce("importo_ruolo").ToString, 0) - par.IfEmpty(rVoce("imp_ruolo_pagato").ToString, 0), 2)
            If pagabile > 0 Then
                If disponibilita > pagabile Then
                    impPagato = pagabile
                Else
                    impPagato = disponibilita

                End If
                par.cmd.CommandText = "update siscom_mi.bol_bollette set imp_ruolo_pagato = (nvl(imp_ruolo_pagato,0) + " & par.VirgoleInPunti(impPagato) & ") where id =  " & rVoce("id").ToString
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
            par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO (ID_BOLLETTA,DATA_PAGAMENTO,DATA_OPERAZIONE,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_RUOLO,DATA_VALUTA) VALUES " _
                    & "(" & voce & ",'" & par.AggiustaData(Me.txtDataPagamento.Text) & "','" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "'," & par.VirgoleInPunti(pagato) & "," & cmbTipoPagamento.SelectedValue & "," & Me.idIncasso.Value & ",'" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "')"
            par.cmd.ExecuteNonQuery()
        End If
    End Sub



    Protected Sub btnSalvaPag_Click(sender As Object, e As System.EventArgs) Handles btnSalvaPag.Click
        Dim errore As String = ""
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If

            If Me.confPagamento.Value = 1 Then 'conferma scrittura pagamento
                If Controlli() = True Then ' controlli campi obbligatori
                    Me.impWritePagamento.Value = CDec(Me.txtImpPagamento.Text.Replace(".", ""))
                    If scriviIncasso(errore) = True Then
                        Select Case rdbTipoIncasso.SelectedValue
                            Case -1
                                If PagamentoAutomatico(errore) = False Then
                                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                                    connData.chiudiTransazione(False)
                                    'riapro la transazione per successivi comandi
                                    connData.apriTransazione()

                                    Exit Sub
                                End If
                            Case 0
                                '**********************************PAGAMENTO AUTOMATICO***************************
                                If PagamentoSemiAutomatico(errore) = False Then
                                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                                    connData.chiudiTransazione(False)
                                    'riapro la transazione per successivi comandi
                                    connData.apriTransazione()
                                    Exit Sub
                                End If
                        End Select
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                        connData.chiudiTransazione(False)
                        'riapro la transazione per successivi comandi
                        connData.apriTransazione()
                        Exit Sub
                    End If
                    'AggiornaBollette()

                    connData.chiudiTransazione(True)
                    connData.apriTransazione()
                    If OPEN_RU.Value = "1" Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('INCASSO REGISTRATO CORRETTAMENTE!');if (opener.document.getElementById('AGGBOLL')){opener.document.getElementById('AGGBOLL').value=1};opener.document.getElementById('form1').submit();", True)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('INCASSO REGISTRATO CORRETTAMENTE!');", True)
                    End If

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
                par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            End If

            Session.Add("ERRORE", "Provenienza: btnSalvaPag_Click - " & errore & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
    End Sub

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

        par.cmd.CommandText = "select bol_bollette.id,importo_ruolo,nvl(imp_ruolo_pagato,0) as imp_ruolo_pagato from siscom_mi.bol_bollette " _
                            & " where id = " & idBolletta & "  order by importo_ruolo ASC"

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable()
        da.Fill(dt)
        Dim Pagabile As Decimal = dt.Compute("SUM(importo_ruolo)", String.Empty) - dt.Compute("SUM(imp_ruolo_pagato)", String.Empty)
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

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function


    Private Function scriviIncasso(ByRef errore As String) As Boolean
        scriviIncasso = False

        errore = "scriviIncasso"
        If idIncasso.Value = 0 Then
            par.cmd.CommandText = "select SISCOM_MI.SEQ_INCASSI_RUOLI.nextval from dual"
            idIncasso.Value = par.cmd.ExecuteScalar

            par.cmd.CommandText = "insert into siscom_mi.INCASSI_RUOLI (id,id_tipo_pag,motivo_pagamento," _
                                & "id_contratto,data_pagamento,data_registrazione,fl_annullata,importo," _
                                & "id_operatore,numero_assegno,fl_annullabile,data_ora,fl_sgravio) values " _
                                & "(" & idIncasso.Value & "," & Me.cmbTipoPagamento.SelectedValue & ", " _
                                & "'" & par.PulisciStrSql(Me.txtnote.Text.ToUpper) & "', " & vIdContratto.Value & ", " _
                                & "'" & par.AggiustaData(Me.txtDataPagamento.Text) & "','" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "',0," & par.VirgoleInPunti(Me.impWritePagamento.Value) & "," _
                                & Session.Item("ID_OPERATORE") & ",'" & par.PulisciStrSql(txtNumAssegno.Text.ToUpper) & "',1,'" & Format(Now, "yyyyMMddHHmmss") & "'," & Valore01(chkSgravio.Checked) & ")"

            par.cmd.ExecuteNonQuery()
        Else
            Me.impWritePagamento.Value = CDec(Me.txtImpPagamento.Text.Replace(".", "") - PagInMorosita.Value)

            par.cmd.CommandText = "UPDATE siscom_mi.INCASSI_RUOLI SET IMPORTO = " & par.VirgoleInPunti(CDec(Me.txtImpPagamento.Text.Replace(".", ""))) & ", DATA_PAGAMENTO = '" & par.AggiustaData(Me.txtDataPagamento.Text) & "' , DATA_REGISTRAZIONE = '" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "', " _
                                & "ID_TIPO_PAG =" & Me.cmbTipoPagamento.SelectedValue & " ,MOTIVO_PAGAMENTO = '" & par.PulisciStrSql(Me.txtnote.Text) & "', numero_assegno = '" & par.PulisciStrSql(txtNumAssegno.Text.ToUpper) & "' " _
                                & "WHERE ID = " & idIncasso.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO SET DATA_PAGAMENTO = '" & par.AggiustaData(Me.txtDataPagamento.Text) & "', DATA_REGISTRAZIONE =  '" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "', DATA_VALUTA = '" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "' WHERE ID_INCASSO_RUOLO  = " & idIncasso.Value
            par.cmd.ExecuteNonQuery()
        End If

        scriviIncasso = True
        errore = ""

    End Function

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        If Not IsNothing(HttpContext.Current.Session.Item("PGMANUALERUOLI" & vIdConnessione.Value)) Then
            CType(HttpContext.Current.Session.Item("PGMANUALERUOLI" & vIdConnessione.Value), CM.datiConnessione).chiudi(False)
            HttpContext.Current.Session.Remove("PGMANUALERUOLI" & vIdConnessione.Value)
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
        End If
        HFExitForce.Value = "1"
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "x", "validNavigation=true;self.close();", True)
    End Sub

    Protected Sub rdbTipoIncasso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbTipoIncasso.SelectedIndexChanged
        GestioneTipoIncasso(Me.rdbTipoIncasso.SelectedValue)
        Me.tipoPagValue.Value = Me.rdbTipoIncasso.SelectedValue

        CaricaBollNonPagate()
    End Sub

    Private Sub GestioneTipoIncasso(ByVal tipo As Integer)
        Dim idBolletta As Integer = 0
        Dim idVoce As Integer = 0
        Me.SumSelected.Value = 0
        Me.bollette.Style.Value = Me.bollette.Style.Value & " visibility: visible;"
        Me.tblAutomatica.Style.Value = Me.tblAutomatica.Style.Value & " visibility: visible;"
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
    End Sub

End Class
