
Partial Class Contratti_Pagamenti_PagaManuale
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim lstBolliParz As New System.Collections.Generic.List(Of String)

    Public Property CTRAperto() As Integer
        Get
            If Not (ViewState("par_CTRAperto") Is Nothing) Then
                Return CInt(ViewState("par_CTRAperto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_CTRAperto") = value
        End Set

    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsNothing(Session.Item("PGMANUALE" & vIdConnessione.Value)) Then
            Me.connData = New CM.datiConnessione(par, False, False)
        Else
            Me.connData = CType(HttpContext.Current.Session.Item("PGMANUALE" & vIdConnessione.Value), CM.datiConnessione)
            par.SettaCommand(par)
        End If
        If Not IsPostBack Then
            If IsNothing(Session.Item("PGMANUALE" & vIdConnessione.Value)) Then
                vIdConnessione.Value = Format(Now, "yyyyMMddHHmmss")
            End If

            If IsNothing(Request.QueryString("IDCON")) = False Then
                CTRAperto = 1
            Else
                CTRAperto = 0
            End If

            vIdContratto.Value = Request.QueryString("IDCONT")
            vIdAnagrafica.Value = Request.QueryString("IDANA")

            Me.txtDataPagamento.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtDataPagamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataRegistrazione.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtDataRegistrazione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.txtDataDal.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


            Me.txtDataAl.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.txtEmesDal.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtEmesDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtEmesAl.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtEmesAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.txtDataAssegno.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
            Me.txtDataAssegno.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")



            Me.txtImpPagamento.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            Me.txtImpPagamento.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);GestTableResidui(this);return false;")

            'Me.cmbTipoPagamento.Attributes.Add("onkeypress", "javascript:IsAssegno(this);")

            Me.rdbTipoIncasso.SelectedValue = -1
            Me.tipoPagValue.Value = -1
            CaricaInfo()
            CaricaBollNonPagate()
            If contrLocked.Value = 1 Then
                FrmSoloLettura()
                If Not IsNothing(HttpContext.Current.Session.Item("PGMANUALE" & vIdConnessione.Value)) Then
                    CType(HttpContext.Current.Session.Item("PGMANUALE" & vIdConnessione.Value), CM.datiConnessione).chiudi(False)
                    HttpContext.Current.Session.Remove("PGMANUALE" & vIdConnessione.Value)
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

    Private Sub CaricaInfo()
        Try
            ' *******************APERURA CONNESSIONE E MANTENIMENTO DELLA STESSA*********************
            connData.apri(False)
            connData.apriTransazione()
            HttpContext.Current.Session.Add("PGMANUALE" & vIdConnessione.Value, connData)

            Try

                If CTRAperto = 0 Then
                    ' *******************LOCK RU e CONTROLLO NON SIA LOCCATO*********************
                    par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where id = " & vIdContratto.Value & " for update nowait"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where id = " & vIdContratto.Value & " "
                    par.cmd.ExecuteNonQuery()
                End If

            Catch exLok As Oracle.DataAccess.Client.OracleException
                If exLok.Number = 54 Then
                    contrLocked.Value = 1
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Il contratto è aperto da un altro utente!\IMPOSSIBILE ESEGUIRE MODIFICHE SUI PAGAMENTI!');", True)
                    SoloLettura.Value = 1
                Else
                    If par.OracleConn.State = Data.ConnectionState.Open Then
                        connData.chiudi(False)
                    End If
                    Session.Add("ERRORE", "Provenienza: Proprieta - CaricaInfo - " & exLok.Message)
                    Response.Redirect("../../Errore.aspx", False)
                End If
            End Try

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
                par.caricaComboBox("select id, UPPER(descrizione) as descrizione from siscom_mi.tipo_pag_parz  WHERE UTILIZZABILE=1 order by descrizione asc", cmbTipoPagamento, "id", "descrizione")
            End If
            par.cmd.CommandText = "select nvl(valore,'19000101') from siscom_mi.parametri_bolletta where id = 48"
            Me.bloccoData.Value = par.IfNull(par.cmd.ExecuteScalar, "19000101")
            par.cmd.CommandText = "select nvl(valore,'19000101') from siscom_mi.parametri_bolletta where id = 49"
            Me.bloccoRangeData.Value = par.IfNull(par.cmd.ExecuteScalar, "19000101")

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - CaricaInfo - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
    End Sub
    Private Sub CaricaBollNonPagate(Optional filtro As String = "", Optional esclNoSaldo As Boolean = False)
        Try

            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If


            Dim NoSaldoTot As String = ""
            Dim NoSaldoPag As String = ""
            If esclNoSaldo = True Then
                NoSaldoTot = " - NVL(QUOTA_SIND_B,0) "
                NoSaldoPag = " - NVL(QUOTA_SIND_PAGATA_B,0) "
            End If

            Dim condizCompFacility As String = ""
            If rdbTipoIncasso.SelectedValue <> 2 Then
                '/* Condizione per escludere le bollette con voce di Compenso Facility (SD 164/2017)*/
                condizCompFacility = " AND siscom_mi.bol_bollette.id NOT IN (select id_bolletta from siscom_mi.bol_bollette_voci where id_voce in (126,182)) "
            End If

            par.cmd.CommandText = "select count(bol_bollette_voci.id) from siscom_mi.bol_bollette_voci,siscom_mi.bol_bollette " _
                                & " where id_bolletta = bol_bollette.id and FL_ANNULLATA = '0' AND ID_BOLLETTA_RIC IS NULL AND ID_RATEIZZAZIONE IS NULL " _
                                & " AND NVL(IMPORTO_RUOLO,0) = 0 AND NVL(IMPORTO_INGIUNZIONE,0) = 0 " _
                                & " AND round(nvl(IMPORTO_PAGATO,0) ,2) < round(IMPORTO_TOTALE ,2) AND IMPORTO_TOTALE  > 0" _
                                & " " & condizCompFacility & " AND BOL_BOLLETTE.ID_STATO <> -1 AND BOL_BOLLETTE.ID_CONTRATTO = " & vIdContratto.Value _
                                & " ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC ,BOL_BOLLETTE.data_emissione ASC,BOL_BOLLETTE.ID ASC "
            Dim resConta As Integer = 0
            resConta = par.cmd.ExecuteScalar
            If resConta > 500 Then
                ObbFiltro.Value = 1
            End If
            par.cmd.CommandText = "SELECT  '' AS ID_VOCE_BOL,'' as id_tipo_voce, BOL_BOLLETTE.ID AS ID_BOLLETTA,'' AS id_voce_aliquota," _
                                & "('<a href=""#"" onclick=""javascript:validNavigation=true;window.open(''../../Contabilita/DettaglioBolletta.aspx?IDCONT='||ID_CONTRATTO||'&IDBOLL='||ID||'&IDANA=" & Request.QueryString("IDANA") & "'',''DET_'||NUM_BOLLETTA||''','''');validNavigation=false;void(0);"">'||NUM_BOLLETTA||'</a>') as NUM_BOLLETTA," _
                                & "BOL_BOLLETTE.N_RATA,GETDATA(DATA_EMISSIONE) AS DATA_EMISSIONE," _
                                & "(SELECT ACRONIMO FROM SISCOM_MI.TIPO_BOLLETTE,SISCOM_MI.BOL_BOLLETTE BOLBOLLETTE WHERE TIPO_BOLLETTE.ID(+)=BOLBOLLETTE.ID_TIPO AND BOL_BOLLETTE.ID=BOLBOLLETTE.ID) as TIPOBOLL,GETDATA(DATA_SCADENZA)AS DATA_SCADENZA,TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMPORTO_PAGATO,0) " & NoSaldoPag & ",'9G999G999G990D99'))AS IMPORTO_PAGATO,TRIM(TO_CHAR(NVL(BOL_BOLLETTE.IMPORTO_TOTALE,0) " & NoSaldoTot & ",'9G999G999G990D99'))AS IMPORTO_TOTALE, " _
                                & "(GETDATA(riferimento_da)||' al '||GETDATA(riferimento_a)) AS RIFERIMENTO,GETDATA(DATA_PAGAMENTO) AS DATA_PAGAMENTO,TRIM(TO_CHAR(NVL((NVL(IMPORTO_TOTALE,0) " & NoSaldoTot & ") - (NVL(IMPORTO_PAGATO,0) " & NoSaldoPag & "),0),'9G999G999G990D99')) AS RESIDUO " _
                                & "FROM siscom_mi.BOL_BOLLETTE " _
                                & "WHERE FL_ANNULLATA = '0' " & condizCompFacility & " AND ID_BOLLETTA_RIC IS NULL AND ID_RATEIZZAZIONE IS NULL  AND NVL(IMPORTO_RUOLO,0) = 0  AND ID_BOLLETTA_STORNO IS NULL " _
                                & "AND NVL(IMPORTO_INGIUNZIONE,0) = 0 AND round(nvl(IMPORTO_PAGATO,0) " & NoSaldoPag & ",2) < round(IMPORTO_TOTALE " & NoSaldoTot & ",2) AND IMPORTO_TOTALE " & NoSaldoTot & " > 0 " _
                                & "AND BOL_BOLLETTE.ID_STATO <> -1 AND  BOL_BOLLETTE.ID_CONTRATTO = " & vIdContratto.Value & filtro & " ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC ,BOL_BOLLETTE.data_emissione ASC,BOL_BOLLETTE.ID ASC "
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

            If rdbTipoIncasso.SelectedValue <> -1 And rdbTipoIncasso.SelectedValue <> 0 Then
                dtFinale = AggiungiVoci(dt, esclNoSaldo)
            Else
                dtFinale = dt
            End If
            '************AZZERO TOTALI***********
            totPagabile.Value = 0
            totPagato.Value = 0
            totResiduo.Value = 0
            '************AZZERO TOTALI***********
            Dim row As Data.DataRow
            For Each row In dt.Rows
                totPagabile.Value += CDec((par.IfNull(row.Item("IMPORTO_TOTALE"), 0)))
                totPagato.Value += CDec(par.IfNull(row.Item("IMPORTO_PAGATO"), 0))
                totResiduo.Value += CDec(par.IfNull(row.Item("RESIDUO"), 0))
            Next


            row = dtFinale.NewRow()
            row.Item("NUM_BOLLETTA") = "TOTALE"
            row.Item("IMPORTO_TOTALE") = Format(CDec(totPagabile.Value), "##,##0.00")
            row.Item("IMPORTO_PAGATO") = Format(CDec(totPagato.Value), "##,##0.00")
            row.Item("RESIDUO") = Format(CDec(totResiduo.Value), "##,##0.00")

            dtFinale.Rows.Add(row)
            Me.dgvBollVoci.DataSource = dtFinale
            Me.dgvBollVoci.DataBind()

            Me.lblResiduo.Text = "RESIDUO INQUILINO €. " & Format(CDec(totResiduo.Value), "##,##0.00")
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
    Private Sub GestioneGrafica(ByVal dtFinale As Data.DataTable)
        ''**************GESTIONE GRAFICA ******************
        Dim idBolletta As Integer = 0
        Dim idVoce As Integer = 0
        For Each ri As DataGridItem In dgvBollVoci.Items
            idBolletta = 0
            idVoce = 0
            idBolletta = par.IfEmpty(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text.Replace("&nbsp;", ""), -1)
            idVoce = par.IfEmpty(ri.Cells(par.IndDGC(dgvBollVoci, "ID_VOCE_BOL")).Text.Replace("&nbsp;", ""), -1)

            If idBolletta = -1 And idVoce = -1 Then
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Visible = False
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = False

                ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).ForeColor = Drawing.Color.Navy
                ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).HorizontalAlign = HorizontalAlign.Center
                'ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).BackColor = Drawing.Color.LightYellow
                ' ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).BorderColor = Drawing.Color.Lime

                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).ForeColor = Drawing.Color.Navy
                'ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).BackColor = Drawing.Color.LightYellow
                'ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).BorderColor = Drawing.Color.Lime

                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).ForeColor = Drawing.Color.Navy
                'ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).BackColor = Drawing.Color.LightYellow
                'ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).BorderColor = Drawing.Color.Lime

                ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).ForeColor = Drawing.Color.Navy
                'ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).BackColor = Drawing.Color.LightYellow
                'ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).BorderColor = Drawing.Color.Lime
            ElseIf idVoce <> -1 Then
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).FindControl("chkSel"), CheckBox).Visible = False
                'ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).BackColor = Drawing.Color.LightYellow
                'ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).BorderColor = Drawing.Color.Black
                ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).Font.Size = 8
                ri.Cells(par.IndDGC(dgvBollVoci, "RIFERIMENTO")).HorizontalAlign = HorizontalAlign.Left

                'ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).BackColor = Drawing.Color.LightYellow
                'ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).BorderColor = Drawing.Color.Black
                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).Font.Size = 8

                'ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).BackColor = Drawing.Color.LightYellow
                'ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).BorderColor = Drawing.Color.Black
                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).Font.Size = 8

                'ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).BackColor = Drawing.Color.LightYellow
                'ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).BorderColor = Drawing.Color.Black
                ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Bold = False
                ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Size = 8

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

        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_TOTALE")).BackColor = Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).BackColor

        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "IMPORTO_PAGATO")).BackColor = Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).BackColor

        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Bold = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Italic = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Font.Underline = True
        Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).BackColor = Me.dgvBollVoci.Items(dtFinale.Rows.Count - 1).Cells(par.IndDGC(dgvBollVoci, "NUM_BOLLETTA")).BackColor

        '************************* ULTIMA RIGA BOLLETTE NON PAGATE **********************************

    End Sub
    Private Function AggiungiVoci(ByVal dt As Data.DataTable, Optional esclNoSaldo As Boolean = False) As Data.DataTable
        AggiungiVoci = New Data.DataTable
        Dim dtFinale As New Data.DataTable
        dtFinale = dt.Clone
        Try
            If dt.Rows.Count > 0 Then
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter
                Dim dtInt As New Data.DataTable()
                Dim row As Data.DataRow
                Dim filtNoSaldo As String = ""
                If esclNoSaldo = True Then
                    filtNoSaldo = " and t_voci_bolletta.FL_NO_SALDO = 0 "
                End If
                For Each r As Data.DataRow In dt.Rows
                    dtFinale.Rows.Add(r.ItemArray)
                    par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.ID AS ID_VOCE_BOL,ID_BOLLETTA,ID_VOCE as id_tipo_voce ,T_VOCI_BOLLETTA.DESCRIZIONE," _
                                        & "TRIM(TO_CHAR(nvl(IMPORTO,0),'9G999G999G990D99')) AS IMPORTO ,TRIM(TO_CHAR(nvl(IMP_PAGATO,0),'9G999G999G990D99')) AS IMP_PAGATO ,TRIM(TO_CHAR((IMPORTO - NVL(IMP_PAGATO,0)),'9G999G999G990D99')) AS RES_VOCE " _
                                        & "FROM siscom_mi.BOL_BOLLETTE_VOCI,siscom_mi.T_VOCI_BOLLETTA  " _
                                        & "WHERE IMPORTO <> 0 AND T_VOCI_BOLLETTA.ID = BOL_BOLLETTE_VOCI.ID_VOCE " & filtNoSaldo & " AND ID_BOLLETTA = " & r.Item("id_bolletta")
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    dtInt = New Data.DataTable
                    da.Fill(dtInt)
                    da.Dispose()
                    If dtInt.Rows.Count > 0 Then
                        row = dtFinale.NewRow()
                        row.Item("RIFERIMENTO") = "VOCI BOLLETTA"
                        row.Item("IMPORTO_TOTALE") = "V.TOT"
                        row.Item("IMPORTO_PAGATO") = "V.PAGATO"
                        row.Item("RESIDUO") = "V.RESIDUO"
                        dtFinale.Rows.Add(row)
                        For Each rint As Data.DataRow In dtInt.Rows
                            row = dtFinale.NewRow()
                            row.Item("ID_VOCE_BOL") = par.IfEmpty(rint.Item("ID_VOCE_BOL").ToString, 0)
                            row.Item("id_tipo_voce") = par.IfEmpty(rint.Item("id_tipo_voce").ToString, 0)
                            row.Item("ID_BOLLETTA") = par.IfEmpty(rint.Item("ID_BOLLETTA").ToString, 0)
                            row.Item("RIFERIMENTO") = par.IfEmpty(rint.Item("DESCRIZIONE").ToString, 0)
                            row.Item("IMPORTO_TOTALE") = par.IfEmpty(rint.Item("IMPORTO").ToString, 0)
                            row.Item("IMPORTO_PAGATO") = par.IfEmpty(rint.Item("IMP_PAGATO").ToString, 0)
                            row.Item("RESIDUO") = par.IfEmpty(rint.Item("RES_VOCE").ToString, 0)
                            dtFinale.Rows.Add(row)
                        Next
                    End If
                Next
            End If
            dtFinale.AcceptChanges()
            AggiungiVoci = dtFinale
        Catch ex As Exception

            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - AggiungiVoci - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
        Return AggiungiVoci

    End Function
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click

        Session.Remove("PagInMor2")
        Session.Remove("idIncassoDaMor")

        If Not IsNothing(HttpContext.Current.Session.Item("PGMANUALE" & vIdConnessione.Value)) Then
            CType(HttpContext.Current.Session.Item("PGMANUALE" & vIdConnessione.Value), CM.datiConnessione).chiudi(False)
            HttpContext.Current.Session.Remove("PGMANUALE" & vIdConnessione.Value)
        End If
        HFExitForce.Value = "1"
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "x", "validNavigation=true;self.close();", True)
    End Sub
    Private Sub JsFunzioniDgv(ByVal dgv As DataGrid)
        For Each ri As DataGridItem In dgv.Items
            CType(ri.Cells(par.IndDGC(dgv, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            CType(ri.Cells(par.IndDGC(dgv, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Attributes.Add("ondblclick", "javascript:AutoFill(this,'" & ri.Cells(par.IndDGC(dgv, "RESIDUO")).Text & "');")
            CType(ri.Cells(par.IndDGC(dgv, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Attributes.Add("onfocus", "javascript:oldValue=this.value;")
            CType(ri.Cells(par.IndDGC(dgv, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);SommaAutomatica(this," & ri.Cells(par.IndDGC(dgv, "RESIDUO")).Text.Replace(".", "").Replace(",", ".") & ");return false;")
            CType(ri.Cells(par.IndDGC(dgv, "SELEZIONE")).FindControl("chkSel"), CheckBox).Attributes.Add("onclick", "javascript:sommaChek(this," & ri.Cells(par.IndDGC(dgv, "RESIDUO")).Text.Replace(".", "").Replace(",", ".") & ");")

        Next
    End Sub

    Protected Sub rdbTipoIncasso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbTipoIncasso.SelectedIndexChanged

        '*************************AGGIUNTA FILTRO PER TROPPE BOLLETTE SUL CONTRATTO ******************************
        If Not String.IsNullOrEmpty(Session.Item("DATADAL")) Then
            Me.txtDataDal.Text = par.FormattaData(Session.Item("DATADAL"))
            Session.Remove("DATADAL")
        End If
        If Not String.IsNullOrEmpty(Session.Item("DATAAL")) Then
            Me.txtDataAl.Text = par.FormattaData(Session.Item("DATAAL"))
            Session.Remove("DATAAL")
        End If
        If Not String.IsNullOrEmpty(Session.Item("EMDAL")) Then
            Me.txtEmesDal.Text = par.FormattaData(Session.Item("EMDAL"))
            Session.Remove("EMDAL")
        End If
        If Not String.IsNullOrEmpty(Session.Item("EMAL")) Then
            Me.txtEmesAl.Text = par.FormattaData(Session.Item("EMAL"))
            Session.Remove("EMAL")
        End If
        If Not String.IsNullOrEmpty(Session.Item("NUMBOL")) Then
            Me.FiltNumBol.Value = Session.Item("NUMBOL")
            Session.Remove("NUMBOL")
        End If
        Dim annFilt As String = 0
        If Not String.IsNullOrEmpty(Session.Item("ANNULFILT")) Then
            annFilt = Session.Item("ANNULFILT")
            Session.Remove("ANNULFILT")
        End If
        If rdbTipoIncasso.SelectedValue <= 0 Or annFilt = 1 Then
            Me.FiltNumBol.Value = ""
            Me.txtDataDal.Text = ""
            Me.txtDataAl.Text = ""
            Me.txtEmesDal.Text = ""
            Me.txtEmesAl.Text = ""
            OldSelTipoPag.Value = "-1"
        End If
        If annFilt = 1 Then
            Me.rdbTipoIncasso.SelectedValue = -1
        End If

        '*************************AGGIUNTA FILTRO PER TROPPE BOLLETTE SUL CONTRATTO ******************************
        Dim filtro As String = ControlFiltro()
        If filtro = "-1" Then
            filtro = ""
        End If
        GestioneTipoIncasso(Me.rdbTipoIncasso.SelectedValue)
        Me.tipoPagValue.Value = Me.rdbTipoIncasso.SelectedValue

        'Me.cmbTipoPagamento.SelectedValue = "-1"
        'Me.txtNumAssegno.Text = ""

        If esclQSind() = True Then
            CaricaBollNonPagate(filtro, True)
            txtqs.Visible = True
        Else
            CaricaBollNonPagate(filtro, False)
            txtqs.Visible = False
        End If

        If Not String.IsNullOrEmpty(Session.Item("FILTAPPLICATO")) Then
            If Session.Item("FILTAPPLICATO") = 1 Then
                If rdbTipoIncasso.SelectedValue > 0 Then
                    Me.txtDataDal.Enabled = False
                    Me.txtDataAl.Enabled = False
                    Me.txtEmesDal.Enabled = False
                    Me.txtEmesAl.Enabled = False
                    Me.btnRefresh.Visible = False
                    Session.Remove("FILTAPPLICATO")
                End If
            End If
        Else
            Me.txtDataDal.Enabled = True
            Me.txtDataAl.Enabled = True
            Me.txtEmesDal.Enabled = True
            Me.txtEmesAl.Enabled = True
            Me.btnRefresh.Visible = True

        End If
    End Sub
    Private Sub GestioneTipoIncasso(ByVal tipo As Integer)
        Dim idBolletta As Integer = 0
        Dim idVoce As Integer = 0
        Dim idTipoVoce As Integer = 0
        Me.SumSelected.Value = 0
        Me.bollette.Style.Value = Me.bollette.Style.Value & " visibility: visible;"
        Me.dgvBollVoci.Visible = True
        Me.tblAutomatica.Style.Value = Me.tblAutomatica.Style.Value & " visibility: visible;"
        Me.txtDataDal.Visible = True
        Me.txtDataAl.Visible = True
        Me.txtEmesDal.Visible = True
        Me.txtEmesAl.Visible = True
        Me.filtDate.Style.Value = " display : block; "

        Me.btnRefresh.Visible = True
        Me.txtPagResoconto.Text = Me.txtImpPagamento.Text
        If tipo = -1 Then
            Me.tblAutomatica.Style.Value = "border: 1px solid #66FF33;  background-color: #F2EBBF;visibility: visible;"
            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "IMPORTO €")).Visible = False
            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "SELEZIONE")).Visible = False

            'Me.dgvBollVoci.Visible = False
            Me.tblAutomatica.Style.Value = Me.tblAutomatica.Style.Value & " visibility: hidden;"
            Me.txtDataDal.Visible = False
            Me.txtDataAl.Visible = False
            Me.txtEmesDal.Visible = False
            Me.txtEmesAl.Visible = False
            Me.filtDate.Style.Value = " display : none; "
            Me.btnRefresh.Visible = False

        ElseIf tipo = 0 Then

            'Me.txtImpPagamento.Text = ""
            Me.txtImpPagamento.ReadOnly = False
            For Each ri As DataGridItem In dgvBollVoci.Items
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("imgSelezionaMor"), ImageButton).Visible = False
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text = ""
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "SELEZIONE")).FindControl("chkSel"), CheckBox).Checked = False
            Next
            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "IMPORTO €")).Visible = False
            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "SELEZIONE")).Visible = True
            Me.tblAutomatica.Style.Value = "border: 1px solid #66FF33;  background-color: #F2EBBF;visibility: visible;"

        ElseIf tipo = 1 Then
            'Me.txtImpPagamento.Text = ""
            Me.tblAutomatica.Style.Value = "border: 1px solid #66FF33; background-color: #F2EBBF; visibility: visible;"

            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "IMPORTO €")).Visible = True
            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "SELEZIONE")).Visible = False
            For Each ri As DataGridItem In dgvBollVoci.Items
                '12/02/2015 Aggiungo condizione per bollette MOR
                If ri.Cells(par.IndDGC(dgvBollVoci, "TIPOBOLL")).Text = "MOR" Then
                    idTipoBoll.Value = "4"
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = False
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("imgSelezionaMor"), ImageButton).Visible = True
                Else
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("imgSelezionaMor"), ImageButton).Visible = False
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text = ""
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "SELEZIONE")).FindControl("chkSel"), CheckBox).Checked = False

                    idBolletta = 0
                    idVoce = 0
                    idBolletta = par.IfEmpty(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text.Replace("&nbsp;", ""), -1)
                    idVoce = par.IfEmpty(ri.Cells(par.IndDGC(dgvBollVoci, "ID_VOCE_BOL")).Text.Replace("&nbsp;", ""), -1)
                    If idBolletta <> -1 And idVoce = -1 Then
                        CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = True
                    ElseIf idBolletta <> -1 And idVoce <> -1 Then
                        CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = False
                    End If
                End If
                '12/02/2015 FINE Aggiungo condizione per bollette MOR
            Next
        ElseIf tipo = 2 Then
            'Me.txtImpPagamento.Text = ""
            Me.tblAutomatica.Style.Value = "border: 1px solid #66FF33; background-color: #F2EBBF; visibility: visible;"

            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "IMPORTO €")).Visible = True
            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "SELEZIONE")).Visible = False

            For Each ri As DataGridItem In dgvBollVoci.Items
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("imgSelezionaMor"), ImageButton).Visible = False
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text = ""
                CType(ri.Cells(par.IndDGC(dgvBollVoci, "SELEZIONE")).FindControl("chkSel"), CheckBox).Checked = False

                idBolletta = 0
                idVoce = 0
                idBolletta = par.IfEmpty(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text.Replace("&nbsp;", ""), -1)
                idVoce = par.IfEmpty(ri.Cells(par.IndDGC(dgvBollVoci, "ID_VOCE_BOL")).Text.Replace("&nbsp;", ""), -1)
                idTipoVoce = par.IfEmpty(ri.Cells(par.IndDGC(dgvBollVoci, "id_tipo_voce")).Text.Replace("&nbsp;", ""), -1)

                If idBolletta <> -1 And idVoce = -1 Then
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = False
                ElseIf idBolletta <> -1 And idVoce <> -1 Then
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = True
                End If

                If idTipoVoce = 150 Then
                    idTipoBoll.Value = "4"
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = False
                    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("imgSelezionaMor"), ImageButton).Visible = True
                End If

                'If idTipoVoce = 126 Or idTipoVoce = 182 Then
                '    CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Enabled = False
                'End If
            Next
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "jsc", "GestTableResidui(document.getElementById('txtImpPagamento'));", True)

    End Sub
    Private Function ControlFiltro() As String
        ControlFiltro = ""
        If Not String.IsNullOrEmpty(Me.txtDataDal.Text) Then
            If Not String.IsNullOrEmpty(Me.txtDataAl.Text) Then
                If par.AggiustaData(Me.txtDataDal.Text) > par.AggiustaData(Me.txtDataAl.Text) Then
                    ControlFiltro = "-1"
                    Exit Function
                Else
                    ControlFiltro += " AND BOL_BOLLETTE.RIFERIMENTO_DA>='" & par.AggiustaData(Me.txtDataDal.Text) & "' "

                End If
            Else
                ControlFiltro += " AND BOL_BOLLETTE.RIFERIMENTO_DA>='" & par.AggiustaData(Me.txtDataDal.Text) & "' "
            End If
        End If


        If Not String.IsNullOrEmpty(Me.txtDataAl.Text) Then
            If Not String.IsNullOrEmpty(Me.txtDataDal.Text) Then
                If par.AggiustaData(Me.txtDataAl.Text) < par.AggiustaData(Me.txtDataDal.Text) Then
                    ControlFiltro = "-1"
                    Exit Function
                Else
                    ControlFiltro += " AND BOL_BOLLETTE.RIFERIMENTO_A<='" & par.AggiustaData(Me.txtDataAl.Text) & "' "

                End If
            Else
                ControlFiltro += " AND BOL_BOLLETTE.RIFERIMENTO_DA<='" & par.AggiustaData(Me.txtDataAl.Text) & "' "
            End If
        End If

        '**************FILTRO SU EMISSIONE DELLA BOLLETTA 

        If Not String.IsNullOrEmpty(Me.txtEmesDal.Text) Then
            If Not String.IsNullOrEmpty(Me.txtEmesAl.Text) Then
                If par.AggiustaData(Me.txtEmesDal.Text) > par.AggiustaData(Me.txtEmesAl.Text) Then
                    ControlFiltro = "-1"
                    Exit Function
                Else
                    ControlFiltro += " AND BOL_BOLLETTE.DATA_EMISSIONE>='" & par.AggiustaData(Me.txtEmesDal.Text) & "' "

                End If
            Else
                ControlFiltro += " AND BOL_BOLLETTE.DATA_EMISSIONE>='" & par.AggiustaData(Me.txtEmesDal.Text) & "' "
            End If
        End If


        If Not String.IsNullOrEmpty(Me.txtEmesAl.Text) Then
            If Not String.IsNullOrEmpty(Me.txtEmesDal.Text) Then
                If par.AggiustaData(Me.txtEmesAl.Text) < par.AggiustaData(Me.txtEmesDal.Text) Then
                    ControlFiltro = "-1"
                    Exit Function
                Else
                    ControlFiltro += " AND BOL_BOLLETTE.DATA_EMISSIONE<='" & par.AggiustaData(Me.txtEmesAl.Text) & "' "

                End If
            Else
                ControlFiltro += " AND BOL_BOLLETTE.DATA_EMISSIONE<='" & par.AggiustaData(Me.txtEmesAl.Text) & "' "
            End If
        End If



        '**************FILTRO SU NUMERO DELLA BOLLETTA 
        If Not String.IsNullOrEmpty(Me.FiltNumBol.Value) Then
            ControlFiltro += " AND UPPER(BOL_BOLLETTE.NUM_BOLLETTA)='" & par.PulisciStrSql(Me.FiltNumBol.Value.ToUpper) & "' "
        End If
    End Function
    Protected Sub btnRefresh_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnRefresh.Click
        Dim filtro As String = ControlFiltro()
        If filtro <> "-1" Then
            If esclQSind() = True Then
                CaricaBollNonPagate(filtro, True)
                txtqs.Visible = True
            Else
                CaricaBollNonPagate(filtro, True)
                txtqs.Visible = False
            End If
            SelUnsel(False)
        Else
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Il periodo di riferimento non è valido!\Nessun filtro applicato sulle bollette!');", True)

        End If
    End Sub
    Private Sub FrmSoloLettura()
        Try
            Me.txtDataPagamento.Enabled = False
            Me.txtDataRegistrazione.Enabled = False
            Me.txtImpPagamento.Enabled = False
            Me.rdbTipoIncasso.Enabled = False
            Me.btnSalvaPag.Visible = False
            Me.txtDataDal.Enabled = False
            Me.txtDataAl.Enabled = False
            Me.txtEmesDal.Enabled = False
            Me.txtEmesAl.Enabled = False
            Me.cmbTipoPagamento.Enabled = False
            Me.btnRefresh.Visible = False
            Me.dgvBollVoci.Columns(par.IndDGC(dgvBollVoci, "IMPORTO €")).Visible = False
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


    Protected Sub chkSelAll_CheckedChanged(sender As Object, e As System.EventArgs)
        If Not String.IsNullOrEmpty(Me.txtImpPagamento.Text) Then
            Me.txtPagResoconto.Text = Me.txtImpPagamento.Text
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
            If par.IfEmpty(txtImpPagamento.Text, 0) > 0 Then
                Me.txtPagResoconto.Text = Me.txtImpPagamento.Text
                Me.txtSommaSel.Text = Format(CDec(SumSelected.Value), "##,##0.00")

                If CDec(Me.txtPagResoconto.Text.Replace(".", "")) - CDec(SumSelected.Value) < 0 Then
                    Me.txtResResoconto.Text = Format(CDec(0), "##,##0.00")
                Else
                    Me.txtResResoconto.Text = Format(CDec(Me.txtPagResoconto.Text.Replace(".", "")) - CDec(SumSelected.Value), "##,##0.00")
                End If

            End If
        End If

    End Sub


