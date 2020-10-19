Imports Telerik.Web.UI
Imports System.Data

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FattureCDP
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then
            par.caricaComboTelerik("select cod_fornitore||' - '||ragione_sociale as descrizione,id  from siscom_mi.fornitori where id in (select id_fornitore from siscom_mi.pagamenti_utenze_voci where id_tipo_utenza <> 4) order by fornitori.ragione_sociale asc", cmbFornitore, "id", "descrizione", True)
            HFGriglia.Value = dgvFattUtenze.ClientID
            HFAltezzaSottratta.Value = 400
            caricaEsercizioFinanziario()
            CaricaFattureUtenza()
            txtEmDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtEmAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtScadDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtScadAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtCaricDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtCaricAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtInizioDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtInizioAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtFinPerDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtFinPerAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If

    End Sub
    Private Sub CaricaFattureUtenza(Optional ByVal filtro As String = "")
        Try
            connData.apri()
            par.cmd.CommandText = "select fatture_utenze.ID,(SELECT PAGAMENTI.PROGR||'/'||PAGAMENTI.ANNO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO = PAGAMENTI.ID AND PRENOTAZIONI.ID = fatture_utenze.ID_PRENOTAZIONE) AS CDP, " _
                                & " NOME_FILE, " _
                                & " siscom_mi.getdata(DATA_CARICAMENTO) as DATA_CARICAMENTO, " _
                                & " FORNITORI.RAGIONE_SOCIALE AS rag_sociale," _
                                & " (SELECT GETDATA(INIZIO)||' - '||GETDATA(FINE) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID = (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF))) AS PIANO, " _
                                & " siscom_mi.getdata(DATA_EMISSIONE) as data_emissione, " _
                                & " siscom_mi.getdata(DATA_SCADENZA) as data_scadenza, " _
                                & " NUMERO_FATTURA, " _
                                & " ANNO_FATTURA, " _
                                & " siscom_mi.getdata(DATA_INIZIO_PERIODO) as data_inizio_periodo, " _
                                & " siscom_mi.getdata(DATA_FINE_PERIODO) as data_fine_periodo, " _
                                & " POD," _
                                & " NOME_VIA_FORNITURA, " _
                                & " NUMERO_CIVICO_FORNITURA, " _
                                & " BARRATO_FORNITURA, " _
                                & " CAP_FORNITURA, " _
                                & " LOCALITA_FORNITURA, " _
                                & " PROVINCIA_FORNITURA," _
                                & " (select (PF_VOCI.CODICE /*||' '||PF_VOCI.DESCRIZIONE*/) from siscom_mi.pf_voci where id = id_voce_pf) as VOCE_BP, " _
                                & " trim(TO_CHAR((TOTALE_ONERI_DIVERSI),'9G999G999G999G999G990D99')) AS FORM_TOTALE_ONERI_DIVERSI," _
                                & " trim(TO_CHAR((BASE_IMPONIBILE),'9G999G999G999G999G990D99')) AS FORM_BASE_IMPONIBILE , " _
                                & " trim(TO_CHAR((IVA),'9G999G999G999G999G990D99')) AS FORM_IVA," _
                                & " trim(TO_CHAR((TOTALE_BOLLETTA),'9G999G999G999G999G990D99')) AS FORM_TOTALE_BOLLETTA," _
                                & " trim(TO_CHAR((TOTALE_BOLLETTINO),'9G999G999G999G999G990D99')) AS FORM_TOTALE_BOLLETTINO " _
                                & " FROM siscom_mi.FATTURE_UTENZE, siscom_mi.FORNITORI,siscom_mi.PAGAMENTI_UTENZE_VOCI " _
                                & " WHERE PAGAMENTI_UTENZE_VOCI.ID = FATTURE_UTENZE. ID_PARAM_UTENZA AND FORNITORI.ID = PAGAMENTI_UTENZE_VOCI.id_fornitore AND id_prenotazione is not null " & filtro & " order by id_prenotazione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            BindDGV(dt)
            Me.lblTitolo.Text = "Utenze - Fatture con CDP - record trovati: " & dt.Rows.Count
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaModalita - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub
    Private Sub BindDGV(ByVal dt As Data.DataTable)
        Session.Add("dtFatUt", dt)
        Me.dgvFattUtenze.DataSource = dt
        Me.dgvFattUtenze.DataBind()

    End Sub

    Protected Sub dgvFattUtenze_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvFattUtenze.ItemDataBound
        'If e.Item.ItemType = ListItemType.Item Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
        '    e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
        '    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';")
        '    'DirectCast(e.Item.FindControl("chkSelezione"), CheckBox).Attributes.Add("onchange", "IsSelected(this)")
        'ElseIf e.Item.ItemType = ListItemType.AlternatingItem Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
        '    e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='gainsboro'}")
        '    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';")
        '    'DirectCast(e.Item.FindControl("chkSelezione"), CheckBox).Attributes.Add("onchange", "IsSelected(this)")

        'End If

    End Sub
    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Cerca()
    End Sub

    Private Sub Cerca()
        Dim strFiltro As String = ""
        Dim primo As Boolean = False

        If Not String.IsNullOrEmpty(Me.txtEmDal.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "
            End If

            strFiltro += " data_emissione >= '" & par.AggiustaData(Me.txtEmDal.Text) & "'"
        End If
        If Not String.IsNullOrEmpty(Me.txtEmAl.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " data_emissione <= '" & par.AggiustaData(Me.txtEmAl.Text) & "'"

        End If

        If Not String.IsNullOrEmpty(Me.txtScadDal.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " data_scadenza >= '" & par.AggiustaData(Me.txtScadDal.Text) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtScadAl.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " data_scadenza <= '" & par.AggiustaData(Me.txtScadAl.Text) & "'"

        End If
        '************FILTRO DATA CARICAMENTO*****************
        If Not String.IsNullOrEmpty(Me.txtCaricDal.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_CARICAMENTO >= '" & par.AggiustaData(Me.txtCaricDal.Text) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtCaricAl.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_CARICAMENTO <= '" & par.AggiustaData(Me.txtCaricAl.Text) & "'"

        End If

        '************FILTRO DATA INIZIO PERIODO*****************
        If Not String.IsNullOrEmpty(Me.txtInizioDal.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_INIZIO_PERIODO >= '" & par.AggiustaData(Me.txtInizioDal.Text) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtInizioAl.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_INIZIO_PERIODO <= '" & par.AggiustaData(Me.txtInizioAl.Text) & "'"

        End If

        '************FILTRO DATA FINE PERIODO*****************
        If Not String.IsNullOrEmpty(Me.txtFinPerDal.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_FINE_PERIODO >= '" & par.AggiustaData(Me.txtFinPerDal.Text) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtFinPerAl.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_FINE_PERIODO <= '" & par.AggiustaData(Me.txtFinPerAl.Text) & "'"

        End If
        '************FILTRO POD*****************

        If Not String.IsNullOrEmpty(Me.txtPOD.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " pod like '" & par.PulisciStrSql(Me.txtPOD.Text.Replace("*", "%")) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtNumFattura.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " NUMERO_FATTURA like '" & par.PulisciStrSql(Me.txtNumFattura.Text.Replace("*", "%")) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtAnno.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " ANNO_FATTURA like '" & par.PulisciStrSql(Me.txtAnno.Text.Replace("*", "%")) & "'"

        End If
        If Me.cmbFornitore.SelectedValue <> "-1" Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If

            strFiltro += " id_fornitore = " & Me.cmbFornitore.SelectedValue
        End If
        If EsercizioFinanziario.SelectedValue <> "-1" Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "
            End If

            strFiltro += " ID_PIANO_FINANZIARIO IN (SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & EsercizioFinanziario.SelectedValue & ") "
        End If



        CaricaFattureUtenza(strFiltro)
    End Sub

    Protected Sub btnPulisci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPulisci.Click
        Pulisci()
    End Sub
    Private Sub Pulisci()
        Me.txtEmDal.Text = ""
        Me.txtEmAl.Text = ""
        Me.txtScadDal.Text = ""
        Me.txtScadAl.Text = ""
        Me.txtPOD.Text = ""
        Me.txtNumFattura.Text = ""
        Me.txtAnno.Text = ""
        Me.cmbFornitore.SelectedValue = "-1"
        Me.txtCaricDal.Text = ""
        Me.txtCaricAl.Text = ""
        Me.txtInizioDal.Text = ""
        Me.txtInizioAl.Text = ""
        Me.txtFinPerDal.Text = ""
        Me.txtFinPerAl.Text = ""
        CaricaFattureUtenza()

    End Sub

    Sub cambiataPagina(ByVal numero As Integer)
        If numero >= 0 Then
            'Label3.Text = "0"
            dgvFattUtenze.CurrentPageIndex = numero
            BindDGV(CType(Session.Item("dtFatUt"), Data.DataTable))
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        End If
    End Sub

    Protected Sub btnExportXls_Click(sender As Object, e As System.EventArgs) Handles btnExportXls.Click
        Dim dtNEW As New Data.DataTable
        dtNEW.Merge(CType(Session.Item("dtFatUt"), Data.DataTable))
        dtNEW.Columns.Remove("ID")
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "FtUtenze", "Export", dtNEW)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub dgvFattUtenze_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles dgvFattUtenze.PageIndexChanged
        cambiataPagina(e.NewPageIndex)
    End Sub

    Private Sub cmbFornitore_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbFornitore.SelectedIndexChanged
        Cerca()
    End Sub

    Private Sub dgvFattUtenze_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvFattUtenze.NeedDataSource
        Try
            connData.apri()
            Dim strFiltro As String = ""
            Dim primo As Boolean = False

            If Not String.IsNullOrEmpty(Me.txtEmDal.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "
                End If

                strFiltro += " data_emissione >= '" & par.AggiustaData(Me.txtEmDal.Text) & "'"
            End If
            If Not String.IsNullOrEmpty(Me.txtEmAl.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " data_emissione <= '" & par.AggiustaData(Me.txtEmAl.Text) & "'"

            End If

            If Not String.IsNullOrEmpty(Me.txtScadDal.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " data_scadenza >= '" & par.AggiustaData(Me.txtScadDal.Text) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtScadAl.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " data_scadenza <= '" & par.AggiustaData(Me.txtScadAl.Text) & "'"

            End If
            '************FILTRO DATA CARICAMENTO*****************
            If Not String.IsNullOrEmpty(Me.txtCaricDal.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_CARICAMENTO >= '" & par.AggiustaData(Me.txtCaricDal.Text) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtCaricAl.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_CARICAMENTO <= '" & par.AggiustaData(Me.txtCaricAl.Text) & "'"

            End If

            '************FILTRO DATA INIZIO PERIODO*****************
            If Not String.IsNullOrEmpty(Me.txtInizioDal.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_INIZIO_PERIODO >= '" & par.AggiustaData(Me.txtInizioDal.Text) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtInizioAl.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_INIZIO_PERIODO <= '" & par.AggiustaData(Me.txtInizioAl.Text) & "'"

            End If

            '************FILTRO DATA FINE PERIODO*****************
            If Not String.IsNullOrEmpty(Me.txtFinPerDal.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_FINE_PERIODO >= '" & par.AggiustaData(Me.txtFinPerDal.Text) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtFinPerAl.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_FINE_PERIODO <= '" & par.AggiustaData(Me.txtFinPerAl.Text) & "'"

            End If
            '************FILTRO POD*****************

            If Not String.IsNullOrEmpty(Me.txtPOD.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " pod like '" & par.PulisciStrSql(Me.txtPOD.Text.Replace("*", "%")) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtNumFattura.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " NUMERO_FATTURA like '" & par.PulisciStrSql(Me.txtNumFattura.Text.Replace("*", "%")) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtAnno.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " ANNO_FATTURA like '" & par.PulisciStrSql(Me.txtAnno.Text.Replace("*", "%")) & "'"

            End If
            If Me.cmbFornitore.SelectedValue <> "-1" Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If

                strFiltro += " id_fornitore = " & Me.cmbFornitore.SelectedValue
            End If
            If EsercizioFinanziario.SelectedValue <> "-1" Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "
                End If

                strFiltro += " ID_PIANO_FINANZIARIO IN (SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & EsercizioFinanziario.SelectedValue & ") "
            End If

            par.cmd.CommandText = "select fatture_utenze.ID,(SELECT PAGAMENTI.PROGR||'/'||PAGAMENTI.ANNO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO = PAGAMENTI.ID AND PRENOTAZIONI.ID = fatture_utenze.ID_PRENOTAZIONE) AS CDP, " _
                                & " NOME_FILE, " _
                                & " siscom_mi.getdata(DATA_CARICAMENTO) as DATA_CARICAMENTO, " _
                                & " FORNITORI.RAGIONE_SOCIALE AS rag_sociale," _
                                & " (SELECT GETDATA(INIZIO)||' - '||GETDATA(FINE) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID = (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF))) AS PIANO, " _
                                & " siscom_mi.getdata(DATA_EMISSIONE) as data_emissione, " _
                                & " siscom_mi.getdata(DATA_SCADENZA) as data_scadenza, " _
                                & " NUMERO_FATTURA, " _
                                & " ANNO_FATTURA, " _
                                & " siscom_mi.getdata(DATA_INIZIO_PERIODO) as data_inizio_periodo, " _
                                & " siscom_mi.getdata(DATA_FINE_PERIODO) as data_fine_periodo, " _
                                & " POD," _
                                & " NOME_VIA_FORNITURA, " _
                                & " NUMERO_CIVICO_FORNITURA, " _
                                & " BARRATO_FORNITURA, " _
                                & " CAP_FORNITURA, " _
                                & " LOCALITA_FORNITURA, " _
                                & " PROVINCIA_FORNITURA," _
                                & " (select (PF_VOCI.CODICE /*||' '||PF_VOCI.DESCRIZIONE*/) from siscom_mi.pf_voci where id = id_voce_pf) as VOCE_BP, " _
                                & " trim(TO_CHAR((TOTALE_ONERI_DIVERSI),'9G999G999G999G999G990D99')) AS FORM_TOTALE_ONERI_DIVERSI," _
                                & " trim(TO_CHAR((BASE_IMPONIBILE),'9G999G999G999G999G990D99')) AS FORM_BASE_IMPONIBILE , " _
                                & " trim(TO_CHAR((IVA),'9G999G999G999G999G990D99')) AS FORM_IVA," _
                                & " trim(TO_CHAR((TOTALE_BOLLETTA),'9G999G999G999G999G990D99')) AS FORM_TOTALE_BOLLETTA," _
                                & " trim(TO_CHAR((TOTALE_BOLLETTINO),'9G999G999G999G999G990D99')) AS FORM_TOTALE_BOLLETTINO " _
                                & " FROM siscom_mi.FATTURE_UTENZE, siscom_mi.FORNITORI,siscom_mi.PAGAMENTI_UTENZE_VOCI " _
                                & " WHERE PAGAMENTI_UTENZE_VOCI.ID = FATTURE_UTENZE. ID_PARAM_UTENZA AND FORNITORI.ID = PAGAMENTI_UTENZE_VOCI.id_fornitore AND id_prenotazione is not null " & strFiltro & " order by id_prenotazione asc"
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Session.Add("dtFatUt", dt)

            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            Me.lblTitolo.Text = "Utenze - Fatture con CDP - record trovati: " & dt.Rows.Count
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaModalita - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Private Sub caricaEsercizioFinanziario()
        par.caricaComboTelerik("SELECT ID,SISCOM_MI.GETDATA(INIZIO)||' - '||SISCOM_MI.GETDATA(FINE) AS ANNO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN) ORDER BY ID DESC", EsercizioFinanziario, "ID", "ANNO", True)
    End Sub

    Private Sub CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FattureCDP_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class