#Region "SalvaPagamento"

    '' ''******************************************************************************************************
    '' ''******************************FUNZIONI SALVA PAGAMENTO MANUALE****************************************
    '' ''******************************************************************************************************

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
        If rdbTipoIncasso.SelectedValue <> 0 Then

            If CDec(par.IfEmpty(Me.SumSelected.Value, 0)) > CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)) Then
                msgAnomalia &= "\n- La somma degli importi non può superare l\'importo del pagamento"
                Controlli = False
            End If
        End If

        If Me.cmbTipoPagamento.SelectedValue <> 8 Then
            'ULTERIORE CONTROLLO PELLEGRI 11/08/2016
            'If par.AggiustaData(Me.txtDataPagamento.Text) > Me.bloccoRangeData.Value Then
            '    'If par.AggiustaData(Me.txtDataPagamento.Text) < Me.bloccoData.Value Then
            '    'msgAnomalia &= "\n- Non è consentito inserire un pagamento con data inferiore a " & par.FormattaData(Me.bloccoData.Value)
            '    Controlli = False
            '    'End If
            'End If
        Else
            If Me.rdbTipoIncasso.SelectedValue >= 0 Then
                If CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)) > CDec(par.IfEmpty(Me.SumSelected.Value, 0)) Then
                    msgAnomalia &= "\n- Non è consentito inserire un pagamento del tipo CONTR. SOL. con importo a credito!"
                    Controlli = False

                End If
            Else
                msgAnomalia &= "\n- Non è consentito inserire un pagamento del tipo CONTR. SOLID.in modo AUTOMATICO!"
                Controlli = False
            End If
        End If

        'segnalazione 508/2016 VIETARE INCASSO CON ECCEDENZA SE DERIVANTE DA ASSEGNO
        If Me.cmbTipoPagamento.SelectedValue = 5 Then
            If String.IsNullOrEmpty(Me.txtNumAssegno.Text) Then
                msgAnomalia &= "\n- Definire il numero assegno!"
                Controlli = False
            End If
            If String.IsNullOrEmpty(Me.txtDataAssegno.Text) Then
                msgAnomalia &= "\n- Definire la data assegno!"
                Controlli = False
            End If

            If Me.rdbTipoIncasso.SelectedValue >= 0 Then

                If CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)) > CDec(par.IfEmpty(Me.SumSelected.Value, 0)) Then
                    msgAnomalia &= "\n- Non è consentito inserire un pagamento da ASSEGNO. con importo a credito!"
                    Controlli = False
                End If
            Else
                If CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)) > CDec(par.IfEmpty(totResiduo.Value, 0)) Then
                    msgAnomalia &= "\n- Non è consentito inserire un pagamento da ASSEGNO. con importo a credito!"
                    Controlli = False
                End If
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
        ElseIf Me.rdbTipoIncasso.SelectedValue > 0 Then
            Dim ValueInserted As Integer = 0
            For Each ri As DataGridItem In dgvBollVoci.Items
                If Not String.IsNullOrEmpty(CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text) Then
                    If CDec(CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text) <> 0 Then
                        ValueInserted += 1
                        idBolControl += ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text & ","
                        If ri.Cells(par.IndDGC(dgvBollVoci, "TIPOBOLL")).Text = "RAT" Then
                            If CDec(CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text.Replace(".", "")) < CDec(ri.Cells(par.IndDGC(dgvBollVoci, "RESIDUO")).Text.Replace(".", "")) Then
                                anomaliaBolRat = True
                            End If
                        End If
                    End If
                End If
            Next
            If ValueInserted = 0 And PagInMorosita.Value = 0 Then
                msgAnomalia &= "\n- Inserire almeno un importo nell\'elenco bollette"
                Controlli = False
            End If
            If Me.rdbTipoIncasso.SelectedIndex >= 2 Then
                If CDec(par.IfEmpty(Me.SumSelected.Value.Replace(".", ""), 0)) > CDec(par.IfEmpty(Me.txtImpPagamento.Text.Replace(".", ""), 0)) Then
                    msgAnomalia &= "\n- L\'importo inserito nelle bollette è maggiore di quello previsto per l\'incasso\nVerificare gli importi!"
                    Controlli = False
                End If
                Dim numBolAnomlia As String = ""

                If ctrlPagIntera(numBolAnomlia) = False Then
                    msgAnomalia &= "\n- L\'importo inserito nella bolletta num." & numBolAnomlia & " è superiore al residuo!\nVerificare gli importi!"
                    Controlli = False
                End If
            End If
        End If


        If ControlPagamValsuEmissione(idBolControl) = False Then
            msgAnomalia &= "\n- La data pagamento e/o la data valuta sono inferiori alla data emissione della/e bollette da incassare!"
            Controlli = False
        End If

        If anomaliaBolRat = True Then
            msgAnomalia &= "\n- Non è possibile pagare parzialmente le bollette di RATEIZZAZIONE!"
            Controlli = False
        End If

        If Not String.IsNullOrEmpty(msgAnomalia) Then
            Me.txtPagResoconto.Text = Me.txtImpPagamento.Text
            Me.txtSommaSel.Text = Me.SumSelected.Value
            Me.txtResResoconto.Text = Me.ResCredito.Value
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ATTENZIONE!" & msgAnomalia & "\nOPERAZIONE ANNULLATA!');", True)
            Exit Function
        End If
        If Me.cmbTipoPagamento.SelectedValue <> 5 Then
            Me.txtNumAssegno.Text = ""
        End If

        Controlli = True
    End Function
    ''' <summary>
    ''' Controlla che l'insieme delle voci pagate manualmente, per bolletta, non sia superiore al residuo della stessa
    ''' </summary>
    ''' <param name="numBolAnomala">Restituisce il primo numero bolletta anomalo</param>
    ''' <returns>Ritorna se ci sono o meno anomalie</returns>
    ''' <remarks></remarks>
    Private Function ctrlPagIntera(ByRef numBolAnomala As String) As Boolean

        ctrlPagIntera = False
        Dim dicBolTXT As New System.Collections.Generic.Dictionary(Of String, Decimal)
        Dim idBol As String = "0"
        Dim importo As Decimal = 0
        For Each ri As DataGridItem In dgvBollVoci.Items
            importo = 0
            idBol = "0"
            If Not String.IsNullOrEmpty(CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text) Then
                If CDec(CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text) <> 0 Then
                    importo = CDec(CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text.Replace(".", ""))
                    idBol = ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text
                    If Not dicBolTXT.ContainsKey(idBol) Then
                        dicBolTXT.Add(idBol, importo)
                    Else
                        dicBolTXT.Item(idBol) = dicBolTXT.Item(idBol) + importo
                    End If
                End If
            End If
        Next
        Dim pair As System.Collections.Generic.KeyValuePair(Of String, Decimal)
        Dim totDbBolletta As Decimal = 0
        For Each pair In dicBolTXT
            totDbBolletta = 0
            par.cmd.CommandText = "select sum(nvl(importo_totale,0) - nvl(importo_pagato,0)) as residuo from siscom_mi.bol_bollette where id = " & pair.Key
            totDbBolletta = par.cmd.ExecuteScalar
            If totDbBolletta < pair.Value Then
                par.cmd.CommandText = "select nvl(num_bolletta,'N.D.') as num_bolletta from siscom_mi.bol_bollette where id =  " & pair.Key
                numBolAnomala = par.cmd.ExecuteScalar
                ctrlPagIntera = False
                Exit Function
            End If
        Next
        ctrlPagIntera = True

    End Function
    '*********************PUNTO 4 OFFERTA TECNICA 25/SEPA
    Private Function ControlPagamValsuEmissione(ByVal idBolToContrl As String) As Boolean
        ControlPagamValsuEmissione = False
        Dim minEmissione As String = ""
        Dim EsclFacility As String = strEsclFacility()
        Dim NoSaldoTot As String = ""
        Dim NoSaldoPag As String = ""
        If Not String.IsNullOrEmpty(EsclFacility) Then
            NoSaldoTot = " - NVL(QUOTA_SIND_B,0) "
            NoSaldoPag = " - NVL(QUOTA_SIND_PAGATA_B,0) "

        End If
        If rdbTipoIncasso.SelectedValue < 0 Then
            Dim filtro As String = ""
            filtro = ControlFiltro()
            par.cmd.CommandText = "select nvl(min(data_emissione),0) as MIN_D_EMIS from siscom_mi.bol_bollette " _
                                & "WHERE FL_ANNULLATA = '0' AND ID_BOLLETTA_RIC IS NULL AND ID_RATEIZZAZIONE IS NULL AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                & "AND NVL(IMPORTO_INGIUNZIONE,0) = 0 AND round(nvl(IMPORTO_PAGATO ,0) " & NoSaldoPag & ",2)  < round(IMPORTO_TOTALE " & NoSaldoTot & ",2) AND round(IMPORTO_TOTALE " & NoSaldoTot & ",2) > 0 " _
                                & "AND  BOL_BOLLETTE.ID_CONTRATTO = " & vIdContratto.Value & filtro
            minEmissione = par.cmd.ExecuteScalar
        Else
            If idBolToContrl <> "" Then
                idBolToContrl = idBolToContrl.Substring(0, idBolToContrl.Length - 1)
                par.cmd.CommandText = "select nvl(min(data_emissione),0) as MIN_D_EMIS from siscom_mi.bol_bollette " _
                                    & "WHERE FL_ANNULLATA = '0' AND ID_BOLLETTA_RIC IS NULL AND ID_RATEIZZAZIONE IS NULL AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                    & "AND NVL(IMPORTO_INGIUNZIONE,0) = 0 AND BOL_BOLLETTE.id in (" & idBolToContrl & ")"
                minEmissione = par.cmd.ExecuteScalar
            End If

        End If


        If par.AggiustaData(Me.txtDataPagamento.Text) < minEmissione Then
            ControlPagamValsuEmissione = False
            Exit Function
        End If
        If par.AggiustaData(Me.txtDataRegistrazione.Text) < minEmissione Then
            ControlPagamValsuEmissione = False
            Exit Function
        End If

        ControlPagamValsuEmissione = True
    End Function

    Private Sub ResetCampi()
        Me.txtImpPagamento.Text = ""
        Me.txtDataPagamento.Text = ""
        Me.txtDataRegistrazione.Text = ""
        Me.txtDataDal.Text = ""
        Me.txtDataAl.Text = ""
        Me.txtEmesDal.Text = ""
        Me.txtEmesAl.Text = ""
        If ObbFiltro.Value = 1 Then
            Me.rdbTipoIncasso.SelectedValue = "-1"
            OldSelTipoPag.Value = "-1"
        End If

        Me.cmbTipoPagamento.SelectedValue = -1
        Me.tipoPagValue.Value = Me.rdbTipoIncasso.SelectedValue
        Me.SumSelected.Value = 0
        Me.ResCredito.Value = 0
        Me.txtPagResoconto.Text = ""
        Me.txtResResoconto.Text = ""
        Me.txtSommaSel.Text = ""
        Me.NonAttrib.Value = 0
        Me.txtnote.Text = ""
        Me.idIncasso.Value = 0
        Me.impWritePagamento.Value = 0
        Me.FiltNumBol.Value = ""
        OldSelTipoPag.Value = "-1"
        Me.BolloPagParz.Value = "0"
        txtqs.Visible = False
        Session.Remove("PagInMor2")
        Session.Remove("idIncassoDaMor")
        If esclQSind() = True Then
            CaricaBollNonPagate(, True)
            txtqs.Visible = True

        Else
            CaricaBollNonPagate(, False)
            txtqs.Visible = False

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
                    '*******************apertura transazione
                    'connData.apriTransazione()
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

                            Case 1
                                If PagamentoManualeBolletta(errore) = False Then
                                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                                    connData.chiudiTransazione(False)
                                    'riapro la transazione per successivi comandi
                                    connData.apriTransazione()

                                    Exit Sub
                                End If
                            Case 2
                                If PagamentoManualeVoci(errore) = False Then
                                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                                    connData.chiudiTransazione(False)
                                    'riapro la transazione per successivi comandi
                                    connData.apriTransazione()

                                    Exit Sub
                                End If


                        End Select

                        If Me.impWritePagamento.Value > 0 Then 'SE DOPO IL PAGAMENTO HO ANCORA DISPONIBILITA' SCRIVO IN PARTITA GESTIONALE
                            Me.impWritePagamento.Value = Math.Round(CDec(impWritePagamento.Value), 2)
                            If CreditoGestionale(impWritePagamento.Value, errore) = False Then
                                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                                connData.chiudiTransazione(False)
                                'riapro la transazione per successivi comandi
                                connData.apriTransazione()

                                Exit Sub
                            End If
                        End If

                        If VerificaFinale(errore) = False Then
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                            connData.chiudiTransazione(False)
                            'riapro la transazione per successivi comandi
                            connData.apriTransazione()

                            Exit Sub
                        End If

                        If ValorizzColonneImporti(errore, CDec(impWritePagamento.Value)) = False Then
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                            connData.chiudiTransazione(False)
                            'riapro la transazione per successivi comandi
                            connData.apriTransazione()

                            Exit Sub
                        End If

                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & errore & "!Nessun dato salvato o modificato!');", True)
                        connData.chiudiTransazione(False)
                        'riapro la transazione per successivi comandi
                        connData.apriTransazione()

                        Exit Sub
                    End If
                    AggiornaBollette()

                    If lstBolliParz.Count = 0 Then
                        BolloPagParz.Value = 0
                    End If

                    If BolloPagParz.Value = "0" Then

                        ''ULTERIORE CONTROLLO PELLEGRI 11/08/2016 NON VALIDO PER IL CONTR. DI SOLIDARIETA
                        If noCtrlDate.Value = 0 Then
                            If ctrlAnnoSolare() = True Then
                                '*******************chiusura transazione
                                connData.chiudiTransazione(True)
                                'riapro la transazione per successivi comandi
                                connData.apriTransazione()
                                '*******************messaggio finale
                                If CTRAperto = 1 Then
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('INCASSO REGISTRATO CORRETTAMENTE!');if (opener.document.getElementById('AGGBOLL')){opener.document.getElementById('AGGBOLL').value=1};opener.document.getElementById('form1').submit();", True)
                                Else
                                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('INCASSO REGISTRATO CORRETTAMENTE!');", True)
                                End If
                            Else
                                '
                                '*******************chiusura transazione
                                connData.chiudiTransazione(False)
                                'riapro la transazione per successivi comandi
                                connData.apriTransazione()
                                '*******************messaggio finale
                                If CTRAperto = "1" Then
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('La data pagamento e/o la data valuta non può essere al di fuori dell\' anno solare!OPERAZIONE INTERROTTA!');if (opener.document.getElementById('AGGBOLL')){opener.document.getElementById('AGGBOLL').value=1};opener.document.getElementById('form1').submit();", True)

                                Else
                                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('La data pagamento e/o la data valuta non può essere al di fuori dell\' anno solare!OPERAZIONE INTERROTTA!');", True)

                                End If

                            End If
                        Else
                            '*******************chiusura transazione
                            connData.chiudiTransazione(True)
                            'riapro la transazione per successivi comandi
                            connData.apriTransazione()
                            '*******************messaggio finale
                            If CTRAperto = 1 Then
                                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('INCASSO REGISTRATO CORRETTAMENTE!');if (opener.document.getElementById('AGGBOLL')){opener.document.getElementById('AGGBOLL').value=1};opener.document.getElementById('form1').submit();", True)
                            Else
                                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('INCASSO REGISTRATO CORRETTAMENTE!');", True)
                            End If
                        End If
                    Else
                        '*******************chiusura transazione
                        connData.chiudiTransazione(False)
                        'riapro la transazione per successivi comandi
                        connData.apriTransazione()
                        '*******************messaggio finale
                        If CTRAperto = "1" Then
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('INCASSO INTERROTTO PER PAGAMENTO PARZIALE DI UN BOLLO!\nProcedere scegliendo la metodologia MANUALE PER VOCE!');if (opener.document.getElementById('AGGBOLL')){opener.document.getElementById('AGGBOLL').value=1};opener.document.getElementById('form1').submit();", True)

                        Else
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('INCASSO INTERROTTO PER PAGAMENTO PARZIALE DI UN BOLLO!\nProcedere scegliendo la metodologia MANUALE PER VOCE!');", True)

                        End If
                    End If

                    ResetCampi()
                Else
                    SelUnsel(False)
                End If
            Else
                SelUnsel(False)
                Me.txtPagResoconto.Text = Me.txtImpPagamento.Text
                Me.txtSommaSel.Text = Me.SumSelected.Value
                Me.txtResResoconto.Text = Me.ResCredito.Value
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
    'ULTERIORE CONTROLLO PELLEGRI 11/08/2016
    Private Function ctrlAnnoSolare() As Boolean
        ctrlAnnoSolare = False

        par.cmd.CommandText = "select max(data_emissione) from siscom_mi.bol_bollette where id in" _
            & "( SELECT (CASE WHEN b.id_bolletta_ric IS NULL THEN b.ID ELSE b.id_bolletta_ric END) FROM SISCOM_MI.BOL_BOLLETTE b  WHERE b.ID IN " _
            & "(select id_bolletta from siscom_mi.bol_bollette_voci where id in (select id_voce_bolletta from siscom_mi.bol_bollette_voci_pagamenti where id_incasso_extramav = " & idIncasso.Value & ")))"
        Dim maxDataEmissione As String = par.IfNull(par.cmd.ExecuteScalar, "19000101")

        If maxDataEmissione > "20090930" Then
            par.cmd.CommandText = "select data_pagamento from siscom_mi.incassi_extramav where id = " & idIncasso.Value
            Dim DataPagIncasso As String = par.cmd.ExecuteScalar
            'DataPagIncasso = DataPagIncasso.Substring(0, 4)

            If DataPagIncasso <= Me.bloccoData.Value Then
                ctrlAnnoSolare = False
                Exit Function
            End If

            par.cmd.CommandText = "select data_registrazione from siscom_mi.incassi_extramav where id = " & idIncasso.Value
            Dim DataPagValuta As String = par.IfNull(par.cmd.ExecuteScalar, "99999999")
            'DataPagValuta = DataPagValuta.Substring(0, 4)
            If DataPagValuta <= Me.bloccoData.Value Then
                ctrlAnnoSolare = False
                Exit Function
            End If

        End If
        ctrlAnnoSolare = True

    End Function

    ''' <summary>
    ''' Controlla che se la bolletta contiene voci negative e il totale pagato si eguaglia con il totale della bolletta
    ''' viene scritta come pagata completamente
    ''' </summary>
    ''' <param name="errore"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VerificaFinale(ByRef errore As String) As Boolean
        VerificaFinale = False
        par.cmd.CommandText = "SELECT id_bolletta,round(SUM(importo),2) AS totale,round(SUM(NVL(imp_pagato,0)),2) AS pagato " _
                            & "FROM siscom_mi.BOL_BOLLETTE_VOCI " _
                            & "WHERE id_bolletta in (select id_bolletta from siscom_mi.bol_bollette_voci bvc2 where bvc2.ID IN (SELECT id_voce_bolletta FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE id_incasso_extramav = " & idIncasso.Value & ")) " _
                            & "GROUP BY id_bolletta "
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        While lettore.Read
            If lettore("totale") = lettore("pagato") Then
                par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato = importo where id_bolletta = " & lettore("id_bolletta")
                par.cmd.ExecuteNonQuery()
            End If
        End While
        lettore.Close()
        VerificaFinale = True
        errore = ""
    End Function


    Private Function ValorizzColonneImporti(ByRef errore As String, ByVal importoEccedenza As Decimal) As Boolean
        ValorizzColonneImporti = False
        Dim incassato As Decimal = 0
        par.cmd.CommandText = "SELECT id_bolletta,round(SUM(importo_pagato),2) AS incassato " _
                    & "FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI " _
                    & "WHERE nvl(FL_NO_REPORT,0)=0 AND id_incasso_extramav = " & idIncasso.Value & " " _
                    & "GROUP BY id_bolletta "
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        While lettore.Read
            incassato = incassato + par.IfNull(lettore("incassato"), 0)
        End While
        lettore.Close()

        par.cmd.CommandText = "update siscom_mi.incassi_extramav set importo_incassato = " & par.insDbValue(incassato, False) & ", " _
                & "importo_eccedenza=" & par.insDbValue(importoEccedenza, False) & " where id = " & idIncasso.Value
        par.cmd.ExecuteNonQuery()

        ValorizzColonneImporti = True
        errore = ""
    End Function


    Private Function scriviIncasso(ByRef errore As String) As Boolean
        scriviIncasso = False

        errore = "scriviIncasso"
        If idIncasso.Value = 0 Then '<------ potrebbe essere già stato scritto da Teresa tramite pagamenti Bolletta MORORISTA!!!

            par.cmd.CommandText = "select siscom_mi.seq_incassi_extramav.nextval from dual"
            idIncasso.Value = par.cmd.ExecuteScalar

            par.cmd.CommandText = "insert into siscom_mi.incassi_extramav (id,id_tipo_pag,motivo_pagamento," _
                                & "id_contratto,data_pagamento,data_registrazione,riferimento_da, riferimento_a,fl_annullata,importo," _
                                & "id_operatore,numero_assegno,fl_annullabile,data_ora,data_assegno) values " _
                                & "(" & idIncasso.Value & "," & Me.cmbTipoPagamento.SelectedValue & ", " _
                                & "'" & par.PulisciStrSql(Me.txtnote.Text.ToUpper) & "', " & vIdContratto.Value & ", " _
                                & "'" & par.AggiustaData(Me.txtDataPagamento.Text) & "','" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "','','',0," & par.VirgoleInPunti(Me.impWritePagamento.Value) & "," _
                                & Session.Item("ID_OPERATORE") & ",'" & par.PulisciStrSql(txtNumAssegno.Text.ToUpper) & "',1,'" & Format(Now, "yyyyMMddHHmmss") & "','" & par.AggiustaData(Me.txtDataAssegno.Text) & "')"

            par.cmd.ExecuteNonQuery()


        Else


            Me.impWritePagamento.Value = CDec(Me.txtImpPagamento.Text.Replace(".", "") - PagInMorosita.Value)

            par.cmd.CommandText = "UPDATE siscom_mi.INCASSI_EXTRAMAV SET IMPORTO = " & par.VirgoleInPunti(CDec(Me.txtImpPagamento.Text.Replace(".", ""))) & ", DATA_PAGAMENTO = '" & par.AggiustaData(Me.txtDataPagamento.Text) & "' , DATA_REGISTRAZIONE = '" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "', " _
                                & "ID_TIPO_PAG =" & Me.cmbTipoPagamento.SelectedValue & " ,MOTIVO_PAGAMENTO = '" & par.PulisciStrSql(Me.txtnote.Text) & "', numero_assegno = '" & par.PulisciStrSql(txtNumAssegno.Text.ToUpper) & "' " _
                                & "WHERE ID = " & idIncasso.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI SET DATA_PAGAMENTO = '" & par.AggiustaData(Me.txtDataPagamento.Text) & "', DATA_REGISTRAZIONE =  '" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "', DATA_VALUTA = '" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "' WHERE ID_INCASSO_EXTRAMAV  = " & idIncasso.Value
            par.cmd.ExecuteNonQuery()


        End If

        scriviIncasso = True
        errore = ""

    End Function
    Private Sub RegistraIncassiAttribuiti(ByVal idVoceBolletta As String, ByVal importoPagatoVoceBolletta As Decimal, ByVal idIncassoExtraMAV As String, Optional ByVal idVoceGest As String = "null")
        'effettua l'insert di tutte le voci delle bollette interessate nella
        'tabella incassi_Attribuiti
        par.cmd.CommandText = "INSERT INTO siscom_mi.INCASSI_ATTRIBUITI (ID_INCASSO_NON_ATTR,ID_INCASSO_EXTRAMAV,ID_VOCE_BOLLETTA,IMPORTO,ID_VOCE_GEST) " _
                            & "VALUES (" & Session.Item("IdIncassoNonAttribuito") & "," & idIncassoExtraMAV & "," & idVoceBolletta & "," & Replace(CStr(importoPagatoVoceBolletta), ",", ".") & "," & idVoceGest & ")"
        par.cmd.ExecuteNonQuery()
        '02/02/2015 nuova gestione incassi non attribuibili
        If Not IsNothing(Session.Item("IdIncassoNonAttribuito")) And Not IsNothing(Session.Item("ImportoIncassoNonAttribuito")) Then
            par.cmd.CommandText = "update siscom_mi.incassi_non_attribuibili set " _
                                & "importo_residuo = importo_residuo - " & Replace(CStr(importoPagatoVoceBolletta), ",", ".") _
                                & " where id = " & Session.Item("IdIncassoNonAttribuito")
            par.cmd.ExecuteNonQuery()
        End If

    End Sub
    Private Function strEsclFacility() As String
        strEsclFacility = ""

        If rdbTipoIncasso.SelectedValue <= 1 Then
            strEsclFacility = ""
            par.cmd.CommandText = "select count(id) as conta from siscom_mi.tipo_pag_parz where fl_no_saldo = 1 and id = " & Me.cmbTipoPagamento.SelectedValue
            Dim isNosaldo As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If isNosaldo > 0 Then
                strEsclFacility = " and t_voci_bolletta.fl_no_saldo = 0 "
            End If
            Return strEsclFacility

        End If
    End Function
#Region "AUTOMATICO"
    Private Function PagamentoAutomatico(ByRef errore As String) As Boolean
        PagamentoAutomatico = False
        errore = "PagamentoAutomatico"
        While impWritePagamento.Value > 0
            'ESCLUSIONE DELLE VOCI DEL FACILITY SE MODALITA' PAGAMENTO = 
            Dim EsclFacility As String = strEsclFacility()


            'COMMENTO  PUCCIA
            '- - - ATTENZIONE - - -
            'IL METODO DEL PAGAMENTO AUTOMATICO ESCLUDE LE VOCI NEGATIVE DALL'INCASSO, PERCHE' SEGUE LA PRIORITA DEFINITA DAL Cod.Civile.
            'ALLA FINE DEL PAGAMENTO SE LA BOLLETTA CONTENESSE VOCI NEGATIVE (escluse in fase di ricerca priorità) E FOSSE INTERAMENTE PAGATE 
            ' QUESTE VENGONO AGGIORNATE PER L'INTERO IMPORTO.

            ''' , bol_bollette_voci.id_bolletta asc  <--AGGIUNGO CONDIZIONE PER Richiesta Pellegri/Mura
            ''' 

            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.ID,id_bolletta,id_voce,importo,round(NVL (imp_pagato, 0),2) as imp_pagato, (case when (importo - nvl(imp_pagato,0))<0 then 0 else priorita end)as ordine," _
                                & "(importo - round(NVL (imp_pagato, 0),2)) as residuo,'' as c_aliq,data_scadenza,data_emissione,T_VOCI_BOLLETTA.priorita, " _
                                & "id_voce_aliquota,(CASE WHEN data_scadenza < TRIM(TO_CHAR(SYSDATE, 'YYYYMMDD')) THEN 1 ELSE 0 END) AS scaduta " _
                                & "FROM siscom_mi.BOL_BOLLETTE,siscom_mi.BOL_BOLLETTE_VOCI,siscom_mi.T_VOCI_BOLLETTA " _
                                & "WHERE BOL_BOLLETTE.ID = BOL_BOLLETTE_VOCI.ID_BOLLETTA " _
                                & "AND BOL_BOLLETTE_VOCI.id_voce = T_VOCI_BOLLETTA.ID(+) AND NVL(IMPORTO_RUOLO,0) = 0 " _
                                & "AND round(NVL (imp_pagato, 0),2) < round(IMPORTO,2) AND IMPORTO_TOTALE > 0 " _
                                & " AND siscom_mi.bol_bollette.id NOT IN (select id_bolletta from siscom_mi.bol_bollette_voci where id_voce in (126,182)) /* Condizione per escludere le bollette con voce di Compenso Facility (SD 164/2017)*/" _
                                & "AND FL_ANNULLATA = '0' AND ID_BOLLETTA_RIC IS NULL AND ID_RATEIZZAZIONE IS NULL and (importo - round(NVL(imp_pagato, 0),2)) <> 0 " & EsclFacility _
                                & "AND BOL_BOLLETTE.ID_CONTRATTO = " & vIdContratto.Value _
                                & " ORDER BY scaduta DESC " _
                                & " , BOL_BOLLETTE.DATA_SCADENZA ASC " _
                                & " ,BOL_BOLLETTE.data_emissione ASC " _
                                & " ,BOL_BOLLETTE_VOCI.id_bolletta ASC " _
                                & " ,ordine ASC" _
                                & " ,(importo-NVL(imp_pagato,0)) DESC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then

                Dim dtFiltrata As Data.DataTable
                dtFiltrata = TrovaScadute(dt) '<--debito scaduto
                If dtFiltrata.Rows.Count > 1 Then
                    dtFiltrata = PrioritaVoce(dtFiltrata) '<-- meno garantito (ossia priorita voce)
                    If dtFiltrata.Rows.Count > 1 Then
                        dtFiltrata = PiuOnerosi(dtFiltrata) '<-- più oneroso (residuo da pagare più grande)
                        If dtFiltrata.Rows.Count > 1 Then
                            dtFiltrata = PiuAntico(dtFiltrata) ' <-- più antico (data min emissione)
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
    Private Function PrioritaVoce(ByVal dt As Data.DataTable) As Data.DataTable
        PrioritaVoce = New Data.DataTable
        Dim view As New Data.DataView(dt)
        par.cmd.CommandText = "select distinct priorita from siscom_mi.t_voci_bolletta order by priorita asc"
        Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        While reader.Read
            view.RowFilter = "PRIORITA = " & par.IfNull(reader("priorita"), "- 1") & " AND ID_BOLLETTA = " & dt.Rows(0).Item("id_bolletta")
            PrioritaVoce = view.ToTable
            If PrioritaVoce.Rows.Count > 0 Then
                Exit While
            End If
        End While
        reader.Close()
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
                ' DETRAGGO IL RESIDUO DA PAGARE DAL PAGAMENTO INIZIALE
                impWritePagamento.Value = CDec(impWritePagamento.Value) - impPag
                Dim view As New Data.DataView(dt)
                'selezione bollette scadute, se non trovo scadute prendo le non scadute
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
                    'funzione proporzionale per tutte le voci tranne l'ultima, dove metto il residuo
                    propPagVoce = Math.Round((CDec(par.IfEmpty(rProp.Item("residuo").ToString, 0)) * writePagIniziale) / resVoci, 2)
                    ' DETRAGGO LA QUOTA PROPORZIONALE DAL PAGAMENTO INIZIALE
                    impWritePagamento.Value = CDec(impWritePagamento.Value) - propPagVoce
                    resPagIniziale = resPagIniziale - propPagVoce
                    Dim view As New Data.DataView(dt)
                    'selezione bollette scadute, se non trovo scadute prendo le non scadute
                    view.RowFilter = "ID = " & rProp.Item("id")
                    rPagamento = view.ToTable
                    PagaElVoci(rPagamento, propPagVoce, errore, True)
                ElseIf i = dt.Rows.Count - 1 Then
                    If resPagIniziale > 0 Then
                        impWritePagamento.Value = CDec(impWritePagamento.Value) - resPagIniziale
                        Dim view As New Data.DataView(dt)
                        'selezione bollette scadute, se non trovo scadute prendo le non scadute
                        view.RowFilter = "ID = " & rProp.Item("id")
                        rPagamento = view.ToTable
                        PagaElVoci(rPagamento, resPagIniziale, errore, True)
                    End If
                End If
                i += 1
            Next
        End If

    End Sub
#End Region

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
    Private Function PagamentoManualeBolletta(ByRef errore As String) As Boolean
        PagamentoManualeBolletta = False
        errore = "PagamentoManualeBolletta"

        Dim impPagamento As Decimal = 0
        For Each ri As DataGridItem In dgvBollVoci.Items
            If CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = True Then
                impPagamento = CDec(par.IfEmpty(CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text.Replace(".", ""), 0))
                If impPagamento > 0 And Me.impWritePagamento.Value > 0 Then
                    PagaBolletta(ri.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text, impPagamento, errore)
                End If
            End If
        Next

        PagamentoManualeBolletta = True
        errore = ""
    End Function

    Private Function PagamentoManualeVoci(ByRef errore As String) As Boolean
        PagamentoManualeVoci = False
        errore = "PagamentoManualeVoci"
        Dim impPagamento As Decimal = 0
        Dim idVoce As Integer = 0

        'DOPPIO CICLO SULLE VOCI DA PAGARE PER SOMMARE IMPORTI NEGATIVI ALL'INCASSO DA EFFETTURAE
        For Each ri As DataGridItem In dgvBollVoci.Items
            If CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = True Then
                impPagamento = CDec(par.IfEmpty(CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text.Replace(".", ""), 0))
                idVoce = par.IfEmpty(ri.Cells(par.IndDGC(dgvBollVoci, "ID_VOCE_BOL")).Text.Replace("&nbsp;", ""), 0)
                If impPagamento < 0 And Me.impWritePagamento.Value > 0 And idVoce > 0 Then
                    par.cmd.CommandText = "select id,id_bolletta,importo,nvl(imp_pagato,0) as imp_pagato,id_voce_aliquota from siscom_mi.bol_bollette_voci where id = " & idVoce
                    Dim daManual As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtManual As New Data.DataTable()
                    daManual.Fill(dtManual)
                    PagaElVoci(dtManual, impPagamento, errore, False)
                    Me.impWritePagamento.Value = Math.Round(CDec(Me.impWritePagamento.Value) - impPagamento, 2)
                End If
            End If
        Next





        For Each ri As DataGridItem In dgvBollVoci.Items
            If CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Visible = True Then
                impPagamento = CDec(par.IfEmpty(CType(ri.Cells(par.IndDGC(dgvBollVoci, "IMPORTO €")).FindControl("txtImpPag"), TextBox).Text.Replace(".", ""), 0))
                idVoce = par.IfEmpty(ri.Cells(par.IndDGC(dgvBollVoci, "ID_VOCE_BOL")).Text.Replace("&nbsp;", ""), 0)
                If impPagamento > 0 And Me.impWritePagamento.Value > 0 And idVoce > 0 Then
                    par.cmd.CommandText = "select id,id_bolletta,importo,nvl(imp_pagato,0) as imp_pagato,id_voce_aliquota from siscom_mi.bol_bollette_voci where id = " & idVoce
                    Dim daManual As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtManual As New Data.DataTable()
                    daManual.Fill(dtManual)
                    PagaElVoci(dtManual, impPagamento, errore, False)
                    Me.impWritePagamento.Value = Math.Round(CDec(Me.impWritePagamento.Value) - impPagamento, 2)
                End If
            End If
        Next
        PagamentoManualeVoci = True
        errore = ""
    End Function

    Private Sub PagaBolletta(ByVal idBolletta As Integer, ByVal importo As Decimal, ByRef errore As String)
        errore = "PagaVociBolletta"
        Dim EsclFacility As String = strEsclFacility()


        par.cmd.CommandText = "select bol_bollette_voci.id,id_bolletta,importo,nvl(imp_pagato,0) as imp_pagato,(case when (importo - nvl(imp_pagato,0))<0 then 0 else nvl(priorita,999) end)as ordine," _
                            & " id_voce_aliquota from siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta " _
                            & " where t_voci_bolletta.id = bol_bollette_voci.id_voce and " _
                            & " id_bolletta = " & idBolletta & EsclFacility & "  order by ordine ASC , importo asc " '<---FONDAMENTALE ORDINARE PER IMPORTO PER AGGIUNGERE I NEGATIVI!!!

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable()
        da.Fill(dt)
        Dim Pagabile As Decimal = dt.Compute("SUM(importo)", String.Empty) - dt.Compute("SUM(imp_pagato)", String.Empty)
        If Pagabile > 0 Then
            If importo >= Pagabile Then
                PagaTutteLeVoci(dt, errore)
                Me.impWritePagamento.Value = CDec(impWritePagamento.Value) - Pagabile
            ElseIf importo < Pagabile Then
                PagaElVoci(dt, importo, errore)
                Me.impWritePagamento.Value = Math.Round(CDec(Me.impWritePagamento.Value) - importo, 2)
            End If
        End If
        errore = ""
    End Sub
    Private Sub PagaTutteLeVoci(ByVal dtVoci As Data.DataTable, ByRef errore As String)
        errore = "PagaTutteLeVoci"
        Dim impPagato As Decimal = 0
        Dim IsMor As Boolean = False
        For Each rVoce As Data.DataRow In dtVoci.Rows
            impPagato = Math.Round(par.IfEmpty(rVoce("importo").ToString, 0) - par.IfEmpty(rVoce("imp_pagato").ToString, 0), 2)
            If impPagato <> 0 Then
                PagaVoce(rVoce("id").ToString, impPagato, IsMor)
                'If IsMor = False Then
                WriteVociPagamenti(impPagato, rVoce("id").ToString, "")
                'End If
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

            If isAutomatico = True Then
                'DELLA VOCE CHE PAGO VERIFICO SE SULLA BOLLETTA CI SONO VOCI NEGATIVE, PER PAGARLE
                par.cmd.CommandText = "select id,importo,imp_pagato from siscom_mi.bol_bollette_voci where id_bolletta = " & rVoce.Item("ID_BOLLETTA").ToString & " and importo < 0 and importo - nvl(imp_pagato,0) <> 0"
                Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim aggiuntaNegativi As Decimal = 0
                Dim pagato As Decimal = 0
                While reader.Read
                    pagato = Math.Abs(CDec(par.IfNull(reader("importo"), 0))) - Math.Abs(CDec(par.IfNull(reader("imp_pagato"), 0)))
                    par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato = importo where id =  " & reader("id")
                    par.cmd.ExecuteNonQuery()
                    WriteVociPagamenti(pagato * -1, reader("id").ToString, "")
                    aggiuntaNegativi = aggiuntaNegativi + pagato
                End While
                reader.Close()
                'aggiungo eventuali voci negative alla disponibilità
                disponibilita = disponibilita + aggiuntaNegativi
            End If

            pagabile = 0
            impPagato = 0
            If (par.IfNull(rVoce("importo"), 0)) < 0 Then
                'VOCE CON IMPORTO NEGATIVO, LA METTO PAGATA,SCRIVO IN BOL_BOLLETTE_VOCI_PAGAMENTI ED AUMENTO LA DISPONIBILITA SUL PAGAMENTO
                If (Math.Abs(CDec(par.IfNull(rVoce("importo"), 0))) - Math.Abs(CDec(par.IfNull(rVoce("imp_pagato"), 0)))) > 0 Then
                    impPagato = Math.Round((Math.Abs(CDec(par.IfNull(rVoce("importo"), 0))) - Math.Abs(CDec(par.IfNull(rVoce("imp_pagato"), 0)))), 2)
                    disponibilita = disponibilita + impPagato
                    If impPagato > 0 Then
                        par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato = importo where id =  " & rVoce("id").ToString
                        par.cmd.ExecuteNonQuery()
                        WriteVociPagamenti(impPagato * -1, rVoce("id").ToString, "")
                    End If
                End If
            Else
                If par.IfEmpty(rVoce.Item("id_voce_aliquota").ToString, 0) > 0 Then
                    'Gestione pagamento parziale proporzionale della aliquota
                    'disponibilita = PagaAliquota(disponibilita, rVoce.Item("id").ToString, rVoce.Item("id_voce_aliquota").ToString, errore)
                Else
                    'Pagamento parziale voce
                    pagabile = Math.Round(par.IfEmpty(rVoce("importo").ToString, 0) - par.IfEmpty(rVoce("imp_pagato").ToString, 0), 2)
                    If pagabile > 0 Then
                        If disponibilita > pagabile Then
                            impPagato = pagabile
                        Else
                            impPagato = disponibilita

                            'SEGNALAZIONE 476/2016 NON POSSO PAGARE I BOLLI PARZIALMENTE
                            If isPagabileBollo(rVoce("id").ToString, impPagato) = False Then
                                'salta il pagamento e va alla voce successiva
                                Continue For
                        End If

                        End If
                        PagaVoce(rVoce("id").ToString, impPagato, isMor)
                        'If isMor = False Then
                        WriteVociPagamenti(impPagato, rVoce("id").ToString, "")
                        'End If
                        disponibilita = disponibilita - impPagato
                    End If
                End If
            End If
        Next
        If disponibilita > 0 Then
            impWritePagamento.Value = CDec(impWritePagamento.Value) + disponibilita
        End If
        errore = ""
    End Sub

    'Private Function PagaAliquota(ByVal pagam As Decimal, ByVal idVocImponibile As Integer, ByVal idVocAliquota As Integer, ByRef errore As String, ByVal idVoceRata As String) As Decimal
    '    PagaAliquota = 0
    '    Dim residuo As Decimal = 0
    '    Dim impAliquota As Decimal = 0
    '    Dim PercentAliquota As Decimal = 0
    '    Dim impImponibile As Decimal = 0
    '    Dim competenzAliquota As Decimal = 0
    '    Dim competenzaImponibile As Decimal = 0
    '    Dim pagato As Decimal = 0


    '    par.cmd.CommandText = "select PERCENTUALE_ALIQUOTA from siscom_mi.aliquote_iva where codice =(select c_aliq from siscom_mi.bol_bollette_voci where id = " & idVocImponibile & " ) "
    '    PercentAliquota = par.cmd.ExecuteScalar

    '    par.cmd.CommandText = "SELECT SUM(importo)-SUM(NVL(imp_pagato,0)) " _
    '                        & "FROM siscom_mi.BOL_BOLLETTE_VOCI WHERE ID = " & idVocImponibile
    '    residuo = par.cmd.ExecuteScalar


    '    Dim ivaSuImp As Decimal = 0
    '    Dim totIvato As Decimal = 0


    '    If residuo > 0 Then
    '        ivaSuImp = Math.Round(((residuo * PercentAliquota) / 100), 2)
    '        totIvato = residuo + ivaSuImp

    '        Dim IsMor As Boolean = False
    '        If pagam >= totIvato Then
    '            '********PAGO COMPLETAMENTE ALIQUOTA PER LA PARTE DI COMPETENZA (una voce iva può racchiudere più imponibili)
    '            PagaVoce(idVocAliquota, ivaSuImp, IsMor)
    '            WriteVociPagamenti(ivaSuImp, idVocAliquota, idVoceRata)
    '            pagam = pagam - ivaSuImp
    '            '********PAGO COMPLETAMENTE IMPONIBILE
    '            PagaVoce(idVocImponibile, residuo, IsMor)
    '            WriteVociPagamenti(residuo, idVocImponibile, idVoceRata)
    '            pagam = pagam - residuo
    '        ElseIf pagam < totIvato Then
    '            par.cmd.CommandText = "SELECT nvl(importo,0) - nvl(imp_pagato,0) FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID =" & idVocAliquota
    '            impAliquota = par.cmd.ExecuteScalar
    '            par.cmd.CommandText = "SELECT nvl(importo,0) - nvl(imp_pagato,0) FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID =" & idVocImponibile
    '            impImponibile = par.cmd.ExecuteScalar
    '            competenzAliquota = Math.Round((pagam * impAliquota) / (impImponibile + impAliquota), 2)
    '            competenzaImponibile = pagam - competenzAliquota
    '            '********PAGO COMPETENZA ALIQUOTA
    '            PagaVoce(idVocAliquota, competenzAliquota)
    '            WriteVociPagamenti(competenzAliquota, idVocAliquota, idVoceRata)
    '            pagam = pagam - competenzAliquota
    '            '********PAGO COMPETENZA IMPONIBILE
    '            PagaVoce(idVocImponibile, competenzaImponibile)
    '            WriteVociPagamenti(competenzaImponibile, idVocImponibile, idVoceRata)
    '            pagam = pagam - competenzaImponibile
    '        End If
    '    Else
    '        '26/05/2015
    '        'possono esserci anomalie sui pagamenti, in base alle quali
    '        ' è stata pagata completamente l'aliquota, senza aver pagato
    '        ' in proporzione l'iva (dovute a import dati oppure ad incassi 
    '        ' precedenti alla nuova metoologia) quindi se:
    '        ' RESIDUO sulla voce imponibile = 0, copro con la disponibilità, l'iva corrispondente, 
    '        '   per la quota totale dell'imponibile 

    '        par.cmd.CommandText = "select importo from SISCOM_MI.bol_bollette_voci where id = " & idVocImponibile
    '        Dim totImponibile As Decimal = par.cmd.ExecuteNonQuery
    '        ivaSuImp = Math.Round(((totImponibile * PercentAliquota) / 100), 2)
    '        Dim resIva As Decimal = 0
    '        'in caso di pagamento anomalo e parziale della voce iva, posso pagarla solo di quanto resta da pagare
    '        par.cmd.CommandText = "SELECT SUM(importo)-SUM(NVL(imp_pagato,0)) " _
    '                & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID = " & idVocAliquota
    '        resIva = par.cmd.ExecuteScalar
    '        If resIva < ivaSuImp Then
    '            ivaSuImp = resIva
    '        End If

    '        '********PAGO SOLO ALIQUOTA PER RISOLVERE L'ANOMALIA
    '        PagaVoce(idVocAliquota, ivaSuImp)
    '        WriteVociPagamenti(ivaSuImp, idVocAliquota, idVoceRata)
    '        pagam = pagam - ivaSuImp
    '    End If


    '    PagaAliquota = pagam
    'End Function



    Private Function isPagabileBollo(ByVal idVoce As Integer, ByVal impPagamento As Decimal) As Boolean
        isPagabileBollo = True
        Dim resBollo As Decimal = 0
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

    Private Sub PagaVoce(ByVal idVoce As Integer, ByVal ImpPagamento As Decimal, ByRef isMor As Boolean) ', ByRef pagataOk As Boolean
        ImpPagamento = Math.Round(CDec(ImpPagamento), 2)
        par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato = (nvl(imp_pagato,0) + " & par.VirgoleInPunti(ImpPagamento) & ") ,  " _
                        & " importo_riclassificato_pagato = (nvl(importo_riclassificato_pagato,0) + " _
                        & "(case when nvl(importo_riclassificato,0) >0 then " & par.VirgoleInPunti(ImpPagamento) & " else  0 end)) where id = " & idVoce
        par.cmd.ExecuteNonQuery()

        lstBolliParz.Remove(idVoce)

        'pagamento delle bollette riclassificate in rateizzazione
        'se la voce è di bolletta tipo 5 (rat.)
        ControlIsRat(idVoce, ImpPagamento)
        ControlIsMor(idVoce, ImpPagamento, isMor)
    End Sub
    Private Sub WriteVociPagamenti(ByVal pagato As Decimal, ByVal voce As Integer, ByVal voceRata As String)
        If pagato <> 0 Then

            par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,DATA_REGISTRAZIONE,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV,DATA_VALUTA) VALUES " _
                    & "(" & voce & ",'" & par.AggiustaData(Me.txtDataPagamento.Text) & "','" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "'," & par.VirgoleInPunti(pagato) & ",2," & Me.idIncasso.Value & ",'" & par.AggiustaData(Me.txtDataRegistrazione.Text) & "')"
            par.cmd.ExecuteNonQuery()
            '02/02/2015 gestione incassi non attribuiti
            If Not IsNothing(Session.Item("importoIncassoNonAttribuito")) And Not IsNothing(Session.Item("IdIncassoNonAttribuito")) Then
                RegistraIncassiAttribuiti(voce, pagato, idIncasso.Value)
            End If

        End If

    End Sub
    Private Sub AggiornaBollette()
        par.cmd.CommandText = "select id,data_pagamento,data_valuta from siscom_mi.bol_bollette where id in (SELECT ID_BOLLETTA FROM siscom_mi.BOL_BOLLETTE_VOCI WHERE " _
                            & " ID in (select id_voce_bolletta from siscom_mi.bol_bollette_voci_pagamenti where id_incasso_extramav  = " & idIncasso.Value & "))"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtBollette As New Data.DataTable
        da.Fill(dtBollette)
        For Each rBolletta As Data.DataRow In dtBollette.Rows
            par.cmd.CommandText = "update siscom_mi.bol_bollette set " _
                                & "data_pagamento = " & par.insDbValue(Me.txtDataPagamento.Text, True, True) & ", " _
                                & "data_valuta = " & par.insDbValue(Me.txtDataRegistrazione.Text, True, True) & ", " _
                                & "FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1 " _
                                & "where id = " & rBolletta.Item("id").ToString
            par.cmd.ExecuteNonQuery()
            ''NON SERVE PIU' SI AGGIORNA PARTENDO DA INCASSI_EXTRAMAV
            'If Not String.IsNullOrEmpty(rBolletta.Item("data_pagamento").ToString) Then
            '    par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_DATE_PAG (ID_EVENTO,ID_BOLLETTA,DATA_PREC,DATA_VALUTA_PREC) VALUES " _
            '        & "(" & idIncasso.Value & ", " & rBolletta.Item("id").ToString & ", '" & rBolletta.Item("data_pagamento").ToString & "','" & rBolletta.Item("data_valuta").ToString & "')"
            '    par.cmd.ExecuteNonQuery()

            'End If
        Next
    End Sub

  

    Private Function CreditoGestionale(ByVal importo As Decimal, ByRef errore As String, Optional IdTipo As Integer = 4) As Boolean
        CreditoGestionale = False
        errore = "CreditoGestionale"
        Dim dataInizioCompet As String = ""
        Dim dataFineCompet As String = ""
        Dim idBollGest As Long = 0

        If txtDataPagamento.Text <> "" Then
            dataInizioCompet = Right(txtDataPagamento.Text, 4) & txtDataPagamento.Text.Substring(3, 2) & "01"
            dataFineCompet = Right(txtDataPagamento.Text, 4) & txtDataPagamento.Text.Substring(3, 2) & DateTime.DaysInMonth(Right(txtDataPagamento.Text, 4), txtDataPagamento.Text.Substring(3, 2))
        End If
        par.cmd.CommandText = "SELECT siscom_mi.SEQ_BOL_BOLLETTE_GEST.NEXTVAL FROM DUAL"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            idBollGest = myReader(0)
        End If
        myReader.Close()
        Dim NOTE As String
        If Not String.IsNullOrEmpty(Me.txtNumAssegno.Text) Then
            NOTE = "CREDITO MATURATO PER INCASSO MANUALE - ASSEGNO NUM." & Me.txtNumAssegno.Text
        Else
            NOTE = "CREDITO MATURATO PER INCASSO MANUALE"
        End If

        par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                            & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE,ID_EVENTO_PAGAMENTO ) " _
                            & "VALUES (" & idBollGest & "," & vIdContratto.Value & "," & par.RicavaEsercizioCorrente() & "," & vIdUnita.Value & "," & vIdAnagrafica.Value & "," _
                            & "'" & dataInizioCompet & "','" & dataFineCompet & "'," & par.VirgoleInPunti(importo * (-1)) & "," _
                            & "'" & Format(Now, "yyyyMMdd") & "','" & par.AggiustaData(par.IfEmpty(Me.txtDataPagamento.Text, Format(Now, "dd/MM/yyyy"))) & "'," _
                            & "'" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.IfEmpty(txtDataPagamento.Text, Format(Now, "dd/MM/yyyy"))))) & "'" _
                            & "," & IdTipo & ",'N',NULL,'" & NOTE & "'," & idIncasso.Value & ")"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                            & "VALUES (siscom_mi.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",711," & par.VirgoleInPunti(importo * -1) & ")"
        par.cmd.ExecuteNonQuery()
        '02/02/2015 gestione incassi non attribuiti
        If Not IsNothing(Session.Item("importoIncassoNonAttribuito")) And Not IsNothing(Session.Item("IdIncassoNonAttribuito")) Then
            Dim ID_VOCE_GEST As Integer = 0
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.CURRVAL FROM DUAL"
            ID_VOCE_GEST = par.cmd.ExecuteScalar
            RegistraIncassiAttribuiti(ID_VOCE_GEST, importo, idIncasso.Value, idBollGest)
        End If

        errore = ""
        CreditoGestionale = True
    End Function

    '' ''******************************************************************************************************
    '' ''***************************FINE FUNZIONI SALVA PAGAMENTO MANUALE**************************************
    '' ''******************************************************************************************************

#End Region
#Region "PAGA RATEIZZATE"
    Private Sub ControlIsRat(ByVal idVoce As Integer, ByVal importo As Decimal)
        par.cmd.CommandText = "SELECT ID,id_bolletta,id_voce,importo,(SELECT id_tipo FROM siscom_mi.BOL_BOLLETTE WHERE BOL_BOLLETTE.ID = BOL_BOLLETTE_VOCI.id_bolletta ) AS id_tipo FROM siscom_mi.BOL_BOLLETTE_VOCI WHERE ID =" & idVoce
        Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If reader.Read Then
            If par.IfNull(reader("id_tipo"), 1) = "5" Then
                Dim bl As Integer = 0
                par.cmd.CommandText = "select fl_no_report from siscom_mi.t_voci_bolletta where id = " & reader("id_voce")
                bl = par.IfNull(par.cmd.ExecuteScalar, 0)
                If bl = 1 Then
                    par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,(case when (importo - nvl(imp_pagato,0))<0 then 1000000 + abs((importo - nvl(imp_pagato,0))) else (importo - nvl(imp_pagato,0)) end)as ordine " _
                                        & "FROM siscom_mi.BOL_BOLLETTE_VOCI,siscom_mi.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                                        & "WHERE BOL_BOLLETTE_VOCI.id_bolletta = BOL_BOLLETTE.ID AND NVL(BOL_BOLLETTE_VOCI.IMPORTO_RICLASSIFICATO,0) <> 0 " _
                                        & " AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                                        & " AND BOL_BOLLETTE.id_rateizzazione = (SELECT ID_RATEIZZAZIONE FROM siscom_mi.BOL_RATEIZZAZIONI_DETT WHERE ID_BOLLETTA = " & reader("id_bolletta") & ") " _
                                        & " AND FL_ANNULLATA = '0' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL " _
                                        & " ORDER BY BOL_BOLLETTE_VOCI.ID_BOLLETTA ASC, " _
                                        & " ORDINE DESC, GRUPPO ASC, TIPO_VOCE ASC"

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable()
                    da.Fill(dt)
                    PagaVociRat(dt, importo, idVoce)

                End If
            End If
        End If
        reader.Close()
    End Sub
    Private Sub PagaVociRat(ByVal dtVoci As Data.DataTable, ByVal importo As Decimal, ByVal idVoceRata As String)
        Dim disponibilita As Decimal = importo
        Dim impPagato As Decimal = 0
        Dim pagabile As Decimal = 0

        For Each rVoce As Data.DataRow In dtVoci.Rows
            If disponibilita > 0 Then

                pagabile = 0
                impPagato = 0
                If (par.IfEmpty(rVoce("importo").ToString, 0)) < 0 Then
                    'VOCE CON IMPORTO NEGATIVO, LA METTO PAGATA,SCRIVO IN BOL_BOLLETTE_VOCI_PAGAMENTI ED AUMENTO LA DISPONIBILITA SUL PAGAMENTO
                    If (Math.Abs(CDec(par.IfEmpty(rVoce("importo").ToString, 0))) - Math.Abs(CDec(par.IfEmpty(rVoce("imp_pagato").ToString, 0)))) > 0 Then
                        impPagato = Math.Round((Math.Abs(CDec(par.IfEmpty(rVoce("importo").ToString, 0))) - Math.Abs(CDec(par.IfEmpty(rVoce("imp_pagato").ToString, 0)))), 2)
                        disponibilita = disponibilita + impPagato
                        If impPagato > 0 Then
                            par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato = importo,IMPORTO_RICLASSIFICATO_PAGATO = importo_riclassificato  where id =  " & rVoce("id").ToString
                            par.cmd.ExecuteNonQuery()
                            WriteVociPagamenti(impPagato * -1, rVoce("id").ToString, idVoceRata)
                        End If
                    End If
                Else
                    If par.IfEmpty(rVoce.Item("id_voce_aliquota").ToString, 0) > 0 Then
                        'Gestione pagamento parziale aliquota
                        'disponibilita = PagaAliquota(disponibilita, rVoce.Item("id").ToString, rVoce.Item("id_voce_aliquota").ToString, "", idVoceRata)
                    Else
                        'Pagamento parziale voce
                        pagabile = Math.Round(par.IfEmpty(rVoce("importo").ToString, 0) - par.IfEmpty(rVoce("imp_pagato").ToString, 0), 2)
                        If pagabile > 0 Then
                            If disponibilita > pagabile Then
                                impPagato = pagabile
                            Else
                                impPagato = disponibilita

                                'SEGNALAZIONE 476/2016 NON POSSO PAGARE I BOLLI PARZIALMENTE
                                If isPagabileBollo(rVoce("id").ToString, impPagato) = False Then
                                    'salta il pagamento e va alla voce successiva
                                    Continue For
                                End If

                            End If
                            PagaVoce(rVoce("id").ToString, impPagato, False)
                            WriteVociPagamenti(impPagato, rVoce("id").ToString, "")
                            disponibilita = disponibilita - impPagato
                        End If
                    End If
                End If
            Else
                Exit For
            End If

        Next

        '08/10/2018 Se disponibile una parte di quota capitali da ripartire sulle ricla 
        'scrivo una gestionale per successiva integrazione da parte dell'utente
        If disponibilita > 0 And disponibilita > 0.05D Then
            Dim err As String = ""
            If CreditoGestionale(disponibilita, err, 4) = False Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ANOMALIA " & err & "!Nessun dato salvato o modificato!');", True)
                connData.chiudiTransazione(False)
                Exit Sub
            End If


        End If



    End Sub

#End Region

#Region "M.Teresa - PAGA BOLLLETTE MESSE IN MORA"
    Private Sub ControlIsMor(ByVal idVoce As Integer, ByVal importo As Decimal, ByRef IsMorosita As Boolean)

        par.cmd.CommandText = "SELECT ID,id_bolletta,id_voce,importo,(SELECT id_tipo FROM siscom_mi.BOL_BOLLETTE WHERE BOL_BOLLETTE.ID = BOL_BOLLETTE_VOCI.id_bolletta ) AS id_tipo FROM siscom_mi.BOL_BOLLETTE_VOCI WHERE ID =" & idVoce
        Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If reader.Read Then
            If par.IfNull(reader("id_tipo"), 1) = "4" Then
                Dim bl As Integer = 0
                par.cmd.CommandText = "select fl_no_report from siscom_mi.t_voci_bolletta where id = " & reader("id_voce")
                bl = par.IfNull(par.cmd.ExecuteScalar, 0)
                If bl = 1 Then

                    IsMorosita = True
                    par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,(case when (importo - nvl(imp_pagato,0))<0 then 1000000 + abs((importo - nvl(imp_pagato,0))) else (importo - nvl(imp_pagato,0)) end)as ordine " _
                                        & "FROM siscom_mi.BOL_BOLLETTE_VOCI,siscom_mi.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA  " _
                                        & "WHERE BOL_BOLLETTE_VOCI.id_bolletta = BOL_BOLLETTE.ID " _
                                        & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC = " & reader("id_bolletta") & " " _
                                        & " AND FL_ANNULLATA = '0' " _
                                        & " AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                                        & " AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE)" _
                                        & " AND NVL(BOL_BOLLETTE_VOCI.IMPORTO_RICLASSIFICATO,0) <> 0 " _
                                        & " ORDER BY BOL_BOLLETTE_VOCI.ID_BOLLETTA ASC, " _
                                        & " ORDINE DESC, GRUPPO ASC, TIPO_VOCE ASC"

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable()
                    da.Fill(dt)
                    PagaVociRicla(dt, importo)
                End If
            End If
        End If
        reader.Close()

    End Sub
    Private Sub PagaVociRicla(ByVal dtVoci As Data.DataTable, ByVal importo As Decimal)
        Dim disponibilita As Decimal = importo
        Dim impPagato As Decimal = 0
        Dim pagabile As Decimal = 0
        Dim isMor As Boolean = False

        For Each rVoce As Data.DataRow In dtVoci.Rows

            If disponibilita > 0 Then

                pagabile = 0
                impPagato = 0
                If (par.IfNull(rVoce("importo"), 0)) < 0 Then
                    'VOCE CON IMPORTO NEGATIVO, LA METTO PAGATA,SCRIVO IN BOL_BOLLETTE_VOCI_PAGAMENTI ED AUMENTO LA DISPONIBILITA SUL PAGAMENTO
                    If (Math.Abs(CDec(par.IfNull(rVoce("importo"), 0))) - Math.Abs(CDec(par.IfNull(rVoce("imp_pagato"), 0)))) > 0 Then
                        impPagato = Math.Round((Math.Abs(CDec(par.IfNull(rVoce("importo"), 0))) - Math.Abs(CDec(par.IfNull(rVoce("imp_pagato"), 0)))), 2)
                        disponibilita = disponibilita + impPagato
                        If impPagato > 0 Then
                            par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato = importo where id =  " & rVoce("id").ToString
                            par.cmd.ExecuteNonQuery()
                            WriteVociPagamenti(impPagato * -1, rVoce("id").ToString, "")
                        End If
                    End If
                Else
                    If par.IfEmpty(rVoce.Item("id_voce_aliquota").ToString, 0) > 0 Then
                        'Gestione pagamento parziale aliquota
                        'disponibilita = PagaAliquota(disponibilita, rVoce.Item("id").ToString, rVoce.Item("id_voce_aliquota").ToString, "")
                    Else
                        'Pagamento parziale voce
                        pagabile = Math.Round(par.IfNull(rVoce("importo"), 0) - par.IfNull(rVoce("imp_pagato"), 0), 2)
                        If pagabile > 0 Then
                            If disponibilita > pagabile Then
                                impPagato = pagabile
                            Else
                                impPagato = disponibilita

                                'SEGNALAZIONE 476/2016 NON POSSO PAGARE I BOLLI PARZIALMENTE
                                If isPagabileBollo(rVoce("id").ToString, impPagato) = False Then
                                    'salta il pagamento e va alla voce successiva
                                    Continue For
                                End If

                            End If
                            PagaVoce(rVoce("id").ToString, impPagato, isMor)
                            WriteVociPagamenti(impPagato, rVoce("id").ToString, "")
                            disponibilita = disponibilita - impPagato
                        End If
                    End If
                End If
            Else
                Exit For
            End If

        Next
    End Sub

#End Region

    Protected Sub btnIncNonAttrib_Click(sender As Object, e As System.EventArgs) Handles btnIncNonAttrib.Click
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
            Me.NonAttrib.Value = 1
            testo.Visible = True

            Session.Remove("TipoIncassNonAtt")
            If cmbTipoPagamento.SelectedValue = 5 Then
                Me.txtNumAssegno.Style("visibility") = "visible"
                Me.lblNumAssegno.Style("visibility") = "visible"
            Else
                Me.txtNumAssegno.Style("visibility") = "hidden"
                Me.lblNumAssegno.Style("visibility") = "hidden"
            End If
            DettagliIncassoNonAttribuito(Session.Item("IdIncassoNonAttribuito"))

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
                    Me.txtnote.Text = "Incasso non attribuito del " & par.FormattaData(par.IfNull(lettore("data_incasso"), "00000000")) & " disponibilità €." & par.IfNull(lettore("importo_residuo"), "0,00")
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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - AggiungiVoci - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try

    End Sub

    Protected Sub dgvBollVoci_EditCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvBollVoci.EditCommand
        idBollettaMor.Value = e.Item.Cells(par.IndDGC(dgvBollVoci, "ID_BOLLETTA")).Text
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "apriFinestra", "document.getElementById('btnApriDettMor').click();", True)
    End Sub

    Protected Sub btnApriDettMor_Click(sender As Object, e As System.EventArgs) Handles btnApriDettMor.Click
        If IsNothing(Session.Item("PagInMor2")) Then
            Session.Add("PagInMor2", Session.Item("PagInMor"))
            PagInMorosita.Value = CDec(Session.Item("PagInMor2"))
        Else
            PagInMorosita.Value = CDec(Session.Item("PagInMor2")) + CDec(Session.Item("PagInMor"))
        End If
        If Not IsNothing(Session.Item("idIncassoDaMor")) Then
            idIncasso.Value = Session.Item("idIncassoDaMor")
            SumSelected.Value = CDec(SumSelected.Value) + CDec(Session.Item("PagInMor"))
            Me.txtPagResoconto.Text = Me.txtImpPagamento.Text
            Me.txtSommaSel.Text = Me.SumSelected.Value
            Me.txtResResoconto.Text = Me.ResCredito.Value
            'Session.Remove("idIncassoDaMor")
            Session.Remove("PagInMor")

            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('ATTENZIONE!\nL\'incasso sulla bolletta MOR, non è definitivo!\nRicorda di fare click sul pulsante SALVA PAGAMENTO, al termine delle operazioni!');GestTableResidui(" & SumSelected.Value & ");", True)

        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "jsc", "GestTableResidui(document.getElementById('txtImpPagamento'));", True)

        'CaricaBollNonPagate()
    End Sub
    Private Function esclQSind() As Boolean
        esclQSind = False

        Try

            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            End If
            par.cmd.CommandText = "SELECT COUNT(id) FROM SISCOM_MI.TIPO_PAG_PARZ WHERE ID = " & Me.cmbTipoPagamento.SelectedValue & " AND NVL(FL_NO_SALDO,0) = 1"
            Dim noSaldo As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If noSaldo > 0 Then
                esclQSind = True
            End If
            par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.TIPO_PAG_PARZ WHERE ID = " & Me.cmbTipoPagamento.SelectedValue & " AND NVL(FL_NO_CTRL_DATE,0) = 1"
            Dim noDate As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If noDate > 0 Then
                noCtrlDate.Value = 1
            Else
                noCtrlDate.Value = 0
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: cmbTipoPagamento - FiltraFacility - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try

    End Function
    Protected Sub cmbTipoPagamento_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoPagamento.SelectedIndexChanged
        Dim filtro As String = ControlFiltro()
        If filtro = "-1" Then
            filtro = ""
        End If

        If esclQSind() = True Then
            CaricaBollNonPagate(filtro, True)
            txtqs.Visible = True
        Else
            CaricaBollNonPagate(filtro, False)
            txtqs.Visible = False

        End If

    End Sub
End Class
